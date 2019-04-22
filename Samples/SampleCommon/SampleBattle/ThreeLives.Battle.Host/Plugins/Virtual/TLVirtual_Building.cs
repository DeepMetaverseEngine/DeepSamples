using DeepCore.GameHost.Instance;
using TLBattle.Common.Data;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Plugins;
using TLBattle.Server.Scene;

namespace TLBattle.Server.Plugins.Virtual
{
    public class TLVirtual_Building : TLVirtual
    {
        public TLVirtual_Building(InstanceUnit unit) : base(unit)
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
            bool isDynamic = false;
            if (md != null)
            {
                data = md.GetMonsterData(es.Data.ID, mUnit.Info.ID);
                cofData = md.GetMonsterCofData(es.PlayerLv);
                if (data.dynamic == 1)
                    isDynamic = true;
                //非动态等级模式下，创建空数据.
                if (isDynamic == false && cofData == null)
                    cofData = new TLMonsterCofData();
            }

            if (data != null && cofData != null)
            {
                //战斗属性.
                TLUnitProp prop = mProp.ServerData.Prop;

                //TODO 生命独立计算.
                prop.MaxHP = CalProp(isDynamic, data.maxhp, cofData.maxhp);
                int lv = data.level;
                if (es.TeamDynamic)//队伍动态属性场景，更改生命值.
                {
                    prop.MaxHP = CalProp(es.TeamDynamic, prop.MaxHP,
                                                   TLDataMgr.GetInstance().GameConfigData.GetDungeonDynamicCof(es.PlayerCount));

                    lv = es.PlayerLv;
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
                mProp.ServerData.BaseInfo.UnitLv = lv;

                //移动速度.
                this.mUnit.SetMoveSpeed(prop.RunSpeed);

                //客户端信息.
                MonsterVisibleDataB2C b2c = new MonsterVisibleDataB2C();
                b2c.Lv = lv;
                b2c.Name = data.name;
                b2c.Type = (MonsterVisibleDataB2C.MonsterType)data.type;
                if (mInfo.GuardRange == 0)
                {
                    b2c.Tendency = MonsterVisibleDataB2C.AtkTendency.Passive;
                }
                else { b2c.Tendency = MonsterVisibleDataB2C.AtkTendency.Active; }

                this.mUnit.SetVisibleInfo(b2c);
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

        public override string Name()
        {
            return (mUnit.VisibleInfo as MonsterVisibleDataB2C).Name;
        }

        public override bool IsBuilding()
        {
            return true;
        }
    }
}
