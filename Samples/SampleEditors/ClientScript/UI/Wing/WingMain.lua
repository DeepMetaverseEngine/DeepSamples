local _M = {}
_M.__index = _M
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local WingModel = require 'Model/WingModel'
local Util = require 'Logic/Util'
local Helper = require 'Logic/Helper.lua'
local avatarMock = {
    [1] = {avatar_res = 'mount_fox', class = 1, pos_xy = '0,0', zoom = '50'},
    [2] = {avatar_res = 'mount_phoenix', class = 2, pos_xy = '0,0', zoom = '50'},
    [3] = {avatar_res = 'mount_fox', class = 3, pos_xy = '0,0', zoom = '50'},
    [4] = {avatar_res = 'mount_phoenix', class = 4, pos_xy = '0,0', zoom = '50'},
    [5] = {avatar_res = 'mount_sword', class = 5, pos_xy = '0,0', zoom = '50'},
    [6] = {avatar_res = 'mount_rabbit', class = 6, pos_xy = '0,0', zoom = '50'},
    [7] = {avatar_res = 'mount_goldfish', class = 7, pos_xy = '0,0', zoom = '50'},
    [8] = {avatar_res = 'mount_phoenix', class = 8, pos_xy = '0,0', zoom = '50'}
}

local function FillAttributes(self, attrs)
    -- print_r('fill attr',attrs)
    local index = 0
    for i = 1, 11 do
        local lb_shuxing = self.comps['lb_shuxing' .. i]
        local lb_shuxingNum = self.comps['lb_shuxing' .. i .. 'num']
        local ib_up = self.comps['ib_up' .. i]
        local lb_shuxingadd = self.comps['lb_shuxingadd' .. i]
        local attrName, value, numvalue = ItemModel.GetAttributeString(attrs[i])
        lb_shuxing.Text = attrName or ''
        local last_v = tonumber(lb_shuxingNum.Text)
        if numvalue ~= 0 and last_v > 0 and numvalue > last_v then
            ib_up.Visible = true
            lb_shuxingadd.Visible = true
            lb_shuxingadd.Text = numvalue - last_v
            UIUtil.PlayCPJOnce(
                ib_up,
                1,
                function()
                    ib_up.Visible = false
                    lb_shuxingadd.Visible = false
                end
            )
        end
        lb_shuxingNum.Text = value or ''
        lb_shuxing.Visible = true
        lb_shuxingNum.Visible = true
    end
    local score = ItemModel.CalcAttributesScore(attrs)
    if tostring(score) ~= self.comps.lb_fight.Text then
        local t = {
            LayerOrder = self.menu.MenuOrder,
            UILayer = true,
            DisableToUnload = true,
            Parent = self.comps.lb_fight1.Transform,
            Pos = {x = 101, y = -18, z = -600}
        }
        Util.PlayEffect('/res/effect/ui/ef_ui_interface_promote.assetbundles', t)
    end
    self.comps.lb_fight.Text = score
end

local function ReleaseModel(self)
    if self.model then
        RenderSystem.Instance:Unload(self.model)
        self.model = nil
    end
end

local function CheckMaxLv(self)
    local next = GlobalHooks.DB.FindFirst('wings_prop', self.wingLv + 1)
    return next == nil
end

local function OnEquipAvatarClick(self)
    local isChecked = self.comps.tbt_use.IsChecked
    local page = self.selectPage
    WingModel.RequestEquipWingAvatar(
        page,
        isChecked,
        function()
            if isChecked then
                self.currentEquiped = page
            else
                self.currentEquiped = 0
            end
        end,
        function()
            self.comps.tbt_use.IsChecked = not isChecked
        end
    )
end

local function ExpAnim(self, targetNum, targetNum2)
    if self.timer then
        LuaTimer.Delete(self.timer)
        self.comps.gg_exp.Value = self.next_ggvalue
    end
    -- print('expanim', targetNum, targetNum2)
    local interval = 35
    local LifeMS = 1000
    local speed = (self.currentStatic.total_exp / LifeMS) * interval
    self.next_ggvalue = targetNum
    self.timer =
        LuaTimer.Add(
        0,
        interval,
        function()
            local next = self.comps.gg_exp.Value + speed
            next = math.min(self.currentStatic.total_exp, next)
            next = math.min(self.comps.gg_exp.GaugeMaxValue, next)
            if next == self.currentStatic.total_exp and targetNum2 then
                next = 0
                targetNum = targetNum2
                targetNum2 = nil
                self.next_ggvalue = targetNum
            end
            self.comps.gg_exp.Value = next
            local ret = next < targetNum
            return ret
        end
    )
end

local function OnExpChange(self, anim)
    -- print('onexp change' ,anim)
    if self.currentStatic.total_exp == 0 then
        self.comps.gg_exp:SetGaugeMinMax(0, 1)
        self.comps.gg_exp.Value = 1
    else
        self.comps.gg_exp:SetGaugeMinMax(0, self.currentStatic.total_exp)
        if anim then
            ExpAnim(self, self.currentExp)
        else
            if self.timer then
                LuaTimer.Delete(self.timer)
                self.timer = nil
            end
            self.comps.gg_exp.Value = self.currentExp
        end
    end
end

local function FillCostItem(self, costNode, v)
    local lb_num = UIUtil.FindChild(costNode, 'lb_num')
    UIUtil.SetEnoughItemShowAndLabel(self, costNode, lb_num, v)
    costNode.Visible = true
    return v.cur >= v.need
end

local function FillCost(self)
    local costs = ItemModel.ParseCostAndCostGroup(self.currentStatic)
    return FillCostItem(self, self.comps.cvs_costitem1, costs[1])
end

local function OnDrawMainPage(self, index, data)
    local lb_name = UIUtil.FindChild(self.comps.cvs_template, 'lb_name')
    local lb_quality = UIUtil.FindChild(self.comps.cvs_template, 'lb_quality')
    lb_name.Text = Util.GetText(data.wing_name)
    lb_quality.Text = Util.GetText('wing_class_' .. data.wings_class)
    self.comps.tb_explain.Text = Util.GetText(data.wing_desc)
    self.comps.lb_max.Visible = false
    if index == self.currentStatic.wings_class then
        for i = 1, 10 do
            self.comps['cvs_' .. i].Enable = not (self.currentStatic.wings_star < i)
        end
        self.comps.tbt_use.Visible = true
        self.comps.cvs_explain.Visible = false
        if CheckMaxLv(self) then
            -- 最高等级
            self.comps.cvs_attribute.Visible = false
            self.comps.cvs_cost.Visible = false
            self.comps.lb_max.Visible = true
            self.comps.lb_max.Text = Constants.Text.wing_maxlv
        elseif self.currentStatic.is_stopauto == 1 then
            --显示进阶
            self.comps.cvs_attribute.Visible = false
            self.comps.cvs_cost.Visible = true
            self.comps.lb_max.Visible = true
            self.comps.lb_max.Text = Constants.Text.wing_advanced_text
        else
            self.comps.cvs_attribute.Visible = true
            self.comps.cvs_cost.Visible = true
        end
    else
        self.comps.cvs_attribute.Visible = false
        self.comps.cvs_cost.Visible = false
        self.comps.cvs_explain.Visible = true
        self.comps.tbt_use.Visible = index <= self.currentStatic.wings_class
        self.comps.lb_max.Visible = true
        if index > self.currentStatic.wings_class then
            self.comps.lb_max.Text = Constants.Text.wing_unlock_text
            for i = 1, 10 do
                self.comps['cvs_' .. i].Enable = false
            end
        else
            for i = 1, 10 do
                self.comps['cvs_' .. i].Enable = true
            end
            self.comps.lb_max.Text = Constants.Text.wing_developed_text
        end
    end
    local function FixResName(name)
        return '/res/unit/' .. name .. '.assetbundles'
    end
    self.comps.tbt_use.IsChecked = self.currentEquiped == index
    ReleaseModel(self)

    local resname = FixResName(data.avatar_res)
    local fixposdata = string.split(data.pos_xy, ',')
    local fixpos = {x = tonumber(fixposdata[1]), y = tonumber(fixposdata[2])}
    fixpos.x = self.comps.cvs_models.Width * 0.5 + fixpos.x
    fixpos.y = -(self.comps.cvs_models.Height * 0.5 + fixpos.y)
    local fixzoom = tonumber(data.zoom)
    local info = {
        LayerOrder = menuOrder,
        Pos = fixpos,
        Scale = fixzoom,
        Parent = self.comps.cvs_models.Transform,
        UILayer = true
    }
    self.comps.cvs_models.event_PointerMove = function(sender, data)
        local delta = -data.delta.x
        local obj = RenderSystem.Instance:GetAssetGameObject(self.model)
        if obj then
            obj.transform:Rotate(Vector3.up, delta * 1.2)
        end
    end
    self.model = Util.LoadGameUnit(resname, info)
end

local function OnSelectPage(self, index)
    print('onselectpage', self.selectPage)
    self.selectPage = index
    local data = GlobalHooks.DB.FindFirst('wings', {wings_class = index})
    local predata = GlobalHooks.DB.FindFirst('wings', {wings_class = index - 1})
    local nextdata = GlobalHooks.DB.FindFirst('wings', {wings_class = index + 1})
    self.comps.btn_goleft.Visible = predata ~= nil
    self.comps.btn_goright.Visible = nextdata ~= nil
    OnDrawMainPage(self, index, data)
end

local function OnStartLogic(self, noexp)
    local datas = GlobalHooks.DB.Find('wings', {})
    self.selectPage = self.currentStatic.wings_class

    local next = GlobalHooks.DB.FindFirst('wings_prop', self.wingLv + 1)
    if next then
        self.comps.lb_next.Visible = self.currentStatic.wings_star < 10 and next.wings_class == self.currentStatic.wings_class
        self.comps.cvs_upgrade.Visible = next.wings_class ~= self.currentStatic.wings_class
    else
        self.comps.lb_next.Visible = false
        self.comps.cvs_upgrade.Visible = false
    end

    self.comps.lb_now.Text = Util.Format1234(Constants.Text.wing_formatstar, self.currentStatic.wings_star)

    self.comps.lb_next.Text = Util.Format1234(Constants.Text.wing_formatstar, self.currentStatic.wings_star + 1)
    --attrs
    local attrs = ItemModel.GetXlsFixedAttribute(self.currentStatic, true)
    FillAttributes(self, attrs)

    local nextStatic = GlobalHooks.DB.FindFirst('wings_prop', self.wingLv + 1)
    if nextStatic then
        local nextAttrs = ItemModel.GetXlsFixedAttribute(nextStatic)
        local cmpAttr = ItemModel.DiffAttributesWith(nextAttrs, attrs)[1]
        self.comps.lb_nextattr.Visible = cmpAttr ~= nil
        if cmpAttr then
            local attrName, value = ItemModel.GetAttributeString(cmpAttr)
            self.comps.lb_nextattr.Text = attrName .. '+' .. value
        end
    end
    --exp
    if not noexp then
        OnExpChange(self)
    end

    --cost
    local enough = FillCost(self)
    if enough and self.comps.cvs_upgrade.Visible then
        local t = {
            LayerOrder = self.menu.MenuOrder,
            UILayer = true,
            DisableToUnload = true,
            Parent = self.comps.btn_upgrade.Transform,
            Pos = {x = 66, y = -25, z = -600}
        }
        Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t)
    end
    self.comps.cvs_alluse.Visible = not self.comps.cvs_upgrade.Visible
end

local function FixEffectRes(filename)
    return '/res/effect/ui/' .. filename .. '.assetbundles'
end

local function StopAutoAddExp(self)
    if self.auto_eventid then
        EventApi.Task.StopEvent(self.auto_eventid)
        self.auto_eventid = nil
    end
    if self.comps.tbt_autouse then
        self.comps.tbt_autouse.IsChecked = false
    end
end

local function RequestAddExp(self, auto)
    WingModel.RequestAddWingExp(
        auto,
        function(rp)
            local t = {
                LayerOrder = self.menu.MenuOrder,
                UILayer = true,
                DisableToUnload = true,
                Parent = self.comps.cvs_costitem1.Transform,
                Pos = {x = 36, y = -38, z = -600}
            }
            Util.PlayEffect('/res/effect/ui/ef_ui_interface_consume.assetbundles', t)
            if not string.IsNullOrEmpty(rp.s2c_effectres) then
                Util.PlayEffect(
                    rp.s2c_effectres,
                    {LayerOrder = self.menu.MenuOrder, Parent = self.comps.cvs_attribute.Transform, UILayer = true, Pos = {x = 550, y = -70, z = -600}}
                )
            end
            if self.wingLv == rp.s2c_wingLv then
                if self.currentExp ~= rp.s2c_currentExp then
                    self.currentExp = rp.s2c_currentExp
                    SoundManager.Instance:PlaySoundByKey('gongnengshengji', false)
                    OnExpChange(self, true)
                end
                FillCost(self)
                local t = {
                    LayerOrder = self.menu.MenuOrder,
                    UILayer = true,
                    DisableToUnload = true,
                    Parent = self.comps.cvs_models.Transform,
                    Pos = {x = 275, y = -379, z = -600}
                }
                Util.PlayEffect('/res/effect/ui/ef_ui_interface_upgrade.assetbundles', t)
            else
                local t = {
                    LayerOrder = self.menu.MenuOrder,
                    UILayer = true,
                    DisableToUnload = true,
                    Parent = self.comps.cvs_models.Transform,
                    Pos = {x = 275, y = -379, z = -600}
                }
                Util.PlayEffect('/res/effect/ui/ef_ui_interface_upgrade.assetbundles', t)
                self.wingLv = rp.s2c_wingLv
                self.currentExp = rp.s2c_currentExp

                local lastclass = self.currentStatic.wings_class
                self.currentStatic = GlobalHooks.DB.FindFirst('wings_prop', self.wingLv)
                OnStartLogic(self, lastclass == self.currentStatic.wings_class)
                local targetNum2 = self.currentExp / self.currentStatic.total_exp
                if lastclass == self.currentStatic.wings_class then
                    ExpAnim(self, self.currentStatic.total_exp, self.currentExp)
                    local t = {
                        LayerOrder = self.menu.MenuOrder,
                        UILayer = true,
                        DisableToUnload = true,
                        Parent = self.comps['cvs_' .. self.currentStatic.wings_star].Transform,
                        Pos = {x = 13, z = -600}
                    }
                    Util.PlayEffect('/res/effect/ui/ef_ui_interface_lightstar.assetbundles', t)
                    SoundManager.Instance:PlaySoundByKey('dianliang', false)
                else
                    local t = {
                        LayerOrder = self.menu.MenuOrder,
                        UILayer = true,
                        DisableToUnload = true,
                        Parent = self.comps.cvs_models.Transform,
                        Pos = {z = -600}
                    }
                    Util.PlayEffect('/res/effect/ui/ef_ui_interface_advanced.assetbundles', t)
                    SoundManager.Instance:PlaySoundByKey('jinjie', false)

                    local t = {
                        LayerOrder = self.menu.MenuOrder,
                        UILayer = true,
                        DisableToUnload = true,
                        Parent = self.comps.tbt_use.Transform,
                        Pos = {x = 44, y = -47, z = -600}
                    }
                    Util.PlayEffect('/res/effect/ui/ef_ui_interface_jinlun_button.assetbundles', t)

                    t.Parent = self.comps.cvs_models.Transform
                    t.Pos = {x = 280, y = -206, z = -600}
                    Util.PlayEffect('/res/effect/ui/ef_ui_interface_jinlun_unlock.assetbundles', t)
                end
                OnSelectPage(self, self.currentStatic.wings_class)
            end
        end,
        function()
            StopAutoAddExp(self)
        end
    )
end

local function StartAutoAddExp(self)
    while true do
        RequestAddExp(self, false)
        EventApi.Task.Sleep(0.5)
    end
end

function _M.OnEnter(self)
    WingModel.RequestWingInfo(
        function(rp)
            -- print_r('OnEnter ', rp)
            self.wingLv = rp.s2c_wingLv
            self.currentExp = rp.s2c_currentExp
            self.currentEquiped = rp.s2c_equipedRank
            self.currentStatic = GlobalHooks.DB.FindFirst('wings_prop', self.wingLv)
            OnStartLogic(self)
            OnSelectPage(self, self.currentStatic.wings_class)
        end
    )
end

function _M.OnExit(self)
    ReleaseModel(self)
    if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
    for _, v in ipairs(self.effects or {}) do
        RenderSystem.Instance:Unload(v)
    end
    StopAutoAddExp(self)
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
    self.comps.tbt_use.TouchClick = function(sender)
        OnEquipAvatarClick(self)
    end
    self.comps.btn_alluse.TouchClick = function(sender)
        RequestAddExp(self, false)
    end

    self.comps.btn_upgrade.TouchClick = function(sender)
        RequestAddExp(self, false)
    end

    self.comps.btn_goleft.TouchClick = function()
        OnSelectPage(self, self.selectPage - 1)
    end

    self.comps.btn_goright.TouchClick = function()
        OnSelectPage(self, self.selectPage + 1)
    end

    if self.comps.tbt_autouse then
        self.comps.tbt_autouse.TouchClick = function(sender)
            if sender.IsChecked then
                self.auto_eventid = EventApi.Task.StartEvent(StartAutoAddExp, self)
            else
                EventApi.Task.StopEvent(self.auto_eventid)
                self.auto_eventid = nil
            end
        end
    end
end

return _M
