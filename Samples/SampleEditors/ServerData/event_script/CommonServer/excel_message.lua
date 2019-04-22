-- Force-当前场景阵营玩家(<0全部) ServerGroupID :某服务器全服 Team :全队伍 Guild :全公会
function main(ele, ...)
    local PlayerApi = Api.GetPlayerApi(arg)
    local nextarg = Api.GetNextArg(arg)
    local params = Api.DynamicToArgTable(...)
    -- ele.auto_convert = 1
    if ele.auto_convert == 1 then
        local Convert_Texts = {
            {'{PlayerName}', PlayerApi.GetPlayerName},
            {'{GuildName}', PlayerApi.GetGuildName}
        }
        --预留0 给sec
        nextarg.special_index = params.len + 1
        for i, v in ipairs(Convert_Texts) do
            local txtarg = v[2] and v[2](arg.PlayerUUID) or ''
            params[params.len + 1] = txtarg
            params.len = params.len + 1
        end
    end
    local message_eid
    -- print('send_to ',ele.send_to)
    local broadapi_params
    if ele.send_to == 1 then
        local ok,server_group = Api.Task.Wait(Api.Task.GetOrLoadServerGroup(arg.PlayerUUID))
        --本服
        broadapi_params = server_group
    elseif ele.send_to == 2 then
        --本场景
        local ZoneApi = Api.GetZoneApi(arg)
        broadapi_params = ZoneApi.GetAllSessions()
    elseif ele.send_to == 3 then
        --本阵营
        local ZoneApi = Api.GetZoneApi(arg)
        local force
        if arg.Force then
            force = arg.Force
        elseif not arg.PlayerUUID then
            force = ZoneApi.GetUnitForce(arg.LauncherID)
        else
            force = ZoneApi.GetPlayerForce(arg.PlayerUUID)
        end
        broadapi_params = ZoneApi.GetAllSessions(force)
    elseif ele.send_to == 4 then
        --异阵营
        local ZoneApi = Api.GetZoneApi(arg)
        local force
        if arg.Force then
            force = arg.Force
        elseif not arg.PlayerUUID then
            force = ZoneApi.GetUnitForce(arg.LauncherID)
        else
            force = ZoneApi.GetPlayerForce(arg.PlayerUUID)
        end
        broadapi_params = ZoneApi.GetAllSessions(force, true)
    elseif ele.send_to == 5 then
        --本公会
        local players = PlayerApi.GetGuildMembers()
        if #players > 0 then
            local ok, sessions = Api.Task.Wait(Api.Task.GetOrLoadManySessions(players))
            broadapi_params = sessions
        end
    end
    local off_sec = os.time() - arg.TimeNow
    local src_sec = arg.TimeSec or ele.time
    nextarg.TimeSec = Api.GetRounding(src_sec - off_sec)
    -- pprint('nextarg, params', nextarg, params)
    if broadapi_params then
        local ClientApi = Api.CreateBroadcastApi('Client', broadapi_params)
        message_eid = ClientApi.Task.AddEventByKey('client_message.' .. ele.id, nextarg, Api.ArgTableToDynamic(params))
    else
        message_eid = Api.Task.AddEventByKey('client_message.' .. ele.id, nextarg, Api.ArgTableToDynamic(params))
    end
    if not message_eid then
        return false, 'no receiver'
    end
    return Api.Task.Wait(message_eid)
end

function clean()
    arg.TimeSec = nil
end
