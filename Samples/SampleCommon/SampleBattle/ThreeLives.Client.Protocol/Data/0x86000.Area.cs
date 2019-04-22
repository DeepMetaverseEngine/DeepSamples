

using DeepCore.IO;
using TLProtocol.Data;

namespace TLClient.Protocol.Data
{
    [MessageType(TLConstants.TL_AREA_START + 1)]
    public class TLAreaRoleSnap : ISerializable
    {
        public string roleUUID;
        public string sceneUUID;

        public int maxHP;
        public int curHP;

        public int maxMP;
        public int curMP;

      
    }

}
