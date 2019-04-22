local _M = {}
_M.__index = _M

local Util = require("Logic/Util")
local Helper = require 'Logic/Helper.lua'

--5个一组，写好注释
local events = {
    "EventAddFriend",
    "EventRemoveFriend",
    "EventViewInfo",
    "EventSendMessage",
    "EventInviteTeam",

    "EventInviteGuild",
    "EventInvitePK",
    "EventAddBlacklist",
    "EventRemoveBlacklist",
    "EventTeamKickOut",

    "EventTeamChangeLeader",
    "EventTeamLeave",
    "EventGuildTranser",   -- 转让公会
    "EventGuildImpeach",   -- 弹劾会长
    "EventGuildPosition",  -- 职位变更

    "EventGuildLeave",     -- 离开公会
    "EventGuildKickOut",   -- 请离公会
    "EventAskFollow",   -- 召唤队友
    "EventFollowLeader",   -- 跟随队长
    "EventMakeAction",   -- 做动作

    "EventAddRelative",   -- 任命亲信
    "EventChangeRelative",   -- 变更亲信
    "EventRename",   -- 修改头衔
    "EventCancelRelative",   -- 撤销任命
    "EventSocialGift",   -- 赠送礼物
}


local function OnBtnClick(self, btn)
    local idx = btn.UserTag
    local func = _M["OnClick" .. idx]
    if func then
        func(self, events[idx], events[idx], self.cb)
        self.ui.menu:Close()
    else
        print("incorrect menu button id " .. idx)
    end
end

local function ShowBtns(self, arr, colCount, showCfg)
    for i, v in ipairs(arr) do
        local btn = self.btns[i]
        if not btn then
            btn = self.btns[1]:Clone()
            self.ui.comps.cvs_menu:AddChild(btn)
            table.insert(self.btns, btn)
        end
        btn.Visible = true
        local col = (i - 1) % colCount
        local row = math.floor((i - 1) / colCount)
        btn.X = self.borderX + self.gapX + (self.gapX + self.btnW) * col
        btn.Y = self.borderY + self.gapY + (self.gapY + self.btnH) * row
        btn.TouchClick = function( sender )
            OnBtnClick(self, sender)
        end
        btn.UserTag = v.id
        --设置显示文本，不填就默认用第一套，读不到也默认用第一套代替
        showCfg = showCfg or 1
        btn.Text = Util.ContainsTextKey(v.interbehavior[showCfg]) and Util.GetText(v.interbehavior[showCfg]) or Util.GetText(v.interbehavior[1])
    end

    for i = #arr + 1, #self.btns do
        self.btns[i].Visible = false
    end
    local cw = (self.gapX + self.btnW) * colCount + self.gapX
    local ch = (self.gapY + self.btnH) * math.ceil(#arr / colCount) + self.gapY
    self.ui.comps.cvs_menu.Width = cw + self.borderX * 2
    self.ui.comps.cvs_menu.Height = ch + self.borderY * 2
    
    -- pos offset, anchor
    self.param.pos = self.param.pos or {}
    self.param.anchor = self.param.anchor or {}
    
    if self.param.pos.x ~= nil then
        self.ui.comps.cvs_menu.X = self.param.pos.x

        -- anchor x
        if self.param.anchor.x ~= nil then
            self.ui.comps.cvs_menu.X = self.ui.comps.cvs_menu.X - self.ui.comps.cvs_menu.Width * self.param.anchor.x
        end
    else
        self.ui.comps.cvs_menu.X = (self.ui.menu.Transform.rect.width - self.ui.comps.cvs_menu.Width) * 0.5
    end

    if self.param.pos.y ~= nil then
        self.ui.comps.cvs_menu.Y = self.param.pos.y

        -- anchor y
        if self.param.anchor.y ~= nil then
            self.ui.comps.cvs_menu.Y = self.ui.comps.cvs_menu.Y - self.ui.comps.cvs_menu.Height * self.param.anchor.y
        end
    else
        self.ui.comps.cvs_menu.Y = (self.ui.menu.Transform.rect.height - self.ui.comps.cvs_menu.Height) * 0.5
    end

    self.ui.comps.cvs_lb.X = 0
    self.ui.comps.cvs_lb.Y = self.ui.comps.cvs_menu.Height - self.ui.comps.cvs_lb.Height
    self.ui.comps.cvs_rb.X = self.ui.comps.cvs_menu.Width - self.ui.comps.cvs_rb.Width
    self.ui.comps.cvs_rb.Y = self.ui.comps.cvs_menu.Height - self.ui.comps.cvs_rb.Height
end


-- 添加好友
function _M.OnClick1(self, evtKey, cbKey, cb)
    local Social = require("Model/SocialModel")
    local playerId = self.playerId
    local params = self.param.params
    Social.RequestClientApplyFriend(playerId, function()
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 删除好友
function _M.OnClick2(self, evtKey, cbKey, cb)
    local Social = require("Model/SocialModel")
    local playerId = self.playerId
    local params = self.param.params
    Social.RequestClientRemoveFriend(playerId, function()
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 添加黑名单
function _M.OnClick8(self, evtKey, cbKey, cb)
    local Social = require("Model/SocialModel")
    local playerId = self.playerId
    local params = self.param.params
    Social.RequestClientAddBlackList(playerId, function()
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 移除黑名单
function _M.OnClick9(self, evtKey, cbKey, cb)
    local Social = require("Model/SocialModel")
    local playerId = self.playerId
    local params = self.param.params
    Social.RequestClientRemoveBlackList(playerId, function()
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 查看信息
function _M.OnClick3(self, evtKey, cbKey, cb)
    if self.playerId then
        GlobalHooks.UI.OpenUI('LookPlayerInfo',-1,self.playerId)
    end
end
-- 发送信息
function _M.OnClick4(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local playerName = self.playerName
    local ChatModel = require("Model/ChatModel")

    if GlobalHooks.UI.FindUI('ChatMainSmall') == nil and GlobalHooks.UI.FindUI('ChatMain') == nil then
        ChatModel.AddChatRole(playerId,playerName)
        GlobalHooks.UI.OpenUI('ChatMainSmall', 0, ChatModel.ChannelState.CHANNEL_PRIVATE,playerId) 
    else
        EventManager.Fire('Event.Chat.SendPrivateMsg',{roleId = playerId, roleName =playerName,byself = true})
    end
end
-- 邀请队伍
function _M.OnClick5(self, evtKey, cbKey, cb)
    local TeamModel = require 'Model/TeamModel'
    local playerId = self.playerId
    local params = self.param.params
    TeamModel.RequestInviteTeam(playerId, function( rsp )
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 邀请公会
function _M.OnClick6(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local params = self.param.params
    local GuildModel = require 'Model/GuildModel'
    GuildModel.ClientInviteToGuildRequest(playerId, function( rsp )
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 邀请切磋
function _M.OnClick7(self, evtKey, cbKey, cb)
end
-- 踢出队伍
function _M.OnClick10(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local params = self.param.params
    local TeamModel = require 'Model/TeamModel'
    TeamModel.RequestKickOutTeam(playerId, function( rsp )
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end
-- 移交队长
function _M.OnClick11(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local params = self.param.params
    local TeamModel = require 'Model/TeamModel'
    TeamModel.RequestChangeLeader(playerId, function( rsp )
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end

-- 离开队伍
function _M.OnClick12(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local params = self.param.params
    local TeamModel = require 'Model/TeamModel'
    TeamModel.RequestLeaveTeam(function( rsp )
        if cb then cb(cbKey, playerId, params) end
        EventManager.Fire(evtKey, {playerId = playerId})
    end)
end


-- 转让会长
function _M.OnClick13(self, evtKey, cbKey, cb)
    if cb then
        cb(cbKey, self.playerId, Helper.copy_table(self.param.params))
    end
end

-- 弹劾会长
function _M.OnClick14(self, evtKey, cbKey, cb)
    if cb then
        cb(cbKey, self.playerId, Helper.copy_table(self.param.params))
    end
end

-- 更换职位
function _M.OnClick15(self, evtKey, cbKey, cb)
    if cb then
        cb(cbKey, self.playerId, Helper.copy_table(self.param.params))
    end
end

-- 离开公会
function _M.OnClick16(self, evtKey, cbKey, cb)
    if cb then
        cb(cbKey, self.playerId, Helper.copy_table(self.param.params))
    end
end

-- 请离公会
function _M.OnClick17(self, evtKey, cbKey, cb)
    if cb then
        cb(cbKey, self.playerId, Helper.copy_table(self.param.params))
    end
end

--召唤队友
function _M.OnClick18(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local params = self.param.params
    local TeamModel = require 'Model/TeamModel'
    TeamModel.RequestAskFollow(playerId)
    if cb then
        cb(cbKey, self.playerId, params)
    end
end

--做动作
function _M.OnClick20(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local playerName = self.playerName
    EventManager.Fire('Event.Chat.MakeAction',{roleId = playerId, playerName = playerName})
end

--任命亲信
function _M.OnClick21(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local PlayRuleModel = require 'Model/PlayRuleModel'
    PlayRuleModel.RequestAddRelative(playerId,1)
end

--变更亲信
function _M.OnClick22(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local PlayRuleModel = require 'Model/PlayRuleModel'
    PlayRuleModel.RequestAddRelative(playerId,0,function()
        PlayRuleModel.RequestAddRelative(playerId,1)
    end)
end

--修改头衔
function _M.OnClick23(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local PlayRuleModel = require 'Model/PlayRuleModel'
    PlayRuleModel.RequestRename(playerId)
end

--撤销任命
function _M.OnClick24(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    local PlayRuleModel = require 'Model/PlayRuleModel'
    PlayRuleModel.RequestAddRelative(playerId,0)
end

--赠送礼物
function _M.OnClick25(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    GlobalHooks.UI.OpenUI('SocialMain', 0, 'SocialFriend', 4, playerId)
end

--查看相册
function _M.OnClick26(self, evtKey, cbKey, cb)
    local playerId = self.playerId
    GlobalHooks.UI.OpenUI('SocialWatch',0, playerId)
end


function _M.SetData(self, param)
    self.playerId = param.playerId
    self.playerName = param.playerName
    self.cb = param.cb
    self.param = param
    local data = GlobalHooks.DB.Find("menu_config", param.menuKey)
    local arr = string.split(data.menu_enumerate, ',', true, tonumber)
    if param.removeMenuItems then
        for i,v in ipairs(param.removeMenuItems) do
            table.removeItem(arr, v)
        end
    end
    for i,v in ipairs(arr) do
        arr[i] = GlobalHooks.DB.Find("menu_button", v)
    end
    ShowBtns(self, arr, data.menu_type, param.showCfg)
    if param.uipos then
        param.uipos = Vector2(param.uipos.x,param.uipos.y-self.ui.comps.cvs_menu.Height/2)
        self.ui.comps.cvs_menu.Position2D = param.uipos
        param.uipos = nil
    end
end


function _M.OnEnter( self )
    self.ui:EnableTouchFrameClose(true)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
    self.originW = self.ui.comps.cvs_menu.Width
    self.originH = self.ui.comps.cvs_menu.Height
    self.btns = { self.ui.comps.btn_an }
    self.borderX = 7--self.ui.comps.ib_bjtu.X
    self.borderY = 7--self.ui.comps.ib_bjtu.Y
    self.gapX = self.btns[1].X - self.borderX
    self.gapY = self.btns[1].Y - self.borderY
    self.btnW = self.btns[1].Width
    self.btnH = self.btns[1].Height
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end


return _M
