local _M = {}
_M.__index = _M

--UI不直接参与网络通信，通过model中转
local cjson = require "cjson"
local ChatModel = require 'Model/ChatModel'
local Util	  = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'
local ChatUtil  = require "UI/Chat/ChatUtil"
local ChatSend		  = require "UI/Chat/ChatSend"
local ItemModel = require 'Model/ItemModel'

local function clickActionCbfunction(index, data, self)
	-- body
	if index == 1 or index == 2 or index == 3 or index == 4 then			 --0 为做动作，1为发送鲜花，2为发送10朵 3，送屎 4 为送10坨屎  -1 为关闭
		
		ChatModel.interactRequest(index, data.playerId, data.name, function(param)
				-- body
				--print(PrintTable(param))
				GameUtil.ShowAssertBulider("/res/effect/60000_ui/vfx_ui_huaban.assetbundles")

			end)
	end

	if self.m_SelectFriendBtn ~= nil then
		self.m_SelectFriendBtn.IsChecked = false
	end
end

local function InteractiveMenuCb(id, data, self)
	if id == 12 then
		--做动作
		if self.m_curChannel == 0 then
			local lb_tishi = Util.GetText(TextConfig.Type.CHAT, 'message1')
			GameAlertManager.Instance:ShowFloatingTips(lb_tishi)
		else
			local selplayer = {}
			selplayer.s2c_playerId = data.playerId
			selplayer.s2c_name = data.name
			selplayer.s2c_level = data.lv
			selplayer.s2c_pro = data.pro
			ChatSend.MakeAction(self, selplayer)
		end
	elseif id == 23 then
		--送鲜花
		local node,lua_obj = GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUIChatGift, 0, 1)
		--lua_obj.hudCallBack = LuaHudChatCallBack
		lua_obj.clickActionCb = function(index)
			-- body
			clickActionCbfunction(index, data, self)
		end
	end
	if self.m_SelectFriendBtn ~= nil then
		self.m_SelectFriendBtn.IsChecked = false
	end
end

function _M.HandleClicKPerson(displayNode, data, pos, self)
	--local pt = displayNode:LocalToGlobal()
	local player_info1
	local type1
	if data.serverData ~= nil then
		 player_info1={
			name=data.serverData.s2c_name, lv=data.serverData.s2c_level,
			playerId = data.s2c_playerId,
			pro = data.serverData.s2c_pro,
			activeMenuCb = function(id, data)
				-- body
				InteractiveMenuCb(id, data, self)
			end,
		}   
	else
		player_info1={
			name=data.s2c_name, lv=data.s2c_level,
			playerId = data.s2c_playerId,
			pro = data.s2c_pro,
			activeMenuCb = function(id, data)
				-- body
				InteractiveMenuCb(id, data, self)
			end,
		}   
	end
	if data.s2c_playerId ~= DataMgr.Instance.UserData.RoleID then
		if self.m_curChannel == ChatModel.ChannelState.Channel_union or self.m_curChannel == ChatModel.ChannelState.Channel_group then
			type1=InteractiveMenu.TYPE_CHAT_AT
		else
			type1=InteractiveMenu.TYPE_CHAT
		end
	else
		type1=InteractiveMenu.TYPE_CHAT_SELF
	end
	EventManager.Fire("Event.ShowInteractive", {
		type= type1,
		player_info=player_info1,
		--x=pt.x,
		--y=pt.y
	})
end

-- local function HandleTeamMsg(data)
-- 	--print("-----------link---------------", data)
-- 	FubenUtil.onTeamLinkClick(data)
-- end

local function HandleGuildRecruit(data)
	GuildUtil.onGuildRecruitLinkClick(data)
end

-- local function HandleSendMapByIDXY(id, x, y, mapId, instanceId)
-- 	--print("------------", id)
-- 	print('HandleSendMapByIDXY:',id,x,y,mapId,instanceId)
-- 	local ret = GlobalHooks.DB.Find('Map', mapId)
-- 	local str 
-- 	if ret and ret.AllowedTransfer ~= 1  then
-- 		GameAlertManager.Instance:ShowNotify(Util.GetText(TextConfig.Type.MAP, "nochuansong"))
-- 		return
-- 	end
-- 	--mapId,posx,posy,instanceId,
-- 	-- local areaId = tonumber(id)
-- 	--print(id, x, y, mapId, instanceId, DataMgr.Instance.UserData.SceneId)
-- 	if mapId ~= DataMgr.Instance.UserData.MapID then
-- 		PlayerModel.ChangeAreaXYRequest(mapId, x, y, instanceId)
-- 	else
-- 		GameAlertManager.Instance:ShowNotify(Util.GetText(TextConfig.Type.MAP, 'mapxunlu'))
-- 		MenuMgrU.Instance:CloseAllHideMenu()
-- 		DataMgr.Instance.UserData:Seek(mapId, x, y)
-- 	end
-- end

local function HandleMonster(data)
	-- body
	local menu, ui = GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUIMonsterInfo, 0)
	ui:setMonster(data.id, data.lv, data.hard)
end

local function HandlePetMsg(data)
	-- body
	--print(PrintTable(data))
	PetModel.getPetInfoRequest(data.id, data.roleid, function(params)
		local menu, ui = GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUIPetInfoDes, 0)
		ui.setPetInfo(params, data.roleid)
	end)
end

local function HandleSkillMsg(data, pos)
	-- body
	--print("jineng")
	local menu, ui = GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUIPetSkillInfo, 0)
	ui.SetPetInfo(data)
	ui.SetPetPos(pos)
end

local function HandleMultiVSReport(data)
	print("HandleMultiVSReport")
	print(PrintTable(data))
	MultiVS.RequestMultiVSResult(data.id, data.result)
end

local function HandleGoto(data)
	-- print(PrintTable(data))
	if data.name == 'Event.Goto' then
		EventManager.Fire('Event.Goto',data.params)
	elseif data.name == 'AuctionByItemID' then
		local menu,obj = GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUIAuctionList, 0)
		-- 寄卖
		if obj then
			obj:Start(0,true)
			obj:StartSearch({id=data.id,cb=function ()
				-- 打开详情
				-- print('cb----StartSearch---------')
				obj:SelectItemByID(data.id)
			end})
		end		
	end
end

local function HandRedEnvelope(data)
	-- body
	--print(PrintTable(data))
	_M.OpenRedPacketRequest(tostring(data.id))
end

function _M.OpenRedPacketRequest(id)
	RedPacketModel.getRedPacketInfoRequest(id, function(params) 
		--print(PrintTable(params)) 
		DataMgr.Instance.MessageData:RemoveMessage(id, MessageData.MsgType.RedEnvelope)
		local node, lua_obj
		if params.openState == 0 then
			node, lua_obj = GlobalHooks.OpenUIOnlyOne(GlobalHooks.UITAG.GameUIChatRedEnvelopeOpen, 0)
		else
			node, lua_obj = GlobalHooks.OpenUIOnlyOne(GlobalHooks.UITAG.GameUIChatRedEnvelopeGet, 0)
		end
		lua_obj.SetDataParams(params, id)
	end)
	--[[
	local params = {}
	params.openState = 0
	local s2c_basePacket = {}
	s2c_basePacket.type = math.random(1, 2)
	s2c_basePacket.count = 2 --红包个数
	s2c_basePacket.money = 3 --普通红包：单个金额数量， 随机红包:总金额
	s2c_basePacket.descMsg = "居然有描述" --描述信息
	s2c_basePacket.password = "看你妹啊" --口令
	s2c_basePacket.channel = 6 --频道
	local sponsor = {} --发红包的土豪信息
	sponsor.id = 1 --id
	sponsor.pro = math.random(1, 5) --职业
	sponsor.name = "你的名字" --名字

	s2c_basePacket.sponsor = sponsor
	params.baseInfo = s2c_basePacket
	params.money = math.random(1, 10000)
	--]]

  
end

function _M.HandleOnLinkClick(link, info, text, displayNode, playerId, pos, self, parenttype)
	if link == nil or link == "" then
		return
	end
	--CommonUI.Sound.SoundManager.GetInstance():PlaySoundByKey("buttonClick")
	--print("--------------------------", link)
	local msg = cjson.decode(link)
	if msg.MsgType == ChatUtil.LinkType.LinkTypeItem then
		if msg.ID ~= nil  then 
			ItemModel.RequestDetailByID(msg.ID,function(detail)
				-- body
				UIUtil.ShowNormalItemDetail({detail = detail, autoHeight = true})
			end)
		elseif msg.TemplateId ~= nil then
			local itemdetail = ItemModel.GetDetailByTemplateID(msg.TemplateId)
			local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, autoHeight = true})
		end
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypePerson then	--人物
		--print("--------------------------", link)
		-- print(PrintTable(msg))
		if parenttype == "baozang" then
			GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUIVSPlayer, 0, msg.s2c_playerId)
		else
			_M.HandleClicKPerson(displayNode, msg, pos, self)
		end
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeSendPlace then
		--传送消息处理
		local ok = ConfigMgr.Instance.TxtCfg:GetTextByKey(TextConfig.Type.DEMUTATION, "ok_lua");
		local cancle = ConfigMgr.Instance.TxtCfg:GetTextByKey(TextConfig.Type.DEMUTATION, "cancel_lua")  
		GameAlertManager.Instance:ShowAlertDialog(
			AlertDialog.PRIORITY_NORMAL, 
			Util.GetText(TextConfig.Type.FRIEND,'transfer_confirm'),
			ok,
			cancle,
			nil,
			function()
				-- FriendModel.changeAreaByPlayerIdRequest(msg.id, 1, function(data)
				-- -- body
				-- end)
			end,
			nil
		)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeTeamShout then
		-- print_r('-----------link LinkTypeTeamShout msg', msg)
		local  playerId = msg.playerId
		if playerId == DataMgr.Instance.UserData.RoleID then
			--TODO
			GameAlertManager.Instance:ShowNotify(Util.GetText('team_already_in_team'))
			return
		end
		if DataMgr.Instance.TeamData.HasTeam then
			GameAlertManager.Instance:ShowNotify(Util.GetText('team_already_in_team'))
			return
		end
		local TeamModel = require 'Model/TeamModel'
		TeamModel.RequestInviteTeam(playerId,'chat',function()
			-- GameAlertManager.Instance:ShowNotify(Util.GetText('guild_applymsg'))
		end)

	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeGuildRecruit then
		--组队消息处理
		HandleGuildRecruit(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeMapMsg then
		--根据坐标传送
		--print(PrintTable(msg))
		-- HandleSendMapByIDXY(msg.data.areaId, msg.data.targetX, msg.data.targetY, msg.data.mapId, msg.data.instanceId)
		-- print_r('ChatUtil.LinkType.LinkTypeMapMsg: ',msg)

		local action = MoveEndAction()
		action.AimX = msg.data.targetX
		action.AimY =  msg.data.targetY
		action.MapUUid = msg.data.ZoneUUID
		action.MapId = msg.data.MapTemplateId
		if TLBattleScene.Instance.Actor then
			TLBattleScene.Instance.Actor:AutoRunByAction(action)
		end


	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeMonster then
		--处理怪物
		HandleMonster(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypePet then
		--处理宠物
		HandlePetMsg(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeSkill then
		--处理技能介绍
		local curpos = displayNode:ScreenToLocalPoint2D(pos)
		--print("--22222--",pos.position.x, pos.position.y,curpos.x,curpos.y, displayNode.Transform.localPosition.x, displayNode.Transform.localPosition.y)
		HandleSkillMsg(msg.data, displayNode.UnityObject.transform.parent:TransformPoint(Vector3.New(displayNode.Transform.localPosition.x + curpos.x, displayNode.Transform.localPosition.y - curpos.y, displayNode.Transform.localPosition.z)))
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeRecruit then
		local node, luaobj = GlobalHooks.OpenUI(GlobalHooks.UITAG.GameUITeamRecruit, 0, 1)
		luaobj.SetInfo(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeMultiVSReport then
		-- 5v5战报
		HandleMultiVSReport(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeGoto then
		-- 走goto
		HandleGoto(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeRedEnvelope then
		--红包
		HandRedEnvelope(msg.data)
	elseif msg.MsgType == ChatUtil.LinkType.LinkTypeGuildShout then
		-- print_r('-----------link LinkTypeGuildShout msg', msg)
		local  GuildId = msg.GuildId
		local GuildModel = require 'Model/GuildModel'
		GuildModel.ClientApplyGuildRequest(GuildId, function( ... )
			GameAlertManager.Instance:ShowNotify(Util.GetText('guild_applymsg'))
		end)
	else
		print("-----------link---------------", link)
	end
end

return _M
