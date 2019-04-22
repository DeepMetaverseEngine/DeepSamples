
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetMonsterInfoResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009802E : 622638
        // base type : TLProtocol.Protocol.Client.GuildResponse
        public static void W_TLProtocol_Protocol_Client_ClientGetMonsterInfoResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetMonsterInfoResponse)msg;
            W_TLProtocol_Protocol_Client_GuildResponse(output, data);
            output.PutObj(data.s2c_monsterInfo);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetMonsterInfoResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetMonsterInfoResponse)msg;
            R_TLProtocol_Protocol_Client_GuildResponse(input, data);
            data.s2c_monsterInfo = input.GetObj<TLProtocol.Protocol.Data.GuildMonsterSnapData>();
        }      
    }
}

