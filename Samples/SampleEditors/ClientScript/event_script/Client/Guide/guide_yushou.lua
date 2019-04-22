function main()
    --御兽之所
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
		local step = Api.Guide.GetGuideStep("guide_yushou")
		if step then
			return true
		end
	Api.Guide.SetGuideStep("guide_yushou","1")
    Api.Task.WaitActorReady() -- 等待玩家登入
    local ActivityMain = Api.UI.FindByTag('DailyDungeonMain')
    if ActivityMain == nil then
         Api.Guide.WaitTopMenuIsOpenAndGuide() 
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_huodong = Api.UI.FindChild(ui,'btn_hexin', true)
        --print('ui',ui,tbt_zidong)
        local id = Api.Guide.Listen.Touch(btn_huodong, { text = Constants.GuideText.OpenActivityMain, y = -10, right = true, force = true })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('DailyDungeonMain')
        ActivityMain = Api.UI.FindByTag('DailyDungeonMain')
    end
    if ActivityMain == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'DailyDungeonMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    
    local sp_list = Api.UI.FindChild(ActivityMain, 'sp_fblist')
    --Api.UI.Listen.Visible(sp_list)
    local cell = Api.UI.GetScrollListCell(sp_list, 1)
    -- local cell = Api.UI.FindChild(sp_list, function(child)
    --     return Api.UI.GetUserData(child) == 'dungeondaily_mount'
    -- end)
    if cell then

        local id = Api.Guide.Listen.Touch(cell, { text = Constants.GuideText.YushouSelect, y = 10, right = true, force = true })
        Api.Task.Wait(id)
        -- local tbt_select = Api.UI.FindChild(cell, 'tbt_select', true)
        -- if tbt_select and Api.UI.IsActiveInHierarchy(tbt_select) and not Api.UI.IsChecked(tbt_select) then
        --     local id = Api.Guide.Listen.Touch(tbt_select, { text = Constants.GuideText.YushouSelect, y = 10, right = true, force = true })
        --     Api.Task.Wait(id)
        -- end
        -- local btn_go = Api.UI.FindChild(ActivityMain, 'btn_go', true)
        -- if Api.UI.IsActiveInHierarchy(btn_go) then
        --     local id = Api.Guide.Listen.Touch(btn_go, { text = Constants.GuideText.YushouClick, y = 10, right = true, force = true })
        --     Api.Task.Wait(id)
        -- end
    end


end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end