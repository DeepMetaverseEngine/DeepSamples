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
    local showBtnEffect = false
    local showGuideTime = 2 --引导次数
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
        Pos = {z = -200}
    }
    local ok, id_gg = Api.Task.Wait(Api.Scene.Task.LoadGameObject('/res/effect/ui/ef_ui_alchemy_thermometer_04.assetbundles', info2))
    assert(ok)
    Api.Scene.SetAnchoredPosition(id_gg, {x = 0, y = 0})

    local gg1 = Api.Scene.FindChild(id_gg, 'Fill Area/Fill')
    local gg2 = Api.Scene.FindChild(id_gg, 'Fill Area/Fill (1)')
    Api.Scene.SetImageFillAmount(gg1, 0)
    Api.Scene.SetImageFillAmount(gg2, 0)
    local CurX,CurLength
    local scalex = 1
    local btnEffectId = 0
    local function ReleaseEffect()
        if btnEffectId then
             Api.RemovePlayEffect(btnEffectId)
             btnEffectId = nil
         end
         showBtnEffect = false
    end
    local function StartTouchPoint(speed)
        local lastPosX = 0
        local currentSpeed = speed
        local orgspeed = speed
        local slowspeed = 1.7
        local btnEffect = nil
        Api.UI.SetPositionX(ib_hua, 0)
        Api.Listen.AddPeriodicSec(
            0.03,
            function()
                lastPosX = lastPosX + currentSpeed
                
                if lastPosX >= w - ww then
                    lastPosX = w - ww
                    orgspeed = -speed
                    currentSpeed = -speed
                elseif lastPosX <= 0 then
                    orgspeed = speed
                    currentSpeed = speed
                end
                if showGuideTime > 0 then
                    if lastPosX >= CurX and lastPosX + ww <= CurX + CurLength then
                        if not showBtnEffect then
                           if btnEffect then
                             Api.SetPlayEffectActive(btnEffectId,true)
                           else
                             btnEffectId = Api.PlayEffect('/res/effect/ui/ef_ui_alchemy_range.assetbundles', {Parent = ib_liang,Scale = {x = scalex,y = 1, z= 1}})
                           end 
                           showBtnEffect = true
                        end
                        currentSpeed = orgspeed > 0 and slowspeed or -slowspeed
                    else
                        if showBtnEffect and btnEffectId then
                            local ret,aoe = Api.SetPlayEffectActive(btnEffectId,false)
                            if ret then
                                btnEffect = aoe
                            else 
                                Api.RemovePlayEffect(btnEffectId)
                            end
                            showBtnEffect = false
                        end
                        currentSpeed = orgspeed
                    end
                else
                   ReleaseEffect()
                end
                
                Api.UI.SetPositionX(ib_hua, lastPosX)

            end)
        if showGuideTime > 0 then
            local eid = Api.Guide.Listen.Touch(btn_liandan,{y = 0, right = true, force = false,type = 2})
            Api.Task.Wait(eid)
        else
            local id = Api.UI.Listen.PointerDown(btn_liandan)
            Api.Task.Wait(id)
        end

        -- print('lastposx', lastPosX)
        return true, lastPosX
    end
    SoundID = Api.PlaySound('/res/sound/static/uisound/sd_jindutiao.assetbundles', true)
    local onceGv = math.floor(100 / excel_data.times)
    local currentGv = 0
    local eff = {Parent = cvs_uieffect, Pos = {x = 0, y = 0, z = -500}}
    local _w, _h = Api.UI.GetSize(ib_liang)
    for i, length in ipairs(excel_data.length) do
        local speed = excel_data.speed[i]
        if not speed or not length or length == 0 then
            break
        end
        
        scalex = length*1.0/_w
        Api.UI.SetWidth(ib_liang, length)
        local x = (w - length) * 0.5
        CurX = x
        CurLength = length
        Api.UI.SetPositionX(ib_liang, x)
        ::restart::
        local id = Api.Task.AddEvent(StartTouchPoint, speed)
        local ok, ret = Api.Task.Wait(id)
        ReleaseEffect()
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

            if showGuideTime > 0 then
                showGuideTime = showGuideTime - 1
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
