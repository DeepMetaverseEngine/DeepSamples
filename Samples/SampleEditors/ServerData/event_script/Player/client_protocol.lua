local Protocol = {Notify = {}, Request = {}}
local Error_500 = {s2c_code = 500}
local function Error_Lang(key, ...)
    return {s2c_msg = key, msg_params = Api.DynamicToArgTable(...)}
end

local function error_print(err)
    warn(err)
end
local function TryRequestEvent(name, params)
    local fn = Protocol.Request[name]
    local ok, ret = xpcall(fn, error_print, params)
    if not ok then
        ret = {s2c_code = 500}
    end
    -- pprint(name, params, ret)
    Api.SendMessage('Client', Session, 'Protocol.' .. 'Response.' .. name .. '.' .. params.__protocol_index__, ret or {})
end

local function TryNotifyEvent(name, params)
    local fn = Protocol.Notify[name]
    fn(params)
end

local function CheckProtocol(ename, params)
    local info = string.split(ename, '.')
    if info[1] ~= 'Protocol' then
        return
    end
    -- print('client request', ename)
    if info[2] == 'Request' then
        Api.Task.AddEventTo(ID, TryRequestEvent, info[3], params)
    else
        Api.Task.AddEventTo(ID, TryNotifyEvent, info[3], params)
    end
end

function main()
    ClientApi = Api.GetClientApi(Api.UUID)
    Session = Api.GetSession(Api.UUID)
    Api.Listen.Message(CheckProtocol)
    Api.Task.WaitAlways()

end
local PvpEnterFlagKey = {
    pvp4 = 'EnterPvp4Count',
    pvp10 = 'EnterPvp10Count',
    pvp_duoqi = 'Enterpvp_duoqiCount',
    pvp_qinglong = 'Enterpvp_qinglongCount',
    pvp_longshen = 'Enterpvp_longshenCount',
}
local PvpRewardFlagKey = {
    pvp4 = 'pvp4_rewardOk',
    pvp10 = 'Pvp10_rewardOk',
    pvp_duoqi = 'pvp_duoqi_rewardOk',
    pvp_qinglong = 'pvp_qinglong_rewardOk',
    pvp_longshen = 'pvp_longshen_rewardOk',
}
--1 可领取 2 已领取 0 次数不足
function Protocol.Request.PvpInfo()
    local allreward = Api.FindExcelData('pvp/pvp.xlsx/pvp_reward', {})
    local ret = {
        TodayExploit = Api.GetIntFlag('TodayExploit', true),
        reward = {},
        Count = {}
    }
    for _, v in ipairs(allreward) do
        local today_count = Api.GetIntFlag(PvpEnterFlagKey[v.function_id], true)
        ret.Count[v.function_id] = today_count
        local get_key = PvpRewardFlagKey[v.function_id] .. v.partake_num
        local get_state = Api.GetIntFlag(get_key, true)
        ret.reward[v.function_id] = ret.reward[v.function_id] or {}
        if get_state == 0 then
            if today_count >= v.partake_num then
                ret.reward[v.function_id][v.partake_num] = 1
            else
                ret.reward[v.function_id][v.partake_num] = 0
            end
        else
            ret.reward[v.function_id][v.partake_num] = 2
        end
    end
    -- 模拟数据
    -- ret.Count  = {pvp4 = 2, pvp10 = 1}
    -- ret.reward.pvp4[1] = 2
    -- ret.reward.pvp4[3] = 1
    return ret
end

function Protocol.Request.GetPvpReward(params)
    local function_id = params.function_id
    local count = params.count
    local today_count = Api.GetIntFlag(PvpEnterFlagKey[function_id], true)
    local ele = Api.FindFirstExcelData('pvp/pvp.xlsx/pvp_reward', {function_id = function_id, partake_num = count})
    if not ele then
        return Error_500
    end
    if today_count >= count then
        local get_key = PvpRewardFlagKey[function_id] .. math.floor(count)
        local already_get = Api.GetIntFlag(get_key, true) == 1
        if not already_get then
            local infos = {}
            for i, v in ipairs(ele.item.id) do
                if v ~= 0 then
                    table.insert(infos, {TemplateID = v, Count = ele.item.num[i]})
                end
            end
            local ok = Api.Task.Wait(Api.Task.AddMoreItem(infos, {sourceType = 'battlefield_gift_get', ext = {count_type = count}}))
            if ok then
                Api.SetIntFlag(get_key, 1, true)
                Api.CheckPvpRewardRedTips()
            end
        else
            return Error_500
        end
    else
        return Error_Lang('pvp_warn_count')
    end
end

local exclude_list = {
    --['module_open/module_open.xlsx/module_open'] = true,

    --['item/item.xlsx/item'] = true,
    --['equip/equip.xlsx/equip'] = true,
    --['quest/loop_quest.xlsx/loop_quest'] = true,
    --['quest/quest.xlsx/quest'] = true,
    ['reward/common_reward.xlsx/common_reward'] = true,
    ['item/item_consumption.xlsx/item_consumption_reward'] = true,
    ['quest/quest_reward.xlsx/quest_reward.lua'] = true,
    ['team/team_target.xlsx/team_target'] = true
}

local exclude_match = {
    'response-code.lua',
    'lang.properties'
}

function Protocol.Request.SyncExcelData(params)
    local path = params.path
    local version = params.version
    if params.paths then
        pprint('sync excel data:', params.paths)
        local ret = {}
        for _, v in ipairs(params.paths) do
            if not exclude_list[v] then
                local exlude_match
                for _, match in ipairs(exclude_match) do
                    if string.find(v, match) then
                        exlude_match = true
                    end
                end
                if not exlude_match then
                    -- local full = Api.GetXlsFullData(v)
                    local full = Api.GetXlsFileBytes(v)
                    local version = Api.FindExcelData('_luaversion_', '/' .. v .. '.lua')
                    if full then
                        -- print('set exceldata,',v,version)
                        ClientApi.SetExcelData(v, full, version)
                    end
                end
            end
        end
        return ret
    elseif version then
        local server_version
        if string.starts(path, '__virtual/') then
            server_version = Api.FindExcelData(path, '__version')
        elseif path == '_luaversion_' then
            server_version = Api.FindExcelData('_luaversion_', 'version')
        else
            server_version = Api.FindExcelData('_luaversion_', '/' .. path .. '.lua')
        end
        if server_version and server_version ~= version and not exclude_list[path] then
            print('version ', path, version, server_version)
            return {data = Api.GetXlsFullData(path), version = server_version}
        end
    end
end

local CarriageMapID = 501000
local MembersLimit = 2

function Protocol.Request.EnterGuildCarriageZone()
    local guild_uuid = Api.GetGuildUUID()
    if not guild_uuid then
        return Error_Lang('guild_noguild')
    end
    local GuildApi = Api.GetGuildApi(Api.UUID)
    -- 是否在进入时间段
    if not Api.IsFuncOpenTime('guildcarriage_in') then
        return Error_Lang('guild_carriage_unopen')
    end
    local ok, reason, param = Api.Task.Wait(GuildApi.Task.EnterCarriageZone(guild_uuid, Api.UUID))
    -- print('enter guild carriage', ok, reason)
    if ok then
        if reason == 'CountLimit' then
            return Error_Lang('guild_carriage_full')
        end
        if reason == 'KickLimit' then
            return Error_Lang('guild_carriage_kick', param)
        end
    else
        return Error_500
    end
end

local Guild_Wall_TemplateID = {502000, 502001}

function Protocol.Request.EnterGuildWallZone(params)
    local true_map = false
    for _, v in ipairs(Guild_Wall_TemplateID) do
        if v == params.mapid then
            true_map = true
            break
        end
    end
    if not true_map then
        -- 不正确的mapID
        return Error_500
    end
    local guild_uuid = Api.GetGuildUUID()
    if not guild_uuid then
        return Error_Lang('guild_noguild')
    end
    local GuildApi = Api.GetGuildApi(Api.UUID)
    -- 是否在进入时间段
    if not Api.IsFuncOpenTime('guildfort_in') then
        --return Error_Lang('guild_fort_unopen')
    end
    local ok, reason, param = Api.Task.Wait(GuildApi.Task.EnterWallZone(guild_uuid, Api.UUID, params.mapid))
    -- print('enter guild carriage', ok, reason)
    if ok then
        if reason == 'CountLimit' then
            return Error_Lang('guild_fort_full')
        end
    else
        return Error_500
    end
end

function Protocol.Request.EnterGuildWallPlayerCount()
    local GuildApi = Api.GetGuildApi(Api.UUID)
    local guild_uuid = Api.GetGuildUUID()
    if not guild_uuid then
        return {firstCount = 0, secondCount = 0}
    end
    local info = GuildApi.GetPlayerWallZoneInfo(guild_uuid,Api.UUID)
    if not info then
        return {firstCount = 0, secondCount = 0}
    end
    local ret = {firstCount = 0, secondCount = 0}
    local index = guild_uuid == info.uuid[1] and 1 or 2
    for mapid, v in pairs(info.map) do
        local ZoneApi = Api.GetZoneApi(v)
        if mapid == Guild_Wall_TemplateID[1] then
            ret.firstCount = ZoneApi.GetAllPlayersCount(info.force[index])
        else
            ret.secondCount = ZoneApi.GetAllPlayersCount(info.force[index])
        end
    end
    return ret
end

function Protocol.Request.ExitFirstMap()
    local templateID = Api.GetMapTemplateID()
    local firstMap = Api.GetExcelConfig('scene_defaultbirth')
    if firstMap ~= templateID then
        return
    end
    local nextMap = Api.GetExcelConfig('scene_defaultbirth_next')
    local info = {
        MapTemplateID = nextMap,
        Flag = 'chuangjue'
    }
    Api.Task.Wait(Api.Task.TransportPlayer(info))
end


function Protocol.Request.StartWeddingAnime()
    local ok, canStart = Api.Task.Wait(Api.Task.StartWeddingAnime())
    -- print('aaaaaaaaaaaaaaaaaaaaaa', canStart)
    if canStart == 0 then
        local ZoneApi = Api.GetZoneApi(Api.GetZoneUUID())
        local broadapi_params = ZoneApi.GetAllSessions()
        local ClientApi = Api.CreateBroadcastApi('Client', broadapi_params)
        ClientApi.Task.StartEventByKey('client_drama.quest/dungen500400_1', Api.GetNextArg(arg))
    elseif canStart == 1 then
        Api.Task.StartEventByKey('message.194')
    elseif canStart == 2 then
        Api.Task.StartEventByKey('message.195')
    end
end

