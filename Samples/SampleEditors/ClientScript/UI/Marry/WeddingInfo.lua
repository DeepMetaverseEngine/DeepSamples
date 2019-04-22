local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'



function _M.OnEnter( self, args )
	self.comps.lb_name1.Text = args.Husband
	self.comps.lb_name2.Text = args.Wife
	local date = System.DateTime.Parse(args.Date1)
	local dateStr = Util.GetText('marry_day', date.Year, date.Month, date.Day)
	local timeStr = Util.GetText('marry_time'..args.Times, date.Hour)
	self.comps.lb_time.Text = dateStr..' '..timeStr

	-- SocialModel.RequestClientCheckInvitation(args.slotIndex, function()
		
	-- end)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
end

return _M