function main()
     -- 坐骑
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
        Api.Task.WaitActorReady()
        local MountMain =  Api.UI.FindByTag('MountMain')
        if MountMain == nil then
            local eid = Api.UI.Listen.OpenFunMenu()
            Api.Task.Wait(eid)
            Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.OpenMountMain)
            local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
            local btn_mount = Api.UI.FindChild(ui, 'btn_mount',true)
            Api.UI.Listen.Visible(btn_mount)
            local id = Api.Guide.Listen.Touch(btn_mount,{text = Constants.GuideText.OpenMountMain,y = -10, right = true,  force = true,reverse = 1})
            Api.Task.Wait(id)
            Api.Guide.Listen.FindUIByTag('MountMain')
            MountMain =  Api.UI.FindByTag('MountMain')
        end
       
        if MountMain == nil then
            return false
        end
        Api.UI.Listen.MenuExit(
            'MountMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
        local tbt_an2 = Api.UI.FindChild(MountMain, 'tbt_an2')
        local id = Api.Guide.Listen.Touch(tbt_an2,{text = Constants.GuideText.SelectLingMai,y = 0, right = true,  force = true})
        Api.Task.Wait(id)
      
        Api.Guide.Listen.FindUIByTag('MountVeins')
        local MountVeins = Api.UI.FindByTag('MountVeins')
        local btn_use = Api.UI.FindChild(MountVeins,'btn_use')
        Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_mount.assetbundles', false)
        id = Api.Guide.Listen.Touch(btn_use,{text = Constants.GuideText.ActiveClick,y = 0, right = true,  force = true})
        Api.Task.Wait(id)
       
    
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
	-- body
end