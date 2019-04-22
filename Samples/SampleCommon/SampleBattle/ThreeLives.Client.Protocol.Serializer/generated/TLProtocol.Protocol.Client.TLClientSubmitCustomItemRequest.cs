
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientSubmitCustomItemRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00073013 : 471059
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_TLClientSubmitCustomItemRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientSubmitCustomItemRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutList(data.c2s_data, output.PutObj);
            output.PutS32(data.c2s_questId);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientSubmitCustomItemRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientSubmitCustomItemRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_data = input.GetList<TLProtocol.Data.SubmitItemData>(input.GetObj<TLProtocol.Data.SubmitItemData>);
            data.c2s_questId = input.GetS32();
        }      
    }
}
