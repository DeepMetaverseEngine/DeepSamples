function main(dontplay_mapcg,dead_effect)
    Api.FireEventMessage('Event.Scene.Tutorial', true)
    Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )
    ui = Api.UI.Open('xml/hud/ui_hud_xuzhang.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    Api.UI.SetFrameEnable(ui, false)
    local btn_exit = Api.UI.FindChild(ui, 'btn_exit')
    Api.UI.SetScreenAnchor(btn_exit, 'r_t')
    local hud = Api.UI.FindByXml('xml/hud/ui_hud_other.gui.xml')
    cvs_chat = Api.UI.FindChild(hud, 'cvs_chat')
    btn_zuoqi = Api.UI.FindChild(hud, 'btn_zuoqi')
    cvs_topright = Api.UI.FindChild(hud, 'cvs_topright')
    cvs_zidong = Api.UI.FindChild(hud, 'cvs_zidong')
    item_up = Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Item2')
    if item_up then
        Api.Scene.SetActive(item_up,false)
    end
    Api.UI.SetQuestHudVisible(false)
    Api.UI.SetVisible(cvs_chat, false)
    Api.UI.SetVisible(cvs_topright, false)
    Api.UI.SetVisible(btn_zuoqi, false)
    Api.UI.SetVisible(cvs_zidong, false)
    Api.UI.SetGoRoundVisible(false)
    Api.UI.Listen.TouchClick(
        btn_exit,
        function()
            Api.Task.AddEventTo(ID, function()
                local ok = Api.Task.Wait(Api.UI.Task.ShowConfirmAlert(Constants.Text.first_map_warn))
                if ok then
                    Api.Task.Wait(Api.Protocol.Task.Request('ExitFirstMap'))
                end
            end)
        end
    )
    if not dontplay_mapcg then
        Api.SetBlackScreen(true)
        Api.PauseBGM()
        local eid = Api.Task.PlayCG('dungen100000_1')
        Api.Task.WaitActorReady()
        Api.Task.Sleep(0.5)
        Api.SetBlackScreen(false)
        local hudup = Api.UI.FindByXml('xml/hud/ui_hud_up.gui.xml')
        if hudup then
            Api.UI.Close(hudup, false)
        end
        Api.Task.Wait(eid)

        Api.ChangeBGM("/res/sound/dynamic/bgm/zhandou3.assetbundles")
			
		local pro = Api.GetActorPro()
		local gender = Api.GetActorGender()
		local TalkContentbyIds = {
			{'100000_1_41','100000_1_42'},
			{'100000_1_21','100000_1_22'},
			{'100000_1_31','100000_1_32'},
			{'100000_1_11','100000_1_12'}
		}
		Api.Task.Wait(Api.UI.Task.TalkContentbyId(TalkContentbyIds[pro][gender+1]))
		
--		Api.Task.Wait(Api.UI.Task.TalkContentbyId('100000_1'))
--		api.Wait(api.Quest.TalkContentbyId("100000_1"))
        Api.Task.StartEventByKey('client.Guide/guide_playermove')
        Api.Task.StartEventByKey('client.Design/qiao_idle')
--      Api.Task.StartEventByKey('client_message.63',{})
	else
        Api.Task.WaitActorReady()
        local hudup = Api.UI.FindByXml('xml/hud/ui_hud_up.gui.xml')
        if hudup then
            Api.UI.Close(hudup, false)
        end
    end

    if dead_effect then
        Api.Task.StartEventByKey('client.Design/fubencamera3')
        Api.Task.StartEventByKey('client.Design/qiao_dead')
    end
--    Api.ChapterZeroStart()
    Api.Task.WaitAlways()
end

function clean()
    Api.FireEventMessage('Event.Scene.Tutorial', false)
    Api.SetBlackScreen(false)
    Api.UI.Close(ui)
    Api.UI.SetVisible(cvs_chat, true)
    Api.UI.SetVisible(btn_zuoqi, true)
    Api.UI.SetVisible(cvs_topright, true)
    Api.UI.SetVisible(cvs_zidong, true)
    if item_up then
        Api.Scene.SetActive(item_up,true)
    end
    Api.UI.SetGoRoundVisible(true)
    Api.UI.SetQuestHudVisible(true)
end