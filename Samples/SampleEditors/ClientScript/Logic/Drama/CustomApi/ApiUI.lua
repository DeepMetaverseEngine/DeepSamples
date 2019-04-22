---------------------------------
--! @file
--! @brief a Doxygen::Lua ApiUI.lua
---------------------------------
print('-----------------ApiUI.lua load-----------------')
local _M = {}

function _M.OpenByTag(parent,...)
   GlobalHooks.UI.OpenUI(...)
end

function _M._asyncTestWait(self, sec)
	LuaTimer.Add(2000,30,function()
		self:Stop()
		return false
	end)
	self:Await()
end

function _M.ShowTip(self,content,sec)
	TLBattleScene.Instance:AddStoryTip(content,sec)
end

function _M.CleanloadedCache()
	for key,v in pairs(GlobalHooks.UI.UITAG) do
		MenuMgr.Instance:RemoveCacheUIByTag(key,200)
		local t = package.loaded[v[1]]
		if t and t.fin then
			t.fin()
		end
		package.loaded[v[1]] = nil
	end
end

function _M._asyncTestWaitSecond(self,sec)
	LuaTimer.Add(sec*1000,function()
		self:Stop()
	end)
	self:Await()
end


function _M.testUI(self)
	local tempUI = GlobalHooks.UI.CreateUI('AutoEquipsMain', 0)
	local menu = nil
	if type(tempUI) == 'table' then
		menu = tempUI.ui.menu
	else
		menu = tempUI
	end
	MenuMgr.Instance:AddMsgBox(menu)
end
return _M 