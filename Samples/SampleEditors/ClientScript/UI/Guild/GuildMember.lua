local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local Helper = require 'Logic/Helper.lua'
local ItemModel = require 'Model/ItemModel'
local ChatUtil = require 'UI/Chat/ChatUtil'
local ChatModel = require "Model/ChatModel"
local CDExt = require 'Logic/CDExt'

local function OnFuncMenuClick( self, key, playerId, data )
	local dbPosition = unpack(GlobalHooks.DB.Find('guild_position', { position_id = data.position }))
	if key == 'EventGuildLeave' then	--退出公会
		local content = Util.GetText('guild_quit_confirm')
		GameAlertManager.Instance.AlertDialog:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, '', '', nil, 
		    function(parma)
				local guildName = DataMgr.Instance.UserData.GuildName
				GuildModel.ClientQuitGuildRequest(DataMgr.Instance.UserData.RoleID, function( rsp )
		            GameAlertManager.Instance:ShowNotify(Util.GetText('guild_out', guildName))
				end)
		    end, nil)
	elseif key == 'EventGuildKickOut' then	--踢人
		GuildModel.ClientQuitGuildRequest(playerId, function( rsp )
			_M.RefreshList(self)
		end)
	elseif key == 'EventGuildTranser' then	--转让会长
		local content = Util.GetText('guild_transfer_confirm', data.name)
		GameAlertManager.Instance.AlertDialog:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, '', '', nil, 
		    function(parma)
				GuildModel.ClientTransferGuildRequest(playerId, function( rsp )
		    		GameAlertManager.Instance:ShowNotify(Util.GetText('guild_transfer_president_success'))
					_M.RefreshList(self)
				end)
		    end, nil)
	elseif key == 'EventGuildPosition' then	--修改职位
		local params = {tarId = data.roleId, selfPosition = self.position, tarPosition = dbPosition.position_id, cb = function( position )
			data.position = position
			self.pan:RefreshShowCell()
		end}
    	GlobalHooks.UI.OpenUI("GuildPosition", 0, params)
	elseif key == 'EventGuildImpeach' then	--弹劾会长
    	local sec = TimeUtil.TimeLeftSec(data.leaveTime)
    	local impeachTime = GlobalHooks.DB.GetGlobalConfig('guild_impeachment')
		-- if sec <= impeachTime * 24 * 3600 then
		--     GameAlertManager.Instance:ShowNotify(Util.GetText('guild_not_impeachment', impeachTime))
		-- else
			local itemdetail = ItemModel.GetDetailByTemplateID(GlobalHooks.DB.GetGlobalConfig('guild_impeachmentitem'))
			local content = Util.GetText('guild_impeachment_confirm', itemdetail.static.name, GlobalHooks.DB.GetGlobalConfig('guild_impeachmentitemnum'))
			GameAlertManager.Instance.AlertDialog:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, '', '', nil, 
			    function(parma)
					GuildModel.ClientImpeachGuildRequest(function( rsp )
		    			GameAlertManager.Instance:ShowNotify(Util.GetText('guild_impeachment_success', data.name))
						_M.RefreshList(self)
					end)
			    end, nil)
		-- end
	end
end


local function OpenFuncMenu( self, key, pos, anchor, uuid, name, params, cb )
    local menuType = key
    local args = {}
    args.playerId = uuid
    args.playerName = name
    args.menuKey = menuType
    args.pos = pos
    args.anchor = anchor
    args.params = params
    args.cb = cb
    EventManager.Fire("Event.InteractiveMenu.Show", args)
end

local function RefreshCDTime( self )
	if self.cdTime and TimeUtil.TimeLeftSec(self.cdTime) < 0 then
		local time = -TimeUtil.TimeLeftSec(self.cdTime)
		if self.cd then
			self.cd:Stop()
		end
		self.cd = CDExt.New(time, function( cd, label )
			self.ui.comps.lb_cd.Visible = cd ~= 0
			self.ui.comps.lb_cd.Text = TimeUtil.formatCD("%M:%S", cd)
			self.cd = nil
		end)
	else
		self.ui.comps.lb_cd.Visible = false
	end
end

local function RefreshListCellData( self, node, index )
	local data = self.memberList[index]
	local dbPosition = unpack(GlobalHooks.DB.Find('guild_position', { position_id = data.position }))
	MenuBase.SetLabelText(node, 'lb_name', data.name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_job', Util.GetText('pro_'..data.pro), 0, 0)
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.power), 0, 0)
	MenuBase.SetLabelText(node, 'lb_official', Util.GetText(dbPosition.position), 0, 0)
	MenuBase.SetLabelText(node, 'lb_today', tostring(data.contributionDay), 0, 0)
	MenuBase.SetLabelText(node, 'lb_all', tostring(data.contributionMax), 0, 0)
	local gen_icon = node:FindChildByEditName('ib_sex', true)
	UIUtil.SetImage(gen_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|gen_'..data.gender)

    local timeStr
    if data.leaveTime == System.DateTime.MaxValue then
        timeStr = Util.GetText('common_online')
    else
    	local sec = TimeUtil.TimeLeftSec(data.leaveTime)
        timeStr = Util.GetText('common_offline')..TimeUtil.FormatToCN(sec)
    end
	MenuBase.SetLabelText(node, 'lb_online', timeStr, 0, 0)

	node.TouchClick = function( ... )
	SoundManager.Instance:PlaySoundByKey('button',false)
		local menuKey
		if data.roleId == DataMgr.Instance.UserData.RoleID then
			menuKey = 'guild_self'
		else
			local selfPosition = unpack(GlobalHooks.DB.Find('guild_position', { position_id = self.position }))
			menuKey = selfPosition.relation_key
		end
		OpenFuncMenu(self, menuKey, nil, nil, data.roleId, data.name, data, function( key, playerId, params )
			OnFuncMenuClick(self, key, playerId, params)
		end)
	end
end

local function ReSort( self )
	-- print_r(self.memberList)
	if self.sortType ~= GuildModel.SortType.default then
		local flag = self.isAscending and 1 or -1
		table.sort(self.memberList, function( a, b )
			if self.sortType == GuildModel.SortType.level then
				return (a.level - b.level) * flag < 0
			elseif self.sortType == GuildModel.SortType.fightPower then
				return (a.power - b.power) * flag < 0
			elseif self.sortType == GuildModel.SortType.pro then
				return (a.pro - b.pro) * flag < 0
			elseif self.sortType == GuildModel.SortType.position then
				return (a.position - b.position) * flag < 0
			elseif self.sortType == GuildModel.SortType.state then
				return (a.leaveTime - b.leaveTime).TotalSeconds * flag < 0
			elseif self.sortType == GuildModel.SortType.contribution then
				return (a.contributionDay - b.contributionDay) * flag < 0
			elseif self.sortType == GuildModel.SortType.contributionMax then
				return (a.contributionMax - b.contributionMax) * flag < 0
			else
				return true
			end
		end)
		-- print_r(self.memberList)
	else
		self.memberList = Helper.copy_table(self.listSrc)
	end
	self.pan = self.listType == 1 and self.ui.comps.sp_memberlist or self.ui.comps.sp_memberlist1
	local cell = self.listType == 1 and self.ui.comps.cvs_memberinfo or self.ui.comps.cvs_memberinfo1
	UIUtil.ConfigVScrollPan(self.pan, cell, #self.memberList, function(node, index)
		RefreshListCellData(self, node, index)
	end)
end

function _M.RefreshList( self )
	GuildModel.ClientGuildMemListRequest(function( rsp )
		self.listSrc = rsp.s2c_members
		self.memberList = Helper.copy_table(self.listSrc)

		self.ui.comps.lb_number.Text = string.format("%d/%d", rsp.s2c_memberCount, rsp.s2c_memberCountMax)
		self.condition = rsp.s2c_condition
		self.recruitBulletin = rsp.s2c_recruitBulletin
		self.memberNum = rsp.s2c_memberCount
		self.memberMax = rsp.s2c_memberCountMax
		self.online = 0
		self.position = 0
		for _, v in pairs(self.listSrc) do
			self.online = v.leaveTime == System.DateTime.MaxValue and self.online + 1 or self.online
			if v.roleId == DataMgr.Instance.UserData.RoleID then
				self.position = v.position
			end
		end
		local dbPosition = unpack(GlobalHooks.DB.Find('guild_position', { position_id = self.position }))
		self.ui.comps.btn_notice.Visible = dbPosition.recruit_notice == 1
		self.ui.comps.cvs_call.Visible = dbPosition.recruit_propaganda == 1
		self.ui.comps.btn_condition.Visible = dbPosition.apply_condition == 1
		self.ui.comps.cvs_check.Visible = dbPosition.request_player == 1
		self.ui.comps.lb_online.Text = Util.GetText('guild_online', self.online)

		ReSort( self )
	end)
end

function _M.OnEnter( self )
	self.sortType = GuildModel.SortType.default
	RefreshCDTime(self)
    local tbts0 = {
        self.comps.tbt_base,
        self.comps.tbt_extend
    }
	UIUtil.ConfigToggleButton(tbts0, self.comps.tbt_base, true, function( sender )
		if sender:Equals(self.comps.tbt_base) then
			self.ui.comps.cvs_basedata.Visible = true
			self.ui.comps.cvs_extenddata.Visible = false
			self.listType = 1
		else
			self.ui.comps.cvs_basedata.Visible = false
			self.ui.comps.cvs_extenddata.Visible = true
			self.listType = 2
		end
		_M.RefreshList(self)
	end)

    local tbts = {
        self.comps.tbn_official,
        self.comps.tbn_lv,
        self.comps.tbn_job,
        self.comps.tbn_fight,
        self.comps.tbn_online,
        self.comps.tbn_official2,
        self.comps.tbn_today,
        self.comps.tbn_all,
        self.comps.tbn_fight2,
        self.comps.tbn_online2,
    }
    for i, v in ipairs(tbts) do
    	v.IsChecked = false
    	v.IsGray = true
        v.TouchClick = function( ... )
    		for i1, v1 in ipairs(tbts) do
        		v1.IsGray = v ~= v1
        	end
			self.isAscending = not v.IsChecked
			self.sortType = v.UserTag
			ReSort(self)
        end
    end
end

function _M.OnExit( self )
    if self.cd then
    	self.cd:Stop()
    	self.cd = nil
    end
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.comps.cvs_memberinfo.Visible = false
	self.ui.comps.cvs_memberinfo1.Visible = false

	self.ui.comps.btn_check.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI("GuildApplyList", 0, {memberNum=self.memberNum, memberMax=self.memberMax, online=self.online})
	end

	self.ui.comps.btn_condition.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI("GuildCondition", 0, {condition = self.condition, cb = function( condition )
			self.condition = condition
		end})
	end

	self.ui.comps.btn_notice.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI("GuildNotice", 0, { recruitBulletin = self.recruitBulletin, cb = function( recruitBulletin )
			self.recruitBulletin = recruitBulletin
		end})
	end

	self.ui.comps.btn_call.TouchClick = function( sender )
		if self.cdTime and TimeUtil.TimeLeftSec(self.cdTime) < 0 then
			GameAlertManager.Instance:ShowNotify(Util.GetText('chat_waitcd', -TimeUtil.TimeLeftSec(self.cdTime)))
		else
			ChatUtil.GuildRecruitShout(ChatModel.ChannelState.CHANNEL_WORLD)
			self.cdTime = System.DateTime.UtcNow:AddSeconds(GlobalHooks.DB.GetGlobalConfig('guild_recruitcd'))
			RefreshCDTime(self)
		end
	end
end

return _M