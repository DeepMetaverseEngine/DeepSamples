function main()
    -- 仙侣
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
	   Api.Task.WaitActorReady()
        local PartnerMain =  Api.UI.FindByTag('PartnerMain')
        if PartnerMain == nil then
            local eid = Api.UI.Listen.OpenFunMenu()
            Api.Task.Wait(eid)
            Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.OpenPartnerMain)
            local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
            local btn_partner = Api.UI.FindChild(ui, 'btn_partner',true)
            Api.UI.Listen.Visible(btn_partner)
            local id = Api.Guide.Listen.Touch(btn_partner,{text = Constants.GuideText.OpenPartnerMain,y = -10, right = true,  force = true,reverse = 1})
            Api.Task.Wait(id)
            Api.Guide.Listen.FindUIByTag('PartnerMain')
            PartnerMain =  Api.UI.FindByTag('PartnerMain')
        end
       
       if PartnerMain == nil then
            return false
        end
    Api.UI.Listen.MenuExit(
            'PartnerMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
        -- local btn_jihuo = Api.UI.FindChild(PartnerMain, 'btn_jihuo')
        -- if Api.UI.IsActiveInHierarchy(btn_jihuo) then
        --     local id = Api.Guide.Listen.Touch(btn_jihuo,{text = Constants.GuideText.ActiveClick,y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        -- end
        -- Api.Guide.Listen.FindUIByTag('AdvancedTips')
        -- local AdvancedTips =  Api.UI.FindByTag('AdvancedTips')
        -- if AdvancedTips ~= nil then
        --     local btn_ok = Api.UI.FindChild(AdvancedTips, 'btn_ok')
        --     if Api.UI.IsActiveInHierarchy(btn_ok) then
        --       id = Api.Guide.Listen.Touch(btn_ok,{text = Constants.GuideText.LvUpClick,y = -10, right = true,  force = true})
        --       Api.Task.Wait(id)
        --     end
        -- end
        Api.Task.Sleep(0.3)
        local btn_use = Api.UI.FindChild(PartnerMain, 'btn_use',true)
        if btn_use ~= nil and Api.UI.IsActiveInHierarchy(btn_use) then
			Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_partner.assetbundles', false)
            local id = Api.Guide.Listen.Touch(btn_use,{text = Constants.GuideText.TrainClick,y = -40, right = true,  force = true})
            Api.Task.Wait(id)
        end
      
        local tbt_use = Api.UI.FindChild(PartnerMain, 'tbt_use',true)
        if tbt_use and Api.UI.IsActiveInHierarchy(tbt_use) and not Api.UI.IsChecked(tbt_use) then
            local id = Api.Guide.Listen.Touch(tbt_use,{text = Constants.GuideText.BattleClick,y = -40, right = true,  force = true})
            Api.Task.Wait(id)
        end
    
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
	-- body
end