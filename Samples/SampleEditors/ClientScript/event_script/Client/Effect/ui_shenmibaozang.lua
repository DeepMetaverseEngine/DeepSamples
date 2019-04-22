function main()

    -- local eff = {
    --     Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
    --     BindBody = true,

    -- }
    -- Api.PlayUnitEffect(objID, eff)

    -- Api.AddBubbleTalk(objID, '哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈',3)

    -- Api.Task.Sleep(2)
    Api.UI.CloseAll()
    Api.PlayEffect('/res/effect/ui/ef_ui_event_shenmibaozang.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}})
end