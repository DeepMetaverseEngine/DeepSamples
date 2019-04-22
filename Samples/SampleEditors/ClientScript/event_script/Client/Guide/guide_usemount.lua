function main()
	-- 自动战斗
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
		Api.Task.WaitActorReady()
	 	local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_zuoqi = Api.UI.FindChild(ui, 'btn_zuoqi')
        --print('ui',ui,btn_zuoqi)
        local id = Api.Guide.Listen.Touch(btn_zuoqi,{text = Constants.GuideText.AutoBattle,y = -30, left = true, force = false})
        Api.Task.Wait(id)
end

function clean()
	        Api.UnsubscribeGlobalBack('event.'..ID)
end