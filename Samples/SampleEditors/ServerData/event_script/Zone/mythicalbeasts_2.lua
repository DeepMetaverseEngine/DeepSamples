function main(ele)
    local InitScene = false
    local PlayerForce
    local GameOverFlag = false
    --击杀数量.
    local KillCount = 0
    local GuildMonsterLv = 0
    local CG_ID = nil
    --玩家进入场景时
    local function PlayerEnterZoneLogic(uuid)     
         Api.Task.WaitPlayerReady(uuid)
        if GuildMonsterLv == 0 then
            local PlayerApi = Api.GetPlayerApi(uuid)
            --获取玩家的仙盟数据.
            local monsterData = PlayerApi.GetGuildMonsterData()
            --神兽等级.
            GuildMonsterLv = monsterData.MonsterRank
            --GuildMonsterLv = 1
            --print('mythicalbeasts_2.lua monsterData.MonsterLv = ',monsterData.MonsterLv)
            --神兽品阶.
            --monsterData.MonsterRank
            print('mythicalbeasts_2.lua monsterData.MonsterRank = ',monsterData.MonsterRank)
        end    

        if(CG_ID == nil) then

             local exploitdata = Api.FindExcelData('guild/mythicalbeasts.xlsx/mythicalbeasts_cg',function(ele)
                return (ele.level == GuildMonsterLv and ele.play_type == 2)
             end)

             CG_ID = exploitdata[1].cg_id
             print('mythicalbeasts_2.lua CG = ',CG_ID)
        end

        if(CG_ID ~= nil) then
            arg.ClientUUID = Api.GetSession(uuid)
            Api.Task.AddEventByKey(CG_ID,Api.GetNextArg(arg))    
        end
    end

    --复活已死亡的单位.
    local function RebirthAllPlayers()
        local allPlayer = Api.GetAllPlayers()
            for _, v in ipairs(allPlayer) do
                if  Api.IsPlayerDead(v) then
                         Api.StartRebirth(Api.GetPlayerObjectID(v),0,0)
                end
            end
    end

    local function DoGameOver()
    --次数保护
        if GameOverFlag == true then
            return
        end

        GameOverFlag = true
        --怪物停止攻击无敌.    

        --复活全员
        RebirthAllPlayers()

        --触发剧情
        
    --读表计算奖励
         local exploitdata = Api.FindExcelData('guild/mythicalbeasts.xlsx/mode2', function(ele)
            return (KillCount == ele.killcount)   
         end)[1]
    --pprint('exploitdata',exploitdata)

  

    --添加奖励
         if exploitdata ~= nil then
         
            local TLRewardGroup = {reward_id = Api.copy_table(exploitdata.reward.reward_id),flag_reward = Api.copy_table(exploitdata.reward.flag_reward)}

            local allPlayer = Api.GetAllPlayers()
            for _, v in ipairs(allPlayer) do
                local playapi = Api.GetPlayerApi(v)
                  --print('mythicalbeasts_1 CommonReward')
                playapi.CommonReward(TLRewardGroup)
            end    
        end

        local monsterNum = Api.GetEnvironmentoIntVar("monster_num")

        local extDataMap = {}
        extDataMap["ui"] = 'xml/common/common_result.gui.xml'
        extDataMap["func"] = "mythicalbeasts_2"
        extDataMap["killCount"] = KillCount..""
        extDataMap["totalCount"] = monsterNum..""
        local extData = {map = extDataMap}
        Api.SetGameOver(PlayerForce, "win",extData)
        Api.Task.StopEvent(ID)
    
    end

    local function StartClient(uuid)
        Api.Task.AddEventToByKey(ID,'client.mythicalbeasts_mode2', Api.GetArg({ClientUUID = Api.GetSession(uuid)}))
    end

    local function ReadyToStartClient(uuid)
        Api.Task.Wait(Api.Listen.PlayerReady(uuid))
        StartClient(uuid)
    end

    Api.Listen.PlayerEnterZone(function(uuid) 

        -- if Api.IsPlayerReady(uuid) then
        --     Api.Task.AddEventTo(ID,StartClient, uuid)
        -- else
        --     Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        -- end

        Api.Task.AddEventTo(ID,PlayerEnterZoneLogic,uuid)
        --记录玩家阵营
        PlayerForce = Api.GetPlayerForce(uuid)
    end)

    Api.Listen.EnvironmentVarChanged("GameOver",function(key,v)
             print('mythicalbeasts.lua GameOver Flag = ',v)
             if v == true then
                Api.Task.AddEventTo(ID,DoGameOver)
             end
        end
    )

    Api.Listen.EnvironmentVarChanged("kill",function(key,v)
            print('mythicalbeasts.lua kill Flag = ',v)
            KillCount = v
        end
    )

    --结束条件1：当所有玩家死亡，玩法终止.
    Api.Listen.PlayerDead(nil,function(uuid)
            local allPlayer = Api.GetAllPlayers()
            local isAllDead = true
            for _, v in ipairs(allPlayer) do
                if not Api.IsPlayerDead(v) then
                    isAllDead = false
                end
            end

            if isAllDead then
                Api.Task.AddEventTo(ID,DoGameOver)
            end
        end
    )

    -- Api.Listen.SessionReconnect(function(uuid)
    --    print('------------SessionReconnect-----------', uuid)
    --        if Api.IsPlayerReady(uuid) then
    --            Api.Task.AddEventTo(ID,StartClient, uuid)
    --        else
    --            Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
    --        end
    -- end)

    Api.Task.WaitAlways()

end

function clean(reason)

end
