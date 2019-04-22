using System;
using System.Collections.Generic;
using System.Text;
using DeepCore.GameData.Zone;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.ActiveSkills;
using TLCommonSkill.Plugins.Buff;

namespace TLBattle.Server.Skill.Plugins.ActiveSkills
{
    public class DefaultScript_God : TLSkillBase
    {
        protected override bool LoadData => true;

        public int ID = 10002;

        public override int SkillID
        {
            get { return ID; }
            set { ID = value; }
        }

        protected override void OnInitData(TLSkillData data, GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            base.OnInitData(data, info, unit, ref template);
            template.CostMP = CostAnger;
        }

        protected override void OnSkillLogicEvent(BattleParams param)
        {
            param.UseFormulaType = BattleParams.FormulaType.None;
            if (!(param.Attacker is TLVirtual_Player) && !(param.Attacker is TLVirtual_PlayerMirror))
            {
                return;
            }


            //获取仙侣技能信息对BUFF强制赋予能力.
            var atker = (param.Attacker);
            var buffid = param.AtkProp.SkillArgu_2;

            TLBuffData_PlayerActiveSkill bd = new TLBuffData_PlayerActiveSkill();
            var lt = atker.GetGodSkill();
            if (lt == null)
            {
                return;
            }

            List<GameSkill> gslt = new List<GameSkill>();

            foreach (var item in lt)
            {
                gslt.Add(item.Value);
            }

            bd.Skills = gslt;
            bd.Keeps = new List<int>();
            bd.Keeps.Add(atker.GetGodMainSkill().SkillID);
            var buff = SkillHelper.Instance.GetUnitBuff(bd);

            //获取变身技能ID和等级.
            param.Attacker.AddTLBuff(buffid, param.Attacker, buff);
        }
    }
}
