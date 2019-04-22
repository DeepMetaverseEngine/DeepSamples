using System;
using System.Collections.Generic;
using System.Text;
using ThreeLives.Battle.Host.Plugins.TLSkillTemplate.Skills;

namespace ThreeLives.Battle.Skill.Plugins
{
    public class TLFormula
    {
        public const float PERPER = 10000.0f;


        public static float PerToFloat(int v)
        {
            return v / PERPER;
        }

        #region 命中公式.

        //命中率计算.
        public static float CalHitRate(int hit, int dodge)
        {
            float v = (TLBattleFormula.HIT_RATE_C2 + hit - dodge) / TLBattleFormula.HIT_RATE_C3;

            return Math.Max(TLBattleFormula.HIT_RATE_C1, v);
        }

        #endregion

        #region 格挡.

        public static float CalBlockRate(int attackerLv, int hitterBlock)
        {
            float ret = 0;

            ret = Math.Min(TLBattleFormula.BLOCK_C1, hitterBlock / (PERPER + attackerLv * TLBattleFormula.BLOCK_C2));

            return ret;
        }

        public static float CalBlockCoefficient(Random r)
        {
            return r.Next(TLBattleFormula.BLOCK_C3, TLBattleFormula.BLOCK_C4) / PERPER;
        }

        public static int CalBlockDamage(Random r, int damage)
        {
            float ret = (CalBlockCoefficient(r) * damage);
            if (ret > 0 && ret < 1)
            {
                return 1;
            }
            return (int)ret;
        }

        #endregion

        #region 暴击率.

        //暴击率计算.
        public static float CalCritRate(int crit, int rescrit, int extracrit)
        {
            float t = (float)Math.Round((TLBattleFormula.LEVEL_LIMIT - TLBattleFormula.CRIT_RATE_C3) / TLBattleFormula.CRIT_RATE_C4, 0);

            float a = Math.Max(TLBattleFormula.CRIT_RATE_C1, (crit - rescrit) / (TLBattleFormula.CRIT_RATE_C2 + t * TLBattleFormula.CRIT_RATE_C5));
            float b = Math.Min(a, TLBattleFormula.CRIT_RATE_C6);
            return b + extracrit / PERPER;
        }

        public static float CalCritDamagePer(int critDamagePer, int redCritDamagePer)
        {
            return Math.Max(TLBattleFormula.CRIT_DAMAGE_PER_C1, PerToFloat(critDamagePer - redCritDamagePer));
        }

        #endregion

        #region 暴击治疗.

        public static float CalCritHealRate(int crit)
        {
            return Math.Max(TLBattleFormula.CRIT_HEAL_C1, crit / TLBattleFormula.CRIT_HEAL_C2);
        }

        #endregion

        #region 防御减伤公式.

        /// <summary>
        /// 物理减伤.
        /// </summary>
        /// <param name="atkerLv"></param>
        /// <param name="atkerThrough"></param>
        /// <param name="hitterDef"></param>
        /// <returns></returns>
        public static float CalPhyDamageReduceRate(int atkerLv, int atkerThrough, int hitterDef, int hitterDefreduction)
        {
            float a = TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C7 * hitterDef - TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C8 * atkerThrough;
            float b = (float)Math.Round((float)((TLBattleFormula.LEVEL_LIMIT - TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C3) / TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C4), 0);
            float c = atkerLv * TLBattleFormula.DAMAGE_REDUCE_RATE_C1 + TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C2 + b * TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C5;
            float d = Math.Max(TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C1, a / c);
            float ret = d + PerToFloat(hitterDefreduction);
            ret = Math.Min(ret, TLBattleFormula.PHY_DAMAGE_REDUCE_RATE_C6);
            return ret;
        }



        /// <summary>
        /// 魔法减伤.
        /// </summary>
        /// <param name="atkerLv"></param>
        /// <param name="atkerThrough"></param>
        /// <param name="hitterMdef"></param>
        /// <returns></returns>
        public static float CalMagDamageReduceRate(int atkerLv, int atkerThrough, int hitterMdef, int hitterMdefreduction)
        {
            float a = TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C7 * hitterMdef - TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C8 * atkerThrough;
            float b = (float)Math.Round((float)((TLBattleFormula.LEVEL_LIMIT - TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C3) / TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C4), 0);
            float c = atkerLv * TLBattleFormula.DAMAGE_REDUCE_RATE_C1 + TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C2 + b * TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C5;
            float d = Math.Max(TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C1, a / c);
            float ret = d + PerToFloat(hitterMdefreduction);
            ret = Math.Min(ret, TLBattleFormula.MAG_DAMAGE_REDUCE_RATE_C6);
            return ret;
        }

        #endregion

        #region 技能伤害.

        /// <summary>
        /// 获得技能伤害
        /// </summary>
        /// <param name="attack">攻击力</param>
        /// <param name="skillDamage">技能伤害系数</param>
        /// <param name="skillDamageModify">技能伤害绝对值</param>
        /// <returns></returns>
        public static int CalSkillDamage(int attack, int skillDamage, int skillDamageModify)
        {
            float t = PerToFloat(skillDamage);
            return (int)(attack * t + skillDamageModify);
        }

        #endregion

        #region 元素伤害.

        /// <summary>
        /// 元素伤害加成率.
        /// </summary>
        /// <param name="elementDamage"></param>
        /// <param name="elementResist"></param>
        /// <returns></returns>
        public static float CalElementDamageRate(int elementDamage, int elementResist)
        {
            return Math.Max(1, elementDamage - elementResist);
        }

        public static int CalElementSkillDamage(int elementSkillDamage)
        {
            return elementSkillDamage;
        }

        public static int CalElementDamage(Random r, float totalDamagePerRate, int elementSkillDamagePer, int elementSkillDamage, float elementDamageRate)
        {
            int v = r.Next(TLBattleFormula.CAL_DAMAGE_C1, TLBattleFormula.CAL_DAMAGE_C2);
            float k = PerToFloat(v);
            float p = PerToFloat(elementSkillDamagePer);
            int damage = (int)(k * totalDamagePerRate * (p * elementDamageRate + elementSkillDamage));

            return damage;
        }

        #endregion

        #region 伤害计算.


        /// <summary>
        /// 伤害加成减免率
        /// </summary>
        /// <param name="atker_totalDamagePer"></param>
        /// <param name="hitter_redTotalDamagePer"></param>
        /// <returns></returns>
        public static float CalTotalDamagePerRate(int atker_totalDamagePer, int hitter_redTotalDamagePer)
        {
            return Math.Max(TLBattleFormula.CAL_TOTAL_DAMAGE_PER_RATE_C1, 1 + PerToFloat(atker_totalDamagePer - hitter_redTotalDamagePer));
        }

        /// <summary>
        /// 技能伤害计算.
        /// </summary>
        /// <param name="r">随机种子</param>
        /// <param name="totalDamagePerRate">伤害加成减免率</param>
        /// <param name="atker_skillDamage">技能伤害</param>
        /// <param name="atker_toTargetDamage">对玩家/怪物伤害</param>
        /// <param name="hitter_damageReduceRate">防御减伤率</param>
        /// <returns></returns>
        public static int CalDamage(Random r,
                              float totalDamagePerRate,
                              int atker_skillDamage,
                              int atker_toTargetDamage,
                              float hitter_damageReduceRate)

        {
            //浮动系数.
            int v = r.Next(TLBattleFormula.CAL_DAMAGE_C1, TLBattleFormula.CAL_DAMAGE_C2);
            float k = PerToFloat(v);

            //防御减伤率(1-R) * skillDamage.
            float b = (1 - hitter_damageReduceRate) * atker_skillDamage;
            //((SKILL*ATK+ skilldamage )*(1-R)+N)
            float c = b + atker_toTargetDamage;

            float t = (k * totalDamagePerRate * c);

            int ret = 0;
            if (t > 0 && t < 1)
                ret = 1;
            else
                ret = (int)t;


            return ret;
        }

        public static int CalPVEDamage(int damage)
        {
            return (int)(damage * TLBattleFormula.PVE_C);
        }

        public static int CalPVPDamage(int damage)
        {
            return (int)(damage * TLBattleFormula.PVP_C);
        }

        #endregion

    }
}
