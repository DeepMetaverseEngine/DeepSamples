using DeepCore.GameData.Zone;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAISpell
    {
        protected override void RegistAllObjectEvent()
        {
            base.RegistAllObjectEvent();
            RegistObjectEvent<SpellLockTargetEvent>(ObjectEvent_SpellLockTargetEvent);
        }

        protected virtual void ObjectEvent_SpellLockTargetEvent(SpellLockTargetEvent ev)
        {
            mTarget = BattleScene.GetBattleObject(ev.target_obj_id);
        }
    }

}