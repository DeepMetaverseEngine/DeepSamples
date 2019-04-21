
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientEnterGateRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace SampleRPG
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00031001 : 200705
        // base type : DeepMMO.Protocol.Request
        public static void W_DeepMMO_Protocol_Client_ClientEnterGateRequest(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterGateRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.c2s_account);
            output.PutUTF(data.c2s_token);
            output.PutOBJ(data.c2s_clientInfo);
        }
        public static void R_DeepMMO_Protocol_Client_ClientEnterGateRequest(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterGateRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_account = input.GetUTF();
            data.c2s_token = input.GetUTF();
            data.c2s_clientInfo = input.GetOBJ<DeepMMO.Data.ClientInfo>();
        }      
    }
}

