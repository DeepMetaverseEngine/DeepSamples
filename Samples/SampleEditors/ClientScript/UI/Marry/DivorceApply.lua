local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'



function _M.OnEnter( self )
	local cost = GlobalHooks.DB.GetGlobalConfig('divorce_force_cost_num')
	self.ui.comps.lb_costnum.Text = tostring(cost)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
	self.ui.comps.btn_choose1.TouchClick = function( ... )
		SocialModel.RequestClientDivorce(0)
	end
	self.ui.comps.btn_choose2.TouchClick = function( ... )
		local content = Util.GetText('compulsory_divorce_content')
		GameAlertManager.Instance.AlertDialog:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, '', '', nil, 
		    function(parma)
				SocialModel.RequestClientDivorce(1)
		    end, nil)
	end
end

return _M