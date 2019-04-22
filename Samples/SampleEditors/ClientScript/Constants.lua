---------------------------------
--! @file 通用的常量表
--! @brief a Doxygen::Lua Constants.lua
---------------------------------
local Util = require 'Logic/Util'
Constants = Constants or {}

Constants.FontSize = {
	detail_name = 24,
	detail_base = 20,
	detail_attr_title = 20,
	detail_normal = 20
}
Constants.ItemType = {
	Equip = 2,
	Gem = 3,
	Use = 4,
	Skill = 99999,
}

Constants.ItemSecType = {
	FateType=31,
}

-- RGBA
Constants.Color = {
	-- 色板
	Normal = 0xa56748,
	Normal2 = 0x7d674d,
	Red = 0xff0000,
	Red1 = 0xce0b0b,
	White = 0xffffff,
	Green = 0x00ff00,
	Green2 = 0x0b9c00,
	Green3 = 0x65ab11,
	Green4 = 0x478f1b,
	detail_normal = 0xffffff,
	detail_limit_red = 0xfb1919,
	detail_extra_attr = 0x80c5ff,
	detail_skill_attr = 0xc881ff,
	detail_attr_title = 0xfff38b,
	detail_gem_attr = 0x60f247,
	detail_suit_light = 0x1faf07,
	detail_suit_gray = 0x909090,
} 
-- 存放客户端代码内部使用的指定图片路径
-- #开头表示图集中图块 #dynamic/effects/skill/skilllevelup.xml|skill_levelup1|21
-- @开头表示动图 @actor_001010/output/actor.xml|actor_001010|001010|3
-- $开头表示图集中指定key的图块  $dynamic/effects/skill/skilllevelup.xml|skill_levelup1|hzdsb
Constants.InternalImg = {
	-- hzdsb = '$dynamic/effects/skill/skilllevelup.xml|skill_levelup1|hzdsb'
	detail_gray_point = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|9',
	split_line = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|37',
	icon_di1 = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|45',
	icon_di2 = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|44',
	icon_di3 = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|46',
	icon_di4 = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|43',
	icon_di5 = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|43',
	arrow_up = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|15',
	arrow_down = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|17',
	detail_bind = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|0',
	detail_gem_frame = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|2',
	detail_gem_lock = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|3',
	detail_gem_fill = '#dynamic/TL_strengthen/output/TL_strengthen.xml|TL_strengthen|11',
	red_pointer = '#static/TL_staticnew/output/TL_staticnew.xml|TL_static|99',
	fate_lock = '#dynamic/TL_fate/output/TL_fate.xml|TL_fate|22',
	fate_cost = '#dynamic/TL_fate/output/TL_fate.xml|TL_fate|53',
	title_lock = '#dynamic/TL_title/output/TL_title.xml|TL_title|7',
	title_equip = '#dynamic/TL_title/output/TL_title.xml|TL_title|16',
}

Constants.Item = 
{
	DefaultSize = UnityEngine.Vector2(74,74),
	DefaultW = 74,
	DefaultH = 74,
	-- DefaultSize = UnityEngine.Vector2(74,74),
}

Constants.LangNum = {
	[0] = Util.GetText("LangNum_0"),
	[1] = Util.GetText("LangNum_1"),
	[2] = Util.GetText("LangNum_2"),
	[3] = Util.GetText("LangNum_3"),
	[4] = Util.GetText("LangNum_4"),
	[5] = Util.GetText("LangNum_5"),
	[6] = Util.GetText("LangNum_6"),
	[7] = Util.GetText("LangNum_7"),
	[8] = Util.GetText("LangNum_8"),
	[9] = Util.GetText("LangNum_9"),
	[10] = Util.GetText("LangNum_10"),
}


Constants.Text = {
	format_countdown = Util.GetText("format_countdown"),--'还剩{0}秒',
	equip_lv_format = Util.GetText("common_level")..": {0}",
	UseSuccess = Util.GetText("UseSuccess"),
    detail_btn_equip = Util.GetText("detail_btn_equip"),
    detail_btn_use = Util.GetText("detail_btn_use"),
    detail_btn_unEquip = Util.GetText("detail_btn_unEquip"),
    detail_btn_strengthen = Util.GetText("detail_btn_strengthen"),
    detail_btn_put = Util.GetText("detail_btn_put"),
    detail_btn_get = Util.GetText("detail_btn_get"),
    detail_btn_combine = Util.GetText("detail_btn_combine"),
    detail_btn_sell = Util.GetText("detail_btn_sell"),
    detail_btn_popGem = Util.GetText("detail_btn_popGem"),
    detail_btn_decompose = Util.GetText("detail_btn_decompose"),
    detail_btn_puton = Util.GetText("trade_onsale"),
    all_pro = Util.GetText("all_pro"),
    gem_locklv_format = Util.GetText("gem_locklv_format"),
    warn_notexitgem = Util.GetText("warn_notexitgem"),
    warn_notexitgemSlot = Util.GetText("warn_notexitgemSlot"),
    warn_extrasize = Util.GetText("warn_extrasize"),
    warn_decompose_limit = Util.GetText("warn_decompose_limit"),
    confirm_lockattr = Util.GetText("confirm_lockattr"),
    confirm_lockattr_title = Util.GetText("confirm_lockattr_title"),
    detail_title_gem = Util.GetText("detail_title_gem"),
    detail_strengthen_format = Util.GetText("detail_strengthen_format"),
    base_attr_title = Util.GetText("base_attr_title"),
    extra_attr_title = Util.GetText("extra_attr_title"),
    unlock_mount_tip = Util.GetText("unlock_mount_tip"),
    nonext_attr_title = Util.GetText("nonext_attr_title"),
    next_attr_cost = Util.GetText("next_attr_cost"),
    no_attr = Util.GetText("no_attr"),
    
    strengthen_ranklv = Util.GetText("strengthen_ranklv"),
    strengthen_nonelv = Util.GetText("strengthen_nonelv"),
    strengthen_rank = Util.GetText("strengthen_rank"),
    strengthen_lv = Util.GetText("strengthen_lv"),
    strengthen_rankadded = Util.GetText("strengthen_rankadded"),
    
    
    confirm_sell = Util.GetText("confirm_sell"),
    confirm_sell_title = Util.GetText("confirm_sell_title"),
    confirm_bagsize = Util.GetText("confirm_bagsize"),
    confirm_bagsize_title = Util.GetText("confirm_bagsize_title"),
    confirm_warehoursesize = Util.GetText("confirm_warehoursesize"),
    confirm_warehoursesize_title = Util.GetText("confirm_warehoursesize_title"),
    warn_decompquality = Util.GetText("warn_decompquality"),
    warn_maxLockCount = Util.GetText("warn_maxLockCount"),
    
    
    confirm_team_follow = Util.GetText("confirm_team_follow"),
    team_target_desc = Util.GetText("team_target_desc"),
    team_lv = Util.GetText("team_lv"),
    team_fightpower = Util.GetText("team_fightpower"),
    team_warnAutoApply = Util.GetText("team_warnAutoApply"),
    team_warnNoTeam = Util.GetText("team_warnNoTeam"),
    team_warnNotLeader = Util.GetText("team_warnNotLeader"),
    
	partner_growthcoefficient = Util.GetText("partner_growthcoefficient"),
	partner_goto = Util.GetText("partner_goto"),
	partner_itemCount = Util.GetText("partner_itemCount"),
	partner_refinelv = Util.GetText("partner_refinelv"),
	partner_skillslotlimit = Util.GetText("partner_skillslotlimit"),
	partner_refinelvlimit = Util.GetText("partner_refinelvlimit"),
	partner_fight = Util.GetText("partner_fight"),
	partner_FateUnlock = Util.GetText("partner_FateUnlock"),
	partner_skilllv = Util.GetText("partner_skilllv"),
	partner_green = Util.GetText("partner_green"),
	partner_blue = Util.GetText("partner_blue"),
	partner_purple = Util.GetText("partner_purple"),
	partner_orange = Util.GetText("partner_orange"),
	partner_noskilltolearn = Util.GetText("partner_noskilltolearn"),
	partner_skilllimit = Util.GetText("partner_skilllimit"),
	partner_existskill = Util.GetText("partner_existskill"),
	partner_noskillbook = Util.GetText("partner_noskillbook"),
	partner_hasskillbook = Util.GetText("partner_hasskillbook"),


	fallenpartner_rank1 = Util.GetText("fallenpartner_rank1"),
	fallenpartner_rank2 = Util.GetText("fallenpartner_rank2"),
	fallenpartner_rank3 = Util.GetText("fallenpartner_rank3"),
	fallenpartner_rank4 = Util.GetText("fallenpartner_rank4"),
	fallenpartner_rank5 = Util.GetText("fallenpartner_rank5"),
	fallenpartner_rank6 = Util.GetText("fallenpartner_rank6"),
	fallenpartner_rank7 = Util.GetText("fallenpartner_rank7"),
	fallenpartner_rank8 = Util.GetText("fallenpartner_rank8"),
	fallenpartner_rank9 = Util.GetText("fallenpartner_rank9"),
	fallenpartner_skilllv = Util.GetText("fallenpartner_skilllv"),
	fallenpartner_power = Util.GetText("fallenpartner_power"),


	wing_unlock_text = Util.GetText("wing_unlock_text"),
	wing_advanced_text = Util.GetText("wing_advanced_text"),
	wing_maxlv = Util.GetText("wing_maxlv"),
	wing_formatstar = Util.GetText("wing_formatstar"),
	wing_developed_text = Util.GetText('wing_developed_text'),


	skill_Player = Util.GetText("skill_Player"),
	skill_Level = Util.GetText("skill_Level"),
	skill_Open = Util.GetText("skill_Open"),

	

	shop_Discount = Util.GetText("shop_Discount"),
	shop_Day = Util.GetText("shop_Day"),
	shop_Hour = Util.GetText("shop_Hour"),
	shop_LessHour = Util.GetText("shop_LessHour"),
	shop_noItem = Util.GetText("shop_noItem"),
	shop_buysucc = Util.GetText("shop_buysucc"),
	show_buyFailed = Util.GetText("show_buyFailed"),
	show_VipTimes = Util.GetText("show_VipTimes"),
	show_overbuytime =Util.GetText("show_overbuytime"),

	pvp_rescount = Util.GetText("pvp_rescount"),
	pvp_exploit = Util.GetText("pvp_exploit"),
	pvp_exploitlimit = Util.GetText("pvp_exploitlimit"),
	pvp_countdown = Util.GetText("pvp_countdown"),
	pvp_joinlimit = Util.GetText("pvp_joinlimit"),

	pvp_warn_count = Util.GetText("pvp_warn_count"),
	pvp_rewardcount = Util.GetText("pvp_rewardcount"),
	pvp_wincount = Util.GetText("pvp_wincount"),
	pvp_winresult = Util.GetText("pvp_winresult"),
	pvp_redwin = Util.GetText("pvp_redwin"),
	pvp_bluewin = Util.GetText("pvp_bluewin"),
	pvp_drawwin = Util.GetText("pvp_drawwin"),
	pvp_resscore = Util.GetText("pvp_resscore"),
	pvp_warn_robber = Util.GetText("pvp_warn_robber"),
	pvp_single_score = '积分{0}',
	exit_zone_warn = Util.GetText("exit_zone_warn"),

	artifact_inbattle = Util.GetText("artifact_inbattle"),
	artifact_equip_again = Util.GetText("artifact_equip_again"),
	artifact_equipIndex_notOpen = Util.GetText("artifact_equipIndex_notOpen"),
	artifact_Main = Util.GetText("artifact_Main"),
	artifact_Second = Util.GetText("artifact_Second"),
	artifact_buyCost = Util.GetText("artifact_buyCost"),
	artifact_lvupCost = Util.GetText("artifact_lvupCost"),

	godisland_passbefore = Util.GetText("godisland_passbefore"),
	godisland_passlock = Util.GetText("godisland_passlock"),

	chat_getItem = Util.GetText("chat_getItem"),
	chat_noread = Util.GetText("chat_noread"),
	chat_noTarget = Util.GetText("chat_noTarget"),
	chat_noItem = Util.GetText("chat_noItem"),

	wardobe_takeOnText = Util.GetText("wardobe_takeOnText"),
	wardobe_takeOffText = Util.GetText("wardobe_takeOffText"),
	wardobe_dayLeftText = Util.GetText("wardobe_dayLeftText"),

	goto = Util.GetText("goto"),
	notintime = Util.GetText("notintime"),

	notinselfguild = Util.GetText("notinselfguild"),
	countdowntime = Util.GetText("countdowntime"),
	equip_refine_effect = Util.GetText("equip_refine_effect"),
	guild_carriage_nomatch = Util.GetText("guild_carriage_nomatch"),
	guild_carriage_matching = Util.GetText("guild_carriage_matching"),
	guild_carriage_nojoin = Util.GetText("guild_carriage_nojoin"),
	guild_carriage_join = Util.GetText("guild_carriage_join"),
	guild_carriage_btntrans = Util.GetText("guild_carriage_btntrans"),
	guild_carriage_btnjoin = Util.GetText("guild_carriage_btnjoin"),
	guild_carriage_pcount = Util.GetText("guild_carriage_pcount"),
	guild_carriage_distance = Util.GetText("guild_carriage_distance"),
	guild_carriage_win = Util.GetText("guild_carriage_win"),
	guild_carriage_lose = Util.GetText("guild_carriage_lose"),
	guild_carriage_btnleave = Util.GetText("guild_carriage_btnleave"),
	first_map_warn = Util.GetText("first_map_warn"),
	ChatHornMaxString = Util.GetText("ChatHornMaxString"),

	NoEnoughCopper = Util.GetText("NoEnoughCopper"),
	SaveSetting = Util.GetText("SaveSetting"),
	TimeOrCount = Util.GetText("TimeOrCount"),
	enter_now = Util.GetText("enter_now"),

	NoEquipTitle = Util.GetText("NoEquipTitle"),
	Fate_LvUp = Util.GetText("Fate_LvUp"),
	Fate_Lock = Util.GetText("Fate_Lock"),
	Fate_UnLock = Util.GetText("Fate_UnLock"),
	Fate_Decompose = Util.GetText("Fate_Decompose"),
	Fate_CD = Util.GetText("Fate_CD"),

	Pvp1WaitStr = '对方将在{0}秒内回应'
	
}

Constants.StarLevel = {
	[0] = Util.GetText("StarLevel_0"),
	[1] = Util.GetText("StarLevel_1"),
	[2] = Util.GetText("StarLevel_2"),
	[3] = Util.GetText("StarLevel_3")
}

Constants.StarQuality = {
	[0] = 0xadadad,
	[1] = 0x3acaff,
	[2] = 0xfa66ff,
	[3] = 0xfff15a
}

Constants.ChatFomatText = 
{
	ShowPos = Util.GetText("ChatFomatText_ShowPos"),

}

Constants.GuildWant =
{
	CallConstants = Util.GetText("GuildWant_CallConstants"),
	CallTip = Util.GetText("GuildWant_CallTip"),
	HelpTip = Util.GetText("GuildWant_HelpTip"),
	CountLimit = Util.GetText("GuildWant_CountLimit"),
	CallTittle = Util.GetText("GuildWant_CallTittle"),
	HelpTxt = Util.GetText("GuildWant_HelpTxt"),
	RefreshTittle = Util.GetText("GuildWant_RefreshTittle"),
	PayRefreshTittle = Util.GetText("GuildWant_PayRefreshTittle"),

}

--师门身份赛弹窗文本
Constants.PlayRule =
{
	CancelRelativeTips = Util.GetText("PlayRule_CancelRelativeTips"),
	AddRelativeTips = Util.GetText("PlayRule_AddRelativeTips"),
	ClickSelfTips = Util.GetText("PlayRule_ClickSelfTips"),
	NotSelectFrient = Util.GetText("PlayRule_NotSelectFrient"),
	BattleTimeShow = Util.GetText("PlayRule_BattleTimeShow"),
	CanNotSelectRelative = Util.GetText("PlayRule_CanNotSelectRelative"),

}

Constants.StroeLimit = 
{
	--商店
	[1] = Util.GetText("StroeLimit_1"),
	[2] = Util.GetText("StroeLimit_2"),
	[3] = Util.GetText("StroeLimit_3"),
	[4] = Util.GetText("StroeLimit_4"),
}

--TODO 修改成读取配置
Constants.ProName = 
{
	[1] = Util.GetText("ProName_1"),
	[2] = Util.GetText("ProName_2"),
	[3] = Util.GetText("ProName_3"),
	[4] = Util.GetText("ProName_4"),
	-- [5] = '奶妈',
}

--TODO 修改成读取配置
Constants.EquipPartName = 
{
	[1] = Util.GetText("EquipPartName_1"),
	[2] = Util.GetText("EquipPartName_2"),
	[3] = Util.GetText("EquipPartName_3"),
	[4] = Util.GetText("EquipPartName_4"),
	[5] = Util.GetText("EquipPartName_5"),
	[6] = Util.GetText("EquipPartName_6"),
	[7] = Util.GetText("EquipPartName_7"),
	[8] = Util.GetText("EquipPartName_8"),
}

Constants.Quality = {
	White = 0,
	Green = 1,
	Blue = 2,
	Purple = 3,
	Orange = 4,
}

Constants.QualityText = 
{
	[1] = Util.GetText("QualityText_1"),
	[2] = Util.GetText("QualityText_2"),
	[3] = Util.GetText("QualityText_3"),
	[4] = Util.GetText("QualityText_4"),
	[5] = Util.GetText("QualityText_5"),
}

Constants.QualityColor =
{
	[0] = 0xffffff,
	[1] = 0x38dc26,
	[2] = 0x2facff,
	[3] = 0xe867ff,
	[4] = 0xff8400,
	[5] = 0xfb1919,
}


Constants.Activity =
{
	PlayerNeedLevel = Util.GetText("PlayerNeedLevel"),
	ActivityTime = Util.GetText("ActivityTime"),
	DoneState = Util.GetText("DoneState"),
	Count = Util.GetText("Count"),
	Delaytimestart = Util.GetText("Delaytimestart"),
	ActivityPushTime = Util.GetText("ActivityPushTime"),
	BagNotEmpty = Util.GetText("BagNotEmpty"),
}

Constants.RankList =
{
	RankName = Util.GetText("RankName"),
}

Constants.PagodaStoryBtn =
{
	[0] = Util.GetText("PagodaStoryBtn_0"),
	[1] = Util.GetText("PagodaStoryBtn_1"),
	[2] = Util.GetText("PagodaStoryBtn_2"),
	[3] = Util.GetText("PagodaStoryBtn_3"),
}

Constants.PagodaChestBtn =
{
	UseKeys = Util.GetText("UseKeys"),
}

Constants.PagodaMain =
{
	Nofirst = Util.GetText("Nofirst"),
	ElapsedTime = Util.GetText("ElapsedTime"),
	CurWave = Util.GetText("CurWave"),
	RewardTittle1 = Util.GetText("RewardTittle1"),
	RewardTittle2 = Util.GetText("RewardTittle2"),
	LevelEnough = Util.GetText("LevelEnough"),
	LevelNotEnough = Util.GetText("LevelNotEnough"),
	LevelNotEnoughTips = Util.GetText("LevelNotEnoughTips"),
	LockTips = Util.GetText("LockTips"),
}

Constants.Dungeon=
{
	LeaveDungeon = Util.GetText("LeaveDungeon"),
 	MyTeam = Util.GetText("MyTeam"),
 	QuickMatching = Util.GetText("QuickMatching"),
 	MustHaveTeam = Util.GetText("MustHaveTeam"),
 	Go = Util.GetText("Go"),
 	Back = Util.GetText("Back"),
 	MustSolo = Util.GetText("MustSolo"),
}

Constants.Vip=
{
	Get=Util.GetText("Vip_Get"),
	Day=Util.GetText("Vip_Day"),
	Off=Util.GetText("Vip_Off"),
	VIPLV=Util.GetText("Vip_VIPLV"),
}

Constants.Business=
{
	EnterError = Util.GetText("Business_EnterError"),
	GetSuccess = Util.GetText("Business_GetSuccess"),
}

Constants.ReLive=
{
	RebirthCoolingTime = Util.GetText("RebirthCoolingTime"),
	Remainder = Util.GetText("Remainder"),
}

Constants.PK={
	[0] = "普通",
	[1] = "押注",
	SilverTxt = "银两",
	DiamondTxt = "元宝"
}

Constants.System=
{
	GuildName = Util.GetText("GuildName"),
	NoGuild = Util.GetText("NoGuild"),
	PlayerName = Util.GetText("PlayerName"),
	ServerName = Util.GetText("ServerName"),
	PlayerId = Util.GetText("PlayerId"),
	ResetPos = Util.GetText("ResetPos"),
	Sticking = Util.GetText("Sticking"),
	Welcome = Util.GetText("Welcome"),
	OffLineTime = Util.GetText("OffLineTime"),
	Point = Util.GetText("Point"),
}

Constants.VirtualItems = {
	Copper = 1,
	Silver = 2,
	Diamond = 3,
	Exp = 4,
	Soul = 5,
	Contribution = 6,
	Exploit = 7,
	GoodFortune = 10,
}

Constants.AvatarPart = {
	Avatar_Body = 1,
	Avatar_Head = 2,
	Foot_Buff = 3,
	L_Hand_Buff = 4,
	R_Hand_Buff = 5,
	L_Hand_Weapon = 6,
	R_Hand_Weapon = 7,
	Rear_Weapon = 8,
	Chest_Buff = 9,
	Chest_Nlink = 10,
	Rear_Equipment = 11,
	Treasure_Equipment = 12,
	Ride_Avatar01 = 13,
	Head_Buff = 14,
	Equip_Buff = 15, 
    L_Hand_Weapon_Buff = 16,
    R_Hand_Weapon_Buff = 17, 
}

Constants.Layer = {
	UI = 5,
	STAGE_NAV = 8,
	CAGE = 9,             --场景中3D遮挡物层
	CG = 10,              --剧情动画层
	LightLayer   = 11,      --场景特殊光照层
	SelectableUnit = 12,    --场景中可触摸的单位.
	CharacterUnlit = 13, --接受平行光方向信息
	ColliderObject = 14, --用于隐藏遮挡玩家的场景物体.
	NpcLayer = 15,      --场景中的npc层
	SelfLayer = 16,      --场景中的npc层
}

Constants.FunctionUtil = {
	hasInScene = Util.GetText("hasInScene"),

}

Constants.LookPlayerInfo = {
	[1] = Util.GetText("LookPlayerInfo_1"),
	[2] = Util.GetText("LookPlayerInfo_2"),
	[3] = Util.GetText("LookPlayerInfo_3"),
	[4] = Util.GetText("LookPlayerInfo_4"),
	[5] = Util.GetText("LookPlayerInfo_5"),
	Level = Util.GetText("LookPlayerInfo_Level")
}

Constants.QuestCgLang = {
	[1] = Util.GetText("LookPlayerInfo_1"),
	[2] = Util.GetText("LookPlayerInfo_2"),
	[3] = Util.GetText("LookPlayerInfo_3"),
	[4] = Util.GetText("LookPlayerInfo_4"),
	[5] = Util.GetText("LookPlayerInfo_5"),
	Level = Util.GetText("LookPlayerInfo_Level")
}

Constants.PhotoText = {
	VipText = Util.GetText("PhotoText_VipText"),
}

Constants.GuideText=
{
	AutoBattle = Util.GetText("GuideText_AutoBattle"),
	MoveRock = Util.GetText("GuideText_MoveRock"),
	UseSkill = Util.GetText("GuideText_UseSkill"),
	UseOneBtn = Util.GetText("GuideText_UseOneBtn"),
	Understand = Util.GetText("GuideText_Understand"),
	UnderstandPractice = Util.GetText("GuideText_UnderstandPractice"),
	QuestClick = Util.GetText("GuideText_QuestClick"),
	SmithyClick = Util.GetText("GuideText_SmithyClick"),
	EffectClick = Util.GetText("GuideText_EffectClick"),
	ActiveClick = Util.GetText("GuideText_ActiveClick"),
	ActiveMiracleClick = Util.GetText("GuideText_ActiveMiracleClick"),
	BecomeDaddyClick = Util.GetText("GuideText_BecomeDaddyClick"),
	StoneInsetClick = Util.GetText("GuideText_StoneInsetClick"),
	LvUpClick = Util.GetText("GuideText_LvUpClick"),
	ClothClick = Util.GetText("GuideText_ClothClick"),
	ClothMiracleClick = Util.GetText("GuideText_ClothMiracleClick"),
	EquipClick = Util.GetText("GuideText_EquipClick"),
	TrainClick = Util.GetText("GuideText_TrainClick"),
	WingClick = Util.GetText("GuideText_WingClick"),
	BattleClick = Util.GetText("GuideText_BattleClick"),
	RecommendClick = Util.GetText("GuideText_RecommendClick"),
	ApplyClick = Util.GetText("GuideText_ApplyClick"),
	ActiveWardrobeClick = Util.GetText("GuideText_ActiveWardrobeClick"),
	GetWardrobeClick = Util.GetText("GuideText_GetWardrobeClick"),
	SelectCloth = Util.GetText("GuideText_SelectCloth"),
	SelectFootprint = Util.GetText("GuideText_SelectFootprint"),
	SelectStoneInset = Util.GetText("GuideText_SelectStoneInset"),
	SelectShenNong = Util.GetText("GuideText_SelectShenNong"),
	SelectLingMai = Util.GetText("GuideText_SelectLingMai"),
	SelectMedicine = Util.GetText("GuideText_SelectMedicine"),
	SelectWardrobe = Util.GetText("GuideText_SelectWardrobe"),
	OpenSkillMain = Util.GetText("GuideText_OpenSkillMain"),
	OpenSmithyMain = Util.GetText("GuideText_OpenSmithyMain"),
	OpenPracticeMain = Util.GetText("GuideText_OpenPracticeMain"),
	OpenWardrobeMain = Util.GetText("GuideText_OpenWardrobeMain"),
	OpenWingMain = Util.GetText("GuideText_OpenWingMain"),
	OpenMountMain = Util.GetText("GuideText_OpenMountMain"),
	OpenPartnerMain = Util.GetText("GuideText_OpenPartnerMain"),
	OpenJiaohuMain = Util.GetText("GuideText_OpenJiaohuMain"),
	OpenMiracleMain = Util.GetText("GuideText_OpenMiracleMain"),
	OpenMedicineMain = Util.GetText("GuideText_OpenMedicineMain"),
	CloseMenu = Util.GetText("GuideText_CloseMenu"),
	OpenActivityMain = Util.GetText("GuideText_OpenActivityMain"),
	OpenPagodaMain = Util.GetText("GuideText_OpenPagodaMain"),
	ShiMenSelect = Util.GetText("GuideText_ShiMenSelect"),
	ShiMenClick = Util.GetText("GuideText_ShiMenClick"),
	IslandSelect = Util.GetText("GuideText_IslandSelect"),
	IslandClick = Util.GetText("GuideText_IslandClick"),
	YushouSelect = Util.GetText("GuideText_YushouSelect"),
	YushouClick = Util.GetText("GuideText_YushouClick"),
	DungeonSelect = Util.GetText("GuideText_DungeonSelect"),
	DungeonClick = Util.GetText("GuideText_DungeonClick"),
	PagodaSelect = Util.GetText("GuideText_PagodaSelect"),
	PagodaClick = Util.GetText("GuideText_PagodaClick"),
	UseMiracleSkill = Util.GetText("GuideText_UseMiracleSkill"),
	ShiMenAccept = Util.GetText("GuideText_ShiMenAccept"),
	btn_effectClick = Util.GetText("GuideText_btn_effectClick"),
	FateClick = Util.GetText("GuideText_FateClick"),
	ScrollableClick = Util.GetText("GuideText_ScrollableClick"),
	ScrollableSelect = Util.GetText("GuideText_ScrollableSelect"),
	ScrollableEquip = Util.GetText("GuideText_ScrollableEquip"),
	SelectStoneCompose = Util.GetText("GuideText_SelectStoneCompose"),
	accept
	-- AutoBattle = '',
	-- MoveRock = '',
	-- UseSkill= '',
	-- UseOneBtn = '',
	-- Understand = '',
	-- QuestClick = '',
	-- SmithyClick = '',
	-- EffectClick = '',
	-- ActiveClick = '',
	-- BecomeDaddyClick = '',
	-- StoneInsetClick = '',
	-- LvUpClick = '',
	-- ClothClick = '',
	-- EquipClick = '',
	-- TrainClick = '',
	-- BattleClick = '',

	-- SelectCloth = '',
	-- SelectFootprint = '',
	-- SelectStoneInset = '',
	-- SelectShenNong = '',
	-- SelectLingMai = '',

	-- OpenSkillMain = '',
	-- OpenSmithyMain = '',
	-- OpenPracticeMain = '',
	-- OpenWardrobeMain = '',
	-- OpenWingMain = '',
	-- OpenMountMain = '',
	-- OpenPartnerMain = '',

	-- UseMiracleSkill = '',
	
}

Constants.Title = 
{
	takeOn = Util.GetText("Title_takeOn"),
	takeOff = Util.GetText("Title_takeOff"),
	switch = Util.GetText("Title_switch"),
	timeOver = Util.GetText("Title_timeOver"),
	timeForever = Util.GetText("Title_timeForever"),
	timeHour = Util.GetText("Title_timeHour"),
	already = Util.GetText("Title_already"),
	noEquipText = Util.GetText("noEquipText"),
}

Constants.Achievement =
{
	Total =  Util.GetText("AchievementTotal"),
	Hidden= Util.GetText("AchievementHidden"),
	GetReward= Util.GetText("AchievementGetReward"),
	AchievementCompletionTips= Util.GetText("AchievementCompletionTips"),
	AchievementTargetText= Util.GetText("AchievementTargetText"),	

}

Constants.OpeningEvent=
{
	TurnTime = Util.GetText("OpeningEvent_TurnTime"),
	TurnTimeMax = Util.GetText("OpeningEvent_TurnTimeMax"),
	TurnTimeLeft = Util.GetText("OpeningEvent_TurnTimeLeft"),
}

Constants.SpringFestival=
{
	NotEnoughCoin = Util.GetText("SpringFestival_NotEnoughCoin"),
	OpenWord = Util.GetText("SpringFestival_OpenWord"),
	NotOpenTime = Util.GetText("SpringFestival_NotOpenTime"),
	OpenTime = Util.GetText("SpringFestival_OpenTime"),
	MonthDayTime = Util.GetText("SpringFestival_MonthDayTime"),
	Pray = Util.GetText("SpringFestival_Pray"),
	PrayLimit = Util.GetText("SpringFestival_PrayLimit"),
	NoEnoughPray = Util.GetText("SpringFestival_NoEnoughPray"),
	LowLevel = Util.GetText("SpringFestival_LowLevel"),
	LevelToJoin = Util.GetText("SpringFestival_LevelToJoin"),
	ActivityEnd = Util.GetText("SpringFestival_ActivityEnd"),
}


Constants.BagPackUpCoolDownSec = 10
Constants.WarehoursePackUpCoolDownSec = 10
Constants.SellConfirmQuality = GlobalHooks.DB.GetGlobalConfig('sell_confirm_quality')