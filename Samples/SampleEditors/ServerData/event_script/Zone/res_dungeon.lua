function main(ele)
    print("-------------------------------------res_dungeon.lua start------------------------------------- ")
  
    local GameOverFlag = false
    local PlayerForce

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
        print("res_dungeon Do GameOver")
        --次数保护
        if GameOverFlag == true then
            return
        end
        local allPlayer = Api.GetAllPlayers()
        GameOverFlag = true
     --击杀数量.
        local score = Api.GetEnvironmentoIntVar("kill")
        print("res_dungeon kill score = ",score)
      --副本ID
        local maptemplateID = Api.GetMapTemplateID()
        print("res_dungeon maptemplateID = ",maptemplateID)
      --掉落组 
        local groupID = Api.FindExcelData('dungeondata_daily/dungeondata_daily.xlsx/dungeondata_daily', {map_id = maptemplateID})[1].group_id
        print("res_dungeon groupID = ",groupID)
     --找查定奖励
        local exploitdata = Api.FindExcelData('dungeondata_daily/dungeondata_daily.xlsx/dungeondata_reward', function(ele)
            return(groupID == ele.group_id and score <= ele.kill_max and score >= ele.kill_min)
            end)
       
        if exploitdata ~= nil and exploitdata[1] then
            local data = exploitdata[1]
            print("res_dungeon rewardData : item.id = ",data.reward.item.id[1]," item.num = ",data.reward.item.num[1]," flag = ",data.reward.flag_reward[1])

            local Items = {id = Api.copy_table(data.reward.item.id),num = Api.copy_table(data.reward.item.num)}
            local addition = 0
            --获取加成值.
            local playerLv = 0
             for _, v in ipairs(allPlayer) do
                playerLv = Api.GetPlayerLevel(v)
                print("res_dungeon playerLv = ",playerLv)
                break
            end

            print("res_dungeon findData groupID = ",groupID)
            print("res_dungeon findData playerLv = ",playerLv)
            local additionData = Api.FindExcelData('dungeondata_daily/dungeondata_daily.xlsx/dungeondata_addition',function(ele)
            return (groupID == ele.group_id and playerLv >= ele.level_min and playerLv <=ele.level_max)
            end)

            if additionData ~= nil and additionData[1] then
                addition  = additionData[1].reward_item_add
            end
  
            for i, v in ipairs(Items.num) do
                Items.num[i]= v * addition /10000   
                print("res_dungeon item num = ",v)
                print("res_dungeon addition = ",addition)
                print("res_dungeon item result = ", Items.num[i])  
            end

            local rewardGroup = {item = Items,flag_reward = Api.copy_table(data.reward.flag_reward)}

       
            for _, v in ipairs(allPlayer) do
                local playapi = Api.GetPlayerApi(v)
                playapi.CommonReward(rewardGroup,nil,"res_dungeon")
                break
            end
        end
     
      --获取每个人的等级
     
      --推送奖励
      --推GAMEOVER
            local extDataMap = {}
            extDataMap["ui"] = 'xml/common/common_result.gui.xml'
            extDataMap["func"] = "dailydungeon"
            extDataMap["score"] = score..""
            local extData = {map = extDataMap}

            Api.SetGameOver(PlayerForce, "win",extData)
            Api.Task.StopEvent(ID)
    end
   
    Api.Listen.PlayerEnterZone(function(uuid) 
        --记录玩家阵营
        PlayerForce = Api.GetPlayerForce(uuid)
    end)

    Api.Listen.EnvironmentVarChanged("gameover",function(key,v)
             if v == true then
                Api.Task.AddEventTo(ID,DoGameOver)
             end
        end
    )

    Api.Task.WaitAlways()
end

function clean(reason)


end
