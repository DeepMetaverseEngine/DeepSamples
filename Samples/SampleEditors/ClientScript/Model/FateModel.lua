local ItemModel = require 'Model/ItemModel'

local _M = {}
_M.__index = _M
 
local allAttribute

_M.GetAllAttribute = function ( ... )
  -- body
    if allAttribute then
        return allAttribute
    end
    allAttribute = {}
    table.insert(allAttribute,'attack')
    table.insert(allAttribute,'maxhp')
    table.insert(allAttribute,'defend')
    table.insert(allAttribute,'mdef')
    table.insert(allAttribute,'through')
    table.insert(allAttribute,'block')
    table.insert(allAttribute,'hit')
    table.insert(allAttribute,'dodge')
    table.insert(allAttribute,'crit')
    table.insert(allAttribute,'rescrit')

    table.insert(allAttribute,'cridamageper')
    table.insert(allAttribute,'redcridamageper')
    table.insert(allAttribute,'thunderdamage')
    table.insert(allAttribute,'winddamage')
    table.insert(allAttribute,'icedamage')
    table.insert(allAttribute,'firedamage')
    table.insert(allAttribute,'soildamage')

    table.insert(allAttribute,'thunderresist')
    table.insert(allAttribute,'windresist')
    table.insert(allAttribute,'fireresist')
    table.insert(allAttribute,'soilresist')
    table.insert(allAttribute,'targettomonster')
    table.insert(allAttribute,'targettoplayer')
 
    return allAttribute
end

local function TryAddAttribute(all, key, v, allowzero)
    --print('TryAddAttribute',key, v)
    if (v and v ~= 0) or (v and allowzero) then
        table.insert(all, {Name = key, Tag = 'FixedAttributeTag', Value = v, ValueType = 1})
    end
end

_M.GetXlsFixedAttribute = function(static_data)
    local all = {}
    TryAddAttribute(all, 'attack', static_data.attack)
    TryAddAttribute(all, 'maxhp', static_data.maxhp)
    TryAddAttribute(all, 'defend', static_data.defend)
    TryAddAttribute(all, 'mdef', static_data.mdef)
    TryAddAttribute(all, 'through', static_data.through)
    TryAddAttribute(all, 'block', static_data.block)
    TryAddAttribute(all, 'hit', static_data.hit)
    TryAddAttribute(all, 'dodge', static_data.dodge)
    TryAddAttribute(all, 'crit', static_data.crit)
    TryAddAttribute(all, 'rescrit', static_data.rescrit)


    TryAddAttribute(all, 'cridamageper', static_data.cridamageper)
    TryAddAttribute(all, 'redcridamageper', static_data.redcridamageper)
    TryAddAttribute(all, 'thunderdamage', static_data.thunderdamage)
    TryAddAttribute(all, 'winddamage', static_data.winddamage)
    TryAddAttribute(all, 'icedamage', static_data.icedamage)
    TryAddAttribute(all, 'firedamage', static_data.firedamage)
    TryAddAttribute(all, 'soildamage', static_data.soildamage)

    TryAddAttribute(all, 'thunderresist', static_data.thunderresist)
    TryAddAttribute(all, 'windresist', static_data.windresist)
    TryAddAttribute(all, 'iceresist', static_data.iceresist)
    TryAddAttribute(all, 'fireresist', static_data.fireresist)
    TryAddAttribute(all, 'soilresist', static_data.soilresist)
    TryAddAttribute(all, 'targettomonster', static_data.targettomonster)
    TryAddAttribute(all, 'targettoplayer', static_data.targettoplayer)

    return all
end

_M.GetFateNextLvData = function (templateID,lv)
    return GlobalHooks.DB.FindFirst('FateLevel',{item_id = templateID,fate_lv = lv+1})
end

_M.GetNextFatedAttribute = function (templateID,lv)
	local nextLvData = GlobalHooks.DB.FindFirst('FateLevel',{item_id = templateID,fate_lv = lv+1})
	if not nextLvData then
       	return nil
    end
    
    local next_attr
    local all = _M.GetXlsFixedAttribute(nextLvData)
    for _,v in ipairs(all) do
    	next_attr = next_attr or {}
		local attr_name, v_str = ItemModel.GetAttributeString(v)
		local rgb = ItemModel.GetAttributeColorRGB(v)
		table.insert(next_attr,{attrname = {Text = string.format('%s:', attr_name), Color = rgb},
                        attr = {Text = string.format('+%s', v_str), Color = rgb},
                        img = Constants.InternalImg.detail_gray_point}) 
    end
    return next_attr
end

_M.RequestFateLottery = function(fateLotteryId,cb)
	-- body
	local request = {c2s_fateLotteryId = fateLotteryId}
	Protocol.RequestHandler.ClientFateLotteryRequest(request, function(rsp)
   		if cb then
   			cb(rsp,true)
  		end
	end, function(rsp)
        if cb then
            cb(nil,false)
        end
    end)
end

_M.RequestFateDecompose = function(slots,cb)
	-- body
	local request = {c2s_slots = slots}
	Protocol.RequestHandler.ClientFateDecomposeItemRequest(request, function(rsp)
		if cb then
   			cb(rsp)
  		end
	end)
end

_M.RequestFateLevelUp = function(equipID,cb)
	-- body
	local request = {c2s_equipID = equipID}
	Protocol.RequestHandler.ClientFateLevelUpRequest(request, function(rsp)
		if cb then
   			cb(rsp)
  		end
	end)
end

_M.RequestFateLock = function(equipID,lock,cb)
	-- body
	local request = {c2s_equipID = equipID,c2s_lock = lock}
	Protocol.RequestHandler.ClientLockFateRequest(request, function(rsp)
		if cb then
   			cb(rsp)
  		end
	end)
end


return _M