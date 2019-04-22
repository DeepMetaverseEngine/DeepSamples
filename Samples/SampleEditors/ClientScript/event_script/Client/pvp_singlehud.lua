local function ResultShow(gameover)
    local members = gameover.players
    IsGameOver = true
    local actoruuid = Api.GetActorUUID()
    result_ui = Api.UI.Open('xml/battleground_single/ui_battleground_single_account.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local ele_events = {}
    local cvs_rank = Api.UI.FindChild(result_ui, 'cvs_rank')
    -- local cvs_own = Api.UI.FindChild(result_ui, 'cvs_own')
    local lb_redresource = Api.UI.FindChild(result_ui, 'lb_redresource')
    local lb_blueresource = Api.UI.FindChild(result_ui, 'lb_blueresource')

    local sp_rank = Api.UI.FindChild(result_ui, 'sp_rank')
    local btn_level = Api.UI.FindChild(result_ui, 'btn_level')
    local cvs_tips = Api.UI.FindChild(result_ui, 'cvs_tips')
    local lb_num = Api.UI.FindChild(result_ui, 'lb_num')
    local lb_time = Api.UI.FindChild(result_ui, 'lb_time')
    local lb_over = Api.UI.FindChild(result_ui, 'lb_over')
    Api.UI.SetVisible(cvs_rank, false)
    Api.UI.Listen.TouchClick(
        btn_level,
        function()
            Api.ExitCurrentZone()
        end
    )
    local function FillElement(node, index, notlist)
        Api.UI.SetVisible(node, true)
        local data = members[index]
        pprint('index', index, data)
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_name = Api.UI.FindChild(node, 'lb_name')
        local lb_kill = Api.UI.FindChild(node, 'lb_kill')
        local lb_result = Api.UI.FindChild(node, 'lb_result')
        local lb_point = Api.UI.FindChild(node, 'lb_point')
        local txt = Api.GetText(Constants.Text.pvp_single_score, data.force_score)
        Api.UI.SetText(lb_point, txt)
        Api.UI.SetText(lb_rank, index)
        Api.UI.SetText(lb_name, data.name)
        Api.UI.SetText(lb_kill, data.kill_count)
        for i = 1, 3 do
            local ib_rankback = Api.UI.FindChild(node, 'ib_rank' .. i)
            if ib_rankback then
                Api.UI.SetVisible(ib_rankback, index == i)
            end
        end

        local txt_exploit = Api.GetText(Constants.Text.pvp_exploit, data.exploit)
        Api.UI.SetText(lb_result, txt_exploit)
        if not notlist and data.uuid == actoruuid then
            -- FillElement(cvs_own, index, true)
            local limit_today = Api.GetExcelConfig('pvp_value_limit')
            local str = data.today_exploit .. '/' .. limit_today
            Api.UI.SetVisible(lb_over, data.today_exploit >= limit_today)
            Api.UI.SetText(lb_num, str)
            local ib_own = Api.UI.FindChild(node, 'ib_own')
            Api.UI.SetVisible(ib_own,true)
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
        off_time = sdata.now_time-os.time()
    end
    -- 121 123  start_time 121  123 os.time()+off_time
    -- print('off_time',off_time)

    local ele = Api.FindFirstExcelData('event/common_pvp.xlsx/common_pvp', id)
    ui = Api.UI.Open('xml/hud/ui_hud_battleground_single.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
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

    local cvs_title = Api.UI.FindChild(ui, 'cvs_title')
    local cvs_rank = Api.UI.FindChild(ui, 'cvs_rank')
    local lb_betime = Api.UI.FindChild(ui, 'lb_betime')
    local sp_player = Api.UI.FindChild(ui, 'sp_player')
    local cvs_player = Api.UI.FindChild(ui, 'cvs_player')

    Api.UI.SetScreenAnchor(cvs_title, 't')
    Api.UI.SetScreenAnchor(cvs_rank, 'r')
    Api.UI.SetVisible(cvs_player, false)
    local str = Api.FormatSecondStr(sdata.left_time)
    Api.UI.SetText(lb_betime, str)
    local playernodes
    local actoruuid = Api.GetActorUUID()
    local function SyncMember()
        local function FillRankElement(data, node, index)
            local lb_rank = Api.UI.FindChild(node, 'lb_rank')
            local lb_player = Api.UI.FindChild(node, 'lb_player')
            local lb_point = Api.UI.FindChild(node, 'lb_point')
            local ib_pback = Api.UI.FindChild(node, 'ib_pback')
            Api.UI.SetText(lb_rank, index)
            Api.UI.SetText(lb_player, data.name)
            Api.UI.SetVisible(ib_pback, actoruuid == data.uuid)
            local txt = Api.GetText(Constants.Text.pvp_single_score, data.score)
            Api.UI.SetText(lb_point, txt)
        end
        while true do
            local lefttime = ele.battlefield_time - (os.time()  + off_time - sdata.start_time - ele.air_flag_time)
            if lefttime < 0 then
                lefttime = 0
            end
            local str = Api.FormatSecondStr(lefttime)
            Api.UI.SetText(lb_betime, str)
            local rank_players = {}
            for f, t in pairs(sdata.players) do
                for _, pinfo in ipairs(t) do
                    local score = sdata.scores[f] and sdata.scores[f].score or 0
                    table.insert(rank_players, {name = pinfo.name, uuid = pinfo.uuid, score = score})
                end
            end
            table.sort(
                rank_players,
                function(m1, m2)
                    return m1.score > m2.score
                end
            )

            if not playernodes then
                playernodes = {}
                Api.UI.SetScrollList(
                    sp_player,
                    cvs_player,
                    #rank_players,
                    function(node, index)
                        playernodes[index] = node
                        Api.UI.SetNodeTag(node, index)
                        FillRankElement(rank_players[index], node, index)
                    end
                )
            else
                for index, node in pairs(playernodes) do
                    if index == Api.UI.GetNodeTag(node) then
                        FillRankElement(rank_players[index], node, index)
                    end
                end
            end
            Api.Task.Sleep(1)
            sdata.left_time = sdata.left_time - 1
        end
    end
    Api.Listen.Message(
        'pvp_common.res',
        function(ename, newres)
            pprint('pvp_common.res', newres)
            for f, v in pairs(newres) do
                sdata.scores[f] = v
            end
        end
    )

    if not sdata.players then
        local success, ename, ret = Api.Task.Wait(Api.Listen.Message('pvp_common.players'))
        sdata.players = ret
    end

    pprint('sdata', sdata)
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
    pprint('gameover', gameover)
    ResultShow(gameover)
end

function clean()
    Api.UI.Close(ui)
    Api.UI.Close(result_ui)
end
