local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local Helper = require 'Logic/Helper.lua'

local function RefreshListCellData( self, node, index )
	local data = self.applyList[index]
	MenuBase.SetLabelText(node, 'lb_name', data.name, 0, 0)
	-- MenuBase.SetLabelText(node, 'lb_job', Util.GetText('pro_'..data.pro), 0, 0)
	local job_icon = node:FindChildByEditName('lb_job', true)
	UIUtil.SetImage(job_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|pro_'..data.pro)


	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.power), 0, 0)
	MenuBase.SetVisibleUENode(node, 'cvs_btns', data.status == nil)
	local gen_icon = node:FindChildByEditName('ib_sex', true)
	UIUtil.SetImage(gen_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|gen_'..data.gender)

	local btn_no = node:FindChildByEditName('btn_no', true)
	btn_no.TouchClick = function( ... )
		GuildModel.ClientDealGuildApplyRequest(data.msgId, false, function( rsp )
			self.online = data.online and self.online + 1 or self.online
			data.status = 0
			MenuBase.SetVisibleUENode(node, 'cvs_btns', false)
			MenuBase.SetVisibleUENode(node, 'lb_state', true)
			MenuBase.SetLabelText(node, 'lb_state', Util.GetText('common_refused'), 0, 0)
		end)
	end
	local btn_yes = node:FindChildByEditName('btn_yes', true)
	btn_yes.TouchClick = function( ... )
		GuildModel.ClientDealGuildApplyRequest(data.msgId, true, function( rsp )
			data.status = 1
			self.online = data.online and self.online + 1 or self.online
			self.memberNum = rsp.s2c_memberCount
			self.ui.comps.lb_number.Text = string.format("%d/%d", self.memberNum, self.memberMax)
			self.ui.comps.lb_online.Text = Util.GetText('guild_online', self.online)
			MenuBase.SetVisibleUENode(node, 'cvs_btns', false)
			MenuBase.SetVisibleUENode(node, 'lb_state', true)
			MenuBase.SetLabelText(node, 'lb_state', Util.GetText('common_accepted'), 0, 0)
			GameAlertManager.Instance:ShowFloatingTips(Util.GetText('guild_applydone'))
		end)
	end
end

local function ReSort( self )
	-- print_r(self.applyList)
	if self.sortType ~= GuildModel.SortType.default then
		local flag = self.isAscending and 1 or -1
		table.sort(self.applyList, function( a, b )
			if self.sortType == GuildModel.SortType.level then
				return (a.level - b.level) * flag < 0
			elseif self.sortType == GuildModel.SortType.fightPower then
				return (a.power - b.power) * flag < 0
			else
				return true
			end
		end)
		-- print_r(self.applyList)
	else
		self.applyList = Helper.copy_table(self.listSrc)
	end
	local pan = self.ui.comps.sp_oar
	local cell = self.ui.comps.cav_list
	UIUtil.ConfigVScrollPan(pan, cell, #self.applyList, function(node, index)
		RefreshListCellData(self, node, index)
	end)
end

local function RefreshList( self )
	GuildModel.ClientGuildApplyListRequest(function( rsp )
		self.listSrc = rsp.s2c_applyList
		self.applyList = Helper.copy_table(self.listSrc)
		-- print_r(self.applyList)
		ReSort(self)
	end)
end

function _M.OnEnter( self, params )
	self.memberNum = params.memberNum
	self.memberMax = params.memberMax
	self.online = params.online
	self.ui.comps.lb_number.Text = string.format("%d/%d", self.memberNum, self.memberMax)
	self.ui.comps.lb_online.Text = Util.GetText('guild_online', self.online)
	self.sortType = GuildModel.SortType.default
	RefreshList(self)

    local tbts = {
        self.comps.tbn_lv,
        self.comps.tbn_fight
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
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.comps.cav_list.Visible = false
end

return _M