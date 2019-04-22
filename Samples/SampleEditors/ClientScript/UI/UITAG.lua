--  tag = string or function
local RechargeModel=require('Model/RechargeModel')
local UITAG = {
  -- lua 写法 require path, xmlName, oninit
	Test = {'UI/Test/TestUI', 'xml/common/common_pipetips.gui.xml'},

	-- frame定义- 标签页菜单界面
	Test_Frame = {'UI/TagFrame','xml/attribute/ui_attribute_background.gui.xml',
		{
			-- Togglebutton-子UITag
			tbt_an1 = 'Test_tbt_an1',
			tbt_an2 = 'Test_tbt_an2',
			tbt_an3 = 'Test_tbt_an3',
			OnInit = function()
				-- Frame初始化之后会调用的接口
				print(' Test_Frame OnInit')
			end
		},
	},

	Test_tbt_an1 = {'UI/Test/Test_tbt_an1','xml/attribute/ui_attribute_main.gui.xml'},
	Test_tbt_an2 = {'UI/Test/Test_tbt_an2','xml/attribute/ui_attribute_prestige.gui.xml'},
	Test_tbt_an3 = {'UI/Test/Test_tbt_an2','xml/attribute/ui_attribute_message.gui.xml'},

	-----------------------------------------正式---------------------------------------------

	--数字框
	NumInput = {'UI/NumInput','xml/common/common_keypad.gui.xml'},

	--获得sth提示框
	AdvancedTips = {'UI/AdvancedTips','xml/common/common_remind.gui.xml'},

	--匹配弹框
	PlayerEnterMap = {'UI/PlayerEnterMap','xml/common/common_pipei.gui.xml'},
	-- 属性
	-- AttributeMain = {'UI/TagFrame','xml/attribute/ui_attribute_background.gui.xml',
	-- 	{
	-- 		-- Togglebutton-子UITag
	-- 		tbt_an1 = 'AttributeRoleFrame',
	-- 		tbt_an2 = 'AttributePrestige',
	-- 		tbt_an3 = 'AttributeInfoFrame',
	-- 		OnInit = function(self)
	-- 			-- Frame初始化之后会调用的接口
	-- 			print(' AttributeFrame OnInit')
	-- 			self.ui.menu.ShowType = UIShowType.HideBackMenu
	-- 		end
	-- 	},
	-- },
	AttributeMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'AttributeRoleFrame',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	AttributeRoleFrame = {'UI/Attribute/AttributeFrame','xml/attribute/ui_attribute_background.gui.xml'},
	AttributeRole = {'UI/Attribute/AttributeRole','xml/attribute/ui_attribute_role.gui.xml'},
	AttributeEquip = {'UI/Attribute/AttributeEquip','xml/attribute/ui_attribute_main.gui.xml'},
	AttributePrestige = {'UI/Attribute/AttributePrestige','xml/attribute/ui_attribute_prestige.gui.xml'},
	AttributeInfoFrame = {'UI/Attribute/AttributeFrame'},
	AttributeInfo = {'UI/Attribute/AttributeInfo','xml/attribute/ui_attribute_message.gui.xml'},

	-- 聊天 
	ChatMainSmall= {'UI/Chat/ChatMainSmall','xml/chat/ui_chat_backgrand_small.gui.xml'},
	ChatMain= {'UI/Chat/ChatMain','xml/chat/ui_chat_backgrand_big.gui.xml'},

	ChatUIFace= {'UI/Chat/ChatUIFace', 'xml/chat/ui_chat_emoji.gui.xml'},
	ChatUIAction= {'UI/Chat/ChatUIAction', 'xml/chat/ui_chat_action.gui.xml'},
	ChatUIHistory={'UI/Chat/ChatUIHistory', 'xml/chat/ui_chat_history.gui.xml'},
	ChatHudSetting={'UI/Chat/ChatHudSetting', 'xml/chat/ui_chat_choice.gui.xml'},
	ChatShowItem={'UI/Chat/ChatShowItem', 'xml/chat/ui_chat_itemshow.gui.xml'},
	ChatInvite = {'UI/Chat/ChatInvite','xml/chat/ui_chat_invite_siliao.gui.xml'},
	

	--社交
	SocialMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml','needBack', 
	{
		subui_1 = 'SocialFrame',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	SocialFrame = {'UI/TagFrame','xml/social/ui_social_background.gui.xml',
	{
		tbt_an1 = 'SocialFriend',
		tbt_an2 = 'SocialEnemy',
		tbt_an3 = 'SocialShow',
		OnInit = function (self)
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
		end
	}},
	SocialFriend = {'UI/Social/SocialFriend','xml/social/ui_social_friend.gui.xml'},
	SocialEnemy = {'UI/Social/SocialEnemy','xml/social/ui_social_enemy.gui.xml'},
	SocialGiftSelect = {'UI/Social/SocialGiftSelect','xml/common/common_alluse.gui.xml'},

	--邮件
	MailMain = {'UI/Mail/MailMain','xml/mail/ui_mail_background.gui.xml', 'needBack'},

	--铁匠铺
	--SmithyMain = {'UI/smithy/SmithyMain','xml/forge/ui_forge_background.gui.xml'},
	SmithyFrame = {'UI/TagFrame','xml/forge/ui_forge_background.gui.xml',
		{
		    tbt_an1 = 'SmithyStrengthen',
		    tbt_an2 = 'SmithyGemstone',
		    tbt_an3 = 'SmithyCompose',
		    tbt_an4 = 'SmithyIdentify',
			OnInit = function (self)
				self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
		    end
		},
	},
	
	SmithyMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'SmithyFrame',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	
	--强化
	SmithyStrengthen = {'UI/Smithy/SmithyStrengthen','xml/forge/ui_forge_intensify.gui.xml'},
	--强化特效
	SmithStrengthenEffect = {'UI/Smithy/SmithStrengthenEffect','xml/forge/ui_forge_effectshow.gui.xml'},
	--鉴定
	SmithyIdentify = {'UI/Smithy/SmithyIdentify','xml/forge/ui_forge_authenticate.gui.xml'},
	--宝石镶嵌
	SmithyGemstone = {'UI/Smithy/SmithyGemstone','xml/forge/ui_forge_inlay.gui.xml'},
	--合成
	SmithyCompose = {'UI/Smithy/SmithyCompose','xml/forge/ui_forge_synthetic.gui.xml'},
	
	--充值面板
	Recharge = {'UI/UIFrame','xml/common/common_denglong.gui.xml','needBack',
			{
				subui_1 = 'RechargeFrame',
				subui_2 = 'Money',
				OnInit = function(self)
					-- Frame初始化之后会调用的接口
					self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
					self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveDown)
				end
			}},
	
	--充值系统
	RechargeFrame = {'UI/TagFrame','xml/recharge/ui_recharge_background.gui.xml', 
					 {
						   tbt_an1 = 'RechargeShop',
						   tbt_an2 = 'RechargePay',
						   tbt_an3 = 'RechargeGift',
						   tbt_an4 = 'RechargeVip',
						   tbt_an5 = 'RechargeShopSilver',
						   OnInit = function (self)
							   self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
								   RechargeModel.RequestRechargeInfo(RechargeModel.GetRechargePlatformID(false),function(rsp)
									   RechargeModel.BuyInfo=rsp.s2c_data
									   local params={
										   buycount=rsp.s2c_data
									   }
									   EventManager.Fire('Event.Recharge.RechargeUIInit',params)
								   end)
						   end
				   }, 
	},
	--充值元宝
	RechargePay={'UI/Recharge/RechargePay','xml/recharge/ui_recharge_pay.gui.xml'},
	--充值商店元宝
	RechargeShop={'UI/Recharge/RechargeShop','xml/recharge/ui_recharge_shop.gui.xml'},
    --充值商店银两
	RechargeShopSilver={'UI/Recharge/RechargeShop','xml/recharge/ui_recharge_shop.gui.xml'},
	--充值vip
	RechargeVip={'UI/Recharge/RechargeVip','xml/recharge/ui_recharge_vip.gui.xml'},
	--充值礼包
	RechargeGift={'UI/Recharge/RechargeGift','xml/recharge/ui_recharge_libao.gui.xml'},
	--首充奖励
	FirstRecharge={'UI/Recharge/FirstRecharge','xml/hud/ui_hud_firstrecharge.gui.xml'},
	--仙盟商店
	RechargeGuild = {'UI/Recharge/RechargeShop','xml/recharge/ui_recharge_shop.gui.xml'},
	
	-- StoreFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 
	-- {
	-- 	subui_1 = 'StoreMain',
	-- 	subui_2 = 'Money',
	-- 	OnInit = function(self)
	--  		-- Frame初始化之后会调用的接口
	-- 		self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	-- 		self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
	-- 	end
	-- }},
 --    -- 商城
 --  	StoreMain = {'UI/TagFrame','xml/store/ui_store_background.gui.xml',
	-- {
	-- 	tbt_an1 = 'Recharge',
	-- 	tbt_an2 = 'Shop',
	-- 	tbt_an3 = 'Prestige',
	-- 	OnInit = function (self)
	-- 		-- body
	-- 		print ('Store OnInit')
	-- 	end
	-- }},

	-- Recharge = {'UI/Store/Recharge','xml/store/ui_store_recharge.gui.xml'},
	-- Prestige = {'UI/Store/Prestige','xml/store/ui_store_prestige.gui.xml'},
	ShopFrame = {'UI/Store/Shop','xml/store/ui_store_shop.gui.xml'},

	Shop = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 
	{
		subui_1 = 'ShopFrame',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},



	SmallMapFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack', 
	{
		subui_1 = 'SmallMapMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	SceneMap = {'UI/SmallMap/SceneMap','xml/map/ui_map_scene.gui.xml'},
	WorldMap = {'UI/SmallMap/WorldMap','xml/map/ui_map_world.gui.xml'},
	
    --  小地图
  	SmallMapMain = {'UI/TagFrame','xml/map/ui_map_background.gui.xml',
	{
		tbt_an1 = 'SceneMap',
		tbt_an2 = 'WorldMap',
		OnInit = function (self)
			
		end
	}},

	PlayMap = {'UI/SmallMap/PlayMap','xml/map/ui_map_play.gui.xml','needBack'},

	-- 神器
	MiracleFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml','needBack',
	{
		subui_1 = 'MiracleMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},


	MiracleMain = {'UI/Miracle/MiracleMain','xml/miracle/ui_miracle.gui.xml'},

	-- 坐骑
	MountFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml','needBack', 
	{
		subui_1 = 'MountMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	
	MountMain = {'UI/TagFrame','xml/mount/ui_mount_backgroud.gui.xml',
		{
			tbt_an1 = 'MountInfo',
			tbt_an2 = 'MountVeins',
			OnInit = function ( ... )
				-- body
				print('MountUI OnInit')
			end
		},
	},
 
	MountInfo = {'UI/Mount/MountInfo','xml/mount/ui_mount_info.gui.xml'},
	MountVeins = {'UI/Mount/MountVeins','xml/mount/ui_mount_pulse.gui.xml'},


	IslandMain = {'UI/Island/IslandMain','xml/island/ui_island_main.gui.xml', 'needBack'},

 	--资源追回
	GetBackFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'GetBackMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	GetBackMain = {'UI/GetBack/GetBackMain','xml/getback/ui_getback_background.gui.xml'},

	-- 技能
	SkillFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml','needBack',
	{
		subui_1 = 'SkillMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	SkillMain = {'UI/Skill/SkillMain','xml/skill/ui_skill_background.gui.xml'},

    --npc对话框
	UINpcTalk = {'UI/Quest/UINpcTalk','xml/mission/ui_mission_receive.gui.xml'},

	DialogueTalk = {'UI/Quest/DialogueTalk','xml/mission/ui_mission_receive.gui.xml'},

	ItemDetail = {'UI/Bag/ItemDetail','xml/package/ui_tips.gui.xml'},
	ItemGetWay = {'UI/Bag/ItemGetWay','xml/common/common_itemget.gui.xml'},


	Money = {'UI/Bag/Money','xml/package/ui_money.gui.xml'},

	--背包

	BagMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'RoleBag',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	RoleBag = {'UI/Bag/RoleBag','xml/package/ui_background.gui.xml'},
	RoleBagItem = {'UI/Bag/RoleBagItem','xml/package/ui_package.gui.xml'},
	RoleBagWarehourse = {'UI/Bag/RoleBagWarehourse','xml/package/ui_storage.gui.xml'},
	RoleBagDecompose = {'UI/Bag/RoleBagDecompose','xml/package/ui_decompose.gui.xml'},
	RoleBagBatchUse = {'UI/Bag/RoleBagBatchUse','xml/common/common_alluse.gui.xml'},
	AutoUseItem = {'UI/Bag/AutoUseItem','xml/common/common_use.gui.xml'},
	--任务

	RoleBagFate = {'UI/Fate/RoleBagFate','xml/fate/ui_fate_background.gui.xml'},
	FateMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'RoleBagFate',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	

	QuestMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'QuestUI',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	QuestUI = {'UI/Quest/QuestUI', 'xml/mission/ui_mission_background.gui.xml'},

    --翅膀
	WingFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'Wing',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	Wing = {'UI/Wing/WingMain','xml/wings/ui_wings_main.gui.xml'},

	--公会
	GuildMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack', 
	{
		subui_1 = 'GuildFrame',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	GuildFrame = {'UI/TagFrame','xml/guild/ui_guild_background.gui.xml',
		{
		    tbt_an1 = 'GuildInfo',
		    tbt_an2 = 'GuildMember',
		    tbt_an3 = 'GuildTalent',
		    tbt_an4 = 'GuildActivity',
		    tbt_an5 = 'RechargeGuild',
		    -- tbt_an5 = { tag = 'RechargeGuild', arg = { shopKey = 'guild_storeid' } },
		    OnInit = function ( self )
				self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
		    end
		},
    },
	GuildList = {'UI/Guild/GuildList', 'xml/guild/ui_guild_applyui.gui.xml', 'needBack'},
	GuildCreate = {'UI/Guild/GuildCreate', 'xml/guild/ui_guild_found.gui.xml', 'needBack'},
	GuildInfo   = {'UI/Guild/GuildInfo', 'xml/guild/ui_guild_message.gui.xml'},
	GuildMember = {'UI/Guild/GuildMember', 'xml/guild/ui_guild_member_data.gui.xml'},
	GuildTalent = {'UI/Guild/GuildTalent', 'xml/guild/ui_guild_skill.gui.xml'},
	GuildActivity = {'UI/Guild/GuildActivity', 'xml/guild/ui_guild_activity.gui.xml'},
	GuildApplyList = {'UI/Guild/GuildApplyList', 'xml/guild/ui_guild_applylist.gui.xml'},
	GuildCondition = {'UI/Guild/GuildCondition', 'xml/guild/ui_guild_apply_condition.gui.xml'},
	GuildNotice = {'UI/Guild/GuildNotice', 'xml/guild/ui_guild_notice.gui.xml'},
	GuildDonate = {'UI/Guild/GuildDonate', 'xml/guild/ui_guild_donate.gui.xml', 'needBack'},
	GuildBuild = {'UI/Guild/GuildBuild', 'xml/guild/ui_guild_jianzhu.gui.xml', 'needBack'},
	GuildPosition = {'UI/Guild/GuildAuthority', 'xml/guild/ui_guild_duty.gui.xml', 'needBack'},
	GuildGift = {'UI/Guild/GuildGift', 'xml/guild/ui_guild_gift.gui.xml', 'needBack'},
	GuildMonster = {'UI/Guild/GuildMonster', 'xml/guild/ui_guild_monster.gui.xml', 'needBack'},
	GuildAttack = {'UI/Guild/GuildAttack', 'xml/guild/ui_guild_break.gui.xml', 'needBack'},
	GuildWant = {'UI/Guild/GuildWant','xml/guild/ui_guild_wanted.gui.xml', 'needBack'},    
	GuildCarriage = {'UI/Guild/GuildCarriage','xml/guild/ui_guild_dart.gui.xml', 'needBack'},
	GuildFortInfo = {'UI/Guild/GuildFortInfo','xml/guild/ui_guild_fortfiedpoint.gui.xml', 'needBack'},
    UIStoryTip = {'Story/UIStoryTip','xml/mission/ui_mission_talk.gui.xml'},

	UISubmitItem = {'UI/Quest/UISubmitItem','xml/mission/ui_mission_give.gui.xml'},
	UICustomSubmitItem = {'UI/Quest/UICustomSubmitItem','xml/mission/ui_mission_tijiao.gui.xml'},
	ItemUseUI = {'UI/Common/ItemUseUI','xml/common/common_use.gui.xml', 'needBack'},
	--交互菜单
	InteractiveMenuUI = {'UI/Common/InteractiveMenuUI','xml/common/common_menu.gui.xml', 'needBack'},
	CustomButtonLayout = {'UI/Common/CustomButtonLayout','xml/common/common_menu.gui.xml', 'needBack'},

	--技能Tips
	UINuqiTip={'UI/Hud/NuqiTipsMain','xml/hud/ui_hud_nuqi.gui.xml'},


	-- 队伍
	TeamFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml',  'needBack',
	{
		subui_1 = 'TeamMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	TeamMain = {'UI/TagFrame','xml/team/ui_team_background.gui.xml', 
	{
		tbt_an1 = 'TeamInfo',
		tbt_an2 = 'TeamPlatform',
		OnInit = function ( self )
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
		end
	}},





	TeamInfo = {'UI/Team/TeamInfo','xml/team/ui_team_main.gui.xml'},
	TeamInvite = {'UI/Team/TeamInvite','xml/team/ui_team_invite.gui.xml'},
	TeamTarget = {'UI/Team/TeamTarget','xml/team/ui_team_target.gui.xml'},
	TeamApply = {'UI/Team/TeamApply','xml/team/ui_team_applylist.gui.xml'},
	TeamPlatform = {'UI/Team/TeamPlatform','xml/team/ui_teamplatform.gui.xml'},
	MatchInfo = {'UI/Team/MatchInfo','xml/common/common_pipetips.gui.xml'},

	--战场界面
	BattleGround = {'UI/Pvp/BattleGround','xml/battleground/ui_battleground_background.gui.xml', 'needBack',},
	Carriagetext = {'UI/Pvp/Carriagetext','xml/carriage/ui_carriage_person_explain.gui.xml','needBack',},
	Transporter = {'UI/Pvp/Transporter','xml/carriage/ui_carriage_person.gui.xml', 'needBack'},
	
	EnterMapPopup = {'UI/Pvp/EnterMapPopup','xml/battleground/ui_battleground_go.gui.xml'},
	
	--新仙侣---------------------------
	PartnerFrame  = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack',
	{
		subui_1 = 'Partner',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	Partner = {'UI/TagFrame','xml/partner/ui_partner_background.gui.xml',
					 {
						 tbt_an1 = 'PartnerMain',
						 tbt_an2 = 'PartnerEntanglement',
						 OnInit = function (self)
							 self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
						 end
					 },
	},
	
	PartnerMain = {'UI/Partner/FallenPartner','xml/partner/ui_partner_main.gui.xml'},
	PartnerEntanglement={'UI/Partner/PartnerEntanglement','xml/partner/ui_partner_together.gui.xml'},
	PartnerPreview = {'UI/Partner/FallenPartnerPreview','xml/partner/ui_partner_show.gui.xml'},
	PartnerRankUp = {'UI/Partner/FallenPartnerRankUp','xml/partner/ui_partner_up.gui.xml'},
	-- PartnerMain = {'UI/TagFrame','xml/partner/ui_partner_background.gui.xml',
	-- 	{
	-- 	    tbt_an1 = 'PartnerInfoFrame',
	-- 	    tbt_an2 = 'PartnerBreakFrame',
	-- 	    tbt_an3 = 'PartnerSkillUpFrame',
	-- 	    tbt_an4 = 'PartnerFate',
	-- 	    tbt_an5 = 'PartnerPokedex',
	-- 	    OnInit = function (self)
	-- 	        -- body
	-- 	        self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
	-- 	    end
	-- 	},
 --    },
 --    PartnerList = {'UI/Partner/PartnerList','xml/partner/ui_partner_list.gui.xml'},
 --    PartnerShow = {'UI/Partner/PartnerShow','xml/partner/ui_partner_show.gui.xml'},
 --    PartnerInfoFrame = {'UI/Partner/PartnerMain'},
	-- PartnerBreakFrame = {'UI/Partner/PartnerMain'},
	-- PartnerSkillUpFrame = {'UI/Partner/PartnerMain'},
	-- PartnerInformation = {'UI/Partner/PartnerInformation','xml/partner/ui_partner_info.gui.xml'},
	-- PartnerBreakThrough = {'UI/Partner/PartnerBreakThrough','xml/partner/ui_partner_break.gui.xml'},
	-- PartnerSkillUp = {'UI/Partner/PartnerSkillUp','xml/partner/ui_partner_skill.gui.xml'},
	-- PartnerFate = {'UI/Partner/PartnerFate','xml/partner/ui_partner_yoke.gui.xml'},
	-- PartnerPokedex = {'UI/Partner/PartnerPokedex','xml/partner/ui_partner_pokedex.gui.xml'},

	--修行之道
    PracticeMain = {'UI/Practice/PracticeMain','xml/practice/ui_practice.gui.xml', 'needBack'},
    PracticeHelp = {'UI/Practice/PracticeHelp','xml/practice/ui_practice_help.gui.xml', 'needBack'},
    PracticeReward = {'UI/Practice/PracticeReward','xml/practice/ui_practice_reward.gui.xml', 'needBack'},

	--衣柜
	WardrobeMain = {'UI/Wardrobe/WardrobeMain','xml/wardrobe/ui_main.gui.xml','needBack'},
	WardrobeDetial = {'UI/Wardrobe/WardrobeDetial','xml/wardrobe/ui_detail.gui.xml','needBack'},
    --回忆录
    MemoirsFrame  = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 
	{
		subui_1 = 'MemoirsMain',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
		end
	}},
 	MemoirsMain = {'UI/Memoirs/MemoirsMain','xml/sanshenglu/ui_sanshenglu_main.gui.xml'},
  	ChapterMain = {'UI/Common/ChapterMain','xml/common/common_chapter.gui.xml'},
 	--吃药设置界面.
 	MedicineMain = {'UI/Medicine/MedicineMain','xml/automedicine/ui_automedicine.gui.xml'},
	
	--血池
	MedicinePoolMain = {'UI/Medicine/MedicinePoolMain','xml/automedicine/ui_automedicine_pool.gui.xml'},
	
 	--仙丹界面.
 	MedicineXianDan = {'UI/Medicine/MedicineXianDan','xml/automedicine/ui_automedicine_xiandan.gui.xml'},
	--血池
	MedicinePool = {'UI/Medicine/MedicinePool','xml/automedicine/ui_automedicine_pool.gui.xml'},


 	--头顶debuffList
 	ShowBuffListMain = {'UI/Hud/ShowBuffListMain','xml/hud/ui_hud_bufflist.gui.xml'},

 	--装备自动检测
 	AutoEquipsMain = {'UI/Common/AutoEquipsMain','xml/common/common_quickuse.gui.xml'},

 	--活力系统
 	ActivityMain= {'UI/Activity/ActivityMain','xml/activity/ui_activity.gui.xml','needBack'},

    --活力推送
 	ActivityPushMain = {'UI/Hud/ActivityPushMain','xml/hud/ui_hud_activity_push.gui.xml','needBack'},
	
    --副本面板
    DungeonMain={'UI/Dungeon/DungeonMain','xml/fuben/ui_fuben_main.gui.xml','needBack'},
    DungeonPass={'UI/Dungeon/DungeonPass','xml/fuben/ui_fuben_pass.gui.xml'},
    DungeonFail={'UI/Dungeon/DungeonFail','xml/fuben/ui_fuben_fail.gui.xml'},
    --DungeonResult={'UI/Dungeon/DungeonResult','xml/common/common_result.gui.xml'},
    --副本和镇妖塔细节面板
    DungeonAndPagodaDetail={'UI/Dungeon/DungeonAndPagodaDetail','xml/fuben/ui_fuben_hud.gui.xml'},


    --战斗力面板
    PowerMain={'UI/Power/PowerEffectMain','xml/hud/ui_hud_fightup.gui.xml'},

   --系统设置
    SystemBackGround={'UI/UIFrame', 'xml/common/common_denglong.gui.xml','needBack',
       {			
       		subui_1='SystemSetFrame',
       	 	OnInit=function(self)	
       			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
				self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
			end
   		}},
    SystemSetFrame={'UI/TagFrame','xml/system/ui_background.gui.xml', 'needBack',
		{	
			tbt_system='SystemSetting',
			tbt_person='PersonSetting',
				OnInit=function(self)
			end	
		}},
	SystemSetting={'UI/System/SystemHud','xml/system/ui_system.gui.xml'},
	PersonSetting={'UI/System/PersonHud','xml/system/ui_person.gui.xml'},

	--排行榜
	RankMain = {'UI/Rank/RankMain','xml/rank/ui_ranklist.gui.xml','needBack'},

	--锁妖塔
	PagodaMain = {'UI/Tower/PagodaMain','xml/tower/ui_tower_main.gui.xml','needBack'},
	PagodaChest = {'UI/Tower/PagodaChest','xml/tower/ui_tower_chest.gui.xml'},
	PagodaStory = {'UI/Tower/PagodaStory','xml/tower/ui_tower_story.gui.xml'},
	PagodaRewardPreview = {'UI/Tower/PagodaRewardPreview','xml/tower/ui_tower_reward.gui.xml'},
	-- PagodaTips = {'UI/Tower/PagodaTips','xml/tower/ui_tower_itemshow.gui.xml'},
	DoubleTower = {'UI/Tower/DoubleTower','xml/tower/ui_tower_cp.gui.xml','needBack'},

	--功能开放
	FuncOpen = {'UI/Hud/FuncOpenUI','xml/hud/ui_hud_openfun.gui.xml'},

	--普通复活
	ReliveMain={'UI/Hud/ReliveMain','xml/hud/ui_hud_revive2.gui.xml'},
	--变强复活
	ReliveWithStrongMain={'UI/Hud/ReliveWithStrongMain','xml/hud/ui_hud_revive1.gui.xml'},

	--镇妖塔&仙灵岛面板
	SceneStage={'UI/Hud/SceneStage','xml/hud/ui_hud_stage.gui.xml'},

	--目标系统
	TargetDetail={'UI/Target/TargetDetail','xml/target/ui_target_detail.gui.xml'},
	
    --变强系统
    BeStrongMain={'UI/BeStrong/BeStrongMain','xml/bestrong/ui_bestrong_main.gui.xml','needBack'},

    --活动系统
    BusinessFrame = {'UI/Business/BusinessFrame','xml/business/ui_business_welfare.gui.xml','needBack',2},
    NewOpenFrame = {'UI/Business/BusinessFrame','xml/newopen/ui_newopen_background.gui.xml','needBack',1},
  	
  	--世界boss
    WorldBossMain={'UI/WorldBoss/WorldBossMain','xml/worldboss/ui_worldboss_main.gui.xml','needBack'},
    WorldBossDamage={'UI/WorldBoss/WorldBossDamage','xml/hud/ui_hud_worldboss.gui.xml'},

    --神兽陪练
    MythicalBeastsBoss={'UI/Hud/MythicalBeastsBoss','xml/hud/ui_hud_beasts_one.gui.xml'},
	MythicalBeastsMonster= {'UI/Hud/MythicalBeastsMonster','xml/hud/ui_hud_beasts.gui.xml'},

    --日常核心副本
    DailyDungeonMain={'UI/DailyDungeon/DailyDungeonMain','xml/fuben_daily/ui_fuben_daily_main.gui.xml','needBack'},
    DailyDungeonHud={'UI/DailyDungeon/DailyDungeonHud','xml/fuben_daily/ui_fuben_daily_hud.gui.xml'},

    --查看玩家信息
    LookPlayerInfo={'UI/Attribute/LookPlayerInfo','xml/attribute/ui_attribute_information.gui.xml','needBack'},

    --委托板
    ActivityEntrust = {'UI/Activity/ActivityEntrust','xml/activity_limit/ui_activity_limit_entrust.gui.xml','needBack'},

    --凤凰宝库
    PhoenixTreasure={'UI/Phoenix/PhoenixMain','xml/activity_limit/ui_activity_limit_treasury.gui.xml'},

    --离线经验
    OffLineExp={'UI/OffLineExp/OffLineMain','xml/offline/ui_offline.gui.xml'},
	
	--师门身份赛
	PlayRuleMain={'UI/PlayRule/PlayRuleMain','xml/guild/ui_guild_identitybattle.gui.xml','needBack'},

	
	TitleFrame = {'UI/UIFrame','xml/common/common_denglong.gui.xml','needBack', 
	{
		subui_1 = 'TitleMain',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},

	TitleMain = {'UI/Title/TitleMain','xml/title/ui_title_main.gui.xml'},
	GetTitle = {'UI/Title/GetTitle','xml/title/ui_title__tips.gui.xml'},

	--拍卖行
	AuctionMain = {'UI/UIFrame','xml/common/common_denglong.gui.xml', 'needBack', 
	{
		subui_1 = 'AuctionFrame',
		subui_2 = 'Money',
		OnInit = function(self)
	 		-- Frame初始化之后会调用的接口
			self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
			self.ui.menu:SetCompAnime(self.ui.menu.mRoot, UIAnimeType.FadeMoveRight)
		end
	}},
	AuctionFrame = {'UI/TagFrame','xml/exchange/ui_exchange_background.gui.xml',
		{
		    tbt_an1 = 'AuctionList',
		    tbt_an2 = 'AuctionShelves',
		    OnInit = function ( self )
				self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
		    end
		},
    },
	AuctionList = {'UI/Auction/AuctionList', 'xml/exchange/ui_exchange_buy.gui.xml'},
	AuctionShelves = {'UI/Auction/AuctionShelves', 'xml/exchange/ui_exchange_sell.gui.xml'},
	AuctionBuy = {'UI/Auction/AuctionBuy', 'xml/exchange/ui_exchange_get.gui.xml', 'needBack'},
	AuctionPut = {'UI/Auction/AuctionPut', 'xml/exchange/ui_exchange_putaway.gui.xml', 'needBack'},
	AuctionRecord = {'UI/Auction/AuctionRecord', 'xml/exchange/ui_exchange_extract.gui.xml', 'needBack'},
	
	--太虚幻境
	TaiXuMain = {'UI/TaiXu/TaiXuMain', 'xml/taixu/ui_taixu_Illusory.gui.xml','needBack'},
	
	--获得物品
	GainItem = {'UI/Common/GainItemUI','xml/common/common_acquire.gui.xml'},
	
	--查看物品
	PreviewItem={'UI/Common/PreviewItemUI','xml/common/common_check.gui.xml'},
	
	--师门/仙盟循环任务
	TaskLoop={'UI/Hud/TaskLoop','xml/hud/ui_hud_taskloop.gui.xml'},

	GetTreasureUI = {'UI/Hud/GetTreasureUI','xml/hud/ui_hud_getitem.gui.xml'},
	
	--功能提升
	AbilityUpMain={'UI/Hud/AbilityUpMain','xml/hud/ui_hud_up.gui.xml'},
	
	--省电模式
	SavingPower={'UI/Hud/SavingPower','xml/hud/ui_hud_lowpower.gui.xml'},
	
	--等级封印
	LevelSeal={'UI/Hud/LevelSeal','xml/hud/ui_hud_levelseal.gui.xml'},
	
	--成就
	AchievementMain={'UI/Achievement/AchievementMain','xml/achievement/ui_achievement.gui.xml','needBack'},
	AchievementTip={'UI/Achievement/AchievementTip','xml/achievement/ui_achievementtip.gui.xml'},
	
	--伤害统计
	DamageRecount={'UI/Hud/DamageRecount','xml/hud/ui_hud_damage_rank.gui.xml'},
	
	--双飞塔结算
	DoubleFlyResult={'UI/Tower/DoubleFlyResult','xml/tower/ui_tower_result.gui.xml'},
	
	--名片功能
	SocialShow={'UI/Social/SocialShow','xml/social/ui_social_photo.gui.xml'},
	SocialWatch={'UI/Social/SocialWatch','xml/social/ui_social_wacth.gui.xml',"needBack"},
	
	--开服活动
	SevenDay={'UI/Business/SevenDay','xml/newopen/ui_newopen_sevenday.gui.xml','needBack'},
	
	--上线活动推送Tips
	FirstOnlineShow={'UI/Common/FirstOnlineShow','xml/business/ui_business_onlinetip.gui.xml','needBack'},
	
	--春节活动
	SpringFestivalMain={'UI/SpringFestival/SpringFestivalMain','xml/business/ui_business_springfestival.gui.xml','needBack'},
	SpringFestivalStore={'UI/SpringFestival/SpringFestivalStore','xml/business/ui_business_fubistore.gui.xml','needBack'},
	--方便调试，可直接打开
	worldboss = {'UI/SpringFestival/SpringFestivalWorldboss','xml/business/ui_business_nianshou.gui.xml'},

	--情人节活动
	CPActivityFrame={'UI/Business/CPActivityFrame','xml/business/ui_business_valentinesday.gui.xml','needBack'},	
	
	--社区
	CommunityMain={'UI/Common/CommunityMain','xml/hud/ui_hud_shequ.gui.xml','needBack'},
	
	--经脉
	MeridiansMain={'UI/Meridians/MeridiansMain','xml/meridians/ui_meridians.gui.xml','needBack'},
	
	--擂台赛
	PKCostMain={'UI/PKCost/PKCostMain','xml/anrenacontest/ui_arena_startfight.gui.xml','needBack'},
	PKed = {'UI/PKCost/PKed','xml/anrenacontest/ui_arena_receivefight.gui.xml','needBack'},
	WaitPK = {'UI/PKCost/WaitPK','xml/hud/ui_hud_arenatip.gui.xml'},
	
	--改名卡
	RenamePerson = {'UI/Rename/RenamePerson','xml/hud/ui_hud_changename_person.gui.xml'},
	RenameGuild = {'UI/Rename/RenameGuild','xml/hud/ui_hud_changename_guild.gui.xml'},

	--結婚
	MarryApply = {'UI/Marry/MarryApply','xml/marry/ui_marry_wedding.gui.xml','needBack'},
	MarryHelp = {'UI/Marry/MarryHelp','xml/marry/ui_marry_marrytip.gui.xml','needBack'},
	MarryInvite = {'UI/Marry/MarryInvite','xml/marry/ui_marry_invitation.gui.xml','needBack'},
	WeddingReserve = {'UI/Marry/WeddingReserve','xml/marry/ui_marry_reserve.gui.xml','needBack'},
	WeddingInfo = {'UI/Marry/WeddingInfo','xml/marry/ui_marry_invitation_check.gui.xml','needBack'},
	DivorceApply = {'UI/Marry/DivorceApply','xml/marry/ui_marry_divorce.gui.xml','needBack'},
	DivorceHelp = {'UI/Marry/DivorceHelp','xml/marry/ui_marry_divorcetip.gui.xml','needBack'},
	MarryWarehouse = {'UI/Marry/MarryWarehouse','xml/package/ui_marrystorage.gui.xml',},
	MarrySuccess = {'UI/Marry/MarrySuccess','xml/marry/ui_marry_proposeresult.gui.xml','needBack'},

	
	-----------------------------------------csharp 写法---------------------------------------------
    
	-- csharp 写法
	-- EnterGameMenu = {EnterGameMenu.Create,'参数'}
}

GlobalHooks.UI.UITAG = UITAG
