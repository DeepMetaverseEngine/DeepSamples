function main()
    -- 技能界面引导
    --Api.Task.Sleep(3)
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady() -- 等待玩家登入
    local skillui = Api.UI.FindByTag('SkillMain')
    if skillui == nil then
        local eid = Api.UI.Listen.OpenFunMenu()
        Api.Task.Wait(eid)
        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.OpenSkillMain)
        local ui = Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        local btn_skill = Api.UI.FindChild(ui, 'btn_skill', true)
        Api.UI.Listen.Visible(btn_skill)
        local id = Api.Guide.Listen.Touch(btn_skill, { text = Constants.GuideText.OpenSkillMain, y = -10, right = true, force = true, reverse = 1 })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('SkillMain')
        skillui = Api.UI.FindByTag('SkillMain')
    end

    if skillui == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'SkillMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    local btn_allup = Api.UI.FindChild(skillui, 'btn_up')
    if Api.UI.IsActiveInHierarchy(btn_allup) then
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_skillframe.assetbundles', false)
        local id = Api.Guide.Listen.Touch(btn_allup, { text = Constants.GuideText.UseOneBtn, y = 0, right = true, force = true })
        Api.Task.Wait(id)
    end


end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end