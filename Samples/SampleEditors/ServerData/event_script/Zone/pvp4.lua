local LimitLifeTimeSec = 300
local FirstCountdown = 15
local NewBattleCountdown = 5
local KillPlayerScore = 10
local RedForce = 2
local BlueForce = 3
local WinCount = 3
local AirWall = {'r_airwall1', 'b_airwall1'}
local BirthPoint = {[RedForce] = 'red_birth_point', [BlueForce] = 'blue_birth_point'}
local ForcePlayers = {}
--force -- wincount
local ForceScores = {}
local IsGameOver
local CurrentBattle_ID
local EndBattle
local NewBattle
local function SendMessageToClient(...)
    for force, t in pairs(ForcePlayers) do
        for uuid, v in pairs(t) do
            if v.state ~= 'Offline' then
                Api.SendMessageToClient(uuid, ...)
            end
        end
    end
end

local function ForeachOnlinePlayers(fn)
    for force, t in pairs(ForcePlayers) do
        for uuid, v in pairs(t) do
            if v.state ~= 'Offline' then
                fn(v, t)
            end
        end
    end
end

local function GetPlayerInfo(uuid)
    local info = ForcePlayers[RedForce][uuid]
    if info then
        return info, RedForce
    else
        return ForcePlayers[BlueForce][uuid], BlueForce
    end
end

local function IsAllDead(force)
    for _, v in pairs(ForcePlayers[force]) do
        if v.state == 'Normal' then
            return false
        end
    end
    return true
end

local function OnGameOver(force, reason)
    print('OnGameOver ', force, reason, IsGameOver)
    if IsGameOver then
        return
    end
    IsGameOver = true
    local exploitdata = Api.FindFirstExcelData('pvp/pvp.xlsx/pvp', {map_id = arg.MapTemplateID})
    if not exploitdata then
        exploitdata = {victory_value = 0, draw_value = 0, fail_value = 0}
    end
    local result_players = {}
    local sub_events = {}
    local limit_today = Api.GetExcelConfig('pvp_value_limit')

    local function CalcPlayerInfo(v)
        local exploit
        local win
        if v.force == force then
            exploit = exploitdata.victory_value
            win = 1
        elseif force ~= BlueForce and force ~= RedForce then
            exploit = exploitdata.draw_value
            win = 0
        else
            exploit = exploitdata.fail_value
            win = -1
        end
        local playerApi = Api.GetPlayerApi(v.arg)
        local today_exploit = playerApi.SetPvpResult('pvp4', win, exploit)
        v.today_exploit = today_exploit
        v.exploit = exploit
        v.killed = v.score / KillPlayerScore
        table.insert(result_players, v)
    end
    ForeachOnlinePlayers(
        function(v)
            table.insert(sub_events, Api.Task.AddEvent(CalcPlayerInfo, v))
        end
    )
    Api.Task.WaitParallel(sub_events)
    table.sort(
        result_players,
        function(x, y)
            if x.score ~= y.score then
                return x.score > y.score
            elseif ForceScores[x.force] ~= ForceScores[y.force] then
                return ForceScores[x.force] > ForceScores[y.force]
            else
                return x.force < y.force
            end
        end
    )

    local gameover_ret = {force = force, reason = reason, scores = ForceScores, players = result_players}
    SendMessageToClient('pvp4.gameover', gameover_ret)
    -- pprint('gameover',gameover_ret)
    Api.Task.Sleep(20)
    Api.SetGameOver(force, reason)
end

EndBattle = function(winForce)
    -- print('NewBattle', winForce)
    for _, v in ipairs(AirWall) do
        Api.OpenFlag(v)
    end
    Api.Task.Sleep(1)
    local count = ForceScores[winForce] + 1
    ForceScores[winForce] = count
    if count ~= WinCount then
        SendMessageToClient('pvp4.win', winForce)
        Api.Task.Sleep(3)
    end
    ForeachOnlinePlayers(
        function(v)
            if v.state == 'Dead' then
                v.state = 'Normal'
                Api.StartRebirth(v.object_id)
            end
            Api.SetPlayerPositionToStart(v.uuid)
            Api.AddUnitHpPercent(v.object_id, 100)
        end
    )

    SendMessageToClient('pvp4.res', ForceScores)
    if count == WinCount then
        --GameOver
        Api.Task.AddEventTo(ID, OnGameOver, winForce, 'win')
    else
        CurrentBattle_ID = Api.Task.AddEventTo(ID, NewBattle, false)
    end
end

NewBattle = function(first)
    local delaySec = first and FirstCountdown or NewBattleCountdown
    -- print('NewBattle', first)
    if not first then
        ForeachOnlinePlayers(
            function(v)
                Api.Task.StartEventByKey('client.countdown_ui', Api.GetNextArg(v.arg), delaySec, 'pvp_restart')
            end
        )
    end
    Api.Task.Sleep(delaySec)
    if first then
        SendMessageToClient('pvp4.members', ForcePlayers)
    end
    for _, v in ipairs(AirWall) do
        Api.CloseFlag(v)
    end
    local function PlayerDeaded(uuid, attackid)
        local force = Api.GetUnitForce(attackid)
        if force ~= RedForce and force ~= BlueForce then
            return
        end
        if Api.IsPlayer(attackid) then
            local killer = Api.GetPlayerUUID(attackid)
            --计算个人总评分
            local killInfo = GetPlayerInfo(killer)
            killInfo.score = killInfo.score + KillPlayerScore
            local ClientApi = Api.GetClientApi(killer)
            ClientApi.PlayPlayerEffect(uuid, {BindBody = true, Name = '/res/effect/ef_buff_kill.assetbundles'})
        end

        local info, currentforce = GetPlayerInfo(uuid)
        info.state = 'Dead'
        if IsAllDead(currentforce) then
            Api.Task.AddEventTo(ID, EndBattle, force)
            Api.Task.StopEvent(CurrentBattle_ID)
            CurrentBattle_ID = nil
        end
    end

    Api.Listen.PlayerDead(
        function(uuid, attackid)
            Api.Task.AddEventTo(ID, PlayerDeaded, uuid, attackid)
        end
    )
    Api.Task.WaitAlways()
end

local function StartClient(uuid, force, clientarg)
    local curclock = os.clock()
    local pass = math.floor(curclock - start_clock)

    local res = {}
    for f, v in pairs(ForceScores) do
        res[f] = {cur = v, max = WinCount}
    end

    local limittime = LimitLifeTimeSec
    local members
    if FirstCountdown - pass < 0 then
        limittime = limittime + (FirstCountdown - pass)
        members = ForcePlayers
    end
    Api.Task.StartEventByKey('client.pvp4', Api.GetNextArg(clientarg), force, res, limittime, FirstCountdown - pass, members)
end

local function ListenReady(uuid, force)
    Api.Task.Wait(Api.Listen.PlayerReady(uuid))
    StartClient(uuid, force, GetPlayerInfo(uuid).arg)
end

local function PlayerEnterLogic(uuid, reconnect)
    if IsGameOver then
        return
    end
    if not reconnect then
        Api.Task.AddEventTo(
            ID,
            function()
                local playerApi = Api.GetPlayerApi(uuid)
                playerApi.AddIntFlag('EnterPvp4Count', 1, true)
                playerApi.CheckPvpRewardRedTips()
            end
        )
    end
    local force = Api.GetPlayerForce(uuid)
    local info = ForcePlayers[force][uuid] or {}
    info.score = info.score or 0
    info.name = Api.GetPlayerName(uuid)
    info.arg = Api.GetArg({PlayerUUID = uuid})
    info.object_id = Api.GetPlayerObjectID(uuid)
    info.state = 'Normal'
    info.force = force
    info.uuid = uuid

    -- pprint('info ', info)
    ForcePlayers[force][uuid] = info
    if Api.IsPlayerReady(uuid) then
        Api.Task.AddEventTo(ID, StartClient, uuid, force)
    else
        Api.Task.AddEventTo(ID, ListenReady, uuid, force)
    end
end

local function PlayerLeaveLogic(uuid)
    -- pprint('player leave ', uuid, ForcePlayers)
    local info = GetPlayerInfo(uuid)
    if info then
        info.state = 'Offline'
        if CurrentBattle_ID and IsAllDead(info.force) then
            local winforce = info.force == RedForce and BlueForce or RedForce
            Api.Task.AddEventTo(ID, EndBattle, winforce)
            Api.Task.StopEvent(CurrentBattle_ID)
            CurrentBattle_ID = nil
        end
    end
end

local function InitPlayerForce(uuid)
    if ForcePlayers[BlueForce][uuid] then
        return BlueForce
    end
    if ForcePlayers[RedForce][uuid] then
        return RedForce
    end
    return 0
end

function main()
    -- 同步阵营信息
    ForcePlayers[BlueForce] = {}
    ForcePlayers[RedForce] = {}
    ForceScores[BlueForce] = 0
    ForceScores[RedForce] = 0
    for _, v in ipairs(AirWall) do
        Api.OpenFlag(v)
    end
    Api.Listen.InitPlayerForce(InitPlayerForce)
    Api.Listen.PlayerEnterZone(PlayerEnterLogic)
    Api.Listen.PlayerLeaveZone(PlayerLeaveLogic)
    Api.Listen.SessionReconnect(PlayerEnterLogic, true)
    start_clock = os.clock()
    CurrentBattle_ID = Api.Task.AddEvent(NewBattle, true)
    Api.Task.Sleep(LimitLifeTimeSec)

    local blueScore = ForceScores[BlueForce]
    local redScore = ForceScores[RedForce]
    local winForce = 0
    if blueScore > redScore then
        winForce = BlueForce
    elseif blueScore < redScore then
        winForce = RedForce
    end
    OnGameOver(winForce, 'timeout')
    Api.Task.WaitAlways()
end

function clean()
end
