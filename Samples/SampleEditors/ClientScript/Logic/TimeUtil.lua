local ServerTime = require('Logic/ServerTime')
local Util = require 'Logic/Util'

local TimeUtil = {
    ONE_DAY = 24 * 3600,
}

-- 获取减去服务器时间后的剩余时间(second)
function TimeUtil.TimeLeftSec( datetime )
    local timeSnap = ServerTime.getServerTime() - datetime
    local sec = math.floor(timeSnap.TotalSeconds)
    return sec
end

--计算复活倒计时 >0 表示还在读秒，<=0表示可以复活
function TimeUtil.CaluReliveLeftSec(datetime)
    local timeSnap = datetime - ServerTime.getServerTime()
    local sec = math.floor(timeSnap.TotalSeconds)
    return sec
end

-- 把秒转换成最大时间单位，优先级：天 小时 分钟 秒
function TimeUtil.FormatToCN( sec )
    local timeStr
    sec = math.floor(sec)
    if sec < 60 then
        timeStr = Util.GetText('common_sec', sec)
    elseif sec < 3600 then
        timeStr = Util.GetText('common_min', math.floor(sec / 60))
    elseif sec < 86400 then
        timeStr = Util.GetText('common_hour', math.floor(sec / 3600))
    else
        timeStr = Util.GetText('common_day', math.floor(sec / 86400))
    end
    return timeStr
end

-- 把秒转换成详细时间：天 时 分 秒
function TimeUtil.FormatToAllCN( sec )
    local timeStr = ""
    local day,hour,min
    sec = math.floor(sec)
    day = math.floor(sec / 86400)
    sec = sec % 86400
    hour = math.floor(sec / 3600)
    sec = sec % 3600
    min = math.floor(sec / 60)
    sec = sec % 60
    if day ~= 0 then
        timeStr = timeStr..Util.GetText('business_activity_day',day)
    end
    if hour ~= 0 then
        timeStr = timeStr..Util.GetText('business_activity_hour',hour)
    end
    if min ~= 0 then
        timeStr = timeStr..Util.GetText('business_activity_min',min)
    end
    if sec ~= 0 then
        timeStr = timeStr..Util.GetText('business_activity_sec',sec)
    end
    return timeStr
end

function TimeUtil.FormatToAllCN2( sec )
    local timeStr = ""
    local day,hour,min
    sec = math.floor(sec)
    day = math.floor(sec / 86400)
    sec = sec % 86400
    hour = math.floor(sec / 3600)
    sec = sec % 3600
    min = math.floor(sec / 60)
    if day ~= 0 then
        timeStr = timeStr..Util.GetText('common_day',day)
    end
    if hour ~= 0 then
        timeStr = timeStr..Util.GetText('common_hour',hour)
    end
    if min ~= 0 then
        timeStr = timeStr..Util.GetText('common_min',min)
    end
    return timeStr
end

function TimeUtil.SecToTimeformat( sec )
    local hh,mm,ss
    
    hh = sec >= 3600 and math.floor(sec/3600) or 0
    mm = sec%3600 >= 60 and math.floor((sec%3600)/60) or 0
    ss = sec%60
    if string.len(hh) == 1 then
        hh='0'..hh
    end
    if string.len(mm) == 1 then
        mm='0'..mm
    end
    if string.len(ss) == 1 then
        ss='0'..ss
    end
    return hh..':'..mm..':'..ss
end

function TimeUtil.SecToTimeformatToMS( sec )
    
    if sec <=0 then
        return "00:00"
    end

    local minute = math.fmod(math.floor(sec/60), 60)  
    local second = math.fmod(sec, 60) 

    if string.len(minute) == 1 then
        minute='0'..minute
    end
    if string.len(second) == 1 then
        second='0'..second
    end 
    local time = string.format("%s:%s", minute, second)  
  
    return time  
end

function TimeUtil.CustomTodayTimeToUtc(time)

   return FunctionUtil.ParseServerTime(time)
   -- local timeAry = string.split(time, ':')
   -- if #timeAry < 3 then
   --   UnityEngine.Debug.LogError("time format is hh:MM:ss not "..time)
   --   return
   -- end
  
   -- return GameSceneMgr.Instance.syncServerTime:GetTodayTimeToUtcTime(time)
end

function TimeUtil.CustomTodayOfWeekday()
   return GameSceneMgr.Instance.syncServerTime:GetTodayOfWeek()
end


-- format default %M:%S
function TimeUtil.formatCD(format, cd)
    if cd < 0 then cd = 0 end
    local date = {
        year = 1972, month = 0, day = 0, hour = 0, min = 0, sec = 0, isdst = false,
    }
    local time = os.time(date)
    if os.date("%H", time) ~= "00" then
        date.isdst = true
        time = os.time(date)
    end
    return os.date(format or "%M:%S", time + cd)
end

function TimeUtil.formatCD2(format, endTime)
    local cd = ServerTime.getCD(endTime)
    return TimeUtil.formatCD(format, cd)
end

function TimeUtil.format(format, time)
    return os.date(format or "%M:%S", time or ServerTime.getServerTime())
end

-- format Y-M-D
function TimeUtil.parseYMD(str, sep)
    local t = CommonFunc._stringSplit(str , sep or '-')
    local date = { year = tonumber(t[1]), month = tonumber(t[2]), day = tonumber(t[3]), hour = 0, min = 0, sec = 0 }
    return os.time(date)
end

-- format H:M:S
function TimeUtil.parseCDHMS(str, sep)
    local t = CommonFunc._stringSplit(str, sep or ':')
    local cd = tonumber(t[1]) * 3600
    if t[2] then
        cd = cd + tonumber(t[2]) * 60
    end
    if t[3] then
        cd = cd + tonumber(t[3])
    end
    return cd
end

function TimeUtil.inTime(time1, time2, now)
    now = now or ServerTime.getServerTime()
    return time1 < now and time2 > now
end

function TimeUtil.inTime2(date1, date2, time1, time2, now)
    now = now or ServerTime.getServerTime()
    local nowTime = TimeUtil.todayTime(now)
    local nowDate = TimeUtil.date(now)
    date1 = TimeUtil.date(date1)
    date2 = TimeUtil.date(date2)
    time1 = time1 > TimeUtil.ONE_DAY and TimeUtil.todayTime(time1) or time1
    time2 = time2 > TimeUtil.ONE_DAY and TimeUtil.todayTime(time2) or time2
    if time2 == 0 then
        time2 = TimeUtil.ONE_DAY
        date2 = date2 - TimeUtil.ONE_DAY
    end

    if date1 > nowDate or nowDate > date2 then
        return false
    end

    if time1 <= time2 then
        return time1 <= nowTime and nowTime <= time2
    end
    return time1 <= nowTime or nowTime <= time2
end

function TimeUtil.todayTime(time)
    local dateT = os.date("*t", time)
    return dateT.hour * 3600 + dateT.min * 60 + dateT.sec
end

function TimeUtil.date(time)
    local dateT = os.date("*t", time)
    dateT.hour = 0
    dateT.min = 0
    dateT.sec = 0
    return os.time(dateT)
end

return TimeUtil
