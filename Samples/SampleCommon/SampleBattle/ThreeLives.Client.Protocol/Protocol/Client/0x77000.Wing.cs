using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获取翅膀信息
    /// </summary>
    [MessageType(Constants.TL_WING + 1)]
    public class ClientGetWingInfoRequest : Request, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_WING + 2)]
    public class ClientGetWingInfoResponse : Response, ILogicProtocol
    {
        public int s2c_wingLv;
        public int s2c_currentExp;
        public int s2c_equipedRank;
    }

    /// <summary>
    /// 请求穿戴/取下 翅膀皮肤
    /// </summary>
    [MessageType(Constants.TL_WING + 3)]
    public class ClientEquipWingAvatarRequest : Request, ILogicProtocol
    {
        public int c2s_rank;
        public bool c2s_equip;
    }


    [MessageType(Constants.TL_WING + 4)]
    public class ClientEquipWingAvatarResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")]
        public const int CODE_ARG = 501;
        [MessageCode("尚未达到此进阶等级")]
        public const int CODE_CLASSLIMIT = 502;
    }

    /// <summary>
    /// 培养 c2s_auto为true时，强化到材料使用完毕或者满星
    /// </summary>
    [MessageType(Constants.TL_WING + 5)]
    public class ClientAddWingExpRequest : Request, ILogicProtocol
    {
        public bool c2s_auto;
    }


    [MessageType(Constants.TL_WING + 6)]
    public class ClientAddWingExpResponse : Response, ILogicProtocol
    {
        [MessageCode("参数错误")]
        public const int CODE_ARG = 501;
        [MessageCode("已到最高培养等级")]
        public const int CODE_MAXLIMIT = 502;
        public int s2c_wingLv;
        public int s2c_currentExp;
        public string s2c_effectres;
    }
}