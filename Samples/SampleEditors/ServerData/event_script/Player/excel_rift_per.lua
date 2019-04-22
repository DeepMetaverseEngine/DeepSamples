function main(ele)
    assert(ele)
    local map_data = Api.FindExcelData('map/map_data.xlsx/map_data', ele.riftid)
    local params = {
        MapTemplateID = ele.riftid,
        MapName = map_data.name
    }
    local canEnter, reason = Api.CanEnterMap(params.MapTemplateID)
    if not canEnter then
        local ClientApi = Api.GetClientApi(arg)
        ClientApi.ShowMessage(reason)
        return false, reason
    end
    if ele.team == 1 then
        if Api.HasTeam() and not Api.IsTeamLeader() then
            Api.Task.StartEventByKey('message.13', Api.GetArg())
            return false
        end
        local players = Api.GetTeamMembers()
        if players and #players > 1 then
            params.Players = players
        end
    end
    local ok, reason = Api.Task.Wait(Api.Task.TransportPlayer(params))
    if not ok then
        return false, reason
    end
    if Api.GetMapTemplateID() ~= ele.riftid then
        Api.Task.Wait(Api.Listen.EnterMap(ele.riftid))
    end
    print('enter rift map', ele.riftid)
    arg.ZoneUUID = Api.GetZoneUUID()
    return Api.Task.Wait(Api.Task.AddEventByKey('zone_rift_per.' .. ele.id, Api.GetNextArg(arg)))
end
