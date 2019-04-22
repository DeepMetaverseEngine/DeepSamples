
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// ThreeLives.Client.Protocol.Data.TLBActivitySnapData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // ThreeLives.Client.Protocol.Data
    public partial class Serializer
    {
        // msg id    : 0x0009D501 : 644353
        // base type : 
        public static void W_ThreeLives_Client_Protocol_Data_TLBActivitySnapData(IOutputStream output, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Data.TLBActivitySnapData)msg;
                        
            output.PutS32(data.id);
            output.PutS32(data.state);
            output.PutList(data.requireList, output.PutObj);
        }
        public static void R_ThreeLives_Client_Protocol_Data_TLBActivitySnapData(IInputStream input, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Data.TLBActivitySnapData)msg;
                        
            data.id = input.GetS32();
            data.state = input.GetS32();
            data.requireList = input.GetList<TLProtocol.Data.RequireSnapData>(input.GetObj<TLProtocol.Data.RequireSnapData>);
        }      
    }
}

