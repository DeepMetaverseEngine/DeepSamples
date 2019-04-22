using DeepCore.GUI.Display.Text;
using DeepCore.GUI.Gemo;
using DeepCore.Unity3D.UGUI;
using UnityEngine;


/// <summary>
/// 游戏提示功能管理类.
/// </summary>
public class GameAlertManager
{

    private bool mInitFinish = false;

    private WaitingUI mWaiting = null;
    private AlertDialog mAlertDialog = null;
    private AlertUI mStaticTips = null;
    private GoRoundMgr mGoRound = null;
    private FloatingTipsU mFloatingTips = null;
    private CpjAnimeHelper mCpjAnime = null;

    private WaitingUI Waiting
    {
        get
        {
            if (mWaiting == null)
                Init();
            return mWaiting;
        }
    }

    public bool IsWaiting
    {
        get
        {
            return mWaiting.IsWaiting;
        }
    }

    public AlertDialog AlertDialog
    {
        get
        {
            if (mAlertDialog == null)
                Init();
            return mAlertDialog;
        }
    }
    private AlertUI StaticTips
    {
        get
        {
            if (mStaticTips == null)
                Init();
            return mStaticTips;
        }
    }

    public GoRoundMgr GoRound
    {
        get
        {
            if (mGoRound == null)
                Init();
            return mGoRound;
        }
    }
    private FloatingTipsU FloatingTips
    {
        get
        {
            if (mFloatingTips == null)
                Init();
            return mFloatingTips;
        }
    }
    
    public CpjAnimeHelper CpjAnime
    {
        get
        {
            if (mCpjAnime == null)
                mCpjAnime = new CpjAnimeHelper();
            return mCpjAnime;
        }
    }

    private static GameAlertManager mInstance = null;
    public static GameAlertManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GameAlertManager();
            }
            return mInstance;
        }
    }

    private GameAlertManager()
    {

    }

    private void Init()
    {
        if (!mInitFinish)
        {
            //这里初始化从上至下是有顺序的，代表显示层级.
            mAlertDialog = new AlertDialog();
            mGoRound = new GoRoundMgr();
            mFloatingTips = new FloatingTipsU();
            mStaticTips = new AlertUI();
            mWaiting = new WaitingUI();

            mInitFinish = true;
        }
    }

    public void ShowGoRoundTips(string text)
    {
        GoRound.addTip(text);
        //GoRound.changeBG("dynamic/instance/text_bg.png");
    }

    public void ShowGoRoundTips(string text, string path)
    {
        GoRound.addTip(text);
        if (path != null)
            GoRound.changeBG(path);
    }

    /*
     * xmlPath = "#dynamic/dynamic_new/hud/123.xml|hud|24"
     */
    public void ShowGoRoundTipsXml(string text, string xmlPath)
    {
        GoRound.addTip(text);
        if (xmlPath != null)
            GoRound.changeBGXml(xmlPath);
    }

    /// <summary>
    /// 居中显示的静态提示.
    /// </summary>
    /// <param name="content">内容</param>
    public void ShowNotify(string content)
    {
        StaticTips.AddAlert(content);
    }

    /// <summary>
    /// 居中显示的静态提示.
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="rgba">颜色（0为缺省颜色）</param>
    public void ShowNotify(string content, uint rgba)
    {
        StaticTips.AddAlert(content, rgba);
    }

    /// <summary>
    /// 可自定义位置的静态提示.
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="rgba">颜色（0为缺省颜色）</param>
    /// <param name="pos">自定义坐标（UI坐标系，00点在UI左上角）</param>
    public void ShowNotify(string content, uint rgba, Vector2 pos)
    {
        StaticTips.AddAlert(content, rgba, pos);
    }

    /// <summary>
    /// 可自定义位置的静态提示.
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="rgba">颜色（0为缺省颜色）</param>
    /// <param name="pos">自定义坐标（UI坐标系，00点在UI左上角）</param>
    /// <param name="showbg">自定义显示时间</param>
    public void ShowNotify(string content, uint rgba, Vector2 pos, float showTime)
    {
        StaticTips.AddAlert(content, rgba, pos, showTime);
    }

    public void CloseAllAlertUI()
    {

    }

    /// <summary>
    /// 插入一个 单按钮确认框 到显示队列中.
    /// </summary>
    /// <param name="priority">优先级</param>
    /// <param name="content">显示文本(html)</param>
    /// <param name="param">参数传递</param>
    /// <param name="okStr">确定按钮文字</param>
    /// <param name="okCb">确定按钮回调</param>
    public string ShowAlertDialog(int priority, string content, string okStr, object param, AlertDialog.AlertAction okCb)
    {
        return AlertDialog.ShowAlertDialog(priority, content, okStr, param, okCb);
    }

    /// <summary>
    /// 插入一个 单按钮确认框 到显示队列中.
    /// </summary>
    /// <param name="priority">优先级</param>
    /// <param name="content">显示文本(html)</param>
    /// <param name="param">参数传递</param>
    /// <param name="okStr">确定按钮文字</param>
    /// <param name="titleStr">标题文字</param>
    /// <param name="okCb">确定按钮回调</param>
    public string ShowAlertDialog(int priority, string content, string okStr, string titleStr, object param, AlertDialog.AlertAction okCb)
    {
        return AlertDialog.ShowAlertDialog(priority, content, okStr, titleStr, param, okCb);
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
    public string ShowAlertDialog(int priority, string content, string okStr, string cancelStr, object param, AlertDialog.AlertAction okCb, AlertDialog.AlertAction cancelCb)
    {
        return AlertDialog.ShowAlertDialog(priority, content, okStr, cancelStr, param, okCb, cancelCb);
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
    public string ShowAlertDialog(int priority, string content, string okStr, string cancelStr, string titleStr, object param, AlertDialog.AlertAction okCb, AlertDialog.AlertAction cancelCb)
    {
        return AlertDialog.ShowAlertDialog(priority, content, okStr, cancelStr, titleStr, param, okCb, cancelCb);
    }

    #region FloatingTips(漂浮提示).

    /// <summary>
    /// 对象做飘浮效果.
    /// </summary>
    /// <param name="node">执行飘浮的显示对象.</param>
    /// <param name="distance">飘浮距离.</param>
    /// <param name="parent">显示对象所处的父节点.</param>
    /// <param name="beginGloblePosition">指定飘浮起始位置.</param>
    /// 
    /// 
    /// local text = self.m_Root:GetComponent("btn_close"):Clone()
    /// GameAlertManager.Instance:ShowFloatingTips(text, 80, nil, Vector2.New(0, 0))
    public void ShowFloatingTips(DisplayNode node, float distance, DisplayNode parent, Point2D beginPosition)
    {
        if (beginPosition != null)
        {
            FloatingTips.ShowFloatingTips(node, distance, parent, new Vector2(beginPosition.x, beginPosition.y));
        }
        else
        {
            FloatingTips.ShowFloatingTips(node, distance, parent, Vector2.zero);
        }
    }

    public void ShowFloatingTips(AttributedString text, float distance)
    {
        FloatingTips.ShowFloatingTips(text, distance);
    }

    /// <summary>
    /// 文字飘浮效果.
    /// </summary>
    /// <param name="text">文字内容.</param>
    /// <param name="color">文字颜色.</param>
    /// <param name="distance">上飘距离.</param>
    /// <param name="parent">所属父节点.</param>
    /// <param name="beginPosition">相对于父节点的位置.</param>
    public void ShowFloatingTips(string text, uint rgba, float distance, DisplayNode parent, Point2D beginPosition = null, int fontSize = 25)
    {
        FloatingTips.ShowFloatingTips(text, rgba, distance, parent, new Vector2(beginPosition.x, beginPosition.y), fontSize);
    }

    /// <summary>
    /// 文字飘浮效果.
    /// </summary>
    /// <param name="text">文字内容.</param>
    public void ShowFloatingTips(string text)
    {
        FloatingTips.ShowFloatingTips(text);
    }

    public void ShowFloatingTips(string text, uint rgba)
    {
        FloatingTips.ShowFloatingTips(text, rgba);
    }

    public void ShowFloatingTips(string text, uint rgba, int font)
    {
        FloatingTips.ShowFloatingTips(text, rgba, font);
    }

    public void ShowFloatingTipsImage(string imageName, string imagePath)
    {
        FloatingTips.ShowFloatingTipsImage(imageName, imagePath);
    }

    public void ClearAllFloatingTips()
    {
        if(FloatingTips != null)
        {
            FloatingTips.ClearAllFloatingTips();
        }
    }
    #endregion


    #region Loading效果.

    public void ShowLoading(bool flag, bool showImmediately = false, float waitTime = 10, WaitingUI.TimesUpEvent timeUpCB = null)
    {
        Waiting.Show(flag, waitTime, timeUpCB);
    }

    #endregion

    public void Update(float deltaTime)
    {
        if (mInitFinish)
        {
            if (Waiting != null)
                Waiting.Update(deltaTime);
            if (StaticTips != null)
                StaticTips.Update(deltaTime);
            if (FloatingTips != null)
                FloatingTips.Update(deltaTime);
        }
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        AlertDialog.Clear();
        Waiting.Clear();
        if (mCpjAnime != null)
        {
            mCpjAnime.Clear(reLogin, reConnect);
        }
        ClearAllFloatingTips();
        if(mGoRound != null)
            mGoRound.clear();
    }
}
