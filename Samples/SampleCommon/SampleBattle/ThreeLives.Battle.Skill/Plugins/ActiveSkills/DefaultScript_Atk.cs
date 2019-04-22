using System;
using System.Collections.Generic;
using System.Text;
using DeepCore.GameData.Zone;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.ActiveSkills;

namespace ThreeLives.Battle.Skill.Plugins.ActiveSkills
{
    /// <summary>
    /// 用来处理非技能类型（SkillTemplateID = 0）的攻击伤害判定.
    /// </summary>
    public class DefaultScript_Atk : TLSkillBase
    {
        protected override bool LoadData => false;

        public int ID = 0;

        public override int SkillID
        {
            get { return ID; }
            set { ID = value; }
        }

        protected override void OnRegisterUnitEvent(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            //伤害监听.
            unit.RegistCalDamage(OnCallDamageProcess, info);
        }
    }
}
