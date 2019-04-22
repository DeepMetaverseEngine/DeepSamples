local excel_data
local all_players = {}
local force_scores = {}
local IsGameOver
local listen_events = {}
local start_time
local function SendMessageToClient(...)
    for uuid, v in pairs(all_players) do
        if v.state ~= 'Offline' then
            Api.SendMessageToClient(uuid, ...)
        end
    end
end
local function StartClient(uuid, force, clientarg)
    local pass = os.time() - start_time
    local players
    local limittime = excel_data.battlefield_time
    if excel_data.air_flag_time - pass < 0 then
        limittime = limittime + (excel_data.air_flag_time - pass)
        players = {}
        for uuid, v in pairs(all_players) do
            players[v.force] = players[v.force] or {}
            table.insert(players[v.force], {uuid = uuid, name = v.name})
        end
    end
    local syncdata = {
        left_time = limittime,
        wait_time = excel_data.air_flag_time - pass,
        players = players,
        scores = force_scores,
        start_time = start_time,
        now_time = os.time(),
    }
    Api.Task.AddEventToByKey(ID, excel_data.player_ready_event, Api.GetNextArg(clientarg), excel_data.id, syncdata)
end

local function ListenReady(uuid, force, clientarg)
    Api.Task.Wait(Api.Listen.PlayerReady(uuid))
    pprint('listen ready', uuid, force, clientarg)
    StartClient(uuid, force, clientarg)
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
                playerApi.AddIntFlag('Enter' .. excel_data.function_id .. 'Count', 1, true)
                playerApi.CheckPvpRewardRedTips()
            end
        )
    end
    local force = Api.GetPlayerForce(uuid)
    force_scores[force] = force_scores[force] or {score = 0, kill_count = 0, force = force}
    all_players[uuid] = all_players[uuid] or {}
    local info = all_players[uuid]
    info.kill_count = info.kill_count or 0
    info.name = Api.GetPlayerName(uuid)
    info.arg = Api.GetArg({PlayerUUID = uuid})
    info.object_id = Api.GetPlayerObjectID(uuid)
    info.state = 'Normal'
    info.force = force
    info.uuid = uuid
    if Api.IsPlayerReady(uuid) then
        Api.Task.AddEventTo(ID, StartClient, uuid, force, info.arg)
    else
        Api.Task.AddEventTo(ID, ListenReady, uuid, force, info.arg)
    end
end

local function OnGameOver(force, reason)
    if IsGameOver then
        return
    end
    local exploitdata = Api.FindFirstExcelData('pvp/pvp.xlsx/pvp', {map_id = arg.MapTemplateID})
    local fscores = table.values(force_scores)
    if not force then
        table.sort(
            fscores,
            function(x, y)
                if x.score == y.score then
                    return x.kill_count > y.kill_count
                else
                    return x.score > y.score
                end
            end
        )
        if exploitdata.pvp_type == 1 and fscores[1].force == fscores[2].force and fscores[1].kill_count == fscores[2].kill_count then
            force = 0
        else
            force = fscores[1].force
        end
    end
    IsGameOver = true
    for _, v in ipairs(listen_events) do
        Api.Task.StopEvent(v)
    end
    local players = table.values(all_players)
    for _, v in ipairs(players) do
        v.force_score = force_scores[v.force].score
        if force == v.force then
            v.win_flag = 1
        elseif force == 0 then
            v.win_flag = 0
        else
            v.win_flag = -1
        end
    end

    table.sort(
        players,
        function(p1, p2)
            if p1.win_flag ~= p2.win_flag then
                return p1.win_flag > p2.win_flag
            end
            if p1.force_score ~= p2.force_score then
                return p1.force_score > p2.force_score
            end
            return p1.kill_count > p2.kill_count
        end
    )
    local result_players = {}
    local sub_events = {}
    local function CalcPlayerExploit(pinfo, index)
        local exploit
        local win
        if exploitdata.pvp_type == 1 then
            if pinfo.win_flag == 1 then
                exploit = exploitdata.victory_value
            elseif pinfo.win_flag == 0 then
                exploit = exploitdata.draw_value
            else
                exploit = exploitdata.fail_value
            end
            win = pinfo.win_flag
        else
            if index < 3 then
                exploit = exploitdata.ranking_award[index]
                win = index
            else
                exploit = exploitdata.ranking_award[3]
                win = 3
            end
        end
        Api.PlayerRebirthOrgin(pinfo.uuid)
        local playerApi = Api.GetPlayerApi(pinfo.arg)
        local today_exploit = playerApi.SetPvpResult(excel_data.function_id, win, exploit)
        pinfo.today_exploit = today_exploit
        pinfo.exploit = exploit
        table.insert(result_players, pinfo)
    end

    for i, v in ipairs(players) do
        table.insert(sub_events, Api.Task.AddEvent(CalcPlayerExploit, v, i))
    end
    Api.Task.WaitParallel(sub_events)
    local gameover_ret = {force = force, reason = reason, scores = force_scores, players = result_players}
    SendMessageToClient('pvp_common.gameover', gameover_ret)
    Api.Task.Sleep(20)
    Api.SetGameOver(force, reason)
end

local function AddScore(force, score)
    force_scores[force] = force_scores[force] or {score = 0, kill_count = 0, force = force}
    local next_score = force_scores[force].score + score
    force_scores[force].score = next_score
    if next_score >= excel_data.win_score then
        --胜利
        Api.Task.AddEventTo(ID, OnGameOver, force, 'score > win_score')
    end
    SendMessageToClient('pvp_common.res', {[force] = force_scores[force]})
end

local function AddKill(killer, kill_count)
    local pinfo = all_players[killer]
    local force = pinfo.force
    kill_count = kill_count or 1
    force_scores[force] = force_scores[force] or {score = 0, kill_count = 0, force = force}
    local next_count = force_scores[force].kill_count + kill_count
    force_scores[force].kill_count = next_count
    pinfo.kill_count = pinfo.kill_count + kill_count
end

local function PlayerLeaveLogic(uuid)
    local info = all_players[uuid]
    if info then
        info.state = 'Offline'
    end
    if not string.IsNullOrEmpty(excel_data.offline_event) then
        local narg = Api.GetNextArg(info.arg)
        Api.Task.AddEventToByKey(ID, excel_data.offline_event, narg)
    end
    -- 检测阵营剩余数量
    local fp_count = {}
    for _, v in pairs(all_players) do
        if v.state ~= 'Offline' then
            fp_count[v.force] = fp_count[v.force] or 0
            fp_count[v.force] = fp_count[v.force] + 1
        end
    end
    local fs = table.keys(fp_count)
    if #fs == 1 then
        -- 只有一个阵营,直接判定胜利
        Api.Task.AddEventTo(ID, OnGameOver, fs[1], 'Online')
    end
end

local function OnReciveMessage(ename, params, src_type, src_uuid)
    -- pprint('OnReciveMessage', ename, params, src_type, src_uuid)
    if src_type == 'Zone' then
        if ename == 'battle_addscore' then
            local pinfo = all_players[params.PlayerUUID]
            local score = tonumber(params.content)
            AddScore(pinfo.force, score)
        elseif ename == 'battle_addforcescore' then
            local force, score = unpack(string.split(params.content, ','))
            force = tonumber(force)
            score = tonumber(score)
            AddScore(force, score)
        elseif ename == 'battle_gameover' then
            local winforce = tonumber(params.content)
            OnGameOver(winforce, 'battle_editor_gameover')
        elseif ename == 'ui_node' then
            SendMessageToClient('ui_node', params.content)
        end
    end
end

local function OnPlayerDead(uuid, attack_id)
    local pinfo = all_players[uuid]
    if not string.IsNullOrEmpty(excel_data.people_dead_event) then
        local narg = Api.GetNextArg(pinfo.arg)
        Api.Task.AddEventToByKey(ID, excel_data.people_dead_event, narg)
    end
    local attack_uuid = Api.GetPlayerUUID(attack_id)
    if attack_uuid then
        local attack_p = all_players[attack_uuid]
        if not string.IsNullOrEmpty(excel_data.kill_people_event) then
            local narg = Api.GetNextArg(attack_p.arg)
            Api.Task.AddEventToByKey(ID, excel_data.kill_people_event, narg)
        end
        if excel_data.kill_people_score ~= 0 then
            AddScore(attack_p.force, excel_data.kill_people_score)
            AddKill(attack_uuid, 1)
        end
    end
end
function main(ele)
    assert(ele)
    excel_data = ele
    table.insert(listen_events, Api.Listen.PlayerEnterZone(PlayerEnterLogic))
    table.insert(listen_events, Api.Listen.PlayerLeaveZone(PlayerLeaveLogic))
    table.insert(listen_events, Api.Listen.SessionReconnect(PlayerEnterLogic, true))
    table.insert(listen_events, Api.Listen.Message(OnReciveMessage))
    table.insert(listen_events, Api.Listen.PlayerDead(OnPlayerDead))
    start_time = os.time()
    for _, v in pairs(excel_data.air_flag) do
        if not string.IsNullOrEmpty(v) then
            Api.OpenFlag(v)
        end
    end
    Api.Task.Sleep(excel_data.air_flag_time)
    for _, v in pairs(excel_data.air_flag) do
        if not string.IsNullOrEmpty(v) then
            Api.CloseFlag(v)
        end
    end
    local players = {}
    for uuid, v in pairs(all_players) do
        players[v.force] = players[v.force] or {}
        table.insert(players[v.force], {uuid = uuid, name = v.name})
    end
    SendMessageToClient('pvp_common.players', players)
    Api.Task.Sleep(excel_data.battlefield_time - 10)
    if not IsGameOver then
        for uuid, v in pairs(all_players) do
            if v.state ~= 'Offline' then
                Api.Task.AddEventByKey('message.185', Api.GetArg({PlayerUUID = uuid}))
            end
        end
        Api.Task.Sleep(10)
        OnGameOver(nil, 'timeout')
    end
end

function clean()
end
