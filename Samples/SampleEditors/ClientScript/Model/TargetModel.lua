local _M = {}
_M.__index = _M


--通过等级获取数据
local function GetTargetDataByLevel(targetLevel)
	local targetData=unpack(GlobalHooks.DB.Find('TargetData',{target_level=targetLevel}))
    return targetData
end


--获取所有奖励目标数据
local function GetAllTargetData()
	local allTargetData=GlobalHooks.DB.GetFullTable('TargetData')
	return allTargetData
end


--领取物品
local function RequestGetItem(recordnum,cb)
  local request = {c2s_lv=recordnum}
  Protocol.RequestHandler.TLClientTargetLvGetItemRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


---------------------Net-------------------

--升级notify
local function OnLevelChangeNotify(notify)
	local params={lv=notify.s2c_lv}
    EventManager.Fire('Event.TargetSystem.LevelUp',params)
end


function _M.InitNetWork(initNotify)
  if initNotify then
    Protocol.PushHandler.TLClientTargetLvChangeNotify(OnLevelChangeNotify)
  end
end


_M.RequestGetItem=RequestGetItem
_M.GetTargetDataByLevel=GetTargetDataByLevel
_M.GetAllTargetData=GetAllTargetData


return _M