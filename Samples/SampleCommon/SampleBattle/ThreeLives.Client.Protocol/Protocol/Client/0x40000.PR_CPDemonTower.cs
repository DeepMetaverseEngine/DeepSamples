using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using DeepCore;

namespace TLProtocol.Protocol.Client
{
    //请求双人镇妖塔数据
    [MessageType(TLConstants.TL_PlayRule + 70)]
    public class TLClientGetCPDTInfoRequest : Request, ILogicProtocol
    {
        //难度
        public int c2s_mode;
    }

    [MessageType(TLConstants.TL_PlayRule + 71)]
    public class TLClientGetCPDTInfoReponse : Response, ILogicProtocol
    {
        public CPDemonTowerData s2c_data;
    }

 
    // 领取首通奖励
    [MessageType(TLConstants.TL_PlayRule + 72)]
    public class TLClientGetFirstGiftCPDTRequest : Request, ILogicProtocol
    {
        public int c2s_mode;
        public int c2s_giftid;
    }

    [MessageType(TLConstants.TL_PlayRule + 73)]
    public class TLClientGetFirstGiftCPDTResponse : Response, ILogicProtocol
    {
        public List<int> c2s_giftid;
        [MessageCode("已领取过该奖励")]
        public const int ERR_HAVEGOTTEN = 501;
        [MessageCode("未通关该层")]
        public const int ERR_NOPASS = 502;
        [MessageCode("没有可领取奖励")]
        public const int ERR_NOREWARD = 503;
    }




}