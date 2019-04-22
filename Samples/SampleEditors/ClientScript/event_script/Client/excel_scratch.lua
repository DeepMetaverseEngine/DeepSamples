function main(id, src_indexs)
    --pprint(id, src_indexs)
    local ele = Api.GetExcelByEventKey('scratch.' .. id)
    assert(ele)

    Api.PlayRippleEffect()
    Api.UI.CloseAll()
    ui = Api.UI.Open('xml/hud/ui_hud_scratch.gui.xml')
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    Api.UI.SetBackground(ui, 0.5)

    local cvs_cardlist = Api.UI.FindChild(ui, 'cvs_cardlist')
    local cvs_cardTeamplate = Api.UI.FindChild(ui, 'cvs_card')
    local w, h = Api.UI.GetSize(cvs_cardlist)
    local ww, hh = Api.UI.GetSize(cvs_cardTeamplate)
    local card_y = (h - hh) * 0.5
    local cvs_card = cvs_cardTeamplate
    local all_card = {}
    local eraser_nodes = {}
    local is_stoping
    local function OnSuccess(index, card)
        is_stoping = true
        Api.Task.PlayEffect('/res/effect/ui/ef_ui_scratchcard_open.assetbundles', {Parent = card, Pos = {x = 120, y = -170, z = -200}})
        Api.Task.PlayEffect('/res/effect/ui/ef_ui_scratchcard_hint.assetbundles', {Parent = card, Pos = {x = 120, y = -170, z = -200}})
        Api.PlaySound('/res/sound/static/uisound/sd_gamewin.assetbundles')

        
        Api.Task.Wait()
        Api.Task.StopEvent(ID, true, index)
    end

    for _, i in ipairs(src_indexs) do
        local v = ele.card[i]
        assert(not string.IsNullOrEmpty(v))
        if not cvs_card then
            cvs_card = Api.UI.Clone(cvs_cardTeamplate)
        end
        Api.UI.SetPositionY(cvs_card, card_y)
        local ib_card = Api.UI.FindChild(cvs_card, 'ib_card')
        local ib_bg = Api.UI.FindChild(cvs_card, 'ib_bg')
        local current_card = cvs_card
        Api.Listen.AddPeriodicSec(
            2,
            function()
                if is_stoping then
                    return
                end
                local percent = Api.UI.GetEraserPercent(ib_bg)
                if percent > 0 then
                    --其他的不能刮了
                    for i, v in ipairs(eraser_nodes) do
                        if v ~= ib_bg then
                            Api.UI.StopEraserMode(v)
                        end
                    end
                end
                if percent >= 0.6 then
                    Api.SetEventOutput(ID, i)
                    Api.UI.SetVisible(ib_bg, false)
                    Api.Task.AddEventTo(ID, OnSuccess, i, current_card)
                end
            end
        )
        Api.UI.StartEraserMode(ib_bg, 100)
        table.insert(eraser_nodes, ib_bg)
        Api.UI.SetEnable(ib_bg, true)
        Api.UI.SetImage(ib_card, v)
        table.insert(all_card, cvs_card)
        Api.UI.SetVisible(cvs_card, true)
        cvs_card = nil
    end
    Api.UI.SetNodesToCenterHorizontal(w, ww, true, all_card)
    Api.Task.WaitAlways()
end

function clean()
    Api.UI.Close(ui)
    Api.PlayRippleEffect()
end
