function clean()
    if NpcID then
        Api.RemoveObject(NpcID)
    end
end

function main(ele)
    assert(ele)
    assert(arg.PlayerUUID)
    assert(arg.ZoneUUID)
    assert(arg.MapTemplateID == ele.mapid)
    local x, y = Api.GetFlagPosition(ele.npcflag)
    assert(x and y and x >= 0 and y >= 0, 'not found flag ' .. Api.GetMapTemplateID())
    if ele.planes == 0 then
        Api.Task.PlayerAoiWork(arg.PlayerUUID)
    end
    local currentFlagIndex = 0
    local targetFlag
    local targetX, targetY
    local targetWays
    local function GetNextPosition(cx, cy)
        currentFlagIndex = currentFlagIndex + 1
        targetFlag = ele.point[currentFlagIndex]

        if string.IsNullOrEmpty(targetFlag) then
            Api.KillUnit(NpcID)
            NpcID = nil
            Api.Task.StopEvent(ID, true)
        else
            targetX, targetY = Api.GetFlagPosition(targetFlag)
            targetWays = Api.FindPath(cx, cy, targetX, targetY)
            Api.TrySetQuestMove(arg, ele.auto_move, Api.GetMapTemplateID(), {x = targetX, y = targetX})
        end
    end

    local unit = {
        TemplateID = ele.npcid,
        X = x,
        Y = y,
        Force = 0,
    }
    NpcID = Api.AddUnit(unit)
    assert(NpcID~=0)
    GetNextPosition(x, y)
    Api.Task.PlayerAoiWork(aoiBinder)
    print('npc added ', NpcID, x, y)

    Api.Task.WaitPlayerReady(arg.PlayerUUID)
    Api.Task.AddEventToByKey(ID,'client.stalker_effect', arg, NpcID, ele.inner, ele.center)


    local failEventID
    local isStart
    local isInPointEvent = false
    local function StartPointEvent(pointEvent)
        isInPointEvent = true
        local ok, ret = Api.Task.Wait(Api.Task.AddEventByKey(pointEvent, Api.GetNextArg(arg), NpcID))
        if not ok then
            Api.Task.StopEvent(ID, false, tostring(ret))
        end
        isInPointEvent = false
    end

    local function FailedEvent(eventKey, time)
        Api.Task.AddEventByKey(eventKey, Api.GetArg({PlayerUUID = arg.PlayerUUID}))
        Api.Task.Sleep(time)
        Api.Task.StopEvent(ID,false,'distance')
    end

    Api.Listen.AddPeriodicSec(
        1,
        function()
            if isInPointEvent then
                return
            end
            --检测玩家位置
            local px, py = Api.GetPlayerPosition(arg.PlayerUUID)
            local nx, ny = Api.GetObjectPosition(NpcID)
            local d = Api.GetDistance(px, py, nx, ny)

            if d < ele.inner then
                if failEventID or not isStart then
                    return
                end
                --触发倒计时
                failEventID = Api.Task.AddEventTo(ID, FailedEvent, ele.innerevent, ele.innerevent_time)
                return
            elseif d > ele.outer then
                if failEventID or not isStart then
                    return
                end
                failEventID = Api.Task.AddEventTo(ID, FailedEvent, ele.outerevent, ele.outerevent_time)
                return
            else
                isStart = true
                if failEventID then
                    Api.Task.StopEvent(failEventID)
                    failEventID = nil
                end
            end
                 local ok = Api.UnitMoveTo(NpcID, targetWays[1].x, targetWays[1].y)
                 if ok then
                     table.remove(targetWays, 1)
                     if #targetWays == 0 then
                         local pointEvent = ele.pointevent[currentFlagIndex]
                         if not string.IsNullOrEmpty(pointEvent) then
                             Api.Task.AddEventTo(ID, StartPointEvent, pointEvent)
                         end
                         GetNextPosition(nx, ny)
                     end
                 end

        end
    )

    Api.Listen.PlayerDead(
        arg.PlayerUUID,
        function()
            Api.Task.StopEvent(ID, false)
        end
    )

    Api.Listen.PlayerLeaveZone(
        arg.PlayerUUID,
        function()
            Api.Task.StopEvent(ID, false)
        end
    )

    Api.Task.WaitAlways()
end
