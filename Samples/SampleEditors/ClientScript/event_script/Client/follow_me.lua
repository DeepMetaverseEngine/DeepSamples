function main(id, objID)
    Api.Task.WaitActorReady()
    local params = {
        main = 'Client/Follow/follow_' .. math.floor(id),
        safecall = true
    }
    local eid = Api.Task.AddEvent(params, objID)
    Api.Task.Wait(eid)
end
