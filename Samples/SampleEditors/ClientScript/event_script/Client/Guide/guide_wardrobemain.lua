function main()
    --衣柜引导
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    Api.Task.WaitActorReady() -- 等待玩家登入
    local WardrobeMain = Api.UI.FindByTag('WardrobeMain')
    if WardrobeMain == nil then
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local btn_wardrobe = Api.UI.FindChild(ui, 'btn_wardrobe', true)
        --print('ui',ui,tbt_zidong)
        local id = Api.Guide.Listen.Touch(btn_wardrobe, { text = Constants.GuideText.OpenWardrobeMain, y = -10, right = true, force = true })
        Api.Task.Wait(id)
        Api.Guide.Listen.FindUIByTag('WardrobeMain')
        WardrobeMain = Api.UI.FindByTag('WardrobeMain')

    end
    if WardrobeMain == nil then
        return false
    end
    Api.UI.Listen.MenuExit(
            'WardrobeMain',
            function()
                Api.Task.StopEvent(ID, true, 'MenuExit')
            end)
    local tbt_an_m4 = Api.UI.FindChild(WardrobeMain, 'tbt_an_m4')
    if not tbt_an_m4 then
       Api.Guide.Listen.FindUIChild(WardrobeMain, 'tbt_an_m4',function(node)
            tbt_an_m4 = node
        end)
    end
	Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_wardrobemain.assetbundles', false)
    local id = Api.Guide.Listen.Touch(tbt_an_m4, { text = Constants.GuideText.EffectClick, y = 0, left = true, force = true })
    Api.Task.Wait(id)

     Api.Task.Sleep(0.1)
    local sp_itemlist = Api.UI.FindChild(WardrobeMain,'sp_itemlist')
    local cell = Api.UI.GetScrollListCell(sp_itemlist, 1)
    if not cell then
        Api.Guide.Listen.GetScrollListCell(sp_itemlist,1,function(node)
            cell = node
        end)
    end
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
	 local id = Api.Guide.Listen.Touch(itemnode,{text = Constants.GuideText.SelectFootprint,y = 70,x = 300, right = true,  time=5 ,OnlyText = true })
	  Api.Task.Wait(id)
     --id = Api.Guide.Listen.Touch(cell,{text = Constants.GuideText.SelectFootprint,y = -10, right = true,  force = true, time=2  })
    -- Api.Task.Wait(id)
    --todo 获取制定id的特效
    -- local btn_unlock = Api.UI.FindChild(WardrobeMain, 'btn_unlock')
    -- if Api.UI.IsActiveInHierarchy(btn_unlock) then
    --   id = Api.Guide.Listen.Touch(btn_unlock,{text = Constants.GuideText.ActiveClick,y = -10, right = true,  force = true})
    --   Api.Task.Wait(id)
    -- end
    -- Api.Task.Sleep(0.3)
    -- local btn_yongjiu = Api.UI.FindChild(WardrobeMain, 'btn_yongjiu',true)
    -- if Api.UI.IsActiveInHierarchy(btn_yongjiu) then
    --   id = Api.Guide.Listen.Touch(btn_yongjiu,{text = Constants.GuideText.ActiveClick,y = -10, right = true,  force = true})
    --   Api.Task.Wait(id)
    -- end

end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end