function main()
    local ele = Api.FindFirstExcelData('personal_carriage/personal_carriage.xlsx/personal_carriage', {carriage_type = 2})
    return Api.Task.Wait(Api.Quest.Task.AcceptQuest(ele.quest_id))
end
