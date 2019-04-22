using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLCommonSkill.Plugins.Buff;

namespace TLBattle.Server.Skill.Plugins
{
    public class TLSkillHelper : SkillHelper
    {
        public override UnitBuff GetUnitBuff(TLBuffData bd)
        {
            return CreateUnitBuff(bd);
        }

        public UnitBuff CreateUnitBuff(TLBuffData bd)
        {
            if (bd == null) return null;

            TLBuff ret = null;

            if (bd is TLBuffData_ChangeProp)
            {
                ret = new TLBuff_ChangeProp();
            }
            else if (bd is TLBuffData_AbsorbDamage)
            {
                ret = new TLBuff_AbsorbDamage();
            }
            else if (bd is TLBuffData_HPChange)
            {
                ret = new TLBuff_HPChange();
            }
            else if (bd is TLBuffData_Slient)
            {
                ret = new TLBuff_Slient();
            }
            else if (bd is TLBuffData_Mocking)
            {
                ret = new TLBuff_Mocking();
            }
            else if (bd is TLBuffData_PlayerActiveSkill)
            {
                ret = new TLBuff_PlayerActiveSkill();
            }
            else if (bd is TLBuffData_ActiveSkill)
            {
                ret = new TLBuff_ActiveSkill();
            }
            else if (bd is TLBuffData_LoopSkill)
            {
                ret = new TLBuff_LoopSkill();
            }
            else if(bd is TLBuffData_Purge)
            {
                ret = new TLBuff_Purge();
            }
            else if(bd is TLBuffData_LockHP)
            {
                ret = new TLBuff_LockHP();
            }
            else if(bd is TLBuffData_MPChange)
            {
                ret = new TLBuff_MPChange();
            }
            else
            {
                ret = new TLBuff();
            }
            if (ret != null)
            {
                ret.Init(bd);
            }

            return ret;
        }

    }
}
