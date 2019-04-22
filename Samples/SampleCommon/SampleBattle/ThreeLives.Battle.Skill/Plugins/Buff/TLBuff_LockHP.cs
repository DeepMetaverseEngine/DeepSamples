using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using System;
using ThreeLives.Battle.Skill.Plugins;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    /// <summary>
    /// 锁定血量.
    /// </summary>
    public class TLBuff_LockHP : TLBuff
    {
        /// <summary>
        /// 锁定值.
        /// </summary>
        private int LockHPGuideValue;
        /// <summary>
        /// 锁定类型.
        /// </summary>
        private byte GuideValueType;
        /// <summary>
        /// 锁定次数，小于0无限.
        /// </summary>
        private int UseTimes;

        private int registID = 0;

        private bool IsPercentValue()
        {
            return (GuideValueType == 1);
        }

        private bool TriggerBuffEffect(int curHP, float damage)
        {
            int hp = (int)(curHP - damage);

            int v = LockHPGuideValue;
            if (IsPercentValue())
            {
                float p = TLFormula.PerToFloat(LockHPGuideValue);
                v = (int)(p * curHP);
            }

            if (hp < v)
                return true;

            return false;
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_LockHP;
            var d = data as TLBuffData_LockHP;

            this.LockHPGuideValue = d.LockHPGuideValue;
            this.GuideValueType = d.GuideValueType;
            this.UseTimes = d.UseTimes;

            base.Init(bd);
        }

        protected override void OnBuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            GameSkill gs = new GameSkill();
            gs.SkillID = 0;
            registID = hitter.RegistOnHitDamage(OnHandleHitDmage, gs);
            base.OnBuffBegin(hitter, attacker, state);
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            hitter.UnRegistOnHitDamage(registID);
            base.BuffEnd(hitter, state);
        }

        //单位被攻击时.伤害吸收计算.
        private float OnHandleHitDmage(float damage, TLVirtual hitted, TLVirtual attacker, AttackSource source, GameSkill sk, ref TLVirtual.AtkAppendData result)
        {
            float ret = damage;

            //伤害.
            if (damage > 0 && (UseTimes != 0))
            {
                int curHP = hitted.mUnit.CurrentHP;
                int lefthp = (int)(curHP - damage);
                lefthp = Math.Max(lefthp, 0);

                int v = LockHPGuideValue;
                if (IsPercentValue())
                {
                    float p = TLFormula.PerToFloat(LockHPGuideValue);
                    v = (int)(p * hitted.mUnit.MaxHP);
                }

                if (lefthp == 0)//致命一击.trigger effect.
                {
                    if (curHP >= v)
                        ret = curHP - v;
                    else
                        ret = 0;

                    if (UseTimes > 0)
                        UseTimes--;
                }
                else
                {
                    if (lefthp <= v)//trigger effect.
                    {
                        ret = 0;
                        if (UseTimes > 0)
                            UseTimes--;
                    }
                }
            }

            return ret;
        }
    }
}
