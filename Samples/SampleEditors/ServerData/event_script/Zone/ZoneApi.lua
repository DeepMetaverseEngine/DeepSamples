local Api = {
	Task = {},
	Listen = {}
}

function Api.GetExcelPostion(flag, coordinate)
	local x, y
	if not EventApi.string_IsNullOrEmpty(flag) then
		x, y = EventApi.GetFlagPosition(flag)
	else
		x, y = unpack(EventApi.string_split(coordinate, ','))
	end
	x = x or -1
	y = y or -1
	x, y = tonumber(x), tonumber(y)
	if x >= 0 and y >= 0 then
		return x, y
	end
end

function Api.Task.WaitPlayerReady(uuid, timeout)
	if not EventApi.IsPlayerReady(uuid) then
		if timeout then
			local readyid = EventApi.Listen.PlayerReady(uuid)
			local tid = EventApi.Task.DelaySec(timeout)
			local ok,eid = EventApi.Task.WaitAny(readyid,tid)
			return eid == readyid
		else
			return EventApi.Task.Wait(EventApi.Listen.PlayerReady(uuid))
		end
	else
		return true
	end
end


function Api.Task.WaitAnyPlayerReady(uuids, timeout)
	for i, v in ipairs(uuids) do
		if  EventApi.IsPlayerReady(v) then
			return true
		end
	end
	local readyids = {}
	for i, v in ipairs(uuids) do
		table.insert(readyids, EventApi.Listen.PlayerReady(v))
	end
	if timeout then
		local tid = EventApi.Task.DelaySec(timeout)
		table.insert(readyids,tid)
		local ok,eid = EventApi.Task.WaitAny(readyids)
		return eid ~= tid
	else
		return EventApi.Task.WaitAny(readyids)
	end
	
end
return Api
