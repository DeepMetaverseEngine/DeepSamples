function main(ele,arg,scriptName)
    assert(ele)
    arg.ZoneUUID = Api.GetZoneUUID()
    ZoneApi = Api.GetZoneApi(arg)
    ZoneApi.Task.PlayerAoiWork(arg.PlayerUUID)

    local client_id = Api.Task.AddEventByKey(scriptName, Api.GetNextArg(arg), ele.id)
    Api.Listen.SessionReconnect(function()
        Api.Task.StopEvent(ID, false)
    end)
    return Api.Task.Wait(client_id)
end
