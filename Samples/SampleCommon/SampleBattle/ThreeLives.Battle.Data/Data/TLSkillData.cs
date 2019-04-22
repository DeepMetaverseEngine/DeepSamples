namespace TLBattle.Common.Data
{
    /// <summary>
    /// 战斗技能配置表数据.
    /// </summary>
    public class TLSkillData
    {
        public int id;
        public int skill_id;
        public string skill_name;

        public int damage_type;
        public int element_type;

        public int skill_lv;
        public int skill_cd;

        public int skill_coefficient;
        public int skill_damage;

        public int element_coefficient;
        public int element_damage;

        public int recover_anger;
        public int cost_anger;

        public int extra_monster;
        public int extra_player;
    }

    public class TLMeridiansData
    {
        /// <summary>
        ///ID.
        /// </summary>
        public int id;
        /////穴位ID.
        public int main;
        /// <summary>
        /// 技能ID.
        /// </summary>
        public int skill_id;
        /// <summary>
        /// 技能CD.
        /// </summary>
        public int skill_cd;
        /// <summary>
        /// 技能回复怒气.
        /// </summary>
        public int recover_anger;
        /// <summary>
        /// 普通技能系数比例.
        /// </summary>
        public int skill_coefficient;
        /// <summary>
        /// 固定伤害.
        /// </summary>
        public int skill_damage;
        /// <summary>
        /// 元素伤害系数比例.
        /// </summary>
        public int element_coefficient;
        /// <summary>
        /// 元素伤害固定值.
        /// </summary>
        public int element_damage;
        /// <summary>
        /// 对怪物额外伤害.
        /// </summary>
        public int extra_monster;
        /// <summary>
        /// 对玩家额外伤害.
        /// </summary>
        public int extra_player;
    }
}
