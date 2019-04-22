local _M = {}
_M.__index = _M

local AuctionModel = require 'Model/AuctionModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'


local function DoBuyItem( self )
	if DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.SILVER) < self.item.price * self.inputNum then
		GameAlertManager.Instance:ShowNotify(Util.GetText('notenough_sliver'))
		return
	end
    AuctionModel.ClientAuctionBuyRequest(self.item.uuid, self.item.price, self.inputNum, function( rsp )
    	if self.cb then
    		self.cb()
    	end
    	self.ui.menu:Close()
    end)
end

local function ChangeNum( self, value )
	local itemSnap = self.item.item

	if value < 1 then
		self.inputNum = 1
	elseif value > itemSnap.Count then
		self.inputNum = itemSnap.Count
	else
		self.inputNum = value
	end

	self.ui.comps.lb_num.Text = tostring(self.inputNum)
	self.ui.comps.lb_costnum.Text = tostring(self.item.price * self.inputNum)
end

local function InitBuyInfo( self )
	local itemSnap = self.item.item
	self.itemdb = GlobalHooks.DB.FindFirst('Item', { id = itemSnap.TemplateID })

	--图标
    local detail = ItemModel.GetDetailByTemplateID(itemSnap.TemplateID)
	local cvs_item = self.ui.comps.cvs_item
	local num = itemSnap.Count
	local itshow = UIUtil.SetItemShowTo(cvs_item, detail, num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
    	if string.IsNullOrEmpty(itemSnap.ID) then	--非装备，直接取模板详情
        	UIUtil.ShowNormalItemDetail({detail = detail, autoHeight = true})
    	else --货架上的装备，向服务器请求装备详情
    		ItemModel.RequestDetailByID(itemSnap.ID, function(entityDetail)
        		UIUtil.ShowNormalItemDetail({detail = entityDetail, autoHeight = true})
    		end)
		end
    end

    --名字
	MenuBase.SetLabelText(self.ui.menu.mRoot, 'lb_name', Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))

	--数量
	local pos2 = self.ui.menu:LocalToScreenGlobal(self.ui.comps.cvs_num)
	local posParam = { pos = { y = pos2.y + self.ui.comps.cvs_num.Height }, anchor = { x = 0.5, y = 0 } }
	ChangeNum(self, itemSnap.Count)
	self.ui.comps.btn_minus.TouchClick = function( ... )
		ChangeNum(self, self.inputNum - 1)
	end
	self.ui.comps.btn_plus.TouchClick = function( ... )
		ChangeNum(self, self.inputNum + 1)
	end
	self.ui.comps.cvs_num.TouchClick = function( ... )
	    GlobalHooks.UI.OpenUI('NumInput', 0, 1, itemSnap.Count, posParam, function(value)
			self.ui.comps.lb_num.Text = tostring(value)
	    end, function( value )
			ChangeNum(self, value)
	    end)
	end
end

function _M.OnEnter( self, param )
    self.cb = param.cb
    self.item = param.item
    InitBuyInfo(self)
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
    
	self.inputNum = 0

    self.ui.comps.btn_buy.TouchClick = function( sender )
    	DoBuyItem(self)
    end
end

return _M