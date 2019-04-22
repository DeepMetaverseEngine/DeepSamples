local latency = 0
local ServerTime = {
    localTime = os.time(),
    serverTime = os.time(),
    -- 服务器时间-客户端时间
}

function ServerTime.setServerTime(time)
    latency = time - os.time()
    -- localTime = os.time()
    -- serverTime = time or os.time()
end

function ServerTime.getServerTime()
    -- return os.time() + latency
    return GameSceneMgr.Instance.syncServerTime:GetServerTimeUTC()
end

function ServerTime.getPassedTime(time)
    local span = time - ServerTime.getServerTime()
    return span > 0 and span or 0
end

function ServerTime.getCD(time)
    local span = ServerTime.getServerTime() - time
    return span > 0 and span or 0
end

return ServerTime
