function main(objID)
    print('objid',objID)
    local eff = {
        Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
        BindBody = true,

    }
    Api.PlayUnitEffect(objID, eff)

    Api.AddBubbleTalk(objID, '哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈',3)

    Api.Task.Sleep(2)
end