function main()
    --师门引导
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
		local step = Api.Guide.GetGuideStep("guide_shimen")
		if step then
			return true
		end
		Api.Guide.SetGuideStep("guide_shimen","1")
    Api.Task.WaitActorReady() -- 等待玩家登入
    local ActivityMain = Api.UI.FindByTag('ActivityMain')
    if ActivityMain == nil then
        local eid = Api.UI.Listen.OpenFunMenu()
        Api.Task.Wait(eid)
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_huodong = Api.UI.FindChild(ui, 'btn_huodong', true)
        --print('ui',ui,tbt_zidong)
        local id = Api.Guide.Listen.Touch(btn_huodong, { text = Constants.GuideText.OpenActivityMain, y = -10, right = true, force = true })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('ActivityMain')
        ActivityMain = Api.UI.FindByTag('ActivityMain')
    end
    if ActivityMain == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'ActivityMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    --local sp_list = Api.UI.FindChild(ActivityMain, 'sp_list')
    --Api.UI.Listen.Visible(sp_list)
    --Api.Task.Sleep(0.1)
    --local cell = Api.UI.FindChild(sp_list, function(child)
    --    return Api.UI.GetUserData(child) == 'shimen'
    --end)
    --if cell then
    --    local tbt_select = Api.UI.FindChild(cell, 'tbt_select', true)
    --    if tbt_select and Api.UI.IsActiveInHierarchy(tbt_select) and not Api.UI.IsChecked(tbt_select) then
    --        local id = Api.Guide.Listen.Touch(tbt_select, { text = Constants.GuideText.ShiMenSelect, y = 10, right = true, force = true })
    --        Api.Task.Wait(id)
    --    end
    --end

    local btn_go = Api.UI.FindChild(ActivityMain, 'btn_go', true)
    if btn_go and Api.UI.IsActiveInHierarchy(btn_go) then
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_shimen.assetbundles', false)
        local id = Api.Guide.Listen.Touch(btn_go, { text = Constants.GuideText.ShiMenClick, y = 10, right = true, force = true })
        Api.Task.Wait(id)
    end
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end