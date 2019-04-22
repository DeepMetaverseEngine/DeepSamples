local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local _M = {}
DisplayUtil.warpOOPSelf(_M)
local subui = nil
function _M.doSwitchTaskPage(self,pageIdx)
	if not self.ui.menu.IsRunning then return end

	--print("pageIdx-----------------",pageIdx)
	local index,data = self.radioExt:selectedIdx()
	--print("index-----------------",index,data)
	if subui~=nil then
		subui.ui:Close() 
		subui = nil
	end
	if data == "xinxi" then 
		subui = GlobalHooks.UI.CreateUI('PetInformation', 0)
	elseif data == "tupo" then
		subui = GlobalHooks.UI.CreateUI('PetBreakThrough', 0)
	elseif data == "jineng" then
		subui = GlobalHooks.UI.CreateUI('FriendSearch', 0)	
	end

	self.ui:AddSubUI(subui.ui)

end
-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	print('OnEnter',self,...)
	self.ui.comps.btn_close.TouchClick = function()
		print('bt_login TouchClick')
		self.ui:Close()
	end

	self.radioExt:selectIdx(1)
	self:doSwitchTaskPage(1)
end

function _M.OnExit( self )
	print('OnExit')
end

function _M.OnDestory( self )
	print('OnDestory')
end

function _M.OnInit( self )
	print('OnInit')
    self.radioExt = RadioGroupExt.new(
    {self.ui.comps.tbt_an1, self.ui.comps.tbt_an2, self.ui.comps.tbt_an3, self.ui.comps.tbt_an4},
    nil, {'xinxi', 'tupo','jineng','jineng2'},
    self._self_doSwitchTaskPage)
end

return _M