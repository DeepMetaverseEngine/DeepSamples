function main(xmlpath, timesec, fadeout)
    ui = Api.UI.Open(xmlpath, UIShowType.Cover,{AnimeType = UIAnimeType.NoAnime})
    Api.Task.Sleep(timesec or 3)
    fadeout = fadeout or true
    if fadeout then
        Api.Task.Wait(Api.UI.Task.AlphaTo(ui, 0, 0.6))
    end
end

function clean()
    Api.UI.Close(ui)
end 