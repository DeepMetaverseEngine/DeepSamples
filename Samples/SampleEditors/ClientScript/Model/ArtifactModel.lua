local ItemModel = require 'Model/ItemModel' 

local _M = {}
_M.__index = _M

--神器
_M.artifactMap = {}
_M.itemListener = {}
 
local function CanGet(itemData,canNotEmpty)
    local costs = ItemModel.ParseCostAndCostGroup(itemData)
    if canNotEmpty and not next(costs) then
        return false
    end 
    for i,v in ipairs(costs or {}) do 
        if v.cur < v.need then
            return false
        end
    end
    return true
end  

local function GetArtifactData(artifactMap,MainEquipId,SecondEquipId)
    -- body
    local artifacts = GlobalHooks.DB.GetFullTable('Artifact')
    table.sort(artifacts, function(a,b)
        
        local lhs = artifactMap[a.id]
        local rhs = artifactMap[b.id]

        if lhs and rhs then
            if MainEquipId ~= 0 then
                if MainEquipId == a.id then 
                    return true
                elseif MainEquipId == b.id then
                    return false
                end
            end

            if SecondEquipId ~= 0 then
                if SecondEquipId == a.id then
                    if MainEquipId ~= 0 and MainEquipId == b.id then
                        return false
                    end 
                    return true
                elseif SecondEquipId == b.id then
                    if MainEquipId ~= 0 and MainEquipId == b.id then
                        return true
                    end 
                    return false
                end
            end
        elseif lhs then 
            return true
        elseif  rhs then
            return false
        end 
       
        -- -- 此处计算可激活
        local canGetA = CanGet(a)
        local canGetB = CanGet(b)
        if canGetA and canGetB then
            return a.order < b.order
        elseif canGetA then
            return true
        elseif canGetB then
            return false
        end

        return a.order < b.order

    end)

    return artifacts
end



local function GetArtifactEquipData(artifactMap,MainEquipId,SecondEquipId)
    -- body
    local artifacts = GlobalHooks.DB.GetFullTable('Artifact')

    -- print_r('artifacts:',artifacts)

    local users = {}

    for k,v in pairs(artifacts) do
        if  artifactMap[v.id] then
            table.insert(users,v)
        end
    end

    -- print_r('users:',users)
    
    table.sort(users, function(a,b)
        
        if SecondEquipId ~= 0 then
            if SecondEquipId == a.id then
                if MainEquipId ~= 0 and MainEquipId == b.id then
                    return false
                end 
                return true
            elseif SecondEquipId == b.id then
                if MainEquipId ~= 0 and MainEquipId == b.id then
                    return true
                end 
                return false
            end
        end

       

        return a.order < b.order
    end)

    return users
end



local function GetArtifact(artifactId)
	-- body
	local ArtifactData = GlobalHooks.DB.FindFirst('Artifact',{id = artifactId})
	return ArtifactData
end  

local function GetArtifactAttr(artifactId,level)
	-- body
	local attrData = GlobalHooks.DB.FindFirst('ArtifactAttr',{artifact_id = artifactId,artifact_lv = level})
	return attrData
end


local function TryAddAttribute(all, key, v,index)
    if v ~= 0 then
        table.insert(all, {Name = key, Tag = 'FixedAttributeTag', Value = v, ValueType = 1,index = index,})
    end
end
 

local function GetXlsFixedAttribute(static_data)
    local all = {}
    TryAddAttribute(all, 'maxhp', static_data.maxhp,1)
    TryAddAttribute(all, 'attack', static_data.attack,2)
    TryAddAttribute(all, 'defend', static_data.defend,3)
    TryAddAttribute(all, 'mdef', static_data.mdef,4)
    TryAddAttribute(all, 'through', static_data.through,5)
    TryAddAttribute(all, 'block', static_data.block,6)
    TryAddAttribute(all, 'crit', static_data.crit,7)
    TryAddAttribute(all, 'rescrit', static_data.rescrit,8)
    TryAddAttribute(all, 'hit', static_data.hit,9)
    TryAddAttribute(all, 'dodge', static_data.dodge,10)

-- 雷属性攻击4
    TryAddAttribute(all, 'thunderdamage', static_data.thunderdamage,11)
    TryAddAttribute(all, 'winddamage', static_data.winddamage,12)
    TryAddAttribute(all, 'icedamage', static_data.icedamage,13)
    TryAddAttribute(all, 'firedamage', static_data.firedamage,14)
    TryAddAttribute(all, 'soildamage', static_data.soildamage,15)
    TryAddAttribute(all, 'thunderresist', static_data.thunderresist,16)
    TryAddAttribute(all, 'windresist', static_data.windresist,17)
    TryAddAttribute(all, 'iceresist', static_data.iceresist,18)
    TryAddAttribute(all, 'fireresist', static_data.fireresist,19)
	TryAddAttribute(all, 'soilresist', static_data.soilresist,20)

    return all
end

local function GetAllArtifactAttribute(artifactMap)
	local all = {}
	for artifactId,level in pairs(artifactMap) do
		local templateData = GetArtifactAttr(artifactId,level)
		local attrData = GetXlsFixedAttribute(templateData)
		for k,v in pairs(attrData) do
			--print_r(k,v)
			local lastV = all[v.Name]
			if lastV ~= nil then
				all[v.Name].Value = lastV.Value + v.Value
			else
				all[v.Name] = v
			end
		end
	end
	return all
end  

local function GetSecondEquipOpenLv( ... )
    local value = GlobalHooks.DB.GetGlobalConfig('artifact_openlv')
    -- print('GetSecondEquipOpenLv:',value,type(value))
    return tonumber(value)
end

--获取列表
local function ReqArtifactList(cb)
    local msg = {}
    Protocol.RequestHandler.TLClientGetArtifactListRequest(msg, function(resp)
        -- print_r(resp)
        _M.artifactMap = resp.artifactMap
        if cb then
            cb(resp)
        end
    end)
end

local function CheckArtifactLevelUp(artifactId,artifactLevel)
    local Level = artifactLevel or _M.artifactMap[artifactId] or 0
    if Level == 0 then
        return
    end
    local lvdb = GetArtifactAttr(artifactId,Level)
    if not lvdb then
        GlobalHooks.UI.SetRedTips('miraclemain',0, artifactId)
        return
    end
    local costs = ItemModel.ParseCostAndCostGroup(lvdb)
    local enough = ItemModel.IsCostAndCostGroupEnough(costs,true)
    GlobalHooks.UI.SetRedTips('miraclemain', enough and 1 or 0, artifactId)
end

--激活
local function ReqGetArtifact(artifactId,cb)
    local msg = {}
    msg.c2s_artifactId = artifactId
    Protocol.RequestHandler.TLClientGetArtifactRequest(msg, function(resp)
        -- print_r(resp)
        local artifactLevel = 1
        _M.artifactMap[artifactId] = artifactLevel
        if _M.itemListener['artifact'..artifactId] then
            _M.itemListener['artifact'..artifactId].listener:Dispose()
        else
            _M.itemListener['artifact'..artifactId] = {}
        end
        local lvdb = GetArtifactAttr(artifactId, artifactLevel)
        _M.itemListener['artifact'..artifactId].listener = ItemModel.ListenCostXlsLine(lvdb, function()
            CheckArtifactLevelUp(artifactId,artifactLevel)
         end)
         CheckArtifactLevelUp(artifactId,artifactLevel)

        if cb then
            cb(resp)
        end
    end)
end

--装备
local function ReqUseArtifact(pos,artifactId,cb)
    local msg = {}
	msg.c2s_pos = pos
    msg.c2s_artifactId = artifactId
    Protocol.RequestHandler.TLClientEquipArtifactRequest(msg, function(resp)
        -- print_r(resp)
        if cb then
            cb(resp)
        end
    end)
end

local function CheckArtifactGet(artifact)
    local costs = ItemModel.ParseCostAndCostGroup(artifact)
    local enouth = ItemModel.IsCostAndCostGroupEnough(costs)
    GlobalHooks.UI.SetRedTips('miraclemain', enouth and 1 or 0, artifact.id)
end

local function ReqArtifactLevelUp(artifactId,cb)
    local msg = {}
    msg.c2s_artifactId = artifactId
    Protocol.RequestHandler.TLClientArtifactLevelUpRequest(msg, function(resp)
        _M.artifactMap[artifactId] = _M.artifactMap[artifactId] + 1
        local artifacts = GlobalHooks.DB.GetFullTable('Artifact')
        for k,artifact in ipairs(artifacts) do
            local artifactId = artifact.id
            local artifactLevel = _M.artifactMap[artifactId] or 0 
            if artifactLevel > 0 then
                CheckArtifactLevelUp(artifactId)
            else
                CheckArtifactGet(artifact)
            end
        end
        CheckArtifactLevelUp(artifactId)
        if cb then
            cb(resp)
        end
    end)
end





function _M.OnBagReady() 
    
    if not GameGlobal.Instance.netMode then 
        return
    end
    
    ReqArtifactList(function(resp)
        local artifacts = GlobalHooks.DB.GetFullTable('Artifact')
        for k,artifact in ipairs(artifacts) do
            
            local artifactId = artifact.id
            local artifactLevel = _M.artifactMap[artifactId] or 0
            _M.itemListener['artifact'..artifactId] = {}
            _M.itemListener['artifact'..artifactId].level = artifactLevel
            if artifactLevel > 0 then
                 local lvdb = GetArtifactAttr(artifactId, artifactLevel)
                _M.itemListener['artifact'..artifactId].listener = ItemModel.ListenCostXlsLine(lvdb, function()
                    CheckArtifactLevelUp(artifactId)
                end)
                CheckArtifactLevelUp(artifactId)
            else
                _M.itemListener['artifact'..artifactId].listener = ItemModel.ListenCostXlsLine(artifact, function()
                    CheckArtifactGet(artifact)
                end)
                CheckArtifactGet(artifact)
            end
        end
    end)
end

function _M.InitNetWork(initNotify)
    if initNotify then

      
    end
end


function _M.initial()
    _M.itemListener = {}
end

function _M.fin()
     if _M.itemListener then
        for _, v in pairs(_M.itemListener) do
            if v.listener then
                v.listener:Dispose()
            end
        end
        _M.itemListener = {}
    end
end



_M.CanGet = CanGet
_M.GetArtifact = GetArtifact
_M.GetArtifactData = GetArtifactData
_M.GetArtifactEquipData = GetArtifactEquipData

_M.GetArtifactAttr = GetArtifactAttr
_M.GetXlsFixedAttribute = GetXlsFixedAttribute
_M.GetAllArtifactAttribute = GetAllArtifactAttribute
_M.GetSecondEquipOpenLv = GetSecondEquipOpenLv

_M.ReqArtifactList = ReqArtifactList
_M.ReqGetArtifact = ReqGetArtifact
_M.ReqUseArtifact = ReqUseArtifact
_M.ReqArtifactLevelUp = ReqArtifactLevelUp

return _M