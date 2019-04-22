using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DeepCore.Unity3D
{
    [RequireComponent(typeof(AudioSource))]
    public class AssetAudio : AssetComponent
    {
        [Flags]
        public enum AudioType
        {
            BGM = 1,
            UI = 1 << 1,
            Dynamic = 1 << 2,
            Scene = 1<<3,
        }
        
        public int ID { get; private set; }
        private AudioSource mSource;
        //3d音乐音距
        private const int MinDistance = 3;
        private const int MaxDistance = 32;
        private AudioSource Source
        {
            get
            {
                if (!mSource)
                {
                    mSource = GetComponent<AudioSource>();
                }

                return mSource;
            }
        }

        public void SetSourceDistance(float min = MinDistance,float max = MaxDistance)
        {
            Source.minDistance = min;
            Source.maxDistance = max;
        }

        private static Transform sAudioSourceRoot;

        private static Transform AudioSourceRoot
        {
            get
            {
                if (!sAudioSourceRoot)
                {
                    var obj = new GameObject("AudioSourceRoot");
                    GameObject.DontDestroyOnLoad(obj);
                    sAudioSourceRoot = obj.transform;
                }

                return sAudioSourceRoot;
            }
        }

        private static readonly HashMap<int, AssetAudio> sEnableAssetAudio = new HashMap<int, AssetAudio>();


        internal static AssetAudio GetAssetAudio(int id)
        {
            return sEnableAssetAudio.Get(id);
        }

        internal static void StopAll(AudioType t)
        {
            foreach (var entry in sEnableAssetAudio)
            {
                if ((entry.Value.Type & t) != 0)
                {
                    entry.Value.Stop();
                }
            }
        }
        
        internal static void SetSoundVolume(AudioType t,float v)
        {
            foreach (var entry in sEnableAssetAudio)
            {
                if ((entry.Value.Type & t) != 0)
                {
                    entry.Value.Volume = v;
                }
            }
        }
        #region audioclip 资源管理

        /// <summary>
        /// 记录同一个AudioClip的引用次数
        /// </summary>
        private static readonly HashMap<string, int> sAudioClipRef = new HashMap<string, int>();

        //todo 还需要优化一下
        private static readonly HashMap<string, AudioClip> sAudioClipCache = new HashMap<string, AudioClip>();

        private static void DestroyAudioClip(string resName, AudioType type)
        {
            //Debug.Log("UnloadAsset " + clip);
            var clip = sAudioClipCache.RemoveByKey(resName);
            if (clip)
            {
                if (type == AudioType.BGM)
                {
                    Resources.UnloadAsset(clip);
                }
                else
                {
                    UnityObjectCacheCenter.GetTypeCache<AudioClip>().Push(resName, clip);
                }
            }
        }

        private static AudioClip GetClipCache(string resName)
        {
            var clip = sAudioClipCache.Get(resName);
            if (!clip)
            {
                clip = UnityObjectCacheCenter.GetTypeCache<AudioClip>().Pop(resName);
            }

            return clip;
        }

        private static void AddAudioClipRef(string resName)
        {
            int refCount;
            if (!sAudioClipRef.TryGetValue(resName, out refCount))
            {
                refCount = 0;
            }

            refCount = refCount + 1;
            sAudioClipRef[resName] = refCount;
        }

        private static void RemoveAudioClipRef(string resName, AudioType type)
        {
            int refCount;
            if (!sAudioClipRef.TryGetValue(resName, out refCount))
            {
                DestroyAudioClip(resName, type);
                return;
            }

            refCount = refCount - 1;
            if (refCount <= 0)
            {
                sAudioClipRef.Remove(resName);
                DestroyAudioClip(resName, type);
            }
            else
            {
                sAudioClipRef[resName] = refCount;
            }
        }

        #endregion

        static AssetAudio()
        {
            var c = UnityObjectCacheCenter.GetTypeCache<AssetAudio>();
            c.Capacity = 20;

            var cClip = UnityObjectCacheCenter.GetTypeCache<AudioClip>();
            cClip.Capacity = 10;
        }

        #region 动态控制参数

        /// <summary>
        /// 最大持续时间 小于等于0表示随AudioSource停止而处理Unload逻辑
        /// </summary>
        public float Duration;

        /// <summary>
        /// 持续时间是否包含加载时间
        /// </summary>
        public bool DurationContainsLoadTime;

        /// <summary>
        /// 是否为循环播放模式
        /// </summary>
        public bool Loop
        {
            get { return Source.loop; }
            set { Source.loop = value; }
        }

        /// <summary>
        /// 音量控制
        /// </summary>
        public float Volume
        {
            get { return Source.volume; }
            set { Source.volume = value; }
        }

        /// <summary>
        /// 3d, 2d 音效控制
        /// </summary>
        public float SpatialBlend
        {
            get { return Source.spatialBlend; }
            set { Source.spatialBlend = value; }
        }

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return Source.isPlaying; }
        }

        #endregion

        public AudioType Type { get; private set; }
        public bool AutoUnload { get; private set; }

        private FuckAssetLoader mNextLoader;

        /// <summary>
        /// 加载失败
        /// </summary>
        private bool mLoadFailed;

        /// <summary>
        /// 是否已暂停
        /// </summary>
        private bool mPause;

        /// <summary>
        /// 是否已停止
        /// </summary>
        private bool mStoped;

        /// <summary>
        /// 是否可以开始播放
        /// </summary>
        private bool mStartPlaying;

        /// <summary>
        /// 持续总时间
        /// </summary>
        public float PassTime { get; private set; }

        private bool mStopUpdate = false;
        private bool TryUnload()
        {
            var ret = false;
            if (mLoadFailed || mStoped)
            {
                // 加载失败
                ret = true;
            }
            else if (Duration > 0.001)
            {
                // 时长超出限制
                ret = Duration <= PassTime;
            }
            else if (mNextLoader != null)
            {
                //正在加载
                ret = false;
            }
            else if (!Loop && mStartPlaying)
            {
                // 已播放完成
                ret = !Source.isPlaying && !mPause;
            }

            if (ret)
            {
                if (AutoUnload)
                {
                    Unload();
                }
                else if(!mStoped)
                {
                    Stop();
                }
            }

            return ret;
        }

        private string mCurrentBundleName;

        //todo resName修改为BundleName
        private void SetAudioClip(string bundleName, AudioClip clip)
        {
            Source.clip = clip;
            mCurrentBundleName = bundleName;
            if (!sAudioClipCache.ContainsKey(bundleName))
            {
                sAudioClipCache.Add(bundleName, clip);
            }
            //加载完成 ，如果调用过Play且没有Pause，自动播放
            if (mStartPlaying && !mPause)
            {
                Source.Play();
            }
        }

        private bool TryLoading()
        {
            if (mNextLoader != null && mNextLoader.IsDone)
            {
                //提前加入ref
                AddAudioClipRef(mNextLoader.BundleName);
                ReleaseCurrent();
                //处理加载完成逻辑
                if (mNextLoader.IsAudioClip)
                {
                    SetAudioClip(mNextLoader.BundleName, (AudioClip) mNextLoader.AssetObject);
                }
                else
                {
                    mLoadFailed = true;
                    Debug.LogWarning(this + "load error ");
                }

                mNextLoader = null;
            }

            return Source.clip;
        }

        private void Update()
        {
            if (mStopUpdate || TryUnload())
            {
                return;
            }

            if (mStartPlaying && DurationContainsLoadTime && !mPause)
            {
                PassTime = PassTime + Time.deltaTime;
            }

            if (!TryLoading())
            {
                return;
            }

            if (mStartPlaying && !mPause)
            {
                PassTime = PassTime + Time.deltaTime;
            }
        }

        private void Reset()
        {
            Stop();
            Duration = 0;
            PassTime = 0;
            mLoadFailed = false;
            transform.position = Vector3.zero;
            ReleaseCurrent();
        }

        public override void Unload()
        {
            if (IsUnload)
            {
                return;
            }

            Reset();

            base.Unload();

            mStopUpdate = true;
        }

        public void Pause()
        {
            mStoped = false;
            mPause = true;
            if (!Source.clip)
            {
                return;
            }

            Source.Pause();
        }

        public void Stop()
        {
            if (Source.clip)
            {
                Source.Stop();
            }

            mPause = false;
            mStartPlaying = false;
            mStoped = true;
        }

        public void Play()
        {
            if (mPause)
            {
                //从暂停恢复
                mPause = false;
            }
            else
            {
                //重播 or 初次播放
                Stop();
            }

            mStoped = false;
            mStartPlaying = true;
            if (Source.clip)
            {
                Source.Play();
            }
        }

        private void ReleaseCurrent()
        {
            if (Source.clip)
            {
                RemoveAudioClipRef(mCurrentBundleName,Type);
                mCurrentBundleName = null;
            }

            Source.clip = null;
        }


        public string BundleName { get; private set; }

        /// <summary>
        /// 设置加载资源
        /// </summary>
        /// <param name="resName"></param>
        public void SetResource(string resName)
        {
            if (mNextLoader != null)
            {
                mNextLoader.Discard();
                mNextLoader = null;
            }
            
            BundleName = resName;
            mLoadFailed = false;
            if (!string.IsNullOrEmpty(resName))
            {
                var clip = GetClipCache(resName);
                if (clip)
                {
                    mNextLoader = new FuckAssetLoader(resName, null, clip);
                    TryLoading();
                }
                else
                {
                    mNextLoader = new FuckAssetLoader(resName);
                }
            }
            else
            {
                ReleaseCurrent();
            }
        }


        private void OnEnable()
        {
            if (ID == 0)
            {
                ID = this.GetInstanceID();
            }
            sEnableAssetAudio.Add(ID, this);
        }

        private void OnDisable()
        {
            sEnableAssetAudio.Remove(ID);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ReleaseCurrent();
        }

        /// <summary>
        /// AudioType自带的属性设置
        /// </summary>
        /// <param name="t"></param>
        protected virtual void SetOption(AudioType t)
        {
            //set option
            Source.volume = 1;
            switch (t)
            {
                case AudioType.BGM:
                    Source.spatialBlend = 0;
                    Source.loop = true;
                    break;
                case AudioType.UI:
                    Source.spatialBlend = 0;
                    break;
                case AudioType.Scene:
                case AudioType.Dynamic:
                    Source.spatialBlend = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("t", t, null);
            }
        }

        //todo xxxxxx 显示AssetAudio的同时播放数量
        internal static AssetAudio GetOrCreate(AudioType t, bool autoUnload)
        {
            var ao = UnityObjectCacheCenter.GetTypeCache<AssetAudio>().Pop(t.ToString());
            if (!ao)
            {
                var obj = new GameObject(t.ToString());
                ao = obj.AddComponent<AssetAudio>();
                ao.CacheName = t.ToString();
                ao.Source.minDistance = MinDistance;
                ao.Source.maxDistance = MaxDistance;
                ao.Type = t;
                ao.SetOption(t);
            }
            ao.mStopUpdate = false;
            ao.AutoUnload = autoUnload;
            ao.transform.SetParent(AudioSourceRoot);
            return ao;
        }
    }
}