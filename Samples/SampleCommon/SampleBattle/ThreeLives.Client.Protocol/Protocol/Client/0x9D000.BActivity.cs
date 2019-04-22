using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeLives.Client.Protocol.Data;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获取活动信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 1)]
    public class ClientGetBActityInfosRequest : Request, ILogicProtocol
    {
        public int c2s_activity_id;
    }

    /// <summary>
    ///  获取活动信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 2)]
    public class ClientGetBActityInfosResponse : Response, ILogicProtocol
    {
        public HashMap<int, TLBActivitySnapData> activityMap;

        [MessageCodeAttribute("客户端参数错误")]
        public const int CODE_ARG_ERR = 501;

        [MessageCodeAttribute("活动尚未开启")]
        public const int CODE_NOT_OPEN = 502;

        [MessageCodeAttribute("活动已经结束")]
        public const int CODE_CLOSED = 503;

        [MessageCodeAttribute("不在活动时间内")]
        public const int CODE_NOT_INTIME = 504;

    }


    /// <summary>
    /// 获取活动奖励.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 3)]
    public class ClientGetBActivityRewardRequest : Request, ILogicProtocol
    {
        public int c2s_activity_id;

        public int c2s_sub_id;
    }
    #region 月卡周卡.

    /// <summary>
    /// 获取活跃度奖励.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 4)]
    public class ClientGetBActivityRewardResponse : Response, ILogicProtocol
    {

        [MessageCodeAttribute("客户端参数错误")]
        public const int CODE_ARG_ERR = 501;

        [MessageCodeAttribute("活动尚未开启")]
        public const int CODE_NOT_OPEN = 502;

        [MessageCodeAttribute("活动已经结束")]
        public const int CODE_CLOSED = 503;

        [MessageCodeAttribute("尚未达成条件")]
        public const int CODE_UNCHIEVED = 504;

        [MessageCodeAttribute("你已经领取过该奖励")]
        public const int CODE_COMPLETED = 505;

        [MessageCodeAttribute("不在活动时间内")]
        public const int CODE_NOT_INTIME = 506;

    }

    /// <summary>
    /// 获取周卡月卡商品信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 5)]
    public class ClientGetTimeRechargeInfoRequest : Request, ILogicProtocol
    {
        public int c2s_platformID;
    }

    /// <summary>
    /// 获取周卡月卡商品信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 6)]
    public class ClientGetTimeRechargeInfoResponse : Response, ILogicProtocol
    {
        public List<TLTimeRechargeProductInfo> s2c_list;
    }

    /// <summary>
    /// 获取周卡奖励请求.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 7)]
    public class ClientGetTimeRechargeRewardRequest : Request, ILogicProtocol
    {
        public int c2s_productID;
    }

    /// <summary>
    /// 获取周卡奖励回执.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 8)]
    public class ClientGetTimeRechargeRewardResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("特权卡已过期")]
        public const int CODE_OVER_TIME = 501;
        [MessageCodeAttribute("奖励数据配置异常")]
        public const int CODE_CARD_DATA_ERROR = 502;

        public int s2c_productID;
    }

    /// <summary>
    /// 月卡周卡信息更新.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 9)]
    public class ClientTimeRechargeInfoInfoNotify : Notify, ILogicProtocol
    {
        public List<TLTimeRechargeProductInfo> s2c_list;
    }

    #endregion

    #region 每日充值礼包.

    [MessageType(Constants.TL_BACTIVITY_START + 10)]
    public class ClientGetDailyRechargeGiftInfoRequest : Request, ILogicProtocol
    {
        public int c2s_platformID;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 11)]
    public class ClientGetDailyRechargeGiftInfoResponse : Response, ILogicProtocol
    {
        public List<TLDailyRechargeProductInfo> s2c_list;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 12)]
    public class ClientGetDailyRechargeGiftInfoNotify : Notify, ILogicProtocol
    {
        public List<TLDailyRechargeProductInfo> s2c_list;
    }

    #endregion

    #region 首充

    [MessageType(Constants.TL_BACTIVITY_START + 17)]
    public class ClientGetFirstRechargeInfoNotify : Notify, ILogicProtocol
    {
        /// <summary>
        /// 开启.
        /// </summary>
        public const int OPEN = 1;
        /// <summary>
        /// 可领取.
        /// </summary>
        public const int AVILIABLE = 2;
        /// <summary>
        /// 关闭（已经领取）.
        /// </summary>
        public const int CLOSE = 3;

        public int s2c_status;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 13)]
    public class ClientGetFirstRechargeRewardRequest : Request, ILogicProtocol
    {
        public int c2s_rank;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 14)]
    public class ClientGetFirstRechargeRewardResponse : Response, ILogicProtocol
    {
        public const int CODE_CONDITION_FAILED = ClientGetFirstRechargeRewardResponse.CODE_ERROR + 1;

        public bool s2c_close;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 15)]
    public class ClientGetFirstRechargeInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_BACTIVITY_START + 16)]
    public class ClientGetFirstRechargeInfoResponse : Response, ILogicProtocol
    {
        /// <summary>
        /// 无0,低1,高2
        /// </summary>
        public int s2c_recharge_rank;
        /// <summary>
        ///领取进度.
        /// </summary>
        public int s2c_get_reward_progress_high;
        /// <summary>
        /// 是否能领取奖励.
        /// </summary>
        public bool s2c_available_high_get;
        /// <summary>
        ///领取进度.
        /// </summary>
        public int s2c_get_reward_progress_low;
        /// <summary>
        /// 是否能领取奖励.
        /// </summary>
        public bool s2c_available_low_get;

        /// <summary>
        /// 首充功能关闭.
        /// </summary>
        public bool s2c_close;
    }

    #endregion

    #region CDKey

    [MessageType(Constants.TL_BACTIVITY_START + 18)]
    public class ClientGetCDKeyRewardRequest : Request, ILogicProtocol
    {
        public int c2s_PlatformID;

        public string c2s_CDkey;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 19)]
    public class ClientGetCDKeyRewardResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("无效的CDKey")]
        public const int CODE_INVALID = 501;
        [MessageCodeAttribute("不在CDKey使用期间内")]
        public const int CODE_OVER_TIME = 502;
        [MessageCodeAttribute("CDKey已使用")]
        public const int CODE_CARD_COMPLETE = 503;
        [MessageCodeAttribute("账号不在领取范围内")]
        public const int CODE_OVER_TIME2 = 504;
        [MessageCodeAttribute("你已经参加过这次活动")]
        public const int CODE_JOIN_OVER = 505;


    }

    #endregion

    
    #region 成长基金.

    [MessageType(Constants.TL_BACTIVITY_START + 20)]
    public class ClientGetFundInfoRequest : Request, ILogicProtocol
    {
        public int c2s_platformID;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 21)]
    public class ClientGetFundInfoResponse : Response, ILogicProtocol
    {
        /// <summary>
        /// ID,Count
        /// </summary>
        public HashMap<int, int> s2c_map;
    }

    [MessageType(Constants.TL_BACTIVITY_START + 22)]
    public class ClientFundInfoNotify : Notify, ILogicProtocol
    {
        /// <summary>
        /// ID,Count
        /// </summary>
        public HashMap<int, int> s2c_map;
    }

    #endregion


    [MessageType(Constants.TL_BACTIVITY_START + 23)]
    public class ClientGetAllBActityInfosRequest : Request, ILogicProtocol
    {
        public int c2s_activity_type;
    }

    /// <summary>
    ///  获取活动信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 24)]
    public class ClientGetAllBActityInfosResponse : Response, ILogicProtocol
    {
        public class ActivityData : ISerializable
        {
            public HashMap<int, TLBActivitySnapData> data;
        }

        public HashMap<int, ActivityData> s2c_activityMap;

        [MessageCodeAttribute("客户端参数错误")]
        public const int CODE_ARG_ERR = 501;

    }

    /// <summary>
    /// 获取充值返利奖励
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 25)]
    public class ClientGetRechargeRebateRewardReqeust : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取充值返利奖励
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 26)]
    public class ClientGetRechargeRebateRewardResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("该服不能参加活动")]
        public const int CODE_ERROR_INVALID_SERVER = ClientGetRechargeRebateRewardResponse.CODE_ERROR + 1;
        [MessageCodeAttribute("不在奖励名单内")]
        public const int CODE_ERROR_INVALID_ACCOUNT = ClientGetRechargeRebateRewardResponse.CODE_ERROR + 2;
        [MessageCodeAttribute("已领取")]
        public const int CODE_ERROR_REWARDED = ClientGetRechargeRebateRewardResponse.CODE_ERROR + 3;
        [MessageCodeAttribute("账号格式错误")]
        public const int CODE_ERROR_ACCOUNT_FORMAT = ClientGetRechargeRebateRewardResponse.CODE_ERROR + 4;
    }

    /// <summary>
    /// 参与封测奖励.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 27)]
    public class ClientGetCBRewardRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 奖励类型.
        /// </summary>
        public byte c2s_type;
    }

    /// <summary>
    /// 参与封测奖励.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 28)]
    public class ClientGetCBRewardResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("该服不能参加活动")]
        public const int CODE_ERROR_INVALID_SERVER = ClientGetCBRewardResponse.CODE_ERROR + 1;
        [MessageCodeAttribute("不在奖励名单内")]
        public const int CODE_ERROR_INVALID_ACCOUNT = ClientGetCBRewardResponse.CODE_ERROR + 2;
        [MessageCodeAttribute("已领取")]
        public const int CODE_ERROR_REWARDED = ClientGetCBRewardResponse.CODE_ERROR + 3;
        [MessageCodeAttribute("账号格式错误")]
        public const int CODE_ERROR_ACCOUNT_FORMAT = ClientGetCBRewardResponse.CODE_ERROR + 4;
    }

    /// <summary>
    /// 获取新春祈福活动信息
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 29)]
    public class ClientGetSFXCQFInfoRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取新春祈福活动信息
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 30)]
    public class ClientGetSFXCQFInfoResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("数据配置错误")]
        public const int CODE_ERROR_DATA_ERROR = ClientGetSFXCQFInfoResponse.CODE_ERROR + 1;
        [MessageCodeAttribute("活动未开放")]
        public const int CODE_ERROR_OPENTIME_ERROR = ClientGetSFXCQFInfoResponse.CODE_ERROR + 2;

        /// <summary>
        /// 当前道具数量.
        /// </summary>
        //public uint[] s2c_items_count;
        /// <summary>
        /// 剩余次数.
        /// </summary>
        public int s2c_times;
    }

    /// <summary>
    /// 祈福
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 31)]
    public class ClientGetSFXCQFRewardRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 祈福
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 32)]
    public class ClientGetSFXCQFRewardResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("数据配置错误")]
        public const int CODE_ERROR_DATA_ERROR = ClientGetSFXCQFRewardResponse.CODE_ERROR + 1;

        [MessageCodeAttribute("条件不足")]
        public const int CODE_ERROR_COND_ERROR = ClientGetSFXCQFRewardResponse.CODE_ERROR + 2;

        [MessageCodeAttribute("发放奖励错误")]
        public const int CODE_ERROR_REWARD_ERROR = ClientGetSFXCQFRewardResponse.CODE_ERROR + 3;

        [MessageCodeAttribute("活动未开放")]
        public const int CODE_ERROR_OPENTIME_ERROR = ClientGetSFXCQFRewardResponse.CODE_ERROR + 4;

        [MessageCodeAttribute("道具不足")]
        public const int CODE_ERROR_ITEM_NOT_ENOUGH = ClientGetSFXCQFRewardResponse.CODE_ERROR + 5;

        public int s2c_times;
        //public uint[] s2c_items_count;
        public List<TLDropItemSnapData> s2c_reward_items;
    }

    /// <summary>
    /// 新春活动领红包
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 33)]
    public class ClientGetSFHBInfoRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 新春活动领红包
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 34)]
    public class ClientGetSFHBInfoResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("活动未开放")]
        public const int CODE_ERROR_DATA_ERROR = ClientGetSFHBInfoResponse.CODE_ERROR + 1;

        /// <summary>
        /// 元宝数量.
        /// </summary>
        public uint s2c_currency_count;
        /// <summary>
        /// 福气币数量.
        /// </summary>
        public uint s2c_item_count;
        /// <summary>
        /// 累积值.
        /// </summary>
        public uint s2c_cumulative_count;
    }
    
    
    
     #region  植树节活动
    
    /// <summary>
    /// 获取植树节信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 35)]
    public class ClientGetPlantingInfoRequest : Request, ILogicProtocol
    {
        public int c2s_platformID;
    }

    /// <summary>
    /// 获取植树节商品信息.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 36)]
    public class ClientGetPlantingInfoResponse : Response, ILogicProtocol
    {
        public List<TLPlantingProductInfo> s2c_list;
    }

    /// <summary>
    /// 植树节获得奖励请求.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 37)]
    public class ClientGetPlantingRewardRequest : Request, ILogicProtocol
    {
        public int c2s_productID;
    }

    /// <summary>
    /// 获取植树节回执.
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 38)]
    public class ClientGetPlantingRewardResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("请先购买水壶")]
        public const int NO_Activity = 501;
        [MessageCodeAttribute("活动已过期")]
        public const int No_Vaild = 502;
        [MessageCodeAttribute("今日已浇过水")]
        public const int Has_Watering = 503;
        [MessageCodeAttribute("领取已达最大次数")]
        public const int CODE_MAXReward = 504;
        [MessageCodeAttribute("奖励数据配置异常")]
        public const int CODE_DATA_ERROR = 505;

        public int s2c_times = 0;
        /// <summary>
        /// 0 未购买 1未浇水 2浇水 3 到达上限
        /// </summary>
        public int s2c_state;
    }

    /// <summary> 
    /// </summary>
    [MessageType(Constants.TL_BACTIVITY_START + 39)]
    public class ClientPlantingInfoNotify : Notify, ILogicProtocol
    {
        public List<TLPlantingProductInfo> s2c_list;
    }

    #endregion
    
    
}

