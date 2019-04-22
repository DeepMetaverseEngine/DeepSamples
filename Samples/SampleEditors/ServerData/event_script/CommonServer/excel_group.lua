function main(ele)
	if ele.sequence == 0 then
		local event_ids = {}
        for _,v in ipairs(ele.event.id) do
            if not string.IsNullOrEmpty(v) then
                table.insert(event_ids, Api.Task.AddEventByKey(v,Api.GetNextArg(arg)))
            end
		end
		return Api.Task.WaitParallel(event_ids)
    else
        for _,v in ipairs(ele.event.id) do
            if not string.IsNullOrEmpty(v) then
                local ok,ret = Api.Task.Wait(Api.Task.AddEventByKey(v,Api.GetNextArg(arg)))
                if not ok then
                    return ok, ret
                end
            end
		end
	end
end
