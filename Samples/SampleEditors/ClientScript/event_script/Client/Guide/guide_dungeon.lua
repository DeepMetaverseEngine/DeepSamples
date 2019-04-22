function main()
  --多人副本
          Api.SubscribeGlobalBack('event.'..ID, function() return true end)
		local step = Api.Guide.GetGuideStep("guidedungeon")
		if step then
			return true
		end
		Api.Guide.SetGuideStep("guidedungeon","1")
		Api.Task.WaitActorReady() -- 等待玩家登入

        local ActivityMain = Api.UI.FindByTag('ActivityMain')
        if ActivityMain == nil then
            Api.Guide.WaitTopMenuIsOpenAndGuide()
            local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
            local btn_huodong = Api.UI.FindChild(ui, 'btn_huodong', true)
            --print('ui',ui,tbt_zidong)
            local id = Api.Guide.Listen.Touch(btn_huodong, { text = Constants.GuideText.OpenActivityMain, y = -10, right = true, force = true })
            Api.Task.Wait(id)
            Api.Guide.Listen.FindUIByTag('ActivityMain')
            ActivityMain = Api.UI.FindByTag('ActivityMain')
    
        end
    local tbt_subtype4 = nil
    Api.Guide.Listen.FindUIChild(ActivityMain, 'tbt_subtype4',function(node)
            tbt_subtype4 = node
    end)
    if Api.UI.IsActiveInHierarchy(tbt_subtype4) and not Api.UI.IsChecked(tbt_subtype4) then
        id = Api.Guide.Listen.Touch(tbt_subtype4, {  y = -10, left = true, force = true })
        Api.Task.Wait(id)
    end
    local btn_go = Api.UI.FindChild(ActivityMain, 'btn_go')
    if Api.UI.IsActiveInHierarchy(btn_go) then
        id = Api.Guide.Listen.Touch(btn_go, { text = Constants.GuideText.DungeonSelect, y = -10, right = true, force = true })
        Api.Task.Wait(id)
    end
    Api.Guide.Listen.FindUIByTag('DungeonMain')
    ActivityMain = Api.UI.FindByTag('DungeonMain')

        if ActivityMain then
            --local sp_fblist  = Api.UI.FindChild(DungeonMain,'sp_fblist')
            --local cvs_fbdetails = Api.UI.FindChild(DungeonMain,'cvs_fbdetails')
            --if Api.UI.IsActiveInHierarchy(sp_fblist) then
            --    local cell = Api.UI.GetScrollListCell(sp_fblist, 1)
            --    if cell then
				--	Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_dungeon.assetbundles', false)
            --        local cvs_fb = Api.UI.FindChild(cell,'cvs_fb')
            --        local id = Api.Guide.Listen.Touch(cell,{text = Constants.GuideText.DungeonClick,y = -10, right = true, force = true})
            --        Api.Task.Wait(id)
            --    end
            --end
            local btn_start  = Api.UI.FindChild(ActivityMain,'btn_start')
            Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_dungeon.assetbundles', false)
            id = Api.Guide.Listen.Touch(btn_start, { text = Constants.GuideText.DungeonClick, y = -10, right = true, force = fasle,time = 5 })
            Api.Task.Wait(id)
        end
end

function clean()
	-- body
        Api.UnsubscribeGlobalBack('event.'..ID)
end