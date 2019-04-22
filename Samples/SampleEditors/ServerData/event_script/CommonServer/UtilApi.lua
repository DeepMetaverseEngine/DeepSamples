--! @addtogroup CommonServer
--! @{
--! @cond DO_NOT_DOCUMENT

local keyPath = _total_global.path .. 'event_script/ScriptKey'
local key_scriptMap = require(keyPath)
local session_cache = {}
local CommonServer = {
	Task = {},
	Listen = {}
}

local function GetSession(uuid)
	local session = EventApi.GetSession(uuid)
	if not session then
		session = session_cache[uuid]
	end
	return session
end

local function cleanLogic(success, reason, arg, ele, params)
	if ele and ele.radar == 1 then
		local ClientApi = EventApi.GetClientApi(arg)
		ClientApi.StopRadar.NoAutoWait = true
		local key = tostring(arg.QuestID or arg.Key)
		ClientApi.StopRadar(key)
	end
end

local function mainLogic(arg, ele, params)
	if ele then
		for _, v in ipairs(ele.condition and ele.condition.id or {}) do
			if not EventApi.string_IsNullOrEmpty(v) then
				local condOk, condRet = EventApi.Task.Wait(EventApi.Task.AddEventByKey(v, EventApi.GetNextArg(arg)))
				if not condOk then
					return condOk, condRet
				end
			end
		end
	end
	local eid = EventApi.Task.AddEvent(unpack(params))
	assert(eid and eid ~= 0)
	EventApi.Listen.ListenEvent(
		eid,
		function(...)
			local fatherID = EventApi.GetParentEventID(eid)
			assert(fatherID and fatherID ~= 0)
			EventApi.TriggerEvent(fatherID, ...)
		end
	)
	if not ele or arg.Sub then
		return EventApi.Task.Wait(eid)
	end
	if ele and ele.auto_move then
		local current_id = EventApi.GetCurrentEventID()
		local move_mapid, move_flag, x, y, monsterid
		if ele.move_mapid then
			move_mapid = ele.move_mapid
		elseif ele.mapid then
			move_mapid = ele.mapid
		else
			move_mapid = arg.MapTemplateID
		end
		if not EventApi.string_IsNullOrEmpty(ele.move_flag) then
			move_flag = ele.move_flag
		elseif not EventApi.string_IsNullOrEmpty(ele.flag) then
			move_flag = ele.flag
		end
		if not move_flag and not EventApi.string_IsNullOrEmpty(ele.coordinate) then
			x, y = unpack(EventApi.string_split(ele.coordinate, ','))
			x, y = tonumber(x), tonumber(y)
		end
		if ele.monsterid then
			monsterid = type(ele.monsterid) == 'table' and ele.monsterid[1] or ele.monsterid
		end
		local function SetQuestMove(reconnect)
			if reconnect then
				EventApi.Task.WaitPlayerReady(arg.PlayerUUID)
			end
			local ret = EventApi.TrySetQuestMove(arg, ele.auto_move, move_mapid, {x = x, y = y, flag = move_flag,
			 monsterID = monsterid, radar = ele.radar,hints = ele.dont_finding_hints})
			return ret
		end
		if SetQuestMove() then
			EventApi.Listen.SessionReconnect(
				arg.PlayerUUID,
				function()
					EventApi.Task.AddEventTo(current_id, SetQuestMove, true)
				end
			)
		end
	end
	local argTable = EventApi.DynamicToArgTable(EventApi.Task.Wait(eid))
	local ok = argTable[1]
	local nextOutput

	if ok then
		-- success events
		for _, v in ipairs(ele.success and ele.success.id or {}) do
			if not EventApi.string_IsNullOrEmpty(v) then
				local nextArg = EventApi.GetNextArg(arg)
				EventApi.Task.StartEventByKey(v, nextArg)
			end
		end
	else
		-- failed events
		for _, v in ipairs(ele.fail and ele.fail.id or {}) do
			if not EventApi.string_IsNullOrEmpty(v) then
				local nextArg = EventApi.GetNextArg(arg)
				EventApi.Task.StartEventByKey(v, nextArg)
			end
		end
	end
	-- next events
	if ok and not EventApi.string_IsNullOrEmpty(ele.next_event) then
		local nextArg = EventApi.GetNextArg(arg)
		nextArg.Pre = {Key = key, IsSuccess = ok, UnpackOutput = true, Output = EventApi.RepackArgTabel(argTable, 2)}
		local nextid = EventApi.Task.AddEventByKey(ele.next_event, nextArg)
		return EventApi.Task.Wait(nextid)
	end
	return EventApi.ArgTableToDynamic(argTable)
end

local function DoKeyEvent(isAdd, toID, key, arg, ...)
	local name, id = unpack(EventApi.string_split(key, '.', 2))
	local scriptInfo = key_scriptMap[name]
	if not scriptInfo then
		EventApi.warn('not find scriptkey ' .. key .. ' try find scritp')
		scriptInfo = {path = key, manager = EventApi.ManagerName}
	end
	arg = arg or EventApi.GetArg()
	arg = EventApi.FixArg(arg)
	local targetManagerName = scriptInfo.manager
	local uuid = targetManagerName and arg[targetManagerName .. 'UUID'] or nil
	if EventApi.IsLocolManager(targetManagerName, uuid) then
		local params
		local ele
		arg = EventApi.FixArg(arg)
		assert(not arg.Key, 'must new arg')
		arg.Key = key
		arg.Sub = scriptInfo.sub
		local numid = tonumber(id)
		if numid then
			-- EventApi.pprint('scriptInfo',scriptInfo)
			ele = EventApi.FindExcelData(scriptInfo.excel, numid)
			params = {{main = scriptInfo.path, env = {arg = arg, Key = arg.Key, ArgID = numid}}, ele, ...}
		else
			params = {{main = scriptInfo.path, env = {arg = arg, Key = arg.Key, ArgID = id}}, ...}
		end
		if toID then
			return EventApi.Task.AddEventTo(toID, {main = mainLogic, desc = key, clean = cleanLogic}, arg, ele, params)
		elseif isAdd then
			return EventApi.Task.AddEvent({main = mainLogic, desc = key, clean = cleanLogic}, arg, ele, params)
		else
			return EventApi.Task.StartEvent({main = mainLogic, desc = key, clean = cleanLogic}, arg, ele, params)
		end
	else
		if targetManagerName == 'Client' then
			EventApi.print((toID and 'Add' or 'Start') .. ' client event:' .. key)
			if not uuid then
				-- TODO load session
			end
		end
		local remoteApi = EventApi.CreateRemoteApi(targetManagerName, uuid)
		if toID then
			return remoteApi.Task.AddEventToByKey(toID, key, arg, ...)
		elseif isAdd then
			return remoteApi.Task.AddEventByKey(key, arg, ...)
		else
			return remoteApi.Task.StartEventByKey(key, arg, ...)
		end
	end
end
--! @endcond

--! @brief 按key的方式添加一个字事件
--! @param key 即excel表格中的表名.行id，例：reward.2
function CommonServer.Task.AddEventByKey(key, arg, ...)
	-- EventApi.print('AddEventByKey:' .. key)
	-- EventApi.print('AddEventByKey:' .. (key or 'nil') .. '\n' .. debug.traceback())
	return DoKeyEvent(true, nil, key, arg, ...)
end

function CommonServer.Task.AddEventToByKey(id, key, arg, ...)
	-- EventApi.print('AddEventToByKey:' .. key)
	-- EventApi.print('AddEventByKey:' .. (key or 'nil') .. '\n' .. debug.traceback())
	return DoKeyEvent(true, id, key, arg, ...)
end

-- arg:{ZoneUUID-场景实例ID，MapTemplateID-地图模板ID，PlayerUUID-玩家UUID}
-- excelEvent中返回值如果有Continue,则可以作为下一次StartEventByKey的arg
--! @brief 按key的方式启动一个新事件
--! @param key 即excel表格中的表名.行id，例：reward.2
function CommonServer.Task.StartEventByKey(key, arg, ...)
	-- EventApi.print('StartEventByKey:' .. key)
	-- EventApi.print('StartEventByKey:' .. (key or 'nil') .. '\n' .. debug.traceback())
	return DoKeyEvent(false, nil, key, arg, ...)
end

function CommonServer.Task.StopEventByKey(key)
	local name, id = unpack(string.split(key, '.'))
	local scriptInfo = key_scriptMap[name]
	if not scriptInfo then
		scriptInfo = {path = key, manager = Api.ManagerName}
	end
	local eids = {Api.GetEventID(scriptInfo.path)}
	for _, v in ipairs(eids) do
		local sandbox = Api.GetEventSandbox(v)
		if sandbox.Key == key then
			Api.Task.StopEvent(v)
		end
	end
end

function CommonServer.IsClientScript(key)
	local name, id = unpack(EventApi.string_split(key, '.'))
	local scriptInfo = key_scriptMap[name]
	return scriptInfo and scriptInfo.manager == 'Client'
end

--! @brief 通过arg获取ZoneApi
function CommonServer.GetZoneApi(arg)
	local uuid
	local t = type(arg)
	if t == 'table' then
		uuid = arg.ZoneUUID
	elseif t == 'string' then
		uuid = arg
	end
	if not uuid then
		return nil
	end
	return EventApi.CreateRemoteApi('Zone', uuid)
end

--! @brief 通过arg获取ClientApi
function CommonServer.GetClientApi(arg)
	local uuid
	local t = type(arg)
	if t == 'table' then
		if not arg.ClientUUID and arg.PlayerUUID then
			arg.ClientUUID = GetSession(arg.PlayerUUID)
		end
		uuid = arg.ClientUUID
	elseif t == 'string' then
		uuid = GetSession(arg)
	end
	if not uuid then
		return nil
	end
	return EventApi.CreateRemoteApi('Client', uuid)
end

function CommonServer.GetGuildApi(arg)
	local uuid
	local t = type(arg)
	if t == 'table' then
		uuid = arg.PlayerUUID
	elseif t == 'string' then
		uuid = arg
	end
	local serverGroupID = EventApi.GetPlayerServerGroup(uuid)
	return EventApi.CreateRemoteApi('Guild', serverGroupID)
end

function CommonServer.GetArenaServiceApi()
	return EventApi.CreateRemoteApi('Arena')
end

function CommonServer.GetMasterRaceApi(arg)
	local uuid
	local t = type(arg)
	if t == 'table' then
		uuid = arg.PlayerUUID
	elseif t == 'string' then
		uuid = arg
	end
	local serverGroupID = EventApi.GetPlayerServerGroup(uuid)
	return EventApi.CreateRemoteApi('MasterRace', serverGroupID)
end

function CommonServer.SendMessageToClient(uuid, msgname, content)
	if EventApi.IsPlayerExist and not EventApi.IsPlayerExist(uuid) then
		return
	end
	local session = GetSession(uuid)
	EventApi.SendMessage('Client', session, msgname, content)
end

--! @brief 通过arg获取PlayerApi
function CommonServer.GetPlayerApi(arg)
	local uuid
	local t = type(arg)
	if t == 'table' then
		uuid = arg.PlayerUUID
	elseif t == 'string' then
		uuid = arg
	end
	if not uuid then
		return nil
	end
	return EventApi.CreateRemoteApi('Player', uuid)
end

function CommonServer.GetAreaManagerApi()
	return EventApi.CreateRemoteApi('AreaManager')
end

--1 自动 2 手动
function CommonServer.TrySetQuestMove(arg, movestate, mapid, params)
	if movestate and movestate > 0 and arg.QuestID and mapid then
		local ClientApi = Api.GetClientApi(arg)
		ClientApi.SetQuestAutoMove.NoAutoWait = true
		ClientApi.SetQuestAutoMove(arg.QuestID, movestate, mapid, params)
		return true
	end
	if params.radar == 1 then
		local ClientApi = Api.GetClientApi(arg)
		ClientApi.StartRadar.NoAutoWait = true
		local key = tostring(arg.QuestID or arg.Key)
		ClientApi.StartRadar(mapid, key, params)
		return true
	end
	return false
end

function CommonServer.SaveSession(playeruuid, session)
	session_cache[playeruuid] = session
end

function CommonServer.GetArg(other)
	local ret = EventApi.FixArg({})
	if other then
		EventApi.merge_table(ret, other)
	end
	return ret
end

function CommonServer.FixArg(arg)
	if not arg.ZoneUUID and EventApi.GetZoneUUID then
		arg.ZoneUUID = EventApi.GetZoneUUID()
	end
	if not arg.MapTemplateID and EventApi.GetMapTemplateID then
		arg.MapTemplateID = EventApi.GetMapTemplateID()
	end
	if not arg.PlayerUUID and EventApi.GetPlayerUUID then
		arg.PlayerUUID = EventApi.GetPlayerUUID()
	end
	if arg.PlayerUUID then
		arg.ClientUUID = GetSession(arg.PlayerUUID)
	end
	arg.TimeNow = os.time()
	return arg
end

function CommonServer.Task.WaitPlayerReady(uuid)
	if not EventApi.IsPlayerReady(uuid) then
		EventApi.Task.Wait(EventApi.Listen.PlayerReady(uuid))
	end
end

return CommonServer
--! @}
