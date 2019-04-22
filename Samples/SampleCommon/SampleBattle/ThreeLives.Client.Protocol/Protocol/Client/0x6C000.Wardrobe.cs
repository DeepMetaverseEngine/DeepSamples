using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;
using DeepMMO.Protocol.Client;
using DeepMMO.Protocol;
using TLProtocol.Data;
using DeepMMO.Attributes;
using System;

namespace TLProtocol.Protocol.Client
{

    [MessageType(Constants.TL_WARDROBE_START + 1)]
    public class TLClientGetWardrobeDataRequest : Request, ILogicProtocol
    {
     
    }

    [MessageType(Constants.TL_WARDROBE_START + 2)]
    public class TLClientGetWardrobeDataResponse : Response, ILogicProtocol
    {
        public int s2c_Score;

        public int s2c_Level;

        public HashMap<int, DateTime> s2c_dayMap;

        //groupId
        public HashMap<int,int> s2c_equipMap;
    }

    [MessageType(Constants.TL_WARDROBE_START + 3)]
    public class TLClientGetWardrobeInfoRequest : Request, ILogicProtocol
    {
        public int c2s_avatarType;
    }

    [MessageType(Constants.TL_WARDROBE_START + 4)]
    public class TLClientGetWardrobeInfoResponse : Response, ILogicProtocol
    {
        public HashMap<int, RequireInfo> s2c_requireMap;
    }


    [MessageType(Constants.TL_WARDROBE_START + 5)]
    public class TLClientBuyAvatarRequest : Request, ILogicProtocol
    {
        public int c2s_avatarId;
    }

    [MessageType(Constants.TL_WARDROBE_START + 6)]
    public class TLClientBuyAvatarResponse : Response, ILogicProtocol
    {
        // avatarId,天数
        public HashMap<int, DateTime> s2c_dayMap;

        public int s2c_Score;

        [MessageCodeAttribute("参数错误，没有这个时装")]
        public const int CODE_ARG_ID_ERROR = 501;
        [MessageCodeAttribute("已经有永久时装，无需重复购买")]
        public const int CODE_AGAIN_ERROR = 502;
    }

    [MessageType(Constants.TL_WARDROBE_START + 7)]
    public class TLClientWardrobeLevelUpRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_WARDROBE_START + 8)]
    public class TLClientWardrobeLevelUpResponse : Response, ILogicProtocol
    {
        public int s2c_Score;

        public int s2c_Level;

        [MessageCode("积分不足不能升级")]
        public const int CODE_SCORE_NOT_ENOUGH = 501;
 
    }

    [MessageType(Constants.TL_WARDROBE_START + 9)]
    public class TLClientWardrobeEquipRequest : Request, ILogicProtocol
    {
        public int c2s_takeOff;
        public int c2s_avatarId;
    }


    [MessageType(Constants.TL_WARDROBE_START + 10)]
    public class TLClientWardrobeEquipResponse : Response, ILogicProtocol
    {
        public int s2c_avatarId;
        [MessageCodeAttribute("参数错误，没有这个时装")]
        public const int CODE_ARG_ID_ERROR = 501;
        [MessageCodeAttribute("时装已过期")]
        public const int CODE_TIMEOUT = 502;
    }
}
