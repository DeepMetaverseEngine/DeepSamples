
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.GuildWantedData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00040028 : 262184
        // base type : 
        public static void W_TLProtocol_Data_GuildWantedData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.GuildWantedData)msg;
                        
            output.PutS32(data.curReceivedTimes);
            output.PutS32(data.maxReceivedTimes);
            output.PutS32(data.curPartakeTimes);
            output.PutS32(data.maxPartakeTimes);
            output.PutS32(data.curRefreshTimes);
            output.PutS32(data.maxRefreshTimes);
            output.PutDateTime(data.refreshTime);
            output.PutList(data.QuestMap, output.PutObj);
        }
        public static void R_TLProtocol_Data_GuildWantedData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.GuildWantedData)msg;
                        
            data.curReceivedTimes = input.GetS32();
            data.maxReceivedTimes = input.GetS32();
            data.curPartakeTimes = input.GetS32();
            data.maxPartakeTimes = input.GetS32();
            data.curRefreshTimes = input.GetS32();
            data.maxRefreshTimes = input.GetS32();
            data.refreshTime = input.GetDateTime();
            data.QuestMap = input.GetList<TLProtocol.Data.GuildWantedInfoData>(input.GetObj<TLProtocol.Data.GuildWantedInfoData>);
        }      
    }
}
