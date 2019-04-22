local RedForce = 2
local BlueForce = 3

function main(force, res, countdownSec, firstCountdown, members)
    if firstCountdown > 0 then
        Api.Task.AddEvent('Client/countdown_ui', firstCountdown, Constants.Text.pvp_countdown)
    end
    ui = Api.UI.Open('xml/hud/ui_hud_battleground_4v4.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    Api.UI.SetFrameEnable(ui, false)
    local btn_leave = Api.UI.FindChild(ui, 'btn_leave')
    Api.UI.SetScreenAnchor(btn_leave, 'r')
    Api.UI.Listen.TouchClick(
        btn_leave,
        function()
            Api.ExitCurrentZone(true)
        end
    )
    local cvs_title = Api.UI.FindChild(ui, 'cvs_title')
    local cvs_redplayer = Api.UI.FindChild(ui, 'cvs_redplayer')
    local cvs_blueplayer = Api.UI.FindChild(ui, 'cvs_blueplayer')
    local lb_redwin = Api.UI.FindChild(ui, 'lb_redwin')
    local lb_bluewin = Api.UI.FindChild(ui, 'lb_bluewin')
    local lb_betime = Api.UI.FindChild(ui, 'lb_betime')

    Api.UI.SetScreenAnchor(cvs_title, 't')
    Api.UI.SetScreenAnchor(cvs_redplayer, 'l')
    Api.UI.SetScreenAnchor(cvs_blueplayer, 'r')

    Api.UI.SetVisible(cvs_redplayer, false)
    Api.UI.SetVisible(cvs_blueplayer, false)
    --force, nodes
    local playernodes = {}
    local rednodes = {}
    local bluenodes = {}
    for i = 1, 4 do
        rednodes['lb_player' .. i] = Api.UI.FindChild(cvs_redplayer, 'lb_redplayer' .. i)
        rednodes['gg_player' .. i] = Api.UI.FindChild(cvs_redplayer, 'gg_redplayer' .. i)
        rednodes['ib_break' .. i] = Api.UI.FindChild(cvs_redplayer, 'ib_redbreak' .. i)
        rednodes['cvs_player' .. i] = Api.UI.FindChild(cvs_redplayer, 'cvs_redplayer' .. i)
    end
    for i = 1, 4 do
        bluenodes['lb_player' .. i] = Api.UI.FindChild(cvs_blueplayer, 'lb_blueplayer' .. i)
        bluenodes['gg_player' .. i] = Api.UI.FindChild(cvs_blueplayer, 'gg_blueplayer' .. i)
        bluenodes['ib_break' .. i] = Api.UI.FindChild(cvs_blueplayer, 'ib_bluebreak' .. i)
        bluenodes['cvs_player' .. i] = Api.UI.FindChild(cvs_blueplayer, 'cvs_blueplayer' .. i)
    end
    playernodes[RedForce] = rednodes
    playernodes[BlueForce] = bluenodes

    local str = Api.FormatSecondStr(countdownSec)
    Api.UI.SetText(lb_betime, str)

    local function SyncRes()
        local forceres = res[RedForce]
        Api.UI.SetText(lb_redwin, Api.GetText(Constants.Text.pvp_wincount, forceres.cur, forceres.max))

        forceres = res[BlueForce]
        Api.UI.SetText(lb_bluewin, Api.GetText(Constants.Text.pvp_wincount, forceres.cur, forceres.max))
    end

    SyncRes()

    Api.Listen.Message(
        'pvp4.res',
        function(ename, newres)
            pprint('pvp4.res', newres)
            for f, v in pairs(newres) do
                res[f].cur = v
            end
            SyncRes()
        end
    )

    local function SyncMember()
        while true do
            if countdownSec < 0 then
                countdownSec = 0
            end
            local str = Api.FormatSecondStr(countdownSec)
            Api.UI.SetText(lb_betime, str)
            for f, t in pairs(members) do
                local index = 1
                for uuid, v in pairs(t) do
                    local lb_player = playernodes[f]['lb_player' .. index]
                    local gg_player = playernodes[f]['gg_player' .. index]
                    local ib_break = playernodes[f]['ib_break' .. index]
                    local cvs_player = playernodes[f]['cvs_player' .. index]
                    index = index + 1
                    local playerInfo = Api.GetPlayerInfo(uuid)
                    Api.UI.SetVisible(ib_break, playerInfo.State == 'Offline')
                    if playerInfo.State == 'Dead' or playerInfo.State == 'Offline' then
                        Api.UI.SetGray(lb_player, true)
                        Api.UI.SetGray(gg_player, true)
                        if playerInfo.State == 'Dead' then
                            Api.UI.SetGaugeMinMax(gg_player, 0, 100)
                            Api.UI.SetGaugeValue(gg_player, 0)
                        end
                    else
                        Api.UI.SetGray(lb_player, false)
                        Api.UI.SetGray(gg_player, false)
                        Api.UI.SetGaugeMinMax(gg_player, 0, playerInfo.MaxHp)
                        Api.UI.SetGaugeValue(gg_player, playerInfo.Hp)
                        Api.UI.SetText(lb_player, Api.GetShortText(playerInfo.Name, 3))
                    end
                    Api.UI.Listen.TouchClick(
                        cvs_player,
                        function()
                            if Api.IsActorDead() then
                                Api.Camera.FollowPlayer(uuid)
                            end
                        end
                    )
                end
                for i = index, 4 do
                    local cvs_player = playernodes[f]['cvs_player' .. i]
                    Api.UI.SetVisible(cvs_player, false)
                end
            end
            Api.Task.Sleep(1)
            countdownSec = countdownSec - 1
        end
    end

    if not members then
        local success, ename, syncmembers = Api.Task.Wait(Api.Listen.Message('pvp4.members'))
        members = syncmembers
    end

    Api.Listen.ActorBirth(
        function()
            Api.Camera.FollowActor()
        end
    )
    Api.UI.SetVisible(cvs_redplayer, true)
    Api.UI.SetVisible(cvs_blueplayer, true)
    Api.Task.AddEvent(SyncMember)

    Api.Listen.Message(
        'pvp4.win',
        function(ename, force)
            if force == RedForce then
                Api.Task.StartEvent('Client/show_ui', 'xml/battleground/ui_battleground_redwin.gui.xml', 2, true)
            elseif force == BlueForce then
                Api.Task.StartEvent('Client/show_ui', 'xml/battleground/ui_battleground_bluewin.gui.xml', 2, true)
            end
        end
    )

    local success, ename, gameover = Api.Task.Wait(Api.Listen.Message('pvp4.gameover'))
    pprint('gameover ', gameover)
    local eff_eid
    if gameover.force == RedForce then
        --红方胜
        eff_eid = Api.PlayEffect('/res/effect/ui/ef_ui_victory_red.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
    elseif gameover.force == BlueForce then
        --蓝方胜
        eff_eid = Api.PlayEffect('/res/effect/ui/ef_ui_victory_blue.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
    else
        --平局
        eff_eid = Api.PlayEffect('/res/effect/ui/ef_ui_draw.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
    end
    Api.Task.Sleep(2.5)

    local function FillPlayerResult(node, index, data)
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_point = Api.UI.FindChild(node, 'lb_point')
        local lb_name = Api.UI.FindChild(node, 'lb_name')
        local lb_kill = Api.UI.FindChild(node, 'lb_kill')
        local lb_result = Api.UI.FindChild(node, 'lb_result')
        local ib_blueback = Api.UI.FindChild(node, 'ib_blueback')
        local ib_redback = Api.UI.FindChild(node, 'ib_redback')
        local ib_rank1 = Api.UI.FindChild(node, 'ib_rank1')
        local ib_rank2 = Api.UI.FindChild(node, 'ib_rank2')
        local ib_rank3 = Api.UI.FindChild(node, 'ib_rank3')

        Api.UI.SetVisible(ib_rank1, index == 1)
        Api.UI.SetVisible(ib_rank2, index == 2)
        Api.UI.SetVisible(ib_rank3, index == 3)
        if ib_blueback then
            Api.UI.SetVisible(ib_blueback, data.force == BlueForce)
        end
        if ib_redback then
            Api.UI.SetVisible(ib_redback, data.force == RedForce)
        end

        Api.UI.SetText(lb_rank, tostring(index))
        Api.UI.SetText(lb_point, tostring(data.score))
        Api.UI.SetText(lb_name, tostring(data.name))
        Api.UI.SetText(lb_kill, tostring(data.killed))
        local txt_exploit = Api.GetText(Constants.Text.pvp_exploit, data.exploit)
        Api.UI.SetText(lb_result, txt_exploit)
    end

    --结算界面
    ui_result = Api.UI.Open('xml/battleground/ui_battleground_account.gui.xml', {Layer = 'MessageBox'})
    Api.UI.Listen.MenuExit(
        ui_result,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    local sp_rank = Api.UI.FindChild(ui_result, 'sp_rank')
    local cvs_rank = Api.UI.FindChild(ui_result, 'cvs_rank')
    local cvs_own = Api.UI.FindChild(ui_result, 'cvs_own')
    local lb_time = Api.UI.FindChild(ui_result, 'lb_time')
    local btn_start1 = Api.UI.FindChild(ui_result, 'btn_start1')
    local lb_redresource = Api.UI.FindChild(ui_result, 'lb_redresource')
    local lb_blueresource = Api.UI.FindChild(ui_result, 'lb_blueresource')
    local lb_num = Api.UI.FindChild(ui_result, 'lb_num')
    local lb_title = Api.UI.FindChild(ui_result, 'lb_title')
    local lb_over = Api.UI.FindChild(ui_result, 'lb_over')
    if gameover.force == RedForce then
        Api.UI.SetText(lb_title, Constants.Text.pvp_redwin)
    elseif gameover.force == BlueForce then
        Api.UI.SetText(lb_title, Constants.Text.pvp_bluewin)
    else
        Api.UI.SetText(lb_title, Constants.Text.pvp_drawwin)
    end
    Api.UI.Listen.TouchClick(
        btn_start1,
        function()
            Api.ExitCurrentZone()
        end
    )

    local forceres = res[RedForce]
    Api.UI.SetText(lb_redresource, Api.GetText(Constants.Text.pvp_winresult, gameover.scores[RedForce] or 0, forceres.max))

    forceres = res[BlueForce]
    Api.UI.SetText(lb_blueresource, Api.GetText(Constants.Text.pvp_winresult, gameover.scores[BlueForce] or 0, forceres.max))

    Api.UI.SetVisible(cvs_rank, false)
    local actoruuid = Api.GetActorUUID()
    local limit_today = Api.GetExcelConfig('pvp_value_limit')
    for i, v in ipairs(gameover.players) do
        if v.uuid == actoruuid then
            FillPlayerResult(cvs_own, i, v)
            local str = v.today_exploit .. '/' .. limit_today
            Api.UI.SetVisible(lb_over, v.today_exploit >= limit_today)
            Api.UI.SetText(lb_num, str)
        end
    end
    Api.UI.SetScrollList(
        sp_rank,
        cvs_rank,
        #gameover.players,
        function(node, index)
            FillPlayerResult(node, index, gameover.players[index])
        end
    )
    local maxSec = 20
    Api.UI.SetText(lb_time, Api.FormatLangSecond(maxSec))
    --结算界面倒计时
    Api.Task.Wait(
        Api.Listen.AddPeriodicSec(
            1,
            maxSec,
            function(total)
                local cur = maxSec - total
                Api.UI.SetText(lb_time, Api.FormatLangSecond(cur))
            end
        )
    )
end

function clean()
    Api.UI.Close(ui)
    Api.UI.Close(ui_result)
    Api.Camera.FollowActor()
end
