local RedForce = 2
local BlueForce = 3
local WatchForce = 0

function main(force, res, countdownSec, firstCountdown, members)
    pprint('main:',force, res, countdownSec, firstCountdown, members)

    if firstCountdown > 0 then
        Api.Task.AddEvent('Client/countdown_ui', firstCountdown, Constants.Text.pvp_countdown)
    end

    ui = Api.UI.Open('xml/hud/ui_hud_arenacontest.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
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
    -- local cvs_redplayer = Api.UI.FindChild(ui, 'cvs_redplayer')
    -- local cvs_blueplayer = Api.UI.FindChild(ui, 'cvs_blueplayer')
    local lb_redwin = Api.UI.FindChild(ui, 'lb_redwin')
    local lb_bluewin = Api.UI.FindChild(ui, 'lb_bluewin')

    local lb_betime = Api.UI.FindChild(ui, 'lb_betime')
    local btn_leave = Api.UI.FindChild(ui, 'btn_leave')

    Api.UI.SetScreenAnchor(cvs_title, 't')
    -- Api.UI.SetScreenAnchor(cvs_redplayer, 'l')
    -- Api.UI.SetScreenAnchor(cvs_blueplayer, 'r')

    -- Api.UI.SetVisible(cvs_redplayer, false)
    -- Api.UI.SetVisible(cvs_blueplayer, false)

    if force == RedForce or force == BlueForce then
        Api.UI.SetVisible(btn_leave, false)
    end
    

 
    --force, nodes
    -- local playernodes = {}
    -- local rednodes = {}
    -- local bluenodes = {}
    
    -- rednodes['lb_player'] = Api.UI.FindChild(cvs_redplayer, 'lb_redplayer1')
    -- rednodes['gg_player'] = Api.UI.FindChild(cvs_redplayer, 'gg_redplayer1')
    -- rednodes['ib_break'] = Api.UI.FindChild(cvs_redplayer, 'ib_redbreak1')
    -- rednodes['cvs_player'] = Api.UI.FindChild(cvs_redplayer, 'cvs_redplayer1')

    -- bluenodes['lb_player'] = Api.UI.FindChild(cvs_blueplayer, 'lb_blueplayer1')
    -- bluenodes['gg_player'] = Api.UI.FindChild(cvs_blueplayer, 'gg_blueplayer1')
    -- bluenodes['ib_break'] = Api.UI.FindChild(cvs_blueplayer, 'ib_bluebreak1')
    -- bluenodes['cvs_player'] = Api.UI.FindChild(cvs_blueplayer, 'cvs_blueplayer1')

    -- playernodes[RedForce] = rednodes
    -- playernodes[BlueForce] = bluenodes

    local str = Api.FormatSecondStr(countdownSec)
    Api.UI.SetText(lb_betime, str)

    local function SyncRes()
        local forceres = res[RedForce]
        Api.UI.SetText(lb_redwin, Api.GetText(Constants.Text.pvp_wincount, forceres.cur, forceres.max))

        forceres = res[BlueForce] or forceres
        Api.UI.SetText(lb_bluewin, Api.GetText(Constants.Text.pvp_wincount, forceres.cur, forceres.max))
    end

    SyncRes()

    Api.Listen.Message(
        'pvp1.res',
        function(ename, newres)
            pprint('pvp1.res', newres)
            for f, v in pairs(newres) do
                res[f].cur = v
            end
            SyncRes()
        end
    )

    -- if firstCountdown > 0 then
    --       Api.Task.Sleep(firstCountdown)
    -- end
    
    local function SyncTimer()
        while true do
            if countdownSec < 0 then
                countdownSec = 0
            end
            local str = Api.FormatSecondStr(countdownSec)
            Api.UI.SetText(lb_betime, str)
            -- for _, info in pairs(members) do
            --     local uuid = info.uuid
            --     local nodes = playernodes[info.force]
            --     local lb_player = nodes['lb_player']
            --     local gg_player = nodes['gg_player']
            --     local ib_break = nodes['ib_break']
            --     local cvs_player = nodes['cvs_player']
            --     local playerInfo = Api.GetPlayerInfo(uuid)
            --     Api.UI.SetVisible(ib_break, playerInfo.State == 'Offline')
            --     if playerInfo.State == 'Dead' or playerInfo.State == 'Offline' then
            --         Api.UI.SetGray(lb_player, true)
            --         Api.UI.SetGray(gg_player, true)
            --         if playerInfo.State == 'Dead' then
            --             Api.UI.SetGaugeMinMax(gg_player, 0, 100)
            --             Api.UI.SetGaugeValue(gg_player, 0)
            --         end
            --     else
            --         Api.UI.SetGray(lb_player, false)
            --         Api.UI.SetGray(gg_player, false)
            --         Api.UI.SetGaugeMinMax(gg_player, 0, playerInfo.MaxHp)
            --         Api.UI.SetGaugeValue(gg_player, playerInfo.Hp)
            --         Api.UI.SetText(lb_player, Api.GetShortText(playerInfo.Name, 3))
            --     end
            --     Api.UI.Listen.TouchClick(cvs_player,function()
            --         if Api.IsActorDead() then
            --             Api.Camera.FollowPlayer(uuid)
            --         end
            --     end)
            -- end
            Api.Task.Sleep(1)
            countdownSec = countdownSec - 1
        end
    end

       

    if force == WatchForce then
        
        Api.Camera.FollowPlayer(members[RedForce].uuid)

        local cvs_redwin = Api.UI.FindChild(ui, 'cvs_redwin')
        Api.UI.Listen.TouchClick(cvs_redwin,function()
            Api.Camera.FollowPlayer(members[RedForce].uuid)
        end)
        
        local cvs_bluewin = Api.UI.FindChild(ui, 'cvs_bluewin')
        Api.UI.Listen.TouchClick(cvs_bluewin,function()
            if members[BlueForce] and members[BlueForce].uuid then
                Api.Camera.FollowPlayer(members[BlueForce].uuid)
            end
        end)
        
    end



    -- if not members then
    --     local success, ename, syncmembers = Api.Task.Wait(Api.Listen.Message('pvp1.members'))
    --     members = syncmembers
    -- end

    Api.Listen.ActorBirth(
        function()
            Api.Camera.FollowActor()
        end
    )
    -- Api.UI.SetVisible(cvs_redplayer, false)
    -- Api.UI.SetVisible(cvs_blueplayer, false)
    local syncId = Api.Task.AddEvent(SyncTimer)

    Api.Listen.Message(
        'pvp1.win',
        function(ename, force)
            if force == RedForce then
                Api.Task.StartEvent('Client/show_ui', 'xml/battleground/ui_battleground_redwin.gui.xml', 2, true)
            elseif force == BlueForce then
                Api.Task.StartEvent('Client/show_ui', 'xml/battleground/ui_battleground_bluewin.gui.xml', 2, true)
            end
        end
    )

    
    Api.Listen.Message('pvp1.newbattle',function (ename,delaySec)
        countdownSec = delaySec or countdownSec
        local str = Api.FormatSecondStr(countdownSec)
        Api.UI.SetText(lb_betime, str)
        Api.Task.StopEvent(syncId)
        syncId = Api.Task.AddEventTo(ID,SyncTimer)
    end)

    local success, ename, gameover = Api.Task.Wait(Api.Listen.Message('pvp1.gameover'))
  
    if gameover.force == 0 then
        return
    end

    local eff_eid
    if gameover.force == RedForce then
        --红方胜
        eff_eid = Api.PlayEffect('/res/effect/ui/ef_ui_victory_red.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
    elseif gameover.force == BlueForce then
        --蓝方胜
        eff_eid = Api.PlayEffect('/res/effect/ui/ef_ui_victory_blue.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
    else
        return
    end
    Api.Task.Sleep(2.5)




    --结算界面
    ui_result = Api.UI.Open('xml/anrenacontest/ui_arena_account.gui.xml', {Layer = 'MessageBox'})
    Api.UI.Listen.MenuExit(
        ui_result,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
     
    local redName = Api.UI.FindChild(ui_result, 'lb_name1')
    local blueName = Api.UI.FindChild(ui_result, 'lb_name2')
    Api.UI.SetText(redName,gameover.players[RedForce].name)
    Api.UI.SetText(blueName,gameover.players[BlueForce].name)

    local lb_resultnum = Api.UI.FindChild(ui_result, 'lb_resultnum')
    Api.UI.SetText(lb_resultnum,gameover.scores)

    local lb_winname = Api.UI.FindChild(ui_result, 'lb_winname')
    local winmessage = Api.GetText('arena_win',gameover.players[gameover.force].name)
    Api.UI.SetText(lb_winname,winmessage)
    -- Api.UI.SetText(lb_redresource, Api.GetText(Constants.Text.pvp_winresult, gameover.scores[RedForce] or 0, forceres.max))
    -- local lb_winname = Api.UI.FindChild(ui_result, 'lb_winname')

    local ok, snap = Api.Task.Wait(Api.Task.GetRoleSnap(gameover.players[RedForce].uuid))
    local cvs_touxiang1 = Api.UI.FindChild(ui_result, 'cvs_touxiang1')
    local img = 'static/target/'..snap.Pro..'_'..snap.Gender..'.png'
    Api.UI.SetImage(cvs_touxiang1,img) 

    local cvs_touxiang2 = Api.UI.FindChild(ui_result, 'cvs_touxiang2')
    local ok2, snap2 = Api.Task.Wait(Api.Task.GetRoleSnap(gameover.players[BlueForce].uuid))
    local img2 = 'static/target/'..snap2.Pro..'_'..snap2.Gender..'.png'
    Api.UI.SetImage(cvs_touxiang2,img2)
   
    local btn_close = Api.UI.FindChild(ui_result, 'btn_close')
    Api.UI.Listen.TouchClick(
        btn_close,
        function()
            Api.ExitCurrentZone()
        end
    )
 

    local lb_time = Api.UI.FindChild(ui_result, 'lb_countdown')

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
