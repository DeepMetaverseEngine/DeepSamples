function main(ele)
	local ids = {}
	local weights = {}
	for i,v in ipairs(ele.event.id) do
		if not string.IsNullOrEmpty(v) then
			table.insert(ids, v)
			table.insert(weights, ele.event.weight[i])
		end
    end
	local result = Api.RandomWeight(weights,ele.times)
	
	local event_ids = {}
    for _,v in ipairs(result) do
		local index = v + 1
		if ele.sequence == 0 then
			table.insert(event_ids, Api.Task.AddEventByKey(ids[index],Api.GetNextArg(arg)))
		else
			local ok,ret = Api.Task.Wait(Api.Task.AddEventByKey(ids[index],Api.GetNextArg(arg)))
			if not ok then
				return ok, ret
			end
		end
	end
	if ele.sequence == 0 then
		local event_ids = {}
		for _,v in ipairs(result) do
			local index = v + 1
			table.insert(event_ids, Api.Task.AddEventByKey(ids[index],Api.GetNextArg(arg)))
		end
		return Api.Task.WaitParallel(event_ids)
	else
		for _,v in ipairs(result) do
			local index = v + 1
			local ok,ret = Api.Task.Wait(Api.Task.AddEventByKey(ids[index],Api.GetNextArg(arg)))
			if not ok then
				return ok, ret
			end
		end
	end

end 