function main()
    --金轮引导
		-- local step = Api.Guide.GetGuideStep("guide_wing")
		-- if step then
		-- 	return true
		-- end
		-- Api.Guide.SetGuideStep("guide_wing","1")
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady() -- 等待玩家登入
    local WingFrame = Api.UI.FindByTag('Wing')
    if WingFrame == nil then
        local eid = Api.UI.Listen.OpenFunMenu()
        Api.Task.Wait(eid)
        Api.Guide.WaitMenuIsOpenAndGuide()
        local ui = Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        local btn_wings = Api.UI.FindChild(ui, 'btn_wings', true)
        Api.UI.Listen.Visible(btn_wings)
        local id = Api.Guide.Listen.Touch(btn_wings, { text = Constants.GuideText.OpenWingMain, y = -10, right = true, force = true, reverse = 1 })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('Wing')
        WingFrame = Api.UI.FindByTag('Wing')
    end

    if WingFrame == nil then
        return false
    end

    Api.UI.Listen.MenuExit(
            'Wing',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    local btn_use = Api.UI.FindChild(WingFrame, 'btn_alluse')
	Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_wing.assetbundles', false)

    local id = Api.Guide.Listen.Touch(btn_use, { text = Constants.GuideText.WingClick, y = -40, right = true, force = true })
    Api.Task.Wait(id)

    
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end