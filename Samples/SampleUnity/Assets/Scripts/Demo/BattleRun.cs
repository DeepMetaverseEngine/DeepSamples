

using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.Lua;
using System.Collections.Generic;
using TLClient;
using UnityEngine;


public class BattleRun : MonoBehaviour
{
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && !UNITY_ANDROID && !UNITY_IOS
    static BattleRun()
    {
        //new DeepCore.Template.MoonSharp.TemplateLoaderMoonSharp();
        
    }
#endif
    public TLBattleScene Client { get; set; }

    private BattleLoader mLoader;

    private bool mFirstInit;
    private bool mLoadMapComplete;

    public const int Init_Ready = -1;
    public const int Init_BattleResource = 0;
    public const int Init_BattleClient = 1;
    public const int Init_Finish = 2;
    private int mInitProcess = Init_Ready;

    public delegate void OnLoadingProcessChangeEvent(float process);
    public event OnLoadingProcessChangeEvent OnLoadingProcessChange;
    public delegate void OnLoadingCompleteEvent(bool isFirst);
    public event OnLoadingCompleteEvent OnLoadingComplete;

    void Start()
    {

    }

    public void Init()
    {
        GameSceneMgr.Instance.EnterLoadingScene(true);
        ReloadScene();
        mFirstInit = true;
    }

    public bool ReloadScene()
    {
        mFirstInit = false;
        mLoadMapComplete = false;
        BattleLoaderMgr.Instance.localBattle = !GameGlobal.Instance.netMode;
        int sceneId = GameUtil.GetSceneIDToMapID(DataMgr.Instance.UserData.MapTemplateId);
        int RoleTemplateId = DataMgr.Instance.UserData.RoleTemplateId;

        bool needLoadScene = BattleLoaderMgr.Instance.LoadSceneById(sceneId, RoleTemplateId);
        GameLoadMgr.Instance.StartGameLoad();

        LuaSystem.Instance.DoFunc("GlobalHooks.Init");

        HudManager.Instance.InitHud(true);

        LuaSystem.Instance.DoFunc("GlobalHooks.OnEnterScene");

        mInitProcess = Init_BattleResource;

        return needLoadScene;
    }

    private void InitLocalBattleClient()
    {
#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && !UNITY_ANDROID && !UNITY_IOS

        // 本地战斗
        SceneData scene = BattleLoaderMgr.Instance.GetCurrentSceneData();
        TLClientBattleManager.DataRoot.SelectTemplates(scene);
        // 加载战斗场景
        //CommonAIClient.Client.BattleLocalPlay localBattle = TLBattleManager.Instance.CreateLocalBattle(scene);
        TLBattleLocal localBattle = BattleRunManager.CreateLocalBattle(scene);
        BattleRunManager.AddLocalActor(localBattle, GameGlobal.Instance.ActorTemplateID);
        Client = (TLBattleScene)TLBattleFactory.Instance.CreateBattleScene(localBattle);
        localBattle.Layer.ActorSyncMode = DeepCore.GameData.ZoneClient.SyncMode.MoveByClient_PreSkillByClient;
#endif
    }

    private void UpdateInitProcess()
    {
        if (mInitProcess == Init_Finish)
            return;
        else
        {
            int process = GameLoadMgr.Instance.GetLoadProcess();
            if (OnLoadingProcessChange != null)
            {
                OnLoadingProcessChange((float)process / (float)100);
            }
        }

        if (mInitProcess == Init_BattleResource)
        {
            if (BattleLoaderMgr.Instance.IsLoadFinish())
            {
                mLoadMapComplete = true;
                mInitProcess = Init_BattleClient;
            }
        }
        else if (mInitProcess == Init_BattleClient)
        {
            if (Client == null) //单机模式
            {
                if (!GameGlobal.Instance.netMode)
                    InitLocalBattleClient();
            }
            else if (Client.IsRunning && GameLoadMgr.Instance.LoadFinish)
            {
                if (OnLoadingComplete != null)
                {
                    OnLoadingComplete(mFirstInit);
                }
                mInitProcess = Init_Finish;
            }
        }
    }

    void Update()
    {
        UpdateInitProcess();

        if (Client != null && mLoadMapComplete)
        {
            Client.Update(Time.deltaTime);
        }
    }

    public void Clear(bool reLogin)
    {
        if (mInitProcess != Init_Ready)
        {
            mInitProcess = Init_Ready;
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
        }
        BattleLoaderMgr.Instance.Clear(reLogin);
    }

    //void OnGUI()    //测试 
    //{
    //    if (SkillTouchHandler.delayMode)
    //    {
    //        if (GUI.Button(new Rect(100, 150, 160, 40), "速度" + SkillTouchHandler.speed))
    //        {
    //            SkillTouchHandler.delayMode = false;
    //        }
    //        if (GUI.Button(new Rect(100, 200, 80, 40), "减速"))
    //        {
    //            SkillTouchHandler.speed -= 0.1f;
    //        }
    //        if (GUI.Button(new Rect(200, 200, 80, 40), "加速"))
    //        {
    //            SkillTouchHandler.speed += 0.1f;
    //        }
    //    }
    //    else
    //    {
    //        if (GUI.Button(new Rect(100, 150, 160, 40), "映射模式"))
    //        {
    //            SkillTouchHandler.delayMode = true;
    //        }
    //    }
    //}

}