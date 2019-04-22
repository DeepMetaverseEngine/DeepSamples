
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.TLProgressData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00066002 : 417794
        // base type : 
        public static void W_TLProtocol_Data_TLProgressData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.TLProgressData)msg;
                        
            output.PutUTF(data.Type);
            output.PutS32(data.Arg1);
            output.PutS32(data.Arg2);
            output.PutS32(data.CurValue);
            output.PutS32(data.TargetValue);
        }
        public static void R_TLProtocol_Data_TLProgressData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.TLProgressData)msg;
                        
            data.Type = input.GetUTF();
            data.Arg1 = input.GetS32();
            data.Arg2 = input.GetS32();
            data.CurValue = input.GetS32();
            data.TargetValue = input.GetS32();
        }      
    }
}
