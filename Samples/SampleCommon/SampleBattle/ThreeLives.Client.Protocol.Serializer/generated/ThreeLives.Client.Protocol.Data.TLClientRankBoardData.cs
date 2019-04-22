
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// ThreeLives.Client.Protocol.Data.TLClientRankBoardData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // ThreeLives.Client.Protocol.Data
    public partial class Serializer
    {
        // msg id    : 0x0009B064 : 634980
        // base type : 
        public static void W_ThreeLives_Client_Protocol_Data_TLClientRankBoardData(IOutputStream output, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Data.TLClientRankBoardData)msg;
                        
            output.PutS32(data.id);
            output.PutUTF(data.name);
            output.PutList(data.childList, output.PutObj);
        }
        public static void R_ThreeLives_Client_Protocol_Data_TLClientRankBoardData(IInputStream input, object msg)
        {
            var data = (ThreeLives.Client.Protocol.Data.TLClientRankBoardData)msg;
                        
            data.id = input.GetS32();
            data.name = input.GetUTF();
            data.childList = input.GetList<ThreeLives.Client.Protocol.Data.TLClientRankSubBoardData>(input.GetObj<ThreeLives.Client.Protocol.Data.TLClientRankSubBoardData>);
        }      
    }
}
