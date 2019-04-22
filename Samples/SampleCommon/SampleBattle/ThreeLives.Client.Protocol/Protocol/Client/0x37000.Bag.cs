using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获取背包道具数据
    /// </summary>
    [MessageType(Constants.BAG_START + 1)]
    public class ClientGetPackageRequest : Request, ILogicProtocol
    {
        public byte c2s_type;
    }

    /// <summary>
    /// 获取背包道具数据
    /// </summary>
    [MessageType(Constants.BAG_START + 2)]
    public class ClientGetPackageResponse : Response, ILogicProtocol
    {
        public byte s2c_type;
        public BagData s2c_data;
    }


    /// <summary>
    /// 通知客户端背包道具格子信息变更
    /// </summary>
    [MessageType(Constants.BAG_START + 3)]
    public class ClientPackageSlotNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public string s2c_reason;
        public byte s2c_type;
        public bool s2c_init;
        public HashMap<int,EntityItemData> s2c_slots;
    }

    /// <summary>
    /// 整理
    /// </summary>
    [MessageType(Constants.BAG_START + 4)]
    public class ClientPackagePackUpRequest : Request, ILogicProtocol
    {
        public byte c2s_type;
    }


    /// <summary>
    /// 整理
    /// </summary>
    [MessageType(Constants.BAG_START + 5)]
    public class ClientPackagePackUpResponse : Response, ILogicProtocol
    {
    }


    [MessageType(Constants.BAG_START + 6)]
    public class ClientSwapItemRequest : Request, ILogicProtocol
    {
        public byte c2s_fromType;
        public byte c2s_toType;

        public BagSlotConditon c2s_slot;
    }

    [MessageType(Constants.BAG_START + 7)]
    public class ClientSwapItemResponse : Response, ILogicProtocol
    {
    }


    [MessageType(Constants.BAG_START + 8)]
    public class ClientItemDataRequest : Request, ILogicProtocol
    {
        public string c2s_id;
    }

    [MessageType(Constants.BAG_START + 9)]
    public class ClientItemDataResponse : Response, ILogicProtocol
    {
        [MessageCode("未找到该道具")]
        public const int ERROR_CODE_ITEM_NULL = 501;
        public EntityItemData s2c_data;
    }


    [MessageType(Constants.BAG_START + 10)]
    public class ClientUseItemRequest : Request, ILogicProtocol
    {
        public List<BagSlotConditon> c2s_items;
    }


    [MessageType(Constants.BAG_START + 11)]
    public class ClientUseItemResponse : Response, ILogicProtocol
    {
    }


    [MessageType(Constants.BAG_START + 12)]
    public class ClienRandomItemTestRequest : Request, ILogicProtocol
    {
    }

    [MessageType(Constants.BAG_START + 13)]
    public class ClienRandomItemTestResponse : Response, ILogicProtocol
    {
    }

    [MessageType(Constants.BAG_START + 14)]
    public class ClientCombineItemRequest : Request, ILogicProtocol
    {
        public int c2s_combineID;
        public uint c2s_count;
    }

    [MessageType(Constants.BAG_START + 15)]
    public class ClientCombineItemResponse : Response, ILogicProtocol
    {
    }

    /// <summary>
    /// 获取所有背包数据
    /// </summary>
    [MessageType(Constants.BAG_START + 16)]
    public class ClientGetAllPackageRequest : Request, ILogicProtocol
    {
        public byte c2s_type;
    }

    /// <summary>
    /// 获取所有背包数据
    /// </summary>
    [MessageType(Constants.BAG_START + 17)]
    public class ClientGetAllPackageResponse : Response, ILogicProtocol
    {
        public HashMap<byte, BagData> s2c_bags;
    }

    /// <summary>
    /// 鉴定预览请求
    /// </summary>
    [MessageType(Constants.BAG_START + 18)]
    public class ClientIdentifyPreviewRequest : Request, ILogicProtocol
    {
        public string c2s_equipID;
    }

    /// <summary>
    /// 鉴定预览返回
    /// </summary>
    [MessageType(Constants.BAG_START + 19)]
    public class ClientIdentifyPreviewResponse : Response, ILogicProtocol
    {
        public List<ItemPropertyData> s2c_properties;
    }

    /// <summary>
    /// 保存鉴定属性
    /// </summary>
    [MessageType(Constants.BAG_START + 20)]
    public class ClientSaveIdentifyRequest : Request, ILogicProtocol
    {
        public string c2s_equipID;
    }

    /// <summary>
    /// 保存鉴定属性
    /// </summary>
    [MessageType(Constants.BAG_START + 21)]
    public class ClientSaveIdentifyResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("道具不存在")] public const int CODE_NOTEXISTITEM = 502;
    }

    /// <summary>
    /// 锁定/解锁某条鉴定属性
    /// </summary>
    [MessageType(Constants.BAG_START + 22)]
    public class ClientLockEquipPropertyRequest : Request, ILogicProtocol
    {
        public string c2s_equipID;
        public int c2s_propertyIndex;
        public bool c2s_lock;
        public bool c2s_isBuff;
    }

    /// <summary>
    /// 锁定某条属性
    /// </summary>
    [MessageType(Constants.BAG_START + 23)]
    public class ClientLockEquipPropertyResponse : Response, ILogicProtocol
    {
        [MessageCode("未找到请求位置的属性")] public const int CODE_NOTEXISTATTR = 501;
        [MessageCode("已到最大锁定数量")] public const int CODE_MAXLOCKED = 502;
    }

    /// <summary>
    /// 查看上一次的所有鉴定预览
    /// </summary>
    [MessageType(Constants.BAG_START + 24)]
    public class ClientIdentifyPreviewInfoRequest : Request, ILogicProtocol
    {
    }

    /// <summary>
    /// 查看上一次的所有鉴定预览
    /// </summary>
    [MessageType(Constants.BAG_START + 25)]
    public class ClientIdentifyPreviewInfoResponse : Response, ILogicProtocol
    {
        [MessageType(Constants.BAG_START + 26)]
        public class IdentifyPreviewKeyProperty : ISerializable
        {
            public string ID;
            public List<ItemPropertyData> Properties;
        }

        public List<IdentifyPreviewKeyProperty> s2c_data;
    }

    [MessageType(Constants.BAG_START + 27)]
    public class ClientSellItemRequest : Request, ILogicProtocol
    {
        public int c2s_index;
        public uint c2s_count;
    }

    [MessageType(Constants.BAG_START + 28)]
    public class ClientSellItemResponse : Response, ILogicProtocol
    {
        [MessageCode("该道具无法出售")] public const int CODE_SELLLIMIT = 501;
        [MessageCode("道具不存在")] public const int CODE_NOTEXISTITEM = 502;
        [MessageCode("道具数量不足")] public const int CODE_NOTENOUGHCOUNT = 503;
    }

    [MessageType(Constants.BAG_START + 29)]
    public class ClientDecomposeItemRequest : Request, ILogicProtocol
    {
        public BagSlotConditon[] c2s_slots;
    }

    [MessageType(Constants.BAG_START + 30)]
    public class ClientDecomposeItemResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("道具不存在")] public const int CODE_NOTEXISTITEM = 502;
        [MessageCode("该道具无法分解")] public const int CODE_DECOMPOSELIMIT = 503;
        [MessageCode("道具数量不足")] public const int CODE_NOTENOUGHCOUNT = 504;
    }

    [MessageType(Constants.BAG_START + 31)]
    public class ClientAddBagSizeRequest : Request, ILogicProtocol
    {
        public byte c2s_type;
        public int c2s_targetSize;
    }

    [MessageType(Constants.BAG_START + 32)]
    public class ClientAddBagSizeResponse : Response, ILogicProtocol
    {
        [MessageCode("未找到该包裹")] public const int CODE_NOPACKAGE = 501;
        [MessageCode("参数错误")] public const int CODE_MAXLIMIT = 502;
        [MessageCode("道具数量不足")] public const int CODE_NOTENOUGHCOUNT = 503;
        public int s2c_targetSize;
    }

    [MessageType(Constants.BAG_START + 33)]
    public class ClientComposeItemRequest : Request, ILogicProtocol
    {
        public int c2s_itemID;
        public int c2s_count;
    }

    [MessageType(Constants.BAG_START + 34)]
    public class ClientComposeItemResponse : Response, ILogicProtocol
    {
        [MessageCode("道具数量不足")] public const int CODE_NOTENOUGHCOUNT = 501;
    }
}