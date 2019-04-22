local player_map = {}
local player_enter_events = {}
local guild_map
local start_time_sec
local main_running = false
local close_time_sec
local GuildApi
local RedForce = 2
local BlueForce = 3
local ForceConfig = {
    [RedForce] = {
        CarStart = 'left_carriageborn',
        CarTemplateID = 21201,
        PathPrefix = 'left_',
        AirFlag = 'airwall1'
    },
    [BlueForce] = {
        CarStart = 'right_carriageborn',
        CarTemplateID = 21202,
        PathPrefix = 'right_',
        AirFlag = 'airwall2'
    }
}

local AirMessageEvent = 'message.65'
local CarDeadMessageEvent1 = 'message.71'
local CarDeadMessageEvent2 = 'message.99'
local CarDamageMessageEvent = 'message.67'
local CarEndFlag = 'carriagefinish_1'
local OpenAirWallSec = 60
local HelpEachScore = 100
local KillPlayerScore = 100
local KillCarScore = 300
local win_guild_uuid
local notify_tick_event
local kick_players = {}
local car_damage_message_time = {}

local SINGLE_DEBUG = false

if SINGLE_DEBUG then
    OpenAirWallSec = 10
end
local function CalcDistance(info)
    local x, y = Api.GetObjectPosition(info.car)
    local index = info.ppath or 1
    local paths = ForceConfig[info.force].Paths
    local distance = 0
    for i = index, #paths do
        distance = distance + Api.GetDistance(x, y, paths[i].x, paths[i].y)
        x, y = paths[i].x, paths[i].y
    end
    info.distance = distance
end

local function OnGameOver(reason)
    local members = {}
    for uuid, v in pairs(guild_map) do
        Api.Task.StopEvent(v.car_event)
        if v.distance ~= 0 then
            CalcDistance(v)
        end
        if not win_guild_uuid then
            win_guild_uuid = uuid
        elseif guild_map[win_guild_uuid].distance > v.distance then
            win_guild_uuid = uuid
        end
        for _, vv in ipairs(v.members) do
            table.insert(members, vv)
        end
    end
    table.sort(
        members,
        function(m1, m2)
            return m1.help_score + m1.kill_score > m2.help_score + m2.kill_score
        end
    )

    local function CalcRankReward(uuid, index)
        local info = Api.FindFirstExcelData('guild/guild_carriage.xlsx/rank_reward', index)
        if info then
            local PlayerApi = Api.GetPlayerApi(uuid)
            PlayerApi.CommonReward(info.reward, nil, 'guild_carriage_rank')
        end
    end
    -- 排名奖励
    for i, v in ipairs(members) do
        Api.Task.AddEvent(CalcRankReward, v.uuid, i)
    end
    -- pprint('win_guild', win_guild_uuid, guild_map)
    main_running = false
    local t = os.time()
    GuildApi.SetCarriageGameOver(table.keys(guild_map), win_guild_uuid)

    -- 配置里面countdown_time_sec配正确，这里可以去掉，配0这里为20，目前是10
    Api.Task.Sleep(10)
    Api.SetGameOver(guild_map[win_guild_uuid].force, reason)
end

local function AppendCarAttribute(info, guildUUID)
    if SINGLE_DEBUG then
        return 
    end
    local stablelv = GuildApi.GetBuildLevel(guildUUID, 'Stable')
    local destroyCount = GuildApi.GetDestroyCount(guildUUID, 1)
    local maxhp = Api.GetUnitMaxHp(info.car)
    local speed = Api.GetUnitMoveSpeed(info.car)
    local stableInfo = Api.FindFirstExcelData('guild/guild.xlsx/guild_stable', {level = stablelv})
    if destroyCount > 0 then
        local destroyInfo = Api.FindFirstExcelData('guild/guild_destroy.xlsx/guild_destroy_debuff', 1)
        destroyCount = math.min(destroyCount, destroyInfo.destroy_limit)
        stableInfo.add_maxhp = stableInfo.add_maxhp - destroyCount * destroyInfo.carriage_hpdown
        stableInfo.add_runspeed = stableInfo.add_runspeed - destroyCount * destroyInfo.carriage_speeddown
        stableInfo.destroy_buff_id = destroyInfo.buff_id
    end
    if stableInfo.buff_id ~= 0 then
        Api.UnitAddBuff(info.car, stableInfo.buff_id)
    end
    if stableInfo.destroy_buff_id ~= 0 then
        Api.UnitAddBuff(info.car, stableInfo.destroy_buff_id)
    end
    maxhp = maxhp + math.floor(maxhp * (stableInfo.add_maxhp / 10000))
    speed = speed + speed * (stableInfo.add_runspeed / 10000)
    Api.SetUnitMaxHp(info.car, maxhp)
    Api.SetUnitMoveSpeed(info.car, speed)
end

local function CarLogic(info, guildUUID)
    AppendCarAttribute(info, guildUUID)
    Api.Listen.UnitDamage(
        info.car,
        function(id, attackid, hp)
            local t = car_damage_message_time[guildUUID]
            local now = os.time()
            if not t or now - t > 10 then
                car_damage_message_time[guildUUID] = now
                Api.Task.AddEventToByKey(info.car_event, CarDamageMessageEvent, Api.GetNextArg(arg, {LauncherID = info.car}))
            end
        end
    )

    local paths = ForceConfig[info.force].Paths
    info.ppath = 1
    local pos = paths[info.ppath]
    local endrunning = false
    Api.Listen.UnitDead(
        info.car,
        function(carid, attachid)
            local attackuuid = Api.GetPlayerUUID(attachid)
            if not attackuuid then
                return
            end
            player_map[attackuuid].kill_score = player_map[attackuuid].kill_score + KillCarScore
            Api.Task.AddEventToByKey(info.car_event, CarDeadMessageEvent1, Api.GetNextArg(arg, {LauncherID = info.car, PlayerUUID = attackuuid}))
            Api.Task.AddEventToByKey(info.car_event, CarDeadMessageEvent2, Api.GetNextArg(arg, {LauncherID = info.car, PlayerUUID = attackuuid}))
        end
    )
    local function CheckPostion()
        if not main_running then
            return
        end
        local nx, ny = Api.GetObjectPosition(info.car)
        local ok = Api.UnitMoveTo(info.car, pos.x, pos.y)
        if ok then
            if endrunning then
                info.distance = 0
                Api.Task.AddEventTo(ID, OnGameOver, 'win')
            else
                info.ppath = info.ppath + 1
                if #paths < info.ppath then
                    pos.x, pos.y = Api.GetFlagPosition(CarEndFlag)
                    endrunning = true
                else
                    pos = paths[info.ppath]
                end
            end
        end
    end
    Api.Listen.AddPeriodicSec(0.5, CheckPostion)
    Api.Task.WaitAlways()
end

local function GetClientSyncInfo(neeed_static)
    local info = {}
    for k, v in pairs(guild_map) do
        if v.car then
            CalcDistance(v)
        end
        info[k] = {members = v.members, distance = v.distance and math.floor(v.distance)}
        if neeed_static then
            info[k].name = v.name
        end
    end
    return info
end

local function NotifyBattleInfo()
    if not guild_map then
        return
    end
    local info = GetClientSyncInfo(false)
    if win_guild_uuid then
        info.win_guild = win_guild_uuid
        Api.Task.StopEvent(notify_tick_event)
    end
    info.countdown_sec = math.floor(close_time_sec - os.time())
    for uuid, v in pairs(player_map) do
        Api.SendMessageToClient(uuid, 'guild_carriage.battleinfo', info)
    end

    if main_running and not Api.IsFuncOpenTime('guildcarriage_in') then
        --活动结束
        Api.Task.AddEventTo(ID, OnGameOver, 'timeout')
    end
end

local function DoClientStart(uuid, parentid)
    local client_syncinfo = GetClientSyncInfo(true)
    parentid = parentid or player_enter_events[uuid]
    Api.Task.StartEventByKey('client.guild_carriage', Api.GetNextArg(arg, {PlayerUUID = uuid}), client_syncinfo)
    local off_time = os.time() - start_time_sec
    if off_time < OpenAirWallSec then
        local countdown_sec = math.floor(OpenAirWallSec - off_time)
        Api.Task.AddEventToByKey(parentid, AirMessageEvent, Api.GetNextArg(arg, {PlayerUUID = uuid, TimeSec = countdown_sec}))
    end
end
local function OnPlayerOnEnter(uuid, guildUUID)
    if not guild_map then
        guild_map = {}
        -- 获取当前公会信息和敌对公会信息
        GuildApi = Api.GetGuildApi(uuid)
        local cforce = Api.GetPlayerForce(uuid)
        --生成镖车
        guild_map[guildUUID] = {name = Api.GetPlayerGuildName(uuid), force = cforce, members = {}, guild_uuid = guildUUID}

        local enemyUUID, enemyName
        if not SINGLE_DEBUG then
            enemyUUID, enemyName = GuildApi.GetCarriageEnemy(guildUUID)
        else
            enemyUUID, enemyName = 'kfjowej432j4','测试公会'
        end
        guild_map[enemyUUID] = {name = enemyName, members = {}, guild_uuid = enemyUUID, force = cforce == RedForce and BlueForce or RedForce}
    end
    while not guild_map[guildUUID] do
        Api.Task.Sleep(1)
    end
    table.insert(guild_map[guildUUID].members, player_map[uuid])
    DoClientStart(uuid, Api.GetCurrentEventID())
    Api.Task.WaitAlways()
end

local function OnPlayerEnterZone(uuid)
    kick_players[uuid] = nil
    local guildUUID = Api.GetPlayerGuildUUID(uuid)
    player_map[uuid] = {kill_score = 0, help_score = 0, guild_uuid = guildUUID, uuid = uuid}
    -- pprint('player --',player_map[uuid])
    player_enter_events[uuid] = Api.Task.AddEventTo(ID, OnPlayerOnEnter, uuid, guildUUID)
end

local function OnPlayerLeaveZone(uuid)
    local guildUUID = player_map[uuid].guild_uuid
    Api.Task.AddEventTo(
        ID,
        function()
            if not win_guild_uuid then
                GuildApi.NotifyLeaveCarriageZone(guildUUID, uuid, kick_players[uuid])
            end
        end
    )
    local members = guild_map[guildUUID].members
    for i, v in ipairs(members) do
        if v.uuid == uuid then
            table.remove(members, i)
            break
        end
    end
    player_map[uuid] = nil
    Api.Task.StopEvent(player_enter_events[uuid])
end

local function OnPlayerPickedItem(uuid)
    player_map[uuid].help_score = player_map[uuid].help_score + HelpEachScore
end

local function OnPlayerKillOtherPlayer(uuid, deaduuid)
    if uuid then
        player_map[uuid].kill_score = player_map[uuid].kill_score + KillPlayerScore
    end
end

local function ParseStringTime(str)
    local src_time = string.split(str, ':')
    local h, min = tonumber(src_time[1]), tonumber(src_time[2])
    local now = os.date('*t')
    local ret = {hour = h, min = min, year = now.year, month = now.month, day = now.day}
    return os.time(ret)
end

local function CaclOffsetTimeSec(from, t)
    local t2 = os.time(t)
    os.difftime(from, t2)
end

function main()
    start_time_sec = os.time()
    -- 获取活动时间
    local data = Api.FindFirstExcelData('functions/open_time.xlsx/open_time', {function_id = 'guildcarriage_in'})
    for _, v in ipairs(data.time.close) do
        if not string.IsNullOrEmpty(v) then
            local t = ParseStringTime(v)
            if start_time_sec < t then
                if not close_time_sec or t < close_time_sec then
                    close_time_sec = t
                end
            end
        end
    end
    local next_ctime = os.date(close_time_sec)
    -- Api.SetSceneExpireTime(next_ctime.hour, next_ctime.min)
    -- pprint('ForceConfig', ForceConfig)
    -- print('close_time ',close_time_sec)
    -- 路线计算
    for force, v in pairs(ForceConfig) do
        local index = 1
        local paths = {}
        while true do
            local flag = v.PathPrefix .. index
            local ok, x, y = Api.TryGetFlagPosition(flag)
            if not ok then
                break
            end
            table.insert(paths, {x = x, y = y})
            index = index + 1
        end
        v.Paths = paths
        Api.OpenFlag(v.AirFlag)
    end

    local allplayers = Api.GetAllPlayers()
    for _, v in ipairs(allplayers or {}) do
        Api.SetPlayerPositionToStart(v)
        OnPlayerEnterZone(v)
    end

    Api.Listen.PlayerEnterZone(OnPlayerEnterZone)
    Api.Listen.PlayerLeaveZone(OnPlayerLeaveZone)
    Api.Listen.SessionReconnect(DoClientStart)

    notify_tick_event = Api.Listen.AddPeriodicSec(1, NotifyBattleInfo)
    local function OnKickMember(fromuuid, uuid)
        local PlayerApi = Api.GetPlayerApi(fromuuid)
        local postion = PlayerApi.GetGuildPosition()
        local postion_config = Api.FindFirstExcelData('guild/guild.xlsx/guild_position', {position_id = postion})
        if postion_config.allow_kick == 1 then
            local KickPlayerApi = Api.GetPlayerApi(uuid)
            kick_players[uuid] = true
            KickPlayerApi.LeaveCurrentZone()
        else
            local ClientApi = Api.GetClientApi(fromuuid)
            ClientApi.ShowMessage('guild_carriage_kicklimit')
        end
    end
    Api.Listen.Message(
        'guild_carriage.kick_member',
        function(ename, uuid, from, fromuuid)
            Api.Task.AddEventTo(ID, OnKickMember, fromuuid, uuid)
        end
    )
    Api.Task.Sleep(OpenAirWallSec)
    for force, v in pairs(ForceConfig) do
        Api.CloseFlag(v.AirFlag)
    end
    main_running = true
    for k, v in pairs(guild_map) do
        local x, y = Api.GetFlagPosition(ForceConfig[v.force].CarStart)
        local info = {TemplateID = ForceConfig[v.force].CarTemplateID, Force = v.force, X = x, Y = y}
        local car_id = Api.AddUnit(info)
        v.car = car_id
        v.car_event = Api.Task.AddEventTo(ID, CarLogic, v, k)
        CalcDistance(v)
    end

    -- pprint('guild_map ', guild_map)
    Api.Listen.PlayerPickedItem(OnPlayerPickedItem)
    Api.Listen.PlayerDead(
        function(uuid, attachid)
            local attackuuid = Api.GetPlayerUUID(attachid)
            OnPlayerKillOtherPlayer(attackuuid, uuid)
        end
    )
    Api.Task.WaitAlways()
end

function clean()
end
