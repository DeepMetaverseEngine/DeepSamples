using DeepCore.Unity3D.Utils;
using DeepCore;
using DeepCore.Concurrent;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public partial class DisplayCell : IDisposable
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

        #endregion

        public enum RenderSwitch
        {
            DEFAULT,
            ENABLE,
            UNENABLE,
        }

        public class RenderDefaultData
        {
            public bool enable;
            public int layer;
        }
        
        /// <summary>
        /// 主体上的挂载列表
        /// </summary>
        protected HashMap<string, DisplayCell> mAttachParts = new HashMap<string, DisplayCell>();

        private bool mDisposed = false;
        private bool mActiveSelf;
        private GameObject mObjectRoot;
        protected FuckAssetObject mModel;

        /// <summary>
        /// 模型内包含的渲染器 value值为初始化时的值
        /// </summary>
        private HashMap<Renderer, RenderDefaultData> mRenderers = new HashMap<Renderer, RenderDefaultData>();
        private RenderSwitch mEnableRender = RenderSwitch.DEFAULT;
        private int mLayer = -1;
        private bool mNoCache;
        private int mDefaultLayer;

        protected HashMap<Renderer, RenderDefaultData> Renderers { get { return mRenderers; } }
        public bool IsDisposed { get { return mDisposed; } }

        /// <summary>
        /// 慎用
        /// </summary>
        public GameObject ObjectRoot { get { return mObjectRoot; } }
        public Animator Animator { get; private set; }
        public Vector3 Position
        {
            get { return mObjectRoot.Position(); }
            set { mObjectRoot.Position(value); }
        }
        public Quaternion Rotation
        {
            get { return mObjectRoot.Rotation(); }
            set { mObjectRoot.Rotation(value); }
        }
        public Vector3 localPosition
        {
            get { return mObjectRoot.transform.localPosition; }
            set { mObjectRoot.transform.localPosition = value; }
        }
        public Vector3 localScale
        {
            get { return mObjectRoot.transform.localScale; }
            set { mObjectRoot.transform.localScale = value; }
        }
        public Quaternion localRotation
        {
            get { return mObjectRoot.transform.localRotation; }
            set { mObjectRoot.transform.localRotation = value; }
        }
        public Vector3 localEulerAngles
        {
            get { return mObjectRoot.transform.localEulerAngles; }
            set { mObjectRoot.transform.localEulerAngles = value; }
        }
        public bool activeSelf
        {
            get { return mActiveSelf; }
            set
            {
                this.mActiveSelf = value;
                this.mObjectRoot.SetActive(mActiveSelf);
            }
        }

        public DisplayCell(GameObject root, string name = "DisplayCell")
        {
            mObjectRoot = new GameObject(name);
            if (root != null)
            {
                mObjectRoot.ParentRoot(root);
            }
            s_alloc_count++;
            s_active_count++;
        }

        ~DisplayCell()
        {
            s_alloc_count--;
        }

        public virtual void SetLayer(int layer)
        {
            if (layer >= 0 && mLayer != layer)
            {
                mLayer = layer;

                InnerSetLayer();

                foreach (var elem in mAttachParts)
                {
                    elem.Value.SetLayer(layer);
                }
            }
        }

        private void InnerSetLayer()
        {
            if (mLayer >= 0 && mModel != null)
            {
                var allTrans = mModel.GetComponentInChildren<Transform>();

                foreach (Transform elem in allTrans)
                {
                    elem.gameObject.layer = mLayer;
                }
            }
        }

        public void EnableRender(bool enable)
        {
            RenderSwitch mode = enable ? RenderSwitch.ENABLE : RenderSwitch.UNENABLE;
            if (mEnableRender != mode)
            {
                mEnableRender = mode;
                InnerEnableRender();

                foreach (var elem in mAttachParts)
                {
                    elem.Value.EnableRender(enable);
                }
            }
        }

        private void InnerEnableRender()
        {
            if (mEnableRender != RenderSwitch.DEFAULT)
            {
                foreach (var elem in mRenderers)
                {
                    elem.Key.enabled = (mEnableRender == RenderSwitch.ENABLE) ? true : false;
                }
            }
        }

        public virtual void SetModel(FuckAssetObject model)
        {
            if (this.IsDisposed)
            {
                Debug.LogError("this.Disposed!");
                model.Unload();
                return;
            }

            if (model != mModel)
            {
                mDefaultLayer = model.gameObject.layer;
                foreach (var elem in mRenderers)
                {
                    elem.Key.gameObject.layer = elem.Value.layer;
                    elem.Key.enabled = elem.Value.enable;
                }
                mRenderers.Clear();

                Renderer[] renderers = model.GetComponentsInChildren<Renderer>(true);
                foreach (var elem in renderers)
                {
                    var data = new RenderDefaultData();
                    data.enable = elem.enabled;
                    data.layer = elem.gameObject.layer;
                    mRenderers.Add(elem, data);
                    if (mEnableRender != RenderSwitch.DEFAULT)
                    {
                        elem.enabled = (mEnableRender == RenderSwitch.ENABLE) ? true : false;
                    }
                }

                if (mModel)
                {
                    mModel.Unload();
                }
                mModel = model;

                InnerSetLayer();

                mModel.gameObject.ParentRoot(mObjectRoot);
                this.Animator = mModel.gameObject.GetComponent<Animator>();

                foreach (var elem in mAttachParts)
                {
                    GameObject dummyNode = GetNode(elem.Key);
                    if (dummyNode == null)
                    {
                        dummyNode = mObjectRoot;
                    }
                    elem.Value.ParentRoot(dummyNode);
                }
               
            }
        }

        public void Parent(GameObject root)
        {
            if (this.IsDisposed)
            {
                Debug.LogError("this.Disposed!");                
            }
            else
            {
                mObjectRoot.Parent(root);
            }
        }

        public void ParentRoot(GameObject root)
        {
            if (this.IsDisposed)
            {
                Debug.LogError("this.Disposed!");
            }
            else
            {
                mObjectRoot.ParentRoot(root);
            }
        }

        public DisplayCell AttachPart(string name, string dummy)
        {
            if (this.IsDisposed)
            {
                Debug.LogError("this.Disposed!");
                return null;
            }

            GameObject dummyNode = GetNode(dummy);
            if (dummyNode == null)
            {
                dummyNode = mObjectRoot;
            }
            DisplayCell displayCell = BattleFactory.Instance.CreateDisplayCell(dummyNode, name);
            displayCell.SetLayer(mLayer);
            mAttachParts.Add(name.ToLower(), displayCell);
            return displayCell;
        }

        public DisplayCell AttachPart(string name , string dummy, DisplayCell displayCell,bool IsAttach = false)
        {
            if (this.IsDisposed)
            {
                Debug.LogError("this.Disposed!");
                displayCell.Dispose();
                return null;
            }

            GameObject dummyNode = GetNode(dummy);
            if (dummyNode == null)
            {
                dummyNode = mObjectRoot;
            }

            displayCell.ParentRoot(dummyNode);
            displayCell.SetLayer(mLayer);

            //重新Attach其实是重设下父节点，不需要往map里放
            if(IsAttach == false)
            {
                mAttachParts.Add(name.ToLower(), displayCell);
            }
            
            //this.PlayAnim(LastAnimName, false, LastWrapMode
            //    , LastSpeed, LastTime);
            return displayCell;
        }
 

        public void DetachPart(string name)
        {
            DisplayCell displayCell = mAttachParts.RemoveByKey(name.ToLower());
            if (displayCell != null)
            {
                displayCell.Dispose();
            }
        }

        public DisplayCell GetPart(string name)
        {
            DisplayCell tmp;
            mAttachParts.TryGetValue(name.ToLower(), out tmp);
            return tmp;
        }

        public void DetachAllPart()
        {
            foreach (var elem in mAttachParts)
            {
                elem.Value.Dispose();
            }
            mAttachParts.Clear();
        }

        public virtual GameObject GetNode(string name)
        {
            if (mModel == null || string.IsNullOrEmpty(name))
            {
                return null;
            }

            GameObject dummyNode = mModel.FindNode(name);
            if (dummyNode == null)
            {
                foreach (var elem in mAttachParts)
                {
                    dummyNode = elem.Value.GetNode(name);
                    if (dummyNode != null)
                        break;
                }
            }
            return dummyNode;
        }
        
        public bool HasAnim(string name)
        {
            if (!string.IsNullOrEmpty(name) && this.Animator != null)
            {
                return this.Animator.HasState(0, Animator.StringToHash(name));
            }
            return false;
        }

        public void Update(float deltaTime)
        {
            if (!mDisposed)
            {
                OnUpdate(deltaTime);
            }
            else
            {
                Debug.LogError("this.Disposed");
            }
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
            if (!mDisposed)
            {
                s_active_count--;
                mDisposed = true;
                OnDispose();
            }
        }

        protected virtual void OnDispose()
        {
            this.Animator = null;
            DetachAllPart();

            foreach (var elem in mRenderers)
            {
                elem.Key.enabled = elem.Value.enable;
                elem.Key.gameObject.layer = elem.Value.layer;
            }
            mRenderers.Clear();
            if (mModel)
            {
                mModel.Unload();
            }
            GameObject.DestroyObject(mObjectRoot);
        }
    }
}