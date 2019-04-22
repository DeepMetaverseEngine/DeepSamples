function main()
	-- 技能2使用
	 	-- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_2/skill')
   --      --print('obj ',obj)
   --      local id = Api.Guide.Listen.Touch(obj,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = false,type = 2})
        --Api.Task.WaitAlways(id)
        --Api.SubscribeGlobalBack('event.'..ID, function() return true end)
        Api.Task.WaitActorReady()
        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.CloseMenu,false)
        local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_3/skill/Num/BattleNumber')
        local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_3/skill')
        local id 
        
        local function OnTrigger()
            Api.Task.StopEvent(id)
            local eid = Api.Guide.Listen.Touch(skillobj,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = false,type = 2})
            Api.Task.Wait(eid)
        end
        id = Api.Listen.AddPeriodicSec(0.3,function()
            if Api.Scene.IsActiveInHierarchy(obj)  then
				Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_playuseskill3.assetbundles', false)
                Api.Task.AddEventTo(ID,OnTrigger)
            end
        end)
        Api.Task.Wait()
end

function clean()
        --Api.UnsubscribeGlobalBack('event.'..ID)
end