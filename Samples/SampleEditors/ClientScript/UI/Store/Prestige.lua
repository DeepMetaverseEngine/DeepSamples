local _M = {}
_M.__index = _M

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)

    
end

function _M.OnExit( self )
    print('Prestige OnExit')
end

function _M.OnDestory( self )
    print('Prestige OnDestory')
end

function _M.OnInit( self )
    print('Prestige OnInit')
	self.ui:EnableTouchFrame(false)
    self.ui:EnableChildren(false)
end

return _M