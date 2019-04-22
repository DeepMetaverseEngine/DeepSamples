using DeepCore.GameHost.Instance;
using DeepCore.Log;
using System;
using System.Collections.Generic;
using System.Text;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff_LoopSkill : TLBuff
    {
        public List<TLGameSkillSnap> LoopSkillList;

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_LoopSkill begin", null);
            hitter.SkillModule.SkillLoopList = LoopSkillList;
            base.BuffBegin(hitter, attacker, state);
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_LoopSkill end", null);
            hitter.SkillModule.SkillLoopList = null;
            base.BuffEnd(hitter, state);
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_LoopSkill;
            var d = data as TLBuffData_LoopSkill;
            if (data != null)
            {
                this.LoopSkillList = d.SkillLoopList;
            }

            base.Init(bd);
        }
    }
}
