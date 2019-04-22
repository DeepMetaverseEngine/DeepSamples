function main(ele, params)
    assert(ele, arg.Key)
    local mapTemplateID = Api.GetMapTemplateID()
    assert(mapTemplateID == ele.mapid, arg.Key)
    local count = ele.count or 1
    local x, y, flag, force, direction
    if params then
        x = params.x
        y = params.y
        flag = params.flag
        force = params.force
        direction = params.direction
    end
    if not x and not y and not flag then
        x, y = Api.GetExcelPostion(ele.flag, ele.coordinate)
    elseif flag then
        x, y = Api.GetFlagPosition(flag)
    end
    x = x or arg.x
    y = y or arg.y
    force = force or ele.force
    direction = direction or tonumber(ele.direction)
    assert(x and y)
    local itemInfo = {
        TemplateID = ele.itemid,
        X = x,
        Y = y,
        Force = force,
        Direction = direction
    }

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

    local player_times = {}
    local function StartTriggerEvent(uuid)
        local pick_eid
        if hasPickEvent then
            pick_eid = Api.Task.StartEventByKey(ele.trigger_event, Api.GetNextArg(arg, {PlayerUUID = uuid}))
            picking[pick_eid] = true
        end
        if count > 0 then
            count = count - 1
            if count == 0 then
                Api.RemoveObject(itemID)
            end
            if pick_eid then
                picking[pick_eid] = true
                local ok, stoped_eid = Api.Task.WaitAny(pick_eid, Api.Task.DelaySec(15))
                if not ok and stoped_eid == pick_eid and ele.fail_cut_count == 0 then
                    count = count + 1
                    if count == 1 then
                        -- 重新启动
                        Api.Task.AddEventTo(ID, start_item_event)
                    end
                elseif count == 0 then
                    Api.Task.StopEvent(ID)
                end
                picking[pick_eid] = nil
            end
        end
    end
    local function StartZoneItemEvent()
        itemID = Api.AddItem(itemInfo)
        local CDing = false
        local function CD()
            CDing = true
            Api.Task.Sleep(1)
            CDing = false
        end
        Api.Listen.PlayerTryPickItem(
            itemID,
            function()
                return not CDing
            end
        )
        print('item added', itemInfo.TemplateID, direction, itemInfo.X, itemInfo.Y)
        Api.Task.Wait(
            Api.Listen.PlayerPickedItem(
                itemID,
                function(uuid, itemid)
                    local lasttime = player_times[uuid] or 0
                    local now = os.time()
                    if now - lasttime > 3 then
                        player_times[uuid] = now
                        Api.TriggerEvent(ID, uuid, itemid)
                        arg.PlayerUUID = uuid
                        arg.Force = Api.GetPlayerForce(uuid)
                        Api.Task.AddEventTo(ID, StartTriggerEvent, uuid, itemid)
                    else
                        -- print(uuid..' cding')
                    end
                end
            )
        )
        if not next(picking) then
            Api.Task.StopEvent(ID)
        end
    end

    start_item_event = StartZoneItemEvent
    Api.Task.AddEvent(StartZoneItemEvent)
    Api.Task.WaitAlways()
end

function clean()
    if itemID then
        Api.RemoveObject(itemID)
    end
end
