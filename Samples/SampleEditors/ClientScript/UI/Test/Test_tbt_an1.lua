local _M = {}
_M.__index = _M

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
    print('OnEnter '..self.ui.tag, ...)
end

function _M.OnExit( self )
    print('OnExit'..self.ui.tag)
end

function _M.OnDestory( self )
    print('OnDestory '..self.ui.tag)
end

function _M.OnInit( self )
    self.ui:EnableTouchFrame(false)
    self.ui:EnableChildren(false)
    print('OnInit '..self.ui.tag)
end

return _M