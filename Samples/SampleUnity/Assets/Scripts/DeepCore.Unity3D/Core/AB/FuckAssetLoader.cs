using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DeepCore.Unity3D
{
    public class FuckAssetLoader : CustomYieldInstruction
    {
        private static int sNextID = 1;

        public static int GenID()
        {
            var ret = sNextID++;
            if (ret == 0)
            {
                ret = GenID();
            }
            return ret;
        }

        private static readonly HashMap<int, WeakReference> sAllLoader = new HashMap<int, WeakReference>();

        public static FuckAssetLoader GetLoader(int id)
        {
            var w =  sAllLoader.Get(id);
            if (w != null && w.IsAlive)
            {
                return (FuckAssetLoader) w.Target;
            }

            return null;
        }

        ~FuckAssetLoader()
        {
            sAllLoader.Remove(ID);
        }

        private FuckAssetLoader()
        {
            ID = GenID();
            sAllLoader.Add(ID, new WeakReference(this));
        }
        public readonly int ID;

        private const int MaxAsyncLoadingLoader = int.MaxValue;

        public override bool keepWaiting
        {
            get { return !IsDone; }
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}] ", BundleName, IsSuccess);
        }

        public bool IsGameObject
        {
            get { return AssetObject is GameObject; }
        }

        public bool IsAudioClip
        {
            get { return AssetObject is AudioClip; }
        }

        public bool IsDiscard { get; private set; }

        public void Discard()
        {
            IsDiscard = true;
            //if (IsDone)
            //{
            //    if (mFirstLinkedLoader != null)
            //    {
            //        mFirstLinkedLoader.IsLinkedLoad = false;
            //    }
            //    else
            //    {
            //        if (AssetObject is AudioClip)
            //        {
            //            Resources.UnloadAsset(AssetObject);
            //        }
            //        else
            //        {
            //            HZUnityAssetBundleManager.GetInstance().CheckAssetRef(BundleName);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 表明该资源是一个通过link方式加载进来的
        /// </summary>
        public bool IsLinkedLoad { get; private set; }


        public bool IsSuccess
        {
            get { return AssetObject; }
        }

        public Object AssetObject { get; private set; }

        public bool IsDone { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool ActualImmediate { get; private set; }

        public HZUnityAssetBundle Bundle { get; private set; }

        #region 启动参数

        public readonly string BundleName;
        public readonly string AssetName;
        public readonly bool Async;

        #endregion

        private Queue<FuckAssetLoader> mLinkedLoader;
        private int mNextDepIndex;
        private List<string> mDeps;
        private Action<FuckAssetLoader> mLoadAction;

        private static readonly HashMap<string, FuckAssetLoader> sLoadingLoader = new HashMap<string, FuckAssetLoader>();
        private static readonly List<FuckAssetLoader> sPausingLoader = new List<FuckAssetLoader>(5);

        public string[] GetDeps()
        {
            if (mDeps != null)
            {
                return mDeps.ToArray();
            }

            return new string[0];
        }

        private static void TryStartPausingLoader()
        {
            //todo empty检测移入for循环中，提升重复加载某一资源的效率
            var maxEmpty = MaxAsyncLoadingLoader - sLoadingLoader.Count;
            if (maxEmpty == 0)
            {
                return;
            }
            for (var i = 0; i < sPausingLoader.Count && i < maxEmpty; i++)
            {
                var cur = sPausingLoader[i];
                FuckAssetLoader loader;
                if (sLoadingLoader.TryGetValue(cur.BundleName, out loader))
                {
                    loader.AddLinkedLoader(cur);
                    maxEmpty = maxEmpty + 1;
                }
                else
                {
                    sPausingLoader[i].StartLoading();
                }
            }
            sPausingLoader.RemoveRange(0, Math.Min(maxEmpty, sPausingLoader.Count));
        }

        public static FuckAssetLoader LoadImmediate(string bundleName)
        {
            return LoadImmediate(bundleName, null);
        }

        public static FuckAssetLoader LoadImmediate(string bundleName, string assetName)
        {
            return Load(bundleName, assetName, false, null);
        }

        public static FuckAssetLoader Load(string bundleName)
        {
            return Load(bundleName, assetName: null);
        }

        public static FuckAssetLoader Load(string bundleName, string assetName)
        {
            return Load(bundleName, assetName, true, null);
        }

        public static FuckAssetLoader Load(string bundleName, string assetName, Action<FuckAssetLoader> cb)
        {
            var ret = Load(bundleName, assetName, true, cb);
            return ret;
        }

        public static FuckAssetLoader Load(string bundleName, Action<FuckAssetLoader> cb)
        {
            var ret = Load(bundleName, null, true, cb);
            return ret;
        }

        private static FuckAssetLoader Load(string bundleName, string assetName, bool async, Action<FuckAssetLoader> cb)
        {
            return new FuckAssetLoader(bundleName, assetName, async, cb);
        }

        private static Dictionary<string, string> nameDict = new Dictionary<string, string>();

        public static string GetAssetNameFromBundleName(string bundleName)
        {
            string rtn;
            if(!nameDict.TryGetValue(bundleName, out rtn))
            {
                rtn = Path.GetFileNameWithoutExtension(bundleName);
                nameDict[bundleName] = rtn;
            }

            return rtn;
        }

        public FuckAssetLoader(string bundleName, string assetName, Object obj) : this()
        {
            BundleName = bundleName.ToLower();
            AssetName = assetName;
            IsLinkedLoad = true;
            if (string.IsNullOrEmpty(AssetName))
            {
                AssetName = GetAssetNameFromBundleName(BundleName);
            }
#if (UNITY_EDITOR && !UNITY_ANDROID) || UNITY_STANDALONE
            if (BundleName.ToLower() != BundleName)
            {
                Debug.LogError("[FuckAssetLoader]assetbundle name must be lower!" + BundleName);
            }
#endif
            Async = false;
            OnAssetLoadCallBack(AssetName, obj, null, obj);
        }


        public FuckAssetLoader(string bundleName,
            string assetName = null,
            bool async = true,
            Action<FuckAssetLoader> cb = null) : this()
        {
            BundleName = bundleName.ToLower();
            AssetName = assetName;
            if (string.IsNullOrEmpty(AssetName))
            {
                AssetName = GetAssetNameFromBundleName(BundleName);
            }

#if (UNITY_EDITOR && !UNITY_ANDROID) || UNITY_STANDALONE
            if (BundleName.ToLower() != BundleName)
            {
                Debug.LogError("[FuckAssetLoader]assetbundle name must be lower!" + BundleName);
            }
#endif


            Async = async;
            mLoadAction = cb;

            if (string.IsNullOrEmpty(BundleName))
            {
                OnAssetLoadCallBack(null, null, null, false);
                return;
            }
            if (!async)
            {
                StartLoading();
                return;
            }

            FuckAssetLoader loader;
            if (sLoadingLoader.TryGetValue(BundleName, out loader))
            {
                loader.AddLinkedLoader(this);
            }
            else
            {
                if (sLoadingLoader.Count > MaxAsyncLoadingLoader)
                {
                    sPausingLoader.Add(this);
                }
                else
                {
                    StartLoading();
                }
            }
        }

        private void OnAssetLoadCallBack(string name, Object o, object userdata, bool isLoadOk)
        {
            if (mInStartLoading && Async)
            {
                ActualImmediate = true;
            }
            if (!isLoadOk)
            {
                OnLoadError("Asset Load Error");
            }
            else
            {
                OnLoadSuccess(o);
            }
        }

        private void OnBundleLoadCallBack(HZUnityAssetBundle ab)
        {
            if (ab == null)
            {
                OnLoadError("AssetBundle Load Error ");
            }
            else
            {
                Bundle = ab;
                ab.GetAsset(AssetName, Async, OnAssetLoadCallBack, null);
            }
        }

        private void OnDepLoadCallBack(string name, Object o, object userdata, bool isLoadOk)
        {
            if (!isLoadOk)
            {
                Debug.LogError(this + "LoadDep " + name);
            }
            LoadNextDep();
        }

        private void LoadNextDep()
        {
            if (mDeps != null && mNextDepIndex < mDeps.Count)
            {
                HZUnityAssetBundleManager.GetInstance().LoadDep(mDeps[mNextDepIndex++], Async, Async, OnDepLoadCallBack, null, BundleName);
            }
            else
            {
                HZUnityAssetBundleManager.GetInstance().GetAssetBundle(BundleName, OnBundleLoadCallBack, Async);
            }
        }

        private bool mInStartLoading;

        private void StartLoading()
        {
            mInStartLoading = true;
#if (UNITY_EDITOR && !UNITY_ANDROID) || UNITY_STANDALONE
            Debug.Log(this + "StartLoading " + sLoadingLoader.Count);
#endif
            sLoadingLoader.Add(BundleName, this);
            mNextDepIndex = 0;
            mDeps = HZUnityAssetBundleManager.GetInstance().GetDepList(BundleName);
            //string dep = string.Empty;
            //mDeps.ForEach(e => dep = dep + e);
            //if (!string.IsNullOrEmpty(dep))
            //{
            //    Debug.Log("deplist:" + dep);
            //}
            LoadNextDep();
            mInStartLoading = false;
        }

        /// <summary>
        /// 链接一个新的FuckAssetLoader, 加载完毕后调用该loader的OnBundleLoadCallBack
        /// </summary>
        /// <returns></returns>
        private void AddLinkedLoader(FuckAssetLoader loader)
        {
            if (mLinkedLoader == null)
            {
                mLinkedLoader = new Queue<FuckAssetLoader>(2);
            }
            mLinkedLoader.Enqueue(loader);
        }

        private void OnLoadError(string errMessage)
        {
            ErrorMessage = errMessage;
            if (!string.IsNullOrEmpty(BundleName))
            {
                Debug.LogError(this + errMessage);
            }
            OnLoadFinish();
        }


        private void OnLoadSuccess(Object obj)
        {
            if (!IsLinkedLoad && obj is AudioClip)
            {
                //AudioClip 不需要缓存AB
                HZUnityAssetBundleManager.GetInstance().UnloadAssetBundleImmediate(BundleName, false);
            }
            AssetObject = obj;
            OnLoadFinish();
        }

        private FuckAssetLoader mFirstLinkedLoader;
        /// <summary>
        /// 加载完毕（无论成功或失败）
        /// </summary>
        private void OnLoadFinish()
        {
            if (!IsSuccess && !string.IsNullOrEmpty(BundleName))
            {
                Debug.LogError(this + "loadError");
            }

            sLoadingLoader.Remove(BundleName);
            IsDone = true;
            if (!IsDiscard && mLoadAction != null)
            {
                mLoadAction.Invoke(this);
            }
            mLoadAction = null;
            if (mLinkedLoader != null)
            {
                while (mLinkedLoader.Count > 0)
                {
                    var loader = mLinkedLoader.Dequeue();
                    if (!loader.IsDiscard)
                    {
                        loader.IsLinkedLoad = true;
                        loader.Bundle = Bundle;
                        loader.OnAssetLoadCallBack(loader.AssetName, AssetObject, null, IsSuccess);
                        mFirstLinkedLoader = loader;
                    }
                }
            }
            if (IsDiscard)
            {
                if (mFirstLinkedLoader != null)
                {
                    mFirstLinkedLoader.IsLinkedLoad = false;
                }
                else
                {
                    if (AssetObject is AudioClip)
                    {
                        Resources.UnloadAsset(AssetObject);
                    }
                    else
                    {
                        HZUnityAssetBundleManager.GetInstance().CheckAssetRef(BundleName);
                    }
                }
            }

            TryStartPausingLoader();
        }
    }
}