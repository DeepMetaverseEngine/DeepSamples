local UIUtil = require 'UI/UIUtil'
local FuncOpen = require "Model/FuncOpen"

local function OpenUI( tag,cacheLevel,... )
	local m = GlobalHooks.UI.CreateUI(tag,cacheLevel,...)
	if m == nil then
		return
	end

	local menu
	if type(m) == 'table' then
		menu = m.ui.menu
		if m.OnOpen then m.OnOpen() end
	else
		menu = m
	end
	if menu then MenuMgr.Instance:AddMenu(menu) end

	if type(tag) == 'table' then
		tag = tag.tag
	end
	if GlobalHooks.IsFuncWaitToPlay(tag) then
		FuncOpen.SetPlayedFunctionByName(tag)
	end
	return m
end

local function OpenMsgBox(tag, cacheLevel,...)
	local m = GlobalHooks.UI.CreateUI(tag,cacheLevel,...)
	if m == nil then
		return
	end

	local menu
	if type(m) == 'table' then
		menu = m.ui.menu
		if m.OnOpen then m.OnOpen() end
	else
		menu = m
	end
	if menu then MenuMgr.Instance:AddMsgBox(menu) end
	if type(tag) == 'table' then
		tag = tag.tag
	end
	if GlobalHooks.IsFuncWaitToPlay(tag) then
		FuncOpen.SetPlayedFunctionByName(tag)
	end
	return m	
end

local function OpenHud(tag, cacheLevel,...)
	local m = GlobalHooks.UI.CreateUI(tag,cacheLevel,...)
	if m == nil then
		return
	end

	local menu
	if type(m) == 'table' then
		menu = m.ui.menu
		if m.OnOpen then m.OnOpen() end
	else
		menu = m
	end
	if menu then MenuMgr.Instance:AddHudMenu(menu) end
	if type(tag) == 'table' then
		tag = tag.tag
	end
	if GlobalHooks.IsFuncWaitToPlay(tag) then
		FuncOpen.SetPlayedFunctionByName(tag)
	end
	return m	
end

-- 动态 {tag = 'xxx',info = {'UI/UIFrame','xml/common/common_denglong.gui.xml'}}
local function CreateUI( tag,cacheLevel,... )
	local tagInfo
	if type(tag) == 'table' then
		local srcInfo = tag
		tag = srcInfo.tag
		tagInfo = srcInfo.info
	end

	--检查功能开放
	if not GlobalHooks.IsFuncOpen(tag, true) then
		return nil
	end

	cacheLevel = cacheLevel or 0
	local node
	
	if cacheLevel >= 0 then
		node = MenuMgr.Instance:GetCacheUIByTag(tag)
	end

	if not node then
		if not tagInfo then
			tagInfo = GlobalHooks.UI.UITAG[tag]
		end
		if type(tagInfo) ~= 'table' then
			error(tag..' UITAG 书写错误: 不是一个table'..type(tagInfo))
		end

		if type(tagInfo[1]) == 'string' then
			local last = package.loaded[tagInfo[1]]
			if last and not last.initial and not last.fin and not last.OnEnterScene and not last.OnExitScene then
				package.loaded[tagInfo[1]] = nil
			end
			local m = require(tagInfo[1])
			m = UIUtil.Bind(m,tag,tagInfo[2])
			local init_p
			if type(tagInfo[3]) == 'table' then
				init_p = tagInfo[3]
			else
				init_p = tagInfo[4]
			end
			if m.OnInit then
				m:OnInit(init_p)
			end
			node = m.ui.menu
			node.LuaTable = m
			if tagInfo[3] == 'needBack' then
				node.NeedBack = true
			end
		elseif type(tagInfo[1]) == 'function' then
			node = tagInfo(tagInfo[2])
		end
	end

	if node then
		node.MenuTag  = tag
		node.CacheLevel = cacheLevel
		local params = {...}
		if #params > 0 then
			node:SetLuaParams(params)
		end
		local lua = node.LuaTable
		if lua then
			lua.__params = params
			--检查ui红点
			GlobalHooks.UI.CheckUIRedPoint(tag, lua)
			return lua
		else
			return node
		end
	end
end

-- 返回所有满足条件的UI，默认返回第一个，用{}获取所有
-- 例子：loacl node = GlobalHooks.UI.FindUI('Test')
--	   node.ui.menu.Visible = false
local function FindUI(tag)
	local list = MenuMgr.Instance:FindMenusByTag(tag)
	if not list then return nil end
	local ret = {}
	for menu in Slua.iter(list) do
		table.insert(ret, menu.LuaTable and menu.LuaTable or menu)
	end
	return unpack(ret)
end

local function CloseUIByTag(tag)
	local ui = {FindUI(tag)}
	for _, v in ipairs(ui or {}) do
		v:Close()
	end
end

local function OnUIOpen(eventname, params)
	local tag = params.tag
	local cacheLevel = params.cacheLevel
	local param = {}
	-- print("---------OnUIOpen-------------", tag, cacheLevel, #params)
	if params.params ~= nil then
		for i=1, #params.params do
			param[i] = params.params[i]
		end
	end
	GlobalHooks.UI.OpenUI(tag, cacheLevel, unpack(param))
end

-- UI缓存静态配置表，key代表UITag，value代表缓存级别，-1表示不缓存，值越大缓存优先级越高
local CacheDefaultLv = {
	AttributeRoleFrame = -1,
	AttributeInfoFrame = -1,
	AttributeRole = 100,
	AttributeEquip = 100,
	RoleBagItem = 100,
	ChatMainSmall = 100,
	UINpcTalk = 100,
}

local function InitUICache(eventname, params)
  MenuMgr.Instance:UICacheInit(CacheDefaultLv)
end

local backQueue = {}

-- key 必须唯一，否则会相互覆盖
-- callback 按下返回键后会回调到订阅者的模块
-- callback里走自己的逻辑，比如关闭UI，或者什么都不做（拦截）
-- callback必须带bool返回值，返回true 回调后保持订阅，返回false 回调后自动取消订阅
local function SubscribeGlobalBack( key, callback )
	if callback then
		local data = {}
		data.key = key
		data.callback = callback
		backQueue[#backQueue + 1] = data
		print('------------SubscribeGlobalBack------------', key)
	end
end

-- key 订阅时用的key
local function UnsubscribeGlobalBack( key )
	for i = #backQueue, 1, -1 do
		local data = backQueue[i]
		if data.key == key then
			table.remove(backQueue, i)
			print('------------UnsubscribeGlobalBack------------', key)
			break
		end
	end
end

local function OnBackClick( eventname, params )
	print_r('------------OnBackClick-------', backQueue)
	if #backQueue > 0 then
		local data = backQueue[#backQueue]
		if type(data.callback) == 'function' then
			if not data.callback() then
				UnsubscribeGlobalBack(data.key)
			end
		elseif type(data.callback) == 'userdata' then
			if not data.callback:Invoke('') then
				UnsubscribeGlobalBack(data.key)
			end
		end
	else
		EventManager.Fire('Event.System.Back.NoUI', {})
	end
end


local function fin()
	print("------------- OpenUI.fin --------------")
	EventManager.Unsubscribe("Event.OpenUI.Open", OnUIOpen)
	EventManager.Unsubscribe("Event.System.Back", OnBackClick)
	backQueue = {}
end

local function initial()
	print("------------- OpenUI.initial --------------")
	backQueue = {}
	InitUICache()
	EventManager.Subscribe("Event.OpenUI.Open", OnUIOpen)
	EventManager.Subscribe("Event.System.Back", OnBackClick)
end

GlobalHooks.UI.OpenUI = OpenUI
GlobalHooks.UI.OpenMsgBox = OpenMsgBox
GlobalHooks.UI.CreateUI = CreateUI
GlobalHooks.UI.FindUI = FindUI
GlobalHooks.UI.CloseUIByTag = CloseUIByTag
GlobalHooks.UI.OpenHud=OpenHud
GlobalHooks.SubscribeGlobalBack = SubscribeGlobalBack
GlobalHooks.UnsubscribeGlobalBack = UnsubscribeGlobalBack

return { initial = initial, fin = fin }
