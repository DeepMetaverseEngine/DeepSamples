using DeepCore.GameHost.Formula;
using System.Collections.Generic;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    partial class TLVirtual
    {
        public enum AttackRlt : int
        {
            NoControl = -1,
            NormalAttack,
            CritAttack,
            DodgeAttack,
            BlockAttack,
        }

        /// <summary>
        /// 伤害类型：物理，魔法.
        /// </summary>
        public enum AttackType : byte
        {
            none = 0,
            phyAtk = 1,
            magAtk = 2,
            mixAtk = 3
        }

        //单位vs单位计算hit计算.
        private int On_hit_unit_vs_unit(TLVirtual attacker, TLVirtual hitter, AttackSource source, ref AtkAppendData data)
        {
            source.OutClientState = (int)BattleAtkNumberEventB2C.AtkNumberType.Normal;

            //是否为技能.
            bool isBuff = source.FromBuffState != null ? true : false;

            int damage = 0;

            if (isBuff == true)
            {
                BuffState bs = source.FromBuffState;

                if (bs.Tag != null)
                {
                    List<UnitBuff> list = bs.Tag as List<UnitBuff>;

                    for (int ltIndex = 0; ltIndex < list.Count; ltIndex++)
                    {
                        damage += list[ltIndex].BuffHit(hitter, attacker, source, ref data);
                    }
                }
            }
            else
            {
                //1.计算攻击结果.2计算攻击伤害.
                damage = CalDamage(attacker, hitter, source, ref data);
            }

            //打印结果.
            switch (source.OutClientState)
            {
                case (int)BattleAtkNumberEventB2C.AtkNumberType.Dodge:
                    TLAttackProperties prop = (source.Attack.Properties as TLAttackProperties);
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "{0}对目标{1}使用技能【{2}】被目标【闪避】", attacker.mInfo.Name, hitter.mInfo.Name, prop.SkillTemplateID);
                    damage = 0;
                    break;
                case (int)BattleAtkNumberEventB2C.AtkNumberType.Crit:
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "【{0}】攻击【{1}】结果 =【{2}】，伤害 =【{3}】", attacker.mInfo.Name, hitter.mInfo.Name, "暴击", damage);
                    break;
                case (int)BattleAtkNumberEventB2C.AtkNumberType.Normal:
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "【{0}】攻击【{1}】结果 =【{2}】，伤害 =【{3}】", attacker.mInfo.Name, hitter.mInfo.Name, "普攻", damage);
                    break;
                case (int)BattleAtkNumberEventB2C.AtkNumberType.Immunity:
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "【{0}】攻击【{1}】结果 =【{2}】，伤害 =【{3}】", attacker.mInfo.Name, hitter.mInfo.Name, "免疫", damage);
                    break;
                case (int)BattleAtkNumberEventB2C.AtkNumberType.Absorb:
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "【{0}】攻击【{1}】结果 =【{2}】，伤害 =【{3}】", attacker.mInfo.Name, hitter.mInfo.Name, "吸收", damage);
                    break;
                case (int)BattleAtkNumberEventB2C.AtkNumberType.IronMaiden:
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "【{0}】攻击【{1}】结果 =【{2}】，伤害 =【{3}】", attacker.mInfo.Name, hitter.mInfo.Name, "反伤", damage);
                    break;
                default:
                    break;
            }

            if (damage == 0)
            {
                //闪避时不产生受击.
                source.OutIsDamage = false;
            }



            return damage;
        }

        //伤害覆写
        public int CalDamage(TLVirtual attacker, TLVirtual hitter, AttackSource source, ref AtkAppendData data)
        {
            int ret = 0;
            ret = attacker.DispatchCalDamageEvent(hitter, source, ref data);
            return ret;
        }

        /// <summary>
        /// 纷发单位打到其他单位，其他单位被攻击，(TLSkillBase调用).
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="attacker"></param>
        /// <param name="hitter"></param>
        /// <param name="source"></param>
        public void DispatchHitEvents(ref int damage, TLVirtual attacker, TLVirtual hitter, AttackSource source, ref AtkAppendData data)
        {
            //分发事件单位打到别人.
            damage = (int)attacker.DispatchHitOtherEvent(damage, hitter, source, ref data);

            //分发事件单位收到伤害.
            damage = (int)hitter.DispatchHitDamageEvent(damage, attacker, source, ref data);
        }

        /// <summary>
        /// 技能计算扩展类.
        /// </summary>
        public struct AtkAppendData
        {
            /// <summary>
            /// 仇恨值.
            /// </summary>
            public int ThreatValue { get; set; }

            /// <summary>
            /// 更改百分比.
            /// </summary>
            public int ThreatValueChangePer { get; set; }

            /// <summary>
            /// 更改绝对值.
            /// </summary>
            public int ThreatValueChangeModify { get; set; }

            /// <summary>
            /// 打击伤害.
            /// </summary>
            public List<SplitHitInfo> HitInfo { get; set; }

            /// <summary>
            /// 多段打击总伤害.
            /// </summary>
            public int HitInfoTotalDamage { get; set; }
        }

    }
}
