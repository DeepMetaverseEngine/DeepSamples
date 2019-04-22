---------------------------------
--! @file
--! @brief a Doxygen::Lua DramaManger.lua
---------------------------------
print('-----------------DramaManger.lua load-----------------')
local DEBUG_MODE
if UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor or UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsPlayer then
	DEBUG_MODE = true
else
	DEBUG_MODE = false
end

local RootDir = 'design/'
local ApiList = 
{
	Quest = 'Logic/Drama/CustomApi/ApiQuest',
	UI = 'Logic/Drama/CustomApi/ApiUI',
	Unit = 'Logic/Drama/CustomApi/ApiUnit',
	Global = 'Logic/Drama/CustomApi/ApiGlobal',
}



local Director
local DramaInstanceEnv

local function GetSandbox(name)
	local FullApi = require 'Logic/Drama/FullEnv'
	local Helper = require 'Logic/Helper'

	for _,v in ipairs(FullApi) do
		if v == name then
			local ret = {}
			for key,v in pairs(_G) do
				ret[key] = v
			end
			return ret
		end
	end

	local Sandbox = require 'Logic/Sandbox'
	return  Sandbox.getSandbox()
end

local function loadAndGetEnv(sFileName)
	
	local fUntrusted, sMessage = loadfile(RootDir .. sFileName)
	if not fUntrusted then return nil, sMessage end
	local tSandbox = GetSandbox(sFileName)
	local function process()
		if type(fUntrusted) ~= 'function' then
			return false, fUntrusted
		end
		local _ENV = tSandbox
		if setfenv then
			setfenv(fUntrusted, tSandbox)
		end
		--local ok, _ = pcall(fUntrusted)
		return pcall(fUntrusted)
	end
	local ok, res = process()
	if not ok then return nil, res end
	if DramaInstanceEnv then
		for k, v in pairs(DramaInstanceEnv) do
			tSandbox[k] = v
		end
	end
	return tSandbox
end

local function RegisterApi()
	for k, v in pairs(ApiList) do
		Director:RegisterApi(require(v), k ~= 'Global' and k or nil)
	end

	print_r(Director.api)
	-- 需要注入sandbox的第三方api
	DramaInstanceEnv = {
		Start = GlobalHooks.Drama.Start,
		Stop = GlobalHooks.Drama.Stop,
		print_r = print_r
	}

end

local function StartsWith(org, res)
    local pos = string.find(org, res)
    if(pos and pos == 1)then
        return true
    else
        return false
    end
end 
function StartCGLua(org)
	local filter = {"effect/"}
	local isCg = false
	for i,v in ipairs(filter) do
		if StartsWith(org,v) then
			return false
		end
	end
	return true
end
local function Init()
	
	local DramaDirector = require 'Logic/Drama/DramaDirector'
	GlobalHooks.Drama.mgr = DramaDirector.Create(loadAndGetEnv)
	Director = GlobalHooks.Drama.mgr
	Director:AddInvalidCB(function(script)
		-- name script.name
		-- id script.id
		if not script.parent then
			-- 根节点
			print('script.name end',script.name)
			EventManager.Fire("DirectorEventFinish",{fileName = script.name,id = script.id})
		end
	end)
	RegisterApi()
	LuaTimer.Add(0, 35, function(id)
		Director:Update()
		return true
	end)
end

local function StopAll()
	if not Director then return end
	Director:StopAllScript()
end



local function Start(name)
	if not GlobalHooks.Drama.mgr then
		Init()
	end
	if not Director then
		Director = GlobalHooks.Drama.mgr
	end
	return Director:StartScript(name)
end

local function Stop(name)
	if not Director then return end
	if type(name) == 'number' then
		Director:StopScript(name)
	else
		local ids = {Director:GetScriptID(name)}
		--print("stop=",name)
		--print_r(ids)
		for _, v in ipairs(ids) do
			Director:StopScript(v)
		end
	end
end


local function HotReload()
	if not DEBUG_MODE then return end
	StopAll()
	for k, v in pairs(ApiList) do
		package.loaded['/ui_edit/lua/' .. v] = nil
	end
	package.loaded['/ui_edit/lua/Logic/Drama/DramaDirector'] = nil
	package.loaded['/ui_edit/lua/Logic/Sandbox'] = nil
	package.loaded['/ui_edit/lua/Logic/Drama/DramaManger'] = nil
	package.loaded['/ui_edit/lua/Logic/Drama/FullEnv'] = nil
	GlobalHooks.Drama = nil
	require 'Logic/Drama/DramaManger'
end

local function OnKeyDown(keyCode)
	if not DEBUG_MODE then return end
	Start('test/'..keyCode..'.lua')
end


local function GetScriptFile(name)
	local filelist = require 'design/file_list'
	if filelist ~= nil and filelist[name] ~= nil then
		local content = filelist[name]
		return content[2]
	end
	return nil
end
GlobalHooks.Drama = GlobalHooks.Drama or {}
GlobalHooks.Drama.Start = Start
GlobalHooks.Drama.Stop = Stop
GlobalHooks.Drama.StopAll = StopAll
GlobalHooks.Drama.HotReload = HotReload
GlobalHooks.Drama.OnKeyDown = OnKeyDown
GlobalHooks.Drama.GetScriptFile = GetScriptFile