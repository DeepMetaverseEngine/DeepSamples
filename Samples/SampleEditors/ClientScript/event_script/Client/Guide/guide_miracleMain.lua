function main()
	     -- 神器
          Api.SubscribeGlobalBack('event.'..ID, function() return true end)
         Api.Task.WaitActorReady()
          local MiracleMain =  Api.UI.FindByTag('MiracleMain')
          if MiracleMain == nil then
              local eid = Api.UI.Listen.OpenFunMenu()
              Api.Task.Wait(eid)
              Api.Guide.WaitMenuIsOpenAndGuide()
              local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
              local btn_miracle = Api.UI.FindChild(ui, 'btn_miracle',true)
              Api.UI.Listen.Visible(btn_miracle)
              local id = Api.Guide.Listen.Touch(btn_miracle,{text = Constants.GuideText.OpenMiracleMain,y = -40, right = true,  force = true,reverse = 1})
              Api.Task.Wait(id)
              Api.Guide.Listen.FindUIByTag('MiracleMain')
              MiracleMain =  Api.UI.FindByTag('MiracleMain')
          end

          if MiracleMain == nil then
              return false
          end
           
            Api.UI.Listen.MenuExit(
            'MiracleMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)

         
          local sp_list = Api.UI.FindChild(MiracleMain, 'sp_list',true)
          Api.UI.Listen.Visible(sp_list)

          Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_miracleMain.assetbundles', false)
          local node = Api.UI.FindChild(sp_list, function (child)
                return Api.UI.GetUserTag(child) == 5 
              -- body
          end)

          if Api.UI.IsActiveInHierarchy(node) then
              local tbt_icon = Api.UI.FindChild(node,'tbt_icon')
              local id = Api.Guide.Listen.Touch(tbt_icon,{text = Constants.GuideText.SelectShenNong,y = 0, left = true,  force = true})
              Api.Task.Wait(id)
          end

          local btn_get = Api.UI.FindChild(MiracleMain, 'btn_get')

          local id = Api.Guide.Listen.Touch(btn_get,{text = Constants.GuideText.ActiveMiracleClick,y = -50, right = true,  force = true})
          Api.Task.Wait(id)
          

          Api.Guide.Listen.FindUIByTag('AdvancedTips')
          local AdvancedTips =  Api.UI.FindByTag('AdvancedTips')
          if AdvancedTips ~= nil then
			     Api.Task.Sleep(0.3)
            local btn_close = Api.UI.FindChild(AdvancedTips, 'btn_close')
            if Api.UI.IsActiveInHierarchy(btn_close) then
              id = Api.Guide.Listen.Touch(btn_close,{y = -10, right = true,  force = true})
              Api.Task.Wait(id)
            end
          end
          

          local btn_up = Api.UI.FindChild(MiracleMain, 'btn_up')
          if Api.UI.IsActiveInHierarchy(btn_up) then
            id = Api.Guide.Listen.Touch(btn_up,{text = Constants.GuideText.LvUpClick,y = -40, right = true,  force = true})
            Api.Task.Wait(id)
          end

          local tbt_use = Api.UI.FindChild(MiracleMain, 'tbt_use')
          if Api.UI.IsActiveInHierarchy(tbt_use) then
            id = Api.Guide.Listen.Touch(tbt_use,{text = Constants.GuideText.ClothMiracleClick,y = 0, right = true,  force = true})
            Api.Task.Wait(id)
          end

          local cvs_zhu = Api.UI.FindChild(MiracleMain, 'cvs_zhu')
          if Api.UI.IsActiveInHierarchy(cvs_zhu) then
            id = Api.Guide.Listen.Touch(cvs_zhu,{y = -10, left = true,  force = true})
            Api.Task.Wait(id)
          end
          
	     
     
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
	-- body
end