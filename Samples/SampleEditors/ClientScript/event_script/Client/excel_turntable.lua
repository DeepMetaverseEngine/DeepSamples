local function FixFileName(name)
    return '/res/effect/ui/' .. name .. '.assetbundles'
end

function main(id)
    local ele = Api.GetExcelByEventKey('turntable.' .. id)
    assert(ele)
    Api.UI.CloseAll()
    ui = Api.UI.Open('xml/hud/ui_hud_turntable.gui.xml')
    Api.UI.SetBackground(ui, 0.5)
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    
    local cvs_model = Api.UI.FindChild(ui, 'cvs_model')
    local cvs_modellist = Api.UI.FindChild(ui, 'cvs_modellist')
    local btn_close = Api.UI.FindChild(ui, 'btn_close')
    local ib_showpic = Api.UI.FindChild(ui, 'ib_showpic')
    Api.UI.SetImage(ib_showpic, ele.show_pic)
    local dragObject = Api.UI.GetUnityObject(cvs_modellist)
    Api.Scene.RemoveComponents(dragObject, 'ColliderRotateZ')
    local info = {
        Parent = cvs_model,
        Pos = {z = -200},
        Deg = {y = 180},
        Scale = 8
    }



    local ids = {}
    table.insert(ids, Api.Scene.Task.LoadGameObject(FixFileName(ele.core), info))
    for i, v in ipairs(ele.ring) do
        local inforing = {
            Parent = cvs_model,
            Pos = {z = -200},
            Deg = {y = 180, z = math.random(90, 270)},
            Scale = 8
        }
        if not string.IsNullOrEmpty(v) then
            table.insert(ids, Api.Scene.Task.LoadGameObject(FixFileName(v), inforing))
        end
    end
    local ok, results = Api.Task.WaitParallel(ids)
    assert(ok)

    local rings = {}
    for i = 2, #results do
        local ringObjID = Api.UnpackOutput(results[i])
        table.insert(rings, ringObjID)
    end
    for _, v in ipairs(rings) do
        Api.Scene.RotateColliderZ(dragObject, v)
    end

    local offset_angle = 5
    local function Tick()
        local right = true
        for _, v in ipairs(rings) do
            local angles = Api.Scene.GetLocalEulerAngles(v)
            local m = math.abs(angles.z % 360)
            if m > offset_angle and (360 - m) > offset_angle then
                right = false
                break
            end
        end
        if right then
            Api.Task.StopEvent(timeEvent)
        end
    end

    timeEvent = Api.Listen.AddPeriodicSec(0.5, Tick)
    Api.Task.Wait(timeEvent)
    Api.UI.SetEnable(cvs_modellist, false)
    Api.PlayEffect('/res/effect/ui/ef_ui_turntable_seal_01.assetbundles', {Parent = cvs_model, Deg = {x = 90}, Scale = 8, Pos = {z = -500}})
    Api.PlaySound('/res/sound/static/uisound/sd_gamewin.assetbundles')
    Api.Task.Wait(
    Api.Task.PlayEffect('/res/effect/ui/ef_ui_turntable_seal_02.assetbundles', {UILayer = true, Pos = {y = 200 ,z = -500 }})
       
    )
    
end

function clean()
    Api.UI.Close(ui)
end
