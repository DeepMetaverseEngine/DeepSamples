using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DeepCore.Unity3D
{
    public class FuckAssetLoader : CustomYieldInstruction
    {
        private const int MaxAsyncLoadingLoader = 20;


        public override bool keepWaiting
        {
            get { return !IsDone; }
        }

        public override string ToString()
        {
            return string.Format("[{0} {1} {2}] ", BundleName, IsSuccess, AssetSource);
        }

        public bool IsSuccess
        {
            get { return AssetSource || mAssetComp || mAudioClip; }
        }

        public bool IsDone { get; private set; }

        public string ErrorMessage { get; private set; }

        public HZUnityAssetBundle Bundle { get; private set; }
        public static Func<string, string> FixBundleNameDelegate;
        public static Func<string, string> FixAssetNameDelegate;

        private FuckAssetObject mAssetComp;

        public FuckAssetObject AssetComp
        {
            get
            {
                if (mAssetComp)
                {
                    return mAssetComp;
                }
                if (!AssetSource)
                {
                    return null;
                }
                if (AssetSource is GameObject)
                {
                    var gameObject = (GameObject) Object.Instantiate(AssetSource);
                    mAssetComp = gameObject.AddComponent<FuckAssetObject>();
                    AssetComp.AddRef(BundleName, Bundle.Version);
                }
                else
                {
                    Debug.LogError(this + "AssetSource is not GameObject ");
                }
                return mAssetComp;
            }
        }

        private AudioClip mAudioClip;

        public AudioClip Audio
        {
            get
            {
                if (mAudioClip)
                {
                    return mAudioClip;
                }
                if (!AssetSource)
                {
                    return null;
                }
                //todo AudioClip是否需要Instantiate?
                //if (AssetSource is AudioClip)
                //{
                //    mAudioClip = (AudioClip) Object.Instantiate(AssetSource);
                //}
                mAudioClip = AssetSource as AudioClip;
                return mAudioClip;
            }
        }


        /// <summary>
        /// Asset载体
        /// </summary>
        public Object AssetSource;

        #region 启动参数

        public readonly string BundleName;
        public readonly string AssetName;
        public readonly bool AsyncLoadBundle;
        public readonly bool AsyncLoadAsset;

        #endregion

        private Queue<FuckAssetLoader> mLinkedLoader;
        private int mNextDepIndex;
        private List<string> mDeps;
        private readonly Action<FuckAssetLoader> mLoadAction;

        private static readonly HashMap<string, FuckAssetLoader> sLoadingLoader = new HashMap<string, FuckAssetLoader>(MaxAsyncLoadingLoader);
        private static readonly List<FuckAssetLoader> sPausingLoader = new List<FuckAssetLoader>(5);

        public string[] GetDeps()
        {
            return mDeps.ToArray();
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

        public static FuckAssetLoader GetOrLoadImmediate(string bundleName,string assetName)
        {
            return GetOrLoad(bundleName, assetName, null, false, false);
        }


        public static FuckAssetLoader GetOrLoad(string bundleName, string assetName)
        {
            return GetOrLoad(bundleName, assetName, null, true, true);
        }

        /// <summary>
        /// 返回true 表示内部已按同步方式执行完毕
        /// </summary>
        /// <returns></returns>
        public static bool GetOrLoad(string bundleName, string assetName,Action<FuckAssetLoader> cb)
        {
            var loader = GetOrLoad(bundleName, assetName, cb, true, true);
            return loader.IsDone;
        }

        /// <summary>
        /// 统一加载入口 TODO asyncAssetBundle 和 asyncAsset合并为一个参数
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetName"></param>
        /// <param name="asyncAssetBundle"></param>
        /// <param name="asyncAsset"></param>
        /// <param name="cb"></param>
        /// <returns></returns>
        private static FuckAssetLoader GetOrLoad(
            string bundleName,
            string assetName,
            Action<FuckAssetLoader> cb,
            bool asyncAssetBundle,
            bool asyncAsset
        )
        {
            bundleName = FixBundleNameDelegate != null ? FixBundleNameDelegate(bundleName) : bundleName.ToLower();
            assetName = FixAssetNameDelegate != null ? FixAssetNameDelegate(assetName) : assetName.ToLower();
            var ret = new FuckAssetLoader(bundleName, assetName, asyncAssetBundle, asyncAsset, cb);
            //try load from cache

            //FuckAssetObject Cache
            var ao = FuckAssetObject.GetCache(bundleName);
            if (ao)
            {
                if (asyncAssetBundle || asyncAsset)
                {
                    UnityHelper.WaitForEndOfFrame(ret,p =>
                    {
                        p.IsDone = true;
                        p.mAssetComp = ao;
                        if (cb != null)
                        {
                            cb.Invoke(p);
                        }
                    });
                }
                return ret;
            }

            //同步加载
            if (!asyncAssetBundle && !asyncAsset)
            {
                ret.StartLoading();
                return ret;
            }

            //todo other asset cache
            //...
            //

            FuckAssetLoader loader;
            if (sLoadingLoader.TryGetValue(bundleName, out loader))
            {
                loader.AddLinkedLoader(ret);
            }
            else
            {
                if ((asyncAssetBundle || asyncAsset) && sLoadingLoader.Count > MaxAsyncLoadingLoader)
                {
                    sPausingLoader.Add(ret);
                }
                else
                {
                    ret.StartLoading();
                }
            }
            return ret;
        }

        private FuckAssetLoader(string bundleName, string assetName, bool asyncAssetBundle, bool asyncAsset, Action<FuckAssetLoader> cb)
        {
            BundleName = bundleName;
            AssetName = assetName;
            AsyncLoadBundle = asyncAssetBundle;
            AsyncLoadAsset = asyncAsset;
            mLoadAction = cb;
        }

        private void OnAssetLoadCallBack(string name, Object o, object userdata, bool isLoadOk)
        {
            if (mInStartLoading && (AsyncLoadBundle || AsyncLoadBundle))
            {
                UnityHelper.WaitForEndOfFrame(() =>
                {
                    if (!isLoadOk)
                    {
                        OnLoadError("Asset Load Error");
                    }
                    else
                    {
                        OnLoadSuccess(o);
                    }
                });
            }
            else
            {
                if (!isLoadOk)
                {
                    OnLoadError("Asset Load Error");
                }
                else
                {
                    OnLoadSuccess(o);
                }
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
                ab.GetAsset(AssetName, AsyncLoadAsset, OnAssetLoadCallBack, null);
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
                HZUnityAssetBundleManager.GetInstance().LoadDep(mDeps[mNextDepIndex++], AsyncLoadBundle, AsyncLoadAsset, OnDepLoadCallBack, null, BundleName);
            }
            else
            {
                HZUnityAssetBundleManager.GetInstance().GetAssetBundle(BundleName, OnBundleLoadCallBack, AsyncLoadBundle);
            }
        }

        private bool mInStartLoading;

        private void StartLoading()
        {
            mInStartLoading = true;
            Debug.Log(this + "StartLoading");
            sLoadingLoader.Add(BundleName, this);
            mNextDepIndex = 0;
            mDeps = HZUnityAssetBundleManager.GetInstance().GetDepList(BundleName);
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
            Debug.LogError(this + errMessage);
            OnLoadFinish();
        }

        private void OnLoadSuccess(Object obj)
        {
            AssetSource = obj;
            OnLoadFinish();
        }

        /// <summary>
        /// 加载完毕（无论成功或失败）
        /// </summary>
        private void OnLoadFinish()
        {
            if (AssetSource != null)
            {
                Debug.Log(this + "OnLoadFinish");
            }

            sLoadingLoader.Remove(BundleName);
            IsDone = true;
            if (mLoadAction != null)
            {
                mLoadAction.Invoke(this);
            }
            if (mLinkedLoader != null)
            {
                while (mLinkedLoader.Count > 0)
                {
                    var loader = mLinkedLoader.Dequeue();
                    loader.Bundle = Bundle;
                    loader.OnAssetLoadCallBack(loader.AssetName, AssetSource, null, IsSuccess);
                }
            }
            TryStartPausingLoader();
        }
    }
}