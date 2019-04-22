using DeepCore.GameData.Zone;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.ActiveSkills;

namespace TLBattle.Server.Skill.Plugins.ActiveSkills
{
    /// <summary>
    /// 编辑器默认脚本.
    /// </summary>
    public class DefaultScript_Editor : TLSkillBase
    {
        protected override bool LoadData => false;

        public int ID = 10001;

        public override int SkillID
        {
            get { return ID; }
            set { ID = value; }
        }

        protected override void OnSkillCoefficientEvent(BattleParams param)
        {
            if (param.AtkProp.SkillArgu_1 == 0)
            {
                param.AttackType = TLVirtual.AttackType.phyAtk;
            }
            else if (param.AtkProp.SkillArgu_1 == 1)
            {
                param.AttackType = TLVirtual.AttackType.magAtk;
            }

            param.SkillDamagePer = 10000;
            param.ElementDamageType = BattleParams.ElementType.None;
        }

    }
}
