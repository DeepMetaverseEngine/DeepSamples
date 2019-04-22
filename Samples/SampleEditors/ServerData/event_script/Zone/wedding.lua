local CarID
local flags = {'jhlx_1','jhlx_2','jhlx_3','jhlx_4','jhlx_5','jhlx_6','jhlx_7','jhlx_8','jhlx_9','jhlx_10','jhlx_11','jhlx_12','jhlx_13','jhlx_14'}
 local PlayerApis = {}
function main()
	pprint('wedding -----', arg)
	local info = {TemplateID = 32206, Force = 0, X = 45, Y = 152, Speed = 4}
    CarID = Api.AddUnit(info)
    assert(CarID)

    local currentFlagIndex = 0
    local targetWays
    local targetFlag
    local last_car_x, last_car_y
    PlayerApis[1] = Api.GetPlayerApi(arg.PlayerUUID)
   	PlayerApis[2] = Api.GetPlayerApi(arg.TargetPlayerUUID)

   	for _,papi in ipairs(PlayerApis) do
   		papi.PlayerLockTransport.NoAutoWait = true 
   		papi.PlayerLockTransport(600) 
   	end
    local function GetNextPosition(cx, cy)
        currentFlagIndex = currentFlagIndex + 1
        targetFlag = flags[currentFlagIndex]

        print('GetNextPosition',targetFlag)
        if string.IsNullOrEmpty(targetFlag) then
            --Api.SendMessageToClient(arg.PlayerUUID,'carriage_success')

            -- Api.Task.StartEventByKey('item_pub.25100')
            Api.Task.AddEventTo(ID,function()
	           	local transids = {}
			   	for _,papi in ipairs(PlayerApis) do
			   		transids[#transids+1] = papi.Task.TransportPlayer({MapTemplateID=500400})
			   	end
			   	Api.Task.WaitParallel(transids)
	            Api.Task.StopEvent(ID, true)
            end)

            return false
        else
            local targetX, targetY = Api.GetFlagPosition(targetFlag)
            -- targetWays = Api.FindPath(cx, cy, targetX, targetY)
            targetWays = {{x=targetX, y=targetY}}
            return true
        end
    end

	local function CheckPostion()
        if not CarID or not Api.IsExistObject(CarID) or not targetWays or not targetWays[1] then
            return
        end
        local nx, ny = Api.GetObjectPosition(CarID)
        if Api.IsPlayerExist(arg.PlayerUUID) then
        	Api.SetPlayerPosition(arg.PlayerUUID, nx,ny)
        end
        if Api.IsPlayerExist(arg.TargetPlayerUUID) then
        	Api.SetPlayerPosition(arg.TargetPlayerUUID, nx,ny)
        end
        last_car_x, last_car_y = nx,ny
        local ok = Api.UnitMoveTo(CarID, targetWays[1].x, targetWays[1].y)
        if ok then
            table.remove(targetWays, 1)
            if #targetWays == 0 and GetNextPosition(nx, ny) then
                CheckPostion()
            end
        end
    end


    local function StartClient(uuid)
    	-- print('cccccccccccccccccc', CarID)
    	local husbandId = arg.PlayerUUID
    	local wifeId = arg.TargetPlayerUUID
    	local clientargs = { wType = 'wedding', CarID = CarID, husband = husbandId, wife = wifeId, husbandOId = Api.GetPlayerObjectID(husbandId), wifeOId = Api.GetPlayerObjectID(wifeId) }
        Api.Task.AddEventToByKey(ID,'client.wedding', Api.GetArg({ClientUUID = Api.GetSession(uuid)}), clientargs )
    end

    local function ReadyToStartClient(uuid)
        Api.Task.Wait(Api.Listen.PlayerReady(uuid))
        StartClient(uuid)
    end
    
    Api.Listen.PlayerEnterZone(function(uuid)
        print('------------PlayerEnterZone-----------', uuid)
        --进入場景，启动客户端脚本
        if Api.IsPlayerReady(uuid) then
            Api.Task.AddEventTo(ID,StartClient, uuid)
        else
            Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        end
    end)
    
    Api.Listen.PlayerLeaveZone(function(uuid)
        print('------------PlayerLeaveZone-----------', uuid)
        --离开温泉区域，销毁客户端脚本
        Api.SendMessageToClient(uuid, 'wedding.end', {})
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

    local allplayers = Api.GetAllPlayers()
    for _, uuid in ipairs(allplayers or {}) do
        if Api.IsPlayerReady(uuid) then
            Api.Task.AddEventTo(ID,StartClient, uuid)
        else
            Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        end
    end

    GetNextPosition(info.X, info.Y)
    Api.Listen.AddPeriodicSec(0.5, CheckPostion)
    Api.Task.WaitAlways()
end

function clean()
	Api.RemoveObject(CarID)
    CarID = nil
    for _,papi in ipairs(PlayerApis) do
    	
    	papi.PlayerLockTransport(0) 
    end
end