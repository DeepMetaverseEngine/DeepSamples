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
   
    [MessageType(TLConstants.TL_PlayRule + 400)]
    public class TLClientSweepNotify : Notify, ILogicProtocol,INetProtocolS2C
    {
        public List<SweepRewardData> c2s_data;
    }
    [MessageType(TLConstants.TL_PlayRule + 402)]
    public class TLClientSweepRequest : Request, ILogicProtocol
    {
        public string s2c_functionid;
    }
    [MessageType(TLConstants.TL_PlayRule + 403)]
    public class TLClientSweepResponse : Response, ILogicProtocol
    {
        [MessageCode("无效的参数")] public const int CODE_ERRORDATA = 501;
        [MessageCode("扫荡次数不足")] public const int CODE_ERRNOTIMES = 502;
    }
} 