using System;
using DeepCore;
using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Zone;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAICell
    {
        private HashMap<Type, Action<ObjectEvent>> mObjectEvens = new HashMap<Type, Action<ObjectEvent>>();


        public void DoObjectEvent(ObjectEvent ev)
        {
            Action<ObjectEvent> action = null;
            if (mObjectEvens.TryGetValue(ev.GetType(), out action))
            {
                if (!IsDisposed)
                    action(ev);
            }
        }

        protected virtual void RegistAllObjectEvent()
        {
            RegistObjectEvent<UnitEffectEvent>(ObjectEvent_UnitEffectEvent);
        }

        public void RegistObjectEvent<T>(Action<T> action) where T :ObjectEvent
        {
            Type type = typeof(T);
            Action<ObjectEvent> outVal = null;
            if (!mObjectEvens.TryGetValue(type, out outVal))
            {
                mObjectEvens.Add(type, (e) =>
                {
                    if(!IsDisposed)
                        action((T)e);
                });
            }
        }

        protected virtual void ObjectEvent_UnitEffectEvent(UnitEffectEvent ev)
        {
            this.ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);
            PlayEffect(ev.effect, EffectRoot.Position(), EffectRoot.Rotation());
        }
    }

}