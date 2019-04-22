local _M = {}
_M.__index = _M


local function GetGroup(mainindex,subindex)
	local detail = unpack(GlobalHooks.DB.Find('RankList', {group = mainindex,sub = subindex}))
	return detail
end

local function FormRanklist(cb)
	local msg = {}
	Protocol.RequestHandler.ClientGetRankBoardDataRequest(msg, function(rsp)
        if cb then
	        cb(rsp)
	    end
    end)
end

local function GetRankListDetail(group,child,cb)
	local msg = { group_id = group ,child_id = child }
	Protocol.RequestHandler.ClientGetRanklistDataRequest(msg, function(rsp)
        if cb then
	        cb(rsp)
	    end
    end)
end



_M.GetGuildSnap = GetGuildSnap
_M.GetGroup = GetGroup
_M.FormRanklist = FormRanklist
_M.GetRankListDetail = GetRankListDetail
return _M