local FirstCountdown = 15
local NewBattleCountdown = 3
local KillPlayerScore = 1

local RedForce = 2
local BlueForce = 3
local WatchForce = 0

local AirWall = {'r_airwall1', 'b_airwall1'}
local BirthPoint = {[RedForce] = 'red_birth_point', [BlueForce] = 'blue_birth_point'}


local RedPlayer
local BluePlayer
local ForcePlayers = {}
local ForceScores = {}

local WatchPlayers = {}

local IsGameOver
local CurrentBattle_ID
local EndBattle
local NewBattle
local WaitBluePlayerID

local LimitLifeTimeSec = 180 --180
local WinCount =   3
local WaitTimeSec =  60  --自动拒绝倒计时
 
local DestorySec =  1100


local serverGroupID


local function SendMessageToClient(...)
    for _, info in pairs(ForcePlayers) do
        if info.state ~= 'Offline' then
            Api.SendMessageToClient(info.uuid, ...)
        end
    end
    for _, watcher in pairs(WatchPlayers) do
        if watcher.state ~= 'Offline' then
            Api.SendMessageToClient(watcher.uuid, ...)
        end
    end
end

local function ForeachOnlinePlayers(fn)
    for _, info in pairs(ForcePlayers) do
        if info.state ~= 'Offline' then
            fn(info, t)
        end
    end
end

local function GetPlayerInfo(uuid)
    local info = ForcePlayers[RedForce]
    if info and info.uuid == uuid then
        return info, RedForce
    end
    info = ForcePlayers[BlueForce]
    if info and info.uuid == uuid then
        return info, BlueForce
    end
    return WatchPlayers[uuid],WatchForce
end

local function IsAllDead(force)
    local info = ForcePlayers[force]
    if info then
        if info.state == 'Normal' then
            return false
        end
    end
    return true
end

--挑战者掉线结束
local function OnGameOverByRedForceOff(force,reason)
    print('OnGameOverByRedForceOff ', force, reason, IsGameOver)
    if IsGameOver then
        return
    end

     IsGameOver = true

    -- local gameover_ret = {force = force, reason = reason} 
    -- SendMessageToClient('pvp1.gameover', gameover_ret)

    local uuid = ForcePlayers[RedForce].uuid 
    local ArenaApi = Api.GetArenaServiceApi()
    
    ArenaApi.SetArenaGameOver(serverGroupID,uuid,-2)
    ArenaApi.NoAutoWait = true

    Api.Task.Sleep(2)
    Api.SetGameOver(force, reason)
end

--拒绝结束 都是红方胜利
local function OnGameOverByRefuse(force,reason)
    print('OnGameOver ', force, reason, IsGameOver)
    if IsGameOver then
        return
    end

     IsGameOver = true

    -- local gameover_ret = {force = RedForce, reason = reason} 
    -- SendMessageToClient('pvp1.gameover', gameover_ret)

    local uuid = ForcePlayers[RedForce].uuid

    local ArenaApi = Api.GetArenaServiceApi()
    
    ArenaApi.SetArenaGameOver(serverGroupID,uuid,force)
    ArenaApi.NoAutoWait = true

    Api.Task.Sleep(2)
    Api.SetGameOver(force, reason)
end

local function WaitBluePlayer(uuid)
    Api.Task.Wait(Api.Listen.PlayerReady(uuid))
    local clientarg = GetPlayerInfo(uuid).arg
 
    Api.Task.StartEventByKey('client.pvp1wait', Api.GetNextArg(clientarg),WaitTimeSec)

    Api.Task.Sleep(WaitTimeSec)

    -- print('WaitBluePlayer GameOver red win')
    Api.Task.AddEventTo(ID, OnGameOverByRefuse, -1, 'timeout')
end

--收到拒绝消息
local function OnReciveMessage(ename, params, src_type, src_uuid)
    -- pprint('OnReciveMessage2222222222222222222222222222222222222222222222222222222222', ename,params or {}, src_type, src_uuid)
    if ename == 'arena_refuse' then
        -- if src_type == 'Player' then
        -- print(' OnReciveMessage sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss ')
        if WaitBluePlayerID then
            -- print(' OnReciveMessage zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz ')
            Api.Task.StopEvent(WaitBluePlayerID)
            WaitBluePlayerID = nil
            
            local info = ForcePlayers[RedForce]
            if info and info.state ~= 'Offline' then
                -- print('SendMessage pvp1wait 1111111111111111111111111111111111111111111111111 ',info.uuid)
                Api.SendMessageToClient(info.uuid, 'pvp1wait.over',true)
            end
        end

        Api.Task.AddEventTo(ID, OnGameOverByRefuse, 0, 'arena_refuse')
    end
end


local function OnGameOver(force, reason)
    print('OnGameOver ', force, reason, IsGameOver)
    if IsGameOver then
        return
    end

    IsGameOver = true
 
    local scorestr = ForceScores[RedForce] .. ' : ' .. ForceScores[BlueForce] 
    local gameover_ret = {force = force, reason = reason, scores = scorestr, players = ForcePlayers}
    SendMessageToClient('pvp1.gameover', gameover_ret)
    

    local uuid = ForcePlayers[RedForce].uuid 
    local ArenaApi = Api.GetArenaServiceApi()
    ArenaApi.SetArenaGameOver(serverGroupID,uuid,force)
    ArenaApi.NoAutoWait = true

	Api.Task.Sleep(20)
    Api.SetGameOver(force, reason)
end



EndBattle = function(winForce,gameover)
    -- print('NewBattle', winForce)
    for _, v in ipairs(AirWall) do
        Api.OpenFlag(v)
    end
    Api.Task.Sleep(1)
    local count = ForceScores[winForce] + 1

    -- print('44444444444444444444444444444444444444444444444444444444444444:count',count)

    ForceScores[winForce] = count
    if count ~= WinCount then
        SendMessageToClient('pvp1.win', winForce)
        Api.Task.Sleep(3)
    end
    ForeachOnlinePlayers(
        function(v)
            if v.state == 'Dead' then
                v.state = 'Normal'
                Api.StartRebirth(v.object_id)
            end
            Api.SetPlayerPositionToStart(v.uuid)
            Api.PlayerResetStatus(v.uuid)
            Api.AddUnitHpPercent(v.object_id, 100)
        end
    )

    SendMessageToClient('pvp1.res', ForceScores)
    if count == WinCount or gameover then
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


    if not first then
        SendMessageToClient('pvp1.newbattle', LimitLifeTimeSec)    
    end

    for _, v in ipairs(AirWall) do
        Api.CloseFlag(v)
    end
    local function PlayerDeaded(uuid, attackid)
        local force = Api.GetUnitForce(attackid)
        if force ~= RedForce and force ~= BlueForce then
            return
        end
        -- print('22222222222222222222222222222222222222222222222222222222222222222222222')
        if Api.IsPlayer(attackid) then
            local killer = Api.GetPlayerUUID(attackid)
            --计算个人总评分
            local killInfo = GetPlayerInfo(killer)
            killInfo.score = killInfo.score + KillPlayerScore
            local ClientApi = Api.GetClientApi(killer)
            ClientApi.PlayPlayerEffect(uuid, {BindBody = true, Name = '/res/effect/ef_buff_kill.assetbundles'})

            -- print('333333333333333333333333333333333333333333333333333333333333333333333')
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


    -- if first then
    --     local curclock = os.clock()
    --     local pass = math.floor(curclock - start_clock)
    --     local limittime = LimitLifeTimeSec + (FirstCountdown - pass)
    --     Api.Task.Sleep(limittime + 3 )
    -- else
    --     Api.Task.Sleep(LimitLifeTimeSec + 3)
    -- end


    Api.Task.Sleep(LimitLifeTimeSec)
    --todo 时间到了结算
    -- print('NewBattle timeover zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz')

    local redPlayer = ForcePlayers[RedForce]
    local bluePlayer = ForcePlayers[BlueForce]
    if redPlayer.state == 'Offline' then
        Api.Task.AddEventTo(ID, EndBattle, BlueForce)
        Api.Task.StopEvent(CurrentBattle_ID)
        return
    elseif bluePlayer.state == 'Offline' then
        Api.Task.AddEventTo(ID, EndBattle, RedForce)
        Api.Task.StopEvent(CurrentBattle_ID)
        return
    end

    local redHp = Api.GetUnitHp(redPlayer.object_id)
    local redMaxHp = Api.GetUnitMaxHp(redPlayer.object_id)
    -- pprint("NewBattle timeover redHp redMaxHp:",redHp,redMaxHp)
  
    local blueHp = Api.GetUnitHp(bluePlayer.object_id)
    local blueMaxHp = Api.GetUnitMaxHp(bluePlayer.object_id)
    -- pprint("NewBattle timeover blueHp blueMaxHp:",redHp,blueMaxHp)
  

    if (redHp/redMaxHp) > (blueHp/blueMaxHp) then
        Api.Task.AddEventTo(ID, EndBattle, RedForce)
        Api.Task.StopEvent(CurrentBattle_ID)
        return
    else
        Api.Task.AddEventTo(ID, EndBattle, BlueForce)
        Api.Task.StopEvent(CurrentBattle_ID)
        return
    end
     

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
    local members = ForcePlayers
    if FirstCountdown - pass < 0 then
        limittime = limittime + (FirstCountdown - pass)
    end

    -- print('StartClient zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz:',uuid, force)
    Api.Task.StartEventByKey('client.pvp1', Api.GetNextArg(clientarg), force, res, limittime, FirstCountdown - pass, members)
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
        
    end

    local force = Api.GetPlayerForce(uuid)
 
    local info = ForcePlayers[force] or {}
    info.score = info.score or 0
    info.name = Api.GetPlayerName(uuid)
    info.arg = Api.GetArg({PlayerUUID = uuid})
    info.object_id = Api.GetPlayerObjectID(uuid)
    info.state = 'Normal'
    info.force = force
    info.uuid = uuid

    if force == RedForce then

        ForcePlayers[RedForce] = info

        serverGroupID = Api.GetPlayerServerGroup(uuid)

        WaitBluePlayerID = Api.Task.AddEventTo(ID, WaitBluePlayer,uuid)   

    elseif force == BlueForce then
        local RedPlayer = ForcePlayers[RedForce]
        local redUUid = RedPlayer.uuid
        
        if WaitBluePlayerID then
            Api.Task.StopEvent(WaitBluePlayerID)
            if info.state ~= 'Offline' then
                Api.SendMessageToClient(redUUid, 'pvp1wait.over',false)
            end
        end

        ForcePlayers[BlueForce] = info

        start_clock = os.clock()
       
        if Api.IsPlayerReady(redUUid) then
            -- print('red red red red red 1111111111111111111111111111111111111111111111111111111111111111')
            Api.Task.AddEventTo(ID, StartClient, redUUid, RedForce,GetPlayerInfo(redUUid).arg)
        else
            -- print('red red red red red 2222222222222222222222222222222222222222222222222222222222222222')
            Api.Task.AddEventTo(ID, ListenReady, redUUid, RedForce)
        end

        if Api.IsPlayerReady(uuid) then
            -- print('blue blue blue blue 1111111111111111111111111111111111111111111111111111111111111111')
            Api.Task.AddEventTo(ID, StartClient, uuid, force,GetPlayerInfo(uuid).arg)
        else
            -- print('blue blue blue blue 2222222222222222222222222222222222222222222222222222222222222222')
            Api.Task.AddEventTo(ID, ListenReady, uuid, force)
        end
        
        CurrentBattle_ID =  Api.Task.AddEventTo(ID,NewBattle, true)
    else
        
        Api.SetPlayerForce(uuid,WatchForce)
        WatchPlayers[uuid] = info

        --观众
        -- print('444444444444444444444444444444444444444444444444444',uuid,force)
         if Api.IsPlayerReady(uuid) then
            -- print('guanzhong 44444444444444444444444444444444444444111111111111111')
            Api.Task.AddEventTo(ID, StartClient, uuid, WatchForce,GetPlayerInfo(uuid).arg)
        else
            -- print('guanzhong 44444444444444444444444444444444444442222222222222')
            Api.Task.AddEventTo(ID, ListenReady, uuid, WatchForce)
        end
    end
end

local function PlayerLeaveLogic(uuid)
    -- pprint('player leave ', uuid, ForcePlayers)
    local info = GetPlayerInfo(uuid)
    if info and (info.force == RedForce or info.force == BlueForce) then
        info.state = 'Offline'
        if CurrentBattle_ID then
            print('PlayerLeaveLogic ................................')
            local winforce = info.force == RedForce and BlueForce or RedForce
            local forceGameOver = true
            Api.Task.AddEventTo(ID, EndBattle, winforce,forceGameOver)
            Api.Task.StopEvent(CurrentBattle_ID)
            CurrentBattle_ID = nil
            return
        end

        print('33333333333333333333333333333333333333333333333333333333')
        if info.force == RedForce and WaitBluePlayerID then
            Api.Task.StopEvent(WaitBluePlayerID)
            WaitBluePlayerID = nil
            print('4444444444444444444444444444444444444444444444')
            Api.Task.AddEventTo(ID, OnGameOverByRedForceOff, -2, 'Offline')
        end
    end
end

local function InitPlayerForce(uuid)
    if ForcePlayers[RedForce] and ForcePlayers[RedForce].uuid == uuid then
        return BlueForce
    end
    if ForcePlayers[BlueForce] and ForcePlayers[BlueForce].uuid == uuid then
        return RedForce
    end
    return 0
end

function main()


    WinCount =  Api.GetExcelConfig('arena_win') or 3
    WaitTimeSec = Api.GetExcelConfig('arena_accept_times') or 60  --自动拒绝倒计时
 
    DestorySec = Api.GetExcelConfig('arena_time_max') or  1100

    -- 同步阵营信息
    ForcePlayers = {}
 
    ForceScores[BlueForce] = 0
    ForceScores[RedForce] = 0

    for _, v in ipairs(AirWall) do
        Api.OpenFlag(v)
    end

    Api.Listen.InitPlayerForce(InitPlayerForce)
    Api.Listen.PlayerEnterZone(PlayerEnterLogic)
    Api.Listen.PlayerLeaveZone(PlayerLeaveLogic)

    Api.Listen.SessionReconnect(PlayerEnterLogic, true)

    Api.Listen.Message(OnReciveMessage)
    -- start_clock = os.clock()
    -- CurrentBattle_ID = Api.Task.AddEvent(NewBattle, true)
    Api.Task.Sleep(DestorySec)
    -- print('main timeover main timeover main timeover main timeover main timeover main timeover main timeover main timeover main timeover ')
    local blueScore = ForceScores[BlueForce]
    local redScore = ForceScores[RedForce]
    local winForce = 0
    if redScore > blueScore then
        winForce = RedForce
    else
        winForce = BlueForce   
    end
    OnGameOver(winForce, 'timeout')
    Api.Task.WaitAlways()
end

function clean()

end
