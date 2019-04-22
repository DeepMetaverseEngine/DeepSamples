function main()
    print('---------guildattack.lua Start-----------')
    
    local GuildApi

    local function SetPlayerRebirthType(uuid)
    	-- local zoneGuildId = Api.GetZoneGuildUUID()
    	-- local force = zoneGuildId == guildID and 2 or 3
    	local force = Api.GetPlayerForce(uuid)
    	Api.Task.WaitPlayerReady(uuid)
    	-- print('------------aaaaaaaaaa-force1', force)
    	local playerApi = Api.GetPlayerApi(uuid)
    	playerApi.SetPlayerRebirthType(force == 2 and 4 or 5)
    end

    local function PlayerDeaded( uuid, attackid )
    	local zoneGuildId = Api.GetZoneGuildUUID()
    	local atkUUID = Api.GetPlayerUUID(attackid)
    	local atkGuildId = Api.GetPlayerGuildUUID(atkUUID)
    	if atkGuildId == zoneGuildId then
	    	local atkInfo = Api.GetPlayerKillInfo(uuid)
	    	local reward = Api.GetExcelConfig('guild_breakreward')
	    	for k, v in pairs(atkInfo) do
	    		local playerGuildId = Api.GetPlayerGuildUUID(k)
	    		if playerGuildId == zoneGuildId then
	    			local playerApi = Api.GetPlayerApi(k)
                    if playerApi.GetIntFlag('DailyGuildAttackKills', true) < Api.GetExcelConfig('guild_defense_times') then
                        playerApi.AddIntFlag('DailyGuildAttackKills', 1, true)
                        local items = {}
                        table.insert(items, {TemplateID = VirtualItems.Contribution, Count = reward})
                        playerApi.Task.AddMoreItem(items)
                        Api.Task.StartEventByKey('message.11',Api.GetArg({PlayerUUID = k}), reward)
                    else
                        Api.Task.StartEventByKey('message.118', Api.GetArg({PlayerUUID = k}))
                    end
	    		end
	    	end
    	end
    end

    local function OnPlayerEnter(uuid)
        print('------------PlayerEnterZone-----------', uuid)
        if not GuildApi then
            GuildApi = Api.GetGuildApi(uuid)
        end
        local guildUUID = Api.GetPlayerGuildUUID(uuid)
        GuildApi.NotifyGuildZoneInfoChange(guildUUID, true)
        -- Api.NotifyGuildPlayerInOut(uuid, true)
        Api.RecordAttackData(uuid)
        local force = Api.GetPlayerForce(uuid)
        if force ~= 2 then --敌方阵营，随机传送
            local objId = Api.GetPlayerObjectID(uuid)
            -- 随机传送
            local index = Api.RandomInteger(1, 4 + 1)
            local x, y = Api.GetFlagPosition('xl_pohuai'..index)
            Api.SetUnitPosition(objId, x, y)
        end

        Api.Task.AddEventTo(ID, SetPlayerRebirthType, uuid)
    end

    Api.Listen.PlayerEnterZone(function(uuid)
        Api.Task.AddEventTo(ID, OnPlayerEnter, uuid)
    end)
    
    Api.Listen.InitPlayerForce(function(uuid,teamid,guildID)
    	local zoneGuildId = Api.GetZoneGuildUUID()
    	local force = zoneGuildId == guildID and 2 or 3
    	-- print('-------------force', force, zoneGuildId)
    	return force
    end)

    local function OnPlayerLeave( uuid )
        print('------------PlayerLeaveZone-----------', uuid)
        local guildUUID = Api.GetPlayerGuildUUID(uuid)
        GuildApi.NotifyGuildZoneInfoChange(guildUUID, false)
        -- Api.NotifyGuildPlayerInOut(uuid, false)
    end

    Api.Listen.PlayerLeaveZone(function(uuid)
        Api.Task.AddEventTo(ID, OnPlayerLeave, uuid)
    end)
    
    --------------监听死亡事件----------------
    Api.Listen.PlayerDead(function(uuid, attackid)
        Api.Task.AddEventTo(ID, PlayerDeaded, uuid, attackid)
    end)
    ------------------------------------------

    --------------监听道具采集----------------

    local function OnItemPickFinish( uuid, itemid )
        local itemTemplateId = Api.GetItemTemplateID(itemid)
    	local db = Api.FindFirstExcelData('guild/guild_destroy.xlsx/guild_destroy', {gatherid = itemTemplateId})
        Api.NotifyGuildPlayerAttack(uuid, db.destroy_type)
        if db.effervescent ~= '' then
	        local x, y = Api.GetObjectPosition(itemid)
	        local eff = {
		        Name = '/res/effect/'..db.effervescent,
		        -- IsLoop = true,
		        -- EffectTimeMS = 10000,
		        -- ScaleToBodySize = 50,
		    } 
		    Api.PlaySceneEffect(eff, x, y)
        end
    end

    local guildData = Api.FindExcelData('guild/guild_destroy.xlsx/guild_destroy', {})
    for i = 1, #guildData do
    	Api.Listen.PlayerPickedTemplateItem(guildData[i].gatherid, OnItemPickFinish)
    end
    ------------------------------------------

    Api.Task.WaitAlways()
end