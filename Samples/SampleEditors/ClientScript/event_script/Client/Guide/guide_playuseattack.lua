function main()
	-- 技能1使用
	 	-- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill')
   --      --print('obj ',obj)
   --      local id = Api.Guide.Listen.Touch(obj,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = false,type = 2})
   --      Api.Task.WaitAlways(id)
       --print("ScriptDesc",ScriptDesc)
   -- Api.SubscribeGlobalBack('event.'..ID, function() return true end)
        Api.Guide.RemoveRepeat(ScriptDesc,ID)
        Api.Task.WaitActorReady()
        Api.Guide.WaitMenuIsOpenAndGuide(Constants.GuideText.CloseMenu,false)
       
     --   local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill')

        local id 
		local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill/Num/BattleNumber')

        local function OnTrigger2()
			local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill')

			Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_guide_playuseskill1_2.assetbundles', false)
            local eid = Api.Guide.Listen.Touch(skillobj,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = false,type = 2})
            Api.Task.Wait(eid)
        end
		
		local function OnTrigger1()
			Api.Task.StopEvent(id)
			local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Attack/skill')

			local sound1 =  Api.PlaySound('/res/sound/dynamic/guidesound/sd_guide_guide_playuseskill1.assetbundles', false)
            local eid = Api.Guide.Listen.Touch(skillobj,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = false,type = 2})
            Api.Task.Wait(eid)
			Api.StopSound(sound1)
			OnTrigger2()
        end
		id = Api.Listen.AddPeriodicSec(0,function()
            if Api.Scene.IsActiveInHierarchy(obj)then
                Api.Task.AddEventTo(ID,OnTrigger1)
            end
        end)
        
        Api.Task.Wait()
end

function clean()
        --Api.UnsubscribeGlobalBack('event.'..ID)
end