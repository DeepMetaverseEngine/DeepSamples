
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGetMeridiansInfoReponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0004032B : 262955
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientGetMeridiansInfoReponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetMeridiansInfoReponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutList(data.s2c_activited, output.PutS32);
            output.PutList(data.s2c_unlocked, output.PutS32);
            output.PutList(data.s2c_cost, output.PutS32);
            output.PutUTF(data.s2c_overflowexp);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGetMeridiansInfoReponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetMeridiansInfoReponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_activited = input.GetList<int>(input.GetS32);
            data.s2c_unlocked = input.GetList<int>(input.GetS32);
            data.s2c_cost = input.GetList<int>(input.GetS32);
            data.s2c_overflowexp = input.GetUTF();
        }      
    }
}
