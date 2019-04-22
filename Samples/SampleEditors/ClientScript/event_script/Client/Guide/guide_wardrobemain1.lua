function main()
    --衣柜引导
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady() -- 等待玩家登入

   --点击修行之道衣服
    local WardrobeMain = Api.UI.FindByTag('WardrobeMain')
    if WardrobeMain == nil then
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_wardrobe = Api.UI.FindChild(ui, 'btn_wardrobe', true)
        local id = Api.Guide.Listen.Touch(btn_wardrobe, { text = Constants.GuideText.OpenWardrobeMain, y = -10, right = true, force = true })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('WardrobeMain')
        WardrobeMain = Api.UI.FindByTag('WardrobeMain')
    end
    if WardrobeMain == nil then
        return false
    end
    local step = 1
    :: restart ::
    Api.Task.Sleep(0.3)
    if step == 2 then
        --发型按钮
        local tbt_an2 = Api.UI.FindChild(WardrobeMain, 'tbt_an2', true)
        local id = Api.Guide.Listen.Touch(tbt_an2, { text = Constants.GuideText.SelectWardrobe, y = 0, right = true, force = true })
        Api.Task.Wait(id)
    end
    local sp_itemlist = Api.UI.FindChild(WardrobeMain, 'sp_itemlist')
    Api.Task.Sleep(0.3)
    local cell = Api.UI.GetScrollListCell(sp_itemlist, 2)
    local itemnode = Api.UI.FindChild(cell, function(child)
        local name = Api.UI.GetNodeName(child)
        if string.find(name, ".png") then
            return true
        else
            return false
        end

    end)
    if itemnode == nil then
        return false
    end
    local id = Api.Guide.Listen.Touch(itemnode, { text = Constants.GuideText.SelectWardrobe, y = 0, right = true, force = true })
    Api.Task.Wait(id)
    --id = Api.Guide.Listen.Touch(cell,{text = Constants.GuideText.SelectFootprint,y = -10, right = true,  force = true})
    --Api.Task.Wait(id)
    --todo 获取制定id的特效
    local btn_unlock = Api.UI.FindChild(WardrobeMain, 'btn_unlock')
    if Api.UI.IsActiveInHierarchy(btn_unlock) then
        id = Api.Guide.Listen.Touch(btn_unlock, { text = Constants.GuideText.ActiveWardrobeClick, y = -10, right = true, force = true })
        Api.Task.Wait(id)
    end
    local btn_yongjiu = Api.UI.FindChild(WardrobeMain, 'btn_yongjiu', true)
    if Api.UI.IsActiveInHierarchy(btn_yongjiu) then
        id = Api.Guide.Listen.Touch(btn_yongjiu, { y = -10, right = true, force = true })
        Api.Task.Wait(id)
    end
    if step < 2 then
        step = step + 1
        goto restart
    end

end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end