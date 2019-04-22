
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientGetLvLimitInfoResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0009602B : 614443
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientGetLvLimitInfoResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetLvLimitInfoResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_left_mins);
            output.PutS64(data.s2c_overflow_exp);
            output.PutS32(data.s2c_cur_lv_limit);
            output.PutS32(data.s2c_next_lv_limit);
        }
        public static void R_TLProtocol_Protocol_Client_ClientGetLvLimitInfoResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientGetLvLimitInfoResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_left_mins = input.GetS32();
            data.s2c_overflow_exp = input.GetS64();
            data.s2c_cur_lv_limit = input.GetS32();
            data.s2c_next_lv_limit = input.GetS32();
        }      
    }
}

