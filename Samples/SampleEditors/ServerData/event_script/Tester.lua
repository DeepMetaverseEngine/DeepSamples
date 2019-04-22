local function TransportPlayerByExcel(ele)
    if not ele.mapid then
        return
    end
    Api.Task.Wait(Api.Task.TransportPlayerByExcel({mapid = ele.mapid}))
    Api.Task.Wait(Api.Task.TransportPlayerByExcel(ele))
    if not ClientApi.IsActorReady() then
        Api.Task.Wait(ClientApi.Listen.ActorReady())
    end
    if ele.monsterid then
        ClientApi.SetActorAutoGuard(true)
    end
end

local function Default(key, ele)
    TransportPlayerByExcel(ele)
    return Api.Task.AddEventByKey(key, Api.GetNextArg(arg))
end

local function Default_RandomTransport(key, ele)
    if Api.RandomPercent(50) then
        TransportPlayerByExcel(ele)
        return Api.Task.AddEventByKey(key, Api.GetNextArg(arg))
    else
        local id = Api.Task.AddEventByKey(key, Api.GetNextArg(arg))
        Api.Task.Sleep(0.1)
        TransportPlayerByExcel(ele)
        return id
    end
end

local test_case = {
    item_pub = Default,
    item_per = Default_RandomTransport,
    message = Default,
    scratch = Default,
    reward = Default,
    alchemy = Default,
    supperzzle = Default,
    turntable = Default,
    horse = Default,
    ninth_palace = Default,
    rift_per = Default_RandomTransport,
    rift_pub = Default,
    monster_per = Default_RandomTransport,
    monster_pub = Default,
    transfer = Default,
    follow = Default,
    stalker = Default,
    personal_carriage = Default,
    map = function(key, ele, results)
        local name, mapid = unpack(string.split(key, '.'))
        Api.Task.Wait(Api.Task.TransportPlayer({MapTemplateID = mapid}))
        local ids = {}
        local function main()
            for _, v in ipairs(ele) do
                local id = Api.Task.AddEventByKey(v, Api.GetNextArg(arg))
                ids[id] = v
            end
            Api.Task.Sleep(0.5)
            for id, v in pairs(ids) do
                if Api.IsEventStoped(id) and not Api.IsEventSuccess(id) then
                    results[key .. '-' .. v] = 'false'
                end
            end
            Api.Task.Wait()
        end
        local function clean(success, reason)
        end
        return Api.Task.AddEvent({main = main, clean = clean})
    end
}

local function CollectEventKeys(name)
    if name == 'map' then
        local maps = Api.FindExcelData('map/map_data.xlsx/map_data', {})
        local ret = {}
        for _, v in ipairs(maps) do
            local key = 'map.' .. v.id
            for __, vv in ipairs(v.init_event) do
                if not string.IsNullOrEmpty(vv) then
                    ret[key] = ret[key] or {}
                    table.insert(ret[key], vv)
                end
            end
        end
        return ret
    else
        return Api.CollectEventKeys(name)
    end
end

local function GetExcelByEventKey(key)
    if string.starts(key, 'map') then
        local name, id = unpack(string.split(key, '.'))
        local ele = Api.FindExcelData('map/map_data.xlsx/map_data', tonumber(id))
        local ret = {}
        for __, vv in ipairs(ele.init_event) do
            if not string.IsNullOrEmpty(vv) then
                table.insert(ret, vv)
            end
        end
        return ret
    else
        return Api.GetExcelByEventKey(key)
    end
end
function main(key, sec, timeout)
    arg = Api.GetArg()
    timeout = timeout or 60
    local name, id
    if key then 
        name,id = unpack(string.split(key, '.'))
    end
    local testfn = name and test_case[name]
    if not testfn and name then
        return
    end
    local results = {}
    ClientApi = Api.GetClientApi(arg)
    local function each_test(k, ele)
        Api.Task.Sleep(1)
        local id = testfn(k, ele, results)
        local timeout = Api.Task.DelaySec(timeout)
        if sec and sec > 0 then
            Api.Task.Sleep(sec)
            if Api.IsEventStoped(id) then
                if not Api.IsEventSuccess(id) then
                    return false, 'false'
                end
            else
                Api.Task.StopEvent(id)
            end
        else
            local ok, successID, reason = Api.Task.WaitSelect(id, timeout)
            if successID == id then
                if not Api.IsEventSuccess(id) then
                    return false, reason or 'false'
                end
            end
        end
    end

    local function each_event(k, ele)
        local ok, reason = Api.Task.Wait(Api.Task.AddEvent(each_test, k, ele))
        if not ok then
            results[k] = reason
        end
    end

    local function each_keyevents(event_name)
        local all = CollectEventKeys(event_name)
        local index = 1
        for k, ele in pairs(all) do
            print(string.format('===========TEST %s %d/%d===========', k, index, table.len(all)))
            index = index + 1
            each_event(k, ele)
        end
    end
    if not name then
        local allnames = Api.CollectEventTypes()
        for _,v in ipairs(allnames) do
            each_keyevents(v)
        end
    elseif not id then
        each_keyevents(name)
    else
        local ids = string.split(id, '|')
        for index, v in ipairs(ids) do
            local k = name .. '.' .. v
            print(string.format('===========TEST %s %d/%d===========', k, index, #ids))
            local ele = GetExcelByEventKey(k)
            each_event(k, ele)
        end
    end
    Api.Task.Wait()
    pprint('===========Test Failed===========\n', results, '\n=================================================================')
end
