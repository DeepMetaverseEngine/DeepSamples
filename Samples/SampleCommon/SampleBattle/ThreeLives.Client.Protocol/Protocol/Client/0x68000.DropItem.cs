using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_DROPITEM_START + 1)]
    public class DropItemsNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public TLMonsterDropData info;
    }
}
