function main(ele)


    local InitScene = false
    local PlayerForce
    local BossID
    local BossFlag = "xl_boss"
    local BossForce = 1
    local BossDir = 1.5
    local GuildMonsterLv = 0  
    local GameOverFlag = false
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
            --print('mythicalbeasts_1.lua monsterData.MonsterLv = ',monsterData.MonsterLv)
            --神兽品阶.
            --monsterData.MonsterRank
            print('mythicalbeasts_1.lua monsterData.MonsterRank = ',monsterData.MonsterRank)
        end    

        --只执行一次.
        if InitScene == false then
            --------------------------------------------------------------------------------------
            --创建对应神兽等级的怪物单位.
            local exploitdata = Api.FindExcelData('guild/mythicalbeasts.xlsx/mode1', {level = GuildMonsterLv})
            print('mythicalbeasts_1.lua exploitdata.monster_id = ',exploitdata[1].monster_id)
            --TODO读取配置表.
            local info = {TemplateID = exploitdata[1].monster_id, Force = BossForce, Direction = BossDir,EditorName = "boss"}
            info.X, info.Y = Api.GetFlagPosition(BossFlag)
            BossID = Api.AddUnit(info)
            --锁血BUFF.
            Api.UnitAddBuff(BossID,22012)
            --------------------------------------------------------------------------------------
            --场景初始化标记.
            InitScene = true
        end

       if(CG_ID == nil) then

             local exploitdata = Api.FindExcelData('guild/mythicalbeasts.xlsx/mythicalbeasts_cg',function(ele)
                return (ele.level == GuildMonsterLv and ele.play_type == 1)
             end)

             CG_ID = exploitdata[1].cg_id
             print('mythicalbeasts_1.lua CG = ',CG_ID)
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

    --停止计时.
    Api.EnableEditorScript("time",false)
    --flag停止
    Api.EnableEditorScript("timeover",false)


    --获取怪物血量结算成绩.
    local hppct = 0
    local curhp = 0
    local maxhp = 0
    BossHPData = Api.GetUnitHPData(BossID)
    if(BossHPData ~= nil)then
        --print('mythicalbeasts_1.lua BossHPData = ',BossHPData.HPPct)
        hppct = BossHPData.HPPct
        curhp = BossHPData.CurHP
        maxhp = BossHPData.MaxHP
    end
    
    local totalDamage = maxhp - curhp
    local score = 100 - hppct
    local damagePct = score
    --取整数
    score = score - score%1
    --读表计算奖励
      print('mythicalbeasts_1.lua exploitdata match MonsterLv = ',GuildMonsterLv)
      print('mythicalbeasts_1.lua exploitdata match score = ',score)
     local exploitdata = Api.FindExcelData('guild/mythicalbeasts.xlsx/mode1', function(ele)
        --print('mythicalbeasts_1.lua exploitdata match:maxhp = ',ele.hp_max," minhp = ",ele.hp_min)
        return (GuildMonsterLv == ele.level and score <= ele.hp_max and score >= ele.hp_min)   
     end)[1]
    pprint('exploitdata',exploitdata)

    --移除BOSS.
    --Api.KillUnit(BossID)

    --添加奖励
     if exploitdata ~= nil then   
          -- local _id
          -- for i,v in ipairs(exploitdata.reward.reward_id) do
          --       _id = {reward_id = v}             
          -- end
          --    local _flag
          -- for i,v in ipairs(exploitdata.reward.flag_reward) do
          --     _flag = {flag_reward = v}             
          -- end

           local TLRewardGroup = {reward_id = Api.copy_table(exploitdata.reward.reward_id),flag_reward = Api.copy_table(exploitdata.reward.flag_reward)}

           local allPlayer = Api.GetAllPlayers()
            for _, v in ipairs(allPlayer) do
                local playapi = Api.GetPlayerApi(v)
                print('mythicalbeasts_1 CommonReward')
                playapi.CommonReward(TLRewardGroup)
            end
        --出结算界面
        --gameover
        --print('mythicalbeasts.lua SetGameOver winForce= ',PlayerForce)
     
    end

    local extDataMap = {}
    extDataMap["ui"] = 'xml/common/common_result.gui.xml'
    extDataMap["func"] = "mythicalbeasts_1"
    extDataMap["totalDamage"] = totalDamage..""
    --两位小数.
    damagePct = damagePct - damagePct%0.01
    extDataMap["damagePct"] = damagePct..""
    local extData = {map = extDataMap}

   Api.SetGameOver(PlayerForce, "win",extData)
   Api.Task.StopEvent(ID)
    
    end

    local function StartClient(uuid)
        Api.Task.AddEventToByKey(ID,'client.mythicalbeasts_mode1', Api.GetArg({ClientUUID = Api.GetSession(uuid)}))
    end

    local function ReadyToStartClient(uuid)
        Api.Task.Wait(Api.Listen.PlayerReady(uuid))
        StartClient(uuid)
    end

    Api.Listen.PlayerEnterZone(function(uuid) 

        --if Api.IsPlayerReady(uuid) then
        --   Api.Task.AddEventTo(ID,StartClient, uuid)
        --else
        --  Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
        --end

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
     --   print('------------SessionReconnect-----------', uuid)
      --      if Api.IsPlayerReady(uuid) then
      --          Api.Task.AddEventTo(ID,StartClient, uuid)
      --      else
      --          Api.Task.AddEventTo(ID,ReadyToStartClient, uuid)
      --      end
    --end)

    Api.Task.WaitAlways()

end

function clean(reason)

end
