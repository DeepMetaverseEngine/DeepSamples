local RedForce = 2
local BlueForce = 3
local ActorForce
local ActorGuildUUID
local Car_End_Flag = 'carriagefinish_1'
local car_endx, car_endy
local member_ui
local ui
local result_ui
local function OnShowMemberInfo(members)
    member_ui = Api.UI.Open('xml/guild/ui_guild_dartbattledyninfo.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local cid = Api.GetCurrentEventID()
   
    local cvs_onenfo = Api.UI.FindChild(member_ui, 'cvs_onenfo')
    Api.UI.SetVisible(cvs_onenfo, false)
    local sp_list = Api.UI.FindChild(member_ui, 'sp_list')
    local bt_yes = Api.UI.FindChild(member_ui, 'bt_yes')

    local select_index
    local select_node

    local ele_events = {}
    -- pprint('members ',members)
    local function RefreshList()
        select_index = nil
        select_node = nil
        for _, v in ipairs(ele_events) do
            Api.Task.StopEvent(v)
        end
        local function FillElement(node, index)
            Api.UI.SetVisible(node, true)
            Api.UI.SetEnable(node, select_index ~= index)
            if select_index == index then
                select_node = node
            end
            local data = members[index]
            local ok, snap = Api.Task.Wait(Api.Task.GetRoleSnap(data.uuid))
            local lb_rank = Api.UI.FindChild(node, 'lb_rank')
            local lb_name = Api.UI.FindChild(node, 'lb_name')
            local lb_power = Api.UI.FindChild(node, 'lb_power')
            local lb_killscore = Api.UI.FindChild(node, 'lb_killscore')
            local lb_assiscore = Api.UI.FindChild(node, 'lb_assiscore')
            local lb_totalscore = Api.UI.FindChild(node, 'lb_totalscore')
            Api.UI.SetText(lb_rank, index)
            Api.UI.SetText(lb_name, snap.Name)
            Api.UI.SetText(lb_power, snap.FightPower)
            Api.UI.SetText(lb_killscore, data.kill_score)
            Api.UI.SetText(lb_assiscore, data.help_score)
            Api.UI.SetText(lb_totalscore, data.kill_score + data.help_score)
            Api.UI.Listen.TouchClick(
                node,
                function()
                    Api.UI.SetEnable(node, false)
                    if select_node then
                        Api.UI.SetEnable(select_node, true)
                    end
                    select_index = index
                    select_node = node
                    Api.UI.SetVisible(bt_yes, members[select_index].uuid ~= ActorUUID)
                    -- Api.UI.SetEnable(bt_yes, members[select_index].uuid ~= ActorUUID)
                    -- Api.UI.SetGray(bt_yes, members[select_index].uuid == ActorUUID)
                end
            )
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
    RefreshList()

    Api.UI.Listen.TouchClick(
        bt_yes,
        function()
            if not select_index then
                return
            end
            -- 踢人
            Api.SendMessage('Zone', Api.GetZoneUUID(), 'guild_carriage.kick_member', members[select_index].uuid)
        end
    )
    local last_count = #members
    Api.Listen.AddPeriodicSec(
        1,
        function()
            if last_count ~= #members then
                RefreshList()
            end
        end
    )
    Api.UI.SetVisible(bt_yes, false)
    -- Api.UI.SetEnable(bt_yes, false)
    -- Api.UI.SetGray(bt_yes, true)
    Api.Task.WaitAlways()
end

local function OnShowMemberClose()
    Api.UI.Close(member_ui)
end

local function OnShowGameOverResult(title, members)
    result_ui = Api.UI.Open('xml/guild/ui_guild_dartbattlejiesuan.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local cid = Api.GetCurrentEventID()
    Api.UI.Listen.MenuExit(
        result_ui,
        function()
            Api.Task.StopEvent(cid)
        end
    )
    -- local lb_title = Api.UI.FindChild(result_ui, 'lb_title')
    local cvs_onenfo = Api.UI.FindChild(result_ui, 'cvs_onenfo')
    local cvs_myinfo = Api.UI.FindChild(result_ui, 'cvs_myinfo')
    local ib_win = Api.UI.FindChild(result_ui, 'ib_win')
    local ib_defeat = Api.UI.FindChild(result_ui, 'ib_defeat')
    Api.UI.SetVisible(ib_win,title == 'ib_win')
    Api.UI.SetVisible(ib_defeat,title == 'ib_defeat')
    -- Api.UI.SetText(lb_title, title)
    Api.UI.SetVisible(cvs_onenfo, false)
    local sp_list = Api.UI.FindChild(result_ui, 'sp_list')
    local bt_leave = Api.UI.FindChild(result_ui, 'bt_leave')

    local function FillElement(node, index, notlist)
        Api.UI.SetVisible(node, true)
        local data = members[index]
        local ok, snap = Api.Task.Wait(Api.Task.GetRoleSnap(data.uuid))
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_name = Api.UI.FindChild(node, 'lb_name')
        local lb_power = Api.UI.FindChild(node, 'lb_power')
        local lb_killscore = Api.UI.FindChild(node, 'lb_killscore')
        local lb_assiscore = Api.UI.FindChild(node, 'lb_assiscore')
        local lb_totalscore = Api.UI.FindChild(node, 'lb_totalscore')
        Api.UI.SetText(lb_rank, index)
        Api.UI.SetText(lb_name, snap.Name)
        Api.UI.SetText(lb_power, snap.FightPower)
        Api.UI.SetText(lb_killscore, data.kill_score)
        Api.UI.SetText(lb_assiscore, data.help_score)
        Api.UI.SetText(lb_totalscore, data.kill_score + data.help_score)
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

function main(guild_map)
    Api.Task.WaitActorReady()
     Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )
    
    local desc_ui = Api.UI.Open('xml/guild/ui_guild_darttips.gui.xml')
    if desc_ui then
       Api.UI.EnableTouchFrameClose(desc_ui, true)
    end
    ui = Api.UI.Open('xml/guild/ui_guild_dartbattleinfo.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    local cvs_cloumninfo = Api.UI.FindChild(ui, 'cvs_cloumninfo')
    local cvs_leave = Api.UI.FindChild(ui, 'cvs_leave')
    local lb_name1 = Api.UI.FindChild(ui, 'lb_name1')
    local lb_name2 = Api.UI.FindChild(ui, 'lb_name2')
    local lb_disnum1 = Api.UI.FindChild(ui, 'lb_disnum1')
    local lb_disnum2 = Api.UI.FindChild(ui, 'lb_disnum2')
    local lb_num1 = Api.UI.FindChild(ui, 'lb_num1')
    local lb_num2 = Api.UI.FindChild(ui, 'lb_num2')
    local lb_countdown = Api.UI.FindChild(ui, 'lb_countdown')
    local btn_checkinfo = Api.UI.FindChild(ui, 'btn_checkinfo')
    Api.UI.SetScreenAnchor(cvs_cloumninfo, 't')
    Api.UI.SetScreenAnchor(cvs_leave, 'r_t')
    local x = Api.UI.GetPositionX(cvs_leave)
    local y = Api.UI.GetPositionY(cvs_cloumninfo)

    -- Api.UI.SetPositionX(cvs_leave, x)
    -- Api.UI.SetPositionY(cvs_cloumninfo, y)
    Api.UI.SetFrameEnable(ui, false)
    local btn_fbleave = Api.UI.FindChild(ui, 'btn_fbleave')
    Api.UI.Listen.TouchClick(
        btn_fbleave,
        function()
            Api.ExitCurrentZone(true)
        end
    )
    Api.UI.Listen.TouchClick(
        btn_checkinfo,
        function()
            local members = guild_map[ActorGuildUUID].members
            table.sort(
                members,
                function(m1, m2)
                    return m1.help_score + m1.kill_score > m2.help_score + m2.kill_score
                end
            )
            Api.Task.AddEventTo(ID, {main = OnShowMemberInfo, clean = OnShowMemberClose}, members)
        end
    )

    ActorForce = Api.GetActorForce()
    ActorGuildUUID = Api.GetActorGuildUUID()
    ActorUUID = Api.GetActorUUID()
    car_endx, car_endy = Api.GetFlagPosition(Car_End_Flag)
    for k, v in pairs(guild_map) do
        if k == ActorGuildUUID then
            Api.UI.SetText(lb_name1, v.name)
            v.lb_num = lb_num1
            v.lb_disnum = lb_disnum1
        else
            v.lb_disnum = lb_disnum2
            v.lb_num = lb_num2
            Api.UI.SetText(lb_name2, v.name)
        end
        Api.UI.SetText(v.lb_num, Api.GetText(Constants.Text.guild_carriage_pcount, #v.members))
        Api.UI.SetText(v.lb_disnum, Api.GetText(Constants.Text.guild_carriage_distance, v.distance))
    end

    local function SyncWithServer()
        if guild_map.win_guild then
            -- pprint('guild_map', guild_map)
            --结算
            local win_guild = guild_map.win_guild
            guild_map.win_guild = nil
            local members = {}
            for _, v in pairs(guild_map) do
                for _, vv in ipairs(v.members) do
                    table.insert(members, vv)
                end
            end
            table.sort(
                members,
                function(m1, m2)
                    return m1.help_score + m1.kill_score > m2.help_score + m2.kill_score
                end
            )
            local title = win_guild == ActorGuildUUID and 'ib_win' or 'ib_defeat'
            Api.Task.AddEventTo(ID, OnShowGameOverResult, title, members)
        else
            for k, v in pairs(guild_map) do
                Api.UI.SetText(v.lb_num, Api.GetText(Constants.Text.guild_carriage_pcount, #v.members))
                Api.UI.SetText(v.lb_disnum, Api.GetText(Constants.Text.guild_carriage_distance, v.distance or '???'))
            end
        end
    end
    -- Api.Listen.AddPeriodicSec(1,SyncHud)
    Api.Listen.Message(
        'guild_carriage.battleinfo',
        function(msgname, params)
            for k, v in pairs(guild_map) do
                v.distance = params[k].distance
                v.members = params[k].members
            end
            -- print('params.countdown_sec',params.countdown_sec)
            local txt = Api.FormatSecondStr(params.countdown_sec)
            Api.UI.SetText(lb_countdown, txt)
            guild_map.win_guild = params.win_guild
            SyncWithServer()
        end
    )
    Api.Task.WaitAlways()
end

function clean()
    Api.UI.Close(ui)
    Api.UI.Close(member_ui)
    Api.UI.Close(result_ui)
end
