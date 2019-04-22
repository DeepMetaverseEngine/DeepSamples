function main()
    local mapID = Api.GetMapTemplateID()
    local map_data = Api.FindExcelData('map/map_data.xlsx/map_data', mapID)
    if Api.GetZoneRoomKey then
        local rk = Api.GetZoneRoomKey()
        if rk and string.starts(rk, 'Team') then
            map_data.team_sameforce = 0
        end
    end
    -- print('map_data.team_sameforce', map_data.team_sameforce)
    if map_data.team_sameforce == 1 then
        Api.Task.AutoForceTeam()
    end
    if type(map_data.init_event) == 'table' then
        for _, v in ipairs(map_data.init_event) do
            if not string.IsNullOrEmpty(v) then
                Api.Task.StartEventByKey(v)
            end
        end
    elseif not string.IsNullOrEmpty(map_data.init_event) then
        Api.Task.StartEventByKey(map_data.init_event)
    end
    Api.Listen.PlayerEnterZone(
        function(uuid)
            local session = Api.GetSession(uuid)
            Api.SaveSession(uuid, session)
        end
    )
    Api.Task.WaitAlways()
end
