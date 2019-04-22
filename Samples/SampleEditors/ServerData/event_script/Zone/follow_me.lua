function main(playerUUID, id)
    assert(playerUUID)
    assert(id)
    local ele = Api.GetExcelByEventKey('follow.'..id)
    assert(ele)
    Api.Task.WaitPlayerReady(playerUUID)
    ObjectID = Api.AddFollowUnit(playerUUID, ele.npc, ele.othercansee == 0)
    Api.Task.Sleep(1)
    local params = {
        main = 'Zone/Follow/follow_' .. math.floor(id),
        safecall = true
    }
    Api.Task.AddEvent(params, playerUUID, ObjectID)
    
    local ClientApi = Api.GetClientApi(playerUUID)
    ClientApi.Task.AddEvent('Client/follow_me', id, ObjectID)

    Api.Task.WaitAlways()
end

function clean(reason)
    Api.RemoveObject(ObjectID)
end
