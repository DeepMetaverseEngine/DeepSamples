
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetAllBActityInfosResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009D018 : 643096
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetAllBActityInfosResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetAllBActityInfosResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutMap(data.s2c_activityMap, output.PutS32, output.PutObj);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetAllBActityInfosResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetAllBActityInfosResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_activityMap = input.GetMap<int, TLProtocol.Protocol.Client.ClientGetAllBActityInfosResponse.ActivityData>(input.GetS32, input.GetObj<TLProtocol.Protocol.Client.ClientGetAllBActityInfosResponse.ActivityData>);
        }      
    }
}
