function main(ele)
    local id = Api.Task.AddEvent('Player/start_client_qte', ele, arg, 'client.excel_alchemy')
    local ok, ret = Api.Task.Wait(id)
    return ok, ret
end
