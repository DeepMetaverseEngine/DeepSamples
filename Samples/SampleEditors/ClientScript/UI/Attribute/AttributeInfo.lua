local _M = {}
_M.__index = _M

function _M.OnEnter( self )
	print('AttributeInfo OnEnter')
end

function _M.OnExit( self )
	print('AttributeInfo OnExit')
end

function _M.OnDestory( self )
	print('AttributeInfo OnDestory')
end

function _M.OnInit( self )
	print('AttributeInfo OnInit')
    self.ui:EnableTouchFrame(false)
    self.ui.comps.cvs_package.Enable = false
    self.ui.comps.cav_element.Enable = false
end

return _M