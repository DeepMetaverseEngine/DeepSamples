function main()
    --强化引导
    --print("guidesmithymain")
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady() -- 等待玩家登入
    local SmithyMainUI = Api.UI.FindByTag('SmithyFrame')
    if SmithyMainUI == nil then
        local eid = Api.UI.Listen.OpenFunMenu()
        Api.Task.Wait(eid)
        --print("guidesmithymain1")

        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.OpenSmithyMain)
        --print("guidesmithymain2")
        local ui = Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        local btn_forge = Api.UI.FindChild(ui, 'btn_forge', true)
        Api.UI.Listen.Visible(btn_forge)
        local id = Api.Guide.Listen.Touch(btn_forge, { text = Constants.GuideText.OpenSmithyMain, y = -10, right = true, force = true, reverse = 1 })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('SmithyFrame')
        SmithyMainUI = Api.UI.FindByTag('SmithyFrame')
    end
    --Api.Task.Sleep(0.3)
    if SmithyMainUI == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'SmithyFrame',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)

    Api.Guide.Listen.FindUIByTag('SmithyStrengthen')
    local SmithyStrengthenUI = Api.UI.FindByTag('SmithyStrengthen')
    Api.UI.Listen.MenuExit(
            'SmithyStrengthen',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    local btn_use = Api.UI.FindChild(SmithyStrengthenUI, 'btn_use')
    if Api.UI.IsActiveInHierarchy(btn_use) then
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_smithymain.assetbundles', false)
        local bid = Api.Guide.Listen.Touch(btn_use, { text = Constants.GuideText.SmithyClick, y = 0, right = true, force = true })
        Api.Task.Wait(bid)

        bid = Api.Guide.Listen.Touch(btn_use, { text = Constants.GuideText.SmithyClick, y = -10, right = true, force = true })
        Api.Task.Wait(bid)

    end

    local cvs_clothes = Api.UI.FindChild(SmithyStrengthenUI, 'cvs_clothes', true)
    if Api.UI.IsActiveInHierarchy(cvs_clothes) and Api.UI.IsEnable(cvs_clothes) then
        bid = Api.Guide.Listen.Touch(cvs_clothes, { y = -10, left = true, force = true })
        Api.Task.Wait(bid)
    end

    if Api.UI.IsActiveInHierarchy(btn_use) then
	--	Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_smithymain1.assetbundles', false)
        bid = Api.Guide.Listen.Touch(btn_use, { text = Constants.GuideText.SmithyClick, y = -10, right = true, force = true })
        Api.Task.Wait(bid)
    end
	
    local btn_effect = Api.UI.FindChild(SmithyStrengthenUI, 'btn_effect')	

	  if Api.UI.IsActiveInHierarchy(btn_effect) then

        bid = Api.Guide.Listen.Touch(btn_effect, { text = Constants.GuideText.btn_effectClick, x = 50,y = -10, right = true, force = true })
        Api.Task.Wait(bid)
        Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_smithymain1.assetbundles', false)
    end

end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end