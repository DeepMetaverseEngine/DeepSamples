
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetStoreBoughtInfoResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0006A002 : 434178
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetStoreBoughtInfoResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetStoreBoughtInfoResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutList(data.salelist, output.PutS32);
            output.PutMap(data.s2c_data, output.PutS32, output.PutObj);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetStoreBoughtInfoResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetStoreBoughtInfoResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.salelist = input.GetList<int>(input.GetS32);
            data.s2c_data = input.GetMap<int, TLProtocol.Data.StoreSnapData>(input.GetS32, input.GetObj<TLProtocol.Data.StoreSnapData>);
        }      
    }
}
