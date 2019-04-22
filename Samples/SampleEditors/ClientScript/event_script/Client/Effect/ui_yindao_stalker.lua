function main()
ui = Api.UI.Open('xml/common/common_stalker_tips.gui.xml')
    Api.UI.SetBackground(ui, 0.5)

       Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')

        end
    )

end