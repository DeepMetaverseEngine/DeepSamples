using DeepCore.IO;
using DeepCore.ORM;
using System;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Data
{

    [PersistType]
    [MessageType(TLConstants.TL_AUCTION_START + 1)]
    public class AuctionItemSnap : ISerializable, ICloneable, IStructMapping
    {
        //拍卖订单号
        [PersistField]
        public string uuid;

        //卖家id
        [PersistField]
        public string seller;

        //单价
        [PersistField]
        public int price;

        //评分
        [PersistField]
        public int score;

        //税率
        [PersistField]
        public int tax;

        //上架时间
        [PersistField]
        public DateTime time;

        //道具源数据
        [PersistField]
        public ItemSnapData item;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    [PersistType]
    [MessageType(TLConstants.TL_AUCTION_START + 2)]
    public class AuctionSaleRecordSnap : ISerializable, IStructMapping
    {
        //交易流水号
        [PersistField]
        public string transactionId;

        //模板id
        [PersistField]
        public int templateId;

        //数量
        [PersistField]
        public uint num;

        //收益值
        [PersistField]
        public int price;

        //售出时间
        [PersistField]
        public DateTime time;
    }

}
