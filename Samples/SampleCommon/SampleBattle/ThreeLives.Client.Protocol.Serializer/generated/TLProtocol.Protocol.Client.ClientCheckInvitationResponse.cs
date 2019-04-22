
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientCheckInvitationResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00050036 : 327734
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientCheckInvitationResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientCheckInvitationResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            
        }
        public static void R_TLProtocol_Protocol_Client_ClientCheckInvitationResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientCheckInvitationResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            
        }      
    }
}

