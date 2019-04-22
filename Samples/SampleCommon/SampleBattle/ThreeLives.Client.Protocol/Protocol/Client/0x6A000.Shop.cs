using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Data;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获取商店物品列表
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 1)]
    public class ClientGetStoreBoughtInfoRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 商店类型
        /// </summary>
        public int c2s_store_type;
    }

    /// <summary>
    /// 获取已购回执.
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 2)]
    public class ClientGetStoreBoughtInfoResponse : Response, ILogicProtocol
    {
        // 
        [MessageCode("商城暂未开放")] public const int STORE_NOT_OPEN = 501;

        [MessageCode("公会商店暂未开放")] public const int GUILDSHOP_NOT_OPEN = 502;
 
 
        public List<int> salelist;

        //id 和数量
        public HashMap<int, StoreSnapData> s2c_data;

    }

 
    /// <summary>
    /// 购买商店物品
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 3)]
    public class ClientStoreBuyItemRequest : Request, ILogicProtocol
    {
        public int c2s_store_type;
        /// <summary>
        /// 物品Id
        /// </summary>
        public int c2s_item_id;

        /// <summary>
        /// 购买数量
        /// </summary>
        public int c2s_num;


    }

    /// <summary>
    ///  购买商店物品回执.
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 4)]
    public class ClientStoreBuyItemResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("没有这个物品")] public const int CODE_NOTEXISTITEM = 502;
        [MessageCode("购买次数已用完")] public const int CODE_ITEMLIMIT = 503;
        [MessageCode("商品尚未开始售卖")] public const int CODE_TIMENOTSTAR = 504;
        [MessageCode("商品售卖时间已过")] public const int CODE_TIMEEND = 505;
        [MessageCode("资源不足无法购买")] public const int CODE_COST_NOT_ENOUGH = 506;
    }

}


