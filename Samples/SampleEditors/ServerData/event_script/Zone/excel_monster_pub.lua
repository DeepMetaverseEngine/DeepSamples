function main(ele)
    if ele.lefttime and ele.lefttime > 0 then
        Api.Task.AddEvent(
            function()
                Api.Task.Sleep(ele.lefttime / 1000)
                Api.Task.StopEvent(ID, false, 'timeout')
            end
        )
    end
    local id = Api.Task.AddEvent('Zone/point_pawn_unit', ele)
    return Api.Task.Wait(id)
end
