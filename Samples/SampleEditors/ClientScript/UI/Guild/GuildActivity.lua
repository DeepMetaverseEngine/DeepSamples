local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ServerTime = require 'Logic/ServerTime'
local TimeUtil = require 'Logic/TimeUtil'

local function RefreshListCellData( self, node, index )
	local data = self.data[index]
	MenuBase.SetLabelText(node, 'lb_itemname', Util.GetText(data.activity_name), 0, 0)
	MenuBase.SetLabelText(node, 'lb_itemprice', Util.GetText(data.activity_desc), 0, 0)
	MenuBase.SetLabelText(node, 'lb_time', string.format('%d/%d', data.curCount, data.maxCount), 0, 0)
	local cvs_itemicon = node:FindChildByEditName('cvs_itemicon', true)
	UIUtil.SetImage(cvs_itemicon, data.activity_icon)

	local ret, beginTime, endTime = FunctionUtil.CheckNowIsOpen(data.function_id, false)
	-- print('sssssssss', ret, beginTime, endTime)
	if ret then --在活动时间
		local btn_go = node:FindChildByEditName('btn_go', true)
		btn_go.TouchClick = function( ... )
			FunctionUtil.OpenFunction(data.function_id)
		end
		MenuBase.SetGrayUENode(node, false)
		MenuBase.SetVisibleUENode(node, 'ib_over', false)
		MenuBase.SetVisibleUENode(node, 'lb_begin', false)
	else
		local datetime = System.DateTime.FromBinary(beginTime)
		local serverBeginTime = datetime:ToUniversalTime():Add(System.DateTime.UtcNow - ServerTime.getServerTime())
		local isOver = serverBeginTime < ServerTime.getServerTime()
		MenuBase.SetGrayUENode(node, isOver)
		MenuBase.SetVisibleUENode(node, 'ib_over', isOver)
		MenuBase.SetVisibleUENode(node, 'lb_begin', not isOver)
		MenuBase.SetLabelText(node, 'lb_begin', Util.GetText('guild_activitytime_text', GameUtil.FormatDateTime(datetime, "HH:mm")), 0, 0)
	end
	MenuBase.SetVisibleUENode(node, 'btn_go', ret)
end

local function InitData( self, info )
	local db = GlobalHooks.DB.GetFullTable('guild_activity')
	self.data = {}
	for i = #db, 1, -1 do
		local data = db[i]
		local ret, beginTime, endTime = FunctionUtil.CheckNowIsOpen(data.function_id, false)
		if not ret and beginTime == -1 then --当天没有活动
			-- table.remove(self.db, i)
		else
			local tb = data
			local activityInfo = info.s2c_activityInfo[data.function_id]
			tb.curCount = activityInfo.curCount
			tb.maxCount = activityInfo.maxCount
			table.insert(self.data, tb)
		end
	end
end

local function RefreshList( self )
	GuildModel.ClientGuildActivityInfoRequest(function( rsp )
		InitData(self, rsp)
		local pan = self.ui.comps.sp_oar
		local cell = self.ui.comps.cvs_mission
		cell.Visible = false
		UIUtil.ConfigVScrollPan(pan, cell, #self.data, function(node, index)
			RefreshListCellData(self, node, index)
		end)
	end)
end

function _M.OnEnter( self )
	RefreshList(self)
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )

end

return _M