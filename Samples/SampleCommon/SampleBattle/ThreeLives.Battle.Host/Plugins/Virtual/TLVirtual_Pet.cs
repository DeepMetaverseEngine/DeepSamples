using DeepCore.GameHost.Instance;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Units;

namespace TLBattle.Server.Plugins.Virtual
{
    /// <summary>
    /// 宠物.
    /// </summary>
    public class TLVirtual_Pet : TLVirtual
    {

        private TLVirtual_Player mMaster = null;

        private uint mCurAtkTarget = 0;

        public TLVirtual_Pet(InstanceUnit unit) : base(unit)
        {
        }

        public override bool IsPlayerUnit()
        {
            return true;
        }

        public override TLVirtual GetPlayerUnit()
        {
            return mMaster;
        }

        public override string GetPlayerUUID()
        {
            if (mMaster != null)
            {
                return mMaster.GetPlayerUUID();
            }
            return null;
        }

        public override string GuildUUID()
        {
            if (mMaster != null)
            {
                return mMaster.GuildUUID();
            }
            return null;
        }

        protected override void DoDispose(InstanceUnit owner)
        {
            if (mMaster != null)
            {
                mMaster.OnAtkUnit -= TLVirtual_Pet_OnAtkUnit;
                mMaster.OnCombatStateChangeHandle -= MMaster_OnCombatStateChangeHandle;
            }

            base.DoDispose(owner);
        }

        public void InitVisibleData(PetBaseInfo info)
        {
            PetVisibleDataB2C data = new PetVisibleDataB2C();
            data.BaseInfo = info;
            mUnit.Level = info.level;
            mUnit.SetVisibleInfo(data);
        }

        public void SetMaster(TLVirtual master)
        {
            mMaster = master as TLVirtual_Player;

            if (this.mUnit != null)
            {
                (this.mUnit as TLInstancePet).Master = mMaster.mUnit;
                (this.mUnit as TLInstancePet).SummonerUnit = mMaster.mUnit;

                mMaster.OnAtkUnit += TLVirtual_Pet_OnAtkUnit;
                mMaster.OnCombatStateChangeHandle += MMaster_OnCombatStateChangeHandle;
            }
        }

        private void MMaster_OnCombatStateChangeHandle(TLVirtual unit, CombatStateChangeEventB2C.BattleStatus status)
        {
            if (mUnit != null)
            {
                (mUnit as TLInstancePet).MasterCombatstateChange(status);
            }
        }

        private void TLVirtual_Pet_OnAtkUnit()
        {
            if (mMaster != null)
            {
                SetCurAtkTarget(mMaster.GetLastAtkUnit());
                (mUnit as TLInstancePet).MasterBeginAtkUnit();
            }

        }

        private void SetCurAtkTarget(uint id)
        {
            mCurAtkTarget = id;

        }

        public uint GetCurAtkTarget()
        {
            return mCurAtkTarget;
        }

        public void UpdateBaseInfo(PetBaseInfo info)
        {
            if (info != null)
            {
                if (info.level != GetUnitLv())
                {
                    //升级回满血.
                    AddHP(MirrorProp.MaxHP, mUnit, false);
                }
                InitVisibleData(info);
            }
        }

        public void UpdatePetProps(TLUnitProp prop)
        {
            this.PropModule.PropChange(prop);
        }

        public void UpdateSkillInfo(TLUnitSkillInfo info)
        {
            //TODO.
            this.SkillModule.ReplaceSkill(info.Skills);
        }

    }
}
