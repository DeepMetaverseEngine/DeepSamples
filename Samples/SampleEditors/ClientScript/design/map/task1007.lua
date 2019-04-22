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
	api.Unit.AddBubbleTalk(30019,"我昨天在村外玩的时候，有只大猴子冲了出来，我被吓坏了，逃跑的时候，我的洋娃娃丢在森林里了！","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30019,"你能帮我找回洋娃娃吗？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(0,"没问题，那个大猴子长什么样？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30019,"它长得特别巨大，还长了很多黄色的胡子！","0",2)
	api.Sleep(3)
	--添加气泡 对象objid 文字内容 气泡类型 显示时间
	--api.Unit.AddCGBubbleTalk(objid,"这是个CG单位跑步测试泡泡","0",2000)
    --播放占中对话框 内容 + 时间
	--api.UI.ShowTip("测试对话",5000)
    --播放cg
    --api.PlayCG("tasksp1001")
end