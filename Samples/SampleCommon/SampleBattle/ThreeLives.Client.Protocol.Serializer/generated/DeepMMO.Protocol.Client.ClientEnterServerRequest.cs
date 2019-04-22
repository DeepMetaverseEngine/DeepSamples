
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientEnterServerRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00032001 : 204801
        // base type : DeepMMO.Protocol.Request
        public static void W_DeepMMO_Protocol_Client_ClientEnterServerRequest(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterServerRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.c2s_account);
            output.PutUTF(data.c2s_gate_token);
            output.PutUTF(data.c2s_login_token);
            output.PutUTF(data.c2s_session_token);
            output.PutDateTime(data.c2s_time);
            output.PutObj(data.c2s_clientInfo);
        }
        public static void R_DeepMMO_Protocol_Client_ClientEnterServerRequest(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterServerRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_account = input.GetUTF();
            data.c2s_gate_token = input.GetUTF();
            data.c2s_login_token = input.GetUTF();
            data.c2s_session_token = input.GetUTF();
            data.c2s_time = input.GetDateTime();
            data.c2s_clientInfo = input.GetObj<DeepMMO.Data.ClientInfo>();
        }      
    }
}

