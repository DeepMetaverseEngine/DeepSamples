using System.Linq;
using DeepCore.IO;
using DeepCore.ORM;

namespace TLProtocol.Data
{
    public enum AlertMessageType
    {
        TeamApply = 1, //组队申请
        TeamInvite = 2, //组队邀请
        TeamFollowInvite = 3, //队伍跟随邀请
        TeamEnterMap = 4, //team进入副本邀请
        PlayerEnterMap = 5, //进入地图邀请
        MatchExitConfirm = 6, //是否退出匹配弹框
        GuildInvite = 10, //公会邀请
        EventEntermap = 11, // 事件库邀请进入地图弹框
        BecomeQinXin = 12, // 成为掌门小弟
        MarryInvite = 13, //求婚
        MarryRefuse = 14, //拒婚
        DivorceInvite = 15, //離婚
    }

    /// <summary>
    /// 消息数据
    /// </summary>
    [MessageType(TLConstants.TL_MESSAGE_START + 4)]
    public class MessageSnap : ISerializable
    {
        public string id;
        public AlertMessageType type;
        public int lifeTimeMS;
        public string strMsg;

        /// <summary>
        /// 玩家发过来的消息拥有RoleID
        /// </summary>
        public string fromRoleID;

        public string targetRoleID;

        /// <summary>
        /// 支持同步的玩家ID，此种情况下MessageHandleData的id为玩家id
        /// </summary>
        public MessageHandleData[] syncPlayers;

        public MessageSnap Clone()
        {
            var ret = (MessageSnap)MemberwiseClone();
            ret.syncPlayers = syncPlayers?.Select(m => new MessageHandleData {id = m.id, agree = m.agree}).ToArray();
            return ret;
        }
    }


    public enum AlertHandlerType : byte
    {
        None = 0,
        Agree = 1,
        Cancel = 2,
    }
    /// <summary>
    /// 消息数据
    /// </summary>
    [MessageType(TLConstants.TL_MESSAGE_START + 5)]
    public class MessageHandleData : ISerializable
    {
        public string id;
        /// <summary>
        /// 0 未响应 1 同意 2 不同意
        /// </summary>
        public AlertHandlerType agree;
    }

    public enum TextMessageType
    {
        TYPE_BLACK = 1,
        TYPE_FLOAT = 2,
        TYPE_SCROLL = 3,
        TYPE_SCROLLANDEFFECT = 4,
    }
}