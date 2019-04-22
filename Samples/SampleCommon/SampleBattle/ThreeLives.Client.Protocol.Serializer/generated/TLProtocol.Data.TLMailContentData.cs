
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.TLMailContentData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00082003 : 532483
        // base type : 
        public static void W_TLProtocol_Data_TLMailContentData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.TLMailContentData)msg;
                        
            output.PutUTF(data.txt_content);
            output.PutList(data.attachment_list, output.PutObj);
        }
        public static void R_TLProtocol_Data_TLMailContentData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.TLMailContentData)msg;
                        
            data.txt_content = input.GetUTF();
            data.attachment_list = input.GetList<TLProtocol.Data.ItemSnapData>(input.GetObj<TLProtocol.Data.ItemSnapData>);
        }      
    }
}
