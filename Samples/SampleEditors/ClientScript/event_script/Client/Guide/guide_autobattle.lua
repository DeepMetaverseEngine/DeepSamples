function main()
	Api.SubscribeGlobalBack('event.'..ID, function() return true end)
	-- 自动战斗
		Api.Task.WaitActorReady()
        Api.Guide.RemoveRepeat(ScriptDesc,ID)
	 	local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local tbt_zidong = Api.UI.FindChild(ui, 'tbt_zidong')
		
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_autobattle.assetbundles',false)
        --print('ui',ui,tbt_zidong)
        local id = Api.Guide.Listen.Touch(tbt_zidong,{text = Constants.GuideText.AutoBattle,x = 0,y = -30, left = true, force = false})
        Api.Task.WaitAlways(id)
end

function clean()
	Api.ResumeBGMVol()
	Api.UnsubscribeGlobalBack('event.'..ID)
end