local _M = {}
_M.__index = _M

local function GetGodIsland()
	return GlobalHooks.DB.GetFullTable('god_island')
end

local function GetCheckPoint(chapterId)
	-- body
	return  GlobalHooks.DB.Find('god_island_checkpoint',{chapter_id = chapterId})
end

function _M.GetFunctionIdByCheckPoint(checkPointId)
    return  unpack(GlobalHooks.DB.Find('god_island_checkpoint',{checkpoint_id = checkPointId}))
end


local function ReqIslandInfo(cb)
  -- print('---------RequestStarUp----------')
  local request = {}
  Protocol.RequestHandler.TLClientGetIslandInfoRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end

local function ReqEnterIsland(checkpointId,cb)
  -- print('---------RequestStarUp----------')
  local request = {c2s_CheckPointId = checkpointId}
  Protocol.RequestHandler.TLClientEnterIslandRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end

local function ReqGetPass1stReward(checkpointId,cb)
  -- print('---------RequestStarUp----------')
  local request = {c2s_CheckPointId = checkpointId}
  Protocol.RequestHandler.TLClientGetFirstPassRewardRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


--请求扫荡仙灵岛
function _M.RequestSweepIsland(functionid,cb)
    local request = {s2c_functionid = functionid}
    Protocol.RequestHandler.TLClientSweepRequest(request, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end



_M.GetGodIsland = GetGodIsland 
_M.GetCheckPoint = GetCheckPoint
_M.ReqIslandInfo = ReqIslandInfo
_M.ReqEnterIsland = ReqEnterIsland
_M.ReqGetPass1stReward = ReqGetPass1stReward

return _M