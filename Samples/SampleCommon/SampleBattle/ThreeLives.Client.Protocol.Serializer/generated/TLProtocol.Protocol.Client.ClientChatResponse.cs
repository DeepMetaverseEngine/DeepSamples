
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientChatResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00092002 : 598018
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientChatResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientChatResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            
        }
        public static void R_TLProtocol_Protocol_Client_ClientChatResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientChatResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            
        }      
    }
}

