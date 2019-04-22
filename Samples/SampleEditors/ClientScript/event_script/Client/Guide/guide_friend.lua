function main()
    --加好友引导
              Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    local step = Api.Guide.GetGuideStep("guidefriend")
    if step then
        return
    end

    Api.Task.WaitActorReady()
    local SocialFriend = Api.UI.FindByTag('SocialFriend')
    if SocialFriend == nil then
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_jiaohu = Api.UI.FindChild(ui, 'btn_jiaohu')
        Api.UI.Listen.Visible(btn_jiaohu)
        --print('ui',ui,tbt_zidong)
        local id = Api.Guide.Listen.Touch(btn_jiaohu, { text = Constants.GuideText.OpenJiaohuMain, y = -10, right = true, force = true })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('SocialFriend')
        SocialFriend = Api.UI.FindByTag('SocialFriend')
    end

    Api.UI.Listen.MenuExit(
            'SocialFriend',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    local tbt_recommend = Api.UI.FindChild(SocialFriend, 'tbt_recommend')

    local id = Api.Guide.Listen.Touch(tbt_recommend, { text = Constants.GuideText.RecommendClick, y = -10, left = true, force = true })

    Api.Task.Wait(id)
	Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_friend.assetbundles',false)
    local sp_friendlist3 = Api.UI.FindChild(SocialFriend, 'sp_friendlist3')

    if Api.UI.IsActiveInHierarchy(sp_friendlist3) then
        local cell = Api.UI.GetScrollListCell(sp_friendlist3, 1)
        if cell then
            local bt_apply = Api.UI.FindChild(cell, 'bt_apply', true)
            local id = Api.Guide.Listen.Touch(bt_apply, { text = Constants.GuideText.ApplyClick, y = -10, right = true, force = true })
            Api.Task.Wait(id)

        end
    end
    Api.Guide.SetGuideStep("guidefriend", "1")
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end