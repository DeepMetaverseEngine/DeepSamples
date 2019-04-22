using System.Collections.Generic;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using TLProtocol.Data;
using DeepMMO.Attributes;
using System;

namespace TLProtocol.Protocol.Client
{
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 1)]
    public class TLClientTalkWithNpcRequest : Request, ILogicProtocol
    {
        public int c2s_npcId;
    }


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 2)]
    public class TLClientQuestWithNpcResponse : Response, ILogicProtocol
    {
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 3)]
    public class TLClientSubmitItemRequest : Request, ILogicProtocol
    {
        public List<SubmitItemData> c2s_data;
        public int c2s_questId;
    }


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 4)]
    public class TLClientSubmitItemResponse : Response, ILogicProtocol
    {
        [MessageCode("没有足够的物品可提交.")]
        public const int CODE_ITEM_NOT_ENOUGH = 502;
        [MessageCode("不符合提交的条件.")]
        public const int CODE_ITEM_ERROR = 503;
    }

    
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 7)]
    public class TLClientQueryLoadWayRequest : Request, ILogicProtocol
    {
        public int c2s_toAreaId;
    }


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 8)]
    public class TLClientQueryLoadWayResponse : Response, ILogicProtocol
    {
        [MessageCode("未能找到路点.")]
        public const int CODE_ERROR_NO_POINT = 501;
        public List<TLSceneNextLink> s2c_wayList = null;
    }
   

    /// <summary>
    /// 接取任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 9)]
    public class TLClientAcceptQuestRequest : Request, ILogicProtocol
    {
        public int c2s_id;
    }

    /// <summary>
    /// 接取任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 10)]
    public class TLClientAcceptQuestResponse : Response, ILogicProtocol
    {
        [MessageCode("接取条件不符.")]
        public const int CODE_ERROR_CONDITION_FAIL = 501;
    }

    
    /// <summary>
    /// 提交任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 11)]
    public class TLClientSubmitQuestRequest : Request, ILogicProtocol
    {
        public int c2s_id;
        public byte c2s_inputValue;
    }

    /// <summary>
    /// 提交任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 12)]
    public class TLClientSubmitQuestResponse : Response, ILogicProtocol
    {
        [MessageCode("无效的请求.")]
        public const int CODE_ERROR_NULL = 501;
        [MessageCode("提交条件不符.")]
        public const int CODE_ERROR_CONDITION_FAIL = 503;
    }

    /// <summary>
    /// 放弃任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 13)]
    public class TLClientGiveUpQuestRequest : Request, ILogicProtocol
    {
        public int c2s_id;
    }

    /// <summary>
    /// 放弃任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 14)]
    public class TLClientGiveUpQuestResponse : Response, ILogicProtocol
    {
        [MessageCode("无效的请求.")]
        public const int CODE_ERROR_NULL = 501;
        [MessageCode("无法放弃任务.")]
        public const int CODE_ERROR_CONDITION_FAIL = 502;
    }

    /// <summary>
    /// 通知客户端任务信息变更
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 15)]
    public class TLClientQuestDataNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
       
        //新增.
        public const byte Op_Add = 1;
        //改变指定条.
        public const byte Op_Change = 2;
        //重置.
        public const byte Op_Reset = 3;
        //初始化
        public const byte Op_Init = 4;
        public byte OperatorType;
        public List<QuestDataSnap> s2c_data = null;
    }

    /// <summary>
    /// 通知客户端删除任务信息
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 16)]
    public class TLClientQuestDataRemoveNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int QuestID;
    }




    /// <summary>
    /// 开启loop任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 17)]
    public class TLClientBeginLoopQuestRequest : Request, ILogicProtocol
    {
        public int c2s_groupid;
    }

    /// <summary>
    /// 开启Loop任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 18)]
    public class TLClientBeginLoopQuestResponse : Response, ILogicProtocol
    {
        
        [MessageCode("接取条件不符.")]
        public const int CODE_ERROR_CONDITION_FAIL = 501;
        [MessageCode("任务已经开启")]
        public const int CODE_ERROR_CONDITION_HASGOT = 502;
        [MessageCode("任务次数已达上限")]
        public const int CODE_ERROR_CONDITION_MAX = 503;
        [MessageCode("不是自己的仙盟")]
        public const int CODE_ERROR_CONDITION_NOSELFGUILD = 504;
        [MessageCode("请先加入仙盟")]
        public const int CODE_ERROR_CONDITION_NOGUILD = 505;

    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 19)]
    public class TLClientSubmitCustomItemRequest : Request, ILogicProtocol
    {
        public List<SubmitItemData> c2s_data;
        public int c2s_questId;
    }


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 20)]
    public class TLClientSubmitCustomItemResponse : Response, ILogicProtocol
    {
        [MessageCode("没有足够的物品可提交.")]
        public const int CODE_ITEM_NOT_ENOUGH = 502;
        [MessageCode("不满足提交的条件.")]
        public const int CODE_ITEM_ERROR = 503;

    }


    /// <summary>
    /// 获得所有任务列表
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 21)]
    public class TLClientGetQuestListRequest : Request, ILogicProtocol
    {
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 22)]
    public class TLClientGetQuestListResponse : Response, ILogicProtocol
    {
       public List<QuestDataSnap> snaplist;
    }


    /// <summary>
    /// form接任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 23)]
    public class TLClientAcceptQuestByFormRequest : Request, ILogicProtocol
    {
        public string checkname;
        public int c2s_id;
    }

    /// <summary>
    /// form提交任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 24)]
    public class TLClientAcceptQuestByFormResponse : Response, ILogicProtocol
    {
        [MessageCode("验证失败")]
        public const int CODE_ERROR_CONDITION_FAIL = 501;
    }
    

    /// <summary>
    /// form提交任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 25)]
    public class TLClientSubmitQuestByFormRequest : Request, ILogicProtocol
    {
        public string checkname;
        public int c2s_id;
        public int c2s_inputValue;
    }

    /// <summary>
    /// form提交任务
    /// </summary>
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 26)]
    public class TLClientSubmitQuestByFormResponse : Response, ILogicProtocol
    {
        [MessageCode("验证失败")]
        public const int CODE_ERROR_CONDITION_FAIL = 501;
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 27)]
    public class TLSceneNextLink : ISerializable
    {
        public string from_flag_name;
        public int to_map_id;
        public string to_flag_name;
    }


    ///// <summary>
    ///// 通知客户端任务信息变更
    ///// </summary>
    //[MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 28)]
    //public class TLClientQuestAutoMoveNotify : Notify, ILogicProtocol
    //{
    //    public float s2c_aimX;
    //    public float s2c_aimY;
    //    public int s2c_mapId;
    //    public int s2c_monsterID;
    //}


    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 29)]
    public class ClientAcceptCarriageRequest : Request, ILogicProtocol
    {
        public int c2s_id;
    }

  
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 30)]
    public class ClientAcceptCarriageResponse : Response, ILogicProtocol
    {
        [MessageCode("今日押镖次数已用光")]
        public const int CODE_ERROR_COUNT_LIMIT = 501;
        [MessageCode("已存在押镖或护镖任务")]
        public const int CODE_ERROR_EXISTS = 502;
    }

    //检查任务周期
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 31)]
    public class TLClientCheckQuestTimeRequest : Request, ILogicProtocol
    {
        public int c2s_questid;
    }
    //检查任务周期
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 32)]
    public class TLClientCheckQuestTimeResponse : Response, ILogicProtocol
    {
        
        [MessageCode("任务状态异常")]
        public const int ERR_QUESTSTATE = 501;

    }
	
    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 33)]
    public class ClientTodayCarriageCountRequest : Request, ILogicProtocol
    {
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 34)]
    public class ClientTodayCarriageCountResponse : Response, ILogicProtocol
    {
        public int s2c_count;
    }

    [MessageType(TLProtocol.Protocol.Client.Constants.TL_QUEST_START + 35)]
    public class ClientQuestGoOnTipsNotify : Notify,ILogicProtocol, INetProtocolS2C
    {
        //类型
        public int s2c_type;
        //当前轮数
        public int s2c_curTurn;
    }


}
