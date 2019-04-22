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
	api.Unit.AddBubbleTalk(30001,"你是谁呀，怎么在海滩上睡觉？","0",2)
	api.Sleep(2)
	api.Unit.AddBubbleTalk(0,"海滩？我是谁？头好疼啊！什么都想不起来了！","0",2)
	api.Sleep(2)
    api.Unit.AddBubbleTalk(0,"让我好好想想，我好像叫...<font color=\"fffed514\">%name</font>，我为什么在这？唉！实在想不起来了！","0",3)
	api.Sleep(2)
	--添加气泡 对象objid 文字内容 气泡类型 显示时间
	--api.Unit.AddCGBubbleTalk(objid,"这是个CG单位跑步测试泡泡","0",2000)
    --播放占中对话框 内容 + 时间
	--api.UI.ShowTip("测试对话",5000)
    --播放cg
    --api.PlayCG("tasksp1001")
end