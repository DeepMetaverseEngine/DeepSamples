using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using DeepCore;

namespace TLProtocol.Protocol.Client
{
    //请求镇妖塔数据
    [MessageType(TLConstants.TL_PlayRule + 10)]
    public class TLClientGetDTInfoRequest : Request, ILogicProtocol
    {
        public int c2s_mode; //1 简单 2困难 3 懵比
    }

    [MessageType(TLConstants.TL_PlayRule + 11)]
    public class TLClientGetDTInfoReponse : Response, ILogicProtocol
    {
        public DemonTowerData s2c_data;
    }

    
    //重置次数
    [MessageType(TLConstants.TL_PlayRule + 12)]
    public class TLClientResetDTRequest : Request, ILogicProtocol
    {

    }

    [MessageType(TLConstants.TL_PlayRule + 13)]
    public class TLClientResetDTReponse : Response, ILogicProtocol
    {
        public int s2c_resetTime;//当前重置次数
        public int s2c_curLayer;//当前层数
        public int s2c_maxLayer;//当前最大层数
        [MessageCode("重置次数已用完")]
        public const int ERR_NORESETTIMES = 501;
        [MessageCode("当前场景不可以重置")]
        public const int ERR_NOALLOWRESET = 502;
    }

    //// 进入镇妖塔
    //[MessageType(TLConstants.TL_PlayRule + 14)]
    //public class TLClientEnterDTRequest : Request, ILogicProtocol
    //{
    //    public int c2s_mode; //1 简单 2困难 3懵比
    //}

    //[MessageType(TLConstants.TL_PlayRule + 15)]
    //public class TLClientEnterDTReponse : Response, ILogicProtocol
    //{
    //    //[MessageCode("当前难度已通关")]
    //    //public const int ERR_LIMIT = 501;
    //    //[MessageCode("请求数据异常")]
    //    //public const int ERR_DATA = 502;
    //}

    // 领取首通奖励
    [MessageType(TLConstants.TL_PlayRule + 16)]
    public class TLClientGetFirstGiftDTRequest : Request, ILogicProtocol
    {
        public int c2s_giftid;
    }

    [MessageType(TLConstants.TL_PlayRule + 17)]
    public class TLClientGetFirstGiftDTResponse : Response, ILogicProtocol
    {
        public int s2c_giftid;
        [MessageCode("已领取过该奖励")]
        public const int ERR_HAVEGOTTEN = 501;
        [MessageCode("未通关该层")]
        public const int ERR_NOPASS = 502;
    }



    // 激活秘闻
    [MessageType(TLConstants.TL_PlayRule + 18)]
    public class TLClientActiveSecertBookRequest : Request, ILogicProtocol
    {
        public int c2s_bookId;
    }

    [MessageType(TLConstants.TL_PlayRule + 19)]
    public class TLClientActiveSecertBookResponse : Response, ILogicProtocol
    {
        public int s2c_bookid;
        [MessageCode("未激活所有线索")]
        public const int ERR_NOTENOUGHCLUE = 501;
        [MessageCode("已激活此秘闻")]
        public const int ERR_HAVEACTIVED = 502;
    }


    ////通关结算
    //[MessageType(TLConstants.TL_PlayRule + 20)]
    //public class TLClientDTPassRewardNotify : Notify, ILogicProtocol
    //{
    //    /// <summary>
    //    /// 下一层 0 就是到达顶层
    //    /// </summary>
    //    public int s2c_nextLayer;
    //    List<TLDropItemSnapData> itemList;
    //}


    //神奇盒子
    [MessageType(TLConstants.TL_PlayRule + 21)]
    public class TLClientGetMagicBoxInfoRequest : Request, ILogicProtocol
    {
        
    }

    [MessageType(TLConstants.TL_PlayRule + 22)]
    public class TLClientGetMagicBoxInfoResponse : Response, ILogicProtocol
    {
        //物品id 当前数量
        public HashMap<int, int> s2c_keyMap;
        //有可镶嵌钥匙
        public bool s2c_havekeys = false;
    }

    //上钥匙
    [MessageType(TLConstants.TL_PlayRule + 23)]
    public class TLClientUseKeyRequest : Request, ILogicProtocol
    {

    }

    [MessageType(TLConstants.TL_PlayRule + 24)]
    public class TLClientUseKeyResponse : Response, ILogicProtocol
    {
        //物品id 当前数量
        public HashMap<int, int> s2c_keyMap;
        [MessageCode("没有可镶嵌钥匙")]
        public const int ERR_NOKEYS = 501;
    }


    //宝箱宝箱快开启
    [MessageType(TLConstants.TL_PlayRule + 25)]
    public class TLClientOpenBoxRequest : Request, ILogicProtocol
    {

    }

    
    [MessageType(TLConstants.TL_PlayRule + 26)]
    public class TLClientOpenBoxResponse : Response, ILogicProtocol
    {
        //物品id 当前数量
        public HashMap<int, int> s2c_keyMap;
        [MessageCode("所需钥匙不足")]
        public const int ERR_NOTENOUGHT = 501;
    }


    ////退出镇妖塔
    //[MessageType(TLConstants.TL_PlayRule + 27)]
    //public class TLClientExitDTRequest : Request, ILogicProtocol
    //{

    //}


    //[MessageType(TLConstants.TL_PlayRule + 28)]
    //public class TLClientExitDTResponse : Response, ILogicProtocol
    //{
        
    //}


    // 领取秘闻
    [MessageType(TLConstants.TL_PlayRule + 29)]
    public class TLClientGetSecertRewardRequest : Request, ILogicProtocol
    {
        public int c2s_bookId;
    }

    [MessageType(TLConstants.TL_PlayRule + 30)]
    public class TLClientGetSecertRewardResponse : Response, ILogicProtocol
    {
        [MessageCode("已领取过秘闻")]
        public const int ERR_HAVEDONE = 501;
        [MessageCode("未完成秘闻任务")]
        public const int ERR_NOFINISHQUEST = 502;
    }


    //请求镇妖塔秘闻数据
    [MessageType(TLConstants.TL_PlayRule + 31)]
    public class TLClientGetDTSecertBookInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(TLConstants.TL_PlayRule + 32)]
    public class TLClientGetDTSecertBookInfoReponse : Response, ILogicProtocol
    {
        public SecretBookDataList s2c_data;
    }


    //请求镇妖塔通关数据
    [MessageType(TLConstants.TL_PlayRule + 33)]
    public class TLClientGetDTPassedInfoRequest : Request, ILogicProtocol
    {
       
    }

    [MessageType(TLConstants.TL_PlayRule + 34)]
    public class TLClientGetDTPassedInfoReponse : Response, ILogicProtocol
    {
        public List<bool> s2c_data;
    }

    //扫荡镇妖塔
    [MessageType(TLConstants.TL_PlayRule + 35)]
    public class TLClientDemonTowerSweepRequest : Request, ILogicProtocol
    {
        //难度
        public int s2c_index;  
    }

    [MessageType(TLConstants.TL_PlayRule + 36)]
    public class TLClientDemonTowerSweepReponse : Response, ILogicProtocol
    {
        //扫荡的难度
        public int c2s_index;
        //难度当前对应的层数
        public int c2s_curLayer;
        [MessageCode("没有可扫荡的关卡")] public const int ERR_NOSWEEP = 501;
       
    }
}