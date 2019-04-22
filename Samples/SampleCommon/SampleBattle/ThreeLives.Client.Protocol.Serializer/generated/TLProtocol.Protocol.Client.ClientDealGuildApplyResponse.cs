
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientDealGuildApplyResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00098010 : 622608
        // base type : TLProtocol.Protocol.Client.GuildResponse
        public static void W_TLProtocol_Protocol_Client_ClientDealGuildApplyResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientDealGuildApplyResponse)msg;
            W_TLProtocol_Protocol_Client_GuildResponse(output, data);
            output.PutS32(data.s2c_memberCount);
        }
        public static void R_TLProtocol_Protocol_Client_ClientDealGuildApplyResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientDealGuildApplyResponse)msg;
            R_TLProtocol_Protocol_Client_GuildResponse(input, data);
            data.s2c_memberCount = input.GetS32();
        }      
    }
}
