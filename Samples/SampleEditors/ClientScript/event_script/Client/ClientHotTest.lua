function main()
    print('HotTest client reload222111')
     --local p = Api.GetPlayerUUID()
    print('user id', p)
      -- do return end
    -- pprint('api ',Api)
    -- local aa = Api.FindExcelData('bag/bag_config.xlsx/bag_config', {})
    -- pprint('result', Api)

    -- Api.PlayEffect('/res/effect/ui/ef_ui_alchemy_success.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
    -- -- Api.ReStart()
    -- -- Api.UI.CloseAll()
    -- -- Api.Task.AddEvent('Client/countdown_ui', 20, Constants.Text.pvp_countdown)
    --Api.Task.AddEvent('Client/excel_supperzzle', 1, Api.UpsetArray({1})

     
  


    -- Api.Task.AddEvent('Client/excel_alchemy', 80001)
    -- Api.Task.AddEvent('Client/camera_lookme', Api.GetActorID())

    -- Api.Task.AddEvent('Client/camera_lookme', 1)

    -- 炼丹测试
    -- Api.Task.AddEvent('Client/excel_alchemy', 80001)
    -- Api.Task.Wait()

    -- 罗盘测试
    -- Api.Task.AddEvent('Client/excel_treasure', 51001)
    -- Api.Task.Wait()

    -- 转盘测试
    -- Api.Task.AddEvent('Client/excel_turntable', 1)
    -- Api.Task.AddEvent('client/Effect/ui_yindao_turntable')
    -- Api.Task.Wait()

    -- 翻拍测试
    -- Api.Task.AddEvent('Client/excel_supperzzle', 1, Api.UpsetArray({1,2,3,4,5,6}))
    -- Api.Task.AddEvent('client/Effect/ui_yindao_supperzzle')
    -- Api.Task.Wait()

    -- 刮卡测试
    -- Api.Task.AddEvent('Client/excel_scratch', 1,Api.UpsetArray({1}))
    -- Api.Task.AddEvent('client/Effect/ui_yindao_scratch')
    -- Api.Task.Wait()

    -- 九宫测试   
    -- Api.Task.AddEvent('Client/excel_ninth_palace', 1)
    -- Api.Task.AddEvent('client/Effect/ui_yindao_ninth_palace')
    -- Api.Task.Wait()

    --狮妖界面

    -- Api.Task.AddEvent('client/Effect/ui_yindao_stalker')
    -- Api.Task.Wait()
    -- Api.PlayEffect('/res/effect/ui/ef_ui_event_qiyu.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}})

  
        -- Api.Task.AddEvent('client/Effect/ui_remind')

    -- do return end
    -- Api.CG.Task.EnterGameWorld()
    -- local eff = {
    --     -- BindBody = true,
    --     Name = '/res/effect/ef_player_kl_skill01_01.assetbundles'
    -- }
    -- local x, y = Api.GetActorPostion()
    -- local u = Api.CG.CreateUnit()
    -- print('u-=-----------------',u)
    -- local sample = {
    --     -- {Api.Task.DelaySec, 5},
    --     'Sequence',
    --     {Api.CG.Task.LoadUnit, u, '/res/unit/player02.assetbundles'},
    --     {Api.CG.UnitSetPostion, u, x, y},
    --     {Api.CG.UnitSetDirection, u, 2.33},
    --     {Api.Camera.FollowCGUnit,u},
    --     {
    --         'Parallel',
    --         {Api.CG.UnitPlayEffect, u, eff},
    --         {Api.Task.DelaySec, 10},
    --     },
    --     --{Api.CG.UnitPlayEffect, u, eff},
    --     {Api.CG.Task.UnitMoveTo,u,6,x+20,y+9},
    -- }


    
    -- local function Printtest(name)
    --     print('test ------11111', os.clock(), name)
    -- end

    -- local sample2 = {
    --     'Sequence',
    --     -- Arg = 5,
    --     {Api.Task.DelaySec, 5},
    --     {Api.Task.AddEvent, Printtest, tostring(u)},
    --     {Api.Task.DelaySec, 5},
    --     {Api.Task.AddEvent, Printtest, 'Sequence11'}
    -- }
    -- -- Api.Task.Wait(Api.Task.RunEvents(sample))
    -- print('end ? ---------------')
    -- -- Api.Task.Wait()

    
    -- Api.Task.StartEventByKey('itemshow.1')

    -- local id = Api.Task.Wait(Api.Task.CreateActor3DModel())
    -- local eventId =
    --     Api.Task.UnitMountVehicle(
    --     {
    --         InstanceID = id,
    --         VehicleFileName = 'Mount_Horse'
    --     }
    -- )
    -- VehicleID = Api.Task.Wait(eventId)
    -- local p = {
    --     InstanceID = id,
    --     AnimationName = 'm_tame01'
    -- }
    -- Api.UnitPlayAnimation(p)
    -- p.InstanceID = VehicleID
    -- Api.UnitPlayAnimation(p)
    -- p.Direction = 1.3
    -- Api.SetUnitDirection(p)
    -- Api.Task.Sleep(555)
    -- Api.DestroyUnityObject(VehicleID)
    -- do
    --     return
    -- end
    -- local animPath = 'dynamic/effect/mission_complete/output/mission_complete.xml'
    -- local animName = 'mission_complete'
    -- Api.UI.Task.PlayScreenCPJOnce(animPath, animName, 0, -640 * 0.25)
    -- do return end
    --  Api.Task.Sleep(5)

    --  Api.Task.StartEvent('Client/excel_horse', 51001)

    -- Api.PlayFadeInEffect(2)
    -- Api.Task.AddEvent('Client/excel_treasure', 51001)

    -- local objID = Api.GetActorID()
    -- local eff = {
    --     BindBody = true,
    --     Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
    -- }

    -- Api.PlayUnitEffect(objID, eff)
    -- Api.AddBubbleTalk(objID, '哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈',3)
    -- Api.PlayUnitAnimation(objID,'f_attack01').
   


    -- Api.Task.AddEvent('Client/excel_alchemy', 80001)
    -- -- Api.Camera.
     -- Api.Task.AddEvent('Client/excel_supperzzle', 1, Api.UpsetArray({1,2,3,4,5,6,7,8}))
     -- Api.Task.Wait()
    -- do return end

    ------------------------------------------------------------
    --     Api.CG.Task.EnterGameWorld()
    --     local loadids = {}
    --     local x,y = Api.GetActorPostion()
    --     table.insert(loadids,Api.CG.Task.CreateUnit('/res/unit/player02.assetbundles',x,y+1,2))
    --     table.insert(loadids,Api.CG.Task.CreateUnit('/res/unit/player02.assetbundles',x,y+2,2))
    --     table.insert(loadids,Api.CG.Task.CreateUnit('/res/unit/player02.assetbundles',x,y+3,2))
    --     -- table.insert(loadids,Api.CG.Task.CreateUnit('/res/unit/player02.assetbundles',x,y+4,2))
    --     -- table.insert(loadids,Api.CG.Task.CreateUnit('/res/unit/player02.assetbundles',x,y+5,2))
    --     -- table.insert(loadids,Api.CG.Task.CreateUnit('/res/unit/player02.assetbundles',x,y+6,2))

    --     local eff = {
    --         -- BindBody = true,
    --         Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
    --     }

    --     -- Api.Task.Sleep(2)

    --     local success,result = Api.Task.WaitParallel(loadids)
    --     local ids = {}
    --     loadids = {}
    --     for _,v in ipairs(result) do
    --         local id = Api.UnpackOutput(v)
    --         table.insert(ids, id)
    --         Api.Camera.FollowCGUnit(id)
    --         Api.CG.UnitPlayEffect(id,eff)
    --         Api.CG.AddBubbleTalk(id, "的温柔热污染微软234 "..id,3)
    --         table.insert(loadids,Api.CG.Task.UnitMoveTo(id,6,x+20,y+9))

    --     end
    --     Api.Task.WaitParallel(loadids)
    --     -- Api.Camera.ResetLocation()
    --     -- do return end
    --     Api.Task.Sleep(3)
    --     Api.Camera.SetLocation({x=0,y=0,z=0})
    --     Api.Task.Sleep(3)
    --     local mid = Api.Camera.Task.MoveTo({x=172,y=1.62,z=77},3)
    --     local rid = Api.Camera.Task.RotateTo({x=55,y=27,z=77},3)
    --     Api.Task.Wait(mid)
    --     Api.Task.Wait(rid)
    --     --Api.Camera.ResetLocation()

    --     -- if Api.IsEventSuccess(id) then
    --     --     print('ok--')
    --     -- end

    -- end
    -------------------------------------------------------------------------------------------------------



--     Api.UI.SetHudVisible(false)

--     Api.Camera.Task.MoveTo({x=0,y=153,z=100},0.5)

--     Api.Task.Wait()

--     Api.SetTimeScale(0.5)

--     Api.Camera.Task.MoveToAndSet({x=0,y=8,z=-8},2)

--     Api.Task.Sleep(2)
--     Api.SetTimeScale(1)
--     Api.Task.Sleep(1.5)

--     -- Api.Task.Wait(cam)
-- end
    -------------------------------------------------------------------------------------------------------



-- local obj = Api.Scene.FindGameObject('MapObject/MapNode/rot/tiangong_map01_ANIMqiao_FBX')

-- Api.Scene.PlayAnimation(obj,'dead')


-- Api.ShowWeather({ShowWeather = {"EF_rain","FastSnow","Blizzard"}})


-- Api.UI.SetHudVisible(false)


-- Api.Task.Sleep(5)

-- Api.ShowWeather({CloseWeather = {"EF_rain","FastSnow"}})


    -- local cam = Api.Camera.SetArgument({pos = {{x=11,y=-12},{x=11.1,y=-12.1},{x=11.2,y=-12.2}}, agl = {42,42,42} ,fov = {45,45,45}},{x=0,y=11,z=-12},{x=10,y=0,z=0})

    -- Api.Task.Wait(cam)


-- end

-- function clean()
--    Api.Camera.ResetLocation()


--     Api.UI.SetHudVisible(true)



-- end


--   Api.Camera.StopFollowActor()

--     Api.UI.SetHudVisible(false)

--     Api.Camera.Task.MoveTo({x=148,y=1,z=89}, 0.5)

--     Api.Task.Wait()

--     Api.SetTimeScale(0.5)

--     Api.Camera.Task.MoveToAndSet({x=0,y=8,z=-8},2)
--     Api.Camera.Task.RotateToAndSet({x=0,y=8,z=-8},2)

--     Api.Task.Sleep(2)
--     Api.SetTimeScale(1)
--     Api.Task.Sleep(1.5)
-- end

-- function clean()
--     Api.SetTimeScale(1)
--     Api.UI.SetHudVisible(true)
--     Api.Camera.ResetLocation()
end