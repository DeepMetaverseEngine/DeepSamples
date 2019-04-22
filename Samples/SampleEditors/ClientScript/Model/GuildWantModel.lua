local _M = {}
_M.__index = _M

local timer = {}


function _M.GetWantControlData()
	local detail = GlobalHooks.DB.GetFullTable('guild_wanted')
	return detail
end

function _M.GetWantData(wantedid)
	local detail = unpack(GlobalHooks.DB.Find('guild_wanted',{wanted_id = wantedid}))
	return detail
end

function _M.GetGuildWantedInfo(cb)
	Protocol.RequestHandler.TLClientGetGuildWantedInfoRequest({}, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

function _M.AccpetGuildWanted(wantedId,cb)
	local msg = { c2s_wantedId = wantedId }
	Protocol.RequestHandler.TLClientAccpetGuildWantedRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

function _M.RefreshGuildWanted(cb)
	Protocol.RequestHandler.TLClientRefreshGuildWantedRequest({}, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

function _M.SubmitGuildWanted(wantedId,cb)
	local msg = { c2s_wantedId = wantedId }
	Protocol.RequestHandler.TLClientSubmitGuildWantedRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

function _M.GiveUpGuildWanted(wantedId,cb)
	local msg = { c2s_wantedId = wantedId }
	Protocol.RequestHandler.TLClientGiveUpGuildWantedRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

function _M.RefreshTimeRequest(cb)
	Protocol.RequestHandler.TLClientGuildWantedRefreshTimeRequest({}, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

function _M.RefreshCallTime(index)
	_M.Calltime[index] = 120
	if timer[index] then
		LuaTimer.Delete(timer[index])
	end
	timer[index] = LuaTimer.Add(0,1000,function()
		if _M.Calltime[index] > 0 then
			_M.Calltime[index] = _M.Calltime[index] - 1
			return true
		else
			_M.Calltime[index] = 0
			-- LuaTimer.Delete(timer[index])
			return false
		end
	end)
end

function _M.fin()
	for i=3,1,-1 do
		if timer[i] then
			LuaTimer.Delete(timer[i])
			timer[i] = nil
		end		
	end
end


_M.Calltime = {0,0,0}
return _M