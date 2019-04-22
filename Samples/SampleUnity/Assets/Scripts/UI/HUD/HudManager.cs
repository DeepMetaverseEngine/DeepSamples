 
using UnityEngine;

using System.Collections.Generic;
using System;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;

public class HudManager : DisplayNode
{

    //左上角玩家和目标信息条.
    public PlayerInfoHud PlayerInfo { private set; get; }
    //右上角小地图.
    public SmallMapHud SmallMap { private set; get; }
    ////中间靠下的交互控件.
    public InteractiveHud Interactive { private set; get; }
    //靠左边的队伍和任务栏
    public TeamQuestHud TeamQuest { private set; get; }
    ////BUFF图标管理器
    //public BuffUIManager BuffUIMgr { private set; get; }
    //左下角的摇杆.
    public RockerHud Rocker
    {
        get
        {
            if (GameSceneMgr.Instance.UGUI != null)
                return GameSceneMgr.Instance.UGUI.Rock;
            return null;
        }
    }
    //右下角的技能栏.
    public SkillBarHud SkillBar
    {
        get
        {
            if (GameSceneMgr.Instance.UGUI != null)
                return GameSceneMgr.Instance.UGUI.SkillBar;
            return null;
        }
    }

    //上个Demo版本的遗留，先放着用用，之后重新实现
    private SkillProgressUI mSkillProcess;
    public SkillProgressUI SkillProcess
    {
        get
        {
            if (mSkillProcess == null)
            {
                HZCanvas cvs = GetHudUI(HudName.MainHud.ToString()).FindChildByEditName<HZCanvas>("skillprogress");
                //HZCanvas cvs = PlayerInfo.Root.FindChildByEditName<HZCanvas>("skillprogress");
                mSkillProcess = cvs.AddComponent<SkillProgressUI>();
            }
            return mSkillProcess;
        }
    }
    //lua实现的hud界面，用ExtHud方法获取具体实例.
    private Dictionary<string, HZRoot> mHudList = new Dictionary<string, HZRoot>();

    private int mInitProcess = 0;
    public bool InitFinish { get { return mInitProcess == (int)HudName.End; } }

    private bool mLuaHudLoadFinish = false;
    
    private Dictionary<string, ChildEventHandler> mEnterActionList = new Dictionary<string, ChildEventHandler>();
    private Dictionary<string, ChildEventHandler> mExitActionList = new Dictionary<string, ChildEventHandler>();


    public const int HUD_TOP =      0x1 << 0;
    public const int HUD_LEFT =     0x1 << 1;
    public const int HUD_BOTTOM =   0x1 << 2;
    public const int HUD_RIGHT =    0x1 << 3;
    public const int HUD_CENTER =   0x1 << 4;
    public const int HUD_XCENTER =   0x1 << 5;
    public const int HUD_YCENTER =   0x1 << 6;


    public enum HudName
    {
        PlayerInfo,
        SmallMap,
        Interactive,
        Rocker,
        SkillBar,
        TeamQuest,

        MainHud,
        BuffHud,

        End,
    }

    private static HudManager mInstance;
    public static HudManager Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = new HudManager();
            return mInstance;
        }
    }

    public HudManager()
    {
        mInstance = this;
        Init();
    }

    private void Init()
    {
        Name = "HudRoot";
        this.Enable = true;
        this.EnableChildren = true;
        HZUISystem.SetNodeFullScreenSize(this);

        HZUISystem.Instance.HUDLayerAddChild(this);
    }

    [SLua.DoNotToLua]
    public void InitHud(bool immediately)
    {
        if (immediately)
        {
            while (mInitProcess < (int)HudName.End)
            {
                Init(immediately);
            }
        }
        else
        {
            GameGlobal.Instance.StartCoroutine(StartInitHud(immediately));
        }
    }

    System.Collections.IEnumerator StartInitHud(bool immediately)
    {
        mInitProcess = 0;
        while (mInitProcess < (int)HudName.End)
        {
            Init(immediately);
            yield return 1;
        }
    }

    private int Init(bool immediately)
    {
        bool init = false;
        if (!GameGlobal.Instance.netMode)
        {
            if (mInitProcess == (int)HudName.Rocker || mInitProcess == (int)HudName.SkillBar)
            {
                init = true;
            }
        }
        if (init)
        {
            switch (mInitProcess)
            {
                case (int)HudName.PlayerInfo:
                    InitPlayerInfo(immediately);
                    break;
                case (int)HudName.SmallMap:
                    InitSmallMap(immediately);
                    break;
                case (int)HudName.Interactive:
                    InitInteractive(immediately);
                    break;
                case (int)HudName.Rocker:
                    InitRocker();
                    break;
                case (int)HudName.SkillBar:
                    InitSkillBar(immediately);
                    break;
                case (int)HudName.TeamQuest:

                    InitTeamQuest(immediately);
                    break;
                case (int)HudName.MainHud:
                    InitLuaHud();
                    break;
                case (int)HudName.BuffHud:
                    InitBuffMgr();
                    break;
            }

        }
        

        if (++mInitProcess > (int)HudName.End)
        {
            mInitProcess = (int)HudName.End;
        }

        return mInitProcess;
    }

    private void InitLuaHud()
    {
        if (!mLuaHudLoadFinish)
        {
            mLuaHudLoadFinish = true;
            EventManager.Fire(GameEvent.UI_HUD_LUAHUDINIT, EventManager.defaultParam);
        }
    }

    private void InitPlayerInfo(bool immediately)
    {
        if (this.PlayerInfo == null)
        {
            this.PlayerInfo = new PlayerInfoHud();
            this.AddHudUI(this.PlayerInfo.Root, HudName.PlayerInfo.ToString());
        }
        if(!immediately)
            PlayerInfo.OnEnterScene();
    }

    private void InitBuffMgr()
    {
        //if (this.BuffUIMgr == null)
        //{
        //    this.BuffUIMgr = this.Transform.gameObject.AddComponent<BuffUIManager>();
        //    this.BuffUIMgr.Init();
        //}
    }

    private void InitSmallMap(bool immediately)
    {
        if (!GameGlobal.Instance.netMode)
        {
            return;
        }
        if (this.SmallMap == null)
        {
            this.SmallMap = new SmallMapHud();
            this.AddHudUI(this.SmallMap.Root, HudName.SmallMap.ToString());
        }
        if(!immediately)
            SmallMap.OnEnterScene();
    }

    private void InitInteractive(bool immediately)
    {
        if (!GameGlobal.Instance.netMode)
        {
            return;
        }
        if (this.Interactive == null)
        {
            this.Interactive = new InteractiveHud();
            this.AddHudUI(this.Interactive.Root, HudName.Interactive.ToString());
        }
        if (!immediately)
            Interactive.OnEnterScene();
    }

    private void InitRocker()
    {
        
    }

    private void InitSkillBar(bool immediately)
    {
        if (!immediately && SkillBar != null)
            SkillBar.OnEnterScene();
    }

    private void InitTeamQuest(bool immediately)
    {
        if (this.TeamQuest == null)
        {
            this.TeamQuest = new TeamQuestHud();
            this.AddHudUI(this.TeamQuest.Root, HudName.TeamQuest.ToString());
        }
        if (!immediately)
            TeamQuest.OnEnterScene();
    }

    protected override void OnChildRemoved(DisplayNode child)
    {
        base.OnChildRemoved(child);
        ChildEventHandler ret;
        if (mExitActionList.TryGetValue(child.Name,out ret))
        {
            ret.Invoke(this, child);
        }
    }

    protected override void OnChildAdded(DisplayNode child)
    {
        base.OnChildAdded(child);
        ChildEventHandler ret;
        if (mEnterActionList.TryGetValue(child.Name, out ret))
        {
            ret.Invoke(this,child);
        }
    }

    public void SubscribHudRemoved(string name, ChildEventHandler act)
    {
        mExitActionList[name] = act;
    }

    public void UnSubscribHudRemoved(string name)
    {
        mExitActionList.Remove(name);
    }

    public void SubscribHudAdded(string name, ChildEventHandler act)
    {
        mEnterActionList[name] = act;
    }

    public void UnSubscribHudAdded(string name)
    {
        mEnterActionList.Remove(name);
    }

    public void AddHudUI(HZRoot hudRoot, string name)
    {
        if (hudRoot == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(name))
        {
            Debugger.LogError("hud need a unique name!");
            return;
        }
        mHudList.Add(name, hudRoot);
        DisplayNode hud = hudRoot.Parent == null ? hudRoot : hudRoot.Parent;
        hud.Name = name;
        this.AddChild(hud);
    }

    public UERoot AddHudUIFromXml(string xmlPath, string hudName)
    {
        HZRoot root = (HZRoot)HZUISystem.CreateFromFile(xmlPath);
        AddHudUI(root, hudName);
        return root;
    }

    public void RemoveHudUI(string name)
    {
        if (mHudList.ContainsKey(name))
        {
            this.RemoveChild(mHudList[name], true);
            mHudList.Remove(name);
        }
    }

    public HZRoot GetHudUI(string name)
    {
        if (mHudList.ContainsKey(name))
        {
            return mHudList[name];
        }
        return null;
    }

    public HZRoot FindByXmlName(string name)
    {
        var editer = HZUISystem.Instance.Editor;
        foreach(var item in mHudList)
        {
            string metaKey = editer.GetMetaKey(item.Value.MetaData);
            if(metaKey != null && metaKey.Contains(name))
            {
                return item.Value as HZRoot;
            }
        }
        return null;
    }

    public void HideAllHud(bool hide)
    {
        this.Visible = !hide;
        if (Rocker != null)
            Rocker.Visible = !hide;
        if (SkillBar != null)
            SkillBar.Visible = !hide;
    }

    public void InitAnchorWithNode(DisplayNode node, int anchor)
    {
        if (node == null)
            return;

        if ((anchor & HUD_LEFT) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x - HZUISystem.Instance.StageOffsetX, node.Position2D.y);
        }
        if ((anchor & HUD_TOP) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x, node.Position2D.y - HZUISystem.Instance.StageOffsetY);
        }
        if ((anchor & HUD_RIGHT) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x + HZUISystem.Instance.StageOffsetX, node.Position2D.y);
        }
        if ((anchor & HUD_BOTTOM) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x, node.Position2D.y + HZUISystem.Instance.StageOffsetY);
        }

        if ((anchor & HUD_XCENTER) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x + HZUISystem.Instance.StageOffsetX / 2, node.Position2D.y);
        }

        if ((anchor & HUD_YCENTER) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x, node.Position2D.y - HZUISystem.Instance.StageOffsetY / 2);
        }

        if ((anchor & HUD_CENTER) != 0)
        {
            node.Position2D = new Vector2(node.Position2D.x + HZUISystem.Instance.StageOffsetX / 2, node.Position2D.y - HZUISystem.Instance.StageOffsetY/2);
        }
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (PlayerInfo != null)
            PlayerInfo.Clear(reLogin, reConnect);
        if (SmallMap != null)
            SmallMap.Clear(reLogin, reConnect);
        if (Interactive != null)
            Interactive.Clear(reLogin, reConnect);
        if (TeamQuest != null)
            TeamQuest.Clear(reLogin, reConnect);
        if (Rocker != null)
            Rocker.Clear(reLogin, reConnect);
        if (SkillBar != null)
            SkillBar.Clear(reLogin, reConnect);
        //if (BuffUIMgr)
        //{
        //    BuffUIMgr.ResetSelfBuff();
        //    BuffUIMgr.ResetTargetBuff();
        //}
        //HideAllHud(true);

        if (reLogin)
        {
            foreach (var d in mHudList)
            {
                //使其Added和Removed配套
                d.Value.RemoveFromParent(true);
            }
            mHudList.Clear();
            HZUISystem.Instance.HUDLayerRemoveChild(this, true);
            PlayerInfo = null;
            SmallMap = null;
            //Interactive = null;
            TeamQuest = null;
            mInstance = null;
            mInitProcess = (int)HudName.End;
        }
    }
	
}
