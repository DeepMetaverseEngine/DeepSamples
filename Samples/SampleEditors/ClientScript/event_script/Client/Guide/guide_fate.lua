function main()
         -- 神器
          Api.SubscribeGlobalBack('event.'..ID, function() return true end)
         Api.Task.WaitActorReady()
          local fate =  Api.UI.FindByTag('RoleBagFate')
          if fate == nil then
              local eid = Api.UI.Listen.OpenFunMenu()
              Api.Task.Wait(eid)
              Api.Guide.WaitMenuIsOpenAndGuide()
              local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
              local btn_fate = Api.UI.FindChild(ui, 'btn_fate',true)
              Api.UI.Listen.Visible(btn_fate)
              local id = Api.Guide.Listen.Touch(btn_fate,{text = Constants.GuideText.FateClick,y = -40, right = true,  force = true,reverse = 1})
              Api.Task.Wait(id)
              Api.Guide.Listen.FindUIByTag('RoleBagFate')
              fate =  Api.UI.FindByTag('RoleBagFate')
          end

          if fate == nil then
              return false
          end
           
            Api.UI.Listen.MenuExit(
            'FateMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
          local btn_low = Api.UI.FindChild(fate, 'btn_low',true)
          local id = Api.Guide.Listen.Touch(btn_low,{text = Constants.GuideText.ScrollableClick,y = -50, right = true,  force = true})
          Api.Task.Wait(id)
          
           local scrollablenode = Api.UI.FindChild(fate, function(child)
                  local name = Api.UI.GetNodeName(child)
                  if string.find(name, 'scrollable') then
                      return true
                  else
                      return false
                  end 
            end)

           local itemnode = nil
           local waitid = 0
            waitid = Api.Listen.AddPeriodicSec(0.1, function()
                  itemnode = Api.UI.FindChild(scrollablenode, function(child)
                      local name = Api.UI.GetNodeName(child)
                      if string.find(name, ".png") and name ~= '229999.png' then
                           return true
                      end 
                      return false 
                  end)
                  if Api.UI.IsActiveInHierarchy(itemnode) then
                     Api.Task.StopEvent(waitid)
                  end
            end)
          Api.Task.Wait(waitid)

        
          if Api.UI.IsActiveInHierarchy(itemnode) then
              local id = Api.Guide.Listen.Touch(itemnode,{text = Constants.GuideText.ScrollableSelect,y = 0, right = true,  force = true})
              Api.Task.Wait(id)
          end

          Api.Guide.Listen.FindUIByTag('ItemDetail')
          local ItemDetail =  Api.UI.FindByTag('ItemDetail')
          local btn_1 = Api.UI.FindChild(ItemDetail, 'btn_1',true)
          if Api.UI.IsActiveInHierarchy(itemnode) then
              local id = Api.Guide.Listen.Touch(btn_1,{text = Constants.GuideText.ScrollableEquip,y = 0, right = true,  force = true})
              Api.Task.Wait(id)
          end

end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end