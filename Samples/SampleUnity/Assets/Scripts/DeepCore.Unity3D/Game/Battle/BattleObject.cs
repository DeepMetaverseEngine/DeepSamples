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

        public static readonly TypeAllocRecorder Alloc  = new TypeAllocRecorder(typeof(BattleObject));
        public static int AllocCount { get { return Alloc.AllocCount; } }
        public static int ActiveCount { get { return Alloc.ActiveCount; } }

        public  delegate int OnPlayEffect(BattleObject _object, LaunchEffect eff, Vector3 pos, Quaternion rot);
        public static OnPlayEffect OnPlayEffectHandle;
        #endregion

        public AnimPlayer animPlayer { get; private set; }
        public virtual bool IsDisposed { get
            {
                return mDisposed;
            }
        }
        protected bool mDisposed = false;
        public int DisposeDelayMS { get; protected set; }

        private bool mActiveSelf;
        private BattleScene mBattleScene;
        private GameObject mObjectRoot;
        private GameObject mDisplayRoot;
        private GameObject mDummyRoot;
        private GameObject mEffectRoot;
        private GameObject mBuffRoot;
        private DisplayCell mDisplayCell;
        private HashMap<string, DummyNode> mDummys = new HashMap<string, DummyNode>();
        protected BattleScene BattleScene { get { return mBattleScene; } }
        public GameObject ObjectRoot { get { return mObjectRoot; } }
        protected GameObject DisplayRoot { get { return mDisplayRoot; } }
        protected GameObject DummyRoot { get { return mDummyRoot; } }
        public GameObject EffectRoot { get { return mEffectRoot; } }
        public GameObject BuffRoot { get { return mBuffRoot; } }
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
            Alloc.RecordConstructor(GetType());

               mBattleScene = battleScene;
            mObjectRoot = new GameObject(name);

            mDisplayRoot = new GameObject("DisplayRoot");
            mDisplayRoot.ParentRoot(mObjectRoot);

            mDummyRoot = new GameObject("DummyRoot");
            mDummyRoot.ParentRoot(mObjectRoot);

            mEffectRoot = new GameObject("EffectRoot");
            mEffectRoot.ParentRoot(mObjectRoot);

            mBuffRoot = new GameObject("BuffRoot");
            mBuffRoot.ParentRoot(mObjectRoot);

            this.animPlayer = new AnimPlayer();//mObjectRoot.AddComponent<AnimPlayer>();

            mDisplayCell = BattleFactory.Instance.CreateDisplayCell(mDisplayRoot);
            animPlayer.AddAnimator(mDisplayCell);
        }

        ~BattleObject()
        {
            Alloc.RecordDestructor(GetType());
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
            else
            {
                //TODO 就算有，也不确定追踪的目标一定是存在的
                if (!dummyNode.IsTrace)
                {
                    GameObject node = mDisplayCell.GetNode(name);
                    if (node == null)
                    {
                        Debug.Log("node not exist " + name);
                        //return null;
                    }
                    dummyNode.Init(name, node);
                }
            }
            return dummyNode;
        }

        /// <summary>
        /// TODO need fuck xxxxxx
        /// </summary>
        /// <param name="eff"></param>
        /// <param name="pos"> unity pos</param>
        /// <param name="rot"> unity rot</param>
        public virtual int PlayEffect(LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            var effID = 0;

            if (eff != null)
            {
                return OnPlayEffectHandle(this.IsDisposed ? null : this, eff, pos, rot);
            }

            return effID;
        }

        public virtual void OnLoadWarningEffect(FuckAssetObject aoe
            , LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            aoe.gameObject.Parent(EffectRoot);
            pos.y = mObjectRoot.transform.position.y;
            aoe.gameObject.Position(pos);
            var script = EffectAutoDestroy.GetOrAdd(aoe.gameObject);
            script.aoeHandler = aoe;
            script.duration = 1000;
        }

        public virtual void OnLoadEffectSuccess(FuckAssetObject aoe
            , LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            aoe.gameObject.Parent(BattleScene.EffectRoot);
            aoe.gameObject.Position(pos);
            aoe.gameObject.Rotation(rot);
            var script = EffectAutoDestroy.GetOrAdd(aoe.gameObject);
            script.aoeHandler = aoe;
            script.duration = eff.EffectTimeMS / 1000f;
            script.IsLoop = eff.IsLoop;
            Transform dummy = mEffectRoot.transform;
            if (!string.IsNullOrEmpty(eff.BindPartName))
            {
                dummy = GetDummyNode(eff.BindPartName).transform;
                if (dummy == null)
                {
                    dummy = mEffectRoot.transform;
                }

                aoe.gameObject.Position(dummy.position);
            }

            if (eff.BindBody)
            {
                aoe.gameObject.ParentRoot(dummy.gameObject);
            }
            if (Math.Abs(eff.ScaleToBodySize) > 0.01)
            {
                aoe.transform.localScale = new Vector3(eff.ScaleToBodySize, eff.ScaleToBodySize, eff.ScaleToBodySize);
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
            if (DisposeDelayMS > 0)
            {
                DisposeDelayMS -= (int)(Time.deltaTime * 1000f);
                DisposeDelayMS = Math.Max(DisposeDelayMS, 0);
            }
            
        }

        public void Dispose()
        {
            if (!mDisposed)
            {
                Alloc.RecordDispose(GetType());
                mDisposed = true;
                OnDispose();
            }
        }

        protected virtual void OnDispose()
        {
            //清理Dummy下面挂载的例子
            foreach (var dummy in mDummys)
            {
                dummy.Value.Unload();
            }
            mDummys.Clear();

            if (mDisplayCell != null)
            {
                mDisplayCell.Dispose();
                animPlayer.RemoveAnimator(mDisplayCell);
                mDisplayCell = null;
            }
            
            FuckAssetObject[] aoes = mEffectRoot.GetComponentsInChildren<FuckAssetObject>(true);
            foreach (var elem in aoes)
            {
                var aoe = elem;
                aoe.transform.SetParent(null);
                var script = aoe.gameObject.GetComponent<EffectAutoDestroy>();
                if (script)
                {
                    script.DoDestroy();
                }
                else
                {
                    aoe.Unload();
                }
            }

            mBattleScene = null;
            DeepCore.Unity3D.UnityHelper.Destroy(mObjectRoot);
            //for (int i = 0; i < mObjectRoot.transform.childCount; i++)
            //{
            //    mObjectRoot.transform.GetChild(i).SetParent(UnityHelper.DisableParent);
            //}
            
            //mObjectRoot.transform.SetParent(UnityHelper.DisableParent);
        }

        public virtual bool IsImportant()
        {
            return false;
        }

        public virtual bool SoundImportant()
        {
            return true;
        }
        //设置不显示的调用
        public virtual void OnLoadNotShow(LaunchEffect eff)
        {
           
        }
    }
}
