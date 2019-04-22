
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientAddEnemyRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00050013 : 327699
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientAddEnemyRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientAddEnemyRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.roleID);
        }
        public static void R_TLProtocol_Protocol_Client_ClientAddEnemyRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientAddEnemyRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.roleID = input.GetUTF();
        }      
    }
}
