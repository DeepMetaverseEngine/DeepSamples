
using UnityEngine;
using System.Collections.Generic;

using DeepMMO.Protocol.Client;
using DeepMMO.Data;
using TLClient.Net;
using System;
using Assets.Scripts;

public class LoginController : MonoBehaviour {

    private LoginAnimeScene mLoginAnimeScene;
    private ServerList mServerList;
    private EnterGameMenu mEnterGame;

    private float mRefreshSrvListTimer;

    private bool mInitFinish = false;

    public void Init()
    {
        if (!mInitFinish)
        {
            mInitFinish = true;
            GameSceneMgr.Instance.InitNetWork();

            mLoginAnimeScene = gameObject.AddComponent<LoginAnimeScene>();
            mLoginAnimeScene.InitFinish = () =>
            {
                mLoginAnimeScene.SwitchSceneAnime(LoginAnimeScene.CameraAnimeTag.Title, false, null, () =>
                {
                    mEnterGame = EnterGameMenu.Create(null);
                    mEnterGame.EnterGame = OnEnterServer;
                    mEnterGame.OpenServerList = OnOpenServerList;
                    MenuMgr.Instance.AddMenu(mEnterGame);

                    if (LoginNoticeMenu.autoShowNotice)
                    {
                        mEnterGame.Visible = false;
                        LoginNoticeMenu notice = LoginNoticeMenu.Create();
                        notice.CloseUI = OnGameStart;
                        MenuMgr.Instance.AddMenu(notice,UIShowType.Cover);
                    }
                    else
                    {
                        OnGameStart(true);
                    }
                });
            };

            this.gameObject.SetActive(true);

            EventManager.Subscribe("Event.System.Back", OnGlobalBack);
        }
    }

    private void OnGameStart(bool isAutoLogin)
    {
        mEnterGame.Visible = true;
#if (UNITY_ANDROID || UNITY_IOS)
        if(OneGameSDK.Instance.Channel == 0){
            LoginOfficial login = LoginOfficial.Create();
            login.LoginSuccess = OnLoginSuccess;
            MenuMgr.Instance.AddMenu(login, UIShowType.HideBackMenu);
        }
        else{
           OneGameSDK.Instance.Login(isAutoLogin);
                OneGameEventListener.Instance.onLoginSuccess = (SDKTypeEvent evt) =>{
                Debug.Log("LoginResult " + evt.evtData.DataToString());
                //SDKBaseData data = evt.evtData;
                //string userID = data.GetData(SDKAttName.USER_ID);
                //string userToken = data.GetData(SDKAttName.USER_TOKEN);
                //SDKBaseData platform = OneGameSDK.Instance.GetPlatformData();
                //int msgId = (int)UserCenterMsgID.Login;
                //Dictionary<string, string> arg = new Dictionary<string, string>();
                //arg.Add("type", msgId.ToString());
                //arg.Add("token", userToken);
                //arg.Add("appid", OneGameSDK.Instance.AppId.ToString());
                //arg.Add("channel", OneGameSDK.Instance.Channel.ToString());
                //arg.Add("ostype", PublicConst.OSType.ToString());

                Debug.Log("LoginResult " + evt.evtData.DataToString());
                SDKBaseData data = evt.evtData;
                string userName = data.GetData(SDKAttName.USER_NAME);
                string userToken = data.GetData(SDKAttName.USER_TOKEN);

                LoginMessage msg = new LoginMessage();
                msg.username = userName;
                msg.sign = userToken;
                msg.time = long.MaxValue;
                OnLoginSuccess(msg);



                //arg.Add("mac", PlatformMgr.PluginGetUUID());
                //arg.Add("clientUA", PlatformMgr.PluginGetUserAgent());

                //HttpRequest httpRequest = GameGlobal.Instance.gameObject.AddComponent<HttpRequest>();
                //httpRequest.RequestPost(platform.GetData(SDKAttName.AUTH_URL), arg, msgId, (result) =>
                //{
                //    httpRequest.Destroy();
                //    Debug.Log("auth_url" + platform.GetData(SDKAttName.AUTH_URL) + "data: " + result.Content);
                //    if (result.Result == HttpRequest.ResultType.Success)
                //    {
                //        var responseBody = MiniJSON.Json.Deserialize(result.Content);
                //        if (responseBody is Dictionary<string, object>)
                //        {
                //            var resMap = responseBody as Dictionary<string, object>;
                //            LoginMessage msg = new LoginMessage();
                //            msg.username = resMap["username"].ToString();
                //            msg.channelName = resMap["username"].ToString();
                //            msg.time = long.MaxValue;
                //            msg.sign = "123456789";
                //            OnLoginSuccess(msg);
                //        }
                //    }
                //});


                //AccountData ad = DataMgr.Instance.AccountData;
                //ad.SdkToken = ret.Sid;
                //string username = ret.Username;
                //string password = ret.Password;
                //ad.LoginType = ret.LoginType;
                //int appid = SDK.SDKWrapper.Instance.AppId;
                //int channel = SDK.SDKWrapper.Instance.Channel;
                //int ostype = PublicConst.OSType;
                //int version = PublicConst.LogicVersion;
                //string mac = PlatformMgr.PluginGetUUID();
                //ad.SdkExpansion = ret.SdkExpansion;
                //ad.SdkVersion = ret.SdkVersion;
                //ad.ApnsID = ret.ApnsId;
                //string userAgent = PlatformMgr.PluginGetUserAgent();

                //GameDebug.Log("OnGameStart sid: " + ad.SdkToken + " appid: " + appid + " channel: " + channel);

                //DataMgr.Instance.LoginData.GetLoginData(username, password, ad.LoginType, ad.SdkExpansion, ad.SdkVersion, ad.SdkToken, "", "", "", (result) =>
                //{
                //    if (result.Result == HttpRequest.ResultType.Success)
                //    {
                //        LoginMessage msg = HttpMsgUtils.ParseXmlString(typeof(LoginMessage), result.Content) as LoginMessage;
                //
                //        if (msg != null)
                //        {
                //            if (msg.Status.Equals("1"))
                //            {
                //                OnLoginSuccess(msg);
                //            }
                //            else if (!string.IsNullOrEmpty(msg.Message))
                //            {
                //                GameAlertManager.Instance.ShowNotify(msg.Message);
                //            }
                //        }
                //    }
                //});
                };
        }
#else
        LoginOfficial login = LoginOfficial.Create();
        login.LoginSuccess = OnLoginSuccess;
        MenuMgr.Instance.AddMenu(login, UIShowType.HideBackMenu);
#endif
    }

    private void OnLoginSuccess(LoginMessage msg)
    {
        DataMgr.Instance.AccountData.Account = msg.username;
        DataMgr.Instance.AccountData.Sign = msg.sign;
        DataMgr.Instance.AccountData.TimeStamp = msg.time;

        //SDKWrapper.Instance.channelUsername = msg.ChannelName;
        //SDK.SDKWrapper.Instance.startPush(msg.Username);
        InitServerList();
    }


    private void InitServerList()
    {
        mServerList = new ServerList();

        if (mServerList.GetLastLoginServer() != null || mServerList.GetRecomServerByIndex(0) != null)
        {
            mEnterGame.ShowServerInfo(mServerList);
        }
        else
        {
            OnOpenServerList();
        }
    }

    private void OnOpenServerList()
    {
        ServerListMenu serverListMenu = ServerListMenu.Create(mServerList);
        serverListMenu.EnterServer = OnEnterServer;
        MenuMgr.Instance.AddMenu(serverListMenu, UIShowType.HideBackMenu);
    }

    private void OnEnterServer(ServerInfo server)
    {
        if (string.IsNullOrEmpty(DataMgr.Instance.AccountData.Account) || server == null)
        {
            OnGameStart(false);
            return;
        }
        if (server.state == "4")
        {
            GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString(server.state_text));
            return;
        }

        string account = DataMgr.Instance.AccountData.Account;
        string sign = DataMgr.Instance.AccountData.Sign;
        ulong time = DataMgr.Instance.AccountData.TimeStamp;
        string ip;
        int port;
        DeepCore.Net.IPUtil.TryParseHostPort(server.address, out ip, out port);
        string clientUA = PlatformMgr.PluginGetUserAgent();

        string mac = PlatformMgr.PluginGetUUID();
        int ostype = PublicConst.OSType;
        string region = PublicConst.ClientRegion;
        string channel = OneGameSDK.Instance.Channel.ToString();
        string version = PublicConst.LogicVersion.ToString();

        DataMgr.Instance.LoginData.RequestEntryServer(server.id, account, sign, time, clientUA, ip, port, mac, ostype, region, channel, version, (rsp)=> {
            if (rsp != null)
            {
                PlayerPrefs.SetString("lastLoginAccount", account);
                PlayerPrefs.SetString("lastLoginServerId", server.id);
                DataMgr.Instance.AccountData.CurSrvInfo = server;
                DataMgr.Instance.LoginData.RequestRoleList((rsp1) =>
                {
                    MenuMgr.Instance.HideMenu(true);
                    mLoginAnimeScene.SwitchSceneAnime(LoginAnimeScene.CameraAnimeTag.Role, true, () =>
                    {
                        List<RoleSnap> roleList = rsp1.s2c_roles;
                        if (roleList != null && roleList.Count > 0)
                        {
                            //select role
                            SelectRoleMenu selectRole = SelectRoleMenu.Create(mLoginAnimeScene, roleList);
                            selectRole.EnterScene = OnEnterScene;
                            selectRole.OnGoBack = OnBackToPreMenu;
                            MenuMgr.Instance.AddMenu(selectRole, UIShowType.HideBackMenu);
                        }
                        else
                        {
                            //create role
                            CreateRoleMenu createRole = CreateRoleMenu.Create(mLoginAnimeScene);
                            createRole.EnterScene = OnEnterScene;
                            createRole.OnGoBack = OnBackToPreMenu;
                            MenuMgr.Instance.AddMenu(createRole, UIShowType.HideBackMenu);
                        }
                    },
                    () =>
                    {
                        MenuMgr.Instance.HideMenu(false);
                    });
                });
            }
            else
            {
                GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, HZLanguageManager.Instance.GetString("common_server_auth_failed"), "", null, (object param) => 
                {
                    MenuMgr.Instance.CloseMenuByTag("ServerListMenu");

                    OnGameStart(true);
                });
            }
        });
    }

    private void OnBackToPreMenu()
    {
        //mEnterGame.FadeOut(0);
        //MenuMgr.Instance.HideMenu(true);
        //mLoginAnimeScene.SwitchSceneAnime(LoginAnimeScene.CameraAnimeTag.Title, true, () =>
        //{
        //    mEnterGame.FadeIn(0.5f);
        //    MenuMgr.Instance.HideMenu(false);
        //}, null);
        GameSceneMgr.Instance.ExitGame(null);
    }

    private void OnEnterScene(string playerId, int scendId)
    {
        DataMgr.Instance.LoginData.RequestEnterGame(playerId, (rsp1) =>
        {
            if (rsp1 != null)
            {
                if (rsp1.IsSuccess)
                {
                    PlayerPrefs.SetString("lastRole", playerId);
                    TLProtocol.Data.TLClientRoleData rData = rsp1.s2c_role as TLProtocol.Data.TLClientRoleData;
                    DataMgr.Instance.AccountData.CurPlayerId = playerId;
                    DataMgr.Instance.UserData.ReadRoleData(rData);
                    Clear();
                    GameSceneMgr.Instance.EnterGameScene();
                    //提交游戏相关数据给SDK
                    int create_time = (int)rsp1.s2c_role.create_time.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                    var localUserData = OneGameSDK.Instance.GetUserData();
                    localUserData.SetData(SDKAttName.DATE_TYPE, RoleDateType.enterGame);
                    localUserData.SetData(SDKAttName.ROLE_ID, rsp1.s2c_role.uuid);
                    localUserData.SetData(SDKAttName.ROLE_NAME, rsp1.s2c_role.name);
                    localUserData.SetData(SDKAttName.ROLE_LEVEL, rsp1.s2c_role.level);
                    localUserData.SetData(SDKAttName.PARTY_NAME, rData.guildName);
                    localUserData.SetData(SDKAttName.VIP_LEVEL, rData.VipLv);
                    localUserData.SetData(SDKAttName.ROLE_CREATE_TIME, create_time);
                    localUserData.SetData(SDKAttName.ZONE_ID, DataMgr.Instance.AccountData.CurSrvInfo.id);
                    localUserData.SetData(SDKAttName.ZONE_NAME, DataMgr.Instance.AccountData.CurSrvInfo.view_realm_name);
                    localUserData.SetData(SDKAttName.SERVER_ID, DataMgr.Instance.AccountData.CurSrvInfo.id);
                    localUserData.SetData(SDKAttName.ZONE_NAME, DataMgr.Instance.AccountData.CurSrvInfo.name);
                    OneGameSDK.Instance.UpdatePlayerInfo();
                }
            }
        });
    }

    public delegate void QuickLoginEvent(ClientEnterGameResponse msg);
    public static void QuickLogin(QuickLoginEvent cb)
    {
        string playerId = DataMgr.Instance.AccountData.CurPlayerId;
        DataMgr.Instance.LoginData.Reconnect((rsp) =>
        {
            if (rsp != null)
            {
                DataMgr.Instance.LoginData.RequestEnterGame(playerId, (rsp1) =>
                {
                    if (rsp1 != null)
                    {
                        TLProtocol.Data.TLClientRoleData rData = rsp1.s2c_role as TLProtocol.Data.TLClientRoleData;
                        DataMgr.Instance.UserData.ReadRoleData(rData);
                        GameSceneMgr.Instance.StartChangeScene();
                    }
                    if (cb != null)
                    {
                        cb(rsp1);
                    }
                });
            }
            else
            {
                if (cb != null)
                {
                    cb(null);
                }
            }
        });
    }

    private void OnGlobalBack(EventManager.ResponseData res)
    {
        var menu = MenuMgr.Instance.GetTopMenu();
        if (menu != null)
        {
            if (menu is SelectRoleMenu)
            {
                (menu as SelectRoleMenu).DoBack();
            }
            else if (!(menu is EnterGameMenu))
            {
                menu.Close();
            }
            else if (menu is EnterGameMenu)
            {
                EventManager.Fire("Event.System.Back.NoUI", EventManager.defaultParam);
            }
        }
    }

    public void Update()
    {
        if (mServerList != null && !string.IsNullOrEmpty(DataMgr.Instance.AccountData.Account))
        {
            mRefreshSrvListTimer += Time.deltaTime;
            if (mRefreshSrvListTimer > 30.0f)
            {
                mRefreshSrvListTimer = 0;
                mServerList.RequestSvrList();
            }
        }
    }

    public void Clear()
    {
        if (mInitFinish)
        {
            mInitFinish = false;

            if (mLoginAnimeScene != null)
            {
                mLoginAnimeScene.Destroy();
                mLoginAnimeScene = null;
            }

            if (mServerList != null)
            {
                mServerList.Destroy();
                mServerList = null;
            }

            mEnterGame = null;

            EventManager.Unsubscribe("Event.System.Back", OnGlobalBack);

            MenuMgr.Instance.Clear(true, false);
            this.gameObject.SetActive(false);

            HZUISystem.Instance.Editor.ReleaseAllTexture();

            var op = Resources.UnloadUnusedAssets();
        }
    }

    void OnDestroy()
    {
        
    }

}
