function main()
	--api.Sleep(5)
	--打開ui
	-- api.Call('GlobalHooks.UI.OpenUI','Test')
	-- api.UI.OpenByTag('Test',-1)
	--添加對象 對象模板-0就是玩家自己  坐標x 坐標y 方向（弧度） 返回對象objid
	-- local objid = api.Unit.AddUnit(0,215,90,0)
	--  print("objid=",objid) 	
	--移除對象 對象objid
	--api.Unit.RemoveUnit(objid)
	--播放動畫 對象objid 動作名字 延遲播放時間 播放速度 播放類型1是播放一次2是循環
	--api.Unit.PlayAnim(objid,"n_run", 0, 1,1)
	--添加氣泡 模板id為0就是玩家自己  文字內容 氣泡類型 顯示時間
--	api.Unit.AddBubbleTalk(0,"這是個主角測試泡泡1泡泡1泡泡1泡泡2泡泡2泡泡2泡泡3","0",2000)

--	api.Unit.AddBubbleTalk(30001,"這是個NPC測試泡泡","0",2000)
	--添加氣泡 對象objid 文字內容 氣泡類型 顯示時間
	--api.Unit.AddCGBubbleTalk(objid,"這是個CG單位跑步測試泡泡","0",2000)
    --播放占中對話框 內容 + 時間
	--api.UI.ShowTip("測試對話",5000)
    --播放cg
	--api.CanSkipCG(true)
    --api.PlayCG("questsp1002_3")
	--api.Unit.AddBubbleTalk(30033,"過了橋就是我家了","0",4)
      -- local params = {
     	-- duration = 10,
     	-- nodeparams = "30026|ef_buff_heal", --①模板id為0就是玩家自己反之就是unit模板id ②掛指定模板 后面沒有節點名字 就默認走腳底,節點注意大小寫，
 	 -- }
     -- api.PlaySceneEffect(30026,"ef_buff_heal",params)
	 -- api.Unit.ChangeUnitPlayAnim(30026,'n_idle')
	 -- api.Sleep(1)
	--api.Unit.AddBubbleTalk(21027,"你竟然能分辨出我！","0",4)
--	api.Unit.AddBubbleTalk(0,"素素，我們在中榮城見","0",4)
    local objid = api.Unit.AddUnit(30086,122.96,171.55,0.8)
end