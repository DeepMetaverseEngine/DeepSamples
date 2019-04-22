local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local Helper = require 'Logic/Helper.lua'
local CDExt = require 'Logic/CDExt'
local TimeUtil = require 'Logic/TimeUtil'
local TeamModel = require 'Model/TeamModel'

local function AgreeRequest(self)
    self.comps.bt_yes.IsGray = true
    self.comps.bt_yes.Enable = false
    self.comps.bt_no.IsGray = true
    self.comps.bt_no.Enable = false
    self.agree = true
    DataMgr.Instance.MsgData:RequestMessageResult(
        self.msg.Id,
        1,
        function()
        end
    )
end

local function OnFillPlayers(self, msg)
    self.msg = msg
    local infos = CSharpArray2Table(self.msg.SyncPlayers)
    local rolesID = {}
    for _, v in ipairs(infos) do
        table.insert(rolesID, v.id)
    end
    self.comps.lb_title.Text = Util.Format1234(self.titleSrc, self.msg.Content)
    self.comps.bt_yes.IsGray = false
    self.comps.bt_yes.Enable = true
    self.comps.bt_no.IsGray = false
    self.comps.bt_no.Enable = true
    self.nodes = {}
    local function SetPlayerInfo(node, snap, agree)
        if not snap then
            node.Visible = false
            return true
        end
        node.Visible = true
        local lb_name = UIUtil.FindChild(node, 'lb_name')
        local lb_lv = UIUtil.FindChild(node, 'lb_lv')
        local ib_icon = UIUtil.FindChild(node, 'ib_icon', true)
        local ib_yes = UIUtil.FindChild(node, 'ib_yes')
        local ib_no = UIUtil.FindChild(node, 'ib_no')
        lb_name.Text = snap.Name
        -- lb_lv.Text = Util.GetText('common_level2', snap.Level)
        lb_lv.Text = snap.Level
        UIUtil.SetImage(ib_icon, GameUtil.GetProIcon(snap.Pro))
        self.nodes[snap.ID] = {ib_yes = ib_yes, ib_no = ib_no}
        if agree == AlertHandlerType.Agree then
            ib_yes.Visible = true
            ib_no.Visible = false
            if snap.ID == DataMgr.Instance.UserData.RoleID then
                self.comps.bt_yes.IsGray = true
                self.comps.bt_yes.Enable = false
                self.comps.bt_no.IsGray = true
                self.comps.bt_no.Enable = false
            end
            return true
        elseif agree == AlertHandlerType.Cancel then
            ib_yes.Visible = false
            ib_no.Visible = true
            return true
        else
            ib_yes.Visible = false
            ib_no.Visible = false
            return false
        end
    end

    Util.GetManyRoleSnap(
        rolesID,
        function(snaps)
            -- print('self.msg.id',self.msg.Id)
            local infos = CSharpArray2Table(self.msg.SyncPlayers)
            local allok = true
            for i = 1, 4 do
                local ok = SetPlayerInfo(self.comps['cvs_playerinfo' .. i], snaps[i], infos[i] and infos[i].agree)
                if not ok then
                    allok = false
                end
            end
            self.comps.cvs_playerlist.Visible = true
            if allok then
            -- self:Close()
            end
        end
    )
end

function _M.OnEnter(self, msg)
    self.agree = false
    self.matchTargetID = nil
    self.hasTeam = false
    -- if not msg then
    --     return 
    -- end
    DataMgr.Instance.MsgData:AttachLuaObserver('PlayerEnterMap', self)
    OnFillPlayers(self, msg)

    local function cdFun(cd)
        EventApi.Task.BlockActorAutoRun(10)
        while cd > 0 do
            self.comps.lb_time.Text = TimeUtil.formatCD('%Ss', cd)
            EventApi.Task.Sleep(1)
            cd = cd - 1
            if cd <= 3 and not self.agree then
                AgreeRequest(self)
            end
            if cd <= 0 then
                break
            end
        end
        self:Close()
    end

    self.eventid = EventApi.Task.StartEvent(cdFun, 10)
    local target =
        GlobalHooks.DB.FindFirst(
        'team_target',
        function(ele)
            return Util.GetText(ele.target_name) == self.msg.Content
        end
    )
    if target then
        -- pprint('target',target)
        if DataMgr.Instance.TeamData.HasTeam then
            if DataMgr.Instance.TeamData:IsLeader() then
                self.matchTargetID = target.id
                -- DataMgr.Instance.TeamData.Setting.TargetID
                self.hasTeam = true
            end
        else
            self.matchTargetID = target.id
        end
    end
end

function _M.OnExit(self)
    if EventApi then
        EventApi.Task.StopEvent(self.eventid)
    end
    self.eventid = nil
    self.comps.cvs_playerlist.Visible = false
    DataMgr.Instance.MsgData:DetachLuaObserver('PlayerEnterMap')
    if self.agree and self.matchTargetID then
        if self.hasTeam and not DataMgr.Instance.TeamData:IsLeader() then
            return
        end
        local players = CSharpArray2Table(self.msg.SyncPlayers)
        local memberRefuse
        local allagree = true
        for _, v in pairs(players) do
            if v.agree ~= AlertHandlerType.Agree then
                allagree = false
                if self.hasTeam and DataMgr.Instance.TeamData:IsTeamMember(v.id) then
                    memberRefuse = true
                end
            end
        end
        if not memberRefuse and not allagree then
            print('RequestAutoMatch', self.matchTargetID)
            TeamModel.RequestAutoMatch(self.matchTargetID)
        end
    end
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.titleSrc = self.comps.lb_title.Text
    self.comps.bt_no.TouchClick = function()
        DataMgr.Instance.MsgData:RequestMessageResult(
            self.msg.Id,
            0,
            function()
            end
        )
        self:Close()
    end
    self.comps.bt_yes.TouchClick = function()
        AgreeRequest(self)
    end
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
end

function _M.Notify(status, flag, self, opt)
    if status == MsgData.NotiFyStatus.AgreeSync then
        print('entermap notify', flag, opt.agree)
        local cur = self.nodes[opt.id]
        if not cur then
            return
        end
        cur.ib_yes.Visible = opt.agree == AlertHandlerType.Agree
        cur.ib_no.Visible = not cur.ib_yes.Visible
        local allsync = true
        for _, v in pairs(self.nodes) do
            if not v.ib_yes.Visible and not v.ib_no.Visible then
                allsync = false
            end
        end
        if allsync then
            UnityHelper.WaitForEndOfFrame(
                function()
                    if self.eventid then
                        self:Close()
                    end
                end
            )
        end
    end
end

return _M
