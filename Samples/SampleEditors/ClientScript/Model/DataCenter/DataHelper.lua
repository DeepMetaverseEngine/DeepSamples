local DataTag = require 'Model/DataCenter/DataTag'
local DataHelper = require 'event_script/DataHelper'
GlobalHooks.DB = GlobalHooks.DB or {}
GlobalHooks.DB.GetFullTable = DataHelper.GetFullTable
GlobalHooks.DB.FindFirst = DataHelper.FindFirst
GlobalHooks.DB.Find = DataHelper.Find
DataHelper.SetRootPath('Data/')
DataHelper.SetDataTag(DataTag)

--获取全局配置
function GlobalHooks.DB.GetGlobalConfig(name)
    local ele = GlobalHooks.DB.FindFirst('GameConfig', name)
    if not ele then
        return nil
    elseif ele.paramtype == 'NUMBER' then
        return tonumber(ele.paramvalue)
    else
        return ele.paramvalue
    end
end

function GlobalHooks.DB.SyncExcelData(tb_name, cb)
    local Api = EventApi
    Api.Task.StartEvent(
        function()
            local version = DataHelper.GetVersion(tb_name)
            local path = DataHelper.GetPath(tb_name)
            local rid = Api.Protocol.Task.Request('SyncExcelData', {path = path, version = version})
            local ok, ret = Api.Task.Wait(rid)
            if ok and ret.data then
                -- print('version ',version, ret.version)
                DataHelper.SetDataTable(tb_name, ret.data, ret.version)
            end
            cb()
        end
    )
end

return {}
