
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    /// <summary>
    /// 任务快照.
    /// </summary>
    [MessageType(TLConstants.TL_PlayRule + 500)]
    public class AchievementDataSnap : ISerializable
    {
       
        /// <summary>
        /// ID.
        /// </summary>
        public int id;
        /// <summary>
        /// 进度.
        /// </summary>
        public List<TLProgressData> progressList;
        /// <summary>
        /// 状态.
        /// </summary>
        public int state;
        //任务类型
        public int type;
        public int rank;
        public DateTime finishTime;

    }

    [MessageType(TLConstants.TL_PlayRule + 501)]
    public class CatalogData : ISerializable
    {
        //类型
        public int type;

        //当前数据
        public int curVal;

        //最大数据
        public int maxVal;
    }
    
    [MessageType(TLConstants.TL_PlayRule + 502)]
    public class AchievementReward : ISerializable
    {
        //物品模板id
        public int templateid;

        //数量
        public int num;
    }
      
}
