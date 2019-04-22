local _M = {}
_M.__index = _M

--修行之道信息请求
function _M.RequestClientGetPracticeInfo(cb)
    -- print('---------RequestClientGetPracticeInfo----------')
    local request = {}
    Protocol.RequestHandler.ClientGetPracticeInfoRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--修行之道突破请求
function _M.RequestClientPracticeUp(cb)
    -- print('---------RequestClientPracticeUp----------')
    local request = {}
    Protocol.RequestHandler.ClientPracticeUpRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--修行之道渡劫任务请求
function _M.RequestClientPracticeQuest(cb)
    -- print('---------RequestClientPracticeQuest----------')
    local request = {}
    Protocol.RequestHandler.ClientPracticeQuestRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end


function _M.InitNetWork()
  -- print('----------PracticeModel InitNetWork------------')
end

function _M.fin()
  -- print('----------PracticeModel fin------------')
end

function _M.initial()
  -- print('----------PracticeModel inital------------')
end

return _M
