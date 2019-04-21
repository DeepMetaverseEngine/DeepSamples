using System;
using DeepCore;
using DeepCore.GameData.Zone;

namespace DeepCore.Unity3D.Battle
{
    public partial class BattleScene
    {
        private HashMap<Type, Action<ZoneEvent>> mZoneEvens = new HashMap<Type, Action<ZoneEvent>>();


        protected virtual void RegistAllZoneEvent()
        {
            RegistZoneEvent<AddEffectEvent>(ZoneEvent_AddEffectEvent);
        }

        protected void RegistZoneEvent<T>(Action<T> action) where T :ZoneEvent
        {
            Type type = typeof(T);
            Action<ZoneEvent> outVal = null;
            if (!mZoneEvens.TryGetValue(type, out outVal))
            {
                mZoneEvens.Add(type, (e) =>
                {
                    action((T)e);
                });
            }
        }

        protected virtual void ZoneEvent_AddEffectEvent(AddEffectEvent ev)
        {
            PlayEffectWithZoneCoord(ev.effect, ev.x, ev.y, ev.direction);
        }
    }

}