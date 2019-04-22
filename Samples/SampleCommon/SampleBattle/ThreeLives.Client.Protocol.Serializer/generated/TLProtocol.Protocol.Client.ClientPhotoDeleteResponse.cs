
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientPhotoDeleteResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00079008 : 495624
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientPhotoDeleteResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPhotoDeleteResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutUTF(data.s2c_photoName);
        }
        public static void R_TLProtocol_Protocol_Client_ClientPhotoDeleteResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPhotoDeleteResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_photoName = input.GetUTF();
        }      
    }
}
