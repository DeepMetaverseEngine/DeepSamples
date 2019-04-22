function main(ele)
    local floor_order = 1
    local floor = nil
    local PlayerApi = nil
    local mapdata = Api.FindExcelData('cptower/cptower.xlsx/cptower', {})
    local mapid = Api.GetNextArg(arg).MapTemplateID
    local countdown = Api.GetExcelConfig('cptower_battletime')
    local maxLayer = #mapdata
    local hasReward = {}
    local isRun = false
    local curTime = 0
    local curDiff 
    local begin = false
    local function SendPos(playerUUID,flag)
            if not string.IsNullOrEmpty(flag) then
            local x,y = Api.GetFlagPosition(flag)
            Api.SetPlayerPosition(playerUUID,x,y)
        end
    end
    local function GetMapData(floor,floor_order)
        local data = Api.FindExcelData('cptower/cptower.xlsx/cptower_monster', {floor = floor,floor_order = floor_order})[1]
        if data == null or next(data) == nil then
            return nil,0
        end
        --pprint("data",data)
        return data,data.difficulty
    end

    --复活已死亡的单位.
    local function RebirthAllPlayers()
        local allPlayer = Api.GetAllPlayers()
        for _, v in ipairs(allPlayer) do
            Api.PlayerRecoveryOrRebirth(v)
        end
    end
  
    local function SendNextWavePos(playerUUID,index,floor,floor_order)
        local data = GetMapData(floor,floor_order)
        if data == nil then
            return
        end
        SendPos(playerUUID,data.startpoint[index])
    end
    local function SendToClientInitTime()
        
        Api.SetEnvironment("CurLayer",floor)
        --Api.SetEnvironment("CurWave",1)
        Api.SetEnvironment("count_down",curTime)
    end
    
    local function EnterPlayer(playerUUID)
            local difficulty = Api.CPTower.GetCPTowerModeByMapId(mapid)
                if floor == nil then
                     local data = Api.FindExcelData('cptower/cptower.xlsx/cptower_monster', {infloor = 1,difficulty = difficulty})[1]
                     floor = data.floor
                end
	        local allPlayer = Api.GetAllPlayers()
	        for i, v in ipairs(allPlayer) do
	            if v == playerUUID then
	                SendNextWavePos(playerUUID,i,floor,floor_order)
	            end
	        end
    end
    
    local function SetPassEvent(playerapi,iswin)
		local nextarg = Api.GetNextArg(arg)
        playerapi.CPDemonTower.CPDemonTowerRewardItem(nextarg.MapTemplateID,floor, iswin)
    end
    local function StartLogic(playerUUID)
        PlayerApi = Api.GetPlayerApi(playerUUID)
        if PlayerApi.HasTeam()then
            local players = PlayerApi.GetTeamMembers()
            Api.Task.WaitAnyPlayerReady(players)
        else
            Api.Task.WaitPlayerReady(playerUUID)
        end
        Api.AddEnvironment("CurLayer", floor)
        Api.AddEnvironment("count_down", curTime)
        ::restart::
        isRun = false
        curTime = countdown
        SendToClientInitTime()
        --print("floor",floor)
        local data, difficulty = GetMapData(floor, 1)
        curDiff = difficulty
        assert(floor,"双人塔层数异常")
        local data = GetMapData(floor,floor_order)
        if data == nil then
            return false
        end
        local allPlayer = Api.GetAllPlayers()
        for _, v in ipairs(allPlayer) do
            Api.Task.StartEventByKey('message.49',Api.GetArg{PlayerUUID = v})
        end
       
        --事件发送需要目标的arg
        --
        isRun = true
        Api.Task.Sleep(3)
        local ids = {}
        for _,v in ipairs(data.event) do
            if not string.IsNullOrEmpty(v) then
                table.insert(ids,Api.Task.AddEventByKey(v))
            end
        end

        local success,result = Api.Task.WaitParallel(ids)
        assert(success)

        --结算
        floor_order = floor_order + 1
        local nextdata = GetMapData(floor,floor_order) --Api.FindExcelData('pagoda/pagoda.xlsx/pagoda_monster', {floor = floor,floor_order = floor_order})
        if nextdata == nil then
            local passtime = curTime
            RebirthAllPlayers()
            if hasReward[floor] == nil then --发奖品
                hasReward[floor] = true

                local allPlayer = Api.GetAllPlayers()
                for _, v in ipairs(allPlayer) do
                    local tempPlayerApi = Api.GetPlayerApi(v)
                    --if tempPlayerApi ~= nil and tempPlayerApi.GetMapTemplateID() == mapid then
                        tempPlayerApi.CPDemonTower.CPRecordPassTime(floor,countdown - passtime,allPlayer) --发送通关时间
                    --end
                end
                
                isRun = false
                local nextfloor =  floor + 1
                local _,difficulty = GetMapData(nextfloor,1)
                if nextfloor > maxLayer or curDiff ~= difficulty then
                    allPlayer = Api.GetAllPlayers()
                    for _, v in ipairs(allPlayer) do
                        local tempPlayerApi = Api.GetPlayerApi(v)
                        Api.Task.StartEventByKey('message.64',Api.GetArg{PlayerUUID = v})
                        SetPassEvent(tempPlayerApi,true)
                    end
                    return true
                else
                    SendToClientInitTime()
                    floor = nextfloor
                    Api.Task.Sleep(1)
                    local isPlayCg = false
                    allPlayer = Api.GetAllPlayers()
                    for i, v in ipairs(allPlayer) do
                        local tempPlayerApi = Api.GetPlayerApi(v)
                        arg.ClientUUID = Api.GetSession(v)
                        for _,k in ipairs(data.success_event) do
                            --print("v",v,floor)
                            if not string.IsNullOrEmpty(k) then
                                Api.Task.AddEventByKey(k,Api.GetNextArg(arg))
                                isPlayCg = true
                            end
                        end
                        SendNextWavePos(v,i,floor,1)
                    end
                    if isPlayCg then
                        Api.Task.Sleep(6)
                    end
                end
            end
            floor_order = 1
        end
        
        goto restart
    end
   
    --Api.Listen.PlayerLeaveZone(function(playerUUID)   
    --     Api.Task.StopEvent(ID)  
    --end)
    --结算
    local function GameOver()
        Api.Task.AddEventTo(ID,function()
            local allPlayer = Api.GetAllPlayers()
            for _, v in ipairs(allPlayer) do
                local tempPlayerApi = Api.GetPlayerApi(v)
                SetPassEvent(tempPlayerApi)
            end
            Api.Task.StopEvent(ID)
        end)
    end
    Api.Listen.PlayerEnterZone(function(playerUUID)
        EnterPlayer(playerUUID)
        if not begin then
            begin = true;
        Api.Task.AddEventTo(ID,StartLogic,playerUUID)
        end
    end)

     Api.Listen.AddPeriodicSec(1,function()
        if isRun then
            curTime = curTime - 1
            curTime = math.max(curTime,0)
            Api.SetEnvironment("count_down",curTime)
            if curTime == 0 then
                GameOver()
            end
        end
    end)
    
    Api.Listen.PlayerDead(nil,
            function(uuid)
                local allPlayer = Api.GetAllPlayers()
                local isAllDead = true
                for _, v in ipairs(allPlayer) do
                    if not Api.IsPlayerDead(v) then
                        isAllDead = false
                    end
                end

                if isAllDead then
                    --gameover
                    GameOver(false)
                end
            end
    )

  
    Api.Task.WaitAlways()
end

function clean()
    
end