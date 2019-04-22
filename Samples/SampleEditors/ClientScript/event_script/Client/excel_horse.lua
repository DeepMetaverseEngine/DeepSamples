function main(id)
    local excel_data = Api.GetExcelByEventKey('horse.' .. id)
    assert(excel_data)
    Api.PlayRippleEffect()
    Api.SetMainActorVisible(false)

    local avatarMap = Api.GetActorAvartarMap()
    local x, y = Api.GetActorPostion()
    local pos = Api.GetUnityPosistion(x, y)
    avatarMap[Constants.AvatarPart.Ride_Avatar01] = excel_data.model
    local id = Api.Scene.Task.LoadAvatarGameObject(avatarMap, {Pos = pos,Deg = {y=118}, AnimatorState = 'm_tame01'})

    local ok, resid = Api.Task.Wait(id)
    ActorID = resid
    local id = Api.Task.AddEvent('Client/ui_qte', excel_data, false, true)
    local ok1, ret = Api.Task.Wait(id)

    Api.PlayRippleEffect()
    return ok1, ret
end

function clean()
    if ActorID then
        Api.Scene.Destroy(ActorID)
    end
    Api.SetMainActorVisible(true)
end
