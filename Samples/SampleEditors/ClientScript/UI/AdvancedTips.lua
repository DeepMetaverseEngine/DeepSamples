local _M = {}
_M.__index = _M
 

local function UnlockEffect(self,parent)
	if self.unlockEffectId ~= nil then
		RenderSystem.Instance:Unload(self.unlockEffectId)
	end
	local transSet = TransformSet()
	-- local parent = self.ui.comps.cvs_pulse
	transSet.Parent = parent.Transform
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_advanced.assetbundles'
	self.unlockEffectId = RenderSystem.Instance:PlayEffect(assetname, transSet)

	SoundManager.Instance:PlaySoundByKey('jinjie',false)
end


function _M.OnEnter( self, ShowText)
 	self.ui.comps.lb_content.Text = ShowText or ""

 	UnlockEffect(self,self.ui.comps.cvs_back)
end

function _M.OnExit( self )
	print('OnExit')
end

function _M.OnDestory( self )
	print('OnDestory')
	if self.unlockEffectId ~= nil then
		RenderSystem.Instance:Unload(self.unlockEffectId)
	end
end


function _M.OnInit( self )
	print('OnInit')

	self.ui.menu.ShowType = UIShowType.Cover
	-- self.ui.menu.event_PointerUp = function( sender, e )
	-- 	self.ui:Close()
	-- end
	self.ui.comps.btn_close.TouchClick = function (sender)
		self.ui:Close()
	end
end

return _M