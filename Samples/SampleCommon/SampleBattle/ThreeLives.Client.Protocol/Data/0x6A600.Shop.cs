using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;

namespace TLProtocol.Data
{   
    [MessageType(TLConstants.TL_SHOP_START + 1)]
    public class StoreSnapData : ISerializable
    {
        public int templateID;

        public int boughtNum;

        public int vipTimes;
    }
}
