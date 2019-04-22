
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientSaveOtherOptionsRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009601F : 614431
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientSaveOtherOptionsRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientSaveOtherOptionsRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutMap(data.c2s_options, output.PutUTF, output.PutUTF);
        }
        public static void R_TLProtocol_Protocol_Client_ClientSaveOtherOptionsRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientSaveOtherOptionsRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_options = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
        }      
    }
}
