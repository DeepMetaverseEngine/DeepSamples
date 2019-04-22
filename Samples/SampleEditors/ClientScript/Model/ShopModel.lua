local _M = {}
_M.__index = _M

local ServerTime = require 'Logic/ServerTime'

local function CheckNotOpen(startDate,endDate)
    local serverTime = ServerTime.getServerTime()
 
    -- local shicha = serverTime:Subtract(System.DateTime.UtcNow).TotalHours
    -- startDate = startDate:ToUniversalTime():AddHours(shicha)
    -- endDate = endDate:ToUniversalTime():AddHours(shicha)

    startDate = startDate:ToUniversalTime() 
    endDate = endDate:ToUniversalTime()
    -- 未开始和已结束
    if System.DateTime.Compare(serverTime,startDate) < 0 or System.DateTime.Compare(serverTime,endDate) > 0 then
        return true
    end
    return false
end

local function GetTimeSpan(dateTime)
    local endDate = System.DateTime.Parse(dateTime)
    
    -- local shicha = serverTime:Subtract(System.DateTime.UtcNow).TotalHours
    -- endDate = endDate:ToUniversalTime():AddHours(shicha)
    endDate = endDate:ToUniversalTime()

    local timeSpan = endDate:Subtract(ServerTime.getServerTime())
    if timeSpan.TotalSeconds > 0 then
        local day = timeSapan.TotalDays
        if day > 1 then
            return  math.modf(day) .. Constants.Text.shop_Day
        end
        local hour = timeSapan.TotalHours
        if hour > 1 then
            return math.modf(hour) .. Constants.Text.shop_Hour
        end
        return  Constants.Text.shop_LessHour
    end
end

function _M.GetTimeText(itemData)
  -- 
    local startTime = itemData.sell_star_time 
    if string.IsNullOrEmpty(startTime) then
        return ""
    end

    local endTime = item.sell_end_time
    if string.IsNullOrEmpty(endTime) then
        return ""
    end

    return GetTimeSpan(dateTime)
end 

-- 客户端过滤显示道具 return true是要过滤掉
local function filterItem(showlimit,startTime,endTime)

    local playerLevel = DataMgr.Instance.UserData.Level
    for k,filterKey in pairs(showlimit.key) do
        if filterKey == 'pLevel' then
            local minLv = showlimit.minval[k]
            local maxLv = showlimit.maxval[k]

            if playerLevel < minLv then
                return true
            end

            if maxLv ~= -1 and playerLevel > maxLv then
                return true
            end
        end
    end 

    if (not string.IsNullOrEmpty(startTime)) and (not string.IsNullOrEmpty(endTime)) then
        local startDt = System.DateTime.Parse(startTime)
        local endDt = System.DateTime.Parse(endTime)
        return CheckNotOpen(startDt,endDt)
    end
 
    return false
end

function _M.GetStoreData(storeType,saleMap)
    
    local storeData = GlobalHooks.DB.Find('Store',function (item)
        return item.store_type == storeType and (not filterItem(item.showlimit,item.sell_star_time,item.sell_end_time)) 
            -- and (salelist == nil or #salelist == 0 or table.indexOf(salelist,item.id))
            and (saleMap[item.item_id] ~= nil)
    end)

    table.sort(storeData, function(a,b)
        return a.item_order < b.item_order
    end)

    return storeData
end


function _M.RequestGetStoreBoughtInfo(storeType,cb)

  local request = 
  {
      c2s_store_type = storeType,
  }

  Protocol.RequestHandler.ClientGetStoreBoughtInfoRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

function _M.RequestBuyItem(storeType,itemId,buNum,cb)

  local request = 
  {
      c2s_store_type = storeType,
      c2s_item_id = itemId,
      c2s_num = buNum,
  }
 
  Protocol.RequestHandler.ClientStoreBuyItemRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


function _M.InitNetWork()
  -- print('----------MountModel InitNetWork------------')
end

function _M.fin()
  -- print('----------MountModel fin------------')
end

function _M.initial()
  -- print('----------MountModel inital------------')
end


return _M