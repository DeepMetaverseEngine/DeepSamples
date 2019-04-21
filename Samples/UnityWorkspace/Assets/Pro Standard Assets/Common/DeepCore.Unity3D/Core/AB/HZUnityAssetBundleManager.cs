using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCore.Unity3D
{
    /// <summary>
    /// HZUnityAssetBundle加载管理器.
    /// </summary>
    public class HZUnityAssetBundleManager : MonoBehaviour
    {
        private static HZUnityAssetBundleManager mInstance = null;
        private Dictionary<string, HZUnityAssetBundle> mABMap = null;
        private Dictionary<string, HZUnityABLoadAdapter> mLoadTaskMap = null;
        public AssetBundleManifest Manifest { get; private set; }
        public List<Shader> ShaderList { get; private set; }
        private Dictionary<string, List<string>> mDepsList = new Dictionary<string, List<string>>();
        public int PrefabCapacity = 50;
        public delegate void ShaderListLoadOverHandler(ShaderVariantCollection svc);
        public static ShaderListLoadOverHandler OnShaderListLoadOverCallBack;
        
        private bool mNeedCleanUp = false;

        //private HZUnityAssetBundleManager()
        //{
        //    mInstance = this;
        //    Init();
        //}
        //private List<HZUnityABLoadAdapter> mTempLoadTask = null;
        private AsyncOperation mUnloadOp = null;
        private bool mNeedUnload = false;
        void Awake()
        {
            mInstance = this;
            Init();
        }

        void Start()
        {
            
        }

        void Update()
        {
            //检查加载任务
            //mTempLoadTask = new List<HZUnityABLoadAdapter>(mLoadTaskMap.Values);
            //var iter = mTempLoadTask.GetEnumerator();
            //while (iter.MoveNext())
            //{
            //    if(iter.Current.OnAdapterUpdate())
            //    {
            //        iter.Current.Dispose();
            //        mLoadTaskMap.Remove(iter.Current.GetURL());
            //
            //    }
            //}
            //mTempLoadTask.Clear();
            using (var list = ListObjectPool<HZUnityABLoadAdapter>.AllocAutoRelease(mLoadTaskMap.Values))
            using (var iter = list.GetEnumerator())
            {
                while (iter.MoveNext())
                {
                    if (iter.Current.OnAdapterUpdate())
                    {
                        iter.Current.Dispose();
                        mLoadTaskMap.Remove(iter.Current.GetURL());
                    }
                }
            }

            //是否存在加载任务
            bool isLoading = false;
            if (mLoadTaskMap.Count == 0)
            {
                using (var item = mABMap.GetEnumerator())
                {
                    while (item.MoveNext())
                    {
                        if (item.Current.Value.BundleStatus == HZUnityAssetBundle.Status.LoadAsset || item.Current.Value.BundleStatus == HZUnityAssetBundle.Status.LoadDep)
                        {
                            isLoading = true;
                            break;
                        }
                    }
                }
   
            }
            else
            {
                isLoading = true;
            }
                //在合适的时候执行Cleanup
            if (!isLoading && mNeedCleanUp)
            {
                CleanAssetBundleMap(false);
                //Resources.UnloadUnusedAssets ();
                System.GC.Collect();
                mNeedCleanUp = false;
            }

            //检查unload进度
            if(mUnloadOp != null && mUnloadOp.isDone)
            {
                mUnloadOp = null; 
            }
            //在合适的时候执行unload
            if (!isLoading  && mNeedUnload)
            {
                mUnloadOp = Resources.UnloadUnusedAssets();
                mNeedUnload = false;
            }
        }

        private void Init()
        {
            mABMap = new Dictionary<string, HZUnityAssetBundle>();
            mLoadTaskMap = new Dictionary<string, HZUnityABLoadAdapter>();
            //mTempLoadTask = new List<HZUnityABLoadAdapter>();
            ShaderList = new List<Shader>();
            StartLoadManifest();
            StartLoadShaderList();
        }
        void StartLoadManifest()
        {
            HZUnityABLoadAdapter load = new HZUnityABLoadAdapter();
            load.LoadAsync = false;
            load.SetFinishCallBack(OnManifestFinish);
            load.Load("/res/" + GetPlatformForAssetBundles(), null);
            StartCoroutine(CheckLoadManifest(load));
        }
        IEnumerator CheckLoadManifest(HZUnityABLoadAdapter load)
        {
            while (!load.HasDone)
            {
                load.OnAdapterUpdate();
                yield return null;
            }
        }
        private void OnManifestFinish(HZUnityABLoadAdapter adapter)
        {
            if (adapter.GetAssetBundle() != null)
            {

                Manifest = adapter.GetAssetBundle().LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
            else
            {
                Debug.LogError("HZUnityAssetBundleManager can not find Manifest");
            }

        }
        void StartLoadShaderList()
        {
            //HZUnityABLoadAdapter load = new HZUnityABLoadAdapter();
            //load.LoadAsync = false;
            //load.SetFinishCallBack(OnShaderListFinish);
            //load.Load("/res/shaderslist.assetbundles", null);
            //StartCoroutine(CheckLoadShaderList(load));

            HZUnityABLoadAdapter shaderload = new HZUnityABLoadAdapter();
            shaderload.LoadAsync = false;
            shaderload.SetFinishCallBack(OnShaderVariantsFinish);
            shaderload.Load("/res/shadervariants.assetbundles", null);
            StartCoroutine(CheckLoadShaderVariants(shaderload));
        }
        IEnumerator CheckLoadShaderVariants(HZUnityABLoadAdapter load)
        {
            while (!load.HasDone)
            {
                load.OnAdapterUpdate();
                yield return null;
            }
        }
        private void OnShaderVariantsFinish(HZUnityABLoadAdapter adapter)
        {
            if (adapter.GetAssetBundle() != null)
            {
                AssetBundle ab = adapter.GetAssetBundle();
                var shaderobject = ab.LoadAllAssets();
                foreach (var sb in shaderobject)
                {
                    var item = sb as Shader;
                    if (item != null)
                    {
                        ShaderList.Add(item);
                    }
                }
                ShaderVariantCollection svc = ab.LoadAsset<ShaderVariantCollection>("ShaderVariants");
                if (svc == null)
                {
                    Debug.LogError("ShaderVariantCollection can not be Found ");
                    return;
                }
                svc.WarmUp();
                if (OnShaderListLoadOverCallBack != null)
                {
                    OnShaderListLoadOverCallBack.Invoke(svc);
                }

            }
            else
            {
                Debug.LogWarning("HZUnityAssetBundleManager can not find ShaderVariant");
            }

        }
        IEnumerator CheckLoadShaderList(HZUnityABLoadAdapter load)
        {
            while (!load.HasDone)
            {
                load.OnAdapterUpdate();
                yield return null;
            }
        }
        private void OnShaderListFinish(HZUnityABLoadAdapter adapter)
        {
            //if (adapter.GetAssetBundle() != null)
            //{

            //    Object[] shaderobject = adapter.GetAssetBundle().LoadAllAssets();
            //    ShaderVariantCollection svc = new ShaderVariantCollection();;
            //    foreach (Object sb in shaderobject)
            //    {
            //        if (sb is Shader)
            //        {
            //            ShaderList.Add((Shader)sb);
            //            //ShaderVariantCollection.ShaderVariant sv = new ShaderVariantCollection.ShaderVariant((Shader)sb,PassType.Normal);
            //            //svc.Add(sv);
            //        }
            //        else
            //        {
            //            Debug.Log("ShaderList add dif type=" + sb.GetType());
            //        }

            //    }
            //    //Shader.WarmupAllShaders();
            //    //svc.WarmUp();
                
                
            //}
            //else
            //{
            //    Debug.LogWarning("HZUnityAssetBundleManager can not find ShaderList");
            //}

        }
        public void Dispose()
        {
            CleanAssetBundleMap(true);
            mInstance = null;
        }
        public static HZUnityAssetBundleManager GetInstance()
        {
            if (mInstance == null)
            {
                Debug.LogError("HZUnityAssetBundleManager must create before use");
            }

            return mInstance;
        }
        public void UnloadUnusedAssets()
        {
            if(mUnloadOp == null)
            {
                mNeedUnload = true;
            }
        }
        public List<string> GetDepList(string name)
        {
            name = name.ToLower();
            string dep_key = name.ToLower().Replace("/res/", "");
            if (!mDepsList.ContainsKey(name))
            {
                string[] deps = null;
                if(Manifest != null)
                {
                    deps = Manifest.GetAllDependencies(dep_key);
                }
                else
                {
                    deps = new string[0];
                }

                List<string> ds = new List<string>(deps.Length);
                for (int i = 0; i < deps.Length; i++)
                {
                    if(deps[i] != "shaderslist.assetbundles"&&deps[i] != "shadervariants.assetbundles")
                    {
                        string key = "/res/" + deps[i];
                        //if (name != key)
                        //{
                        ds.Add(key);
                        //}
                        //else
                        //{
                        //    Debug.LogError("what the fuck");
                        //}
                    }
                }
                mDepsList[name] = (ds);
            }
            return mDepsList[name];
        }
        public void GetAssetBundle(string name, HZUnityABLoadAdapter.HZUnityLoadAdapterCallBack callBack, bool async = true)
        {
            name = name.ToLower();
            HZUnityAssetBundle ab = null;
            HZUnityABLoadAdapter adapter = null;
            if (mABMap.TryGetValue(name, out ab))   //从AB中寻找AssetBundle.
            {
                if (callBack != null)
                {
                    callBack.Invoke(ab);
                    //return null;
                }
            }
            else if (async && mLoadTaskMap.TryGetValue(name, out adapter)) //从正在加载的map中寻找.
            {
                if (callBack != null)
                {
                    adapter.AddCallBack(callBack);
                }
                //return null;
            }
            else //创建加载器.
            {
                HZUnityABLoadAdapter load = new HZUnityABLoadAdapter();
				load.LoadAsync = async;
                load.SetFinishCallBack(OnAdapterFinish);
                load.Load(name, callBack);
                if (async)
                {
                    mLoadTaskMap.Add(name, load);
                }
                else
                {
                    load.OnAdapterUpdate();
                }
                //return load;
            }

            //return null;
        }
        public AssetBundle GetAssetBundle(string name)
        {
            name = name.ToLower();
            HZUnityAssetBundle ret = null;
            mABMap.TryGetValue(name, out ret);
            return ret.AssetBundle;
        }
        public HZUnityAssetBundle GetHZUnityAssetBundle(string name)
        {
            name = name.ToLower();
            HZUnityAssetBundle ret = null;
            mABMap.TryGetValue(name, out ret);
            if (ret == null)
            {
                return null;
            }
            return ret;
        }
        public bool AddAssetBundle(string name, HZUnityAssetBundle ab)
        {
            if (!string.IsNullOrEmpty(name) && ab != null)
            {
                mABMap.Add(name, ab);
                return true;
            }

            Debug.Log("HZUnityAssetBundleManager AddAssetBundle Error: Invaild Data");

            return false;
        }
        /// <summary>
        /// 将remove操作push到一个队列中执行，只有当当前没有加载任务时才会触发.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isUnloadAll"></param>
        public void UnloadAssetBundle(string name, bool isUnloadAll = false, bool force = false)
        {
            HZUnityAssetBundle ab = null;
            if (mABMap.TryGetValue(name, out ab))
            {
                if (ab != null)
                {
                    if(ab.BundleType == HZUnityAssetBundle.Type.DEP_ASSET)
                    {
                        Debug.LogError("Cant unload a dep assetbundle: " + name);
                    }
                    List<string> deps = GetDepList(name);
                    foreach (var item in deps)
                    {
                        HZUnityAssetBundle mfab = null;
                        if (mABMap.TryGetValue(item, out mfab))
                        {
                            if (mfab != null)
                            {
                                if (mfab.TryUnloadDep(name, isUnloadAll, force))
                                {
                                    mABMap.Remove(item);
                                }
                            }
                        }
                    }
                    UnloadAssetBundleImmediate(name, isUnloadAll, force);
                }
            }
            else
            {
                //通常只有切换场景时没有清除资源的情况下强制卸载了所有ab才会出，可以用来监控逻辑加载卸载成对的情况
                Debug.LogWarning("[策划，测试请无视]UnloadAssetBundle Error, bundle not exists: " + name);
            }
        }
        public void UnloadAssetBundleImmediate(string name, bool isUnloadAll, bool force = false)
        {
            // 内部自动处理
            HZUnityAssetBundle ab = null;
            if (mABMap.TryGetValue(name, out ab))
            {
                if (ab != null)
                {
                    //TODO 销毁HZUnityAssetBundle，主要是缓存和prefab
                    if(ab.Unload(isUnloadAll, force))
                    {
                        mABMap.Remove(name);
                    }
                }
            }
            else
            {
                Debug.LogWarning("[策划，测试请无视]UnloadAssetBundleImmediate Error, bundle not exists: " + name);
            }
        }
        public void LoadDep(string name, bool loadABASync, bool loadAssetASync, HZUnityAssetBundle.LoadResCallBack callback, object userdata, string childAB)
        {
            HZUnityAssetBundleManager.GetInstance().GetAssetBundle(name, (HZUnityAssetBundle mfab) =>
            {
                if (mfab != null)
                {
                    mfab.MarkAsDeps(childAB);
                }
                callback(name, null, userdata, mfab != null);
            }, loadABASync);
        }
        public void LoadAsset(string name, string asset, bool loadABASync, bool loadAssetASync, HZUnityAssetBundle.LoadResCallBack callback, object userdata, System.Type type = null)
        {
            GetAssetBundle(name, (HZUnityAssetBundle mfab) =>
            {
                if (mfab != null)
                {
                    mfab.GetAsset(asset, loadAssetASync, callback, userdata, type);
                }
                else
                {
                    callback(asset, null, userdata, false);
                }
            }, loadABASync);
        }
        public void LoadAsset(string name, bool loadABASync, bool loadAssetASync, HZUnityAssetBundle.LoadResCallBack callback, object userdata, System.Type type = null)
        {
            int starIndex = name.LastIndexOf('/') + 1;
            int length = name.Length - starIndex - ".assetbundles".Length;
            string objectName = name.Substring(starIndex, length);
            LoadAsset(name.ToLower(), objectName, loadABASync, loadAssetASync, callback, userdata, type);
        }
        public void CleanAssetBundleMap(bool isUnloadAll)
        {
            Dictionary<string, HZUnityAssetBundle> temp = new Dictionary<string, HZUnityAssetBundle>();
            foreach (KeyValuePair<string, HZUnityAssetBundle> kvp in mABMap)
            {
                if (kvp.Value != null)
                {
                    if(!kvp.Value.Unload(isUnloadAll, true))
                    {
                        temp.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            mABMap.Clear();
            mABMap = temp;
            assetRef.Clear();
        }

        private readonly Dictionary<string, int> assetRef = new Dictionary<string, int>();

        public void AddAssetRef(string refName)
        {
            int i;
            if (assetRef.TryGetValue(refName, out i))
            {
                assetRef[refName] = i + 1;
            }
            else
            {
                assetRef[refName] = 1;
            }
        }

        public void RemoveAssetRef(string refName)
        {
            int refCount;
            if (assetRef.TryGetValue(refName, out refCount))
            {
                var newCount = refCount - 1;
                assetRef[refName] = newCount;
                if (newCount == 0)
                {
                    UnloadAssetBundle(refName, true);
                }
            }
            else
            {
                Debug.LogWarning("RemoveRef error: " + name);
            }
        }
        public void CleanAssetBundleMapInQueue()
        {
            mNeedCleanUp = true;
        }
        private void OnAdapterFinish(HZUnityABLoadAdapter adapter)
        {
            HZUnityAssetBundle mfab = adapter.GetHZUnityAssetBundle();
            if (mfab != null)
            {
                mABMap.Add(adapter.GetURL(), mfab);
            }

            if (mLoadTaskMap.ContainsKey(adapter.GetURL()))
            {
                mLoadTaskMap.Remove(adapter.GetURL());
            }
            else
            {
                Debug.Log("HZUnityAssetBundleManager can not find Adapter");
            }

            adapter.DispatchCallBack();
        }
        public bool ContainsBundle(string name) { return mABMap.ContainsKey(name); }

        public static string GetPlatformForAssetBundles()
        {
            RuntimePlatform platform = Application.platform;
            switch (platform)
            {
                case RuntimePlatform.Android:
                    return "android";
                case RuntimePlatform.IPhonePlayer:
                    return "ios";
                case RuntimePlatform.WebGLPlayer:
                    return "webgl";
//                 case RuntimePlatform.OSXWebPlayer:
//                 case RuntimePlatform.WindowsWebPlayer:
//                     return "standalonewindows";
                case RuntimePlatform.WindowsEditor:
#if UNITY_ANDROID
                return "android";
#elif UNITY_IOS
                    return "ios";
#else
                    return "standalonewindows";
#endif
                case RuntimePlatform.WindowsPlayer:
                    return "standalonewindows";
			case RuntimePlatform.OSXEditor:
				#if UNITY_IOS
				return "ios";
				#else
				return "standalonewindows";
				#endif
                case RuntimePlatform.OSXPlayer:
                    return "osx";
                default:
                    return null;
            }
        }
    }
}