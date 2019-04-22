function main()

    -- local eff = {
    --     Name = '/res/effect/ef_player_kl_skill01_01.assetbundles',
    --     BindBody = true,

    -- }
    -- Api.PlayUnitEffect(objID, eff)

    -- Api.AddBubbleTalk(objID, '哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈',3)

    -- Api.Task.Sleep(2)
    local step = Api.Guide.GetGuideStep("FirstRecharge")
		if step then
			return true
		end
	Api.Guide.SetGuideStep("FirstRecharge","1")
    Api.UI.CloseAll()
     local ui = Api.UI.OpenByTag('FirstRecharge')
end