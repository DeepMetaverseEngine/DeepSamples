local _M = {}
_M.__index = _M
local CDExt = require 'Logic/CDExt'
local TimeUtil = require 'Logic/TimeUtil'
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local current_instance
local Next

local Life_Sec = 15
local function Use(self)
    local itdata = ItemModel.GetBagItemDataByIndex(self.current.index)
    if itdata then
        ItemModel.UseItem(self.current.index, itdata.Count, self.current.detail)
    end
    Next(self)
end

local function Draw(self)
    local detail = self.current.detail
    UIUtil.SetItemShowTo(self.comps.cvs_item, detail)
    self.comps.lb_name.Text = Util.GetText(detail.static.name)
    self.comps.lb_name.FontColorRGB = Constants.QualityColor[detail.static.quality]
    -- local function cdFun(cd)
    --     if cd <= 0 then
    --         Use(self)
    --     end
    --     self.comps.btn_use.Text = Constants.Text.detail_btn_use .. TimeUtil.FormatToCN(cd)
    -- end
    -- if not self.timer then
    --     self.timer = CDExt.New(Life_Sec, cdFun)
    -- else
    --     self.timer:Reset(Life_Sec, cdFun)
    -- end
    self.comps.btn_use.Text = Constants.Text.detail_btn_use
end

Next = function(self)
    --use and draw next
    -- print_r('next ----',self.stack)
    if not self.stack or #self.stack == 0 then
        self:Close()
    else
        self.current = self.stack[1]
        table.remove(self.stack, 1)
        local itdata = ItemModel.GetBagItemDataByIndex(self.current.index)
        if not itdata then
            Next(self)
        else
            Draw(self)
        end
    end
end

local function Push(self, index, detail)
    self.stack = self.stack or {}
    table.insert(self.stack, {index = index, detail = detail})
    if not self.current then
        Next(self)
    end
end

function _M.OnEnter(self, index, detail)
    current_instance = self
    Push(self, index, detail)
end

function _M.OnExit(self)
    current_instance = nil
    self.current = nil
    self.stack = nil
    if self.timer then
        self.timer:Stop()
        self.timer = nil
    end
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.ui.menu.ShowType = UIShowType.Cover
    self.comps.btn_close.TouchClick = function()
        Next(self)
    end
    self.comps.btn_use.TouchClick = function()
        Use(self)
    end
    self:EnableTouchFrame(false)
end

local function OnCountUpdate(ename, params)
    -- print_r('oncount update ',params)
    if params.Count < 0 or params.Virtual then
        return
    end
    local detail = ItemModel.GetDetailByBagIndex(params.Index)
    if not detail or detail.static.item_type ~= Constants.ItemType.Use then
        return
    end
    if not detail.static_consumption or detail.static_consumption.is_shortcut ~= 1 then
        return
    end
    if current_instance then
        Push(current_instance, params.Index, detail)
    else
        GlobalHooks.UI.OpenUI('AutoUseItem', 0, params.Index, detail)
    end
end

local function initial()
    -- print('initial -------------')
    EventManager.Subscribe('Event.Item.CountUpdate', OnCountUpdate)
end

local function fin()
    EventManager.Unsubscribe('Event.Item.CountUpdate', OnCountUpdate)
end

_M.initial = initial
_M.fin = fin
return _M
