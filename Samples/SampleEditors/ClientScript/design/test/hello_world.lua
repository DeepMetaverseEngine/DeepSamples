function main()
	--api.Sleep(5)
	--打开ui
	-- api.Call('GlobalHooks.UI.OpenUI','Test')
	-- api.UI.OpenByTag('Test',-1)
	--添加对象 对象模板-0就是玩家自己  坐标x 坐标y 方向（弧度） 返回对象objid
	--local objid = api.Unit.AddUnit(0,118,267,0)
	--  print("objid=",objid) 	
	--移除对象 对象objid
	--api.Unit.RemoveUnit(objid)
	--播放动画 对象objid 动作名字 延迟播放时间 播放速度 播放类型1是播放一次2是循环
	--api.Unit.PlayAnim(objid,"n_run", 0, 1,1)
	--添加气泡 模板id为0就是玩家自己  文字内容 气泡类型 显示时间
	--api.Unit.AddBubbleTalk(0,"这是个主角测试泡泡1泡泡1泡泡1泡泡2泡泡2泡泡2泡泡3这是个主角测试泡泡1泡泡1泡2泡泡2泡泡2泡泡3这是","0",2)
	--api.Unit.AddBubbleTalk(30001,"这是个|<q {\"index\":1}></q>|npc主角测试泡泡1泡泡1泡泡1泡泡2泡泡2泡泡2泡泡3这是个主角测试泡泡1泡泡1泡2泡泡2泡泡2泡泡3这是","0",2)
	--添加气泡 对象objid 文字内容 气泡类型 显示时间
	--api.Unit.AddCGBubbleTalk(objid,"这是个CG单位跑步测试泡泡","0",20)
    --播放占中对话框 内容 + 时间
	--api.UI.ShowTip("测试对话",5000)
    --播放cg
	-- api.CanSkipCG(true)
    -- api.PlayCG("dungen210000_1")
	 --api.PlayBGM("dynamic/bgm/zhandou1")
	 --api.ChangeBGM("dynamic/bgm/junjishan")
	 --api.StopBGM()
     --api.PauseBGM()
    --api.ResumeBGM()
    --无缝切换背景音乐
     --api.ChangeBGM("bgm/zora")
	 --api.PlayVoice("bgm/zora")
	 --api.StopVoice()
	 --api.PlayCG("battle1007")
	 --api.Sleep(5)
	 --api.CanSkipCG(true)
	 --api.Unit.AddBubbleTalk(0,"这个就是洛丽塔的洋娃娃吧，已经被蹂躏的不成样子了啊！","0",2)
	 --************有步骤的时候判断步骤
	 -- local step = api.GetStep()
	 -- if step and step == "mmp" then
	 -- 	api.SendStep("mmp1")
	 -- api.PlayCG("questsp1116_3")
	 -- api.CanSkipCG(true)
     -- end
     -- ***********************

     --************第一次的时候判断
	 -- local step = api.GetStep()
	 -- if not step then
	 -- 	api.SendStep("mmp")
	 -- ***黑屏api
	 --	api.BlackScreen(true)
	 --******************
	 --******************
	--api.CanSkipCG(true)
	--api.PlayCG("questsp1047_1")
	--   api.ShowChapter(1)
	--api.Unit.AddBubbleTalk(21001,"喵！毛球","0",4)
	--api.Unit.AddBubbleTalk(0,"白虎精和小猫一样习性","0",4)
	--   api.CanSkipCG(true)
	 --脚本运行完毕通知战斗服
	 --api.StoryFinish()
     -- end
     --***************************
	 --print('return ',api.Wait(api.TestWaitSecond(2)))
     --api.PlayCG("tasksp1001")

     --print("PlayCGOver")
     --api 支持发送聊天信息
     --api.ChatSend("@gm acceptTask 1 1011")
     --控制真实主角转向 第2个参数为是否相机跟随
     --api.ActorChangeDirection(2.9,true)
     --api.PlayRippleEffect()
     --结束非cg剧情
     --EventManager.Fire(Events.PLAYCG_START,{PlayCG = false})

     --动作序列播放 当前视野范围内的模板
 	-- local action = {
     -- 	{"c_attack",1},
     -- 	{"n_idle",1},
 	-- }
 	-- api.Unit.ChangeUnitPlayAnim(30035,action)
 	
 	--动作名字播放 当前视野范围内的模板
 	--单一动作
 	--api.Unit.ChangeUnitPlayAnim(30035,'c_attack')
     --显示章节
	 --api.Wait(api.ShowChapter(2))
     --直接调用对话
     --api.Wait(api.Quest.TalkContent("dialogue_1.dialogue_content"))
     --直接根据id调用对话
     --api.CanSkipCG(true)
    --  api.PlayCG("questsp1025_3",false)
     -- api.CanSkipCG(true)
    --  api.PlayCG("questsp1004_3")
     -- print("PlayCG")
     -- api.Wait(api.Quest.TalkContentbyId("1047_2"))
     -- api.Wait(api.Quest.TalkContentbyId("1001_2"))
    --pi.Wait(api.Quest.TalkContentbyId("1036_2_1"))
     --播放场景特效
   --   local params = {
   --   	pos = {117,179},  --场景坐标
   --   	duration = 10, --持续时间 -1不限时
   --		direct = 0   --角度(弧度)
 	 -- }
   --   api.PlaySceneEffect(1,"ef_buff_bossmagiccircle",params)--①自定义key要唯一  ②特效名字

      --挂人物节点特效
   --    local params = {
   --   	duration = 10,
   --   	nodeparams = "0|Chest_Buff", --①模板id为0就是玩家自己反之就是unit模板id ②挂指定模板 后面没有节点名字 就默认走脚底,节点注意大小写，
 	 -- }
   --  api.PlaySceneEffect(1,"ef_buff_bossmagiccircle",params)
   -- 开启剧情模式的边框 参数是开和关
   -- api.ShowLetterBox(true)

   --策划自定义界面
   --api.OpenUI('1000')
	--api.UI.testUI()
	--立刻刷新视野内npc
	--api.RefreshNpc()
	--api.FindTreasure(true,100,100)
	--i.Unit.AddBubbleTalk(30061,"我感觉好多了！","0",5)
	--i.Unit.AddBubbleTalk(30036,"多些仙君相救","0",5)
	--转向api arg1 模板id arg2 转向角度
	--api.Unit.ChangeDirection(30029,4)
	--获得当前模板对象相对玩家的角度 
	--local dir = api.Unit.GetDirFromPlayer(30029)
	--api.Unit.ChangeDirection(30029,dir)
	--模板对象看向玩家
	--api.Unit.FaceToPlayer(30029)
	--删除特效
	--api.RemoveEffectByKey(1)
	--调用事件库
	--api.StartClientScript("Client/Test")
	--停止/恢复转向
	--api.Unit.UnitStopTurnDirect(30029,false)
	--api.StartClientScript('Client/ui_showitem','#static/TL_hud/output/TL_hud.xml|TL_hud|23',"67dsb")
	--api.testshowitem(1)
	--api.ShowWeather({ShowWeather = {"ef_rain"},CloseWeather = {"ef_blizzard"}})
	--local objid = api.Unit.AddUnit(30065,98.15,174.72,1.6)
     --直发聊天命令
     --api.SendChat("@gm acceptquest 1036")
     --api.Sleep(1)
     --api.SendChat("@gm finishquest 1036")

     ----职业
     --print("pro..",api.Unit.GetPro())
     ----性别
     --print("sex..",api.Unit.GetSex())
	api.Wait(api.ListenPlayerInRegion('zhaohuantudigong'))
end
