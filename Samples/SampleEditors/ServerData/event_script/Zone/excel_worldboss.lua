local BossID
local BossEventID 
 

local players = {}
local damageTimeLeft = 3

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

local function broadcastDamage(result,damages)
    for uuid, rank in pairs(players) do
        if rank ~= nil then
            local args = {}
            args.damages = result
            args.playerRank = rank 
            args.playerDamage = damages[uuid] or 0
                -- pprint('SendMessageToClient:',uuid,rank)
            Api.SendMessageToClient(uuid, 'worldboss.damages', args)
        end
    end     
end

local function SendDamageTickEvent()
    while BossID do
        Api.Task.Sleep(damageTimeLeft)

        local damages = Api.GetMonsterDamageInfo(BossID)

        if damages then
            local result = {}
            for k,v in pairs(damages or {}) do
                table.insert(result,{uid=k,damage=v})
            end

            local hasDamage = false
            if next(result) then
                hasDamage = true
                table.sort(result,function(a,b)
                    return a.damage > b.damage
                end)
            end

            if hasDamage then
                local rank10 = {}
                for k,v in ipairs(result) do
                    players[v.uid] = k
                    if k <= 10 then
                        table.insert(rank10,v)
                    end
                end
                broadcastDamage(result,damages)
            end
        end        
    end 
end

local function StartClient(uuid,ele)
    Api.Task.AddEventToByKey(ID,'client.excel_worldboss', Api.GetArg({PlayerUUID = uuid}),ele.monster_name)
    players[uuid] = 0
end

local function EndClient(uuid)
    if players[uuid] then
        Api.SendMessageToClient(uuid, 'worldboss.outRegion', {})
        players[uuid] = nil
    end
end

local function BossEvent(ele)
    if BossID then
        print('5555555555555555555555555 BossEvent BossID:',BossID)
        return
    end

    local info = {TemplateID = ele.monster_id, Force = 1, Direction = 1.5}
    info.X, info.Y =  Api.GetExcelPostion(ele.flag)
    BossID = Api.AddUnit(info)
  
    if not string.IsNullOrEmpty(ele.zone_flag) then
        
        Api.Task.AddEvent(SendDamageTickEvent)    
        
        Api.Listen.PlayerEnterRegion(ele.zone_flag, function(uuid)
            if BossID then
                StartClient(uuid,ele)       
            end
        end)

        Api.Listen.PlayerLeaveRegion(ele.zone_flag, function(uuid)
            EndClient(uuid)
        end)
    end 

    Api.Listen.UnitDead(BossID,function(DeadId,KillerId)
    	-- Api.Task.StopEvent(BossEventID)
        BossID = nil

        if not string.IsNullOrEmpty(ele.dead_event) then
          
            if string.starts(ele.dead_event,'message') then 
                local uuid = Api.GetPlayerUUID(KillerId)
                local nextArg = Api.GetNextArg(arg,{PlayerUUID = uuid})
                local lineIndex = Api.GetLineIndex()
                Api.Task.AddEventToByKey(BossEventID,ele.dead_event, nextArg,lineIndex)  
            else
                local nextArg = Api.GetNextArg(arg)
                Api.Task.AddEventToByKey(BossEventID,ele.dead_event, nextArg)  
            end
               
        end
    end)

    for k,v in pairs(ele.last.time) do
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

local function BossClean()

    if BossID then
        Api.RemoveObject(BossID)
        BossID = nil
    end 
   
    BossEventID = nil 

    for uuid, rank in pairs(players) do
        if rank ~= nil then
            EndClient(uuid)
        end
    end 
    players = {}
   
end

function main(ele)
    assert(ele)
    if not ele then
        local mapID = Api.GetMapTemplateID()
        --step1 取地图所有flags
        print('excel_worldboss ele is empty mapID : '.. mapID)
        return false
    end
    damageTimeLeft = Api.GetExcelConfig('damage_rankcd') or damageTimeLeft
    -- print('damageTimeLeft:',damageTimeLeft)
 
	local brushDt = {}
	local leaveDt = {}
 
    local Time2Open = false
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
            assert(leaveTime[1])
            assert(leaveTime[2])

            local leaveHour = tonumber(leaveTime[1])
            local leaveMinute = tonumber(leaveTime[2])
            local leaveSecond = 0

            table.insert(leaveDt,{Hour = leaveHour, Minute = leaveMinute,Second = leaveSecond})
 
            if now.Hour * 60 + now.Minute >= brushHour * 60 + brushMinute and now.Hour * 60 +  now.Minute <= leaveHour * 60 + leaveMinute then
                Time2Open = true
                BrushHour = brushHour
                BrushMinute = brushMinute
            end
        end
    end

    if Time2Open then
        if isOpenDay(ele) then 
            BossEventID = Api.Task.AddEventTo(ID,{main = BossEvent,clean = BossClean},ele)    
            Api.SetSceneExpireTime(BrushHour,BrushMinute,ele.keep_time)
        end
    end

    --测试代码
    -- local now2 = Api.DateTimeNow()
    -- Api.SetSceneExpireTime(now2.Hour,now2.Minute,ele.keep_time)

	Api.Listen.TodayTime(brushDt,function()

        if BossEventID then
            Api.Task.StopEvent(BossEventID)
        end

        if not isOpenDay(ele) then 
            return
        end

		BossEventID = Api.Task.AddEventTo(ID,{main = BossEvent,clean = BossClean},ele)  
        local now2 = Api.DateTimeNow()
        Api.SetSceneExpireTime(now2.Hour,now2.Minute,ele.keep_time)
	end)

	Api.Listen.TodayTime(leaveDt,function()
 		if BossEventID then
			Api.Task.StopEvent(BossEventID)
            BossEventID = nil
		end
	end)

	Api.Task.Wait()   
end

function clean(success)
    if BossEventID then
        Api.Task.StopEvent(BossEventID)
        BossEventID = nil
    end
end