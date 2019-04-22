function main()
    -- api.UI.CleanloadedCache()
    print('string.utf8len',string.utf8len)
    -- local a = GlobalHooks.UI.FindUI('TeamInfo')
    -- for i=1,10 do
    --     a._lastclock = 0
    --     api.Sleep(0.01)
    --     a:RefreshList()
    -- end
    -- EventApi.ReStart()
    -- local source = {tag = 'RoleBag111',info = {'UI/Smithy/SmithyCompose','xml/forge/ui_forge_synthetic.gui.xml'}}
    -- GlobalHooks.UI.OpenUI(source,0)
    -- local function FixEffectRes(filename)
    --     return '/res/effect/ui/'..filename..'.assetbundles'
    -- end

    -- local transSet = TransformSet()
    -- transSet.Pos = Vector3(0,0,0)
    -- RenderSystem.Instance:PlayEffect(FixEffectRes('EF_UI_Interface_Advanced'), transSet, 0, 0)
    -- RenderSystem.Instance:PlayEffect(FixEffectRes('EF_UI_Interface_Upgrade'), transSet, 0, 0)
    local Api = EventApi
    Api.SetAllDirty()
    local ID
    ID = Api.Task.StartEvent(function()
        -- local obj = Api.Scene.FindGameObject('MapObject/MapNode/rot/tiangong_map01_ANIMqiao_FBX')
        -- print('obj',obj)
        -- Api.Scene.PlayAnimation(obj,'huai')
        -- Api.Task.Sleep(10)
        -- local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        -- local btn_beibao = Api.UI.FindChild(ui, 'btn_beibao')
        -- print('ui',ui,btn_beibao)
        -- local id = Api.Guide.Listen.Touch(btn_beibao,{text = '<f color="ffff0000">你好漂亮</f>',y = -30, left = true, force = true})
        -- local ok,ui = Api.Task.Wait(Api.UI.Listen.MenuEnter('RoleBagItem'))
        -- -- Api.Task.Sleep(3)
        -- local btn_neaten = Api.UI.FindChild(ui, 'btn_neaten')
        -- Api.Guide.Listen.Touch(btn_neaten,{text = '<f color="ffff0000">你好漂亮a</f>',y = -30, left = true})
        -- Api.Task.Wait(Api.UI.Listen.MenuExit('RoleBagItem'))

        -- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/RockerFrame/RockerBG')
        -- --print('obj ',obj)
        -- local id = Api.Guide.Listen.Touch(obj,{text = '<f color="ffff0000">滑动摇杆移动</f>',y = -10, left = true,  force = false})

        -- Api.Listen.AddPeriodicSec(
        --     0.03,
        --     function()
        --       if Api.InRockMove() then
        --          Api.Task.StopEvent(ID)
        --       end
        --     end
        -- )
        -- Api.Task.WaitAlways(id)
        -- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill')
        -- --print('obj ',obj)
        -- local id = Api.Guide.Listen.Touch(obj,{text = '<f color="ffff0000">点击技能</f>',y = -10, right = true,  force = false})
        -- Api.Task.WaitAlways(id)


        -- local ui =  Api.UI.FindHud('xml/hud/ui_hud_team_quest.gui.xml')
        -- local sp_oar = Api.UI.FindChild(ui,'sp_oar')
        -- local cvs_kuang = Api.UI.FindChild(sp_oar,function(child)
        --     return Api.UI.GetUserTag(child) == 1046 
        --  end)
        -- local id = Api.Guide.Listen.Touch(cvs_kuang,{text = '<f color="ffff0000">点击任务寻路</f>',y = -10, left = true,  force = true})
        -- Api.Task.Wait(id)

        -- local SmithyMainUI =  Api.UI.FindByTag('SmithyFrame')
        -- local tbt_an1 = Api.UI.FindChild(SmithyMainUI, 'tbt_an1')
        -- local bid = Api.Guide.Listen.Touch(tbt_an1,{text = '<f color="ffff0000">点击强化</f>',y = -10, right = true,  force = true})
        -- Api.Task.Wait(bid)

        -- local SmithyStrengthenUI =  Api.UI.FindByTag('SmithyStrengthen')
        -- local cvs_clothes = Api.UI.FindChild(SmithyStrengthenUI, 'cvs_clothes')
        -- bid = Api.Guide.Listen.Touch(cvs_clothes,{text = '<f color="ffff0000">选择衣服</f>',y = -10, left = true,  force = true})
        -- Api.Task.Wait(bid)

        -- local btn_use = Api.UI.FindChild(SmithyStrengthenUI, 'btn_use')
        -- bid = Api.Guide.Listen.Touch(btn_use,{text = '<f color="ffff0000">点击强化</f>',y = -10, right = true,  force = true})
        --Api.Task.Wait(bid)
        -- --todo 获取制定id的特效
        -- local btn_unlock = Api.UI.FindChild(WardrobeMain, 'btn_unlock')
        -- local uid = Api.Guide.Listen.Touch(btn_unlock,{text = '<f color="ffff0000">点击激活</f>',y = -10, right = true,  force = true})
        -- Api.Task.Wait(uid)
        -- local function startlogic()
        --     Api.Task.Sleep(3)
        --     local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        --     local btn_skill = Api.UI.FindChild(ui, 'btn_skill')

        --     local id = Api.Guide.Listen.Touch(btn_skill,{text = '<f color="ffff0000">打开技能界面</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)

        --     local skillui =  Api.UI.FindByTag('SkillMain')
        --     local btn_allup = Api.UI.FindChild(skillui, 'btn_allup')
        --     local bid = Api.Guide.Listen.Touch(btn_allup,{text = '<f color="ffff0000">点击一键升级</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(bid)
        -- end
        -- local id = Api.Scene.Listen.OpenFunMenu('SkillFrame',function()
        --     startlogic()
        -- end)
        -- Api.Task.WaitAlways(id)

        -- local id = Api.Scene.Listen.OpenFunMenu('SmithyMain',function()
        --     startlogic()
        -- end)
        -- Api.Task.WaitAlways(id)


        --  local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
        --  local btn_wardrobe = Api.UI.FindChild(ui, 'btn_wardrobe')
        --  print('ui',ui,tbt_zidong)
        --  local id = Api.Guide.Listen.Touch(btn_wardrobe,{text = '<f color="ffff0000">打开衣柜</f>',y = -10, right = true, force = true})
        --  Api.Task.Wait(id)

        -- local WardrobeMain =  Api.UI.FindByTag('WardrobeMain')
        -- local tbt_an_m4 = Api.UI.FindChild(WardrobeMain, 'tbt_an_m4')
        -- id = Api.Guide.Listen.Touch(tbt_an_m4,{text = '<f color="ffff0000">点击特效</f>',y = -10, left = true,  force = true})
        -- Api.Task.Wait(id)

        -- local sp_itemlist = Api.UI.FindChild(WardrobeMain,'sp_itemlist')
        -- local cell = Api.UI.GetScrollListCell(sp_itemlist, 1)
        -- id = Api.Guide.Listen.Touch(cell,{text = '<f color="ffff0000">点击脚印</f>',y = -10, right = true,  force = true})
        -- Api.Task.Wait(id)
        -- --todo 获取制定id的特效
        -- local btn_unlock = Api.UI.FindChild(WardrobeMain, 'btn_unlock')
        -- id = Api.Guide.Listen.Touch(btn_unlock,{text = '<f color="ffff0000">点击激活</f>',y = -10, right = true,  force = true})
        -- Api.Task.Wait(id)


        -- local PracticeMain =  Api.UI.FindByTag('PracticeMain')
        -- if PracticeMain == nil then
        --     local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        --     local btn_practice = Api.UI.FindChild(ui, 'btn_practice')
        --     local id = Api.Guide.Listen.Touch(btn_practice,{text = Constants.GuideText.OpenPracticeMain ,y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        --     PracticeMain =  Api.UI.FindByTag('PracticeMain')
        -- end

        -- local btn_active = Api.UI.FindChild(PracticeMain, 'btn_active')
        -- if btn_active ~= nil then
        --     local bid = Api.Guide.Listen.Touch(btn_active,{text = Constants.GuideText.Understand,y = -10, right = true,  force = true})
        --     Api.Task.Wait(bid)
        -- end

        -- local id = Api.UI.Listen.OpenFunMenu()
        -- Api.Task.Wait(id)

        -- local MountMain =  Api.UI.FindByTag('MountMain')
        -- if MountMain == nil then
        --     local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        --     local btn_mount = Api.UI.FindChild(ui, 'btn_mount')
        --     local id = Api.Guide.Listen.Touch(btn_mount,{text = '<f color="ffff0000">打开坐骑界面</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        --     MountMain =  Api.UI.FindByTag('MountMain')
        -- end

        -- local tbt_an2 = Api.UI.FindChild(MountMain, 'tbt_an2')
        -- id = Api.Guide.Listen.Touch(tbt_an2,{text = '<f color="ffff0000">选择灵脉</f>',y = -10, right = true,  force = true})
        -- Api.Task.Wait(id)

        -- local MountVeins = Api.UI.FindByTag('MountVeins')
        -- local btn_use = Api.UI.FindChild(MountVeins, 'btn_use')
        -- id = Api.Guide.Listen.Touch(btn_use,{text = '<f color="ffff0000">点击激活</f>',y = -10, right = true,  force = true})
        -- Api.Task.Wait(id)

        -- local PartnerMain =  Api.UI.FindByTag('PartnerMain')
        -- if PartnerMain == nil then
        --     local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        --     local btn_partner = Api.UI.FindChild(ui, 'btn_partner')
        --     local id = Api.Guide.Listen.Touch(btn_partner,{text = '<f color="ffff0000">打开仙侣界面</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        --     PartnerMain =  Api.UI.FindByTag('PartnerMain')
        -- end

        -- local btn_jihuo = Api.UI.FindChild(PartnerMain, 'btn_jihuo')
        -- if Api.UI.IsVisible(btn_jihuo) then
        --     id = Api.Guide.Listen.Touch(btn_jihuo,{text = '<f color="ffff0000">点击激活</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        -- end


        -- local btn_use = Api.UI.FindChild(PartnerMain, 'btn_use')
        -- if Api.UI.IsVisible(btn_use) then
        --     id = Api.Guide.Listen.Touch(btn_use,{text = '<f color="ffff0000">点击培养</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        -- end


        -- local tbt_use = Api.UI.FindChild(PartnerMain, 'tbt_use')
        -- if Api.UI.IsVisible(tbt_use) and not Api.UI.IsChecked(tbt_use) then
        --     id = Api.Guide.Listen.Touch(tbt_use,{text = '<f color="ffff0000">点击出战</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        -- end


        -- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_7/skill/Buf/EF_UI_Partner_Activation(Clone)')
        -- local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_7/skill')
        -- local id 
        -- local function OnTrigger()
        --     Api.Task.StopEvent(id)
        --     local eid = Api.Guide.Listen.Touch(skillobj,{text = '<f color="ffff0000">点击变身</f>',y = -10, right = true,  force = false})
        --     Api.Task.Wait(eid)
        -- end
        -- id = Api.Listen.AddPeriodicSec(0.3,function()
        --     if Api.Scene.IsActiveInHierarchy(obj) then
        --         Api.Task.AddEventTo(ID,OnTrigger)
        --     end
        -- end)

        -- Api.Task.Wait()


        -- local MiracleMain =  Api.UI.FindByTag('MiracleMain')
        -- if MiracleMain == nil then
        --     local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        --     local btn_miracle = Api.UI.FindChild(ui, 'btn_miracle')
        --     local id = Api.Guide.Listen.Touch(btn_miracle,{text = '<f color="ffff0000">打开金轮界面</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        --     MiracleMain =  Api.UI.FindByTag('MiracleMain')
        -- end

        -- local sp_list = Api.UI.FindChild(MiracleMain, 'sp_list')
        -- local node = Api.UI.FindChild(sp_list, function (child)
        --       return Api.UI.GetUserTag(child) == 5 
        --     -- body
        -- end)
        -- if node ~= nil then
        --     local tbt_icon = Api.UI.FindChild(node,'tbt_icon')
        --     id = Api.Guide.Listen.Touch(tbt_icon,{text = '<f color="ffff0000">选择神农鼎</f>',y = -10, left = true,  force = true})
        --     Api.Task.Wait(id)
        -- end

        -- local btn_get = Api.UI.FindChild(MiracleMain, 'btn_get')
        -- if btn_get ~= nil and Api.UI.IsVisible(btn_get) then
        --     id = Api.Guide.Listen.Touch(btn_get,{text = '<f color="ffff0000">点击激活</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        --     Api.Task.Sleep(2)
        -- end

        --  local btn_up = Api.UI.FindChild(MiracleMain, 'btn_up')
        -- if btn_up ~= nil and Api.UI.IsVisible(btn_up) then
        --     id = Api.Guide.Listen.Touch(btn_up,{text = '<f color="ffff0000">点击升级</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)
        -- end


        -- local tbt_use = Api.UI.FindChild(MiracleMain, 'tbt_use')
        -- if tbt_use ~= nil then
        --     id = Api.Guide.Listen.Touch(tbt_use,{text = '<f color="ffff0000">点击穿戴</f>',y = -10, right = true,  force = true})
        --     Api.Task.Wait(id)

        --     local cvs_zhu = Api.UI.FindChild(MiracleMain, 'cvs_zhu')
        --     if cvs_zhu ~= nil then
        --         id = Api.Guide.Listen.Touch(cvs_zhu,{text = '<f color="ffff0000">点击装备</f>',y = -10, left = true,  force = true})
        --         Api.Task.Wait(id)
        --     end
        -- end

        -- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_5/skill/Num/BattleNumber')
        -- local skillobj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_5/skill')
        -- local id 
        -- local function OnTrigger()
        --     Api.Task.StopEvent(id)
        --     local eid = Api.Guide.Listen.Touch(skillobj,{text = '<f color="ffff0000">释放神器技能</f>',y = -10, right = true,  force = false})
        --     Api.Task.Wait(eid)
        -- end
        -- id = Api.Listen.AddPeriodicSec(0.3,function()
        --     if Api.Scene.IsActiveInHierarchy(obj) and Api.Scene.GetChildCount(obj) == 0 and Api.IsInBattleStatus() then
        --         Api.Task.AddEventTo(ID,OnTrigger)
        --     end
        -- end)

        -- Api.Task.Wait()
        -- Api.UI.Listen.OpenFunMenu()
        -- Api.UI.IsOpenMenu()
        -- Api.Task.Wait()
        -- local ui =  Api.UI.FindHud('xml/hud/ui_hud_mainmenu.gui.xml')
        -- local btn_miracle = Api.UI.FindChild(ui, 'btn_miracle')
        -- local cvs_miracle = Api.UI.FindChild(ui, 'cvs_miracle')
        -- print("isShow",Api.UI.IsVisible(Api.UI.GetParent(btn_miracle)))
        -- Api.Task.Wait(Api.UI.WaitShow(Api.UI.GetParent(btn_miracle)))
        -- print('okokokokokooko')
        -- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill')
        -- --print('obj ',obj)
        -- local id = Api.Guide.Listen.Touch(obj,{text = Constants.GuideText.UseSkill,y = -10, right = true,  force = true})
        --Api.Task.WaitAlways(id)
        -- local obj =  Api.Scene.FindGameObject('UGUI_ROOT/HudRoot/SkillManager/Skill_1/skill')
        -- print('obj ',obj)
        -- local id = Api.Guide.Listen.Touch(obj,{text = Constants.GuideText.UseSkill,y = 0, right = true,  force = false,type = 2,reverse = 1})
        -- Api.Task.WaitAlways(id)
		--guide_practicemain 
        Api.Task.StartEventByKey('client.Guide/guide_gemstone')
    end)
end
 