using UnityEngine;
using System.Collections.Generic;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using System;

public class AlertDialog
{

    public const int PRIORITY_NORMAL = 0;
    public const int PRIORITY_FRIEND = 10;
    public const int PRIORITY_TEAM = 20;
    public const int PRIORITY_RELIVE = 99;
    public const int PRIORITY_SYSTEM = 100;
    public delegate void AlertAction(object param);

    private DisplayNode mAlertDialogRoot;
    private List<AlertDialogUIBase> mAlertQue = new List<AlertDialogUIBase>();

    private int mDialogSerial = 0;

    public AlertDialog()
    {
        mAlertDialogRoot = new DisplayNode("AlertDialogRoot");
        mAlertDialogRoot.Enable = false;
        mAlertDialogRoot.EnableChildren = true;
        HZUISystem.SetNodeFullScreenSize(mAlertDialogRoot);

        HZUISystem.Instance.UIAlertLayerAddChild(mAlertDialogRoot);
    }

    private AlertDialogUI CreateAlertDialogUI(int priority, string content, string okStr, string cancelStr, string titleStr, object param, AlertAction okCb, AlertAction cancelCb, bool isSingleBtn)
    {
        AlertDialogUI alertUI = new AlertDialogUI("AlertDialog_" + mDialogSerial++);
        alertUI.Priority = priority;
        alertUI.Param = param;
        alertUI.SetContent(content);
        

        if (isSingleBtn)
            alertUI.SetSingleBtn(okStr);
        else
            alertUI.SetMultiBtn(okStr, cancelStr);
        alertUI.OnOkBtnClick = okCb;
        alertUI.OnCancelBtnClick = cancelCb;

        if (titleStr != null)
        {
            alertUI.SetTitle(titleStr);
        }

        alertUI.SubscribOnClose((p) =>
        {
            PopDialogAlert(alertUI);
        });

        alertUI.OnOkBtnClick = (p) =>
        {
            if (okCb != null)
            {
                okCb(p);
            }
        };
        alertUI.OnCancelBtnClick = (p) =>
        {
            if (cancelCb != null)
            {
                cancelCb(p);
            }
        };

        PushDialogAlert(alertUI);

        return alertUI;
    }

    private AlertDialogTimeUI CreateAlertDialogTimeUI(int priority, string content, float time, string okStr, string cancelStr, string titleStr, object param, AlertAction okCb, AlertAction cancelCb, AlertAction timeCb, bool isSingleBtn)
    {
        AlertDialogTimeUI alertUI = new AlertDialogTimeUI("AlertDialogTimeUI" + mDialogSerial++);
        alertUI.Priority = priority;
        alertUI.Param = param;
        alertUI.SetContent(content, time);

        if (isSingleBtn)
            alertUI.SetSingleBtn(okStr);
        else
            alertUI.SetMultiBtn(okStr, cancelStr);
        alertUI.OnOkBtnClick = okCb;
        alertUI.OnCancelBtnClick = cancelCb;
        alertUI.OnTimeOver = timeCb;

        if (titleStr != null)
        {
            alertUI.SetTitle(titleStr);
        }

        alertUI.SubscribOnClose((p) =>
        {
            PopDialogAlert(alertUI);
        });

        alertUI.OnOkBtnClick = (p) =>
        {
            if (okCb != null)
            {
                okCb(p);
            }
        };
        alertUI.OnCancelBtnClick = (p) =>
        {
            if (cancelCb != null)
            {
                cancelCb(p);
            }
        };

        PushDialogAlert(alertUI);

        return alertUI;
    }

    /// <summary>
    /// 插入一个 单按钮确认框 到显示队列中.
    /// </summary>
    /// <param name="priority">优先级</param>
    /// <param name="content">显示文本(html)</param>
    /// <param name="okStr">确定按钮文字</param>
    /// <param name="param">参数传递</param>
    /// <param name="okCb">确定按钮回调</param>
    public string ShowAlertDialog(int priority, string content, string okStr, object param, AlertAction okCb)
    {
       return ShowAlertDialog(priority, content, okStr, null, param, okCb);
    }

    public string ShowAlertDialog(int priority, string content, string okStr, string titleStr, object param, AlertAction okCb)
    {
        AlertDialogUI alertUI = CreateAlertDialogUI(priority, content, okStr, null, titleStr, param, okCb, null, true);
        return alertUI.Key;
    }

    /// <summary>
    /// 插入一个 双按钮确认框 到显示队列中.
    /// </summary>
    /// <param name="priority">优先级</param>
    /// <param name="content">显示文本(html)</param>
    /// <param name="okStr">确定按钮文字</param>
    /// <param name="cancelStr">取消按钮文字</param>
    /// <param name="param">参数传递</param>
    /// <param name="okCb">确定按钮回调</param>
    /// <param name="cancelCb">取消按钮回调</param>
    public string ShowAlertDialog(int priority, string content, string okStr, string cancelStr, object param, AlertAction okCb, AlertAction cancelCb)
    {
        return ShowAlertDialog(priority, content, okStr, cancelStr, null, param, okCb, cancelCb);
    }

    /// <summary>
    /// 插入一个 双按钮确认框 到显示队列中.
    /// </summary>
    /// <param name="priority">优先级</param>
    /// <param name="content">显示文本(html)</param>
    /// <param name="okStr">确定按钮文字</param>
    /// <param name="cancelStr">取消按钮文字</param>
    /// <param name="titleStr">标题文字</param>
    /// <param name="param">参数传递</param>
    /// <param name="okCb">确定按钮回调</param>
    /// <param name="cancelCb">取消按钮回调</param>
    public string ShowAlertDialog(int priority, string content, string okStr, string cancelStr, string titleStr, object param, AlertAction okCb, AlertAction cancelCb)
    {
        AlertDialogUI alertUI = CreateAlertDialogUI(priority, content, okStr, cancelStr, titleStr, param, okCb, cancelCb, false);
        return alertUI.Key;
    }

    /// <summary>
    /// 插入一个 带关闭按钮的单按钮确认框 到显示队列中.
    /// </summary>
    public string ShowAlertDialogWithCloseBtn(int priority, string content, string okStr, string titleStr, object param, AlertAction okCb)
    {
        AlertDialogUI alertUI = CreateAlertDialogUI(priority, content, okStr, null, titleStr, param, okCb, null, true);
        alertUI.ShowCloseBtn(true);
        return alertUI.Key;
    }

    /// <summary>
    /// 插入一个 带关闭按钮的双按钮确认框 到显示队列中.
    /// </summary>
    public string ShowAlertDialogWithCloseBtn(int priority, string content, string okStr, string cancelStr, string titleStr, object param, AlertAction okCb, AlertAction cancelCb)
    {
        AlertDialogUI alertUI = CreateAlertDialogUI(priority, content, okStr, cancelStr, titleStr, param, okCb, cancelCb, false);
        alertUI.ShowCloseBtn(true);
        return alertUI.Key;
    }

    /// <summary>
    /// 插入一个 单按钮倒计时确认框 到显示队列中.
    /// </summary>
    public string ShowAlertDialogTime(int priority, string content, float time, string okStr, string titleStr, object param, AlertAction okCb, AlertAction timeCb)
    {
        AlertDialogTimeUI alertUI = CreateAlertDialogTimeUI(priority, content, time, okStr, null, titleStr, param, okCb, null, timeCb, true);
        return alertUI.Key;
    }

    /// <summary>
    /// 插入一个 双按钮倒计时确认框 到显示队列中.
    /// </summary>
    public string ShowAlertDialogTime(int priority, string content, float time, string okStr, string cancelStr, string titleStr, object param, AlertAction okCb, AlertAction cancelCb, AlertAction timeCb)
    {
        AlertDialogTimeUI alertUI = CreateAlertDialogTimeUI(priority, content, time, okStr, cancelStr, titleStr, param, okCb, cancelCb, timeCb, false);
        return alertUI.Key;
    }

    /// <summary>
    /// 插入一个 自定义弹出框 到显示队列中.
    /// </summary>
    /// <param name="alertUI">自定义UI</param>
    /// <param name="priority">优先级</param>
    /// <param name="param">参数传递</param>
    public string ShowAlertDialog(AlertDialogUIBase alertUI, int priority, object param)
    {
        alertUI.Key = "AlertDialogUI" + mDialogSerial++;
        alertUI.Priority = priority;
        alertUI.Param = param;
        alertUI.SubscribOnClose((p) =>
        {
            PopDialogAlert(alertUI);
        });

        PushDialogAlert(alertUI);

        return alertUI.Key;
    }

    /// <summary>
    /// 检测制定key的对话框是否正在显示或正在等待队列中.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsDialogExist(string key)
    {
        for (int i = mAlertDialogRoot.NumChildren - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertDialogRoot.GetChildAt(i) as AlertDialogUIBase;
            if (string.Equals(dialog.Key, key))
            {
                return true;
            }
        }
        for (int i = mAlertQue.Count - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertQue[i];
            if (string.Equals(dialog.Key, key))
            {
                return true;
            }
        }
        return false;
    }

    public AlertDialogUIBase GetDialogUI(string dialogKey)
    {
        for (int i = mAlertDialogRoot.NumChildren - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertDialogRoot.GetChildAt(i) as AlertDialogUIBase;
            if (string.Equals(dialog.Key, dialogKey))
            {
                return dialog;
            }
        }
        for (int i = mAlertQue.Count - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertQue[i];
            if (string.Equals(dialog.Key, dialogKey))
            {
                return dialog;
            }
        }
        return null;
    }

    public DisplayNode GetDialogUINode(string dialogKey)
    {
        var alertUI = GetDialogUI(dialogKey);
        if (alertUI != null)
        {
            return alertUI.mRoot;
        }
        return null;
    }

    public int GetPriorityDialogCount(int priority)
    {
        int count = 0;
        for (int i = mAlertDialogRoot.NumChildren - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertDialogRoot.GetChildAt(i) as AlertDialogUIBase;
            if (dialog.Priority == priority)
            {
                count++;
            }
        }
        for (int i = mAlertQue.Count - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertQue[i];
            if (dialog.Priority == priority)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 关闭当前正在显示的对话框，并在后台队列中移除.
    /// </summary>
    /// <param name="key"></param>
    public void CloseDialog(string key)
    {
        for (int i = mAlertDialogRoot.NumChildren - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertDialogRoot.GetChildAt(i) as AlertDialogUIBase;
            if (string.Equals(dialog.Key, key))
            {
                PopDialogAlert(dialog);
                return;
            }
        }
        for (int i = mAlertQue.Count - 1; i >= 0; --i)
        {
            AlertDialogUIBase dialog = mAlertQue[i];
            if (string.Equals(dialog.Key, key))
            {
                mAlertQue.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// 设置指定弹出框的对其方式
    /// </summary>
    /// <param name="key"></param>
    /// <param name="anchor"></param>
    public void SetDialogAnchor(string key, DeepCore.GUI.Data.TextAnchor anchor)
    {
        AlertDialogUI alertUI = GetDialogUI(key) as AlertDialogUI;
        if (alertUI != null)
        {
            alertUI.SetAnchor(anchor);
        }
    }

    private void PushDialogAlert(AlertDialogUIBase alertUI)
    {
        //尝试加入显示.
        bool result = ShowAlertDialog(alertUI);

        if (!result)
        {
            //插入队列（从小到大插入排序）.
            bool isInsert = false;
            for (int i = mAlertQue.Count - 1; i >= 0; --i)
            {
                if (alertUI.Priority > mAlertQue[i].Priority)
                {
                    mAlertQue.Insert(i + 1, alertUI);
                    isInsert = true;
                }
            }
            if (!isInsert)
            {
                mAlertQue.Insert(0, alertUI);
            }
        }
    }

    private void PopDialogAlert(AlertDialogUIBase alertUI)
    {
        alertUI.Close();
        mAlertQue.Remove(alertUI);
        //显示下一个.
        if (mAlertQue.Count > 0)
        {
            AlertDialogUIBase lastAlert = mAlertQue[mAlertQue.Count - 1];
            if (ShowAlertDialog(lastAlert))
            {
                mAlertQue.Remove(lastAlert);
            }
        }
    }

    private bool ShowAlertDialog(AlertDialogUIBase alertUI)
    {
        if (mAlertDialogRoot.NumChildren == 0)
        {
            mAlertDialogRoot.AddChild(alertUI);
            alertUI.OnEnter();
            return true;
        }
        else
        {
            //优先级高于当前正在显示的Alert时，立即覆盖显示.
            AlertDialogUIBase curShowAlert = mAlertDialogRoot.GetChildAt(mAlertDialogRoot.NumChildren - 1) as AlertDialogUIBase;
            if (alertUI.Priority > curShowAlert.Priority)
            {
                mAlertDialogRoot.AddChild(alertUI);
                alertUI.OnEnter();
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        mAlertDialogRoot.RemoveAllChildren(true);
        if (mAlertQue.Count > 0)
        {
            for (int i = 0; i < mAlertQue.Count; ++i)
            {
                mAlertQue[i].Dispose();
            }
            mAlertQue.Clear();
        }
    }

}

public class AlertDialogUIBase : UIComponent
{

    private event AlertDialog.AlertAction OnDialogClose;

    public int Priority { get; set; }
    public object Param { get; set; }
    public string Key { get; set; }

    public HZRoot mRoot = null;
    private bool mDestroy = false;

    public static AlertDialogUIBase Create(string xml)
    {
        AlertDialogUIBase ret = new AlertDialogUIBase(xml);
        return ret;
    }

    protected AlertDialogUIBase(string xml)
    {
        InitXml(xml);
    }

    protected AlertDialogUIBase()
    {

    }

    protected void InitXml(string xml)
    {
        this.Name = "AlertDialogUI";
        this.Enable = true;
        this.EnableChildren = true;
        this.IsInteractive = true;
        this.Layout = new UILayout();
        HZUISystem.SetNodeFullScreenSize(this);

        mRoot = (HZRoot)HZUISystem.CreateFromFile(xml);
        this.AddChild(mRoot);
    }

    /// <summary>
    /// 生命周期和Close成对
    /// </summary>
    internal void OnEnter()
    {
        Predicate<string> cb = OnGlobalBackClick;
        LuaSystem.Instance.DoFunc("GlobalHooks.SubscribeGlobalBack", new object[] { Key, cb });
    }

    /// <summary>
    /// 默认功能是拦截
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    protected virtual bool OnGlobalBackClick(string arg)
    {
        return true;
    }

    public void SubscribOnClose(AlertDialog.AlertAction action)
    {
        OnDialogClose += action;
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
                OnDialogClose(Param);
            }
            OnDialogClose = null;
            this.Param = null;
            LuaSystem.Instance.DoFunc("GlobalHooks.UnsubscribeGlobalBack", new object[] { Key });
        }
    }

}

public class AlertDialogUI : AlertDialogUIBase
{

    public AlertDialog.AlertAction OnOkBtnClick;
    public AlertDialog.AlertAction OnCancelBtnClick;
    
    private const string FILE_PATH = "xml/common/common_alert.gui.xml";

    public static AlertDialogUI CreateFromXml(string xml)
    {
        AlertDialogUI ret = new AlertDialogUI();
        ret.Init(xml);
        return ret;
    }

    public AlertDialogUI(string key)
    {
        this.Key = key;
        Init(FILE_PATH);
    }

    public AlertDialogUI()
    {

    }

    protected void Init(string xml)
    {
        InitXml(xml);
        HZImageBox bg = mRoot.FindChildByEditName<HZImageBox>("ib_mask");
        if (bg != null)
        {
            Rect root = HZUISystem.Instance.RootRect;
            float scale = root.width > HZUISystem.SCREEN_WIDTH ? root.width / (float)HZUISystem.SCREEN_WIDTH : root.height / (float)HZUISystem.SCREEN_HEIGHT;

            float mMaskW = bg.Width * scale;
            float mMaskH = bg.Height * scale;

            float mMaskOffsetX = (HZUISystem.SCREEN_WIDTH - mMaskW) * 0.5f;
            float mMaskOffsetY = (HZUISystem.SCREEN_HEIGHT - mMaskH) * 0.5f;

            bg.Position2D = new Vector2(mMaskOffsetX, mMaskOffsetY);
            bg.Size2D = new Vector2(mMaskW, mMaskH);
            bg.Alpha = 0.5f;
        }

        InitCompmont();
    }

    private void InitCompmont()
    {
        HZTextButton closeBtn = mRoot.FindChildByEditName<HZTextButton>("btn_close");
        if (closeBtn != null)
        {
            closeBtn.TouchClick = (sender) =>
            {
                Close();
            };
        }

        HZTextButton okBtnSingle = mRoot.FindChildByEditName<HZTextButton>("bt_quit");
        if (okBtnSingle != null)
        {
            okBtnSingle.TouchClick = (sender) =>
            {
                if (OnOkBtnClick != null)
                {
                    OnOkBtnClick(Param);
                }
                Close();
            };
        }

        HZTextButton okBtnMulti = mRoot.FindChildByEditName<HZTextButton>("bt_yes");
        if (okBtnMulti != null)
        {
            okBtnMulti.TouchClick = (sender) =>
            {
                if (OnOkBtnClick != null)
                {
                    OnOkBtnClick(Param);
                }
                Close();
            };
        }

        HZTextButton cancelBtnMulti = mRoot.FindChildByEditName<HZTextButton>("bt_no");
        if (cancelBtnMulti != null)
        {
            cancelBtnMulti.TouchClick = (sender) =>
            {
                if (OnCancelBtnClick != null)
                {
                    OnCancelBtnClick(Param);
                }
                Close();
            };
        }
    }

    public void SetTitle(string title, uint textColor = 0, uint borderColor = 0)
    {
        MenuBase.SetLabelText(mRoot, "lb_title", title, textColor, borderColor);
    }

    public void SetContent(string content)
    {
        HZTextBox textBox = mRoot.FindChildByEditName<HZTextBox>("tb_common");
        if (textBox != null)
        {
            //textBox.FontSize = 24;
            textBox.XmlText = "<a>" + content + "</a>";
            int lineNum = (textBox.TextComponent as RichTextBox).RichTextLayer.LineCount;
            textBox.TextComponent.Anchor = lineNum == 1 ? DeepCore.GUI.Data.TextAnchor.C_C : DeepCore.GUI.Data.TextAnchor.L_C;
            //textBox = "<size=\"30\">" + content + "</size>";
            /*            textBox.TextSprite.Graphics.alignment = TextAnchor.MiddleCenter;*/
        }
    }

    public void SetAnchor(DeepCore.GUI.Data.TextAnchor anchor)
    {
        HZTextBox textBox = mRoot.FindChildByEditName<HZTextBox>("tb_common");
        if (textBox != null)
        {
            textBox.TextComponent.Anchor = anchor;
        }
    }

    public void SetSingleBtn(string okStr)
    {
        MenuBase.SetVisibleUENode(mRoot, "bt_quit", true);
        MenuBase.SetVisibleUENode(mRoot, "cvs_choise", false);
        if (!string.IsNullOrEmpty(okStr))
        {
            MenuBase.SetButtonText(mRoot, "bt_quit", okStr);
        }
    }

    public void SetMultiBtn(string okStr, string cancelStr)
    {
        MenuBase.SetVisibleUENode(mRoot, "cvs_choise", true);
        MenuBase.SetVisibleUENode(mRoot, "bt_quit", false);
        if (!string.IsNullOrEmpty(okStr))
        {
            MenuBase.SetButtonText(mRoot, "bt_yes", okStr);
        }
        if (!string.IsNullOrEmpty(cancelStr))
        {
            MenuBase.SetButtonText(mRoot, "bt_no", cancelStr);
        }
    }

    public void ShowCloseBtn(bool visible)
    {
        HZTextButton closeBtn = mRoot.FindChildByEditName<HZTextButton>("btn_close");
        if(closeBtn != null)
            closeBtn.Visible = visible;
    }

    /// <summary>
    /// 有关闭按钮的调关闭，有取消按钮的调取消，否则拦截
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    protected override bool OnGlobalBackClick(string arg)
    {
        HZTextButton closeBtn = mRoot.FindChildByEditName<HZTextButton>("btn_close");
        if(closeBtn != null && closeBtn.Visible)
        {
            Close();
            return false;
        }

        HZCanvas multiCvs = mRoot.FindChildByEditName<HZCanvas>("cvs_choise");
        if (multiCvs != null && multiCvs.Visible)
        {
            if (OnCancelBtnClick != null)
            {
                OnCancelBtnClick(Param);
            }
            Close();
            return false;
        }

        return true;
    }

}

public class AlertDialogTimeUI : AlertDialogUI
{

    public AlertDialog.AlertAction OnTimeOver;

    private string mContent;
    private float mTime;
    private int mCurTime;

    public static AlertDialogTimeUI CreateFromXml(string xml)
    {
        AlertDialogTimeUI ret = new AlertDialogTimeUI();
        ret.Init(xml);
        return ret;
    }

    public AlertDialogTimeUI(string key) : base(key)
    {

    }

    public AlertDialogTimeUI()
    {

    }

    public void SetContent(string content, float time)
    {
        this.mContent = content;
        this.mTime = time;
        this.mCurTime = (int)time;
    }

    public void ChangeContent(string content)
    {
        this.mContent = content;
    }

    private void InitContent(string content)
    {
        HZTextBox textBox = mRoot.FindChildByEditName<HZTextBox>("tb_common");
        if (textBox != null)
        {
            textBox.XmlText = "<a>" + content + "</a>";
            textBox.TextComponent.Anchor = DeepCore.GUI.Data.TextAnchor.C_C;
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (mTime > 0)
        {
            mTime -= Time.deltaTime;
            if ((int)mTime != mCurTime)
            {
                mCurTime = (int)mTime;
                string content = string.Format(mContent, mCurTime);
                InitContent(content);
            }
            if (mTime <= 0)
            {
                mTime = 0;
                if (OnTimeOver != null)
                {
                    OnTimeOver(Param);
                }
            }
        }
    }

}
