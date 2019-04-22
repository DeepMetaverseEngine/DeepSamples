local _M = {}
_M.__index = _M

local roleMenu

local function ShowRoleMenu(isShow)
	-- print('---------ShowRoleMenu----------')
	if roleMenu then
		if roleMenu.ui.menu.Visible ~= isShow then
			roleMenu.ui.menu.Visible = isShow
		end
	end
end

function _M.OnEnter( self )
	-- print('AttributeFrame OnEnter', self.ui.tag)
	if self.ui.tag == "AttributeRoleFrame" then
		roleMenu = GlobalHooks.UI.CreateUI('AttributeRole', 0)
		local param = self.ui.menu.ExtParam
		local showEquipIndex = (param and #self.ui.menu.ExtParam > 0) and param[1] or nil
		local equipSelectIndex = (param and #self.ui.menu.ExtParam > 1) and param[2] or nil
		local equipMenu = GlobalHooks.UI.CreateUI('AttributeEquip', 0, { ShowRoleMenu = ShowRoleMenu, ShowEquip = showEquipIndex, EquipSelIndex = equipSelectIndex } )
		self.ui:AddSubUI(roleMenu.ui)
		self.ui:AddSubUI(equipMenu.ui)
	elseif self.ui.tag == "AttributeInfoFrame" then
		roleMenu = GlobalHooks.UI.CreateUI('AttributeRole', 0)
		local infoMenu = GlobalHooks.UI.CreateUI('AttributeInfo', 0)
		self.ui:AddSubUI(roleMenu.ui)
		self.ui:AddSubUI(infoMenu.ui)
	end
end

function _M.OnExit( self )
	-- print('AttributeFrame OnExit')
	-- self.ui.menu.ParentMenu:Close()
end

function _M.OnDestory( self )
	-- print('AttributeFrame OnDestory')
end

function _M.OnInit( self )
	-- print('AttributeFrame OnInit')
    -- self.ui:EnableTouchFrame(false)
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
end

return _M