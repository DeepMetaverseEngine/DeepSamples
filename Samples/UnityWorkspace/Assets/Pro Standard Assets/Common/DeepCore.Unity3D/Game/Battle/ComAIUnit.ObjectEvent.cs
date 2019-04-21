using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Zone;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAIUnit
    {
        protected override void RegistAllObjectEvent()
        {
            base.RegistAllObjectEvent();
            RegistObjectEvent<UnitHitEvent>(ObjectEvent_UnitHitEvent);
            RegistObjectEvent<UnitDoActionEvent>(ObjectEvent_UnitDoActionEvent);
            RegistObjectEvent<UnitDamageEvent>(ObjectEvent_UnitDamageEvent);
            RegistObjectEvent<UnitDeadEvent>(ObjectEvent_UnitDeadEvent);
        }

        protected virtual void ObjectEvent_UnitHitEvent(UnitHitEvent ev)
        {
            PlayHitEffect(ev.senderId, ev.effect);
        }

        protected void PlayHitEffect(uint senderId, LaunchEffect effect)
        {
            Quaternion rot = ObjectRoot.Rotation();
            ComAICell launcher = BattleScene.GetBattleObject(senderId);
            if (launcher != null && launcher != this)
            {
                rot = launcher.EffectRoot.Rotation();
                Vector3 forward = EffectRoot.Position() - launcher.EffectRoot.Position();
                if (forward != Vector3.zero)
                {
                    rot = Quaternion.LookRotation(forward);
                }
            }
            PlayEffect(effect, ObjectRoot.Position(), rot);
        }

        protected virtual void ObjectEvent_UnitDoActionEvent(UnitDoActionEvent ev)
        {
            //PlayAnim(ev.ActionName, true);
        }

        protected virtual void ObjectEvent_UnitDamageEvent(UnitDamageEvent ev)
        {
           // Debug.Log("UnitDamageHurtEvent.");
        }

        protected virtual void ObjectEvent_UnitDeadEvent(UnitDeadEvent ev)
        {
            
        }
    }
}