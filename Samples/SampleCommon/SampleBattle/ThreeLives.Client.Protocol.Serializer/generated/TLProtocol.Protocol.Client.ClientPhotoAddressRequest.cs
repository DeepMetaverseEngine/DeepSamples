
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientPhotoAddressRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00079001 : 495617
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientPhotoAddressRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPhotoAddressRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.c2s_roleId);
        }
        public static void R_TLProtocol_Protocol_Client_ClientPhotoAddressRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPhotoAddressRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_roleId = input.GetUTF();
        }      
    }
}
