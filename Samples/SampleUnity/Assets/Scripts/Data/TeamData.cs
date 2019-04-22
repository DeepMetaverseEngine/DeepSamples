using System;
using Assets.Scripts;
using SLua;
using System.Collections.Generic;
using DeepCore;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;
using TLTeamData = TLProtocol.Data.TeamData;

/// <summary>
/// todo 新增的一些需求的逻辑和显示分离
/// </summary>
public class TeamData : ISubjectExt<TeamData>
{
    public enum NotiFyStatus : int
    {
        TeamUpdate = 0,
        MemberUpdate = 1,
        TeamJoinOrLeave,
        TeamFollowBtn,
        MatchState,

        //所有标志位
        ALL = int.MaxValue,
    }

    //是否有队伍.
    public bool HasTeam
    {
        get { return !mSnap.IsInvalid; }
    }

    //队伍Id
    public string TeamId
    {
        get { return HasTeam ? mSnap.TeamID : null; }
    }

    //队伍成员数量.
    public int MemberCount
    {
        get { return HasTeam ? mSnap.Members.Count : 0; }
    }

    //队伍是否满员.
    public bool IsTeamFull
    {
        get { return mSnap.Members.Count == mSnap.MaxCount; }
    }

    public string LeaderID
    {
        get { return HasTeam ? mSnap.LeaderID : null; }
    }

    private HashSet<IObserverExt<TeamData>> mObservers = new HashSet<IObserverExt<TeamData>>();
    private Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();

    private readonly TLTeamData mSnap = new TLTeamData();

    public TeamSetting Setting
    {
        get
        {
            if (mSnap.Setting == null)
            {
                mSnap.Setting = new TeamSetting {TargetID = 1, MinLevel = 1};
            }

            return mSnap.Setting;
        }
    }

    public List<TeamMember> AllMembers
    {
        get { return mSnap.Members; }
    }

    private TeamMember mSelf;

    //是否跟随队长
    public bool IsFollowLeader
    {
        get { return mSelf != null && mSelf.IsFollowLeader; }
    }

    public void UploadSetting(TeamSetting setting, Action cb, Action errcb)
    {
        TLNetManage.Instance.Request<ClientSetTeamResponse>(new ClientSetTeamRequest {c2s_settting = setting}, (ex, rp) =>
        {
            if (ex == null && rp.IsSuccess)
            {
                if (cb != null)
                {
                    cb.Invoke();
                }
            }
            else if(errcb != null)
            {
                errcb.Invoke();
            }
        });
    }

    public string CurrentMatchCountdown
    {
        get
        {
            if (DataMgr.Instance.TeamData.MatchNotify == null)
            {
                return null;
            }

            var utc = GameSceneMgr.Instance.syncServerTime.GetServerTimeUTC();
            var span = utc - DataMgr.Instance.TeamData.MatchNotify.s2c_startUtc;
            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddSeconds((int) Math.Floor(span.TotalSeconds));
            return dt.ToString("mm:ss");
        }
    }

    public void RequestFollowLeader(bool var, Action<bool> cb)
    {
        if (!HasTeam)
        {
            return;
        }

        TLNetManage.Instance.Request<TLClientTeamFollowResponse>(new TLClientTeamFollowRequest {c2s_open = var}, (ex, rp) =>
        {
            var ret = ex == null && rp.IsSuccess;
            if (ret)
            {
                if (!IsLeader())
                {
                    mSelf.IsFollowLeader = var;
                    Notify((int) NotiFyStatus.MemberUpdate, mSelf);
                    if (var)
                    {
                        // 取队长uuid
                        TLBattleScene.Instance.Actor.StartTeamFollow(LeaderID);
                    }
                    else
                    {
                        TLBattleScene.Instance.Actor.StopTeamFollow();
                    }
                }
                else
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("team_follow_invite"));
                }
            }

            if (cb != null)
            {
                cb(ret);
            }
        });
    }

    public void InitNetWork()
    {
        TLNetManage.Instance.Listen<TLClientTeamDataNotify>(OnTeamDataNotify);
        TLNetManage.Instance.Listen<ClientMatchStateNotify>(OnMatchStateNotify);
    }

    public ClientMatchStateNotify MatchNotify;

    private void OnMatchStateNotify(ClientMatchStateNotify ntf)
    {
        if (ntf.s2c_matching)
        {
            MatchNotify = ntf;
            var matchName = string.IsNullOrEmpty(ntf.s2c_desc) ? "" : HZLanguageManager.Instance.GetString(ntf.s2c_desc);
            var msg = HZLanguageManager.Instance.GetFormatString("team_matchingmsg", matchName);
            GameAlertManager.Instance.ShowNotify(msg);
        }
        else
        {
            MatchNotify = null;
        }

        Notify((int) NotiFyStatus.MatchState, ntf.s2c_matching);
    }

    public bool IsInMatch
    {
        get { return MatchNotify != null; }
    }

    public string MatchingName
    {
        get { return MatchNotify != null ? string.IsNullOrEmpty(MatchNotify.s2c_desc) ? "" : HZLanguageManager.Instance.GetString(MatchNotify.s2c_desc) : null; }
    }

    public void CreateTeam(Action<bool> cb = null)
    {
        TLNetManage.Instance.Request<TLClientCreateTeamResponse>(new TLClientCreateTeamRequest(), (ex, rp) =>
        {
            if (cb != null)
            {
                cb.Invoke(ex == null && rp.IsSuccess);
            }
        });
    }


    private void OnTeamDataNotify(TLClientTeamDataNotify msg)
    {
        var change = msg.s2c_data;
        mSnap.ApplyChange(change, DataMgr.Instance.UserData.RoleID, ref mSelf);
        switch (change.Type)
        {
            case TeamChangeType.Started:
                var stringList = new string[mSnap.Members.Count];
                for (var i = 0; i < mSnap.Members.Count; i++)
                {
                    var member = mSnap.Members[i];
                    stringList[i] = member.RoleID;
                }

                DataMgr.Instance.UserData.RoleSnapReader.GetMany(stringList, rps => { Notify((int) NotiFyStatus.TeamUpdate, null); });
                if (!IsLeader())
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("team_enter"));
                }

                GameSceneMgr.Instance.UGUI.OnRockFingerUse += UGUI_OnRockFingerUse;
                break;
            case TeamChangeType.MemberIn:
                DataMgr.Instance.UserData.RoleSnapReader.Get(change.Member.RoleID, rp =>
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetFormatString("team_target_enter", rp.Name));
                    Notify((int) NotiFyStatus.TeamUpdate, null);
                });
                break;
            case TeamChangeType.MemberKick:
                if (!HasTeam)
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("team_kick"));
                }
                else
                {
                    var snap = DataMgr.Instance.UserData.RoleSnapReader.GetCache(change.UUID);
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetFormatString("team_target_kick", snap.Name));
                    Notify((int) NotiFyStatus.TeamUpdate, null);
                }

                break;
            case TeamChangeType.MemberLeave:
                if (!HasTeam)
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("team_leave"));
                }
                else
                {
                    var snap = DataMgr.Instance.UserData.RoleSnapReader.GetCache(change.UUID);
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetFormatString("team_target_leave", snap.Name));
                    Notify((int) NotiFyStatus.TeamUpdate, null);
                }

                break;
            case TeamChangeType.MemberInfo:
            {
                if (change.Member.RoleID == mSelf.RoleID)
                {
                    if (TLBattleScene.Instance != null && TLBattleScene.Instance.Actor != null)
                    {
                        if (change.Member.IsFollowLeader)
                        {
                            TLBattleScene.Instance.Actor.StartTeamFollow(LeaderID);
                        }
                        else
                        {
                            TLBattleScene.Instance.Actor.StopTeamFollow();
                        }
                    }
                }

                Notify((int) NotiFyStatus.MemberUpdate, change.Member);
                break;
            }
            case TeamChangeType.LeaderChange:
            {
                var snap = DataMgr.Instance.UserData.RoleSnapReader.GetCache(change.UUID);
                if (change.UUID == DataMgr.Instance.UserData.RoleID)
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("team_be_captain"));
                }
                else
                {
                    GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetFormatString("team_change_captain", snap.Name));
                }

                Notify((int) NotiFyStatus.TeamUpdate, null);
                break;
            }
            case TeamChangeType.SettingChange:
                Notify((int) NotiFyStatus.TeamUpdate, null);
                break;
        }

        if (!HasTeam)
        {
            GameSceneMgr.Instance.UGUI.OnRockFingerUse -= UGUI_OnRockFingerUse;
            Notify((int) NotiFyStatus.TeamJoinOrLeave, null);
            if (TLBattleScene.Instance != null && TLBattleScene.Instance.Actor != null)
            {
                TLBattleScene.Instance.Actor.StopTeamFollow();
            }
        }

        if (change.Type == TeamChangeType.MemberKick || change.Type == TeamChangeType.MemberLeave)
        {
            if (MemberCount == 1)
            {
                var mapData = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
                if (Convert.ToInt32(mapData["team_type"]) == 1)
                {
                    IsKeepTeam();
                }
            }
        }

        if (HasTeam)
        {
            if (IsTeamFull)
            {
                DataMgr.Instance.MsgData.RemoveList(AlertMessageType.TeamApply);
            }

            DataMgr.Instance.MsgData.RemoveList(AlertMessageType.TeamInvite);
        }
    }

    private void UGUI_OnRockFingerUse(bool obj)
    {
        if (IsFollowLeader && obj)
        {
            RequestFollowLeader(false, null);
        }
    }

    public bool IsLeader()
    {
        return IsLeader(DataMgr.Instance.UserData.RoleID);
    }

    public bool IsLeader(string playerId)
    {
        if (HasTeam)
        {
            return mSnap.LeaderID == playerId;
        }

        return false;
    }

    public bool IsTeamMember(string uuid)
    {
        if (!string.IsNullOrEmpty(uuid) && mSnap.GetMember(uuid) != null)
        {
            return true;
        }

        return false;
    }

    private void IsKeepTeam()
    {
        string content = HZLanguageManager.Instance.GetString("team_onlyone");
        string ok = HZLanguageManager.Instance.GetString("common_keep");
        string cancel = HZLanguageManager.Instance.GetString("common_quit");
        GameAlertManager.Instance.AlertDialog.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, ok, cancel, null, null, (param) => { RequestLeaveTeam(null); });
    }


    public void RequestLeaveTeam(Action<bool> act)
    {
        TLNetManage.Instance.Request<TLClientLeaveTeamResponse>(new TLClientLeaveTeamRequest(), (ex, rp) =>
        {
            var ret = ex == null && rp.IsSuccess;
            if (ret)
            {
                //Notify((int) NotiFyStatus.TeamJoinOrLeave, null);
                //GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("team_leave"));
            }

            if (act != null)
            {
                act.Invoke(ret);
            }
        });
    }

    public void AttachObserver(IObserverExt<TeamData> ob)
    {
        mObservers.Add(ob);
    }

    public void DetachObserver(IObserverExt<TeamData> ob)
    {
        mObservers.Remove(ob);
    }

    public void AttachLuaObserver(string key, LuaTable t)
    {
        mLuaObservers[key] = t;
    }

    public void DetachLuaObserver(string key)
    {
        mLuaObservers.Remove(key);
    }

    public void Notify(int status, object opt)
    {
        foreach (var ob in mObservers)
        {
            ob.Notify(status, this, opt);
        }

        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] {(NotiFyStatus) status, this, ob.Value, opt});
        }
    }

    private readonly HashMap<string, bool> mSameSceneMap = new HashMap<string, bool>();

    public bool IsSameScene(string memberID)
    {
        return mSameSceneMap.Get(memberID);
    }

    public void Update(float deltaTime)
    {
        if (AllMembers != null && TLBattleScene.Instance != null)
        {
            foreach (var member in AllMembers)
            {
                var u = TLBattleScene.Instance.GetPlayerUnitByUUID(member.RoleID);
                var same = u != null;
                if (same != IsSameScene(member.RoleID))
                {
                    mSameSceneMap[member.RoleID] = same;
                    Notify((int) NotiFyStatus.MemberUpdate, member);
                }
            }
        }
    }


    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mSnap.SetInvalid();
            mSelf = null;
            mObservers.Clear();
            mLuaObservers.Clear();
        }

        if (reConnect)
        {
            mSnap.SetInvalid();
            mSelf = null;
            Notify((int) NotiFyStatus.TeamJoinOrLeave, null);
        }
    }
}