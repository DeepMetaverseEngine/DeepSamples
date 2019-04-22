local _M = {}
_M.__index = _M

local function GetRewardBack(functionID)
    return GlobalHooks.DB.FindFirst('RewardBack',{function_id = functionID})
end

local function GetGetRewardBackInfo(GetRewardBackInfoResp)
	local resp = GetRewardBackInfoResp
	local result = {}
 
    local costID = 0
    local totalCost = 0

	for functionID,timeLeft in pairs(resp.s2c_backMap or {}) do
		local rback = GetRewardBack(functionID)
		if rback then
			rback.timeLeft = timeLeft
			table.insert(result,rback)
            costID = rback.cost.id[1]
            totalCost = totalCost + rback.cost.num[1] * timeLeft
		end
	end
	table.sort(result, function(a,b)
		return a.id < b.id
 	end)
	return result,costID,totalCost
end


function _M.GetReawrdGroup(groupId,playerLv)
	local data = GlobalHooks.DB.FindFirst('RewardGroup',function (item)
        return item.group_id == groupId and (playerLv >= item.level_min and playerLv <= item.level_max)
    end)
    return data
end 

function _M.GetReawrdGroupReward(groupId,playerLv)
	local data = _M.GetReawrdGroup(groupId,playerLv)
	if data then
		return data.reward.item
	end
end 

function _M.ClientGetBackInfoRequest(cb)
 	local msg = {}
    Protocol.RequestHandler.TLClientGetRewardBackInfoRequest(msg, function(resp)
        if cb then
        	local result,costID,totalcost = GetGetRewardBackInfo(resp)
        	
            cb(result,costID,totalcost,resp.s2c_ydayLv)
        end
    end)
end



function _M.ClientFreeGetRewardBackRequest(functionId,cb)
 	local msg = {c2s_FunctionID = functionId}
    Protocol.RequestHandler.TLClientFreeGetRewardBackRequest(msg, function(resp)
        if cb then
            cb(resp)
        end
    end)
end


function _M.ClientCostGetRewardBackRequest(functionId,cb)
 	local msg = {c2s_FunctionID = functionId}
    Protocol.RequestHandler.TLClientCostGetRewardBackRequest(msg, function(resp)
        if cb then
            cb(resp)
        end
    end)
end

local function OnTLClientRewardBackNotify(notify)
 	-- print_r('OnTLClientRewardBackNotify:',notify)
 	EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_getback', showIcon = notify.showIcon })
end

function _M.initial()

end

function _M.fin()

end

 
function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.TLClientRewardBackNotify(OnTLClientRewardBackNotify)
    end
end

return _M