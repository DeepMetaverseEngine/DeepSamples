
local FoolyooEventID
local function FoolyooEvent(ele)
    for k,EventKey in pairs(ele.event.id) do
        if EventKey and  not string.IsNullOrEmpty(EventKey) then
            Api.Task.AddEventByKey(EventKey)
        end
    end
    if not string.IsNullOrEmpty(ele.show_effect) then
        -- local ClientApi = Api.CreateBroadcastApi('Client', serverGroupID) 
        -- local ClientApi = Api.GetClientApi(uuid)
		-- local ZoneApi = Api.GetZoneApi(arg)
        -- local ClientApi = Api.CreateBroadcastApi('Client', ZoneApi.GetAllSessions()) 
        -- ClientApi.PlayEffect('/res/effect/ui/ef_ui_alchemy_success.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
        local allPlayer = Api.GetAllPlayers()
		for _, uuid in ipairs(allPlayer) do
            local ClientApi = Api.GetClientApi(uuid)
            ClientApi.PlayEffect.NoAutoWait = true
            ClientApi.PlayEffect(ele.show_effect, {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
        end
    end
    
    for k,v in ipairs(ele.last.time) do
        Api.Task.Sleep(v)
        local message = ele.message[k]
        if not string.IsNullOrEmpty(message) then
            local allPlayer = Api.GetAllPlayers()
            for _, uuid in ipairs(allPlayer) do
                Api.Task.StartEventByKey(message,Api.GetArg({PlayerUUID = uuid}))
            end
        end
    end
    Api.Task.Wait()
end

-- local function func(start_time, end_time, ekey)
--     local id = Api.Task.AddEventByKey(ekey)
--     Api.t
--     local cid = Api.Task.DelaySec()
--     Api.Task.WaitAny(cid, id)
-- end

local function FoolyooClean()


end

local function isOpenDay(ele)
    local isInDay = true
    if not string.IsNullOrEmpty(ele.start_day) and not string.IsNullOrEmpty(ele.end_day) then
        isInDay = Api.IsInAppointedDay(ele.start_day,ele.end_day)
    end
    if isInDay and not string.IsNullOrEmpty(ele.open_day) then 
        local opendays = string.split(ele.open_day,',')
        local today = os.date("%w")
        if today == '0' or today == 0 then 
            today = '7'
        end
        return table.ContainsValue(opendays,today)
    end
    return isInDay
end

function main(ele)
    assert(ele)
        
    local foolyooOpen = false
    local foolyooEvents = {}

	local brushDt = {}
	local leaveDt = {}
 
    local WorldBossOpen = false
    local BrushHour = 0
    local BrushMinute = 0
    local now = Api.DateTimeNow()
    for k,v in ipairs(ele.brush.time) do
      
        if not string.IsNullOrEmpty(v) and not string.IsNullOrEmpty(ele.leave.time[k]) then

            local brushTime = string.split(v, ':')
            assert(brushTime[1])
            assert(brushTime[2])
            local brushHour = tonumber(brushTime[1])
            local brushMinute = tonumber(brushTime[2])
            local brushSecond = 0
            table.insert(brushDt,{Hour = brushHour, Minute = brushMinute,Second = brushSecond})

            local leaveTime = string.split(ele.leave.time[k], ':')     
            local leaveHour = tonumber(leaveTime[1])
            local leaveMinute = tonumber(leaveTime[2])
            local leaveSecond = 0

            table.insert(leaveDt,{Hour = leaveHour, Minute = leaveMinute,Second = leaveSecond})

            if now.Hour * 60 + now.Minute >= brushHour * 60 + brushMinute and now.Hour * 60 +  now.Minute <= leaveHour * 60 + leaveMinute then
                foolyooOpen = true
            end

        end
    end

 
    if foolyooOpen then
        if  isOpenDay(ele) then 
            FoolyooEventID = Api.Task.AddEventTo(ID,{main = FoolyooEvent,clean = FoolyooClean},ele)    
            Api.SetSceneExpireTime(BrushHour,BrushMinute,ele.keep_time)
        end
    end

	Api.Listen.TodayTime(brushDt,function()
        if FoolyooEventID then
            Api.Task.StopEvent(FoolyooEventID)
        end

        if not isOpenDay(ele) then 
            return
        end

        FoolyooEventID = Api.Task.AddEventTo(ID,{main = FoolyooEvent,clean = FoolyooClean},ele)
        local now2 = Api.DateTimeNow()
        Api.SetSceneExpireTime(now2.Hour,now2.Minute,ele.keep_time)
	end)

	Api.Listen.TodayTime(leaveDt,function()
        if FoolyooEventID then
            Api.Task.StopEvent(FoolyooEventID)
        end
        FoolyooEventID = nil
	end)

    Api.Task.WaitAlways()
end

function clean(success)
    if FoolyooEventID then
        Api.Task.StopEvent(FoolyooEventID)
    end
    FoolyooEventID = nil
end