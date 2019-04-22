using DeepCore.Unity3D.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DeepCore.Unity3D
{
    public class HZUnityAssetBundleLoader : HZUnityLoadIml
    {

        private static Action<string> mOnBeginLoadData;

        //public static event Action<string> OnBegionLoadData { add { mOnBeginLoadData += value; } remove { mOnBeginLoadData -= value; } }

        public enum HZUnityLoadType
        {
            MPQ,
            WWW
        }

        public static string UNITY_RES_SUFFIXS
        {
            get
            {
                string ret = "";
                foreach (var elem in gUnityResSuffixs)
                {
                    ret += elem + ";";
                }
                return ret;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string [] suffix = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    gUnityResSuffixs.Clear();
                    foreach (var elem in suffix)
                    {
                        gUnityResSuffixs.Add(value.ToLower());
                    }
                }
            }
        }

        private static HashSet<string> gUnityResSuffixs = new HashSet<string>();
        public static HZUnityLoadType DefaultLoadType { get; set; }
        private HZUnityLoadType mLoadType;
        private AssetBundle mAB = null;
        private string mErrorLog = null;
        private bool mHasDone = false;
		//System.Diagnostics.Stopwatch sw;

        #region WWW

        private WWW m3W = null;
		private string url;

        #endregion

        #region MPQ
    
        private AssetBundleCreateRequest mABRequest = null;

        #endregion


        public HZUnityAssetBundleLoader()
        {
            mLoadType = DefaultLoadType;
        }

        public HZUnityAssetBundleLoader(HZUnityLoadType loadType)
        {
            mLoadType = loadType;
        }

        public override float GetProgress()
        {
            if(mHasDone)
            {
                return 1;
            }

            switch(mLoadType)
            {
                case HZUnityLoadType.WWW:
                    return m3W.progress;
                case HZUnityLoadType.MPQ:
                    if(!mLoadAsync)
                    {
                        if(mAB != null) { return 1; }
                        else { return 0; }
                    }
                    else { return mABRequest.progress; }

            }

            return 0;
        }
        public override bool IsLoadFinish()
        {
            if(mHasDone) { return mHasDone; }
            switch(mLoadType)
            {
                case HZUnityLoadType.WWW:
                    if(m3W.isDone)
                    {
                        OnLoadFinish();
                        return true;
                    }
                    break;
                case HZUnityLoadType.MPQ:
                    if (mABRequest != null && mABRequest.isDone)
                    {
                        OnLoadFinish();
                        return true;
                    }
                    break;
            }

            return false;
        }
        public override void Load(string url)
        {
            //string suffix = System.IO.Path.GetExtension(url);
            //if (gUnityResSuffixs.Contains(suffix))
            //{
            //    mLoadType = HZUnityLoadType.WWW;
            //}
			//sw = System.Diagnostics.Stopwatch.StartNew();
			this.url = url;
            //StringBuilder sb = new StringBuilder();
            //sb.Length = 0;
            //sb.Append("mpq://");
            //sb.Append(url);

            //if (mOnBeginLoadData != null)
            //{
            //    mOnBeginLoadData.Invoke(sb.ToString());
            //}
            switch(mLoadType)
            {
                case HZUnityLoadType.WWW:
                    if (!mLoadAsync)
                    {
                        mAB = UnityDriver.UnityInstance.LoadAssetBundleImmediate(url);
                        mHasDone = true;
                        if (mAB == null) { mErrorLog = "LoadAssetBundleImmediate Error" + url; }
                    }
                    else
                    {
                        m3W = new WWW(url);
                    }
                    break;
                case HZUnityLoadType.MPQ:

                    //url = sb.ToString();

                    if(!mLoadAsync)
                    {
                        mAB = UnityDriver.UnityInstance.LoadAssetBundleImmediate(url);
                        mHasDone = true;
                        if(mAB == null) { mErrorLog = "LoadAssetBundleImmediate Error" + url; }
                    }
                    else
                    {
                        mABRequest = UnityDriver.UnityInstance.LoadAssetBundle(url);
                        if(mABRequest == null)
                        {
							mErrorLog = "LoadAssetBundle Error: " + url;
                            mHasDone = true;
                        }
                    }

                    break;
            }
        }
        public override string GetErrorLog()
        {
            return mErrorLog;
        }
        public override bool IsLoadError()
        {
            if(!string.IsNullOrEmpty(mErrorLog))
            {
                return true;
            }

            switch(mLoadType)
            {
                case HZUnityLoadType.WWW:
                    if (m3W != null && m3W.error != null)
                    {
                        mErrorLog = m3W.error.ToString() + m3W.url;
                        mHasDone = true;
                    }
                    return !string.IsNullOrEmpty(mErrorLog);
                case HZUnityLoadType.MPQ:
                    return !string.IsNullOrEmpty(mErrorLog);
            }

            return false;
        }
        public override AssetBundle GetAssetBundle()
        {
            switch(mLoadType)
            {
                case HZUnityLoadType.WWW:
                    return mAB;
                case HZUnityLoadType.MPQ:
                    return mAB;
            }
            return null;
        }
        public override void Dispose()
        {
            mHasDone = true;
            if(m3W != null)
            {
                m3W.Dispose();
                m3W = null;
            }

            if(mABRequest != null)
            {
                mABRequest = null;
            }

            mAB = null;
        }
        protected virtual void OnLoadFinish()
        {
            if(!string.IsNullOrEmpty(mErrorLog))
            {
                mAB = null;
                m3W.Dispose();
            }
            else
            {
                switch(mLoadType)
                {
                    case HZUnityLoadType.WWW:
                        mAB = m3W.assetBundle;
                        m3W.Dispose();
                        break;
                    case HZUnityLoadType.MPQ:
                        if(mLoadAsync) { mAB = mABRequest.assetBundle as AssetBundle; }
                        break;
                }
            }

			//sw.Stop();
			//Debug.LogError("[yyyyyyyyyyyyyyyy] " + url.ToString() + " "+sw.ElapsedMilliseconds/1000f);
            mHasDone = true;
        }

    }
}