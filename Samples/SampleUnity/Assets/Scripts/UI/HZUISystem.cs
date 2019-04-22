using UnityEngine;
using System.Collections;
 

using DeepCore.GUI.Data;
using UnityEngine.UI;
 
using System.Collections.Generic;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.Impl;
using DeepCore.Unity3D.UGUI;
using DeepCore.GUI.Display.Text;
using DeepCore.Unity3D.UGUIEditor.UI;
using Cache;

public class HZUISystem : MonoBehaviour
{
    #region 初始化字体.

    public Font mDefaultFont;
    public TextAsset mInitText;

    #endregion

    #region 开发基准分辨率.

    public const int SCREEN_WIDTH = 1136;
    public const int SCREEN_HEIGHT = 640;

    #endregion

    #region 缩放偏移.

    private float mScale = 1.0f;
    private float mMaxScale = 1.0f;
    private float mStageOffsetX = 0.0f;
    private float mStageOffsetY = 0.0f;
    public float NotchOffX { get; private set; }
    public float IPhoneXOffY { get; private set; }

    //UGUI根节点大小.
    private Rect mRootRect;

    #endregion

    #region 基础分层.

    private Canvas mUGUIRoot = null;
    private HZUIEditor mEditor = null;
    public HZUIEditor Editor {
        private set
        {
            mEditor = value;
        }
        get
        {
            if(mEditor == null)
            {
                mEditor = new HZUIEditor(Assets.Scripts.Setting.ProjectSetting.UIEditorPath);
                mEditor.SetDefaultFont(DefaultFont);
            }
            return mEditor;
        }
    }

    private static HZUISystem mInstance = null;
    private DisplayRoot mRoot = null;
    private DisplayNode mHUDLayer = null;
    private DisplayNode mUILayer = null;
    private DisplayNode mAlertLayer = null;
    private DisplayNode mBubbleLayer = null;
    private DisplayNode mPickLayer = null;
    private DisplayNode mCGLayer = null;
    #endregion

    #region 响应过滤.

    /// <summary>
    /// 点击过滤.
    /// </summary>
    //private ITouchInterface mTouchIml = null;

    HZUITouchHandler mTouchHandler;
    private Dictionary<int, HZUITouchHandler.OnPinchMoveHandler> mPinchQueue = new Dictionary<int, HZUITouchHandler.OnPinchMoveHandler>();

    #endregion

    #region 主逻辑功能.

    public float StageScale { get { return mScale; } }
    public float StageOffsetX { get { return mStageOffsetX - NotchOffX; } }
    public float StageOffsetY { get { return mStageOffsetY; } }
    public float MaxStageScale { get { return mMaxScale; } }
    public Rect RootRect { get { return mRootRect; } }
    public Font DefaultFont { get { return mDefaultFont; } }

    public static HZUISystem Instance
    { 
        get{
            return mInstance;
        }
    }

    public DisplayRoot UERoot { get { return mRoot; } }

    public bool Visible
    {
        get
        {
            return mAlertLayer.Visible && mHUDLayer.Visible && mUILayer.Visible && mBubbleLayer.Visible; //&& mPickLayer.Visible; 拾取层不是很重要，要不要算进去呢
        }
        set
        {
            mAlertLayer.Visible = value;
            mHUDLayer.Visible = value;
            mUILayer.Visible = value;
            mBubbleLayer.Visible = value;
            mPickLayer.Visible = value;
            mCGLayer.Visible = value;
        }
    }

    void Awake()
    {
        mInstance = this;
        Init();
    }

    void Init()
    {
        Texture texture = DefaultFont.material.mainTexture;
        if(texture.width < 1024 && texture.height < 1024)
        {
            DefaultFont.characterInfo = null;
            DefaultFont.RequestCharactersInTexture(mInitText.text, 16);
        }
        Debugger.Log(string.Format("texture:{0}   {1}", texture.width, texture.height));   // 纹理大小  

        InitScreenResolution();
        LayerInit();
        //触摸控制初始化
        
        mTouchHandler = new HZUITouchHandler(UnityDriver.UnityInstance);
    }

    private void InitScreenResolution()
    {
        CanvasScaler cs = GetComponent<CanvasScaler>();
        float baseRatio = (float)SCREEN_WIDTH / (float)SCREEN_HEIGHT;
        float targetRatio = (float)Screen.width / (float)Screen.height;
        if (targetRatio < baseRatio)   //宽高比小于基准分辨率，缩放模式以宽度为基准.
        {
            cs.matchWidthOrHeight = 0;
            mScale = (float)Screen.width / (float)SCREEN_WIDTH;
            mMaxScale = (float)Screen.height / (float)SCREEN_HEIGHT;
            mStageOffsetX = 0;
            mStageOffsetY = ((float)Screen.height / mScale - (float)SCREEN_HEIGHT) * 0.5f;
            mRootRect = new Rect(0, 0, SCREEN_WIDTH, (float)Screen.height / mScale);
        }
        else   //宽高比大于基准分辨率，缩放模式以高度为基准.
        {
            cs.matchWidthOrHeight = 1;
            mScale = (float)Screen.height / (float)SCREEN_HEIGHT;
            mMaxScale = (float)Screen.width / (float)SCREEN_WIDTH;
            mStageOffsetX = ((float)Screen.width / mScale - (float)SCREEN_WIDTH) * 0.5f;
            mStageOffsetY = 0;
            mRootRect = new Rect(0, 0, (float)Screen.width / mScale, SCREEN_HEIGHT);
        }
        //ResetScreenOffset();
    }

    public void ResetScreenOffset()
    {
        NotchOffX = GameUtil.GetNotchX() / mScale;
        IPhoneXOffY = GameUtil.IOSScnMode() * 21.0f / mScale;
    }

    /*
                      - HUDLayer.
       stage - root - - UILayer.
                      - UIAlertLayer.
      */
    private void LayerInit()
    {
        mUGUIRoot = GetComponent<UnityEngine.Canvas>();

        mRoot = new DisplayRoot(mUGUIRoot, "UE_ROOT");

        //----- init ui layer -----
        mBubbleLayer = new DisplayNode("BubbleChatLayer");
        mBubbleLayer.Enable = false;
        mBubbleLayer.EnableChildren = true;
        SetNodeFullScreenSize(mBubbleLayer);

        mHUDLayer = new DisplayNode("HUDLayer");
        mHUDLayer.Enable = false;
        mHUDLayer.EnableChildren = true;
        SetNodeFullScreenSize(mHUDLayer);

        mPickLayer = new DisplayNode("PickLayer");
        mPickLayer.Enable = false;
        mPickLayer.EnableChildren = true;
        SetNodeFullScreenSize(mPickLayer);

        mUILayer = new DisplayNode("UILayer");
        mUILayer.Enable = false;
        mUILayer.EnableChildren = true;
        SetNodeFullScreenSize(mUILayer);

        mAlertLayer = new DisplayNode("AlertLayer");
        mAlertLayer.Enable = false;
        mAlertLayer.EnableChildren = true;
        SetNodeFullScreenSize(mAlertLayer);
        mAlertLayer.Transform.localPosition = new Vector3(0, 0, -20000);


        var dramaLayer = new DisplayNode("DramaUILayer");
        dramaLayer.Enable = false;
        dramaLayer.EnableChildren = true;
        //UILayerMgr.SetPositionZ(dramaLayer.UnityObject, -1500);
        //Canvas vas = dramaLayer.AddComponent<Canvas>();
        //dramaLayer.AddComponent<GraphicRaycaster>();
        DramaUIManage.Init(dramaLayer);
        SetNodeFullScreenSize(dramaLayer);


        mCGLayer = new DisplayNode("CGUILayer");
        mCGLayer.Enable = false;
        mCGLayer.EnableChildren = true;
        SetNodeFullScreenSize(mCGLayer);
        

        mRoot.AddChild(mBubbleLayer);
        mRoot.AddChild(mHUDLayer);
        mRoot.AddChild(mPickLayer);
        mRoot.AddChild(mUILayer);
        mRoot.AddChild(mAlertLayer);
        mRoot.AddChild(dramaLayer);
        mRoot.AddChild(mCGLayer);

        UILayerMgr.SetLayerOrder(mHUDLayer.UnityObject, 0, true, (int)PublicConst.LayerSetting.UI);
        UILayerMgr.SetLayerOrder(mBubbleLayer.UnityObject, 1, true, (int)PublicConst.LayerSetting.UI);
        UILayerMgr.SetLayerOrder(mPickLayer.UnityObject, 1000, true, (int)PublicConst.LayerSetting.UI);
        UILayerMgr.SetLayerOrder(dramaLayer.UnityObject, 1300, true, (int)PublicConst.LayerSetting.UI);
        UILayerMgr.SetLayerOrder(mAlertLayer.UnityObject, 2000, true, (int)PublicConst.LayerSetting.UI);
        UILayerMgr.SetLayerOrder(mCGLayer.UnityObject, 3000, true, (int)PublicConst.LayerSetting.UI);
    }

    public static void SetNodeFullScreenSize(DisplayNode node)
    {
        node.Transform.anchorMin = Vector2.zero;
        node.Transform.anchorMax = Vector2.one;
        node.Transform.pivot = new Vector2(0.5f, 0.5f);
        node.Bounds2D = new Rect(0, 0, 0, 0);
        //node.Transform.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// 设置node的坐标到 relativeNode节点的anchor的方向并偏移offset
    /// </summary>
    /// <param name="relativeNode"></param>
    /// <param name="node"></param>
    /// <param name="anchor"></param>
    /// <param name="offset"></param>
    public static void AroundRelativeNode(DisplayNode relativeNode, DisplayNode node, ImageAnchor anchor, Vector2 offset)
    {
        var v = relativeNode.LocalToGlobal();
        var v1 = node.Parent.GlobalToLocal(v, true);

        Rect bounds = new Rect();
        UIUtils.AdjustAnchor(anchor, relativeNode.Size2D, ref bounds);

        if(anchor == ImageAnchor.L_C || anchor == ImageAnchor.L_T || anchor == ImageAnchor.L_B)
        {
            v1.x = v1.x - node.Width;
        }
        else if (anchor == ImageAnchor.C_T || anchor == ImageAnchor.C_C || anchor == ImageAnchor.C_B)
        {
            v1.x = v1.x - node.Width * 0.5f;
        }

        if (anchor == ImageAnchor.L_T || anchor == ImageAnchor.C_T || anchor == ImageAnchor.R_T)
        {
            v1.y = v1.y - node.Height;
        }
        else if (anchor == ImageAnchor.L_C || anchor == ImageAnchor.C_C || anchor == ImageAnchor.R_C)
        {
            v1.y = v1.y - node.Height * 0.5f;
        }
        bounds.position += v1 + offset;
        node.Position2D = bounds.position;
    }

    /// <summary>
    /// 将nodeA中的位置转换为nodeB中的位置
    /// </summary>
    /// <param name="scr"></param>
    /// <param name="srcPt"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vector2 ToLocalPostion(DisplayNode nodeA, Vector2 aPt, DisplayNode nodeB)
    {
        var v1 = nodeA.LocalToGlobal();
        var v2 = nodeB.GlobalToLocal(v1, true);
        return v2 + aPt;
    }

    /// <summary>
    /// 从xml创建一个ui节点
    /// HZUISystem.CreateFromFile("xml/team/team_admin.gui.xml")
    /// </summary>
    /// <param name="path">xml/aaa/bbb.gui.xml</param>
    /// <returns></returns>
    public static UIComponent CreateFromFile(string path)
    {
        if (!path.StartsWith("xml/"))
        {
            int index = path.IndexOf("xml/");
            string errorStr;
            if (index == -1)
                errorStr = "xml path error: " + path;
            else
                errorStr = "<a>please replace <f color='ffff0000'>" + path + "</f> to <f color='ff00ff00'>" + path.Substring(index) + "</f></a>";
            Debugger.LogError(errorStr);
            GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, errorStr, "", null, null);
            return null;
        }
        UIComponent uiRoot = null;
        if (HZUISystem.Instance.Editor == null)
        {
            HZUISystem.Instance.Editor = new HZUIEditor(Assets.Scripts.Setting.ProjectSetting.UIEditorPath);
        }
        uiRoot = HZUISystem.Instance.Editor.CreateFromFile(path);
        return uiRoot;
    }

    /// <summary>
    /// 从xml创建一个节点名称为node_name的ui节点
    /// </summary>
    /// <param name="path"></param>
    /// <param name="node_name">节点在ui编辑器中的名称</param>
    /// <returns></returns>
    public static UIComponent CreateFromFile(string path, string node_name)
    {
        HZUIEditor editor = HZUISystem.Instance.Editor;
        UIComponentMeta meta = editor.GetMeta(path);
        if (meta != null)
        {
            UIComponentMeta child_meta = editor.FindChildMetaByEditName(meta, node_name);
            if(child_meta != null)
            {
                return editor.CreateFromMeta(child_meta);
            }
        }
        return null;
    }

    /// <summary>
    /// 清理多个层级UILayer.
    /// </summary>
    public void CleanAllUILayer()
    {
        if(mHUDLayer != null)
        {
            mHUDLayer.RemoveAllChildren(true);
        }

        if(mUILayer != null)
        {
            mUILayer.RemoveAllChildren(true);
        }

        if(mAlertLayer != null)
        {
            mAlertLayer.RemoveAllChildren(true);
        }

        if (mBubbleLayer != null)
        {
            mBubbleLayer.RemoveAllChildren(true);
        }

        if (mPickLayer != null)
        {
            mPickLayer.RemoveAllChildren(true);
        }

        if (mCGLayer != null)
        {
            mCGLayer.RemoveAllChildren(true);
        }
    }

    /// <summary>
    /// HZUISystem.CreateLayoutFromFile("static/common_new/bag_kong.png", UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
    /// </summary>
    /// <param name="path"></param>
    /// <param name="style"></param>
    /// <param name="clipSize"></param>
    /// <returns></returns>
    public static UILayout CreateLayoutFromFile(string path, UILayoutStyle style, int clipSize)
    {
        if (Instance.Editor != null && !string.IsNullOrEmpty(path))
        {
            if (path.StartsWith("/"))   //前缀不能带斜杠.
            {
                string errorStr = "<a>please replace <f color='ffff0000'>" + path + "</f> to <f color='ff00ff00'>" + path.Substring(1) + "</f></a>";
                Debugger.LogError(errorStr);
                GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, errorStr, "", null, null);
                return null;

            }
            UILayoutMeta m = new UILayoutMeta();
            m.ClipSize = clipSize;
            m.Style = style;
            m.ImageName = path;
            var ret = Instance.Editor.CreateLayout(m);
            if (ret.ImageSrc != null)
            {
                return ret;
            }
        }
        return null;
    }

    /// <param name="atlas_name">"#dynamic/effects/skill/skilllevelup.xml|skill_levelup1|21"</param> id方式
    /// <param name="atlas_name">"$dynamic/effects/skill/skilllevelup.xml|skill_levelup1|key1"</param> key方式
    public static UILayout CreateLayout(string path, UILayoutStyle style, int clipSize)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }
        else if (path.StartsWith("#"))
        {
            return CreateLayoutFromAtlas(path, style, clipSize);
        }
        else if (path.StartsWith("@"))
        {
            return CreateLayoutFromCpj(path);
        }
        else if (path.StartsWith("$"))
        {
            return CreateLayoutFromAtlasKey(path, style, clipSize);
        }
        else
        {
            return CreateLayoutFromFile(path, style, clipSize);
        }
    }


    /// <param name="atlas_name">"#dynamic/effects/skill/skilllevelup.xml|skill_levelup1|21"</param>
    public static UILayout CreateLayoutFromAtlas(string atlas_name, UILayoutStyle style, int clipSize)
    {
        DeepCore.GUI.Gemo.Rectangle2D region;
        UnityImage src = Instance.Editor.ParseAtlasTile(atlas_name, out region);
        if (src != null)
        {
            return UILayout.CreateUILayoutImage(style, src, clipSize, region);
        }
        return null;
    }

    /// <param name="atlas_name">"$dynamic/effects/skill/skilllevelup.xml|skill_levelup1|key1"</param>
    public static UILayout CreateLayoutFromAtlasKey(string atlas_name, UILayoutStyle style, int clipSize)
    {
        if (atlas_name.StartsWith("$"))
        {
            atlas_name = atlas_name.Substring(1);
            string[] args = atlas_name.Split('|');
            string a_name = args[0];
            string a_tg = args[1];
            string a_key = args[2];
            DeepCore.GUI.Cell.CPJAtlas atlas = CreateAtlas(a_name, a_tg);
            if (atlas != null)
            {
                int tileId = atlas.GetIndexByKey(a_key);
                UnityImage image = atlas.GetTile(tileId) as UnityImage;
                DeepCore.GUI.Gemo.Rectangle2D region = atlas.GetAtlasRegion(tileId);
                UILayout layout = UILayout.CreateUILayoutImage(style, image, clipSize, region);
                return layout;
            }
        }
        return null;
    }

    public static UILayout CreateLayoutFromCpj(string xmlPath, string spliteName, int animIndex = 0)
    {
        UILayoutMeta m = new UILayoutMeta();
        m.Style = UILayoutStyle.SPRITE;
        m.SpriteName = "@" + xmlPath + "||" + spliteName + "|" + animIndex.ToString();

        if (Instance.Editor != null)
        {
            UILayout lt = Instance.Editor.CreateLayout(m);
            return lt.SpriteController != null ? lt : null;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// a_xml_name a_img_name a_spr_name animIndex
    /// </summary> 
    /// <param name="xmlPath">"@actor_001010/output/actor.xml|actor_001010|001010|3"</param>
    /// <returns></returns>
    public static UILayout CreateLayoutFromCpj(string xmlPath)
    {
        UILayoutMeta m = new UILayoutMeta();
        m.Style = UILayoutStyle.SPRITE;
        m.SpriteName = xmlPath;

        if (Instance.Editor != null)
        {
            UILayout lt = Instance.Editor.CreateLayout(m);
            return lt.SpriteController != null ? lt : null;
        }
        else
        {
            return null;
        }
    }

    public static ImageSprite CreateImageSpriteFromFile(string image_name)
    {
        return CreateImageSpriteFromFile(image_name, Vector2.zero);
    }

    public static ImageSprite CreateImageSpriteFromFile(string image_name, Vector2 pivot)
    {
        if (Instance.Editor != null)
        {
            return Instance.Editor.ParseImageSpriteFromImage(image_name, pivot);
        }
        else
        {
            return null;
        }
    }

    public static ImageSprite CreateImageSpriteFromAtlas(string image_name)
    {
        return CreateImageSpriteFromAtlas(image_name, Vector2.zero);
    }

    public static ImageSprite CreateImageSpriteFromAtlas(string image_name, Vector2 pivot)
    {
        if (Instance.Editor != null)
        {
            return Instance.Editor.ParseImageSpriteFromAtlas(image_name, pivot);
        }
        else
        {
            return null;
        }
    }
    
    public static  UnityImage CreateUnityImageFromFile(string image_name)
    {
        if (Instance.Editor != null)
        {
            return Instance.Editor.GetImage(image_name);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 创建一整张图集.
    /// HZUISystem.Instance.CreateAtlas("dynamic/dynamic_new/skill/skill.xml", "skill")
    /// </summary>
    /// <param name="cpj_file_name"></param>
    /// <param name="atlas_name"></param>
    /// <returns></returns>
    public static DeepCore.GUI.Cell.CPJAtlas CreateAtlas(string cpj_file_name, string atlas_name)
    {
        if (Instance.Editor != null)
        {
            DeepCore.GUI.Cell.CPJAtlas atlas = Instance.Editor.GetAtlas(cpj_file_name, atlas_name);
            return atlas;
        }
        else
        {
            return null;
        }
    }

    public static AttributedString  DecodeHtmlTextToAText(string htmlText)
    {
        if(Instance.Editor != null)
        {
            return Instance.Editor.DecodeAttributedString(DeepCore.Xml.XmlUtil.FromString(htmlText));
        }
        else
        {
            return null;
        }
    }

    public static List<DisplayNode> GetAllChildren(DisplayNode parent)
    {
        List<DisplayNode> ret = new List<DisplayNode>();
        parent.GetAllChild(ret);
        return ret;
    }

    public static DisplayNode FindChildByName(DisplayNode node, string name, bool recursive = true)
    {
        return node.FindChildByName<DisplayNode>(name, recursive);
    }

    public static DisplayNode FindChildByType(DisplayNode node, string type, bool recursive = true)
    {
        return node.FindChildAs<DisplayNode>((obj) => obj.GetType().Name == type || obj.GetType().FullName == type, recursive);
    }

    public static DisplayNode FindChildByUserTag(DisplayNode node, int usertag, bool recursive = true)
    {
        return node.FindChildAs<DisplayNode>(obj =>  obj.UserTag == usertag, recursive);
    }

    public DisplayNode GetUIAlertLayer() { return mAlertLayer; }
    public DisplayNode GetHUDLayer() { return mHUDLayer; }
    public DisplayNode GetUILayer() { return mUILayer; }
    public DisplayNode GetPickLayer() { return mPickLayer; }
    public DisplayNode GetCGLayer() { return mCGLayer; }
    public void HUDLayerAddChild(DisplayNode child) { mHUDLayer.AddChild(child); }
    public void HUDLayerRemoveChild(DisplayNode child, bool dispose) { mHUDLayer.RemoveChild(child, dispose); }
    public void UILayerAddChild(DisplayNode child) { mUILayer.AddChild(child); }
    public void UILayerRemoveChild(DisplayNode child, bool dispose) { mUILayer.RemoveChild(child, dispose); }
    public void UIAlertLayerAddChild(DisplayNode child) { mAlertLayer.AddChild(child); }
    public void UIBubbleChatLayerAddChild(DisplayNode child) { mBubbleLayer.AddChild(child); }
    public void UIBubbleChatLayerRemoveChild(DisplayNode child, bool dispose) { mBubbleLayer.RemoveChild(child, dispose); }
    public void UIPickLayerAddChild(DisplayNode child) { mPickLayer.AddChild(child); }
    public void UIPickLayerRemoveChild(DisplayNode child, bool dispose) { mPickLayer.RemoveChild(child, dispose); }
    public void UICGLayerAddChild(DisplayNode child) { mCGLayer.AddChild(child); }
    public void UICGLayerRemoveChild(DisplayNode child, bool dispose) { mCGLayer.RemoveChild(child, dispose); }

    #endregion

    #region 事件响应.

    public void AddUIPinchHandler(int key, HZUITouchHandler.OnPinchMoveHandler handler)
    {
        if (!mPinchQueue.ContainsKey(key))
        {
            mPinchQueue.Add(key, handler);
            mTouchHandler.OnPinchMoveEvent += handler;
        }
    }

    public void RemoveUIPinchHandler(int key)
    {
        if (mPinchQueue.ContainsKey(key))
        {
            mTouchHandler.OnPinchMoveEvent -= mPinchQueue[key];
            mPinchQueue.Remove(key);
        }
    }


    #endregion
}

public class HZUIEditor : UIEditor
{
    public HZUIEditor(string root)
        : base(root)
    {
        UnityDriver driver =UnityDriver.UnityInstance;
       
        driver.OnGetDefaultImg = Assets.Scripts.Setting.ProjectSetting.OnGetDefaultImage;

        //DefaultFont = Resources.Load<Font>("Font/STHeiti-Medium_0");
        //DefaultFont = Resources.Load<Font>("Font/DroidSansFallback");
        GlobalUseBitmapText = false;


        HZTextButton.DefaultSoundKey = "button";
        HZToggleButton.DefaultCheckedSoundKey = "togglebutton";
        HZToggleButton.DefaultUnCheckedSoundKey = "togglebutton";
        HZScrollPan.DefaultSoundKey = "scrollpan";
    }

    public void SetDefaultFont(Font f)
    {
        DefaultFont = f;
    }

    public UIComponentMeta GetMeta(string path)
    {
        return AddMeta(path);
    }

    public string GetMetaKey(UIComponentMeta meta)
    {
        foreach(var item in mMetaMap)
        {
            if(item.Value == meta)
            {
                return item.Key;
            }
        }
        return null;
    }

    public void ReleaseTextureExt(string imgPath, string suffixName = GameUtil.IMG_SUFFIXNAME)
    {
        string imgMapName = string.Format("{0}{1}", imgPath, suffixName);
        ReleaseTexture(imgMapName);
    }

    /// <summary>
    /// 释放ui中标记为支持释放（默认）的dynamic路径下的UnityImage中的Texture
    /// </summary>
    public void ReleaseDynamicTexture()
    {
        foreach (var item in mImageMap)
        {
            if (item.Key.Contains("res/dynamic"))
            {
                item.Value.ReleaseTexture();
            }
        }
    }

    public UIComponentMeta FindChildMetaByEditName(UIComponentMeta meta,string edit_name)
    {
        if (meta.Childs != null)
        {
            int len = meta.Childs.Count;
            for (int i = 0; i < len; ++i)
            {
                UIComponentMeta child = meta.Childs[i];
                if(child.EditorName == edit_name)
                {
                    return child;
                }
            }

            //recursive
            for (int i = 0; i < len; ++i)
            {
                UIComponentMeta ret = FindChildMetaByEditName(meta.Childs[i],edit_name);
                if (ret != null)
                {
                    return ret;
                }
            }
        }
        return null;
    }
    public override Font CreateFont(string fontName)
    {
        return DefaultFont;
    }
    protected override UIComponent CreateComponent(UIComponentMeta name)
    {
        switch (name.ClassName)
        {
            case UIEditorMeta.UERoot_ClassName: return new HZRoot();
            case UIEditorMeta.UEButton_ClassName: return new HZTextButton();
            case UIEditorMeta.UEToggleButton_ClassName: return new HZToggleButton();
            case UIEditorMeta.UEImageBox_ClassName: return new HZImageBox();
            case UIEditorMeta.UECheckBox_ClassName: return new HZCheckBox();
            case UIEditorMeta.UELabel_ClassName: return new HZLabel();
            case UIEditorMeta.UECanvas_ClassName: return new HZCanvas();
            case UIEditorMeta.UEGauge_ClassName: return new HZGauge();
            case UIEditorMeta.UEFileNode_ClassName: return new HZFileNode();
            case UIEditorMeta.UEScrollPan_ClassName: return new HZScrollPan();
            case UIEditorMeta.UETextBox_ClassName: return new HZTextBox();
            case UIEditorMeta.UETextInput_ClassName: return new HZTextInput();
            case UIEditorMeta.UETextBoxHtml_ClassName: return new HZTextBoxHtml();
            default: return new HZCanvas();
        }
    }

    public override UGUIRichTextLayer CreateRichTextLayer(DisplayNode parent, bool use_bitmap)
    {
        return new HZEditorRichTextLayer(this, parent, use_bitmap);
    }

    public class HZEditorRichTextLayer : EditorRichTextLayer
    {
        public HZEditorRichTextLayer(UIEditor editor, DisplayNode parent, bool use_bitmap_font) : base(editor, parent, use_bitmap_font)
        {
        }

        protected override bool TestTextLineBreak(string text, TextAttribute ta, float testW, out float tw, out float th)
        {
            if (UseBitmapFont)
            {
                return UnityDriver.Instance.testTextLineBreak(text, ta.fontSize, ta.fontStyle, this.BorderCount, testW, out tw, out th);
            }
            else
            {
                int fsize = (int)ta.fontSize;
                UnityEngine.FontStyle fstyle = (UnityEngine.FontStyle)ta.fontStyle;
                TextGenerationSettings setting = base.TextGenSetting;
                setting.fontSize = fsize;
                setting.fontStyle = fstyle;
                float pixelsPerUnit = 1;
                if (!setting.font || setting.font.dynamic)
                {
                    pixelsPerUnit = UGUIMgr.Scale;
                }
                else if (fsize > 0 && setting.font.fontSize > 0)
                {
                    pixelsPerUnit = setting.font.fontSize / (float)fsize;
                }

                setting.scaleFactor = pixelsPerUnit;
                setting.generationExtents = Vector2.zero; 
                tw = base.TextGen.GetPreferredWidth(text, setting) / pixelsPerUnit;
                th = base.TextGen.GetPreferredHeight(text, setting) / pixelsPerUnit;

                //允许3像素精度误差
                if (testW < tw - 3)
                {
                    return true;
                }
                return false;
            }
        }
    }
    
    protected override string FormatPath(string path)
    {
        if (path.StartsWith(ResRoot +CacheImage.Instance.ImageCachePath))
        {
            path = path.Substring(ResRoot.Length);
            return path;
        }

        return path;
    }
}
