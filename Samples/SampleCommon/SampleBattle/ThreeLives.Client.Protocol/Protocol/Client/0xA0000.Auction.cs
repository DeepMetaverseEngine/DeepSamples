using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using TLProtocol.Protocol.Data;

namespace TLProtocol.Protocol.Client
{

    public class AuctionResponse : Response
    {
        [MessageCode("道具不存在")] public const int CODE_ERROR_AUCTION_ITEM_NOT_EXSIT = 501;
        [MessageCode("道具不可交易")] public const int CODE_ERROR_AUCTION_ITEM_CAN_NOT_TRADE = 502;
        [MessageCode("道具数量不足")] public const int CODE_ERROR_AUCTION_ITEM_NOT_ENOUGH = 503;
        [MessageCode("价格不在出售区间")] public const int CODE_ERROR_AUCTION_PRICE_OUT_OF_RANGE = 504;
        [MessageCode("货架已满，请先下架其他物品")] public const int CODE_ERROR_AUCTION_SHELF_FULL = 505;
        [MessageCode("暂不支持此分类")] public const int CODE_ERROR_AUCTION_KIND_NOT_SUPPORT = 506;
        [MessageCode("道具价格不一致")] public const int CODE_ERROR_AUCTION_PRICE_NOT_THE_SAME = 507;
        [MessageCode("慢了一步，物品一被买走")] public const int CODE_ERROR_AUCTION_ITEM_HAS_PUT_OFF = 508;
        [MessageCode("银两不足")] public const int CODE_ERROR_AUCTION_GOLD_NOT_ENOUGH = 509;
        [MessageCode("暂无可提取收益")] public const int CODE_ERROR_AUCTION_GOLD_ZERO = 510;
        [MessageCode("不能购买自己上架的物品")] public const int CODE_ERROR_AUCTION_CANNOT_BUY_SELF_ITEM = 511;
    }

    /// <summary>
    /// 物品上架
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 1)]
    public class ClientPutOnShelvesRequest : Request, ILogicProtocol
    {
        public int c2s_slotIndex;
        public int c2s_num;
        public int c2s_price;
    }
    [MessageType(Constants.TL_AUCTION_START + 2)]
    public class ClientPutOnShelvesResponse : AuctionResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 物品下架
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 3)]
    public class ClientPutOffShelvesRequest : Request, ILogicProtocol
    {
        public string c2s_auctionId;
    }
    [MessageType(Constants.TL_AUCTION_START + 4)]
    public class ClientPutOffShelvesResponse : AuctionResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 修改上架物品价格
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 5)]
    public class ClientAuctionChangePriceRequest : Request, ILogicProtocol
    {
        public string c2s_auctionId;
        public int c2s_price;
    }
    [MessageType(Constants.TL_AUCTION_START + 6)]
    public class ClientAuctionChangePriceResponse : AuctionResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取商品列表
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 7)]
    public class ClientGetAuctionItemListRequest : Request, ILogicProtocol
    {
        public int c2s_item_type;
        public int c2s_sec_type;
        public int c2s_pro;
        public int c2s_level;
        public int c2s_quality;
        public int c2s_star_level;
        public List<int> c2s_templateIds;
        //分页请求，0序
        public int c2s_part;
        public string c2s_lastId;
    }
    [MessageType(Constants.TL_AUCTION_START + 8)]
    public class ClientGetAuctionItemListResponse : AuctionResponse, ILogicProtocol
    {
        //是否已全部获取
        public bool s2c_isFull;
        public List<AuctionItemSnap> s2c_list;
    }

    /// <summary>
    /// 获取出售列表
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 9)]
    public class ClientGetSaleItemListRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_AUCTION_START + 10)]
    public class ClientGetSaleItemListResponse : AuctionResponse, ILogicProtocol
    {
        public HashMap<int, AuctionItemSnap> s2c_saleList;
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 11)]
    public class ClientAuctionBuyRequest : Request, ILogicProtocol
    {
        public string c2s_auctionId;
        public int c2s_price;
        public int c2s_num;
    }
    [MessageType(Constants.TL_AUCTION_START + 12)]
    public class ClientAuctionBuyResponse : AuctionResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取正在出售的同类物品
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 13)]
    public class ClientGetOtherItemListRequest : Request, ILogicProtocol
    {
        public int c2s_templateId;
    }
    [MessageType(Constants.TL_AUCTION_START + 14)]
    public class ClientGetOtherItemListResponse : AuctionResponse, ILogicProtocol
    {
        public List<AuctionItemSnap> s2c_list;
    }

    /// <summary>
    /// 获取收益信息
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 15)]
    public class ClientGetSalesRecordRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_AUCTION_START + 16)]
    public class ClientGetSalesRecordResponse : AuctionResponse, ILogicProtocol
    {
        public HashMap<string, AuctionSaleRecordSnap> s2c_recordList;
        public ulong s2c_goldMax;
    }

    /// <summary>
    /// 提取收益
    /// </summary>
    [MessageType(Constants.TL_AUCTION_START + 17)]
    public class ClientAuctionExtractRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_AUCTION_START + 18)]
    public class ClientAuctionExtractResponse : AuctionResponse, ILogicProtocol
    {
        public ulong s2c_goldMax;
    }

}
