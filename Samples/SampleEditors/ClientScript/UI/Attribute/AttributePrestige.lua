local _M = {}
_M.__index = _M

function _M.OnEnter( self )
	print('AttributePrestige OnEnter')
	GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, "此功能暂未开放", '', nil, nil)
end

function _M.OnExit( self )
	print('AttributePrestige OnExit')
end

function _M.OnDestory( self )
	print('AttributePrestige OnDestory')
end

function _M.OnInit( self )
	print('AttributePrestige OnInit')
    self.ui:EnableTouchFrame(false)
    self.ui.comps.cvs_package.Enable = false
    self.ui.comps.cav_element.Enable = false
end

return _M