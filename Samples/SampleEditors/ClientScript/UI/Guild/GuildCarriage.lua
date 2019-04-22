local _M = {}
_M.__index = _M
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local GuildModel = require 'Model/GuildModel'

local function OnDraw(self, state_info)
    self.state_info = state_info
    local is_jointime = FunctionUtil.CheckNowIsOpen('guildcarriage_open')
    local is_entertime = FunctionUtil.CheckNowIsOpen('guildcarriage_in')
    -- print_r('statetinfo',is_jointime,is_entertime,state_info)
    self.comps.btn_signup.Visible = false
    self.comps.btn_signup.IsGray = false
    self.comps.btn_signup.Enable = true

    -- print_r('state_info',state_info)
    if not state_info then
        self.comps.lb_enemy2.Text = Constants.Text.guild_carriage_nomatch
        self.comps.lb_state2.Text = Constants.Text.guild_carriage_nojoin
        self.comps.cvs_cost.Visible = false
        return
    end

    self.comps.cvs_cost.Visible = true
    local sdata = GlobalHooks.DB.FindFirst('guild_carriage', {guild_lv = state_info.s2c_guildLv})
    UIUtil.SetEnoughLabel(self, self.comps.lb_costnum, state_info.s2c_currentFund, sdata.enter_cost, true)
    if state_info.s2c_joined then
        self.comps.lb_enemy2.Text = state_info.s2c_enemyGuildName or Constants.Text.guild_carriage_matching
        self.comps.lb_state2.Text = Constants.Text.guild_carriage_join
    else
        self.comps.lb_enemy2.Text = Constants.Text.guild_carriage_nomatch
        self.comps.lb_state2.Text = Constants.Text.guild_carriage_nojoin
    end

    self.comps.cvs_cost.Visible = is_jointime

    if is_jointime and (not is_entertime or not state_info.s2c_joined) then
        self.comps.btn_signup.Visible = true
        self.button_join = not state_info.s2c_joined
        if not state_info.s2c_joined then
            --报名参加
            self.comps.btn_signup.Text = Constants.Text.guild_carriage_btnjoin
        else
            self.comps.btn_signup.Text = Constants.Text.guild_carriage_btntrans
            self.comps.btn_signup.IsGray = true
            self.comps.btn_signup.Enable = false
        end
    elseif is_entertime then
        self.button_join = false
        self.comps.btn_signup.Text = Constants.Text.guild_carriage_btntrans
        if not state_info.s2c_joined then
            self.comps.btn_signup.Visible = false
        elseif state_info.s2c_enemyGuildName then
            self.comps.btn_signup.Visible = true
        else
            self.comps.btn_signup.Visible = true
            self.comps.btn_signup.IsGray = true
            self.comps.btn_signup.Enable = false
        end
    end
end

local function RequestTransport(self)
    local Api = EventApi
    Api.Task.Wait(Api.Protocol.Task.Request('EnterGuildCarriageZone'))
end

local function OnButtonClick(self)
    if self.button_join then
        GuildModel.ClientGuildJoinCarriageRequest(
            function()
                --匹配中
                self.state_info.s2c_joined = true
                OnDraw(self, self.state_info)
            end
        )
    else
        EventApi.Task.StartEvent(RequestTransport, self)
    end
end

function _M.OnEnter(self)
    -- 预览奖励
    local guildlv = DataMgr.Instance.GuildData.Level
    local data = GlobalHooks.DB.FindFirst('guild/guild_carriage.xlsx/guild_carriage_reward', {guild_lv = guildlv})
    self.comps.sp_itemlist.Visible = data ~= nil
    if data then
        local items = {}
        for _, v in pairs(data.reward.id) do
            if v ~= 0 then
                table.insert(items, v)
            end
        end
        UIUtil.ConfigHScrollPanWithOffset(
            self.comps.sp_itemlist,
            self.comps.cvs_itemicon1,
            #items,
            6,
            function(node, index)
                local cvs_item1 = UIUtil.FindChild(node,'cvs_item1')
                local id = items[index]
                local itshow = UIUtil.SetItemShowTo(cvs_item1, id)
                itshow.EnableTouch = true
                itshow.TouchClick = function()
                    UIUtil.ShowNormalItemDetail({templateID = id,autoHeight = true, x = 280,itemShow = itshow})
                end
                
            end
        )
    end
    OnDraw(self, self.state_info)
end

function _M.OnLoad(self, callBack, params)
    -- print('----------OnLoad', params)
    GuildModel.ClientGuildCarriageStateRequest(
        function(rsp)
            self.state_info = rsp
            callBack:Invoke(true)
        end,
        function()
            callBack:Invoke(true)
            if string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) then
                self:Close()
            end
        end
    )
end

function _M.OnExit(self)
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.comps.btn_signup.TouchClick = function()
        OnButtonClick(self)
    end
    self.comps.cvs_itemicon1.Visible = false
end

return _M
