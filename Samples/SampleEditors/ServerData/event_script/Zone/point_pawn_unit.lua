function main(ele, playerUUID, mirrorData)
    assert(ele)
    if type(ele) == 'number' then
        if string.IsNullOrEmpty(playerUUID) then
            ele = Api.GetExcelByEventKey('monster_pub.' .. ele)
        else
            ele = Api.GetExcelByEventKey('monster_per.' .. ele)
            local ok = Api.Task.WaitPlayerReady(playerUUID, 20)
            if not ok then
                return false, 'player not ready'
            end
            PlayerObjectID = Api.GetPlayerObjectID(playerUUID)
        end
    end
    assert(ele)
    local mapTemplateID = Api.GetMapTemplateID()
    assert(mapTemplateID == ele.mapid)

    local x, y = Api.GetExcelPostion(ele.flag, ele.coordinate)

    local function SpawnUnitDead(uid, templateID, attackID, attackUUID)
        if not ele.dead_event or type(ele.dead_event) ~= 'table' then
            return
        end
        for i, v in ipairs(ele.monsterid) do
            if v == templateID and not string.IsNullOrEmpty(ele.dead_event[i]) then
                local x,y = Api.GetObjectPosition(uid)
                local p = {
                    LauncherID = attackID,
                    PlayerUUID = attackUUID,
                    Flag = ele.flag,
                    TemplateID = templateID,
                    UnitName = Api.GetUnitName(uid),
                    x = x, 
                    y = y
                }
                local nextarg = Api.GetArg(p)
                Api.Task.StartEventByKey(ele.dead_event[i], nextarg)
            end
        end
    end

    local params = {
        AOIBinder = playerUUID,
        X = x,
        Y = y,
        WithoutAlive = false,
        Force = 1,
        TotalLimit = ele.TotalLimit,
        OnceCount = ele.OnceCount,
        AliveLimit = ele.AliveLimit,
        IntervalMS = ele.IntervalMS,
        RadiusSize = ele.RadiusSize,
        ResetOnWithoutAlive = ele.ResetOnWithoutAlive == 1,
        StartTimeDelayMS = ele.StartTimeDelayMS or 0,
        ObjectTemplates = {},
        DeadCallBack = SpawnUnitDead
    }

    for i, v in ipairs(ele.monsterid) do
        if v ~= 0 then
            table.insert(params.ObjectTemplates, {TemplateID = v, Direction = tonumber(ele.Direction), MirrorData = mirrorData})
        end
    end

    if PlayerObjectID then
        Api.Listen.ObjectRemove(
            PlayerObjectID,
            function()
                Api.Task.StopEvent(ID, false, 'ReStart_ZoneEvent')
            end
        )
        if ele.enter_range and ele.enter_range > 0 then
            -- local inround = Api.UnitInRound(PlayerObjectID, params.X, params.Y, ele.enter_range)
            Api.Task.Wait(Api.Listen.UnitEnterRound(params.X, params.Y, ele.enter_range, PlayerObjectID))
        end
        Api.Listen.UnitDead(
            PlayerObjectID,
            function()
                Api.Task.StopEvent(ID, false, 'PlayerDead')
            end
        )
        if ele.planes == 0 then
            Api.Task.PlayerAoiWork(playerUUID)
        end
    end
    --pprint('UnitPositionSpawnparams',params)
    local id = Api.Task.UnitPositionSpawn(params)
    --print('UnitPositionSpawn', params.X, params.Y)
    if ele.out_range and ele.out_range > 0 then
        local outtask = Api.Listen.UnitLeaveRound(params.X, params.Y, ele.out_range, PlayerObjectID)
        local ok, successid, ret = Api.Task.WaitSelect(id, outtask)
        if not ok then
            return ok, ret
        end
        if successid == id then
            return true
        else
            return false, 'out range'
        end
    else
        return Api.Task.Wait(id)
    end
end
