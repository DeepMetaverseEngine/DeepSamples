function main(objid)
    -- local x,y = Api.GetActorPostion()
    Api.Task.Wait(Api.Listen.UnitInView(objid))
    local x,y = Api.GetObjectPosition(objid)
    local pos = Api.GetUnityPosistion(x, y)
    Api.Camera.StopFollowActor()

  --  Api.UI.SetHudVisible(false)

    Api.Camera.Task.MoveTo(pos, 0.5)

    Api.Task.Wait()

    Api.SetTimeScale(0.5)

    Api.Camera.Task.MoveToAndSet({x=0,y=8,z=-8},2)

    Api.Task.Sleep(2)
    Api.SetTimeScale(1)
    Api.Task.Sleep(1.5)
end

function clean()
    Api.SetTimeScale(1)
    Api.UI.SetHudVisible(true)
    Api.Camera.ResetLocation()
end
