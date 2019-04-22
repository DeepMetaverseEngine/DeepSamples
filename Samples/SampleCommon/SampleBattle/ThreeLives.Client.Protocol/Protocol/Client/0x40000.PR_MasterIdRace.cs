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
    //请求门派数据
    [MessageType(TLConstants.TL_PlayRule + 210)]
    public class TLClientGetMasterIdListInfoRequest : Request, ILogicProtocol
    {
        public int c2s_index;
    }

    [MessageType(TLConstants.TL_PlayRule + 211)]
    public class TLClientGetMasterIdListInfoReponse : Response, ILogicProtocol
    {
        public MasterIdListData s2c_data;
    }


    //添加亲信操作
    [MessageType(TLConstants.TL_PlayRule + 214)]
    public class TLClientMasterIdAppointRequest : Request, ILogicProtocol
    {
        public string roleID;//玩家id
        public int type;  //0 撤销，1 邀请
    }

    [MessageType(TLConstants.TL_PlayRule + 215)]
    public class TLClientMasterIdAppointReponse : Response, ILogicProtocol
    {

        public string roleID;
       
        //邀请反馈
        [MessageCode("玩家不存在")] public const int ERR_PLAYER_NOT_EXIST = 501;
        [MessageCode("玩家不是你的好友")] public const int ERR_NOTFRIEND = 502; 
        [MessageCode("你已经有一个亲信了")] public const int ERR_HAVEONE = 503;
        [MessageCode("玩家不在线")] public const int ERR_PLAYER_OFFLINE = 504;

        //撤销 
        [MessageCode("玩家不是你的亲信")] public const int ERR_NOTYOURQINXIN = 505;
        [MessageCode("活动中不能执行操作")] public const int ERR_INACTIVITY = 506;
        [MessageCode("你不是掌门，不能此功能")] public const int ERR_NORIGHT = 507;
    }

    //更改亲信名字操作
    [MessageType(TLConstants.TL_PlayRule + 216)]
    public class TLClientMasterIdChangeTitleRequest : Request, ILogicProtocol
    {
        public string roleID;//玩家id
        public string rename; //名字
    }

    [MessageType(TLConstants.TL_PlayRule + 217)]
    public class TLClientMasterIdChangeTitleReponse : Response, ILogicProtocol
    {
        public string rename;
        [MessageCode("还不能更名,请稍候")] public const int ERR_INCD = 501;
        [MessageCode("你不是掌门，不能使用改名功能")] public const int ERR_NORIGHT = 502;
        [MessageCode("活动中不能更名")] public const int ERR_INACTIVITY = 503;
        [MessageCode("请先任命一个亲信")] public const int ERR_NOQINXIN = 504;
        [MessageCode("任命不能为空")] public const int ERR_NONAME = 505;

    }
    
    [MessageType(TLConstants.TL_PlayRule + 218)]
    public class TLClientMasterRaceQinXinChangeNotify : Notify, ILogicProtocol,INetProtocolS2C
    {
        public string qinxinname;
        public string roldid;
    }
    
    [MessageType(TLConstants.TL_PlayRule + 219)]
    public class TLClientMasterRaceIdChangeNotify : Notify, ILogicProtocol,INetProtocolS2C
    {
        public int masterid;
    }
    
    
    [MessageType(TLConstants.TL_PlayRule + 220)]
    public class TLClientMasterRaceResultRequest : Request, ILogicProtocol
    {
        
    }

    [MessageType(TLConstants.TL_PlayRule + 221)]
    public class TLClientMasterRaceResultResponse : Response, ILogicProtocol
    {
        //职业 数据
        public HashMap<int,MasterResult> raceDataMap;
        public DateTime refreshTime;

    }
    
    
    
} 