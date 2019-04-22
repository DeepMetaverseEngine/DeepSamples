
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientWeddingInfoNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00082069 : 532585
        // base type : DeepMMO.Protocol.Notify
        public static void W_TLProtocol_Protocol_Client_TLClientWeddingInfoNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientWeddingInfoNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutMap(data.info, output.PutS32, output.PutUTF);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientWeddingInfoNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientWeddingInfoNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.info = input.GetMap<int, string>(input.GetS32, input.GetUTF);
        }      
    }
}
