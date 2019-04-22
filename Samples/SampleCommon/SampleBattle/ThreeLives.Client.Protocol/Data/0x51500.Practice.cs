using DeepCore.IO;

namespace TLProtocol.Data
{

    [MessageType(TLConstants.TL_PRACTICE_START + 1)]
    public class PracticeInfoData : ISerializable
    {
        public long fightPower;
        public byte practiceLv;
        public byte stageLv;
        public bool needQuest;
    }

}
