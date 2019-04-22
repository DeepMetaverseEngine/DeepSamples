function main()
  --吃药引导
  		-- local step = Api.Guide.GetGuideStep("guidefriend")
  		-- if step then
  		-- 	return 
  		-- end
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)		
      Api.Task.WaitActorReady()
       local MedicineMain =  Api.UI.FindByTag('MedicinePoolMain')
       if MedicineMain == nil then
         local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
         local btn_morehp = Api.UI.FindChild(ui, 'btn_morehp')
         Api.UI.Listen.Visible(btn_morehp)
         --print('ui',ui,tbt_zidong)
         local id = Api.Guide.Listen.Touch(btn_morehp,{text = Constants.GuideText.OpenMedicineMain,x = 40,y = -10, left = true, force = true})
         Api.Task.Wait(id)
         Api.Guide.Listen.FindUIByTag('MedicinePoolMain')
         MedicineMain =  Api.UI.FindByTag('MedicinePoolMain')
       end

      
       if MedicineMain == nil then 
            return false
        end

        Api.UI.Listen.MenuExit(
                'MedicinePoolMain',
                function()
                    Api.Task.StopEvent(ID, true, 'MenuExit')
                end)
        
        -- local sp_medicinelist = Api.UI.FindChild(MedicineMain,'sp_medicinelist')
        -- if Api.UI.IsActiveInHierarchy(sp_medicinelist) then
          -- local cell = Api.UI.GetScrollListCell(sp_medicinelist, 1)
          -- if cell then
              -- Api.Task.Sleep(0.3)

             
              -- local cvs_medicine1 = Api.UI.FindChild(cell,function(child)
                -- return true
              -- end)
              -- local img = Api.UI.FindChild(cell,function(child)
                  -- return Api.UI.GetUserTag(child)==599999
              -- end)
              -- if img == nil or not Api.UI.IsActiveInHierarchy(img) then
                  -- local id = Api.Guide.Listen.Touch(cvs_medicine1,{text = Constants.GuideText.SelectMedicine, y = 0, left = true,  force = true})
                  -- Api.Task.Wait(id)
              -- end

             
          -- end
        -- end
        -- local tbt_use1 = Api.UI.FindChild(MedicineMain,'tbt_use1')
        -- if not Api.UI.IsChecked(tbt_use1) then
        --    local id = Api.Guide.Listen.Touch(tbt_use1,{text = Constants.GuideText.SelectFootprint,y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        -- end
         local tbt_autoheal = Api.UI.FindChild(MedicineMain,'tbt_autoheal')
        if not Api.UI.IsChecked(tbt_autoheal) then
		      Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_usedrugs.assetbundles', false)
           local id = Api.Guide.Listen.Touch(tbt_autoheal,{text = Constants.GuideText.MedicineClick,y = -10, right = true,  force = true})
              Api.Task.Wait(id)
        end
				
	
         local tbt_autoput = Api.UI.FindChild(MedicineMain,'tbt_autoput')
        if not Api.UI.IsChecked(tbt_autoput) then
           local id = Api.Guide.Listen.Touch(tbt_autoput,{text = Constants.GuideText.MedicineClick,y = -10, right = true,  force = true})
              Api.Task.Wait(id)
        end
		
         local btn_one = Api.UI.FindChild(MedicineMain,'btn_one')
		 if Api.UI.IsInteractive(btn_one) then
           local id = Api.Guide.Listen.Touch(btn_one,{text = Constants.GuideText.MedicineClick,y = -10, right = true,  force = true})
              Api.Task.Wait(id)
	     end
  
         local btn_ok = Api.UI.FindChild(MedicineMain,'btn_ok')
           local id = Api.Guide.Listen.Touch(btn_ok,{text = Constants.GuideText.MedicineClick,y = -10, right = true,  force = true})
              Api.Task.Wait(id)

		    -- Api.Guide.SetGuideStep("guidefriend","1")
end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
	-- body
end