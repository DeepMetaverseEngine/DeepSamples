﻿using DeepCore.IO;
using DeepCore.ORM;

/// <summary>
/// 公共了类型数据
/// </summary>
namespace DeepMMO.Data
{
    /// <summary>
    /// 表示一个场景的位置，实际坐标或者FlagName
    /// </summary>
    [MessageType(Constants.DATA_START + 1)]
    public class ZonePosition : ISerializable
    {
        public string flagName;
        public float x = -1;
        public float y = -1;

        public bool HasFlag { get { return !string.IsNullOrEmpty(flagName); } }
        public bool HasPos { get { return x >= 0 && y >= 0; } }
    }

    /// <summary>
    /// 当前场景快照信息.
    /// </summary>
    [MessageType(Constants.DATA_START + 2)]
    public class ZoneInfoSnap : ISerializable
    {
        /// <summary>
        /// 线.
        /// </summary>
        public int lineIndex;
        /// <summary>
        /// 当前玩家数量.
        /// </summary>
        public int curPlayerCount;
        /// <summary>
        /// 人数硬上限数量.
        /// </summary>
        public int playerMaxCount;
        /// <summary>
        /// 人数软上限.
        /// </summary>
        public int playerFullCount;
        /// <summary>
        /// 场景ID.
        /// </summary>
        public string uuid;
    }

    public class EventStoreData : IObjectMapping
    {
        [PersistField]
        public byte[] Bytes;
    }
}
