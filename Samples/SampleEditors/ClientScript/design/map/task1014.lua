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
	
	api.Unit.AddBubbleTalk(0,"你是谁？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30054,"我是守梦者组织的创始人之一，人称“冥君维普尔”！","0",2)
	api.Sleep(2)
    api.Unit.AddBubbleTalk(0,"刚刚我怎么了？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(30054,"没意思，被催眠控制的人醒来除了问你是谁，我怎么了，就没有有新意的问题了吗？","0",2)
	api.Sleep(3)
	--添加气泡 对象objid 文字内容 气泡类型 显示时间
	--api.Unit.AddCGBubbleTalk(objid,"这是个CG单位跑步测试泡泡","0",2000)
    --播放占中对话框 内容 + 时间
	--api.UI.ShowTip("测试对话",5000)
    --播放cg
    --api.PlayCG("tasksp1001")
end