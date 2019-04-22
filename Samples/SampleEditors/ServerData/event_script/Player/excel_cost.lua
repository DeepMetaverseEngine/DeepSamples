function main(ele)
    local agree = Api.Task.Wait(Api.Task.AddEventByKey('client_excel_cost.' .. ele.id, Api.GetNextArg(arg)))
    if agree then
        local id = Api.Task.CostItem(ele.cost.id, ele.cost.num, ele.costgroup.id, ele.costgroup.num)
        return Api.Task.Wait(id)
    else
        return agree
    end
end
