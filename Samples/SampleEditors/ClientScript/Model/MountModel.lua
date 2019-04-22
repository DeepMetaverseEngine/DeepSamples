local ItemModel = require 'Model/ItemModel'

local _M = {}
_M.__index = _M

_M.veinId = 0
_M.listener = nil

local function updateNetData(netData)
    local userData = {}
    local userMoutMap = {}
    if netData.mounts ~= nil then
        for i,v in pairs(netData.mounts) do
            userMoutMap[v] = i
        end
    end
    userData.userMoutMap = userMoutMap
    userData.currentMountId  = netData.currentId
    userData.veinId = netData.veinId
    _M.veinId = netData.veinId
    return userData
end

local function GetMountBySheetType(sheetType,userMoutMap,currentMountId)
  -- print('GetMountBySheetType sheetType:',sheetType)
  userMoutMap = userMoutMap or {}
  local mounts = GlobalHooks.DB.Find('Mount',{sheet_type=tonumber(sheetType)})

  table.sort(mounts, function(a,b)
      if currentMountId ~= 0 then
          if currentMountId == a.avatar_id then 
              return true
          elseif currentMountId == b.avatar_id then
              return false
          end
      end

      local lhs = userMoutMap[a.avatar_id]
      local rhs = userMoutMap[b.avatar_id]

      if  lhs and rhs then
      elseif lhs then 
          return true
      elseif  rhs then
          return false
      end 
      return a.order < b.order
  end)
  return mounts
end


local function GetMountVeinBgByRank(veinRank)
    local data = GlobalHooks.DB.FindFirst('MountVeinName',{vein_rank=veinRank})
    return data.backgroud
end


local function GetMountVeinNameByRank(veinRank)
    local data = GlobalHooks.DB.FindFirst('MountVeinName',{vein_rank=veinRank})
    return data.vein_name
end


local function GetMountVeinNameByLevel(veinLevel)
    -- print('GetMountVeinNameByLevel:',veinLevel)
    local data = GlobalHooks.DB.FindFirst('MountVeinName',{vein_level=veinLevel})

    return data.vein_name
end



local function GetVeinData(veinId)
    return GlobalHooks.DB.FindFirst('MountVein',{id = veinId})
end


local function GetNextVeinData(veinId)
    local nextVeinId = veinId + 1
    return GlobalHooks.DB.FindFirst('MountVein',{id = nextVeinId})
end



local function TryGetAttribute(all, key)
    -- print_r('TryGetAttribute:',all[key])
    return all[key] or 0
end

local function TryAddAttribute(all, key, v)
    --print('TryAddAttribute',key, v)
    if v ~= 0 then
        table.insert(all, {Name = key, Value = v})
    end
end

local function GetXlsFixedAttribute(static_data)
    local all = {}
    TryAddAttribute(all, 'attack', static_data.attack)
    TryAddAttribute(all, 'maxhp', static_data.maxhp)
    TryAddAttribute(all, 'defend', static_data.defend)
    TryAddAttribute(all, 'mdef', static_data.mdef)
    TryAddAttribute(all, 'through', static_data.through)
    TryAddAttribute(all, 'block', static_data.block)
    TryAddAttribute(all, 'crit', static_data.crit)
    TryAddAttribute(all, 'rescrit', static_data.rescrit)
    TryAddAttribute(all, 'hit', static_data.hit)
    TryAddAttribute(all, 'dodge', static_data.dodge)
    TryAddAttribute(all, 'targettomonster', static_data.targettomonster)
    return all
end


local function GetFightByAttr(attrs)
    local ret = 0
    for k, v in pairs(attrs or {}) do
        local Name = v.Name
        local Value = v.Value
        local attr = GlobalHooks.DB.FindFirst('Attribute', {key = Name})
        if attr and Value then
            ret = ret + Value * (attr.fight / 10000)
        end
    end
    return math.floor(ret)
end

local function GetFightByVeinId(veinId)
    local veinData = GetVeinData(veinId)
    local all = GetXlsFixedAttribute(veinData)
    local fight = GetFightByAttr(all)
    return fight
end

--------------------------------------Net

local function RequestRidingMount(ride,cb)
  -- print('---------RequestRidingMount----------')
  local request = {ride = ride}
  -- local t1 = os.clock()
  -- print("ClientRidingMountRequest t1 : " ..t1)
  Protocol.RequestHandler.ClientRidingMountRequest(request, 
      function(rsp)
          if cb then
              cb()
          end
      end,
      function()
          if cb then
              cb()
          end
      end)
end

-- 请求坐骑信息
local function RequestMountInfo(cb,force)
  -- print('---------RequestMountInfo----------')
  local request = {}
 
  Protocol.RequestHandler.ClientGetRoleMountInfoRequest(request, function(rsp)
        local userData = updateNetData(rsp.s2c_data)
        if cb then
            cb(userData)
        end
  
  end, PackExtData(force, force))
end


local function ChangeMount(mountId,cb)
  -- print('---------RequestMountInfo----------')
  local request = {mountId = mountId}
  Protocol.RequestHandler.ClientChangeMountRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end

local function RequestUnlock(mountId,cb)
  -- print('---------RequestMountInfo----------')
  local request = {mountId = mountId}
 
  Protocol.RequestHandler.ClientUnlockMountRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end

local function RequestStarUp(onekey,cb,failedCb)
  -- print('---------RequestStarUp----------')
  local request = {oneKey = onekey}
  Protocol.RequestHandler.ClientMountStarUpRequest(request, 
    function(rsp)
      _M.veinId = rsp.veinId
      --检测红点
      
      if cb then
        cb(rsp)
      end
    end,
    function()
      if failedCb then
        failedCb()
      end
    end)
end

local function OnRideStatusNotify(notify)
    EventManager.Fire("Event.UI.RideStatusChange", {isRideFailed=notify.isRideFailed,rideStatus=notify.rideStatus})   
end



local function OnTLClientUnlockMountNotify(notify)
    -- 该功能改成了客户端直接判断解锁 
    -- EventManager.Fire("Event.UI.UnlockMount", {mountId=notify.mountId})   
end
 
local function CheckVeinUp()
    if not GlobalHooks.IsFuncOpen('MountFrame') then
        return
    end
    local veindb = GetVeinData(_M.veinId + 1)
    if veindb then
      local costs = ItemModel.ParseCostAndCostGroup(veindb)
      local enough = ItemModel.IsCostAndCostGroupEnough(costs)
      GlobalHooks.UI.SetRedTips('mount', enough and 1 or 0)
    else
      GlobalHooks.UI.SetRedTips('mount', 0)
    end
end

function _M.OnBagReady()
    if not GameGlobal.Instance.netMode then 
        return 
    end

    local veindb = GetVeinData(1)
    _M.listener = ItemModel.ListenCostXlsLine(veindb, function()
        CheckVeinUp()
    end)

    RequestMountInfo(function (resp)
        CheckVeinUp()
    end,false)
end



function _M.InitNetWork(initNotify)
  -- print('----------MountModel InitNetWork------------')
    if initNotify then
      Protocol.PushHandler.RideStatusNotify(OnRideStatusNotify)
      Protocol.PushHandler.TLClientUnlockMountNotify(OnTLClientUnlockMountNotify)
    end
end

  
function _M.initial()

end

function _M.fin()
    if _M.listener then
        _M.listener:Dispose()
        _M.listener = nil
    end
end

_M.GetMountBySheetType= GetMountBySheetType
_M.GetMountVeinBgByRank = GetMountVeinBgByRank
_M.GetMountVeinNameByRank= GetMountVeinNameByRank
_M.GetMountVeinNameByLevel= GetMountVeinNameByLevel
_M.GetVeinData= GetVeinData
_M.GetNextVeinData= GetNextVeinData
_M.GetFightByAttr= GetFightByAttr
_M.GetFightByVeinId= GetFightByVeinId
_M.TryGetAttribute= TryGetAttribute
_M.GetXlsFixedAttribute= GetXlsFixedAttribute
 

_M.RequestRidingMount = RequestRidingMount
_M.RequestMountInfo = RequestMountInfo
_M.ChangeMount = ChangeMount
_M.RequestUnlock = RequestUnlock
_M.RequestStarUp = RequestStarUp

_M.CheckVeinUp = CheckVeinUp

return _M