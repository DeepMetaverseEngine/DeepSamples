
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetRechargeInfoResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009E002 : 647170
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetRechargeInfoResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetRechargeInfoResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutMap(data.s2c_data, output.PutS32, output.PutObj);
            output.PutS32(data.vip_level);
            output.PutS32(data.vip_exp);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetRechargeInfoResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetRechargeInfoResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_data = input.GetMap<int, TLProtocol.Data.RechargeProductSnap>(input.GetS32, input.GetObj<TLProtocol.Data.RechargeProductSnap>);
            data.vip_level = input.GetS32();
            data.vip_exp = input.GetS32();
        }      
    }
}

