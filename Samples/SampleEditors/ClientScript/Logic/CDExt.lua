local ServerTime = require 'Logic/ServerTime'

local CDExt = {}
CDExt.__index = CDExt

function CDExt.New(cd, callback, interval, isAddTime)
    local o = {}
    setmetatable(o, CDExt)
    o._started = false
    o:Reset(cd, callback, interval, isAddTime)
    return o
end

function CDExt:Reset(cd, callback, interval, isAddTime)
    self._cd = cd
    self._callback = callback
    self._begenTime = os.clock()
    local updateFunc = isAddTime and self._onUpdateAdd or self._onUpdate
    if self._started then
        self:Stop()
    end
    self._started = true
    interval = interval or 0.033
    self._timer = LuaTimer.Add(0, interval * 1000, function( id )
        updateFunc(self)
    end)
end

-- function CDExt:Start()
--     if self._started then return end
--     self._started = true
-- end

function CDExt:Stop()
    if not self._started then return end
    self._started = false
    LuaTimer.Delete(self._timer)
end

-- 结束时间戳，单位是秒
function CDExt:SetEndTime(time)
    self._begenTime = os.clock()
    self._cd = time - ServerTime.getServerTime()
    if self._cd < 0 then self._cd = 0 end
    return self._cd
end

-- cd时间，单位是秒
function CDExt:SetCD(time)
    self._begenTime = os.clock()
    self._cd = time
end

function CDExt:_onUpdate()
    local cd = self._cd - (os.clock() - self._begenTime)
    if cd <= 0 then
        cd = 0
        self:Stop()
    end
    self._callback(cd)
end

function CDExt:_onUpdateAdd()
    local cd = self._cd + (os.clock() - self._begenTime)
    if cd <= 0 then
        cd = 0
    end
    self._callback(cd)
end

return CDExt
