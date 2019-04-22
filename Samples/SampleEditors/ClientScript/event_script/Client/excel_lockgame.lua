function main(id)
    local ele = Api.GetExcelByEventKey('lock.' .. id)
    Api.UI.CloseAll()
    ui = Api.UI.Open('xml/hud/ui_hud_openlock.gui.xml')
    Api.UI.SetBackground(ui, 0.5)
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    local cvs_zhizhen = Api.UI.FindChild(ui, 'cvs_zhizhen')
    local cvs_lunpan = Api.UI.FindChild(ui, 'cvs_lunpan')
    local ib_center = Api.UI.FindChild(ui, 'ib_center')
    local ib_lock = Api.UI.FindChild(ui, 'ib_lock')
    local cvs_qtetap = Api.UI.FindChild(ui, 'cvs_qtetap')
    local btn_qteopen = Api.UI.FindChild(ui, 'btn_qteopen')
    
    local w, h = Api.UI.GetSize(ib_lock)

    local rTreasure = Api.UI.GetSize(ib_center)
    rTreasure = rTreasure * 0.5 + 70
    local cvs_lunpan_w, cvs_lunpan_h = Api.UI.GetSize(cvs_lunpan)
    local treasureStartX, treasureStartY = 0, 0

    treasureStartX, treasureStartY = treasureStartX + cvs_lunpan_w * 0.5 + 3, treasureStartY + cvs_lunpan_h * 0.5 + 3
    local pointTreasure = 360
    local pausepointTreasure
    local timeScale = 1
    local speed = ele.rotation_speed
    speed = speed * 0.1
    local unlock_num = 0

    local angles = {}
    local nodes = {}
    local size_angle = 33
    local node = cvs_qtetap

    local function CheckIntersect(cur)
        local d1 = math.rad(-cur - 270)
        local x1, y1 = math.cos(d1) * rTreasure + treasureStartX, math.sin(d1) * rTreasure + treasureStartY
        for i, v in ipairs(angles) do
            local d2 = math.rad(-v - 270)
            local x2, y2 = math.cos(d2) * rTreasure + treasureStartX, math.sin(d2) * rTreasure + treasureStartY
            if Api.RectIntersect(x1, y1, w, h, x2, y2, w, h) then
                return i
            end
        end
    end

    for i = 1, ele.lock_num do
        local a
        for ii = 1, 10000 do
            a = Api.RandomInteger(0, 360)
            if not CheckIntersect(a) then
                break
            end
        end
        -- a = size_angle * (i-1)
        if not node then
            node = Api.UI.Clone(cvs_qtetap)
        end
        angles[i] = a
        local d = math.rad(-a - 270)
        local x, y = math.cos(d) * rTreasure + treasureStartX, math.sin(d) * rTreasure + treasureStartY
        Api.UI.SetPosition(node, x, y)
        Api.UI.SetVisible(node, true)
        Api.UI.SetSiblingIndex(node, 1)
        nodes[i] = node
        -- Api.UI.SetName(node, 'cvs_qtetap' .. i)
        node = nil
    end
    local rad_offset = math.rad(size_angle) * 0.5
    
    Api.UI.Listen.TouchClick(
        btn_qteopen,
        function()
            local index
            for i, v in ipairs(angles) do
                if Api.UI.IsVisible(nodes[i]) then
                    local d1 = math.rad(-v - 270)
                    local d2 = math.rad(-pointTreasure - 270)
                    if math.abs(d1 - d2) <= rad_offset then
                        index = i
                        break
                    end
                end
            end
            if index then
                unlock_num = unlock_num + 1
                local cvs_anchor = Api.UI.FindChild(nodes[index], 'cvs_anchor')
                local ib_lock = Api.UI.FindChild(nodes[index], 'ib_lock')
                Api.UI.SetEnable(btn_qteopen,false)
                Api.Task.AddEventTo(ID,function()
                    pausepointTreasure = true
                    local eff = {Parent = cvs_anchor, DisableToUnload = true, Pos = {x = 0, y = 0, z = -500}}
                    Api.PlayEffect('/res/effect/ui/ef_ui_broken.assetbundles', eff)
                    Api.Task.Sleep(1)
                    Api.UI.SetVisible(ib_lock,false)
                    Api.Task.Sleep(1)
                    Api.UI.SetVisible(nodes[index], false)
                    pausepointTreasure = false
                    if unlock_num == ele.lock_num then
                        Api.Task.StopEvent(ID, true)
                    else
                        Api.UI.SetEnable(btn_qteopen,true)
                    end
                end)
            end
        end
    )
    Api.Listen.AddPeriodicSec(
        0.03,
        function()
            if pausepointTreasure then
                return
            end
            pointTreasure = pointTreasure - speed * timeScale
            if pointTreasure <= 0 then
                pointTreasure = 360 + pointTreasure
            end
            Api.UI.SetRotation(cvs_zhizhen, 0, 0, pointTreasure)
        end
    )
    Api.Task.WaitAlways()
end

function clean()
    Api.UI.Close(ui)
end
