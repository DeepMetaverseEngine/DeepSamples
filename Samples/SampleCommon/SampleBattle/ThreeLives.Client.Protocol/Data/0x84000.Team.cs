using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using TLClient.Protocol;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_TEAM_START + 1)]
    public class TeamMember : ISerializable
    {
        public enum RoleState : byte
        {
            Normal,
            Offline,
            Dead,
        }

        public RoleState State
        {
            get => (RoleState) InnerState;
            set => InnerState = (byte) value;
        }

        public string RoleID;
        public string ServerGroupID;
        public string SessionName;
        public bool IsFollowLeader;
        public byte InnerState;
        public byte Pro;
        public byte Gender;
        public int Level;

        public TeamMember Clone()
        {
            return (TeamMember) MemberwiseClone();
        }
    }


    /// <summary>
    /// 队伍信息
    /// </summary>
    [MessageType(TLConstants.TL_TEAM_START + 2)]
    [PersistType]
    public class TeamData : ISerializable, IObjectMapping, IPublicSnap
    {
        [PersistField]
        public int MaxCount;
        [PersistField]
        public string TeamID;
        [PersistField]
        public string LeaderID;
        [PersistField]
        public string TeamServiceName;
        [PersistField]
        public TeamSetting Setting = new TeamSetting {TargetID = 1};
        [PersistField]
        public List<TeamMember> Members;

        public bool IsFull => MaxCount <= Members?.Count;
        public int MemberCount => Members?.Count ?? 0;
        [PersistField]
        public DateTime ExpiredUtc;
        public DateTime ExpiredUtcTime => ExpiredUtc;

        public const string StoreType = "Team:";
        public TeamData Clone()
        {
            var ret = (TeamData) MemberwiseClone();
            ret.Members = Members?.Select(m => m.Clone()).ToList();
            ret.Setting = Setting?.Clone();
            return ret;
        }


        public TeamMember GetMember(string roleID)
        {
            return Members?.Find(m => m.RoleID == roleID);
        }

        public bool ContainsMember(string roleID)
        {
            return Members?.Exists(m => m.RoleID == roleID) ?? false;
        }

        public void RemoveMember(string roleID)
        {
            Members?.RemoveAll(m => m.RoleID == roleID);
        }

        public bool IsInvalid => TeamID == null;

        public void SetInvalid()
        {
            TeamID = null;
            Members?.Clear();
            LeaderID = null;
            TeamServiceName = null;
        }


        public void ApplyChange(TeamChange change, string currentRoleID, ref TeamMember self)
        {
            switch (change.Type)
            {
                case TeamChangeType.Started:
                    Members = change.Data.Members;
                    MaxCount = change.Data.MaxCount;
                    TeamID = change.Data.TeamID;
                    Setting = change.Data.Setting;
                    LeaderID = change.Data.LeaderID;
                    TeamServiceName = change.Data.TeamServiceName;
                    self = GetMember(currentRoleID);
                    break;
                case TeamChangeType.Destroy:
                    SetInvalid();
                    break;
                case TeamChangeType.MemberIn:
                    if (Members == null)
                    {
                        Members = new List<TeamMember>();
                    }
                    Members.Add(change.Member);
                    break;
                case TeamChangeType.SettingChange:
                    Setting = change.Setting;
                    break;
                case TeamChangeType.LeaderChange:
                    LeaderID = change.UUID;
                    break;
                case TeamChangeType.MemberInfo:
                    var m = GetMember(change.Member.RoleID);
                    if (m != null)
                    {
                        m.IsFollowLeader = change.Member.IsFollowLeader;
                        m.State = change.Member.State;
                    }

                    break;
                case TeamChangeType.TeamServiceChange:
                    TeamServiceName = change.UUID;
                    break;
                default:
                    if (change.IsMemberOut)
                    {
                        RemoveMember(change.UUID);
                        if (change.UUID == currentRoleID)
                        {
                            self = null;
                            SetInvalid();
                        }
                    }

                    break;
            }
        }
    }

    public enum TeamChangeType : byte
    {
        Started = 1,
        Destroy = 2,
        MemberIn = 3,
        MemberLeave = 4,
        MemberKick = 5,
        MemberInfo = 6,
        LeaderChange = 7,
        SettingChange = 8,
        TeamServiceChange = 9,
    }

    [MessageType(TLConstants.TL_TEAM_START + 3)]
    public class TeamSetting : ISerializable, IStructMapping
    {
        /// <summary>
        /// 为1表示没有目标
        /// </summary>
        public int TargetID;

        /// <summary>
        /// 目标允许自动开始的情况下此值为true自动开始
        /// </summary>
        public bool AutoStartTarget = true;

        public int MinLevel;
        public int MinFightPower;
        public bool AutoMatch;

        public TeamSetting Clone()
        {
            return (TeamSetting) MemberwiseClone();
        }
    }

    [MessageType(TLConstants.TL_TEAM_START + 4)]
    public class TeamChange : ISerializable
    {
        public TeamChangeType Type
        {
            get => (TeamChangeType) SourceType;
            set => SourceType = (byte) value;
        }

        public byte SourceType;
        public string UUID;

        public bool IsMemberCountChange => Type == TeamChangeType.Started ||
                                           Type == TeamChangeType.Destroy ||
                                           Type == TeamChangeType.MemberIn ||
                                           Type == TeamChangeType.MemberKick ||
                                           Type == TeamChangeType.MemberLeave;

        public bool IsMemberOut => Type == TeamChangeType.MemberKick ||
                                   Type == TeamChangeType.MemberLeave;


        public TeamData Data;
        public TeamMember Member;
        public TeamSetting Setting;
    }
}