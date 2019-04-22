function main()
    print('---------guildspring.lua Start-----------')

    local in_exp_add = {}

   -- pprint('arg   ',arg)
    local function PlayerEnterLogic(uuid)
        -- 是否在开放时间
        print('player enter logic',uuid)
        if Api.IsFuncOpenTime('enterspring') then
            local mapinfo = {
                MapTemplateID = arg.MapTemplateID,
                PlayerUUID = uuid,
                Flag = 'hot_spring_entrance'
            }
            Api.Task.Wait(Api.Task.TransportPlayer(mapinfo))
        else
            Api.Task.StartEventByKey('message.4',Api.GetArg({PlayerUUID = uuid}))
        end
    end

    local function PlayerOutLogic(uuid)
        local mapinfo = {
            MapTemplateID = arg.MapTemplateID,
            PlayerUUID = uuid,
            Flag = 'hot_spring_exits'
        }
        Api.Task.Wait(Api.Task.TransportPlayer(mapinfo))
    end

    local function StartAddExp( ... )
        local exp_db = Api.FindExcelData('guild/guild_hotspring.xlsx/guild_hotspring_exp', {})
        local addition_db = Api.FindExcelData('guild/guild_hotspring.xlsx/guild_hotspring_addition', {})
        local interval = Api.GetExcelConfig('guild_spring_time')
        while true do
            Api.Task.Sleep(interval)
            -- 计算分值
            local playerCount = table.len(in_exp_add)
            if playerCount > 0 then
                for k, v in pairs(in_exp_add) do
                    local PlayerApi = v
                    PlayerApi.AddExp.NoAutoWait = true
                    local index = Api.GetPlayerLevel(k)
                    index = index > #exp_db and #exp_db or index
                    local baseExp = exp_db[index].exp
                    local additionData = addition_db[playerCount].addition
                    local additionExp = baseExp * (additionData / 10000)
                    PlayerApi.AddExp(additionExp)
                end
            end
        end 
    end

    local function StartAddExpClean()
        for k, v in pairs(in_exp_add) do
            Api.Task.AddEventTo(ID,PlayerOutLogic, k)
        end
        ExpEventID = nil
    end

    local function GetClientSyncInfoArgs( ... )
        local args = nil
        local playerCount = table.len(in_exp_add)
        if playerCount > 0 then
            local addition_db = Api.FindExcelData('guild/guild_hotspring.xlsx/guild_hotspring_addition', {})
            local additionData = addition_db[playerCount]
            local additionValue = additionData.addition / 10000 * 100
            args = { playerCount = playerCount, additionExp = additionValue }
        end
        return args
    end

    local function ClientSyncInfo( ... )
        local args = GetClientSyncInfoArgs()
        if args then
            for k, v in pairs(in_exp_add) do
                if Api.IsPlayerReady(k) then
                    Api.SendMessageToClient(k, 'guildspring.info', args)
                end
            end
        end
    end

    Api.Listen.Message('guildspring.enter',function(ename, params, source, uuid)
        print('---------client request enter--------------', source,uuid)
        if source ~= 'Client' then
            return
        end
        Api.Task.AddEventTo(ID, PlayerEnterLogic, uuid)
    end)

    Api.Listen.Message('guildspring.out',function(ename, params, source, uuid)
        print('------------client request out-----------', source,uuid)
        if source ~= 'Client' then
            return
        end
        Api.Task.AddEventTo(ID, PlayerOutLogic, uuid)
    end)

    local function StartClient(uuid)
        local args = GetClientSyncInfoArgs()
        Api.Task.AddEventToByKey(ID,'client.guildspringInfo', Api.GetArg({ClientUUID = Api.GetSession(uuid)}), args)
        in_exp_add[uuid].SetIntFlag('EnterGuildSpring', 1, true)
    end

    local function ReadyToStartClient(uuid)
        Api.Task.Wait(Api.Listen.PlayerReady(uuid))
        StartClient(uuid)
    end
    
    Api.Listen.PlayerEnterRegion('hot_spring_area', function(uuid)
        print('------------PlayerEnterRegion-----------', uuid)
        in_exp_add[uuid] = Api.GetPlayerApi(uuid)
        --进入温泉区域，启动客户端脚本
        if Api.IsPlayerReady(uuid) then
            Api.Task.AddEventTo(ID,StartClient, uuid)
        else
            Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        end

        --启动服务端定时器
        if not ExpEventID then
            ExpEventID = Api.Task.AddEventTo(ID,{main =StartAddExp,clean = StartAddExpClean})
            Api.Task.AddEventTo(ID,function()
                Api.Task.Wait(Api.Task.FuncOpening('enterspring'))
                Api.Task.StopEvent(ExpEventID)
            end)
        end
        ClientSyncInfo()
    end)
    
    Api.Listen.PlayerLeaveRegion('hot_spring_area', function(uuid)
        print('------------PlayerLeaveRegion-----------', uuid)
        in_exp_add[uuid] = nil
        --离开温泉区域，销毁客户端脚本
        Api.SendMessageToClient(uuid, 'guildspring.end', {})
        ClientSyncInfo()
    end)
    
    Api.Listen.SessionReconnect(function(uuid)
        print('------------SessionReconnect-----------', uuid)
        --断线重连，如果还在温泉区域，重新启动客户端脚本
        if in_exp_add[uuid] ~= nil then
            if Api.IsPlayerReady(uuid) then
                Api.Task.AddEventTo(ID,StartClient, uuid)
            else
                Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
            end
        end
    end)
   
    
    Api.Task.WaitAlways()
end



--3种
-- 同步 Api.
-- 任务类型；返回ID，Api.Task.Wait(ID) 等待任务结束返回结果 
-- 监听