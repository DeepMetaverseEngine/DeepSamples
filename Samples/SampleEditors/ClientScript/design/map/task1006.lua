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
	api.Unit.AddBubbleTalk(0,"你认识我？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30009,"不，完全不认识，我只知道你是一个雇佣兵，仅此而已！","0",2)
	api.Sleep(2)
    api.Unit.AddBubbleTalk(0,"那你说的怪物是怎么回事？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30009,"我们在同一艘被诅咒的货船上！两天前的暴风雨之夜，我们被海盗袭击了！","0",2)
	api.Sleep(3)
	api.Unit.AddBubbleTalk(30009,"海盗释放了深海巨妖库拉肯！最后关头，还是你冲上去与它殊死搏斗，我们才能逃生的！","0",2)
	api.Sleep(3)
	
	--添加气泡 对象objid 文字内容 气泡类型 显示时间
	--api.Unit.AddCGBubbleTalk(objid,"这是个CG单位跑步测试泡泡","0",2000)
    --播放占中对话框 内容 + 时间
	--api.UI.ShowTip("测试对话",5000)
    --播放cg
    --api.PlayCG("tasksp1001")
end