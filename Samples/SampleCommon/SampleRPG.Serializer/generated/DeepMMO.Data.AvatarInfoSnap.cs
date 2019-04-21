
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Data.AvatarInfoSnap


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace SampleRPG
{
    // DeepMMO.Data
    public partial class Serializer
    {
        // msg id    : 0x00022006 : 139270
        // base type : 
        public static void W_DeepMMO_Data_AvatarInfoSnap(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Data.AvatarInfoSnap)msg;
                        
            output.PutS32(data.PartTag);
            output.PutUTF(data.FileName);
            output.PutUTF(data.DefaultName);
        }
        public static void R_DeepMMO_Data_AvatarInfoSnap(IInputStream input, object msg)
        {
            var data = (DeepMMO.Data.AvatarInfoSnap)msg;
                        
            data.PartTag = input.GetS32();
            data.FileName = input.GetUTF();
            data.DefaultName = input.GetUTF();
        }      
    }
}
