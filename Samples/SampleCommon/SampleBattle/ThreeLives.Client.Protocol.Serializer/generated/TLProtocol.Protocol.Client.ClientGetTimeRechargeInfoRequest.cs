
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetTimeRechargeInfoRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009D005 : 643077
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientGetTimeRechargeInfoRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetTimeRechargeInfoRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutS32(data.c2s_platformID);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetTimeRechargeInfoRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetTimeRechargeInfoRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_platformID = input.GetS32();
        }      
    }
}

