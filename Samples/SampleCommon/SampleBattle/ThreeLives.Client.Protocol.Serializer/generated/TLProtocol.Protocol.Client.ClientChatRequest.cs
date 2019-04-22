
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientChatRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00092001 : 598017
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientChatRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientChatRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.to_uuid);
            output.PutUTF(data.content);
            output.PutS16(data.channel_type);
            output.PutS16(data.show_type);
            output.PutArray(data.show_channel, output.PutS16);
            output.PutS16(data.func_type);
            output.PutUTF(data.from_name);
            output.PutUTF(data.from_uuid);
        }
        public static void R_TLProtocol_Protocol_Client_ClientChatRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientChatRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.to_uuid = input.GetUTF();
            data.content = input.GetUTF();
            data.channel_type = input.GetS16();
            data.show_type = input.GetS16();
            data.show_channel = input.GetArray<short>(input.GetS16);
            data.func_type = input.GetS16();
            data.from_name = input.GetUTF();
            data.from_uuid = input.GetUTF();
        }      
    }
}
