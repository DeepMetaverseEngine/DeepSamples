
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.GetAccountExtTargetKeyValueResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009602F : 614447
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_GetAccountExtTargetKeyValueResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.GetAccountExtTargetKeyValueResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutUTF(data.s2c_value);
        }
        public static void R_TLProtocol_Protocol_Client_GetAccountExtTargetKeyValueResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.GetAccountExtTargetKeyValueResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_value = input.GetUTF();
        }      
    }
}

