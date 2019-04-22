
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetEnemyListResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0006A012 : 434194
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetEnemyListResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetEnemyListResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutObj(data.s2c_data);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetEnemyListResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetEnemyListResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_data = input.GetObj<TLProtocol.Data.EnemySnapData>();
        }      
    }
}
