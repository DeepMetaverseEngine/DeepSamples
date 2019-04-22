using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepCore;
using DeepMMO.Attributes;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
 
    [MessageType(Constants.TL_REWARDBACK_START + 1)]
    public class TLClientGetRewardBackInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_REWARDBACK_START + 2)]
    public class TLClientGetRewardBackInfoResponse : Response, ILogicProtocol
    {
        public int s2c_ydayLv;

        public HashMap<string, int> s2c_backMap;
        
    }

    [MessageType(Constants.TL_REWARDBACK_START + 3)]
    public class TLClientFreeGetRewardBackRequest : Request, ILogicProtocol
    {  
        // 功能id,如果不填表示全部领取
        public string c2s_FunctionID;
    }

    [MessageType(Constants.TL_REWARDBACK_START + 4)]
    public class TLClientFreeGetRewardBackResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARG = 501;
        [MessageCode("没有资源可以找回")] public const int TIME_OVER = 502;
        [MessageCode("资源不足")] public const int COST_LIMIT = 503;
    }

    [MessageType(Constants.TL_REWARDBACK_START + 5)]
    public class TLClientCostGetRewardBackRequest : Request, ILogicProtocol
    {
        // 功能id,如果不填表示全部领取
        public string c2s_FunctionID;
    }

    [MessageType(Constants.TL_REWARDBACK_START + 6)]
    public class TLClientCostGetRewardBackResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARG = 501;
        [MessageCode("没有资源可以找回")] public const int TIME_OVER = 502;
        [MessageCode("资源不足")] public const int COST_LIMIT = 503;
    }
 
    [MessageType(Constants.TL_REWARDBACK_START + 7)]
    public class TLClientRewardBackNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public bool showIcon;
    }
}