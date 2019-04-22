using DeepCore;
using System.Collections.Generic;
using UnityEngine;
using DeepCore.Unity3D.Utils;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAIUnit
    {
        public class Avatar
        {
            private DisplayCell mDisplayCell;
            private object mTag;


            protected internal DisplayCell DisplayCell
            {
                get { return mDisplayCell; }
                internal set { mDisplayCell = value; }
            }
            public object Tag
            {
                get { return mTag; }
                set { mTag = value; }
            }
        }

        private List<Avatar> mAvatarStack = new List<Avatar>();
        private IComparer<Avatar> mAvatarComparer;


        public IComparer<Avatar> AvatarComparer { set { mAvatarComparer = value; } }
        private System.Action mAddAvatarFinish;
        private event System.Action OnAddAvatarFinish { add { mAddAvatarFinish += value; } remove { mAddAvatarFinish -= value; } }

        protected override void CorrectDummyNode()
        {
            if (mAvatarStack.Count == 0)
            {
                base.CorrectDummyNode();
                return;
            }

            if (mAvatarStack[0].DisplayCell != null)
            {
                DisplayCell display = mAvatarStack[0].DisplayCell;

                foreach (var elem in this.Dummys)
                {
                    GameObject trace = display.GetNode(elem.Key);
                    elem.Value.Init(elem.Key, trace);
                }
            }
        }

        /// <summary>
        /// 如果name不为空 绝对返回一个DummyNode
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override DummyNode GetDummyNode(string name)
        {
            if (mAvatarStack.Count == 0 || mAvatarStack[0].DisplayCell == null)
            {
                return base.GetDummyNode(name);
            }

            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("string.IsNullOrEmpty(name)");
                return null;
            }

            DisplayCell display = mAvatarStack[0].DisplayCell;

            DummyNode dummyNode = null;
            if (!this.Dummys.TryGetValue(name, out dummyNode))
            {
                GameObject node = display.GetNode(name);
                if (node == null)
                {
                    Debug.LogWarning("node not exist " + name);
                }
                GameObject tmp = new GameObject(name);
                tmp.ParentRoot(this.DummyRoot);
                dummyNode = tmp.AddComponent<DummyNode>();
                dummyNode.Init(name, node);
                this.Dummys.Add(name, dummyNode);
            }
            return dummyNode;
        }

        /// <summary>
        /// 增加一个avatar 返回在avatar列表中的顺序
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public virtual Avatar AddAvatar(string assetBundleName, string assetName)
        {
            if (!string.IsNullOrEmpty(assetBundleName)
                && !string.IsNullOrEmpty(assetName))
            {
                Avatar info = new Avatar();
                mAvatarStack.Add(info);
                if (mAvatarComparer != null)
                {
                    mAvatarStack.Sort(mAvatarComparer);
                }

                info.DisplayCell = BattleFactory.Instance.CreateDisplayCell(mAvatarRoot);
                this.animPlayer.AddAnimator(info.DisplayCell);
                info.DisplayCell.LoadModel(assetBundleName, assetName, (loader) =>
                {
                    if (IsDisposed || mAvatarStack.IndexOf(info) == -1)
                    {
                        if (loader)
                        {
                            info.DisplayCell.Unload();
                        }
                        return;
                    }
                    if (loader)
                    {
                        if (mAvatarStack[0] == info)
                        {
                            DisplayCell.activeSelf = false;
                            //映射节点
                            CorrectDummyNode();

                            //info.DisplayCell.PlayAnim(LastAnimName, false, LastWrapMode, LastSpeed);
                        }
                        else
                        {
                            mAvatarStack[0].DisplayCell.activeSelf = false;
                        }
                        if (mAddAvatarFinish != null)
                        {
                            mAddAvatarFinish();
                        }
                    }
                });

                return info;
            }

            return null;
        }

        public void RemoveAvatar(Avatar info)
        {
            int index = mAvatarStack.IndexOf(info);
            if (index == -1)
                return;

            if (info.DisplayCell != null)
            {
                this.animPlayer.RemoveAnimator(info.DisplayCell);
                info.DisplayCell.Dispose();
            }
            mAvatarStack.Remove(info);

            if (index == 0)
            {
                if (mAvatarStack.Count > 0)
                {
                    CorrectDummyNode();
                    mAvatarStack[0].DisplayCell.activeSelf = true;
                    //PlayAnim(LastAnimName, false, LastWrapMode, LastSpeed);
                }
                else
                {

                    this.DisplayCell.activeSelf = true;
                    CorrectDummyNode();
                }
                //PlayAnim(LastAnimName, false, LastWrapMode, LastSpeed);
                ChangeAction(this.ZUnit.CurrentState, true);
            }
        }
    }
}