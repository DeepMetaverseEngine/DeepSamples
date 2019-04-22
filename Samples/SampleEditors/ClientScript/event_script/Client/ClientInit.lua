local function TryCheckDataVersion()
    -- print('TryCheckDataVersion ---------')
    local version = Api.GetExcelVersion('_luaversion_')
    local rid = Api.Protocol.Task.Request('SyncExcelData', {path = '_luaversion_', version = version})
    local ok, ret = Api.Task.Wait(rid)
    if ok and ret.data then
        local notsame = Api.SetExcelData('_luaversion_', ret.data)
        if #notsame > 0 then
            -- pprint('not same', notsame)
            local rrid = Api.Protocol.Task.Request('SyncExcelData', {paths = notsame})
            local ok, ret = Api.Task.Wait(rrid)
            if ok then
                -- pprint('ret ---',ret)
                for k, v in pairs(ret) do
                    if v.url then
                        local okload, dataload = Api.Task.Wait(Api.Task.LoadWWWLua(v.url))
                        if okload then
                            v.data = dataload
                        end
                    end
                    local data = v.data or v.bytes
                    if data then
                        Api.SetExcelData(k, data, v.version)
                    end
                end
            end
        end
    end
    -- local version = Api.GetExcelVersion('__virtual/message') or 'unknown'
    -- local rid = Api.Protocol.Task.Request('SyncExcelData', {path = '__virtual/message', version = version})
    -- local ok, ret = Api.Task.Wait(rid)
    -- local data = ret.data or ret.bytes
    -- if ok and data then
    --     Api.SetExcelData('__virtual/message', data, ret.version)
    --     Api.FireEventMessage('__virtual.message')
    -- end
end

function main(...)
    Api.Listen.Message(
        function(ename, params)
            Api.FireEventMessage('EventScrpt.' .. ename, params)
        end
    )

    local recheck_sec = 30
    Api.Listen.AddPeriodicSec(
        recheck_sec,
        function()
            Api.Task.AddEventTo(ID, TryCheckDataVersion)
        end
    )
    Api.Task.Sleep(1.2)
    TryCheckDataVersion()
    Api.Task.WaitAlways()
end
