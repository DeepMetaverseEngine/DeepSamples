function main(ele, params)
    params = params or {}
    local function PlayerEntered()
        local birth_x, birth_y = Api.GetFlagPosition(ele.appear_point)
        assert(birth_x and birth_y)
        Api.SetPlayerForce(arg.PlayerUUID, 3)
        if not params.IsLineStop then
            Api.SetPlayerPosition(arg.PlayerUUID, birth_x, birth_y)
        end
    end
    Api.Listen.Message(
        'carriage_line.' .. arg.PlayerUUID,
        function()
            local ret = {
                IsLineStop = true
            }
            Api.SetEventOutput(ID, ret)
            Api.Task.StopEvent(ID, true)
        end
    )
    PlayerEntered()
    Api.Task.Wait(Api.Listen.PlayerLeaveZone(arg.PlayerUUID))
    Api.Task.Sleep(5)
end
