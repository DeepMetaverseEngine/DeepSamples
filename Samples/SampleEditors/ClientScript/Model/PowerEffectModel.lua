local _M = {}
_M.__index = _M

local BusinessModel = require 'Model/BusinessModel'

local curPower=0
local nowPower=0
local canShowPower = false

function _M.Notify(status, userdata,self)

	if not canShowPower then
	end
    if curPower==0 then
	   curPower=DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.FIGHTPOWER)
	end

	if userdata:ContainsKey(status, UserData.NotiFyStatus.FIGHTPOWER)then
	    nowPower= DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.FIGHTPOWER)
			if nowPower > curPower then
				BusinessModel.Notify("pFightPower",nowPower)
				local Params = {curPower = curPower,nowPower = nowPower}
				GlobalHooks.UI.OpenUI('PowerMain',0,Params)
			end  
			   curPower = nowPower
	end	
end

--进入场景时调用
local function OnEnterScene()
   canShowPower = true
end

--退出场景时调用，参数：是否短线重连触发的切场景
local function OnExitScene(reconnect)
	canShowPower = false
end

local function initial()
	DataMgr.Instance.UserData:AttachLuaObserver('PowerMain',_M)
end 

local function fin()
	--关闭
	DataMgr.Instance.UserData:DetachLuaObserver('PowerMain')
end

_M.initial=initial
_M.fin=fin
_M.OnEnterScene = OnEnterScene
_M.OnExitScene = OnExitScene
return _M