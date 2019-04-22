using DeepCore.IO;
using TLProtocol.Data;

namespace ThreeLives.Client.Protocol.Data
{
    [MessageType(TLConstants.TL_GOD_START + 1)]
    public class ClientGodSnap : ISerializable
    {
        public enum GodStatus : byte
        {
            EIdle,
            EFight,
        }

        public int s2c_god_id;
        public int s2c_god_lv;
        /// <summary>
        /// GodStatus.出战状态.
        /// </summary>
        public byte s2c_god_status;
    }
}
