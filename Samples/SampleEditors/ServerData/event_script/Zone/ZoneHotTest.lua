function main()
    -- Api.ReStart()
    -- do return end

    print('HotTest Zone reload')
    local eff2 = {
        Name = '/res/effect/ef_task_circle02.assetbundles',
        IsLoop = true,
        EffectTimeMS = 10000,
        ScaleToBodySize = 50
    }
    -- Api.PlaySceneEffect(eff2,171,163,1)
    local allPlayer = Api.GetAllPlayers()
    for _, v in ipairs(allPlayer) do
        local objId = Api.GetPlayerObjectID(v)
        print('vvvvv', v, objId)
        local x, y = Api.GetObjectPosition(objId)
        local arg = {PlayerUUID = v, ZoneUUID = Api.GetZoneUUID(), ClientUUID = Api.GetSession(v)}
        local PlayerApi = Api.GetPlayerApi(arg)
        local params = {
            -- MapTemplateID = 500050,
            -- MapTemplateID = 220010,
            MapTemplateID = 100020,
            -- MapTemplateID = 100010,
            -- MapTemplateID = 2100101,
            -- MapTemplateID = 100030,
            PlayerUUID = arg.PlayerUUID,
            TimeoutMS = 2500,
            MapName = '测试下大家一起进'
            -- Players = allPlayer,
            -- X = 39,

            -- Y = 72
        }

        -- Api.SetPlayerPosition(v, 82,126)
        -- params.Players = PlayerApi.GetTeamMembers()
        -- Api.Task.TransportPlayer(params)
        -- local ClientApi = Api.CreateRemoteApi('Client', arg.ClientUUID)
        -- PlayerApi.Task.TransportPlayer(params)
        -- ClientApi.Hello.Task.Fuck('432432')
        -- ClientApi.Task.StartEvent('Client/fade_screen',2)
        -- ClientApi.ShowErrorMessage('你好')
        -- Api.Task.AddEventByKey('client.mythicalbeasts_mode1',arg)
        -- Api.Task.AddEventByKey('group.10001',arg)
        -- Api.Task.AddEventByKey('group.10002',arg)
        -- Api.Task.AddEventByKey('transfer.60001',arg)
        -- Api.Task.AddEventByKey('alchemy.80001',arg)
        -- Api.Task.AddEventByKey('treasure.51002',arg)
        -- Api.Task.AddEventByKey('horse.50001', arg)
        -- Api.Task.StartEventByKey('item_per.20001',arg)

        -- Api.Task.StartEventByKey('item_per.7000110',arg)

        -- Api.Task.StartEventByKey('item_per.7000110',arg)
        -- Api.Task.StartEventByKey('item_per.7000110',arg)
        -- Api.Task.StartEventByKey('item_per.7000110',arg)
        -- Api.Task.AddEventByKey('reward.2',arg)
        -- Api.Task.StartEventByKey('rift_per.7000430',arg)
        -- Api.Task.AddEventByKey('rift_per.7000430',arg)
        -- Api.Task.AddEventByKey('stalker.1',arg)
        -- Api.Task.StartEventByKey('follow.4',arg)
        -- Api.Task.AddEventByKey('scratch.1',arg)
        Api.Task.AddEventByKey('monster_per.30003', arg)
        -- Api.Task.AddEventByKey('client.ClientHotTest',arg)
        -- Api.Task.AddEventByKey('itemshow.1',arg)
        -- Api.Task.AddEventByKey('turntable.1',arg)
        -- Api.Task.AddEventByKey('ninth_palace.1',arg)
        -- Api.Task.AddEventByKey('guildspring',arg)
        --  Api.Task.AddEventByKey('cost.20003',arg)

        -- Api.SetUnitPosition(objId, 39,23)

        -- Api.SendMessage('Player', v, 'hllerwe', {hellp = 'world', 1, 2, 3})
    end

    Api.Task.Wait()
end

