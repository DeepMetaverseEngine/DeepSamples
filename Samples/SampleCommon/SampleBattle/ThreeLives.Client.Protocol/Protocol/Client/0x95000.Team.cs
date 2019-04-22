using System;
using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    public class TeamResponse : Response
    {
        [MessageCode("队伍不存在")] public const int CODE_ERROR_TEAM_NULL = 501;
        [MessageCode("该玩家已经下线了")] public const int CODE_ERROR_TEAM_OFFLINE = 502;
        [MessageCode("对方已经加入了别的队伍")] public const int CODE_ERROR_TEAM_TARGET_IN_TEAM = 503;
        [MessageCode("对方已经加入了你的队伍，无需再次邀请")] public const int CODE_ERROR_TEAM_TARGET_ALREADY_IN_TEAM = 504;
        [MessageCode("您已经在队伍中")] public const int CODE_ERROR_ALREADY_IN_TEAM = 505;
        [MessageCode("您当前不在队伍中")] public const int CODE_ERROR_TEAM_NOT_IN_TEAM = 506;
        [MessageCode("您离开了队伍")] public const int CODE_ERROR_TEAM_LEAVE = 507;
        [MessageCode("只有队长才能进行这项操作")] public const int CODE_ERROR_TEAM_NO_AUTHORITY = 508;
        [MessageCode("队伍人数已满")] public const int CODE_ERROR_TEAM_IS_FULL = 509;
        [MessageCode("您已提交过申请，无法再次申请")] public const int CODE_ERROR_TEAM_ALREADY_INVITATION = 511;
        [MessageCode("您已邀请过对方")] public const int CODE_ERROR_TEAM_ALREADY_INVITED = 512;
        [MessageCode("对方队伍已满，无法加入")] public const int CODE_ERROR_TEAM_TARGET_IS_FULL = 513;
        [MessageCode("对方没有在您的队伍中")] public const int CODE_ERROR_PLAYER_NOT_IN_TEAM = 514;
        [MessageCode("双方均有队伍，无法进行此项操作")] public const int CODE_ERROR_ALL_IN_TEAM = 515;
        [MessageCode("已经在同一队伍中")] public const int CODE_ERROR_SAME_TEAM = 516;
        [MessageCode("队长不能设置跟随")] public const int CODE_ERROR_LEADER_CANNOT_FOLLOW = 517;
        [MessageCode("您的等级未满足队伍条件")] public const int CODE_ERROR_LEVEL_LIMIT = 518;
        [MessageCode("您的战力未满足队伍条件")] public const int CODE_ERROR_FIGHTPOWRER_LIMIT = 519;
        [MessageCode("您已处于该匹配中")] public const int CODE_ERROR_ALREADY_IN_MATCH = 520;
        [MessageCode("没有该类型的匹配")] public const int CODE_ERROR_MATCH_NULL = 521;
        [MessageCode("您的等级未满足要求")] public const int CODE_ERROR_MATCH_LEVEL_LIMIT = 522;
        [MessageCode("没有该类型的目标")] public const int CODE_ERROR_TARGET_NULL = 524;
        [MessageCode("队伍人数过多，不支持该类型匹配")] public const int CODE_ERROR_TEAM_MEMBERCOUNT_LIMIT = 525;
        [MessageCode("死亡状态无法发起跟随邀请")] public const int CODE_ERROR_ASK_FOLLOW_LIMIT = 526;
        [MessageCode("死亡状态无法跟随")] public const int CODE_ERROR_FOLLOW_LIMIT = 527;
        [MessageCode("当前地图无法执行该操作")] public const int CODE_ERROR_MAP_LIMIT = 528;
        [MessageCode("该活动不在开放时间")] public const int CODE_ERROR_NOT_OPEING_TIME = 529;
        [MessageCode("消息已过期")] public const int CODE_ERROR_MSG_TIMEOUI = 530;
    }

    /// <summary>
    /// 设置跟随状态
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 1)]
    public class TLClientTeamFollowRequest : Request, ILogicProtocol
    {
        public bool c2s_open;
    }

    [MessageType(Constants.TL_TEAM_START + 2)]
    public class TLClientTeamFollowResponse : TeamResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 创建队伍
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 3)]
    public class TLClientCreateTeamRequest : Request, ILogicProtocol
    {
    }

    /// <summary>
    /// 创建队伍
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 4)]
    public class TLClientCreateTeamResponse : TeamResponse, ILogicProtocol
    {
        public string s2c_teamID;
    }

    /// <summary>
    /// 获取队伍简介
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 5)]
    public class ClientGetTeamSnapRequest : Request, ILogicProtocol
    {
        public string[] c2s_teamKeys;
    }

    [MessageType(Constants.TL_TEAM_START + 6)]
    public class ClientGetTeamSnapResponse : TeamResponse, ILogicProtocol
    {
        public TeamData[] s2c_teamList;
    }


    /// <summary>
    /// 离开队伍
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 7)]
    public class TLClientLeaveTeamRequest : Request, ILogicProtocol
    {
    }

    /// <summary>
    /// 离开队伍
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 8)]
    public class TLClientLeaveTeamResponse : TeamResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 改变队长
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 9)]
    public class TLClientChangeTeamLeaderRequest : Request, ILogicProtocol
    {
        public string c2s_leaderId;
    }

    /// <summary>
    /// 改变队长
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 10)]
    public class TLClientChangeTeamLeaderResponse : TeamResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// todo 支持单个选项设置
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 11)]
    public class ClientSetTeamRequest : Request
    {
        public TeamSetting c2s_settting;
    }

    [MessageType(Constants.TL_TEAM_START + 12)]
    public class ClientSetTeamResponse : TeamResponse
    {
    }

    /// <summary>
    /// 获取指定目标的队伍列表
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 13)]
    public class ClientGetTargetTeamRequest : Request, ILogicProtocol
    {
        public int c2s_point;
        public int c2s_target;
    }

    [MessageType(Constants.TL_TEAM_START + 14)]
    public class ClientGetTargetTeamResponse : TeamResponse, ILogicProtocol
    {
        public int s2c_point;
        public string[] s2c_teamList;
    }

    [MessageType(Constants.TL_TEAM_START + 15)]
    public class ClientAutoTeamRequest : Request, ILogicProtocol
    {
        public int c2s_target;
    }

    [MessageType(Constants.TL_TEAM_START + 16)]
    public class ClientAutoTeamResponse : TeamResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 邀请入队
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 17)]
    public class TLClientInviteTeamRequest : Request, ILogicProtocol
    {
        public string c2s_roleId;
        public string c2s_source;
    }

    /// <summary>
    /// 邀请入队
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 18)]
    public class TLClientInviteTeamResponse : TeamResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 通知客户端队伍信息变更
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 19)]
    public class TLClientTeamDataNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public TeamChange s2c_data;
    }


    [MessageType(Constants.TL_TEAM_START + 20)]
    public class ClientMatchStateNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public bool s2c_matching;
        public DateTime s2c_startUtc;
        public string s2c_channel;
        public string s2c_desc;
    }

    /// <summary>
    /// 踢人
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 21)]
    public class TLClientKickTeamMemberRequest : Request, ILogicProtocol
    {
        public string c2s_roleId;
    }

    /// <summary>
    /// 踢人
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 22)]
    public class TLClientKickTeamMemberResponse : TeamResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 玩家请求进入匹配
    /// </summary>
    [MessageType(Constants.TL_TEAM_START + 23)]
    public class ClientJoinMatchRequest : Request, ILogicProtocol
    {
        public string c2s_channel;
    }

    [MessageType(Constants.TL_TEAM_START + 24)]
    public class ClientJoinMatchResponse : TeamResponse, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_TEAM_START + 25)]
    public class ClientTeamAskFollowRequest : Request, ILogicProtocol
    {
        public string c2s_roleID;
    }

    [MessageType(Constants.TL_TEAM_START + 26)]
    public class ClientTeamAskFollowResponse : TeamResponse, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_TEAM_START + 27)]
    public class ClientGetRoleListRequest : Request, ILogicProtocol
    {
        public enum ListType
        {
            Friend = 1,
            Guild,
        }

        public byte c2s_type;
    }

    [MessageType(Constants.TL_TEAM_START + 28)]
    public class ClientGetRoleListResponse : TeamResponse, ILogicProtocol
    {
        public List<string> s2c_playerList;
    }
}