
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientMatchStateNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00095014 : 610324
        // base type : DeepMMO.Protocol.Notify
        public static void W_TLProtocol_Protocol_Client_ClientMatchStateNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientMatchStateNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutBool(data.s2c_matching);
            output.PutDateTime(data.s2c_startUtc);
            output.PutUTF(data.s2c_channel);
            output.PutUTF(data.s2c_desc);
        }
        public static void R_TLProtocol_Protocol_Client_ClientMatchStateNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientMatchStateNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.s2c_matching = input.GetBool();
            data.s2c_startUtc = input.GetDateTime();
            data.s2c_channel = input.GetUTF();
            data.s2c_desc = input.GetUTF();
        }      
    }
}

