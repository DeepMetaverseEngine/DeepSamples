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
    //请求委托数据
    [MessageType(TLConstants.TL_PlayRule + 102)]
    public class TLClientGetConsignationInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(TLConstants.TL_PlayRule + 103)]
    public class TLClientGetConsignationInfoReponse : Response, ILogicProtocol
    {
        public ConsignationData s2c_data;
    }


    //接取委托任务
    [MessageType(TLConstants.TL_PlayRule + 104)]
    public class TLClientAccpetConsignationRequest : Request, ILogicProtocol
    {
        public int c2s_wantedId;
    }
    //接取委托任务
    [MessageType(TLConstants.TL_PlayRule + 105)]
    public class TLClientAccpetConsignationReponse : Response, ILogicProtocol
    {
        public int s2c_wantedId;
        [MessageCode("任务状态异常")]
        public const int ERR_QUESTSTATE = 501;
    }


    //提交委托任务
    [MessageType(TLConstants.TL_PlayRule + 106)]
    public class TLClientSubmitConsignationRequest : Request, ILogicProtocol
    {
        public int c2s_wantedId;
    }
    //提交委托任务
    [MessageType(TLConstants.TL_PlayRule + 107)]
    public class TLClientSubmitConsignationResponse : Response, ILogicProtocol
    {
        public ConsignationInfoData s2c_data;
        [MessageCode("任务状态异常")]
        public const int ERR_QUESTSTATE = 501;
    }

    //更新刷新时间
    [MessageType(TLConstants.TL_PlayRule + 108)]
    public class TLClientConsignationRefreshTimeRequest : Request, ILogicProtocol
    {
        public int index;
    }
    //更新刷新时间
    [MessageType(TLConstants.TL_PlayRule + 109)]
    public class TLClientConsignationRefreshTimeResponse : Response, ILogicProtocol
    {
        public ConsignationInfoData s2c_data;
        [MessageCode("任务状态异常")]
        public const int ERR_NORECEIVETIMES = 501;

    }
}