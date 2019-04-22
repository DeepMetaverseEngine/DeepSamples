
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGuildWantedRefreshTimeResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0004003D : 262205
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientGuildWantedRefreshTimeResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGuildWantedRefreshTimeResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_RefreshTime);
            output.PutDateTime(data.s2c_RefreshDateTime);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGuildWantedRefreshTimeResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGuildWantedRefreshTimeResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_RefreshTime = input.GetS32();
            data.s2c_RefreshDateTime = input.GetDateTime();
        }      
    }
}

