
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.LogicTimeNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00035004 : 217092
        // base type : DeepMMO.Protocol.Response
        public static void W_DeepMMO_Protocol_Client_LogicTimeNotify(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.LogicTimeNotify)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.index);
            output.PutDateTime(data.time);
        }
        public static void R_DeepMMO_Protocol_Client_LogicTimeNotify(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.LogicTimeNotify)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.index = input.GetS32();
            data.time = input.GetDateTime();
        }      
    }
}

