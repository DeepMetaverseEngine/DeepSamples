
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGetMagicBoxInfoResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00040016 : 262166
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientGetMagicBoxInfoResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetMagicBoxInfoResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutMap(data.s2c_keyMap, output.PutS32, output.PutS32);
            output.PutBool(data.s2c_havekeys);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGetMagicBoxInfoResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetMagicBoxInfoResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_keyMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
            data.s2c_havekeys = input.GetBool();
        }      
    }
}

