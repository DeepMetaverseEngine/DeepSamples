using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using SampleBattle.Scene;
using System;

namespace SampleBattle.Plugins
{
    public class SampleFormula : IFormula
    {
        public SampleFormula()
        {
        }
        
        public override int OnHit(InstanceUnit attacker, AttackSource source, InstanceUnit targget)
        {
            return Math.Min(attacker.RandomN.Next(1, targget.MaxHP / 10), 100);
        }
        public override void OnUnitDead(InstanceUnit targget, InstanceUnit killer)
        {
        }
        public override void OnUnitRemoved(InstanceUnit unit)
        {
        }
        public override void OnUnitHandleNetMessage(InstanceUnit unit, ObjectAction action)
        {
        }


        public override void OnBuffBegin(InstanceUnit unit, InstanceUnit.BuffState buff, InstanceUnit sender)
        {
        }
        public override void OnBuffUpdate(InstanceUnit unit, InstanceUnit.BuffState buff, int time)
        {
        }
        public override void OnBuffEnd(InstanceUnit unit, InstanceUnit.BuffState buff, string result)
        {
        }
        public override bool TryLaunchSkill(InstanceUnit unit, InstanceUnit.SkillState skill, ref InstanceUnit.LaunchSkillParam param)
        {
            var sa = unit.Parent.GetArea(unit.X, unit.Y);
            if (sa != null && sa.CurrentMapNodeValue == MapBlockValue.AREA_SAFE)
            {
                return false;
            }
            return true;
        }
        public override bool TryLaunchSpell(InstanceUnit launcher, ref SpellTemplate spell)
        {
            return true;
        }
        public override bool TrySummonUnit(InstanceUnit owner, SummonUnit summon, ref UnitInfo summonUnit, ref string name)
        {
            return true;
        }
        public override bool TryAddBuff(AddBuff add)
        {
            return true;
        }

    }
}
