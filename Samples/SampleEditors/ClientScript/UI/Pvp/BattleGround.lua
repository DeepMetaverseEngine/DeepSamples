local _M = {}
_M.__index = _M
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TeamModel = require 'Model/TeamModel'
local Helper = require 'Logic/Helper.lua'
local ItemModel = require 'Model/ItemModel'
local ActivityModel = require 'Model/ActivityModel'
local Api = EventApi

local function RefreshButtonState(self, target)
    local match_name = DataMgr.Instance.TeamData.MatchingName
    if not target then
        for k, v in pairs(self.team_targets) do
            local btn_start = self.enter_btns[k]
            if match_name == Util.GetText(v.target_name) then
                UIUtil.EnableButtonWithGray(btn_start, false, Constants.Text.guild_carriage_matching)
            else
                UIUtil.EnableButtonWithGray(btn_start, true, Constants.Text.enter_now)
            end
        end
    else
        local btn_start = self.enter_btns[target.function_id]
        if match_name == Util.GetText(target.target_name) then
            UIUtil.EnableButtonWithGray(btn_start, false, Constants.Text.guild_carriage_matching)
        else
            UIUtil.EnableButtonWithGray(btn_start, true, Constants.Text.enter_now)
        end
    end
end

local function OnPvp10Click(self)
    if self.pvp10_target then
        TeamModel.RequestAutoMatch(self.pvp10_target.id)
    else
        GameAlertManager.Instance:ShowNotify(Constants.Text.pvp_joinlimit)
    end
end
local function OnPvp4Click(self)
    if self.pvp4_target then
        TeamModel.RequestAutoMatch(self.pvp4_target.id)
    else
        GameAlertManager.Instance:ShowNotify(Constants.Text.pvp_joinlimit)
    end
end

local function OnExploitUpdate(self)
    local totalExploit = ItemModel.CountItemByTemplateID(Constants.VirtualItems.Exploit)
    self.comps.lb_exnum.Text = totalExploit
end

local function RequestReward(self, itemShow, ib_gotted, function_id, count)
    local req = {function_id = function_id, count = count}
    local ok = Api.Task.Wait(Api.Protocol.Task.Request('GetPvpReward', req))
    RenderSystem.Instance:Unload(itemShow.UserTag)
    if ok then
        self.reward_info[function_id][count] = 2
        RenderSystem.Instance:Unload(itemShow.UserTag)
        itemShow.IsGray = false
        ib_gotted.Visible = true
    end
end

--1 可领取 2 已领取 0 次数不足
local function FillReward(self)
    local function Sort(x, y)
        return x.partake_num < y.partake_num
    end
    local reward_pvp4 = GlobalHooks.DB.Find('pvp_reward', {function_id = 'pvp4'})
    local reward_pvp10 = GlobalHooks.DB.Find('pvp_reward', {function_id = 'pvp10'})
    table.sort(reward_pvp4, Sort)
    table.sort(reward_pvp10, Sort)

    local pvp4_max_picknum = 0
    local pvp10_max_picknum = 0
    for _, v in ipairs(reward_pvp4) do
        pvp4_max_picknum = math.max(pvp4_max_picknum, v.partake_num)
    end
    for _, v in ipairs(reward_pvp10) do
        pvp10_max_picknum = math.max(pvp10_max_picknum, v.partake_num)
    end

    self.comps.gg_count1:SetGaugeMinMax(0, pvp4_max_picknum)
    self.comps.gg_count2:SetGaugeMinMax(0, pvp10_max_picknum)
    -- self.CountInfo['pvp4'] = 5
    -- self.CountInfo['pvp10'] = 3
    self.comps.gg_count1.Value = math.min(self.CountInfo['pvp4'], pvp4_max_picknum)
    self.comps.gg_count2.Value = math.min(self.CountInfo['pvp10'], pvp10_max_picknum)
    for i = 1, 3 do
        local cvs_pvp4 = self.comps['cvs_item1' .. i]
        local cvs_pvp10 = self.comps['cvs_item2' .. i]
        local ib_count_pvp4 = self.comps['lb_count1' .. i]
        local ib_got_pvp4 = self.comps['ib_got1' .. i]
        local ib_count_pvp10 = self.comps['lb_count2' .. i]
        local ib_got_pvp10 = self.comps['ib_got2' .. i]
        cvs_pvp4.Visible = reward_pvp4[i] ~= nil
        cvs_pvp10.Visible = reward_pvp10[i] ~= nil

        local reward4_state = self.reward_info['pvp4'][reward_pvp4[i].partake_num]
        local reward10_state = self.reward_info['pvp10'][reward_pvp10[i].partake_num]
        if reward_pvp4[i] then
            ib_count_pvp4.Text = Util.GetText(Constants.Text.pvp_rewardcount, reward_pvp4[i].partake_num)
            local itemShow = UIUtil.SetItemShowTo(cvs_pvp4, reward_pvp4[i].item.id[1], reward_pvp4[i].item.num[1])
            itemShow.EnableTouch = true
            ib_got_pvp4.Visible = reward4_state == 2
            itemShow.IsGray = reward4_state == 0
            itemShow:SetParentIndex(0)

            if reward4_state == 1 then
                local t = {
                    LayerOrder = self.menu.MenuOrder,
                    UILayer = true,
                    DisableToUnload = true,
                    Parent = itemShow.Transform,
                    Scale = 0.9,
                    Pos = {x = 30, y = -30, z = -600}
                }
                itemShow.UserTag = Util.PlayEffect('/res/effect/ui/ef_ui_icon_frame_01.assetbundles', t)
            end
            itemShow.TouchClick = function()
                local reward4_state = self.reward_info['pvp4'][reward_pvp4[i].partake_num]
                if reward4_state == 1 then
                    Api.Task.StartEvent(RequestReward, self, itemShow, ib_got_pvp4, 'pvp4', reward_pvp4[i].partake_num)
                else
                    local pos = UIUtil.ToLocalPos(itemShow, self.root)
                    UIUtil.ShowNormalItemDetail(
                        {
                            x = pos.x,
                            y = pos.y,
                            anchor = 'l_b',
                            itemShow = itemShow,
                            autoHeight = true,
                            detail = ItemModel.GetDetailByTemplateID(reward_pvp4[i].item.id[1])
                        }
                    )
                end
            end
        end
        if reward_pvp10[i] then
            ib_count_pvp10.Text = Util.GetText(Constants.Text.pvp_rewardcount, reward_pvp10[i].partake_num)
            local itemShow = UIUtil.SetItemShowTo(cvs_pvp10, reward_pvp10[i].item.id[1], reward_pvp10[i].item.num[1])
            itemShow.EnableTouch = true
            itemShow:SetParentIndex(0)
            ib_got_pvp10.Visible = reward10_state == 2
            itemShow.IsGray = reward10_state == 0
            if reward10_state == 1 then
                local t = {
                    LayerOrder = self.menu.MenuOrder,
                    UILayer = true,
                    DisableToUnload = true,
                    Parent = itemShow.Transform,
                    Scale = 0.9,
                    Pos = {x = 30, y = -30, z = -600}
                }
                itemShow.UserTag = Util.PlayEffect('/res/effect/ui/ef_ui_icon_frame_01.assetbundles', t)
            end
            itemShow.TouchClick = function()
                local reward10_state = self.reward_info['pvp10'][reward_pvp10[i].partake_num]
                if reward10_state == 1 then
                    Api.Task.StartEvent(RequestReward, self, itemShow, ib_got_pvp10, 'pvp10', reward_pvp10[i].partake_num)
                else
                    local pos = self.menu:LocalToUIGlobal(itemShow)
                    UIUtil.ShowNormalItemDetail(
                        {
                            x = pos.x + itemShow.Width,
                            y = pos.y,
                            anchor = 'r_b',
                            itemShow = itemShow,
                            autoHeight = true,
                            detail = ItemModel.GetDetailByTemplateID(reward_pvp10[i].item.id[1])
                        }
                    )
                end
            end
        end
    end
end
local function ShowHelpDesc(self, function_id)
    self.comps.cvs_helpback.Visible = true
    local data = GlobalHooks.DB.FindFirst('pvp/pvp.xlsx/pvp_desc', {type = function_id})
    UIUtil.SetImage(self.comps.ib_helppic, data.battlefield_description)
    self.comps.lb_helptext.Text = Util.GetText(data.battlefield_name)
    self.comps.sp_list.Visible = false
end

local function CloseHelpDesc(self)
    self.comps.cvs_helpback.Visible = false
    self.comps.sp_list.Visible = true
end
local function FillElement(self, node, data)
    local lb_title = UIUtil.FindChild(node, 'lb_title')
    local lb_name = UIUtil.FindChild(node, 'lb_name')
    local lb_opentime = UIUtil.FindChild(node, 'lb_opentime')
    local lb_win = UIUtil.FindChild(node, 'lb_win', true)
    local lb_draw = UIUtil.FindChild(node, 'lb_draw', true)
    local lb_lose = UIUtil.FindChild(node, 'lb_lose', true)
    local gg_count = UIUtil.FindChild(node, 'gg_count', true)
    local btn_start = UIUtil.FindChild(node, 'btn_start')
    local btn_help = UIUtil.FindChild(node, 'btn_help')
    lb_opentime.Text = Util.GetText(data.open_time)
    lb_title.Text = Util.GetText(data.battlefield_name)
    local function_id = data.type
    local target = self.team_targets[function_id]
    local reward_data = GlobalHooks.DB.FindFirst('pvp/pvp.xlsx/pvp', {map_id = target.mapid})
    self.enter_btns = self.enter_btns or {}
    self.enter_btns[function_id] = btn_start
    btn_start.TouchClick = function(sender)
        TeamModel.RequestAutoMatch(target.id)
    end
    btn_help.TouchClick = function(sender)
        ShowHelpDesc(self, function_id)
    end
    RefreshButtonState(self, target)
    -- 奖励
    lb_name.Text = Util.GetText(target.target_name)
    if reward_data.pvp_type == 1 then
        lb_win.Text = Util.GetText('pvp_award_text1', reward_data.victory_value)
        lb_draw.Text = Util.GetText('pvp_award_text2', reward_data.draw_value)
        lb_lose.Text = Util.GetText('pvp_award_text3', reward_data.fail_value)
    else
        lb_win.Text = Util.GetText('pvp_award_text4', reward_data.ranking_award[1])
        lb_draw.Text = Util.GetText('pvp_award_text5', reward_data.ranking_award[2])
        lb_lose.Text = Util.GetText('pvp_award_text6', reward_data.ranking_award[3])
    end
    local function Sort(x, y)
        return x.partake_num < y.partake_num
    end
    -- 参与奖励
    local reward_join = GlobalHooks.DB.Find('pvp_reward', {function_id = function_id})
    table.sort(reward_join, Sort)
    local max_picknum = 0
    for _, v in ipairs(reward_join) do
        max_picknum = math.max(max_picknum, v.partake_num)
    end
    gg_count:SetGaugeMinMax(0, max_picknum)
    -- self.CountInfo['pvp4'] = 5
    -- self.CountInfo['pvp10'] = 3
    local gg_v = math.min(self.CountInfo[function_id], max_picknum)
    gg_count.Value = gg_v
    for i = 1, 3 do
        local cvs_item = UIUtil.FindChild(node, 'cvs_item' .. i, true)
        if reward_join[i] then
            local partake_num = reward_join[i].partake_num
            cvs_item.Visible = true
            local ib_count = UIUtil.FindChild(cvs_item, 'lb_count' .. i)
            local ib_got = UIUtil.FindChild(cvs_item, 'ib_got' .. i)
            ib_count.Text = Util.GetText(Constants.Text.pvp_rewardcount, partake_num)
            local itemShow = UIUtil.SetItemShowTo(cvs_item, reward_join[i].item.id[1], reward_join[i].item.num[1])
            itemShow.EnableTouch = true
            local reward_state = self.reward_info[function_id][partake_num]
            ib_got.Visible = reward_state == 2
            itemShow.IsGray = reward_state == 0
            itemShow:SetParentIndex(0)
            if reward_state == 1 then
                local t = {
                    LayerOrder = self.menu.MenuOrder,
                    UILayer = true,
                    DisableToUnload = true,
                    Parent = itemShow.Transform,
                    Scale = 0.9,
                    Pos = {x = 30, y = -30, z = -600},
                    Clip = self.comps.sp_list.Transform
                }
                itemShow.UserTag = Util.PlayEffect('/res/effect/ui/ef_ui_icon_frame_01.assetbundles', t)
            end
            itemShow.TouchClick = function()
                local reward_state = self.reward_info[function_id][partake_num]
                if reward_state == 1 then
                    Api.Task.StartEvent(RequestReward, self, itemShow, ib_got, function_id, partake_num)
                else
                    local pos = UIUtil.ToLocalPos(itemShow, self.root)
                    UIUtil.ShowNormalItemDetail(
                        {
                            x = pos.x,
                            y = pos.y,
                            anchor = 'l_b',
                            itemShow = itemShow,
                            autoHeight = true,
                            detail = ItemModel.GetDetailByTemplateID(reward_join[i].item.id[1])
                        }
                    )
                end
            end
        else
            cvs_item.Visible = false
        end
    end
end

local function FillBattleList(self)
    local all = GlobalHooks.DB.Find('pvp/pvp.xlsx/pvp_desc', {})
    local today_list = {}
    local team_targets = {}
    for _, v in ipairs(all) do
        if FunctionUtil.IsInToday(v.type, true) then
            local target = TeamModel.FindTargetByFunctionID(v.type)
            if target then
                table.insert(today_list, v)
                team_targets[v.type] = target
            end
        end
    end
    self.today_list = today_list
    self.team_targets = team_targets
    UIUtil.ConfigHScrollPan(
        self.comps.sp_list,
        self.comps.cvs_detail,
        -- 2,
        #today_list,
        -- 100,
        -- 10,
        function(node, index)
            FillElement(self, node, today_list[index])
        end
    )
end

local function RequestPvpInfo(self)
    local ok, info = Api.Task.Wait(Api.Protocol.Task.Request('PvpInfo'))
    if not ok then
        return
    end
    local limit_today = Api.GetExcelConfig('pvp_value_limit')
    self.comps.lb_exday.Text = info.TodayExploit .. '/' .. limit_today
    self.reward_info = info.reward
    self.CountInfo = info.Count
    FillBattleList(self)
    -- FillReward(self)
end

function _M.Notify(status, flag, self, opt)
    RefreshButtonState(self)
end

function _M.OnEnter(self)
    Api = EventApi
    DataMgr.Instance.TeamData:AttachLuaObserver('BattleGround' .. tostring(self), self)
    OnExploitUpdate(self)
    self.listener =
        ItemModel.ListenByTemplateID(
        Constants.VirtualItems.Exploit,
        function()
            OnExploitUpdate(self)
        end
    )
    Api.Task.StartEvent(RequestPvpInfo, self)
end

function _M.OnExit(self)
    self.listener:Dispose()
    DataMgr.Instance.TeamData:DetachLuaObserver('BattleGround' .. tostring(self))
end

function _M.OnDestory(self)
end

local function OpenPvpShop(self)
    local shopId = GlobalHooks.DB.GetGlobalConfig('pvp_storeid')
    shopId = shopId or 4
    local ui = GlobalHooks.UI.CreateUI('Shop', 0, shopId)
    self:AddSubUI(ui)
end

function _M.OnInit(self)
    self.comps.cvs_detail.Visible = false
    self.comps.btn_shop.TouchClick = function()
        OpenPvpShop(self)
    end
    self.comps.btn_close2.TouchClick = function()
        CloseHelpDesc(self)
    end
end

return _M
