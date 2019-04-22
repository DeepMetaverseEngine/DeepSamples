local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local ChatModel = require 'Model/ChatModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local Helper = require 'Logic/Helper.lua'

local function GetSearchList( self, key, srcList )
	local list = {}
    for _, v in ipairs(srcList) do
    	if string.find(v.name, key) then
    		table.insert(list, v)
    	end
    end
    return list
end

local function RefreshListCellData( self, node, index )
	local data = self.guildList[index]
	MenuBase.SetLabelText(node, 'lb_name', data.name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	MenuBase.SetLabelText(node, 'lb_num', string.format('%d/%d', data.memberNum, data.maxMemberNum), 0, 0)
	local condition = data.condition > 0 and Util.GetText('common_fight')..data.condition or (data.condition == 0 and Util.GetText('guild_condition_none') or Util.GetText('guild_condition_need'))
	local color = (data.condition > 0 and DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.FIGHTPOWER) < data.fightPower) and 0xff0000 or 0
	MenuBase.SetLabelText(node, 'lb_condition', condition, GameUtil.RGB2Color(color))
	MenuBase.SetVisibleUENode(node, 'ib_select', self.selectIndex == index)
	if self.selectIndex == index then
		self.ui.comps.lb_ghname.Text = data.name
		self.ui.comps.tb_msg.Text = data.recruitBulletin
		self.ui.comps.lb_zi2.Text = data.presidentName
	    if data.fort ~= 0 then
	        local dbfort = GlobalHooks.DB.FindFirst('guild_fort', { id = data.fort })
	        self.ui.comps.lb_zi4.Text = Util.GetText(dbfort.fort_name)
	    else
	        self.ui.comps.lb_zi4.Text = Util.GetText('common_none')
	    end
	end
	node.TouchClick = function( ... )
		self.selectIndex = index
		self.lastPos = self.pan.Scrollable.Container.Position2D
		_M.RefreshList(self, false)
		SoundManager.Instance:PlaySoundByKey('button',false)
	end
end

function _M.RefreshList( self, force )
	GuildModel.ClientGetGuildListRequest(force, function( list )
		self.guildList = Helper.copy_table(list)
		-- print_r(self.guildList)
		if self.sortType ~= GuildModel.SortType.default then
			local flag = self.isAscending and 1 or -1
			table.sort(self.guildList, function( a, b )
				if self.sortType == GuildModel.SortType.level then
					return (a.level - b.level) * flag < 0
				elseif self.sortType == GuildModel.SortType.fightPower then
					return (a.fightPower - b.fightPower) * flag < 0
				elseif self.sortType == GuildModel.SortType.memberCount then
					return (a.memberNum - b.memberNum) * flag < 0
				else
					return true
				end
			end)
			-- print_r(self.guildList)
		end
		if self.searchName ~= '' then
			self.guildList = GetSearchList(self, self.searchName, self.guildList)
			if #list > 0 then
				self.ui.comps.btn_del.Visible = true
			end
		end
		self.pan = self.ui.comps.sp_guildlist
		local cell = self.ui.comps.cav_guildinfo
		cell.Visible = false
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.guildList, function(node, index)
			RefreshListCellData(self, node, index)
		end)
		if self.lastPos then
			self.pan.Scrollable:LookAt(-self.lastPos)
		end
	end)
end

local function RequestJoinGuild( self, guildId )
	GuildModel.ClientApplyGuildRequest(guildId, function( rsp )
		if rsp.s2c_guildInfo then
			-- GameAlertManager.Instance:ShowNotify(Util.GetText('guild_join', rsp.s2c_guildInfo.guildBase.name))
			self.ui.menu:Close()
			GlobalHooks.UI.OpenUI("GuildMain", 0, 'GuildInfo', { data = rsp.s2c_guildInfo, position = rsp.s2c_position })
		else
			GameAlertManager.Instance:ShowNotify(Util.GetText('guild_applymsg'))
		end
	end)
end

function _M.OnEnter( self )
	self.selectIndex = 1
	self.sortType = GuildModel.SortType.default
	self.searchName = ''

	self.ui.comps.lb_ghname.Text = ''
	self.ui.comps.tb_msg.Text = ''
	self.ui.comps.lb_zi2.Text = ''
	self.ui.comps.lb_zi4.Text = ''
	self.ui.comps.btn_del.Visible = false

	_M.RefreshList(self, true)

    local tbts = {
        self.comps.tbn_lv,
        self.comps.tbn_fight,
        self.comps.tbn_num
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
			_M.RefreshList(self, false)
        end
    end
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.comps.btn_an1.TouchClick = function( sender )
		-- self.ui.menu:Close()
		GlobalHooks.UI.OpenUI("GuildCreate",0)
	end
	self.ui.comps.ti_import.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('guild_namelimit')
	self.ui.comps.btn_serch.TouchClick = function( sender )
		self.searchName = self.ui.comps.ti_import.Input.Text or ''
		_M.RefreshList(self, false)
	end
	self.ui.comps.btn_del.TouchClick = function( sender )
		self.ui.comps.ti_import.Input.Text = ''
		self.searchName = ''
		_M.RefreshList(self, false)
		sender.Visible = false
	end
	self.ui.comps.btn_an2.TouchClick = function( sender )
		local gData = self.guildList[self.selectIndex]
		if gData then
			RequestJoinGuild(self, gData.id)
		end
	end
	self.ui.comps.btn_an3.TouchClick = function( sender )
		RequestJoinGuild(self, '')
	end

	self.ui.comps.btn_find1.TouchClick = function( sender )
		if self.guildList and #self.guildList > 0 then
			local roleId = self.guildList[self.selectIndex].presidentId
			local roleName = self.guildList[self.selectIndex].presidentName
			ChatModel.OpenPrivateChatUI(roleId, roleName)
		end
	end
end

return _M