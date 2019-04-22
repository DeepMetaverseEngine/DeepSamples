
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientBuyDailyDungeonTicketsResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00096027 : 614439
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientBuyDailyDungeonTicketsResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientBuyDailyDungeonTicketsResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutUTF(data.s2c_dungeon_type);
            output.PutS32(data.s2c_count);
        }
        public static void R_TLProtocol_Protocol_Client_ClientBuyDailyDungeonTicketsResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientBuyDailyDungeonTicketsResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_dungeon_type = input.GetUTF();
            data.s2c_count = input.GetS32();
        }      
    }
}

