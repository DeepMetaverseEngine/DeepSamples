--! @addtogroup Common
--! @{
local DataHelper = require(_total_global.path .. 'event_script/DataHelper')
DataHelper.SetRootPath(_total_global.path .. _total_global.config.ExcelRootPath)
local keyPath = _total_global.path .. 'event_script/ScriptKey'
local key_scriptMap = require(keyPath)

local CommonApi = {
	Task = {}
}

local _hotCache = {}

function CommonApi.CheckHotUpdate(script_name)
	local reload = false
	local lines = {}
	local fname = EventApi.RootPath .. script_name .. '.lua'
	EventApi.SetAllDirty()
	for line in io.lines(fname) do
		table.insert(lines, line)
	end
	local lastLines = _hotCache[script_name] or {}
	if #lines == #lastLines then
		for i, v in ipairs(lines) do
			if v ~= lastLines[i] then
				reload = true
				break
			end
		end
	elseif #lastLines ~= 0 then
		reload = true
	end
	_hotCache[script_name] = lines
	return reload
end

function CommonApi.Task.HotReload(script_name, sec)
	return EventApi.Listen.AddPeriodicSec(
		sec or 3,
		function()
			local reload = EventApi.CheckHotUpdate(script_name)
			if reload then
				EventApi.Task.StopEvent(script_name)
				EventApi.Task.StartEvent(script_name)
			end
		end
	)
end



local cache_keep_sec = 30
local xls_caches = {}
local xls_versions = {}
function CommonApi.FindExcelData(tb_name, find_key)
	local t = type(find_key)
	local ret
	local iskey = t == 'string' or t == 'number'
	if iskey then
		xls_caches[tb_name] = xls_caches[tb_name] or {}
		local info = xls_caches[tb_name][find_key]
		if info and os.time() - info.time > cache_keep_sec then
			info = nil
			xls_caches[tb_name][find_key] = nil 
		end
		ret = info and info.data
	end
	if not ret then
		ret = EventApi.GetXlsData(tb_name, find_key)
		if iskey then
			local info = {time = os.time(), data = ret}
			xls_caches[tb_name][find_key] = info
		end
	end
	return ret
end

function CommonApi.FindFirstExcelData(tb_name, find_key)
	local ret = EventApi.FindExcelData(tb_name, find_key)
	if type(find_key) == 'table' then
		return ret and ret[1]
	else
		return ret
	end
end

function CommonApi.GetExcelConfig(key)
	local ele = EventApi.FindExcelData('config/game_config.xlsx/game_config', key)
	if not ele then
		return nil
	elseif ele.paramtype == 'NUMBER' then
		return tonumber(ele.paramvalue)
	else
		return ele.paramvalue
	end
end

function CommonApi.GetExcelByEventKey(key)
	local name, id = unpack(EventApi.string_split(key, '.'))
	id = tonumber(id)
	if name and id then
		local scriptInfo = key_scriptMap[name]
		return EventApi.FindExcelData(scriptInfo.excel, id), name, id
	end
end

function CommonApi.CollectEventKeys(prefix, fn)
	local scriptInfo = key_scriptMap[prefix]
	local datas = EventApi.FindExcelData(scriptInfo.excel, {})
	local ret = {}
	for _, v in ipairs(datas) do
		local key = prefix .. '.' .. v.id
		ret[key] = v
		if fn then
			fn(key, v)
		end
	end
	return ret
end

function CommonApi.CollectEventTypes()
	return table.keys(key_scriptMap)
end

function CommonApi.StopEventByKey(key)
	local name, id = unpack(string.split(key, '.'))
	local scriptInfo = key_scriptMap[name]
	if not scriptInfo then
		scriptInfo = {path = key, manager = Api.ManagerName}
	end
	local eids = {Api.GetEventID(scriptInfo.path)}
	for _, v in ipairs(eids) do
		if key == scriptInfo.path then
			Api.StopEvent(v)
		else
			local sandbox = Api.GetEventSandbox(v)
			if sandbox.Key == key then
				Api.StopEvent(v)
			end
		end
	end
end

function CommonApi.GetRounding(a)
    local r1, r2 = math.modf(a, 1)
    r2 = r2 >= 0.5 and 1 or 0
    return r1 + r2
end

function CommonApi.GetRunningKeys()
	local all = Api.GetRunningEvents()
	local ret = {}
	for id, path in pairs(all) do
		local sandbox = Api.GetEventSandbox(id)
		if sandbox.Key then
			ret[sandbox.Key] = true
		else
			ret[path] = true
		end
	end
	return table.keys(ret)
end

function CommonApi.ParseExcelPosition(ele)
	local x, y
	if ele.coordinate then
		x, y = unpack(EventApi.string_split(ele.coordinate, ','))
		x = x or -1
		y = y or -1
		x, y = tonumber(x), tonumber(y)
	end
	return ele.flag, x, y
end

local function ParseStringTime(str)
    local src_time = string.split(str, ':')
    local h, min = tonumber(src_time[1]), tonumber(src_time[2])
    local ret = {Hour = h, Minute = min, Second = 0}
    return ret
end

function CommonApi.GetTodayOpenTime(funid)
	local data = EventApi.FindFirstExcelData('functions/open_time.xlsx/open_time', {function_id = funid})
	local now = EventApi.DateTimeNow()
	if data.open_type == 0 then
		return nil
	elseif data.open_type == 1 then
		return {Hour = 0, Minute = 0, Second = 0}
	elseif data.open_type == 2 then
		local alldays = EventApi.string_split(data.open_day,',')
		local dayin = false
		for _,v in ipairs(alldays) do
			local wday = v == '7' and 0 or tonumber(v)
			if wday == now.DayOfWeek then
				dayin = true
			end
		end
		if not dayin then
			return nil
		end
		local ret = {}
		for _,v in ipairs(data.time.open) do
			if not EventApi.string_IsNullOrEmpty(v) then
				table.insert(ret, ParseStringTime(v))
			end
		end
		return ret
	elseif data.open_type == 3 then
		-- todo 
	end
	
end

function CommonApi.GetTodayCloseTime(funid)

end

function CommonApi.GetQuestData(questID)
	local qData = EventApi.FindExcelData('quest/quest.xlsx/quest', questID)
	if not qData then
		qData = EventApi.FindExcelData('quest/loop_quest.xlsx/loop_quest', questID)
	end
	if not qData then
		qData = EventApi.FindExcelData('quest/guild_quest.xlsx/guild_quest', questID)
	end
	if not qData then
		qData = EventApi.FindExcelData('quest/consignation_quest.xlsx/consignation_quest', questID)
	end
	return qData
end
function CommonApi.UpsetArray(src)
	local ret = {}
	local randomSrc = EventApi.copy_table(src)
	while #randomSrc > 0 do
		local index = math.random(1, #randomSrc)
		table.insert(ret, randomSrc[index])
		table.remove(randomSrc, index)
	end
	return ret
end

function CommonApi.GetDistance(x1, y1, x2, y2)
	local r1 = x1 - x2
	local r2 = y1 - y2
	return math.sqrt(r1 * r1 + r2 * r2)
end

function CommonApi.RectIntersect(sx1, sy1, sw, sh, dx1, dy1, dw, dh)
	local sx2 = sx1 + sw
	local dx2 = dx1 + dw
	if sx2 < dx1 then
		return false
	end
	if sx1 > dx2 then
		return false
	end
	local sy2 = sy1 + sh
	local dy2 = dy1 + dh
	if sy2 < dy1 then
		return false
	end
	if sy1 > dy2 then
		return false
	end
	return true
end

function CommonApi.RectIncludePoint(sx1, sy1, sw, sh, dx, dy)
	local sx2 = sx1 + sw
	if sx2 < dx then return false end
	if sx1 > dx then return false end
	local sy2 = sy1 + sh
	if sy2 < dy then return false end
	if sy1 > dy then return false end
	return true
end

function CommonApi.GetNextArg(arg, other)
	local ret = EventApi.FixArg and EventApi.FixArg({}) or {}
	for k, v in pairs(arg or {}) do
		--合并
		if not ret[k] then
			if type(v) == 'table' then
				ret[k] = EventApi.copy_table(v)
			else
				ret[k] = v
			end
		end
	end
	if other then
		--覆盖
		for k, v in pairs(other) do
			if type(v) == 'table' then
				ret[k] = EventApi.copy_table(v)
			else
				ret[k] = v
			end
		end
	end
	ret.Key = nil
	return ret
end

function CommonApi.DumpMemory(compare)
	local mri = require(_total_global.path .. 'event_script/MemoryReferenceInfo')
	mri.m_cConfig.m_bAllMemoryRefFileAddTime = false
	collectgarbage('collect')
	if compare then
		mri.m_cMethods.DumpMemorySnapshot('./', 'After-' .. EventApi.Address, -1)
		collectgarbage('collect')
		local before = string.format('./LuaMemRefInfo-All-[%s].txt', EventApi.Address)
		local after = string.format('./LuaMemRefInfo-All-[%s].txt', 'After-' .. EventApi.Address)
		mri.m_cMethods.DumpMemorySnapshotComparedFile('./', 'Compared-' .. EventApi.Address, -1, before, after)
	else
		mri.m_cMethods.DumpMemorySnapshot('./', EventApi.Address, -1)
	end
end



return CommonApi
--! @}
