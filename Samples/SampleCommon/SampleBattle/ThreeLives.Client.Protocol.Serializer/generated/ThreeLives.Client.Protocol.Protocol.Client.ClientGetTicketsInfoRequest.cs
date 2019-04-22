
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// ThreeLives.Client.Protocol.Protocol.Client.ClientGetTicketsInfoRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // ThreeLives.Client.Protocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009E207 : 647687
        // base type : DeepMMO.Protocol.Request
        public static void W_ThreeLives_Client_Protocol_Protocol_Client_ClientGetTicketsInfoRequest(IOutputStream output, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Protocol.Client.ClientGetTicketsInfoRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.c2s_functionid);
        }
        public static void R_ThreeLives_Client_Protocol_Protocol_Client_ClientGetTicketsInfoRequest(IInputStream input, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Protocol.Client.ClientGetTicketsInfoRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_functionid = input.GetUTF();
        }      
    }
}
