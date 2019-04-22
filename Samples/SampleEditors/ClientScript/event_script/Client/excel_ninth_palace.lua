function main(id)
    local ele = Api.GetExcelByEventKey('ninth_palace.' .. id)
    assert(ele)
    local blocks = 9
    if ele.row_number then
        blocks = ele.row_number * ele.column_number
    end

    local xml_def = {
        [9] = 'xml/hud/ui_hud_ninthpalace.gui.xml',
        [4] = 'xml/hud/ui_hud_fourpalace.gui.xml'
    }
    Api.UI.CloseAll()
    ui = Api.UI.Open(xml_def[blocks])
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    Api.UI.SetBackground(ui, 0.5)
    local cardsPos = {}
    local lastTouched
    local lockedNodes = 0
    local cardnodes = {}
    local srcpos = {}
    local ib_select = Api.UI.FindChild(ui, 'ib_select')

    local function OnSuccess()
        Api.PlayEffect('/res/effect/ui/ef_ui_jigsawpuzzle_success.assetbundles', {UILayer = true, Pos = {y = 200}})
        Api.Task.Sleep(2)
        Api.Task.StopEvent(ID)
    end

    local function CheckRight(cvs_card)
        local x, y = Api.UI.GetPosition(cvs_card)
        local src_pos = cardsPos[cvs_card]
        if math.abs(src_pos.x - x) < 0.0001 and math.abs(src_pos.y - y) < 0.0001 then
            lockedNodes = lockedNodes + 1
            Api.UI.SetGray(cvs_card, false)
            Api.UI.SetEnable(cvs_card, false)
            Api.PlayEffect('/res/effect/ui/ef_ui_jigsawpuzzle_right.assetbundles', {Parent = cvs_card, Pos = {x = 85, y = -85, z = -200}})
            Api.PlaySound('/res/sound/static/uisound/sd_gamewin.assetbundles')

            if lockedNodes == blocks then
                Api.Task.AddEventTo(ID, OnSuccess)
            end
        end
    end

    local function OnCardTouch(cvs_card)
        Api.PlaySound('/res/sound/static/uisound/sd_button.assetbundles')
        if lastTouched then
            Api.UI.SetVisible(ib_select, false)
            local x, y = Api.UI.GetPosition(lastTouched)
            local x1, y1 = Api.UI.GetPosition(cvs_card)
            Api.UI.SetPosition(cvs_card, x, y)
            CheckRight(cvs_card)
            Api.UI.SetPosition(lastTouched, x1, y1)
            CheckRight(lastTouched)
            lastTouched = nil
        else
            lastTouched = cvs_card
            local x, y = Api.UI.GetPosition(lastTouched)
            Api.UI.SetVisible(ib_select, true)
            Api.UI.SetPosition(ib_select, x, y)
        end
    end

    for i = 1, blocks do
        local cvs_card = Api.UI.FindChild(ui, 'cvs_card' .. i)
        Api.UI.SetImage(cvs_card, ele.icon[i])
        Api.UI.SetGray(cvs_card, true)
        local x, y = Api.UI.GetPosition(cvs_card)
        cardsPos[cvs_card] = {x = x, y = y}
        srcpos[i] = {x = x, y = y}
        cardnodes[i] = cvs_card
        Api.UI.Listen.TouchClick(
            cvs_card,
            function()
                OnCardTouch(cvs_card)
            end
        )
    end
    local upsetNodes
    for ii = 1, 10 do
        upsetNodes = Api.UpsetArray(cardnodes)
        local reset = false
        for i, v in ipairs(upsetNodes) do
            if v == cardnodes[i] then
                reset = true
            end
        end
        if not reset then
            break
        end
    end
    for i, v in ipairs(upsetNodes) do
        Api.UI.SetPosition(v, srcpos[i].x, srcpos[i].y)
    end

    Api.Task.WaitAlways()
end

function clean()
    Api.UI.Close(ui)
end
