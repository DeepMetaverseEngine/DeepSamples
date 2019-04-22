
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.SubmitItemData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00066003 : 417795
        // base type : 
        public static void W_TLProtocol_Data_SubmitItemData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.SubmitItemData)msg;
                        
            output.PutS32(data.index);
            output.PutS32(data.count);
        }
        public static void R_TLProtocol_Data_SubmitItemData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.SubmitItemData)msg;
                        
            data.index = input.GetS32();
            data.count = input.GetS32();
        }      
    }
}

