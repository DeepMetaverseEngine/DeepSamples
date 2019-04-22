function main()
    --仙灵岛引导
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
		local step = Api.Guide.GetGuideStep("guide_xianlingdao")
		if step then
			return true
		end
		Api.Guide.SetGuideStep("guide_xianlingdao","1")
    Api.Task.WaitActorReady() -- 等待玩家登入
    local ActivityMain = Api.UI.FindByTag('ActivityMain')
    if ActivityMain == nil then
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
    local sp_list = nil--Api.UI.FindChild(ActivityMain, 'sp_list')
    Api.Guide.Listen.FindUIChild(ActivityMain,'sp_list',
    function(node)
        sp_list = node
    end)
    --Api.Task.Sleep(0.3)
    local cell = nil
    Api.Guide.Listen.FindUIChild(sp_list,
    function(child)
        return Api.UI.GetUserData(child) == 'island'
    end,
    function(node)
        cell = node
    end)
    -- local cell = Api.UI.FindChild(sp_list, function(child)
    --     return Api.UI.GetUserData(child) == 'island'
    -- end)
    if cell then
        local tbt_select = Api.UI.FindChild(cell, 'tbt_select', true)
        if tbt_select and Api.UI.IsActiveInHierarchy(tbt_select) and not Api.UI.IsChecked(tbt_select) then
            local id = Api.Guide.Listen.Touch(tbt_select, { text = Constants.GuideText.IslandSelect, y = 10, right = true, force = true })
            Api.Task.Wait(id)
        end
    end

    local btn_go = Api.UI.FindChild(ActivityMain, 'btn_go', true)
    if Api.UI.IsActiveInHierarchy(btn_go) then
        local id = Api.Guide.Listen.Touch(btn_go, { text = Constants.GuideText.IslandClick, y = 10, right = true, force = true })
        Api.Task.Wait(id)
    end
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end