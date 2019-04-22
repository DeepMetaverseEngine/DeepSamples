function main(ele)
    -- print('arg.launcherID',arg.PlayerUUID, arg.LauncherID)
    if not arg.LauncherID then
        arg.LauncherID = Api.GetPlayerObjectID(arg.PlayerUUID)
    end
    local launch_force = Api.GetUnitForce(arg.LauncherID)
    if ele.force_type == 0 then
        assert(arg.LauncherID)
        Api.UnitAddBuff(arg.LauncherID, ele.buffid)
    elseif ele.force_type > 0 then
        local f = launch_force
        local reverse_force = false
        if ele.force_type == 2 then
            reverse_force = true
        elseif ele.force_type == 3 then
            f = 0
        end
        -- pprint('ele.targetid',ele.target_id)
        local hasTarget 
        for _,v in ipairs(ele.target_id) do
            if v ~= 0 then
                hasTarget = true
                local units = Api.FindUnits(f, v, reverse_force)
                for _,v in ipairs(units) do
                    Api.UnitAddBuff(v, ele.buffid)
                end
            end
        end
        if not hasTarget then
            local units = Api.FindUnits(f, 0, reverse_force)
            for _,v in ipairs(units) do
                Api.UnitAddBuff(v, ele.buffid)
            end
        end
    end
end
