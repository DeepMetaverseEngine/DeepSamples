
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// ThreeLives.Client.Protocol.Data.ClientGodSnap


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // ThreeLives.Client.Protocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00088501 : 558337
        // base type : 
        public static void W_ThreeLives_Client_Protocol_Data_ClientGodSnap(IOutputStream output, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Data.ClientGodSnap)msg;
                        
            output.PutS32(data.s2c_god_id);
            output.PutS32(data.s2c_god_lv);
            output.PutU8(data.s2c_god_status);
        }
        public static void R_ThreeLives_Client_Protocol_Data_ClientGodSnap(IInputStream input, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Data.ClientGodSnap)msg;
                        
            data.s2c_god_id = input.GetS32();
            data.s2c_god_lv = input.GetS32();
            data.s2c_god_status = input.GetU8();
        }      
    }
}
