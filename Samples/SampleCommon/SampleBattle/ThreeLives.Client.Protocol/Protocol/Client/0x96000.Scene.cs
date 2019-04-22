using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System;
using System.Collections.Generic;
using TLClient.Protocol.Data;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// PK模式变更请求.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 1)]
    public class ClientChangePKModeRequest : Request, ILogicProtocol
    {
        public byte c2s_mode;
    }

    /// <summary>
    /// PK模式变更返回.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 2)]
    public class ClientChangePKModeResponse : Response, ILogicProtocol
    {

    }

    /// <summary>
    /// 复活请求.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 3)]
    public class ClientRebirthRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 原地复活.
        /// </summary>
        public const byte Insitu = 0;
        /// <summary>
        /// 复活点复活.
        /// </summary>
        public const byte RebirthPoint = 1;
        /// <summary>
        /// 出生点复活.
        /// </summary>
        public const byte StartRegion = 2;

        public byte type;
    }

    /// <summary>
    /// 复活响应.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 4)]
    public class ClientRebirthResponse : Response, ILogicProtocol
    {
        [MessageCode("没有该物品.")]
        public const int CODE_ITEM_NOT_ENOUGH = 501;
        [MessageCode("没有复活次数.")]
        public const int CODE_NO_TIMES = 502;
        [MessageCode("复活冷却中.")]
        public const int CODE_IN_CD = 503;

    }

    /// <summary>
    /// 复活通知.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 5)]
    public class ServerPlayerRebirthNotfy : Notify, ILogicProtocol, INetProtocolS2C
    {
        //复活类型
        public int s2c_rebirthType;
        //描述.
        public string s2c_descStr;
        //操作1记录次数.
        public int s2c_btn_1_RecordTimes;
        //操作1倒计时.
        public DateTime s2c_btn_1_timeStamp;
        //操作记录次数.
        public int s2c_btn_2_RecordTimes;
        //操作2倒计时.
        public DateTime s2c_btn_2_timeStamp;

    }

    [MessageType(Constants.TL_SCENE_START + 8)]
    public class ClientSetModifyDataRequest : Request, ILogicProtocol
    {
        public string c2s_key;
        public string c2s_value;
    }
    [MessageType(Constants.TL_SCENE_START + 9)]
    public class ClientSetModifyDataResponse : Response, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SCENE_START + 10)]
    public class ClientGetModifyDataRequest : Request, ILogicProtocol
    {
        public string c2s_key;
    }
    [MessageType(Constants.TL_SCENE_START + 11)]
    public class ClientGetModifyDataResponse : Response, ILogicProtocol
    {
        public string s2c_value;
    }

    [MessageType(Constants.TL_SCENE_START + 12)]
    public class ClientTakeMedicineRequest : Request, ILogicProtocol
    {
        //物品ID.
        public int c2s_itemID;
    }
    [MessageType(Constants.TL_SCENE_START + 13)]
    public class ClientTakeMedicineResponse : Response, ILogicProtocol
    {
        [MessageCode("没有该物品")]
        public const int CODE_ITEM_NOT_ENOUGH = 501;
        [MessageCode("当前等级无法使用该物品")]
        public const int CODE_USE_LEVEL_ERROR = 502;
        [MessageCode("当前地图无法使用")]
        public const int CODE_MAP = 503;
        [MessageCode("药品CD中")]
        public const int CODE_CD = 504;
        [MessageCode("死亡时无法使用")]
        public const int CODE_DEAD = 505;



        //使用冷却时间戳.
        public DateTime s2c_CoolDownTimeStamp;
    }

    [MessageType(Constants.TL_SCENE_START + 14)]
    public class ClientSaveOptionsRequest : Request, ILogicProtocol
    {
        public bool c2s_AutoUseItem;
        public bool c2s_AutoRecharge;
        public byte c2s_UseThreshold;
    }
    [MessageType(Constants.TL_SCENE_START + 15)]
    public class ClientSaveOptionsResponse : Response, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SCENE_START + 16)]
    public class ClientSyncServerTimeRequest : Request, ILogicProtocol
    {
        public long c2s_ticks;
    }

    [MessageType(Constants.TL_SCENE_START + 17)]
    public class ClientSyncServerTimeResponse : Response, ILogicProtocol
    {
        public long s2c_clientTicks;
        public long s2c_serverTicks;
    }



    /// <summary>
    /// 快捷传送场景请求.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 18)]
    public class ClientChangeSceneRequest : Request, ILogicProtocol
    {
        public string c2s_MapUUID;
        public int c2s_MapId;
        public string c2s_GuildUUID;
        public string c2s_NextMapPosition;
        public HashMap<string, string> c2s_Ext;
    }

    /// <summary>
    /// 快捷传送场景响应.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 19)]
    public class ClientChangeSceneResponse : Response, ILogicProtocol
    {
        [MessageCode("已经到达目标地图")]
        public const int CODE_SAMEMAP = 502;
    }

    /// <summary>
    /// 请求进入副本\玩法\类地图场景.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 20)]
    public class ClientEnterDungeonRequest : Request, ILogicProtocol
    {
        public string c2s_FuncId;
        public int c2s_MapId;
        public HashMap<string, string> c2s_ext;
    }

    /// <summary>
    /// 请求进入副本\玩法\类地图场景.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 21)]
    public class ClientEnterDungeonResponse : Response, ILogicProtocol
    {

    }

    /// <summary>
    /// 客户端结算奖励通知.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 22)]
    public class ClientSettlementNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public const byte WIN = 1;
        public const byte LOSE = 0;

        public int s2c_exp;
        public int s2c_gold;
        public DateTime s2c_counttime;
        public bool s2c_noAward;
        public int s2c_finishtime_sec;
        //0失败1胜利.
        public byte s2c_status;
        public List<TLDropItemSnapData> s2c_itemList;
        public HashMap<string, string> s2c_ext;
    }

    /// <summary>
    /// 获取组队本每日奖励次数请求.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 23)]
    public class ClientGetTeamDungeonTicketsRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取组队本每日奖励次数回执.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 24)]
    public class ClientGetTeamDungeonTicketsResponse : Response, ILogicProtocol
    {
        public int s2c_tickets;
        public int s2c_hard_tickets;
    }

    /// <summary>
    /// 离开副本玩法.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 25)]
    public class ClientLeaveDungeonRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 离开副本玩法.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 26)]
    public class ClientLeaveDungeonResponse : Response, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SCENE_START + 27)]
    public class ClientGetModuleScoreRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SCENE_START + 28)]
    public class ClientGetModuleScoreResponse : Response, ILogicProtocol
    {
        public const byte function_equiplv = 1;
        public const byte function_gem = 2;
        public const byte function_equiprefine = 3;
        public const byte function_mount = 4;
        public const byte function_wing = 5;
        public const byte function_god = 6;
        public const byte function_artifact = 7;
        public const byte function_skill = 8;
        public const byte function_fate = 9;
        public const byte function_meridians = 10;
        public Dictionary<byte, long> scoremap;
    }

    [MessageType(Constants.TL_SCENE_START + 29)]
    public class ClientGetSystemOptionsRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SCENE_START + 30)]
    public class ClientGetSystemOptionsResponse : Response, ILogicProtocol
    {
        public HashMap<string, string> s2c_options;
    }

    [MessageType(Constants.TL_SCENE_START + 31)]
    public class ClientSaveOtherOptionsRequest : Request, ILogicProtocol
    {
        public HashMap<string, string> c2s_options;
    }

    [MessageType(Constants.TL_SCENE_START + 32)]
    public class ClientSaveOtherOptionsResponse : Response, ILogicProtocol
    {

    }

    /// <summary>
    /// 每日副本（核心产出）信息.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 33)]
    public class ClientGetDailyDungeonInfoRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 每日副本（核心产出）信息.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 34)]
    public class ClientGetDailyDungeonInfoResponse : Response, ILogicProtocol
    {
        public HashMap<string, int> s2c_info;
    }

    /// <summary>
    /// 脱离卡死.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 35)]
    public class ClientEscapeUnmoveableMapRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 脱离卡死.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 36)]
    public class ClientEscapeUnmoveableMapResponse : Response, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SCENE_START + 37)]
    public class ClientOfflineExpNotify : Notify, ILogicProtocol
    {
        public int c2s_exp;
        public int c2s_extra_exp;
        public TimeSpan c2s_time;
    }

    /// <summary>
    /// 客户端购买每日副本门票.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 38)]
    public class ClientBuyDailyDungeonTicketsRequest : Request, ILogicProtocol
    {
        public string c2s_dungeon_type;
    }

    /// <summary>
    /// 客户端购买每日副本门票
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 39)]
    public class ClientBuyDailyDungeonTicketsResponse : Response, ILogicProtocol
    {
        [MessageCode("购买次数已达上限.")]
        public const int CODE_BUY_LIMIT = 501;

        /// <summary>
        /// 场景类型.
        /// </summary>
        public string s2c_dungeon_type;
        /// <summary>
        /// 可进入次数.
        /// </summary>
        public int s2c_count;
    }

    [MessageType(Constants.TL_SCENE_START + 40)]
    public class ClientSaveMedicineItemRequest : Request, ILogicProtocol
    {
        public int c2s_itemID;
    }

    [MessageType(Constants.TL_SCENE_START + 41)]
    public class ClientSaveMedicineItemResponse : Response, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取等级封印信息.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 42)]
    public class ClientGetLvLimitInfoRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取等级封印信息.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 43)]
    public class ClientGetLvLimitInfoResponse : Response, ILogicProtocol
    {
        /// <summary>
        /// 下一等级开放时间.
        /// </summary>
        public int s2c_left_mins;
        /// <summary>
        /// 储存经验.
        /// </summary>
        public long s2c_overflow_exp;
        /// <summary>
        /// 当前等级封印
        /// </summary>
        public int s2c_cur_lv_limit;
        /// <summary>
        /// 下一级等级封印.
        /// </summary>
        public int s2c_next_lv_limit;
    }

    /// <summary>
    /// 获取封印等级累积经验.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 44)]
    public class ClientGetOverflowExpRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取封印等级累积经验.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 45)]
    public class ClientGetOverflowExpResponse : Response, ILogicProtocol
    {

    }

    /// <summary>
    /// 获得账号扩展数据.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 46)]
    public class GetAccountExtTargetKeyValueRequest : Request, ILogicProtocol
    {
        public string c2s_key;
    }


    /// <summary>
    /// 获得账号扩展数据.
    /// </summary>
    [MessageType(Constants.TL_SCENE_START + 47)]
    public class GetAccountExtTargetKeyValueResponse : Response, ILogicProtocol
    {
        public string s2c_value;
    }

    [MessageType(Constants.TL_SCENE_START + 48)]
    public class ClientPushKeyValueNotify : Notify, ILogicProtocol
    {
        public string s2c_key;
        public string s2c_value;
    }

    [MessageType(Constants.TL_SCENE_START + 49)]
    public class ClientChangeRoleNameRequest : Request, ILogicProtocol
    {
        public const byte TYPE_NAME = 0;
        public const byte TYPE_GUILD = 1;

        public int c2s_item_index;
        public byte c2s_change_type = 0;
        public string c2s_name = null;
    }

    [MessageType(Constants.TL_SCENE_START + 50)]
    public class ClientChangeRoleNameResponse : Response, ILogicProtocol
    {
        [MessageCode("角色名已存在")]
        public const int CODE_NAME_EXIST = CODE_ERROR + 1;
        [MessageCode("名字中含有敏感字符")]
        public const int CODE_BLACK_NAME = CODE_ERROR + 2;
        [MessageCode("无效的名字")]
        public const int CODE_NAME_INVAILD = CODE_ERROR + 3;
        [MessageCode("无效的道具")]
        public const int CODE_ITEM_INVAILD = CODE_ERROR + 4;
        [MessageCode("道具不足")]
        public const int CODE_ITEM_COUNT = CODE_ERROR + 5;
        [MessageCode("变更类型错误")]
        public const int CODY_CHANGE_TYPE_ERROR = CODE_ERROR + 6;
        [MessageCode("您还没有仙盟")]
        public const int CODE_ERROR_NOT_IN_GUILD = CODE_ERROR + 7;
        [MessageCode("您没有操作权限")]
        public const int CODE_ERROR_GUILD_NO_AUTHORITY = CODE_ERROR + 8;
        [MessageCode("该仙盟不存在")]
        public const int CODE_ERROR_GUILD_NOT_FOUND = CODE_ERROR + 9;
        [MessageCode("仙盟名字过长")]
        public const int CODE_ERROR_GUILD_NAME_LONG = CODE_ERROR + 10;
        [MessageCode("仙盟名字已存在")]
        public const int CODE_ERROR_GUILD_NAME_REPEAT = CODE_ERROR + 11;
    }

}