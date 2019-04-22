local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local Helper = require 'Logic/Helper.lua'

local function SetGuildAttackRequest( self, guildId )
	GuildModel.ClientSetGuildAttackRequest(guildId, function( ... )
		GameAlertManager.Instance:ShowNotify(Util.GetText('common_setover'))
		_M.RefreshList(self, true)
	end)
end

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
	print('-------RefreshListCellData-------', index)
	local data = self.guildList[index]
	MenuBase.SetLabelText(node, 'lb_name1', data.name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_count1', tostring(data.attackCount), 0, 0)
	local atkGuild = string.IsNullOrEmpty(data.attackedGuild) and Util.GetText('NoAnything') or data.attackedGuild
	MenuBase.SetLabelText(node, 'lb_appoint1', atkGuild, 0, 0)
	-- MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	-- MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	-- MenuBase.SetLabelText(node, 'lb_num', string.format('%d/%d', data.memberNum, data.maxMemberNum), 0, 0)
	MenuBase.SetVisibleUENode(node, 'ib_select', self.selectIndex == index)
	node.TouchClick = function( ... )
		self.selectIndex = index
		self.lastPos = self.pan.Scrollable.Container.Position2D
		_M.RefreshList(self, false)
		SoundManager.Instance:PlaySoundByKey('button',false)
	end
end

function _M.RefreshList( self, force )
	GuildModel.ClientGetGuildAttackListRequest(force, function( list, attackGuild, attackedGuild )
		print_r('---------list', list)
		self.guildList = Helper.copy_table(list)
		if self.sortType ~= GuildModel.SortType.default then
			local flag = self.isAscending and 1 or -1
			table.sort(self.guildList, function( a, b )
				if self.sortType == GuildModel.SortType.attackCount then
					if a.attackCount ~= b.attackCount then
						return (a.attackCount - b.attackCount) * flag < 0
					elseif a.attackCount ~= 0 then
						return (a.attackTime - b.attackTime).TotalSeconds * flag < 0
					end
				elseif self.sortType == GuildModel.SortType.attacked then
					local aId, bId
					aId = a.attackedGuild ~= nil and 1 or 0
					bId = b.attackedGuild ~= nil and 1 or 0
					return (aId - bId) * flag < 0
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
		self.pan = self.ui.comps.sp_list
		local cell = self.ui.comps.cvs_listcell
		cell.Visible = false
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.guildList, function(node, index)
			RefreshListCellData(self, node, index)
		end)
		if self.lastPos then
			self.pan.Scrollable:LookAt(-self.lastPos)
		end

		self.ui.comps.lb_name2.Text = string.IsNullOrEmpty(attackedGuild) and Util.GetText('NoAnything') or attackedGuild
		self.ui.comps.lb_name3.Text = string.IsNullOrEmpty(attackGuild) and Util.GetText('NoAnything') or attackGuild
	end)
end

function _M.OnEnter( self )
	self.selectIndex = 1
	self.sortType = GuildModel.SortType.attackCount
	self.searchName = ''

	self.ui.comps.lb_name2.Text = ''
	self.ui.comps.lb_name3.Text = ''
	self.ui.comps.btn_del.Visible = false

	_M.RefreshList(self, true)

    local tbts = {
        self.comps.tbn_count,
        self.comps.tbn_appoint
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
    self.comps.tbn_count.IsChecked = true
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	-- self.ui.menu.ShowType = UIShowType.Cover

	self.ui.comps.tb_msg.Scrollable = true
	self.ui.comps.ti_serch.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('guild_namelimit')
	self.ui.comps.btn_serch.TouchClick = function( sender )
		self.searchName = self.ui.comps.ti_serch.Input.Text or ''
		_M.RefreshList(self, false)
	end

	self.ui.comps.btn_del.TouchClick = function( sender )
		self.ui.comps.ti_serch.Input.Text = ''
		self.searchName = ''
		_M.RefreshList(self, false)
		sender.Visible = false
	end

	self.ui.comps.btn_ok.TouchClick = function( sender )
		if self.guildList and #self.guildList > 0 then
			SetGuildAttackRequest(self, self.guildList[self.selectIndex].id)
		end
	end

	self.ui.comps.btn_clear.TouchClick = function( sender )
		SetGuildAttackRequest(self, nil)
	end
end

return _M