
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.SocialGiftRecordSnap


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x0005050D : 328973
        // base type : 
        public static void W_TLProtocol_Data_SocialGiftRecordSnap(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.SocialGiftRecordSnap)msg;
                        
            output.PutUTF(data.transactionId);
            output.PutUTF(data.presenter);
            output.PutUTF(data.receiver);
            output.PutS32(data.templateId);
            output.PutU32(data.num);
            output.PutDateTime(data.time);
        }
        public static void R_TLProtocol_Data_SocialGiftRecordSnap(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.SocialGiftRecordSnap)msg;
                        
            data.transactionId = input.GetUTF();
            data.presenter = input.GetUTF();
            data.receiver = input.GetUTF();
            data.templateId = input.GetS32();
            data.num = input.GetU32();
            data.time = input.GetDateTime();
        }      
    }
}

