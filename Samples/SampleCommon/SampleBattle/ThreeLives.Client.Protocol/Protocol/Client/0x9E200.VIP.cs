using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using TLProtocol.Protocol.Client;

namespace ThreeLives.Client.Protocol.Protocol.Client
{

    [MessageType(Constants.TL_VIP_START + 1)]
    public class ClientVIPInfoRequest : Request, DeepMMO.Protocol.Client.ILogicProtocol
    {

    }

    [MessageType(Constants.TL_VIP_START + 2)]
    public class ClientVIPInfoResponse : Response, DeepMMO.Protocol.Client.ILogicProtocol
    {
        /// <summary>
        /// vip等级，是否可领取
        /// </summary>
        public HashMap<int, bool> s2c_vip_reward_record;
    }

    [MessageType(Constants.TL_VIP_START + 3)]
    public class ClientGetVIPRewardRequest : Request, DeepMMO.Protocol.Client.ILogicProtocol
    {
        public int c2s_viplv;
    }

    [MessageType(Constants.TL_VIP_START + 4)]
    public class ClientGetVIPRewardResponse : Response, DeepMMO.Protocol.Client.ILogicProtocol
    {
        [MessageCode("已领取该奖励")]
        public const int CODE_HAS_TAKEN = CODE_ERROR + 1;
        [MessageCode("已领取该奖励")]
        public const int CODE_INVALID_LV = CODE_ERROR + 2;

    }


    /// <summary>
    /// 客户端购买每日副本门票.
    /// </summary>
    [MessageType(Constants.TL_VIP_START + 5)]
    public class ClientBuyDailyTicketsRequest : Request, DeepMMO.Protocol.Client.ILogicProtocol
    {
        public string c2s_functionid;
    }

    /// <summary>
    /// 客户端购买每日副本门票
    /// </summary>
    [MessageType(Constants.TL_VIP_START + 6)]
    public class ClientBuyDailyTicketsResponse : Response, DeepMMO.Protocol.Client.ILogicProtocol
    {

        [MessageCode("购买次数已达上限.")]
        public const int CODE_BUY_LIMIT = 501;
        
       //可购买次数
        public int s2c_count;
        public string s2c_functionid;
    }
    
    /// <summary>
    /// 客户端请求门票次数.
    /// </summary>
    [MessageType(Constants.TL_VIP_START + 7)]
    public class ClientGetTicketsInfoRequest : Request, DeepMMO.Protocol.Client.ILogicProtocol
    {
        public string c2s_functionid;
    }

    /// <summary>
    /// 客户端请求门票次数.
    /// </summary>
    [MessageType(Constants.TL_VIP_START + 8)]
    public class ClientGetTicketsInfoResponse : Response, DeepMMO.Protocol.Client.ILogicProtocol
    {

        [MessageCode("购买类型异常")]
        public const int ERROR_DATA = 501;
        //剩余购买次数
        public int s2c_count;
        //购买总次数
        public int s2c_limit;
    }
    
    
     
    /// <summary>
    /// 客户端请求购买门票.
    /// </summary>
    [MessageType(Constants.TL_VIP_START + 9)]
    public class ClientCanBuyTicketsRequest : Request, DeepMMO.Protocol.Client.ILogicProtocol
    {
        public string c2s_functionid;
    }

    /// <summary>
    ///客户端请求购买门票.
    /// </summary>
    [MessageType(Constants.TL_VIP_START + 10)]
    public class ClientCanBuyTicketsResponse : Response, DeepMMO.Protocol.Client.ILogicProtocol
    {
        [MessageCode("购买类型异常")]
        public const int ERROR_DATA = 501;
        
        [MessageCode("购买次数已达上限")]
        public const int CODE_BUY_LIMIT = 502;
    }
}
