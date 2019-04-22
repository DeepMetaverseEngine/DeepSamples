
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGridRefineRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00076001 : 483329
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientGridRefineRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGridRefineRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutS32(data.c2s_equipPos);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGridRefineRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGridRefineRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_equipPos = input.GetS32();
        }      
    }
}
