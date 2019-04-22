local RedForce = 2
local BlueForce = 3

local function ResultShow(gameover)
    local members = gameover.players
    IsGameOver = true
    local actoruuid = Api.GetActorUUID()
    result_ui = Api.UI.Open('xml/battleground/ui_battleground_account_nopoint.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local ele_events = {}
    local cvs_rank = Api.UI.FindChild(result_ui, 'cvs_rank')
    local cvs_own = Api.UI.FindChild(result_ui, 'cvs_own')
    local lb_redresource = Api.UI.FindChild(result_ui, 'lb_redresource')
    local lb_blueresource = Api.UI.FindChild(result_ui, 'lb_blueresource')

    local sp_rank = Api.UI.FindChild(result_ui, 'sp_rank')
    local btn_start1 = Api.UI.FindChild(result_ui, 'btn_start1')
    local cvs_tips = Api.UI.FindChild(result_ui, 'cvs_tips')
    local lb_num = Api.UI.FindChild(result_ui, 'lb_num')
    local lb_time = Api.UI.FindChild(result_ui, 'lb_time')
    local lb_over = Api.UI.FindChild(result_ui, 'lb_over')
    Api.UI.SetVisible(cvs_rank, false)
    Api.UI.Listen.TouchClick(
        btn_start1,
        function()
            Api.ExitCurrentZone()
        end
    )
    local function FillElement(node, index, notlist)
        Api.UI.SetVisible(node, true)
        local data = members[index]
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_name = Api.UI.FindChild(node, 'lb_name')
        local lb_kill = Api.UI.FindChild(node, 'lb_kill')
        local lb_result = Api.UI.FindChild(node, 'lb_result')
        local ib_blueback = Api.UI.FindChild(node, 'ib_blueback')
        local ib_redback = Api.UI.FindChild(node, 'ib_redback')

        Api.UI.SetText(lb_rank, index)
        Api.UI.SetText(lb_name, data.name)
        Api.UI.SetText(lb_kill, data.kill_count)
        for i = 1, 3 do
            local ib_rankback = Api.UI.FindChild(node, 'ib_rank' .. i)
            if ib_rankback then
                Api.UI.SetVisible(ib_rankback, index == i)
            end
        end
        if ib_blueback then
            Api.UI.SetVisible(ib_blueback, data.force == BlueForce)
        end
        if ib_redback then
            Api.UI.SetVisible(ib_redback, data.force == RedForce)
        end
        local txt_exploit = Api.GetText(Constants.Text.pvp_exploit, data.exploit)
        Api.UI.SetText(lb_result, txt_exploit)
        if not notlist and data.uuid == actoruuid then
            FillElement(cvs_own, index, true)
            local limit_today = Api.GetExcelConfig('pvp_value_limit')
            local str = data.today_exploit .. '/' .. limit_today
            Api.UI.SetVisible(lb_over, data.today_exploit >= limit_today)
            Api.UI.SetText(lb_num, str)
        end
    end
    Api.UI.SetScrollList(
        sp_rank,
        cvs_rank,
        #members,
        function(node, index)
            FillElement(node, index)
        end
    )

    local maxSec = 20
    Api.Task.Wait(
        Api.Listen.AddPeriodicSec(
            1,
            maxSec,
            function(total)
                local cur = maxSec - total
                Api.UI.SetText(lb_time, Api.GetText('common_sec', math.floor(cur)))
            end
        )
    )
    Api.UI.Close(result_ui)
end

function main(id, sdata)
    Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )
    if sdata.wait_time > 0 then
        Api.Task.AddEvent('Client/countdown_ui', sdata.wait_time, Constants.Text.pvp_countdown)
    end
    local off_time = 0
    if sdata.now_time then
        off_time = sdata.now_time - os.time()
    end

    local ele = Api.FindFirstExcelData('event/common_pvp.xlsx/common_pvp', id)
    ui = Api.UI.Open('xml/hud/ui_hud_battleground_loot.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    Api.UI.SetFrameEnable(ui, false)
    local btn_leave = Api.UI.FindChild(ui, 'btn_leave')
    Api.UI.SetScreenAnchor(btn_leave, 'r')
    Api.UI.Listen.TouchClick(
        btn_leave,
        function()
            Api.ExitCurrentZone(true)
        end
    )
    Api.Listen.Message(
        'ui_node',
        function(ename, param)
            local compname, text = unpack(string.split(param, ','))
            node_caches = node_caches or {}
            local lb = node_caches[compname] or Api.UI.FindChild(ui, compname)
            if lb then
                Api.UI.SetVisible(lb, true)
                Api.UI.SetText(lb, text)
                Api.UI.SetVisible(Api.UI.GetParent(lb), true)
            else
                print('not found', compname)
            end
        end
    )
    local playernodes = {}
    local rednodes = {}
    local bluenodes = {}
    local cvs_redplayer = Api.UI.FindChild(ui, 'cvs_redplayer')
    local cvs_blueplayer = Api.UI.FindChild(ui, 'cvs_blueplayer')
    local cvs_title = Api.UI.FindChild(ui, 'cvs_title')
    local lb_red = Api.UI.FindChild(ui, 'lb_red')
    local lb_blue = Api.UI.FindChild(ui, 'lb_blue')
    local gg_red = Api.UI.FindChild(ui, 'gg_red')
    local gg_blue = Api.UI.FindChild(ui, 'gg_blue')
    Api.UI.SetScreenAnchor(cvs_title, 't')
    Api.UI.SetScreenAnchor(cvs_redplayer, 'l')
    Api.UI.SetScreenAnchor(cvs_blueplayer, 'r')
    Api.UI.SetGaugeMinMax(gg_red, 0, ele.win_score)
    Api.UI.SetGaugeMinMax(gg_blue, 0, ele.win_score)
    local lb_betime = Api.UI.FindChild(ui, 'lb_betime')
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
    -- pprint('sdata', sdata)
    Api.UI.SetText(gg_red, 0 .. '/' .. ele.win_score)
    Api.UI.SetText(gg_blue, 0 .. '/' .. ele.win_score)
    Api.UI.SetGaugeValue(gg_red, 0)
    Api.UI.SetGaugeValue(gg_blue, 0)
    local str = Api.FormatSecondStr(sdata.left_time)
    Api.UI.SetText(lb_betime, str)
    Api.UI.SetVisible(cvs_redplayer, false)
    Api.UI.SetVisible(cvs_blueplayer, false)
    Api.Listen.ActorBirth(
        function()
            Api.Camera.FollowActor()
        end
    )
    local function SyncMember()
        while true do
            local lefttime = ele.battlefield_time - (os.time() + off_time - sdata.start_time - ele.air_flag_time)
            if lefttime < 0 then
                lefttime = 0
            end
            local str = Api.FormatSecondStr(lefttime)
            Api.UI.SetText(lb_betime, str)
            Api.UI.SetVisible(cvs_redplayer, true)
            Api.UI.SetVisible(cvs_blueplayer, true)
            for f, t in pairs(sdata.players) do
                local index = 1
                for _, pinfo in ipairs(t) do
                    local uuid = pinfo.uuid
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
                    if cvs_player then
                        Api.UI.SetVisible(cvs_player, false)
                    end
                end
            end
            Api.Task.Sleep(1)
            sdata.left_time = sdata.left_time - 1
        end
    end
    local function SyncRes()
        local redscore = math.min(sdata.scores[RedForce].score, ele.win_score)
        local bluescore = math.min(sdata.scores[BlueForce].score, ele.win_score)
        Api.UI.SetGaugeValue(gg_red, redscore)
        Api.UI.SetGaugeValue(gg_blue, bluescore)
        Api.UI.SetText(gg_red, redscore .. '/' .. ele.win_score)
        Api.UI.SetText(gg_blue, bluescore .. '/' .. ele.win_score)
    end
    sdata.scores[RedForce] = sdata.scores[RedForce] or {score = 0, kill_count = 0, force = RedForce}
    sdata.scores[BlueForce] = sdata.scores[BlueForce] or {score = 0, kill_count = 0, force = BlueForce}
    Api.Listen.Message(
        'pvp_common.res',
        function(ename, newres)
            pprint('pvp_common.res', newres)
            for f, v in pairs(newres) do
                sdata.scores[f] = v
            end
            SyncRes()
        end
    )

    SyncRes()
    if not sdata.players then
        local success, ename, ret = Api.Task.Wait(Api.Listen.Message('pvp_common.players'))
        sdata.players = ret
    end
    pprint('sdata', sdata)
    Api.UI.SetVisible(cvs_redplayer, true)
    Api.UI.SetVisible(cvs_blueplayer, true)
    Api.Task.AddEvent(SyncMember)
    Api.Listen.Message(
        'pvp_common.stepwin',
        function(ename, force)
            if force == RedForce then
                Api.Task.StartEvent('Client/show_ui', 'xml/battleground/ui_battleground_redwin.gui.xml', 2, true)
            elseif force == BlueForce then
                Api.Task.StartEvent('Client/show_ui', 'xml/battleground/ui_battleground_bluewin.gui.xml', 2, true)
            end
        end
    )
    local success, ename, gameover = Api.Task.Wait(Api.Listen.Message('pvp_common.gameover'))
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
    -- pprint('gameover',gameover)
    ResultShow(gameover)
end

function clean()
    Api.UI.Close(ui)
    Api.UI.Close(result_ui)
end
