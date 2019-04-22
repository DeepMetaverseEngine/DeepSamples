using System.Collections.Generic;
using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLBattle.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLBattle.Server.Plugins.Units;

namespace TLBattle.Server.Plugins.Virtual
{
    public class TLVirtual_PlayerMirror : TLVirtual_Monster
    {
        /// <summary>
        /// 宠物.
        /// </summary>
        private TLVirtual_Pet mPet = null;

        public TLVirtual_PlayerMirror(InstanceUnit unit) : base(unit)
        {

        }

        public override TLVirtual GetPlayerUnit()
        {
            return null;
        }

        public override string GetPlayerUUID()
        {
            return "";
        }

        protected override void DoInit()
        {
            InitBaseData(mProp.ServerData.Prop);
            ChangePet(mProp.ServerData.UnitPetData);
        }

        protected override void OnRecoveryTimerTick()
        {
            base.OnRecoveryTimerTick();
            if (mUnit.IsDead == false && InGodStatus() == false)
            {
                AddMP(TLDataMgr.GetInstance().GameConfigData.GetAngerRecovery(), false);
            }

        }
        private int GetGodBuffID()
        {
            if (mProp.ServerData != null && mProp.ServerData.UnitGodData != null)
            {
                return mProp.ServerData.UnitGodData.BuffID;
            }

            return 0;
        }

        private bool InGodStatus()
        {
            int buffID = GetGodBuffID();
            if (buffID == 0) return false;

            var b = this.mUnit.GetBuffByID(buffID);
            if (b == null)
                return false;
            return true;
        }

        private void ChangePet(PetData data)
        {
            DismissPet();

            SummonPet(data);
        }

        private void DismissPet()
        {
            if (mPet != null)
            {
                mPet.mUnit.OnDead -= Pet_OnDead;
                this.mUnit.Parent.RemoveObjectByID(mPet.mUnit.ID);
                mPet = null;
            }
        }

        private void SummonPet(PetData data)
        {
            if (data == null) return;

            //添加宠物.
            UnitInfo info = TLBattleSkill.GetUnitInfo(data.EditorID);

            if (info == null) return;

            var pos = mUnit.Parent.PathFinder.Terrain.FindNearRandomMoveableNode(
                  mUnit.RandomN,
                      mUnit.X,
                      mUnit.Y,
                      2, true);

            TLUnitProperties prop = info.Properties as TLUnitProperties;

            prop.ServerData.BaseInfo.UnitLv = data.BaseInfo.level;
            prop.ServerData.BaseInfo.Name = data.BaseInfo.name;

            prop.ServerData.Prop = data.UnitProp;
            prop.ServerData.SkillInfo = data.SkillInfo;

            var evt = new AddUnit();
            {
                evt.info = info;
                evt.editor_name = info.Name;
                evt.player_uuid = info.Name;
                evt.force = mUnit.Force;
                evt.level = data.BaseInfo.level;
                evt.pos = pos;
                evt.direction = mUnit.Direction;
                evt.summoner = mUnit;
            }

            var unit = mUnit.Parent.AddUnit(evt) as TLInstancePet;
            if (unit != null)
            {
                unit.OnDead += Pet_OnDead;
                unit.SetRebirthTime(data.RebirthTimeMS);
                var petVirtual = (unit.Virtual) as TLVirtual_Pet;
                mPet = petVirtual;
                data.BaseInfo.MasterID = this.mUnit.ID;
                mPet.InitVisibleData(data.BaseInfo);
                mPet.SetMaster(this);
            }

        }
        private GodData GetGodData()
        {
            return mProp.ServerData?.UnitGodData;
        }
        public override GameSkill GetGodMainSkill()
        {
            var data = GetGodData();
            if (data != null && data.SkillInfo != null)
            {
                foreach (var item in data.SkillInfo)
                {
                    if (item.Value.SkillType == GameSkill.TLSkillType.God)
                    {
                        return item.Value;
                    }
                }
            }

            return null;
        }
        public override HashMap<int, GameSkill> GetGodSkill()
        {
            HashMap<int, GameSkill> ret = new HashMap<int, GameSkill>();
            GodData godData = GetGodData();
            if (godData == null)
            {
                return null;
            }
            var map = godData.SkillInfo;
            if (map == null)
            {
                return null;
            }
            foreach (var item in map)
            {
                if (item.Value.SkillType != GameSkill.TLSkillType.God)
                {
                    ret.Add(item.Key, item.Value);
                }
            }

            return ret;
        }
        private void Pet_OnDead(InstanceUnit unit, InstanceUnit attacker)
        {
            //通知逻辑服仙侣死亡.
            //变更仙侣状态.
        }

        public override void DataAddSkill(List<GameSkill> lt, bool syncSkillModule)
        {
            var src = mProp.ServerData.SkillInfo.Skills;
            src.AddRange(lt);
            if (syncSkillModule && InGodStatus() == false)
            {
                this.SkillModule.AddSkill(lt);
            }
        }

        public override void DataRemoveSkill(List<GameSkill> lt, bool syncSkillModule)
        {
            var src = mProp.ServerData.SkillInfo.Skills;

            List<GameSkill> rmlist = new List<GameSkill>();

            for (int i = 0; i < lt.Count; i++)
            {
                for (int k = 0; k < src.Count; k++)
                {
                    if (lt[i].SkillID == src[k].SkillID)
                    {
                        rmlist.Add(src[k]);
                    }
                }
            }

            for (int i = 0; i < rmlist.Count; i++)
            {
                src.Remove(rmlist[i]);
            }

            if (syncSkillModule && InGodStatus() == false)
            {
                this.SkillModule.RemoveSkill(lt);
            }
        }

        public void DataResetSkill(List<GameSkill> lt, bool syncSkillModule)
        {
            mProp.ServerData.SkillInfo.Skills = lt;
            if (syncSkillModule && InGodStatus() == false)
            {
                this.SkillModule.ResetSkill(lt);
            }
        }

        protected override void InitBaseData(TLUnitProp data)
        {
            base.InitBaseData(data);
            //客户端信息.
            PlayerVisibleDataB2C b2c = new PlayerVisibleDataB2C
            {
                BaseInfo = mProp.ServerData.BaseInfo,
                AvatarMap = mProp.ServerData.AvatarMap,
                UnitPKInfo = mProp.ServerData.UnitPKInfo,
            };
            this.mUnit.SetVisibleInfo(b2c);
            this.mUnit.set_level(mProp.ServerData.BaseInfo.UnitLv);
            this.mUnit.SetMaxMP(TLDataMgr.GetInstance().GameConfigData.GetAngerLimit(), false);
            this.mUnit.CurrentHP = this.mUnit.MaxHP;
            this.mUnit.CurrentMP = 0;
        }
        public override string Name()
        {
            return (this.mInfo.Properties as TLUnitProperties).ServerData.BaseInfo.Name;
        }

        public override bool IsPlayerUnit()
        {
            return true;
        }

    }
}
