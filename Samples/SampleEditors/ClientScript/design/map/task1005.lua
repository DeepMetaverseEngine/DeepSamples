function main()
	--api.Sleep(5)
	--打开ui
	-- api.Call('GlobalHooks.UI.OpenUI','Test')
	-- api.UI.OpenByTag('Test',-1)
	--添加对象 对象模板-0就是玩家自己  坐标x 坐标y 方向（弧度） 返回对象objid
	-- local objid = api.Unit.AddUnit(0,215,90,0)
	--  print("objid=",objid) 	
	--移除对象 对象objid
	--api.Unit.RemoveUnit(objid)
	--播放动画 对象objid 动作名字 延迟播放时间 播放速度 播放类型1是播放一次2是循环
	--api.Unit.PlayAnim(objid,"n_run", 0, 1,1)
	--添加气泡 模板id为0就是玩家自己  文字内容 气泡类型 显示时间

	api.Unit.AddBubbleTalk(0,"所以你许诺我的奖赏呢？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30020,"这个不必着急！","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30020,"我从王都来，是要为尊贵的主教大人接收一批货物！","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30020,"不知道为什么，主教大人坚持要走海运！结果我在路上被强盗劫持的时候听说，那艘船被海盗袭击了！","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30020,"我在海滩上看到了一艘船，你可以去帮我调查一下吗？","0",2)
	api.Sleep(2)
    api.Unit.AddBubbleTalk(0,"货船？？？被海盗袭击？？？我好像想起来了点什么！我这就去看看！","0",2)
	api.Sleep(3)
	
	--添加气泡 对象objid 文字内容 气泡类型 显示时间
	--api.Unit.AddCGBubbleTalk(objid,"这是个CG单位跑步测试泡泡","0",2000)
    --播放占中对话框 内容 + 时间
	--api.UI.ShowTip("测试对话",5000)
    --播放cg
    --api.PlayCG("tasksp1001")
end