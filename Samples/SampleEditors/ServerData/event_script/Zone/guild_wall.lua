local Main_TemplateID = 502000
local Help_TemplateID = 502001
local RedForce = 2
local BlueForce = 3
local SINGLE_DEBUG = false
local GlobalConfig = {
    MaxRunningSec = 1200,
    OpenAirWallSec = 120,
    PickResourceEvent = 'message.160',
    SuccessEvent = 'message.129',
    FailEvent = 'message.130',
    [Main_TemplateID] = {
        MainZone = true,
        RepairItemTemplateID = 502000,
        RepairBuffID = 21502,
        RepairResource = 100,
        MapTemplateID = Main_TemplateID,
        RepairWarnEvent = 'message.142',
        RepairSuccessEvent = 'message.143',
        AirFlag = {'l_airwall1', 'l_airwall2', 'l_airwall3', 'r_airwall1', 'r_airwall2', 'r_airwall3'},
        [RedForce] = {
            OtherZoneTransferEvent = 'item_pub.50200003',
            Tower = {
                top = {
                    {Flag = 'left11', TemplateID = 27011},
                    {Flag = 'left12', TemplateID = 27012}
                },
                middle = {
                    {Flag = 'left21', TemplateID = 27013},
                    {Flag = 'left22', TemplateID = 27014}
                },
                bottom = {
                    {Flag = 'left31', TemplateID = 27015},
                    {Flag = 'left32', TemplateID = 27016}
                }
            },
            Boss = {Flag = 'left_boss'},
            MonsterAttackFlag = 'left_reborn'
        },
        [BlueForce] = {
            OtherZoneTransferEvent = 'item_pub.50200004',
            Tower = {
                top = {
                    {Flag = 'right11', TemplateID = 27017},
                    {Flag = 'right12', TemplateID = 27018}
                },
                middle = {
                    {Flag = 'right21', TemplateID = 27019},
                    {Flag = 'right22', TemplateID = 27020}
                },
                bottom = {
                    {Flag = 'right31', TemplateID = 27021},
                    {Flag = 'right32', TemplateID = 27022}
                }
            },
            MonsterAttackFlag = 'right_reborn',
            Boss = {Flag = 'right_boss'}
        },
        AirMessageEvent = 'message.140',
        CommandMessageEvent = 'message.131'
    },
    [Help_TemplateID] = {
        MapTemplateID = Help_TemplateID,
        PickAddResource = 10,
        AirFlag = {'l_airwall1', 'l_airwall2', 'l_airwall3', 'r_airwall1', 'r_airwall2', 'r_airwall3'},
        [RedForce] = {
            OtherZoneTransferEvent = 'item_pub.50200101'
        },
        [BlueForce] = {
            OtherZoneTransferEvent = 'item_pub.50200102'
        },
        AirMessageEvent = 'message.141',
        CommandMessageEvent = 'message.131',
        HelpMonsters = {
            {Flag = 'shengshou1', TemplateID = 27023, BirthTemplateID = 27028, Direction = 0, BuffID = 21081, PickTemplateID = 5020011},
            {Flag = 'shengshou2', TemplateID = 27024, BirthTemplateID = 27029, Direction = 0, BuffID = 21082, PickTemplateID = 5020012},
            {Flag = 'shengshou3', TemplateID = 27025, BirthTemplateID = 27030, Direction = 0, BuffID = 21081, PickTemplateID = 5020013},
            {Flag = 'shengshou4', TemplateID = 27026, BirthTemplateID = 27031, Direction = 0, BuffID = 21082, PickTemplateID = 5020014},
            {Flag = 'shengshou5', TemplateID = 27027, BirthTemplateID = 27032, Direction = 0, BuffID = 21083, PickTemplateID = 5020015}
        }
    }
}
local GuildApi
local zone_info
local start_time_sec
local is_gameover
local kick_players = {}
local player_map = {}
local other_player_map = {}
local player_enter_events = {}
local guild_map
local send_queues = {}
local current_config
local sync_data = {
    [RedForce] = {
        score = 0,
        resource = 0,
        kill_boss = 0,
        orders = {}
    },
    [BlueForce] = {
        score = 0,
        resource = 0,
        kill_boss = 0,
        orders = {}
    }
}
local air_messages = {}

local function GetEnemyForce(force)
    return force == RedForce and BlueForce or RedForce
end

local function GetEnemyConfig(force)
    return current_config[GetEnemyForce(force)]
end

local function GetForceGuildUUID(force)
    for guild_uuid, v in pairs(guild_map) do
        if v.force == force then
            return guild_uuid
        end
    end
end

local function GetOtherZoneTemplateID()
    return current_config.MapTemplateID == Main_TemplateID and Help_TemplateID or Main_TemplateID
end
local function GetOtherZoneUUID()
    return zone_info and zone_info.map[GetOtherZoneTemplateID()]
end

local function SendToOtherZone(ename, params)
    if not zone_info then
        table.insert(send_queues, {name = ename, content = params})
    else
        local v = GetOtherZoneUUID()
        local uuid = Api.GetZoneUUID()
        Api.SendMessage('Zone', v, ename, params)
    end
end
local player_dirty
local other_player_dirty

local function SyncClientDataTick()
    if not guild_map or is_gameover then
        return
    end
    for f, v in pairs(sync_data) do
        for _, order in pairs(v.orders) do
            order.count = Api.GetPlayerCountNearFlag(order.flag, 7, f)
        end
    end

    local ret = {sync_data = sync_data, main = current_config.MainZone, start_time = start_time_sec, time = os.time()}
    for uuid, v in pairs(player_map) do
        Api.SendMessageToClient(uuid, 'guild-wall-battleinfo', ret)
    end
    -- if main_running and not Api.IsFuncOpenTime('guildfort_in') then
    --活动结束
    -- Api.Task.AddEventTo(ID, OnGameOver, 'timeout')
    -- end
end

local function StartZoneEventByKey(ekey, appendArg, ...)
    appendArg = appendArg or {}
    Api.Task.AddEventToByKey(ID, ekey, Api.GetArg(appendArg), ...)

    local otherArg = Api.GetArg(appendArg)
    otherArg.ZoneUUID = GetOtherZoneUUID()
    if otherArg.ZoneUUID then
        local ZoneApi = Api.GetZoneApi(otherArg)
        ZoneApi.Task.AddEventToByKey(ID, ekey, otherArg, ...)
    end
end

local function MonsterAttackTo(uid, flag)
    Api.UnitAttachToFlag(uid, flag)
end

local function OnReciveMessage(ename, params, src_type, src_uuid)
    pprint('OnReciveMessage', ename, params, src_type, src_uuid)
    if src_type == 'Guild' then
        if ename == 'guildwall-init' then
            zone_info = params
            local uuid = Api.GetZoneUUID()
            for _, v in ipairs(send_queues) do
                SendToOtherZone(v.name, v.content)
            end
            for _, v in pairs(player_map) do
                SendToOtherZone('guildwall-memberin', v)
            end
            SendToOtherZone('guildwall-starttime', start_time_sec)
            -- pprint('guildwall-init ------------------------', params)
            send_queues = {}
        end
    elseif src_type == 'Zone' then
        if ename == 'guildwall-sync' then
            local sdata = sync_data[params.force]
            sdata.score = sdata.score + params.score
            sdata.resource = sdata.resource + params.resource
        elseif ename == 'guildwall-monster-birth' then
            local x, y = Api.GetFlagPosition(current_config[params.force].Boss.Flag)
            local uinfo = {TemplateID = params.TemplateID, Force = params.force, X = x, Y = y}
            local uid = Api.AddUnit(uinfo)
            -- print('monster birth ', x, y, uid)
            Api.Task.AddEventTo(ID, MonsterAttackTo, uid, current_config[params.force].MonsterAttackFlag)
        elseif ename == 'guildwall-addbuff' then
            local buffid = params.buff_id
            local bossid = current_config[params.force].Boss.id
            Api.UnitAddBuff(bossid, buffid)
        elseif ename == 'guildwall-memberin' then
            other_player_map[params.uuid] = params
        elseif ename == 'guildwall-memberout' then
            other_player_map[params.uuid] = nil
        elseif ename == 'guildwall-memberscore' then
            other_player_map[params.uuid].score = params.score
            other_player_map[params.uuid].kill_count = params.kill_count
        elseif ename == 'guildwall-starttime' then
            if start_time_sec > params then
                start_time_sec = params
            end
        elseif ename == 'guildwall-gameover' then
            Api.Task.AddEventTo(
                ID,
                function()
                    pprint('guildwall-gameover -- bla', params)
                    Api.Task.Sleep(20)
                    Api.SetGameOver(params.force, params.reason)
                end
            )
        end
    elseif ename == 'guildwall-command' then
        Api.Task.AddEventTo(
            ID,
            function()
                local PlayerApi = Api.GetPlayerApi(src_uuid)
                local postion = PlayerApi.GetGuildPosition()
                local postion_config = Api.FindFirstExcelData('guild/guild.xlsx/guild_position', {position_id = postion})
                if postion_config.fort_order == 1 then
                    local info = player_map[src_uuid]
                    local o = params.data
                    local f = info.force
                    local orders = sync_data[f].orders
                    if params.checked then
                        if table.len(orders) < 2 then
                            local flag = f == RedForce and o.order_position_left or o.order_position_right
                            orders[o.id] = {flag = flag, count = Api.GetPlayerCountNearFlag(flag, 10, f)}
                            Api.Task.AddEventToByKey(ID, current_config.CommandMessageEvent, Api.GetArg({Force = f, Playe}), o.order_name)
                        end
                    else
                        orders[o.id] = nil
                    end
                else
                    local ClientApi = Api.GetClientApi(src_uuid)
                    ClientApi.ShowMessage('guild_carriage_kicklimit')
                end
            end
        )
    elseif ename == 'guildwall-getmembers' then
        Api.SendMessageToClient(src_uuid, 'guildwall-members', {flag = params.flag, players = player_map, other_players = other_player_map})
    end
end

local function OnGameOver(force, reason)
    if not force then
        force = sync_data[RedForce].score > sync_data[BlueForce].score and RedForce or BlueForce
    end
    local win_guild = GetForceGuildUUID(force)
    SendToOtherZone('guildwall-gameover', {force = force, reason = reason, win_guild = win_guild})
    local calc_win_guild = GuildApi.WallGameOver(win_guild, sync_data[RedForce].score == sync_data[BlueForce].score)
    local win_force = calc_win_guild == win_guild and force or GetEnemyForce(force)

    StartZoneEventByKey(GlobalConfig.SuccessEvent, {Force = win_force})
    StartZoneEventByKey(GlobalConfig.FailEvent, {Force = GetEnemyForce(win_force)})
    -- 参与结算
    local members = {}
    for _, v in pairs(player_map) do
        table.insert(members, v)
    end
    for _, v in pairs(other_player_map) do
        table.insert(members, v)
    end
    table.sort(
        members,
        function(m1, m2)
            return m1.score > m2.score
        end
    )
    local result = {members = members, win_force = win_force}
    -- pprint('result ', result)
    local function CalcRankReward(uuid, index)
        local info = Api.FindFirstExcelData('guild/guild_fort.xlsx/rank_reward', index)
        if info then
            local PlayerApi = Api.GetPlayerApi(uuid)
            if other_player_map[uuid] then
                PlayerApi.SendMessageToClient(uuid, 'guildwall-result', result)
            end
            PlayerApi.CommonReward(info.reward, nil, 'guild_fort_rank')
        end
    end
    for i, v in ipairs(members) do
        Api.Task.AddEvent(CalcRankReward, v.uuid, i)
        if player_map[v.uuid] then
            Api.SendMessageToClient(v.uuid, 'guildwall-result', result)
        end
    end
    is_gameover = true
    Api.Task.Sleep(20)
    Api.SetGameOver(force, reason)
end

local function IsTowerAllDead(force, key)
    local towers = current_config[force].Tower[key]
    for _, v in ipairs(towers) do
        if not v.dead then
            return false
        end
    end
    return true
end

local function CheckTowerDead(unitid)
    if not current_config.MainZone then
        return
    end
    local redTower = current_config[RedForce].Tower
    local blueTower = current_config[BlueForce].Tower
    local all_tower = {redTower, blueTower}
    for _, towers in ipairs(all_tower) do
        for key, v in pairs(towers) do
            for i, vv in ipairs(v) do
                if vv.id == unitid then
                    return vv, key
                end
            end
        end
    end
    return
end

local function CheckHelperMonsterDead(unitid, attack_force)
    if current_config.MainZone then
        return
    end
    for _, v in ipairs(current_config.HelpMonsters) do
        if v.id == unitid then
            return v
        end
    end
    return
end

local function AddScore(force, val, res, playeruuid, playerscore, kill_player)
    if is_gameover then
        return
    end
    local sdata = sync_data[force]
    if not sdata then
        return
    end
    res = res or 0
    val = val or 0
    sdata.score = sdata.score + val
    sdata.resource = sdata.resource + res
    if playeruuid and playerscore then
        player_map[playeruuid].score = player_map[playeruuid].score + playerscore
        if kill_player then
            player_map[playeruuid].kill_count = player_map[playeruuid].kill_count + 1
        end
        SendToOtherZone(
            'guildwall-memberscore',
            {
                uuid = playeruuid,
                score = player_map[playeruuid].score,
                kill_count = player_map[playeruuid].kill_count
            }
        )
    end
    if res > 0 then
        StartZoneEventByKey(GlobalConfig.PickResourceEvent, {Force = force, PlayerUUID = playeruuid}, res)
    end
    SendToOtherZone('guildwall-sync', {force = force, score = val, resource = res})
end

local function GetResource(force)
    local sdata = sync_data[force]
    return sdata.resource
end

local function RepairSourceLogic(help_monster)
    Api.Listen.PlayerPickedItem(
        help_monster.item_id,
        function(uuid, item_id)
            local info = player_map[uuid]
            AddScore(info.force, 1, current_config.PickAddResource, uuid, 3)
        end
    )
    Api.Task.WaitAlways()
end

local function OnUnitDead(unitid, attack_id)
    if is_gameover then
        return
    end
    local attack_uuid = Api.GetPlayerUUID(attack_id)
    local attack_force = Api.GetUnitForce(attack_id)
    local force = GetEnemyForce(attack_force)
    local isplayer = Api.IsPlayer(unitid)
    local uname = Api.GetUnitName(unitid)
    -- print('onunitdead ', uname)
    if isplayer then
        AddScore(attack_force, 1, 0, attack_uuid, 10, true)
    elseif current_config.MainZone and current_config[force].Boss.id == unitid then
        Api.Task.AddEventTo(ID, OnGameOver, attack_force, 'killed')
    else
        local tower, tower_key = CheckTowerDead(unitid)
        if tower then
            tower.dead = true
            if tower.bind_event_id then
                Api.Task.StopEvent(tower.bind_event_id)
                tower.bind_event_id = nil
            end
            if tower.item_id then
                Api.RemoveObject(tower.item_id)
            end
            AddScore(attack_force, 30, 0, attack_uuid, 30)

            StartZoneEventByKey('message.125', {Force = attack_force}, uname)
            StartZoneEventByKey('message.126', {Force = force}, uname)
            if IsTowerAllDead(force, tower_key) then
                current_config[force].Boss.invincible = false
                Api.SetUnitInvincibleMS(current_config[force].Boss.id, 0)
            end
        end

        local help_monster = CheckHelperMonsterDead(unitid, attack_force)
        if help_monster then
            AddScore(attack_force, 50, 0, attack_uuid, 50)
            SendToOtherZone('guildwall-monster-birth', {force = attack_force, TemplateID = help_monster.BirthTemplateID})
            StartZoneEventByKey('message.127', {Force = attack_force}, uname)
            StartZoneEventByKey('message.128', {Force = force}, uname)
            if help_monster.PickTemplateID then
                local x, y = Api.GetObjectPosition(unitid)
                local iinfo = {TemplateID = help_monster.PickTemplateID, X = x, Y = y}
                help_monster.item_id = Api.AddItem(iinfo)
                help_monster.item_event = Api.Task.AddEventTo(ID, RepairSourceLogic, help_monster)
            end
            if help_monster.BuffID then
                SendToOtherZone('guildwall-addbuff', {buff_id = help_monster.BuffID, force = attack_force})
            end
        end
    end
end

local function DoClientStart(uuid, parentid)
    local client_syncinfo = {
        sync_data = sync_data,
        main = current_config.MainZone,
        start_time = start_time_sec,
        max_running_sec = GlobalConfig.MaxRunningSec,
        time = os.time()
    }
    parentid = parentid or player_enter_events[uuid]
    Api.Task.StartEventByKey('client.guild_wall', Api.GetNextArg(arg, {PlayerUUID = uuid}), client_syncinfo)
    Api.Task.AddEventTo(
        parentid,
        function()
            Api.Task.Sleep(1.5)
            local off_time = os.time() - start_time_sec
            local openairsec = GlobalConfig.OpenAirWallSec
            if off_time < openairsec then
                local countdown_sec = math.floor(openairsec - off_time)
                local id = Api.Task.AddEventToByKey(parentid, current_config.AirMessageEvent, Api.GetNextArg(arg, {PlayerUUID = uuid, TimeSec = countdown_sec}))
                table.insert(air_messages, id)
            end
        end
    )
end

local function OnZoneInit()
    if current_config.MainZone then
        -- 生成防御塔
        local redTower = current_config[RedForce].Tower
        local blueTower = current_config[BlueForce].Tower
        local all_tower = {[RedForce] = redTower, [BlueForce] = blueTower}
        for force, towers in pairs(all_tower) do
            for key, v in pairs(towers) do
                for _, vv in ipairs(v) do
                    local x, y = Api.GetFlagPosition(vv.Flag)
                    local uinfo = {TemplateID = vv.TemplateID, Force = force, X = x, Y = y}
                    vv.id = Api.AddUnit(uinfo)

                    local lastTowerTime = os.time()
                    local uname = Api.GetUnitName(vv.id)
                    Api.Listen.UnitDamage(
                        vv.id,
                        function()
                            if os.time() - lastTowerTime > 10 then
                                lastTowerTime = os.time()
                                StartZoneEventByKey('message.144', {Force = force}, uname)
                            end
                        end
                    )

                    if vv.BindEvent then
                        vv.bind_event_id = Api.Task.AddEventToByKey(ID, vv.BindEvent, Api.GetArg())
                    end

                    -- 修理道具
                    local iinfo = {TemplateID = current_config.RepairItemTemplateID, Force = force, X = x, Y = y}
                    vv.item_id = Api.AddItem(iinfo)
                    Api.Listen.PlayerTryPickItem(
                        vv.item_id,
                        function(uuid)
                            local info = player_map[uuid]
                            local cur_res = GetResource(info.force)
                            local ok = cur_res >= current_config.RepairResource
                            if not ok then
                                Api.Task.AddEventToByKey(ID, current_config.RepairWarnEvent, Api.GetArg({PlayerUUID = uuid}), current_config.RepairResource)
                            end
                            local hpinfo = Api.GetUnitHPData(vv.id)
                            if hpinfo.HPPct > 98 then
                                ok = false
                            end
                            return ok
                        end
                    )
                    Api.Listen.PlayerPickedItem(
                        vv.item_id,
                        function(uuid, item_id)
                            -- 添加修复buff
                            -- StartZoneEventByKey(current_config.RepairBuffEvent, {PlayerUUID = uuid})
                            Api.UnitAddBuff(vv.id, current_config.RepairBuffID)
                            AddScore(force, 0, -current_config.RepairResource)
                            Api.Task.AddEventToByKey(ID, current_config.RepairSuccessEvent, Api.GetArg({PlayerUUID = uuid}))
                        end
                    )
                end
            end
        end
        -- 神兽
        for guild_uuid, v in pairs(guild_map) do
            local bossInfo = current_config[v.force].Boss
            local monster_tid = GuildApi.GetWallMonsterTemplateID(guild_uuid)
            local x, y = Api.GetFlagPosition(bossInfo.Flag)
            local uinfo = {TemplateID = monster_tid, Force = v.force, X = x, Y = y}
            bossInfo.id = Api.AddUnit(uinfo)
            local lastDamageTime = os.time()
            Api.Listen.UnitDamage(
                bossInfo.id,
                function()
                    if os.time() - lastDamageTime > 5 then
                        StartZoneEventByKey('message.162', {Force = v.force})
                        lastDamageTime = os.time()
                    end
                end
            )

            local lastnvincibleTime = os.time()
            Api.Listen.UnitEnterRound(x, y, 10, function(unitid)
                local force = Api.GetUnitForce(unitid)
                if bossInfo.invincible and v.force ~= force and os.time() - lastnvincibleTime > 10 then
                    StartZoneEventByKey('message.163', {Force = GetEnemyForce(v.force)})
                    lastnvincibleTime = os.time()
                end
            end)
            bossInfo.invincible = true
            Api.SetUnitInvincibleMS(bossInfo.id, GlobalConfig.MaxRunningSec * 1000)
        end
    else
        -- 生成副战场神兽
        for _, v in ipairs(current_config.HelpMonsters) do
            local x, y = Api.GetFlagPosition(v.Flag)
            local monster_templateid = v.TemplateID
            local uinfo = {TemplateID = monster_templateid, X = x, Y = y, Force = 1, Direction = v.Direction}
            v.id = Api.AddUnit(uinfo)
        end
    end

    -- 其他战场传送门
    local trans = {current_config[RedForce].OtherZoneTransferEvent, current_config[BlueForce].OtherZoneTransferEvent}
    for _, v in ipairs(trans) do
        local transarg = Api.GetArg({TargetZoneUUID = GetOtherZoneUUID()})
        pprint('transarg ', transarg)
        Api.Task.AddEventToByKey(ID, v, transarg)
    end
    Api.Task.WaitAlways()
end

local function OnPlayerOnEnter(uuid, guildUUID, cforce)
    if not guild_map then
        guild_map = {}
        -- 获取当前公会信息和敌对公会信息
        GuildApi = Api.GetGuildApi(uuid)
        -- 向公会服注册

        guild_map[guildUUID] = {name = Api.GetPlayerGuildName(uuid), force = cforce, members = {}, guild_uuid = guildUUID}

        local enemyUUID, enemyName
        if not SINGLE_DEBUG then
            enemyUUID, enemyName = GuildApi.GetWallEnemy(guildUUID)
        else
            enemyUUID, enemyName = 'kfjowej432j4', '测试公会'
        end
        guild_map[enemyUUID] = {name = enemyName, members = {}, guild_uuid = enemyUUID, force = cforce == RedForce and BlueForce or RedForce}
        GuildApi.RegisterWallZone(guildUUID, enemyUUID, cforce, GetEnemyForce(cforce), current_config.MapTemplateID, Api.GetZoneUUID())
        Api.Task.AddEventTo(ID, OnZoneInit)
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
    local cforce = Api.GetPlayerForce(uuid)
    player_map[uuid] = {score = 0, kill_count = 0, guild_uuid = guildUUID, uuid = uuid, force = cforce}
    player_enter_events[uuid] = Api.Task.AddEventTo(ID, OnPlayerOnEnter, uuid, guildUUID, cforce)
    SendToOtherZone('guildwall-memberin', player_map[uuid])
end

local function OnPlayerLeaveZone(uuid)
    local guildUUID = player_map[uuid].guild_uuid
    local function DoLeaveEvent()
        GuildApi.NotifyLeaveWallZone(guildUUID, uuid, current_config.MapTemplateID)
    end
    Api.Task.AddEventTo(ID, DoLeaveEvent)
    local members = guild_map[guildUUID].members
    for i, v in ipairs(members) do
        if v.uuid == uuid then
            table.remove(members, i)
            break
        end
    end
    player_map[uuid] = nil
    SendToOtherZone('guildwall-memberout', {uuid = uuid})
    Api.Task.StopEvent(player_enter_events[uuid])
end

function main()
    local allplayers = Api.GetAllPlayers()
    for _, v in ipairs(allplayers) do
        OnPlayerEnterZone(v)
    end
    Api.Listen.PlayerEnterZone(OnPlayerEnterZone)
    Api.Listen.PlayerLeaveZone(OnPlayerLeaveZone)
    Api.Listen.SessionReconnect(DoClientStart)
    Api.Listen.Message(OnReciveMessage)
    Api.Listen.AddPeriodicSec(2, SyncClientDataTick)
    Api.Listen.UnitDead(OnUnitDead)
    start_time_sec = os.time()
    local max_close_time = start_time_sec + GlobalConfig.MaxRunningSec
    local dt = Api.DateTime(max_close_time)
    Api.SetSceneExpireDateTime(dt)

    current_config = GlobalConfig[Api.GetMapTemplateID()]
    for _, v in pairs(current_config.AirFlag) do
        Api.OpenFlag(v)
    end
    local airsec = GlobalConfig.OpenAirWallSec
    for i = 1, airsec do
        if os.time() - start_time_sec > GlobalConfig.OpenAirWallSec then
            break
        end
        Api.Task.Sleep(1)
        airsec = airsec - 1
    end
    for _, v in ipairs(air_messages) do
        Api.Task.StopEvent(v)
    end
    air_messages = {}
    for _, v in pairs(current_config.AirFlag) do
        Api.CloseFlag(v)
    end
    Api.Task.Sleep(GlobalConfig.MaxRunningSec - GlobalConfig.OpenAirWallSec - 1)
    if current_config.MainZone or not zone_info then
        OnGameOver(nil, 'timeout')
    else
        Api.Task.Sleep(60)
    end
end
