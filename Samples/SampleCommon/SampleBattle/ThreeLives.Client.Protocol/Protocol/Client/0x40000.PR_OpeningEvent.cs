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
   
    [MessageType(TLConstants.TL_PlayRule + 610)]
    public class TLClientOpeningEventCompletedNotify : Notify, ILogicProtocol,INetProtocolS2C
    {
        public List<OpeningEventDataSnap> s2c_data;
        public bool ShowTips;//是否显示提示框 
    }
    
    [MessageType(TLConstants.TL_PlayRule + 612)]
    public class TLClientGetOpeningEventRewardRequest : Request, ILogicProtocol
    {
        public int c2s_id;
    }
    [MessageType(TLConstants.TL_PlayRule + 613)]
    public class TLClientGetOpeningEventRewardReponse : Response, ILogicProtocol
    {
        public List<OpeningEventReward> s2c_data;
        public int s2c_curFinishPoints;//当前开服活动积分
        public HashMap<int, int> exchargeDataMap;
        [MessageCode("无效的活动")] public const int CODE_ERRDATA = 501;
        [MessageCode("奖励已经领取")] public const int CODE_ERRGET = 502;
        [MessageCode("活动已过期")] public const int CODE_ERRPASS = 503;
    }
    
    
    [MessageType(TLConstants.TL_PlayRule + 614)]
    public class TLClientOpeningEventListRequest : Request, ILogicProtocol
    {
        //类型 传0推送目录数据
        public int c2s_type;
    }
    [MessageType(TLConstants.TL_PlayRule + 615)]
    public class TLClientOpeningEventListReponse : Response, ILogicProtocol
    {
        public List<OpeningEventDataSnap> s2c_data;
        public int s2c_day;//当前天数
        public int s2c_curFinishPoints;//当前开服活动积分
        public HashMap<int, int> exchargeDataMap;
    }
    
    
    [MessageType(TLConstants.TL_PlayRule + 616)]
    public class TLClientOpeningEventExchargeRequest : Request, ILogicProtocol
    {
        public int c2s_id;
    }
    [MessageType(TLConstants.TL_PlayRule + 617)]
    public class TLClientOpeningEventExchargeReponse : Response, ILogicProtocol
    {
        public List<OpeningEventReward> s2c_data;
        public int s2c_curFinishPoints;//当前开服活动积分
        public HashMap<int, int> exchargeDataMap;
        [MessageCode("无效的活动")] public const int CODE_ERRDATA = 501;
        [MessageCode("奖励已经领取")] public const int CODE_ERRGET = 502;
        [MessageCode("活动已过期")] public const int CODE_ERRPASS = 503;
    }
} 