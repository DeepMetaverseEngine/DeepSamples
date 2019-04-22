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
   
    [MessageType(TLConstants.TL_PlayRule + 310)]
    public class TLClientTaiXuListRequest : Request, ILogicProtocol
    {
        //0 就返回所有太虚次数 否则返回对应groupid的次数
        public int c2s_index;
    }

    [MessageType(TLConstants.TL_PlayRule + 311)]
    public class TLClientTaiXuListResponse : Response, ILogicProtocol
    {
        
        public List<TaiXuData> s2c_data;
        [MessageCode("参数异常")] public const int ERR_DATA = 501;

    }
    
    
    
    [MessageType(TLConstants.TL_PlayRule + 312)]
    public class TLClientTaiXuSweepRequest : Request, ILogicProtocol
    {
      
        public int c2s_groupid;
        public int c2s_subid;
    }
    
    [MessageType(TLConstants.TL_PlayRule + 313)]
    public class TLClientTaiXuSweepResponse : Response, ILogicProtocol
    {
        public int s2c_groupid;
        public int s2c_times;
        [MessageCode("今天的扫荡次数已满")] public const int ERR_DATA = 501;
        
    }
    
} 