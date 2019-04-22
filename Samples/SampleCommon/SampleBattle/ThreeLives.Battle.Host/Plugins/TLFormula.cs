using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Plugins
{
    public class TLFormula : IFormula
    {

        public TLFormula()
        {
        }

        public override int OnHit(InstanceUnit attacker, AttackSource source, InstanceUnit targget)
        {
            var src_prop = attacker.Virtual as TLVirtual;
            var dst_prop = targget.Virtual as TLVirtual;
            return (dst_prop).OnHit(src_prop, source);
        }

        public override void OnUnitDead(InstanceUnit targget, InstanceUnit killer)
        {
            var src_prop = killer.Virtual as TLVirtual;
            var dst_prop = targget.Virtual as TLVirtual;
            (src_prop).OnUnitDead(dst_prop,src_prop);
        }

        public override void OnUnitRemoved(InstanceUnit unit)
        {
            var prop = unit.Virtual as TLVirtual;
            (prop as TLVirtual).OnUnitRemoved();
        }

        public override void OnUnitHandleNetMessage(InstanceUnit unit, ObjectAction action)
        {
            var unit_prop = unit.Virtual as TLVirtual; ;
            (unit_prop as TLVirtual).OnHandleNetMessage(action);
        }

        public override void OnBuffBegin(InstanceUnit unit, InstanceUnit.BuffState buff, InstanceUnit sender)
        {
            var unit_prop = unit.Virtual as TLVirtual;
            var sender_prop = sender.Virtual as TLVirtual;
            (unit_prop as TLVirtual).OnBuffBegin(buff, sender_prop as TLVirtual);
        }

        public override void OnBuffUpdate(InstanceUnit unit, InstanceUnit.BuffState buff, int time)
        {
            var unit_prop = unit.Virtual as TLVirtual;
            (unit_prop as TLVirtual).OnBuffUpdate(buff, time);
        }

        public override void OnBuffEnd(InstanceUnit unit, InstanceUnit.BuffState buff, string result)
        {
            var unit_prop = unit.Virtual as TLVirtual;
            (unit_prop).OnBuffEnd(buff);
        }

        public override bool TryLaunchSkill(InstanceUnit unit, InstanceUnit.SkillState skill, ref InstanceUnit.LaunchSkillParam param)
        {
            var unit_prop = unit.Virtual as TLVirtual;
            return (unit_prop as TLVirtual).TryLaunchSkill(skill, ref param);
        }

        public override bool TryLaunchSpell(InstanceUnit launcher, ref SpellTemplate spell)
        {
            var unit_prop = launcher.Virtual as TLVirtual;
            return (unit_prop as TLVirtual).TryLaunchSpell(ref spell);
        }

        public override bool TryAddBuff(AddBuff add)
        {
            var owner_prop = add.unit.Virtual as TLVirtual;
            var sender_prop = add.sender.Virtual as TLVirtual;
            return (sender_prop).TrySendBuff(add);
        }

        public override bool TrySummonUnit(InstanceUnit owner, SummonUnit summon, ref UnitInfo summonUnit, ref string name)
        {
            var owner_prop = owner.Virtual as TLVirtual;

            return (owner_prop).TrySummonUnit(owner_prop, summon, ref summonUnit);
        }

    }

}
