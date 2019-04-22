
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientExitGameRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0003300F : 208911
        // base type : DeepMMO.Protocol.Request
        public static void W_DeepMMO_Protocol_Client_ClientExitGameRequest(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientExitGameRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.c2s_roleUUID);
        }
        public static void R_DeepMMO_Protocol_Client_ClientExitGameRequest(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientExitGameRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_roleUUID = input.GetUTF();
        }      
    }
}

