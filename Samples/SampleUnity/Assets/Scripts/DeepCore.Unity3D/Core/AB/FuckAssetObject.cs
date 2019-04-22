//#define DEBUG_TRACE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DeepCore.Unity3D.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DeepCore.Unity3D
{
    /// <summary>
    /// FuckAssetObject加载和使用流程中的所有GameObject不能在业务层主动调用Unity的Destory
    /// FuckAssetObject不涉及业务逻辑，只用于资源性GameObject管理和资源重置
    /// </summary>
    public sealed class FuckAssetObject : AssetComponent
    {
        static FuckAssetObject()
        {
            //默认缓存规则
            var gameObjectCache = UnityObjectCacheCenter.GetTypeCache<FuckAssetObject>();
            gameObjectCache.Capacity = 100;
        }

        private static readonly HashMap<int, FuckAssetObject> sAll = new HashMap<int, FuckAssetObject>();

        public static FuckAssetObject Get(int id)
        {
            return sAll.Get(id);
        }

        public int ID { get; private set; }

        public new GameObject gameObject
        {
            get
            {
                if (!CheckFuck())
                {
                    return base.gameObject;
                }
                return null;
            }
        }

        public new Transform transform
        {
            get
            {
                if (!CheckFuck())
                {
                    return base.transform;
                }
                return null;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            HZUnityAssetBundleManager.GetInstance().RemoveAssetRef(CacheName);
            sAll.Remove(ID);
        }

        /// <summary>
        /// 模拟刚加载成功的
        /// </summary>
        private void LooksLikeLoaded()
        {
        }
        private static FastString fs = new FastString();
        private static FuckAssetObject NewAssetObject(FuckAssetLoader loader, string trace)
        {
            if (!loader.IsGameObject)
            {
                if (!string.IsNullOrEmpty(loader.BundleName))
                {
                    fs.Set(loader.BundleName);
                    fs.Append(" is not gameobject");
                    Debug.LogError(fs.ToString());
                }
                return null;
            }
            var gameObject = (GameObject) Instantiate(loader.AssetObject);
            var ao = gameObject.AddComponent<FuckAssetObject>();
            ao.CacheName = loader.BundleName;
            ao.ID = loader.ID;
            ao.mTrace = trace;
            sAll.Add(ao.ID, ao);
            return ao;
        }

        private static FuckAssetLoader InternelLoad(string bundleName, string assetName, Action<FuckAssetObject> cb)
        {
            string trace = null;
#if UNITY_STANDALONE || UNITY_EDITOR
            trace = new StackTrace().ToString();
#endif
            return FuckAssetLoader.Load(bundleName, assetName, loader =>
            {
                //提前AddAssetRef 避免被UnusedAsset干掉
                if (loader.IsGameObject)
                {
                    HZUnityAssetBundleManager.GetInstance().AddAssetRef(loader.BundleName);
                }

                if (loader.ActualImmediate)
                {
                    UnityHelper.WaitForEndOfFrame(() =>
                    {
                        var ao = NewAssetObject(loader,trace);
                        cb.Invoke(ao);
                    });
                }
                else
                {
                    cb.Invoke(NewAssetObject(loader,trace));
                }
            });
        }


        public static FuckAssetObject GetOrLoadImmediate(string bundleName, string assetName)
        {
			bundleName = bundleName.ToLower();
#if (UNITY_EDITOR && !UNITY_ANDROID) || UNITY_STANDALONE
            if(bundleName.ToLower() != bundleName)
            {
                Debug.LogError("[HZUnityAssetBundleManager]assetbundle name must be lower!" + bundleName);
            }
#endif
            var ao = UnityObjectCacheCenter.GetTypeCache<FuckAssetObject>().Pop(bundleName);
            if (ao)
            {
                ao.LooksLikeLoaded();
                return ao;
            }
            var loader = FuckAssetLoader.LoadImmediate(bundleName, assetName);
            if (loader.IsGameObject)
            {
                HZUnityAssetBundleManager.GetInstance().AddAssetRef(loader.BundleName);
            }
#if UNITY_STANDALONE || UNITY_EDITOR
            var trace = new StackTrace().ToString();
            return NewAssetObject(loader, trace);
#else
            return NewAssetObject(loader,null);
#endif
        }

        public static int Load(string bundleName, string assetName, Action<FuckAssetObject> cb)
        {
            return InternelLoad(bundleName, assetName, cb).ID;
        }

        public static void PreLoad(string bundleName, string assetName)
        {
            InternelLoad(bundleName, assetName, (ao) =>
            {
                if (ao)
                {
                    ao.Unload();
                }
            });
        }
        
        public static int GetOrLoad(string bundleName, string assetName, Action<FuckAssetObject> cb)
        {
			bundleName = bundleName.ToLower();
#if (UNITY_EDITOR && !UNITY_ANDROID) || UNITY_STANDALONE
            if (bundleName.ToLower() != bundleName)
            {
                Debug.LogError("[HZUnityAssetBundleManager]assetbundle name must be lower!" + bundleName);
            }
#endif
            var ao = UnityObjectCacheCenter.GetTypeCache<FuckAssetObject>().Pop(bundleName);
            if (ao)
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                ao.mTrace = new StackTrace().ToString();
#endif
                UnityHelper.WaitForEndOfFrame(() =>
                {
                    if (!ao.IsUnload)
                    {
                        ao.LooksLikeLoaded();
                        cb.Invoke(ao);
                    }
                    else
                    {
                        Debug.LogError("这个操作真的神一般" + ao);
                    }
                });
                return ao.ID;
            }
            return InternelLoad(bundleName, assetName, cb).ID;
        }

        #region 一点点缓存查找支持

        private HashMap<string, GameObject> mGameObjects;

        public GameObject FindNode(string childName)
        {
            if (CheckFuck())
            {
                return null;
            }

            GameObject ret = null;
            if (mGameObjects != null)
            {
                ret = mGameObjects.Get(childName);
            }
            if (ret == null)
            {
                var t = transform.FindRecursive(childName, StringComparison.OrdinalIgnoreCase);
                if (t)
                {
                    ret = t.gameObject;
                    if (mGameObjects == null)
                    {
                        mGameObjects = new HashMap<string, GameObject>();
                    }
                    mGameObjects[childName] = ret;
                }
            }
            return ret;
        }

        private HashMap<Type, ICollection> mComponentsMap;

        public new ICollection<T> GetComponentsInChildren<T>()
        {
            if (mComponentsMap == null)
            {
                mComponentsMap = new HashMap<Type, ICollection>();
            }
            ICollection ret;
            if (mComponentsMap.TryGetValue(typeof(T), out ret))
            {
                return ret as List<T>;
            }
            var list = new List<T>();
            GetComponentsInChildren<T>(true, list);
            mComponentsMap.Add(typeof(T), list);
            return list;
        }

        public void ResetTrailRenderer()
        {
            var all = GetComponentsInChildren<TrailRenderer>();
            foreach (var t in all)
            {
                t.Clear();
            }
        }

        protected internal override void OnCacheDidPush()
        {
            base.OnCacheDidPush();
            ResetTrailRenderer();
        }

        #endregion
    }
}