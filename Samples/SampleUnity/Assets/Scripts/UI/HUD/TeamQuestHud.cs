using System;
using System.Collections.Generic;
using Assets.Scripts;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIAction;
using DeepCore.Unity3D.UGUIEditor.UI;
using UnityEngine;

public class TeamQuestHud : DisplayNode, IObserverExt<TeamData>
{
    private bool _isQuest;
    private bool _isTeam;
    private bool _isTips;
    private HZRoot _mRoot = null;
    private HZCanvas _mMoveNode;
    private HZToggleButton _questBtn;
    private HZToggleButton _teamBtn;
    private HZToggleButton _fubenBtn;
    private List<Dictionary<string, object>> _mGuildData;
    
    public HZRoot Root { get { return _mRoot; } }
    public TeamHud Team { get; private set; }
    public QuestHud Quest { get; private set; }
    public DungeonTips Tips { get; set; }

    public TeamQuestHud()
    {
        Init();
    }

    protected override void OnDispose()
    {
        base.OnDispose();
    }

    private Dictionary<string, object> _mapData;
    private bool _isRaid = false;//如果是多人本，进去默认显示队伍
    private bool IsInDungeon()
    {
        if (!GameGlobal.Instance.netMode)
            return false;

        _mapData = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
        _isRaid = Convert.ToInt32(_mapData["type"]) == 3 && DataMgr.Instance.TeamData.MemberCount > 1 ;
        return Convert.ToInt32(_mapData["dungeondesc"]) != 0;
    }
    
    private bool IsShowTeamBtn()
    {
        var mapdesc = GameUtil.GetDBData("DungeonDesc", Convert.ToInt32(_mapData["dungeondesc"]));
        if (mapdesc==null || !mapdesc.ContainsKey("is_teamshow"))
            return false;
        return Convert.ToInt32(mapdesc["is_teamshow"]) == 1;
    }
    
    //每次进场景前会调用
    public void OnEnterScene()
    {
        if (IsInDungeon())
        {
            if (_isRaid)
            {
                _teamBtn.IsChecked = true;
                _fubenBtn.IsChecked = false;
                ShowTeam();
            }
            else
            {
                _teamBtn.IsChecked = false;
                _fubenBtn.IsChecked = true;
                ShowTips();
            }
            _questBtn.Visible = false;
            _fubenBtn.Visible = true;
            _teamBtn.Visible = IsShowTeamBtn();
            this.Visible = true;
        }
        else
        {
            _teamBtn.IsChecked = false;
            _fubenBtn.Visible = false;
            _questBtn.Visible = true;
            _questBtn.IsChecked = true;
            _teamBtn.Visible = true;
            ShowQuest();
        }
    }

    public void OnOpenFuncEntryMenu(EventManager.ResponseData res)
    {
		_mRoot.Visible = false;
    }

    public void OnCloseFuncEntryMenu(EventManager.ResponseData res)
    {
        _mRoot.Visible = true;
    }

    private void Init()
    {
        if (!GameGlobal.Instance.netMode)
        {
            return;
        }
        DataMgr.Instance.TeamData.AttachObserver(this);
        HZUISystem.SetNodeFullScreenSize(this);
        _mRoot = (HZRoot)HZUISystem.CreateFromFile("xml/hud/ui_hud_team_quest.gui.xml");
        Enable = false;
        _mRoot.Enable = false;
        EnableChildren = true;
        if (_mRoot != null) { AddChild(_mRoot); }

        HudManager.Instance.InitAnchorWithNode(_mRoot, HudManager.HUD_TOP | HudManager.HUD_LEFT);

        Team = new TeamHud(_mRoot.FindChildByEditName<HZCanvas>("cvs_team"));
        Quest = new QuestHud(_mRoot.FindChildByEditName<HZCanvas>("cvs_mission"));
        Tips=new DungeonTips(_mRoot.FindChildByEditName<HZCanvas>("cvs_fuben"));

        InitCompmont();
    }

    private void InitCompmont()
    {
        var closeBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbt_shrink");
        if (closeBtn != null)
        {
            closeBtn.TouchClick = (sender) =>
            {
                if (closeBtn.IsChecked)
                {
                    HideFrame(true);
                }
                else
                {
                    ShowFrame(true);
                }
            };
        }
        
        _mMoveNode = _mRoot.FindChildByEditName<HZCanvas>("cvs_teamquest");
        _questBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbt_mission");
        _questBtn.IsChecked = true;
        _teamBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbn_team");
        _teamBtn.IsChecked = false;
        _fubenBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbt_fuben");
        
        if (_questBtn != null)
        {
            _questBtn.TouchClick = (sender) =>
            {
                if (_isQuest)
                {
                    //show quest menu
                    _questBtn.IsChecked = true;
                    MenuMgr.Instance.OpenUIByTag("QuestMain");
                    return;
                }
                _teamBtn.IsChecked = false;
                _questBtn.IsChecked = true;
                ShowQuest();
            };
        }

        if (_fubenBtn!=null)
        {
            _fubenBtn.TouchClick = (sender) =>
            {
                if (_isTips)
                {
                    _fubenBtn.IsChecked = true;
                    return;
                }
                _fubenBtn.IsChecked = true;
                _teamBtn.IsChecked = false;
                ShowTips();
            };
        }
        
        if (_teamBtn != null)
        {
            _teamBtn.TouchClick = (sender) =>
            {
                if (_isTeam)
                {
                    MenuMgr.Instance.OpenUIByTag("TeamFrame", 0, new object[] { "TeamInfo" });
                    //show team menu
                    //if (DataMgr.Instance.TeamData.HasTeam)
                    //{
                    //    MenuMgr.Instance.OpenUIByTag("TeamFrame", 0, new object[] {"TeamInfo"});
                    //}
                    //else
                    //{
                    //    MenuMgr.Instance.OpenUIByTag("TeamFrame", 0, new object[] { "TeamPlatform" });
                    //}
                    _teamBtn.IsChecked = true;
                    return;
                }
                _teamBtn.IsChecked = true;
                _questBtn.IsChecked = false;
                _fubenBtn.IsChecked = false;
                ShowTeam();
            };
        }

        var pipei = _mRoot.FindChildByEditName<HZCanvas>("cvs_pipei");
        pipei.TouchClick = (sender) =>
        {
            MenuMgr.Instance.OpenUIByTag("MatchInfo");
        };

        //默认展开
        ShowFrame(true);
        //默认显示任务
        if (_isTips)
        {
            ShowTips();
        }
        else
        {
            ShowQuest();
        }
    }

    public void ShowFrame(bool withAnime)
    {
        MenuBase.SetVisibleUENode(_mMoveNode, true);
        if (withAnime)
        {
            var ma = new MoveAction { 
                Duration = 0.2f,
                TargetX = 0,
                TargetY = _mMoveNode.Y,};
            _mMoveNode.AddAction(ma);
        }
        else
        {
            _mMoveNode.X = 0;
        }
    }

    public void HideFrame(bool withAnime)
    {
        if (withAnime)
        {
            var ma = new MoveAction {
                Duration = 0.2f,
                TargetX = -_mMoveNode.Width, 
                TargetY = _mMoveNode.Y,
                ActionFinishCallBack = (sender) =>
                {
                    MenuBase.SetVisibleUENode(_mMoveNode, false);
                }
            };
            _mMoveNode.AddAction(ma);
        }
        else
        {
            _mMoveNode.X = -_mMoveNode.Width;
            MenuBase.SetVisibleUENode(_mMoveNode, false);
        }
    }

    private void ShowQuest()
    {
        MenuBase.SetVisibleUENode(_mRoot, "cvs_mission", true);
        MenuBase.SetVisibleUENode(_mRoot, "cvs_team", false);
        MenuBase.SetVisibleUENode(_mRoot, "cvs_fuben", false);
        _isQuest = true;
        _isTeam = false;
        _isTips = false;
    }

    private void ShowTeam()
    {
        MenuBase.SetVisibleUENode(_mRoot, "cvs_mission", false);
        MenuBase.SetVisibleUENode(_mRoot, "cvs_team", true);
        MenuBase.SetVisibleUENode(_mRoot, "cvs_fuben", false);
        _isTeam = true;
        _isQuest = false;
        _isTips = false;
    }

    private void ShowTips()
    {
        MenuBase.SetVisibleUENode(_mRoot, "cvs_mission", false);
        MenuBase.SetVisibleUENode(_mRoot, "cvs_team", false);
        MenuBase.SetVisibleUENode(_mRoot, "cvs_fuben", true);
        _isQuest = false;
        _isTeam = false;
        _isTips = true;
    }
    
    public void SwitchLabel(bool isQuest)
    {
        _questBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbt_mission");
        _teamBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbn_team");
        _fubenBtn = _mRoot.FindChildByEditName<HZToggleButton>("tbt_fuben");
        _questBtn.IsChecked = isQuest;
        _teamBtn.IsChecked = !isQuest;
        _fubenBtn.IsChecked = _isTips;
        if (_isQuest)
        {
            ShowQuest();
        }
        else if (_isTips)
        {
            ShowTips();
        }
        else
        {
            ShowTeam();
        }
    }

    private UELabel _mMatchLable;
    private UELabel _mMatchNameLable;
    protected override void OnUpdate()
    {
        if (IsInMatch && _mMatchLable != null)
        {
            _mMatchLable.Text = DataMgr.Instance.TeamData.CurrentMatchCountdown;
        }
        if (Team != null)
            Team.Update();
        if (Quest != null)
            Quest.Update();
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (Team != null)
        {
            Team.Clear(reLogin);
        }
        if (Quest != null)
        {
            Quest.Clear(reLogin,reConnect);
        }
        if (Tips!=null)
        {
            Tips.Clear(reLogin,reConnect);
        }

        if (reConnect)
        {
            IsInMatch = false;
        }
        if (reLogin)
        {
            Team = null;
            Quest = null;
            Tips = null;
            _mMatchLable = null;
            IsInMatch = false;
            DataMgr.Instance.TeamData.DetachObserver(this);
        }
    }

    public bool IsInMatch
    {
        get
        {
            return DataMgr.Instance.TeamData.MatchNotify != null;
        }
        set
        {
            if (_mMatchLable == null)
            {
                _mMatchLable = _mRoot.FindChildByEditName<UELabel>("lb_time");
            }

            if (_mMatchNameLable == null)
            {
                _mMatchNameLable = _mRoot.FindChildByEditName<UELabel>("lb_pipei");
            }

            if (value)
            {
                var name = HZLanguageManager.Instance.GetString(DataMgr.Instance.TeamData.MatchNotify.s2c_desc);
                _mMatchNameLable.Text = HZLanguageManager.Instance.GetFormatString("team_matchingmsg2", name);
            }

            _mMatchLable.Text = "";

            MenuBase.SetVisibleUENode(_mRoot, "cvs_pipei", value);
        }
    }

    public void Notify(int status, TeamData subject, object opt)
    {
        if (status == (int)TeamData.NotiFyStatus.MatchState)
        {
            IsInMatch = (bool)opt;
        }
    }
}
