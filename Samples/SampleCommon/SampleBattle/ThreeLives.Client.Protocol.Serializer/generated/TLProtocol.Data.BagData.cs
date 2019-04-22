
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.BagData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00025002 : 151554
        // base type : 
        public static void W_TLProtocol_Data_BagData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.BagData)msg;
                        
            output.PutS32(data.MaxSize);
            output.PutS32(data.EnableSize);
            output.PutMap(data.Slots, output.PutS32, output.PutObj);
        }
        public static void R_TLProtocol_Data_BagData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.BagData)msg;
                        
            data.MaxSize = input.GetS32();
            data.EnableSize = input.GetS32();
            data.Slots = input.GetMap<int, TLProtocol.Data.EntityItemData>(input.GetS32, input.GetObj<TLProtocol.Data.EntityItemData>);
        }      
    }
}

