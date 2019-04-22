function main(id)
    local excel_data = Api.GetExcelByEventKey('treasure.' .. id)
    assert(excel_data)

    local id = Api.Task.AddEvent('Client/ui_qte', excel_data, true, true)
    return Api.Task.Wait(id)
end

function clean()
end
