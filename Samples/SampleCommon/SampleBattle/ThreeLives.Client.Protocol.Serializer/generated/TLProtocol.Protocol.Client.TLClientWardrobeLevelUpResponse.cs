
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientWardrobeLevelUpResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0006C008 : 442376
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientWardrobeLevelUpResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientWardrobeLevelUpResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_Score);
            output.PutS32(data.s2c_Level);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientWardrobeLevelUpResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientWardrobeLevelUpResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_Score = input.GetS32();
            data.s2c_Level = input.GetS32();
        }      
    }
}
