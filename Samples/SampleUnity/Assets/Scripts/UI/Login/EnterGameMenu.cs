using Assets.Scripts.Setting;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepMMO.Data;
using UnityEngine;

 

public class EnterGameMenu : MenuBase
{

    public delegate void OnEnterGame(ServerInfo server);
    public OnEnterGame EnterGame;
    public delegate void OnOpenServerList();
    public OnOpenServerList OpenServerList;

    private ServerList mServerList;
    private ServerInfo mDefaultServer;

    private float mFadeTimeMax;
    private float mFadeTimer;
    private FadeState mFadeState;

    private enum FadeState
    {
        Stop,
        FadeIn,
        FadeOut
    }

    public static EnterGameMenu Create(ServerList serverList)
    {
        EnterGameMenu ret = new EnterGameMenu(serverList);
        if (ret != null && ret.OnInit())
        {
            return ret;
        }
        return null;
    }

    public EnterGameMenu(ServerList serverList) : base("EnterGameMenu")
    {
        SetServerList(serverList);
    }

    protected override bool OnInit()
    {
        InitWithXml("xml/login/login_noticeserver.gui.xml");

        //compmont
        InitCompment();

        return true;
    }

    private void InitCompment()
    {
        this.SetCompAnime(this, UIAnimeType.NoAnime);
        AudioListener.volume = 1;
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_top"), HudManager.HUD_TOP);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_bottom"), HudManager.HUD_BOTTOM);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_left"), HudManager.HUD_LEFT);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_right"), HudManager.HUD_RIGHT);
        

        //close button
        HZTextButton closeBtn = mRoot.FindChildByEditName<HZTextButton>("bt_changesever");
        if (closeBtn != null)
        {
            closeBtn.TouchClick = (sender) => { CloseAndDestroy(); };
        }

        HZTextButton startBtn = mRoot.FindChildByEditName<HZTextButton>("bt_login");
        startBtn.TouchClick = DoEnterServer;

        HZTextButton serverBtn = mRoot.FindChildByEditName<HZTextButton>("bt_change");
        serverBtn.TouchClick = DoOpenServerList;

        //去掉切换账号按钮，临时用用户按钮代替，正式接SDK时，用户按钮为调出SDK
        HZTextButton changeActBtn = mRoot.FindChildByEditName<HZTextButton>("btn_user");
        changeActBtn.TouchClick = DoChangeAccount;
        
        HZTextButton btn_notice = mRoot.FindChildByEditName<HZTextButton>("btn_notice");
        btn_notice.TouchClick = ShowNotice;

        string version = PublicConst.ClientVersion + "." + PublicConst.SVNVersion;
        SetLabelText("lb_version", version);
        SetLabelText("lb_version2", ProjectSetting.UPDATE_RES_VERSION);


        InitServer();
    }

    private void ShowNotice(UIComponent sender)
    {
        LoginNoticeMenu notice = LoginNoticeMenu.Create();
        MenuMgr.Instance.AddMenu(notice,UIShowType.Cover);
    }

    private void InitServer()
    {
        SetVisibleUENode("btn_change", mServerList != null);
        SetVisibleUENode("cvs_sever", mServerList != null);
        if (mDefaultServer != null)
        {
            SetLabelText("lb_severname", mDefaultServer.name, (uint)mDefaultServer.view_rgba);
            SetImageBox("ib_light", mDefaultServer.icon);
        }
    }

    public void SetServerList(ServerList serverList)
    {
        if (serverList == null)
        {
            SetVisibleUENode("cvs_sever", false);
            SetVisibleUENode("btn_change", false);
            return;
        }
        mServerList = serverList;
        mServerList.OnServerListRefresh -= SetServerList;
        mServerList.OnServerListRefresh += SetServerList;
        this.mDefaultServer = serverList.GetLastLoginServer();
        if (mDefaultServer == null)
        {
            mDefaultServer = serverList.GetRecomServerByIndex(0);
        }

        SetLabelText("lb_id", DataMgr.Instance.AccountData.Account);

        InitServer();
    }

    public void ShowServerInfo(ServerList serverList)
    {
        SetServerList(serverList);
    }

    private void DoEnterServer(DisplayNode sender)
    {
        if (EnterGame != null)
        {
            EnterGame(mDefaultServer);
        }
    }

    private void DoOpenServerList(DisplayNode sender)
    {
        if (OpenServerList != null)
        {
            OpenServerList();
        }
    }

    private void DoChangeAccount(DisplayNode sender)
    {
        //SDK 切换账号功能实现
        OneGameSDK.Instance.SwitchAccount();

        DataMgr.Instance.AccountData.Account = "";
        SetVisibleUENode("cvs_sever", false);
        SetVisibleUENode("btn_change", false);
        DoEnterServer(sender);
    }

    public void FadeIn(float time)
    {
        if (mFadeState == FadeState.FadeIn) return;
        mFadeState = FadeState.FadeIn;
        mFadeTimeMax = time;
        mFadeTimer = 0;
        this.Alpha = 0;
    }

    public void FadeOut(float time)
    {
        if (mFadeState == FadeState.FadeOut) return;
        mFadeState = FadeState.FadeOut;
        mFadeTimeMax = time;
        mFadeTimer = time;
        this.Alpha = 1;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        switch (mFadeState)
        {
            case FadeState.Stop:
                return;
            case FadeState.FadeOut:
                if (mFadeTimer <= 0)
                {
                    mFadeTimer = 0;
                    mFadeState = FadeState.Stop;
                }
                else
                    mFadeTimer -= Time.deltaTime;
                break;
            case FadeState.FadeIn:
                if (mFadeTimer >= mFadeTimeMax)
                {
                    mFadeTimer = mFadeTimeMax;
                    mFadeState = FadeState.Stop;
                }
                else
                {
                    mFadeTimer += Time.deltaTime;
                }
                break;
        }
        this.Alpha = mFadeTimer / mFadeTimeMax;
    }

    protected override string UITag() { return "EnterGameMenu"; }

}
