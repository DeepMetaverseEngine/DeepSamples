using DeepCore;
using DeepCore.GameData;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Attributes;
using DeepCore.IO;
using DeepCore.Reflection;
using System;
using System.Collections.Generic;
using TLBattle.Common.Data;
using TLBattle.Message;

namespace TLBattle.Common.Plugins
{
    [MessageType(TLConstants.BATTLE_START + 1)]
    [DescAttribute("单位战斗属性")]
    public class TLUnitProperties : IUnitProperties
    {
        /// <summary>
        /// 单位类型.
        /// </summary>
        public enum TLUnitType : byte
        {
            [DescAttribute("无类型")]
            NONE,
            [DescAttribute("普通怪")]
            Normal,
            [DescAttribute("精英怪")]
            Elite,
            [DescAttribute("BOSS怪")]
            Boss,
            [DescAttribute("假玩家")]
            Player,
            [DescAttribute("助战机器人")]
            Helper,
            [DescAttribute("建筑物")]
            Building = 20,
        }
        /// <summary>
        /// 服务端数据.
        /// </summary>
        [DescAttribute("单位服务器属性")]
        public TLUnitData ServerData = new TLUnitData();

        [DescAttribute("单位类型")]
        public TLUnitType UnitType;

        [DescAttribute("用于客户端表现属性")]
        public TLUnitDisplayConfig UnitDisplayConfig = new TLUnitDisplayConfig();

        [DescAttribute("单位警戒配置")]
        public TLUnitGuardConfig UnitGuardConfig = null;

        [DescAttribute("开心的时候不主动干活")]
        public bool DoNothingWhenHappy;

        [DescAttribute("预加载资源列表")]
        public List<string> PreLoadResList = null;

        public TLUnitProperties()
        {

        }
        public override string ToString()
        {
            return "TL 单位扩展属性";
        }
        public object Clone()
        {
            TLUnitProperties ret = new TLUnitProperties();
            ret.UnitType = this.UnitType;

            if (this.ServerData != null)
                ret.ServerData = (TLUnitData)this.ServerData.Clone();

            if (this.UnitDisplayConfig != null)
                ret.UnitDisplayConfig = (TLUnitDisplayConfig)this.UnitDisplayConfig.Clone();

            if (this.UnitGuardConfig != null)
                ret.UnitGuardConfig = (TLUnitGuardConfig)this.UnitGuardConfig.Clone();

            ret.DoNothingWhenHappy = this.DoNothingWhenHappy;

            if (this.PreLoadResList != null)
                ret.PreLoadResList = new List<string>(this.PreLoadResList);

            return ret;

        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(UnitType);
            output.PutExt(ServerData);
            output.PutExt(UnitDisplayConfig);
            output.PutExt(UnitGuardConfig);
            output.PutBool(DoNothingWhenHappy);
            output.PutList(PreLoadResList, output.PutUTF);
        }
        public void ReadExternal(IInputStream input)
        {
            this.UnitType = input.GetEnum8<TLUnitType>();
            this.ServerData = input.GetExt<TLUnitData>();
            this.UnitDisplayConfig = input.GetExt<TLUnitDisplayConfig>();
            this.UnitGuardConfig = input.GetExt<TLUnitGuardConfig>();
            this.DoNothingWhenHappy = input.GetBool();
            this.PreLoadResList = input.GetList(input.GetUTF);
        }
    }

    /// <summary>
    /// 游戏服单位数据.
    /// </summary>
    [MessageType(TLConstants.BATTLE_START + 2)]
    [DescAttribute("游戏服数据")]
    public class TLUnitData : ICloneable, IExternalizable
    {
        /// <summary>
        /// 单位基础信息.
        /// </summary>
        public TLUnitBaseInfo BaseInfo = new TLUnitBaseInfo();

        /// <summary>
        ///装备时装信息.
        /// </summary>
        public HashMap<int, TLAvatarInfo> AvatarMap = new HashMap<int, TLAvatarInfo>();

        public List<TLAvatarInfo> AvatarList;

        /// <summary>
        /// 骑乘状态
        /// </summary>
        public bool RideStatus = false;

        /// <summary>
        /// 坐骑速度
        /// </summary>
        public int MountSpeed = 0;
        /// <summary>
        ///技能信息. 
        /// </summary>
        public TLUnitSkillInfo SkillInfo = null;
        /// <summary>
        /// 单位能力属性.
        /// </summary>
        public TLUnitProp Prop = new TLUnitProp();
        /// <summary>
        /// 单位掉落奖励拾取信息
        /// </summary>
        public TLDropRewardPickUp dropReward = new TLDropRewardPickUp();
        /// <summary>
        /// 任务数据.
        /// </summary>
        public List<TLQuestData> Quests = new List<TLQuestData>();
        /// <summary>
        /// PK信息.
        /// </summary>
        public PKInfo UnitPKInfo = new PKInfo();
        /// <summary>
        /// 队伍信息.
        /// </summary>
        public TeamData UnitTeamData = null;
        /// <summary>
        /// 复仇列表.
        /// </summary>
        public List<string> RevengeList = null;
        /// <summary>
        /// 宠物数据.
        /// </summary>
        public PetData UnitPetData = null;
        /// <summary>
        /// 仙侣数据.
        /// </summary>
        public GodData UnitGodData = null;
        /// <summary>
        /// 战斗BUFF数据.
        /// </summary>
        public List<BuffSnap> BattleBuffInfo = null;
        /// <summary>
        /// 场景数据.
        /// </summary>
        public BattleSceneData SceneDataInfo = null;

        public HashMap<int, int> MeridiansInfo = null;

        public TLUnitData()
        {

        }
        public override string ToString()
        {
            return "TL 单位扩展属性";
        }
        public object Clone()
        {
            TLUnitData ret = new TLUnitData();
            if (this.Prop != null) { ret.Prop = (TLUnitProp)this.Prop.Clone(); }
            if (this.SkillInfo != null) { ret.SkillInfo = (TLUnitSkillInfo)this.SkillInfo.Clone(); }
            if (this.BaseInfo != null) { ret.BaseInfo = (TLUnitBaseInfo)this.BaseInfo.Clone(); }
            if (this.AvatarMap != null) { ret.AvatarMap = new HashMap<int, TLAvatarInfo>(this.AvatarMap); }
            ret.RideStatus = this.RideStatus;
            ret.MountSpeed = this.MountSpeed;
            if (this.dropReward != null) { ret.dropReward = (TLDropRewardPickUp)(this.dropReward.Clone()); }
            if (this.Quests != null) { ret.Quests = new List<TLQuestData>(this.Quests); }
            if (UnitPKInfo != null) { ret.UnitPKInfo = (PKInfo)this.UnitPKInfo.Clone(); }
            if (UnitTeamData != null) { ret.UnitTeamData = (TeamData)UnitTeamData.Clone(); }
            if (RevengeList != null) { ret.RevengeList = new List<string>(RevengeList); }
            if (UnitPetData != null) { ret.UnitPetData = (PetData)UnitPetData.Clone(); }
            if (UnitGodData != null) { ret.UnitGodData = (GodData)UnitGodData.Clone(); }
            if (BattleBuffInfo != null) { ret.BattleBuffInfo = new List<BuffSnap>(BattleBuffInfo); }
            if (SceneDataInfo != null) { ret.SceneDataInfo = (BattleSceneData)SceneDataInfo.Clone(); }
            if (AvatarList != null) { ret.AvatarList = new List<TLAvatarInfo>(AvatarList); }
            if (MeridiansInfo != null) { ret.MeridiansInfo = new HashMap<int, int>(MeridiansInfo); }
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutExt(this.Prop);
            output.PutExt(this.SkillInfo);
            output.PutExt(this.BaseInfo);
            output.PutDataMap(this.AvatarMap);

            output.PutBool(this.RideStatus);
            output.PutS32(this.MountSpeed);
            output.PutList(this.Quests, output.PutExt);

            output.PutExt(this.UnitPKInfo);
            output.PutExt(this.UnitTeamData);
            output.PutList<string>(this.RevengeList, output.PutUTF);
            output.PutExt(this.UnitPetData);
            output.PutExt(this.UnitGodData);
            output.PutList<BuffSnap>(this.BattleBuffInfo, output.PutExt);
            output.PutExt(this.SceneDataInfo);
            output.PutList(this.AvatarList, output.PutExt);
            output.PutMap(this.MeridiansInfo, output.PutS32, output.PutS32);
        }
        public void ReadExternal(IInputStream input)
        {
            this.Prop = input.GetExt<TLUnitProp>();
            this.SkillInfo = input.GetExt<TLUnitSkillInfo>();
            this.BaseInfo = input.GetExt<TLUnitBaseInfo>();
            this.AvatarMap = input.GetDataMap<int, TLAvatarInfo>();

            this.RideStatus = input.GetBool();
            this.MountSpeed = input.GetS32();
            this.Quests = input.GetList<TLQuestData>(input.GetExt<TLQuestData>);

            this.UnitPKInfo = input.GetExt<PKInfo>();
            this.UnitTeamData = input.GetExt<TeamData>();
            this.RevengeList = input.GetUTFList();
            this.UnitPetData = input.GetExt<PetData>();
            this.UnitGodData = input.GetExt<GodData>();
            this.BattleBuffInfo = input.GetList(input.GetExt<BuffSnap>);
            this.SceneDataInfo = input.GetExt<BattleSceneData>();
            this.AvatarList = input.GetList(input.GetExt<TLAvatarInfo>);
            this.MeridiansInfo = input.GetMap(input.GetS32, input.GetS32);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 3)]
    [DescAttribute("单位基础信息")]
    public class TLUnitBaseInfo : IExternalizable, ICloneable
    {
        public enum GenderType : byte
        {
            MALE = 0,
            FEMALE = 1
        }

        public enum ProType : byte
        {
            None,       //无.
            YiZhu,
            TianGong,
            KunLun,
            QingQiu,
        }

        public enum MirrorType : byte
        {
            Monster,
            Player,
        }

        //等级.
        public int UnitLv = 0;
        //名字.
        public string Name = "";
        //职业.
        public ProType RolePro = ProType.None;
        //自动战斗状态.
        public bool IsGuard;
        /// <summary>
        /// 公会UUID.
        /// </summary>
        public string GuildUUID;
        /// <summary>
        /// 公会name.
        /// </summary>
        public string GuildName;
        /// <summary>
        /// 公会追杀列表.
        /// </summary>
        public List<string> GuildChaseList;
        /// <summary>
        /// 性别.
        /// </summary>
        public GenderType Gender;
        [DescAttribute("修行之道")]
        public int PracticeLv;

        [DescAttribute("称号id")]
        public int TitleID = 0;

        [DescAttribute("称号名称的扩展")]
        public string TitleNameExt;


        [DescAttribute("玩家镜像类型")]
        public MirrorType PlayerMirrorType = MirrorType.Monster;

        public List<TeamMemberSnap> list = null;
        public string teamLeaderUUID = null;
        public string teamUUID = null;


        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(UnitLv);
            output.PutUTF(Name);
            output.PutEnum8(RolePro);
            output.PutBool(IsGuard);
            output.PutUTF(GuildUUID);
            output.PutUTF(GuildName);
            output.PutList(GuildChaseList, output.PutUTF);
            output.PutEnum8(Gender);
            output.PutS32(PracticeLv);
            output.PutS32(this.TitleID);
            output.PutUTF(this.TitleNameExt);
            output.PutEnum8(PlayerMirrorType);
            output.PutList(list, output.PutExt);
            output.PutUTF(teamLeaderUUID);
            output.PutUTF(teamUUID);
        }
        public void ReadExternal(IInputStream input)
        {
            UnitLv = input.GetS32();
            Name = input.GetUTF();
            RolePro = input.GetEnum8<ProType>();
            IsGuard = input.GetBool();
            GuildUUID = input.GetUTF();
            GuildName = input.GetUTF();
            GuildChaseList = input.GetList(input.GetUTF);
            Gender = input.GetEnum8<GenderType>();
            PracticeLv = input.GetS32();
            this.TitleID = input.GetS32();
            this.TitleNameExt = input.GetUTF();
            PlayerMirrorType = input.GetEnum8<MirrorType>();
            list = input.GetList(input.GetExt<TeamMemberSnap>);
            teamLeaderUUID = input.GetUTF();
            teamUUID = input.GetUTF();
        }
        public object Clone()
        {
            TLUnitBaseInfo ret = new TLUnitBaseInfo();
            ret.Name = this.Name;
            ret.UnitLv = this.UnitLv;
            ret.RolePro = this.RolePro;
            ret.IsGuard = this.IsGuard;
            ret.GuildName = this.GuildName;
            ret.GuildUUID = this.GuildUUID;
            ret.GuildChaseList = this.GuildChaseList;
            ret.Gender = this.Gender;
            ret.PracticeLv = this.PracticeLv;
            ret.TitleID = this.TitleID;
            ret.TitleNameExt = this.TitleNameExt;
            ret.PlayerMirrorType = this.PlayerMirrorType;
            if (this.list != null)
                ret.list = new List<TeamMemberSnap>(this.list);
            ret.teamLeaderUUID = this.teamLeaderUUID;
            ret.teamUUID = this.teamUUID;

            return ret;
        }
    }

    [MessageType(TLConstants.BATTLE_START + 4)]
    [DescAttribute("单位avatar信息")]
    public class TLAvatarInfo : IExternalizable
    {
        public enum TLAvatar
        {
            None,
            Avatar_Body,    //身体 1
            Avatar_Head,    //头 1
            Foot_Buff,      //脚部特效挂载点.   
            L_Hand_Buff,        // 左手Buff(玩家)
            R_Hand_Buff,        // 右手Buff(玩家)  
            L_Hand_Weapon,      // 左手武器(玩家)（换武器 
            R_Hand_Weapon,      // 右手武器(玩家)（换武器）
            Rear_Weapon,      // 背部武器(玩家)（换武器）
            Chest_Buff,        // 胸部链接节点(玩家\怪物)
            Chest_Nlink,        // 胸部无链接节点(玩家\怪物)


            Rear_Equipment,     // 背部结点，翅膀
            Treasure_Equipment, // 法宝节点(玩家) 
            Ride_Avatar01,      // 坐骑节点(坐骑) 
            Head_Buff,          // 头部特效挂载点
            Equip_Buff,         // 身体特效
            L_Hand_Weapon_Buff, //武器特效结点
            R_Hand_Weapon_Buff, //武器特效结点

        }

        public TLAvatar PartTag = TLAvatar.None;
        public string FileName = null;
        //新时装用来标识显示的颜色
        public string DefaultName = null;
        public TLAvatarInfo() { }
        public void WriteExternal(IOutputStream output)
        {
            output.PutS32((int)PartTag);
            output.PutUTF(FileName);
            output.PutUTF(DefaultName);
        }
        public void ReadExternal(IInputStream input)
        {
            PartTag = (TLAvatar)input.GetS32();
            FileName = input.GetUTF();
            DefaultName = input.GetUTF();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 5)]
    [DescAttribute("单位技能信息")]
    public class TLUnitSkillInfo : ICloneable, IExternalizable
    {
        public List<GameSkill> Skills = null;
        public List<SkillSlot> Slots = null;
        public TLUnitSkillInfo()
        {
        }

        public object Clone()
        {
            TLUnitSkillInfo ret = new TLUnitSkillInfo();
            ret.Skills = CUtils.CloneList<GameSkill>(this.Skills);
            ret.Slots = CUtils.CloneList<SkillSlot>(this.Slots);
            return ret;
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutList<GameSkill>(this.Skills, output.PutExt);
            output.PutList<SkillSlot>(this.Slots, output.PutExt);
        }

        public void ReadExternal(IInputStream input)
        {
            this.Skills = input.GetList<GameSkill>(input.GetExt<GameSkill>);
            this.Slots = input.GetList<SkillSlot>(input.GetExt<SkillSlot>);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 6)]
    [DescAttribute("单位技能数据")]
    public class GameSkill : ICloneable, IExternalizable
    {
        public enum TLSkillType : byte
        {
            [Desc("主动技能")]
            active = 1, //主动攻击技能.
            [Desc("被动技能")]
            passive,    //被动技能.
            [Desc("普攻")]
            normalAtk,
            [Desc("仙侣")]
            God,//仙侣技能.
            [Desc("隐藏技能，技能面板不显示")]
            hideActive,//隐藏主动技能，客户端不显示.

        }
        [DescAttribute("技能类型")]
        public TLSkillType SkillType = TLSkillType.active;
        [DescAttribute("技能id")]
        [TemplateIDAttribute(typeof(SkillTemplate))]
        public int SkillID = 0;
        [DescAttribute("技能等级")]
        public int SkillLevel = 1;
        [DescAttribute("天赋等级")]
        public int TalentLevel = 0;
        [DescAttribute("技能位")]
        public int SkillIndex = 0;
        [DescAttribute("CD时间戳-服务端用")]
        public long SkillTimestampMS = 0;
        [DescAttribute("自动释放")]
        public bool AutoLaunch = true;

        public object Clone()
        {
            GameSkill ret = new GameSkill();
            ret.SkillID = this.SkillID;
            ret.SkillLevel = this.SkillLevel;
            ret.SkillType = this.SkillType;
            ret.SkillIndex = this.SkillIndex;
            ret.TalentLevel = this.TalentLevel;
            ret.SkillTimestampMS = this.SkillTimestampMS;
            ret.AutoLaunch = this.AutoLaunch;
            return ret;
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(SkillID);
            output.PutS32(SkillLevel);
            output.PutEnum8(SkillType);
            output.PutS32(SkillIndex);
            output.PutS32(TalentLevel);
            output.PutS64(SkillTimestampMS);
            output.PutBool(AutoLaunch);
        }

        public void ReadExternal(IInputStream input)
        {
            SkillID = input.GetS32();
            SkillLevel = input.GetS32();
            SkillType = input.GetEnum8<TLSkillType>();
            SkillIndex = input.GetS32();
            TalentLevel = input.GetS32();
            this.SkillTimestampMS = input.GetS64();
            this.AutoLaunch = input.GetBool();
        }
    }

    /// <summary>
    /// 单位能力值.
    /// </summary>
    [MessageType(TLConstants.BATTLE_START + 7)]
    [DescAttribute("单位战斗属性")]
    public class TLUnitProp : ICloneable, IExternalizable
    {
        //[DescAttribute("当前魔法")]
        //public int CurMP = 1000;
        //[DescAttribute("最大魔法")]
        //public int MaxMP = 1000;
        //[DescAttribute("魔法回复")]
        //public int AutoRecoverMP;

        [DescAttribute("当前怒气")]
        public int CurAnger = 0;

        [DescAttribute("当前生命")]
        public int CurHP = 1000;
        [DescAttribute("最大生命")]
        public int MaxHP = 1000;

        [DescAttribute("物理攻击")]
        public int Attack = 1000;
        [DescAttribute("物理防御")]
        public int PhyDef;
        [DescAttribute("法术防御")]
        public int MagDef;

        [DescAttribute("穿透等级（百分比减少物理和法术防御）")]
        public int Through;
        [DescAttribute("格挡等级")]
        public int Block;

        [DescAttribute("命中")]
        public int Hit = 1000;
        [DescAttribute("闪避")]
        public int Dodge;

        [DescAttribute("暴击")]
        public int Crit;
        [DescAttribute("抗暴")]
        public int ResCrit;

        [DescAttribute("暴击伤害")]
        public int CriDamagePer;
        [DescAttribute("暴伤减免")]
        public int RedCriDamagePer;

        [DescAttribute("移动速度")]
        public int RunSpeed = 700;
        [DescAttribute("生命回复")]
        public int AutoRecoverHp;

        [DescAttribute("伤害减免（万分比）")]
        public int TotalReduceDamagePer;
        [DescAttribute("伤害加成（万分比）")]
        public int TotalDamagePer;

        [DescAttribute("火元素伤害")]
        public int FireDmage;
        [DescAttribute("雷元素伤害")]
        public int ThunderDamage;
        [DescAttribute("土元素伤害")]
        public int SoilDamage;
        [DescAttribute("冰元素伤害")]
        public int IceDamage;
        [DescAttribute("风元素伤害")]
        public int WindDamage;

        [DescAttribute("火元素抗性")]
        public int FireResist;
        [DescAttribute("雷元素抗性")]
        public int ThunderResist;
        [DescAttribute("土元素抗性")]
        public int SoilResist;
        [DescAttribute("冰元素抗性")]
        public int IceResist;
        [DescAttribute("风元素抗性")]
        public int WindResist;

        [DescAttribute("全元素伤害")]
        public int AllelementDamage;
        [DescAttribute("全元素抗性")]
        public int AllelementResist;

        [DescAttribute("击中回复生命")]
        public int OnHitRecoverHP;
        [DescAttribute("击杀回复生命")]
        public int KillRecoverHP;

        [DescAttribute("金币加成（万分比）")]
        public int ExtraGoldPer;
        [DescAttribute("经验加成（万分比）")]
        public int ExtraEXPPer;
        [DescAttribute("真实伤害")]
        public int GodDamage;

        [DescAttribute("对怪物伤害")]
        public int TargetToMonster;
        [DescAttribute("对玩家伤害")]
        public int TargetToPlayer;

        [DescAttribute("物理免伤")]
        public int DefReduction;
        [DescAttribute("法术免伤")]
        public int MDefReduction;
        [DescAttribute("额外暴击")]
        public int Extracrit;

        public TLUnitProp()
        {
        }

        public object Clone()
        {
            TLUnitProp ret = new TLUnitProp();

            //ret.CurMP = this.CurMP;
            //ret.MaxMP = this.MaxMP;

            ret.CurAnger = this.CurAnger;

            ret.CurHP = this.CurHP;
            ret.MaxHP = this.MaxHP;

            ret.Attack = this.Attack;
            ret.PhyDef = this.PhyDef;
            ret.MagDef = this.MagDef;

            ret.Through = this.Through;
            ret.Block = this.Block;

            ret.Hit = this.Hit;
            ret.Dodge = this.Dodge;
            ret.Crit = this.Crit;
            ret.ResCrit = this.ResCrit;

            ret.CriDamagePer = this.CriDamagePer;
            ret.RedCriDamagePer = this.RedCriDamagePer;

            ret.RunSpeed = this.RunSpeed;
            ret.AutoRecoverHp = this.AutoRecoverHp;

            ret.TotalReduceDamagePer = this.TotalReduceDamagePer;
            ret.TotalDamagePer = this.TotalDamagePer;

            ret.FireDmage = this.FireDmage;
            ret.ThunderDamage = this.ThunderDamage;
            ret.SoilDamage = this.SoilDamage;
            ret.IceDamage = this.IceDamage;
            ret.WindDamage = this.WindDamage;

            ret.FireResist = this.FireResist;
            ret.ThunderResist = this.ThunderResist;
            ret.SoilResist = this.SoilResist;
            ret.IceResist = this.IceResist;
            ret.WindResist = this.WindResist;

            ret.AllelementDamage = this.AllelementDamage;
            ret.AllelementResist = this.AllelementResist;

            ret.OnHitRecoverHP = this.OnHitRecoverHP;
            ret.KillRecoverHP = this.KillRecoverHP;

            ret.ExtraGoldPer = this.ExtraGoldPer;
            ret.ExtraEXPPer = this.ExtraEXPPer;

            ret.TargetToMonster = this.TargetToMonster;
            ret.TargetToPlayer = this.TargetToPlayer;

            ret.GodDamage = this.GodDamage;

            ret.DefReduction = this.DefReduction;
            ret.MDefReduction = this.MDefReduction;
            ret.Extracrit = this.Extracrit;
            //39个.
            return ret;
        }

        public void WriteExternal(IOutputStream output)
        {
            //output.PutS32(CurMP);
            //output.PutS32(MaxMP);
            output.PutS32(CurAnger);

            output.PutS32(CurHP);
            output.PutS32(MaxHP);

            output.PutS32(Attack);
            output.PutS32(PhyDef);
            output.PutS32(MagDef);

            output.PutS32(Through);
            output.PutS32(Block);

            output.PutS32(Hit);
            output.PutS32(Dodge);
            output.PutS32(Crit);
            output.PutS32(ResCrit);

            output.PutS32(CriDamagePer);
            output.PutS32(RedCriDamagePer);

            output.PutS32(RunSpeed);
            output.PutS32(AutoRecoverHp);
            output.PutS32(TotalReduceDamagePer);
            output.PutS32(TotalDamagePer);

            output.PutS32(FireDmage);
            output.PutS32(ThunderDamage);
            output.PutS32(SoilDamage);
            output.PutS32(IceDamage);
            output.PutS32(WindDamage);

            output.PutS32(FireResist);
            output.PutS32(ThunderResist);
            output.PutS32(SoilResist);
            output.PutS32(IceResist);
            output.PutS32(WindResist);

            output.PutS32(AllelementDamage);
            output.PutS32(AllelementResist);

            output.PutS32(OnHitRecoverHP);
            output.PutS32(KillRecoverHP);

            output.PutS32(ExtraGoldPer);
            output.PutS32(ExtraEXPPer);

            output.PutS32(TargetToMonster);
            output.PutS32(TargetToPlayer);

            output.PutS32(GodDamage);

            output.PutS32(DefReduction);
            output.PutS32(MDefReduction);
            output.PutS32(Extracrit);
        }

        public void ReadExternal(IInputStream input)
        {
            //this.CurMP = input.GetS32();
            //this.MaxMP = input.GetS32();
            this.CurAnger = input.GetS32();

            this.CurHP = input.GetS32();
            this.MaxHP = input.GetS32();

            this.Attack = input.GetS32();
            this.PhyDef = input.GetS32();
            this.MagDef = input.GetS32();

            this.Through = input.GetS32();
            this.Block = input.GetS32();

            this.Hit = input.GetS32();
            this.Dodge = input.GetS32();

            this.Crit = input.GetS32();
            this.ResCrit = input.GetS32();

            this.CriDamagePer = input.GetS32();
            this.RedCriDamagePer = input.GetS32();

            this.RunSpeed = input.GetS32();
            this.AutoRecoverHp = input.GetS32();

            this.TotalReduceDamagePer = input.GetS32();
            this.TotalDamagePer = input.GetS32();

            this.FireDmage = input.GetS32();
            this.ThunderDamage = input.GetS32();
            this.SoilDamage = input.GetS32();
            this.IceDamage = input.GetS32();
            this.WindDamage = input.GetS32();

            this.FireResist = input.GetS32();
            this.ThunderResist = input.GetS32();
            this.SoilResist = input.GetS32();
            this.IceResist = input.GetS32();
            this.WindResist = input.GetS32();

            this.AllelementDamage = input.GetS32();
            this.AllelementResist = input.GetS32();

            this.OnHitRecoverHP = input.GetS32();
            this.KillRecoverHP = input.GetS32();

            this.ExtraGoldPer = input.GetS32();
            this.ExtraEXPPer = input.GetS32();

            this.TargetToMonster = input.GetS32();
            this.TargetToPlayer = input.GetS32();

            this.GodDamage = input.GetS32();

            this.DefReduction = input.GetS32();
            this.MDefReduction = input.GetS32();
            this.Extracrit = input.GetS32();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 8)]
    [DescAttribute("攻击属性")]
    public class TLAttackProperties : IAttackProperties
    {
        [DescAttribute("技能ID（通过ID获取技能等级）", "技能ID")]
        public int SkillTemplateID = 0;//扩展脚本默认ID.
        [DescAttribute("参数1(0物理/1魔法)", "技能扩展参数1")]
        public int SkillArgu_1 = 0;
        [DescAttribute("参数2", "技能扩展参数2")]
        public int SkillArgu_2 = 0;
        [DescAttribute("伤害分割", "伤害扩展")]
        public DamageSplitData SplitData = null;

        [DescAttribute("是否使用脚本配置", "伤害扩展")]
        public bool UseConfig = true;
        [DescAttribute("元素伤害(0无/1雷元素/2风元素/3冰元素/4火元素/5土元素/)", "伤害扩展")]
        public int ElementType = 0;
        [DescAttribute("伤害系数(万分比)", "伤害扩展")]
        public int SkillCoefficient = 0;
        [DescAttribute("伤害绝对值", "伤害扩展")]
        public int SkillDamage = 0;
        [DescAttribute("元素伤害系数(万分比)", "伤害扩展")]
        public int ElementCoefficient = 0;
        [DescAttribute("元素伤害绝对值", "伤害扩展")]
        public int ElementDamage = 0;


        public TLAttackProperties()
        {

        }
        public override string ToString()
        {
            return "TL 攻击扩展属性";
        }
        public object Clone()
        {
            TLAttackProperties ret = new TLAttackProperties();

            ret.SkillTemplateID = this.SkillTemplateID;
            ret.SkillArgu_1 = this.SkillArgu_1;
            ret.SkillArgu_2 = this.SkillArgu_2;
            ret.SplitData = this.SplitData;
            ret.UseConfig = this.UseConfig;
            ret.ElementType = this.ElementType;
            ret.ElementCoefficient = this.ElementCoefficient;
            ret.ElementDamage = this.ElementDamage;
            ret.SkillCoefficient = this.SkillCoefficient;
            ret.SkillDamage = this.SkillDamage;
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(SkillTemplateID);
            output.PutS32(SkillArgu_1);
            output.PutS32(SkillArgu_2);
            output.PutExt(SplitData);
            output.PutBool(UseConfig);
            output.PutS32(ElementType);
            output.PutS32(ElementCoefficient);
            output.PutS32(ElementDamage);
            output.PutS32(SkillCoefficient);
            output.PutS32(SkillDamage);
        }
        public void ReadExternal(IInputStream input)
        {
            SkillTemplateID = input.GetS32();
            SkillArgu_1 = input.GetS32();
            SkillArgu_2 = input.GetS32();
            SplitData = input.GetExt<DamageSplitData>();
            UseConfig = input.GetBool();
            ElementType = input.GetS32();
            ElementCoefficient = input.GetS32();
            ElementDamage = input.GetS32();
            SkillCoefficient = input.GetS32();
            SkillDamage = input.GetS32();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 9)]
    [DescAttribute("BUFF属性")]
    public class TLBuffProperties : IBuffProperties
    {

        //[DescAttribute("技能循环列表", "Buff扩展")]
        //public List<TLGameSkillSnap> SkillLoopList = null;

        [ListAttribute(typeof(TLBuffData))]
        public List<TLBuffData> buffData = null;
        [DescAttribute("Buff期间绑定特效资源（客户端用）", "特效")]
        public string CustomEffectRes;
        [DescAttribute("Buff期间是否在左上角显示（客户端用）", "特效")]
        public bool IsShowLabel = false;
        [DescAttribute("Buff期间是否文字显示（客户端用）", "特效")]
        public bool IsShowWord = false;
        [DescAttribute("Buff是否可被驱散", "Buff")]
        public bool CanBePurged = true;
        [DescAttribute("Buff切场景是否保存", "Buff")]
        public bool SaveOnChangeScene = true;

        public TLBuffProperties()
        {

        }
        public override string ToString()
        {
            return "TL BUFF扩展属性";
        }
        public object Clone()
        {
            TLBuffProperties prop = new TLBuffProperties();
            prop.buffData = this.buffData;
            prop.CustomEffectRes = this.CustomEffectRes;
            prop.IsShowLabel = this.IsShowLabel;
            prop.IsShowWord = this.IsShowWord;
            prop.CanBePurged = this.CanBePurged;
            prop.SaveOnChangeScene = this.SaveOnChangeScene;
            return prop;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutList(this.buffData, output.PutExt);
            output.PutUTF(this.CustomEffectRes);
            output.PutBool(this.IsShowLabel);
            output.PutBool(this.IsShowWord);
            output.PutBool(this.CanBePurged);
            output.PutBool(this.SaveOnChangeScene);
        }
        public void ReadExternal(IInputStream input)
        {
            this.buffData = input.GetList(input.GetExt<TLBuffData>);
            this.CustomEffectRes = input.GetUTF();
            this.IsShowLabel = input.GetBool();
            this.IsShowWord = input.GetBool();
            this.CanBePurged = input.GetBool();
            this.SaveOnChangeScene = input.GetBool();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 10)]
    [DescAttribute("道具属性")]
    public class TLItemProperties : IItemProperties
    {
        public enum ItemType : byte
        {
            None,
            Task,
        }

        [DescAttribute("可进行采集的阵营,默认为0，所有阵营可采集", "物品属性")]
        public byte PickUnitForce = 0;
        [DescAttribute("是否自动战斗中自动拾取", "物品属性")]
        public bool AllowAutoPick = true;


        [DescAttribute("是否需要点击拾取，为true需要手动点击拾取，且此时PreTimes无效", "物品属性")]
        public bool HandPick = true;
        [DescAttribute("N毫秒后自动采集", "物品属性")]
        public int PreTimes = 0;

        [DescAttribute("是否显示进度百分比", "物品属性")]
        public bool ShowPickPercentText = true;
        [DescAttribute("是否显示进度条", "物品属性")]
        public bool ShowPickPercentGauge = true;

        [DescAttribute("物品类型", "物品属性")]
        public ItemType Type = ItemType.None;

        [LocalizationTextAttribute]
        [DescAttribute("等待拾取按钮名字(浇水)", "物品属性")]
        public string WaitPickBtnName = null;

        [LocalizationTextAttribute]
        [DescAttribute("拾取按钮名字(浇水)", "物品属性")]
        public string PickBtnName = null;
        [DescAttribute("拾取动作（n_fish）", "物品属性")]
        public string PickAction = null;
        [DescAttribute("拾取特效.", "物品属性")]
        public string PickEffect = null;
        [DescAttribute("拾取触发事件.", "物品属性")]
        public string PickTriggerEvt = null;
        [DescAttribute("完成拾取触发事件.", "物品属性")]
        public string FinishPickTriggerEvt = null;

        [DescAttribute("交互按钮图片", "物品属性")]
        public string PickIcon = null;
        [DescAttribute("是否显示获得特效.", "物品属性")]
        public bool ShowGotEffect = true;


        public TLItemProperties()
        {

        }
        public override string ToString()
        {
            return "TL 道具扩展属性";
        }
        public object Clone()
        {
            TLItemProperties ret = new TLItemProperties();

            ret.PickUnitForce = this.PickUnitForce;
            ret.AllowAutoPick = this.AllowAutoPick;

            ret.PreTimes = this.PreTimes;
            ret.HandPick = this.HandPick;

            ret.ShowPickPercentGauge = this.ShowPickPercentGauge;
            ret.ShowPickPercentText = this.ShowPickPercentText;

            ret.Type = this.Type;
            ret.WaitPickBtnName = this.WaitPickBtnName;
            ret.PickBtnName = this.PickBtnName;
            ret.PickAction = this.PickAction;
            ret.PickEffect = this.PickEffect;
            ret.PickTriggerEvt = this.PickTriggerEvt;
            ret.FinishPickTriggerEvt = this.FinishPickTriggerEvt;

            ret.PickIcon = this.PickIcon;
            ret.ShowGotEffect = this.ShowGotEffect;
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutU8(this.PickUnitForce);
            output.PutBool(this.AllowAutoPick);

            output.PutS32(this.PreTimes);
            output.PutBool(this.HandPick);

            output.PutBool(this.ShowPickPercentGauge);
            output.PutBool(this.ShowPickPercentText);

            output.PutEnum8(this.Type);
            output.PutUTF(this.WaitPickBtnName);
            output.PutUTF(this.PickBtnName);
            output.PutUTF(this.PickAction);
            output.PutUTF(this.PickEffect);
            output.PutUTF(this.PickTriggerEvt);
            output.PutUTF(this.FinishPickTriggerEvt);

            output.PutUTF(PickIcon);
            output.PutBool(ShowGotEffect);
        }
        public void ReadExternal(IInputStream input)
        {
            PickUnitForce = input.GetU8();
            AllowAutoPick = input.GetBool();

            PreTimes = input.GetS32();
            HandPick = input.GetBool();

            ShowPickPercentGauge = input.GetBool();
            ShowPickPercentText = input.GetBool();

            Type = input.GetEnum8<ItemType>();

            WaitPickBtnName = input.GetUTF();
            PickBtnName = input.GetUTF();

            PickAction = input.GetUTF();
            PickEffect = input.GetUTF();
            PickTriggerEvt = input.GetUTF();
            FinishPickTriggerEvt = input.GetUTF();

            PickIcon = input.GetUTF();
            ShowGotEffect = input.GetBool();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 11)]
    [DescAttribute("技能属性")]
    public class TLSkillProperties : ISkillProperties
    {
        /// <summary>
        /// 技能目标类型.
        /// </summary>
        public enum TLSkillTargetType : byte
        {
            [Desc("无目标-施法目标类型.")]
            None,
            [Desc("需要目标-施法目标类型")]
            NeedTarget,
            [Desc("对自己-施法目标类型")]
            Self,
        }

        [DescAttribute("单位死亡终止技能 - 扩展属性.")]
        public bool StopSkillOnUnitDead = false;

        [DescAttribute("主动/被动/普攻 - 技能类型.")]
        public GameSkill.TLSkillType SkillType = GameSkill.TLSkillType.active;

        [DescAttribute("无/敌人/自己 - 技能施法目标类型.")]
        public TLSkillTargetType TargetType = TLSkillTargetType.NeedTarget;

        [DescAttribute("圆形/扇形/箭头 - 技能施放模式.")]
        public TLSkillLaunchModeData LaunchModeData = new TLSkillLaunchModeData();

        [DescAttribute("表现配置 - 表现配置.")]
        public TLSkillDisplayConfig DisplayConfig = new TLSkillDisplayConfig();

        [DescAttribute("触发事件 - 表现配置.")]
        public string TriggerEvent = null;
        [DescAttribute("是否读取配置(配置表/本地) - 技能配置.")]
        public bool LoadConfig = false;

        public TLSkillProperties() { }
        public override string ToString()
        {
            return "TL 技能扩展属性";
        }
        public object Clone()
        {
            TLSkillProperties ret = new TLSkillProperties();
            ret.StopSkillOnUnitDead = this.StopSkillOnUnitDead;
            ret.SkillType = this.SkillType;
            ret.TargetType = this.TargetType;
            ret.LaunchModeData = this.LaunchModeData;
            ret.DisplayConfig = this.DisplayConfig;
            ret.TriggerEvent = this.TriggerEvent;
            ret.LoadConfig = this.LoadConfig;
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutBool(this.StopSkillOnUnitDead);
            output.PutEnum8(this.SkillType);
            output.PutEnum8(this.TargetType);
            output.PutExt(this.LaunchModeData);
            output.PutExt(this.DisplayConfig);
            output.PutUTF(this.TriggerEvent);
            output.PutBool(this.LoadConfig);
        }

        public void ReadExternal(IInputStream input)
        {
            this.StopSkillOnUnitDead = input.GetBool();
            this.SkillType = input.GetEnum8<GameSkill.TLSkillType>();
            this.TargetType = input.GetEnum8<TLSkillTargetType>();
            this.LaunchModeData = input.GetExt<TLSkillLaunchModeData>();
            this.DisplayConfig = input.GetExt<TLSkillDisplayConfig>();
            this.TriggerEvent = input.GetUTF();
            this.LoadConfig = input.GetBool();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 12)]
    [DescAttribute("场景属性")]
    public class TLSceneProperties : ISceneProperties
    {
        public enum SceneType : byte
        {
            None = 0,           //野外.
            Dungeon,        //副本.
        }

        /// <summary>
        /// 当前场景类型.
        /// </summary>
        [DescAttribute("当前场景类型", "游戏服场景配置数据.")]
        public SceneType CurSceneType;

        [DescAttribute("进视野range", "视野相关")]
        public int Sync_InRange = 20;
        [DescAttribute("出视野range", "视野相关")]
        public int Sync_OutRange = 30;

        [DescAttribute("服务端场景配置数据", "游戏服场景配置数据.")]
        public TLServerSceneData ServerSceneData = new TLServerSceneData();


        public TLSceneProperties()
        {
        }
        public override string ToString()
        {
            return "TL 场景扩展属性";
        }
        public object Clone()
        {
            TLSceneProperties ret = new TLSceneProperties();
            ret.CurSceneType = this.CurSceneType;
            ret.Sync_InRange = this.Sync_InRange;
            ret.Sync_OutRange = this.Sync_OutRange;
            ret.ServerSceneData = this.ServerSceneData;
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(CurSceneType);
            output.PutS32(Sync_InRange);
            output.PutS32(Sync_OutRange);
            output.PutExt(ServerSceneData);
        }
        public void ReadExternal(IInputStream input)
        {
            this.CurSceneType = input.GetEnum8<SceneType>();
            Sync_InRange = input.GetS32();
            Sync_OutRange = input.GetS32();
            this.ServerSceneData = input.GetExt<TLServerSceneData>();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 14)]
    [DescAttribute("法术属性")]
    public class TLSpellProperties : ISpellProperties
    {
        [DescAttribute("线性/抛物线/ - 客户端弹道类型表现")]
        public enum ClientBallisticType : byte
        {
            [DescAttribute("线性")]
            Linear,
            [DescAttribute("抛物线")]
            Parabola,
        }

        public ClientBallisticType BallisticType = ClientBallisticType.Linear;

        public TLSpellProperties()
        {

        }

        public override string ToString()
        {
            return "TL 法术扩展属性";
        }
        public object Clone()
        {
            TLSpellProperties ret = new TLSpellProperties();
            ret.BallisticType = this.BallisticType;
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(BallisticType);
        }
        public void ReadExternal(IInputStream input)
        {
            BallisticType = input.GetEnum8<ClientBallisticType>();
        }
    }

    [MessageType(TLConstants.BATTLE_START + 15)]
    [DescAttribute("单位显示相关数据.")]
    public class TLUnitDisplayConfig : ICloneable, IExternalizable
    {
        public enum HPBannerStatus : byte
        {
            ShowInCombat, //战斗中显示.
            Hide,         //始终不显示.
            Always,       //始终显示.
        }

        [DescAttribute("头像图片")]
        public string HeadIcon = "default";

        [DescAttribute("死亡淡出时间(毫秒)")]
        public int DeadAlphaTime = 0;

        [DescAttribute("血条显示配置:战中显示/始终不显示/始终显示")]
        public HPBannerStatus HPBannerCfg = HPBannerStatus.ShowInCombat;

        [DescAttribute("NPC对话时候是否需要拉近镜头")]
        public bool IsNeedTalkCamera = true;

        [DescAttribute("影子系数")]
        public float ShadowCoefficient = 1f;

        [DescAttribute("是否触发遇敌(显示感叹号)")]
        public bool CanShowSign = true;

        [DescAttribute("npc对话摄像机间距")]
        public float CameraDistance = 1.8f;

        [DescAttribute("对话时是否转身")]
        public bool TalkTurnRound = true;

        public TLUnitDisplayConfig() { }

        public object Clone()
        {
            TLUnitDisplayConfig ret = new TLUnitDisplayConfig();

            ret.HeadIcon = this.HeadIcon;
            ret.DeadAlphaTime = this.DeadAlphaTime;
            ret.IsNeedTalkCamera = this.IsNeedTalkCamera;
            ret.ShadowCoefficient = this.ShadowCoefficient;
            ret.CanShowSign = this.CanShowSign;
            ret.CameraDistance = this.CameraDistance;
            ret.HPBannerCfg = this.HPBannerCfg;
            ret.TalkTurnRound = this.TalkTurnRound;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            this.HeadIcon = input.GetUTF();
            this.DeadAlphaTime = input.GetS32();
            this.IsNeedTalkCamera = input.GetBool();
            this.ShadowCoefficient = input.GetF32();
            this.CanShowSign = input.GetBool();
            this.CameraDistance = input.GetF32();
            this.HPBannerCfg = input.GetEnum8<HPBannerStatus>();
            this.TalkTurnRound = input.GetBool();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(HeadIcon);
            output.PutS32(DeadAlphaTime);
            output.PutBool(this.IsNeedTalkCamera);
            output.PutF32(this.ShadowCoefficient);
            output.PutBool(this.CanShowSign);
            output.PutF32(this.CameraDistance);
            output.PutEnum8(this.HPBannerCfg);
            output.PutBool(this.TalkTurnRound);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 16)]
    [DescAttribute("巡逻配置.")]
    public class TLUnitGuardConfig : ICloneable, IExternalizable
    {
        //怪物警戒范围类型
        public enum GuardType : byte
        {
            [DescAttribute("圆形,坐标是否在圆形范围内")]
            RoundCenter,
            [DescAttribute("圆形,身体是否在圆形范围内")]
            RoundBody,
            [DescAttribute("矩形,坐标是否在矩形范围内")]
            RectCenter,
            [DescAttribute("扇形,坐标是否在扇形范围内")]
            FanCenter,
            [DescAttribute("扇形,身体是否在扇形范围内")]
            FanBody,
        }

        [DescAttribute("警戒范围类型")]
        public GuardType UGuardType = GuardType.RoundBody;

        [DescAttribute("归位后是否回复满血")]
        public bool ReturnToFullHP = false;

        [DescAttribute("NPC触发范围")]
        public float NpcNearRange = 3f;

        #region 根据实际需求整合成wanderConfig.

        [DescAttribute("大王叫我来巡山", "npc巡逻专用")]
        public bool IsPatrolNpc = false;
        [DescAttribute("巡逻随机范围", "npc巡逻专用")]
        [DependOnProperty("IsPatrolNpc")]
        public float PatrolDistance = 5;
        [DescAttribute("最大巡逻等待时间(毫秒)", "npc巡逻专用")]
        [DependOnProperty("IsPatrolNpc")]
        public int PatrolWaitMaxTime = 1000;
        [DependOnProperty("IsPatrolNpc")]
        [DescAttribute("最小巡逻等待时间(毫秒)", "npc巡逻专用")]
        public int PatrolWaitMinTime = 1000;
        [DescAttribute("最大巡逻移动时间(毫秒)", "npc巡逻专用")]
        [DependOnProperty("IsPatrolNpc")]
        public int PatrolRunMaxTime = 5000;
        [DescAttribute("最小巡逻移动时间(毫秒)", "npc巡逻专用")]
        [DependOnProperty("IsPatrolNpc")]
        public int PatrolRunMinTime = 5000;
        [DescAttribute("巡逻移动速度(万分比)正值加速，负值减速", "npc巡逻专用")]
        [DependOnProperty("IsPatrolNpc")]
        public int PatrolRunSpeed = -7000;

        #endregion

        public object Clone()
        {
            TLUnitGuardConfig ret = new TLUnitGuardConfig();

            ret.UGuardType = this.UGuardType;
            ret.ReturnToFullHP = this.ReturnToFullHP;
            ret.NpcNearRange = this.NpcNearRange;

            ret.IsPatrolNpc = this.IsPatrolNpc;
            ret.PatrolDistance = this.PatrolDistance;
            ret.PatrolWaitMaxTime = this.PatrolWaitMaxTime;
            ret.PatrolWaitMinTime = this.PatrolWaitMinTime;
            ret.PatrolRunMaxTime = this.PatrolRunMaxTime;
            ret.PatrolRunMinTime = this.PatrolRunMinTime;
            ret.PatrolRunSpeed = this.PatrolRunSpeed;

            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            this.UGuardType = input.GetEnum8<GuardType>();

            this.ReturnToFullHP = input.GetBool();
            this.NpcNearRange = input.GetF32();
            this.IsPatrolNpc = input.GetBool();
            this.PatrolDistance = input.GetF32();
            this.PatrolWaitMaxTime = input.GetS32();
            this.PatrolWaitMinTime = input.GetS32();
            this.PatrolRunMaxTime = input.GetS32();
            this.PatrolRunMinTime = input.GetS32();
            this.PatrolRunSpeed = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(this.UGuardType);

            output.PutBool(this.ReturnToFullHP);
            output.PutF32(this.NpcNearRange);
            output.PutBool(this.IsPatrolNpc);
            output.PutF32(this.PatrolDistance);
            output.PutS32(this.PatrolWaitMaxTime);
            output.PutS32(this.PatrolWaitMinTime);
            output.PutS32(this.PatrolRunMaxTime);
            output.PutS32(this.PatrolRunMinTime);
            output.PutS32(this.PatrolRunSpeed);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 17)]
    [DescAttribute("游戏服场景配置数据.")]
    public class TLServerSceneData : ICloneable, IExternalizable
    {
        [DescAttribute("是否允许自动挂机", "游戏服场景配置数据.")]
        public bool AllowAutoGuard = true;
        [DescAttribute("场景配置ID", "游戏服场景配置数据.")]
        public int DataID = 0;
        [DescAttribute("是否计算PK值", "游戏服场景配置数据.")]
        public bool CalPKValue = true;
        [DescAttribute("场景类型", "游戏服场景配置数据.")]
        public TLConstants.SceneType SceneType = TLConstants.SceneType.Normal;

        public object Clone()
        {
            TLServerSceneData ret = new TLServerSceneData();
            ret.AllowAutoGuard = this.AllowAutoGuard;
            ret.DataID = this.DataID;
            ret.CalPKValue = this.CalPKValue;
            ret.SceneType = this.SceneType;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            AllowAutoGuard = input.GetBool();
            DataID = input.GetS32();
            CalPKValue = input.GetBool();
            SceneType = input.GetEnum8<TLConstants.SceneType>();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutBool(AllowAutoGuard);
            output.PutS32(DataID);
            output.PutBool(CalPKValue);
            output.PutEnum8(SceneType);
        }
    }

    [Desc("施法区域提示")]
    [MessageType(TLConstants.BATTLE_START + 18)]
    [ExpandableAttribute]
    public class TLSkillLaunchModeData : ICloneable, IExternalizable
    {
        public enum TLSkillLaunchMode : byte
        {
            [Desc("普通模式")]
            Mode_None,
            [Desc("箭头模式")]
            Mode_Arrow,
            [Desc("扇形模式")]
            Mode_Fan,
            [Desc("区域模式")]
            Mode_Area,
        }

        [Desc("施法模式")]
        public TLSkillLaunchMode Mode = TLSkillLaunchMode.Mode_None;
        [Desc("技能范围半径")]
        public float Shape_Radius = 0;
        [Desc("箭头模式下代表箭头宽度")]
        public float Shape_Width = 0;
        [Desc("扇形模式下代表扇形展开范围")]
        public int Shape_Angle = 0;
        [Desc("施放点和单位中心点偏移距离")]
        public float Shape_LaunchCenterOffset = 0;
        [Desc("显示施法引导")]
        public bool ShowLaunchGuide = true;

        public object Clone()
        {
            TLSkillLaunchModeData ret = new TLSkillLaunchModeData();

            ret.Mode = this.Mode;
            ret.Shape_Radius = this.Shape_Radius;
            ret.Shape_Angle = this.Shape_Angle;
            ret.Shape_Width = this.Shape_Width;
            ret.Shape_LaunchCenterOffset = this.Shape_LaunchCenterOffset;
            ret.ShowLaunchGuide = this.ShowLaunchGuide;
            return ret;
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(Mode);
            output.PutF32(Shape_Radius);
            output.PutS32(Shape_Angle);
            output.PutF32(Shape_Width);
            output.PutF32(Shape_LaunchCenterOffset);
            output.PutBool(ShowLaunchGuide);
        }

        public void ReadExternal(IInputStream input)
        {
            Mode = input.GetEnum8<TLSkillLaunchMode>();
            Shape_Radius = input.GetF32();
            Shape_Angle = input.GetS32();
            Shape_Width = input.GetF32();
            Shape_LaunchCenterOffset = input.GetF32();
            ShowLaunchGuide = input.GetBool();
        }
    }

    [Desc("技能ID")]
    [MessageType(TLConstants.BATTLE_START + 19)]
    [ExpandableAttribute]
    public class TLGameSkillSnap : ICloneable, IExternalizable
    {
        [TemplateIDAttribute(typeof(SkillTemplate))]
        public int SkillID;

        public object Clone()
        {
            var ret = new TLGameSkillSnap();
            ret.SkillID = this.SkillID;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            SkillID = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(SkillID);
        }
    }

    [Desc("技能表现配置")]
    [MessageType(TLConstants.BATTLE_START + 20)]
    public class TLSkillDisplayConfig : ICloneable, IExternalizable
    {
        [Desc("脸朝向单位")]
        public bool FaceToTarget = true;

        public object Clone()
        {
            TLSkillDisplayConfig ret = new TLSkillDisplayConfig();
            ret.FaceToTarget = this.FaceToTarget;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            FaceToTarget = input.GetBool();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutBool(FaceToTarget);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 21)]
    [DescAttribute("掉落奖励拾取信息")]
    public class TLDropRewardPickUp : ICloneable, IExternalizable
    {
        public int dailyGold;

        public int dailyExp;

        [Desc("场景id,怪物id,首通时间")]
        public HashMap<int, HashMap<int, string>> firstPassMap = new HashMap<int, HashMap<int, string>>();


        public object Clone()
        {
            TLDropRewardPickUp ret = new TLDropRewardPickUp();
            ret.dailyGold = this.dailyGold;
            ret.dailyExp = this.dailyExp;
            ret.firstPassMap = new HashMap<int, HashMap<int, string>>(this.firstPassMap);
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            dailyGold = input.GetS32();
            dailyExp = input.GetS32();
            firstPassMap = input.GetDataMap<int, HashMap<int, string>>();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(dailyGold);
            output.PutS32(dailyExp);
            output.PutDataMap(firstPassMap);
        }
    }


    [MessageType(TLConstants.BATTLE_START + 22)]
    [DescAttribute("任务数据")]
    public class TLQuestData : ICloneable, IExternalizable
    {
        /// <summary>
        /// 任务ID.
        /// </summary>
        [DescAttribute("任务ID")]
        public string TaskID = null;


        //任务类型
        public static readonly string Attribute_TaskType = "type";

        //采集类型
        public static readonly string Attribute_InterActive = "InterActive";

        //采集目标ID
        public static readonly string Attribute_InterActiveTargetId = "InterActiveTargetId";

        //任务完成目标的需求数量
        public static readonly string Attribute_InterActiveTargetNum = "InterActiveTargetNum";

        public enum TaskType : int
        {
            InterActiveItem = 13,
        }


        /// <summary>
        /// 任务状态.
        /// </summary>
        [DescAttribute("任务状态")]
        public string TaskState = null;

        public List<QuestAttribute> Attributes = null;

        //public static int GetTaskTypeByString(string typeValue)
        //{
        //    int ret = 0;
        //    int.TryParse(typeValue, out ret);
        //    return ret;
        //}



        public void AddInterActive(int targetId, int targetNum)
        {

            AddAttribute(Attribute_InterActive, targetId.ToString() + ',' + targetNum.ToString());

            //AddAttribute(Attribute_InterActiveTargetId, targetId.ToString());

            //AddAttribute(Attribute_InterActiveTargetNum, targetNum.ToString());
        }


        public void AddAttribute(string key, string value)
        {
            if (Attributes == null)
            {
                Attributes = new List<QuestAttribute>();
            }
            QuestAttribute attr = new QuestAttribute();
            attr.Key = key;
            attr.Value = value;
            Attributes.Add(attr);
        }

        public object Clone()
        {
            TLQuestData ret = new TLQuestData();
            ret.TaskID = this.TaskID;
            ret.TaskState = this.TaskState;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            this.TaskID = input.GetUTF();
            this.TaskState = input.GetUTF();
            this.Attributes = input.GetList<QuestAttribute>(input.GetExt<QuestAttribute>);
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(TaskID);
            output.PutUTF(TaskState);
            output.PutList<QuestAttribute>(this.Attributes, output.PutExt);
        }
    }

    #region TL任务数据.
    [MessageType(TLConstants.BATTLE_START + 23)]
    [ExpandableAttribute]
    public class QuestAttribute : ICloneable, IExternalizable
    {
        public string Key;
        public string Value;
        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(Key);
            output.PutUTF(Value);
        }

        public void ReadExternal(IInputStream input)
        {
            this.Key = input.GetUTF();
            this.Value = input.GetUTF();
        }

        public object Clone()
        {
            QuestAttribute ret = new QuestAttribute();
            ret.Key = this.Key;
            ret.Value = this.Value;
            return ret;
        }
    }
    #endregion

    /// <summary>
    /// 单位PK信息.
    /// </summary>
    [MessageType(TLConstants.BATTLE_START + 24)]
    [DescAttribute("PK信息")]
    public class PKInfo : ICloneable, IExternalizable
    {

        /// <summary>
        /// PK模式.决定玩家攻击判定.
        /// </summary>
        public enum PKMode : byte
        {
            Peace = 0,   //和平模式（该模式下不能攻击其他任何玩家，被攻击也不能还手）.
            Justice = 1, //善恶模式（只能攻击PK值>0的玩家）.
            Camp = 2,    //阵营模式（只能攻击和自己不同阵营的玩家）.
            Guild = 3,   //公会模式（只能攻击不是自己公会/家族的玩家）.
            Team = 4,    //组队模式(只能攻击对外意外的玩家).
            Server = 5,  //本服模式(只能攻击其他服务器的玩家。这个模式只在连服生效).
            All = 6,     //全体模式（攻击自己及队友以外的玩家等价于队友模式）.
            Group = 7,   //团队模式
            Revenger = 8,//复仇模式(只攻击自己的仇人)
        }

        //当前PK等级.
        public int CurPKLevel = 1;
        //当前PK模式.
        public PKMode CurPKMode = PKMode.Peace;
        //当前PK值.
        public int CurPKValue = 0;
        //名字颜色.
        public uint CurColor = 0;
        //灰名时间.
        public long GrayTimeMS = 0;

        public object Clone()
        {
            PKInfo ret = new PKInfo();

            ret.CurPKLevel = this.CurPKLevel;
            ret.CurPKMode = this.CurPKMode;
            ret.CurPKValue = this.CurPKValue;
            ret.CurColor = this.CurColor;
            ret.GrayTimeMS = this.GrayTimeMS;

            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            this.CurPKLevel = input.GetS32();
            this.CurPKMode = input.GetEnum8<PKMode>();
            this.CurPKValue = input.GetS32();
            this.CurColor = input.GetU32();
            this.GrayTimeMS = input.GetS64();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(CurPKLevel);
            output.PutEnum8(CurPKMode);
            output.PutS32(CurPKValue);
            output.PutU32(CurColor);
            output.PutS64(GrayTimeMS);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 25)]
    [DescAttribute("无功能BUFF.")]
    public abstract class TLBuffData : ICloneable, IExternalizable
    {
        public BattleAtkNumberEventB2C.AtkNumberType TipsType = BattleAtkNumberEventB2C.AtkNumberType.None;

        public virtual object Clone()
        {
            return null;
        }

        public virtual void ReadExternal(IInputStream input)
        {
            TipsType = input.GetEnum8<BattleAtkNumberEventB2C.AtkNumberType>();
        }

        public virtual void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(TipsType);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 26)]
    [DescAttribute("TLBuffData_AbsorbDamage - 吸收伤害.")]
    public class TLBuffData_AbsorbDamage : TLBuffData
    {
        [DescAttribute("伤害吸收总值.")]
        public int AbsorbDamageSum = 0;

        [DescAttribute("是否吸收过量伤害.")]
        public bool AbsorbOverFlowDamage = false;

        public override object Clone()
        {
            TLBuffData_AbsorbDamage ret = new TLBuffData_AbsorbDamage();
            ret.AbsorbDamageSum = this.AbsorbDamageSum;
            ret.AbsorbOverFlowDamage = this.AbsorbOverFlowDamage;
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            AbsorbDamageSum = input.GetS32();
            AbsorbOverFlowDamage = input.GetBool();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutS32(AbsorbDamageSum);
            output.PutBool(AbsorbOverFlowDamage);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 27)]
    [DescAttribute("TLBuffData_HPChange - 血量变更.")]
    public class TLBuffData_HPChange : TLBuffData
    {
        [DescAttribute("正为加血，负为减血.")]
        public int ChangeValue;
        [DescAttribute("血量改变类型，0绝对值/1万分比")]
        public byte ChangeValueType;
        public override object Clone()
        {
            TLBuffData_HPChange ret = new TLBuffData_HPChange();
            ret.ChangeValue = this.ChangeValue;
            ret.TipsType = this.TipsType;
            ret.ChangeValueType = this.ChangeValueType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            ChangeValue = input.GetS32();
            ChangeValueType = input.GetU8();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutS32(ChangeValue);
            output.PutU8(ChangeValueType);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 28)]
    [DescAttribute("TLBuffData_Slient - 沉默.")]
    public class TLBuffData_Slient : TLBuffData
    {
        public override object Clone()
        {
            TLBuffData_Slient ret = new TLBuffData_Slient();
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 29)]
    [DescAttribute("TLBuffData_ChangeProp - 属性变更.")]
    public class TLBuffData_ChangeProp : TLBuffData
    {
        [DescAttribute("变更属性类型.")]
        public TLPropObject.PropType ChangeType;
        [DescAttribute("变更值类型 - 绝对值/万分比.")]
        public TLPropObject.ValueType ValueType;
        [DescAttribute("具体变更值.")]
        public int Value;
        [DescAttribute("是否飘字.")]
        public bool showTips = true;

        public override object Clone()
        {
            TLBuffData_ChangeProp ret = new TLBuffData_ChangeProp();
            ret.ChangeType = this.ChangeType;
            ret.showTips = this.showTips;
            ret.ValueType = this.ValueType;
            ret.Value = this.Value;
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            ChangeType = input.GetEnum8<TLPropObject.PropType>();
            ValueType = input.GetEnum8<TLPropObject.ValueType>();
            Value = input.GetS32();
            showTips = input.GetBool();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(ChangeType);
            output.PutEnum8(ValueType);
            output.PutS32(Value);
            output.PutBool(showTips);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 30)]
    [DescAttribute("TLBuffData_Mocking - 嘲讽.")]
    public class TLBuffData_Mocking : TLBuffData
    {
        public override object Clone()
        {
            TLBuffData_Mocking ret = new TLBuffData_Mocking();
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
        }

    }

    [MessageType(TLConstants.BATTLE_START + 31)]
    [DescAttribute("TLBuffData_PlayerActiveSkill - 技能变更.")]
    public class TLBuffData_PlayerActiveSkill : TLBuffData
    {
        public List<GameSkill> Skills = null;
        public List<int> Keeps = null;
        public int BuffID = 0;

        public override object Clone()
        {
            TLBuffData_PlayerActiveSkill ret = new TLBuffData_PlayerActiveSkill();
            ret.Skills = new List<GameSkill>(Skills);
            ret.Keeps = new List<int>(Keeps);
            ret.BuffID = BuffID;
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            this.Skills = input.GetList<GameSkill>(input.GetExt<GameSkill>);
            this.Keeps = input.GetList<int>(input.GetS32);
            this.BuffID = input.GetS32();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutList<GameSkill>(this.Skills, output.PutExt);
            output.PutList<int>(this.Keeps, output.PutS32);
            output.PutS32(BuffID);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 32)]
    public class SkillSlot : ICloneable, IExternalizable
    {
        /// <summary>
        /// 槽位0开始.
        /// </summary>
        public byte Index;
        /// <summary>
        /// 开放等级.
        /// </summary>
        public int OpenLv;

        public object Clone()
        {
            SkillSlot slot = new SkillSlot();
            slot.Index = this.Index;
            slot.OpenLv = this.OpenLv;
            return slot;
        }

        public void ReadExternal(IInputStream input)
        {
            Index = input.GetU8();
            OpenLv = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutU8(Index);
            output.PutS32(OpenLv);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 33)]
    [DescAttribute("TLBuffData_ActiveSkill - 技能变更.")]
    public class TLBuffData_ActiveSkill : TLBuffData
    {
        public List<GameSkill> Skills = null;
        public List<int> Keeps = null;
        public int BuffID = 0;

        public override object Clone()
        {
            TLBuffData_ActiveSkill ret = new TLBuffData_ActiveSkill();
            ret.Skills = new List<GameSkill>(Skills);
            ret.Keeps = new List<int>(Keeps);
            ret.BuffID = BuffID;
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            this.Skills = input.GetList<GameSkill>(input.GetExt<GameSkill>);
            this.Keeps = input.GetList<int>(input.GetS32);
            this.BuffID = input.GetS32();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutList<GameSkill>(this.Skills, output.PutExt);
            output.PutList<int>(this.Keeps, output.PutS32);
            output.PutS32(BuffID);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 34)]
    [DescAttribute("TLBuff纯飘字.")]
    public class TLBuff_Tips : TLBuffData
    {
        public override object Clone()
        {
            TLBuff_Tips ret = new TLBuff_Tips();
            ret.TipsType = this.TipsType;
            return ret;
        }
    }

    [MessageType(TLConstants.BATTLE_START + 35)]
    [DescAttribute("TLBuf_LoopSkill-技能按指定序列施放")]
    public class TLBuffData_LoopSkill : TLBuffData
    {
        public List<TLGameSkillSnap> SkillLoopList;

        public override object Clone()
        {
            TLBuffData_LoopSkill ret = new TLBuffData_LoopSkill();
            ret.SkillLoopList = new List<TLGameSkillSnap>();
            for (int i = 0; i < SkillLoopList.Count; i++)
            {
                ret.SkillLoopList.Add((TLGameSkillSnap)SkillLoopList[i].Clone());
            }
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            this.SkillLoopList = input.GetList(input.GetExt<TLGameSkillSnap>);
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutList(this.SkillLoopList, output.PutExt);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 36)]
    [DescAttribute("TLBufData_Purge---净化(移除所有/指定的BUFF)")]
    public class TLBuffData_Purge : TLBuffData
    {
        public enum PurgeBuffType : byte
        {
            [DescAttribute("有害的")]
            Harmful = 0,
            [DescAttribute("有益的")]
            Beneficial = 1,
            [DescAttribute("所有")]
            All = 2,
        }

        [TemplatesIDAttribute(typeof(BuffTemplate))]
        public List<int> BuffList;
        public PurgeBuffType PurgeType;

        public override object Clone()
        {
            TLBuffData_Purge ret = new TLBuffData_Purge();
            ret.PurgeType = this.PurgeType;
            ret.BuffList = new List<int>();
            for (int i = 0; i < BuffList.Count; i++)
            {
                ret.BuffList.Add(BuffList[i]);
            }
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            this.BuffList = input.GetList(input.GetS32);
            this.PurgeType = input.GetEnum8<PurgeBuffType>();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutList(this.BuffList, output.PutS32);
            output.PutEnum8(PurgeType);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 37)]
    [DescAttribute("TLBufData_LockHP---锁定血量(单位血量不低于指定血量)")]
    public class TLBuffData_LockHP : TLBuffData
    {
        [DescAttribute("锁定值.")]
        public int LockHPGuideValue;
        [DescAttribute("锁定值类型 (0绝对值/1万分比).")]
        public byte GuideValueType;
        [DescAttribute("起效次数（小于0，不限次数")]
        public int UseTimes;

        public override object Clone()
        {
            TLBuffData_LockHP ret = new TLBuffData_LockHP();
            ret.LockHPGuideValue = this.LockHPGuideValue;
            ret.GuideValueType = this.GuideValueType;
            ret.UseTimes = this.UseTimes;
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            LockHPGuideValue = input.GetS32();
            GuideValueType = input.GetU8();
            UseTimes = input.GetS32();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutS32(LockHPGuideValue);
            output.PutU8(GuideValueType);
            output.PutS32(UseTimes);
            base.WriteExternal(output);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 38)]
    [DescAttribute("TLBufData_MPChange---添加怒气")]
    public class TLBuffData_MPChange : TLBuffData
    {
        [DescAttribute("锁定值类型 (0绝对值/1万分比).")]
        public byte ChangeValueType;
        [DescAttribute("增加值.")]
        public int ChangeValue;

        public override object Clone()
        {
            TLBuffData_MPChange ret = new TLBuffData_MPChange();
            ret.ChangeValueType = this.ChangeValueType;
            ret.ChangeValue = this.ChangeValue;
            ret.TipsType = this.TipsType;
            return ret;
        }

        public override void ReadExternal(IInputStream input)
        {
            ChangeValueType = input.GetU8();
            ChangeValue = input.GetS32();
            base.ReadExternal(input);
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutU8(ChangeValueType);
            output.PutS32(ChangeValue);
            base.WriteExternal(output);
        }
    }

    //[25-200]留给buff.               
    [MessageType(TLConstants.BATTLE_START + 201)]
    [DescAttribute("宠物基础信息 - 宠物.")]
    public class PetBaseInfo : ICloneable, IExternalizable
    {
        /// <summary>
        /// 名字.
        /// </summary>
        public string name = "蔡弯弯";

        /// <summary>
        /// 等级.
        /// </summary>
        public int level = 1;

        /// <summary>
        /// 品质.
        /// </summary>
        public byte QColor = 1;

        /// <summary>
        ///特效模型.
        /// <summary>
        public string EffectModel = "vfx_pet_illusion_lv5";

        /// <summary>
        ///光环特效.
        /// <summary>
        public int EffectScale = 150;

        /// <summary>
        /// 主人ID.
        /// </summary>
        public uint MasterID = 0;

        public object Clone()
        {
            PetBaseInfo ret = new PetBaseInfo();
            ret.EffectModel = this.EffectModel;
            ret.EffectScale = this.EffectScale;
            ret.level = this.level;
            ret.MasterID = this.MasterID;
            ret.name = this.name;
            ret.QColor = this.QColor;

            return ret;
        }

        public void ReadExternal(IInputStream input)
        {

            EffectModel = input.GetUTF();
            EffectScale = input.GetS32();
            level = input.GetS32();
            MasterID = input.GetU32();
            name = input.GetUTF();
            QColor = input.GetU8();

        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(EffectModel);
            output.PutS32(EffectScale);
            output.PutS32(level);
            output.PutU32(MasterID);
            output.PutUTF(name);
            output.PutU8(QColor);

        }
    }

    [MessageType(TLConstants.BATTLE_START + 202)]
    [DescAttribute("宠物数据 - 宠物.")]
    public class PetData : ICloneable, IExternalizable
    {
        public int EditorID;
        public int RebirthTimeMS;

        /// <summary>
        ///基础信息. 
        /// </summary>
        public PetBaseInfo BaseInfo = null;

        /// <summary>
        /// 能力属性. 
        /// </summary>
        public TLUnitProp UnitProp = null;

        /// <summary>
        /// 技能信息.
        /// </summary>
        public TLUnitSkillInfo SkillInfo = null;

        public object Clone()
        {
            PetData ret = new PetData();
            ret.EditorID = EditorID;
            ret.RebirthTimeMS = RebirthTimeMS;
            ret.BaseInfo = (PetBaseInfo)BaseInfo?.Clone();
            ret.UnitProp = (TLUnitProp)UnitProp?.Clone();
            ret.SkillInfo = (TLUnitSkillInfo)SkillInfo?.Clone();

            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            EditorID = input.GetS32();
            RebirthTimeMS = input.GetS32();
            BaseInfo = input.GetExt<PetBaseInfo>();
            UnitProp = input.GetExt<TLUnitProp>();
            SkillInfo = input.GetExt<TLUnitSkillInfo>();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(EditorID);
            output.PutS32(RebirthTimeMS);
            output.PutExt(BaseInfo);
            output.PutExt(UnitProp);
            output.PutExt(SkillInfo);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 203)]
    [DescAttribute("伤害分割 - 战斗技能扩展.")]
    public class DamageSplitData : IExternalizable, ICloneable
    {
        /// <summary>
        /// 分割频率.
        /// </summary>
        [DescAttribute("分割频率", "扩展")]
        [ListAttribute(typeof(int))]
        public List<int> SplitFrameMSList = new List<int>();
        public LaunchEffect hitEffect = null;


        public object Clone()
        {
            DamageSplitData ret = new DamageSplitData();
            ret.SplitFrameMSList = new List<int>(SplitFrameMSList);
            ret.hitEffect = (LaunchEffect)this.hitEffect.Clone();
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            this.SplitFrameMSList = input.GetList<int>(input.GetS32);
            this.hitEffect = input.GetExt<LaunchEffect>();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutList<int>(this.SplitFrameMSList, output.PutS32);
            output.PutExt(this.hitEffect);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 204)]
    [DescAttribute("仙侣 - 战斗技能扩展.")]
    public class GodData : ICloneable, IExternalizable
    {
        /// <summary>
        /// 仙侣技能信息.
        /// </summary>
        public HashMap<int, GameSkill> SkillInfo;

        public int BuffID;

        public object Clone()
        {
            GodData ret = new GodData();
            ret.SkillInfo = new HashMap<int, GameSkill>(SkillInfo);
            ret.BuffID = BuffID;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            SkillInfo = input.GetDataMap<int, GameSkill>();
            BuffID = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutDataMap(SkillInfo);
            output.PutS32(BuffID);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 205)]
    public class BuffSnap : ICloneable, IExternalizable
    {
        public int BuffID;
        public int PassTimeMS;
        public int TotalTimeMS;
        public byte OverlayLevel;
        public List<TLBuffData> BuffDataLt;

        public object Clone()
        {
            BuffSnap ret = new BuffSnap();
            ret.BuffID = this.BuffID;
            ret.PassTimeMS = this.PassTimeMS;
            ret.BuffDataLt = new List<TLBuffData>(this.BuffDataLt);
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            BuffID = input.GetS32();
            TotalTimeMS = input.GetS32();
            PassTimeMS = input.GetS32();
            OverlayLevel = input.GetU8();
            BuffDataLt = input.GetList(input.GetExt<TLBuffData>);
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(BuffID);
            output.PutS32(TotalTimeMS);
            output.PutS32(PassTimeMS);
            output.PutU8(OverlayLevel);
            output.PutList(this.BuffDataLt, output.PutExt);
        }
    }

    /// <summary>
    /// 场景信息.
    /// </summary>
    [MessageType(TLConstants.BATTLE_START + 206)]
    public class BattleSceneData : ICloneable, IExternalizable
    {
        public int MapID;

        public object Clone()
        {
            var ret = new BattleSceneData();
            ret.MapID = this.MapID;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            MapID = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(MapID);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 207)]
    public class TeamData : ICloneable, IExternalizable
    {
        public List<TeamMemberSnap> MemberList;
        public string TeamLeaderUUID;
        public string TeamUUID;

        public object Clone()
        {
            TeamData ret = new TeamData();
            ret.TeamLeaderUUID = TeamLeaderUUID;
            ret.TeamUUID = TeamUUID;

            if (MemberList != null)
                ret.MemberList = new List<TeamMemberSnap>(MemberList);

            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            TeamLeaderUUID = input.GetUTF();
            MemberList = input.GetList<TeamMemberSnap>(input.GetExt<TeamMemberSnap>);
            TeamUUID = input.GetUTF();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(TeamLeaderUUID);
            output.PutList<TeamMemberSnap>(MemberList, output.PutExt);
            output.PutUTF(TeamUUID);
        }
    }

    [MessageType(TLConstants.BATTLE_START + 208)]
    public class TeamMemberSnap : ICloneable, IExternalizable
    {
        public string UUID;
        public byte Gender;
        public byte Pro;
        public int Lv;

        public object Clone()
        {
            var ret = new TeamMemberSnap();
            ret.UUID = this.UUID;
            ret.Gender = this.Gender;
            ret.Pro = this.Pro;
            ret.Lv = this.Lv;
            return ret;
        }

        public void ReadExternal(IInputStream input)
        {
            UUID = input.GetUTF();
            Gender = input.GetU8();
            Pro = input.GetU8();
            Lv = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(UUID);
            output.PutU8(Gender);
            output.PutU8(Pro);
            output.PutS32(Lv);
        }
    }
}