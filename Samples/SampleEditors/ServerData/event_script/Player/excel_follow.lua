function main(ele,questid)
    local last_zone_eid
    local function StartFollow(mapID)
        if last_zone_eid then
            Api.Task.StopEvent(last_zone_eid)
            last_zone_eid = nil
        end
        -- 通过mapID判断特殊地形不召唤
        local map_data = Api.FindExcelData('map/map_data.xlsx/map_data', mapID)
        local ZoneApi = Api.GetZoneApi(Api.GetZoneUUID())
        if ele.maptype ~= 0 and map_data.type == ele.maptype then
            last_zone_eid = ZoneApi.Task.AddEventTo(ID,'Zone/follow_me', arg.PlayerUUID, ele.id)
        else
            for i, v in ipairs(ele.mapid) do
                if v ~= 0 and v == mapID then
                    last_zone_eid = ZoneApi.Task.AddEventTo(ID,'Zone/follow_me', arg.PlayerUUID, ele.id)
                    break
                end
            end
        end
    end
    if questid then
        arg.QuestID = questid
    end
    Api.SetRestartScript(arg.Key,arg.QuestID)
    -- 当前地图判断
    StartFollow(Api.GetMapTemplateID())
    Api.Listen.EnterMap(StartFollow)
    Api.Task.Wait(Api.Listen.QuestState(ele.questid, ele.queststate))
    Api.RemoveRestartScript(arg.Key)
end

function clean(success,reason)
    if not success and Api.GetQuestState(arg.QuestID) == QuestState.Accepted then
        Api.RemoveRestartScript(arg.Key)
    end
end