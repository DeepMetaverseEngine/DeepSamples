local _M = {}
_M.__index = _M

local AuctionModel = require 'Model/AuctionModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'

local function DoExtract( self )
	if self.gold == 0 then
		GameAlertManager.Instance:ShowNotify(Util.GetText('trade_no_income'))
		return
	end
	AuctionModel.ClientAuctionExtractRequest(function( rsp )
		self.gold = 0
		self.ui.comps.lb_num.Text = self.gold
		GlobalHooks.UI.SetRedTips('auction_get', 0)
	end)
end

local function RefreshListCellData( self, node, index )
	local data = self.list[index]
	local templateID = data.templateId
    local detail = ItemModel.GetDetailByTemplateID(templateID)
    --名字
	MenuBase.SetLabelText(node, 'lb_item', Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))
	--数量
	MenuBase.SetLabelText(node, 'lb_itemnum', '*'..data.num, 0, 0)
    --价格
	MenuBase.SetLabelText(node, 'lb_money', tostring(data.num * data.price), 0, 0)
	--时间
	local localTime = data.time:ToLocalTime()
	local time = string.format('%d:%d', localTime.Hour, localTime.Minute)
	MenuBase.SetLabelText(node, 'lb_time', time, 0, 0)
	--日期
	local date = Util.GetText('month_day', localTime.Month, localTime.Day)
	MenuBase.SetLabelText(node, 'lb_day', date, 0, 0)
end

function _M.RefreshRecordList( self )
    AuctionModel.ClientGetSalesRecordRequest(function( rsp )
    	local list = {}
    	for k, v in pairs(rsp.s2c_recordList) do
    		table.insert(list, v)
    	end
		table.sort(list, function( a, b )
			return a.time > b.time
		end)
		self.list = list

		self.pan = self.ui.comps.sp_list
		local cell = self.ui.comps.cvs_detail
		cell.Visible = false
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.list, function(node, index)
			RefreshListCellData(self, node, index)
		end)

		self.gold = rsp.s2c_goldMax
		self.ui.comps.lb_num.Text = self.gold
	end)
end

function _M.OnEnter( self, param )
	_M.RefreshRecordList(self)
    
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
    

	self.ui.comps.btn_get.TouchClick = function( ... )
		DoExtract(self)
	end
end

return _M