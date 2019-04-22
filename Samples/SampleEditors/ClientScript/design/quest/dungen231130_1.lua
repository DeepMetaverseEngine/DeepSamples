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
	--api.PauseBGM()
	api.CanSkipCG(true)
    api.PlayCG("dungen231130_1")
	--api.ResumeBGM()
end