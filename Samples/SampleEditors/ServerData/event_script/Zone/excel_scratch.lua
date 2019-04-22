function main(ele)
    Api.Task.PlayerAoiWork(arg.PlayerUUID)
    local indexs = {}
    for i, v in ipairs(ele.card) do
        if string.IsNullOrEmpty(v) then
            break
        end
        indexs[i] = i
    end
    local indexs = Api.UpsetArray(indexs)
    local client_id = Api.Task.AddEventByKey('client.excel_scratch', Api.GetNextArg(arg), ele.id, indexs)
    local ok, index = Api.Task.Wait(client_id)
    if ok then
        local event = ele.cardevent[index]
        if not string.IsNullOrEmpty(event) then
            arg.ZoneUUID = Api.GetZoneUUID()
            return Api.Task.Wait(Api.Task.AddEventByKey(event, Api.GetNextArg(arg)))
        end
    end
    return ok, index
end
