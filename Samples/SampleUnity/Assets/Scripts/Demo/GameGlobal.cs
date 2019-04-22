

using CmdEditor;



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Setting;
using DeepCore.Log;
using DeepCore.Unity3D.Impl;
using TLBattle.Client;
using DeepCore.Unity3D;
using System.IO;
using DeepCore.GameData.RTS;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameEvent;
using DeepCore.GameEvent.Lua;
using DeepCore.IO;
using DeepCore.Template.SLua;
using DeepMMO.Client;
using Localized;
using ThreeLives.Client.Unity3D;
#if UNITY_STANDALONE_WIN
using TLBattle.Server;
#endif
using TLClient.Net;

public class GameGlobal : MonoBehaviour
{
    private static GameGlobal _instance;

    public bool netMode;
    public bool useMpq;
    //public bool localMpq;
    public int SceneID = 2069869533;
    public int ActorTemplateID = 10001;

    public GameLoadScene loadingUI;
    public GameLoadScene loadingUI2;
    public GameLoadScene loadingUI3;
    public OverlayEffect overlayEffect;
    public string language = "zh_CN";


    public bool UseCache = true;
    private bool mLastUseCache = true;


    public ShaderVariantCollection mSvc;

    public List<Shader> mShaderList;

    //资源加载原文件类型
    public enum LoaderSourceType
    {
        //从本地SVN读取测试资源
        LocalFileSystem,

        //从资源更新目录读取资源
        DownloadedAssets
    }

    public string LuaRootPath
    {
        get { return TLDataPathHelper.CLIENT_SCRIPT_ROOT; }
    }

    private static LuaEventManager CreatClientEventManager(string mgrName, string id)
    {
        return new LuaEventManager(mgrName, id, new SLuaAdapter())
        {
            RootPath = "",
            Config = "event_script/ClientConfig",
            CustomMainLua = "event_script/Main.lua"
        };
    }

    void Awake()
    {
        _instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        EventManagerFactory.Instance.RegisterName(EventModule.ClientManagerName, CreatClientEventManager);
        UnityHelper.Init();
        UnityDriver.SetUnityDriver(new MyUnityDriver());
        UnityDriver.SetDirver();
        HZUnityLoadImlFactory.SetFactory(new HZDSBHZUnityLoadImlFactory());
        LoggerFactory.SetFactory(new TLUnityLogFactory());
        new TLClientZoneFactory();
       
        if(!netMode){
#if UNITY_STANDALONE_WIN
            new TLServerZoneFactory();
            new TLUnityDataLocalFactory();
#endif
        }
        else
        {
            new TLUnityDataFactory();
        }

        new TLClient.TLClientBattleManager();
//#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && !UNITY_ANDROID && !UNITY_IOS
        //if (netMode)
        //{
        //    new TLBattle.Common.TLDataFactory();
        //}
        //else
        //{
        //    //客户端编辑器单机模式需要跑服务端模块.
        //    new TLServerZoneFactory();
        //}
//#endif
        //      new TLClient.TLClientBattleManager();

        ProjectSetting.Init();
        LocalizedTextManager.Instance.Init();
        //GetSet();
    }
#if UNITY_STANDALONE_WIN
    class TLUnityDataLocalFactory : TLServerDataFactory
    {
        //public override RTSAstar CreateAstarTerrain(object owner, ZoneInfo mesh, int spaceDiv, EditorTemplates data_root)
        //{
        //    return new CommonAI.RTS.Manhattan.ZoneManhattanAstar(mesh, data_root.Templates.TerrainDefinition, true, 0, TemplateManager.IsEditor);
        //}
        public TLUnityDataLocalFactory() : base()
        {
            TemplateManager.IsEditor = true;
        }
            
    }
#endif

    class TLUnityDataFactory : TLBattle.Common.TLDataFactory
    {
        //public override RTSAstar CreateAstarTerrain(object owner, ZoneInfo mesh, int spaceDiv, EditorTemplates data_root)
        //{
        //    return base.CreateAstarTerrain(owner, mesh, 0, data_root);
        //}
    }

    void Start()
    {
        using (var mem = new MemoryStream())
        {
            var output = new OutputStream(mem, null);
            {
                output.PutS32(1024);
                output.PutS32(-512);
                output.PutU32(uint.MaxValue);
                output.PutU32(512);
                output.PutVS64(-164332);
                output.PutUTF("凸- -凸");
            }
            mem.Position = 0;
            var input = new InputStream(mem, null);
            {
                var num = input.GetS32();
                var num2 = input.GetS32();
                var unum = input.GetU32();
                var unum2 = input.GetU32();
                var vs64 = input.GetVS64();
                var txt = input.GetUTF();

                Debugger.Log(txt);
            }
        }

        int defaultValue = UnityEngine.EventSystems.EventSystem.current.pixelDragThreshold;
        UnityEngine.EventSystems.EventSystem.current.pixelDragThreshold =
                Mathf.Max(
                     defaultValue,
                     (int)(defaultValue * Screen.dpi / 160f));

        LuaSystem.Instance.OnInitComplete = () =>
        {
            new DeepCore.Lua.LuaTemplateLoader(new DeepCore.Template.SLua.SLuaAdapter());
            EventManager.Fire(GameEvent.SYS_GAME_START, EventManager.defaultParam);
            TLClient.TLClientBattleManager.Instance.Init(ProjectSetting.GAME_EDITOR_DATA_ROOT);
            //协议多语言
            new TLMessageCodeManager(TLClient.TLNetClient.TLNetCodec);
        };
        this.gameObject.AddComponent<PlatformMgr>();
        this.gameObject.AddComponent<FPSMono>();

    }


    private void GameGlobal_OnShaderListLoadOverCallBack(ShaderVariantCollection svc)
    {
        mSvc = svc;
        mShaderList = shaderlist;
    }

    public static GameGlobal Instance { get { return _instance; } }

    private FingerGesturesCtrl mFGCtrl = null;

    public FingerGesturesCtrl FGCtrl
    {
        get
        {
            if (mFGCtrl == null)
            {
                mFGCtrl = gameObject.AddComponent<FingerGesturesCtrl>();
            }
            return mFGCtrl;
        }
    }

    private List<Shader> shaderlist
    {
        get
        {
            return HZUnityAssetBundleManager.GetInstance().ShaderList;
        }
    }
    public Shader getShader(string shadername)
    {
        if (shaderlist.Count == 0)
        {
            return Shader.Find("MFUGUI/Image");
        }
        foreach (Shader _shader in shaderlist)
        {
            if (_shader.name.Equals(shadername))
            {
                return _shader;
            }
        }
        return Shader.Find("MFUGUI/Image");
    }

    public static IEnumerator WaitForSeconds(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        if (callback != null)
            callback();
    }

    public static IEnumerator WaitForFrame(int frame, System.Action callback)
    {
        while (frame > 0)
        {
            frame--;
            yield return 1;
        }
        if (callback != null)
            callback();
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        TLNetManage.Instance.Update(deltaTime);
        GameAlertManager.Instance.Update(deltaTime);
        DataMgr.Instance.Update(deltaTime);
        TLBattleFactory.Instance.Update(Time.deltaTime);
        if (mLastUseCache != UseCache)
        {
            mLastUseCache = UseCache;
            var gameObjectCache = UnityObjectCacheCenter.GetTypeCache<FuckAssetObject>();
            gameObjectCache.UseCache = UseCache;
            var audioCache = UnityObjectCacheCenter.GetTypeCache<AssetAudio>();
            audioCache.UseCache = UseCache;
            var audioClipCache = UnityObjectCacheCenter.GetTypeCache<AudioClip>();
            audioClipCache.UseCache = UseCache;
            var assetGameCache = UnityObjectCacheCenter.GetTypeCache<AssetGameObject>();
            assetGameCache.UseCache = UseCache;
        }
        //CheckMemory(deltaTime);
#if UNITY_ANDROID
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (OneGameSDK.Instance.GetPlatformData().GetBool(SDKAttName.IS_SUPPORT_EXIT))
            {
                EventManager.Fire("Event.System.Back");
            }
        }
#endif
    }


    public void ClearCache()
    {
        var gameObjectCache = UnityObjectCacheCenter.GetTypeCache<FuckAssetObject>();
        gameObjectCache.Clear();
        var audioCache = UnityObjectCacheCenter.GetTypeCache<AssetAudio>();
        audioCache.Clear();
        var audioClipCache = UnityObjectCacheCenter.GetTypeCache<AudioClip>();
        audioClipCache.Clear();
        var assetGameCache = UnityObjectCacheCenter.GetTypeCache<AssetGameObject>();
        assetGameCache.Clear();
    }

    void OnDestroy()
    {
        SoundPlayer.OnSoundEffectHandler = null;
        SoundPlayer.On3DSoundEffectHandler = null;
        SoundPlayer.OnSoundBGMHandler = null;
        TLNetManage.Instance.Disconnect();
    }

    public void MPQCallBack()
    {
        InitHZUnityLoadCompment();
        InitOthers();
    }

    //private void InitLanguage(string language, string relativeDir)
    //{
    //    Properties lang = new Properties();
    //    string path = Assets.Scripts.Setting.ProjectSetting.LangPath;
    //    var listmap = Properties.LoadFromResource(path + relativeDir + "list.txt");
    //    if (listmap != null)
    //    {
    //        foreach (var elem in listmap)
    //        {
    //            var tmp = Properties.LoadFromResource(path + relativeDir + elem.Key);
    //            if (tmp != null)
    //            {
    //                lang.AddAll(tmp);
    //            }
    //        }
    //    }
    //    HZLanguageManager.Instance.AddLanguage(language, lang);
    //}

    private void InitOthers()
    {
        //初始化多国语言
        HZLanguageManager.Instance.InitLanguage(language);
        LuaSystem.Instance.Start();
        
        if (netMode)
        {
            new TLClient.TLClientTemplateManager();
        }
        
        SoundPlayer.OnSoundEffectHandler = PlayerEffectSound;
        SoundPlayer.OnSoundBGMHandler = PlayerBGMSound;
        SoundPlayer.On3DSoundEffectHandler = On3DSoundEffectHandler;
        SoundPlayer.OnStopSoundHandler = OnStopSoundHandler;
		  //初始化音量
        int ismusic = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.ISMUSIC);
        int music = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.MUSIC);
        int isaudio = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.ISAUDIO);
        int audio = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.AUDIO);
        SoundManager.Instance.DefaultBGMVolume = ismusic == 1 ? (float)music / 100 : 0f;
        SoundManager.Instance.DefaultSoundVolume = isaudio == 1 ? (float)audio / 100 : 0f;
    }

    private void OnStopSoundHandler(int id)
    {
        SoundManager.Instance.StopSound(id);
    }

    private int On3DSoundEffectHandler(string bundlename, Transform parent, float volume, float mindistance, float maxdistance, bool loop)
    {
        volume = Mathf.Min(volume, SoundManager.Instance.DefaultSoundVolume);
        return SoundManager.Instance.PlaySound(bundlename,0, parent, volume,mindistance,maxdistance, loop,AssetAudio.AudioType.Scene);
    }

    private void PlayerBGMSound(string bundleName, float volume)
    {
        SoundManager.Instance.PlayBGM(bundleName, volume);
    }

    private int PlayerEffectSound(string bundleName, Transform parent, float volume, bool loop)
    {
        volume = Mathf.Min(volume, SoundManager.Instance.DefaultSoundVolume);
        return SoundManager.Instance.PlaySound(bundleName, parent, volume, loop);
    }

/// <summary>
/// 游戏3d资源加载组件.
/// </summary>
private void InitHZUnityLoadCompment()
    {
        HZUnityAssetBundleLoader.DefaultLoadType = (HZUnityAssetBundleLoader.HZUnityLoadType.WWW);
        HZUnityAssetBundleLoader.UNITY_RES_SUFFIXS = "assetbundles;unity3d";

#if UNITY_STANDALONE || UNITY_EDITOR

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            string resPath = null;
            string editorPath = null;
            Debugger.Log("Game Start");
            Debugger.Log("-------------------- CommandLine --------------------");
            Debugger.Log(CommandLineReader.GetCommandLine());

            if (!netMode && CommandLineReader.GetCommandLineArgs().Length > 1)
            {
                Assets.Scripts.Setting.ProjectSetting.CommandMode = true;
                editorPath = CommandLineReader.GetCustomArgument("FolderPath");

                if (!string.IsNullOrEmpty(editorPath))
                {
                    //GameDataPath = editorPath.Replace("\\", "/");
                    Debugger.Log(" DataPath: " + editorPath);
                }

                resPath = CommandLineReader.GetCustomArgument("ResPath");
                if (!string.IsNullOrEmpty(resPath))
                {
                    //AssetBundleRoot = resPath.Replace("\\", "/");
                    Debugger.Log(" DataPath: " + resPath);
                }

                SceneID = int.Parse(CommandLineReader.GetCustomArgument("MapID"));
                Debugger.Log("MapID: " + SceneID);

                ActorTemplateID = int.Parse(CommandLineReader.GetCustomArgument("UserID"));
                Debugger.Log("UserID: " + ActorTemplateID);
                Debug.LogError("-------------------------MapID: " + SceneID);
                Debug.LogError("-------------------------UserID: " + ActorTemplateID);
            }
        }

        //UnityDriver.SetTestDataPath(ProjectSetting.UIEditorPath);

#endif

        //TODO  ------这个AB加载路径设置还是有奇怪
#if UNITY_ANDROID && !UNITY_EDITOR
       HZUnityABLoadAdapter.SetLoadRootPath(ProjectSetting.GameEditorABPath);
        //else
        //{
        //    HZUnityAssetBundleManager.OnShaderListLoadOverCallBack = GameGlobal_OnShaderListLoadOverCallBack;
        //}
#else
        HZUnityABLoadAdapter.SetLoadRootPath(ProjectSetting.GameEditorABPath);
#endif
        HZUnityAssetBundleManager.OnShaderListLoadOverCallBack = GameGlobal_OnShaderListLoadOverCallBack;
        this.gameObject.AddComponent<HZUnityAssetBundleManager>();
    }


    public class HZDSBHZUnityLoadImlFactory : HZUnityLoadImlFactory
    {
        public override HZUnityLoadIml CreateLoadIml()
        {
            if (!GameGlobal.Instance.useMpq)
            {
                return new HZUnityAssetBundleLoader(HZUnityAssetBundleLoader.HZUnityLoadType.WWW);
            }
            else
            {
                return new HZUnityAssetBundleLoader(HZUnityAssetBundleLoader.HZUnityLoadType.MPQ);
            }
        }
    }

	public static TLClient.Net.SrvListMessage GetSet()
    {
        TLClient.Net.SrvListMessage sm = new SrvListMessage();
        var a = sm.rolebasic;
		var b = sm.message;
		var c = sm.position;
		var d = sm.recom;
		var e = sm.status;
		var f = sm.srvList;

		sm.rolebasic = a;
		sm.message = b;
		sm.position = c;
		sm.recom = d;
		sm.status = e;
		sm.srvList = f;

		return sm;
    }

    public GameLoadScene GetLoadingUI(int type)
    {
        if (type == 0)
            return loadingUI;
        else if(type == 1)
            return loadingUI2;
        else
            return loadingUI3;
    }
    
    private float mPassWarningDelta = -1;
    private float mPassDeltaMem = 0;

    private const float mMinWarnInterval = 2;
    private const float mResetMemwarnDelta = 300;
    private const float mUnloadMemInterval = 200;
    private int mMemoryWarningTimes = 0;
    public void ResetMemoryWarning()
    {
        mMemoryWarningTimes = 0;
        mPassWarningDelta = -1;
        Debug.Log("[ResetMemoryWarning]");
    }

    private void CheckMemory(float delta)
    {
        if (TLBattleScene.Instance == null || !TLBattleScene.Instance.IsRunning)
        {
            //没有主角不进行检测
            return;
        }
        if (mPassWarningDelta >= 0)
        {
            mPassWarningDelta += delta;
            if(mPassWarningDelta > mResetMemwarnDelta)
            {
                ResetMemoryWarning();
            }
        }
        mPassDeltaMem = mPassDeltaMem + delta;
        if (mPassDeltaMem > mUnloadMemInterval)
        {
            Debug.Log("[CheckAndUnload] Interval");
            mPassDeltaMem = 0;
            HZUnityAssetBundleManager.GetInstance().UnloadUnusedAssets();
        }
    }
    
    void UnloadResource(string message)
    {
        if (TLBattleScene.Instance != null && TLBattleScene.Instance.IsRunning)
        {
            Debug.Log("[CheckAndUnload]");
            //ClearCache();
            HZUnityAssetBundleManager.GetInstance().UnloadUnusedAssets();
            BattleLoaderMgr.Instance.DestroyCache();
            if (mPassWarningDelta < 0 || mPassWarningDelta > mMinWarnInterval)
            {
                mMemoryWarningTimes++;
                if (mMemoryWarningTimes >= 2) 
                {
                    ClearCache();
                }
                if (mMemoryWarningTimes >= 4)
                {
                    mMemoryWarningTimes = 0;                  
                }
                MenuMgr.Instance.ClearAllCacheUI(int.MaxValue);
                HZUISystem.Instance.Editor.ReleaseAllTexture();
//                LuaSystem.Instance.LuaGC();
                System.GC.Collect();
                mPassWarningDelta = 0;
            }
        }
    }

}

public class MyUnityDriver : UnityDriver
{
    public override bool TestTextSpellBreak(string regionText, int splitIndex, out int spellSplitIndex)
    {
        spellSplitIndex = splitIndex;
        return false;
    }
}