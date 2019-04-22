
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.FriendsInfoSnapData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00050502 : 328962
        // base type : TLProtocol.Data.SocialBaseSnapData
        public static void W_TLProtocol_Data_FriendsInfoSnapData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.FriendsInfoSnapData)msg;
            W_TLProtocol_Data_SocialBaseSnapData(output, data);
            output.PutDateTime(data.leaveTime);
            output.PutS32(data.relationLv);
            output.PutS32(data.relationExp);
        }
        public static void R_TLProtocol_Data_FriendsInfoSnapData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.FriendsInfoSnapData)msg;
            R_TLProtocol_Data_SocialBaseSnapData(input, data);
            data.leaveTime = input.GetDateTime();
            data.relationLv = input.GetS32();
            data.relationExp = input.GetS32();
        }      
    }
}

