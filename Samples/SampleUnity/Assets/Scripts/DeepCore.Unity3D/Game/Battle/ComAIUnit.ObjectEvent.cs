
using System.Collections.Generic;
using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Zone;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAIUnit
    {

        private class EffectCallBack
        {
            public Vector3 pos;
            public Quaternion rot;

            public EffectCallBack(Vector3 pos,Quaternion rot)
            {
                this.pos = pos;
                this.rot = rot;
            }
            
        }
        private HashMap<string,FuckAssetObject> CacheHitEffectMap = new HashMap<string,FuckAssetObject>();
        private HashMap<string,List<EffectCallBack>> HitEffectCallBackMap = new HashMap<string,List<EffectCallBack>>();
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
            PlayHitEffect(ev.AttackerID, ev.effect);
        }

        protected void PlayHitEffect(uint senderId, LaunchEffect effect)
        {
            if (effect == null)
            {
                return;
            }
            Quaternion rot = ObjectRoot.Rotation();
            ComAICell launcher = BattleScene.GetBattleObject(senderId);
            if (launcher != null && launcher != this && !launcher.IsDisposed)
            {
                rot = launcher.EffectRoot.Rotation();
                Vector3 forward = EffectRoot.Position() - launcher.EffectRoot.Position();
                if (forward != Vector3.zero)
                {
                    rot = Quaternion.LookRotation(forward);
                }
            }

            FuckAssetObject res ;
            if (CacheHitEffectMap.TryGetValue(effect.Name, out res))
            {
                if (res != null)
                {
                    if (!res.IsUnload)
                    {
                        var psystem = res.gameObject.GetComponentsInChildren<ParticleSystem>();
                        foreach (var ps in psystem)
                        {
                            ps.Stop();
                            ps.Play();
                        }
                        OnLoadEffectSuccess(res, effect, ObjectRoot.Position(), rot);
                        return;
                    }
                }
                else
                {
                    List<EffectCallBack> effectcblist;
                    HitEffectCallBackMap.TryGetOrCreate(effect.Name, out effectcblist,
                        (key) => new List<EffectCallBack>());
                    var cb = new EffectCallBack(ObjectRoot.Position(), rot);
                    effectcblist.Add(cb);
                    return;
                }

            }
            else
            {
                CacheHitEffectMap.Add(effect.Name, null);
            }
            
            PlayEffect(effect, ObjectRoot.Position(), rot);
           
        }

        public override void OnLoadNotShow(LaunchEffect eff)
        {
            
            if (!string.IsNullOrEmpty(eff.Name))
            {
                FuckAssetObject res = null;
                if (CacheHitEffectMap.TryGetValue(eff.Name, out res))
                {
                    if (res == null)
                    {
                        CacheHitEffectMap.RemoveByKey(eff.Name);
                    }
                    List<EffectCallBack> list;
                    if (HitEffectCallBackMap.TryGetValue(eff.Name, out list))
                    {
                        if (list != null)
                        {
                            list.Clear();
                        }
                    
                    }
                }
            }
            
            
        }

        public override void OnLoadEffectSuccess(FuckAssetObject aoe, LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            if (CacheHitEffectMap.ContainsKey(eff.Name))
            {
                CacheHitEffectMap[eff.Name] = aoe;
                base.OnLoadEffectSuccess(aoe, eff, pos, rot); 
                List<EffectCallBack> list;
                if (HitEffectCallBackMap.TryGetValue(eff.Name,out list))
                {
                    int num = list.Count;
                    if (num > 0)
                    {
                        for (int i = 0;i< num;i++)
                        {
                            var data = list[i];
                            base.OnLoadEffectSuccess(aoe, eff, data.pos, data.rot); 
                        }
                        list.Clear();
                    }
                }
                return;
            }
            
            base.OnLoadEffectSuccess(aoe, eff, pos, rot);
            
           
          
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