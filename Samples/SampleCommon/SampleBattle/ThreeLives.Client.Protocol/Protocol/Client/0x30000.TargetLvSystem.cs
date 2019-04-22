using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using DeepCore;

namespace TLProtocol.Protocol.Client
{
    //获得目标系统物品
    [MessageType(TLConstants.TL_TargetLv + 1)]
    public class TLClientTargetLvGetItemRequest : Request, ILogicProtocol
    {
       public int c2s_lv;
    }

    [MessageType(TLConstants.TL_TargetLv + 2)]
    public class TLClientTargetLvGetItemResponse : Response, ILogicProtocol
    {
        [MessageCode("没有到达可领取等级")]
        public const int CODE_ERROR_LVNOTENOUGH = 501;
        [MessageCode("已经领取过奖励")]
        public const int CODE_ERROR_HASGOT = 502;
        [MessageCode("没有该等级段的奖励")]
        public const int CODE_ERROR_NOTHISREWARD = 503;
    }

    //目标等级变化
    [MessageType(TLConstants.TL_TargetLv + 3)]
    public class TLClientTargetLvChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_lv;
    }
    

}