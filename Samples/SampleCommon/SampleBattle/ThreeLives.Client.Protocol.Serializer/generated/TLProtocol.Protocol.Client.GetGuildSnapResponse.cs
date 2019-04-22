
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.GetGuildSnapResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00037104 : 225540
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_GetGuildSnapResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.GetGuildSnapResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutArray(data.s2c_data, output.PutObj);
        }
        public static void R_TLProtocol_Protocol_Client_GetGuildSnapResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.GetGuildSnapResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_data = input.GetArray<TLProtocol.Protocol.Data.TLClientGuildSnap>(input.GetObj<TLProtocol.Protocol.Data.TLClientGuildSnap>);
        }      
    }
}

