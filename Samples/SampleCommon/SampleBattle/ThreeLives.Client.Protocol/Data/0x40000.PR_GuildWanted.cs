
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 40)]
    public class GuildWantedData : ISerializable
    {
        public const byte QuestState_Available = 1;    //可接取.
        public const byte QuestState_Accepted = 2;     //已接取.
        public const byte QuestState_Completed = 3;    //已完成.

        public int curReceivedTimes;// 领取次数
        public int maxReceivedTimes;// 领取最大次数
        public int curPartakeTimes;// 当日参与数
        public int maxPartakeTimes;// 当日最大参与数
        public int curRefreshTimes;//当前刷新次数
        public int maxRefreshTimes;//最大刷新次数
        public DateTime refreshTime;//刷新时间戳
        public List<GuildWantedInfoData> QuestMap;
    }

    [MessageType(TLConstants.TL_PlayRule + 41)]
    public class GuildWantedInfoData : ISerializable
    {
       public int state;
       public int wantedid;
    }
}
