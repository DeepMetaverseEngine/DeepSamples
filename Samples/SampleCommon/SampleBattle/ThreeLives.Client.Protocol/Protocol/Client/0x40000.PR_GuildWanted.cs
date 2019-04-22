using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using DeepCore;
using System;

namespace TLProtocol.Protocol.Client
{
    //请求仙盟悬赏数据
    [MessageType(TLConstants.TL_PlayRule + 50)]
    public class TLClientGetGuildWantedInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(TLConstants.TL_PlayRule + 51)]
    public class TLClientGetGuildWantedInfoReponse : Response, ILogicProtocol
    {
        public GuildWantedData s2c_data;
        [MessageCode("未加入仙盟")]
        public const int ERR_NOGUILD = 502;
    }

    //刷新悬赏公告
    [MessageType(TLConstants.TL_PlayRule + 52)]
    public class TLClientRefreshGuildWantedRequest : Request, ILogicProtocol
    {
        
    }
    //刷新悬赏公告
    [MessageType(TLConstants.TL_PlayRule + 53)]
    public class TLClientRefreshGuildWantedReponse : Response, ILogicProtocol
    {
        public List<GuildWantedInfoData> s2c_data;
        public int s2c_RefreshTime;
        public DateTime s2c_RefreshDateTime;
        [MessageCode("刷新次数已用完")]
        public const int ERR_NOALLOWRESET = 501;
        [MessageCode("未加入仙盟")]
        public const int ERR_NOGUILD = 502;
        [MessageCode("没有可刷新的任务")]
        public const int ERR_CANNOTRESET = 503;
        [MessageCode("元宝不足")]
        public const int ERR_CANNOTDIAMOND = 504;
    }


    //接取悬赏任务
    [MessageType(TLConstants.TL_PlayRule + 54)]
    public class TLClientAccpetGuildWantedRequest : Request, ILogicProtocol
    {
        public int c2s_wantedId;
    }
    //接取悬赏任务
    [MessageType(TLConstants.TL_PlayRule + 55)]
    public class TLClientAccpetGuildWantedReponse : Response, ILogicProtocol
    {
        public int s2c_wantedId;
        public int s2c_ReceiveTime;
        [MessageCode("领取次数已达上限")]
        public const int ERR_NORECEIVETIMES = 501;
        [MessageCode("未加入仙盟")]
        public const int ERR_NOGUILD = 502;
        [MessageCode("任务状态异常")]
        public const int ERR_QUESTSTATE = 503;
    }


    //提交悬赏任务
    [MessageType(TLConstants.TL_PlayRule + 56)]
    public class TLClientSubmitGuildWantedRequest : Request, ILogicProtocol
    {
        public int c2s_wantedId;
    }
    //提交悬赏任务
    [MessageType(TLConstants.TL_PlayRule + 57)]
    public class TLClientSubmitGuildWantedResponse : Response, ILogicProtocol
    {
        public List<GuildWantedInfoData> s2c_data;
        [MessageCode("未加入仙盟")]
        public const int ERR_NOGUILD = 502;
        [MessageCode("任务状态异常")]
        public const int ERR_QUESTSTATE = 503;
    }

    //取消悬赏任务
    [MessageType(TLConstants.TL_PlayRule + 58)]
    public class TLClientGiveUpGuildWantedRequest : Request, ILogicProtocol
    {
        public int c2s_wantedId;
    }
    //取消悬赏任务
    [MessageType(TLConstants.TL_PlayRule + 59)]
    public class TLClientGiveUpGuildWantedResponse : Response, ILogicProtocol
    {
        public int s2c_wantedId;
        public int s2c_ReceiveTime;
        [MessageCode("任务状态异常")]
        public const int ERR_NORECEIVETIMES = 501;

    }

    //更新刷新时间
    [MessageType(TLConstants.TL_PlayRule + 60)]
    public class TLClientGuildWantedRefreshTimeRequest : Request, ILogicProtocol
    {
    }
    //更新刷新时间
    [MessageType(TLConstants.TL_PlayRule + 61)]
    public class TLClientGuildWantedRefreshTimeResponse : Response, ILogicProtocol
    {

        public int s2c_RefreshTime;
        public DateTime s2c_RefreshDateTime;
        [MessageCode("任务状态异常")]
        public const int ERR_NORECEIVETIMES = 501;

    }
}