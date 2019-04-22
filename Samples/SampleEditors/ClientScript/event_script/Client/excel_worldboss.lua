function main(bossName)
	print('-----------worldboss Start----------')
 	--创建UI
    ui = Api.UI.Open('xml/hud/ui_hud_worldboss.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    Api.UI.Listen.MenuExit(ui, function()
        Api.Task.StopEvent(ID, false, 'MenuExit')
    end)
	Api.UI.SetFrameEnable(ui, false)

	local cvs_tips = Api.UI.FindChild(ui, 'cvs_tips')
	Api.UI.SetVisible(cvs_tips, false)

	local btn_open = Api.UI.FindChild(ui, 'btn_open')
	Api.UI.Listen.TouchClick(
        btn_open,
        function()
        	Api.UI.SetVisible(cvs_tips, not Api.UI.IsVisible(cvs_tips))
        end
    )

    if not Api.string_IsNullOrEmpty(bossName) then
        local lb_name = Api.UI.FindChild(ui, 'lb_name')
        Api.UI.SetText(lb_name, Api.GetText(bossName))
    end
    

	local uid = Api.GetActorUUID() 
  	local actorInfo = Api.GetPlayerInfo(uid)

  	local lb_own = Api.UI.FindChild(ui, 'lb_own')
  	local lb_ranko = Api.UI.FindChild(ui, 'lb_ranko')
	local lb_ownum = Api.UI.FindChild(ui, 'lb_ownum')

	Api.UI.SetText(lb_own, tostring(actorInfo.Name))
	Api.UI.SetText(lb_ranko,Api.GetText('rank_outrank'))

      
	Api.UI.SetText(lb_ownum, '0')

	local function FillPlayerResult(node, index, data)
		pprint('-----------FillPlayerResult ', data)
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_player = Api.UI.FindChild(node, 'lb_player')
        local lb_num = Api.UI.FindChild(node, 'lb_num')
   
  		local playerInfo = Api.GetPlayerInfo(data.uid)

        Api.UI.SetText(lb_rank, tostring(index))
        Api.UI.SetText(lb_player, playerInfo.Name)
        Api.UI.SetText(lb_num, tostring(data.damage))
    end

	local sp_player = Api.UI.FindChild(ui, 'sp_player')

    local cvs_player = Api.UI.FindChild(ui, 'cvs_player')
    Api.UI.SetVisible(cvs_player, false)



    --监听服务端数据刷新消息
    Api.Listen.Message('worldboss.damages', function(ename, info)

        if info.playerRank > 0 then
            Api.UI.SetText(lb_ranko, tostring(info.playerRank))
        else
            Api.UI.SetText(lb_ranko, Api.GetText('rank_outrank'))
        end
        Api.UI.SetText(lb_ownum, tostring(info.playerDamage))
        

        Api.UI.SetScrollList(
        sp_player,
        cvs_player,
        #info.damages,
        function(node, index)
            FillPlayerResult(node, index, info.damages[index])
        end)
    end)


    Api.Task.Wait(Api.Listen.Message('worldboss.outRegion'))

end

function clean()
    Api.UI.Close(ui)
end