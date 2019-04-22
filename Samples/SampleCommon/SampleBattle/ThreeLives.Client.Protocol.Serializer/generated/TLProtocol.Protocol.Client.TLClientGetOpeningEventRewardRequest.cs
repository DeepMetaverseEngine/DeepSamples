
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGetOpeningEventRewardRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00040264 : 262756
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_TLClientGetOpeningEventRewardRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetOpeningEventRewardRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutS32(data.c2s_id);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGetOpeningEventRewardRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetOpeningEventRewardRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_id = input.GetS32();
        }      
    }
}
