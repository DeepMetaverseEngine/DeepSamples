function main()
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    local step = Api.Guide.GetGuideStep("guide_questshimen")
	
    if step then
        return true
    end
    --Api.Task.WaitActorReady()
    local UINpcTalk = Api.UI.FindByTag('UINpcTalk')
    if UINpcTalk == nil then
        Api.Guide.Listen.FindUIByTag('UINpcTalk')
        UINpcTalk = Api.UI.FindByTag('UINpcTalk')
    end

    if UINpcTalk == nil then
        return false
    end
 
    --Api.Task.Sleep(0.3)
    local tbn_an1 = Api.UI.FindChild(UINpcTalk, 'tbn_an1', true)
    if tbn_an1 ~= nil and Api.UI.IsActiveInHierarchy(tbn_an1) then
		Api.Guide.SetGuideStep("guide_questshimen","1")
        local id = Api.Guide.Listen.Touch(tbn_an1, {text = Constants.GuideText_ShiMenAccept, y = -40, right = true, force = true})
        Api.Task.Wait(id)
    end

  

end

function clean()
        Api.UnsubscribeGlobalBack('event.'..ID)
    -- body
end