
local RedForce = 2
local BlueForce = 3 

function main(countdownSec)

    -- print(' =======pvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1waitpvp1wait')

    Api.Task.WaitActorReady()
    Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )

    ui = Api.UI.Open('xml/hud/ui_hud_arenatip.gui.xml', UIShowType.Cover, {Layer = 'MessageBox', EnableFrame = false})
    local cvs = Api.UI.FindChild(ui, 'cvs_tip')
    local lb_tip2 = Api.UI.FindChild(cvs, 'lb_tip2')

    local function SyncTimer()
        while true do
            if countdownSec < 0 then
                countdownSec = 0
            end
            local str = Api.GetText(Constants.Text.Pvp1WaitStr,countdownSec)
            Api.UI.SetText(lb_tip2, str)
            Api.Task.Sleep(1)
            countdownSec = countdownSec - 1
        end
    end

    local eid = Api.Task.AddEvent(SyncTimer)

    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )


    local success, ename, win = Api.Task.Wait(Api.Listen.Message('pvp1wait.over'))
    -- pprint('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa',success, ename, win)
    -- if win == false then
    --      Api.Task.StopEvent(eid)
    --     return
    -- end

    -- local eff_eid = Api.PlayEffect('/res/effect/ui/ef_ui_victory_red.assetbundles', {UILayer = true, Pos = {x = 0, y = 200, z = -333}})
 
    -- Api.Task.Sleep(2.5)

 
    -- --结算界面
    -- ui_result = Api.UI.Open('xml/anrenacontest/ui_arena_account.gui.xml', {Layer = 'MessageBox'})
    -- Api.UI.Listen.MenuExit(
    --     ui_result,
    --     function()
    --         Api.Task.StopEvent(ID, false, 'MenuExit')
    --     end
    -- )
     
    -- local lb_resultnum = Api.UI.FindChild(ui_result, 'lb_resultnum')
    -- Api.UI.SetText(lb_resultnum,0)
    -- -- Api.UI.SetText(lb_redresource, Api.GetText(Constants.Text.pvp_winresult, gameover.scores[RedForce] or 0, forceres.max))
    -- -- local lb_winname = Api.UI.FindChild(ui_result, 'lb_winname')
     
    -- local btn_close = Api.UI.FindChild(ui_result, 'btn_close')
    -- Api.UI.Listen.TouchClick(
    --     btn_close,
    --     function()
    --         Api.ExitCurrentZone()
    --     end
    -- )
 

    -- local lb_time = Api.UI.FindChild(ui_result, 'lb_countdown')

    -- local maxSec = 20
    -- Api.UI.SetText(lb_time, Api.FormatLangSecond(maxSec))
    -- --结算界面倒计时
    -- Api.Task.Wait(
    --     Api.Listen.AddPeriodicSec(
    --         1,
    --         maxSec,
    --         function(total)
    --             local cur = maxSec - total
    --             Api.UI.SetText(lb_time, Api.FormatLangSecond(cur))
    --         end
    --     )
    -- )
     

end

function clean()
    Api.UI.Close(ui)
    if ui_result then
        Api.UI.Close(ui_result)
    end
end
