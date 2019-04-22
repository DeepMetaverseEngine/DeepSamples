local GuildWall_TemplateID = {502000, 502001}
function main(ele)
    assert(ele)
    local params = {
        MapTemplateID = ele.mapid,
        ZoneUUID = arg.TargetZoneUUID
    }
    if not string.IsNullOrEmpty(ele.flag) then
        params.Flag = ele.flag
    else
        params.X, params.Y = unpack(string.split(ele.coordinate, ','))
    end
    local ClientApi = Api.GetClientApi(arg)
    ClientApi.Task.StartEvent('Client/fade_screen', 2)

    if arg.MapTemplateID ~= params.MapTemplateID then
        for _, v in ipairs(GuildWall_TemplateID) do
            if v == params.MapTemplateID then
                local GuildApi = Api.GetGuildApi(Api.UUID)
                local guildUUID = Api.GetGuildUUID()
                if not params.ZoneUUID then
                    return Api.Task.Wait(GuildApi.Task.EnterWallZone(guildUUID, Api.UUID, v))
                else
                    local roomkey, force = GuildApi.GetWallZoneInfo(guildUUID, v)
                    params.Force = force
                    break
                end
            end
        end

        -- if params.MapTemplateID == 500400 then
        --     --婚礼现场
        --     local ok, isentered = Api.Task.Wait(Api.Task.EnterMarryZone(params))
        --     if not isentered then
        --         Api.Task.StartEventByKey('message.182')
        --     end
        --     return ok
        -- end
    end
    local id = Api.Task.TransportPlayer(params)
    return Api.Task.Wait(id)
end
