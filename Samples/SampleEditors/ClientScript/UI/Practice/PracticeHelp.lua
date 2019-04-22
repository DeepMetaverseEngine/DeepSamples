local _M = {}
_M.__index = _M

local PracticeModel = require 'Model/PracticeModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

function _M.OnEnter( self )

end

function _M.OnExit( self )
	
end

function _M.OnDestory( self )
	
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
end

return _M