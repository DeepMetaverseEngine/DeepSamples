
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientPhotoInfoRequest


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00079003 : 495619
        // base type : DeepMMO.Protocol.Request
        public static void W_TLProtocol_Protocol_Client_ClientPhotoInfoRequest(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPhotoInfoRequest)msg;
            W_DeepMMO_Protocol_Request(output, data);
            output.PutUTF(data.c2s_roleId);
        }
        public static void R_TLProtocol_Protocol_Client_ClientPhotoInfoRequest(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPhotoInfoRequest)msg;
            R_DeepMMO_Protocol_Request(input, data);
            data.c2s_roleId = input.GetUTF();
        }      
    }
}

