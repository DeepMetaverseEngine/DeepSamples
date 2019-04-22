
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientGetTurnTableReponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x000402C7 : 262855
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientGetTurnTableReponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetTurnTableReponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutList(data.s2c_data, output.PutObj);
            output.PutS32(data.s2c_times);
            output.PutList(data.s2c_RewardList, output.PutObj);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientGetTurnTableReponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientGetTurnTableReponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_data = input.GetList<TLProtocol.Data.TurnTableReward>(input.GetObj<TLProtocol.Data.TurnTableReward>);
            data.s2c_times = input.GetS32();
            data.s2c_RewardList = input.GetList<TLProtocol.Data.TurnTableGotReward>(input.GetObj<TLProtocol.Data.TurnTableGotReward>);
        }      
    }
}

