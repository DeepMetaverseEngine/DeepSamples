
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGetSkillListResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0006B002 : 438274
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientGetSkillListResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetSkillListResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutMap(data.skillMap, output.PutS32, output.PutS32);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGetSkillListResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetSkillListResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.skillMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
        }      
    }
}
