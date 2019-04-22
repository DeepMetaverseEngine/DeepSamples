
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetBActityInfosResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009D002 : 643074
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetBActityInfosResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetBActityInfosResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutMap(data.activityMap, output.PutS32, output.PutObj);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetBActityInfosResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetBActityInfosResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.activityMap = input.GetMap<int, ThreeLives.Client.Protocol.Data.TLBActivitySnapData>(input.GetS32, input.GetObj<ThreeLives.Client.Protocol.Data.TLBActivitySnapData>);
        }      
    }
}

