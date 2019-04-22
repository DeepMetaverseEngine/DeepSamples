using UnityEngine;
using System.Collections;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;

/// <summary>
/// 游戏Loading效果(菊花).
/// </summary>
public class WaitingUI : UIComponent
{

    private const string FILE_PATH = "xml/tips/waiting.gui.xml";

    private HZRoot mRoot = null;
    //Loading效果.
    private HZImageBox mImg = null;

    private float mAnimateWaitTime = 0;
    private bool mAnimateShow = false;

    public delegate void TimesUpEvent();
    public TimesUpEvent TimesUpCallBack;

    //引用计数，为0时关闭动画.
    private int mShowCount = 0;

    private const float DelayShowTime = 0.5f;
    private float mDelayShowTime;

    public bool IsWaiting
    {
        get
        {
            return mShowCount > 0;
        }
    }
    public WaitingUI()
    {
        Init();
    }

    private void Init()
    {
        this.Name = "WaitingUI";
        this.Enable = true;
        this.IsInteractive = true;
        this.Layout = new UILayout();
        HZUISystem.SetNodeFullScreenSize(this);
        HZUISystem.Instance.UIAlertLayerAddChild(this);

        mRoot = (HZRoot)HZUISystem.CreateFromFile(FILE_PATH);
        this.AddChild(mRoot);

        mImg = mRoot.FindChildByEditName<HZImageBox>("loading");

        //mask
        ShowAnimate(false);
    }

    /// <summary>
    /// 显示Loading效果.
    /// </summary>
    /// <param name="flag">是否显示.</param>
    /// <param name="showImmediately">是否马上显示.</param>
    /// <param name="waitTime">超时时间.</param>
    /// <param name="callBack">超时回调.</param>
    public void Show(bool flag, float waitTime = 10, TimesUpEvent timeUpCB = null)
    {
        if(flag)
        {
            mShowCount++;
            mAnimateWaitTime = waitTime;
            TimesUpCallBack = timeUpCB;
            ShowAnimate(true);
        }
        else
        {
            if (--mShowCount <= 0)
            {
                ShowAnimate(false);
            }
        }
    }

    private void ShowAnimate(bool show)
    {
        this.Enable = show;
        mAnimateShow = show;
        if (!show)
        {
            mShowCount = 0;
            mAnimateWaitTime = 0;
            mDelayShowTime = DelayShowTime;
            TimesUpCallBack = null;
        }
    }

    public void Update(float delattime)
    {
        if (mAnimateShow)
        {
            mDelayShowTime -= delattime;
            if (mDelayShowTime <= 0)
            {
                if(!mRoot.Visible)
                    mRoot.Visible = true;
            }
        }
        else
        {
            if (mRoot.Visible)
            {
                mRoot.Visible = false;
                mDelayShowTime = DelayShowTime;
            }
            return;
        }

        mAnimateWaitTime -= delattime;
        if (mAnimateWaitTime <= 0)
        {
            TimesUp();
        }
    }

    private void TimesUp()
    {
        TimesUpEvent callback = TimesUpCallBack;
        ShowAnimate(false);
        if (callback != null)
        {
            callback();
        }
    }

    public void Clear()
    {
        ShowAnimate(false);
    }

}
