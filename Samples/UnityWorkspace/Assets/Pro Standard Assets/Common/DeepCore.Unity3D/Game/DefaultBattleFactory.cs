using DeepCore.GameSlave;
using DeepCore.GameSlave.Client;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DeepCore.Unity3D
{
    public class DefaultBattleScene : BattleScene
    {
        public DefaultBattleScene(AbstractBattle client) : base(client)
        {
        }
    }

    public class DefaultBattleFactory : BattleFactory
    {
        public enum LayerSetting
        {
            UI = 5,
            STAGE_NAV = 8,
            CAGE = 9,
            CG = 10,
        }

        private DefaultTerrainAdapter mTerrainAdapater;
        private DefaultSoundAdapter mSoundAdapter;


        protected DefaultBattleFactory() : base()
        {
            mTerrainAdapater = new DefaultTerrainAdapter();
            mSoundAdapter = new DefaultSoundAdapter(8);
        }

        public override void Update(float deltaTime)
        {
        }

        public override BattleDecoration CreateBattleDecoration(BattleScene battleScene, ZoneEditorDecoration zf)
        {
            return new BattleDecoration(battleScene, zf);
        }

        public override BattleScene CreateBattleScene(AbstractBattle battle)
        {
            return new DefaultBattleScene(battle);
        }


        public override SoundAdapter SoundAdapter
        {
            get
            {
                return mSoundAdapter;
            }
        }

        public override TerrainAdapter TerrainAdapter
        {
            get
            {
                return mTerrainAdapater;
            }
        }

        public override int StageNavLay
        {
            get
            {
                return (int)LayerSetting.STAGE_NAV;
            }
        }

        public override ICamera Camera
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DisplayCell CreateDisplayCell(GameObject root, string name)
        {
            return new DisplayCell(root, name);
        }

        public override ComAICell CreateComAICell(BattleScene battleScene, ZoneObject obj)
        {
            ComAICell cell = null;
            if (obj is ZoneActor)
            {
                cell = new ComAIActor(battleScene, obj as ZoneActor);
            }
            else if (obj is ZoneUnit)
            {
                cell = new ComAIUnit(battleScene, obj as ZoneUnit);
            }
            else if (obj is ZoneItem)
            {
                cell = new ComAIItem(battleScene, obj as ZoneItem);
            }
            else if (obj is ZoneSpell)
            {
                cell = new ComAISpell(battleScene, obj as ZoneSpell);
            }

            if (cell != null)
            {
                cell.OnCreate();
            }

            return cell;
        }

        public override void OnError(string msg)
        {

        }

        public override void MakeDamplingJoint(GameObject body, GameObject from, GameObject to)
        {
            //DampingJoint dj = body.transform.GetComponentInChildren<DampingJoint>();
            //if (dj != null)
            //{
            //    dj.Hook(from.transform, to.transform);
            //}
        }
    }
    public class DefaultAudio : Audio
    {
        public string Name { get; set; }
        public AudioSource AudioSource { get; private set; }

        private static GameObject mSoundRoot;
        private static GameObject SoundRoot
        {
            get
            {
                if (mSoundRoot == null)
                {
                    mSoundRoot = new GameObject("SoundRoot");
                    mSoundRoot.SetActive(false);
                    GameObject.DontDestroyOnLoad(mSoundRoot);
                }
                return mSoundRoot;
            }
        }

        public DefaultAudio() : base()
        {
            GameObject sound = new GameObject("Sound3D");
            this.AudioSource = sound.AddComponent<AudioSource>();
        }

        public void Play()
        {
            this.AudioSource.gameObject.name = Name;
            this.AudioSource.gameObject.Parent(null);
            this.AudioSource.gameObject.SetActive(true);
            if (this.AudioSource.clip != null)
            {
                this.AudioSource.Play();
            }
        }

        public void Stop()
        {
            this.AudioSource.Stop();
            this.AudioSource.clip = null;
            if (this.AudioSource.gameObject != null)
            {
                this.AudioSource.gameObject.Parent(SoundRoot);
                this.AudioSource.gameObject.SetActive(false);
            }

        }


        public void Pause()
        {
            this.AudioSource.Pause();
        }

        public void UnPause()
        {
            this.AudioSource.UnPause();
        }

    }

    public class SoundLoader
    {
        private bool mDisposed;
        private string mAssetBundleName;
        private AudioClip mAudioClip;
        private System.Action<bool, AudioClip> mCallbacks;
        private bool mLoadFinished;

        public string AssetBundleName { get { return mAssetBundleName; } }


        public void Load(System.Action<bool, AudioClip> callback)
        {
            if (mLoadFinished)
            {
                callback(mAudioClip != null, mAudioClip);
                return;
            }

            mCallbacks += callback;
        }

        public void Dispose()
        {
            if (!mDisposed)
            {
                mDisposed = true;
                if (mAudioClip != null)
                {
                    UnityEngine.Object.DestroyImmediate(mAudioClip, true);
                    mAudioClip = null;
                }

                if (mCallbacks != null)
                {
                    mCallbacks(false, null);
                    mCallbacks = null;
                }
            }
        }

        public SoundLoader(string assetBundleName)
        {
            mAssetBundleName = assetBundleName.ToLower();
            FuckAssetLoader.GetOrLoad(mAssetBundleName, Path.GetFileNameWithoutExtension(mAssetBundleName), (loader) =>
            {
                OnLoadAssetFinish(loader);
            });

        }

        private void OnLoadAssetFinish(FuckAssetLoader loader)
        {
            if (loader.IsSuccess)
            {
                mAudioClip = loader.Audio;
                HZUnityAssetBundleManager.GetInstance().UnloadAssetBundleImmediate(mAssetBundleName, false);
            }

            if (mCallbacks != null)
            {
                mCallbacks(mAudioClip != null, mAudioClip);
                mCallbacks = null;
            }
            mLoadFinished = true;
        }
    }

    public class DefaultSoundAdapter : SoundAdapter
    {
        private ObjectPool<DefaultAudio> mAudioPoool = new ObjectPool<DefaultAudio>();
        private List<DefaultAudio> mLoopAudios = new List<DefaultAudio>();
        private Queue<DefaultAudio> mNormalAudios = new Queue<DefaultAudio>();
        private int mMaxNormalAudioNum;
        private HashMap<string, SoundLoader> mAudioLoaders = new HashMap<string, SoundLoader>();
        private DefaultAudio mBGM;
        private SoundLoader mBGMLoader;
        private DefaultAudio LastVoice = null;

        public DefaultAudio CurrentBGM
        {
            get { return mBGM; }
        }

        public override void Clear()
        {
            foreach (var elem in mAudioLoaders)
            {
                elem.Value.Dispose();
            }
            mAudioLoaders.Clear();
        }

        private void RemoveLoader(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                SoundLoader loader = mAudioLoaders.RemoveByKey(name.ToLower());
                if (loader != null)
                {
                    loader.Dispose();
                }
            }
        }

        private DefaultAudio GetNormalAudio()
        {
            DefaultAudio audio = null;
            if (mNormalAudios.Count >= mMaxNormalAudioNum)
            {
                audio = mNormalAudios.Dequeue();
                audio.Stop();
                mAudioPoool.Release(audio);
            }

            audio = mAudioPoool.Get();
            mNormalAudios.Enqueue(audio);
            return audio;
        }

        public DefaultSoundAdapter(int maxNormalAudioNum) : base()
        {
            mMaxNormalAudioNum = maxNormalAudioNum;
        }

        protected void LoadBGM(string name, System.Action<bool, AudioClip> callback)
        {
            name = name.ToLower();
            if (mBGMLoader != null)
            {
                if (mBGMLoader.AssetBundleName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    mBGMLoader.Load(callback);
                    return;
                }
                mBGMLoader.Dispose();
            }

            mBGMLoader = new SoundLoader(name);
            mBGMLoader.Load(callback);
        }

        protected void LoadAudio(string name, System.Action<bool, AudioClip> callback)
        {
            SoundLoader loader = null;
            name = name.ToLower();
            if (mAudioLoaders.TryGetValue(name, out loader))
            {
                if (callback != null)
                {
                    loader.Load(callback);
                }
                return;
            }

            loader = new SoundLoader(name);
            loader.Load(callback);
            mAudioLoaders.Add(name, loader);
        }

        public override void PlayBGM(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                LoadBGM(name, (succ_, res_) =>
                {
                    if (succ_)
                    {
                        if (mBGM == null)
                        {
                            mBGM = mAudioPoool.Get();
                            GameObject.DontDestroyOnLoad(mBGM.AudioSource.gameObject);
                        }

                        mBGM.Stop();
                        RemoveLoader(mBGM.Name);

                        mBGM.Name = name;
                        mBGM.AudioSource.clip = res_;
                        mBGM.AudioSource.loop = true;
                        mBGM.AudioSource.volume = 1f;
                        mBGM.AudioSource.spatialBlend = 0f;

                        mBGM.Play();
                    }
                });
            }
        }

        public override void PlaySound(string name, Vector3 pos, float distance = 20f)
        {
            LoadAudio(name, (succ_, res_) =>
            {
                if (succ_)
                {
                    DefaultAudio audio = GetNormalAudio();
                    PlaySound(ref audio, name, res_, pos, false, 0, null, distance);
                    //audio.Name = name;
                    //audio.AudioSource.gameObject.Position(pos);
                    //audio.AudioSource.clip = res_;
                    //audio.AudioSource.loop = false;
                    //audio.AudioSource.volume = 1f;
                    //audio.AudioSource.spatialBlend = 0.9f;
                    //audio.AudioSource.maxDistance = distance;

                    //AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
                    //if (script == null)
                    //{
                    //    script = audio.AudioSource.gameObject.AddComponent<AudioAutoStop>();
                    //}
                    //script.Auido = audio;
                    //script.Duration = audio.AudioSource.clip.length;
                    //script.TraceTarget = null;

                    audio.Play();
                }
            });
        }

        public override void PlaySound(string name, int timeMS, Vector3 pos, float distance = 20f)
        {
            LoadAudio(name, (succ_, res_) =>
            {
                if (succ_)
                {
                    DefaultAudio audio = GetNormalAudio();
                    PlaySound(ref audio, name, res_, pos, true, timeMS, null, distance);
                    //audio.Name = name;
                    //audio.AudioSource.gameObject.Position(pos);
                    //audio.AudioSource.clip = res_ as AudioClip;
                    //audio.AudioSource.loop = true;
                    //audio.AudioSource.volume = 1f;
                    //audio.AudioSource.spatialBlend = 0.9f;
                    //audio.AudioSource.maxDistance = distance;

                    //AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
                    //if (script == null)
                    //{
                    //    script = audio.AudioSource.gameObject.AddComponent<AudioAutoStop>();
                    //}
                    //script.Auido = audio;
                    //script.Duration = timeMS / 1000f;
                    //script.TraceTarget = null;
                    audio.Play();
                }
            });
        }

        public override Audio PlaySoundLoop(string name, Vector3 pos, float distance = 20f)
        {
            DefaultAudio audio = mAudioPoool.Get();
            LoadAudio(name, (succ_, res_) =>
            {
                int index = mLoopAudios.IndexOf(audio);
                if (index == -1)
                {
                    return;
                }

                if (succ_)
                {
                    PlaySound(ref audio, name, res_, pos, true, 0, null, distance);
                    //audio.Name = name;
                    //audio.AudioSource.gameObject.Position(pos);
                    //audio.AudioSource.clip = res_ as AudioClip;
                    //audio.AudioSource.loop = true;
                    //audio.AudioSource.volume = 1f;
                    //audio.AudioSource.spatialBlend = 0.9f;
                    //audio.AudioSource.maxDistance = distance;

                    AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
                    if (script != null)
                    {
                        GameObject.Destroy(script);
                    }

                    audio.Play();
                }
            });
            return audio;
        }

        public override void PlaySound(string name, GameObject obj, float distance = 20f)
        {
            LoadAudio(name, (succ_, res_) =>
            {
                if (succ_)
                {
                    DefaultAudio audio = GetNormalAudio();
                    PlaySound(ref audio, name, res_, Vector3.zero, true, 0, obj, distance);
                    //audio.Name = name;
                    //audio.AudioSource.gameObject.Position(obj.Position());
                    //audio.AudioSource.clip = res_ as AudioClip;
                    //audio.AudioSource.loop = false;
                    //audio.AudioSource.volume = 1f;
                    //audio.AudioSource.spatialBlend = 0.9f;
                    //audio.AudioSource.maxDistance = distance;

                    //AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
                    //if (script == null)
                    //{
                    //    script = audio.AudioSource.gameObject.AddComponent<AudioAutoStop>();
                    //}
                    //script.Auido = audio;
                    //script.Duration = audio.AudioSource.clip.length;
                    //script.TraceTarget = obj;

                    audio.Play();
                }
            });
        }

        public override void PlaySound(string name, int timeMS, GameObject obj, float distance = 20f)
        {
            LoadAudio(name, (succ_, res_) =>
            {
                if (succ_)
                {
                    DefaultAudio audio = GetNormalAudio();
                    PlaySound(ref audio, name, res_, Vector3.zero, true, timeMS, obj, distance);
                    //audio.Name = name;
                    //audio.AudioSource.gameObject.Position(obj.Position());
                    //audio.AudioSource.clip = res_ as AudioClip;
                    //audio.AudioSource.loop = true;
                    //audio.AudioSource.volume = 1f;
                    //audio.AudioSource.spatialBlend = 0.9f;
                    //audio.AudioSource.maxDistance = distance;

                    //AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
                    //if (script == null)
                    //{
                    //    script = audio.AudioSource.gameObject.AddComponent<AudioAutoStop>();
                    //}
                    //script.Auido = audio;
                    //script.Duration = timeMS / 1000f;
                    //script.TraceTarget = obj;

                    audio.Play();
                }
            });
        }

        public override Audio PlaySoundLoop(string name, GameObject obj, float distance = 20f)
        {
            DefaultAudio audio = GetNormalAudio();
            LoadAudio(name, (succ_, res_) =>
            {
                int index = mLoopAudios.IndexOf(audio);
                if (index == -1)
                {
                    return;
                }

                if (succ_)
                {
                    PlaySound(ref audio, name, res_, Vector3.zero, true, 0, obj, distance);
                    //audio.Name = name;
                    //audio.AudioSource.gameObject.Position(obj.Position());
                    //audio.AudioSource.clip = res_ as AudioClip;
                    //audio.AudioSource.loop = true;
                    //audio.AudioSource.volume = 1f;
                    //audio.AudioSource.spatialBlend = 0.9f;
                    //audio.AudioSource.maxDistance = distance;

                    AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
                    if (script != null)
                    {
                        GameObject.Destroy(script);
                    }

                    audio.Play();
                }
            });
            return audio;
        }

        public virtual void PlayVoice(string name)
        {
            LoadAudio(name, (succ_, res_) =>
            {
                if (succ_)
                {
                    DefaultAudio audio = GetNormalAudio();
                    PlaySound(ref audio, name, res_, Vector3.zero, true, 0, null, 20);
                    audio.AudioSource.spatialBlend = 0;
                    LastVoice = audio;
                    audio.Play();

                }
            });
        }
        public virtual void StopLastVoice()
        {
            if (LastVoice != null)
            {
                if (LastVoice.AudioSource != null && LastVoice.AudioSource.isPlaying)
                {
                    LastVoice.AudioSource.Stop();
                }
                LastVoice = null;
            }
        }
        private void PlaySound(ref DefaultAudio audio, string Name, AudioClip res_, Vector3 pos, bool loop, int timeMS, GameObject obj, float distance)
        {
            audio.Name = Name;
            if (obj != null)
            {
                audio.AudioSource.gameObject.Position(obj.Position());
            }
            else
            {
                audio.AudioSource.gameObject.Position(pos);
            }
            audio.AudioSource.clip = res_ as AudioClip;
            audio.AudioSource.loop = loop;
            audio.AudioSource.volume = 1f;
            audio.AudioSource.spatialBlend = 1f;
            audio.AudioSource.spread = 180;
            audio.AudioSource.minDistance = 3;
            audio.AudioSource.maxDistance = distance;


            AudioAutoStop script = audio.AudioSource.gameObject.GetComponent<AudioAutoStop>();
            if (script == null)
            {
                script = audio.AudioSource.gameObject.AddComponent<AudioAutoStop>();
            }
            script.Auido = audio;
            if (timeMS != 0)
            {
                script.Duration = timeMS / 1000f;
            }
            else
            {
                script.Duration = audio.AudioSource.clip.length;
            }
            script.TraceTarget = obj;
        }

        public override void StopSoundLoop(Audio sound)
        {
            DefaultAudio audio = sound as DefaultAudio;
            if (audio != null)
            {
                int index = mLoopAudios.IndexOf(sound as DefaultAudio);
                if (index != -1)
                {
                    mLoopAudios.Remove(audio);
                }
                audio.Stop();
                mAudioPoool.Release(audio);
            }
        }

        public override void StopSound(Audio sound)
        {
            DefaultAudio da = sound as DefaultAudio;
            if (da != null)
            {
                da.Stop();
            }
        }

        public override void StopBGM()
        {
            if (mBGM != null)
            {
                mBGM.Stop();
            }
        }

        public override void PauseBGM()
        {
            if (CurrentBGM != null)
            {
                CurrentBGM.Pause();
            }
        }

        public override void UnPauseBGM()
        {
            if (CurrentBGM != null)
            {
                CurrentBGM.UnPause();
            }
        }

        public override void PauseSound(Audio sound)
        {
            DefaultAudio da = sound as DefaultAudio;
            if (da != null)
            {
                da.Pause();
            }
        }

        public override void UnPauseSound(Audio sound)
        {
            DefaultAudio da = sound as DefaultAudio;
            if (da != null)
            {
                da.UnPause();
            }
        }

    }
    public class DefaultTerrainAdapter : TerrainAdapter
    {
        private string mAssetBundleName;
        private string mAsssetName;
        private FuckAssetLoader mAssetLoader;
        private System.Action<bool, GameObject> mCallback;
        private GameObject mMapRoot;
        protected GameObject MapRoot { get { return mMapRoot; } }


        public override void Load(string assetBundleName, System.Action<bool, GameObject> callback)
        {
            if (!string.IsNullOrEmpty(assetBundleName) && callback != null)
            {
                mAssetBundleName = assetBundleName.ToLower();
                mAsssetName = Path.GetFileNameWithoutExtension(mAssetBundleName);
                mCallback = callback;
                mAssetLoader = FuckAssetLoader.GetOrLoadImmediate(mAssetBundleName, mAsssetName);
                OnLoadFinish(mAssetLoader);
            }
        }

        protected virtual void OnLoadFinish(FuckAssetLoader loader)
        {
            if (!loader.IsSuccess)
            {
                mCallback(false, null);
                return;
            }

            mMapRoot = GameObject.Instantiate((GameObject)loader.AssetSource);
            mMapRoot.name = "MapNode";
            mMapRoot.Position(Vector3.zero);

            LoadLightmap(loader.Bundle);
            LoadNav(loader.Bundle);

            Transform statics = mMapRoot.transform.Find("static");
            if (statics != null)
            {
                StaticBatchingUtility.Combine(statics.gameObject);
            }
            // fog settings
            InitFogParam(mMapRoot);

            // camera effect?
            GameObject objA = GameObject.Find("filter_a");
            GameObject objB = GameObject.Find("filter_b");
            if (objA != null && objB != null)
            {
                objA.transform.parent = Camera.main.gameObject.transform;
                objB.transform.parent = Camera.main.gameObject.transform;
                //position
                objA.transform.localPosition = new Vector3(0, 0, 0.24f);
                objB.transform.localPosition = new Vector3(0, 0, 0.24f);
                //rotation
                objA.transform.localRotation = Quaternion.Euler(270, 0, 0);
                objB.transform.localRotation = Quaternion.Euler(270, 0, 0);
                //scale
                float scaleW = 0.04f;
                float scaleH = 0.0225f;
                objA.transform.localScale = new Vector3(scaleW, 1, scaleH);
                objB.transform.localScale = new Vector3(scaleW, 1, scaleH);
            }

            foreach (var file in mAssetLoader.GetDeps())
            {
                if (file.IndexOf("shaderslist", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    HZUnityAssetBundleManager.GetInstance().UnloadAssetBundleImmediate(file, false, true);
                }
            }

            mCallback(true, mMapRoot);
        }

        protected virtual bool InitFogParam(GameObject mMapRoot)
        {
            //             FogAmbientColorSetting fs = mMapRoot.GetComponent<FogAmbientColorSetting>();
            //             if (null != fs && fs.fog)
            //             {
            //                 RenderSettings.fog = true;
            //                 RenderSettings.fogColor = fs.fogColor;
            //                 RenderSettings.fogDensity = fs.fogDensity;
            //                 RenderSettings.fogMode = fs.fogMode;
            //             }
            //             else
            //             {
            //                 RenderSettings.fog = false;
            //             }
            return false;
        }

        private void LoadLightmap(HZUnityAssetBundle ab)
        {
            List<UnityEngine.Object> lms = new List<UnityEngine.Object>();
            int count = 0;
            UnityEngine.Object lightMapObj;
            do
            {
                string reslight = "Lightmap-" + count + "_comp_light";
                lightMapObj = ab.AssetBundle.LoadAsset(reslight);
                if (lightMapObj != null)
                {
                    lms.Add(lightMapObj);
                    ++count;
                }
                else
                {
                    break;
                }
            }
            while (true);

            if (lms.Count > 0)
            {
                LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
                UnityEngine.LightmapData[] lmDataSet = new UnityEngine.LightmapData[lms.Count];

                for (int i = 0; i < lms.Count; i++)
                {
                    Texture2D tex = lms[i] as Texture2D;
                    if (tex != null)
                    {
                        UnityEngine.LightmapData lmData = new UnityEngine.LightmapData();
#if UNITY_5_6_OR_NEWER
                        lmData.lightmapColor = (Texture2D)tex;
#else
                        lmData.lightmapLight = (Texture2D)tex;
#endif
                        lmDataSet[i] = lmData;
                    }
                }
                LightmapSettings.lightmaps = lmDataSet;
            }
            InitLightMapParam(mMapRoot);
        }

        protected virtual bool InitLightMapParam(GameObject mMapRoot)
        {
            //             LightmapParam[] lmd = mMapRoot.GetComponentsInChildren<LightmapParam>(true);
            //             for (int i = 0; i < lmd.Length; i++)
            //             {
            //                 Renderer r = lmd[i].gameObject.GetComponent<Renderer>();
            //                 r.gameObject.isStatic = true;
            //                 r.lightmapIndex = lmd[i].lightmapIndex;
            //                 r.lightmapScaleOffset = lmd[i].lightmapScaleOffset;
            //             }
            return false;
        }

        private void LoadNav(HZUnityAssetBundle ab)
        {
            string resnav = mAsssetName + "_nav";
            UnityEngine.Object navObj = ab.AssetBundle.LoadAsset(resnav);
            if (navObj != null)
            {
                GameObject nav = (GameObject)GameObject.Instantiate(navObj);
                nav.layer = BattleFactory.Instance.StageNavLay;
                nav.transform.parent = mMapRoot.transform;

                foreach (Transform e in nav.GetComponentInChildren<Transform>())
                {
                    MeshCollider bc = e.gameObject.GetComponent<MeshCollider>();
                    if (bc == null)
                    {
                        bc = e.gameObject.AddComponent<MeshCollider>();
                    }
                    bc.gameObject.layer = nav.layer;
                }
            }
        }

    }


}
