local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TeamModel = require 'Model/TeamModel'
local Helper = require 'Logic/Helper.lua'
local function OnSelectTarget(self, target, iselement)
    if iselement then
        self.target = target
        local lv = math.max(DataMgr.Instance.TeamData.Setting.MinLevel, self.target.lv_min)
        self.comps.ti_importlv.Input.Text = lv
    end
end

local function GetSetting(self)
    local automatch = self.comps.tbt_automatch.IsChecked
    local autostart = self.comps.tbt_autostart.IsChecked
    local lv = tonumber(self.comps.ti_importlv.Input.Text) or 1
    local fightpower = tonumber(self.comps.ti_importfight.Input.Text) or 0
    local ret = {
        TargetID = self.target.id,
        AutoStartTarget = autostart,
        AutoMatch = automatch,
        MinLevel = lv,
        MinFightPower = fightpower
    }
    return ret
end

local function OnSetTeamOption(self)
    local current = GetSetting(self)
    print_r('select ', current)
    TeamModel.RequestSetTeam(
        current,
        function()
            self:Close()
        end,
        function()
            FillSettting(self)
        end
    )
end

local function CheckValid(target)
    local lv = DataMgr.Instance.UserData.Level
    return (target.lv_min <= lv or target.lv_min == 0) and (target.lv_max >= lv or target.lv_max == 0)
end

local function FillSettting(self)
    local setting = DataMgr.Instance.TeamData.Setting
    self.target = GlobalHooks.DB.Find('team_target', setting.TargetID)
    self.comps.ti_importlv.Input.Text = setting.MinLevel
    self.comps.ti_importfight.Input.Text = setting.MinFightPower
    self.comps.tbt_automatch.IsChecked = setting.AutoMatch
    self.comps.tbt_autostart.IsChecked = setting.AutoStartTarget
    if self.tree_menu then
        self.tree_menu:Close()
    end
    local tree = UIUtil.CreateTreeMenu()
    local team_target = GlobalHooks.DB.GetFullTable('team_target')
    local map_tree = {}
    local treeEnableNode
    local lv = DataMgr.Instance.UserData.Level
    for _, v in ipairs(team_target) do
        if v.tab_index == 0 and CheckValid(v) then
            map_tree[v.tab_type] =
                tree:AddChild(
                Util.GetText(v.target_name),
                function(iselement)
                    OnSelectTarget(self, v, iselement)
                end
            )
            if v.id == self.target.id then
                treeEnableNode = map_tree[v.tab_type]
            end
        end
    end
    for i, v in pairs(team_target) do
        if v.tab_index ~= 0 and map_tree[v.tab_type] and CheckValid(v) then
            local sub = map_tree[v.tab_type]
            local next =
                sub:AddChild(
                Util.GetText(v.target_name),
                function(iselement)
                    OnSelectTarget(self, v, iselement)
                end
            )
            if v.id == self.target.id then
                treeEnableNode = next
            end
        end
    end

    if treeEnableNode then
        treeEnableNode:SetEnable(true)
    end
    tree:Show(self.comps.tbt_an1, self.comps.tbt_an2, 0, 10, 0, 5)

    UnityHelper.WaitForEndOfFrame(
        function()
            local v = Vector2(treeEnableNode.button.Y, treeEnableNode.button.Y)
            self.comps.sp_target.Scrollable:LookAt(v)
        end
    )
    self.tree_menu = tree
end

function _M.OnEnter(self)
    self.comps.btn_ok.Visible = DataMgr.Instance.TeamData:IsLeader()
    FillSettting(self)
end

function _M.OnExit(self)
    self.tree_menu:Close()
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.menu.ShowType = UIShowType.Cover
    self.comps.btn_ok.TouchClick = function(sender)
        OnSetTeamOption(self)
    end
    self.comps.tbt_automatch.TouchClick = function(sender)
        if not DataMgr.Instance.TeamData:IsLeader() then
            GameAlertManager.Instance:ShowNotify(Util.GetText('team_no_authority'))
            sender.IsChecked = DataMgr.Instance.TeamData.Setting.AutoMatch
        elseif self.target.id == 1 then
            GameAlertManager.Instance:ShowNotify(Constants.Text.team_warnAutoApply)
            sender.IsChecked = false
        end
    end
    self.comps.tbt_autostart.TouchClick = function(sender)
        if not DataMgr.Instance.TeamData:IsLeader() then
            GameAlertManager.Instance:ShowNotify(Util.GetText('team_no_authority'))
            sender.IsChecked = DataMgr.Instance.TeamData.Setting.AutoStartTarget
        end
    end
    self.comps.ti_importlv.Input.event_EndEdit = function()
        local lv = tonumber(self.comps.ti_importlv.Input.Text) or 1
        local lv = math.max(lv, self.target.lv_min)
        self.comps.ti_importlv.Input.Text = lv
    end
end

return _M
