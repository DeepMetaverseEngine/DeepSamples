using System;
using ThreeLives.Battle.Host.Plugins.TLSkillTemplate.Skills;
using ThreeLives.Battle.Skill.Plugins;

namespace TLCommonSkill.Plugins.ActiveSkills
{
    partial class TLSkillBase
    {
        public static float PerToFloat(int v)
        {
            return TLFormula.PerToFloat(v);
        }

        #region 命中公式.

        //命中率计算.
        private float CalHitRate(int hit, int dodge)
        {
            return TLFormula.CalHitRate(hit, dodge);
        }

        #endregion

        #region 格挡.

        private float CalBlockRate(int attackerLv, int hitterBlock)
        {
            return TLFormula.CalBlockRate(attackerLv, hitterBlock);
        }

        private float CalBlockCoefficient(Random r)
        {
            return TLFormula.CalBlockCoefficient(r);
        }

        private int CalBlockDamage(Random r, int damage)
        {
            return TLFormula.CalBlockDamage(r, damage);
        }

        #endregion

        #region 暴击率.

        //暴击率计算.
        private float CalCritRate(int crit, int rescrit, int extracrit)
        {
            return TLFormula.CalCritRate(crit, rescrit, extracrit);
        }

        private float CalCritDamagePer(int critDamagePer, int redCritDamagePer)
        {
            return TLFormula.CalCritDamagePer(critDamagePer, redCritDamagePer);
        }

        #endregion

        #region 暴击治疗.

        private float CalCritHealRate(int crit)
        {
            return TLFormula.CalCritHealRate(crit);
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
        private float CalPhyDamageReduceRate(int atkerLv, int atkerThrough, int hitterDef, int hitterDefreduction)
        {
            return TLFormula.CalPhyDamageReduceRate(atkerLv, atkerThrough, hitterDef, hitterDefreduction);
        }

        /// <summary>
        /// 魔法减伤.
        /// </summary>
        /// <param name="atkerLv"></param>
        /// <param name="atkerThrough"></param>
        /// <param name="hitterMdef"></param>
        /// <returns></returns>
        private float CalMagDamageReduceRate(int atkerLv, int atkerThrough, int hitterMdef, int hitterMDefreduction)
        {
            return TLFormula.CalMagDamageReduceRate(atkerLv, atkerThrough, hitterMdef, hitterMDefreduction);
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
        private int CalSkillDamage(int attack, int skillDamage, int skillDamageModify)
        {
            return TLFormula.CalSkillDamage(attack, skillDamage, skillDamageModify);
        }

        #endregion

        #region 元素伤害.

        /// <summary>
        /// 元素伤害加成率.
        /// </summary>
        /// <param name="elementDamage"></param>
        /// <param name="elementResist"></param>
        /// <returns></returns>
        private float CalElementDamageRate(int elementDamage, int elementResist)
        {
            return TLFormula.CalElementDamageRate(elementDamage, elementResist);
        }

        private int CalElementSkillDamage(int elementSkillDamage)
        {
            return TLFormula.CalElementSkillDamage(elementSkillDamage);
        }

        private int CalElementDamage(Random r, float totalDamagePerRate, int elementSkillDamagePer, int elementSkillDamage, float elementDamageRate)
        {
            return TLFormula.CalElementDamage(r, totalDamagePerRate, elementSkillDamagePer, elementSkillDamage, elementDamageRate);
        }

        #endregion

        #region 伤害计算.

        /// <summary>
        /// 伤害加成减免率
        /// </summary>
        /// <param name="atker_totalDamagePer"></param>
        /// <param name="hitter_redTotalDamagePer"></param>
        /// <returns></returns>
        protected float CalTotalDamagePerRate(int atker_totalDamagePer, int hitter_redTotalDamagePer)
        {
            return TLFormula.CalTotalDamagePerRate(atker_totalDamagePer, hitter_redTotalDamagePer);
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
        private int CalDamage(Random r,
                              float totalDamagePerRate,
                              int atker_skillDamage,
                              int atker_toTargetDamage,
                              float hitter_damageReduceRate)

        {
            return TLFormula.CalDamage(r, totalDamagePerRate, atker_skillDamage, atker_toTargetDamage, hitter_damageReduceRate);
        }

        private int CalPVEDamage(int damage)
        {
            return TLFormula.CalPVEDamage(damage);
        }

        private int CalPVPDamage(int damage)
        {
            return TLFormula.CalPVPDamage(damage);
        }
        #endregion

    }
}
