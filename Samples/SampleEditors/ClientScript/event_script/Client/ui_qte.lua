function clean()
    Api.SetTimeScale(1)
    Api.UI.Close(ui)
    Api.SetEnableBlurEffect({Enable = false})

    if CurrentBGM then
        Api.ChangeBGM(CurrentBGM)
    elseif SoundID then
        Api.StopSound(SoundID)
    end
end

function main(excel_data, showTreasure, pluslife)
    Api.UI.CloseAll()
    ui = Api.UI.Open('xml/hud/ui_hud_qte.gui.xml', UIShowType.Cover)
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    local btn_anniu = Api.UI.FindChild(ui, 'btn_anniu')
    local gg_jindu1 = Api.UI.FindChild(ui, 'gg_jindu1')
    local cvs_qtetap = Api.UI.FindChild(ui, 'cvs_qtetap')
    local cvs_qtetop = Api.UI.FindChild(ui, 'cvs_qtetop')
    local cvs_life1 = Api.UI.FindChild(ui, 'cvs_life1')
    local cvs_lunpan = Api.UI.FindChild(ui, 'cvs_lunpan')
    local cvs_zhizhen = Api.UI.FindChild(ui, 'cvs_zhizhen')
    local cvs_zhuan1 = Api.UI.FindChild(ui, 'cvs_zhuan1')
    local ib_event = Api.UI.FindChild(ui, 'ib_event')

    Api.PlayEffect('/res/effect/ui/ef_ui_treasure_click.assetbundles', {Parent = btn_anniu, Pos = {x=51,y=-45,z = -200}})
    if showTreasure then
        Api.UI.SetBackground(ui, 0.5)
        SoundID = Api.PlaySound('/res/sound/static/uisound/sd_zhuanpan.assetbundles', true)
        Api.UI.SetImage(ib_event, '#static/TL_hud/output/TL_hud.xml|TL_hud|240')
    else
        CurrentBGM = Api.GetCurrentBGM()
        Api.ChangeBGM('/res/sound/dynamic/bgm/event_xunma.assetbundles')
        Api.UI.SetImage(ib_event, '#static/TL_hud/output/TL_hud.xml|TL_hud|73')
    end

    local ib_ms = Api.UI.FindChild(ui, 'ib_ms')
    local life_w = Api.UI.GetSize(cvs_life1)
    local w, h = Api.UI.GetSize(cvs_qtetop)
    local ww, hh = Api.UI.GetSize(btn_anniu)
    local www, hhh = Api.UI.GetSize(ib_ms)
    local rTreasure = Api.UI.GetSize(cvs_lunpan)
    rTreasure = rTreasure * 0.5
    local treasureStartX, treasureStartY = Api.UI.GetPosition(cvs_lunpan)
    treasureStartX, treasureStartY = treasureStartX + rTreasure, treasureStartY + rTreasure
    local off_cpjx, off_cpjy = (-www) * 0.5, -hhh - 10

    local pointTreasure = 0
    local pointTreasure2 = 0

    local pausepointTreasure
    local timeScale = 1
    local speed = math.random(15, 25)
    showTreasure = showTreasure or false
    Api.UI.SetVisible(cvs_lunpan, showTreasure)
    if showTreasure then
        Api.Listen.AddPeriodicSec(
            0.03,
            function()
                if pausepointTreasure then
                    return
                end

                pointTreasure = pointTreasure - speed * timeScale
                pointTreasure2 = pointTreasure2 + 5 * timeScale

                Api.UI.SetRotation(cvs_zhizhen, 0, 0, pointTreasure)
                Api.UI.SetRotation(cvs_zhuan1, 0, 0, pointTreasure2)
            end
        )
    end

    Api.UI.SetGaugeFillMode(gg_jindu1, FillMethod.Radial360, Origin360.Top, true)
    Api.UI.SetGaugeMinMax(gg_jindu1, 0, 100)
    Api.UI.SetVisible(cvs_qtetap, false)
    local lifes
    if pluslife then
        lifes = 0
    else
        lifes = excel_data.life
    end

    local node_p = cvs_life1
    local life_nodes = {}
    local p = Api.UI.GetPositionX(cvs_life1)
    for i = 1, excel_data.life do
        if not node_p then
            node_p = Api.UI.Clone(cvs_life1)
            p = p + 25 + life_w
            Api.UI.SetPositionX(node_p, p)
        end
        table.insert(life_nodes, node_p)
        if pluslife then
            Api.UI.SetEnable(node_p, false)
        end
        node_p = nil
    end

    local lastGaugeValue
    local function StartTouchPoint(x, y, duration)
        local function CountDown(sec)
            local start_time = os.clock()
            local periodSec = 0.03
            local once = 100 / (sec / periodSec)
            Api.Listen.AddPeriodicSec(
                periodSec,
                function()
                    local off = os.clock() - start_time
                    lastGaugeValue = math.floor(off * 100 / sec)
                    Api.UI.SetGaugeValue(gg_jindu1, math.min(lastGaugeValue, 100))
                end
            )
            Api.Task.Sleep(sec)
            lastGaugeValue = 100
            Api.UI.SetGaugeValue(gg_jindu1, 100)
        end

        timeScale = 0.005
        if showTreasure then
            Api.StopSound(SoundID)
            SoundID = Api.PlaySound('/res/sound/static/uisound/sd_zhuanpan2.assetbundles', true)
        end
        Api.SetTimeScale(0.2)
        Api.SetEnableBlurEffect({Enable = true, Strength = 1.5})
        Api.UI.SetVisible(cvs_qtetap, true)
        Api.UI.SetPosition(cvs_qtetap, x, y)
        Api.UI.SetGaugeValue(gg_jindu1, 0)
        local id1 = Api.Task.AddEvent(CountDown, duration)
        local id2 = Api.UI.Listen.TouchClick(btn_anniu)
        local ok, successID = Api.Task.WaitSelect(id1, id2)

        Api.UI.SetVisible(cvs_qtetap, false)
        if successID ~= id2 and not pluslife then
            lifes = lifes - 1
        elseif successID == id2 and pluslife then
            lifes = lifes + 1
        end
        timeScale = 1
        Api.SetTimeScale(1)
        if showTreasure then
            Api.StopSound(SoundID)
            SoundID = Api.PlaySound('/res/sound/static/uisound/sd_zhuanpan.assetbundles', true)
        end
        Api.SetEnableBlurEffect({Enable = false})
        pausepointTreasure = false
    end

    for i, v in ipairs(excel_data.duration) do
        if (not pluslife and lifes == 0) or (pluslife and lifes >= excel_data.life) then
            break
        end
        ::restart::
        local lastLifes = lifes
        Api.Task.Sleep(excel_data.span[i] / 1000)
        local x, y
        if showTreasure then
            local d = math.rad(-pointTreasure - 90)
            x, y = math.cos(d) * rTreasure + treasureStartX, math.sin(d) * rTreasure + treasureStartY
        else
            x, y = math.random(0, w - ww), math.random(0, h - hh)
        end
        local id = Api.Task.AddEvent(StartTouchPoint, x, y, excel_data.duration[i] / 1000)
        Api.Task.Wait(id)
        local indexAnim = math.floor(lastGaugeValue / 25)
        if not pluslife then
            if lastLifes > lifes then
                Api.UI.SetEnable(life_nodes[lastLifes], false)
                indexAnim = 4
            else
                indexAnim = math.min(3, indexAnim)
                Api.PlayEffect('/res/effect/ui/ef_ui_treasure_dot.assetbundles', {Parent = life_nodes[lastLifes], Pos = {x=24,y=15,z = -200}})
            end
        else
            if lastLifes == lifes then
                indexAnim = 4
            else
                indexAnim = math.min(3, indexAnim)
                Api.UI.SetEnable(life_nodes[lifes], true)
                Api.PlayEffect('/res/effect/ui/ef_ui_treasure_dot.assetbundles', {Parent = life_nodes[lifes], Pos = {x=24,y=15,z = -200}})
            end
        end
        Api.UI.SetPosition(ib_ms, x + off_cpjx, y + off_cpjy - 30)
        Api.Task.AddEvent(
            function()
                Api.UI.SetVisible(ib_ms, true)
                Api.PlaySound('/res/sound/static/uisound/sd_youxiwiner.assetbundles')
                Api.Task.Wait(Api.UI.Task.PlayCPJOnce(ib_ms, indexAnim))
                Api.UI.SetVisible(ib_ms, false)
            end
        )
        if pluslife and lastLifes == lifes then
            goto restart
        end
    end
    pausepointTreasure = true
    Api.Task.Sleep(1)
    local eff = {Pos = {x = 0, y = 200, z = -333}, UILayer = true}
    if (not pluslife and lifes > 0) or (pluslife and lifes >= excel_data.life) then
        if showTreasure then
            Api.PlayEffect('/res/effect/ui/ef_ui_treasure_success.assetbundles', eff)
            Api.PlaySound('/res/sound/static/uisound/sd_youxiwiner.assetbundles')
        else
            Api.PlayEffect('/res/effect/ui/ef_ui_tame_success.assetbundles', eff)
            Api.PlaySound('/res/sound/static/uisound/sd_youxiwiner.assetbundles')
        end
        return true, lifes
    else
        if showTreasure then
            Api.PlayEffect('/res/effect/ui/ef_ui_treasure_failure.assetbundles', eff)
            Api.PlaySound('/res/sound/static/uisound/sd_youxifail.assetbundles')
        else
            Api.PlayEffect('/res/effect/ui/ef_ui_tame_failure.assetbundles', eff)
            Api.PlaySound('/res/sound/static/uisound/sd_youxifail.assetbundles')
        end
        return false, 'lifes == 0'
    end
end
