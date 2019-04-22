local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

local _M = {}
_M.__index = _M
local FillGemSlotFn

local function FillTotalAttribute(self)
    self.attrNode = self.attrNode or UIUtil.CreateCacheNodeGroup(self.comps.cvs_attribute, true)
    self.attrNode:SetInitCB(
        function(node, preNode)
            if preNode then
                node.Y = preNode.Y + preNode.Height + 10
            end
        end
    )

    local allAttrs = ItemModel.GetGridGemAttribute(self.selectEquipPos)
    allAttrs = ItemModel.MergerAttributeValue(allAttrs)

    self.attrNode:Reset(#allAttrs)
    local ret = self.attrNode:GetVisibleNodes()
    for i, v in ipairs(ret) do
        local lb_shuxing = UIUtil.FindChild(v, 'lb_shuxing')
        local lb_shuxingnum = UIUtil.FindChild(v, 'lb_shuxingnum')
        local attrName, value = ItemModel.GetAttributeString(allAttrs[i])
        lb_shuxing.Text = attrName
        lb_shuxingnum.Text = '+' .. value
    end
end

local function CalcRedTips(self)
    for k, v in pairs(self.parts) do
        local node = self.comps[v]
        local lb_red_up = UIUtil.FindChild(node, 'lb_red_up')
        local red_count = GlobalHooks.UI.GetRedData('gemstone', k)
        lb_red_up.Visible = red_count > 0
    end
end

local function CheckSlotLvup(self)
    for i = 1, self.gemHoleCount do
        local slotIndex = i
        local cvs_baoshi = self.comps['cvs_baoshi' .. slotIndex]
        local btn_up = UIUtil.FindChild(cvs_baoshi, 'btn_up')
        local gem = ItemModel.GetGridGem(self.selectEquipPos, slotIndex)
        btn_up.Visible = false
        if gem.GemTemplateID > 0 then
            local compinfo = GlobalHooks.DB.FindFirst('item_synthetic', {item_id = gem.GemTemplateID})
            if compinfo then
                local nextGemInfo = GlobalHooks.DB.FindFirst('equip_gem', {item_id = compinfo.target_id})
                if nextGemInfo.gem_lv <= DataMgr.Instance.UserData.Level then
                    local costs = ItemModel.ParseCostAndCostGroup(compinfo)
                    for _, v in ipairs(costs) do
                        if v.id == gem.GemTemplateID then
                            v.cur = v.cur + 1
                            break
                        end
                    end
                    local enoughcount = ItemModel.GetCostGroupEnoughCount(costs)
                    if enoughcount > 0 then
                        btn_up.Visible = true
                    end
                end
            end
        end
    end
end

local function OnUnEquipButtonClick(self, slotIndex, cb)
    ItemModel.RequestUnEquipGridGem(
        self.selectEquipPos,
        slotIndex,
        function()
            FillGemSlotFn(self, slotIndex)
            FillTotalAttribute(self)
            FillGridElementFn(self, self.selectNode, self.selectEquipPos)
            SoundManager.Instance:PlaySoundByKey('baoshi', false)
            CheckSlotLvup(self)
            if cb then
                cb()
            end
        end
    )
end

local function OnAddButtonClick(self, slotIndex)
    if self.listener.ItemCount == 0 then
        UIUtil.ShowMessage(Constants.Text.warn_notexitgem)
        SoundManager.Instance:PlaySoundByKey('button', false)
        return
    end
    local sourceIndex = self.listener:GetSourceIndex(0)
    ItemModel.RequestEquipGridGem(
        self.selectEquipPos,
        sourceIndex,
        slotIndex,
        function()
            FillGemSlotFn(self, slotIndex)
            FillTotalAttribute(self)
            FillGridElementFn(self, self.selectNode, self.selectEquipPos)
            CheckSlotLvup(self)
            SoundManager.Instance:PlaySoundByKey('baoshi', false)
        end
    )
end

local function OnEquipButtonClick(self, index)
    local sourceIndex = self.listener:GetSourceIndex(index - 1)
    local slotIndex = ItemModel.GetFirstEmptyGemSlot(self.selectEquipPos)

    if not slotIndex then
        UIUtil.ShowMessage(Constants.Text.warn_notexitgemSlot)
        return
    end
    ItemModel.RequestEquipGridGem(
        self.selectEquipPos,
        sourceIndex,
        slotIndex,
        function()
            FillGemSlotFn(self, slotIndex, true)
            FillTotalAttribute(self)
            FillGridElementFn(self, self.selectNode, self.selectEquipPos)
            CheckSlotLvup(self)
            SoundManager.Instance:PlaySoundByKey('baoshi', false)
        end
    )
end

local function FillBagGemList(self)
    local gemCount = self.listener.ItemCount
    local function UpdateElement(node, index)
        local cvs_icon = UIUtil.FindChild(node, 'cvs_icon')
        local lb_name = UIUtil.FindChild(node, 'lb_name')
        local lb_lv = UIUtil.FindChild(node, 'lb_lv')
        local lb_lvclour = UIUtil.FindChild(node, 'lb_lvclour')
        local btn_use = UIUtil.FindChild(node, 'btn_use')
        local btn_up = UIUtil.FindChild(node, 'btn_up')

        local it = self.listener:GetItemData(index - 1)
        local detail = ItemModel.GetDetailByTemplateID(it.TemplateID)
        lb_name.Text = Util.GetText(detail.static.name)

        local itshow = UIUtil.SetItemShowTo(cvs_icon, it)
        local gemInfo = self.curStatic[it.TemplateID]
        local attrs = ItemModel.GetXlsFixedAttribute(gemInfo)
        lb_lv.Visible = #attrs > 0
        if #attrs > 0 then
            --取第一个属性显示
            local attrName, value = ItemModel.GetAttributeString(attrs[1])
            lb_lv.Text = attrName .. '+' .. value
        end
        lb_lv.Visible = #attrs > 0
        lb_lvclour.Visible = #attrs > 1
        if #attrs > 1 then
            local attrName, value = ItemModel.GetAttributeString(attrs[2])
            lb_lvclour.Text = attrName .. '+' .. value
        end
        btn_up.Visible = false
        local compinfo = GlobalHooks.DB.FindFirst('item_synthetic', {item_id = it.TemplateID})
        if compinfo then
            local nextGemInfo = GlobalHooks.DB.FindFirst('equip_gem', {item_id = compinfo.target_id})
            if nextGemInfo.gem_lv <= DataMgr.Instance.UserData.Level then
                local costs = ItemModel.ParseCostAndCostGroup(compinfo)
                local enoughcount = ItemModel.GetCostGroupEnoughCount(costs)
                if enoughcount > 0 then
                    btn_up.Visible = true
                    btn_up.TouchClick = function()
                        ItemModel.RequestComposeItem(
                            compinfo.target_id,
                            enoughcount,
                            function()
                                CheckSlotLvup(self)
                            end
                        )
                    end
                end
            end
        end
        btn_use.TouchClick = function()
            OnEquipButtonClick(self, index)
        end
    end
    UIUtil.ConfigVScrollPan(self.comps.sp_oar, self.comps.cvs_item, gemCount, UpdateElement)
end

local function AutoComposeAndEquip(self, slotIndex, compinfo)
    local function Equip()
        --查找背包此ID道具
        local index = ItemModel.FindFirstBagIndexByTemplateID(compinfo.target_id)
        if index then
            ItemModel.RequestEquipGridGem(
                self.selectEquipPos,
                index,
                slotIndex,
                function()
                    FillGemSlotFn(self, slotIndex, true)
                    FillTotalAttribute(self)
                    FillGridElementFn(self, self.selectNode, self.selectEquipPos)
                    SoundManager.Instance:PlaySoundByKey('baoshi', false)
                    CheckSlotLvup(self)
                end
            )
        end
    end
    local function ComposeAndEquip()
        ItemModel.RequestComposeItem(compinfo.target_id, 1, Equip)
    end
    OnUnEquipButtonClick(self, slotIndex, ComposeAndEquip)
end

local function FillGemSlot(self, slotIndex, effect)
    local cvs_baoshi = self.comps['cvs_baoshi' .. slotIndex]
    local cvs_icon = UIUtil.FindChild(cvs_baoshi, 'cvs_icon')
    local lb_lv = UIUtil.FindChild(cvs_baoshi, 'lb_lv')
    local lb_locktext = UIUtil.FindChild(cvs_baoshi, 'lb_locktext')
    local lb_locknum = UIUtil.FindChild(cvs_baoshi, 'lb_locknum')
    local btn_up = UIUtil.FindChild(cvs_baoshi, 'btn_up')
    local lv = self.gemHole[slotIndex].player_lv
    lb_locknum.Text = Util.Format1234(Constants.Text.gem_locklv_format, lv)
    btn_up.Visible = false
    local gem = ItemModel.GetGridGem(self.selectEquipPos, slotIndex)
    if gem.GemTemplateID > 0 then
        local gemInfo = GlobalHooks.DB.FindFirst('equip_gem', {item_id = gem.GemTemplateID})
        -- lb_lv.Text = Util.Format1234(Constants.Text.gem_locklv_format, gemInfo.gem_lv)
        lb_lv.Text = Util.GetText(gemInfo.gem_showtext)
        local gemDetail = ItemModel.GetDetailByTemplateID(gem.GemTemplateID)
        local compinfo = GlobalHooks.DB.FindFirst('item_synthetic', {item_id = gem.GemTemplateID})
        if compinfo then
            local nextGemInfo = GlobalHooks.DB.FindFirst('equip_gem', {item_id = compinfo.target_id})
            if nextGemInfo.gem_lv <= DataMgr.Instance.UserData.Level then
                local costs = ItemModel.ParseCostAndCostGroup(compinfo)
                for _, v in ipairs(costs) do
                    if v.id == gem.GemTemplateID then
                        v.cur = v.cur + 1
                        break
                    end
                end
                local enoughcount = ItemModel.GetCostGroupEnoughCount(costs)
                btn_up.Visible = enoughcount > 0
                btn_up.TouchClick = function()
                    AutoComposeAndEquip(self, slotIndex, compinfo)
                end
            end
        end
        local itshow = UIUtil.SetItemShowTo(cvs_icon, gemDetail.static.atlas_id, gemDetail.static.quality)
        itshow.EnableTouch = true
        itshow.IsCircleQualtiy = true
        itshow.TouchClick = function(sender)
            -- 点击直接卸下
            OnUnEquipButtonClick(self, slotIndex)
            -- local buttons = {
            --     {
            --         cb = function()
            --             OnUnEquipButtonClick(self, slotIndex)
            --         end,
            --         Text = Constants.Text.detail_btn_popGem
            --     }
            -- }
            -- UIUtil.ShowNormalItemDetail({buttons = buttons, detail = gemDetail, autoHeight = true})
        end
    else
        UIUtil.RemoveItemShowFrom(cvs_icon)
    end

    lb_lv.Visible = gem.GemTemplateID > 0
    cvs_icon.Enable = DataMgr.Instance.UserData.Level >= lv
    lb_locktext.Visible = not cvs_icon.Enable
    lb_locknum.Visible = not cvs_icon.Enable
    cvs_baoshi.IsGray = not cvs_icon.Enable
    cvs_icon.UserTag = slotIndex
    cvs_icon.TouchClick = function(sender)
        OnAddButtonClick(self, cvs_icon.UserTag)
    end
    if effect then
        local t = {DisableToUnload = true, Parent = cvs_baoshi.Transform, LayerOrder = self.menu.MenuOrder, Pos = {x = 43, y = -47}, UILayer = true}
        Util.PlayEffect('/res/effect/ui/ef_ui_interface_consume.assetbundles', t)
    end
end

FillGemSlotFn = FillGemSlot

local function OnPartSelect(self, node, equipPos)
    if self.selectNode then
        self.selectNode.Enable = true
    end
    self.selectNode = node
    self.selectNode.Enable = false
    self.selectEquipPos = equipPos
    self.curStatic = {}
    local gemStatic = GlobalHooks.DB.Find('equip_gem', {equip_pos = equipPos})
    for _, v in ipairs(gemStatic) do
        self.curStatic[v.item_id] = v
    end

    for i = 1, self.gemHoleCount do
        FillGemSlot(self, i)
    end

    FillTotalAttribute(self)

    self.listener:Start(true, true)
    FillBagGemList(self)
end

local function FillGridElement(self, node, equipPos)
    local cvs_icon = UIUtil.FindChild(node, 'cvs_icon')
    local lb_name = UIUtil.FindChild(node, 'lb_name')

    for i = 1, self.gemHoleCount do
        local ib_inlay = UIUtil.FindChild(node, 'ib_inlay' .. i)
        ib_inlay.Enable = ItemModel.IsExistGem(equipPos, i)
    end

    local detail = ItemModel.GetDetailByEquipBagIndex(equipPos)
    if detail then
        -- lb_name.Text = Util.GetText(detail.static.name)
        local itshow = UIUtil.SetItemShowTo(cvs_icon, detail)
        itshow.EnableTouch = true
        cvs_icon.Enable = false
        itshow.TouchClick = function()
            UIUtil.ShowNormalItemDetail({detail = detail, x = 230, item = itshow, autoHeight = true})
        end
    else
        -- lb_name.Text = Constants.EquipPartName[equipPos]
        UIUtil.RemoveItemShowFrom(cvs_icon)
    end
end

FillGridElementFn = FillGridElement
local function FillList(self)
    for k, v in pairs(self.parts) do
        FillGridElement(self, self.comps[v], k)
    end
    OnPartSelect(self, self.comps.cvs_equip, 1)
end

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter(self, ...)
    FillList(self)
    CalcRedTips(self)
    self.sub_fn = function()
        CalcRedTips(self)
    end
    EventManager.Subscribe('Event.RedTips.gemstone', self.sub_fn)
    self.comps.lb_tip2.Text = Util.GetText('equip_gem_text', math.floor(DataMgr.Instance.UserData.Level / 10))
end

function _M.OnExit(self)
    self.listener:Stop(false)
    EventManager.Unsubscribe('Event.RedTips.gemstone', self.sub_fn)
end

function _M.OnDestory(self)
    self.listener:Dispose()
end

local function OnGemTypeSelect(self, sender)
    if self.comps.tbt_normal == sender then
        self.selectGemType = 1
    else
        self.selectGemType = 2
    end
    if self.selectEquipPos then
        self.listener:Start(true, true)
        FillBagGemList(self)
    end
end

function _M.OnInit(self)
    self.parts = {
        [1] = 'cvs_equip',
        [2] = 'cvs_helmet',
        [3] = 'cvs_clothes',
        [4] = 'cvs_pants',
        [5] = 'cvs_belt',
        [6] = 'cvs_shoe',
        [7] = 'cvs_necklace',
        [8] = 'cvs_ring'
    }

    UIUtil.ConfigToggleButton(
        {self.comps.tbt_normal, self.comps.tbt_color},
        self.comps.tbt_normal,
        false,
        function(sender)
            OnGemTypeSelect(self, sender)
        end
    )
    for k, v in pairs(self.parts) do
        self.comps[v].Enable = true
        self.comps[v].TouchClick = function(node)
            --print("on touchclick", k, v)
            OnPartSelect(self, node, k)
        end
    end

    self.comps.cvs_item.Visible = false

    self.gemHole = {}
    self.gemHoleCount = 0
    local holes = GlobalHooks.DB.Find('equip_gem_hole', {})
    for _, v in ipairs(holes) do
        self.gemHoleCount = self.gemHoleCount + 1
        self.gemHole[v.hole_order] = v
    end

    self.listener = ItemListener(DataMgr.Instance.UserData.Bag, false, 0)
    local function CheckTypeGem(it)
        local v = self.curStatic[it.TemplateID]
        if v and v.gem_type == self.selectGemType and v.gem_lv <= DataMgr.Instance.UserData.Level then
            return true
        end
        return false
    end
    self.listener.OnMatch = CheckTypeGem

    local function CompareGem(it1, it2)
        local detail1 = ItemModel.GetDetailByItemData(it1)
        local detail2 = ItemModel.GetDetailByItemData(it2)
        if detail1.static.item_turn ~= detail2.static.item_turn then
            return detail2.static.item_turn - detail1.static.item_turn
        end
        if detail2.static.quality == detail1.static.quality then
            return (detail1.static.level_limit - detail2.static.level_limit)
        else
            return (detail2.static.quality - detail1.static.quality)
        end
    end

    self.listener.OnCompare = CompareGem
    self.listener.OnItemUpdateAction = function(act)
        --重刷列表
        if act.Type ~= ItemUpdateAction.ActionType.Init then
            FillBagGemList(self)
        end
    end
    self.comps.btn_buy.TouchClick = function()
        local funid = GlobalHooks.DB.GetGlobalConfig('inlay_buy_interface')
        FunctionUtil.OpenFunction(funid)
    end
end

return _M
