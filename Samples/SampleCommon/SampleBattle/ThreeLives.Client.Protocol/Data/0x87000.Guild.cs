using DeepCore.IO;
using TLProtocol.Data;
using System.Collections.Generic;
using DeepCore.ORM;
using System;
using DeepCore;
using TLClient.Protocol;

namespace TLProtocol.Protocol.Data
{

    /// <summary>
    /// 公会基础信息
    /// </summary>
    [PersistType]
    [MessageType(TLConstants.TL_GUILD_STRAT + 1)]
    public class GuildBaseSnapData : ISerializable, IStructMapping
    {
        [PersistField]
        public string id;
        [PersistField]
        public string name;
        [PersistField]
        public int level;
        [PersistField]
        public long fightPower;
        [PersistField]
        public int memberNum;
        [PersistField]
        public int maxMemberNum;
        //公会长信息
        [PersistField]
        public string presidentId;
        [PersistField]
        public string presidentName;
        /// <summary>
        /// 审批条件
        /// 0:无条件 -1：需审批 >0：战力限制
        /// </summary>
        [PersistField]
        public int condition;
        //据点ID
        [PersistField]
        public int fort;
        //招募公告
        [PersistField]
        public string recruitBulletin;
        //被选定破坏公会id
        [PersistField]
        public string attackedGuild;
        //排名
        [PersistField]
        public int rank;
    }

    /// <summary>
    /// 公会完整信息
    /// </summary>
    [PersistType]
    [MessageType(TLConstants.TL_GUILD_STRAT + 2)]
    public class GuildSnapData : ISerializable, IObjectMapping, IPublicSnap
    {
        [PersistField]
        public GuildBaseSnapData guildBase;
        //排名
        [PersistField]
        public int rank;
        //公会资金
        [PersistField]
        public long fund;
        //维护费用
        [PersistField]
        public int maintenance;
        public int academyLv;
        public int varietyShopLv;
        //礼物等级
        [PersistField]
        public int giftLv;
        //对内公告
        [PersistField]
        public string noticeBoard;
        //公会日志
        public List<GuildLogSnapData> logList;
        //公会建筑
        [PersistField]
        public HashMap<int, int> buildList;

        public int donateCount;
        public int contribution;
        public int contributionMax;

        //interface api
        [PersistField]
        public DateTime ExpiredUtc;
        public DateTime ExpiredUtcTime => ExpiredUtc;

        public TLClientGuildSnap ToClientGuildSnap()
        {
            var ret = new TLClientGuildSnap
            {
                id = guildBase.id,
                name = guildBase.name,
                level = guildBase.level,
                fightPower = guildBase.fightPower,
                memberNum = guildBase.memberNum,
                maxMemberNum = guildBase.maxMemberNum,
                presidentId = guildBase.presidentId,
                fort = guildBase.fort,
                giftLv = this.giftLv
            };
            ret.ExpiredUtc = DateTime.UtcNow.AddMinutes(3);
            return ret;
        }
    }

    /// <summary>
    /// 成员完整信息
    /// </summary>
    [PersistType]
    [MessageType(TLConstants.TL_GUILD_STRAT + 4)]
    public class GuildMemberSnapData : ISerializable, IObjectMapping, IPublicSnap
    {
        //基础数据，不存档
        public string name;
        public string roleId;
        public int level;
        public int pro;
        public int gender;
        public long power;


        //今日已捐献次数
        public int donate;
        /// <summary>
        /// 上次的离线时间 在线传 DateTime.MaxValue
        /// </summary>
        public DateTime leaveTime;

        [PersistField]
        public string guildId;
        /// <summary>
        /// 职位
        /// 10:会长 20:副会长 30:长老 40:精英 50:成员
        /// </summary>
        [PersistField]
        public int position;
        //当前贡献
        [PersistField]
        public int contributionDay;
        //总贡献
        [PersistField]
        public int contributionMax;

        //interface api
        [PersistField]
        public DateTime ExpiredUtc;
        public DateTime ExpiredUtcTime => ExpiredUtc;
    }

    [MessageType(TLConstants.TL_GUILD_STRAT + 6)]
    public class GuildApplySnapData : ISerializable, IStructMapping
    {
        public string msgId;
        public string roleId;
        public string name;
        public int level;
        public int gender;
        public int pro;
        public long power;
        public bool online;
    }
    
    [PersistType]
    [MessageType(TLConstants.TL_GUILD_STRAT + 7)]
    public class GuildLogSnapData : ISerializable, IStructMapping
    {
        [PersistField]
        public DateTime time;
        [PersistField]
        public string content;
        [PersistField]
        public string[] args;
    }

    //天赋列表
    [MessageType(TLConstants.TL_GUILD_STRAT + 9)]
    public class GuildTalentSnapData : ISerializable, IStructMapping
    {
        public int talentLv;
        public HashMap<int, int> talentSkill;
    }

    //礼物列表信息
    [MessageType(TLConstants.TL_GUILD_STRAT + 11)]
    public class GuildGiftSnapData : ISerializable, IStructMapping
    {
        public int giftLv;
        public int giftLvExp;
        public int giftOpenExp;
        public int nextGiftId;
        public List<GuildGiftItemSnapData> giftList;
    }

    //单个礼物
    [MessageType(TLConstants.TL_GUILD_STRAT + 12)]
    public class GuildGiftItemSnapData : ISerializable, IStructMapping
    {
        public int itemId;
        public int source;
        public string roleName;
        public DateTime expiredTime;
    }

    //神兽信息
    [MessageType(TLConstants.TL_GUILD_STRAT + 13)]
    public class GuildMonsterSnapData : ISerializable, IStructMapping
    {
        public int monsterRank;
        public int monsterLevel;
        public int monsterExp;
        public List<GuildLogSnapData> logList;
    }

    [MessageType(TLConstants.TL_GUILD_STRAT + 20)]
    public class TLClientGuildSnap : ISerializable, IPublicSnap
    {
        public string id;
        public string name;
        public int level;
        public long fightPower;
        public int memberNum;
        public int maxMemberNum;
        public string presidentId;
        public int fort;
        public int giftLv;
        public DateTime ExpiredUtc;
        public DateTime ExpiredUtcTime => ExpiredUtc;
    }

    [PersistType]
    public class GuildAtackInfo : ISerializable, IStructMapping
    {
        [PersistField]
        public int attackCount;
        [PersistField]
        public HashMap<int, int> attackType = new HashMap<int, int>();
        [PersistField]
        public DateTime lastAttackTime;
    }

    [PersistType]
    public class GuildActivityInfo : ISerializable, IStructMapping
    {
        [PersistField]
        public int curCount;
        [PersistField]
        public int maxCount;
    }
    
    public class GuildFortPositionInfo : ISerializable
    {
        public string guildId;
        public string guildName;
        public int guildRank;
    }

    [MessageType(TLConstants.TL_GUILD_STRAT + 21)]
    public class GuildFortInfo : ISerializable
    {
        public int fortId;
        public string holdGuildId;
        public string holdGuildName;
        public List<GuildFortPositionInfo> fortPositionList;
    }

    public enum GuildChangeType : byte
    {
        Level = 1 << 1,
        Build = 1 << 2,
        Monster = 1 << 3,
        Donate = 1 << 4,
        Position = 1 << 5,
        Talent = 1 << 6,
    }

}
