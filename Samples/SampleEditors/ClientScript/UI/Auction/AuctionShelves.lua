local _M = {}
_M.__index = _M

local AuctionModel = require 'Model/AuctionModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local CDExt = require 'Logic/CDExt'

local function ClearCDExt( self )
	if self.cdExt then
	    for k, v in pairs(self.cdExt) do
	    	if v then
	    		v:Stop()
	    	end
	    end
	end
end

local function CheckResizeList(self)
    -- print('checkresize list ',self.currentMaxCount,self.bagListener.ItemCount )
    local itemCount = self.bagListener.ItemCount

    if self.currentMaxCount < itemCount or self.currentMaxCount > itemCount + 3 then
        self.currentMaxCount = itemCount
        self.bagListener:Stop(false)
        self.itemList:Init(self.bagListener, self.currentMaxCount)
        self.bagListener:Start(true, true)
    end
end

local function OnPutOnSuccess( self )
	_M.RefreshSaleList(self)
end

function _M.RefreshSaleList( self )
	CheckResizeList(self)
    AuctionModel.ClientGetSaleItemListRequest(function( rsp )
		self.list = rsp.s2c_saleList
		local vipLv = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.VIP, 0)
		local dbv = GlobalHooks.DB.FindFirst('vip_info', { vip_level = vipLv })
		self.ui.comps.lb_open.Text = table.len(self.list)..'/'..dbv.field_num
		local root = self.ui.comps.cvs_list
		for i = 1, root.NumChildren do
			local cvsRoot = root:FindChildByEditName('cvs_item'..i, true)
			if i <= dbv.field_num then
				MenuBase.SetVisibleUENode(cvsRoot, 'cvs_vip', false)
				if self.list[i] then
					MenuBase.SetVisibleUENode(cvsRoot, 'cvs_nothing', false)
					MenuBase.SetVisibleUENode(cvsRoot, 'cvs_selling', true)

					--图标
					local data = self.list[i]
				    local detail = ItemModel.GetDetailByTemplateID(data.item.TemplateID)
					local cvs_item = cvsRoot:FindChildByEditName('cvs_item', true)
					local num = data.item.Count
					local itshow = UIUtil.SetItemShowTo(cvs_item, detail, num)
				    itshow.EnableTouch = true
				    itshow.TouchClick = function()
				    	if string.IsNullOrEmpty(data.item.ID) then	--非装备，直接取模板详情
				        	UIUtil.ShowNormalItemDetail({detail = detail, autoHeight = true})
				    	else --货架上的装备，向服务器请求装备详情
				    		ItemModel.RequestDetailByID(data.item.ID, function(entityDetail)
				        		UIUtil.ShowNormalItemDetail({detail = entityDetail, autoHeight = true})
				    		end)
				    	end
				    end
				    --名字
					MenuBase.SetLabelText(cvsRoot, 'lb_name', Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))
					--价格
					MenuBase.SetLabelText(cvsRoot, 'lb_money', tostring(data.price), 0, 0)
					--时间
					local function cdFun( cd, label )
						MenuBase.SetLabelText(cvsRoot, 'lb_timenum', TimeUtil.formatCD("%H:%M:%S", cd), 0, 0)
						if cd > 0 then

						end
					end
					local time = -TimeUtil.TimeLeftSec(data.time:AddHours(GlobalHooks.DB.GetGlobalConfig('trade_onsale_time')))
					if time > 0 then
						MenuBase.SetVisibleUENode(cvsRoot, 'cvs_time', true)
						MenuBase.SetVisibleUENode(cvsRoot, 'lb_timeup', false)
						if self.cdExt[i] == nil then
							self.cdExt[i] = CDExt.New(time, cdFun)
						else
							self.cdExt[i]:Stop()
							self.cdExt[i]:Reset(time, cdFun)
						end
					else --超时
						MenuBase.SetVisibleUENode(cvsRoot, 'cvs_time', false)
						MenuBase.SetVisibleUENode(cvsRoot, 'lb_timeup', true)
					end
					--下架
					local cvs = cvsRoot:FindChildByEditName('cvs_selling', true)
					cvs.TouchClick = function( ... )
						GlobalHooks.UI.OpenUI('AuctionPut', 0, { item = data, cb = function( ... )
							OnPutOnSuccess(self)
						end })
					end
				else
					MenuBase.SetVisibleUENode(cvsRoot, 'cvs_nothing', true)
					MenuBase.SetVisibleUENode(cvsRoot, 'cvs_selling', false)
				end
			else
				MenuBase.SetVisibleUENode(cvsRoot, 'cvs_vip', true)
				MenuBase.SetVisibleUENode(cvsRoot, 'cvs_nothing', false)
				MenuBase.SetVisibleUENode(cvsRoot, 'cvs_selling', false)
				local db = GlobalHooks.DB.FindFirst('vip_info', { field_num = i })
				MenuBase.SetLabelText(cvsRoot, 'lb_lock', Util.GetText('trade_vip_desc', db.vip_level), 0, 0)
			end
		end
    end)
end


function _M.OnEnter( self, param )
    self.cdExt = {}
	self.bagListener:Start(true, true)
	self.currentMaxCount = self.bagListener.ItemCount
	self.itemList:Init(self.bagListener, self.bagListener.ItemCount)
	_M.RefreshSaleList(self)
end

function _M.OnExit( self )
	self.bagListener:Stop(false)
	ClearCDExt(self)
    self.cdExt = nil
end

function _M.OnDestory( self )
	self.bagListener:Dispose()
end

function _M.OnInit( self )
	self.bagListener = ItemListener(DataMgr.Instance.UserData.Bag,false,0)
	self.bagListener.OnMatch = function ( itdata )
		return itdata.CanTrade
	end
	self.itemList = ItemList(self.comps.cvs_bag.Size2D,UnityEngine.Vector2(65,65),3)
	self.itemList.Position2D = self.comps.cvs_bag.Position2D
    self.itemList.EnableSelect = true
    local btns = {}
    table.insert(btns,
        {
            Text = Constants.Text.detail_btn_puton,
            cb = function(d)
				GlobalHooks.UI.OpenUI('AuctionPut', 0, { slot = self.select_index, cb = function( ... )
					OnPutOnSuccess(self)
				end })
				d.ui.menu:Close()
            end
        }
    )
    self.itemList.OnItemSingleSelect = function(new, old)
    	local itdata = self.bagListener:GetItemData(new)
    	local itemdetail = ItemModel.GetDetailByItemData(itdata)
        self.select_index = self.bagListener:GetSourceIndex(new.Index)
        local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = new, autoHeight = true, buttons = btns})
	end
	self.comps.cvs_bag.Parent:AddChild(self.itemList)

	self.ui.comps.btn_get.TouchClick = function( ... )
		GlobalHooks.UI.OpenUI('AuctionRecord', 0)
	end

	self.ui.comps.btn_help.TouchClick = function( ... )
		self.ui.comps.cvs_help.Visible = true
	end
	self.ui.comps.cvs_help.event_PointerUp = function( ... )
		self.ui.comps.cvs_help.Visible = false
	end
end

return _M