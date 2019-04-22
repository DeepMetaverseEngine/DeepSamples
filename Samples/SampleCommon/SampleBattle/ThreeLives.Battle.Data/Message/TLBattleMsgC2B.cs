using DeepCore;
using DeepCore.GameData.RTS;
using DeepCore.GameData.Zone;
using DeepCore.GameData.ZoneClient;
using DeepCore.IO;
using DeepCore.Reflection;
using System.Collections.Generic;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;

namespace TLBattle.Message
{
    /// <summary>
    /// 用于编写战斗C2B事件.
    /// </summary>

    //剧情完成
    [MessageType(TLConstants.BATTLE_MSG_C2B_START + 1)]
    public class FinishStoryC2B : ObjectAction
    {
        public string fileName;
        public FinishStoryC2B() { }
        public FinishStoryC2B(uint unit_id, string _fileName) : base(unit_id) { this.fileName = _fileName; }

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(this.fileName);
        }

        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            this.fileName = input.GetUTF();
        }
    }


    [MessageType(TLConstants.BATTLE_MSG_C2B_START + 2)]
    public class FindTargetUnitRequest : ActorRequest
    {
        public int TemplateId;
        public bool IgnoreAoi = false;
        public FindTargetUnitRequest() { }
        public FindTargetUnitRequest(uint unit_id, int _TemplateId) : base(unit_id) { this.TemplateId = _TemplateId; }

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(this.TemplateId);
        }

        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            this.TemplateId = input.GetS32();
        }
    }
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 13)]
    public class FindTargetUnitResponse : ActorResponse
    {
        public uint TargetObjectID = 0;
        public float TargetX = 0;
        public float TargetY = 0;
        public int TemplateID = 0;
        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutU32(TargetObjectID);
            output.PutF32(TargetX);
            output.PutF32(TargetY);
            output.PutS32(TemplateID);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            TargetObjectID = input.GetU32();
            TargetX = input.GetF32();
            TargetY = input.GetF32();
            TemplateID = input.GetS32();
        }
    }



    [MessageType(TLConstants.BATTLE_MSG_C2B_START + 50)]
    public class FindTargetItemRequest : ActorRequest
    {
        public int TemplateId;
        public FindTargetItemRequest() { }
        public FindTargetItemRequest(uint unit_id, int _TemplateId) : base(unit_id) { this.TemplateId = _TemplateId; }

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(this.TemplateId);
        }

        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            this.TemplateId = input.GetS32();
        }
    }
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 51)]
    public class FindTargetItemResponse : ActorResponse
    {
        public uint TargetObjectID = 0;
        public float TargetX = 0;
        public float TargetY = 0;
        public int TemplateID = 0;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutU32(TargetObjectID);
            output.PutF32(TargetX);
            output.PutF32(TargetY);
            output.PutS32(TemplateID);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            TargetObjectID = input.GetU32();
            TargetX = input.GetF32();
            TargetY = input.GetF32();
            TemplateID = input.GetS32();
        }
    }

    /// <summary>
    /// 获取场景内玩家UUID.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_C2B_START + 3)]
    public class GetZonePlayersUUIDRequest : ActorRequest
    {

    }

    /// <summary>
    /// 当前场景玩家UUID人数.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 24)]
    public class GetZonePlayersUUIDResponse : ActorResponse
    {
        public List<string> b2c_list = null;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(b2c_list, output.PutUTF);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            b2c_list = input.GetUTFList();
        }
    }

    /// <summary>
    /// 客户端获取加速BUFF请求.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_C2B_START + 4)]
    public class ActorAddSpeedUpBuffRequest : ActorRequest
    {

    }

    /// <summary>
    /// 客户端获取加速BUFF请求.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_C2B_START + 5)]
    public class ActorRemoveSpeedUpBuffRequest : ActorRequest
    {

    }
    //----------------------------------------------------------------------------------
    /// 用于编写战斗B2C事件.
    //----------------------------------------------------------------------------------

    /// <summary>
    /// 战斗状态改变事件.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 1)]
    public class CombatStateChangeEventB2C : ObjectEvent
    {
        public enum BattleStatus : byte
        {
            None = 0,
            PVE = 1,
            PVP = 2,
        }

        private BattleStatus mCurState = BattleStatus.None;
        public BattleStatus CurState { get { return mCurState; } }
        public CombatStateChangeEventB2C() { }
        public CombatStateChangeEventB2C(uint unit_id, BattleStatus state) : base(unit_id) { mCurState = state; }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(mCurState);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            mCurState = input.GetEnum8<BattleStatus>();
        }

    }

    /// <summary>
    /// 客户端玩家单位显示数据.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 2)]
    public class PlayerVisibleDataB2C : IExternalizable, IUnitVisibleData
    {
        /// <summary>
        /// 单位基础信息.
        /// </summary>
        public TLUnitBaseInfo BaseInfo = null;

        /// <summary>
        ///装备时装信息.
        /// </summary>
        public HashMap<int, TLAvatarInfo> AvatarMap = new HashMap<int, TLAvatarInfo>();

        /// <summary>
        /// PK信息.
        /// </summary>
        public PKInfo UnitPKInfo = null;


        public void ReadExternal(IInputStream input)
        {
            this.BaseInfo = input.GetExt<TLUnitBaseInfo>();
            this.AvatarMap = input.GetDataMap<int, TLAvatarInfo>();
            this.UnitPKInfo = input.GetExt<PKInfo>();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutExt(BaseInfo);
            output.PutDataMap(this.AvatarMap);
            output.PutExt(UnitPKInfo);
        }
    }

    /// <summary>
    /// 客户端怪物单位显示数据.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 3)]
    public class MonsterVisibleDataB2C : IExternalizable, IUnitVisibleData
    {
        /// <summary>
        /// 攻击倾向.
        /// </summary>
        public enum AtkTendency : byte
        {
            Passive,
            Active,
        }

        /// <summary>
        /// 怪物类型.
        /// </summary>
        public enum MonsterType : byte
        {
            Normal = 1,             //普通.
            Elite = 2,             //精英.
            Boss = 3,             //BOSS.
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name;

        /// <summary>
        /// 等级.
        /// </summary>
        public int Lv;

        /// <summary>
        /// 倾向.
        /// </summary>
        public AtkTendency Tendency;

        /// <summary>
        /// 怪物类型.
        /// </summary>
        public MonsterType Type;

        /// <summary>
        /// 所有者UUID.
        /// </summary>
        public string OwnerShipUUID;
        /// <summary>
        /// 是否有归属权逻辑.
        /// </summary>
        public bool ShowOwnerShip;
        /// <summary>
        /// 称号.
        /// </summary>
        public int TitleID;

        public void ReadExternal(IInputStream input)
        {
            Name = input.GetUTF();
            Lv = input.GetS32();
            Tendency = input.GetEnum8<AtkTendency>();
            Type = input.GetEnum8<MonsterType>();
            OwnerShipUUID = input.GetUTF();
            ShowOwnerShip = input.GetBool();
            TitleID = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(Name);
            output.PutS32(Lv);
            output.PutEnum8(Tendency);
            output.PutEnum8(Type);
            output.PutUTF(OwnerShipUUID);
            output.PutBool(ShowOwnerShip);
            output.PutS32(TitleID);
        }
    }

    /// <summary>
    /// 向客户端发送文字提示.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 4)]
    public class ShowTipsEventB2C : PlayerEvent
    {
        public string Msg = null;
        public List<string> Params = null;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(Msg);
            output.PutList<string>(Params, output.PutUTF);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            Msg = input.GetUTF();
            Params = input.GetUTFList();
        }
    }

    /// <summary>
    /// 单位属性列表.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 5)]
    public class PlayerBattlePropChangeEventB2C : PlayerEvent
    {
        //TLConstants.BATTLE_MSG_B2C_START + 6
        public List<TLPropObject> PropList = new List<TLPropObject>();

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(PropList, output.PutExt);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            PropList = input.GetList(input.GetExt<TLPropObject>);
        }


    }

    /// <summary>
    /// 战斗伤害数字.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 7)]
    public class BattleAtkNumberEventB2C : ObjectEvent
    {
        public enum AtkNumberType : byte
        {
            Normal = 0,         //普通攻击.
            Crit = 1,           //暴击.
            Dodge = 2,          //闪避.
            Immunity = 3,       //免疫.
            [Desc("反伤")]
            IronMaiden = 4,     //反伤.
            [Desc("吸收")]
            Absorb = 5,         //吸收.
            [Desc("中毒")]
            Poisoning = 6,      //中毒.
            [Desc("灼烧")]
            Burn = 7,           //灼烧.
            [Desc("减速")]
            SpeedDown = 8,      //减速.
            [Desc("攻击提升")]
            AttackUp = 9,       //攻击提升.
            None = 10,          //无.
            [Desc("格挡")]
            Block = 11,         //格挡.
            [Desc("晕眩")]
            Stun = 12,          //晕眩.        
            [Desc("定身")]
            Hold = 13,          //定身.
            [Desc("恐惧")]
            Fear = 14,          //恐惧.
            [Desc("沉默")]
            Slient = 15,        //沉默.
            [Desc("流血")]
            Bleed = 16          //流血.
        }

        public int Value;
        public AtkNumberType Type;
        public uint VisibleUnit;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(Value);
            output.PutEnum8(Type);
            output.PutU32(VisibleUnit);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            Value = input.GetS32();
            Type = input.GetEnum8<AtkNumberType>();
            VisibleUnit = input.GetU32();
        }
    }

    /// <summary>
    /// 单位基础信息变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 8)]
    public class PlayerBaseInfoChangeEventB2C : ObjectEvent
    {
        public TLUnitBaseInfo info;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(info);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            info = input.GetExt<TLUnitBaseInfo>();
        }
    }

    /// <summary>
    /// 触发事件通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 9)]
    public class PlayerTriggerEventB2C : ObjectEvent
    {
        /// <summary>
        /// 事件ID.
        /// </summary>
        public string EventID = null;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(EventID);
        }

        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            EventID = input.GetUTF();
        }

    }

    // +10已经被占用
    //[MessageType(TLConstants.BATTLE_MSG_B2C_START + 10)]


    /// <summary>
    /// 玩家外观变更消息
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 11)]
    public class PlayAvatarEventB2C : ObjectEvent
    {
        public HashMap<int, TLAvatarInfo> AvatarMap = null;

        public PlayAvatarEventB2C()
        {
        }

        public PlayAvatarEventB2C(HashMap<int, TLAvatarInfo> AvatarMap)
        {
            this.AvatarMap = AvatarMap;
        }

        public PlayAvatarEventB2C(uint Id, HashMap<int, TLAvatarInfo> AvatarMap) : base(Id)
        {
            this.AvatarMap = AvatarMap;
        }

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutDataMap(this.AvatarMap);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            this.AvatarMap = input.GetDataMap<int, TLAvatarInfo>();
        }
    }

    /// <summary>
    /// 技能信息变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 12)]
    public class PlayerSkillInfoEventB2C : PlayerEvent
    {
        public List<GameSkill> SkillList = null;
        public int BuffID;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList<GameSkill>(SkillList, output.PutExt);
            output.PutS32(BuffID);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            SkillList = input.GetList(input.GetExt<GameSkill>);
            BuffID = input.GetS32();
        }
    }

    /// <summary>
    /// PK模式变更通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 14)]
    public class PKModeChangeEventB2C : PlayerEvent
    {
        public PKInfo.PKMode mode;
        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(mode);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            mode = input.GetEnum8<PKInfo.PKMode>();
        }
    }

    /// <summary>
    /// 红名信息变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 15)]
    public class PKInfoChangeEventB2C : ObjectEvent
    {
        public int b2c_level;
        public uint b2c_color;
        public int b2c_pkvalue;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(b2c_level);
            output.PutU32(b2c_color);
            output.PutS32(b2c_pkvalue);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            b2c_level = input.GetS32();
            b2c_color = input.GetU32();
            b2c_pkvalue = input.GetS32();
        }
    }

    /// <summary>
    /// 自动战斗状态变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 16)]
    public class GuardStatusChangeEventB2C : ObjectEvent
    {
        public bool b2c_guard;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutBool(b2c_guard);

        }

        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            b2c_guard = input.GetBool();

        }
    }
    /// <summary>
    /// 物品掉落 客户端假掉落
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 17)]
    public class AddClientItemsEventB2C : ObjectEvent
    {
        public List<int> templdateIds;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(templdateIds, output.PutS32);

        }

        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            templdateIds = input.GetList(input.GetS32);

        }
    }

    /// <summary>
    /// 宠物客户端同步数据.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 18)]
    public class PetVisibleDataB2C : IExternalizable, IUnitVisibleData
    {
        /// <summary>
        /// 单位基础信息.
        /// </summary>
        public PetBaseInfo BaseInfo = new PetBaseInfo();
        public void WriteExternal(IOutputStream output)
        {
            output.PutExt(this.BaseInfo);
        }

        public void ReadExternal(IInputStream input)
        {
            this.BaseInfo = input.GetExt<PetBaseInfo>();
        }
    }

    /// <summary>
    /// 宠物基础信息变更事件.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 19)]
    public class PetBaseInfoChangeEventB2C : ObjectEvent
    {
        public PetBaseInfo BaseInfo;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(BaseInfo);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            BaseInfo = input.GetExt<PetBaseInfo>();
        }
    }

    /// <summary>
    /// 战斗伤害数字分割表现.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 20)]
    public class BattleSplitHitEventB2C : ObjectEvent
    {
        public uint sendID;
        public List<SplitHitInfo> hitInfo = null;
        public int TotalDamage = 0;
        public LaunchEffect effect = null;
        private uint effect_sn = 0;

        public override void BeforeWrite(TemplateManager templates)
        {
            if (effect != null)
            {
                this.effect_sn = effect.SerialNumber;
            }
        }
        public override void EndRead(TemplateManager templates)
        {
            this.effect = templates.GetSnData<LaunchEffect>(effect_sn);
        }

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutU32(sendID);
            output.PutList<SplitHitInfo>(hitInfo, output.PutExt);
            output.PutS32(TotalDamage);
            output.PutVU32(effect_sn);

        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            sendID = input.GetU32();
            hitInfo = input.GetList(input.GetExt<SplitHitInfo>);
            TotalDamage = input.GetS32();
            this.effect_sn = input.GetVU32();

        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 21)]
    public class SplitHitInfo : IExternalizable
    {
        /// <summary>
        /// 攻击结果.
        /// </summary>
        public byte hitType;

        /// <summary>
        /// 伤害.
        /// </summary>
        public int Damage;

        /// <summary>
        /// 关键帧.
        /// </summary>
        public int FrameMS;

        public void ReadExternal(IInputStream input)
        {
            hitType = input.GetU8();
            Damage = input.GetS32();
            FrameMS = input.GetS32();
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutU8(hitType);
            output.PutS32(Damage);
            output.PutS32(FrameMS);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 22)]
    public class TeamMemberListChangeEvtB2C : ObjectEvent
    {
        public List<TeamMemberSnap> list = null;
        public string teamLeaderUUID = null;
        public string teamUUID = null;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(list, output.PutExt);
            output.PutUTF(teamLeaderUUID);
            output.PutUTF(teamUUID);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            list = input.GetList(input.GetExt<TeamMemberSnap>);
            teamLeaderUUID = input.GetUTF();
            teamUUID = input.GetUTF();
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 23)]
    public class RevengeListChangeEvtB2C : PlayerEvent
    {
        public List<string> list = null;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(list, output.PutUTF);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            list = input.GetUTFList();
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 25)]
    public class ForceChangeEventB2C : ObjectEvent
    {
        public byte Force;
        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutU8(Force);
        }
        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            Force = input.GetU8();
        }
    }

    /// <summary>
    /// 红名杀戮值变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 26)]
    public class PKValueChangeEventB2C : PlayerEvent
    {
        public int b2c_pkvalue;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(b2c_pkvalue);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            b2c_pkvalue = input.GetS32();
        }
    }

    /// <summary>
    /// 自动变更PK模式.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 27)]
    public class AutoChangePKModeEventB2C : PlayerEvent
    {
        public PKInfo.PKMode s2c_mode;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(s2c_mode);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            s2c_mode = input.GetEnum8<PKInfo.PKMode>();
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 28)]
    public class UnitDynmicEffectB2C : ObjectEvent
    {
        public LaunchEffect Effect;

        public UnitDynmicEffectB2C() { }
        public UnitDynmicEffectB2C(uint unitID, LaunchEffect effect)
            : base(unitID)
        {
            this.Effect = effect;
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(Effect);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            Effect = input.GetExt<LaunchEffect>();
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 29)]
    public class AddDynamicEffectB2C : ZoneEvent
    {
        public LaunchEffect Effect;
        public uint SenderID;
        public float X;
        public float Y;
        public float Direction;
        public bool IsHalf;

        public AddDynamicEffectB2C() { }
        public AddDynamicEffectB2C(uint senderID, bool isHalf, float x, float y, float dir, LaunchEffect effect)
        {
            this.SenderID = senderID;
            this.IsHalf = isHalf;
            this.X = x;
            this.Y = y;
            this.Direction = dir;
            this.Effect = effect;
        }

        public override void ReadExternal(IInputStream input)
        {
            this.SenderID = input.GetVU32();
            this.IsHalf = input.GetBool();
            MoveHelper.ReadPosAndDirection(IsHalf, out X, out Y, out Direction, input);
            Effect = input.GetExt<LaunchEffect>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutVU32(SenderID);
            output.PutBool(IsHalf);
            MoveHelper.WritePosAndDirection(IsHalf, X, Y, Direction, output);
            output.PutExt(Effect);
        }
    }

    /// <summary>
    /// 自动变更PK模式.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 30)]
    public class RadarEventB2C : PlayerEvent
    {
        public float X;
        public float Y;
        public float Distance;

        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutF32(X);
            output.PutF32(Y);
            output.PutF32(Distance);
        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            X = input.GetF32();
            Y = input.GetF32();
            Distance = input.GetF32();
        }
    }

    /// <summary>
    /// 怪物所有权变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 31)]
    public class MonsterOwnerShipChangeEventB2C : ObjectEvent
    {
        public string s2c_uuid;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(s2c_uuid);
        }
        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            s2c_uuid = input.GetUTF();
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 32)]
    public class PlayerPropChangeB2C : ObjectEvent
    {
        public const string PracticeLv = "PracticeLv";

        public const string TitleID = "TitleID";

        public const string TitleNameExt = "TitleNameExt";

        public HashMap<string, int> changes;

        public HashMap<string, string> changeExts;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            changes = input.GetMap<string, int>(input.GetUTF, input.GetS32);
            changeExts = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutMap<string, int>(changes, output.PutUTF, output.PutS32);
            output.PutMap<string, string>(changeExts, output.PutUTF, output.PutUTF);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 33)]
    public class PlayerGuildChaseListChangeB2C : PlayerEvent
    {
        public List<string> s2c_list;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            s2c_list = input.GetList(input.GetUTF);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(s2c_list, output.PutUTF);
        }
    }
}
