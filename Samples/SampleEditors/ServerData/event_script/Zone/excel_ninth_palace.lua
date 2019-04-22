function main(ele)
    assert(ele)
    assert(arg.PlayerUUID)
    Api.Task.PlayerAoiWork(arg.PlayerUUID)
    local ClientApi = Api.GetClientApi(arg)
    local client_id = ClientApi.Task.AddEventByKey('client.excel_ninth_palace', Api.GetNextArg(arg), ele.id)
    return Api.Task.Wait(client_id)
end
