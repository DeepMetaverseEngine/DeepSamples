local _M = {}
_M.__index = _M

local AuctionModel = require 'Model/AuctionModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ServerTime = require 'Logic/ServerTime'
local TimeUtil = require 'Logic/TimeUtil'


local function DoPutOffShelves( self )
    AuctionModel.ClientPutOffShelvesRequest(self.item.uuid, function( rsp )
    	if self.cb then
    		self.cb()
    	end
    	self.ui.menu:Close()
    end)
end

local function DoChangePrice( self )
    AuctionModel.ClientAuctionChangePriceRequest(self.item.uuid, self.inputPrice, function( rsp )
    	if self.cb then
    		self.cb()
    	end
    	self.ui.menu:Close()
    end)
end


local function DoPutOnShelves( self )
    AuctionModel.ClientPutOnShelvesRequest(self.slot, self.inputNum, self.inputPrice, function( rsp )
    	if self.cb then
    		self.cb()
    	end
    	self.ui.menu:Close()
    end)
end

local function RefreshListCellData( self, node, index )
	local data = self.list[index]
	local templateID = data.item.TemplateID
    local detail = ItemModel.GetDetailByTemplateID(templateID)
    --名字
	MenuBase.SetLabelText(node, 'lb_name', Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))
	--图标
    local detail = ItemModel.GetDetailByTemplateID(templateID)
	local cvs_item = node:FindChildByEditName('cvs_item', true)
	local num = data.item.Count
	local itshow = UIUtil.SetItemShowTo(cvs_item, detail, num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
    	if string.IsNullOrEmpty(data.item.ID) then
        	UIUtil.ShowNormalItemDetail({detail = detail, itemShow = itshow, autoHeight = true})
    	else
    		ItemModel.RequestDetailByID(data.item.ID, function(entityDetail)
        		UIUtil.ShowNormalItemDetail({detail = entityDetail, itemShow = itshow, autoHeight = true})
    		end)
    	end
    end
    --评分/单价
    -- if string.IsNullOrEmpty(data.item.ID) then
    	MenuBase.SetVisibleUENode(node, 'lb_price', true)
    	MenuBase.SetVisibleUENode(node, 'lb_score', false)
    	MenuBase.SetVisibleUENode(node, 'ib_money', true)
		MenuBase.SetLabelText(node, 'lb_num', tostring(data.price), 0, 0)
  --   else
  --   	MenuBase.SetVisibleUENode(node, 'lb_price', false)
  --   	MenuBase.SetVisibleUENode(node, 'lb_score', true)
  --   	MenuBase.SetVisibleUENode(node, 'ib_money', false)
		-- MenuBase.SetLabelText(node, 'lb_num', tostring(data.score), 0, 0)
  --   end
end

local function RequestOtherItems( self )
	self.pan = self.ui.comps.sp_list
	self.pan.Visible = false
	self.ui.comps.cvs_nothing.Visible = false

	local templateId = self.slot ~= nil and self.item.TemplateID or self.item.item.TemplateID
	AuctionModel.ClientGetOtherItemListRequest(templateId, function( rsp )
		self.list = rsp.s2c_list
		if self.list ~= nil and #self.list > 0 then
			self.ui.comps.cvs_nothing.Visible = false
			self.pan.Visible = true
			local cell = self.ui.comps.cvs_selling
			cell.Visible = false

			table.sort(self.list, function( a, b )
				return (a.price - b.price) < 0
			end)
			UIUtil.ConfigVScrollPan(self.pan, cell, #self.list, function(node, index)
				RefreshListCellData(self, node, index)
			end)
		else
			self.pan.Visible = false
			self.ui.comps.cvs_nothing.Visible = true
		end
	end)
end

local function ChangePrice( self, value )
	if value < self.itemdb.online_min then
		self.inputPrice = self.itemdb.online_min
	elseif value > self.itemdb.online_max then
		self.inputPrice = self.itemdb.online_max
	else
		self.inputPrice = value
	end
	self.ui.comps.lb_priceup.Text = tostring(self.inputPrice)
	self.ui.comps.lb_costnum.Text = tostring(self.inputPrice * self.inputNum)
end

local function ChangeNum( self, value )
	local itemSnap = self.slot ~= nil and self.item or self.item.item

	if value < 1 then
		self.inputNum = 1
	elseif value > itemSnap.Count then
		self.inputNum = itemSnap.Count
	else
		self.inputNum = value
	end

	self.ui.comps.lb_numup.Text = tostring(self.inputNum)
	self.ui.comps.lb_costnum.Text = tostring(self.inputPrice * self.inputNum)
end

local function InitPutInfo( self )
	local itemSnap = self.slot ~= nil and self.item or self.item.item
	self.itemdb = GlobalHooks.DB.FindFirst('Item', { id = itemSnap.TemplateID })
	local posBack = self.ui.menu:LocalToUIGlobal(self.ui.comps.ib_back)
	local pos = { x = posBack.x - 25, y = posBack.y - 10 }

	--图标
    local detail = ItemModel.GetDetailByTemplateID(itemSnap.TemplateID)
	local cvs_item = self.ui.comps.cvs_itemup
	local num = itemSnap.Count
	local itshow = UIUtil.SetItemShowTo(cvs_item, detail, num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
    	if string.IsNullOrEmpty(itemSnap.ID) then	--非装备，直接取模板详情
        	UIUtil.ShowNormalItemDetail({detail = detail, x = pos.x, y = pos.y, autoHeight = true})
    	else
    		if self.slot ~= nil then	--背包里的装备，直接取本地装备详情
    			local entityDetail = ItemModel.GetDetailByID(itemSnap.ID)
	        	UIUtil.ShowNormalItemDetail({detail = entityDetail, x = pos.x, y = pos.y, autoHeight = true})
    		else --货架上的装备，向服务器请求装备详情
	    		ItemModel.RequestDetailByID(itemSnap.ID, function(entityDetail)
	        		UIUtil.ShowNormalItemDetail({detail = entityDetail, x = pos.x, y = pos.y, autoHeight = true})
	    		end)
    		end
    	end
    end

    --名字
	MenuBase.SetLabelText(self.ui.menu.mRoot, 'lb_nameup', Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))

	--数量
	local pos2 = self.ui.menu:LocalToScreenGlobal(self.ui.comps.ib_back)
	local posParam = { pos = { x = pos2.x - 25 }, anchor = { x = 0, y = 0.5 } }
	ChangeNum(self, itemSnap.Count)
	if self.slot ~= nil then --上架
		self.ui.comps.cvs_num.Enable = true
		self.ui.comps.cvs_num.EnableChildren = true
		self.ui.comps.cvs_num.IsGray = false

		self.ui.comps.btn_minusn.TouchClick = function( ... )
			ChangeNum(self, self.inputNum - 1)
		end
		self.ui.comps.btn_plusn.TouchClick = function( ... )
			ChangeNum(self, self.inputNum + 1)
		end
		self.ui.comps.cvs_num.TouchClick = function( ... )
		    GlobalHooks.UI.OpenUI('NumInput', 0, 1, itemSnap.Count, posParam, function(value)
				self.ui.comps.lb_numup.Text = tostring(value)
		    end, function( value )
				ChangeNum(self, value)
		    end)
		end
	else --重新上架，取回
		self.ui.comps.cvs_num.Enable = false
		self.ui.comps.cvs_num.EnableChildren = false
		self.ui.comps.cvs_num.IsGray = true
	end

	--价格
	local defaultPrice = self.slot ~= nil and self.itemdb.online_min or self.item.price
	ChangePrice(self, defaultPrice)
	self.ui.comps.btn_minusp.TouchClick = function( ... )
		ChangePrice(self, self.inputPrice - 1)
	end
	self.ui.comps.btn_plusp.TouchClick = function( ... )
		ChangePrice(self, self.inputPrice + 1)
	end
	self.ui.comps.cvs_price.TouchClick = function( ... )
	    GlobalHooks.UI.OpenUI('NumInput', 0, self.itemdb.online_min, self.itemdb.online_max, posParam, function(value)
			self.ui.comps.lb_priceup.Text = tostring(value)
	    end, function( value )
			ChangePrice(self, value)
	    end)
	end

	--税率
	local vipLv = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.VIP, 0)
	local dbv = GlobalHooks.DB.FindFirst('vip_info', { vip_level = vipLv })
	self.ui.comps.lb_tax.Text = (dbv.tax / 100)..'%'

	--time
	local puttime = self.slot ~= nil and ServerTime.getServerTime() or self.item.time
	local time = -TimeUtil.TimeLeftSec(puttime:AddHours(GlobalHooks.DB.GetGlobalConfig('trade_onsale_time')))
	if time > 0 then
		self.ui.comps.cvs_time.Visible = true
		self.ui.comps.lb_timeout.Visible = false
		self.ui.comps.lb_time.Text = TimeUtil.formatCD("%H:%M:%S", time)
	else
		self.ui.comps.cvs_time.Visible = false
		self.ui.comps.lb_timeout.Visible = true
	end

	--按钮
	if self.slot ~= nil then --上架
		self.ui.comps.btn_up.Visible = true
		self.ui.comps.btn_reup.Visible = false
		self.ui.comps.btn_down.Visible = false
	else
		self.ui.comps.btn_up.Visible = false
		self.ui.comps.btn_reup.Visible = true
		self.ui.comps.btn_down.Visible = true
	end
end

function _M.OnEnter( self, param )
    -- print_r('eeeeeeeeeeeeeeeeee', param)
    self.cb = param.cb
    if param.slot ~= nil then
    	self.item = ItemModel.GetBagItemDataByIndex(param.slot)
    	self.slot = param.slot
    else
    	self.item = param.item
    	self.slot = nil
    end

    InitPutInfo(self)
    RequestOtherItems(self)
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
    
	self.ui.comps.cvs_selling.Visible = false
	self.inputNum = 0
	self.inputPrice = 0

    self.ui.comps.btn_up.TouchClick = function( sender )
    	DoPutOnShelves(self)
    end

    self.ui.comps.btn_reup.TouchClick = function( sender )
    	DoChangePrice(self)
    end

    self.ui.comps.btn_down.TouchClick = function( sender )
    	DoPutOffShelves(self)
    end
end

return _M