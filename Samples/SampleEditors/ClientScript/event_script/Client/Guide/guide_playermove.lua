function main()
        --移动
          --Api.SubscribeGlobalBack('event.'..ID, function() return true end)
        Api.Task.StartDramaScript("quest/dungen100000_6")  
        Api.Task.WaitActorReady()

        local eid 
	      local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/RockerFrame/RockerBG')
        --print('obj ',obj)
        local id = Api.Guide.Listen.Touch(obj,{text = Constants.GuideText.MoveRock,y = -10, left = true,  force = false,type = 1})
		    Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_guide_playermove.assetbundles', false)
 
        -- eid = Api.Listen.AddPeriodicSec(
        --     0.03,
        --     function()
        --       if Api.InRockMove() then
        --          Api.Task.StopEvent(eid)
        --          Api.Task.StopEvent(id)
        --       end
        --     end
        -- )
        -- Api.Task.Wait()
        Api.Listen.IsEnterRegion('trigger_plot')
end

function clean()
        --Api.UnsubscribeGlobalBack('event.'..ID)
	-- body
end