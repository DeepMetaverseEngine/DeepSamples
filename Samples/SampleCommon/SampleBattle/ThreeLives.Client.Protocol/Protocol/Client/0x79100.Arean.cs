using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using TLProtocol.Data;
using DeepCore;
using ThreeLives.Client.Protocol.Data;
using System.Collections.Generic;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 命石抽奖
    /// </summary>
    [MessageType(Constants.TL_Arean + 1)]
    public class ClientGetPlayersByNameRequest : Request, ILogicProtocol
    {
        public string c2s_filter;
    }

    [MessageType(Constants.TL_Arean + 2)]
    public class ClientGetPlayersByNameResponse : Response, ILogicProtocol
    { 
        public List<string> s2c_roleIdList;

        [MessageCodeAttribute("参数错误")]
        public const int CODE_ARG_ERROR = 501;
    }

  
    [MessageType(Constants.TL_Arean + 3)]
    public class ClientApplyChallengeRequest : Request, ILogicProtocol
    {
        //被挑战者ID
        public string c2s_beChallengerId;

        public int c2s_challengeType;

        public int c2s_challengeGold;

        public string c2s_message;
    }

    [MessageType(Constants.TL_Arean + 4)]
    public class ClientApplyChallengeResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("参数错误")]
        public const int CODE_ARG_ERROR = 501;

        [MessageCodeAttribute("当前有其他人在进行挑战，请稍后再试")]
        public const int CODE_LIMITING = 502;
        [MessageCodeAttribute("今日挑战次数已达5次，请稍作休息，明日再发起挑战")]
        public const int CODE_TIME_LIMIT = 503;
        [MessageCodeAttribute("发起擂台挑战需等级达到30级")]
        public const int CODE_LEVEL_LIMIT = 504;
        [MessageCodeAttribute("组队状态下无法发起挑战")]
        public const int CODE_TEAM_LIMIT = 505;
        [MessageCodeAttribute("您当前正处于匹配中，无法发起挑战")]
        public const int CODE_INMATCH_LIMIT = 506;


        [MessageCodeAttribute("没有找到目标用户，请检查输入昵称是否正确")]
        public const int CODE_AIM_ERROR = 507;
        [MessageCodeAttribute("您要挑战的玩家目前等级不足30级，无法挑战")]
        public const int CODE_LEVEL_LIMIT2 = 508;
        [MessageCodeAttribute("你与目标等级差距过大，胜之不武，无法挑战")]
        public const int CODE_LEVEL_LIMIT3 = 509;
        [MessageCodeAttribute("对方不在线")]
        public const int CODE_NOT_ONLINE = 510;

        [MessageCodeAttribute("目标玩家目前正忙，不方便接受挑战，请稍后再试。")]
        public const int CODE_AIM_BUSY = 511;

    } 

    //回应挑战
    [MessageType(Constants.TL_Arean + 5)]
    public class ClientReplyChallengeRequest : Request, ILogicProtocol
    {
        //0 拒绝  1.答应 2.其他
        public int c2s_opt;
    }

    [MessageType(Constants.TL_Arean + 6)]
    public class ClientReplyChallengeResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("挑战已过期")]
        public const int CODE_TIME_OVER = 501;
    }


    //其他人答应或者是拒绝你的挑战
    [MessageType(Constants.TL_Arean + 7)]
    public class ClientChallengeNotify : Notify
    {
        //客户端通知
        public int s2c_opt;


    }

    //收到被挑战通知
    [MessageType(Constants.TL_Arean + 8)]
    public class ClientBeChallengeNotify : Notify
    {
        public string s2c_challengerId;
        public int s2c_challengeType;
        public int s2c_challengeGold;
        public string s2c_challengeMessage;
        public int s2c_timeLeft;
    }

    [MessageType(Constants.TL_Arean + 9)]
    public class ClientGetArenaInfoRequest : Request, ILogicProtocol
    {
        
    }

    [MessageType(Constants.TL_Arean + 10)]
    public class ClientGetArenaInfoResponse : Response, ILogicProtocol
    {
        public int s2c_status;   //0,空闲  1.战斗中
        
    }

    [MessageType(Constants.TL_Arean + 11)]
    public class ClientWatchArenaRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_Arean + 12)]
    public class ClientWatchArenaResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("比赛尚未开始，请稍后再试")]
        public const int CODE_NOT_OPEN = 501;

        [MessageCodeAttribute("参赛人员尚未入场，请稍后再试")]
        public const int CODE_NOT_JOIN = 502;
    }

}