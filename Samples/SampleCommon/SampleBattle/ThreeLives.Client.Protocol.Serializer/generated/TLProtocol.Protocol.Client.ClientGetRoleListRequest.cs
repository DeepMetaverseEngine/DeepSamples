
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetRoleListRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009501B : 610331
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientGetRoleListRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetRoleListRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutU8(data.c2s_type);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetRoleListRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetRoleListRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_type = input.GetU8();
        }      
    }
}
