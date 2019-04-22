
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientAlertMessageNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00097007 : 618503
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientAlertMessageNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientAlertMessageNotify)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutUTF(data.s2c_id);
            output.PutUTF(data.s2c_roleID);
            output.PutBool(data.s2c_agree);
        }
        public static void R_TLProtocol_Protocol_Client_ClientAlertMessageNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientAlertMessageNotify)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_id = input.GetUTF();
            data.s2c_roleID = input.GetUTF();
            data.s2c_agree = input.GetBool();
        }      
    }
}
