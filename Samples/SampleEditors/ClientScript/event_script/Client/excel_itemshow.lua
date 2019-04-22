function main(ele)
    pprint('success ---------------------',ele)
    --Api.UI.CloseAll()
    --print("filename",filename,showtext)
    ui = Api.UI.Open('xml/hud/ui_itemshow.gui.xml', UIShowType.Cover, {Layer = 'MessageBox',AnimeType=UIAnimeType.NoAnime})
    Api.UI.SetFrameEnable(ui, false)
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    local cvs_item = Api.UI.FindChild(ui, 'cvs_item')
    local ib_icon = Api.UI.FindChild(ui, 'ib_icon')
    local lb_name = Api.UI.FindChild(ui, 'lb_name')
    local cvs_itemshow = Api.UI.FindChild(ui, 'cvs_itemshow')
    Api.UI.SetImage(cvs_item,ele.cvs_item)
    Api.UI.SetImage(ib_icon,ele.ib_icon)
    Api.UI.SetText(lb_name, Api.GetText(ele.lb_name))
    Api.Task.Sleep(ele.time/1000)
    local dur = 1
    Api.Task.Wait(Api.UI.Task.AlphaTo(cvs_itemshow,0,dur))
   
end
function clean()
    --print("ui_showtime")
    Api.UI.Close(ui)
end