function main(ele)
    assert(ele)
    assert(arg.PlayerUUID)
    assert(arg.ClientUUID)
    Api.Task.PlayerAoiWork(arg.PlayerUUID)
    local indexs = {}
    local allCount = ele.transverse * ele.longitudinal
    for i = 1, allCount do
        indexs[i] = i
    end
    -- 洗牌
    indexs = Api.UpsetArray(indexs)
    local ClientApi = Api.GetClientApi(arg)
    local client_id = ClientApi.Task.AddEventByKey('client.excel_supperzzle', Api.GetNextArg(arg), ele.id, indexs)
    local ok, index = Api.Task.Wait(client_id)
    return ok
end
