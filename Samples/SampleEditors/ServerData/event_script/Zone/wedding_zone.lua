function main()

    local function StartClient(uuid)
    	-- print('cccccccccccccccccc', CarID)
    	local husbandId = arg.PlayerUUID
    	local wifeId = arg.TargetPlayerUUID
    	local clientargs = { wType = 'dungen' }
        Api.Task.AddEventToByKey(ID,'client.wedding', Api.GetArg({ClientUUID = Api.GetSession(uuid)}), clientargs )
    end

    local function ReadyToStartClient(uuid)
        Api.Task.Wait(Api.Listen.PlayerReady(uuid))
        StartClient(uuid)
    end

	Api.Listen.PlayerEnterZone(function(uuid)
       if Api.IsPlayerReady(uuid) then
            Api.Task.AddEventTo(ID,StartClient, uuid)
        else
            Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        end
	end)
    
    Api.Listen.SessionReconnect(function(uuid)
        print('------------SessionReconnect-----------', uuid)
        --断线重连，如果还在区域，重新启动客户端脚本
        if Api.IsPlayerReady(uuid) then
            Api.Task.AddEventTo(ID,StartClient, uuid)
        else
            Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        end
    end)
    Api.Task.WaitAlways()

end