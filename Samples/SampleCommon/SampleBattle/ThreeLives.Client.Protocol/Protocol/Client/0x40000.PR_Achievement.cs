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
   
    [MessageType(TLConstants.TL_PlayRule + 510)]
    public class TLClientAchievementCompletedNotify : Notify, ILogicProtocol,INetProtocolS2C
    {
        public List<AchievementDataSnap> s2c_data;
        public bool ShowTips;//是否显示提示框 
    }
    
    
  
    [MessageType(TLConstants.TL_PlayRule + 512)]
    public class TLClientGetAchievementRewardRequest : Request, ILogicProtocol
    {
        public int c2s_id;
    }
    [MessageType(TLConstants.TL_PlayRule + 513)]
    public class TLClientGetAchievementRewardReponse : Response, ILogicProtocol
    {
        public List<AchievementReward> s2c_data;
        [MessageCode("无效的成就")] public const int CODE_ERRDATA = 501;
        [MessageCode("奖励已经领取")] public const int CODE_ERRGET = 502;
    }
    
    
    [MessageType(TLConstants.TL_PlayRule + 514)]
    public class TLClientAchievementListRequest : Request, ILogicProtocol
    {
        //类型 传0推送目录数据
        public int c2s_type;
    }
    [MessageType(TLConstants.TL_PlayRule + 515)]
    public class TLClientAchievementListReponse : Response, ILogicProtocol
    {
        public List<AchievementDataSnap> s2c_data;
        public List<CatalogData> s2c_catalogdata;//c2s_type == 0 传所有目录数据 否则传当前目录完成数
        public int s2c_curFinishPoints;//当前成就点
        public int s2c_totalFinishPoints;//总成就数
    }
} 