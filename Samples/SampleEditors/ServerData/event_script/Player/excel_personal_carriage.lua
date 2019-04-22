function main(ele)
    Api.Task.WaitEnterMap(ele.mapid)
    if ele.last_time and ele.last_time > 0 then
        Api.Task.AddEvent(
            function()
                Api.Task.Sleep(ele.last_time)
                Api.Task.StopEvent(ID, false, 'timeout')
            end
        )
    end
    local eid, zoneUUID, params, entered_othermap
    Api.Listen.EnterMap(
        function(mapid, uuid)
            if ele.mapid ~= mapid then
                entered_othermap = true
                return
            end
            entered_othermap = nil
            if uuid ~= zoneUUID then
                if entered_othermap then
                    Api.Task.AddEventTo(
                        ID,
                        function()
                            Api.Task.Wait(Api.Task.TransportPlayer({MapTemplateID = ele.mapid, ZoneUUID = zoneUUID}))
                        end
                    )
                else
                    Api.SendMessage('Zone', zoneUUID, 'carriage_line.' .. arg.PlayerUUID)
                end
            end
        end
    )
    ::restart::
    eid = Api.Task.AddEventByKey('personal_zone_carriage.' .. ele.id, Api.GetNextArg(arg), params)
    zoneUUID = Api.GetZoneUUID()
    local ok, ret = Api.Task.Wait(eid)
    if ok and ret and ret.IsLineStop then
        params = ret
        goto restart
    end
    return ok, ret
end
