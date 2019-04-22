using DeepCore.IO;
using System;
using System.Collections.Generic;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;

namespace ThreeLives.Client.Protocol.Data
{
    [MessageType(TLConstants.TL_BACTIVITY_START + 1)]
    public class TLBActivitySnapData : ISerializable
    {
        public int id;

        public int state;

        public List<RequireSnapData> requireList;
    }

    [MessageType(TLConstants.TL_BACTIVITY_START + 2)]
    public class TLTimeRechargeProductInfo : ISerializable
    {
        /// <summary>
        /// 剩余时间.
        /// </summary>
        public DateTime s2c_leftTimeUTC;
        /// <summary>
        /// 物品ID.
        /// </summary>
        public int s2c_productID;
        /// <summary>
        /// 总购买次数.
        /// </summary>
        public int s2c_buyCount_Total;

        /// <summary>
        /// 是否已领取.
        /// </summary>
        public bool s2c_available;
    }

    [MessageType(TLConstants.TL_BACTIVITY_START + 3)]
    public class TLDailyRechargeProductInfo : ISerializable
    {
        /// <summary>
        /// 物品ID.
        /// </summary>
        public int s2c_productID;

        /// <summary>
        /// 总购买次数.
        /// </summary>
        public int s2c_buyCount;
    }

    [MessageType(TLConstants.TL_BACTIVITY_START + 4)]
    public class TLFirstRechargeProductInfo 
    {
        /// <summary>
        /// 物品ID.
        /// </summary>
        public int s2c_productID;

        /// <summary>
        /// 总购买次数.
        /// </summary>
        public int s2c_buyCount;
    }

    [MessageType(TLConstants.TL_BACTIVITY_START + 5)]
    public class TLFirstRecharCfgXlsData 
    {
        public int id;
        /// <summary>
        /// 小于等于该金额为档位1.
        /// </summary>
        public int pay_num;
        /// <summary>
        /// 档位激活flag
        /// </summary>
        public string reward_type_low;
        /// <summary>
        /// 档位激活flag
        /// </summary>
        public string reward_type_high;
        /// <summary>
        /// 领取天数记录.
        /// </summary>
        public string record_flag_high;
        /// <summary>
        /// 领取天数记录.
        /// </summary>
        public string record_flag_low;
        /// <summary>
        /// 当日领取记录
        /// </summary>
        public string get_flag_high;
        /// <summary>
        /// 当日领取记录
        /// </summary>
        public string get_flag_low;
        /// <summary>
        /// 最大领取次数.
        /// </summary>
        public int get_num;
    }

    /// <summary>
    /// 消耗积分活动配置.
    /// </summary>
    public class TLActExchangePointsXlsData
    {
        public int id;

        /// <summary>
        /// 消耗金额
        /// </summary>
        public int cost_gold;
        /// <summary>
        /// 获得积分.
        /// </summary>
        public int get_point;
    }


    [MessageType(TLConstants.TL_BACTIVITY_START + 6)]
    public class TLPlantingProductInfo : ISerializable
    {
        /// <summary>
        /// 物品ID.
        /// </summary>
        public int s2c_productID;
        /// <summary>
        /// 状态   /// 0 未购买 1未浇水 2浇水 3 到达上限
        /// </summary>
        public int s2c_state;

        //浇水次数
        public int s2c_times;
    }
}
