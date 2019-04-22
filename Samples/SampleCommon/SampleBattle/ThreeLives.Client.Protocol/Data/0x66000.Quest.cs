
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    /// <summary>
    /// 任务快照.
    /// </summary>
    [MessageType(TLConstants.TL_QUEST_START + 1)]
    public class QuestDataSnap : ISerializable, IStructMapping
    {
        public const byte QuestState_Available = 1;    //可接取.
        public const byte QuestState_Accepted = 2;     //已接取.
        public const byte QuestState_Completed = 3;    //已完成.
        public const byte QuestState_Failed = 4;       //已失败.
        public const byte QuestState_Removed = 5;      //移除.
        public const byte QuestState_Submited = 6;      //提交.

        /// <summary>
        /// ID.
        /// </summary>
        [PersistField]
        public int id;
        /// <summary>
        /// 进度.
        /// </summary>
        [PersistField]
        public List<TLProgressData> ProgressList;
        /// <summary>
        /// 状态.
        /// </summary>
        [PersistField]
        public int state;

        /// <summary>
        /// 当前环数
        /// </summary>
        [PersistField]
        public int curLoopNum;
        /// <summary>
        /// 最大环数
        /// </summary>
        [PersistField]
        public int MaxLoopNum;

        /// <summary>
        /// 主类型
        /// </summary>
        [PersistField]
        public int mainType;

        /// <summary>
        /// 子类型
        /// </summary>
        [PersistField]
        public int subType;
        //生命周期
        [PersistField]
        public DateTime TimeLife ;

        //是否强制接受（无视接取条件，一般gm命令用）
        [PersistField]
        public bool isForceAccept;
        public string QuestName;

        public bool NeedInitListener;
    }

    /// <summary>
    /// 任务进度
    /// </summary>
    [MessageType(TLConstants.TL_QUEST_START + 2)]
    public class TLProgressData : ISerializable, IStructMapping
    {
        /// <summary>
        ///任务条件类型.
        /// </summary>
        [PersistField]
        public string Type;

        /// <summary>
        /// 任务条件ID.
        /// </summary>
        [PersistField]
        public int Arg1;

        /// <summary>
        /// 备用参数击杀怪物时为SCENE_ID.
        /// </summary>
        [PersistField]
        public int Arg2;

        /// <summary>
        /// 当前进度.
        /// </summary>
        [PersistField]
        public int CurValue;
        /// <summary>
        /// 目标进度.
        /// </summary>
        [PersistField]
        public int TargetValue;
    }

    /// <summary>
    /// 提交物品数据.
    /// </summary>
    [MessageType(TLConstants.TL_QUEST_START + 3)]
    public class SubmitItemData : ISerializable
    {
        public int index;
        public int count;
    }


    [MessageType(TLConstants.TL_QUEST_START + 4)]
    public class LoopQuestDataSnap : ISerializable, IStructMapping
    {
        [PersistField]
        public List<QuestDataSnap> QuestList;
    }
}
