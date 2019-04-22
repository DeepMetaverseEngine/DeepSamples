using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using DeepCore;
using System;
using TLClient.Protocol.Modules;

namespace TLProtocol.Protocol.Client
{
   
    [MessageType(TLConstants.TL_PlayRule + 810)]
    public class TLClientGetMeridiansInfoRequest : Request, ILogicProtocol
    {
        //0 给所有数据 否则给对应主经脉数据   
        public int c2s_main;
    }
    [MessageType(TLConstants.TL_PlayRule + 811)]
    public class TLClientGetMeridiansInfoReponse : Response, ILogicProtocol
    {
        //获取对应经脉的数据 会为空
        public List<int> s2c_activited;
        //获取对应经脉的数据 会为空
        public List<int> s2c_unlocked;
        
        //获取对应经脉当前的消耗值
        public List<int> s2c_cost;
        public string s2c_overflowexp;
    }
    
    //冲穴
    [MessageType(TLConstants.TL_PlayRule + 812)]
    public class TLClientGetthroughMeridiansRequest : Request, ILogicProtocol
    {
        //主脉id
        public int c2s_main;
        //冲穴次数0为一键冲穴
        public int c2s_times;
    }
    [MessageType(TLConstants.TL_PlayRule + 813)]
    public class TLClientGetthroughMeridiansReponse : Response, ILogicProtocol
    {
        //获取对应经脉的数据 会为空
        public List<int> s2c_activited;
        //获取对应经脉的数据 会为空
        public List<int> s2c_unlocked;

        public string s2c_overflowexp;
        public int s2c_cost;
        public bool needTips;
      [MessageCode("道具和储存经验数量不足")] public const int ERR_ITEMEMPTY = 501;
      [MessageCode("此脉已全部打通")] public const int ERR_MERIDIANSFULL = 502;
    }

    
} 