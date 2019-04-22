local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TeamModel = require 'Model/TeamModel'
local Helper = require 'Logic/Helper.lua'

local function FillSettting(self)
   
end

function _M.OnEnter(self)
    self.comps.lb_text.Text = Util.GetText(self.formattext,DataMgr.Instance.TeamData.MatchingName)
    self.comps.lb_time1.Text = DataMgr.Instance.TeamData.CurrentMatchCountdown or ''
    self.comps.lb_time2.Text = '05:00'
    self.timer = LuaTimer.Add(0,500,function()
        if DataMgr.Instance.TeamData.IsInMatch then
            self.comps.lb_time1.Text = DataMgr.Instance.TeamData.CurrentMatchCountdown
        else
            self:Close()
        end
    end)
end

function _M.OnExit(self)
    if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.ui.menu.ShowType = UIShowType.Cover
    self.menu.ShowType = UIShowType.Cover
    self.formattext  =self.comps.lb_text.Text
    self.comps.bt_goon.TouchClick = function()
        self:Close()
    end
    self.comps.bt_out.TouchClick = function()
        self:Close()
        TeamModel.ReuestLeaveAutoMatch()
    end
end

return _M
