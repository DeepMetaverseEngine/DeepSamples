using Assets.Scripts.Setting;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;
using TLClient.Net;
using UnityEngine;

 


public class LoginOfficial : MenuBase
{

    public delegate void OnLoginSuccess(LoginMessage msg);
    public OnLoginSuccess LoginSuccess;

    private HZTextInput mAccountInput = null;
    private HZTextInput mPasswordInput = null;

    public static LoginOfficial Create()
    {
        LoginOfficial ret = new LoginOfficial();
        if (ret != null && ret.OnInit())
        {
            return ret;
        }
        return null;
    }

    public LoginOfficial() : base("LoginOfficial")
    {

    }

    protected override bool OnInit()
    {
        InitWithXml("xml/login/login.gui.xml");
        //compmont
        InitCompment();
            
        return true;
    }

    private void InitCompment()
    {
        this.SetCompAnime(this, UIAnimeType.NoAnime);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_top"), HudManager.HUD_TOP);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_bottom"), HudManager.HUD_BOTTOM);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_left"), HudManager.HUD_LEFT);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_right"), HudManager.HUD_RIGHT);

        //back button
        HZTextButton backBtn = mRoot.FindChildByEditName<HZTextButton>("bt_back");
        if (backBtn != null)
        {
            backBtn.TouchClick = (sender) =>
            {
                Close();
            };
        }

        HZTextButton registerBtn = mRoot.FindChildByEditName<HZTextButton>("zhuce");
        registerBtn.TouchClick = OnRegisterBtnClick;
        HZTextButton loginBtn = mRoot.FindChildByEditName<HZTextButton>("denglu");
        loginBtn.TouchClick = OnLoginBtnClick;

        mAccountInput = mRoot.FindChildByEditName<HZTextInput>("yonghumingshurukuang");
        mPasswordInput = mRoot.FindChildByEditName<HZTextInput>("mimashurukuang");

        mAccountInput.Input.characterLimit = PublicConst.AccountLength;
        mPasswordInput.Input.characterLimit = PublicConst.PasswordLength;

        mAccountInput.Input.Text = PlayerPrefs.GetString("account");
        mPasswordInput.Input.Text = PlayerPrefs.GetString("password");

        string version = PublicConst.ClientVersion + "." + PublicConst.SVNVersion;
        SetLabelText("lb_version", version);
        SetLabelText("lb_version2", ProjectSetting.UPDATE_RES_VERSION);
    }

    private void OnRegisterBtnClick(DisplayNode sender)
    {
        DoRegister();
    }

    private void OnLoginBtnClick(DisplayNode sender)
    {
        DoLogin();
    }

    private void DoRegister()
    {
        string username = mAccountInput.Input.Text;
        string password = mPasswordInput.Input.Text;

        DataMgr.Instance.LoginData.RequestRegister(username, password, (data) =>
        {
            if (data.Result == HttpRequest.ResultType.Success)
            {
                RegisterMessage msg = HttpMsgUtils.ParseJsonString<RegisterMessage>(data.Content) as RegisterMessage;

                if (msg.status.Equals("1"))
                {
                    DoLogin();
                }
                else
                {
                    GameAlertManager.Instance.ShowNotify(msg.message);
                }
            }
        });
    }

    private void DoLogin()
    {
        string username = mAccountInput.Input.Text;
        string password = mPasswordInput.Input.Text;
        int logintype = 0;
        string sdkExpansion = "";
        string sdkVersion = "";
        string sdkToken = "";
        string sdkTime = "";
        string sdkChannelId = "";
        string sdkChannelName = "";

        LoginMessage msg = new LoginMessage();
        msg.username = username;
        msg.sign = password;
        msg.time = 999999;
        LoginSuccessCB(msg);

        //DataMgr.Instance.LoginData.RequestLogin(username, password, logintype, sdkExpansion, sdkVersion, sdkToken, sdkTime, sdkChannelId, sdkChannelName, (result) =>
        //{
        //    if (result.Result == HttpRequest.ResultType.Success)
        //    {
        //        LoginMessage msg = HttpMsgUtils.ParseJsonString<LoginMessage>(result.Content) as LoginMessage;

        //        if(msg != null)
        //        {
        //            if (msg.status.Equals("1"))
        //            {
        //                LoginSuccessCB(msg);
        //            }
        //            else if(!string.IsNullOrEmpty(msg.message))
        //            {
        //                GameAlertManager.Instance.ShowNotify(msg.message);
        //            }
        //        }
        //    }
        //});
    }

    protected virtual void LoginSuccessCB(LoginMessage msg)
    {
        PlayerPrefs.SetString("account", mAccountInput.Text);
        PlayerPrefs.SetString("password", mPasswordInput.Text);
        DataMgr.Instance.AccountData.Password = mPasswordInput.Text;

        Close();

        if (LoginSuccess != null)
        {
            LoginSuccess(msg);
        }
    }

    protected override void OnDestory()
    {
        
    }

    protected override string UITag() { return "LoginOfficial"; }

}
