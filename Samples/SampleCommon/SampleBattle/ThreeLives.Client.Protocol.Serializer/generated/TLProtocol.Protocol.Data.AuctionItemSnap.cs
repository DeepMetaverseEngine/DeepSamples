
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Data.AuctionItemSnap


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Data
    public partial class Serializer
    {
        // msg id    : 0x000A0101 : 655617
        // base type : 
        public static void W_TLProtocol_Protocol_Data_AuctionItemSnap(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Data.AuctionItemSnap)msg;
                        
            output.PutUTF(data.uuid);
            output.PutUTF(data.seller);
            output.PutS32(data.price);
            output.PutS32(data.score);
            output.PutS32(data.tax);
            output.PutDateTime(data.time);
            output.PutObj(data.item);
        }
        public static void R_TLProtocol_Protocol_Data_AuctionItemSnap(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Data.AuctionItemSnap)msg;
                        
            data.uuid = input.GetUTF();
            data.seller = input.GetUTF();
            data.price = input.GetS32();
            data.score = input.GetS32();
            data.tax = input.GetS32();
            data.time = input.GetDateTime();
            data.item = input.GetObj<TLProtocol.Data.ItemSnapData>();
        }      
    }
}

