UI = {}

require('UI/OpenUI.lua')
require('UI/UITAG.lua')

local function EVENT_SYS_GAME_START(event, params)
	print('Init.EVENT_SYS_GAME_START')
	-- UI.OpenUI('Test')
end

local function EVENT_SYS_LOGIN_SUCCESS(event, params)
	--删除登录界面

end

local function EVENT_SYS_ENTER_SCENE(event, params)
	--开启loading界面
end

local function EVENT_SYS_READY(event, params)
	--关闭loading界面
end

local function EVENT_SYS_GUIDE(event, params)
	
end

local function EVENT_SYS_ADD_UNIT(event, params)

end

local function EVENT_SYS_REMOVE_UNIT(event, params)

end

local function EVENT_SYS_HP_CHANGED(event, params)

end

local function EVENT_SYS_MP_CHANGED(event, params)

end

local function EVENT_SYS_LV_CHANGED(event, params)
end

local function EVENT_SYS_EXP_CHANGED(event, params)
end

EventManager.Subscribe(Events.SYS_GAME_START, EVENT_SYS_GAME_START)
EventManager.Subscribe(Events.SYS_ADD_UNIT, EVENT_SYS_ADD_UNIT)

