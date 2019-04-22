local _M = {}
_M.__index = _M
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local function CheckResizeList(self)
    -- print('checkresize list ',self.currentMaxCount,self.listener.ItemCount )
    local itemCount = self.listener.ItemCount

    if self.currentMaxCount < itemCount or self.currentMaxCount > itemCount + 4 then
        self.currentMaxCount = itemCount
        self.listener:Stop(false)
        self.list:Init(self.listener, self.currentMaxCount)
        self.listener:Start(true, true)
    end
end

local function DynamicButtonState(self)
    -- print_r('Dynammic ', self.selectIndexs)
    self.comps.btn_decompose.Enable = self.selectIndexs ~= nil and next(self.selectIndexs) ~= nil
    self.comps.btn_decompose.IsGray = not self.comps.btn_decompose.Enable
end

local function PreviewDecompose(self, templateID, isslect, bag_index, itshow)
    local ret = GlobalHooks.DB.FindFirst('item_decompose', templateID)
    if not ret then
        error('not find ' .. templateID .. 'in item_decompose')
    end
    self.selectIndexs = self.selectIndexs or {}
    -- print('PreviewDecompose ',isslect, bag_index)
    if isslect then
        self.selectIndexs[bag_index] = true
    else
        self.selectIndexs[bag_index] = nil
    end
    self.selectParents = self.selectParents or {}
    self.selectParents[bag_index] = isslect and itshow or nil
    for i, v in ipairs(ret.decompose.id) do
        local id = v
        if id ~= 0 then
            if isslect then
                local detail = ItemModel.GetDetailByTemplateID(id)
                -- print(detail.static.id, ret.decompose.num[i], detail.static.max_stacked)
                local it = GameUtil.CreateItemData(detail.static.id, ret.decompose.num[i], detail.static.max_stacked)
                -- local it = GameUtil.CreateItemData(detail.static.id, ret.decompose.num[i], 1)
                -- print('add ',it.TemplateID, it.Count)
                self.preBag:AddItem(it)
            else
                -- print('remove ',ret.decompose.id[i],ret.decompose.num[i])
                self.preBag:Cost(id, ret.decompose.num[i])
            end
        end
    end
    DynamicButtonState(self)
end

local function OnItemSelect(self, listener, itshow)
    if not itshow or itshow.IsEmpty then
        return
    end
    local itdata = listener:GetItemData(itshow)
    local tbt_an = self.comps['tbt_an' .. itshow.Quality]
    if tbt_an.IsChecked and not itshow.IsSelected then
        tbt_an.IsChecked = false
    end
    --查询分解表，添加到预览背包中
    local bag_index = self.rolebagItem.listener:GetSourceIndex(itshow.Index)
    PreviewDecompose(self, itdata.TemplateID, itshow.IsSelected, bag_index, itshow.Parent)
    CheckResizeList(self)
end

local function OnQualitySelect(self, quality, isselected, dont_showmsg)
    local slots = self.rolebagItem.listener.AllSlots
    self.quality_selects = self.quality_selects or {}
    self.quality_selects[quality] = isselected
    --self.listener:Stop(false)
    local has = false
    for i = 1, slots.Length do
        local slot = slots[i]
        if slot.Item then
            local detail = ItemModel.GetDetailByTemplateID(slot.Item.TemplateID)
            if detail.static.quality == quality then
                has = true
                local itshow = self.rolebagItem.listener:GetShowAt(slot.Index)
                if itshow.IsSelected ~= isselected then
                    itshow.IsSelected = isselected
                    local bag_index = self.rolebagItem.listener:GetSourceIndex(slot.Index)
                    PreviewDecompose(self, slot.Item.TemplateID, isselected, bag_index, itshow.Parent)
                end
            end
        end
    end
    --self.listener:Start(true,true)
    CheckResizeList(self)
    if not has and not dont_showmsg then
        local msg = Util.Format1234(Constants.Text.warn_decompquality, Constants.QualityText[quality])
        UIUtil.ShowMessage(msg)
        self.comps['tbt_an' .. quality].IsChecked = false
    end
    return has
end

local function OnCleanSelects(self)
    for i = 1, 4 do
        self.comps['tbt_an' .. i].IsChecked = false
    end
    self.rolebagItem:CleanAllSelect()
    self.preBag:Cleanup()
    self.selectIndexs = nil
    self.selectParents = nil
    DynamicButtonState(self)
end

function _M.OnEnter(self)
    GlobalHooks.UI.CloseUIByTag('RoleBagWarehourse')
    self.currentMaxCount = 4
    --先放一行
    self.list:Init(self.listener, self.currentMaxCount)
    self.listener:Start(true, true)
    self.rolebagItem = GlobalHooks.UI.FindUI('RoleBagItem')
    self.rolebagItem:LockCategory(
        'tbt_all',
        function(itdata)
            local detail = ItemModel.GetDetailByTemplateID(itdata.TemplateID)
            return detail and detail.static.can_decompose == 1 or false
        end,
        function()
            OnCleanSelects(self)
        end
    )
    self.rolebagItem:EnableMultiSelect(
        true,
        function(listener, itshow)
            OnItemSelect(self, listener, itshow)
        end
    )
    self.rolebagItem:SetOnInitDetailCB(
        function(...)
        end
    )
    DynamicButtonState(self)

    for quality, v in pairs(self.quality_selects or {}) do
        if v then
            local ok = OnQualitySelect(self, quality, true, false)
            if ok then
                self.comps['tbt_an' .. quality].IsChecked = true
            end
        end
    end
end

local function OnDecomposeButtonClick(self)
    local allSelct = {}
    for index, v in pairs(self.selectIndexs or {}) do
        if v then
            table.insert(allSelct, {index = index, count = 1})
        end
    end
    if #allSelct == 0 then
        UIUtil.ShowErrorMessage(Constants.Text.warn_decompose_limit)
        return
    end
    -- print_r('OnDecomposeButtonClick', allSelct)
    ItemModel.RequestDecompose(
        allSelct,
        function()
            print('decompse ok')
            for _, v in pairs(self.selectParents or {}) do
                local t = {
                    LayerOrder = self.menu.MenuOrder,
                    UILayer = true,
                    DisableToUnload = true,
                    Parent = v.Transform,
                    Pos = {x = 44, y = -37, z = -600}
                }
                Util.PlayEffect('/res/effect/ui/ef_ui_interface_gamepokey_resolve.assetbundles', t)
            end
            self.selectParents = nil
            OnCleanSelects(self)
        end
    )
end

function _M.OnExit(self)
    for i = 1, 4 do
        self.comps['tbt_an' .. i].IsChecked = false
    end

    self.rolebagItem:UnlockCategory('tbt_all')
    self.rolebagItem:SetOnInitDetailCB(nil)
    self.rolebagItem:EnableMultiSelect(false)
    self.rolebagItem = nil
    self.listener:Stop(false)
    self.preBag:Cleanup()
    self.selectIndexs = nil
end

function _M.OnDestory(self)
    self.listener:Dispose()
end

function _M.OnInit(self)
    self.ui:EnableTouchFrame(false)
    --创建一个虚拟背包
    self.preBag = GameUtil.CreateCustomBag()

    self.list = ItemList(self.comps.cvs_allitem.Size2D, Constants.Item.DefaultSize, 4)
    self.list.Position2D = self.comps.cvs_allitem.Position2D
    self.list.ShowBackground = false
    self.list.EnableSelect = false
    self.list.OnItemClick = function(it)
        local itdata = self.listener:GetItemData(it)
        UIUtil.ShowNormalItemDetail({templateID = itdata.TemplateID, itemShow = it, autoHeight = true, x = 285})
    end

    self.comps.cvs_allitem.Parent:AddChild(self.list)
    self.listener = ItemListener(self.preBag, true, 0)
    for i = 1, 4 do
        self.comps['tbt_an' .. i].TouchClick = function(sender)
            OnQualitySelect(self, i, sender.IsChecked)
        end
    end
    self.comps.btn_decompose.TouchClick = function(sender)
        OnDecomposeButtonClick(self)
    end
    self.comps.btn_clean.TouchClick = function(sender)
        OnCleanSelects(self)
    end
end

return _M
