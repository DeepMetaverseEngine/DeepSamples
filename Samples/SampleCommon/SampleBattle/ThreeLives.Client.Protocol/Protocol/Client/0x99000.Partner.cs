using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    #region Notify.

    /// <summary>
    /// 仙侣属性变更通知.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 7)]
    public class ClientPartnerDataChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public ClientPartnerData s2c_data;
    }

    /// <summary>
    /// 仙侣死亡通知.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 8)]
    public class ClientPartnerDeadNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_partnerID;
        public long s2c_timestamp;
    }

    [MessageType(Constants.TL_PARTNER_START + 13)]
    public class ClientPartnerEXPChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_partnerID;
        public ulong s2c_exp;
    }

    [MessageType(Constants.TL_PARTNER_START + 14)]
    public class ClientPartnerLvUpNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public ClientPartnerData s2c_data;
    }

    #endregion

    #region 召唤.

    /// <summary>
    /// 召唤仙侣.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 1)]
    public class ClientSummonPartnerRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
    }
    /// <summary>
    /// 召唤仙侣.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 2)]
    public class ClientSummonPartnerResponse : Response, ILogicProtocol
    {
        public ClientPartnerData s2c_partner;
    }

    #endregion

    #region 详情.

    /// <summary>
    /// 侠侣详情.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 3)]
    public class ClientGetPartnerDetailRequest : Request, ILogicProtocol
    {
        public List<int> c2s_idLt;
    }
    /// <summary>
    /// 仙侣详情.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 4)]
    public class ClientGetPartnerDetailResponse : Response, ILogicProtocol
    {
        public List<ClientPartnerData> s2c_data;
    }

    #endregion

    #region 升级.

    /// <summary>
    /// 仙侣升级.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 5)]
    public class ClientPartnerAddEXPRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 升级的仙侣.
        /// </summary>
        public int c2s_partnerID;
        /// <summary>
        /// 指定消耗的道具.
        /// </summary>
        public int c2s_itemID;
        /// <summary>
        /// -1为快捷使用.
        /// </summary>
        public int c2s_count;
    }
    /// <summary>
    /// 仙侣升级.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 6)]
    public class ClientPartnerAddEXPResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("仙侣等级不能超过角色等级")]
        public const int CODE_LV_MAX = 501;
        [MessageCodeAttribute("无效的物品")]
        public const int CODE_ITEM_INVALID = 502;

        public ulong s2c_curEXP;
    }

    #endregion

    #region 仙侣进阶突破.

    /// <summary>
    /// 仙侣进阶.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 9)]
    public class ClientPartnerUpgradeQLvRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
        public int c2s_itemID;
        public int c2s_itemCount;
    }
    /// <summary>
    /// 仙侣进阶.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 10)]
    public class ClientPartnerUpgradeQLvResponse : Response, ILogicProtocol
    {

    }

    #endregion

    #region 列表.

    /// <summary>
    /// 仙侣列表
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 11)]
    public class ClientGetPartnerListRequest : Request, ILogicProtocol
    {

    }
    /// <summary>
    /// 仙侣列表.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 12)]
    public class ClientGetPartnerListResponse : Response, ILogicProtocol
    {
        public List<ClientPartnerSnap> s2c_list;
    }

    #endregion

    #region 进阶

    /// <summary>
    /// 进阶
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 15)]
    public class ClientPartnerUpgradeQualityRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
    }
    /// <summary>
    /// 进阶.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 16)]
    public class ClientPartnerUpgradeQualityResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("仙侣品质已达到最高")]
        public const int CODE_LV_MAX = 501;


        public int s2c_quality;

    }

    #endregion

    #region 装备强化.

    /// <summary>
    /// 装备强化.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 17)]
    public class ClientPartnerUpGradeEquipRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
        public int c2s_pos;
    }
    /// <summary>
    /// 装备强化.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 18)]
    public class ClientPartnerUpGradeEquipResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("装备已强化到最高等级")]
        public const int CODE_LV_MAX = 501;

        public int s2c_lv;

    }

    #endregion

    #region 仙侣技能.

    /// <summary>
    /// 技能升级.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 19)]
    public class ClientPartnerSkillRefineRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
        public int c2s_skillID;
    }
    /// <summary>
    /// 技能升级.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 20)]
    public class ClientPartnerSkillRefineResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("技能已强化到最高等级")]
        public const int CODE_LV_MAX = 501;
        [MessageCodeAttribute("已达到当前品阶最高等级")]
        public const int CODE_QUALITY_LV_MAX = 502;
        public int s2c_lv;
        public int s2c_skillID;
        public int s2c_partnerID;
    }
    /// <summary>
    /// 仙侣学习技能.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 21)]
    public class ClientPartnerLearnSkillRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
        /// <summary>
        /// 从1开始.
        /// </summary>
        public byte c2s_index;
        public int c2s_skillBookID;
    }
    /// <summary>
    /// 仙侣学习技能.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 22)]
    public class ClientPartnerLearnSkillResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("仙侣已拥有该技能")]
        public const int CODE_EXIST_SKILL = 501;
        [MessageCodeAttribute("没有多余的技能栏")]
        public const int CODE_NOT_ENOUGH_SLOT = 502;
        [MessageCodeAttribute("不存在该技能")]
        public const int CODE_NOT_EXIT_SKILL = 503;
        [MessageCodeAttribute("技能栏已有技能")]
        public const int CODE_SLOT_HAS_SKILL = 504;

        public int s2c_partnerID;
        public ClientPartnerSkillData s2c_skillData;
    }
    /// <summary>
    /// 仙侣遗忘技能.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 23)]
    public class ClientPartnerForgetSkillRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
        public int c2s_skillID;
    }
    /// <summary>
    /// 仙侣遗忘技能.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 24)]
    public class ClientPartnerForgetSkillResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("主动技能不能遗忘")]
        public const int CODE_INVALID_OP = 501;

        public int s2c_partnerID;
        public int s2c_skillID;
    }

    #endregion

    #region 仙侣出战/备战

    [MessageType(Constants.TL_PARTNER_START + 25)]
    public class ClientPartnerStatusChangeRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;
        /// <summary>
        /// Enum:PartnerStatus
        /// </summary>
        public byte c2s_status;
    }

    [MessageType(Constants.TL_PARTNER_START + 26)]
    public class ClientPartnerStatusChangeResponse : Response, ILogicProtocol
    {
        public int s2c_partnerID;
        /// <summary>
        /// Enum:PartnerStatus
        /// </summary>
        public byte s2c_status;
    }

    #endregion

    #region 仙侣还原.

    [MessageType(Constants.TL_PARTNER_START + 27)]
    public class ClientRestorePartnerRequest : Request, ILogicProtocol
    {
        public int c2s_partnerID;

    }

    [MessageType(Constants.TL_PARTNER_START + 28)]
    public class ClientRestorePartnerResponse : Response, ILogicProtocol
    {
        public ClientPartnerData s2c_data;
    }

    #endregion

    #region 缘分录.
    /// <summary>
    /// 获取缘分录信息.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 29)]
    public class ClientGetPartnerBookInfoRequest : Request, ILogicProtocol
    {

    }
    /// <summary>
    /// 获取缘分录信息.
    /// </summary>
    [MessageType(Constants.TL_PARTNER_START + 30)]
    public class ClientGetPartnerBookInfoResponse : Response, ILogicProtocol
    {
        public List<ClientPartnerBookSnap> s2c_list;
    }
    #endregion

}
