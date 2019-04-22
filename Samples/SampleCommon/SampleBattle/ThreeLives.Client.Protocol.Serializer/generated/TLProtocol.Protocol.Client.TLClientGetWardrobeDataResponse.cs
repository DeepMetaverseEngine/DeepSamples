
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGetWardrobeDataResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0006C002 : 442370
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientGetWardrobeDataResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetWardrobeDataResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_Score);
            output.PutS32(data.s2c_Level);
            output.PutMap(data.s2c_dayMap, output.PutS32, output.PutDateTime);
            output.PutMap(data.s2c_equipMap, output.PutS32, output.PutS32);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGetWardrobeDataResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetWardrobeDataResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_Score = input.GetS32();
            data.s2c_Level = input.GetS32();
            data.s2c_dayMap = input.GetMap<int, System.DateTime>(input.GetS32, input.GetDateTime);
            data.s2c_equipMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
        }      
    }
}

