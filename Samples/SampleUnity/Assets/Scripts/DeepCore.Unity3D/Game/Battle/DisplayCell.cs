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

        private static readonly TypeAllocRecorder Alloc = new TypeAllocRecorder(typeof(DisplayCell));
        public static int AllocCount { get { return Alloc.AllocCount; } }
        public static int ActiveCount { get { return Alloc.ActiveCount; } }

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

        public struct RendererFuck
        {
            public Renderer Ren;
            public RenderDefaultData Default;
        }
        private List<RendererFuck> mmRenderers = new List<RendererFuck>();

        /// <summary>
        /// 模型内包含的渲染器 value值为初始化时的值
        /// </summary>
        private List<RendererFuck> mRenderers
        {
            get
            {
                //for (var i = mmRenderers.Count - 1; i >= 0; i--)
                //{
                //    if (!UnityHelper.IsObjectExist(mmRenderers[i].Ren) || !UnityHelper.IsObjectExist(mmRenderers[i].Ren.gameObject))
                //    {
                //        mmRenderers.RemoveAt(i);
                //    }
                //}
                return mmRenderers;
            }
        }
        private RenderSwitch mEnableRender = RenderSwitch.DEFAULT;
        private int mLayer = -1;
        private bool mNoCache;
        private int mDefaultLayer;

        protected List<RendererFuck> Renderers { get { return mRenderers; } }
        public bool IsDisposed { get { return mDisposed; } }

        private float _speed = 1f;
        public float speed
        {
            get
            {
                return _speed;
            }
            set { _speed = value; }
        }

        public void CrossFade(string stateName, float transDuration, int layer = 0, float normalizedTime = 0)
        {
            if (mDisposed) return;
            if (!string.IsNullOrEmpty(stateName))
            {
                if (!DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator) ||
                    !DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator.gameObject))
                {
                    //Debug.LogError("[Object not exists]");
                    return;
                }
                if (Animator.gameObject.activeInHierarchy)
                {
                    //Debug.Log("crossfade_lastStateName"+ _lastStateName);
                    if (Animator.StateExists(stateName))
                    {
                    //Debug.Log("CrossFade: " + _lastStateName + "\t" + elem.gameObject.name);
                        Animator.CrossFade(stateName, transDuration, layer, normalizedTime);
                    }
                    else
                    {
//                        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
//                        {
//                            Debug.LogWarning("crossfade StateName not exists: " + stateName + "\t" + Animator.gameObject.name);
//                        }
                    }
                }
                else
                {
                    if (Animator.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        //Debug.Log("animplay is activeInHierarchy ");
                    }
                }
            }
        }

        public float GetAnimTime(string stateName)
        {
            if (mDisposed) return 0;
            if (!string.IsNullOrEmpty(stateName))
            {
                if (Animator != null && Animator.runtimeAnimatorController != null)
                {
                    var listanimatorclip = Animator.runtimeAnimatorController.animationClips;
                    if (listanimatorclip != null)
                    {
                        foreach (var clip in listanimatorclip)
                        {
                            if (clip.name.Equals(stateName))
                            {
                                return clip.length;
                            }
                        }
                    }

                }
            }
            return 0;

        }

        public bool IsCurrentStatePlayOver(string name)
        {
            if (mDisposed) return true;
            if (Animator != null)
            {
                AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(0);
                if (info.IsName(name) && info.normalizedTime >= 1)
                {
                    return true;
                }

            }
            return false;
        }

        public void Play(string stateName, int layer = 0, float normalizedTime = 0)
        {
            if (mDisposed) return;
            if (!string.IsNullOrEmpty(stateName))
            {
                if (!DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator) ||
                    !DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator.gameObject))
                {
                    //Debug.LogError("[Object not exists]");
                    return;
                }
                if (Animator.gameObject.activeInHierarchy)
                {
                    if (Animator.StateExists(stateName))
                    {
                        //Debug.Log("Play2 : " + _lastStateName + "\t" + elem.gameObject.name);
                        Animator.Play(stateName, layer, normalizedTime);
                    }
                    else
                    {
                        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                        {
                            Debug.Log("Play StateName not exists: " + stateName + "\t" + Animator.gameObject.name);
                        }
                    }
                }
                else
                {
                    if (Animator.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        //Debug.Log("animplay is activeInHierarchy ");
                    }
                }
            }
        }

        public void SetFloat(string name, float value, float dampTime, float deltaTime)
        {
            if (mDisposed) return;
            if (!string.IsNullOrEmpty(name))
            {
                if (!DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator) ||
                    !DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator.gameObject))
                {
                    //Debug.LogError("[Object not exists]");
                    return;
                }
                if (Animator.gameObject.activeInHierarchy)
                {
                    if (name != "Blend")
                    {
                        Animator.SetFloat(name, value, dampTime, deltaTime);
                    }

                }
                else
                {
                    if (Animator.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        //Debug.Log("animplay is activeInHierarchy ");
                    }

                }
            }
        }

        public void SetFloat(string name, float value)
        {
            if (mDisposed) return;
            if (!string.IsNullOrEmpty(name))
            {
                if (!DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator) ||
                    !DeepCore.Unity3D.UnityHelper.IsObjectExist(Animator.gameObject))
                {
                    //Debug.LogError("[Object not exists]");
                    return;
                }
                if (Animator.gameObject.activeInHierarchy)
                {
                    if (name != "Blend")
                    {
                        Animator.SetFloat(name, value);
                    }

                }
                else
                {
                    if (Animator.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        //Debug.Log("animplay is activeInHierarchy ");
                    }

                }
            }
        }

        /// <summary>
        /// 慎用
        /// </summary>
        public GameObject ObjectRoot { get { return mObjectRoot; } }
        private Animator Animator;
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

        //protected AnimPlayer animPlayer = null;
        //public AnimPlayer GetAnimPlayer()
        //{
        //    if (animPlayer == null)
        //    {
        //        animPlayer = ObjectRoot.AddComponent<AnimPlayer>();
        //    }

        //    return animPlayer;
        //}

        public DisplayCell(GameObject root, string name = "DisplayCell")
        {
            Alloc.RecordConstructor(GetType());
            mObjectRoot = new GameObject(name);
            if (root != null)
            {
                mObjectRoot.ParentRoot(root);
            }
        }

        ~DisplayCell()
        {
            Alloc.RecordDestructor(GetType());
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
                    elem.Ren.enabled = (mEnableRender == RenderSwitch.ENABLE) ? true : false;
                }
            }
        }

        public bool DontUseCache { get; set; }
        
        public int LoadModel(string assetBundleName, string asset, Action<FuckAssetObject> callback = null,Predicate<FuckAssetObject> cond = null)
        {
            if (DontUseCache)
            {
                return FuckAssetObject.Load(assetBundleName, asset, (loader) =>
                {
                    if (loader == null)
                    {
                        if (callback != null)
                        {
                            callback(null);
                        }

                        return;
                    }

                    if (IsDisposed)
                    {
                        if (loader)
                        {
                            loader.Unload();
                        }

                        if (callback != null)
                        {
                            callback(null);
                        }

                        return;
                    }

                    //if(loader)
                    {
                        if (cond == null || cond.Invoke(loader))
                        {
                            _SetModel(loader);

                            if (callback != null)
                            {
                                callback(loader);
                            }
                        }
                    }
                });
            }
            else
            {
                return FuckAssetObject.GetOrLoad(assetBundleName, asset, (loader) =>
                {
                    if (loader == null)
                    {
                        if (callback != null)
                        {
                            callback(null);
                        }

                        return;
                    }

                    if (IsDisposed)
                    {
                        if (loader)
                        {
                            loader.Unload();
                        }

                        if (callback != null)
                        {
                            callback(null);
                        }

                        return;
                    }

                    //if(loader)
                    {
                        if (cond == null || cond.Invoke(loader))
                        {
                            _SetModel(loader);

                            if (callback != null)
                            {
                                callback(loader);
                            }
                        }
                    }
                });
            }
        }

        protected virtual void _SetModel(FuckAssetObject model)
        {
            Unload();
            if (model != mModel)
            {
                mDefaultLayer = model.gameObject.layer;

                Renderer[] renderers = model.GetComponentsInChildren<Renderer>(true);
                foreach (var elem in renderers)
                {
                    var data = new RenderDefaultData();
                    data.enable = elem.enabled;
                    data.layer = elem.gameObject.layer;
                    mRenderers.Add(new RendererFuck{Ren  = elem,Default = data});
                    if (mEnableRender != RenderSwitch.DEFAULT)
                    {
                        elem.enabled = (mEnableRender == RenderSwitch.ENABLE) ? true : false;
                    }
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
                        Debug.LogWarning("_SetModel GameObject GetNode dummyNode is null  " + elem.Key + "mModel:" + mModel);
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
                if(root != null)
                    mObjectRoot.ParentRoot(root);
                else
                {
                    mObjectRoot.transform.parent = null;
                }
            }
        }

        public DisplayCell AttachPart(string name, string dummy, AnimPlayer animPlayer)
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
                Debug.LogWarning("AttachPart GetNode is null 11 : " + dummy);
            }
            DisplayCell displayCell = BattleFactory.Instance.CreateDisplayCell(dummyNode, name);

            animPlayer.AddAnimator(displayCell);
            displayCell.SetLayer(mLayer);
            mAttachParts.Add(name, displayCell);
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
                Debug.LogWarning("AttachPart GetNode is null 22 : " + dummy);
            }

            displayCell.ParentRoot(dummyNode);
            displayCell.SetLayer(mLayer);

            mAttachParts.Put(name, displayCell);

            //this.PlayAnim(LastAnimName, false, LastWrapMode
            //    , LastSpeed, LastTime);
            return displayCell;
        }
 

        public void DetachPart(string name)
        {
            DisplayCell displayCell = mAttachParts.RemoveByKey(name);
            if (displayCell != null)
            {
                displayCell.Dispose();
            }
        }

        public DisplayCell TryDetachPart(string name)
        {
            return mAttachParts.RemoveByKey(name);
        }

        public DisplayCell GetPart(string name)
        {
            DisplayCell tmp;
            mAttachParts.TryGetValue(name, out tmp);
            return tmp;
        }

        public void DetachAllPart()
        {
            foreach (var elem in mAttachParts)
            {
                elem.Value.ParentRoot(null);
                elem.Value.Dispose();
            }
            mAttachParts.Clear();
        }

        public virtual GameObject GetNode(string name)
        {
            if (mModel == null || string.IsNullOrEmpty(name))
            {
                Debug.LogWarning("GameObject GetNode name  " + name + "mModel:" + mModel);
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

        public virtual void Unload()
        {
            this.Animator = null;
            foreach (var elem in mRenderers)
            {
                elem.Ren.enabled = elem.Default.enable;
                elem.Ren.gameObject.layer = elem.Default.layer;
            }
            mRenderers.Clear();

            if (mModel)
            {
                mModel.Unload();
                mModel = null;
            }
        }


        protected virtual void OnDispose()
        {
            mObjectRoot.transform.SetParent(null);
            this.Animator = null;
            DetachAllPart();

            foreach (var elem in mRenderers)
            {
                elem.Ren.enabled = elem.Default.enable;
                elem.Ren.gameObject.layer = elem.Default.layer;
            }
            mRenderers.Clear();

            if (mObjectRoot.transform.childCount > 1)
            {
                Debug.LogError("[what the fuck]" + this + " [child count]" + mObjectRoot.transform.childCount);
            }

            if (mModel)
            {
                mModel.Unload();
                mModel = null;
            }

            DeepCore.Unity3D.UnityHelper.Destroy(mObjectRoot);
            //for (int i = 0; i < mObjectRoot.transform.childCount; i++)
            //{
            //    mObjectRoot.transform.GetChild(i).SetParent(UnityHelper.DisableParent);
            //}
            //mObjectRoot.transform.SetParent(UnityHelper.DisableParent);
        }
    }
}