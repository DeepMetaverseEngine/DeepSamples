function main()
    -- 变身引导
        Api.SubscribeGlobalBack('event.'..ID, function() return true end)

        Api.Task.WaitActorReady()
        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.CloseMenu,false)
	    local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_7/skill/Buf/EF_UI_Partner_Activation(Clone)')
        local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_7/skill')
        local id 
        local function OnTrigger()
            Api.Task.StopEvent(id)
            local eid = Api.Guide.Listen.Touch(skillobj,{text = Constants.GuideText.BecomeDaddyClick,y = -10, right = true,  force = false})
            Api.Task.Wait(eid)
        end
		Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_becomedaddy.assetbundles', false)

        id = Api.Listen.AddPeriodicSec(0.3,function()
            if obj ~= nil and Api.Scene.IsActiveInHierarchy(obj) then
                Api.Task.AddEventTo(ID,OnTrigger)
            end
        end)
        
        Api.Task.Wait(id)
end

function clean()
    Api.UnsubscribeGlobalBack('event.'..ID)
end