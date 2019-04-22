local _M = {}
_M.__index = _M
local CDExt = require 'Logic/CDExt'
local TimeUtil = require 'Logic/TimeUtil'
local Util = require 'Logic/Util'
local function OnEnterMapClick(self)
    DataMgr.Instance.MsgData:RequestMessageResult(
        self.msg.Id,
        1,
        function()
        end
    )
end

function _M.OnEnter(self, msg)
    self.msg = msg
    local name = Util.GetText(msg.Content)
    self.comps.lb_success.Text = Util.GetText('common_match_success', name)
    local function cdFun(cd, label)
        if cd <= 1 then
            self:Close()
            MenuMgr.Instance:CloseAllMenu()
        end
        self.comps.lb_cd.Text = TimeUtil.FormatToCN(cd)
    end
    self.timer = CDExt.New(10, cdFun)
end

function _M.OnExit(self)
    self.timer:Stop()
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.comps.btn_go.TouchClick = function()
        MenuMgr.Instance:CloseAllMenu()
        OnEnterMapClick(self)
    end
end

return _M
