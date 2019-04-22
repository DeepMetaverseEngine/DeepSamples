
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientLeaveTeamResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00095008 : 610312
        // base type : TLProtocol.Protocol.Client.TeamResponse
        public static void W_TLProtocol_Protocol_Client_TLClientLeaveTeamResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientLeaveTeamResponse)msg;
            W_TLProtocol_Protocol_Client_TeamResponse(output, data);
            
        }
        public static void R_TLProtocol_Protocol_Client_TLClientLeaveTeamResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientLeaveTeamResponse)msg;
            R_TLProtocol_Protocol_Client_TeamResponse(input, data);
            
        }      
    }
}

