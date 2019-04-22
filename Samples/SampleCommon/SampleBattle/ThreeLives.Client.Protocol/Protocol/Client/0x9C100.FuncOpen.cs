
using DeepCore;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;

namespace TLProtocol.Protocol.Client
{

    /// <summary>
    /// 客户端通知服务器功能已游玩
    /// </summary>
    [MessageType(Constants.TL_FUNCOPEN_START + 1)]
    public class ClientFunctionPlayedNotify : Notify, ILogicProtocol, INetProtocolC2S
    {
        public string c2s_key;
    }

    /// <summary>
    /// 通知客户端公会变更
    /// </summary>
    //////////////////////////////////////////////////
    [MessageType(Constants.TL_FUNCOPEN_START + 10)]
    public class ClientFunctionOpenNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public HashMap<string, byte> s2c_funList;
    }

}
