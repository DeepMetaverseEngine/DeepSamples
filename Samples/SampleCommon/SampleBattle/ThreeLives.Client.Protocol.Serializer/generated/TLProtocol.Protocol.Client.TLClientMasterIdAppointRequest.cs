
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientMasterIdAppointRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x000400D6 : 262358
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_TLClientMasterIdAppointRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientMasterIdAppointRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.roleID);
            output.PutS32(data.type);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientMasterIdAppointRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientMasterIdAppointRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.roleID = input.GetUTF();
            data.type = input.GetS32();
        }      
    }
}

