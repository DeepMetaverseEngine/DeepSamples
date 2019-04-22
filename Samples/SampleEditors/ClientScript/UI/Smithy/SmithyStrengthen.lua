local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'
local _M = {}
_M.__index = _M

local function GetRefineLevelStr(refine_static)
    if not refine_static or refine_static.refine_rank == 0 then
        return Constants.Text.strengthen_nonelv
    else
        return Util.GetText(Constants.Text.strengthen_ranklv, refine_static.refine_rank, refine_static.refine_lv)
    end
end

local function GetRefineStatic(self, equipPos)
    local d = self.refineMap[equipPos]
    if d then
        return GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = d.Rank, refine_lv = d.Lv})
    else
        return {refine_rank = 0, refine_lv = 0, refine_plus = 0}
    end
end

local function GetNextLevel(self)
    if not self.selectData then
        return GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = 1, refine_lv = 1})
    end

    local rank = self.selectData.Rank
    local lv = self.selectData.Lv
    local refines = GlobalHooks.DB.Find('EquipRefine', {refine_rank = rank})

    local maxRefine = refines[1]
    for _, v in ipairs(refines) do
        maxRefine = (v.refine_lv > maxRefine.refine_lv and v) or maxRefine
    end

    if lv + 1 <= maxRefine.refine_lv then
        return GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = rank, refine_lv = lv + 1})
    else
        return GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = rank + 1, refine_lv = 1})
    end
end

local function FillAttributeTips(self)
    if not self.selectEquipPos then
        return
    end
    local equipDetail = ItemModel.GetDetailByEquipBagIndex(self.selectEquipPos, true)
    if not equipDetail then
        return
    end
    local attrs = ItemModel.GetEquipAttribute(equipDetail, ItemPropertyData.FixedAttributeTag)
    self.cacheTipsNode = self.cacheTipsNode or {}

    local nextY
    for _, v in ipairs(self.cacheTipsNode) do
        v.Visible = false
    end
    nextY = nextY or self.comps.cvs_info1.Y

    while #self.cacheTipsNode < #attrs do
        local node = self.comps.cvs_info1:Clone()
        self.comps.cvs_tips_show:AddChild(node)
        table.insert(self.cacheTipsNode, node)
        node.Y = nextY
        nextY = nextY + node.Height
    end

    for i, v in ipairs(attrs) do
        local node = self.cacheTipsNode[i]
        node.Visible = true
        local lb_attribute = UIUtil.FindChild(node, 'lb_attribute')
        local lb_num1 = UIUtil.FindChild(node, 'lb_num1')
        local lb_num2 = UIUtil.FindChild(node, 'lb_num2')
        local attrName, value = ItemModel.GetAttributeString(v)
        lb_attribute.Text = attrName
        lb_num1.Text = math.floor(v.Value * (1 + self.selectRefine.refine_plus / 10000))

        lb_num2.Text = math.floor(v.Value * (1 + self.selectNextRefine.refine_plus / 10000))
    end
    self.comps.cvs_tips_show.Height = self.cacheTipsNode[#attrs].Y + self.cacheTipsNode[#attrs].Height + 10
    UIUtil.AdjustToCenter(self.ui.root, self.comps.cvs_tips_show, 120, 0)
end

local function ShowPartEquipInfo(self)
    self.selectRefine = GetRefineStatic(self, self.selectEquipPos)
    self.selectNextRefine = GetNextLevel(self)
    local lb_lv = UIUtil.FindChild(self.selectNode, 'lb_lv', true)
    lb_lv.Text = GetRefineLevelStr(self.selectRefine)

    local noRefine = not self.selectRefine or self.selectRefine.refine_rank == 0
    self.comps.lb_wenzi0.Visible = not noRefine
    self.comps.lb_wenzi1.Visible = not noRefine
    self.comps.cvs_gg.Visible = not noRefine
    self.comps.lb_wenzi_center.Visible = noRefine

    self.comps.lb_wenzi0.Text = Util.GetText(Constants.Text.strengthen_rank, self.selectRefine.refine_rank)
    self.comps.lb_wenzi1.Text = Util.GetText(Constants.Text.strengthen_lv, self.selectRefine.refine_lv)

    -- 级亮点
    self.comps.gg_level:SetGaugeMinMax(0, 1)
    if self.selectRefine.refine_lv == 0 then
        self.comps.gg_level.Value = 0
    else
        self.comps.gg_level.Value = self.selectRefine.refine_lv / 5
    end
    -- 附加属性
    local rankAttr = GlobalHooks.DB.FindFirst('EquipRefineRank', {equip_pos = self.selectEquipPos, refine_rank = self.selectRefine.refine_rank + 1})

    self.comps.lb_wenzi3.Visible = rankAttr ~= nil
    if rankAttr then
        local attrs = ItemModel.GetXlsFixedAttribute(rankAttr)
        -- 暂没有多条属性
        -- local attr_str = ""
        -- for _, v in ipairs(attrs) do
        --     local attrName, value = ItemModel.GetAttributeString(v)
        --     attr_str = attr_str .. attrName .. "+" .. value .. " "
        -- end
        local attrName, value = ItemModel.GetAttributeString(attrs[1])
        self.comps.lb_wenzi3.Text = Util.GetText(Constants.Text.strengthen_rankadded, self.selectRefine.refine_rank + 1, attrName, value)
    end

    --当前名称
    if self.comps.lb_wenzi_center.Visible then
        self.comps.ib_text1.Text = self.comps.lb_wenzi_center.Text
    else
        self.comps.ib_text1.Text = self.comps.lb_wenzi0.Text .. self.comps.lb_wenzi1.Text
    end
    self.comps.ib_text3.Text = '+' .. (self.selectRefine.refine_plus / 100) .. '%'
    local detail = ItemModel.GetDetailByEquipBagIndex(self.selectEquipPos)
    if detail then
        ItemModel.ReCalcDetailScore(detail, true)
    end
    --基础装备战力·
    self.comps.ib_text5.Text = detail and detail.score or 0

    local nextRefine = self.selectNextRefine
    self.comps.ib_bjtu.Visible = nextRefine ~= nil
    self.comps.cvs_cost.Visible = nextRefine ~= nil
    self.comps.btn_use.Visible = nextRefine ~= nil
    self.comps.cvs_new.Visible = nextRefine ~= nil
    self.comps.btn_tip.Visible = nextRefine ~= nil

    if not nextRefine then
        return
    end
    --下一等级名称
    self.comps.ib_text6.Text = GetRefineLevelStr(nextRefine)
    --下一等级强化系数
    self.comps.ib_text8.Text = '+' .. (nextRefine.refine_plus / 100) .. '%'
    if nextRefine.refine_plus == self.selectRefine.refine_plus then
        --下一等级装备战力
        self.comps.ib_text10.Text = self.comps.ib_text5.Text
    else
        local detail_next = ItemModel.GetDetailByEquipBagIndex(self.selectEquipPos, true)
        if detail_next then
            for _, v in ipairs(detail_next.dynamicAttrs) do
                if v.Tag == 'FixedAttributeTag' then
                    v.BaseValue = v.Value
                    v.Value = math.floor(v.Value * (1 + nextRefine.refine_plus / 10000))
                end
            end
            ItemModel.ReCalcDetailScore(detail_next,true)
        end
        --下一等级装备战力
        self.comps.ib_text10.Text = detail_next and detail_next.score or 0
    end
    self.comps.btn_tip.Visible = detail ~= nil

    local cost = ItemModel.ParseCostAndCostGroup(nextRefine)

    self.cache_costNode = self.cache_costNode or UIUtil.CreateCacheNodeGroup(self.comps.cvs_costitem, true)
    local halfY = (self.comps.cvs_cost.Height - self.comps.cvs_costitem.Height) * 0.5
    self.cache_costNode:SetInitCB(
        function(node, preNode)
            -- print(' node ----y',halfY)
            node.Y = halfY
        end
    )
    -- cost[#cost+1] = cost[1]
    -- cost[#cost+1] = cost[2]
    self.cache_costNode:Reset(#cost)
    local ret = self.cache_costNode:GetVisibleNodes()

    for i, v in ipairs(cost) do
        local node = ret[i]
        local lb_num = UIUtil.FindChild(node, 'lb_num')
        UIUtil.SetEnoughItemShowAndLabel(self, node, lb_num, v, {x = 525, y = 428, anchor = 'l_b'})
    end

    local offsetx = 34
    local start = self.comps.cvs_cost.Width - (self.comps.cvs_costitem.Width * #cost) - (#cost - 1) * offsetx
    local start = start * 0.5
    UIUtil.SetNodesToCenterStyle(self.comps.cvs_cost.Width, self.comps.cvs_costitem.Width, true, ret, false, start)
    FillAttributeTips(self)
end

local function OnPartSelect(self, node, equipPos)
    if self.selectNode then
        self.selectNode.Enable = true
    end
    self.selectNode = node
    self.selectNode.Enable = false
    self.selectEquipPos = equipPos
    self.selectData = self.refineMap[equipPos]

    ShowPartEquipInfo(self)
end

local function FillElement(self, node, equipPos)
    local cvs_icon = UIUtil.FindChild(node, 'cvs_icon')
    local lb_name = UIUtil.FindChild(node, 'lb_name')
    local lb_lv = UIUtil.FindChild(node, 'lb_lv')

    local cur = GetRefineStatic(self, equipPos)
    lb_lv.Text = GetRefineLevelStr(cur)

    local detail = ItemModel.GetDetailByEquipBagIndex(equipPos)
    if detail then
        -- lb_name.Text = Util.GetText(detail.static.name)
        local itshow = UIUtil.SetItemShowTo(cvs_icon, detail)
        itshow.EnableTouch = true
        itshow.TouchClick = function()
            local reDetail = ItemModel.GetDetailByEquipBagIndex(equipPos)
            UIUtil.ShowNormalItemDetail({itemShow = itshow, detail = reDetail, x = 230, autoHeight = true})
        end
    else
        -- lb_name.Text = Constants.EquipPartName[equipPos]
        UIUtil.RemoveItemShowFrom(cvs_icon)
    end
end
-- todo 去掉refineMap 使用ItemModel的数据
local function FillList(self, data)
    self.refineMap = data
    for k, v in pairs(self.parts) do
        FillElement(self, self.comps[v], k)
    end
    OnPartSelect(self, self.comps.cvs_equip, 1)
end

local function FillRankAttribute(self)
    self.comps.cvs_elevate.Visible = true
    local all = GlobalHooks.DB.Find('EquipRefineRank', {equip_pos = self.selectEquipPos})

    local function UpdateEach(node, index)
        local cur = all[index]
        local attrs = ItemModel.GetXlsFixedAttribute(cur)
        local attrName, value = ItemModel.GetAttributeString(attrs[1])

        if index > 1 then
            local precur = all[index - 1]
            local preattrs = ItemModel.GetXlsFixedAttribute(precur)
            local preattrName, prevalue = ItemModel.GetAttributeString(preattrs[1])
            -- print('value ----',value, prevalue)
            value = value - prevalue
        end

        local lb_lv = UIUtil.FindChild(node, 'lb_lv')
        local lb_attribute = UIUtil.FindChild(node, 'lb_attribute')
        local lb_num3 = UIUtil.FindChild(node, 'lb_num3')
        local ib_lock = UIUtil.FindChild(node, 'ib_lock')
        lb_lv.Text = Util.GetText(Constants.Text.strengthen_rank, cur.refine_rank)
        lb_attribute.Text = attrName
        lb_num3.Text = '+' .. value
        local locked = true
        if self.selectData and self.selectData.Rank >= cur.refine_rank then
            locked = false
        end
        node.Enable = not locked
        ib_lock.Visible = locked
    end

    UIUtil.ConfigVScrollPanWithOffset(self.comps.sp_facelist, self.comps.cvs_lvnum, #all, 6, UpdateEach)
end

local function CalcRedTips(self)
    for k, v in pairs(self.parts) do
        local node = self.comps[v]
        local lb_red_up = UIUtil.FindChild(node,'lb_red_up')
        local red_count = GlobalHooks.UI.GetRedData('strengthen', k)
        if red_count and red_count > 0 then
            lb_red_up.Visible = true
        else
            lb_red_up.Visible = false
        end
    end
end

local function OnRefineButtonClick(self)
    ItemModel.RequestGridRefine(
        self.selectEquipPos,
        function(data)
            local rank = self.selectData and self.selectData.Rank or 0
            local t = {
                LayerOrder = self.menu.MenuOrder,
                UILayer = true,
                DisableToUnload = true
            }
            if rank < data.Rank then
                t.Parent = self.comps.cvs_k1.Transform
                t.Pos = {x = 288, y = -82}
                Util.PlayEffect('/res/effect/ui/ef_ui_interface_strengthen_02.assetbundles', t)
            else
                t.Parent = self.comps.ib_level.Transform
                t.Pos = {x = 118, y = -12}
                t.Scale= {x=1.1}
                Util.PlayEffect('/res/effect/ui/ef_ui_interface_strengthen_01.assetbundles', t)
            end
            self.refineMap[self.selectEquipPos] = data
            self.selectData = data
            ShowPartEquipInfo(self)
            CalcRedTips(self)
            SoundManager.Instance:PlaySoundByKey('gongnengshengji', false)
        end
    )
end

function _M.OnEnter(self, ...)
    ItemModel.RequestGridRefineList(
        function(data)
            FillList(self, data)
            CalcRedTips(self)
        end
    )
    self.sub_fn = function()
        CalcRedTips(self)
    end
    EventManager.Subscribe('Event.RedTips.strengthen', self.sub_fn)
end

function _M.OnExit(self)
    EventManager.Unsubscribe('Event.RedTips.strengthen', self.sub_fn)
end

function _M.OnDestory(self)
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

    for k, v in pairs(self.parts) do
        self.comps[v].Enable = true
        self.comps[v].TouchClick = function(node)
            --print("on touchclick", k, v)
            OnPartSelect(self, node, k)
        end
    end
    self.comps.cvs_costitem.Visible = false
    self.comps.cvs_lvnum.Visible = false
    self.comps.btn_up.TouchClick = function(sender)
        FillRankAttribute(self)
    end
    self.comps.btn_close.TouchClick = function(sender)
        self.comps.cvs_elevate.Visible = false
    end
    self.comps.cvs_elevate.TouchClick = function(sender)
        self.comps.cvs_elevate.Visible = false
    end
    self.comps.btn_use.TouchClick = function(sender)
        OnRefineButtonClick(self)
    end
    self.comps.btn_tip.TouchClick = function(sender)
        self.comps.cvs_tips.Visible = true
    end
    self.comps.btn_close22.TouchClick = function(sender)
        self.comps.cvs_tips.Visible = false
    end
    self.comps.cvs_tips.TouchClick = function(sender)
        self.comps.cvs_tips.Visible = false
    end

    self.comps.btn_effect.TouchClick = function()
        GlobalHooks.UI.OpenUI('SmithStrengthenEffect')
    end
    self.comps.cvs_info1.Visible = false
end

return _M

-- TODO 文本多语言处理
