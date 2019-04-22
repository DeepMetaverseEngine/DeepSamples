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
    /// 发送聊天
    /// </summary>
    [MessageType(Constants.TL_CHAT_START + 1)]
    public class ClientChatRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 无效的
        /// </summary>
        public const short CHANNEL_TYPE_INVALID = -1;


        public const short CHANNEL_TYPE_PRIVATE = 0;
        /// <summary>
        /// 世界
        /// </summary>
        public const short CHANNEL_TYPE_WORLD = 1;
        /// <summary>
        /// 附近
        /// </summary>
        public const short CHANNEL_TYPE_AREA  = 2;
        /// <summary>
        /// 仙盟 /工会
        /// </summary>
        public const short CHANNEL_TYPE_GUILD = 3;
        /// <summary>
        /// 队伍
        /// </summary>
        public const short CHANNEL_TYPE_TEAM = 4;
    
        //平台
        public const short CHANNEL_MENGHUI = 5;


        // 系统
        public const short CHANNEL_SYSTEM = 6;
        /// <summary>
        /// 战场 同阵营的频道
        /// </summary>
        public const short CHANNEL_TYPE_BATTLE = 7;

        // 师门
        public const short CHANNEL_TYPE_SHIMEN = 8;

        // 喇叭
        public const short CHANNEL_TYPE_HORN = 9;

        public const short CHANNEL_TYPE_MAX = 10;


        public string to_uuid;
        public string content;
 
        public short channel_type;

        // -1为喇叭 显示为系统消息，各种聊天框样式等
        public short show_type;
 
        // 某些特殊的消息需要显示在不同的频道里
        public short[] show_channel;

        public short func_type;

        /// <summary>
        /// 服务器填充
        /// </summary>
        public string from_name;
        /// <summary>
        /// 服务器填充
        /// </summary>
        public string from_uuid;


    }

    /// <summary>
    /// 请求聊天回执
    /// </summary>
    [MessageType(Constants.TL_CHAT_START + 2)]
    public class ClientChatResponse : Response, ILogicProtocol
    {
        [MessageCode("该频道不能聊天")]
        public const int CODE_BANNED = 501;
        [MessageCode("黑名单")]
        public const int CODE_BLACKLIST = 502;
        [MessageCode("该玩家已下线")]
        public const int CODE_TARGET_OFFLINE = 503;
        [MessageCode("该玩家不存在")]
        public const int CODE_PLAYER_NOT_EXIST = 504;
        [MessageCode("该频道不存在")]
        public const int CODE_CHANNEL_NOT_EXIST = 505;

        [MessageCode("您还没有加入队伍")]
        public const int CODE_TEAM_NOT_EXIST = 506;

        [MessageCode("您还没有加入公会")]
        public const int CODE_GUILD_NOT_EXIST = 507;

        [MessageCode("您被系统禁言了")]
        public const int CODE_SYS_FORBID = 508;
    }


    /// <summary>
    /// 聊天通知
    /// </summary>
    [MessageType(Constants.TL_CHAT_START + 3)]
    public class TLClientChatNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public short channel_type;
        public string from_name;
        public string from_uuid;
        public string content;
        public string[] langKey;
        public string to_uuid;
        public string to_name;
        public double chat_time;
        public short show_type;
        public short[] show_channel;
        public short func_type;
        public bool isSys = false;
    }


    
}
