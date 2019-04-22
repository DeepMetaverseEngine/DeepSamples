using DeepCore;
using DeepCore.IO;
using DeepCore.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PARTNER_START + 1)]
    public class ClientPartnerData : ISerializable
    {
        public enum PartnerStatus : byte
        {
            EIdle,
            EFight,
        }

        /// <summary>
        /// 编号.
        /// </summary>
        public int ID;
        /// <summary>
        /// 名字.
        /// </summary>
        public string Name;
        /// <summary>
        /// 等级.
        /// </summary>
        public int Lv;
        /// <summary>
        /// 资质.
        /// </summary>
        public int Qualification;
        /// <summary>
        /// 品阶.
        /// </summary>
        public int QLv;
        /// <summary>
        /// 装备等级.
        /// </summary>
        public int[] EquipLv;
        /// <summary>
        /// 当前经验.
        /// </summary>
        public ulong CurEXP;
        /// <summary>
        /// 升级所需经验.
        /// </summary>
        public ulong NeedEXP;
        /// <summary>
        /// 战斗力.
        /// </summary>
        public int FightPower;
        /// <summary>
        /// 当前状态.
        /// </summary>
        public PartnerStatus CurStatus;
        /// <summary>
        /// 重生时间戳.
        /// </summary>
        public long RebirthTimeStamp;
        /// <summary>
        /// 战斗属性.
        /// </summary>
        public ClientPartnerBattleProp BattleProp;
        /// <summary>
        /// 技能.
        /// </summary>
        public HashMap<int, ClientPartnerSkillData> SkillData;
        /// <summary>
        /// 技能槽数量.
        /// </summary>
        public int SkillSlotCount;
    }

    /// <summary>
    /// 仙侣战斗属性.
    /// </summary>
    [MessageType(TLConstants.TL_PARTNER_START + 2)]
    public class ClientPartnerBattleProp : ISerializable
    {
        public int maxhp;
        public int attack;
        public int defend;
        public int mdef;
        public int through;
        public int block;
        public int crit;
        public int hit;
    }

    /// <summary>
    /// 仙侣技能.
    /// </summary>
    [MessageType(TLConstants.TL_PARTNER_START + 3)]
    public class ClientPartnerSkillData : ISerializable
    {
        public enum SkillType : byte
        {
            EActive = 1,
            EPassive = 2,
        }

        public int skillID;
        public int lv;
        public int index;
        public SkillType skillType;
    }

    [MessageType(TLConstants.TL_PARTNER_START + 4)]
    public class ClientPartnerBookSnap : ISerializable
    {
        /// <summary>
        /// 缘分录ID.
        /// </summary>
        public int BookID;
        /// <summary>
        /// 等级.
        /// </summary>
        public int QualLv;
    }

    [MessageType(TLConstants.TL_PARTNER_START + 5)]
    public class ClientPartnerSnap : ISerializable
    {
        public int id;
        public int lv;
        public int qlv;
    }
}
