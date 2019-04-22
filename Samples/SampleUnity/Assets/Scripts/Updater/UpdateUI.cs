using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeepCore.MPQ.Updater;
using System.IO;
using System;

using Assets.Scripts.Setting;
using UnityEngine.Profiling;
using System.Collections.Generic;
using Assets.Scripts;
using DeepCore.Unity3D.Impl;
using DeepCore.MPQ;
using DeepCore.Unity3D;
using DeepCore.IO;
using TLClient.Net;

public class UpdateUI : MonoBehaviour, MPQUpdaterListener
{
    private static UpdateUI mInstance = null;
    public static UpdateUI Instance
    {
        get
        {
            return mInstance;
        }
    }
    private enum LoadStateType
    {
        AndroidCopy,
        Ready,
        Start,
        Finish,
        Over,
    }
    private LoadStateType mLoadState = LoadStateType.Ready;
    private float mLoadStatespeed = 0;

    private MPQUpdater updater;
    public Image bg;
    public Image bg2;
    public Slider ProgressObject;
    public Text DownloadPercent;
    public Text ShowText;
    public Dropdown dpOption;
    public GameObject panelOption;
    public PageView LoadMPQ;

    private GameObject updateObj;
    
    private DirectoryInfo save_root = null;
    private DirectoryInfo bundle_root = null;
    private int firstTime = 0;

    //当前章节
    public static int chapterIndex = 0;
    private string DEFAULTCHAPTER = "DEFAULTCHAPTER";

    public bool isInBattleRun = false;
    void Start ()
    {
        firstTime = PlayerPrefs.GetInt("firstTime", 0);
        if (mInstance == null)
        {
            mInstance = this;
        }

        ProgressObject.value = 0;
        VisibleInfos(false);
        if (!GameGlobal.Instance.useMpq)
            ShowText.text = LocalizedTextManager.Instance.GetText("UPDATE_INITIALIZE");
        Canvas canvas = gameObject.GetComponent<Canvas>();

        if (canvas != null)
        {
            canvas.sortingOrder = -1;
        }

        loadSplash();

        //if (GameGlobal.Instance.useMpq && GameConfig.Instance.options.Count > 1)
        //{
        //    ShowOption();
        //}
        //else
        {
            CheckLogicVersion(); 
        }

        EventManager.Subscribe("Event.LuaSystem.InitComplete", (rsp) =>
        {
            mLuaInitFinish = true;
        });
    }

    void OnEnable()
    {
/*        var texture2d = Resources.Load<Texture2D>("UpdataBG");
        bg.sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);*/
        //调mpq加载优化界面
        LoadMPQ.InitLoadMpq();
    }

    void OnDisable()
    {
        LoadMPQ.DisableLoadMpq();
/*        var bgTexture = bg.sprite.texture;
        UnityEngine.Object.Destroy(bg.sprite);
        bg.sprite = null;
        Resources.UnloadAsset(bgTexture);*/
    }

    void ShowOption()
    {
        panelOption.SetActive(true);

        List<string> options = new List<string>();
        foreach (var k in GameConfig.Instance.options)
        {
            Dictionary<string, object> config = k as Dictionary<string, object>;
            options.Add(config["name"] as string);
        }

        dpOption.AddOptions(options);
        dpOption.transform.parent.gameObject.SetActive(true);
        dpOption.value = GameConfig.Instance.selectedOption;
    }

    private void loadSplash()
    {
        //var path = Application.streamingAssetsPath + "/logo.png";
        //byte[] bytes = Resource.LoadData(path);
        //var mainTexture = new Texture2D(8, 8, TextureFormat.RGBA32, false);
        //mainTexture.LoadImage(bytes);
        //Rect rec = new Rect(0, 0, mainTexture.width, mainTexture.height);
        //bg2.sprite = Sprite.Create(mainTexture, rec, new Vector2(0, 0), 1);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mLoadState)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                case LoadStateType.AndroidCopy:
                    updateAndroidCopy();
                    break;
#endif
            case LoadStateType.Ready:
                updateReady();
                break;
            case LoadStateType.Finish:
                if(mLuaInitFinish)
                    LoadNextScene();
                break;
            case LoadStateType.Over:
                break;

        }
    }

    //需要检查更新的地方调用此方法
    public void StartUpdate()
    {
        mLoadState = LoadStateType.Ready;
        gameObject.SetActive(true);
        //if (ShitNotice.Instance.IsOpen)
        //{
        //    ShitNotice.Instance.Show(ConfigMgr.Instance.GameCfg.GetTextByKey(GameConfig.Type.ShitNotice, "CONTENT"),
        //    () => { CheckLogicVersion(); },
        //
        //    () => { Application.Quit(); });
        //}
        //else
        //{
        //    CheckLogicVersion();
        //}
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void updateAndroidCopy()
    {
        if (ProgressObject == null)
            return;
        long current = 0;
        long total = 0;
        float progress = 0;
        
        if (FileUtils.GetProgress(out current, out total, out progress))
        {
            
            ProgressObject.value = 1;
            mLoadState = LoadStateType.Ready;
            if (firstTime == 0)
            {
                PlayerPrefs.SetInt("firstTime", 1);
            }

            
            Start_Updater(ProjectSetting.UPDATE_URL);
            
        }
        else
        {
            if (firstTime == 0)
            {
                ShowText.text = LocalizedTextManager.Instance.GetText("FIRST_EXTRACT");
            }
            else
            {
                 ShowText.text = LocalizedTextManager.Instance.GetText("MPQ_EXTRACT");
            }
            
            mLoadStatespeed = 0.001f;

            ProgressObject.value += mLoadStatespeed;
            if (ProgressObject.value > progress + 0.1f)
            {
                ProgressObject.value = progress + 0.1f;
            }
            if (progress != 1 && ProgressObject.value > 0.95f)
            {
                ProgressObject.value = 0.95f;
            }
            if (ProgressObject.value < progress)
            {
                ProgressObject.value = progress;
            }
            ProgressObject.value = Math.Min(ProgressObject.value, 1);
        }
    }
#endif

    public void OnOptionOK()
    {
        panelOption.SetActive(false);
        GameConfig.Instance.ChangegOption(dpOption.value);
        CheckLogicVersion();
    }

    /// <summary>
    /// 初始化UI分辨率适配参数
    /// </summary>
    private void InitUIScreen()
    {
        GameSceneMgr.Instance.UGUI.ResetScreenOffset();
        HZUISystem.Instance.ResetScreenOffset();
    }

    public void InitMPQ()
    {
        //选一个合适的地方初始化UI，不能太早也不能太晚
        InitUIScreen();

#if UNITY_ANDROID

        if (!GameGlobal.Instance.useMpq)
        {
            ReadyToLoadNextScene();
        }
        else
        {
#if UNITY_EDITOR
            Start_Updater(ProjectSetting.UPDATE_URL);
#else
        UpdateUtil.CopyAndroidLocalMPQ(txt =>
        {
            Debugger.LogWarning("CopyAndroidLocalMPQtxt===" + txt);
            if (txt == "NoCopy")
            {
                mLoadState = LoadStateType.Ready;
                Start_Updater(ProjectSetting.UPDATE_URL);
            }
            else
            {
                mLoadState = LoadStateType.AndroidCopy;
            }
        }
        );
#endif
        }
#else
        if (!GameGlobal.Instance.useMpq)
        {
            ReadyToLoadNextScene();
        }
        else
        {
           Start_Updater(ProjectSetting.UPDATE_URL);
        }
#endif

        //自定义事件开始更新
        if (!PlayerPrefs.HasKey(SDKAttName.CUSTOM_EVENT_ONE_UPDATE_START))
        {
            var eEvent = new SDKBaseData();
            eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_ONE_UPDATE_START);
            OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
            PlayerPrefs.SetInt(SDKAttName.CUSTOM_EVENT_ONE_UPDATE_START, 1);
        }
    }

    private void OnUpdateAccept(object url)
    {
        PlatformMgr.DoUpdate(url.ToString());
    }

    private void CheckLogicVersion()
    {
        if (!GameGlobal.Instance.netMode)
        {
            InitMPQ();
            return;
        }
        
        Debug.Log("CheckLogicVersion");
        //检测是否需要更新版本
        UpdateUtil.CheckVersion((HttpRequest.ResultType msgType, UpdateVersionMessage msg) =>
        {
            if (msgType == HttpRequest.ResultType.Success)
            {
                if (msg != null)
                {
                    if (msg.status == 1)
                    {
                        //CDN資源地址
                        if (!string.IsNullOrEmpty(msg.cdn_url))
                        {
                            ProjectSetting.UPDATE_URL = msg.cdn_url;
                        }

                        //TODO 分段下载
                        if (msg.res_type == 1)
                        {
                            PlayerPrefs.SetInt("chapterIndex", -1);
                        }
                        //TODO 维护公告
                        if (msg.repair_notice_state == 1)
                        {
                            LoginNoticeMenu.autoShowNotice = true;
                        }
                        LoginNoticeMenu.notices = new List<LoginNoticeMenu.NoticeType>();
                        if (msg.repair_content != null)
                        {
                            foreach (var item in msg.repair_content)
                                LoginNoticeMenu.notices.Add(new LoginNoticeMenu.NoticeType(item.title,item.content));
                        }
                        
                        //TODO 系统公告
                        if (msg.sys_notice_state == 1)
                        {

                        }
                        //TODO MPQ更新公告
                        if (msg.mpq_notice_state == 1)
                        {

                        }
                        ShowText.text = LocalizedTextManager.Instance.GetText("UPDATE_CHECKUPDATE_COMPLETE");
                        ProgressObject.value = 1;

                        if (msg.update_type == 0)
                        {
                            InitMPQ();
                        }
                        else if(msg.update_type == 1) //非强制更新
                            PanelDialog.Create(LocalizedTextManager.Instance.GetText("VERSION_UPDATE"), LocalizedTextManager.Instance.GetText("CHECK_VERSION_UPDATE"), 
                                () => { PlatformMgr.DoUpdate(msg.update_url); },
                                () => { InitMPQ(); }
                                );
                        else//强更
                            PanelDialog.Create(LocalizedTextManager.Instance.GetText("VERSION_UPDATE"), LocalizedTextManager.Instance.GetText("CHECK_VERSION_FORCEUPDATE"), () => { PlatformMgr.DoUpdate(msg.update_url); });
                    }
                    else
                    {
                        var text = LocalizedTextManager.Instance.GetText("CHECK_UPDATE_FAILED");
                        ShowText.text = text +" "+msg.message;
                        PanelDialog.Create("", LocalizedTextManager.Instance.GetText("CHECK_UPDATE_FAILED") + "\n" + msg.message, () => { Application.Quit(); });
                    }
                }
                else
                {
                    var text = LocalizedTextManager.Instance.GetText("CHECK_UPDATE_FAILED");
                    ShowText.text = text;
                    PanelDialog.Create("", LocalizedTextManager.Instance.GetText("CHECK_UPDATE_TRY_AGAIN"), () => { CheckLogicVersion(); });
                }
            }
            else
            {
                string showMessage = string.Empty;
                if (msgType == HttpRequest.ResultType.Error)
                {
                    showMessage = LocalizedTextManager.Instance.GetText("CHECK_UPDATE_FAILED_NETWORK_ERROR");
                }
                else if (msgType == HttpRequest.ResultType.TimeOut)
                {
                    showMessage = LocalizedTextManager.Instance.GetText("CHECK_UPDATE_FAILED_TIMEOUT"); 
                }
                PanelDialog.Create(LocalizedTextManager.Instance.GetText("VERSION_UPDATE"), showMessage, () => { CheckLogicVersion(); });
            }
        });
    }

    private bool initPath()
    {
        try
        {
            if (save_root == null)
            {
                save_root = new DirectoryInfo(ProjectSetting.MPQSavedPath);
            }
            if (bundle_root == null)
            {
                bundle_root = new DirectoryInfo(ProjectSetting.MPQLocalPath);
            }

            if (!save_root.Exists)
                save_root.Create();

            //if (!bundle_root.Exists)
            //    bundle_root.Create();
            return true;
        }
        catch (Exception err)
        {
            Debugger.LogException(err);
        }
        return false;
    }

    private void DisposeUpdater()
    {
        if (updater != null)
        {
            updater.Dispose();
            updater = null;
        }
    }

    private bool CheckChapterNeedUpdate(int chapter)
    {
        bool flag = false;
        if (chapter == -1 && chapterIndex != chapter)
        {
            flag = true;
        }
        else if (chapterIndex != -1 && chapterIndex < chapter)
        {
            flag = true;
        }
        return flag;
    }

    public void Start_Updater(string remote)
    {
        try
        {
            save_root = new DirectoryInfo(ProjectSetting.MPQSavedPath);
            bundle_root = new DirectoryInfo(ProjectSetting.MPQLocalPath);

            Debugger.Log("save_root " + save_root.FullName);
            Debugger.Log("bundle_root " + bundle_root.FullName);

            if (!save_root.Exists)
                save_root.Create();

            //if (!bundle_root.Exists)
            //    bundle_root.Create();
        }
        catch (Exception err)
        {
            //PanelDialog.Create("", "<size=22><color=white>" + ProjectSetting.StorageFolder + HZLanguageManager.Instance.GetString("FILESYSTEM_READ_ERROR") + "\n" + err.Message + "</color></size>", () => { StartUpdate(); });
            Debugger.LogException(err);
            return;
        }
        try
        {
            DisposeUpdater();
            string chapterStr = (string)GameConfig.Instance.Get(DEFAULTCHAPTER);
            int chapterInt = int.Parse(chapterStr);
            chapterIndex = PlayerPrefs.GetInt("chapterIndex", chapterInt);
            Debugger.Log("======init chapterIndex======="+ chapterIndex + "  " + chapterStr);
            //if (chapterIndex != -1 && PlatformMgr.PluginGetNetworkStatus().Equals(PlatformMgr.WIFI))
            //{
            //    chapterIndex = -1;
            //    PlayerPrefs.SetInt("chapterIndex", chapterIndex);
            //}
            
            updater = UnityDriver.CreateMPQUpdater(new Uri(new Uri(ProjectSetting.UPDATE_URL), ProjectSetting.UPDATE_FILE_NAME + "?timestamp=" + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds),
                                     // auto update remote root url
                                     new string[] { remote },
                                     // auto update file name
                                     ProjectSetting.UPDATE_FILE_NAME,
                                     save_root,
                                     bundle_root,
                                     false,
                                     //chapterIndex,
                                     this);

			GameGlobal.Instance.StartCoroutine(UpdateUtil.GetWWWTxt(new Uri(new Uri(ProjectSetting.UPDATE_URL), ProjectSetting.UPDATE_FILE_NAME).ToString(), (www) =>
            {
                if (!string.IsNullOrEmpty(www.text))
                {
                    var offset = www.text.LastIndexOf(".mpq");
                    if(offset >= 0)
                    {
                        ProjectSetting.UPDATE_RES_VERSION = www.text.Substring(www.text.LastIndexOf(".mpq") - 6, 6);
                    }
                }
            }));
            
            //验证需要下载的大小
            long needDownload = CheckNeedUpdate(ProjectSetting.UPDATE_URL);
            float downLoadSize = (float)needDownload / (float)1048576;
            //Debugger.LogError("downLoadSize=" + downLoadSize);
            if (downLoadSize > 0)
            {
                string text = LocalizedTextManager.Instance.GetText("CURRENT_DOWNLOAD");
                string networkStatus = PlatformMgr.PluginGetNetworkStatus();
                string str = string.Format(text, networkStatus, downLoadSize, "M");
                
                if (downLoadSize < 1)
                {
                    downLoadSize = (float)needDownload / (float)1024;
                    str = string.Format(text, networkStatus, downLoadSize, "K");
                }
                PanelDialog.Create(LocalizedTextManager.Instance.GetText("MPQ_UPDATE"), str,
                () =>
                {
                    updater.Start();
                },
                () =>
                {

                    Application.Quit();
                });
            }
            else
            {
                updater.Start();
            }
        }
        catch (Exception e)
        {
            if (!GameGlobal.Instance.useMpq)
                PanelDialog.Create(LocalizedTextManager.Instance.GetText("MPQ_UPDATE"), LocalizedTextManager.Instance.GetText("MPQ_NETWORK_ERROR") + "\n" + e.Message, () => { StartUpdate(); });
            Debugger.LogException(e);
        }

        Debugger.Log("UpdateUI.Start_Updater " + updater + remote + "" + ProjectSetting.UPDATE_FILE_NAME + "?timestamp=" + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
    }

    public long CheckNeedUpdate(string version_url)
    {
        System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
        _watch.Start();
        Debugger.Log("========CheckNeedUpdate========" + chapterIndex);
        MPQUpdater.UpdateInfo info = MPQUpdater.CheckNeedUpdate(new Uri(new Uri(version_url) + ProjectSetting.UPDATE_FILE_NAME + "?timestamp=" + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds), ProjectSetting.UPDATE_FILE_NAME, save_root, bundle_root,(action)=> { }
        ,(remoteFile) =>
        {
            //全资源状态全下
            if (chapterIndex < 0)
            {
                return true;
            }
            //章节状态只下<=本章节的资源
            int curIndex;
            return !string.IsNullOrEmpty(remoteFile.userdata) &&
                   int.TryParse(remoteFile.userdata, out curIndex) &&
                   curIndex <= chapterIndex;
        });
        _watch.Stop();
        Debugger.Log("CheckNeedUpdate Time===========" + _watch.ElapsedMilliseconds / 1000f);
        return info.NeedDownload;
    }

    public int mMPQUpdaterEvent = 0;
    private int ReupdateCount = 0;
    public void onEvent(MPQUpdater updater, MPQUpdaterEvent e)
    {
        Debugger.Log("UpdateUI.onEvent " + updater + " " + e.EventType.ToString() + " " + updater.UrlRoot + updater.CurrentDownloadFile);
        mMPQUpdaterEvent = e.EventType;
        switch (e.EventType)
        {
            case MPQUpdaterEvent.TYPE_COMPLETE:
                InitMPQFileSystem(updater);
                break;
            case MPQUpdaterEvent.TYPE_ERROR:
                if (ReupdateCount < 10)
                {
                    ReUpdate();
                    ReupdateCount++;
                }
                else
                {
                    PanelDialog.Create(LocalizedTextManager.Instance.GetText("MPQ_UPDATE"), LocalizedTextManager.Instance.GetText("MPQ_ERROR"),
                        () => { ReupdateCount = 0; ReUpdate(); },
                        () => { Application.Quit(); });
                    ShowText.text = LocalizedTextManager.Instance.GetText("MPQ_ERROR");
                }
                break;

            case MPQUpdaterEvent.TYPE_NOT_ENOUGH_SPACE:
                PanelDialog.Create(LocalizedTextManager.Instance.GetText("MPQ_UPDATE"), LocalizedTextManager.Instance.GetText("MPQ_SPACE_NOT_ENOUGH"),
                    () => { Application.Quit(); });
                break;
            case MPQUpdaterEvent.TYPE_VALIDATING:
                break;
            case MPQUpdaterEvent.TYPE_DOWNLOADING:
                if (!ProgressObject.gameObject.activeSelf)
                {
                    VisibleInfos(true);

                }
                if (ShowText != null)
                {
                    ShowText.text = LocalizedTextManager.Instance.GetText("DOWNLOADING2");
                }

                break;
            case MPQUpdaterEvent.TYPE_UNZIP:
                if (ShowText != null)
                {
                    ShowText.text = LocalizedTextManager.Instance.GetText("MPQ_EXTRACT");
                    ProgressObject.value = 1;
                    Debugger.Log("TYPE_UNZIPProgressObject.value=" + ProgressObject.value);
                    //ProgressObject.transform.Find("up").gameObject.SetActive(true);
                }
                break;
            default:
                break;

        }
    }

    private void InitMPQFileSystem(MPQUpdater updater)
    {
        //自定义事件更新完成
        if (!PlayerPrefs.HasKey(SDKAttName.CUSTOM_EVENT_ONE_UPDATE_COMPLETE))
        {
            var eEvent = new SDKBaseData();
            eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_ONE_UPDATE_COMPLETE);
            OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
            PlayerPrefs.SetInt(SDKAttName.CUSTOM_EVENT_ONE_UPDATE_COMPLETE, 1);
        }

        //自定义事件开始加载
        if (!PlayerPrefs.HasKey(SDKAttName.CUSTOM_EVENT_ONE_LOAD_START))
        {
            var eEvent = new SDKBaseData();
            eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_ONE_LOAD_START);
            OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
            PlayerPrefs.SetInt(SDKAttName.CUSTOM_EVENT_ONE_LOAD_START, 1);
        }

        ShowText.text = LocalizedTextManager.Instance.GetText("MPQ_LOADING");

        //// 加载MPQ
        //var updateDir = new DirectoryInfo(ProjectSetting.MPQSavedPath);
        //var bundleDir = new DirectoryInfo(ProjectSetting.MPQLocalPath);

        MPQFileSystem mpq = new MPQFileSystem();
        mpq.init(updater);
        
        UnityDriver.AddFileSystem(mpq);

        // Assetbundle资源索引
        //Dictionary<string, string> mapper = new Dictionary<string, string>();
        //foreach (var dir in updateDir.GetDirectories())
        //{
        //    Profiler.BeginSample(dir.Name);
        //    scanResDir(dir.FullName, "/", mapper);
        //    Profiler.EndSample();
        //}

        //HZUnityABLoadAdapter.SetPathRemapper((root, res) =>
        //{
        //    string uri;
        //    if (mapper.TryGetValue(res.ToLower(), out uri))
        //    {
        //        return uri;
        //    }
        //    return null;
        //});

        ReadyToLoadNextScene();

        
    }

    private void scanResDir(string root, string relativeDir, Dictionary<string, string> mapper)
    {
        DirectoryInfo dir = new DirectoryInfo(root + relativeDir);
        foreach (FileInfo file in dir.GetFiles())
        {
            if (!file.Name.EndsWith(".mpq") && !file.Name.EndsWith(".z") && !file.Name.EndsWith(".txt"))
            {
                var uri = new Uri(file.FullName);
                mapper.Add((relativeDir + file.Name).ToLower(), uri.AbsoluteUri);
            }
        }
        foreach (DirectoryInfo sub_dir in dir.GetDirectories())
        {
            scanResDir(root, relativeDir + sub_dir.Name + "/", mapper);
        }
    }

    private void ReUpdate()
    {
        if (!initPath())
        {
            return;
        }
        try
        {
            DisposeUpdater();
            string chapterStr = (string)GameConfig.Instance.Get(DEFAULTCHAPTER);
            int chapterInt = int.Parse(chapterStr);
            chapterIndex = PlayerPrefs.GetInt("chapterIndex", chapterInt);
            updater = UnityDriver.CreateMPQUpdater(new Uri(new Uri(ProjectSetting.UPDATE_URL) + ProjectSetting.UPDATE_FILE_NAME + "?timestamp=" + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds),
                                     // auto update remote root url
                                     new string[] { ProjectSetting.UPDATE_URL },
                                     // auto update file name
                                     ProjectSetting.UPDATE_FILE_NAME,
                                     save_root,
                                     bundle_root,
                                     false,
                                     //chapterIndex,
                                     this);
            updater.Start();
        }
        catch (Exception e)
        {
            Debugger.LogException(e);
        }
    }

    private void VisibleInfos(bool var)
    {
        if (ProgressObject != null ||
           DownloadPercent != null)
        {
            return;
        }
        if (var)
        {
            ProgressObject.gameObject.SetActive(true);
            DownloadPercent.gameObject.SetActive(true);
            ShowText.gameObject.SetActive(true);
        }
        else
        {
            ProgressObject.gameObject.SetActive(false);
            DownloadPercent.gameObject.SetActive(false);
            ShowText.gameObject.SetActive(false);
        }
    }

    private void UpdateShowUnZip()
    {
        float var = (float)updater.CurrentUnzipBytes / (float)updater.TotalUnzipBytes;
        if (ProgressObject != null)
        {
            ProgressObject.value = var;
        }
        int per = System.Convert.ToInt32(var * 100);

        ShowText.text = LocalizedTextManager.Instance.GetText("EXTRACTING");

        if (DownloadPercent != null)
        {
            DownloadPercent.text = per.ToString() + "%";
        }
    }

    private void UpdateShowInfos()
    {
        float var = (float)updater.CurrentDownloadBytes / (float)updater.TotalDownloadBytes;
        if (ProgressObject != null)
        {
            ProgressObject.value = var;
        }
        if (DownloadPercent != null)
        {
            int per = System.Convert.ToInt32(var * 100);
            DownloadPercent.text = per.ToString() + "%";
        }

        if (ShowText != null)
        {
            string t2 = string.Format("{0:0.00}/{1:0.00}MB", (float)updater.CurrentDownloadBytes / (float)1048576, (float)updater.TotalDownloadBytes / (float)1048576);
            ShowText.text = LocalizedTextManager.Instance.GetText("DOWNLOADING2") + t2;

        }
    }

    private void updateReady()
    {
        if (updater == null)
        {
            return;
        }
        if (mMPQUpdaterEvent == MPQUpdaterEvent.TYPE_DOWNLOADING)
        {
            UpdateShowInfos();
        }
        else if (mMPQUpdaterEvent == MPQUpdaterEvent.TYPE_UNZIP)
        {
            UpdateShowUnZip();
        }
        updater.Update();
        //DeepCore.Unity3D.UnityHelper.DestroyImmediate(bg2.sprite.texture, true);
    }
    
    private bool mLuaInitFinish = false;

    private void ReadyToLoadNextScene()
    {
        GameGlobal.Instance.MPQCallBack();
        mLoadState = LoadStateType.Finish;

        //自定义事件加载完成
        if (!PlayerPrefs.HasKey(SDKAttName.CUSTOM_EVENT_ONE_LOAD_COMPLETE))
        {
            var eEvent = new SDKBaseData();
            eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_ONE_LOAD_COMPLETE);
            OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
            PlayerPrefs.SetInt(SDKAttName.CUSTOM_EVENT_ONE_LOAD_COMPLETE, 1);
        }
    }

    private void LoadNextScene()
    {
        mLoadState = LoadStateType.Over;

        //if (DownloadPercent != null)
        //{
        //    DownloadPercent.text = "";
        //}

        //if (ProgressObject != null)
        //{
        //    ProgressObject.value = 0;
        //}

        GameGlobal.Instance.overlayEffect.FadeOut(0.5f, () =>
        {
            if (GameGlobal.Instance.netMode)
            {
                GameSceneMgr.Instance.EnterLoginScene();
            }
            else
            {
                GameGlobal.Instance.overlayEffect.FadeIn(0.5f);
                GameSceneMgr.Instance.EnterGameScene();
            }

            gameObject.SetActive(false);
        });
    }
}
