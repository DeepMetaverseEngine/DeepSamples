local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'
local _M = {}
_M.__index = _M

local function SetElementAttr(node, attr, random_attr, effect)
    local attrName, value = ItemModel.GetAttributeString(attr)
    local lb_shuxing = UIUtil.FindChild(node, 'lb_shuxing')
    local gg_shuxing = UIUtil.FindChild(node, 'gg_shuxing')
    local btn_tip = UIUtil.FindChild(node, 'btn_tip')

    lb_shuxing.Text = attrName
    gg_shuxing.Text = value
    local attrid = attr.ID
    --获取属性最大值
    local random_info = GlobalHooks.DB.FindFirst('EquipRandomExtraAttr', {random_extra_attr = random_attr, attr_id = attrid})
    gg_shuxing:SetGaugeMinMax(0, random_info.attr_max)
    gg_shuxing.Value = math.min(attr.Value, random_info.attr_max)

    -- print('attr.Value min, max',attr.Value,random_attr.attr_min,random_attr.attr_max)
    --TODO buff介绍
    btn_tip.TouchClick = function(sender)
        print('TODO buff介绍')
    end
end

local function FillPreviewInfo(self, detail, effect)
    local curPre = self.previewMap[self.selectItemID]
    self.comps.btn_keep.Visible = curPre ~= nil
    self.comps.cvs_new.Visible = curPre ~= nil
    self.comps.ib_bjtu.Visible = curPre ~= nil
    if not curPre then
        self.comps.cvs_begin.Visible = true
        return
    else
        self.comps.cvs_begin.Visible = false
    end

    self.attrRightCache = self.attrRightCache or UIUtil.CreateCacheNodeGroup(self.comps.cvs_shuxing2, true)
    self.attrRightCache:SetInitCB(
        function(node, preNode)
            if preNode then
                node.Y = preNode.Y + preNode.Height + 10
            end
        end
    )
    self.attrRightCache:Reset(#curPre)
    local ret = self.attrRightCache:GetVisibleNodes()
    for i, node in ipairs(ret) do
        SetElementAttr(node, curPre[i], detail.static_equip.random_extra_attr)
        if effect then
            local t = {
                LayerOrder = self.menu.MenuOrder,
                UILayer = true,
                DisableToUnload = true,
                Parent = node.Transform,
                Pos = {x = 105, y = -11}
            }
            Util.PlayEffect('/res/effect/ui/ef_ui_interface_refresh.assetbundles', t)
        end
    end
    self.comps.lb_num2.Text = ItemModel.CalcAttributesScore(curPre)
end

local function GetNearCost(quality, lock_num, equiplv)
    -- local search = {quality = quality, lock_num = lock_num}
    local cost_static =
        GlobalHooks.DB.FindFirst(
        'equip_checkup',
        function(ele)
            return ele.quality == quality and ele.lock_num == lock_num and ele.equip_level_min <= equiplv and ele.equip_level_max >= equiplv
        end
    )
    if not cost_static and lock_num > 0 then
        return GetNearCost(quality, lock_num - 1, equiplv)
    end
    return cost_static
end

local function FillNextPreviewCost(self, detail)
    local lock_num = 0
    local currentRandomAttrs = ItemModel.GetEquipAttribute(detail, ItemPropertyData.ExtraAttributeTag)
    for _, v in ipairs(currentRandomAttrs) do
        if ItemModel.IsAttritbuteLocked(v) then
            lock_num = lock_num + 1
        end
    end
    print('locknum ---', lock_num)
    local cost_static = GetNearCost(detail.static.quality, lock_num, detail.static.level_limit)
    self.comps.cvs_cost.Visible = cost_static ~= nil
    if not cost_static then
        return
    end
    local cost = ItemModel.ParseCostAndCostGroup(cost_static)
    self.costNodeCache = self.costNodeCache or UIUtil.CreateCacheNodeGroup(self.comps.cvs_costitem, true)
    self.costNodeCache:Reset(#cost)
    local ret = self.costNodeCache:GetVisibleNodes()
    for i, v in ipairs(cost) do
        local node = ret[i]
        local lb_num = UIUtil.FindChild(node, 'lb_num')
        UIUtil.SetEnoughItemShowAndLabel(self, node, lb_num, v)
    end

    local offsetx = 34
    local start = self.comps.cvs_cost.Width - (self.comps.cvs_costitem.Width * #cost) - (#cost - 1) * offsetx
    local start = start * 0.5
    UIUtil.SetNodesToCenterStyle(self.comps.cvs_cost.Width, self.comps.cvs_costitem.Width, true, ret, false, start)
end

local function LockAttribute(self, attr, sender)
    local isChecked = sender.IsChecked
    if isChecked then
        local unlockCount = 0
        for _, v in ipairs(self.identifyAttrs) do
            if not ItemModel.IsAttritbuteLocked(v) then
                unlockCount = unlockCount + 1
            end
        end
        if unlockCount <= 1 then
            sender.IsChecked = false
            UIUtil.ShowErrorMessage(Constants.Text.warn_maxLockCount)
            return
        end
    end

    local function AlertOK()
        ItemModel.RequestLockAttribute(
            self.selectItemID,
            attr,
            isChecked,
            function()
                --锁定成功
                FillNextPreviewCost(self, ItemModel.GetDetailByID(self.selectItemID))
            end,
            function()
                --失败
                sender.IsChecked = not isChecked
            end
        )
    end

    local function AlertCancel()
        sender.IsChecked = not isChecked
    end
    if isChecked then
        UIUtil.ShowCheckBoxConfirmAlert('identify_lock', Constants.Text.confirm_lockattr, Constants.Text.confirm_lockattr_title, AlertOK, AlertCancel)
    else
        AlertOK()
    end
end

local function FillEquipAttribute(self, detail, effect)
    self.attrLeftCache = self.attrLeftCache or UIUtil.CreateCacheNodeGroup(self.comps.cvs_shuxing1, true)
    self.attrLeftCache:SetInitCB(
        function(node, preNode)
            if preNode then
                node.Y = preNode.Y + preNode.Height + 10
            end
        end
    )

    local currentRandomAttrs = ItemModel.GetEquipAttribute(detail, ItemPropertyData.ExtraAttributeTag)
    local buffAttrs = ItemModel.GetEquipAttribute(detail, ItemPropertyData.ExtraBuffTag)
    table.appendList(currentRandomAttrs, buffAttrs)

    self.attrLeftCache:Reset(#currentRandomAttrs)
    local random_extra_id = detail.static_equip.random_extra_attr
    local ret = self.attrLeftCache:GetVisibleNodes()
    for i, node in ipairs(ret) do
        local attr = currentRandomAttrs[i]
        SetElementAttr(node, attr, random_extra_id)
        if effect then
            local t = {
                LayerOrder = self.menu.MenuOrder,
                UILayer = true,
                DisableToUnload = true,
                Parent = node.Transform,
                Pos = {x = 123, y = -11}
            }
            Util.PlayEffect('/res/effect/ui/ef_ui_interface_refresh.assetbundles', t)
        end

        local tbt_lock = UIUtil.FindChild(node, 'tbt_lock')
        tbt_lock.IsChecked = ItemModel.IsAttritbuteLocked(attr)
        tbt_lock.TouchClick = function(sender)
            local isChecked = sender.IsChecked
            LockAttribute(self, attr, sender)
        end
    end
    self.identifyAttrs = currentRandomAttrs
    self.comps.lb_num1.Text = ItemModel.CalcAttributesScore(currentRandomAttrs)
end

local function OnSelectEquip(self, detail)
    local itshow = UIUtil.SetItemShowTo(self.comps.cvs_selecticon, detail)
    itshow.EnableTouch = true
    itshow.TouchClick = function(sender)
        UIUtil.ShowNormalItemDetail({detail = ItemModel.GetDetailByID(self.selectItemID), autoHeight = true})
    end
    self.selectItemID = detail.id
    FillEquipAttribute(self, detail)
    FillPreviewInfo(self, detail)
    FillNextPreviewCost(self, detail)
end

local function SwitchListener(self, nextListener)
    if self.listener then
        self.listener:Stop(false)
    end
    self.listener = nextListener
    self.listener:Start(true, false)
    return CSharpArray2Table(self.listener.AllItems)
end

local function OnEquipNodeSelect(self, sender)
    if self.selectNode then
        self.selectNode.Enable = true
    end
    self.selectNode = sender
    self.selectNode.Enable = false

    OnSelectEquip(self, ItemModel.GetDetailByID(sender.UserData))
end

--todo bug : 不依赖UserTag, 依赖无法跨越缓存
local function SelectByIndex(self, index)
    UIUtil.MoveToScrollCell(
        self.comps.sp_oar,
        index,
        function(node)
            OnEquipNodeSelect(self, node)
        end
    )
end

local function ShowEquipList(self, allItems)
    local function UpdateElement(node, index)
        local cvs_icon = UIUtil.FindChild(node, 'cvs_icon')
        local lb_name = UIUtil.FindChild(node, 'lb_name')
        local lb_lv = UIUtil.FindChild(node, 'lb_lv')
        local it = allItems[index]
        local detail = ItemModel.GetDetailByItemData(it)

        UIUtil.SetItemShowTo(cvs_icon, detail)
        lb_name.Text = Util.GetText(detail.static.name)
        lb_lv.Text = Util.Format1234(Constants.Text.equip_lv_format, detail.static.level_limit)
        node.UserData = it.ID
        node.TouchClick = function(sender)
            OnEquipNodeSelect(self, sender)
        end
        if self.selectItemID then
            node.Enable = self.selectItemID ~= it.ID
        else
            node.Enable = true
        end
    end
    UIUtil.ConfigVScrollPan(self.comps.sp_oar, self.comps.cvs_item, #allItems, UpdateElement)
    self.comps.cvs_intensify.Visible = #allItems > 0
    --默认选中第一个
    if #allItems > 0 then
        SelectByIndex(self, 1)
    end
end

local function OnToggerButtonClick(self, sender)
    if not self.previewMap then
        return
    end
    local allItems
    if sender == self.ui.comps.tbt_1 then
        allItems = SwitchListener(self, self.equipedListener)
    elseif sender == self.ui.comps.tbt_2 then
        allItems = SwitchListener(self, self.bagListener)
    end
    ShowEquipList(self, allItems)
end

local function OnIdentifyButtonClick(self)
    self.comps.cvs_begin.Visible = false
    ItemModel.RequestIdentifyEquipPreview(
        self.selectItemID,
        function(attrs)
            self.previewMap[self.selectItemID] = attrs
            local detail = ItemModel.GetDetailByID(self.selectItemID)
            FillPreviewInfo(self, detail, true)
            FillNextPreviewCost(self, detail)
            SoundManager.Instance:PlaySoundByKey('shuxingshuaxin', false)
        end
    )
end

local function OnSaveButtonClick(self)
    self.comps.cvs_begin.Visible = true
    ItemModel.RequestSaveIdentifyPreview(
        self.selectItemID,
        function()
            self.previewMap[self.selectItemID] = nil
            local detail = ItemModel.GetDetailByID(self.selectItemID)
            FillEquipAttribute(self, detail, true)
            FillPreviewInfo(self, detail)
        end
    )
end

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter(self, ...)
    self.selectItemID = nil
    ItemModel.RequestIdentifyPreviewInfo(
        function(ret)
            self.previewMap = self.previewMap or {}
            for _, v in ipairs(ret or {}) do
                self.previewMap[v.ID] = v.Properties
            end
            --默认选中已装备
            if not self.ui.comps.tbt_1.IsChecked then
                self.ui.comps.tbt_1.IsChecked = true
            else
                OnToggerButtonClick(self, self.ui.comps.tbt_1)
            end
        end
    )
end

function _M.OnExit(self)
    self.listener:Stop(false)
end

function _M.OnDestory(self)
    self.bagListener:Dispose()
    self.equipedListener:Dispose()
end

function _M.OnInit(self)
    local bag = DataMgr.Instance.UserData.Bag
    local equipedBag = DataMgr.Instance.UserData.EquipBag
    self.bagListener = ItemListener(bag, false, bag.Size)
    self.equipedListener = ItemListener(equipedBag, false, equipedBag.Size)

    local function CheckEquip(it)
        local ret = not string.IsNullOrEmpty(it.ID)
        local detail = ItemModel.GetDetailByItemData(it)
        return ret and detail.static_equip.random_extra_attr ~= 0
    end

    local function CompareEquip(it1, it2)
        local detail1 = ItemModel.GetDetailByItemData(it1)
        local detail2 = ItemModel.GetDetailByItemData(it2)
        if detail1.score == detail2.score then
            if detail2.static.quality == detail1.static.quality then
                return (detail1.static.level_limit - detail2.static.level_limit)
            else
                return (detail2.static.quality - detail1.static.quality)
            end
        else
            return (detail2.score - detail1.score)
        end
    end

    self.bagListener.OnMatch = CheckEquip
    self.equipedListener.OnMatch = CheckEquip

    self.bagListener.OnCompare = CompareEquip
    self.equipedListener.OnCompare = CompareEquip

    self.comps.cvs_item.Visible = false
    UIUtil.ConfigToggleButton(
        {self.comps.tbt_1, self.comps.tbt_2},
        self.comps.tbt_1,
        false,
        function(sender)
            OnToggerButtonClick(self, sender)
        end
    )
    self.comps.btn_use.TouchClick = function(sender)
        OnIdentifyButtonClick(self)
    end

    self.comps.btn_keep.TouchClick = function(sender)
        OnSaveButtonClick(self)
    end
end

return _M
