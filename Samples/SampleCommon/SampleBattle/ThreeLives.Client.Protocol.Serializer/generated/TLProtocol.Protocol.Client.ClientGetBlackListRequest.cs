
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetBlackListRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0005000B : 327691
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientGetBlackListRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetBlackListRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetBlackListRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetBlackListRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            
        }      
    }
}

