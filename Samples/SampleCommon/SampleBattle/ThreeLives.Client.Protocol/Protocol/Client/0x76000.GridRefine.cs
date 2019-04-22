using DeepCore;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using DeepMMO.Attributes;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 装备强化请求
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 1)]
    public class ClientGridRefineRequest : Request, ILogicProtocol
    {
        public int c2s_equipPos;
    }

    /// <summary>
    /// 装备强化返回
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 2)]
    public class ClientGridRefineResponse : Response, ILogicProtocol
    {
        [MessageCode("装备位置不存在")] public const int CODE_ERROREQUIPPOS = 501;
        [MessageCode("已强化到最高等级")] public const int CODE_MAXREFINELV = 502;
        [MessageCode("玩家等级不足")] public const int CODE_LEVELLIMIT = 503;
        public GridRefineData s2c_data;
    }

    /// <summary>
    /// 已强化列表请求
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 3)]
    public class ClientGridRefineInfoRequest : Request, ILogicProtocol
    {
    }

    /// <summary>
    ///  已强化列表
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 4)]
    public class ClientGridRefineInfoResponse : Response, ILogicProtocol
    {
        public HashMap<int,GridRefineData> s2c_data;
    }

    /// <summary>
    /// 宝石镶嵌
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 5)]
    public class ClientGridEquipGemRequest : Request, ILogicProtocol
    {
        public int c2s_equipPos;
        public int c2s_gemBagIndex;
        public int c2s_gemSlotIndex;
    }

    /// <summary>
    /// 宝石镶嵌
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 6)]
    public class ClientGridEquipGemResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("道具不存在")] public const int CODE_NOTEXISTITEM = 502;
        [MessageCode("当前无插槽位置可用")] public const int CODE_LEVELLIMIT = 503;
        [MessageCode("不是一个宝石")] public const int CODE_NOTGEM = 504;
        [MessageCode("宝石类型不符合")] public const int CODE_GEMTYPEERROR = 505;
        public GridGemSlotData s2c_slotData;
    }

    /// <summary>
    /// 获取宝石镶嵌列表
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 7)]
    public class ClientGridGemInfoRequest : Request, ILogicProtocol
    {
    }

    /// <summary>
    ///  获取宝石镶嵌列表
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 8)]
    public class ClientGridGemInfoResponse : Response, ILogicProtocol
    {
        public HashMap<int, GridGemData> s2c_data;
    }

    /// <summary>
    /// 取出宝石
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 9)]
    public class ClientGridUnEquipGemRequest : Request, ILogicProtocol
    {
        public int c2s_equipPos;
        public int c2s_gemSlotIndex;
    }

    /// <summary>
    /// 取出宝石
    /// </summary>
    [MessageType(Constants.TL_EQUIP_REFINE + 10)]
    public class ClientGridUnEquipGemResponse : Response, ILogicProtocol
    {
        [MessageCode("所在位置没有宝石")] public const int CODE_GEMNOTEXIST = 501;
        public const int CODE_EXT = 502;
    }


}
