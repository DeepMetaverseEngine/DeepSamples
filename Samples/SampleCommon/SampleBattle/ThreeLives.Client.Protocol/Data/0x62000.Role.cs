using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using DeepMMO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using TLBattle.Common.Plugins;
using TLClient.Protocol;

namespace TLProtocol.Data
{
    /// <summary>
    /// 客户端角色数据.
    /// </summary>
    [MessageType(TLConstants.TL_ROLE_START + 1)]
    public class TLClientRoleData : ClientRoleData
    {
        /// <summary>
        /// 经验.
        /// </summary>
        public long exp;
        /// <summary>
        /// 所需经验.
        /// </summary>
        public long needExp;
        /// <summary>
        /// 溢出经验.
        /// </summary>
        public long overflowExp;
        /// <summary>
        /// 任务数据.
        /// </summary>
        public List<QuestDataSnap> quests;
        /// <summary>
        /// 自动战斗设置.
        /// </summary>
        public TLGameOptionsData gameOptionsData;
        /// <summary>
        /// 自定义数据.
        /// </summary>
        public HashMap<string, string> ClientModifyData;
        /// <summary>
        /// 职业.
        /// </summary>
        public TLClientCreateRoleExtData.ProType proType;
        /// <summary>
        ///性别.
        /// </summary>
        public TLClientCreateRoleExtData.GenderType gender;
        /// <summary>
        /// 公会id.
        /// </summary>
        public string guildId;
        /// <summary>
        /// 公会名.
        /// </summary>
        public string guildName;
        /// <summary>
        /// 战斗力.
        /// </summary>
        public long FightPower;
        /// <summary>
        /// 功能开放数据.
        /// </summary>
        public HashMap<string, byte> funcOpen;
        /// <summary>
        /// 杀戮值.
        /// </summary>
        public int PKValue;
        /// <summary>
        /// 修行之道等级.
        /// </summary>
        public int practiceLv;

        /// <summary>
        /// 目标系统
        /// </summary>
        public int TargerSystemLv;

        /// <summary>
        /// 门派身份
        /// </summary>
        public int MasterId;
        /// <summary>
        /// VIP等级.
        /// </summary>
        public int VipLv;
        /// <summary>
        /// VIP累积经验.
        /// </summary>
        public int VipCurExp;

        /// <summary>
        /// 玩家称号ID
        /// </summary>
        public int TitleID;

        /// <summary>
        /// 玩家称号名称扩展
        /// </summary>
        public string TitleNameExt;
        /// <summary>
        /// 伴侶id.
        /// </summary>
        public string spouseId;
        /// <summary>
        /// 伴侶名.
        /// </summary>
        public string spouseName;


        /// <summary>
        /// 血池当前剩余次数.
        /// </summary>
        public int MedicinePoolCurCount;
        /// <summary>
        /// 累积充值金额.
        /// </summary>
        public int AccumulativeCount;
    }

    /// <summary>
    /// 创角扩展属性.
    /// </summary>
    [MessageType(TLConstants.TL_ROLE_START + 2)]
    public class TLClientCreateRoleExtData : ISerializable
    {
        public enum ProType : byte
        {
            None,       //无.
            YiZu,         //翼族.
            TianGong,     //天宫.
            KunLun,     //昆仑.
            QinqQiu,     //青丘.
        }

        public enum GenderType : byte
        {
            Man,
            Woman
        }

        //职业
        public ProType RolePro;

        //性别
        public GenderType gender;

        //捏脸.
        //服装.
        //头发.
        //等等.
    }


    /// <summary>
    /// 自动战斗配置.
    /// </summary>
    [MessageType(TLConstants.TL_ROLE_START + 3)]
    public class TLGameOptionsData : ISerializable
    {
        public const string KEY_FRIEND_REQUEST = "friend_request";
        public const string KEY_TEAM_REQUEST = "enemy_request";

        public const string KEY_FRIEND_PK = "pk_friend";
        public const string KEY_GUILD_PK = "pk_guild";
        public const string KEY_STRANGER_PK = "pk_stranger";

        public const string KEY_FRIEND_CHAT = "chat_friend";
        public const string KEY_GUILD_CHAT = "chat_guild";
        public const string KEY_STRANGER_CHAT = "chat_stranger";

        //名片头像只对好友可见
        public const string KEY_PROFILE_PHOTO = "key_profile_photo";
        //微信号
        public const string KEY_PROFILE_ID = "key_profile_id";
        //照片
        public const string KEY_PHOTO = "key_photo";
        //位置
        public const string KEY_CITY = "key_city";


        public const string OPEN = "1";
        public const string CLOSE = "0";
        //-------------------------------------------------------------------------------
        //吃药设置.
        //-------------------------------------------------------------------------------
        //是否自动吃药.
        public bool AutoUseItem = false;
        //智能选择物品.
        public bool SmartSelect = false;
        //是否自动填充
        public bool AutoRecharge = false;
        //吃药配置.
        public int itemID;
        //使用阀值.
        public byte UseThreshold = 75;
        //物品使用冷却.
        public DateTime ItemCoolDownTimeStamp;
        //血池使用冷却.
        public DateTime MedicinePoolTimeStamp;
        //-------------------------------------------------------------------------------
        //系统设置.
        //-------------------------------------------------------------------------------
        public HashMap<string, string> Options;
    }
    [MessageType(TLConstants.TL_ROLE_START + 4)]
    public class TLRoleSnap : RoleSnap, IObjectMapping, IPublicSnap
    {
        [PersistField]
        public long FightPower;
        [PersistField]
        public byte UnitPro;
        [PersistField]
        public byte Gender;
        [PersistField]
        public int PracticeLv;
        [PersistField]
        public string GuildId;
        [PersistField]
        public string GuildName;
        [PersistField]
        public DateTime LastOfflineTime;
        [PersistField]
        public string ZoneUUID;
        [PersistField]
        public int MapTemplateID;
        [PersistField]
        public List<AvatarInfoSnap> AvatarInfo;
        [PersistField]
        public int AvatarScore;

        [PersistField]
        public DateTime ExpiredUtc;
        public DateTime ExpiredUtcTime => ExpiredUtc;

        [PersistField]
        public int MaxDemonTowerLayer;
        [PersistField]
        public DateTime MaxDemonTowerDateTime;

        [PersistField]
        public int TitleID = 0;
        /// <summary>
        /// 门派身份
        /// </summary>
        [PersistField]
        public int MasterId;
        /// <summary>
        /// 铜币
        /// </summary>
        [PersistField]
        public ulong Copper;
        /// <summary>
        /// 银两
        /// </summary>
        [PersistField]
        public ulong Sliver;
        /// <summary>
        /// 仙侣战力
        /// </summary>
        [PersistField]
        public long GodScore;
        /// <summary>
        /// 坐骑战力
        /// </summary>
        [PersistField]
        public long MountScore;
        /// <summary>
        /// 神器战力
        /// </summary>
        [PersistField]
        public long ArtifactScore;
        /// <summary>
        /// 金轮战力
        /// </summary>
        [PersistField]
        public long WingScore;
        /// <summary>
        /// 经脉战力
        /// </summary>
        [PersistField]
        public long MeridiansScore;

        [PersistField]
        public int CPTowerLayer;

        [PersistField]
        public HashMap<string, string> Options;

        public TLClientRoleSnap ToClientRoleSnap()
        {
            var ret = new TLClientRoleSnap
            {
                ID = uuid,
                digitID = digitID,
                Avatar = AvatarInfo,
                FightPower = FightPower,
                Level = level,
                Name = name,
                PracticeLv = PracticeLv,
                Pro = UnitPro,
                Gender = Gender,
                GuildId = GuildId,
                GuildName = GuildName,
                ZoneUUID = ZoneUUID,
                MapTemplateID = MapTemplateID,
                TitleID = TitleID,
                MasterId = MasterId,
                CPTowerLayer = CPTowerLayer,
                vip_level = vip_level,

            };
            ret.ExpiredUtc = DateTime.UtcNow.AddMinutes(3);
            ret.Options = new HashMap<string, string>();

            if (this.Options != null)
            {
                this.Options.TryGetValue("Photo0", out string profilePhoto);
                ret.Options.Put("Photo0", profilePhoto);
                this.Options.TryGetValue("TitleNameExt", out string TitleNameExt);
                ret.Options.Put("TitleNameExt", TitleNameExt);
            }

            return ret;
        }
    }

    [MessageType(TLConstants.TL_ROLE_START + 5)]
    public class TLClientRoleSnap : ISerializable, IPublicSnap
    {
        public string ID;
        public string digitID;
        public string Name;
        public int Level;
        public byte Gender;
        public byte Pro;
        public long FightPower;
        public string GuildId;
        public string GuildName;
        public List<AvatarInfoSnap> Avatar;
        public int PracticeLv;
        public byte OnlineState;
        public DateTime LastOfflineTime;
        public DateTime ExpiredUtc;
        public string ZoneUUID;
        public int MapTemplateID;
        public DateTime ExpiredUtcTime => ExpiredUtc;
        public int TitleID;
        /// <summary>
        /// 门派身份
        /// </summary>
        public int MasterId;

        //双人镇妖塔
        public int CPTowerLayer;

        //VIP等级
        public int vip_level;

        //自定义选项
        public HashMap<string, string> Options;

    }


    [MessageType(TLConstants.TL_ROLE_START + 6)]
    public class LogicServiceDesc : ISerializable
    {
        public string RoleID;
        public string SessionName;
    }

    [MessageType(TLConstants.TL_ROLE_START + 7)]
    public class TLRoleSnapExt : ISerializable, IPublicSnap
    {
        [PersistField]
        public string uuid;

        //PK值
        [PersistField]
        public int PKValue;

        //坐骑Id
        [PersistField]
        public int MountId;

        //灵脉Id
        [PersistField]
        public int VeinId;

        //金轮等级
        [PersistField]
        public int WingLv;

        [PersistField]  //avatar_type id
        public HashMap<int, int> SuitEquipMap;

        [PersistField]
        public TLUnitPropSnap PropSnap;

        //神器Id和Lv
        [PersistField]
        public HashMap<int, int> ArtifactMap;

        //神器技能id和Lv
        [PersistField]
        public List<GameSkill> ArtifactSkillList;


        //仙侣技能数据
        [PersistField]
        public GodData GodSkillData;

        //仙侣Id和Lv
        [PersistField]
        public HashMap<int, int> GoldMap;

        [PersistField]
        public HashMap<int, EntityItemData> EquipMap;

        /// 宝石镶嵌列表
        [PersistField]
        public HashMap<int, GridGemData> GemMap;

        ///  强化列表
        [PersistField]
        public HashMap<int, GridRefineData> RefineMap;


        /// 技能列表
        [PersistField]
        public HashMap<int, int> SkillMap;

        [PersistField]
        public DateTime ExpiredUtc;

        public DateTime ExpiredUtcTime => ExpiredUtcTime;


        public TLClientRoleSnapExt ToClientRoleSnapExt()
        {
            var ret = new TLClientRoleSnapExt
            {
                ID = uuid,
                PKValue = PKValue,
                MountId = MountId,
                VeinId = VeinId,
                WingLv = WingLv,

                PropSnap = PropSnap,
                SuitEquipMap = SuitEquipMap,
                GoldMap = GoldMap,
                ArtifactMap = ArtifactMap,
                GemMap = GemMap,
                EquipMap = new HashMap<int, BagSlotData>(),
                RefineMap = RefineMap,

            };
            foreach (var entry in EquipMap)
            {
                ret.EquipMap.Add(entry.Key, new BagSlotData { index = entry.Key, item = entry.Value });
            }
            return ret;
        }

        public static TLRoleSnapExt Create(string uuid)
        {
            TLRoleSnapExt ret = new TLRoleSnapExt();
            ret.uuid = uuid;
            ret.PKValue = 0;
            ret.MountId = 0;
            ret.VeinId = 0;
            ret.WingLv = 0;
            ret.PropSnap = new TLUnitPropSnap();
            ret.GoldMap = new HashMap<int, int>();
            ret.ArtifactMap = new HashMap<int, int>();
            ret.SuitEquipMap = new HashMap<int, int>();
            ret.EquipMap = new HashMap<int, EntityItemData>();
            ret.GemMap = new HashMap<int, GridGemData>();
            ret.RefineMap = new HashMap<int, GridRefineData>();
            ret.SkillMap = new HashMap<int, int>();
            ret.ArtifactSkillList = new List<GameSkill>();
            ret.GodSkillData = new GodData();
            return ret;
        }
    }


    [MessageType(TLConstants.TL_ROLE_START + 8)]
    public class TLClientRoleSnapExt : ISerializable, IPublicSnap
    {
        public string ID;

        //PK值 
        public int PKValue;

        //坐骑Id
        public int MountId;

        //灵脉Id
        public int VeinId;

        //金轮等级
        public int WingLv;

        //属性
        public TLUnitPropSnap PropSnap;

        //神器Id和Lv
        public HashMap<int, int> ArtifactMap;

        //仙侣Id和Lv
        public HashMap<int, int> GoldMap;

        //装备列表
        public HashMap<int, BagSlotData> EquipMap;

        /// 宝石镶嵌列表
        public HashMap<int, GridGemData> GemMap;

        ///  强化列表
        public HashMap<int, GridRefineData> RefineMap;

        //时装
        //avatar_type id
        public HashMap<int, int> SuitEquipMap;

        public DateTime ExpiredUtc;
        public DateTime ExpiredUtcTime => ExpiredUtc;
        //双人镇妖塔最高纪录
        public int CPTowerLayer;
    }

    [MessageType(TLConstants.TL_ROLE_START + 9)]
    public class TLUnitPropSnap : ISerializable
    {
        public int CurAnger = 0;

        public int CurHP = 1000;

        public int MaxHP = 1000;

        public int Attack = 1000;

        public int Defend;

        public int Mdef;

        public int Through;

        public int Block;

        public int Hit = 1000;

        public int Dodge;

        public int Crit;

        public int ResCrit;

        public int CriDamagePer;

        public int RedCriDamagePer;

        public int RunSpeed = 700;

        public int AutoRecoverHp;

        public int TotalReduceDamagePer;

        public int TotalDamagePer;

        public int FireDamage;

        public int ThunderDamage;

        public int SoilDamage;

        public int IceDamage;

        public int WindDamage;

        public int FireResist;

        public int ThunderResist;

        public int SoilResist;

        public int IceResist;

        public int WindResist;

        public int AllelementDamage;

        public int AllelementResist;

        public int OnHitRecoverHP;

        public int KillRecoverHP;

        public int ExtraGoldPer;

        public int ExtraEXPPer;

        public int GodDamage;

        public int TargetToMonster;

        public int TargetToPlayer;

        public int DefReduction;

        public int MDefReduction;

        public int Extracrit;
    }

    [MessageType(TLConstants.TL_ROLE_START + 10)]
    public class TLRoleBuffData : ISerializable
    {
        public HashMap<int, BuffSnap> BuffMap;

        public TLRoleBuffData()
        {
            BuffMap = new HashMap<int, BuffSnap>();
        }
    }

}
