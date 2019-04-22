local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

function _M.OnEnter( self, params )
    local tbts = {
        self.comps.tbt_duty1,
        self.comps.tbt_duty2,
        self.comps.tbt_duty3
    }
    local default
    if params.condition == 0 then	--无需审批
    	default = self.comps.tbt_duty3
		self.ui.comps.lb_import.Text = ''
		self.fightPower = 0
		self.condition = 0
	elseif params.condition == -1 then	--需要审批
		default = self.comps.tbt_duty2
		self.ui.comps.lb_import.Text = ''
		self.fightPower = 0
		self.condition = -1
	else                             --战力审批
		default = self.comps.tbt_duty1
		self.ui.comps.lb_import.Text = params.condition
		self.fightPower = params.condition
		self.condition = -2
	end
	UIUtil.ConfigToggleButton(tbts, default, true, function( sender )
		if sender:Equals(self.comps.tbt_duty3) then
			self.condition = 0	--无需审批
		elseif sender:Equals(self.comps.tbt_duty2) then
			self.condition = -1	--需要审批
		else
			self.condition = -2 --战力审批
		end
	end)

	self.cb = params.cb
end

function _M.OnExit( self )
    self.cb = nil
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover

	local input = self.ui.comps.lb_import
	input.Enable = true
	input.IsInteractive = true
	input.event_PointerUp = function( ... )
		local pos = self.ui.menu:LocalToScreenGlobal(input)
		local posParam = { pos = { x = pos.x, y = pos.y + input.Height }, anchor = { x = 0, y = 0 } }
		local maxValue = GlobalHooks.DB.GetGlobalConfig('guild_fightcondition')
	    GlobalHooks.UI.OpenUI('NumInput', 0, 0, maxValue, posParam, function(value)
	    	self.fightPower = value
	        input.Text = value
	    end)
	end

	self.ui.comps.btn_ok.TouchClick = function( sender )
		if self.condition == -2 and self.fightPower < 1 then
			GameAlertManager.Instance:ShowNotify(Util.GetText('guild_change_massage'))
		else
			self.condition = self.condition == -2 and self.fightPower or self.condition
			GuildModel.ClientApplyConditionRequest(self.condition, function( rsp )
				if self.cb ~= nil then
					self.cb(self.condition)
				end
				GameAlertManager.Instance:ShowNotify(Util.GetText('common_setover'))
				self.ui.menu:Close()
			end)
		end
	end

end

return _M