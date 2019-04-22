using UnityEngine;

using System.Collections.Generic;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;

public class AlertUI : DisplayNode
{

    public bool Cover { get; set; }

    private HZRoot mRoot = null;
    private string FILE_PATH = "xml/tips/tips_small.gui.xml";

    private HZCanvas mFrame;
    private HZCanvas mContentbg;
    private HZTextBox mContent;

    private Vector2 DefaultPos = new Vector2(-1, -1);
    private const uint DefaultColor = 0xe7e5d1ff;   //rgba
    private float ShowTime = 2;
    private float mShowStaticTipsTime = 0;
    private ShowStatus mShowStaticTipsStep = 0;

    private Queue<AlertInfo> mMsgQueue = new Queue<AlertInfo>();
    private Queue<AlertInfo> mMsgCacheQueue = new Queue<AlertInfo>();

    private Rect mDefaultRect;  //缺省文本框位置大小.
    private float mMinWidth;    //最小宽度.

    private enum ShowStatus {
        WaitToShow,
        FadeIn,
        KeepShow,
        FadeOut
    }

    public AlertUI()
    {
        Init();
    }

    private void Init()
    {
        Cover = true;
        this.Name = "AlertUI";
        this.Enable = false;
        HZUISystem.SetNodeFullScreenSize(this);
        HZUISystem.Instance.UIAlertLayerAddChild(this);

        mRoot = (HZRoot)HZUISystem.CreateFromFile(FILE_PATH);
        this.AddChild(mRoot);

        InitCompmont();
    }

    private void InitCompmont()
    {
        this.Visible = false;
        mFrame = mRoot.FindChildByEditName<HZCanvas>("cvs_tips");
        mContentbg = mRoot.FindChildByEditName<HZCanvas>("cvs_tipsbg");
        mContent = mFrame.FindChildByEditName<HZTextBox>("tb_tips");
        mDefaultRect = mFrame.Bounds2D;
        mMinWidth = mFrame.UserTag;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alert">内容</param>
    public void AddAlert(string alert)
    {
        AddAlert(alert, DefaultColor, DefaultPos);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alert">内容</param>
    /// <param name="rgba">颜色（0为缺省颜色）</param>
    public void AddAlert(string alert, uint rgba)
    {
        AddAlert(alert, rgba, DefaultPos);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alert">内容</param>
    /// <param name="rgba">颜色（0为缺省颜色）</param>
    /// <param name="pos">自定义坐标（UI坐标系，00点在UI左上角）</param>
    /// <param name="showTime">自定义显示时间</param>
    public void AddAlert(string alert, uint rgba, Vector2 pos, float showTime = -1)
    {
        AlertInfo info = CreateAlertInfo(alert, rgba, pos, showTime);
        if (Cover)
        {
            mShowStaticTipsStep = ShowStatus.WaitToShow;
            mMsgQueue.Clear();
        }
        mMsgQueue.Enqueue(info);
    }

    private void ShowAlert(string alert, uint rgba, Vector2 pos, float showTime, bool showbg)
    {
        if (mContent != null)
        {
            this.Visible = true;
            mContent.Width = mDefaultRect.width;
            mContent.XmlText = "<f>" + alert + "</f>";
            mContent.FontColor = GameUtil.RGBA2Color(rgba == 0 ? DefaultColor : rgba);
            UGUIRichTextLayer richTextLayer = (mContent.TextComponent as RichTextBox).RichTextLayer;
            richTextLayer.Anchor= richTextLayer.LineCount > 1 ? DeepCore.GUI.Data.TextAnchor.L_C : DeepCore.GUI.Data.TextAnchor.C_C;

            float richW = richTextLayer.ContentWidth;
            float richH = richTextLayer.ContentHeight;
            float paddingX = 20, paddingY = 12;
            mContent.Width = Mathf.Max(richW, mMinWidth);
            mContent.Height = richH;
            mFrame.Width = mContent.Width + paddingX * 2;
            mFrame.Height = richH + paddingY * 2;
            mContent.X = paddingX;
            mContent.Y = (mFrame.Height - richH) * 0.5f;
            if (pos.Equals(DefaultPos))
            {
                pos = new Vector2(mDefaultRect.center.x - mFrame.Width / 2, mDefaultRect.y);
            }
            mFrame.Position2D = pos;

            this.Alpha = 0;
            mShowStaticTipsStep = ShowStatus.FadeIn;
            float p = Mathf.InverseLerp(5, 15, alert.Length);
            mShowStaticTipsTime = 0;
            ShowTime = showTime > 0 ? showTime : Mathf.Lerp(1, 2.5f, p);

            //type == 1不提供显示背景的方法
            if (showbg)
            {
                mContentbg.Visible = true;
                mContentbg.Size2D = mFrame.Size2D;
            }
            else
            {
                mContentbg.Visible = false;
            }
        }
    }

    public void HideAlert()
    {
        this.Visible = false;
    }

    public void Update(float delatTime)
    {
        switch (mShowStaticTipsStep)
        {
            case ShowStatus.WaitToShow:
                if(mMsgQueue.Count > 0){
                    AlertInfo info = mMsgQueue.Dequeue();
                    ShowAlert(info.Content, info.RGBA, info.Pos, info.ShowTime, info.ShowBg);
                    ReleaseAlertInfo(info);
                }
                break;
            case ShowStatus.FadeIn:
                TipsFadeIn(delatTime);
                break;
            case ShowStatus.KeepShow:
                TipsShow(delatTime);
                break;
            case ShowStatus.FadeOut:
                TipsFadeOut(delatTime);
                break;
        }
    }

    private void TipsShow(float delatTime)
    {
        mShowStaticTipsTime += delatTime;
        if (mShowStaticTipsTime > ShowTime)
        {
            mShowStaticTipsStep = ShowStatus.FadeOut;
        }
    }

    private void TipsFadeIn(float delatTime)
    {
        //this.Alpha += 0.05f;
        //if (this.Alpha >= 1.0f)
        //{
        //    mShowStaticTipsStep = ShowStatus.KeepShow;
        //}

        //需求改动，淡入效果去除，直接弹出.
        this.Alpha = 1;
        mShowStaticTipsStep = ShowStatus.KeepShow;
    }

    private void TipsFadeOut(float delatTime)
    {
        this.Alpha -= 0.1f;
        if (this.Alpha <= 0)
        {
            HideAlert();
            mShowStaticTipsStep = ShowStatus.WaitToShow;
        }
    }

    private AlertInfo CreateAlertInfo(string content, uint rgba, Vector2 pos, float showTime = -1)
    {
        AlertInfo info = null;
        if (mMsgCacheQueue.Count > 0)
        {
            info = mMsgCacheQueue.Dequeue();
            info.Content = content;
            info.RGBA = rgba;
            info.Pos = pos;
            info.ShowTime = showTime;
            info.ShowBg = true;
        }
        else
        {
            info = new AlertInfo(content, rgba, pos, showTime);
        }
        return info;
    }

    private void ReleaseAlertInfo(AlertInfo info)
    {
        mMsgCacheQueue.Enqueue(info);
    }

    private class AlertInfo
    {
        public string Content { get; set; }
        public uint RGBA { get; set; }
        public Vector2 Pos { get; set; }
        public float ShowTime { get; set; }
        public bool ShowBg { get; set; }

        public AlertInfo(string content, uint rgba, Vector2 pos, float showTime = -1, bool showbg = true)
        {
            this.Content = content;
            this.RGBA = rgba;
            this.Pos = pos;
            this.ShowTime = showTime;
            this.ShowBg = showbg;
        }
    }

}
