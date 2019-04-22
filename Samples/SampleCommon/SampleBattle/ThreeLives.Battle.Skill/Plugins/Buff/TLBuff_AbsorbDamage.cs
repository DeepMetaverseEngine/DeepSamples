using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.Buff;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff_AbsorbDamage : TLBuff
    {
        /// <summary>
        /// 吸收伤害总量.
        /// </summary>
        public int AbsorbDamageSum = 0;

        /// <summary>
        /// 是否吸收过量伤害.
        /// </summary>
        public bool AbsorbOverFlowDamage = false;

        private int mHandleUUID = 0;

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            GameSkill gs = new GameSkill();
            gs.SkillID = 0;
            //注册监听.
            mHandleUUID = hitter.RegistOnHitDamage(OnHandleHitDmage, gs);
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            //取消监听.
            hitter.UnRegistOnHitDamage(mHandleUUID);
        }

        //单位被攻击时.伤害吸收计算.
        private float OnHandleHitDmage(float damage, TLVirtual hitted, TLVirtual attacker, AttackSource source, GameSkill sk, ref TLVirtual.AtkAppendData result)
        {
            float ret = damage;

            //damage > 0是伤害，damage < 0是加血机制.
            if (damage > 0 && AbsorbDamageSum > 0)
            {
                ret = damage - AbsorbDamageSum;

                if (ret < 0)
                {
                    ret = 0;
                    source.OutClientState = (byte)BattleAtkNumberEventB2C.AtkNumberType.Absorb;
                }

                int abDamage = (int)(damage - ret);

                AbsorbDamageSum -= abDamage;

                if (AbsorbDamageSum <= 0)
                {
                    //破盾.
                    hitted.mUnit.removeBuff(bufftemplateID);

                    //过量伤害吸收.
                    if (AbsorbOverFlowDamage)
                    {
                        ret = 0;
                    }
                }
            }

            return ret;

        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_AbsorbDamage;
            var d = data as TLBuffData_AbsorbDamage;
            if (data != null)
            {
                AbsorbDamageSum = d.AbsorbDamageSum;
                AbsorbOverFlowDamage = d.AbsorbOverFlowDamage;
            }
            base.Init(bd);
        }
    }
}
