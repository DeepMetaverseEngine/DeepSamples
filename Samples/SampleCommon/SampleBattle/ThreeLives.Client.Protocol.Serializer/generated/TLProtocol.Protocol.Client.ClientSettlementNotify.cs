
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientSettlementNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00096016 : 614422
        // base type : DeepMMO.Protocol.Notify
        public static void W_TLProtocol_Protocol_Client_ClientSettlementNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientSettlementNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutS32(data.s2c_exp);
            output.PutS32(data.s2c_gold);
            output.PutDateTime(data.s2c_counttime);
            output.PutBool(data.s2c_noAward);
            output.PutS32(data.s2c_finishtime_sec);
            output.PutU8(data.s2c_status);
            output.PutList(data.s2c_itemList, output.PutObj);
            output.PutMap(data.s2c_ext, output.PutUTF, output.PutUTF);
        }
        public static void R_TLProtocol_Protocol_Client_ClientSettlementNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientSettlementNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.s2c_exp = input.GetS32();
            data.s2c_gold = input.GetS32();
            data.s2c_counttime = input.GetDateTime();
            data.s2c_noAward = input.GetBool();
            data.s2c_finishtime_sec = input.GetS32();
            data.s2c_status = input.GetU8();
            data.s2c_itemList = input.GetList<TLProtocol.Data.TLDropItemSnapData>(input.GetObj<TLProtocol.Data.TLDropItemSnapData>);
            data.s2c_ext = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
        }      
    }
}
