function main(ele)
    Api.Listen.PlayerLeaveZone(
        arg.PlayerUUID,
        function()
            Api.Task.StopEvent(ID, false, 'LeaveZone')
        end
    )
    Api.Task.WaitPlayerReady(arg.PlayerUUID)
    local playforce = Api.GetPlayerForce(arg.PlayerUUID)
    local success, winForce, message = Api.Task.Wait(Api.Listen.GameOver())
    print('winforce message', success, winForce, message)
    if not success then
        message = winForce
        return false, message
    else
        return playforce == winForce, message
    end
end
