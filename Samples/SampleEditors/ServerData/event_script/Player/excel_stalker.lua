function main(ele)
    assert(ele)
    local mapID = Api.GetMapTemplateID()
    if ele.mapid ~= mapID then
        Api.Task.Wait(Api.Listen.EnterMap(ele.mapid))
    end
    local eid = Api.Task.AddEventByKey('zone_stalker.' .. ele.id, Api.GetNextArg(arg))
    local ok, ret = Api.Task.Wait(eid)
    return ok, ret
end
