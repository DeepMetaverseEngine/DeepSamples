
function main(ele)
    local Masterid
    local Masterindex
    local RoleId
    local PlayerUUID
    local TimeLeft = Api.GetExcelConfig('masterrace_battletime')
    local enemydata = {}
    local isFinish = false
    local CurMasterid,CurMasterindex
    local function SetGameOver(win,checkBlood)
        --print("PlayerUUID",PlayerUUID)
        if isFinish then
            return
        end
        arg.ClientUUID = Api.GetSession(PlayerUUID)
        if not win and checkBlood then
           win = Api.MasterRace.IsMaxHpPct(PlayerUUID)
        end
        if win then
            Api.Task.StartEventByKey('client.Effect/ui_identityresult_win', Api.GetNextArg(arg))
        else
            Api.Task.StartEventByKey('client.Effect/ui_identityresult_lose', Api.GetNextArg(arg))
        end
        local PlayerApi = Api.GetPlayerApi(PlayerUUID)
        Api.Task.Wait(PlayerApi.MasterRace.Task.MasterRacePass(Masterid,Masterindex,PlayerUUID,win)) --发送通关
        local mapID = Api.GetMapTemplateID()
        PlayerApi.LeaveDungeon(mapID)
        isFinish = true
     end
     local function StartLogic(playerUUID)
       --print("playerUUID",playerUUID)
         Api.Task.WaitPlayerReady(playerUUID)
         local PlayerApi = Api.GetPlayerApi(playerUUID)
         PlayerPro = PlayerApi.GetPlayerPro()
         local data = Api.FindExcelData('imagearena/imagearena.xlsx/imagearena_default', {imagearena_type = Masterid,job = PlayerPro,imagearena_num = Masterindex})[1]
        --print("GetMasterIndex",Masterid,PlayerPro,Masterindex)
         if data == nil then
            assert(data)
         end
         local id = Api.Task.AddEventByKey(data.enterevent,Api.GetNextArg(arg,{PlayerUUID = playerUUID}))
         local success,result = Api.Task.Wait(id)
         if success then
            SetGameOver(true)
         end
         
        --事件发送需要目标的arg
        -- Api.Task.StartEventByKey('message.49',Api.GetArg{PlayerUUID = playerUUID})
        -- Api.Task.Sleep(3)
        
    end

    Api.Listen.PlayerLeaveZone(function(playerUUID) 
        Api.Task.AddEventTo(ID,function()
            if not isFinish then
                --print("PlayerLeaveZone",isFinish)  
                Api.MasterRace.MasterRaceLost(curMasterid,curMasterindex,Masterid,Masterindex,playerUUID)
                --Api.Task.Wait(PlayerApi.MasterRace.Task.MasterRacePass(Masterid,Masterindex,playerUUID,false)) --发送通关
                isFinish = true 
            end
            Api.Task.StopEvent(ID) 
        end)
         --print("PlayerLeaveZone",playerUUID)  
   
    end)    
    
    local function InitRace(playerUUID)
        local PlayerApi = Api.GetPlayerApi(playerUUID)
        CurMasterid,CurMasterindex = PlayerApi.MasterRace.GetCurMasterIndex()
        Masterid,Masterindex = PlayerApi.MasterRace.GetMasterIndex()
    end
  
    Api.Listen.PlayerEnterZone(function(playerUUID)    
        PlayerUUID = playerUUID     
        --print("PlayerEnterZone",PlayerUUID)  
        Api.Task.AddEventTo(ID,InitRace,playerUUID)
        Api.Task.AddEventTo(ID,StartLogic,playerUUID)
        Api.Task.AddEventTo(ID,function()
            while TimeLeft > 0 do
                Api.Task.Sleep(1)
                TimeLeft = TimeLeft - 1
                Api.SetEnvironment("count_down",TimeLeft)
            end
            SetGameOver(false,true)
        end)
    end)
    
    Api.Listen.PlayerDead(nil,
        function(uuid)
            Api.Task.AddEventTo(ID, SetGameOver,false,false)
            --Api.Task.StopEvent(ID)
        end
    )
    
    Api.Task.WaitAlways()
end

function clean()
    
end