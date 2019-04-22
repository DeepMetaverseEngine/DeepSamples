
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGuildFortSignUpResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009804A : 622666
        // base type : TLProtocol.Protocol.Client.GuildResponse
        public static void W_TLProtocol_Protocol_Client_ClientGuildFortSignUpResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGuildFortSignUpResponse)msg;
            W_TLProtocol_Protocol_Client_GuildResponse(output, data);
            
        }
        public static void R_TLProtocol_Protocol_Client_ClientGuildFortSignUpResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGuildFortSignUpResponse)msg;
            R_TLProtocol_Protocol_Client_GuildResponse(input, data);
            
        }      
    }
}

