function main()
    -- 镶嵌宝石
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady()
    local SmithyMainUI = Api.UI.FindByTag('SmithyFrame')
    if SmithyMainUI == nil then
        local eid = Api.UI.Listen.OpenFunMenu()
        Api.Task.Wait(eid)
        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.OpenSmithyMain)
        local ui = Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        local btn_forge = Api.UI.FindChild(ui, 'btn_forge', true)
        Api.UI.Listen.Visible(btn_forge)
        local id = Api.Guide.Listen.Touch(btn_forge, { text = Constants.GuideText.OpenSmithyMain, y = -10, right = true, force = true, reverse = 1 })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('SmithyFrame')
        SmithyMainUI = Api.UI.FindByTag('SmithyFrame')
    end

    if SmithyMainUI == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'SmithyFrame',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    local cvs_label = Api.UI.FindChild(SmithyMainUI, 'cvs_label')
    local tbt_an2 = Api.UI.FindChild(cvs_label, 'tbt_an2')
    if Api.UI.IsActiveInHierarchy(tbt_an2) then

        local id = Api.Guide.Listen.Touch(tbt_an2, { text = Constants.GuideText.StoneInsetClick, y = 0, right = true, force = true })
        Api.Task.Wait(id)
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_gemstone.assetbundles', false)
    end
    Api.Guide.Listen.FindUIByTag('SmithyGemstone')
    local SmithyGemstone = Api.UI.FindByTag('SmithyGemstone')
    local cvs_itemlist = Api.UI.FindChild(SmithyGemstone, 'cvs_itemlist')
    local sp_oar = Api.UI.FindChild(cvs_itemlist, 'sp_oar')

    local cell = Api.UI.GetScrollListCell(sp_oar, 1)
    Api.Guide.Listen.GetScrollListCell(sp_oar,1,function(node)
        cell = node
    end)
    if Api.UI.IsActiveInHierarchy(cell) then
        local btn_up = Api.UI.FindChild(cell, 'btn_up')
        if Api.UI.IsActiveInHierarchy(btn_up) then
            local id = Api.Guide.Listen.Touch(btn_up, { text = Constants.GuideText.SelectStoneCompose, y = 0, right = true, force = true })
            Api.Task.Wait(id)

            Api.Task.Sleep(0.1)
            --Api.EnterBlockTouch()
            Api.Guide.Listen.GetScrollListCell(sp_oar,1,function(node)
                if cell ~= node then
                    cell = node
                end
            end)
            --Api.ExitBlockTouch()

        end
        local btn_use = Api.UI.FindChild(cell, 'btn_use')
        id = Api.Guide.Listen.Touch(btn_use, { text = Constants.GuideText.SelectStoneInset, y = 0, right = true, force = false })
        Api.Task.Wait(id)
    end
end

function clean()
    Api.ExitBlockTouch()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end