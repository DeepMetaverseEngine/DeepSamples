function main()
    --镇妖塔
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    local step = Api.Guide.GetGuideStep("guide_pagoda")
    if step then
        return true
    end
    Api.Guide.SetGuideStep("guide_pagoda", "1")
    Api.Task.WaitActorReady() -- 等待玩家登入
    local id
    local ActivityMain = Api.UI.FindByTag('ActivityMain')
    if ActivityMain == nil then
        Api.Guide.WaitTopMenuIsOpenAndGuide()
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_huodong = Api.UI.FindChild(ui, 'btn_huodong', true)
        --print('ui',ui,tbt_zidong)
        id = Api.Guide.Listen.Touch(btn_huodong, { text = Constants.GuideText.OpenActivityMain, y = -10, right = true, force = true })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('ActivityMain')
        ActivityMain = Api.UI.FindByTag('ActivityMain')

    end

    local tbt_subtype5 = Api.UI.FindChild(ActivityMain, 'tbt_subtype5', true)
    if not Api.UI.IsChecked(tbt_subtype5) then
        id = Api.Guide.Listen.Touch(tbt_subtype5, {  y = -10, left = true, force = true })
        Api.Task.Wait(id)
    end

    local sp_list = nil--Api.UI.FindChild(ActivityMain, 'sp_list')
    Api.Guide.Listen.FindUIChild(ActivityMain,'sp_list',
    function(node)
        sp_list = node
    end)
    local cell = nil
    -- Api.UI.FindChild(sp_list, function(child)
    --      return Api.UI.GetUserData(child) == 'pagoda'
    -- end)

    Api.Guide.Listen.FindUIChild(sp_list,
    function(child)
        return Api.UI.GetUserData(child) == 'pagoda'
    end,
    function(node)
        cell = node
    end)

    if cell then
        local tbt_select = Api.UI.FindChild(cell, 'tbt_select')
        id = Api.Guide.Listen.Touch(tbt_select, {  y = -10, left = true, force = true })
        Api.Task.Wait(id)
    end

    local btn_go = Api.UI.FindChild(ActivityMain, 'btn_go')
    if Api.UI.IsActiveInHierarchy(btn_go) then
        id = Api.Guide.Listen.Touch(btn_go, {text = Constants.GuideText.PagodaClick, y = -10, right = true, force = true })
        Api.Task.Wait(id)
    end

    Api.Guide.Listen.FindUIByTag('PagodaMain')
    ActivityMain = Api.UI.FindByTag('PagodaMain')

    if ActivityMain == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'PagodaMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    sp_list = Api.UI.FindChild(ActivityMain, 'sp_diflist')

    local cell = nil --= Api.UI.GetScrollListCell(sp_list, 1)
    Api.Guide.Listen.GetScrollListCell(sp_list,1,function(node)
        cell = node
    end)
    if cell then
        --local tbt_select = Api.UI.FindChild(cell,'cvs_diflevel',true)

        local id = Api.Guide.Listen.Touch(cell, { text = Constants.GuideText.PagodaSelect, y = 10, right = true, time = 4, OnlyText = true })
        Api.Task.Wait(id)
        -- local btn_go = Api.UI.FindChild(ActivityMain,'btn_go',true)
        -- if Api.UI.IsActiveInHierarchy(btn_go) then
        --   local id = Api.Guide.Listen.Touch(btn_go,{text = Constants.GuideText.PagodaClick,y = 10, right = true,  force = true})
        --   Api.Task.Wait(id)
        -- end
    end


end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end