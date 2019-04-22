function main(ele)
    assert(ele)
    assert(arg.PlayerUUID)
    local errorReason = Api.CheckRequire(ele.require)
    if not string.IsNullOrEmpty(errorReason) then
        local ClientApi = Api.GetClientApi(arg)
        ClientApi.ShowMessage(errorReason)
        return false
    end
    local ZoneApi = Api.GetZoneApi(Api.GetZoneUUID())
    local x, y = ZoneApi.GetPlayerPosition(arg.PlayerUUID)
    local drop = {Show = ele.show == 1, X = x, Y = y, MonsterType = ele.monster_type}
    Api.CommonReward(ele.reward, drop, arg.Key)
end
