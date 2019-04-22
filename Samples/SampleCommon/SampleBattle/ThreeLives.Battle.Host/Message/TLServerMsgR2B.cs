using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.IO;
using System.Collections.Generic;
using TLBattle.Common.Plugins;

namespace TLBattle.Server.Message
{
    /// <summary>
    /// 背包尺寸变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 1)]
    public class InventorySizeChangeEventR2B : PlayerEvent
    {
        /// <summary>
        /// 当前背包格子数.
        /// </summary>
        public uint curInventorySize;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            curInventorySize = input.GetU32();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutU32(curInventorySize);
        }
    }


    /// <summary>
    /// 接任务.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 2)]
    public class QuestAcceptedR2B : PlayerEvent
    {

        public string playerUUID;
        public string questID;


        public struct KeyValue : IExternalizable
        {
            public string Key;
            public string Value;

            public void ReadExternal(IInputStream input)
            {
                Key = input.GetUTF();
                Value = input.GetUTF();
            }

            public void WriteExternal(IOutputStream output)
            {
                output.PutUTF(Key);
                output.PutUTF(Value);
            }
        }


        public List<KeyValue> status;


        public void AddInterActive(int targetId, int targetNum)
        {

            AddAttribute(TLQuestData.Attribute_InterActive, targetId.ToString() + ',' + targetNum.ToString());

            //AddAttribute(TLQuestData.Attribute_InterActiveTargetId, targetId.ToString());

            //AddAttribute(TLQuestData.Attribute_InterActiveTargetNum, targetNum.ToString());
        }

        public void AddAttribute(string key, string value)
        {
            if (status == null)
            {
                status = new List<KeyValue>();
            }
            KeyValue attr = new KeyValue();
            attr.Key = key;
            attr.Value = value;
            status.Add(attr);
        }


        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerUUID = input.GetUTF();
            questID = input.GetUTF();
            status = input.GetStructListNoHead<KeyValue>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerUUID);
            output.PutUTF(questID);
            output.PutStructListNoHead<KeyValue>(status);
        }
    }

    /// <summary>
    /// 放弃任务.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 3)]
    public class QuestDroppedR2B : PlayerEvent
    {
        public string playerUUID;
        public string questID;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerUUID = input.GetUTF();
            questID = input.GetUTF();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerUUID);
            output.PutUTF(questID);
        }
    }

    /// <summary>
    /// 提交.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 4)]
    public class QuestCommittedR2B : PlayerEvent
    {
        public string playerUUID;
        public string questID;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerUUID = input.GetUTF();
            questID = input.GetUTF();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerUUID);
            output.PutUTF(questID);
        }
    }


    /// <summary>
    /// 坐骑召唤
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 5)]
    public class MountSummonedR2B : PlayerEvent
    {
        public string playerUUID
        {
            get; set;
        }

        public bool IsSummonMount = false;
        public bool IsRideByUser = false;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerUUID = input.GetUTF();
            IsSummonMount = input.GetBool();
            IsRideByUser = input.GetBool();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerUUID);
            output.PutBool(IsSummonMount);
            output.PutBool(IsRideByUser);
        }
    }

    /// <summary>
    /// avatar变更
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 6)]
    public class AvatarChangedR2B : PlayerEvent
    {
        public string playerUUID
        {
            get; set;
        }

        public HashMap<int, TLAvatarInfo> AvatarMap
        {
            get; set;
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerUUID = input.GetUTF();
            this.AvatarMap = input.GetDataMap<int, TLAvatarInfo>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerUUID);
            output.PutDataMap(this.AvatarMap);
        }
    }

    /// <summary>
    /// 技能变更通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 7)]
    public class SkillChangeEventR2B : PlayerEvent
    {
        public enum Operate : byte
        {
            Reset = 0,     //重置所有.
            Replace = 1,   //替换(找到指定ID然后替换).
            Add = 2,       //新增.
            Remove = 3,    //删除.
        }

        public Operate operateType = Operate.Reset;
        public List<GameSkill> SkillList = null;
        override public void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);

            output.PutEnum8(operateType);
            output.PutList<GameSkill>(SkillList, output.PutExt);

        }
        override public void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);

            operateType = input.GetEnum8<Operate>();
            SkillList = input.GetList<GameSkill>(input.GetExt<GameSkill>);
        }
    }

    /// <summary>
    /// 队伍信息变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 8)]
    public class TeamInfoChangeEventR2B : PlayerEvent
    {
        public List<TeamMemberSnap> TeamList = null;
        public string TeamLeader = null;
        public string TeamUUID = null;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList<TeamMemberSnap>(TeamList, output.PutExt);
            output.PutUTF(TeamLeader);
            output.PutUTF(TeamUUID);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            TeamList = input.GetList<TeamMemberSnap>(input.GetExt<TeamMemberSnap>);
            TeamLeader = input.GetUTF();
            TeamUUID = input.GetUTF();
        }
    }

    /// <summary>
    ///PK模式变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 9)]
    public class PKModeChangeEventR2B : PlayerEvent
    {
        public PKInfo.PKMode mode = PKInfo.PKMode.Peace;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(mode);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            mode = input.GetEnum8<PKInfo.PKMode>();
        }
    }

    /// <summary>
    /// PK值变更.
    /// </summary>  
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 10)]
    public class PKValueChangeEventR2B : PlayerEvent
    {
        public int changeValue;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(changeValue);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            changeValue = input.GetS32();
        }
    }

    /// <summary>
    /// 复活通知.
    /// </summary>  
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 11)]
    public class RebirthEventR2B : PlayerEvent
    {
        public enum RebirthType : byte
        {
            None = 0,
            RebirthPoint = 1,    //复活点.
            Insitu = 2,          //原地复活.
            StartRegion = 3,     //出生点.
            TransMainCity = 4,   //传送至主城.
        }

        public RebirthType type;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            type = input.GetEnum8<RebirthType>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(type);
        }

    }

    /// <summary>
    ///仙侣变更通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 12)]
    public class PetChangeEventR2B : PlayerEvent
    {
        public PetData data;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            data = input.GetExt<PetData>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(data);
        }

    }

    /// <summary>
    /// 仙侣属性变更通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 13)]
    public class PetPropChangeEventR2B : PlayerEvent
    {
        public TLUnitProp data;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            data = input.GetExt<TLUnitProp>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(data);
        }
    }

    /// <summary>
    /// 仙侣基础信息变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 14)]
    public class PetBaseInfoChangeEventR2B : PlayerEvent
    {
        public PetBaseInfo data;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            data = input.GetExt<PetBaseInfo>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(data);
        }
    }

    /// <summary>
    /// 仙侣等级变更通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 15)]
    public class PetLvChangeEventR2B : PlayerEvent
    {
        public PetBaseInfo baseInfo;
        public TLUnitProp battleProp;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            baseInfo = input.GetExt<PetBaseInfo>();
            battleProp = input.GetExt<TLUnitProp>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(baseInfo);
            output.PutExt(battleProp);
        }
    }

    /// <summary>
    /// 仙侣技能变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 16)]
    public class GodSkillChangeEventR2B : PlayerEvent
    {
        /// <summary>
        /// 技能数据.
        /// </summary>
        public HashMap<int, GameSkill> info;

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutMap<int, GameSkill>(info, output.PutS32, output.PutExt);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            info = input.GetMap<int, GameSkill>(input.GetS32, input.GetExt<GameSkill>);
        }
    }



    /// <summary>
    /// 出战仙侣变更.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 17)]
    public class GodChangeEventR2B : PlayerEvent
    {
        public GodData god;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            god = input.GetExt<GodData>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(god);
        }
    }

    /// <summary>
    /// 坐骑变更
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 18)]
    public class MountChangedR2B : PlayerEvent
    {

        public string playerUUID
        {
            get; set;
        }

        public int mountSpeed
        {
            get; set;
        }

        public TLAvatarInfo mountAvatar;


        public HashMap<int, TLAvatarInfo> AvatarMap
        {
            get; set;
        }


        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerUUID = input.GetUTF();
            mountSpeed = input.GetS32();
            this.AvatarMap = input.GetDataMap<int, TLAvatarInfo>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerUUID);
            output.PutS32(mountSpeed);
            output.PutDataMap(this.AvatarMap);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 19)]
    public class RevengeListChangeEventR2B : PlayerEvent
    {
        public enum OpType : byte
        {
            Add,
            Remove,
            Reset
        }
        public OpType op;
        public List<string> list = null;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            op = input.GetEnum8<OpType>();
            list = input.GetUTFList();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(op);
            output.PutUTFList(list);
        }
    }

    /// <summary>
    /// 传送通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 20)]
    public class StartTPEventR2B : PlayerEvent
    {
        public string ZoneUUID;
        public int NextSceneID;
        public string NextScenePosition;
        public float x = -1;
        public float y = -1;
        public bool force;
        public string guildUUID;
        public HashMap<string, string> ext;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            ZoneUUID = input.GetUTF();
            NextSceneID = input.GetS32();
            NextScenePosition = input.GetUTF();
            x = input.GetF32();
            y = input.GetF32();
            force = input.GetBool();
            guildUUID = input.GetUTF();
            ext = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(ZoneUUID);
            output.PutS32(NextSceneID);
            output.PutUTF(NextScenePosition);
            output.PutF32(x);
            output.PutF32(y);
            output.PutBool(force);
            output.PutUTF(guildUUID);
            output.PutMap(ext, output.PutUTF, output.PutUTF);
        }
    }

    /// <summary>
    /// 传送通知.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 21)]
    public class TriggerMedicineEffectR2B : PlayerEvent
    {
        /// <summary>
        /// 药物类型.
        /// </summary>
        public enum MedicineType : byte
        {
            HP = 1,
            Atk = 2,
            Def = 3,
            Hot = 4,//持续性回血.
        }

        /// <summary>
        /// 类型.
        /// </summary>
        public MedicineType medicineType;
        /// <summary>
        /// HP:绝对值/万分比
        /// </summary>
        public int arg1;
        /// <summary>
        /// HP:值.
        /// </summary>
        public int arg2;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            medicineType = input.GetEnum8<MedicineType>();
            arg1 = input.GetS32();
            arg2 = input.GetS32();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutEnum8(medicineType);
            output.PutS32(arg1);
            output.PutS32(arg2);
        }
    }

    /// <summary>
    /// 脱离卡死.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 22)]
    public class EscapeUnmoveableMapR2B : PlayerEvent
    {

    }

    /// <summary>
    /// 称号改变
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 23)]
    public class TitleChangedR2B : PlayerEvent
    {
        public int TitleID = 0;
        public string TitleNameExt;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            this.TitleID = input.GetS32();
            this.TitleNameExt = input.GetUTF();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(TitleID);
            output.PutUTF(TitleNameExt);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 24)]
    public class PlayerPropChangeR2B : PlayerEvent
    {
        public const string PracticeLv = "PracticeLv";

        public const string TitleID = "TitleID";

        public HashMap<string, int> changes;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            changes = input.GetMap<string, int>(input.GetUTF, input.GetS32);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutMap<string, int>(changes, output.PutUTF, output.PutS32);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 25)]
    public class PlayerAddBuffEvtR2B : PlayerEvent
    {
        public int r2b_buffID;
        public TLBuffData r2b_buffData;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            r2b_buffID = input.GetS32();
            r2b_buffData = input.GetExt<TLBuffData>();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(r2b_buffID);
            output.PutExt(r2b_buffData);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 26)]
    public class PlayerMeridiansChangeEvtR2B : PlayerEvent
    {
        public HashMap<int, int> r2b_datas;
        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            r2b_datas = input.GetMap(input.GetS32, input.GetS32);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutMap(r2b_datas, output.PutS32, output.PutS32);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_R2B_START + 27)]
    public class PlayerGuildChaseListChangeR2B : PlayerEvent
    {
        public List<string> r2b_list;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            r2b_list = input.GetList(input.GetUTF);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutList(r2b_list, output.PutUTF);
        }
    }
}