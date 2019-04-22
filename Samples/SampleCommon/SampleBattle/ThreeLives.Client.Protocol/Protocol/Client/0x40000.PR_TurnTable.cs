using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using DeepCore;
using System;
using TLClient.Protocol.Modules;

namespace TLProtocol.Protocol.Client
{
   
    [MessageType(TLConstants.TL_PlayRule + 710)]
    public class TLClientGetTurnTableRequest : Request, ILogicProtocol
    {
        public int c2s_type;
    }
    [MessageType(TLConstants.TL_PlayRule + 711)]
    public class TLClientGetTurnTableReponse : Response, ILogicProtocol
    {
        public List<TurnTableReward> s2c_data;
        public int s2c_times;
        public List<TurnTableGotReward> s2c_RewardList;//已领取奖励的列表
        [MessageCode("次数已达上限")] public const int CODE_ERRDATA = 501;
        [MessageCode("消耗道具不足")] public const int CODE_NEEDITEMNOTENOUGH = 502;
        [MessageCode("不在活动时间内")] public const int CODE_DATEVAILD = 503;
    }
    
    
    [MessageType(TLConstants.TL_PlayRule + 712)]
    public class TLClientGetTurnTableInfoRequest : Request, ILogicProtocol
    {
        public int c2s_type;
    }
    [MessageType(TLConstants.TL_PlayRule + 713)]
    public class TLClientGetTurnTableInfoReponse : Response, ILogicProtocol
    {
        public int s2c_times;//获得当前转盘的次数
        public List<TurnTableGotReward> s2c_RewardList;//已领取奖励的列表
    }
    
    [MessageType(TLConstants.TL_PlayRule + 714)]
    public class TLClientExchargeTurnTableRequest : Request, ILogicProtocol
    {
        public int c2s_type;//活动类型
        public int c2s_index;//领取索引
    }
    [MessageType(TLConstants.TL_PlayRule + 715)]
    public class TLClientExchargeTurnTableReponse : Response, ILogicProtocol
    {
        public List<TurnTableReward> s2c_data;//奖励表
        public int s2c_index;//奖励索引
        [MessageCode("已经领取过的奖励")] public const int CODE_HAVEGOTTEN = 501;
    }
    
} 