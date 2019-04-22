local players = {}
local function PlayerEnterZone(uuid)
    local x, y = Api.GetFlagPosition('star')
    Api.SetPlayerPosition(uuid, x, y)
    -- local ClientApi = Api.GetClientApi(uuid)
    Api.Task.PlayerAoiWork(uuid)
    Api.SetPlayerDirection(uuid, 4.5)
    Api.AddPlayerHpPercent(uuid, 100)
    Api.Task.Sleep(0.6)
    Api.Task.StartEventByKey('client.firstmap_init', Api.GetArg({PlayerUUID = uuid}))
    Api.Task.Sleep(2)
    local pro = Api.GetPlayerPro(uuid)
    local gender = Api.GetPlayerGender(uuid)
    local buffids = {
        {61005, 61013},
        {61006, 61014},
        {61007, 61015},
        {61008, 61016}
    }
    Api.PlayerAddBuff(uuid, buffids[pro][gender+1])
    -- Api.PlayerAddBuff(uuid, buffids[pro][2])
    local monster = Api.AddUnit({TemplateID = 21070, Flag = 'Pr1', PlayerUUID = uuid})
    players[uuid].monster = monster
    Api.Task.WaitAlways()
end

local function PlayerEnterRegion(uuid)
    local ClientApi = Api.GetClientApi(uuid)
    local tj = Api.AddUnit({TemplateID = 21100, Flag = 'tianjiang1', Direction = 1.56, PlayerUUID = uuid})
    local tj1 = Api.AddUnit({TemplateID = 21110, Flag = 'tianbing1', Direction = 1.56, PlayerUUID = uuid})
    local tj2 = Api.AddUnit({TemplateID = 21110, Flag = 'tianbing2', Direction = 1.56, PlayerUUID = uuid})
    local tj3 = Api.AddUnit({TemplateID = 21110, Flag = 'tianbing3', Direction = 1.56, PlayerUUID = uuid})
    local tj4 = Api.AddUnit({TemplateID = 21110, Flag = 'tianbing4', Direction = 1.56, PlayerUUID = uuid})
    --ClientApi.PauseBGM()
    --ClientApi.Task.PlayCG('dungen100000_3',false)
    Api.Task.Wait(ClientApi.Task.PlayCG('dungen100000_3',false))
	Api.Task.StartEventByKey('client.Guide/guide_playuseattack', Api.GetArg({PlayerUUID = uuid}))
    -- 等待tj死亡
    Api.Task.Wait(Api.Listen.UnitDead(tj))

    Api.RemoveObject(tj1)
    Api.RemoveObject(tj2)
    Api.RemoveObject(tj3)
    Api.RemoveObject(tj4)
    --ClientApi.PauseBGM()
    local bosschuchang = ClientApi.Task.PlayCG('dungen100000_4',false)
    players[uuid].dead_effect = true
    Api.Task.StartEventByKey('client.Design/fubencamera3', Api.GetArg({PlayerUUID = uuid}))
    Api.Task.Sleep(0.3)
    Api.Task.StartEventByKey('client.Design/qiao_dead', Api.GetArg({PlayerUUID = uuid}))
	
    Api.Task.Wait(bosschuchang)	
	
	local boss = Api.AddUnit({TemplateID = 21108, Flag = 'tengshe', PlayerUUID = uuid, Direction = 1.56})
    --等待boss死亡
    Api.Task.Wait(Api.Listen.UnitDead(boss))
    -- Api.Task.Sleep(38)
    ClientApi.PauseBGM()
    local id1 = ClientApi.Task.PlayCG('dungen100000_5',false)
    local id2 = Api.Task.DelaySec(48)
    Api.Task.WaitAny(id1, id2)
    Api.Task.StartEventByKey('transfer.2', Api.GetArg({PlayerUUID = uuid}))
end

function main()
    local all = Api.GetAllPlayers()
    for _, uuid in ipairs(all) do
        players[uuid] = players[uuid] or {}
        players[uuid].main = Api.Task.AddEventTo(ID, PlayerEnterZone, uuid)
    end
    Api.Listen.PlayerEnterZone(
        function(uuid)
            -- 玩家进入场景
            players[uuid] = players[uuid] or {}
            players[uuid].main = Api.Task.AddEventTo(ID, PlayerEnterZone, uuid)
        end
    )
    Api.Listen.PlayerLeaveZone(
        function(uuid)
            -- 玩家离开场景
            local info = players[uuid]
            Api.Task.StopEvent(info.main)
            players[uuid] = nil
        end
    )

    Api.Listen.SessionReconnect(
        function(uuid)
            -- 玩家重连进入游戏
            Api.Task.StartEventByKey('client.firstmap_init', Api.GetArg({PlayerUUID = uuid}), true, players[uuid].dead_effect)
        end
    )

    Api.Listen.PlayerEnterRegion(
        'trigger_plot',
        function(uuid)
            local info = players[uuid]
            if not info or info.region then
                return
            end
            local player_eid = info.main
            info.region = Api.Task.AddEventTo(player_eid, PlayerEnterRegion, uuid)
            --kill 21070
            Api.RemoveObject(info.monster)
        end
    )
    Api.Task.WaitAlways()
end
