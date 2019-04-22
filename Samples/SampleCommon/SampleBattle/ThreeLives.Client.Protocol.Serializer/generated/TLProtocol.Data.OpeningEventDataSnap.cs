
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.OpeningEventDataSnap


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00040258 : 262744
        // base type : TLProtocol.Data.AchievementDataSnap
        public static void W_TLProtocol_Data_OpeningEventDataSnap(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.OpeningEventDataSnap)msg;
            W_TLProtocol_Data_AchievementDataSnap(output, data);
            
        }
        public static void R_TLProtocol_Data_OpeningEventDataSnap(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.OpeningEventDataSnap)msg;
            R_TLProtocol_Data_AchievementDataSnap(input, data);
            
        }      
    }
}

