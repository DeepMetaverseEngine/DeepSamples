function main(ele)

    local mapID = Api.GetMapTemplateID()

    local function GetCheckPointData()
        local data = Api.FindExcelData('god_island/god_island.xlsx/god_island_checkpoint', {map_id = mapID})[1]
        if data == nil then
            error('error data in god_island_checkpoint with mapID: '.. mapID)
        end
        
        return data
    end
  

    local data = GetCheckPointData(checkpointId)
    local checkpointId = data.checkpoint_id
    local timeLeft = data.time_limit
    
    -- print('###############################################################################checkpointId::',checkpointId)

    -- print('###############################################################################timeLeft::',timeLeft)

    -- print('godisland start ')
    -- local counttimeid
    -- local function RefreshCountDownTime(timeLeft)
    --     print('timeLeft:',timeLeft)
    --     Api.AddEnvironment("time",timeLeft)
    --     if counttimeid ~= nil then
    --         Api.Listen.RemoveEventListen(counttimeid)
    --         counttimeid = nil
    --     end
    --     counttimeid = Api.Listen.AddPeriodicSec(1, function (...)
    --         timeLeft = timeLeft - 1
    --         -- print('timeLeft22222222222:',timeLeft)
    --         Api.SetEnvironment("time",timeLeft)
    --         if timeLeft <= 0 then
    --             -- print('timeLeft33333333333333:',timeLeft)
    --             Api.SetGameOver(1, "")
    --             Api.Task.StopEvent(ID)
    --         end
    --     end)
    -- end

  

    local curTime 
    
    local PlayerApi

    local playerForce 
    

    local function StartLogic(playerUUID)

        curTime = os.clock()

        playerUUID = playerUUID
        PlayerApi = Api.GetPlayerApi(playerUUID)
        playerForce = Api.GetPlayerForce(playerUUID)

        -- checkpointId = PlayerApi.GetIntFlag('CurIslandCpId/')
       

        -- RefreshCountDownTime(timeLeft)
        -- Api.Task.Sleep(timeLeft)

        -- Api.SetGameOver(1, "")
        -- Api.Task.StopEvent(ID)
    end


    local function GameOverLogic(WinForce,message)
        -- print('GameOverLogic playerForce:',playerForce)
        if  playerForce == WinForce and message == "win" then
            local sec = os.clock() - curTime
            PlayerApi.GodIsland.SendIslandPassTime(checkpointId,sec) --发送通关时间
            -- print('GameOverLogic win:',message)
            return
        end
        -- print('GameOverLogic fail: ',message)
    end
  

    Api.Listen.GameOver(function(force,message)
        -- body
        Api.Task.AddEventTo(ID, function ()
             -- print('force,message:',force,message)
             GameOverLogic(force,message)
        end)

    end)

    Api.Listen.PlayerEnterZone(function(playerUUID)         
        Api.Task.AddEventTo(ID,function()
            -- Api.Listen.PlayerReady(playerUUID,function()
            --     --arg.ClientUUID = Api.GetSession(playerUUID)
            --     -- Api.Task.AddEventByKey(v,Api.GetNextArg(arg))
            --     -- Api.Task.Wait(Api.Task.AddEventByKey('client.CountDown',Api.GetNextArg(arg), 5))
            -- end)
            StartLogic(playerUUID)
        end)
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