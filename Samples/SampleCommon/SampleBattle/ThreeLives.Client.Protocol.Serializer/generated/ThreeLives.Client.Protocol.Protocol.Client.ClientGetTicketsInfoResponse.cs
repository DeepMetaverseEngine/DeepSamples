
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// ThreeLives.Client.Protocol.Protocol.Client.ClientGetTicketsInfoResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // ThreeLives.Client.Protocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009E208 : 647688
        // base type : DeepMMO.Protocol.Response
        public static void W_ThreeLives_Client_Protocol_Protocol_Client_ClientGetTicketsInfoResponse(IOutputStream output, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Protocol.Client.ClientGetTicketsInfoResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_count);
            output.PutS32(data.s2c_limit);
        }
        public static void R_ThreeLives_Client_Protocol_Protocol_Client_ClientGetTicketsInfoResponse(IInputStream input, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Protocol.Client.ClientGetTicketsInfoResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_count = input.GetS32();
            data.s2c_limit = input.GetS32();
        }      
    }
}
