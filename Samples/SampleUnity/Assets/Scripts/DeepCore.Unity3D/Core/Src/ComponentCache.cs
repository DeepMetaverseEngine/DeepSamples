using System;
using DeepCore.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DeepCore.Unity3D
{
    public interface IUnityCache
    {
        int Capacity { get; set; }
        bool UseCache { get; set; }
        void Clear();
        Object Pop(string key);
        bool Push(string key, Object val);
    }

    public sealed class UnityObjectCache<T> : IUnityCache where T : Object
    {
        private int mCapacity = 10;

        public int Capacity
        {
            get { return mCapacity; }
            set
            {
                if (mCapacity == value) return;
                mCapacity = value;
                if (mCapacity <= 0)
                {
                    Clear();
                }
                mCache.Resize(mCapacity);
            }
        }

        /// <summary>
        /// 默认打开缓存
        /// </summary>
        private bool mUseCache = true;

        public bool UseCache
        {
            get { return mUseCache; }
            set
            {
                if (UseCache == value)
                {
                    return;
                }
                mUseCache = value;
                if (!mUseCache)
                {
                    Clear();
                }
            }
        }

        private readonly LRUCache<string, T> mCache;
        public event Action<T> OnWillDestroy;
        public event Action<T> OnDidCache;
        public event Action<T> OnCacheHit;

        /// <summary>
        /// 返回false表示不进入缓存
        /// </summary>
        public event Predicate<T> OnWillCache;

        public UnityObjectCache()
        {
            mCache = new LRUCache<string, T>(mCapacity, OnAfterRemoveObject);
        }

        private void OnAfterRemoveObject(T val)
        {
            DestroyUnityObject(val, true);
        }

        private void DestroyUnityObject(T val, bool tryPopLogic)
        {
            if (UnityHelper.IsObjectExist(val))
            {
                if (OnWillDestroy != null)
                {
                    OnWillDestroy.Invoke(val);
                }
                if (val is Component)
                {
                    var comp = val as Component;
                    if (tryPopLogic)
                    {
                        var ac = comp as AssetComponent;
                        if (ac != null)
                        {
                            ac.OnCacheDidPop();
                            DeepCore.Unity3D.UnityHelper.Destroy(ac);
                        }
                    }

                    if (UnityHelper.IsObjectExist(comp.gameObject))
                    {
                        DeepCore.Unity3D.UnityHelper.Destroy(comp.gameObject);
                    }
                    else
                    {
                        Debug.LogError("What the fuck?? " + val);
                    }
                }
                else if (val is AudioClip)
                {
                    Resources.UnloadAsset(val);
                }
                else if (val is GameObject)
                {
                    DeepCore.Unity3D.UnityHelper.Destroy(val);
                }
                else
                {
                    throw new NotSupportedException(val.GetType().FullName);
                }
            }
            else
            {
                Debug.LogError("What the fuck? " + val);
            }
        }

        public void Clear()
        {
            mCache.Clear();
        }

        Object IUnityCache.Pop(string key)
        {
            return Pop(key);
        }

        public bool Push(string key, Object val)
        {
            return Push(key, val as T);
        }

        public T Pop(string key)
        {
            if (!UseCache)
            {
                return null;
            }
            if (mCache.ContainsKey(key))
            {
                var val = mCache.Get(key);
                var comp = val as Component;
                if (comp)
                {
                    if (!UnityHelper.IsObjectExist(comp))
                    {
                        Debug.LogError("what the fuck " + comp);
                        return null;
                    }
                    if (!UnityHelper.IsObjectExist(comp.gameObject))
                    {
                        Debug.LogError("what the fuck " + comp);
                        return null;
                    }
                    var ac = comp as AssetComponent;
                    if (ac != null)
                    {
                        if (ac.Invalid)
                        {
                            return null;
                        }
                        ac.OnCacheDidPop();
                    }
                    comp.gameObject.SetActive(true);
                    comp.transform.SetParent(null);

                }
                if (OnCacheHit != null)
                {
                    OnCacheHit.Invoke(val);
                }
                return val;
            }
            return null;
        }

        public bool Push(string key, T val)
        {
            if (!UnityHelper.IsObjectExist(val))
            {
                Debug.LogError("What the fuck? " + val);
                return false;
            }
            if (!UseCache)
            {
                DestroyUnityObject(val, false);
                return false;
            }
            var ret = true;
            if (OnWillCache != null)
            {
                foreach (var d in OnWillCache.GetInvocationList())
                {
                    var handler = (Predicate<T>) d;
                    if (!handler(val))
                    {
                        ret = false;
                    }
                }
            }
            if (ret)
            {
                var comp = val as Component;
                if (comp != null)
                {
                    if(!UnityHelper.IsObjectExist(comp.gameObject))
                    {
                        Debug.LogError("What the fuck? gameobject null " + val);
                        return false;
                    }
                    var ac = comp as AssetComponent;
                    if (ac != null)
                    {
                        if (ac.Invalid)
                        {
                            Debug.LogError("What the fuck? Invalid " + val);
                            return false;
                        }
                        else
                        {
                            comp.transform.SetParent(UnityHelper.DisableParent);
                            ac.OnCacheDidPush();
                        }
                    }
                    else
                    {
                        comp.transform.SetParent(UnityHelper.DisableParent);
                    }
                }
                mCache.Add(key, val);
                if (OnDidCache != null)
                {
                    OnDidCache.Invoke(val);
                }
            }
            return ret;
        }
    }

    public static class UnityObjectCacheCenter
    {
        private static readonly HashMap<Type, IUnityCache> sAll = new HashMap<Type, IUnityCache>();
        private static readonly HashMap<string, IUnityCache> sTagAll = new HashMap<string, IUnityCache>();

        public static UnityObjectCache<T> GetTypeCache<T>() where T : Object
        {
            var handler = sAll.Get(typeof(T));
            if (handler == null)
            {
                handler = new UnityObjectCache<T>();
                sAll.Add(typeof(T), handler);
            }
            return handler as UnityObjectCache<T>;
        }

        public static IUnityCache GetTypeCache(Type t)
        {
            var handler = sAll.Get(t);
            if (handler == null)
            {
                var d = typeof(UnityObjectCache<>);
                d = d.MakeGenericType(t);
                handler = ReflectionUtil.CreateInstance(d) as IUnityCache;
                sAll.Add(t, handler);
            }
            return handler;
        }

        public static IUnityCache GetTagCache(string tag)
        {
            var handler = sTagAll.Get(tag);
            if (handler == null)
            {
                handler = new UnityObjectCache<Object>();
                sTagAll.Add(tag, handler);
            }
            return handler;
        }

        public static void ClearAll()
        {
            foreach (var entry in sAll)
            {
                entry.Value.Clear();
            }
            foreach (var entry in sTagAll)
            {
                entry.Value.Clear();
            }
        }
    }
}