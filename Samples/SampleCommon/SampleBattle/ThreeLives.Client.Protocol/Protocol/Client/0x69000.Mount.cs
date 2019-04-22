using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 1)]
    public class ClientRidingMountRequest : Request, ILogicProtocol
    {
        public bool ride;
    }


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 2)]
    public class ClientRidingMountResponse : Response, ILogicProtocol
    {
        [MessageCode("您已在骑乘状态，不要重复上坐骑")] public const int CODE_RIDE_AGAIN = 501;
        [MessageCode("当前场景不能骑乘坐骑")] public const int CODE_SCENE_LIMIT = 502;
    }

    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 3)]
    public class RideStatusNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public bool rideStatus;
        public bool isRideFailed;
        public string ReasonKey;
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 4)]
    public class ClientGetRoleMountInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 5)]
    public class ClientGetRoleMountInfoResponse : Response, ILogicProtocol
    {
        public TLRoleMountSnapData s2c_data;

    }


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 6)]
    public class ClientUnlockMountRequest : Request, ILogicProtocol
    {
        public int mountId;
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 7)]
    public class ClientUnlockMountResponse : Response, ILogicProtocol
    {
        public int mountId;
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("当前坐骑已激活")] public const int CODE_LEVELLIMIT = 502; 
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 8)]
    public class ClientMountStarUpRequest : Request, ILogicProtocol
    {
        // 一键升级
        public int oneKey = 0;
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 9)]
    public class ClientMountStarUpResponse : Response, ILogicProtocol
    {
        public int veinId;
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("已经达到最高等级")] public const int CODE_LEVELLIMIT = 502; 
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 10)]
    public class ClientChangeMountRequest : Request, ILogicProtocol
    {
        public int mountId;
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 11)]
    public class ClientChangeMountResponse : Response, ILogicProtocol
    {
        public int mountId;
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("当前坐骑已在驾驭中")] public const int CODE_LEVELLIMIT = 502;
        [MessageCode("当前坐骑未激活")] public const int CODE_NOTEXISTITEM = 503;
    }

    /// <summary>
    /// 解锁坐骑推送
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_MOUNT_START + 12)]
    public class TLClientUnlockMountNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int mountId;
    }

}