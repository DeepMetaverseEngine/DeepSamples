local _M = {}
_M.__index = _M
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
function _M.OnEnter(self, msg)
    
end

function _M.OnExit(self)
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.comps.btn_ok.TouchClick = function()
        self:Close()
    end
    self.comps.tb_explain.Scrollable = true
end

return _M
