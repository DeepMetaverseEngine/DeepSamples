function main(id)
    local excel_data = Api.GetExcelByEventKey('alchemy.' .. id)
    assert(excel_data)
    Api.UI.CloseAll()
    --Api.PlayRippleEffect()
    ui = Api.UI.Open('xml/hud/ui_hud_liandan.gui.xml', UIShowType.Cover)
    Api.UI.SetBackground(ui, 0.5)

    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )

    local cvs_model = Api.UI.FindChild(ui, 'cvs_model')
    local btn_liandan = Api.UI.FindChild(ui, 'btn_liandan')
    local ib_liang = Api.UI.FindChild(ui, 'ib_liang')
    local ib_hua = Api.UI.FindChild(ui, 'ib_hua')
    local lb_percent = Api.UI.FindChild(ui, 'lb_percent')
    local cvs_di = Api.UI.FindChild(ui, 'cvs_di')
    local cvs_uieffect = Api.UI.FindChild(ui, 'cvs_uieffect')
    local ib_back = Api.UI.FindChild(ui, 'ib_back')
    local w, h = Api.UI.GetSize(cvs_di)
    local ww, hh = Api.UI.GetSize(ib_hua)

    local info = {
        Parent = cvs_model,
        Pos = {x = 138, y = -320},
        Deg = {x = 6, y = 180},
        Scale = 155
    }
    local ok = Api.Task.Wait(Api.Scene.Task.LoadGameObject('/res/unit/item_furnace.assetbundles', info))
    assert(ok)

    local info2 = {
        Parent = ib_back,
        Pos = {z = -200},
        Visible = false
    }
    local ok, id_gg = Api.Task.Wait(Api.Scene.Task.LoadGameObject('/res/effect/ui/ef_ui_alchemy_thermometer_04.assetbundles', info2))
    assert(ok)
    Api.Scene.SetAnchoredPosition(id_gg, {x = 0, y = 0})
    
    local gg1 = Api.Scene.FindChild(id_gg, 'Fill Area/Fill')
    local gg2 = Api.Scene.FindChild(id_gg, 'Fill Area/Fill (1)')
    Api.Scene.SetImageFillAmount(gg1, 0)
    Api.Scene.SetImageFillAmount(gg2, 0)
    Api.Scene.SetActive(id_gg, true)
    local function StartTouchPoint(speed)
        local lastPosX = 0
        local currentSpeed = speed
        Api.UI.SetPositionX(ib_hua, 0)
        Api.Listen.AddPeriodicSec(
            0.03,
            function()
                lastPosX = lastPosX + currentSpeed
                if lastPosX >= w - ww then
                    lastPosX = w - ww
                    currentSpeed = -speed
                elseif lastPosX <= 0 then
                    currentSpeed = speed
                end
                Api.UI.SetPositionX(ib_hua, lastPosX)
            end
        )
        local id = Api.UI.Listen.PointerDown(btn_liandan)
        Api.Task.Wait(id)
        -- print('lastposx', lastPosX)
        return true, lastPosX
    end
    SoundID = Api.PlaySound('/res/sound/static/uisound/sd_jindutiao.assetbundles', true)
    local onceGv = math.floor(100 / excel_data.times)
    local currentGv = 0
    local eff = {Parent = cvs_uieffect, Pos = {x = 0, y = 0, z = -500}}
    for i, length in ipairs(excel_data.length) do
        local speed = excel_data.speed[i]
        if not speed or not length or length == 0 then
            break
        end
        Api.UI.SetWidth(ib_liang, length)
        local x = (w - length) * 0.5
        Api.UI.SetPositionX(ib_liang, x)
        ::restart::
        local id = Api.Task.AddEvent(StartTouchPoint, speed)
        local ok, ret = Api.Task.Wait(id)
        -- pprint('ok,ret',ok,ret)
        if ok and ret >= x and ret + ww <= x + length then
            currentGv = currentGv + onceGv
            Api.UI.SetText(lb_percent, currentGv .. '%')
            Api.Scene.SetImageFillAmount(gg1, currentGv / 100)
            Api.Scene.SetImageFillAmount(gg2, currentGv / 100)
            Api.PlayEffect('/res/effect/ui/ef_ui_alchemy_thermometer_03.assetbundles', {Parent = ib_back, Pos = {x = 18.5, y = -226.5, z = -200}})
            if i ~= excel_data.times then
                Api.StopSound(SoundID)
                Api.PlayEffect('/res/effect/ui/ef_ui_addfire_success.assetbundles', eff)
                Api.PlaySound('/res/sound/static/uisound/sd_youxiwiner.assetbundles')
                Api.Task.Sleep(1)
                SoundID = Api.PlaySound('/res/sound/static/uisound/sd_jindutiao.assetbundles', true)
            end
        else
            Api.StopSound(SoundID)
            Api.PlayEffect('/res/effect/ui/ef_ui_addfire_failure.assetbundles', eff)
            Api.PlaySound('/res/sound/static/uisound/sd_youxifail.assetbundles')
            Api.Task.Sleep(1)
            SoundID = Api.PlaySound('/res/sound/static/uisound/sd_jindutiao.assetbundles', true)
            goto restart
        end
    end
    Api.StopSound(SoundID)
    Api.PlayEffect('/res/effect/ui/ef_ui_alchemy_success.assetbundles', eff)
    Api.PlaySound('/res/sound/static/uisound/sd_youxiwiner.assetbundles')
    Api.Task.Sleep(1.5)
end

function clean()
    Api.UI.Close(ui)
end
