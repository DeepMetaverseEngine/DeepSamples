function main(ele)
    Api.Task.WaitEnterMap(ele.mapid)
    local ClientApi = Api.GetClientApi(arg)
    ClientApi.UI.Task.CarriageBackHud()
    local eid, zoneUUID, params
    Api.Listen.EnterMap(
        ele.mapid,
        function(mapid, uuid)
            if uuid ~= zoneUUID then
                Api.SendMessage('Zone', zoneUUID, 'carriage_line.' .. arg.PlayerUUID)
            end
        end
    )
    ::restart::
    eid = Api.Task.AddEventByKey('zone_protect_carriage.' .. ele.id, Api.GetNextArg(arg), params)
    zoneUUID = Api.GetZoneUUID()
    local ok, ret = Api.Task.Wait(eid)
    if ok and ret and ret.IsLineStop then
        params = ret
        goto restart
    end
    return ok, ret
end
