local RedForce = 2
local BlueForce = 3
local AirWall = {'r_airwall1', 'r_airwall2', 'b_airwall1', 'b_airwall2'}

local TransferConfig = {
    [RedForce] = {'item_pub.50000001', 'r_transfer_point'},
    [BlueForce] = {'item_pub.50000002', 'b_transfer_point'}
}

local LimitLifeTimeSec = 600
--20秒开空气墙
local FirstCountdown = 20
local KillPlayerScore = 10
local WinScore = 100

local FlagIDs = {
    [RedForce] = 25008,
    [BlueForce] = 25007
}

local Monsters = {
    shengshou1 = {
        MonsterID = 25003,
        KillScore = 20,
        SecondScore = 1
    },
    shengshou2 = {
        MonsterID = 25004,
        KillScore = 20,
        SecondScore = 1
    },
    shengshou3 = {
        MonsterID = 25005,
        KillScore = 20,
        SecondScore = 1
    },
    shengshou4 = {
        MonsterID = 25006,
        KillScore = 20,
        SecondScore = 1
    },
    neutral_flag = {
        MonsterID = 25009,
        KillScore = 0,
        SecondScore = 3
    }
}

-- 资源分 force - score
local ForceScores = {}
-- 战旗 force - {flag- Monsters.Value}
local ForceFlags = {}

local TransferEventID = {}
--阵营对应的玩家列表 force-{uuid1,uuid2,...}
local ForcePlayers = {}

local SubEvents = {}
--当前战场已正式开始
local IsStarted
local IsGameOver
local function GetResCount(force)
    local count = 0
    for _, v in pairs(ForceFlags[force] or {}) do
        count = count + 1
    end
    return count
end

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

local function OnResChange()
    --同步资源信息
    local res = {}
    for f, v in pairs(ForceScores) do
        local rescount = GetResCount(f)
        table.insert(res, {force = f, cur = ForceScores[f], max = WinScore, rescount = rescount})
    end

    SendMessageToClient('pvp10.res', res)
end

--todo 玩家下线后的处理
local function OnGameOver(force, reason)
    if IsGameOver then
        return
    end
    print('OnGameOver ', force, reason, IsGameOver)
    IsGameOver = true
    for _, v in ipairs(SubEvents) do
        Api.Task.StopEvent(v)
    end
    local exploitdata = Api.FindFirstExcelData('pvp/pvp.xlsx/pvp', {map_id = arg.MapTemplateID})
    if not exploitdata then
        exploitdata = {victory_value = 0, draw_value = 0, fail_value = 0}
    end
    local result_players = {}
    local sub_events = {}
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
        Api.PlayerRebirthOrgin(v.uuid)
        local playerApi = Api.GetPlayerApi(v.arg)
        local today_exploit = playerApi.SetPvpResult('pvp10', win, exploit)
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
    SendMessageToClient('pvp10.gameover', gameover_ret)
    Api.Task.Sleep(20)
    Api.SetGameOver(force, reason)
end

local function AddScore(force, score)
    local total = (ForceScores[force] or 0) + score
    ForceScores[force] = math.min(total, WinScore)
    if total >= WinScore then
        total = WinScore
        Api.Task.AddEventTo(ID, OnGameOver, force, 'Score 20000')
        return true
    end
    return false
end

local function AutoGetScore()
    local isgameover
    while (true) do
        if IsGameOver or isgameover then
            break
        end
        Api.Task.Sleep(1)
        -- 计算分值
        local change = false
        for k, v in pairs(ForceFlags) do
            local score = 0
            for _, vv in pairs(v) do
                isgameover = AddScore(k, vv.SecondScore)
                if isgameover then
                    break
                end
                change = true
            end
        end
        if change then
            OnResChange()
        end
    end
end

local function ListenTransfer(force)
    local trans_conf = TransferConfig[force]
    local current_eid = Api.GetCurrentEventID()
    local item_eid = Api.Task.AddEventByKey(trans_conf[1])
    local x, y = Api.GetFlagPosition(trans_conf[2])
    Api.Listen.ListenEvent(
        item_eid,
        function(uuid)
            local ClientApi = Api.GetClientApi(uuid)
            ClientApi.Task.StartEvent('Client/fade_screen', 2)
            Api.SetPlayerPosition(uuid, x, y)
        end
    )
    Api.Task.Wait(item_eid)
end

local function FlagDoSoming(uid, flag, force)
    ForceFlags[force] = ForceFlags[force] or {}
    ForceFlags[force][flag] = Monsters[flag]
    OnResChange()
    local resCount = GetResCount(force)
    if resCount == 2 then
        --生成传送点
        TransferEventID[force] = Api.Task.AddEvent(ListenTransfer, force)
    end

    local success, id, attackid = Api.Task.Wait(Api.Listen.ObjectRemove(uid))
    -- print('success, id, attackid',success, id, attackid)
    ForceFlags[force][flag] = nil

    resCount = GetResCount(force)
    -- pprint('resCount ',resCount,force,TransferEventID)
    if TransferEventID[force] and resCount < 2 then
        Api.Task.StopEvent(TransferEventID[force])
    end

    local winForce
    if attackid then
        winForce = Api.GetUnitForce(attackid)
    else
        winForce = force == RedForce and BlueForce or RedForce
    end
    local m = Monsters[flag]
    local info = {TemplateID = FlagIDs[winForce], Force = winForce, Direction = 1.5}
    info.X, info.Y = Api.GetFlagPosition(flag)
    -- pprint('flag --info',flag, winForce, info)
    FlagDoSoming(Api.AddUnit(info), flag, winForce)
end

local function MonterDoSoming(uid, flag)
    local current_eid = Api.GetCurrentEventID()
    local damages = {}

    local function PlayerStartAttack(uuid)
        local waitid
        waitid =
            Api.Listen.PlayerBattleStateChange(
            uuid,
            function(state)
                if state == 0 then
                    --脱战
                    print('state === 0')
                    damages[uuid] = nil
                    Api.Task.StopEvent(waitid)
                end
            end
        )
        Api.Task.Wait(waitid)
    end

    Api.Listen.UnitDamage(
        uid,
        function(id, attackid, hp)
            local uuid = Api.GetPlayerUUID(attackid)
            if not damages[uuid] then
                Api.Task.AddEventTo(current_eid, PlayerStartAttack, uuid)
            end
            damages[uuid] = (damages[uuid] or 0) + hp
        end
    )
    Api.Task.Wait(Api.Listen.ObjectRemove(uid))

    local forceDamages = {}
    --伤害判定
    for k, v in pairs(damages) do
        local force = Api.GetPlayerForce(k)
        forceDamages[force] = (forceDamages[force] or 0) + v
    end

    local winer
    for force, damage in pairs(forceDamages) do
        if not winer then
            winer = {force = force, damage = damage}
        elseif damage > winer.damage then
            winer.force = force
            winer.damage = damage
        end
    end
    if not winer then
        return
    end
    local m = Monsters[flag]
    AddScore(winer.force, m.KillScore)
    OnResChange()
    local info = {TemplateID = FlagIDs[winer.force], Force = winer.force, Direction = 1.5}
    info.X, info.Y = Api.GetFlagPosition(flag)
    -- 插旗
    local flagid = Api.AddUnit(info)
    FlagDoSoming(flagid, flag, winer.force)
end

function main(ele)
    if ele then
        --重载配置
        WinScore = ele.win_score
        FlagIDs[RedForce] = ele.red_flagid
        FlagIDs[BlueForce] = ele.blue_flagid
        TransferConfig[RedForce] = {ele.red_item, ele.red_transfer}
        TransferConfig[BlueForce] = {ele.blue_item, ele.blue_transfer}

        Monsters = {}
        Monsters[ele.neutral_flag] = {
            MonsterID = ele.neutral_flagid,
            KillScore = 0,
            SecondScore = ele.big_resource
        }
        for i, v in ipairs(ele.monster.flag) do
            Monsters[v] = {
                MonsterID = ele.monster.id[i],
                KillScore = ele.kill_score,
                SecondScore = ele.small_resource
            }
        end
    end
    -- 开启空气墙
    for _, v in ipairs(AirWall) do
        Api.OpenFlag(v)
    end

    -- 生成4圣兽 灰色大旗
    for k, v in pairs(Monsters) do
        local info = {TemplateID = v.MonsterID, Force = 1, Direction = 1.5}
        info.X, info.Y = Api.GetFlagPosition(k)
        local uid = Api.AddUnit(info)
        print('monster added', info.X, info.Y, uid)
        table.insert(SubEvents, Api.Task.AddEvent(MonterDoSoming, uid, k))
    end

    -- 自动产生资源
    local autores_eid = Api.Task.AddEvent(AutoGetScore)
    table.insert(SubEvents, autores_eid)

    local function PlayerDeaded(uuid, attackid)
        local force = Api.GetUnitForce(attackid)
        if force ~= RedForce and force ~= BlueForce then
            return
        end
        --击杀一个玩家资源涨一分
        AddScore(force, 1)
        print('score add kill', force)
        OnResChange()
        if Api.IsPlayer(attackid) then
            local killer = Api.GetPlayerUUID(attackid)
            --计算个人总评分
            local killInfo = GetPlayerInfo(killer)
            killInfo.score = killInfo.score + KillPlayerScore
            local ClientApi = Api.GetClientApi(killer)
            ClientApi.PlayPlayerEffect(uuid, {BindBody = true, Name = '/res/effect/ef_buff_kill.assetbundles'})
        end
    end

    table.insert(
        SubEvents,
        Api.Listen.PlayerDead(
            function(uuid, attackid)
                Api.Task.AddEventTo(ID, PlayerDeaded, uuid, attackid)
            end
        )
    )

    local start_clock = os.clock()
    local function StartClient(uuid, force)
        local clientarg = Api.GetNextArg(arg, {PlayerUUID = uuid})
        local res = {}

        res[RedForce] = {cur = ForceScores[RedForce], max = WinScore, rescount = GetResCount(RedForce)}
        res[BlueForce] = {cur = ForceScores[BlueForce], max = WinScore, rescount = GetResCount(BlueForce)}

        local curclock = os.clock()
        local pass = math.floor(curclock - start_clock)

        local limittime = LimitLifeTimeSec
        local members
        if FirstCountdown - pass < 0 then
            limittime = limittime + (FirstCountdown - pass)
            members = ForcePlayers[force]
        end
        Api.Task.StartEventByKey('client.pvp10', clientarg, force, res, limittime, FirstCountdown - pass, members)
    end

    local function ListenReady(uuid, force)
        Api.Task.Wait(Api.Listen.PlayerReady(uuid))
        StartClient(uuid, force)
    end

    local function PlayerEnterLogic(uuid)
        local reconnect = GetPlayerInfo(uuid) ~= nil
        if not reconnect then
            Api.Task.AddEventTo(
                ID,
                function()
                    local playerApi = Api.GetPlayerApi(uuid)
                    playerApi.AddIntFlag('EnterPvp10Count', 1, true)
                    playerApi.CheckPvpRewardRedTips()
                end
            )
        end
        local force = Api.GetPlayerForce(uuid)

        local info = ForcePlayers[force][uuid] or {}
        info.score = info.score or 0
        info.name = info.name or Api.GetPlayerName(uuid)
        info.arg = Api.GetArg({PlayerUUID = uuid})
        info.object_id = Api.GetPlayerObjectID(uuid)
        info.state = 'Normal'
        info.force = force
        info.uuid = uuid
        ForcePlayers[force][uuid] = info
        if Api.IsPlayerReady(uuid) then
            Api.Task.AddEventTo(ID, StartClient, uuid, force)
        else
            Api.Task.AddEventTo(ID, ListenReady, uuid, force)
        end
    end

    local function PlayerLeaveLogic(uuid)
        local info = GetPlayerInfo(uuid)
        if info then
            info.state = 'Offline'
        else
            pprint('ForcePlayers', ForcePlayers)
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

    -- 同步阵营信息
    Api.Listen.PlayerEnterZone(PlayerEnterLogic)
    Api.Listen.PlayerLeaveZone(PlayerLeaveLogic)
    Api.Listen.SessionReconnect(PlayerEnterLogic)
    Api.Listen.InitPlayerForce(InitPlayerForce)
    ForcePlayers[BlueForce] = {}
    ForcePlayers[RedForce] = {}
    ForceScores[RedForce] = 0
    ForceScores[BlueForce] = 0
    --  关闭空气墙
    Api.Task.Sleep(FirstCountdown)

    for _, v in ipairs(AirWall) do
        Api.CloseFlag(v)
    end

    IsStarted = true

    ForeachOnlinePlayers(
        function(v, t)
            Api.SendMessageToClient(v.uuid, 'pvp10.members', t)
        end
    )

    Api.Task.Sleep(LimitLifeTimeSec)

    local blueScore = (ForceScores[BlueForce] or 0)
    local redScore = (ForceScores[RedForce] or 0)
    local winForce = 0
    if blueScore > redScore then
        winForce = BlueForce
    elseif blueScore < redScore then
        winForce = RedForce
    end
    --平局， todo
    OnGameOver(winForce, 'timeout')
    Api.Task.WaitAlways()
end
