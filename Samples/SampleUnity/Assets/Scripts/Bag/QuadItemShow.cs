
using DeepCore.GUI.Data;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D;

public abstract class QuadItemShow : UIComponent
{
    //基准尺寸
    private Vector2 mSourceSize;

    private Vector2 mIconSize;
    public const int BackgroundLockunlock = -2;
    public const int BackgroundNone = -1;

    public const int ConfigBackground = 0;
    public const int ConfigIcon = 1;

    public enum ItemStatus
    {
        None,
        Unlock,
        Lock,
    }

    public TouchClickHandle TouchClick { get; set; }
    public string AtlasPath { get; private set; }
    public string AtlasName { get; private set; }

    private bool mShowBackground;

    public bool ShowBackground
    {
        get { return mShowBackground; }
        set
        {
            mShowBackground = value;
            if (mQuality <= 0)
            {
                BackgroundConf.Val = mShowBackground ? BackgroundLockunlock : BackgroundNone;
            }
        }
    }

    #region 节点元素结构

    public class NodeConfig
    {
        public delegate void SetDelegate(NodeConfig me);

        private Stack<SetDelegate> mSetStack = new Stack<SetDelegate>(1);
        private object mVal;
        public object Val
        {
            get { return mVal; }
            set
            {
                mVal = value;
                PeekSetHandle();
            }
        }

        public bool InvalidDisplay { get; set; }

        public int QuadIndex { get; set; }

        public readonly int Name;

        public UIComponent Node { get; set; }

        public NodeConfig(QuadItemShow it, int name)
        {
            Name = name;
            QuadIndex = -1;
        }

        private void CleanNode()
        {
            if (Node != null)
            {
                Node.RemoveFromParent(true);
                Node = null;
            }
        }

        private void PeekSetHandle()
        {
            if (mSetStack.Count > 0)
            {
                mSetStack.Peek().Invoke(this);
                if (this.InvalidDisplay && Node != null)
                {
                    Node.Visible = false;
                }
            }
        }

        public void PushSetHandle(SetDelegate dg)
        {
            mSetStack.Push(dg);
            CleanNode();
        }

        public void PopSetHandle()
        {
            if (mSetStack.Count > 0)
            {
                mSetStack.Pop();
                CleanNode();
            }
        }

        public void Clear()
        {
            mSetStack.Clear();
            CleanNode();
        }

        //~NodeConfig()
        //{
        //    Debug.Log("~NodeConfig " + this.Name);
        //}
    }

    #endregion


    private ItemStatus mStatus;

    //~QuadItemShow()
    //{
    //    Debug.Log("~QuadItemShow ");
    //}

    protected override void OnDispose()
    {
        base.OnDispose();
        TouchClick = null;
        foreach (var item in mNodeConfigMap)
        {
            item.Value.Clear();
        }
    }


    protected override void OnPointerClick(PointerEventData e)
    {
        base.OnPointerClick(e);
        if (TouchClick != null)
        {
            TouchClick(this);
            SoundManager.Instance.PlaySoundByKey("button", false);
        }
    }

    private Dictionary<int, NodeConfig> mNodeConfigMap = new Dictionary<int, NodeConfig>(5);

    public ItemStatus Status
    {
        get { return mStatus; }
        set
        {
            mStatus = value;
            BackgroundConf.Val = ShowBackground ? BackgroundLockunlock : BackgroundNone;
        }
    }

    public bool EnableTouch
    {
        get { return Enable && IsInteractive; }
        set
        {
            IsInteractive = value;
            Enable = value;
            if (value && Layout == null)
            {
                Layout = new UILayout();
            }
        }
    }


    public void AddNodeConfig(int type)
    {
        var conf = new NodeConfig(this, type);
        conf.PushSetHandle(SetConfigCallBack);
        mNodeConfigMap[type] = conf;
    }

    public void RemoveNodeConfig(int name)
    {
        NodeConfig conf = GetNodeConfig(name);
        if (conf != null)
        {
            conf.Clear();
            mNodeConfigMap.Remove(name);
        }
    }

    public NodeConfig GetNodeConfig(int name)
    {
        NodeConfig conf;
        mNodeConfigMap.TryGetValue(name, out conf);
        return conf;
    }

    private void ResizeBackground()
    {
        var conf = GetNodeConfig(ConfigBackground);
        if (conf.Node != null)
        {
            conf.Node.Scale = new Vector2(Size2D.x / mSourceSize.x, Size2D.y / mSourceSize.y);
            UIUtils.AdjustAnchor(ImageAnchor.L_T, this, conf.Node, Vector2.zero);
        }
    }

    private NodeConfig BackgroundConf
    {
        get
        {
            var conf = GetNodeConfig(ConfigBackground);
            if (conf.Node == null)
            {
                conf.Node = new UIComponent("background");
                AddChild(conf.Node);
                conf.Node.Size2D = mSourceSize;
                ResizeBackground();
            }
            return conf;
        }
    }

    protected BatchQuadsSprite mQuadSprite;

    protected void CreateItemNode(NodeConfig conf)
    {
        var t = conf.Name;
        if (t == ConfigBackground)
        {
            return;
        }
        if (t == ConfigIcon)
        {
            conf.Node = new UIComponent(t.ToString());
            conf.Node.Size2D = mIconSize;
            BackgroundConf.Node.AddChildAt(conf.Node, 0);
        }
        else
        {
            OnInitNodeConfig(conf);
        }
    }

    public void VisibleConfigNode(NodeConfig conf, bool visible)
    {
        if (conf == null)
        {
            return;
        }
        if (visible && conf.Node == null && conf.QuadIndex < 0)
        {
            CreateItemNode(conf);
        }
        if (conf.Node != null)
        {
            conf.Node.Visible = visible;
        }
        else if (conf.QuadIndex >= 0)
        {
            mQuadSprite.SetQuadVisible(conf.QuadIndex, visible);
        }
        if (visible && !BackgroundConf.Node.Visible)
        {
            BackgroundConf.Node.Visible = true;
        }
    }


    private void SetConfigCallBack(NodeConfig conf)
    {
        var t = conf.Name;
        //bool resetIndex = conf.Node == null && conf.QuadIndex < 0;
        if (mQuadSprite == null)
        {
            mQuadSprite = new BatchQuadsSprite("BatchQuadsSprite");
            mQuadSprite.Size2D = mSourceSize;

            var atlas = HZUISystem.Instance.Editor.GetAtlas(AtlasPath, AtlasName);
            mQuadSprite.Atlas = atlas;
            BackgroundConf.Node.AddChild(mQuadSprite);
        }
        if (t == ConfigBackground)
        {
            int s = (int) conf.Val;
            UIComponent comp = conf.Node;
            if (s == BackgroundLockunlock)
            {
                comp.Layout = HZUISystem.CreateLayout(GetStateLayout(mStatus), UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
            }
            else if (s != BackgroundNone)
            {
                //获取品质框
                comp.Layout = HZUISystem.CreateLayout(GetQualityLayout(s), UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
            }
            else
            {
                comp.Layout = null;
            }
        }
        else if (t == ConfigIcon)
        {
            //icon
            string val = (string) conf.Val;
            if (string.IsNullOrEmpty(val))
            {
                Status = mStatus;
                VisibleConfigNode(conf, false);
            }
            else
            {
                if (!val.StartsWith("#") && !val.StartsWith("@"))
                {
                    val = IconDir + val;
                    if (val.IndexOf(".png") == -1)
                    {
                        val = val + ".png";
                    }
                }
                VisibleConfigNode(conf, true);
                (conf.Node as UIComponent).Layout = HZUISystem.CreateLayout(val, UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
                UIUtils.AdjustAnchor(ImageAnchor.C_C, conf.Node.Parent, conf.Node, Vector2.zero);
            }
        }
        else
        {
            OnConfigSetValue(conf);
        }
        //VisibleConfigNode(conf, (bool)conf.Val);
    }


    public void SetNodeConfigVal(int t, object val)
    {
        NodeConfig conf = GetNodeConfig(t);
        if (conf != null)
        {
            conf.Val = val;
        }
    }

    public TP GetNodeConfigVal<TP>(int t)
    {
        NodeConfig conf = GetNodeConfig(t);
        if (conf != null)
        {
            if (conf.Val == null)
            {
                return default(TP);
            }
            return (TP) conf.Val;
        }
        return default(TP);
    }


    private int mQuality;

    /// <summary>
    /// 小于0时显示是否解锁的背景，大于等于0为显示品质框
    /// </summary>
    public int Quality
    {
        get { return mQuality; }
        set
        {
            mQuality = value;
            if (mQuality >= 0)
            {
                BackgroundConf.Val = value;
            }
        }
    }


    public string Icon
    {
        get
        {
            var iconConf = GetNodeConfigVal<string>(ConfigIcon);
            return iconConf;
        }
        set
        {
            Name = value;
            SetNodeConfigVal(ConfigIcon, value);
        }
    }

    protected override void OnSizeChanged(Vector2 size)
    {
        base.OnSizeChanged(size);
        ResizeBackground();
    }

    /// <summary>
    /// 清除所有外部添加进的节点
    /// </summary>
    public void ClearExternChildren()
    {
        if (Transform)
        {
            ForEachChilds<DisplayNode>((node) =>
            {
                foreach (var item in mNodeConfigMap)
                {
                    if (item.Value.Node == node || node == mQuadSprite)
                    {
                        return;
                    }
                }
                RemoveChild(node);
            });
        }
    }

    public string IconDir { get; private set; }

    protected QuadItemShow(string atlasPath, string atlasName, string iconDir, Vector2 defaultSize, Vector2 iconSize)
    {
        Name = "QuadItemShow";
        AtlasPath = atlasPath;
        AtlasName = atlasName;
        IconDir = iconDir;
        mSourceSize = defaultSize;
        mIconSize = iconSize;
        AddNodeConfig(ConfigBackground);
        AddNodeConfig(ConfigIcon);
        ShowBackground = false;
        mStatus = ItemStatus.None;
    }

    protected virtual void OnInitNodeConfig(NodeConfig conf)
    {
    }

    protected virtual void OnConfigSetValue(NodeConfig conf)
    {
    }

    protected abstract string GetQualityLayout(int quality);

    protected abstract string GetStateLayout(ItemStatus state);
}