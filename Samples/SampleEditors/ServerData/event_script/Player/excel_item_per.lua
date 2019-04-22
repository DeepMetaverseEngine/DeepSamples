function main(ele)
    assert(ele)
    if ele.lefttime and ele.lefttime > 0 then
        Api.Task.AddEvent(
            function()
                Api.Task.Sleep(ele.lefttime / 1000)
                Api.Task.StopEvent(ID, false, 'timeout')
            end
        )
    end

    local eid, start_item_event
    local picking = {}
    local count = ele.count or 1
    local hasPickEvent = not string.IsNullOrEmpty(ele.trigger_event)
    if count == 0 then
        -- 代表无限
        count = -1
    end

    local function StartTriggerEvent(uuid)
        local pick_eid
        if hasPickEvent then
            pick_eid = Api.Task.AddEventByKey(ele.trigger_event, Api.GetNextArg(arg, {PlayerUUID = uuid}))
        end
        if count > 0 then
            count = count - 1
            if count == 0 then
                Api.Task.StopEvent(eid)
            end
            if pick_eid then
                local needStop = false
                picking[pick_eid] = true
                local ok, stoped_eid = Api.Task.WaitAny(pick_eid, Api.Task.DelaySec(15))
                if not ok and stoped_eid == pick_eid and ele.fail_cut_count == 0 then
                    count = count + 1
                    if count == 1 then
                        -- 重新启动
                        Api.Task.AddEventTo(ID, start_item_event)
                    end
                elseif count == 0 then
                    needStop = true
                end
                picking[pick_eid] = nil
                if needStop then
                    local ok, ret = Api.Task.Wait(pick_eid)
                    Api.Task.StopEvent(ID, ok, ret)
                end
            end
        elseif pick_eid then
            Api.Task.Wait(pick_eid)
        end
    end

    local function StartZoneItemEvent()
        Api.Task.WaitEnterMap(ele.mapid)
        local ZoneApi = Api.GetZoneApi(Api.GetZoneUUID())
        eid = ZoneApi.Task.AddEvent('Zone/gen_player_item', arg.PlayerUUID, ele.id)
        Api.Listen.ListenEvent(
            eid,
            function(...)
                Api.TriggerEvent(ID, ...)
                Api.Task.AddEventTo(ID, StartTriggerEvent, ...)
            end
        )
        local ok, ret = Api.Task.Wait(eid)
        if not ok and ret == 'Dispose' or ret == 'ReStart_ZoneEvent' then
            StartZoneItemEvent()
        elseif not next(picking) then
            Api.Task.StopEvent(ID, ok, ret)
        end
    end
    start_item_event = StartZoneItemEvent
    Api.Task.AddEvent(StartZoneItemEvent)
    Api.Task.WaitAlways()
end
