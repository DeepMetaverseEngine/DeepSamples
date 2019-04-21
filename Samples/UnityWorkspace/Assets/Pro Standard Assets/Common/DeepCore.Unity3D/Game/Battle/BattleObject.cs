using DeepCore.Unity3D.Utils;
using DeepCore;
using DeepCore.Concurrent;
using DeepCore.GameData.Zone;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public class SeqAnim : IDisposable
    {
        public class AnimData
        {
            public string name;
            /// <summary>
            /// second
            /// </summary>
            public float duration;
            public bool crossFade;
            public WrapMode wrapMode = WrapMode.Once;
            public float speed = 1f;
        }

        /// <summary>
        /// param1 animName, param2 crossFade, param3 wrapMode, param4 speed, return success
        /// </summary>
        public Func<string, bool, WrapMode, float, bool> PlayAnim;

        protected bool mDisposed;
        protected List<AnimData> mSeqAnim = new List<AnimData>();
        protected float mDuration;

        public void AddAnim(AnimData data)
        {
            mSeqAnim.Add(data);
        }

        protected virtual void OnUpdate(float deltaTime)
        {
            mDuration -= deltaTime;

            if (PlayAnim != null && mDuration <= 0f && mSeqAnim.Count > 0)
            {
                mDuration = mSeqAnim[0].duration;
                PlayAnim(mSeqAnim[0].name, mSeqAnim[0].crossFade
                    , mSeqAnim[0].wrapMode, mSeqAnim[0].speed);

                mSeqAnim.RemoveAt(0);
            }
            else
            {
                Dispose();
            }
        }

        public void Update(float deltaTime)
        {
            if (!mDisposed)
            {
                OnUpdate(deltaTime);
            }
        }

        protected virtual void OnDispose()
        {
            mSeqAnim.Clear();
            PlayAnim = null;
        }

        public void Dispose()
        {
            if (!mDisposed)
            {
                mDisposed = true;
                OnDispose();
            }
        }
    }

    public partial class BattleObject : IDisposable
    {
        #region RefCount

        private static AtomicInteger s_alloc_count = new AtomicInteger(0);
        private static AtomicInteger s_active_count = new AtomicInteger(0);

        /// <summary>
        /// 分配实例数量
        /// </summary>
        public static int AllocCount { get { return s_alloc_count.Value; } }
        /// <summary>
        /// 未释放实例数量
        /// </summary>
        public static int ActiveCount { get { return s_active_count.Value; } }

        public  delegate void OnPlayEffect(BattleObject _object, LaunchEffect eff, Vector3 pos, Quaternion rot);
        public static OnPlayEffect OnPlayEffectHandle;
        #endregion

        public AnimPlayer animPlayer { get; private set; }
        public bool IsDisposed { get; private set; }
        public int DisposeDelayMS { get; protected set; }

        private bool mActiveSelf;
        private BattleScene mBattleScene;
        private GameObject mObjectRoot;
        private GameObject mDisplayRoot;
        private GameObject mDummyRoot;
        private GameObject mEffectRoot;
        private DisplayCell mDisplayCell;
        private HashMap<string, DummyNode> mDummys = new HashMap<string, DummyNode>();
        /// <summary>
        /// 加载任务列表
        /// </summary>

        protected BattleScene BattleScene { get { return mBattleScene; } }
        public GameObject ObjectRoot { get { return mObjectRoot; } }
        protected GameObject DisplayRoot { get { return mDisplayRoot; } }
        protected GameObject DummyRoot { get { return mDummyRoot; } }
        public GameObject EffectRoot { get { return mEffectRoot; } }
        public Vector3 Position { get { return this.mObjectRoot.Position(); } }
        public Vector3 Forward
        {
            get { return this.mObjectRoot.Forward(); }
            set { this.mObjectRoot.Forward(value); }
        }
        protected DisplayCell DisplayCell { get { return mDisplayCell; } }
        protected HashMap<string, DummyNode> Dummys { get { return mDummys; } }

        public bool ActiveSelf
        {
            get { return mActiveSelf; }
            set
            {
                this.mActiveSelf = value;
                this.ObjectRoot.SetActive(mActiveSelf);
                //this.PlayAnim(mLastAnimName, mLastCrossFade, mLastWrapMode, mLastSpeed);
            }
        }


        public BattleObject(BattleScene battleScene, string name = "")
        {
            s_alloc_count++;
            s_active_count++;

            mBattleScene = battleScene;
            mObjectRoot = new GameObject(name);

            mDisplayRoot = new GameObject("DisplayRoot");
            mDisplayRoot.ParentRoot(mObjectRoot);

            mDummyRoot = new GameObject("DummyRoot");
            mDummyRoot.ParentRoot(mObjectRoot);

            mEffectRoot = new GameObject("EffectRoot");
            mEffectRoot.ParentRoot(mObjectRoot);

            this.animPlayer = mObjectRoot.AddComponent<AnimPlayer>();

            mDisplayCell = BattleFactory.Instance.CreateDisplayCell(mDisplayRoot);
        }

        ~BattleObject()
        {
            s_alloc_count--;
        }

        /// <summary>
        /// 不要随便调用
        /// </summary>
        public virtual void OnCreate()
        {

        }

        protected virtual void CorrectDummyNode()
        {
            foreach (var elem in mDummys)
            {
                GameObject trace = mDisplayCell.GetNode(elem.Key);
                elem.Value.Init(elem.Key, trace);
            }
        }

        /// <summary>
        /// 如果名字不为空 一定会返回一个DummyNode
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual DummyNode GetDummyNode(string name)
        {
            if (this.IsDisposed || string.IsNullOrEmpty(name))
            {
                Debug.Log("string.IsNullOrEmpty(name)");
                return null;
            }

            DummyNode dummyNode = null;
            if (!mDummys.TryGetValue(name, out dummyNode))
            {
                GameObject node = mDisplayCell.GetNode(name);
                if (node == null)
                {
                    Debug.Log("node not exist " + name);
                    //return null;
                }
                GameObject tmp = new GameObject(name);
                tmp.ParentRoot(mDummyRoot);
                dummyNode = tmp.AddComponent<DummyNode>();
                dummyNode.Init(name, node);
                mDummys.Add(name, dummyNode);
            }
            return dummyNode;
        }

        /// <summary>
        /// TODO need fuck xxxxxx
        /// </summary>
        /// <param name="eff"></param>
        /// <param name="pos"> unity pos</param>
        /// <param name="rot"> unity rot</param>
        public virtual void PlayEffect(LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            if (eff != null)
            {
                if (OnPlayEffectHandle != null)
                {
                    OnPlayEffectHandle(this,eff, pos, rot);
                    return;
                }
                //声音
                if (!string.IsNullOrEmpty(eff.SoundName))
                {
                    if (eff.IsLoop)
                    {
                        BattleFactory.Instance.SoundAdapter.PlaySound(eff.SoundName, eff.EffectTimeMS, pos);
                    }
                    else
                    {
                        BattleFactory.Instance.SoundAdapter.PlaySound(eff.SoundName, pos);
                    }
                }

                //震屏.
                if (!string.IsNullOrEmpty(eff.Tag))
                {
                    bool flag = false;
                    if (eff.Tag == "all") { flag = true; }//全同步.
                    else if (eff.Tag == "actor" && IsImportant()) { flag = true; }//只有主角自己会播.
                    if (eff.EarthQuakeMS > 0 && flag)
                    {
                        if (Camera.main != null)
                        {
                            //TODO震屏.
                           //iTween.ShakePosition(Camera.main.gameObject,
                           //                             new Vector3(eff.EarthQuakeXYZ,
                           //                                         eff.EarthQuakeXYZ,
                           //                                         eff.EarthQuakeXYZ),
                           //                             (float)eff.EarthQuakeMS / 1000);
                        }
                    }

                }
                //特效
                if (!string.IsNullOrEmpty(eff.Name))
                {
                    FuckAssetLoader.GetOrLoad(eff.Name, System.IO.Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader.AssetComp)
                        {
                            if (this.IsDisposed && eff.BindBody)
                            {
                                loader.AssetComp.Unload();
                                return;
                            }

                            OnLoadEffectSuccess(loader.AssetComp, eff, pos, rot);
                        }
                    });
                }
            }
        }

        public virtual void OnLoadEffectSuccess(FuckAssetObject aoe
            , LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            aoe.gameObject.Parent(BattleScene.EffectRoot);
            aoe.gameObject.Position(pos);
            aoe.gameObject.Rotation(rot);

            var script = EffectAutoDestroy.GetOrAdd(aoe.gameObject);
            script.aoeHandler = aoe;
            script.duration = eff.IsLoop ? eff.EffectTimeMS / 1000f : 0f;

            GameObject dummy = mEffectRoot;
            if (!string.IsNullOrEmpty(eff.BindPartName))
            {
                dummy = mDisplayCell.GetNode(eff.BindPartName);
                if (dummy == null)
                {
                    dummy = mEffectRoot;
                }

                aoe.gameObject.Position(dummy.Position());
            }

            if (eff.BindBody)
            {
                aoe.gameObject.ParentRoot(dummy);
            }
        }
        public void BeginUpdate(float deltaTime)
        {
            if (!IsDisposed)
            {
                OnBeginUpdate(deltaTime);
            }
        }
        public void Update(float deltaTime)
        {
            if (!IsDisposed)
            {
                OnUpdate(deltaTime);
            }
        }
        protected virtual void OnBeginUpdate(float deltaTime)
        {

        }

        protected virtual void OnUpdate(float deltaTime)
        {
            DisposeDelayMS -= (int)(Time.deltaTime * 1000f);
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                s_active_count--;
                IsDisposed = true;
                OnDispose();
            }
        }

        protected virtual void OnDispose()
        {
            if (mDisplayCell != null)
            {
                mDisplayCell.Dispose();
            }

            FuckAssetObject[] aoes = mDummyRoot.GetComponentsInChildren<FuckAssetObject>();
            foreach (var elem in aoes)
            {
                var aoe = elem;
                EffectAutoDestroy[] scripts = aoe.gameObject.GetComponents<EffectAutoDestroy>();
                if (scripts.Length > 0)
                {
                    foreach (var script in scripts)
                    {
                        script.DoDestroy();
                    }
                }
                else
                {
                    aoe.Unload();
                }
            }

            mBattleScene = null;
            GameObject.Destroy(mObjectRoot);
        }

        public virtual bool IsImportant()
        {
            return false;
        }

    }
}
