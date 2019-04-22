
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetAllPackageResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00037011 : 225297
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetAllPackageResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetAllPackageResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutMap(data.s2c_bags, output.PutU8, output.PutObj);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetAllPackageResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetAllPackageResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_bags = input.GetMap<byte, TLProtocol.Data.BagData>(input.GetU8, input.GetObj<TLProtocol.Data.BagData>);
        }      
    }
}
