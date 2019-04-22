function main()
		-- local step = Api.Guide.GetGuideStep("guide1046")
		-- if step then
		-- 	return true
		-- end
		-- Api.Guide.SetGuideStep("guide1046","1")
	    --Api.SubscribeGlobalBack('event.'..ID, function() return true end)
      	Api.Task.WaitActorReady()
		--Api.Task.Sleep(1.5)
	    local ui =  Api.UI.FindHud('xml/hud/ui_hud_team_quest.gui.xml')
        local sp_oar = Api.UI.FindChild(ui,'sp_oar')
        Api.UI.Listen.Visible(sp_oar)
        local cvs_kuang = Api.UI.FindChild(sp_oar,function(child)
              return Api.UI.GetUserTag(child) == 1046 
        end)
       	if cvs_kuang ~= nil and Api.UI.IsActiveInHierarchy(cvs_kuang) then
			Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_quest1046.assetbundles', false)
	        local id = Api.Guide.Listen.Touch(cvs_kuang,{text = Constants.GuideText.QuestClick, left = true,  force = true})
	        Api.Task.Wait(id)
    	end
end

function clean()
       -- Api.UnsubscribeGlobalBack('event.'..ID)
	-- body
end