
local DailyChestTimeConfig

local function ItemLogic(templateID, x,y, succevent)
	--step4 放道具
	local info = {
		TemplateID = templateID,
		X = x,
		Y = y,
	}
	local itemid = Api.AddItem(info)

	--step5 监听道具被采集
	local success,uuid = Api.Task.Wait(Api.Listen.PlayerPickedItem(itemid))
	if success then
		--step6 奖励相关
		if not string.IsNullOrEmpty(succevent) then

			local PlayerApi = Api.GetPlayerApi(uuid)
            
            local DailyTimes = PlayerApi.GetIntFlag('DailyChestTimes',true)
            if DailyTimes >= DailyChestTimeConfig  then
        		Api.Task.StartEventByKey('message.3',Api.GetArg({PlayerUUID = uuid}))
        	else        		
        		local id = Api.Task.AddEventByKey(succevent,Api.GetArg({PlayerUUID = uuid}))
        		local ok = Api.Task.Wait(id)
        		if ok then
        			PlayerApi.AddIntFlag('DailyChestTimes',1,true)
        			local timeLeft = DailyChestTimeConfig - DailyTimes
        			Api.Task.StartEventByKey('message.5',Api.GetArg({PlayerUUID = uuid}),DailyTimes + 1,timeLeft - 1)
 				end
        	end
		end
	-- else
	-- 	local PlayerApi = Api.GetPlayerApi(uuid)
	-- 	Api.Task.StartEventByKey('message.6',Api.GetArg({PlayerUUID = uuid}))
	end
end

function main(ele)
	assert(ele)
	if not ele then
		local mapID = Api.GetMapTemplateID()
    	--step1 取地图所有flags
      	print('excel_chest ele is empty mapID : '.. mapID)
    	return false
    end

    DailyChestTimeConfig = Api.GetExcelConfig('daily_treasures_max')
    -- print('DailyChestTimeConfig:',DailyChestTimeConfig)

    local src_flags = Api.GetZonePointsName()
     
    local flags = {}
    for k,v in ipairs(src_flags) do
    	if string.starts(v,'chest') then
    		table.insert(flags,v)
    	end
    end
  	
	local ids = {}
	local weights = {}
	for i,v in ipairs(ele.template) do
		if not string.IsNullOrEmpty(v) then
			table.insert(ids, i)
			table.insert(weights, ele.weight[i])
		end
    end

	while true do
		--step2 打乱flag 
	    local targetflags = Api.UpsetArray(flags)
	   	
	    for i =1,ele.selection_num do
	    	local  flag = targetflags[i]
	    	if flag then
	    		local x, y = Api.GetFlagPosition(targetflags[i])
				--step3 
				-- 随机物品index
	    		-- 3个随一个
	    		local result = Api.RandomWeight(weights,1)
    			for k,v in ipairs(result) do
					local index = v + 1
					Api.Task.AddEvent(ItemLogic, ele.template[index], x, y, ele.event[index])
				end
	    	end
	    end
		-- print('ele.refresh_time',ele.refresh_time)
    	Api.Task.Sleep(ele.refresh_time)
	end
 
    Api.Task.WaitAlways()

end