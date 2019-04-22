function main()
    -- 修行
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady()
    local PracticeMain = Api.UI.FindByTag('PracticeMain')
    if PracticeMain == nil then
        local eid = Api.UI.Listen.OpenFunMenu()
        Api.Task.Wait(eid)
        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.OpenPracticeMain)
        local ui = Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        local btn_practice = Api.UI.FindChild(ui, 'btn_practice', true)
        Api.UI.Listen.Visible(btn_practice)
        local id = Api.Guide.Listen.Touch(btn_practice, { text = Constants.GuideText.OpenPracticeMain, y = -10, right = true, force = true, reverse = 1 })
        Api.Task.Wait(id)

        Api.Guide.Listen.FindUIByTag('PracticeMain')
        PracticeMain = Api.UI.FindByTag('PracticeMain')
    end

    if PracticeMain == nil then
        return false
    end

    -- Api.UI.Listen.MenuExit(
            -- 'PracticeMain',
            -- function()
                -- Api.Task.StopEvent(ID, true, 'MenuExit')
            -- end)
    local btn_active = Api.UI.FindChild(PracticeMain, 'btn_active')
    --Api.UI.Listen.IsActiveInHierarchy(btn_active)
    if Api.UI.IsActiveInHierarchy(btn_active) then
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_practicemain.assetbundles', false)
        local bid = Api.Guide.Listen.Touch(btn_active, { text = Constants.GuideText.UnderstandPractice, y = 0, right = true, force = true })
        Api.Task.Wait(bid)
        --点击确认
        Api.Guide.Listen.FindUIByTag('PracticeReward')
        local AdvancedTips = Api.UI.FindByTag('PracticeReward')
        if AdvancedTips ~= nil then
            local bt_wear = Api.UI.FindChild(AdvancedTips, 'bt_wear')
            if Api.UI.IsActiveInHierarchy(bt_wear) then
                local id = Api.Guide.Listen.Touch(bt_wear, { text = Constants.GuideText.GetWardrobeClick, y = 0, right = true, force = true })
                Api.Task.Wait(id)
            end

        end
    else
        return true
    end

    --点击关闭
    local btn_close = Api.UI.FindChild(PracticeMain, 'btn_close')
    --print("btn_close",btn_close)
    if Api.UI.IsActiveInHierarchy(btn_close) then
        local bid = Api.Guide.Listen.Touch(btn_close, { y = 0, left = true, force = true })
        Api.Task.Wait(bid)
    end
   -- 点击衣柜
    -- local WardrobeMain = Api.UI.FindByTag('WardrobeMain')
    -- if WardrobeMain == nil then
        -- local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        -- local btn_wardrobe = Api.UI.FindChild(ui, 'btn_wardrobe', true)
        -- local id = Api.Guide.Listen.Touch(btn_wardrobe, { text = Constants.GuideText.OpenWardrobeMain, y = -10, right = true, force = true })
        -- Api.Task.Wait(id)
        -- Api.Guide.Listen.FindUIByTag('WardrobeMain')
        -- WardrobeMain = Api.UI.FindByTag('WardrobeMain')
    -- end
    -- if WardrobeMain == nil then
        -- return false
    -- end
    -- local step = 1
    -- :: restart ::
    -- Api.Task.Sleep(0.3)
    -- if step == 2 then
    --    发型按钮
        -- local tbt_an2 = Api.UI.FindChild(WardrobeMain, 'tbt_an2', true)
        -- local id = Api.Guide.Listen.Touch(tbt_an2, { text = Constants.GuideText.SelectWardrobe, y = 0, right = true, force = true })
        -- Api.Task.Wait(id)
    -- end
    -- local sp_itemlist = Api.UI.FindChild(WardrobeMain, 'sp_itemlist')
    -- local cell = Api.UI.GetScrollListCell(sp_itemlist, 2)
    -- local itemnode = Api.UI.FindChild(cell, function(child)
        -- local name = Api.UI.GetNodeName(child)
        -- if string.find(name, ".png") then
            -- return true
        -- else
            -- return false
        -- end

    -- end)
    -- if itemnode == nil then
        -- return false
    -- end
    -- local id = Api.Guide.Listen.Touch(itemnode, { text = Constants.GuideText.SelectWardrobe, y = 0, right = true, force = true })
    -- Api.Task.Wait(id)

 --   todo 获取制定id的特效
    -- local btn_unlock = Api.UI.FindChild(WardrobeMain, 'btn_unlock')
    -- if Api.UI.IsActiveInHierarchy(btn_unlock) then
        -- id = Api.Guide.Listen.Touch(btn_unlock, { text = Constants.GuideText.ActiveWardrobeClick, y = -10, right = true, force = true })
        -- Api.Task.Wait(id)
    -- end
    -- local btn_yongjiu = Api.UI.FindChild(WardrobeMain, 'btn_yongjiu', true)
    -- if Api.UI.IsActiveInHierarchy(btn_yongjiu) then
        -- id = Api.Guide.Listen.Touch(btn_yongjiu, { y = -10, right = true, force = true })
        -- Api.Task.Wait(id)
    -- end
    -- if step < 2 then
        -- step = step + 1
        -- goto restart
    -- end


end

function clean()
    -- body
        Api.UnsubscribeGlobalBack('event.'..ID)
end