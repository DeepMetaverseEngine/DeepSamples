local _M = {}
_M.__index = _M
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local function GetItemDetailMenu(self)
    local lastMenu = self.detailMenu
    local detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
    self:AddSubUI(detailMenu)

    local function DetailMenuOnExit(menu)
        if menu == self.detailMenu then
            self.detailMenu = nil
            if self.list then
                self.list:CleanSelect()
            end
        end
    end

    detailMenu:SubscribOnExit(function() 
        DetailMenuOnExit(detailMenu)
    end)
    self.detailMenu = detailMenu

    if lastMenu then 
        lastMenu:Close()
    end
end

local function OnShowDetail(self, detail, count)
	GetItemDetailMenu(self)
	self.detailMenu:Reset({detail = detail, count = count, index = self.select_index, autoHeight = true,globalTouch = true})
	local btns = {{Text = Constants.Text.detail_btn_get, cb = function()
		DataMgr.Instance.UserData.Warehourse:PutToNormalBag(self.detailMenu.index, self.detailMenu.count, function()
			if self.detailMenu then self.detailMenu:Close() end
		end)
	end}}
	self.detailMenu:SetButtons(btns)
	self.detailMenu:SetPos(260,94)
	-- self.detailMenu:EnableTouchFrameClose(true)
end

local function OnBagSizeChange(self)
	local bag = DataMgr.Instance.UserData.Warehourse

	self.comps.lb_num.Text = string.format('%d/%d', bag.Size - bag.EmptySlotCount, bag.Size)
end

local function OnAddBagSizeTo(self, count)
    local function AlertOK()
        DataMgr.Instance.UserData.Warehourse:AddSize(
            count,
            function(success)
                OnBagSizeChange(self)
            end
        )
    end
    
    local bag = DataMgr.Instance.UserData.Warehourse

    local function CheckCost(e)
        local ret =  e.storehouse_min > bag.Size and e.storehouse_min <= count
        return ret
    end

    print('OnAddBagSizeTo ',count, bag.Size)
    local costs_static = GlobalHooks.DB.Find('storehouse_config', CheckCost)
    if #costs_static == 0 then
        UIUtil.ShowErrorMessage(Constants.Text.warn_extrasize)
        return 
    end

    --合并数量
    local costs = {}
    for _,v in ipairs(costs_static) do
        costs[v.cost_id] = costs[v.cost_id] or 0
        costs[v.cost_id] = costs[v.cost_id] + v.cost_num
    end
    local useId,useCount = next(costs)
    local detail = ItemModel.GetDetailByTemplateID(useId)
    local itemIcon = 'static/item/'..detail.static.atlas_id

    local content = Util.Format1234(Constants.Text.confirm_warehoursesize,itemIcon,useCount)
    UIUtil.ShowConfirmAlert(content, Constants.Text.confirm_warehoursesize_title, AlertOK)
end

local function OnItemSingleSelect(self, new, old)
	if not new then
		if self.detailMenu then
			self.detailMenu:Close()
		end
		return
	end
	
	local itdata = self.listener:GetItemData(new)
	self.select_index = self.listener:GetSourceIndex(new.Index)
	print('select index', self.select_index)
	local detail = ItemModel.GetDetailByItemData(itdata)
	OnShowDetail(self, detail, itdata.Count)

end
local function AddPackUpTimer(self)
	self.timeid = LuaTimer.Add(1000, 1000, function()
		local txt = Util.ContainsTextKey('common_arrange') and Util.GetTxt('common_arrange') or '整理'
		if not self._packup_cooldown then
			self.timeid = nil
			self.comps.btn_neaten.Text = txt
			return false
		end
		self._packup_cooldown = self._packup_cooldown - 1
		if self._packup_cooldown <= 0 then
			self._packup_cooldown = nil
			self.comps.btn_neaten.Text = txt
			self.timeid = nil
			return false
		else
			self.comps.btn_neaten.Text = string.format('00:%02d', self._packup_cooldown)
			return true
		end
	end)
end

function _M.OnEnter(self)
	GlobalHooks.UI.CloseUIByTag('RoleBagDecompose')
	self.listener:Start(false, false)
	self.list:Init(self.listener, DataMgr.Instance.UserData.Warehourse.MaxSize)
	OnBagSizeChange(self)
end

function _M.OnExit(self)
	self.listener:Stop(false)
end

function _M.Destory(self)
	self.listener:Dispose()
	if self.timeid then
		LuaTimer.Delete(self.timeid)
	end
end

function _M.OnInit(self)
	self.ui:EnableTouchFrame(false)
	
	local cvs_item = self.comps.cvs_item
	cvs_item.Visible = false
	self.list = ItemList(cvs_item.Size2D, Constants.Item.DefaultSize, 4)
	self.list.Position2D = cvs_item.Position2D
	self.list.ShowBackground = true
	self.list.EnableSelect = true
	self.list.OnItemSingleSelect = function(new, old)
		OnItemSingleSelect(self, new, old)
	end
	self.list.OnItemClick = function(it)
        if it.Status == ItemStatus.Lock then
            OnAddBagSizeTo(self, it.Index + 1)
        end
	end
	
	local warehouse = DataMgr.Instance.UserData.Warehourse
	self.listener = ItemListener(warehouse, false, warehouse.Size)

	
	self.listener.OnFilledSizeChange = function(size)
		OnBagSizeChange(self)
	end
	cvs_item.Parent:AddChild(self.list)
	self.comps.btn_neaten.TouchClick = function()
		if not self._packup_cooldown then
			warehouse:PackUpItems(function() end)
			self._packup_cooldown = Constants.WarehoursePackUpCoolDownSec
			self.comps.btn_neaten.Text = string.format('00:%2d', Constants.WarehoursePackUpCoolDownSec)
			AddPackUpTimer(self)
		end
	end
end

return _M
