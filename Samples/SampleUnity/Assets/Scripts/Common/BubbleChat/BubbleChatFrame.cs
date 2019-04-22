using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.GUI.Display.Text;

public class BubbleChatFrame
{

    public const int PRIORITY_NORMAL = 0;
    public const int PRIORITY_TEAM = 20;
    public delegate void TimeAction();

    private static DisplayNode mBubbleChatFrameRoot;
    private static DisplayNode mCGBubbleChatFrameRoot;
    //private List<BubbleChatFrameUIBase> mAlertQue = new List<BubbleChatFrameUIBase>();

    private int mDialogSerial = 0;

    public BubbleChatFrame()
    {
        if (mBubbleChatFrameRoot == null)
        {
            mBubbleChatFrameRoot = new DisplayNode("BubbleChatFrameRoot");
            mBubbleChatFrameRoot.Enable = false;
            mBubbleChatFrameRoot.EnableChildren = false;
            HZUISystem.Instance.UIBubbleChatLayerAddChild(mBubbleChatFrameRoot);
        }


        if (mCGBubbleChatFrameRoot == null)
        {
            mCGBubbleChatFrameRoot = new DisplayNode("CGBubbleChatFrameRoot");
            mCGBubbleChatFrameRoot.Enable = false;
            mCGBubbleChatFrameRoot.EnableChildren = false;
            HZUISystem.Instance.UIBubbleChatLayerAddChild(mCGBubbleChatFrameRoot);
        }
       
    }

    private BubbleChatFrameTimeUI CreateBubbleChatFrameTimeUI(string key, string content, float time, TimeAction timecb)
    {
        BubbleChatFrameTimeUI bubblechat = new BubbleChatFrameTimeUI(key);
        bubblechat.SetContent(content, time);
        bubblechat.SubscribOnClose(timecb);
        ShowBubbleChatFrame(bubblechat);
        return bubblechat;
    }

    private BubbleChatFrameTimeUI CreateCGBubbleChatFrameTimeUI(string key, string content)
    {
        BubbleChatFrameTimeUI bubblechat = new BubbleChatFrameTimeUI(key);
        bubblechat.SetContent(content,-1);
        ShowCGBubbleChatFrame(bubblechat);
        return bubblechat;
    }
    /// <summary>
    /// 显示CG气泡框
    /// </summary>
    public BubbleChatFrameTimeUI ShowCGBubbleChatFrame(string key, string content)
    {
        BubbleChatFrameTimeUI bubblechat = CreateCGBubbleChatFrameTimeUI(key, content);
        return bubblechat;
    }
    /// <summary>
    /// 显示一个气泡框
    /// </summary>
    public BubbleChatFrameTimeUI ShowBubbleChatFrameTime(string key, string content, float time,TimeAction timecb)
    {
        BubbleChatFrameTimeUI bubblechat = CreateBubbleChatFrameTimeUI(key, content, time, timecb);
        return bubblechat;
    }

    public void Hide(bool value)
    {
        if(mBubbleChatFrameRoot != null)
            mBubbleChatFrameRoot.Visible = !value;
    }

    public void HideCG(bool value)
    {
        if (mCGBubbleChatFrameRoot != null)
            mCGBubbleChatFrameRoot.Visible = !value;
    }

    /// <summary>
    /// 检测制定key的对话框是否正在显示或正在等待队列中.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsDialogExist(string key)
    {
        for (int i = mBubbleChatFrameRoot.NumChildren - 1; i >= 0; --i)
        {
            BubbleChatFrameUIBase dialog = mBubbleChatFrameRoot.GetChildAt(i) as BubbleChatFrameUIBase;
            if (string.Equals(dialog.Key, key))
            {
                return true;
            }
        }
        return false;
    }

  
    private bool ShowBubbleChatFrame(BubbleChatFrameUIBase bubblechat)
    {
        mBubbleChatFrameRoot.AddChild(bubblechat);
        return true;
    }
    private bool ShowCGBubbleChatFrame(BubbleChatFrameUIBase bubblechat)
    {
        mCGBubbleChatFrameRoot.AddChild(bubblechat);
        return true;
    }

    public void Clear()
    {
        if (mBubbleChatFrameRoot != null)
        {
           mBubbleChatFrameRoot.RemoveAllChildren(true);
        }
        if(mCGBubbleChatFrameRoot != null)
        {
            mCGBubbleChatFrameRoot.RemoveAllChildren(true);
        }
        
    }

}

public class BubbleChatFrameUIBase : UIComponent
{

    private event BubbleChatFrame.TimeAction OnDialogClose;
    
    public string Key { get; set; }

    public HZRoot mRoot = null;
    public float OrgX = 0;
    public float OrgY= 0;
    private bool mDestroy = false;
    
    private UIFollowTarget mUIFollow;
    public UIFollowTarget UIFollow { get { return mUIFollow; } }
    public Transform LastTarget { get; set; }
    public static BubbleChatFrameUIBase Create(string xml)
    {
        BubbleChatFrameUIBase ret = new BubbleChatFrameUIBase(xml);
        return ret;
    }

    protected BubbleChatFrameUIBase(string xml)
    {
        InitXml(xml);
    }

    protected BubbleChatFrameUIBase()
    {

    }

    protected void InitXml(string xml)
    {
        this.Name = "BubbleChatFrameUI";
        this.Enable = true;
        this.EnableChildren = true;
        this.IsInteractive = true;
        this.Layout = new UILayout();
        HZUISystem.SetNodeFullScreenSize(this);
        UILayerMgr.SetLayerOrder(this.mGameObject, 1, true, (int)PublicConst.LayerSetting.UI);
        mUIFollow = this.Transform.gameObject.GetComponent<UIFollowTarget>();
        if (mUIFollow == null)
        {
            mUIFollow = this.Transform.gameObject.AddComponent<UIFollowTarget>();
            mUIFollow.enabled = false;
        }
        mRoot = (HZRoot)HZUISystem.CreateFromFile(xml);
       
        OrgY = mRoot.Y;
        OrgX = mRoot.X;
        this.AddChild(mRoot);
    }
    

    public void SubscribOnClose(BubbleChatFrame.TimeAction action)
    {
        OnDialogClose += action;
    }
    protected override void OnDispose()
    {
        if (mUIFollow != null)
        {
            mUIFollow.Dispose();
            mUIFollow = null;
        }
        LastTarget = null;
        base.OnDispose();

    }
public void Close()
    {
        if (!mDestroy)
        {
            mDestroy = true;
            if (this.Parent == null)
            {
                this.Dispose();
            }
            else
            {
                this.RemoveFromParent(true);
            }
            if (OnDialogClose != null)
            {
                OnDialogClose();
                OnDialogClose = null;
            }
        }
    }

}

public class BubbleChatFrameUI : BubbleChatFrameUIBase
{
    private const int MaxWidht = 254;
    private const int MaxHeight = 80;
    private const int MinWidth = 48;
    private const int MinHeight = 48;
    private const string FILE_PATH = "xml/common/common_talk.gui.xml";
    private UECanvas mCanvas = null;
    private UEImageBox mArrow;
   
    public BubbleChatFrameUI(string key)
    {
        this.Key = key;
        Init(FILE_PATH);
    }

    public BubbleChatFrameUI()
    {

    }

    protected void Init(string xml)
    {
        InitXml(xml);
        InitCompmont();
        this.Visible = false;
    }

    private void InitCompmont()
    {
        
    }
    
    protected virtual void SetContent(string content)
    {
        HZTextBoxHtml textBox = mRoot.FindChildByEditName<HZTextBoxHtml>("thb_content");
        mCanvas = mRoot.FindChildByEditName<UECanvas>("cvs_talk");
        mArrow = mRoot.FindChildByEditName<UEImageBox>("ib_jiantou");
        if (textBox != null)
        {
           
            if (textBox.TextComponent is RichTextBox)
            {

                string _content = (string)LuaSystem.Instance.DoFunc("GlobalHooks.NpcTalkParse.ParseContent", content);
                textBox.Size2D = new Vector2(MaxWidht, MaxHeight);
                var richText = textBox.TextComponent as RichTextBox;
                TextAttribute defaultTextAttr = richText.RichTextLayer.DefaultTextAttribute;
                object[] args = { _content, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), defaultTextAttr.fontSize };
                AttributedString acontent = (AttributedString)LuaSystem.Instance.DoFunc("GlobalHooks.UIUtil.HandleTalkDecode", args);
                if(acontent != null)
                {
                    richText.AText = acontent;
                }
                int lineNum = (textBox.TextComponent as RichTextBox).RichTextLayer.LineCount;
                textBox.TextComponent.Anchor = lineNum == 1 ? DeepCore.GUI.Data.TextAnchor.C_C : DeepCore.GUI.Data.TextAnchor.L_C;
                textBox.Size2D = new Vector2(richText.RichTextLayer.ContentWidth, richText.RichTextLayer.ContentHeight);
                mCanvas.Size2D = new Vector2(textBox.Size2D.x + 28, textBox.Size2D.y + 24);
                textBox.X = 14;
                textBox.Y = 12;
                mRoot.Y = - mCanvas.Height - 10;
                mRoot.X = - mCanvas.Width/2;
                mArrow.X = mCanvas.Width / 2 - mArrow.Width /2;
                mArrow.Y = mCanvas.Height - 8;
            }
            //textBox = "<size=\"30\">" + content + "</size>";
            /*            textBox.TextSprite.Graphics.alignment = TextAnchor.MiddleCenter;*/
           
        }
    }

   

    public virtual void OnPositionSync(Transform target)
    {
        if (mCanvas != null)
        {
            this.Visible = true;
            //Vector3 v = GameSceneMgr.Instance.SceneCamera.WorldToScreenPoint(height);//人物屏幕坐标 
            //Vector3 newpos = GameSceneMgr.Instance.UICamera.ScreenToWorldPoint(v);
            //this.Transform.position = newpos;
            if (UIFollow != null && target != LastTarget)
            {
                UIFollow.SetTarget(target);
                UIFollow.enabled = true;
                LastTarget = target;
            }
        }
        
    }
    
}

public class BubbleChatFrameTimeUI : BubbleChatFrameUI
{
    
    private string mContent;
    private float mTime;
    private int mCurTime;
    private bool mNolimitTime = false;
    public static BubbleChatFrameTimeUI CreateFromXml(string xml)
    {
        BubbleChatFrameTimeUI ret = new BubbleChatFrameTimeUI();
        ret.Init(xml);
        return ret;
    }

    public BubbleChatFrameTimeUI(string key) : base(key)
    {

    }

    public BubbleChatFrameTimeUI()
    {

    }

    public void SetContent(string content, float time)
    {
        this.mContent = content;
        if (time != -1)
        {
            this.mTime = time;
            this.mCurTime = (int)time;
        }
        else
        {
            mNolimitTime = true;
        }
        this.SetContent(content);
    }

    public void SetClose(BubbleChatFrame.TimeAction timecb)
    {
        SubscribOnClose(timecb);
    }

    public void ChangeContent(string content)
    {
        this.mContent = content;
    }
    
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (mTime > 0 && !mNolimitTime)
        {
            mTime -= Time.deltaTime;
            if ((int)mTime != mCurTime)
            {
                mCurTime = (int)mTime;
               
            }
            if (mTime <= 0)
            {
                mTime = 0;
                Close();
            }
        }
    }

}
