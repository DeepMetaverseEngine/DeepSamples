function main()
	local effid = Api.PlayEffect('/res/effect/ui/ef_ui_shimen_failure.assetbundles', {UILayer=true,Pos= {x = -30, y = 0, z = -333}})
    local ui = Api.UI.Open('xml/guild/ui_guild_identityresult.gui.xml')


    Api.UI.SetBackground(ui, 0.3)
    Api.UI.Listen.MenuExit(ui,function()
        Api.Task.StopEvent(ID, false, 'MenuExit')
    end)
    local lb_countdown = Api.UI.FindChild(ui, 'lb_countdown')
    local time,id = 5
    Api.UI.SetText(lb_countdown, Api.GetText("dungeon_exit_time",time))
    id = Api.Listen.AddPeriodicSec(1,function()
        if time>0 then
            time = time - 1
            Api.UI.SetText(lb_countdown, Api.GetText("dungeon_exit_time",time))
        else
            Api.RemovePlayEffect(effid)
            Api.Task.StopEvent(id)
            Api.UI.Close(ui)
        end
    end)

    Api.Task.Wait(id)
end