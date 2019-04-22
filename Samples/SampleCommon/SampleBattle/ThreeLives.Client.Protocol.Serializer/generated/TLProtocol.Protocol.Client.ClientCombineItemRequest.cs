
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientCombineItemRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0003700E : 225294
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientCombineItemRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientCombineItemRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutS32(data.c2s_combineID);
            output.PutU32(data.c2s_count);
        }
        public static void R_TLProtocol_Protocol_Client_ClientCombineItemRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientCombineItemRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_combineID = input.GetS32();
            data.c2s_count = input.GetU32();
        }      
    }
}
