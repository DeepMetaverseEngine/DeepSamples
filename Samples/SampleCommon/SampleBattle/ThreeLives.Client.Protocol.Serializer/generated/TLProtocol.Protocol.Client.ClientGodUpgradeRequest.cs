
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGodUpgradeRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00099505 : 627973
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientGodUpgradeRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGodUpgradeRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutS32(data.c2s_god_id);
            output.PutS32(data.c2s_god_lv);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGodUpgradeRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGodUpgradeRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_god_id = input.GetS32();
            data.c2s_god_lv = input.GetS32();
        }      
    }
}

