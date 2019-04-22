local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local pList = {
	-- 'transfer_president',
	'building_lvup',
	'apply_gvg',
	'notice',
	'change_position',
	'request_player',
	'kick_player',
	'impeach_president',
	'recruit_notice',
	'recruit_propaganda',
	'apply_condition',
	'allow_settarget',
}

local function RefreshDetail( self, db )
	local pan = self.ui.comps.sp_oar2
	local cell = self.ui.comps.cvs_frame
	local list = {}
	for _, v in ipairs(pList) do
		if db[v] == 1 then
			table.insert(list, Util.GetText('guild_'..v))
		end
	end
	UIUtil.ConfigVScrollPan(pan, cell, #list, function(node, index)
		MenuBase.SetLabelText(node, 'lb_text', list[index], 0, 0)
	end)
end

local function RefreshListCellData( self, node, index )
	local data = self.dbPosition[index + 1]
	MenuBase.SetLabelText(node, 'lb_text', Util.GetText(data.position), 0, 0)
	local tbt = node:FindChildByEditName('tbt_duty', true)
	tbt.IsChecked = self.selectPId == data.position_id
	tbt.TouchClick = function( ... )
		self.selectPId = data.position_id
		_M.RefreshList(self)
	end
	if self.selectPId == data.position_id then
		RefreshDetail(self, data)
	end
end

function _M.RefreshList( self )
	local pan = self.ui.comps.sp_oar1
	local cell = self.ui.comps.cvs_frame1
	UIUtil.ConfigVScrollPan(pan, cell, #self.dbPosition - 1, function(node, index)
		RefreshListCellData(self, node, index)
	end)
end

function _M.OnEnter( self, params )
	print_r(params)
	self.params = params
	self.dbPosition = GlobalHooks.DB.Find('guild_position', {})
	self.selectPId = params.tarPosition
	_M.RefreshList(self)
end

function _M.OnExit( self )
    self.params = nil
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover

	self.ui.comps.cvs_frame.Visible = false
	self.ui.comps.cvs_frame1.Visible = false

    self.ui.comps.btn_set.TouchClick = function( sender )
        GuildModel.ClientChangePostionRequest(self.params.tarId, self.selectPId, function( rsp )
            if rsp:IsSuccess() then
                GameAlertManager.Instance:ShowNotify(Util.GetText('common_setover'))
                if self.params.cb then
                	self.params.cb(self.selectPId)
                end
            end
        end)
    end
end

return _M