using DeepCore.IO;
using System.Collections.Generic;
using DeepMMO.Protocol.Client;
using DeepMMO.Protocol;
using TLProtocol.Data;
using TLProtocol.Protocol.Data;
using DeepMMO.Attributes;
using DeepCore;
using System;

namespace TLProtocol.Protocol.Client
{
    public class GuildResponse : Response
    {
        [MessageCode("您已经有仙盟了")] public const int CODE_ERROR_GUILD_ALREADY_IN_GUILD = 501;
        [MessageCode("仙盟名字不能为空")] public const int CODE_ERROR_GUILD_NAME_EMPTY = 502;
        [MessageCode("仙盟名字过长")] public const int CODE_ERROR_GUILD_NAME_LONG = 503;
        [MessageCode("招募公告过长")] public const int CODE_ERROR_GUILD_RECUIT_LONG = 504;
        [MessageCode("您的资金不足")] public const int CODE_ERROR_GUILD_COST_NO_ENOUGH = 505;
        [MessageCode("仙盟名字已存在")] public const int CODE_ERROR_GUILD_NAME_REPEAT = 506;

        [MessageCode("该仙盟不存在")] public const int CODE_ERROR_GUILD_NOT_FOUND = 507;
        [MessageCode("对方已经有仙盟了")] public const int CODE_ERROR_TARGET_ALREADY_IN_GUILD = 508;
        [MessageCode("您没有操作权限")] public const int CODE_ERROR_GUILD_NO_AUTHORITY = 509;
        [MessageCode("公告板字数过长")] public const int CODE_ERROR_GUILD_NOTICEBOARD_LONG = 510;
        [MessageCode("您还没有仙盟")] public const int CODE_ERROR_NOT_IN_GUILD = 511;

        [MessageCode("仙盟成员人数已满")] public const int CODE_ERROR_GUILD_FULL = 512;
        [MessageCode("您的战力未达到{0}")] public const int CODE_ERROR_GUILD_LOW_POWER = 513;
        [MessageCode("该仙盟申请人数已满")] public const int CODE_ERROR_GUILD_APPLY_LIST_FULL = 514;
        [MessageCode("最多向{0}个仙盟发起申请")] public const int CODE_ERROR_GUILD_SELF_APPLY_FULL = 515;
        [MessageCode("已向该仙盟发起过申请,请不要重复申请")] public const int CODE_ERROR_GUILD_APPLY_REPEAT = 516;
        [MessageCode("当前没有满足条件的仙盟可申请")] public const int CODE_ERROR_GUILD_APPLY_NOT_FOUND = 517;
        [MessageCode("消息不存在或已过期")] public const int CODE_ERROR_GUILD_NO_IN_APPLY = 518;
        [MessageCode("请输入正确的战力限制")] public const int CODE_ERROR_GUILD_CONDITION_POWER = 519;

        [MessageCode("今日捐献次数已用完")] public const int CODE_ERROR_GUILD_DONATE_DONE = 520;
        [MessageCode("该玩家不存在")] public const int CODE_ERROR_GUILD_TARGET_NO_EXIST = 521;
        [MessageCode("该玩家不在同一个仙盟中")] public const int CODE_ERROR_GUILD_TARGET_NO_MEMBER = 522;
        [MessageCode("该职位不存在")] public const int CODE_ERROR_GUILD_POSTION_NO_EXIST = 523;
        [MessageCode("该职位人数已达上限")] public const int CODE_ERROR_GUILD_POSTION_FULL = 524;
        [MessageCode("目标职位不能高于自己")] public const int CODE_ERROR_GUILD_POSTION_HIGHER = 525;

        [MessageCode("必须先转让盟主才能退出仙盟")] public const int CODE_ERROR_GUILD_QUIT_PRESIDENT = 526;
        [MessageCode("盟主需{0}天不上线后，才能进行弹劾")] public const int CODE_ERROR_GUILD_IMPEACH_TIME = 527;

        [MessageCode("此建筑不存在")] public const int CODE_ERROR_GUILD_BUILD_NO_FIND = 528;
        [MessageCode("此建筑已满级")] public const int CODE_ERROR_GUILD_BUILD_LV_FULL = 529;
        [MessageCode("建筑等级不能高于仙盟等级")] public const int CODE_ERROR_GUILD_NEED_GUILD_LEVEL = 530;
        [MessageCode("升级所需资金不足")] public const int CODE_ERROR_GUILD_BUILD_CAPITAL_NOT_ENOUGH = 531;

        [MessageCode("此天赋技能不存在")] public const int CODE_ERROR_GUILD_TALENT_NO_FIND = 532;
        [MessageCode("此天赋技能已满级")] public const int CODE_ERROR_GUILD_TALENT_SKILL_LV_FULL = 533;
        [MessageCode("研究院等级不足")] public const int CODE_ERROR_GUILD_TALENT_LEVEL_NOT_ENOUGH = 534;
        [MessageCode("不满足前置技能升级条件")] public const int CODE_ERROR_GUILD_TALENT_SKILL_UNLOCK = 535;

        [MessageCode("没有可领取的礼物")] public const int CODE_ERROR_GUILD_GIFT_HAS_NO_GET = 536;
        [MessageCode("神兽殿尚未解锁")] public const int CODE_ERROR_GUILD_MONSTER_LEVEL_ZERO = 537;
        [MessageCode("神兽已达到满级")] public const int CODE_ERROR_GUILD_MONSTER_LEVEL_FUNLL = 538;
        [MessageCode("消耗道具不足")] public const int CODE_ERROR_GUILD_ITEM_NO_ENOUGH = 539;
        [MessageCode("名字中不能包含屏蔽字")] public const int CODE_ERROR_GUILD_NAME_BLACKWORW = 540;

        [MessageCode("此仙盟已被设为目标了，不可重复设置")] public const int CODE_ERROR_GUILD_ATTACK_ALREADY_SET = 541;
        [MessageCode("仙盟等级达到{0}级以上才可设置破坏目标")] public const int CODE_ERROR_GUILD_ATTACK_LV_LIMIT = 542;
        [MessageCode("目标仙盟战斗人数爆满，请稍后再试")] public const int CODE_ERROR_GUILD_ATTACK_PLAYER_MAX = 543;
        [MessageCode("玩家等级达到{0}级才能加入仙盟")] public const int CODE_ERROR_GUILD_INVITE_LEVEL_NOT_ENOUGH = 544;
        [MessageCode("该玩家战力不足")] public const int CODE_ERROR_GUILD_INVITE_POWER_NOT_ENOUGH = 545;
        [MessageCode("当前不在活动时间内,请在活动开始后进行设置")] public const int CODE_ERROR_GUILD_OUTSIDE_ACTIVITY_TIME = 546;
        [MessageCode("对方已拒绝邀请")] public const int CODE_ERROR_GUILD_INVITE_REFUSE = 547;

        [MessageCode("只有同一仙盟的成员可以进入")] public const int CODE_ERROR_DIFFERENT_GUILD = 550;
        [MessageCode("只有仙盟会长可以使用该功能")] public const int CODE_ERROR_NOT_PRESIDENT_ID = 551;
        [MessageCode("不处于仙盟押镖报名时间段")] public const int CODE_ERROR_CARRIAGE_JOIN_NOT_OPEN = 552;
        [MessageCode("不处于仙盟押镖报名时间段")] public const int CODE_ERROR_CARRIAGE_ENTER_NOT_OPEN = 553;

        [MessageCode("据点不存在")] public const int CODE_ERROR_FORT_NOT_FOUND = 560;
        [MessageCode("排行榜前{0}的仙盟可报名，您所在的仙盟没有报名权限")] public const int CODE_ERROR_FORT_RANK_NOT_ENOUGH = 561;
        [MessageCode("您的仙盟已报名，无法重复报名")] public const int CODE_ERROR_FORT_ALREADY_SIGNUP = 562;
        [MessageCode("该据点报名仙盟已满，请选择其它据点报名")] public const int CODE_ERROR_FORT_SIGNUP_FULL = 563;
        [MessageCode("仙盟资金不足，无法报名")] public const int CODE_ERROR_FORT_FUND_NOT_ENOUGH = 564;
        [MessageCode("排名{0}至{1}的仙盟{2}时可参与报名")] public const int CODE_ERROR_FORT_SIGNUP_NOT_EARLY = 565;
        [MessageCode("无法报名已占领的据点")] public const int CODE_ERROR_FORT_NOT_SIGNUP_HOLD_FORT = 566;
    }


    /// <summary>
    /// 创建公会
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 1)]
    public class ClientCreateGuildRequest : Request, ILogicProtocol
    {
        public string c2s_guildName;
        public string c2s_recruitBulletin;
    }
    [MessageType(Constants.TL_GUILD_START + 2)]
    public class ClientCreateGuildResponse : GuildResponse, ILogicProtocol
    {
        public GuildSnapData s2c_guildInfo;
        public int s2c_position;
    }

    /// <summary>
    /// 获取公会列表
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 3)]
    public class ClientGetGuildListRequest : Request, ILogicProtocol
    {
        //分页请求，0序
        public int c2s_part;
    }
    [MessageType(Constants.TL_GUILD_START + 4)]
    public class ClientGetGuildListResponse : GuildResponse, ILogicProtocol
    {
        //是否已全部获取
        public bool s2c_isFull;
        public List<GuildBaseSnapData> s2c_guildList;
    }

    /// <summary>
    /// 获取公会信息
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 5)]
    public class ClientGetGuildInfoRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 6)]
    public class ClientGetGuildInfoResponse : GuildResponse, ILogicProtocol
    {
        public GuildSnapData s2c_guildInfo;
        public int s2c_position;
        public int s2c_giftCount;
    }

    /// <summary>
    /// 修改公告
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 7)]
    public class ClientChangeNoticeRequest : Request, ILogicProtocol
    {
        public string c2s_context;
    }
    [MessageType(Constants.TL_GUILD_START + 8)]
    public class ClientChangeNoticeResponse : GuildResponse, ILogicProtocol
    {
        public string s2c_context;
    }

    /// <summary>
    /// 申请加入公会
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 9)]
    public class ClientApplyGuildRequest : Request, ILogicProtocol
    {
        //为空代表一键加入
        public string c2s_guildId;
    }
    [MessageType(Constants.TL_GUILD_START + 10)]
    public class ClientApplyGuildResponse : GuildResponse, ILogicProtocol
    {
        //无需审批时直接加入成功返回公会信息,否则为空
        public GuildSnapData s2c_guildInfo;
        public int s2c_position;
    }

    /// <summary>
    /// 公会成员列表
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 11)]
    public class ClientGuildMemListRequest : Request, ILogicProtocol
    {
    }
    [MessageType(Constants.TL_GUILD_START + 12)]
    public class ClientGuildMemListResponse : GuildResponse, ILogicProtocol
    {
        public int s2c_memberCount;
        public int s2c_memberCountMax;
        public int s2c_condition;
        public string s2c_recruitBulletin;
        public List<GuildMemberSnapData> s2c_members;
    }

    /// <summary>
    /// 申请列表
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 13)]
    public class ClientGuildApplyListRequest : Request, ILogicProtocol
    {
    }
    [MessageType(Constants.TL_GUILD_START + 14)]
    public class ClientGuildApplyListResponse : GuildResponse, ILogicProtocol
    {
        public List<GuildApplySnapData> s2c_applyList;
    }

    /// <summary>
    /// 公会审批
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 15)]
    public class ClientDealGuildApplyRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 1: 同意 2：拒绝
        /// </summary>
        public bool c2s_operate;
        public string c2s_msgId;
    }
    [MessageType(Constants.TL_GUILD_START + 16)]
    public class ClientDealGuildApplyResponse : GuildResponse, ILogicProtocol
    {
        public int s2c_memberCount;
    }

    /// <summary>
    /// 修改申请条件
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 17)]
    public class ClientApplyConditionRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 0:无条件 -1：需审批 >0：战力限制
        /// </summary>
        public int c2s_condition;
    }
    [MessageType(Constants.TL_GUILD_START + 18)]
    public class ClientApplyConditionResponse : GuildResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 修改招募公告
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 19)]
    public class ClientChangeRecruitRequest : Request, ILogicProtocol
    {
        public string c2s_context;
    }
    [MessageType(Constants.TL_GUILD_START + 20)]
    public class ClientChangeRecruitResponse : GuildResponse, ILogicProtocol
    {
        public string s2c_context;
    }

    /// <summary>
    /// 每日捐献
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 21)]
    public class ClientDailyDonateRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 1: 低级 2 : 中级 3：高级
        /// </summary>
        public int c2s_type;
    }
    [MessageType(Constants.TL_GUILD_START + 22)]
    public class ClientDailyDonateResponse : GuildResponse, ILogicProtocol
    {
        public int s2c_donateCount;
        public int s2c_contribution;
        public int s2c_contributionMax;
    }

    /// <summary>
    /// 公会职位变更
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 23)]
    public class ClientChangePostionRequest : Request, ILogicProtocol
    {
        public string c2s_tarRoleId;
        public int c2s_tarPosition;
    }
    [MessageType(Constants.TL_GUILD_START + 24)]
    public class ClientChangePostionResponse : GuildResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 退出公会
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 25)]
    public class ClientQuitGuildRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// id为自己时：退出 id为其他人：踢人
        /// </summary>
        public string c2s_roleId;
    }
    [MessageType(Constants.TL_GUILD_START + 26)]
    public class ClientQuitGuildResponse : GuildResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 转让会长
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 27)]
    public class ClientTransferGuildRequest : Request, ILogicProtocol
    {
        public string c2s_roleId;
    }
    [MessageType(Constants.TL_GUILD_START + 28)]
    public class ClientTransferGuildResponse : GuildResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 弹劾会长
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 29)]
    public class ClientImpeachGuildRequest : Request, ILogicProtocol
    {
    }
    [MessageType(Constants.TL_GUILD_START + 30)]
    public class ClientImpeachGuildResponse : GuildResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 建筑物升级
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 31)]
    public class ClientBuildLevelUpRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 1:议事厅升级 2:马厩 3:神兽殿 4:研究院升级 5:杂货铺升级
        /// </summary>
        public int c2s_buildId;
    }
    [MessageType(Constants.TL_GUILD_START + 32)]
    public class ClientBuildLevelUpResponse : GuildResponse, ILogicProtocol
    {
        public long s2c_fund;
    }

    /// <summary>
    /// 获取天赋信息
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 37)]
    public class ClientGetTalentDataRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 38)]
    public class ClientGetTalentDataResponse : GuildResponse, ILogicProtocol
    {
        public GuildTalentSnapData s2c_talent;
    }

    /// <summary>
    /// 天赋升级
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 39)]
    public class ClientTalentSkillUpRequest : Request, ILogicProtocol
    {
        public int c2s_skillId;
    }
    [MessageType(Constants.TL_GUILD_START + 40)]
    public class ClientTalentSkillUpResponse : GuildResponse, ILogicProtocol
    {
        public int s2c_skillLv;
    }

    /// <summary>
    /// 获取礼物列表
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 41)]
    public class ClientGiftInfoRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 42)]
    public class ClientGiftInfoResponse : GuildResponse, ILogicProtocol
    {
        public GuildGiftSnapData s2c_giftInfo;
    }

    /// <summary>
    /// 一键领取礼物
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 43)]
    public class ClientOpenGiftRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 44)]
    public class ClientOpenGiftResponse : GuildResponse, ILogicProtocol
    {
        public GuildGiftSnapData s2c_giftInfo;
    }

    /// <summary>
    /// 获取神兽殿信息
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 45)]
    public class ClientGetMonsterInfoRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 46)]
    public class ClientGetMonsterInfoResponse : GuildResponse, ILogicProtocol
    {
        public GuildMonsterSnapData s2c_monsterInfo;
    }

    /// <summary>
    /// 神兽一键喂食
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 47)]
    public class ClientMonsterLeveUpRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 48)]
    public class ClientMonsterLeveUpResponse : GuildResponse, ILogicProtocol
    {
        public GuildMonsterSnapData s2c_monsterInfo;
    }

    /// <summary>
    /// 进入公会场景
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 49)]
    public class ClientEnterGuildSceneRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 50)]
    public class ClientEnterGuildSceneResponse : GuildResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 邀请加入公会
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 51)]
    public class ClientInviteToGuildRequest : Request, ILogicProtocol
    {
        public string c2s_roleId;
    }
    [MessageType(Constants.TL_GUILD_START + 52)]
    public class ClientInviteToGuildResponse : GuildResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 请求目标列表
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 53)]
    public class ClientGetGuildAttackListRequest : Request, ILogicProtocol
    {
        //分页请求，0序
        public int c2s_part;
    }
    [MessageType(Constants.TL_GUILD_START + 54)]
    public class ClientGetGuildAttackListResponse : GuildResponse, ILogicProtocol
    {
        //是否已全部获取
        public bool s2c_isFull;
        public List<GuildBaseSnapData> s2c_guildList;
        public string s2c_attackGuild;
        public string s2c_attackedGuild;
        public HashMap<string, GuildAtackInfo> s2c_attackGuilds;
    }

    /// <summary>
    /// 设置破坏目标
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 55)]
    public class ClientSetGuildAttackRequest : Request, ILogicProtocol
    {
        //为空表示取消
        public string c2s_guildId;
    }
    [MessageType(Constants.TL_GUILD_START + 56)]
    public class ClientSetGuildAttackResponse : GuildResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 仙盟活动信息请求
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 57)]
    public class ClientGuildActivityInfoRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 58)]
    public class ClientGuildActivityInfoResponse : GuildResponse, ILogicProtocol
    {
        public HashMap<string, GuildActivityInfo> s2c_activityInfo;
        public string s2c_fort;
    }


    /// <summary>
    /// 通知客户端公会变更
    /// </summary>
    //////////////////////////////////////////////////
    [MessageType(Constants.TL_GUILD_START + 60)]
    public class ClientGuildChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public string s2c_guildId;
        public string s2c_guildName;
        public bool s2c_isKick;
    }
    /// <summary>
    /// 通知客户端公会变更
    /// </summary>
    //////////////////////////////////////////////////
    [MessageType(Constants.TL_GUILD_START + 61)]
    public class ClientGuildInfoChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_type;

        public int s2c_level;
        public HashMap<int, int> s2c_buildList;
        public int s2c_monsterRank;

        public int s2c_donateCount;
        public int s2c_position;
        public HashMap<int, int> s2c_talentList;

        public DateTime s2c_synTime;
    }
	
    [MessageType(Constants.TL_GUILD_START + 65)]
    public class ClientGuildJoinCarriageRequest : Request, ILogicProtocol
    {
    }
    
    [MessageType(Constants.TL_GUILD_START + 66)]
    public class ClientGuildJoinCarriageResponse : GuildResponse, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_GUILD_START + 67)]
    public class ClientGuildCarriageStateRequest : Request, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_GUILD_START + 68)]
    public class ClientGuildCarriageStateResponse : GuildResponse, ILogicProtocol
    {
        public bool s2c_joined;
        public string s2c_enemyGuildName;
        public int s2c_guildLv;
        public long s2c_currentFund;
    }

    [MessageType(Constants.TL_GUILD_START + 69)]
    public class ClientGuildEnterCarriageRequest : Request, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_GUILD_START + 70)]
    public class ClientGuildEnterCarriageResponse : GuildResponse, ILogicProtocol
    {
    }

    /// <summary>
    /// 仙盟据点信息请求
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 71)]
    public class ClientGuildFortInfoRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GUILD_START + 72)]
    public class ClientGuildFortInfoResponse : GuildResponse, ILogicProtocol
    {
        public HashMap<string, GuildBaseSnapData> s2c_rankList;
        public HashMap<int, GuildFortInfo> s2c_fortList;
        public int s2c_selfRank;
        public int s2c_selfFort;
    }

    /// <summary>
    /// 仙盟据点报名请求
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 73)]
    public class ClientGuildFortSignUpRequest : Request, ILogicProtocol
    {
        public int c2s_fortId;
    }
    [MessageType(Constants.TL_GUILD_START + 74)]
    public class ClientGuildFortSignUpResponse : GuildResponse, ILogicProtocol
    {

    }

    /// <summary>
    /// 仙盟据点进入请求
    /// </summary>
    [MessageType(Constants.TL_GUILD_START + 75)]
    public class ClientGuildFortEnterRequest : Request, ILogicProtocol
    {
        //1 主战场 2 副战场
        public int c2s_battleType;
    }
    [MessageType(Constants.TL_GUILD_START + 76)]
    public class ClientGuildFortEnterResponse : GuildResponse, ILogicProtocol
    {

    }

}
