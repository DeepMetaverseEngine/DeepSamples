using DeepCore;
using DeepCore.IO;
using System;
using System.Collections.Generic;
using System.Text;
using TLProtocol.Protocol.Client;

namespace ThreeLives.Client.Protocol.Data
{
    [MessageType(Constants.TL_ACTIVITY_START + 100)]
    public class TLClientActivityData : ISerializable
    {
        /// <summary>
        /// 领奖记录:
        /// KEY 是累积的活跃度 比如 30 50 60
        /// VAL 是0和1 0代表未领取，1代表已领取.
        /// </summary>
        public HashMap<int, int> RewardRecord;
        /// <summary>
        ///累积活跃点数.
        /// </summary>
        public int ActivityPoint;
        /// <summary>
        /// 活动列表.
        /// </summary>
        public List<TLClientActivitySnap> ActivityList;
    }

    [MessageType(Constants.TL_ACTIVITY_START + 101)]
    public class TLClientActivitySnap : ISerializable
    {
        /// <summary>
        /// 活动ID.
        /// </summary>
        public string function_id;
        /// <summary>
        /// 当前值.
        /// </summary>
        public int cur_val;
        /// <summary>
        /// 目标值.
        /// </summary>
        public int target_val;
    }
}
