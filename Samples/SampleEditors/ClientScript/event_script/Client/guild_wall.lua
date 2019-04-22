local RedForce = 2
local BlueForce = 3
local ActorForce
local ActorUUID
local ActorGuildUUID
local MapID
local order_selects = {}
local order_uis = {}
local battle_infos
local MaxRunningSec
local IsGameOver
local function GetEnemyForce(force)
    return force == RedForce and BlueForce or RedForce
end

local function ResultShow(title, members)
    print('client result show')
    IsGameOver = true
    result_ui = Api.UI.Open('xml/guild/ui_guild_fortfiedresult.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local cid = Api.GetCurrentEventID()
    local ele_events = {}
    Api.UI.Listen.MenuExit(
        result_ui,
        function()
            Api.Task.StopEvent(cid, false, 'MenuExit')
        end
    )
    -- local lb_title = Api.UI.FindChild(result_ui, 'lb_title')
    local cvs_onenfo = Api.UI.FindChild(result_ui, 'cvs_onenfo')
    local cvs_myinfo = Api.UI.FindChild(result_ui, 'cvs_myinfo')
    local ib_win = Api.UI.FindChild(result_ui, 'ib_win')
    local ib_defeat = Api.UI.FindChild(result_ui, 'ib_defeat')
    Api.UI.SetVisible(ib_win, title == 'ib_win')
    Api.UI.SetVisible(ib_defeat, title == 'ib_defeat')
    -- Api.UI.SetText(lb_title, title)
    Api.UI.SetVisible(cvs_onenfo, false)
    local sp_list = Api.UI.FindChild(result_ui, 'sp_list')
    local bt_leave = Api.UI.FindChild(result_ui, 'bt_leave')

    local function FillElement(node, index, notlist)
        Api.UI.SetVisible(node, true)
        local data = members[index]
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_name = Api.UI.FindChild(node, 'lb_name')
        local lb_power = Api.UI.FindChild(node, 'lb_power')
        local lb_level = Api.UI.FindChild(node, 'lb_level')
        local lb_score = Api.UI.FindChild(node, 'lb_score')
        local ok, snap = Api.Task.Wait(Api.Task.GetRoleSnap(data.uuid))
        Api.UI.SetText(lb_rank, index)
        Api.UI.SetText(lb_name, snap.Name)
        Api.UI.SetText(lb_power, snap.FightPower)
        Api.UI.SetText(lb_level, snap.Level)
        Api.UI.SetText(lb_score, data.score)
        if not notlist and data.uuid == ActorUUID then
            FillElement(cvs_myinfo, index, true)
        end
    end
    Api.UI.SetScrollList(
        sp_list,
        cvs_onenfo,
        #members,
        function(node, index)
            Api.Task.AddEventTo(cid, FillElement, node, index)
        end
    )
    Api.UI.Listen.TouchClick(
        bt_leave,
        function()
            Api.ExitCurrentZone()
        end
    )

    local maxSec = 20
    Api.Task.Wait(
        Api.Listen.AddPeriodicSec(
            1,
            maxSec,
            function(total)
                local cur = maxSec - total
                Api.UI.SetText(bt_leave, Api.GetText(Constants.Text.guild_carriage_btnleave, math.floor(cur)))
            end
        )
    )
    Api.UI.Close(result_ui)
end

local function MembersShow(members1, members2)
    member_ui = Api.UI.Open('xml/guild/ui_guild_fortfiedmember.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local cid = Api.GetCurrentEventID()
    local ele_events = {}
    Api.UI.Listen.MenuExit(
        member_ui,
        function()
            Api.Task.StopEvent(cid, false, 'MenuExit')
        end
    )
    local sp_list = Api.UI.FindChild(member_ui, 'sp_list')
    local cvs_onenfo = Api.UI.FindChild(member_ui, 'cvs_onenfo')
    local tbt_main = Api.UI.FindChild(member_ui, 'tbt_main')
    local tbt_assis = Api.UI.FindChild(member_ui, 'tbt_assis')
    Api.UI.SetVisible(cvs_onenfo, false)
    local function RefreshList(members)
        local function FillElement(node, index)
            local data = members[index]
            local lb_rank = Api.UI.FindChild(node, 'lb_rank')
            local lb_name = Api.UI.FindChild(node, 'lb_name')
            local lb_power = Api.UI.FindChild(node, 'lb_power')
            local lb_level = Api.UI.FindChild(node, 'lb_level')
            local lb_totalscore = Api.UI.FindChild(node, 'lb_totalscore')

            local ok, snap = Api.Task.Wait(Api.Task.GetRoleSnap(data.uuid))
            Api.UI.SetText(lb_rank, index)
            Api.UI.SetText(lb_name, snap.Name)
            Api.UI.SetText(lb_power, snap.FightPower)
            Api.UI.SetText(lb_level, snap.Level)
            Api.UI.SetText(lb_totalscore, data.score)
            Api.Task.WaitAlways()
        end

        Api.UI.SetScrollList(
            sp_list,
            cvs_onenfo,
            #members,
            function(node, index)
                table.insert(ele_events, Api.Task.AddEventTo(cid, FillElement, node, index))
            end
        )
    end

    Api.UI.ToggleGroup(
        {tbt_main, tbt_assis},
        tbt_main,
        function(sender)
            if sender == tbt_main then
                RefreshList(members1)
            else
                RefreshList(members2)
            end
        end
    )
    RefreshList(members1)
    Api.Task.WaitAlways()
end
function main(info)
    main_zone = info.main
    MaxRunningSec = info.max_running_sec
    -- pprint('info ',info)
    Api.Task.WaitActorReady()
    Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )
    ui = Api.UI.Open('xml/guild/ui_guild_fortfiedhud.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    Api.UI.SetFrameEnable(ui, false)
    local lb_leftnum = Api.UI.FindChild(ui, 'lb_leftnum')
    local lb_rightnum = Api.UI.FindChild(ui, 'lb_rightnum')
    local lb_materialnum = Api.UI.FindChild(ui, 'lb_materialnum')
    local lb_time = Api.UI.FindChild(ui, 'lb_time')
    local btn_orderlist = Api.UI.FindChild(ui, 'btn_orderlist')
    local cvs_orderlist = Api.UI.FindChild(ui, 'cvs_orderlist')
    local btn_fbleave = Api.UI.FindChild(ui, 'btn_fbleave')

    local sp_orderlist = Api.UI.FindChild(ui, 'sp_orderlist')
    local cvs_ordername = Api.UI.FindChild(ui, 'cvs_ordername')
    local btn_leftcheck = Api.UI.FindChild(ui, 'btn_leftcheck')
    local btn_rightcheck = Api.UI.FindChild(ui, 'btn_rightcheck')
    local cvs_top = Api.UI.FindChild(ui, 'cvs_top')
    local cvs_right = Api.UI.FindChild(ui, 'cvs_right')
    Api.UI.SetScreenAnchor(cvs_top, 't')
    Api.UI.SetScreenAnchor(cvs_right, 'r')
    Api.Task.WaitActorReady()
    local zone_uuid = Api.GetZoneUUID()
    ActorForce = Api.GetActorForce()
    ActorUUID = Api.GetActorUUID()
    ActorGuildUUID = Api.GetActorGuildUUID()
    MapID = Api.GetMapTemplateID()
    local postion = Api.GetActorGuildPosition()
    local postion_config = Api.FindFirstExcelData('guild/guild.xlsx/guild_position', {position_id = postion})
    if ActorForce == RedForce then
        Api.UI.SetVisible(btn_rightcheck, false)
    else
        Api.UI.SetVisible(btn_leftcheck, false)
    end
    local function SyncWithServer(params, countdown_sec)
        Api.UI.SetText(lb_leftnum, params and params[RedForce].score or 0)
        Api.UI.SetText(lb_rightnum, params and params[BlueForce].score or 0)
        Api.UI.SetText(lb_materialnum, params and params[ActorForce].resource or 0)
        countdown_sec = countdown_sec or 0
        if countdown_sec < 0 then
            countdown_sec = 0
        end
        Api.UI.SetText(lb_time, Api.FormatSecondStr(countdown_sec))
    end

    local btn_order1 = Api.UI.FindChild(ui, 'btn_order1')
    local btn_order2 = Api.UI.FindChild(ui, 'btn_order2')
    local btn_add1 = Api.UI.FindChild(ui, 'btn_add1')
    local btn_add2 = Api.UI.FindChild(ui, 'btn_add2')
    local btn_closeorder = Api.UI.FindChild(ui, 'btn_closeorder')

    Api.UI.Listen.TouchClick(
        btn_add1,
        function()
            Api.UI.SetVisible(cvs_orderlist, true)
        end
    )
    Api.UI.Listen.TouchClick(
        btn_add2,
        function()
            Api.UI.SetVisible(cvs_orderlist, true)
        end
    )
    Api.UI.Listen.TouchClick(
        btn_closeorder,
        function()
            Api.UI.SetVisible(cvs_orderlist, false)
        end
    )
    local orders = Api.FindExcelData('guild/guild_fort.xlsx/guild_fort_order', {map_id = MapID})
    local orders_btn = {btn_order1, btn_order2}
    local orders_addbtn = {btn_add1, btn_add2}
    local orders_info = {}
    local function FillOrderList(order_list)
        local len = table.len(order_list)
        local show_list = {}
        for k, v in pairs(order_uis) do
            local tbt_select = Api.UI.FindChild(v, 'tbt_select')
            local o = order_list[k]
            Api.UI.SetChecked(tbt_select, o ~= nil)
            if o then
                table.insert(show_list, k)
            end
            if len >= 2 and not o then
                Api.UI.SetGray(v, true)
                Api.UI.SetEnableChildren(v, false)
            else
                Api.UI.SetGray(v, postion_config.fort_order ~= 1)
                Api.UI.SetEnableChildren(v, postion_config.fort_order == 1)
            end
        end
        local index = 1
        for index = 1, 2 do
            local btn = orders_btn[index]
            local btn_add = orders_addbtn[index]
            local id = show_list[index]
            if id then
                local o = Api.FindExcelData('guild/guild_fort.xlsx/guild_fort_order', id)
                local dynamic_o = order_list[id]
                orders_info[index] = dynamic_o
                Api.UI.SetText(btn, Api.GetText(o.order_name) .. '(' .. dynamic_o.count .. ')')
            else
                orders_info[index] = nil
            end
            Api.UI.SetVisible(btn, id ~= nil)
            Api.UI.SetVisible(btn_add, id == nil)
        end
    end

    local function SendOrder(o, checked)
        local data = {
            id = o.id,
            order_name = Api.GetText(o.order_name),
            order_position_left = o.order_position_left,
            order_position_right = o.order_position_right
        }
        Api.SendMessage('Zone', zone_uuid, 'guildwall-command', {data = data, checked = checked})
    end

    local function OrderEvent(tbt_select, o)
        Api.UI.Listen.TouchClick(
            tbt_select,
            function()
                SendOrder(o, Api.UI.IsChecked(tbt_select))
            end
        )
        Api.Task.WaitAlways()
    end

    local ordercount = #orders
    if ordercount % 2 ~= 0 then
        ordercount = ordercount + 1
    end

 
    Api.UI.SetVisible(cvs_ordername, false)
    Api.UI.SetScrollGrid(
        sp_orderlist,
        cvs_ordername,
        2,
        ordercount,
        function(node, index)
            Api.UI.SetGray(node, postion_config.fort_order ~= 1)
            Api.UI.SetEnableChildren(node, postion_config.fort_order == 1)
            if index > #orders then
                Api.UI.SetVisible(node, false)
            else
                local o = orders[index]
                order_uis[o.id] = node
                local lb_ordername = Api.UI.FindChild(node, 'lb_ordername')
                local tbt_select = Api.UI.FindChild(node, 'tbt_select')
                Api.Task.AddEventTo(ID, OrderEvent, tbt_select, o)
                Api.UI.SetText(lb_ordername, Api.GetText(o.order_name))
            end
        end
    )

    Api.UI.SetHScrollEnable(sp_orderlist, false)
    FillOrderList({})
    Api.UI.Listen.TouchClick(
        btn_orderlist,
        function()
            Api.UI.SetVisible(cvs_orderlist, not Api.UI.IsVisible(cvs_orderlist))
        end
    )
    Api.UI.Listen.TouchClick(
        btn_order1,
        function()
            local o = orders_info[1]
            if o then
                Api.SeekToFlag(MapID, o.flag)
            end
        end
    )
    Api.UI.Listen.TouchClick(
        btn_order2,
        function()
            local o = orders_info[2]
            if o then
                Api.SeekToFlag(MapID, o.flag)
            end
        end
    )
    Api.UI.Listen.TouchClick(
        btn_fbleave,
        function()
            Api.ExitCurrentZone(true)
        end
    )
    Api.UI.Listen.TouchClick(
        btn_leftcheck,
        function()
            Api.SendMessage('Zone', zone_uuid, 'guildwall-getmembers', {flag = 'left'})
        end
    )
    Api.UI.Listen.TouchClick(
        btn_rightcheck,
        function()
            Api.SendMessage('Zone', zone_uuid, 'guildwall-getmembers', {flag = 'right'})
        end
    )

    Api.Listen.Message(
        'guildwall-members',
        function(msgname, params)
            -- pprint('members ', params)
            local members = {}
            for _, v in pairs(params.players) do
                if params.flag == 'right' and v.force == BlueForce then
                    members[#members + 1] = v
                elseif params.flag == 'left' and v.force == RedForce then
                    members[#members + 1] = v
                end
            end
            local members1 = {}
            for _, v in pairs(params.other_players) do
                if params.flag == 'right' and v.force == BlueForce then
                    members1[#members1 + 1] = v
                elseif params.flag == 'left' and v.force == RedForce then
                    members1[#members1 + 1] = v
                end
            end
            if main_zone then
                Api.Task.AddEventTo(ID, MembersShow, members, members1)
            else
                Api.Task.AddEventTo(ID, MembersShow, members1, members)
            end
        end
    )
    local off_time
    Api.Listen.Message(
        'guild-wall-battleinfo',
        function(msgname, params)
            -- local txt = Api.FormatSecondStr(params.countdown_sec)
            -- pprint('params ----', params)
            battle_infos = params
            off_time = battle_infos.time - os.time()
            local orders = battle_infos.sync_data[ActorForce].orders
            if orders then
                orders_info = orders
                FillOrderList(orders)
            end
        end
    )

    Api.Listen.AddPeriodicSec(
        1,
        function()
            if not battle_infos then
                return
            end
            local running_sec = os.time() - battle_infos.start_time + off_time
            local countdown_sec = MaxRunningSec - running_sec
            if not IsGameOver then
                SyncWithServer(battle_infos.sync_data, countdown_sec)
            end
        end
    )
    Api.Listen.Message(
        'guildwall-result',
        function(ename, result)
            local title = result.win_force == ActorForce and 'ib_win' or 'ib_defeat'
            Api.Task.AddEventTo(ID, ResultShow, title, result.members)
        end
    )
    SyncWithServer(params)
    Api.Task.WaitAlways()
end

function clean()
    Api.UI.Close(ui)
    Api.UI.Close(member_ui)
    Api.UI.Close(result_ui)
end
