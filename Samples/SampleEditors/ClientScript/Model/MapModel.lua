local _M = {}
_M.__index = _M

local function CheckRequire(require)
	local minLv = 0
  	if require.key == nil or require.maxval == nil or require.minval == nil then
  		return true,minLv
  	end

	local reason = nil
    for k,requirekey in pairs(require.key) do
        if requirekey == 'pLevel' then
			local playerLevel = DataMgr.Instance.UserData.Level
			-- print('playerLevel:',playerLevel)
            minLv = require.minval[k]
            local maxLv = require.maxval[k]

            if playerLevel < minLv or (maxLv ~= -1 and playerLevel > maxLv) then
                return false, minLv,require.text[k]
            end
        end
    end 
    return true,minLv
end

local function GetMapTempData( mapID )
	return GlobalHooks.DB.FindFirst('MapData',{id = mapID})
end 

local function GetMapData( mapID )
	local data = GlobalHooks.DB.FindFirst('MapData',{id = mapID})
	if data == nil or data.id == nil then
		return nil
	end

	local result,minLv = CheckRequire(data.require)
	return data.name,result,minLv
end




local function GetNpcName(npcId)
	-- body
	local data = GlobalHooks.DB.FindFirst('npc',{id = npcId})
	if data then
		return data.npc_name 
	end
	return nil
end

local function GetNpcData(sceneID)
	-- body
	local npcData = {}
	local data = GlobalHooks.DB.FindFirst('MapNpc',{id = sceneID})
	if data == nil then
		return npcData
	end
	for k,v in pairs(data.npcid) do
		-- print('GetNpcData:',k,v,type(v))
		-- print('GetNpcData icon is: ',v,data.icon[k])
		npcData[v] = data.icon[k]
		-- local npc = {}
		-- npc.id = v
		-- npc.icon = data.icon[k]
		-- table.insert(npcData,data)
	end
	return npcData
end

local function GetTransferData(sceneID)
	-- body
	local transferData = {}
	local data = GlobalHooks.DB.FindFirst('MapTransfer',{id = sceneID})
	if data == nil then
		return transferData
	end
	for k,v in pairs(data.name) do
		transferData[v] = data.flag[k]
	end
	return transferData
end 

local function GetMonsterData(sceneID)
	-- body
	local monsterData = {}
	local data = GlobalHooks.DB.FindFirst('MapMonster',{id = sceneID})

	-- print_r('GetMonstData:',data)
	if data == nil then
		return monsterData
	end
	for k,v in pairs(data.monsterid) do
		-- print(k,v)
		-- print('value is: ',v,data.flag[k])
		monsterData[v] = data.flag[k]
	end
	return monsterData
end

local function GetSceneMonsterData(mapId,monsterId)
	return GlobalHooks.DB.FindFirst('monster',{dungeon_id = mapId,monster_id = monsterId})
end

local function GetMonsterLevel(mapId,monsterId)
	local data = GetSceneMonsterData(mapId,monsterId)
	if data then 
		return data.level
	end
	return 0
end
 


_M.CheckRequire = CheckRequire
_M.GetMapTempData = GetMapTempData
_M.GetMapData = GetMapData

_M.GetNpcData = GetNpcData
_M.GetMonsterData = GetMonsterData
_M.GetTransferData = GetTransferData

_M.GetNpcName = GetNpcName
_M.GetSceneMonsterData = GetSceneMonsterData
_M.GetMonsterLevel = GetMonsterLevel
 

return _M
