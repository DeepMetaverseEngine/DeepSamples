using DeepCore;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using System.Text;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff_PlayerActiveSkill : TLBuff
    {
        public HashMap<int, GameSkill> SkillLt = null;

        public List<int> Keeps = null;

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            hitter.SkillModule.ActiveSkill(SkillLt, Keeps, true);
            if (hitter is TLVirtual_Player)
            {
                (hitter as TLVirtual_Player).SendClientSkillPanelInfo();
            }
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            var lt = hitter.GetAllSkillData();
            HashMap<int, GameSkill> map = new HashMap<int, GameSkill>();
            for (int i = 0 ; i < lt.Count ; i++)
            {
                map.Add(lt[i].SkillID, lt[i]);
            }

            hitter.SkillModule.ActiveSkill(map, Keeps, true);

            if (hitter is TLVirtual_Player)
            {
                (hitter as TLVirtual_Player).SendClientSkillPanelInfo();
            }
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_PlayerActiveSkill;
            var d = data as TLBuffData_PlayerActiveSkill;
            if (d.Skills != null)
            {
                GameSkill gs = null;
                SkillLt = new HashMap<int, GameSkill>();
                for (int i = 0 ; i < d.Skills.Count ; i++)
                {
                    gs = d.Skills[i];
                    SkillLt.Put(gs.SkillID, gs);
                }

            }
            Keeps = d.Keeps;
            base.Init(bd);
        }
    }
}