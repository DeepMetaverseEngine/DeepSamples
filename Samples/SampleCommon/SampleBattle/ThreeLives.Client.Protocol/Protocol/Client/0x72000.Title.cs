using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepCore;
using DeepMMO.Attributes;
using TLProtocol.Data;
using System;

namespace TLProtocol.Protocol.Client
{

    [MessageType(Constants.TL_TITLE_START + 1)]
    public class TLClientGetTitleInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_TITLE_START + 2)]
    public class TLClientGetTitleInfoResponse : Response, ILogicProtocol
    {
        public int s2c_equipTitle = 0;

        public int s2c_equipAttr = 0;

        public HashMap<int, DateTime> s2c_titleMap;
    }

    [MessageType(Constants.TL_TITLE_START + 3)]
    public class TLClientEquiTitleRequest : Request, ILogicProtocol
    {
        public int c2s_equipTitleId = 0;
    }


    [MessageType(Constants.TL_TITLE_START + 4)]
    public class TLClientEquiTitleResponse : Response, ILogicProtocol
    {
        public int s2c_equipTitleId = 0;

        [MessageCode("参数错误,没有这个称号")] public const int CODE_ARG = 501;
        [MessageCode("尚未获得该称号")] public const int NO_TITLE = 502;
        [MessageCode("称号已过期")] public const int TIME_OVER = 503;
        [MessageCode("你没有装备称号，无需卸下")] public const int NO_EQUIPTITLE = 504;
        [MessageCode("你已经装备该称号")] public const int EQUIP_REPEAT = 505;
    }

    [MessageType(Constants.TL_TITLE_START + 5)]
    public class TLClientEquiTitleAttrRequest : Request, ILogicProtocol
    {
        public int c2s_equipTitleAttrId = 0;
      
    }


    [MessageType(Constants.TL_TITLE_START + 6)]
    public class TLClientEquiTitleAttrResponse : Response, ILogicProtocol
    {
        public int s2c_equipTitleAttrId = 0;
        [MessageCode("参数错误,没有这个称号")] public const int CODE_ARG = 501;
        [MessageCode("尚未获得该称号")] public const int NO_TITLE = 502;
        [MessageCode("称号已过期")] public const int TIME_OVER = 503;
        [MessageCode("你没有装备任何属性，无需卸下")] public const int NO_EQUIPTITLE = 504;
        [MessageCode("你已经装备该属性")] public const int EQUIP_REPEAT = 505;
    }


    [MessageType(Constants.TL_TITLE_START + 7)]
    public class TLClientGetTitleNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_titleId;
        public string s2c_nameExt;

    }

    
}