local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TeamModel = require 'Model/TeamModel'
local Helper = require 'Logic/Helper.lua'

local function RefreshButton(self)
    local con1 = not DataMgr.Instance.TeamData.HasTeam and self.target ~= nil
    local con2 = DataMgr.Instance.TeamData.HasTeam and DataMgr.Instance.TeamData:IsLeader()
    self.comps.btn_auto.Visible = con1 or con2
    self.comps.btn_leave.Visible = DataMgr.Instance.TeamData.HasTeam
    self.comps.btn_refresh.Visible = self.target ~= nil
    self.comps.btn_create.Visible = not DataMgr.Instance.TeamData.HasTeam
end

local function FillTeamList(self, teamlist)
    -- print_r('teamlist ', teamlist)
    local function UpdateElement(node, index)
        local teamSnap = teamlist[index]
        local leader = Util.GetCachedRoleSnap(teamSnap.LeaderID)
        local lb_leadname = UIUtil.FindChild(node, 'lb_leadname')
        local ib_target = UIUtil.FindChild(node, 'ib_target', true)
        local lb_leadlv = UIUtil.FindChild(node, 'lb_leadlv', true)
        local lb_leadpractice = UIUtil.FindChild(node, 'lb_leadpractice')
        local lb_needlv = UIUtil.FindChild(node, 'lb_needlv')
        local lb_needfight = UIUtil.FindChild(node, 'lb_needfight')
        local btn_apply = UIUtil.FindChild(node, 'btn_apply')

        if leader.Options then
            local photoname = leader.Options['Photo0']
            if not string.IsNullOrEmpty(photoname) then
                local SocielModel = require 'Model/SocialModel'
                SocielModel.SetHeadIcon(
                    teamSnap.LeaderID,
                    photoname,
                    function(UnitImg)
                        if not ib_target.IsDispose then
                            UIUtil.SetImage(ib_target,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                        end
                    end
                )
            else
                local path = GameUtil.GetHeadIcon(leader.Pro, leader.Gender)
                UIUtil.SetImage(ib_target, path)
            end
        else
            local path = GameUtil.GetHeadIcon(leader.Pro, leader.Gender)
            UIUtil.SetImage(ib_target, path)
        end

        lb_leadname.Text = leader.Name
        lb_leadlv.Text = leader.Level
        local lb_practice = node:FindChildByEditName('lb_practice', true)

        if leader.PracticeLv > 0 then
            lb_leadpractice.Visible = true
            GameUtil.SetPracticeName(lb_leadpractice, leader.PracticeLv, 0)
        else
            lb_leadpractice.Visible = false
        end

        local needLv = teamSnap.Setting and teamSnap.Setting.MinLevel or 0
        local fightPower = teamSnap.Setting and teamSnap.Setting.MinFightPower or 0
        lb_needlv.Visible = needLv > 0
        lb_needfight.Visible = fightPower > 0
        lb_needlv.Text = Util.Format1234(Constants.Text.team_lv, needLv)
        lb_needfight.Text = Util.Format1234(Constants.Text.team_fightpower, fightPower)

        for i = 1, 4 do
            local ib_member = UIUtil.FindChild(node, 'ib_member' .. i)
            local ib_memberlv = UIUtil.FindChild(node, 'lb_memberlv' .. i)

            local m = teamSnap.Members[i]
            ib_member.Visible = m ~= nil
            ib_memberlv.Visible = m ~= nil
            if m then
                UIUtil.SetImage(ib_member, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|pro_' .. m.Pro)
                ib_memberlv.Text = m.Level
            -- UIUtil.SetImage(ib_member, GameUtil.GetProIcon(m.Pro))
            end
        end
        btn_apply.Visible = not DataMgr.Instance.TeamData.HasTeam
        btn_apply.TouchClick = function(sender)
            TeamModel.RequestInviteTeam(teamSnap.LeaderID)
        end
    end

    UIUtil.ConfigVScrollPan(self.comps.sp_teamlist, self.comps.cvs_teaminfo, #teamlist, UpdateElement)
end

local function OnSelectTarget(self, target, point)
    self.target = target
    self.point = point or 0
    RefreshButton(self)
    -- print_r('select target', target)
    TeamModel.RequestTargetTeamList(
        self.target.id,
        self.point,
        function(rsp)
            self.point = rsp.s2c_point
            -- print_r('rsp.s2c_teamList ', rsp.s2c_teamList)
            rsp.s2c_teamList = rsp.s2c_teamList or {}

            TeamModel.SnapReader:LoadMany(
                rsp.s2c_teamList,
                function(teamlist)
                    local leaders = {}
                    -- print_r('teamlist ', teamlist)
                    local dataExistList = {}
                    for i, v in ipairs(teamlist) do
                        if v then
                            table.insert(leaders, v.LeaderID)
                            table.insert(dataExistList, v)
                        end
                    end
                    if #leaders > 0 then
                        Util.GetManyRoleSnap(
                            leaders,
                            function()
                                FillTeamList(self, dataExistList)
                            end
                        )
                    else
                        FillTeamList(self, dataExistList)
                    end
                end
            )
        end
    )
end

local function OnAutoButtonClick(self, sender)
    if not self.target then
        --todo 提示：尚未选中目标
        return
    end
    TeamModel.RequestAutoMatch(self.target.id)
end

local function RefreshRedPoint(self)
    if DataMgr.Instance.TeamData.HasTeam then
        self.comps.ib_dian.Visible = DataMgr.Instance.MsgData:GetMessageCount(AlertMessageType.TeamApply) > 0
    else
        self.comps.ib_dian.Visible = DataMgr.Instance.MsgData:GetMessageCount(AlertMessageType.TeamInvite) > 0
    end
end

local function RefreshTargetText(self)
    if DataMgr.Instance.TeamData.HasTeam then
        local targetID = DataMgr.Instance.TeamData.Setting.TargetID
        local target = GlobalHooks.DB.Find('team_target', targetID)
        self.comps.btn_taget.Visible = true
        self.comps.btn_taget.Text = Constants.Text.team_target_desc .. Util.GetText(target.target_name)
    elseif DataMgr.Instance.TeamData.IsInMatch then
        self.comps.btn_taget.Visible = true
        self.comps.btn_taget.Text = Constants.Text.team_target_desc .. DataMgr.Instance.TeamData.MatchingName
    else
        self.comps.btn_taget.Visible = false
    end
end

local function CheckValid(target)
    --do
    --    return true
    --end
    local lv = DataMgr.Instance.UserData.Level
    return (target.lv_min <= lv or target.lv_min == 0) and (target.lv_max >= lv or target.lv_max == 0)
end

function _M.OnEnter(self)
    self.target = nil
    FillTeamList(self, {})
    DataMgr.Instance.TeamData:AttachLuaObserver('TeamPlatform', self)
    DataMgr.Instance.MsgData:AttachLuaObserver('TeamPlatform', self)
    local tree = UIUtil.CreateTreeMenu()
    local team_target = GlobalHooks.DB.GetFullTable('team_target')
    local map_tree = {}

    local has_element = false
    for _, v in ipairs(team_target) do
        if v.tab_index == 0 and v.tab_type ~= 0 and CheckValid(v) then
            has_element = true
            map_tree[v.tab_type] = tree:AddChild(Util.GetText(v.target_name))
        end
    end
    for i, v in pairs(team_target) do
        if v.tab_index ~= 0 and map_tree[v.tab_type] and CheckValid(v) then
            has_element = true
            local sub = map_tree[v.tab_type]
            sub:AddChild(
                Util.GetText(v.target_name),
                function()
                    OnSelectTarget(self, v)
                end
            )
        end
    end

    self.comps.cvs_none.Visible = not has_element

    tree:Show(self.comps.tbt_an1, self.comps.tbt_an2, 0, 10, 0, 5)
    self.tree_menu = tree

    local targetID = DataMgr.Instance.TeamData.Setting.TargetID
    local target = GlobalHooks.DB.Find('team_target', targetID)
    self.comps.btn_taget.Text = Constants.Text.team_target_desc .. Util.GetText(target.target_name)
    RefreshButton(self)
    RefreshTargetText(self)
end

function _M.OnExit(self)
    DataMgr.Instance.TeamData:DetachLuaObserver('TeamPlatform')
    DataMgr.Instance.MsgData:DetachLuaObserver('TeamPlatform')
    self.tree_menu:Close()
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.comps.btn_leave.TouchClick = function()
        DataMgr.Instance.TeamData:RequestLeaveTeam(
            function(success)
            end
        )
    end
    self.comps.btn_refresh.TouchClick = function()
        OnSelectTarget(self, self.target, self.point)
    end
    self.comps.cvs_teaminfo.Visible = false

    self.comps.btn_auto.TouchClick = function(sender)
        OnAutoButtonClick(self, sender)
    end

    self.comps.btn_taget.TouchClick = function()
        if DataMgr.Instance.TeamData.HasTeam and DataMgr.Instance.TeamData:IsLeader() then
            GlobalHooks.UI.OpenUI('TeamTarget')
        end
    end

    self.comps.btn_create.TouchClick = function()
        TeamModel.RequestCreateTeam()
    end
    self.comps.btn_hanhua.TouchClick = function()
        if not DataMgr.Instance.TeamData.HasTeam then
            GameAlertManager.Instance:ShowNotify(Constants.Text.team_warnNoTeam)
        elseif not DataMgr.Instance.TeamData:IsLeader() then
            GameAlertManager.Instance:ShowNotify(Constants.Text.team_warnNotLeader)
        else
            local targetID = DataMgr.Instance.TeamData.Setting.TargetID
            local target = GlobalHooks.DB.Find('team_target', targetID)
            local ChatUtil = require 'UI/Chat/ChatUtil'

            local showData = Util.GetText('team_yelling', Util.GetText(target.target_name), target.lv_min, target.lv_max)
            ChatUtil.TeamShout(showData)
        end
    end

    self.comps.btn_apply.TouchClick = function()
        if not DataMgr.Instance.TeamData.HasTeam or DataMgr.Instance.TeamData:IsLeader() then
            GlobalHooks.UI.OpenUI('TeamApply', 0)
        else
            GameAlertManager.Instance:ShowNotify(Util.GetText('team_no_authority'))
        end
    end
end

function _M.Notify(status, flag, self, opt)
    if flag == DataMgr.Instance.MsgData then
        RefreshRedPoint(self)
    else
        RefreshButton(self)
        RefreshTargetText(self)
    end
end

return _M
