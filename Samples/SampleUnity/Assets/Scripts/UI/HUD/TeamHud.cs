using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using DeepCore.GUI.Data;
using TLProtocol.Data;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepMMO.Data;

public class TeamHud : IObserverExt<TeamData>
{

    public delegate void OnHeigtChangeEvent(float height);
    public OnHeigtChangeEvent OnHeigtChange;

    private HZCanvas mRoot;

    protected Dictionary<string, DisplayNode> mParts = new Dictionary<string, DisplayNode>();

    private int mShowMax;

    public TeamHud(HZCanvas root)
    {
        mRoot = root;
        Init();
    }

    private int mFollowEffectID;
    
    private void Init()
    {
        DataMgr.Instance.TeamData.AttachObserver(this);

        HZTextButton createTeam = mRoot.FindChildByEditName<HZTextButton>("btn_found");
        if (createTeam != null)
        {
            createTeam.TouchClick = (sender) =>
            {
                DataMgr.Instance.TeamData.CreateTeam((success) =>
                {
                    MenuMgr.Instance.OpenUIByTag("TeamFrame", 0, new object[] { "TeamInfo" });

                });
            };
        }

        HZTextButton searchTeam = mRoot.FindChildByEditName<HZTextButton>("btn_join");
        if (searchTeam != null)
        {
            searchTeam.TouchClick = (sender) =>
            {
                MenuMgr.Instance.OpenUIByTag("TeamFrame", 0, new object[] { "TeamPlatform" });
            };
        }

        HZTextButton btn_follow = mRoot.FindChildByEditName<HZTextButton>("btn_follow");
        if (searchTeam != null)
        {
            btn_follow.TouchClick = (sender) =>
            {
                if (!DataMgr.Instance.TeamData.HasTeam)
                {
                    return;
                }

                if (DataMgr.Instance.TeamData.IsLeader())
                {
                    //召唤跟随
                    DataMgr.Instance.TeamData.RequestFollowLeader(true, null);
                }
                else
                {
                    DataMgr.Instance.TeamData.RequestFollowLeader(!DataMgr.Instance.TeamData.IsFollowLeader, null);
                }
            };
        }

        var tf = new TransformSet
        {
            Parent = btn_follow.UnityObject.transform,
            Pos = new Vector3(18, -18),
            Layer = (int) PublicConst.LayerSetting.UI
        };
        mFollowEffectID = RenderSystem.Instance.PlayEffect("/res/effect/ui/ef_ui_follow.assetbundles", tf);
        RenderSystem.Instance.SetEffectVisible(mFollowEffectID, false);
        //HZTextButton inviteMember = mRoot.FindChildByEditName<HZTextButton>("btn_yaoqing");
        //if (inviteMember != null)
        //{
        //    inviteMember.TouchClick = (sender) =>
        //    {

        //    };
        //}

        //HZTextButton leaveTeam = mRoot.FindChildByEditName<HZTextButton>("btn_out");
        //if (leaveTeam != null)
        //{
        //    leaveTeam.Visible = DataMgr.Instance.TeamData.HasTeam;
        //    leaveTeam.TouchClick = (sender) =>
        //    {

        //    };
        //}


        HZCanvas list = mRoot.FindChildByEditName<HZCanvas>("cvs_list");
        mShowMax = list.UserTag;
        for (int i = 0; i < mShowMax; ++i)
        {
            MenuBase.SetVisibleUENode(mRoot, "fn_a" + (i + 1), false);
        }

        MenuBase.SetVisibleUENode(mRoot, "cvs_list", DataMgr.Instance.TeamData.HasTeam);
        MenuBase.SetVisibleUENode(mRoot, "cvs_noteam", !DataMgr.Instance.TeamData.HasTeam);
        MenuBase.SetVisibleUENode(mRoot, "cvs_group", false);
    }

    private void RefreshShowInfo()
    {
        MenuBase.SetVisibleUENode(mRoot, "cvs_list", DataMgr.Instance.TeamData.HasTeam);
        MenuBase.SetVisibleUENode(mRoot, "cvs_noteam", !DataMgr.Instance.TeamData.HasTeam);

        if (DataMgr.Instance.TeamData.HasTeam)
        {
            if (DataMgr.Instance.TeamData.IsLeader())
            {
                
            }
            else
            {
                
            }
        }
        else
        {
            MenuBase.SetVisibleUENode(mRoot, "btn_askfollow", false);
            MenuBase.SetVisibleUENode(mRoot, "tb_autofollow", false);
        }
    }

    private void OnTeamMemberUpdate(TeamMember info)
    {
        for (int i = 0; i < mShowMax; ++i)
        {
            HZFileNode cvs = mRoot.FindChildByEditName<HZFileNode>("fn_a" + (i + 1));
            if (cvs != null && cvs.Visible)
            {
                if (cvs.UserData == info.RoleID)
                {
                    RefreshOneMember(cvs, info);
                    return;
                }
            }
        }
    }

    private void RefreshOneMember(HZFileNode cvs, TeamMember info)
    {
        var aiplayer = TLBattleScene.Instance.GetAIPlayer(info.RoleID);

        string name;
        int pro;
        int level;
        if (aiplayer != null)
        {
            name = aiplayer.Name();
            pro = (int)aiplayer.PlayerVirtual.RolePro();
            level = aiplayer.Level();
        }
        else
        {
            var roleSnap = DataMgr.Instance.UserData.RoleSnapReader.GetCache(info.RoleID);
            if (roleSnap == null)
            {
                DataMgr.Instance.UserData.RoleSnapReader.Get(info.RoleID, (snap) =>
                {
                    if (snap != null)
                    {
                        RefreshOneMember(cvs, info);
                    }
                });
                return;
            }
            name = roleSnap.Name;
            level = roleSnap.Level;
            pro = roleSnap.Pro;
        }
        cvs.UserData = info.RoleID;
        cvs.Tag = info;
        cvs.Visible = true;
        //name
        MenuBase.SetLabelText(cvs, "lb_name", name);
        //hp
        HZGauge hp = GetComponent(cvs, "gg_xuetiao") as HZGauge;
        //hp.StripLayout = ZeusUISystem.CreateLayoutFroXmlKey("#dynamic/dynamic_new/hudnew/hudnew.xml|hudnew|hppro_" + info.pro, UILayoutStyle.IMAGE_STYLE_H_012, 12);
        hp.SetGaugeMinMax(0, 1);
        hp.Value = 1;
        //lv
        MenuBase.SetLabelText(cvs, "lb_level", level.ToString());
        //icon
        HZImageBox icon = GetComponent(cvs, "ib_josicon") as HZImageBox;
        UILayout layout = HZUISystem.CreateLayout(GameUtil.GetProIcon(pro), UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
        if (layout != null)
        {
            icon.Layout = layout;
        }
        //select
        HZCanvas frame = GetComponent(cvs, "cvs_frame") as HZCanvas;
        frame.TouchClick = (sender) =>
        {
            OpenFuncMenu(info.RoleID, name);
            ChangeTarget(info.RoleID);
        };
        //leader 是否是队长
        MenuBase.SetVisibleUENode(cvs, "ib_group", DataMgr.Instance.TeamData.IsLeader(info.RoleID));

        //offline 离线
        MenuBase.SetVisibleUENode(cvs, "ib_offline", info.State == TeamMember.RoleState.Offline);

        //away 非同场景
        MenuBase.SetGrayUENode(cvs, "cvs_frame", info.State == TeamMember.RoleState.Normal && !DataMgr.Instance.TeamData.IsSameScene(info.RoleID));

        //dead 死亡
        MenuBase.SetVisibleUENode(cvs, "ib_dead", info.State == TeamMember.RoleState.Dead);

        //follow 跟随状态
        MenuBase.SetVisibleUENode(cvs, "tbt_follow", info.IsFollowLeader);

        if (info.RoleID == DataMgr.Instance.UserData.RoleID)
        {
            //跟随特效
            RenderSystem.Instance.SetEffectVisible(mFollowEffectID, info.IsFollowLeader);
        }
    }

    private void OnTeamBaseUpdate()
    {
        var list = DataMgr.Instance.TeamData.AllMembers;
        int len = Mathf.Max(list.Count, mShowMax);
        for (int i = 0; i < len; ++i)
        {
            HZFileNode cvs = mRoot.FindChildByEditName<HZFileNode>("fn_a" + (i + 1));
            if (cvs != null)
            {
                if (i < list.Count)
                {
                    var info = list[i];
                    RefreshOneMember(cvs, info);
                }
                else
                {
                    cvs.Visible = false;
                }
            }
        }
        RefreshShowInfo();
    }

    private void OpenFuncMenu(string uuid, string name)
    {
        string menuType;
        if (uuid == DataMgr.Instance.UserData.RoleID)
            menuType = "team_self";
        else
        {
            if (DataMgr.Instance.TeamData.IsLeader())
                menuType = "teamlead";
            else
                menuType = "teammate";
        }

        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("playerId", uuid);
        args.Add("playerName", name);
        args.Add("menuKey", menuType);
        HZImageBox ib = GetComponent((UIComponent)mRoot.Parent, "ib_bgdi") as HZImageBox;
        if(ib != null)
        {
            args.Add("pos", new Vector2(ib.X + ib.Width, mRoot.Parent.Y));
            args.Add("anchor", new Vector2(0, 0));
        }
        EventManager.Fire("Event.InteractiveMenu.Show", args);
    }

    private void ChangeTarget(string uuid)
    {
        DeepCore.GameSlave.ZoneUnit zu = GameSceneMgr.Instance.BattleRun.Client.GetPlayerUnitByUUID(uuid);
        if (zu != null)
        {
            GameSceneMgr.Instance.BattleRun.Client.Actor.ChangeTarget(zu.ObjectID, false);
        }
        else
        {
            //remove target
            GameSceneMgr.Instance.BattleRun.Client.Actor.ChangeTarget(uint.MaxValue, false);
        }
    }

    public void Notify(int status, TeamData subject, object opt)
    {
        if (status == (int)TeamData.NotiFyStatus.TeamUpdate)
        {
            OnTeamBaseUpdate();
        }
        else if (status == (int)TeamData.NotiFyStatus.MemberUpdate)
        {
            var info = opt as TeamMember;
            OnTeamMemberUpdate(info);
        }
        else if (status == (int)TeamData.NotiFyStatus.TeamJoinOrLeave)
        {
            RefreshShowInfo();
        }

    }

    /// <summary>
    /// 返回搜索的控件，并存入容器中
    /// </summary>
    /// <param name="root">搜索的根结点</param>
    /// <param name="name">控件名</param>
    /// <returns>搜索的控件</returns>
    public DisplayNode GetComponent(UIComponent root, string name)
    {
        if (root == null)
        {
            return null;
        }
        string key = root.EditName + '.' + root.GetHashCode() + '.' + name;
        if (!mParts.ContainsKey(key))
        {
            mParts[key] = root.FindChildByEditName<UIComponent>(name);
        }
        return mParts[key];
    }

    public void Update()
    {
        if (GameSceneMgr.Instance.BattleRun.Client == null)
            return;

        int len = Mathf.Min(DataMgr.Instance.TeamData.MemberCount, mShowMax);
        for (int i = 0; i < len; ++i)
        {
            HZFileNode cvs = GetComponent(mRoot, "fn_a" + (i + 1)) as HZFileNode;
            if (cvs != null)
            {
                string uuid = cvs.UserData;
                var zu = GameSceneMgr.Instance.BattleRun.Client.GetAIPlayer(uuid);
                if (zu != null)
                {
                    cvs.IsGray = false;
                    HZGauge hp = GetComponent(cvs, "gg_xuetiao") as HZGauge;
                    if (hp.Value != zu.HP)
                    {
                        hp.SetGaugeMinMax(0, zu.MaxHP);
                        hp.Value = zu.HP;
                    }
                    MenuBase.SetLabelText(cvs, "lb_level", zu.Level().ToString());
                    MenuBase.SetVisibleUENode(cvs, "ib_dead", zu.IsDead);
                    MenuBase.SetVisibleUENode(cvs, "ib_auto", zu.PlayerVirtual.IsGuard());
                }
                else
                {
                    cvs.IsGray = true;
                }
            }
        }
    }

    public void Clear(bool reLogin)
    {
        if (reLogin)
        {
            mParts.Clear();
            DataMgr.Instance.TeamData.DetachObserver(this);
        }
    }

}
