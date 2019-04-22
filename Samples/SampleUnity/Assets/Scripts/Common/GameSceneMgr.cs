
using System;
using System.Collections.Generic;
using Assets.Scripts;
using SLua;
using UnityEngine;
using DeepCore.Unity3D;
using UnityStandardAssets.ImageEffects;
using DeepMMO.Client.Battle;
using DeepMMO.Protocol.Client;
using DeepMMO.Protocol;


public class GameSceneMgr : IZeusNetObserver
{

    private static GameSceneMgr mInstance = null;
    public SyncServerTime syncServerTime { get; private set; }


    private GameSceneMgr()
    {
        mInstance = this;

        LoginCtrl = GameObject.Find("LoginNode").GetComponent<LoginController>();

        SceneCameraNode = GameObject.Find("CameraNode").GetComponent<SceneCamera>();
        Transform c = SceneCameraNode.transform.Find("PositionNode/PitchNode/MainCamera");
        SceneCamera = c.GetComponent<Camera>();
        SceneCameraCullingMask = SceneCamera.cullingMask;
        SceneCameraCameraClearFlags = SceneCamera.clearFlags;

        LoadingCameraNode = SceneCameraNode.transform.Find("Loadingcamera").GetComponent<LoadingCamera>();

        UGUI = GameObject.Find("/UGUI_ROOT").GetComponent<UGUIMgr>();

        UICamera = UGUIMgr.UGUICamera;
        UICameraCullingMask = UICamera.cullingMask;

        BattleRun = GameObject.Find("BattleRun").GetComponent<BattleRun>();

        syncServerTime = new SyncServerTime();
    }

    private GameLoadScene mLoadingUI = null;

    private Dictionary<string, List<Dictionary<string, object>>> mRouterDict;

    //3D场景摄像机节点
    public SceneCamera SceneCameraNode { get; private set; }

    //3D场景摄像机
    public int SceneCameraCullingMask { get; private set; }
    public CameraClearFlags SceneCameraCameraClearFlags { get; private set; }
    public Camera SceneCamera { get; private set; }

    //NGUI场景摄像机
    private int UICameraCullingMask;
    public Camera UICamera { get; private set; }

    //loading摄像机节点
    public LoadingCamera LoadingCameraNode { get; private set; }

    //BattleRun实例
    public BattleRun BattleRun { get; private set; }

    private int mLastMapId;
    private bool mReconnecting;

    private bool mReadyToSpeicialLoading;

    private DepthOfFieldDeprecated mDof;

    //BattleScene实例
    public TLBattleScene BattleScene
    {
        get
        {
            if (BattleRun != null)
                return BattleRun.Client;
            return null;
        }
    }

    public UGUIMgr UGUI { get; private set; }

    public LoginController LoginCtrl { get; private set; }

    public static GameSceneMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                new GameSceneMgr();
            }
            return mInstance;
        }
    }

    ~GameSceneMgr()
    {
        //Debugger.Log("--------- ~GameSceneMgr -----------");
    }

    [DoNotToLua]
    public void EnterLoginScene()
    {
        LoginCtrl.Init();

        BattleRun.gameObject.SetActive(false);
        HideScene(true);
        UGUI.gameObject.SetActive(false);
    }

    [DoNotToLua]
    public void EnterGameScene()
    {
        BattleRun.gameObject.SetActive(true);
        HideScene(false);
        UGUI.gameObject.SetActive(true);
        BattleRun.Init();
    }

    [DoNotToLua]
    public void EnterLoadingScene(bool showLoading)
    {
        if (showLoading || mZeroScene)
        {
            mLoadingUI = GameGlobal.Instance.GetLoadingUI(mZeroScene ? 2 : (mReadyToSpeicialLoading ? 1 : 0));
            mLoadingUI.SetContent("");
            mLoadingUI.Reset();
            mLoadingUI.Show();
            if (!mReadyToSpeicialLoading)
            {
                MenuMgr.Instance.HideHud(true);
                MenuMgr.Instance.HideMenu(true);
            }
            mZeroScene = false;
        }
        else
        {
            FullScreenEffect.Instance.ShowRippleEffect();
        }

        //订阅loading进度委托.
        if (BattleRun != null)
        {
            BattleRun.OnLoadingProcessChange += this.OnLoadingProcessChange;
            BattleRun.OnLoadingComplete += this.OnLoadingComplete;
        }
    }

    public void ReadyToLoading()
    {
        mReadyToSpeicialLoading = true;
        MenuMgr.Instance.HideHud(true);
        MenuMgr.Instance.HideMenu(true);
        GameSceneMgr.Instance.LoadingCameraNode.ShowOutEffect(() =>
        {
        });
    }

    private bool mZeroScene = false;
    public void ReadyToLoadingExt()
    {
        BattleLoaderMgr.Instance.PreLoadScene(GameUtil.GetSceneIDToMapID(GameUtil.GetIntGameConfig("scene_defaultbirth_next")));
        mZeroScene = true;
    }

    public void SetSceneCameraActive(bool var)
    {
        SceneCamera.enabled = var;
        AudioListener lis = SceneCameraNode.GetComponent<AudioListener>();
        if (lis != null)
        {
            lis.enabled = var;
        }

    }

    #region 切换场景.

    private void OnClientEnterZoneNotify(RPGBattleClient client)
    {
        mNextClient = client;
        ClientEnterZoneNotify msg = client.Enter;
        bool firstEnterScene = mLastMapId == 0;

        DataMgr.Instance.UserData.MapTemplateId = mLastMapId = msg.s2c_MapTemplateID;
        DataMgr.Instance.UserData.ZoneTemplateId = msg.s2c_ZoneTemplateID;
        DataMgr.Instance.UserData.ZoneUUID = msg.s2c_ZoneUUID;
        DataMgr.Instance.UserData.ZoneLineIndex = msg.s2c_SceneLineIndex;
        DataMgr.Instance.UserData.ZoneGuildId = msg.s2c_GuildUUID;
        if (msg.s2c_Ext != null)
            DataMgr.Instance.UserData.CurSceneGuildName = msg.s2c_Ext.Get("guildName");
        else
            DataMgr.Instance.UserData.CurSceneGuildName = null;

        if (firstEnterScene)
        {
            BattleRun.Client = (TLBattleScene)TLBattleFactory.Instance.CreateBattleScene(client);
            mNextClient = null;
        }
        else if (mReconnecting)
        {
            ReleaseAllSceneData(false);
            bool needLoadScene = BattleRun.ReloadScene();
            EnterLoadingScene(needLoadScene);
            StartChangeScene();
        }
        else
        {
            ReleaseAllSceneData(false);
            bool needLoadScene = BattleRun.ReloadScene();
            EnterLoadingScene(needLoadScene);
            StartChangeScene();
        }
    }

    public void StartChangeScene()
    {
        if (mNextClient != null)
        {
            BattleRun.Client = (TLBattleScene)TLBattleFactory.Instance.CreateBattleScene(mNextClient);
            mNextClient = null;
        }
    }

    private void DelayClean()
    {
        ////清除可能由上一张场景留下的缓存
        //GameGlobal.Instance.ClearCache();
        //GameGlobal.Instance.ResetMemoryWarning();

        //string funcName = "GlobalHooks.ClearCache";
        //if (LuaScriptMgr.Instance.IsFuncExists(funcName))
        //{
        //    LuaScriptMgr.Instance.CallLuaFunction(funcName);
        //}
        //LuaScriptMgr.Instance.LuaGC();
        //HZUnityAssetBundleManager.GetInstance().UnloadUnusedAssets();
        //System.GC.Collect();
    }

    private void ReleaseAllSceneData(bool reLogin)
    {
        LoginCtrl.Clear();
        CutSceneManager.Instance.Clear(reLogin);
        BattleRun.Clear(reLogin);
        //SceneSound.ClearAudioSources();
        //TLSoundManager.Instance.ReleaseClipSourceUsingPool();
        //TLSoundManager.Instance.CleanResourceMap(true, true);
        //SoundManager.Instance.StopBGM();  策划说要无缝连接
        SoundManager.Instance.StopAllSound();
        //fin的调用要跟GlobalHooks.Init成对.
        LuaSystem.Instance.DoFunc("GlobalHooks.Fin", new object[] { reLogin, mReconnecting });
        HudManager.Instance.Clear(reLogin, mReconnecting);
        MenuMgr.Instance.Clear(reLogin, mReconnecting);
        BattleInfoBarManager.Clear();
        TextLabel.Clear();
        GameAlertManager.Instance.Clear(reLogin, mReconnecting);
        BubbleChatFrameManager.Instance.Clear(reLogin, mReconnecting);
        HZUISystem.Instance.Editor.ReleaseAllTexture();
        //EventManager.UnsubscribeAllLua();
        DataMgr.Instance.Clear(reLogin, mReconnecting);
        GameAlertManager.Instance.AlertDialog.Clear();
        //string funcName = "GlobalHooks.Drama.Stop";
        //if (LuaScriptMgr.Instance.IsFuncExists(funcName))
        //{
        //    LuaScriptMgr.Instance.CallLuaFunction(funcName);
        //}
        if (DramaUIManage.Instance != null)
        {
            DramaUIManage.Instance.highlightMask.SetArrowTransform(false);
        }
        HZUnityAssetBundleManager.GetInstance().CleanAssetBundleMapInQueue();
        mServerShutDown = mBackToLogin = false;
        if (reLogin)
        {
            mLastMapId = 0;
            if (mLoadingUI != null)
            {
                mLoadingUI.Hide();
            }

            TLNetManage.Instance.Clear();

            GameGlobal.Instance.ClearCache();
            //GameGlobal.Instance.ResetMemoryWarning();
            //LuaScriptMgr.Instance.LuaGC();
        }
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        mReconnecting = false;
        DestoryEffect();
    }

    private void OnLoadingProcessChange(float process)
    {
        if (mLoadingUI != null)
        {
            mLoadingUI.Percent = process;
        }
        if (process == 1)
        {
            OnEnterSceneSuccess();
        }
    }


    private void OnLoadingComplete(bool isFirst)
    {
        if (mLoadingUI != null)
        {
            mLoadingUI.Hide();
        }

        if (BattleRun != null)
        {
            BattleRun.OnLoadingProcessChange -= this.OnLoadingProcessChange;
            BattleRun.OnLoadingComplete -= this.OnLoadingComplete;
        }

        if (mReadyToSpeicialLoading)
        {
            mReadyToSpeicialLoading = false;
            LoadingCameraNode.ShowInEffect(() =>
            {
                LoadingCameraNode.Reset();
                ChangeSceneComplete(isFirst);
            });
        }
        else
        {
            ChangeSceneComplete(isFirst);
        }
    }

    private void ChangeSceneComplete(bool isFirst)
    {
        if (isFirst)
        {
            EventManager.Fire("Event.Scene.FirstInitFinish", EventManager.defaultParam);
        }
        EventManager.Fire("Event.Scene.ChangeFinish", EventManager.defaultParam);
        MenuMgr.Instance.HideHud(false);
        MenuMgr.Instance.HideMenu(false);
        //如果是核心本，获取相关信息
        if (IsInHeXinBen())
        {
            GetEffName();
            GetGroupId();
            Count = 0;
        }
    }
    public int Count = 0;
    public bool IsHeXinBen = false;
    private bool IsInHeXinBen()
    {
        if (!GameGlobal.Instance.netMode)
            return false;

        var mapData = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
        IsHeXinBen = Convert.ToInt32(mapData["type"]) == 14;
        return IsHeXinBen;
    }
    
    public string EffName = "";
    private void GetEffName()
    {
        EffName = DataMgr.Instance.UserData.MapTemplateId == 500100
            ? "/res/effect/ui/ef_ui_trailing_green.assetbundles"
            : "/res/effect/ui/ef_ui_trailing_blue.assetbundles";
    }
    
    public int GroupId = 0;
    private void GetGroupId()
    {
        var mapData = GameUtil.GetDBData2("DailyDungeonInfo","{map_id ="+DataMgr.Instance.UserData.MapTemplateId+"}")[0];
        GroupId = Convert.ToInt32(mapData["group_id"]);
    }

    private void DestoryEffect()
    {
        if (HZUISystem.Instance.GetPickLayer().Transform.childCount > 0)
        {
            var efflist = HZUISystem.Instance.GetPickLayer().Transform.GetComponentsInChildren<FlyToBag>();
            if (efflist == null)
                return;

            foreach (var eff in efflist)
            {
                RenderSystem.Instance.Unload(eff.gameObject);
            }
        }
    }
    
    /// <summary>
    /// 进入场景成功
    /// </summary>
    private void OnEnterSceneSuccess()
    {
        DelayClean();
    }

    #endregion

    public void HideScene(bool isHide)
    {
        if (SceneCamera != null)
        {
            //SceneCamera.enabled = isHide ? false : true;
            //Debugger.LogError("HideSceneSceneCameraCullingMask="+ SceneCameraCullingMask);
            SceneCamera.cullingMask = isHide ? 0 : SceneCameraCullingMask;
            SceneCamera.clearFlags = isHide ? CameraClearFlags.SolidColor : SceneCameraCameraClearFlags;
            //Debugger.LogError("HideScene=" + isHide);
        }
    }

    private Transform GetTransform(GameObject check, string name)
    {
        foreach (Transform t in check.GetComponentsInChildren<Transform>(true))
        {
            if (t.name == name) { return t; }
        }
        return null;
    }

    public void HideNGUI(bool isHide)
    {
        if (UICamera != null)
        {
            //UICamera.enabled = isHide ? false : true;
            UICamera.cullingMask = isHide ? 0 : UICameraCullingMask;
        }
    }

    #region 网络管理.

    public RPGBattleClient mNextClient;

    [DoNotToLua]
    public void InitNetWork()
    {
        TLNetManage.Instance.InitNetWork();
        //战斗服事件必须第一时间监听，否则就会丢包!
        TLNetManage.Instance.OnZoneChanged += new Action<RPGBattleClient>((client) =>
        {
            OnClientEnterZoneNotify(client);
        });

        TLNetManage.Instance.OnBeforeRequest += OnBeforeRequestEvent;
        TLNetManage.Instance.OnRequestStart += OnRequestStartEvent;
        TLNetManage.Instance.OnRequestEnd += OnRequestEndEvent;
        TLNetManage.Instance.AttachObserverGame(this);
        LuaSystem.Instance.DoFunc("GlobalHooks.InitModules");
    }

    public bool OnBeforeRequestEvent(string route, object opt)
    {
        return true;
    }

    private Dictionary<string, float> t = new Dictionary<string, float>();  //debug用

    public void OnRequestStartEvent(string route, object opt)
    {
        //t[route] = Time.realtimeSinceStartup;
        bool isWaiting = true;
        System.Action timeoutCb = null;
        if (opt != null && opt is TLNetManage.PackExtData)
        {
            isWaiting = (opt as TLNetManage.PackExtData).IsWaiting;
            timeoutCb = (opt as TLNetManage.PackExtData).TimeOutCb;
        }
        if (isWaiting)
        {
            GameAlertManager.Instance.ShowLoading(true, true, TLNetManage.WAIT_TIME, () =>
            {
                string tipStr = "timeout";// ConfigMgr.Instance.TxtCfg.GetTextByKey(TextConfig.Type.PUBLICCFG, "timeout");
                if (TLUnityDebug.DEBUG_MODE)
                    tipStr += " route = " + route;
                GameAlertManager.Instance.ShowNotify(tipStr);
                if (timeoutCb != null)
                {
                    timeoutCb();
                }
            });
        }
    }

    public void OnRequestEndEvent(string route, int code, string msg, DeepCore.FuckPomeloClient.PomeloException exp, object opt)
    {
        //Debugger.Log("wwwwwwwwwwwwwwwwwwwwwwwww" + route + ", " + (Time.realtimeSinceStartup - t[route]));
        bool isWaiting = true;
        bool isShowError = true;
        System.Action timeoutCb = null;
        if (opt != null && opt is TLNetManage.PackExtData)
        {
            isWaiting = (opt as TLNetManage.PackExtData).IsWaiting;
            isShowError = (opt as TLNetManage.PackExtData).IsShowError;
            timeoutCb = (opt as TLNetManage.PackExtData).TimeOutCb;
        }
        if (isWaiting && timeoutCb == null)
        {
            GameAlertManager.Instance.ShowLoading(false);
        }
        //if (!string.IsNullOrEmpty(msg))
        //{
        //msg = HZLanguageManager.Instance.GetString(msg);
        //}
        if (code != -1)
        {
            //if (code == Response.CODE_ERROR)    //服务器报错
            //{
            //    string content = "Code: " + code.ToString() + "\n" + msg;
            //    GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM, content, "", null, null);
            //    Debugger.Log(content);
            //}
            if (code != Response.CODE_OK)   //正常错误提示
            {
                if (isShowError)
                {
                    if (!string.IsNullOrEmpty(msg) && !msg.Equals(" "))
                        GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString(msg));
                }
                Debugger.Log("[response] " + route + " " + msg);
            }
        }
        else if (exp != null)
        {
            string content = "route: " + route + "\n" + exp.Message;
            //GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM, content, "", null, null);
            Debugger.LogError(content + "\n" + exp.StackTrace);
        }
    }

    private int mReConnectCount = 0;
    private void ReConnect(object param)
    {
        mReconnecting = true;
        GameAlertManager.Instance.ShowLoading(true, true, 5, () =>
        {
            if (mReConnectCount++ < 6)
            {
                ReConnect(param);
            }
            else
            {
                mReConnectCount = 0;
                GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM + 101,
                    HZLanguageManager.Instance.GetString("common_notconnect_msg"),
                    HZLanguageManager.Instance.GetString("common_connect_again"),
                    HZLanguageManager.Instance.GetString("common_back_to_login"), null, ReConnect, ExitGame);
            }
        });
        LoginController.QuickLogin((ClientEnterGameResponse msg) =>
        {
            GameAlertManager.Instance.ShowLoading(false);
            if (msg != null)
            {
                mReConnectCount = 0;
            }
            else
            {
                if (TLNetManage.Instance.IsGameSocketConnected() || TLNetManage.Instance.IsGateSocketConnected())
                {
                    GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM + 101, HZLanguageManager.Instance.GetString("common_back_login"), "", null, ExitGame);
                }
                else
                {
                    if (mReConnectCount++ < 6)
                    {
                        GameAlertManager.Instance.ShowLoading(true, true);
                        GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(1.0f, () =>
                        {
                            GameAlertManager.Instance.ShowLoading(false);
                            ReConnect(param);
                        }));
                    }
                    else
                    {
                        mReConnectCount = 0;
                        ExitGame(null);
                    }
                }
            }
        });
    }

    public void ExitGame(object param)
    {
        TLNetManage.Instance.Request<ClientExitGameResponse>(new ClientExitGameRequest() { c2s_roleUUID = DataMgr.Instance.UserData.RoleID }, (err, rsp) =>
        {
            GameGlobal.Instance.overlayEffect.FadeOut(0.5f, () =>
            {
                ReleaseAllSceneData(true);
                TLNetManage.Instance.Disconnect();
                EnterLoginScene();
                GameGlobal.Instance.overlayEffect.FadeIn(0.5f);
            });
        });

        //SDK Behavior
        if (param != null && param.Equals("SystemSetting"))
        {
            OneGameSDK.Instance.Logout();
        }
    }

    [DoNotToLua]
    public void OnNetStateChanged(TLNetManage.NetEventData data)
    {
        if (data.Type == TLNetManage.SocketType.GameSocket)
        {
            switch (data.State)
            {
                case TLNetManage.NetWorkState.CONNECTED:
                    OnConnectSuccess();
                    break;
                case TLNetManage.NetWorkState.DISCONNECTED:
                    switch (data.Reason)
                    {
                        case DeepCore.FuckPomeloClient.CloseReason.ClientClose:
                        //case DeepCore.FuckPomeloClient.CloseReason.Error:
                            //do nothing
                            break;
                        case DeepCore.FuckPomeloClient.CloseReason.Disconnect:
                        case DeepCore.FuckPomeloClient.CloseReason.TimeOut:
                            //try to reconnect
                            OnDisConnect(true);
                            break;
                        case DeepCore.FuckPomeloClient.CloseReason.KickByServer:
                            GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM + 101,
                                data.errStr,
                                HZLanguageManager.Instance.GetString("common_back_to_login"),
                                null, ExitGame);
                            break;
                        default:
                            //back to login
                            OnDisConnect(false);
                            break;
                    }
                    break;
                case TLNetManage.NetWorkState.ERROR:
                    //Debugger.LogError(data.Ex.Message + data.Ex.StackTrace);
                    break;
            }
        }
    }

    private void OnConnectSuccess()
    {
        ////初始化其他模块网络监听事件.
        if (!mReconnecting)
        {
            DataMgr.Instance.InitNetWork();
        }

    }

    private void OnDisConnect(bool reconnect)
    {
        if (mLoadingUI != null && mLoadingUI.IsShow())//读条中掉线
        {
            ExitGame(null);
        }
        else if (mLastMapId != 0)//游戏中掉线
        {
            if (reconnect)
                ReConnect(null);
            else
                ExitGame(null);
        }
        else//登录时掉线
        {
            ExitGame(null);
        }
    }

    private bool mServerShutDown, mBackToLogin = false;
    //private void OnKickPlayerPush(KickPlayerPush msg)
    //{
    //    string tips = "pomelo.area.KickPlayerPush:";
    //    switch (msg.s2c_reasonType)
    //    {
    //        case 1: //切图超时强制踢人
    //            tips += "CHANGE_MAP_FAILED";
    //            break;
    //        case 2: //服务器维护
    //            tips += "SERVER_SHUT_DOWN";
    //            string tipStr = ConfigMgr.Instance.TxtCfg.GetTextByKey(TextConfig.Type.PUBLICCFG, "backToTitle");
    //            GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM + 101, tipStr, "", null, ExitGame);
    //            mServerShutDown = true;
    //            break;
    //        case 3: //逻辑错误
    //            tips += "LOGIC_ERROR";
    //            break;
    //        case 4: //GM踢人
    //            tips += "GM_KICK";
    //            mBackToLogin = true;
    //            break;
    //        default:
    //            tips += "UNKNOW";
    //            break;
    //    }
    //    Debugger.Log(tips);
    //}

    #endregion

}
