local _M = {}
_M.__index = _M


local layer --镇妖塔层数
local wave  --波数
local time  --时间


--获取镇妖塔数据，并发送通知给UI
local function GetEnvVarInfo(eventname,params)

	if params.key=='CurLayer' then 
		layer = params.value
	end
	if params.key =='CurWave' then
		wave = params.value
	end
	if params.key =='count_down' then  
		time = params.value	
	end

	EventManager.Fire("Event.SceneStageEvent",{CurLayer=layer,CurWave=wave,CountDownTime=time})
end

local function GetInfo()
	return layer,wave,time
end


local function initial()
	--添加监听
    EventManager.Subscribe("Event.SyncEnvironmentVarEvent",GetEnvVarInfo)
end


local function fin()
	--注销监听
	EventManager.Unsubscribe("Event.SyncEnvironmentVarEvent",GetEnvVarInfo)
end

_M.GetInfo=GetInfo
_M.initial=initial
_M.fin=fin
_M.GetEnvVarInfo=GetEnvVarInfo

return _M