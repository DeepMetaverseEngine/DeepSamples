using DeepCore;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using TLBattle.Common.Data;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Plugins;
using TLBattle.Server.Scene;
using TLBattle.Server.Message;

namespace TLBattle.Server.Plugins.Virtual
{
    public class TLVirtual_Monster : TLVirtual
    {
        private int mHPDropID = 0;

        public TLVirtual_Monster(InstanceUnit unit) : base(unit)
        {

        }

        protected override void DoInit()
        {
            InitMonsterProp();
        }

        private void InitMonsterProp()
        {
            TLEditorScene es = mUnit.Parent as TLEditorScene;
            var md = TLDataMgr.GetInstance().MonsterData;
            TLMonsterData data = null;
            TLMonsterCofData cofData = null;
            int lv = 0;
            if (md != null)
            {
                data = md.GetMonsterData(es.Data.ID, mUnit.Info.ID);
                if (es.TeamDynamic)
                {
                    lv = es.TeamLeaderLv;
                    if (lv == 0)//没有组队时取创建者等级.
                        lv = es.PlayerLv;
                }

                else
                    lv = es.PlayerLv;
                cofData = md.GetMonsterCofData(lv);
            }

            if (data != null && cofData != null)
            {
                bool isDynamic = false;
                if (data.dynamic == 1)
                    isDynamic = true;

                //战斗属性.
                TLUnitProp prop = mProp.ServerData.Prop;

                //TODO 生命独立计算.
                prop.MaxHP = CalProp(isDynamic, data.maxhp, cofData.maxhp);
                int monsterLv = data.level;
                if (es.TeamDynamic)//队伍动态属性场景，更改生命值.
                {
                    prop.MaxHP = CalProp(es.TeamDynamic, prop.MaxHP,
                                         TLDataMgr.GetInstance().GameConfigData.GetDungeonDynamicCof(es.PlayerCount));

                    monsterLv = lv;
                }

                prop.CurHP = prop.MaxHP;

                prop.Attack = CalProp(isDynamic, data.attack, cofData.attack);
                prop.PhyDef = CalProp(isDynamic, data.defend, cofData.defend);
                prop.MagDef = CalProp(isDynamic, data.mdef, cofData.mdef);

                prop.Through = CalProp(isDynamic, data.through, cofData.through);
                prop.Block = CalProp(isDynamic, data.block, cofData.block);

                prop.Hit = CalProp(isDynamic, data.hit, cofData.hit);
                prop.Dodge = CalProp(isDynamic, data.dodge, cofData.dodge);

                prop.Crit = CalProp(isDynamic, data.crit, cofData.crit);
                prop.ResCrit = CalProp(isDynamic, data.rescrit, cofData.rescrit);

                prop.CriDamagePer = CalProp(isDynamic, data.cridamageper, cofData.cridamageper);
                prop.RedCriDamagePer = CalProp(isDynamic, data.redcridamageper, cofData.redcirdamageper);

                prop.AutoRecoverHp = CalProp(isDynamic, data.autorecoverhp, cofData.autorecoverhp);
                prop.OnHitRecoverHP = CalProp(isDynamic, data.onhitrecoverhp, cofData.onhitrecoverhp);
                prop.RunSpeed = CalProp(isDynamic, data.runspeed, cofData.runspeed);
                prop.GodDamage = CalProp(isDynamic, data.goddamage, cofData.goddamage);


                //服务端信息.
                mProp.ServerData.BaseInfo.Name = data.name;
                mProp.ServerData.BaseInfo.UnitLv = monsterLv;
                mProp.ServerData.BaseInfo.TitleID = data.title;

                mUnit.set_level(monsterLv);
                //客户端信息.
                MonsterVisibleDataB2C b2c = new MonsterVisibleDataB2C();
                b2c.Lv = monsterLv;
                b2c.Name = data.name;
                b2c.Type = (MonsterVisibleDataB2C.MonsterType)data.type;
                b2c.TitleID = data.title;
                if (mInfo.GuardRange == 0)
                {
                    b2c.Tendency = MonsterVisibleDataB2C.AtkTendency.Passive;
                }
                else { b2c.Tendency = MonsterVisibleDataB2C.AtkTendency.Active; }

                this.mUnit.SetVisibleInfo(b2c);


                if (data.reward_type == 2)//伤害达成奖励.
                {
                    var t = TLDataMgr.GetInstance().MonsterData.GetMonsterHPDrop(es.Data.ID, mUnit.Info.ID);

                    int[] hpDropInfo = null;
                    hpDropInfo = t.Value;
                    mHPDropID = t.Key;

                    if (hpDropInfo != null && hpDropInfo.Length > 0)
                    {
                        mHateSystem.SetHPDropInfo(hpDropInfo);
                        mHateSystem.HPDropInfoHandle += MHateSystem_DamageDropInfoHandle;
                    }

                }
                else if (data.reward_type == 3)//首刀.
                {
                    mHateSystem.FirstAtkerChangeHandle += MHateSystem_FirstAtkerChangeHandle;
                    b2c.ShowOwnerShip = true;
                }

            }
            else
            {
                //没数据走编辑器.
                MonsterVisibleDataB2C b2c = new MonsterVisibleDataB2C();
                b2c.Lv = mUnit.Level;
                b2c.Name = "error: " + mInfo.ID;//mUnit.Info.Name;
                b2c.Type = MonsterVisibleDataB2C.MonsterType.Normal;
                if (mInfo.GuardRange == 0) b2c.Tendency = MonsterVisibleDataB2C.AtkTendency.Passive;
                else { b2c.Tendency = MonsterVisibleDataB2C.AtkTendency.Active; }

                this.mUnit.SetVisibleInfo(b2c);
            }

        }

        private void MHateSystem_FirstAtkerChangeHandle(string last, string cur)
        {
            var visibleData = this.mUnit.VisibleInfo as MonsterVisibleDataB2C;

            if (string.IsNullOrEmpty(last) && !string.IsNullOrEmpty(cur))//首刀生效通知.
            {
                MonsterOwnerShipChangeEventB2C evt = new MonsterOwnerShipChangeEventB2C();
                evt.s2c_uuid = cur;
                if (visibleData != null) visibleData.OwnerShipUUID = cur;
                this.mUnit.queueEvent(evt);
            }

            if (!string.IsNullOrEmpty(last) && string.IsNullOrEmpty(cur))//首刀取消通知.
            {
                MonsterOwnerShipChangeEventB2C evt = new MonsterOwnerShipChangeEventB2C();
                evt.s2c_uuid = cur;
                if (visibleData != null) visibleData.OwnerShipUUID = cur;
                this.mUnit.queueEvent(evt);
            }
        }

        private int CalProp(bool dynamic, int src, int add)
        {
            if (dynamic)
            {
                return (int)(src * (add / TLBattlePropModule.PER));
            }
            else
            {
                return src;
            }
        }
        protected override void InitBattlePropModule()
        {
            base.InitBattlePropModule();
        }

        internal override int OnHit(TLVirtual attacker, AttackSource source)
        {
            //怪物回程状态不能被攻击.
            if (this.mUnit.CurrentState is InstanceUnit.StateBackToPosition)
            {
                source.OutIsDamage = false;
                source.OutClientState = (int)BattleAtkNumberEventB2C.AtkNumberType.Immunity;
                return 0;
            }

            return base.OnHit(attacker, source);
        }

        public HashMap<string, int> GetAtkDataMap()
        {
            return mHateSystem.AtkDataMap;
        }

        public int GetMonsterID()
        {
            return mInfo.ID;
        }

        public override void SetCombatState(CombatStateChangeEventB2C.BattleStatus value, byte reason = 0)
        {
            //怪物强制转为PVE.
            if (value == CombatStateChangeEventB2C.BattleStatus.PVP)
            {
                value = CombatStateChangeEventB2C.BattleStatus.PVE;
            }
            base.SetCombatState(value, reason);
        }

        public override string Name()
        {
            return (mUnit.VisibleInfo as MonsterVisibleDataB2C).Name;
        }

        protected override void DoDispose(InstanceUnit owner)
        {
            if (mHateSystem != null)
            {
                mHateSystem.HPDropInfoHandle -= MHateSystem_DamageDropInfoHandle;
                mHateSystem.FirstAtkerChangeHandle -= MHateSystem_FirstAtkerChangeHandle;
            }

            base.DoDispose(owner);
        }

        private void MHateSystem_DamageDropInfoHandle(int[] obj)
        {
            if (mHateSystem != null)
            {
                var lt = mHateSystem.GetAtkerList();
                InstancePlayer player = null;
                if (lt != null && lt.Count > 0)
                {
                    for (int i = 0; i < lt.Count; i++)
                    {
                        player = this.mUnit.Parent.getPlayerByUUID(lt[i]);
                        if (player != null)
                        {
                            var evt = new MonsterDropEventB2R();
                            TLEditorScene es = mUnit.Parent as TLEditorScene;
                            evt.dropID = this.mHPDropID;
                            evt.monsterID = this.mUnit.Info.ID;
                            evt.sceneID = es.Data.ID;
                            evt.Drop = obj;
                            evt.x = this.mUnit.X;
                            evt.y = this.mUnit.Y;
                            player.queueEvent(evt);
                        }
                    }

                }
            }
        }


    }
}
