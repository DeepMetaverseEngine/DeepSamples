using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using System;
using System.Collections.Generic;
using TLBattle.Message;
using TLBattle.Common.Plugins;

namespace TLBattle.Client
{
    /// <summary>
    /// 客户端中间层.
    /// </summary>
    public class TLClientVirtual_Player : TLClientVirtual
    {
        private PlayerVisibleDataB2C mData = null;

        private Action<TLUnitBaseInfo> mOnBaseInfoChanged;
        private Action<int> mOnPKLevelChanged;
        private Action<int> mOnPKValueChanged;
        private System.Action mOnRevengeListChange;
        private System.Action mOnGuildChaseMapChange;
        private Action<int> mOnPracticeLvChanged;
        private Action<int, string> mOnTitleIdChanged;


        private HashMap<string, int> RevengeMap;
        private HashMap<string, TeamMemberSnap> TeamMap;

        private HashMap<string, byte> GuildChaseMap;

        private bool mActor;

        public event Action<TLUnitBaseInfo> OnBaseInfoChanged
        {
            add { mOnBaseInfoChanged += value; }
            remove { mOnBaseInfoChanged -= value; }
        }

        public event Action<int> OnPKLevelChanged
        {
            add { mOnPKLevelChanged += value; }
            remove { mOnPKLevelChanged -= value; }
        }

        public event Action<int> OnPKValueChanged
        {
            add { mOnPKValueChanged += value; }
            remove { mOnPKValueChanged -= value; }
        }

        public event Action<int> OnPracticeLvChanged
        {
            add { mOnPracticeLvChanged += value; }
            remove { mOnPracticeLvChanged -= value; }
        }

        public event Action<int, string> OnTitleIdChanged
        {
            add { mOnTitleIdChanged += value; }
            remove { mOnTitleIdChanged -= value; }
        }

        private event Action<bool> mOnRidingStatusChaned;
        /// <summary>
        /// 坐骑状态变更.
        /// </summary>
        public event Action<bool> OnRidingStatusChaned
        {
            add { mOnRidingStatusChaned += value; }
            remove { mOnRidingStatusChaned -= value; }
        }

        private bool mIsGuard;
        private event Action<bool> mGuardStatusChanged;
        /// <summary>
        /// 自动状态变更.
        /// </summary>
        public event Action<bool> GuardStatusChanged
        {
            add { mGuardStatusChanged += value; }
            remove { mGuardStatusChanged -= value; }
        }

        public event System.Action OnRevengeListChanged
        {
            add { mOnRevengeListChange += value; }
            remove { mOnRevengeListChange -= value; }
        }

        public event System.Action OnGuildChaseMapChanged
        {
            add { mOnGuildChaseMapChange += value; }
            remove { mOnGuildChaseMapChange -= value; }
        }

        public string TeamUUID { private set; get; }

        public string TeamLeaderUUID { private set; get; }

        protected override void DoInit(ZoneUnit owner)
        {
            base.DoInit(owner);
            mActor = owner is ZoneActor;
            mData = owner.SyncInfo.VisibleInfo as PlayerVisibleDataB2C;
            mIsGuard = mData.BaseInfo.IsGuard;
            InitGuildChaseList(mData.BaseInfo.GuildChaseList);
            SetTeamInfo(mData.BaseInfo.list, mData.BaseInfo.teamLeaderUUID, mData.BaseInfo.teamUUID);
        }

        protected override void DoDispose(ZoneUnit owner)
        {
            mOnBaseInfoChanged = null;
            mOnPKLevelChanged = null;
            mOnPKValueChanged = null;
            mOnRidingStatusChaned = null;
            mOnRevengeListChange = null;
            mGuardStatusChanged = null;
            mOnGuildChaseMapChange = null;

            base.DoDispose(owner);
        }

        protected override void MOwner_OnDoEvent(ZoneObject obj, ObjectEvent e)
        {
            if (e is PlayerBaseInfoChangeEventB2C)
                OnReceiveBaseInfoChange(e as PlayerBaseInfoChangeEventB2C);
            else if (e is PKModeChangeEventB2C)
                OnReceivePKModeChange(e as PKModeChangeEventB2C);
            else if (e is PKInfoChangeEventB2C)
                OnReceivePKLevelChange(e as PKInfoChangeEventB2C);
            else if (e is GuardStatusChangeEventB2C)
                OnReceiveGuardStatusChange(e as GuardStatusChangeEventB2C);
            else if (e is TeamMemberListChangeEvtB2C)
                OnReceiveTeamMemberListChangeEvtB2C(e as TeamMemberListChangeEvtB2C);
            else if (e is RevengeListChangeEvtB2C)
                OnReceiveRevengeListChangeEvtB2C(e as RevengeListChangeEvtB2C);
            else if (e is ForceChangeEventB2C)
                OnReceiveForceChangeEventB2C(e as ForceChangeEventB2C);
            else if (e is PKValueChangeEventB2C)
                OnReceivePKValueChangeEventB2C(e as PKValueChangeEventB2C);
            else if (e is PlayerPropChangeB2C)
                OnReceivePlayerPropChangeB2C(e as PlayerPropChangeB2C);
            else if (e is PlayerGuildChaseListChangeB2C)
                OnReceivePlayerGuildChaseListChangeB2C(e as PlayerGuildChaseListChangeB2C);

            base.MOwner_OnDoEvent(obj, e);
        }

        public override int GetLv()
        {
            if (mData != null)
            {
                return mData.BaseInfo.UnitLv;
            }
            else
            {
                return mOwner.Level;
            }
        }

        public override string GetName()
        {
            if (mData != null)
            {
                return mData.BaseInfo.Name;
            }
            else
            {
                return mOwner.Name;
            }
        }

        public bool IsTeamMember(TLClientVirtual_Player v)
        {
            return IsTeamMember(v.mOwner.PlayerUUID);
        }

        public bool IsTeamMember(string uuid)
        {
            if (TeamMap == null) return false;
            return TeamMap.ContainsKey(uuid);
        }

        public bool InRevengeList(TLClientVirtual_Player v)
        {
            if (RevengeMap == null) return false;
            return RevengeMap.ContainsKey(v.mOwner.PlayerUUID);
        }

        protected bool IsGuildMember(TLClientVirtual_Player v)
        {
            string id1 = this.GuildUUID();
            string id2 = v.GuildUUID();
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2))
                return false;

            return (id1 == id2);
        }

        public PKInfo.PKMode GetPKMode()
        {
            if (mData != null)
            {
                return mData.UnitPKInfo.CurPKMode;
            }

            return PKInfo.PKMode.Peace;
        }

        public int GetPKLevel()
        {
            if (mData != null)
            {
                return mData.UnitPKInfo.CurPKLevel;
            }

            return 0;
        }

        public uint GetPKColor()
        {
            if (mData != null)
            {
                return mData.UnitPKInfo.CurColor;
            }

            return 0;
        }

        public int GetPKValue()
        {
            if (mData != null)
            {
                return mData.UnitPKInfo.CurPKValue;
            }

            return 0;
        }

        public bool IsRedName()
        {
            if (mData != null)
                return mData.UnitPKInfo.CurPKLevel > 1;

            return false;
        }

        public override bool IsEnemy(ZoneUnit target)
        {
            if (target == mOwner) { return false; }

            var v = target.Virtual as TLClientVirtual;

            //玩家单位判断，后续还有宠物召唤怪.
            if (v is TLClientVirtual_Player)
            {
                var pv = v as TLClientVirtual_Player;
                var mode = GetPKMode();

                if (!IsTeamMember(pv) && InGuildChaseMap(pv))
                    return true;

                switch (mode)
                {
                    case PKInfo.PKMode.Peace:
                        if (target == mOwner)
                            return false;
                        return (base.IsEnemy(target));
                    case PKInfo.PKMode.Guild://不是队伍、公会成员.
                        if (IsTeamMember(pv) || IsGuildMember(pv))
                            return false;
                        return true;
                    case PKInfo.PKMode.Team:
                    case PKInfo.PKMode.All:
                        return !IsTeamMember(pv);
                    case PKInfo.PKMode.Revenger:
                        if (IsTeamMember(pv))
                            return false;
                        return (InRevengeList(pv));
                    case PKInfo.PKMode.Justice:
                        if (IsTeamMember(pv))
                            return false;
                        return pv.IsRedName();
                    default:
                        return true;
                }
            }
            else
            {
                return base.IsEnemy(target);
            }
        }

        public override bool IsAllies(ZoneUnit target)
        {
            var v = target.Virtual as TLClientVirtual;

            //玩家单位判断，后续还有宠物召唤怪.
            if (v is TLClientVirtual_Player)
            {
                var pv = v as TLClientVirtual_Player;
                var mode = GetPKMode();

                switch (mode)
                {
                    case PKInfo.PKMode.Peace:
                        if (target == mOwner)
                            return true;
                        else if (base.IsAllies(target) && !pv.IsRedName())
                            return true;
                        return false;
                    case PKInfo.PKMode.Guild://不是队伍、公会成员.
                        if (IsTeamMember(pv) || IsGuildMember(pv))
                            return true;
                        return false;
                    case PKInfo.PKMode.Team:
                    case PKInfo.PKMode.All:
                    case PKInfo.PKMode.Revenger:
                        return IsTeamMember(pv);
                    default:
                        return false;
                }

            }
            else
            {
                return base.IsAllies(target);
            }
        }

        public bool IsGuard()
        {
            if (mData != null && mData.BaseInfo != null)
                return mData.BaseInfo.IsGuard;
            return false;
        }

        public string GuildName()
        {
            if (mData != null && mData.BaseInfo != null)
                return mData.BaseInfo.GuildName;
            return null;
        }

        public string GuildUUID()
        {
            if (mData != null && mData.BaseInfo != null)
                return mData.BaseInfo.GuildUUID;
            return null;
        }

        public bool IsRiding()
        {
            return (this.mOwner.Dummy_1 == 1);
        }

        public uint CurAtkTarget()
        {
            return this.mOwner.CurrentTarget;
        }

        public TLUnitBaseInfo.ProType RolePro()
        {
            if (mData != null && mData.BaseInfo != null)
            {
                return mData.BaseInfo.RolePro;
            }

            return TLUnitBaseInfo.ProType.None;
        }

        public TLUnitBaseInfo.GenderType RoleGender()
        {
            if (mData != null && mData.BaseInfo != null)
            {
                return mData.BaseInfo.Gender;
            }
            return TLUnitBaseInfo.GenderType.MALE;
        }

        public int PracticeLv()
        {
            if (mData != null)
                return mData.BaseInfo.PracticeLv;

            return 0;
        }

        public bool HasMonsterOwnerShip(string playerUUID, int range)
        {
            if (playerUUID == null) return true;
            if (mData == null) return false;
            if (playerUUID == this.mOwner.PlayerUUID) return true;
            if (TeamMap != null && TeamMap.ContainsKey(playerUUID))
            {
                var player = this.mOwner.Parent.GetPlayerUnit(playerUUID);
                //玩家不在场景内,没有归属权.
                if (player == null) return false;
                if (range <= 0) return true;
                var dis = CMath.getDistance(player.X, player.Y, mOwner.X, mOwner.Y);
                //超出有效距离，没有归属权.
                if (dis > range) return false;
                return true;
            }
            return false;
        }

        public int TitleID()
        {
            if (mData != null)
                return mData.BaseInfo.TitleID;

            return 0;
        }

        public string TitleNameExt()
        {
            if (mData != null)
                return mData.BaseInfo.TitleNameExt;

            return "";
        }

        private void SetTeamInfo(List<TeamMemberSnap> list, string teamLeaderUUID, string teamUUID)
        {
            if (TeamMap == null) TeamMap = new HashMap<string, TeamMemberSnap>();
            else { TeamMap.Clear(); }

            this.TeamUUID = teamUUID;
            this.TeamLeaderUUID = teamLeaderUUID;

            if (list == null) return;

            for (int i = 0; i < list.Count; i++)
            {
                TeamMap.Put(list[i].UUID, list[i]);
            }
        }

        private void InitGuildChaseList(List<string> lt)
        {
            if (GuildChaseMap == null)
                GuildChaseMap = new HashMap<string, byte>();
            else
                GuildChaseMap?.Clear();

            if (lt == null || lt.Count == 0) return;

            for (int i = 0; i < lt.Count; i++)
            {
                GuildChaseMap.Put(lt[i], 0);
            }
        }

        public bool InGuildChaseMap(TLClientVirtual_Player v)
        {
            if (mData == null || string.IsNullOrEmpty(mData.BaseInfo.GuildUUID)) return false;
            if (v == null || GuildChaseMap == null || string.IsNullOrEmpty(v.GuildUUID())) return false;

            return GuildChaseMap.ContainsKey(v.GuildUUID());
        }

        public bool InGuildChase()
        {
            if (mData != null && mData.BaseInfo.GuildChaseList != null)
            {
                return (mData.BaseInfo.GuildChaseList.Count > 0);
            }

            return false;
        }

        #region 协议接受.

        private void OnReceiveBaseInfoChange(PlayerBaseInfoChangeEventB2C evt)
        {
            if (mData != null)
            {
                int lv = mData.BaseInfo.UnitLv;

                mData.BaseInfo = evt.info;

                if (mData.BaseInfo.UnitLv != lv && mActor)
                {
                    mOwner.DoLevelUp();
                }

                if (mOnBaseInfoChanged != null)
                {
                    mOnBaseInfoChanged.Invoke(mData.BaseInfo);
                }

                if (mIsGuard != mData.BaseInfo.IsGuard)
                {
                    mIsGuard = mData.BaseInfo.IsGuard;
                    if (mGuardStatusChanged != null)
                        mGuardStatusChanged.Invoke(mIsGuard);
                }
            }
        }

        private void OnReceivePKModeChange(PKModeChangeEventB2C evt)
        {
            if (mData != null)
            {
                mData.UnitPKInfo.CurPKMode = evt.mode;
            }
        }

        private void OnReceivePKLevelChange(PKInfoChangeEventB2C evt)
        {
            if (mData != null)
            {
                mData.UnitPKInfo.CurPKLevel = evt.b2c_level;
                mData.UnitPKInfo.CurColor = evt.b2c_color;
                mData.UnitPKInfo.CurPKValue = evt.b2c_pkvalue;

                if (mOnPKLevelChanged != null)
                    mOnPKLevelChanged.Invoke(evt.b2c_level);
            }
        }

        private void OnReceiveGuardStatusChange(GuardStatusChangeEventB2C evt)
        {
            if (mData != null && mData.BaseInfo != null)
            {
                mData.BaseInfo.IsGuard = evt.b2c_guard;
            }
        }

        private void OnReceiveTeamMemberListChangeEvtB2C(TeamMemberListChangeEvtB2C evt)
        {
            SetTeamInfo(evt.list, evt.teamLeaderUUID, evt.teamUUID);
        }

        private void OnReceiveRevengeListChangeEvtB2C(RevengeListChangeEvtB2C evt)
        {
            if (RevengeMap == null) RevengeMap = new HashMap<string, int>();
            else { RevengeMap.Clear(); }

            if (evt.list != null)
            {
                for (int i = 0; i < evt.list.Count; i++)
                {
                    RevengeMap.Put(evt.list[i], 0);
                }
            }

            if (mOnRevengeListChange != null)
                mOnRevengeListChange.Invoke();
        }

        private void OnReceiveForceChangeEventB2C(ForceChangeEventB2C evt)
        {
            mOwner.SyncInfo.Force = evt.Force;
        }

        private void OnReceivePKValueChangeEventB2C(PKValueChangeEventB2C evt)
        {
            if (mData != null)
            {
                mData.UnitPKInfo.CurPKValue = evt.b2c_pkvalue;

                if (mOnPKLevelChanged != null)
                    mOnPKLevelChanged.Invoke(evt.b2c_pkvalue);
            }
        }

        private void OnReceivePlayerPropChangeB2C(PlayerPropChangeB2C evt)
        {
            if (mData != null)
            {
                if (evt.changes == null)
                    return;
                if (evt.changes.ContainsKey(PlayerPropChangeB2C.PracticeLv))
                {
                    int v = evt.changes.Get(PlayerPropChangeB2C.PracticeLv);
                    mData.BaseInfo.PracticeLv = v;
                    if (mOnPracticeLvChanged != null)
                        mOnPracticeLvChanged.Invoke(v);
                }

                string titleNameExt = "";
                if (evt.changeExts != null && evt.changeExts.ContainsKey(PlayerPropChangeB2C.TitleNameExt))
                {
                    titleNameExt = evt.changeExts.Get(PlayerPropChangeB2C.TitleNameExt);
                    mData.BaseInfo.TitleNameExt = titleNameExt;
                }

                if (evt.changes.ContainsKey(PlayerPropChangeB2C.TitleID))
                {
                    int v = evt.changes.Get(PlayerPropChangeB2C.TitleID);
                    mData.BaseInfo.TitleID = v;
                    if (mOnTitleIdChanged != null)
                        mOnTitleIdChanged.Invoke(v, titleNameExt);
                }

            }
        }

        private void OnReceivePlayerGuildChaseListChangeB2C(PlayerGuildChaseListChangeB2C evt)
        {
            if (mData != null && mData.BaseInfo != null)
                mData.BaseInfo.GuildChaseList = evt.s2c_list;

            InitGuildChaseList(evt.s2c_list);

            if (mOnGuildChaseMapChange != null)
                mOnGuildChaseMapChange.Invoke();
        }

        #endregion
    }

}
