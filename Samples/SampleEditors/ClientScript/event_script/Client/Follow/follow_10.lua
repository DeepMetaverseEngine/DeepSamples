function main(objID)
    print('objid',objID)
    local eff = {
        Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
        BindBody = true,

    }
    --Api.PlayUnitEffect(objID, eff)

    Api.AddBubbleTalk(objID, '到了洗梧宮會不會看到夜華呢',3)
	Api.Task.Sleep(2)
	Api.AddBubbleTalk(objID, '天宮真漂亮，為什麼會有一種熟悉的感覺',3)
	Api.Task.Sleep(2)
    Api.AddBubbleTalk(objID, '快到了吧，心裏好激動',3)
	Api.Task.Sleep(2)
	Api.AddBubbleTalk(objID, '夫君，馬上就能和你在一起了',3)
    --Api.Task.Sleep(2)
	--Api.AddBubbleTalk(objID, '我家就在俊疾山上',3)
end