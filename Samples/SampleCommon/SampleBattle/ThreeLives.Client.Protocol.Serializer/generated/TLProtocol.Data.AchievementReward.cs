
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.AchievementReward


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x000401F6 : 262646
        // base type : 
        public static void W_TLProtocol_Data_AchievementReward(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.AchievementReward)msg;
                        
            output.PutS32(data.templateid);
            output.PutS32(data.num);
        }
        public static void R_TLProtocol_Data_AchievementReward(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.AchievementReward)msg;
                        
            data.templateid = input.GetS32();
            data.num = input.GetS32();
        }      
    }
}

