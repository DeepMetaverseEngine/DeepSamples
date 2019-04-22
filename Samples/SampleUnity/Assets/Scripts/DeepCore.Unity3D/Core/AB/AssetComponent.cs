using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using DeepCore.Unity3D.Utils;

namespace DeepCore.Unity3D
{
    public class AssetComponent: MonoBehaviour
    {
        public bool Invalid { get; private set; }
        public bool IsInCache { get; private set; }
        public string CacheName { get; set; }

        public bool IsUnload
        {
            get { return IsInCache || Invalid || !UnityHelper.IsObjectExist(gameObject); }
        }

        public bool DontMoveToCache;


        protected internal virtual void OnCacheDidPush()
        {
            IsInCache = true;
            gameObject.transform.position = Vector3.zero;
        }

        protected internal virtual void OnCacheDidPop()
        {
            IsInCache = false;
            mUnload = false;
        }

        protected virtual bool CheckFuck()
        {
            if (Invalid)
            {
                Debug.LogError("[what the fuck] 不能引用一个已删除的资源 " + this);
                return true;
            }
            if (IsInCache)
            {
                Debug.LogError("[what the fuck] 不能引用一个已回收的资源 " + this);
                return true;
            }
            return false;
        }
        
        protected virtual void OnDestroy()
        {
            //Debug.LogWarning("[Destroy]" + CacheName + "\t" + Invalid + "\t" + CacheName);
            Invalid = true;
            if (IsInCache)
            {
                Debug.LogError("尝试删除一个已存在缓存的FuckAssetObject");
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    throw new Exception("看看堆栈，嗨皮一下" + this);
                }
            }
        }

        public virtual void Unload()
        {
            if (IsUnload)
            {
                return;
            }

            if(mUnload)
            {
                return;
            }
            else
            {
                mUnload = true;
            }

            transform.parent = null;
#if UNITY_STANDALONE || UNITY_EDITOR
            mUnloadTrace = new StackTrace().ToString();
#endif
            if (DontMoveToCache)
            {
                DeepCore.Unity3D.UnityHelper.Destroy(this);
                DeepCore.Unity3D.UnityHelper.Destroy(gameObject);
                Invalid = true;
            }
            else
            {
                if(!UnityHelper.IsObjectExist(gameObject))
                {
                    Debug.LogError("[Unload what the fuck]AssetComponent has been destroyed");
                    return;
                }
                var ead = gameObject.GetComponent<EffectAutoDestroy>();
                if(ead && ead.OnDestroyed)
                {
                    Debug.LogError("[Unload what the fuck]EffectAutoDestroy has been destroyed");
                    return;
                }
                if(ead != null)
                {
                    ead.SetBeforeDestroy();
                }
                UnityObjectCacheCenter.GetTypeCache(GetType()).Push(CacheName, this);
            }
        }
        public static void Unload(GameObject obj)
        {
            if (!UnityHelper.IsObjectExist(obj))
            {
                Debug.LogError("Unload what the fuck" + obj);
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    throw new Exception("看看堆栈，嗨皮一下");
                }
                return;
            }
            var comp = obj.GetComponent<AssetComponent>();
            if (comp)
            {
                comp.Unload();
            }
            else
            {
                Debug.LogError("Unload what the fuck", obj);
                DeepCore.Unity3D.UnityHelper.Destroy(obj);
            }
        }
        protected string mTrace;
        protected string mUnloadTrace;
        private bool mUnload = false;

        public override string ToString()
        {
            return string.Format("[{0} {1}] \n Load Trace : {2}##################\n Unload Trace: {3}", CacheName, GetInstanceID(), mTrace, mUnloadTrace);
        }
    }
}