using DeepCore;
using DeepCore.IO;
using DeepMMO.Data;
using System.Collections.Generic;
using System;
using DeepCore.ORM;

namespace TLProtocol.Data
{

    [MessageType(TLConstants.TL_SOCIAL_START + 1)]
    public class SocialBaseSnapData : ISerializable
    {
        public string roleId;
        public string roleName;
        public int pro;
        public int level;
        public int gender;
        public int practiceLv;
        public long fightPower;
        public string guildId;
        public string guildName;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 2)]
    public class FriendsInfoSnapData : SocialBaseSnapData
    {
        /// <summary>
        /// 上次的离线时间 在线传 DateTime.MaxValue
        /// </summary>
        public DateTime leaveTime;
        public int relationLv;
        public int relationExp;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 3)]
    public class FriendsSnapData : ISerializable
    {
        public List<FriendsInfoSnapData> friendList;
        public int friendCount;
        public int friendMax;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 4)]
    public class BlackInfoSnapData : SocialBaseSnapData
    {

    }

    [MessageType(TLConstants.TL_SOCIAL_START + 5)]
    public class BlackSnapData : ISerializable
    {
        public List<BlackInfoSnapData> blackList;
        public int blackCount;
        public int blackMax;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 6)]
    public class ApplyInfoSnapData : SocialBaseSnapData
    {
        public DateTime applyTime;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 7)]
    public class ApplySnapData : ISerializable
    {
        public List<ApplyInfoSnapData> applyList;
        public int applyCount;
        public int applyMax;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 8)]
    public class EnemyInfoSnapData : SocialBaseSnapData
    {
        public DateTime applyTime;
        public bool deepHatred;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 9)]
    public class EnemySnapData : ISerializable
    {
        public List<EnemyInfoSnapData> enemyList;
        public int enemyCount;
        public int enemyMax;
        public int deepCount;
        public int deepMax;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 10)]
    public class PlayerInfoSnapData : SocialBaseSnapData
    {
        public bool applied;

        public static PlayerInfoSnapData Create(string roleID, string roleName, int roleLevel, long fightPower, int pro, int gender, string guildId, string guildName, bool applied)
        {
            PlayerInfoSnapData data = new PlayerInfoSnapData();
            data.roleId = roleID;
            data.roleName = roleName;
            data.level = roleLevel;
            data.fightPower = fightPower;
            data.pro = pro;
            data.gender = gender;
            data.guildId = guildId;
            data.guildName = guildName;
            data.applied = applied;
            return data;
        }
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 11)]
    public class PlayerSnapData : ISerializable
    {
        public List<PlayerInfoSnapData> playerList;
        public int friendCount;
        public int friendMax;
    }

    [MessageType(TLConstants.TL_SOCIAL_START + 12)]
    public class RelationSnapData : ISerializable
    {
        public HashMap<string, SocialGiftRecordSnap> recordList;
        public List<FriendsInfoSnapData> friendList;
        public int friendCount;
        public int friendMax;
    }

    [PersistType]
    [MessageType(TLConstants.TL_SOCIAL_START + 13)]
    public class SocialGiftRecordSnap : ISerializable, IStructMapping
    {
        //交易流水号
        [PersistField]
        public string transactionId;

        //赠送者，为空表示自己
        [PersistField]
        public string presenter;

        //接收者，为空表示自己
        [PersistField]
        public string receiver;

        //模板id
        [PersistField]
        public int templateId;

        //数量
        [PersistField]
        public uint num;

        //赠送时间
        [PersistField]
        public DateTime time;
    }

    [PersistType]
    [MessageType(TLConstants.TL_SOCIAL_START + 14)]
    public class ReservationSnapData : ISerializable, IObjectMapping
    {
        [PersistField]
        public DateTime date;
        [PersistField]
        public HashMap<int, string> info;
    }

    //[PersistType]
    //[MessageType(TLConstants.TL_SOCIAL_START + 15)]
    //public class SocialWarehouseItem : ISerializable, IStructMapping
    //{
    //    [PersistField]
    //    public int slotIndex;
    //    [PersistField]
    //    public ItemSnapData item;

    //    public static SocialWarehouseItem Create(int slotIndex, ItemSnapData item)
    //    {
    //        SocialWarehouseItem ret = new SocialWarehouseItem();
    //        ret.slotIndex = slotIndex;
    //        ret.item = item;
    //        return ret;
    //    }
    //}

}
