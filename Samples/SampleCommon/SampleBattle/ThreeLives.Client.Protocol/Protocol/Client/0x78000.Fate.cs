using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 命石抽奖
    /// </summary>
    [MessageType(Constants.TL_FATE + 1)]
    public class ClientFateLotteryRequest : Request, ILogicProtocol
    {
        // 命石抽奖id
        public int c2s_fateLotteryId;
    }

    [MessageType(Constants.TL_FATE + 2)]
    public class ClientFateLotteryResponse : Response, ILogicProtocol
    {
        //public int[] s2c_emptySlots;
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("背包格子满")] public const int CODE_BAGFULL = 502;
        [MessageCode("资源不足无法购买")] public const int CODE_COST_NOT_ENOUGH = 503;
    }


    [MessageType(Constants.TL_FATE + 3)]
    public class ClientFateDecomposeItemRequest : Request, ILogicProtocol
    {
        public BagSlotConditon[] c2s_slots;
    }

    [MessageType(Constants.TL_FATE + 4)]
    public class ClientFateDecomposeItemResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("道具不存在")] public const int CODE_NOTEXISTITEM = 502;
        [MessageCode("该道具无法分解")] public const int CODE_DECOMPOSELIMIT = 503;
        [MessageCode("道具数量不足")] public const int CODE_NOTENOUGHCOUNT = 504;
        [MessageCode("锁定具无法分解")] public const int CODE_LOCKED = 505;
    }


    [MessageType(Constants.TL_FATE + 5)]
    public class ClientFateLevelUpRequest : Request, ILogicProtocol
    {
        public string c2s_equipID;
    }

    [MessageType(Constants.TL_FATE + 6)]
    public class ClientFateLevelUpResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("道具不存在")] public const int CODE_NOTEXISTITEM = 502;
        [MessageCode("该道具无法升级")] public const int CODE_LEVELUPLIMIT = 503;
        [MessageCode("魂玉不足")] public const int CODE_NOTENOUGHCOUNT = 504;
    }

    [MessageType(Constants.TL_FATE + 7)]
    public class ClientLockFateRequest : Request, ILogicProtocol
    {
        public string c2s_equipID;
        public bool c2s_lock;
    }

    /// <summary>
    /// 锁定某条属性
    /// </summary>
    [MessageType(Constants.TL_FATE + 8)]
    public class ClientLockFateResponse : Response, ILogicProtocol
    {
        [MessageCode("未找到装备")] public const int CODE_NOTEXISTATTR = 501; 
    }

}