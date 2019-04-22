using DeepCore;
using TLBattle.Common.Data;

namespace TLBattle.Server.Skill.Plugins
{
    public class TLSkillValue
    {
        public enum SkillValueType
        {
            none,
            skill_lv,
            skill_cd,
            mp_cost,
            coefficient1,
            coefficient2,
            damage_type,
        }

        private SkillValueType type = SkillValueType.none;

        private HashMap<int, int> dataMap = new HashMap<int, int>();

        public int GetValue(int lv)
        {
            return 0;
        }

        /// <summary>
        /// 是否无效.
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        private bool Invalid(int lv)
        {
            return false;
        }

        public void InitData(HashMap<int, TLSkillData> data)
        {

        }
    }
}
