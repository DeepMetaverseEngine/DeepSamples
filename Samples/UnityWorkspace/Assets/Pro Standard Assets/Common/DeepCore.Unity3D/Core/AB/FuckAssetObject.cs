//#define DEBUG_TRACE

using System;
using DeepCore.Unity3D.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace DeepCore.Unity3D
{
    /// <summary>
    /// FuckAssetObject加载和使用流程中的所有GameObject不能在业务层主动调用Unity的Destory
    /// FuckAssetObject不涉及业务逻辑，只用于资源性GameObject管理和资源重置
    /// </summary>
    public class FuckAssetObject : MonoBehaviour
    {
        #region LRUCache
        /// <summary>
        /// 默认打开缓存
        /// </summary>
        private static bool sUseCache = true;


        public static bool UseCache
        {
            get { return sUseCache; }
            set
            {
                if (sUseCache == value) return;
                sUseCache = value;
                if (!sUseCache)
                {
                    ClearCache();
                }
            }
        }

        public static int CacheCapacity = 800;

        private static readonly LRUCache<string, FuckAssetObject> Cache = new LRUCache<string, FuckAssetObject>(CacheCapacity, OnBeforRemoveObject);

        private static bool IsObjectExist(Object val)
        {
            return val != null && !val.Equals(null);
        }

        public static void ClearCache()
        {
            Cache.Clear();
        }

        public static FuckAssetObject GetCache(string key)
        {
            if (!UseCache)
            {
                return null;
            }
            if (Cache.CacheCount != CacheNode.transform.childCount)
            {
                Debug.LogError("what the fuck ");
            }
            if (Cache.ContainsKey(key))
            {
                var ao = Cache.Get(key);
                ao.IsInCache = false;
                ao.gameObject.SetActive(true);
                return ao;
            }
            return null;
        }

        private static void OnBeforRemoveObject(FuckAssetObject val)
        {
            if (IsObjectExist(val))
            {
                val.IsInCache = false;
                Object.Destroy(val.gameObject);
            }
            else
            {
                Debug.LogError("What the fuck? " + val);
            }
        }

        private static GameObject sCacheNode;

        public static GameObject CacheNode
        {
            get
            {
                if (sCacheNode == null)
                {
                    sCacheNode = new GameObject("FuckAssetObjectCache");
                    Object.DontDestroyOnLoad(sCacheNode);
                    sCacheNode.SetActive(false);
                }
                return sCacheNode;
            }
        }

        private static void AddToCache(FuckAssetObject val)
        {
            if (!UseCache)
            {
                return;
            }
            if (IsObjectExist(val))
            {
                val.transform.SetParent(CacheNode.transform);
                val.IsInCache = true;
                Cache.Add(val.CacheName, val);
            }
            else
            {
                Debug.LogError("What the fuck? " + val);
            }
        }

        public static void Unload(GameObject obj)
        {
            var comp = obj.GetComponent<FuckAssetObject>();
            if (comp)
            {
                comp.Unload();
            }
            else
            {
                GameObject.Destroy(obj);
            }
        }

        #endregion

        private string mStackTrack = string.Empty;

        public override string ToString()
        {
            return string.Format("[{0} {1} {2}]", BundleName, GetInstanceID(), mStackTrack);
        }

        //有名字代表应该被回收
        public string CacheName { get; private set; }

        public string BundleName { get; private set; }

        public int BundleVersion { get; private set; }

        public bool TrailRendererCheck;

        private TrailRenderer[] mTrailRenderer;
        public bool Invalid { get; private set; }

        public bool IsInCache { get; private set; }

        private bool CheckFuck()
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


        private Action<FuckAssetObject> mBeforeUnload;

        public event Action<FuckAssetObject> OnBeforeUnload
        {
            add { mBeforeUnload += value; }
            remove { mBeforeUnload -= value; }
        }

        private void OnEnable()
        {
            if (TrailRendererCheck)
            {
                if (mTrailRenderer == null)
                {
                    mTrailRenderer = gameObject.GetComponentsInChildren<TrailRenderer>(true);
                }

                foreach (var r in mTrailRenderer)
                {
                    r.Clear();
                }
            }
        }

        public void AddRef(string refName, int version)
        {
            if (!string.IsNullOrEmpty(BundleName))
            {
                return;
            }

            HZUnityAssetBundleManager.GetInstance().AddAssetRef(refName);
            BundleName = refName;
            BundleVersion = version;
            CacheName = BundleName;
        }


        public void Unload()
        {
            if (CheckFuck())
            {
                return;
            }
#if DEBUG_TRACE
        mStackTrack = new StackTrace().ToString();
#endif
            if (!UseCache)
            {
                Destroy(gameObject);
            }
            else
            {
                if (mBeforeUnload != null)
                {
                    mBeforeUnload.Invoke(this);
                }
                mBeforeUnload = null;
                gameObject.SetActive(false);
                AddToCache(this);
            }
        }

        private void RemoveRef()
        {
            HZUnityAssetBundle mfab = HZUnityAssetBundleManager.GetInstance().GetHZUnityAssetBundle(BundleName);
            if (mfab == null || mfab.Version != BundleVersion)
            {
                Debug.LogWarning("RemoveRef version not match: " + name);
                return;
            }
            HZUnityAssetBundleManager.GetInstance().RemoveAssetRef(BundleName);
        }

        private void OnDestroy()
        {
            RemoveRef();
            Invalid = true;
            if (IsInCache)
            {
                Debug.LogError("缓存正常的情况下不应该被Destory!");
            }
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
                ret = gameObject.FindChild(childName, StringComparison.OrdinalIgnoreCase);
                if (ret)
                {
                    if (mGameObjects == null)
                    {
                        mGameObjects = new HashMap<string, GameObject>();
                    }
                    mGameObjects[childName] = ret;
                }
            }
            return ret;
        }

        #endregion
    }
}