using DeepCore;

namespace TLClient.Protocol.Modules.Package
{
    public class RecoverSnap
    {
        public string Key;
        public HashMap<int, PackageSlot> DiffSrcSlots;
        public int StartActionCode;
    }
}
