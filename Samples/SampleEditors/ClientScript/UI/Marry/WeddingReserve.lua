local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local function RefreshWeddingTimeInfo( self )
	local data = self.dateList[self.dayIndex]
	local time = data.info

	self.ui.comps.lb_canbook1.Visible = time[1] == nil
	self.ui.comps.lb_cant1.Visible = time[1] ~= nil

	self.ui.comps.lb_canbook2.Visible = time[2] == nil
	self.ui.comps.lb_cant2.Visible = time[2] ~= nil

	self.ui.comps.lb_canbook3.Visible = time[3] == nil
	self.ui.comps.lb_cant3.Visible = time[3] ~= nil

	if time[1] == nil then
		self.ui.comps.tbt_morning.IsChecked = true
	elseif time[2] == nil then
		self.ui.comps.tbt_afternoon.IsChecked = true
	elseif time[3] == nil then
		self.ui.comps.tbt_evening.IsChecked = true
	end
end

local function RefreshDateCellData( self, node, index )
	local data = self.dateList[index]
	local date = data.date
	local time = data.info
	local dayStr = Util.GetText('marry_day', date.Year, date.Month, date.Day)
	MenuBase.SetLabelText(node, 'lb_date', dayStr, 0, 0)
	local weekStr = Util.GetText('marry_day'..GameUtil.TryEnumToInt(date.DayOfWeek))
	MenuBase.SetLabelText(node, 'lb_dweek', weekStr, 0, 0)
	MenuBase.SetVisibleUENode(node, 'lb_canbook', table.len(time) < 3)
	MenuBase.SetVisibleUENode(node, 'lb_cant', table.len(time) >= 3)

	local tbt = node:FindChildByEditName('tbt_date', true)
	tbt.IsChecked = self.dayIndex == index
	tbt.TouchClick = function( ... )
		self.dayIndex = index
		self.pan:RefreshShowCell()
		RefreshWeddingTimeInfo(self)
	end
end

function _M.RefreshDateList( self )
	SocialModel.RequestClientGetReservationInfo(function(rsp)
		local today = rsp.today
		self.dateList = rsp.data
		self.ui.comps.btn_go.Visible = not rsp.expired
		self.ui.comps.btn_recheck.Visible = rsp.expired
		self.ui.comps.lb_todaynum.Text = Util.GetText('marry_day', today.Year, today.Month, today.Day)
		self.ui.comps.lb_week.Text = Util.GetText('marry_day'..GameUtil.TryEnumToInt(today.DayOfWeek))

		self.pan = self.ui.comps.sp_list
		local cell = self.ui.comps.cvs_date
		UIUtil.ConfigGridVScrollPanWithOffset(self.pan, cell, 5, #self.dateList, 4, 5, function(node, index)
			RefreshDateCellData(self, node, index)
		end)
		RefreshWeddingTimeInfo(self)
	end)
end

function _M.OnEnter( self, args )
	self.spouseId = args.spouseId
	self.dayIndex = 1
	self.timeIndex = 1

	_M.RefreshDateList(self)

    local tbts = {
        self.comps.tbt_morning,
        self.comps.tbt_afternoon,
        self.comps.tbt_evening
    }
    local default = self.comps.tbt_morning
    UIUtil.ConfigToggleButton(tbts, default, false,
        function(sender)
			self.timeIndex = sender.UserTag
        end)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
	self.ui.comps.cvs_date.Visible = false

	self.ui.comps.btn_go.TouchClick = function( ... )
		if self.dateList ~= nil then
			local data = self.dateList[self.dayIndex]
			-- if data.info[self.timeIndex] ~= nil then
			-- 	GameAlertManager.Instance:ShowNotify(Util.GetText('此日期已被预约，请选择还未预约的日期'))
			-- else
				SocialModel.RequestClientHoldingWedding(self.spouseId, 2, data.date, self.timeIndex, function( ... )
					_M.RefreshDateList(self)
					-- self.ui:Close()
				end)
			-- end
		end
	end

	self.ui.comps.btn_recheck.TouchClick = function( ... )
		if self.dateList ~= nil then
			local data = self.dateList[self.dayIndex]
			SocialModel.RequestClientWeddingReservation(self.spouseId, 2, data.date, self.timeIndex, function( ... )
				_M.RefreshDateList(self)
				-- self.ui:Close()
			end)
		end
	end
end

return _M