using DeepCore;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using System.Text;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff_ChangeSkill : TLBuff
    {
        public HashMap<int, GameSkill> SkillLt = null;

        public List<int> Keeps = null;

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            hitter.SkillModule.ActiveSkill(SkillLt, Keeps, true);

        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            var lt = hitter.GetSkillData();
            HashMap<int, GameSkill> map = new HashMap<int, GameSkill>();
            for (int i = 0 ; i < lt.Count ; i++)
            {
                map.Add(lt[i].SkillID, lt[i]);
            }

            hitter.SkillModule.ActiveSkill(map, Keeps, true);
        }
    }
}
