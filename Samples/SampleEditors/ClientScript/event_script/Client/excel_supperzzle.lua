function main(id, src_indexs)
    local ele = Api.GetExcelByEventKey('supperzzle.' .. id)
    assert(ele)
    Api.PlayRippleEffect()
    Api.UI.CloseAll()
    ui = Api.UI.Open('xml/hud/ui_hud_supperzzle.gui.xml')
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    Api.UI.SetBackground(ui, 0.5)

    local cvs_cardlist = Api.UI.FindChild(ui, 'cvs_cardlist')
    local cvs_cardTeamplate = Api.UI.FindChild(ui, 'cvs_card')
    local btn_go = Api.UI.FindChild(ui, 'btn_go')
    local cvs_supperzzle = Api.UI.FindChild(ui, 'cvs_supperzzle')
    local w, h = Api.UI.GetSize(cvs_cardlist)
    local ww, hh = Api.UI.GetSize(cvs_cardTeamplate)
    local centerx, centery = (w - ww) * 0.5, (h - hh) * 0.5
    local cvs_card = cvs_cardTeamplate
    local all_cards = {}
    local pairs_count = #src_indexs / 2
    --按正常顺序先
    for i = 1, #src_indexs do
        local cardID = (i - 1) % pairs_count + 1
        local v = ele.card[cardID]
        assert(not string.IsNullOrEmpty(v))
        if not cvs_card then
            cvs_card = Api.UI.Clone(cvs_cardTeamplate)
        end
        local ib_bg = Api.UI.FindChild(cvs_card, 'ib_bg')
        local ib_card = Api.UI.FindChild(cvs_card, 'ib_card')
        Api.UI.SetImage(ib_card, v)
        Api.UI.SetVisible(ib_bg, false)
        Api.UI.SetVisible(ib_card, true)
        Api.UI.SetVisible(cvs_card, true)
        table.insert(all_cards, {id = cardID, cvs_card = cvs_card, ib_card = ib_card, ib_bg = ib_bg})
        cvs_card = nil
    end

    local function RotationEffect(v, front, speed)
        v.lastRY = 0
        local r90card = front and v.ib_bg or v.ib_card
        local r180card = front and v.ib_card or v.ib_bg
        Api.UI.SetEnable(v.cvs_card, false)
        Api.UI.SetVisible(r90card, true)
        Api.UI.SetVisible(r180card, false)
        Api.UI.SetScale(r180card, -1, 1)
        Api.UI.SetRotationAndAdjustPos(r180card, 0, 90, 0)
        speed = speed or 10
        local ry = 0
        v.effect_eid =
            Api.Listen.AddPeriodicSec(
            0.03,
            function()
                -- 翻牌特效
                ry = ry or 0
                local step90 = Api.UI.IsVisible(r90card)
                if step90 then
                    ry = ry + speed
                    if ry >= 90 then
                        Api.UI.SetVisible(r90card, false)
                        Api.UI.SetVisible(r180card, true)
                        Api.UI.SetRotationAndAdjustPos(r180card, 0, ry, 0)
                    else
                        Api.UI.SetRotationAndAdjustPos(r90card, 0, ry, 0)
                    end
                else
                    ry = ry + speed
                    if ry >= 180 then
                        Api.UI.SetEnable(v.cvs_card, true)
                        Api.UI.SetRotation(r90card, 0, 0, 0)
                        Api.UI.SetPosition(r90card, 0, 0)
                        Api.UI.SetRotation(r180card, 0, 0, 0)
                        Api.UI.SetPosition(r180card, 0, 0)
                        Api.UI.SetScale(r180card, 1, 1)
                        Api.Task.StopEvent(v.effect_eid)
                    else
                        Api.UI.SetRotationAndAdjustPos(r180card, 0, ry, 0)
                    end
                end
            end
        )
        return v.effect_eid
    end

    Api.UI.SetEnable(btn_go, false)
    Api.UI.SetGray(btn_go, true)


    local off_w = (w - ww * ele.transverse - 40) / (ele.transverse - 1)
    local off_h = (h - hh * ele.longitudinal - 40) / (ele.longitudinal - 1)
    Api.UI.SetGridLayout(cvs_cardlist, Constraint.FixedColumnCount, ele.transverse, off_w, off_h, {left = 20, right = 20, top = 20, bottom = 20})
    Api.Task.Sleep(1.5)

               Api.PlaySound('/res/sound/static/uisound/sd_xipai.assetbundles')


    for _, v in ipairs(all_cards) do
        RotationEffect(v, false)
    end

    for _, v in ipairs(all_cards) do
        Api.Task.Wait(v.effect_eid)
    end

    --停止网格
    Api.UI.DisableGridLayout(cvs_cardlist)


    --移动到中心位置
    for _, v in ipairs(all_cards) do


        v.x, v.y = Api.UI.GetPosition(v.cvs_card)
        v.effect_eid = Api.UI.Task.MoveTo(v.cvs_card, 0.5, centerx, centery)
    end

    for _, v in ipairs(all_cards) do
        Api.Task.Wait(v.effect_eid)
    end
    Api.Task.Sleep(0.1)

    --散开到实际位置
    for _, v in ipairs(all_cards) do
        v.effect_eid = Api.UI.Task.MoveTo(v.cvs_card, 0.5, v.x, v.y)
    end
    for _, v in ipairs(all_cards) do
        Api.Task.Wait(v.effect_eid)
    end
    Api.Task.Sleep(0.5)
    --重启网格
    for i, v in ipairs(all_cards) do
        Api.UI.SetSiblingIndex(v.cvs_card, src_indexs[i] - 1)
    end
    Api.UI.SetGridLayout(cvs_cardlist, Constraint.FixedColumnCount, ele.transverse, off_w, off_h, {left = 20, right = 20, top = 20, bottom = 20})

    --监听点击
    local last_card_index
    local pass_count = 0
    for i, v in ipairs(all_cards) do
        Api.UI.Listen.TouchClick(
            v.cvs_card,
            function()
                Api.UI.SetVisible(v.ib_card, true)
                Api.UI.SetVisible(v.ib_bg, false)
                if last_card_index == i then
                    return 
                end
                Api.Task.AddEventTo(
                    ID,
                    function()
                        --翻开
                        Api.UI.SetEnableChildren(cvs_supperzzle, false)
                        Api.PlaySound('/res/sound/static/uisound/sd_fapai.assetbundles')
                        local eid = RotationEffect(v, true)
                        Api.Task.Wait(eid)
                        local cont = ele.continue[v.id] == 1
                        if not last_card_index then
                            if cont then
                                last_card_index = i
                            else
                                Api.Task.Sleep(0.3)
                                Api.Task.Wait(RotationEffect(v, false, 10))
                            end
                        else
                            local vother = all_cards[last_card_index]
                            if not cont or (vother and vother.id ~= v.id) then
                                last_card_index = nil
                                Api.Task.Sleep(0.3)
                                RotationEffect(v, false, 10)
                                if vother then
                                    RotationEffect(vother, false, 10)
                                end
                                Api.Task.Wait(v.effect_eid)
                                if vother then
                                    Api.Task.Wait(vother.effect_eid)
                                end
                            else
                                --成功
                                Api.UI.SetEnable(v.cvs_card, false)
                                if vother then
                                    Api.UI.SetEnable(vother.cvs_card, false)
                                end
                                Api.PlayEffect('/res/effect/ui/ef_ui_card_matching.assetbundles', {Parent = v.cvs_card, Pos = {z= -200, y = -95, x = 66}})
                                Api.PlayEffect('/res/effect/ui/ef_ui_card_matching.assetbundles', {Parent = vother.cvs_card, Pos = {z= -200, y = -95, x = 66}})
                                Api.PlaySound('/res/sound/static/uisound/sd_gamewin.assetbundles')

                                pass_count = pass_count + 1
                                if pass_count == ele.count then
                                    Api.UI.SetEnable(btn_go, true)
                                    Api.PlayEffect('/res/effect/ui/ef_ui_interface_frame.assetbundles', {Parent = btn_go, Pos = {x = 63, y = -28, z = -200}})
                                    Api.UI.SetGray(btn_go, false)
                                end
                                last_card_index = nil
                            end
                        end
                        Api.UI.SetEnableChildren(cvs_supperzzle, true)
                    end
                )
            end
        )
    end

    local effectplayed = false
    local function SuccessEffect()
        effectplayed = true
        Api.PlayEffect('/res/effect/ui/ef_ui_card_success.assetbundles', {UILayer = true, Pos = {x = 0, y = 200}})

        Api.Task.Sleep(1)
        Api.Task.StopEvent(ID, true)
        Api.SetEventOutput(ID, pass_count)


    end

    Api.UI.Listen.TouchClick(
        btn_go,
        function()
            if pass_count >= ele.count and not effectplayed then
                Api.Task.AddEventTo(ID, SuccessEffect)
            end
        end
    )
    Api.Task.WaitAlways()
end

function clean()
    Api.PlayRippleEffect()
    Api.UI.Close(ui)
end
