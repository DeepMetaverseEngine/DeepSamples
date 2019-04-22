package.loaded['Logic/Util'] = nil
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'

local _M = {}
_M.__index = _M

local function SetCount(self, count)
    self.cur = count
    self.cur = math.max(self.cur, 1)
    self.cur = math.min(self.cur, self.max)
    self.comps.btn_num.Text = self.cur

    self.is_enough = true

    local ret = self.cache_costNode:GetVisibleNodes()
    for i, v in ipairs(self.costs) do
        local node = ret[i]
        local lb_itemname = UIUtil.FindChild(node, 'lb_itemname')
        lb_itemname.Text = Util.GetText(v.detail.static.name)

        local currentinfo = {need = v.need * self.cur, cur = v.cur, detail = v.detail, id = v.id, group = v.group}
        if self.is_enough and currentinfo.cur < currentinfo.need then
            self.is_enough = false
        end
        local lb_costnum = UIUtil.FindChild(node, 'lb_costnum')
        UIUtil.SetEnoughItemShowAndLabel(
            self,
            node,
            lb_costnum,
            currentinfo,
            {
                x = 495,
                y = 310,
                anchor = 'l_b',
                cb = function(enough)
                    self.comps.btn_use.Enable = enough
                    self.comps.btn_use.IsGray = not enough
                end
            }
        )
    end

    self.comps.lb_itemnum.Text = self.select_data.target_num * count
    if not self.comps.lb_itemnum.Visible then
        self.target_itshow.Num = self.select_data.target_num * count
    end
    self.comps.btn_use.Enable = self.is_enough
    self.comps.btn_use.IsGray = not self.is_enough
end

local function ReCalcRedPointer(self)
    local red_marks = {}
    for k, v in pairs(self.all_datas) do
        local costs = ItemModel.ParseCostAndCostGroup(v)
        local count = 99999
        for _, vv in ipairs(costs) do
            count = math.min(count, math.floor(vv.cur / vv.need))
        end
        local element = self.treeMenu:FindChildByUserTag(k)
        local detail = ItemModel.GetDetailByTemplateID(v.target_id)
        local typeNode = self.treeMenu:FindChildByUserTag(v.synthesis_type)
        local img = UIUtil.FindChildByType(typeNode.button, 'HZImageBox', false)
        -- 显示红点
        if count > 0 then
            element:SetText(Util.GetText(detail.static.name) .. '(' .. count .. ')')
            --local s = {item_type = detail.static.item_type, sec_type = detail.static.sec_type}
            --local compose_ctl = GlobalHooks.DB.FindFirst('item/item_synthetic.xlsx/item_synthetic_redpoint', s)
            if not img then
                img = HZImageBox.CreateImageBox()
                img.Name = 'red_pointer'
                typeNode.button:AddChild(img)
                UIUtil.SetImage(img, Constants.InternalImg.red_pointer, true)
                img.Position2D = Vector2(5, 5)
            end
            red_marks[v.synthesis_type] = true
            img.Visible = true
        else
            if img and not red_marks[v.synthesis_type] then
                img.Visible = false
            end
            element:SetText(Util.GetText(detail.static.name))
        end
    end
end

local function FillCost(self, data)
    local cost = ItemModel.ParseCostAndCostGroup(data)
    self.cache_costNode = self.cache_costNode or UIUtil.CreateCacheNodeGroup(self.comps.cvs_costitem, true)
    local halfY = (self.comps.cvs_cost.Height - self.comps.cvs_costitem.Height) * 0.5
    self.cache_costNode:SetInitCB(
        function(node, preNode)
            -- node.Y = preNode.Y + halfY
        end
    )
    self.cache_costNode:Reset(#cost)
    self.costs = cost
    local ret = self.cache_costNode:GetVisibleNodes()
    local offsetx = 34
    local start = self.comps.cvs_cost.Width - (self.comps.cvs_costitem.Width * #cost) - (#cost - 1) * offsetx
    local start = start * 0.5
    UIUtil.SetNodesToCenterStyle(self.comps.cvs_cost.Width, self.comps.cvs_costitem.Width, true, ret, false, start)

    SetCount(self, self.cur)
end

local function OnSelect(self, data, detail)
    -- print('onselect -----------------------')
    self.select_data = data
    self.select_detail = detail
    self.comps.ib_name.Text = Util.GetText(detail.static.name)
    self.cur = 1
    self.target_itshow = UIUtil.SetItemShowTo(self.comps.cvs_daoju, detail.static.atlas_id, detail.static.quality, self.cur * data.target_num)
    self.target_itshow.EnableTouch = true
    self.target_itshow.TouchClick = function()
        UIUtil.ShowNormalItemDetail({detail = detail, itemShow = self.target_itshow, autoHeight = true, x = 495, y = 276, anchor = 'l_t'})
    end
    FillCost(self, data)
end

local function Do(self)
    if not self.is_enough then
        GameAlertManager.Instance:ShowNotify(Util.GetText('item_notenoughcount'))
    else
        -- print('info ', self.select_data.target_id, self.cur)
        ItemModel.RequestComposeItem(
            self.select_data.target_id,
            self.cur,
            function()
                OnSelect(self, self.select_data, self.select_detail)
                ReCalcRedPointer(self)
            end
        )
    end
end

function _M.OnEnter(self, targetID)
    ReCalcRedPointer(self)
    if not targetID then
        if not self.select_data then
            local element = self.treeMenu:FindChildByUserTag(self.first_targetID)
            element:SetEnableAndInvoke(true)
        else
            OnSelect(self, self.select_data, self.select_detail)
        end
    else
        local element = self.treeMenu:FindChildByUserTag(targetID)
        element:SetEnableAndInvoke(true)
    end
end

function _M.OnExit(self)
end

function _M.OnDestory(self)
end
function _M.OnInit(self)
    self.max = GlobalHooks.DB.GetGlobalConfig('synthetic_max')
    self.treeMenu = UIUtil.CreateTreeMenu()
    local all = GlobalHooks.DB.Find('item_synthetic', {})
    table.sort(
        all,
        function(x, y)
            if x.synthesis_type == y.synthesis_type then
                return x.order < y.order
            else
                return x.synthesis_type < y.synthesis_type
            end
        end
    )

    local compose_ctl = GlobalHooks.DB.Find('item/item_synthetic.xlsx/item_synthetic_redpoint', {})
    local filter_map = {}
    for _, v in ipairs(compose_ctl) do
        filter_map[v.item_type] = filter_map[v.item_type] or {}
        filter_map[v.item_type][v.sec_type] = v.is_show
    end

    local type_parents = {}
    local enableSub
    self.all_datas = {}
    for _, v in ipairs(all) do
        local detail = ItemModel.GetDetailByTemplateID(v.target_id)
        local is_show = filter_map[detail.static.item_type][detail.static.sec_type]
        if not is_show or is_show == 1 then
            self.first_targetID = self.first_targetID or v.target_id
            local sub = type_parents[v.synthesis_type]
            if not sub then
                sub = self.treeMenu:AddChild(Util.GetText(v.type_name))
                type_parents[v.synthesis_type] = sub
                sub:SetUserTag(v.synthesis_type)
            end
            local element =
                sub:AddChild(
                Util.GetText(detail.static.name),
                function()
                    OnSelect(self, v, detail)
                end
            )
            self.all_datas[v.target_id] = v
            element:SetUserTag(v.target_id)
            element:SetTextColorRGB(Constants.QualityColor[detail.static.quality])
            if not enableSub then
                enableSub = element
            end
        end
    end

    self.first_element = enableSub
    self.treeMenu:Show(self.comps.tbt_sub, self.comps.tbt_sub1, nil, 8, 0, 6)

    self.comps.btn_less.TouchClick = function()
        SetCount(self, self.cur - 1)
    end

    self.comps.btn_more.TouchClick = function()
        SetCount(self, self.cur + 1)
    end

    self.comps.btn_num.TouchClick = function()
        local posParam = {pos = {x = 505, y = 210}, anchor = {x = 0, y = 0}}
        GlobalHooks.UI.OpenUI(
            'NumInput',
            0,
            1,
            999,
            posParam,
            function(value)
                SetCount(self, value)
            end
        )
    end

    self.comps.btn_use.TouchClick = function()
        Do(self)
    end
end

return _M
