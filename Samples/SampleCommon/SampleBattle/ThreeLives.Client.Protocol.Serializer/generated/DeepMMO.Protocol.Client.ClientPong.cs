
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientPong


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00035002 : 217090
        // base type : DeepMMO.Protocol.Response
        public static void W_DeepMMO_Protocol_Client_ClientPong(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientPong)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutDateTime(data.time);
            output.PutBytes(data.rawdata);
        }
        public static void R_DeepMMO_Protocol_Client_ClientPong(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientPong)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.time = input.GetDateTime();
            data.rawdata = input.GetBytes();
        }      
    }
}

