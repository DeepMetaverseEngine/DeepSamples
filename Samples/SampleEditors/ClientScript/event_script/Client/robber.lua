local RobberMapID = 500080
function main(mapid)
    mapid = mapid or RobberMapID
    local function CheckTypeFunction(st)
        return st == 2100 or st == 2200
    end

    local quests = Api.Quest.FindAcceptByField({sub_type = CheckTypeFunction})
    if #quests > 0 then
        for _, v in ipairs(quests) do
            local qData = Api.GetQuestData(v)
            if qData.sub_type == 2100 then
                local eid = Api.UI.Task.ShowConfirmAlert(Constants.Text.pvp_warn_robber)
                local ok = Api.Task.Wait(eid)
                if not ok then
                    return
                end
            end
            local giveupok = Api.Task.Wait(Api.Quest.Task.GiveUp(v))
            if not giveupok then
                return
            end
        end
    end
    Api.QuickTransport(mapid)
    local sec_id = Api.Task.DelaySec(10)
    local map_eid = Api.Listen.ActorLeaveMap()
    Api.Task.WaitAny(sec_id, map_eid)

    if mapid == Api.GetMapTemplateID() then
        Api.Task.Wait(Api.UI.Task.CarriageBackHud())
    end
end