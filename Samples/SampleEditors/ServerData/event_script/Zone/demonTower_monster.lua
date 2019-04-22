function main(ele)
    local floor_order = 1
    local floor
    local PlayerApi = nil
    local mapdata = Api.FindExcelData('pagoda/pagoda.xlsx/pagoda', {})
    local mapid = Api.GetExcelConfig('pagoda_map_id')
    local maxLayer = #mapdata
    local hasReward = {}
    local lastFlag 
    local isRun = false
    local curTime = 0
    local curDiff 
    local function SendPos(playerUUID,flag)
            if not string.IsNullOrEmpty(flag) then
              if lastFlag == flag then
                return
              end
              lastFlag = flag
             --  local params = {
             --    MapTemplateID = mapid,
             --    PlayerUUID = playerUUID,
             --    Flag = flag
             -- }
            --print("SendPos",flag)
            local x,y = Api.GetFlagPosition(flag)
            Api.SetPlayerPosition(playerUUID,x,y)
            -- Api.Task.TransportPlayer(params)
        end
    end
    local function GetMapData(floor,floor_order)
        local data = Api.FindExcelData('pagoda/pagoda.xlsx/pagoda_monster', {floor = floor,floor_order = floor_order})[1]
        if data == nil or next(data) == nil then
            return nil,0
        end
        --pprint("data",data)
        return data,data.difficulty
    end

  
    local function SendNextWavePos(playerUUID,floor,floor_order)
        local data = GetMapData(floor,floor_order)
        if data == nil then
            return
        end
        SendPos(playerUUID,data.flag)
    end
    local function SendToClientInitTime()
        Api.SetEnvironment("CurLayer",floor)
        Api.SetEnvironment("CurWave",1)
        Api.SetEnvironment("count_down",0)
    end
    local function StartLogic(playerUUID)

       --print("playerUUID",playerUUID)
        if PlayerApi == nil then
            PlayerApi = Api.GetPlayerApi(playerUUID)
            --print("PlayerApi",PlayerApi)
            floor = PlayerApi.GetIntFlag('CurDemonTowerLayer')
            if floor == 0 then floor = 1 end
            Api.AddEnvironment("CurLayer",floor)
            Api.AddEnvironment("CurWave",floor_order)
            Api.AddEnvironment("count_down",curTime)
            local data,difficulty = GetMapData(floor,1)
            curDiff = difficulty
        end
        Api.Task.WaitPlayerReady(playerUUID)
        assert(floor,"镇妖塔层数异常")
        isRun = false
        curTime = 0
        SendToClientInitTime()
        
        ::restart::
        isRun = false
        local data = GetMapData(floor,floor_order)
        if data == nil then
            return
        end
        SendNextWavePos(playerUUID,floor,floor_order)
        Api.SetEnvironment("CurLayer",floor)
        Api.SetEnvironment("CurWave",floor_order)
        
        --事件发送需要目标的arg
        Api.Task.StartEventByKey('message.49',Api.GetArg{PlayerUUID = playerUUID})
        Api.Task.Sleep(3)

        isRun = true

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
        local nextdata =  GetMapData(floor,floor_order)--Api.FindExcelData('pagoda/pagoda.xlsx/pagoda_monster', {floor = floor,floor_order = floor_order})
        if nextdata == nil then
            local passtime = curTime
            if hasReward[floor] == nil then --发奖品
                hasReward[floor] = true
                PlayerApi.DemonTower.RecordPassTime(floor,passtime) --发送通关时间
                isRun = false
                curTime = 0
                local nextfloor =  floor + 1
                local _,difficulty = GetMapData(nextfloor,1)
                if nextfloor > maxLayer or curDiff ~= difficulty then
                    Api.Task.StartEventByKey('message.64',Api.GetArg{PlayerUUID = playerUUID})
                    PlayerApi.DemonTower.SendRewardItem(mapid,passtime,true)
                    return true
                else
                    SendToClientInitTime()
                    floor = nextfloor
                    PlayerApi.SetIntFlag('CurDemonTowerLayer',nextfloor)
                    PlayerApi.DemonTower.SendRewardItem(mapid,passtime,false)
                    Api.Task.Sleep(5)
                    arg.ClientUUID = Api.GetSession(playerUUID)
                    local isPlayCg = false
                    for _,v in ipairs(data.success_event) do
                        --print("v",v,floor)

                        if not string.IsNullOrEmpty(v) and PlayerApi and PlayerApi.GetMapTemplateID() == mapid then
                            Api.Task.AddEventByKey(v,Api.GetNextArg(arg))
                            isPlayCg = true
                        end
                    end
                    SendNextWavePos(playerUUID,floor,1)

                    if isPlayCg then
                        Api.Task.Sleep(6)
                    end

                end
            end
           floor_order = 1
        end
        goto restart
       
    end
   
    
    Api.Listen.PlayerLeaveZone(function(playerUUID)   
         Api.Task.StopEvent(ID)  
    end)    

    Api.Listen.PlayerEnterZone(function(playerUUID)         
        Api.Task.AddEventTo(ID,StartLogic,playerUUID,true)
    end)

    Api.Listen.AddPeriodicSec(1,function()
        if isRun then
            curTime = curTime + 1
            --print("curTime",curTime)
            Api.SetEnvironment("count_down",curTime)
        end
    end)

    Api.Listen.PlayerDead(nil,
        function(uuid)
            Api.SetGameOver(1, "")
            Api.Task.StopEvent(ID)
        end
    )

    Api.Task.WaitAlways()
end

function clean()
    
end