function main()

    Api.PlayEffect('/res/effect/ui/ef_ui_rocker03.assetbundles', {UILayer=true,Pos={x = -196, y = 126, z = -333}},1.4)
    Api.Task.Sleep(1.5)
    Api.PlayEffect('/res/effect/ui/ef_ui_rocker03.assetbundles', {UILayer=true,Pos={x = -29, y = -46, z = -333}},1.4)

end