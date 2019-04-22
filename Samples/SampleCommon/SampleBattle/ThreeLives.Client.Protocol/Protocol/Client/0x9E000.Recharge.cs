using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using DeepMMO.Attributes;
using TLProtocol.Data;
using System.Collections.Generic;
using DeepCore;

namespace TLProtocol.Protocol.Client
{

    [MessageType(Constants.TL_RECHARGE_START + 1)]
    public class ClientGetRechargeInfoRequest : Request, ILogicProtocol
    {
        public int c2s_platform_id;
    }

    [MessageType(Constants.TL_RECHARGE_START + 2)]
    public class ClientGetRechargeInfoResponse : Response, ILogicProtocol
    {
        [MessageCode("平台ID错误！")]
        public const int CODE_RECHARGE_PLATFORM_ERROR = CODE_ERROR + 1;
        /// <summary>
        /// 商品ID，购买次数.
        /// </summary>
        public HashMap<int, RechargeProductSnap> s2c_data;
        public int vip_level;
        public int vip_exp;
    }

    [MessageType(Constants.TL_RECHARGE_START + 3)]
    public class ClientGetRechargeOrderRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 商品ID.
        /// </summary>
        public int c2s_product_id;
        /// <summary>
        /// 渠道ID.
        /// </summary>
        public int c2s_platform_id;
        /// <summary>
        /// 扩展参数.
        /// </summary>
        public HashMap<string, string> c2s_ext_data;
    }

    [MessageType(Constants.TL_RECHARGE_START + 4)]
    public class ClientGetRechargeOrderResponse : Response, ILogicProtocol
    {
        [MessageCode("无效的平台")]
        public const int CODE_RECHARGE_PLATFORM_ERROR = CODE_ERROR + 1;
        [MessageCode("无效的商品")]
        public const int CODE_RECHARGE_PRODUCTID_ERROR = CODE_ERROR + 2;
        [MessageCode("创建订单失败")]
        public const int CODE_RECHARGE_CREATE_ORDER_ERROR = CODE_ERROR + 3;
        [MessageCode("无法购买该物品")]
        public const int CODE_RECHARGE_CREATE_ORDER_LIMIT = CODE_ERROR + 4;

        /// <summary>
        /// 订单ID.
        /// </summary>
        public string s2c_order_id;
        /// <summary>
        /// 验证地址.
        /// </summary>
        public string s2c_notify_url;
        /// <summary>
        /// 扩展参数.
        /// </summary>
        public HashMap<string, string> s2c_ext_data;
    }

    [MessageType(Constants.TL_RECHARGE_START + 5)]
    public class ClientRechargePayResultRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 充值结果.
        /// </summary>
        public List<RechargePayResultData> c2s_list;
        /// <summary>
        /// 检查订单.
        /// </summary>
        public bool c2s_check_order;
    }

    [MessageType(Constants.TL_RECHARGE_START + 6)]
    public class ClientRechargePayResultResponse : Response, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_RECHARGE_START + 7)]
    public class ClientRechargeOrderStatusChange : Notify, ILogicProtocol
    {
        /// <summary>
        /// 订单ID.
        /// </summary>
        public string c2s_order_id;
    }

    [MessageType(Constants.TL_RECHARGE_START + 8)]
    public class ClientRechargeInfoChangeNotify : Notify, ILogicProtocol
    {
        /// <summary>
        /// 商品ID，购买次数.
        /// </summary>
        public HashMap<int, RechargeProductSnap> s2c_data;
    }
    
    
    
    [MessageType(Constants.TL_RECHARGE_START + 9)]
    public class ClientCostNotEnoughNotify : Notify, ILogicProtocol
    {
        /// <summary>
        /// 物品不足种类
        /// </summary>
        public string c2s_functionid;
        public string[] c2s_message;
    }

}
