using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepCore;
using System;
using DeepMMO.Attributes;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{

    //获取列表信息
    [MessageType(Constants.TL_ISLAND_START + 1)]
    public class TLClientGetIslandInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_ISLAND_START + 2)]
    public class TLClientGetIslandInfoResponse : Response, ILogicProtocol
    {
        // checkpointId data
        public HashMap<int, ChekPointSnapData> PointSnapMap;

        public int FinallyCheckPointId;

        public int TimeLeft;
    }


    //获取列表信息
    [MessageType(Constants.TL_ISLAND_START + 3)]
    public class TLClientEnterIslandRequest : Request, ILogicProtocol
    {
        public int c2s_CheckPointId;
    }

    [MessageType(Constants.TL_ISLAND_START + 4)]
    public class TLClientEnterIslandResponse : ClientEnterDungeonResponse, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARG = 501;
        [MessageCode("今日次数已用完")] public const int TIME_OVER = 502;
        [MessageCode("请先通关前一章节")] public const int OPEN_LIMIT = 503;
    }


    //获取列表信息
    [MessageType(Constants.TL_ISLAND_START + 5)]
    public class TLClientGetFirstPassRewardRequest : Request, ILogicProtocol
    {
        public int c2s_CheckPointId;
    }

    [MessageType(Constants.TL_ISLAND_START + 6)]
    public class TLClientGetFirstPassRewardResponse : Response, ILogicProtocol
    {
        public int s2c_CheckPointId;
        public int state;

        [MessageCode("请先通关再来领取")] public const int CODE_PASS_FIRST = 501;
        [MessageCode("您已经领取过该奖励")] public const int CODE_AGAIN = 502; 
    }
}