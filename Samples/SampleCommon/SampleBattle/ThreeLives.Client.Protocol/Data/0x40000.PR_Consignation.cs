
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 100)]
    public class ConsignationData : ISerializable
    {
        public const byte QuestState_Available = 1;    //可接取.
        public const byte QuestState_Accepted = 2;     //已接取.
        public const byte QuestState_Completed = 3;    //已完成.
        
        public List<ConsignationInfoData> QuestMap;
    }

    [MessageType(TLConstants.TL_PlayRule + 101)]
    public class ConsignationInfoData : ISerializable
    {
        // 任务状态
       public int state;
       //wantedid
       public int wantedid;
        //cd(秒)
       public int cdtime;
        //位置
       public int index;
    }
}
