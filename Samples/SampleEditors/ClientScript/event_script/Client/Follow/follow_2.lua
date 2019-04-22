function main(objID)
    print('objid',objID)
    local eff = {
        Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
        BindBody = true,

    }
    --Api.PlayUnitEffect(objID, eff)

    Api.AddBubbleTalk(objID, '我家就在俊疾山上',3)
	Api.Task.Sleep(2)
	Api.AddBubbleTalk(objID, '怎麼樣，這路上的風景漂亮吧',3)
	Api.Task.Sleep(2)
    Api.AddBubbleTalk(objID, '跟緊點，馬上到了，不要跟丟了哦',3)
	Api.Task.Sleep(2)
	Api.AddBubbleTalk(objID, '快要到了，我回來啦',3)
    --Api.Task.Sleep(2)
	--Api.AddBubbleTalk(objID, '我家就在俊疾山上',3)
end