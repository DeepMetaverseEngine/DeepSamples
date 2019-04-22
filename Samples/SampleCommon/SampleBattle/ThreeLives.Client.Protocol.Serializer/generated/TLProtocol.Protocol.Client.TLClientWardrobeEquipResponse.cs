
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientWardrobeEquipResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0006C00A : 442378
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientWardrobeEquipResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientWardrobeEquipResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_avatarId);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientWardrobeEquipResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientWardrobeEquipResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_avatarId = input.GetS32();
        }      
    }
}
