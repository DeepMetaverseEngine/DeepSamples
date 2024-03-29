
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientResetDTReponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0004000D : 262157
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientResetDTReponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientResetDTReponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_resetTime);
            output.PutS32(data.s2c_curLayer);
            output.PutS32(data.s2c_maxLayer);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientResetDTReponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientResetDTReponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_resetTime = input.GetS32();
            data.s2c_curLayer = input.GetS32();
            data.s2c_maxLayer = input.GetS32();
        }      
    }
}

