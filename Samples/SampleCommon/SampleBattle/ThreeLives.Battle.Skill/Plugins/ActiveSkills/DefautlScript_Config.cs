using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.ActiveSkills;

namespace TLBattle.Server.Skill.Plugins.ActiveSkills
{
    /// <summary>
    /// 物理普攻攻击通用脚本.
    /// </summary>
    public class DefautlScript_Config : TLSkillBase
    {
        protected override bool LoadData => true;

        public int ID = 10000;

        public override int SkillID
        {
            get { return ID; }
            set { ID = value; }
        }

        protected override void OnSkillCoefficientEvent(BattleParams param)
        {

        }
    }
}
