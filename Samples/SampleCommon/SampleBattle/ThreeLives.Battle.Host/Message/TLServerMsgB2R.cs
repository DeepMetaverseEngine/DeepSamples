using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.IO;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using static TLBattle.Server.Plugins.Virtual.TLVirtual;

namespace TLBattle.Server.Message
{
    /// <summary>
    /// 战斗服向游戏服发送的协议.
    /// </summary>
    public interface IB2RMessage { };


    //任务状态变更协议 
    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 1)]
    public class QuestStatusChangedNotify : PlayerEvent, IB2RMessage
    {
        public string questID;
        public string Key;
        public string value;

        public override void ReadExternal(IInputStream input)
        {
            this.questID = input.GetUTF();
            this.Key = input.GetUTF();
            this.value = input.GetUTF();
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutUTF(questID);
            output.PutUTF(Key);
            output.PutUTF(value);
        }
    }


    /// <summary>
    /// 坐骑状态变更 
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 2)]
    public class SummonMountEventB2R : PlayerEvent, IB2RMessage
    {
        /// <summary>
        /// 单位UUID.
        /// </summary>
        public string playerId
        {
            get; set;
        }

        public bool IsSummonMount = false;
        public bool IsRideByUser = false;
        public string ReasonKey = null;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            playerId = input.GetUTF();
            IsSummonMount = input.GetBool();
            IsRideByUser = input.GetBool();
            ReasonKey = input.GetUTF();
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(playerId);
            output.PutBool(IsSummonMount);
            output.PutBool(IsRideByUser);
            output.PutUTF(ReasonKey);
        }

    }

    /// <summary>
    /// 切换场景时保存角色信息.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 3)]
    public class SaveBattleRoleInfoNotify : PlayerEvent, IB2RMessage
    {
        //PK模式相关信息.
        public PKInfo pkInfo;
        //当前血量.
        public int curHP;
        //当前怒气.
        public int curAnger;
        //当前技能CD信息.
        public TLSkillStatusData skillInfo;
        //BUFF信息
        public List<BuffSnap> buffInfo;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            pkInfo = input.GetExt<PKInfo>();
            curHP = input.GetS32();
            curAnger = input.GetS32();
            skillInfo = input.GetExt<TLSkillStatusData>();
            buffInfo = input.GetList<BuffSnap>(input.GetExt<BuffSnap>);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(pkInfo);
            output.PutS32(curHP);
            output.PutS32(curAnger);
            output.PutExt(skillInfo);
            output.PutList<BuffSnap>(buffInfo, output.PutExt);
        }
    }

    /// <summary>
    /// 传送场景请求.
    /// </summary>
    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 4)]
    public class TPB2R : PlayerEvent, IB2RMessage
    {
        public string ZoneUUID;
        /// <summary>
        /// 下个场景ID.
        /// </summary>
        public int NextMapID;
        /// <summary>
        /// 下个场景名字.
        /// </summary>
        public string NextMapPosition;
        /// <summary>
        /// 坐标X.
        /// </summary>
        public float X = -1;
        /// <summary>
        /// 坐标Y.
        /// </summary>
        public float Y = -1;

        public string GuildUUID;
        public string RoomKey;
        /// <summary>
        /// >=0 时有效
        /// </summary>
        public int Force;
        public HashMap<string, string> Ext;

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            ZoneUUID = input.GetUTF();
            NextMapID = input.GetS32();
            NextMapPosition = input.GetUTF();
            X = input.GetF32();
            Y = input.GetF32();
            GuildUUID = input.GetUTF();
            RoomKey = input.GetUTF();
            Force = input.GetS32();
            Ext = input.GetMap(input.GetUTF, input.GetUTF);
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutUTF(ZoneUUID);
            output.PutS32(NextMapID);
            output.PutUTF(NextMapPosition);
            output.PutF32(X);
            output.PutF32(Y);
            output.PutUTF(GuildUUID);
            output.PutUTF(RoomKey);
            output.PutS32(Force);
            output.PutMap(Ext,output.PutUTF,output.PutUTF);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 5)]
    public class BattleServerPlayerRebirthNotifyB2R : PlayerEvent, IB2RMessage
    {

    }

    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 6)]
    public class GameOverExtDataB2R : IExternalizable
    {
        public HashMap<string, string> map;

        public void ReadExternal(IInputStream input)
        {
            map = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutMap(map, output.PutUTF, output.PutUTF);
        }
    }

    [MessageType(TLConstants.BATTLE_MSG_B2R_START + 7)]
    public class MonsterDropEventB2R : PlayerEvent, IB2RMessage
    {
        public int sceneID;
        public int monsterID;
        public int[] Drop;
        public int dropID;
        public float x;
        public float y;

        public override void ReadExternal(IInputStream input)
        {
            sceneID = input.GetS32();
            monsterID = input.GetS32();
            Drop = input.GetArray(input.GetS32);
            dropID = input.GetS32();
            x = input.GetF32();
            y = input.GetF32();
        }

        public override void WriteExternal(IOutputStream output)
        {
            output.PutS32(sceneID);
            output.PutS32(monsterID);
            output.PutArray(Drop,output.PutS32);
            output.PutS32(dropID);
            output.PutF32(x);
            output.PutF32(y);
        }

    }

}
