function main()
    --释放神器技能
        --Api.SubscribeGlobalBack('event.'..ID, function() return true end)
        Api.Task.WaitActorReady()
	 	local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_5/skill/Effect/EF_UI_Partner_Activation(Clone)')
        local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_5/skill')
        local id 
        local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        local tbt_zidong = Api.UI.FindChild(ui, 'tbt_an1')
        if tbt_zidong ~= nil and Api.UI.IsActiveInHierarchy(tbt_zidong) and Api.UI.IsChecked(tbt_zidong) then
              local eid = Api.Guide.Listen.Touch(tbt_zidong,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = true,type = 2})
              Api.Task.Wait(eid)
        end
        local function OnTrigger()
            Api.Task.StopEvent(id)
			Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_guide_playusemiracleskill.assetbundles', false)
            local eid = Api.Guide.Listen.Touch(skillobj,{text = Constants.GuideText.UseMiracleSkill,y = 0, right = true,  force = false,type = 2})
            Api.Task.Wait(eid)
        end
        id = Api.Listen.AddPeriodicSec(0,function()
            if obj and Api.Scene.IsActiveInHierarchy(obj) and Api.IsInBattleStatus() then
                Api.Task.AddEventTo(ID,OnTrigger)
            end
        end)
        
        Api.Task.Wait()
end

function clean()
        --Api.UnsubscribeGlobalBack('event.'..ID)
end