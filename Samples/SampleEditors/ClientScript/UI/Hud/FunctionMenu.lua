local _M = {}
_M.__index = _M

local FuncOpen = require 'Model/FuncOpen'

local self = {}


local function InitUI()
	local btn_role = self.root:FindChildByEditName("btn_role", true)
	btn_role.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('AttributeMain', 0)
	end

	local btn_practice = self.root:FindChildByEditName("btn_practice", true)
	btn_practice.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('PracticeMain', 0)
	end

	local btn_miracle = self.root:FindChildByEditName("btn_miracle", true)
	btn_miracle.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('MiracleFrame', 0,'MiracleMain')
	end

	local btn_mount = self.root:FindChildByEditName("btn_mount", true)
	btn_mount.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('MountFrame', 0,'MountInfo')
	end

	-- local btn_store = self.root:FindChildByEditName("btn_store", true)
	-- btn_store.TouchClick = function(sender)
	-- 	GlobalHooks.UI.OpenUI('Shop', 0, 2)
	-- end
	

	local btn_skill = self.root:FindChildByEditName("btn_skill", true)
	btn_skill.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('SkillFrame', 0)
	end

	local btn_fate = self.root:FindChildByEditName("btn_fate", true)
	btn_fate.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('FateMain', 0)
	end

	local btn_forge = self.root:FindChildByEditName("btn_forge", true)
	btn_forge.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('SmithyMain', 0, 'SmithyStrengthen')
	end

	local btn_wings = self.root:FindChildByEditName("btn_wings", true)
	btn_wings.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('WingFrame', 0)
	end

	local btn_ranklist = self.root:FindChildByEditName("btn_ranklist", true)
	if btn_ranklist then
		btn_ranklist.TouchClick = function(sender)
			GlobalHooks.UI.OpenUI("RankMain",0)
		end
	end

	local btn_partner = self.root:FindChildByEditName("btn_partner", true)
	btn_partner.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI("PartnerFrame",0,"PartnerMain")
	end

	local btn_sanshenglu = self.root:FindChildByEditName("btn_sanshenglu", true)
	btn_sanshenglu.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI("MemoirsFrame",0)
	end

	local btn_system= self.root:FindChildByEditName("btn_system", true)
	btn_system.TouchClick=function(sender)
		GlobalHooks.UI.OpenUI("SystemBackGround",0,"SystemSetting")
	end
	
	local btn_achievement = self.root:FindChildByEditName("btn_achievement",true)
	btn_achievement.TouchClick=function(sender)
		GlobalHooks.UI.OpenUI('AchievementMain',0)
	end

	local btn_guild = self.root:FindChildByEditName("btn_guild", true)
	btn_guild.TouchClick = function(sender)
		if GlobalHooks.IsFuncOpen("GuildMain", true) then
			if DataMgr.Instance.UserData.GuildId ~= nil and DataMgr.Instance.UserData.GuildId ~= '' then
				GlobalHooks.UI.OpenUI("GuildMain", 0, 'GuildInfo')
			else
				GlobalHooks.UI.OpenUI("GuildList",0)
			end
			if GlobalHooks.IsFuncWaitToPlay("GuildMain") then
				FuncOpen.SetPlayedFunctionByName("GuildMain")
			end
		end
	end

	local btn_pagoda = self.root:FindChildByEditName("btn_pagoda",true)
	btn_pagoda.TouchClick=function ( sender )
		 GlobalHooks.UI.OpenUI('PagodaMain',0)
	end
	
	local btn_meridians =self.root:FindChildByEditName('btn_meridians',true)
	btn_meridians.TouchClick=function()
		GlobalHooks.UI.OpenUI('MeridiansMain',0)
	end

	local btn_island = self.root:FindChildByEditName("btn_island", true)
	btn_island.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI("IslandMain",0)
	end

	for i = 1, self.root.NumChildren do
		local cvs = self.root:GetChildAt(i - 1)
		local lb_tip = cvs:FindChildByEditName("lb_tip", true)
		GlobalHooks.UI.SetNodeRelation(lb_tip, 'MainHud/cvs_topright/cvs_shanzi/lb_shanzired')
	end
end

local function ReSort( ... )
	local showComps = {}
	local compsCount = self.root.NumChildren
	for i = 0, compsCount - 1 do
		local cvs = self.root:GetChildAt(i)
		local name = cvs.EditName
    	local dbs = GlobalHooks.DB.Find('module_open', { menu_type = 2, comp = name })
    	local db
    	--从多个二级功能里选出最符合条件的
    	if dbs ~= nil then
    		if #dbs > 1 then
    			for a = 1, #dbs do
    				if GlobalHooks.IsFuncOpen(dbs[a].UI_flag) then --已开启的优先
    					db = dbs[a]
	    				if GlobalHooks.IsFuncWaitToPlay(dbs[a].UI_flag) then --已开启并没有游玩过最优先
	    					break
	    				end
    				end
    			end
    		else
    			db = dbs[1]
    		end
    	end
		local isOpen = (db ~= nil) and GlobalHooks.IsFuncOpen(db.UI_flag) or false
		if isOpen then
			cvs.Visible = true
			--红点
			local isShowRed =  db.open_type ~= 1 and db.open_type ~= 4 and GlobalHooks.IsFuncWaitToPlay(db.UI_flag)
			local lb_red = cvs:FindChildByEditName('lb_tip', true)
			GlobalHooks.UI.ShowRedPoint(lb_red, isShowRed and 1 or 0, 'funcopen')
			--插入排序
    		local t = {}
    		t.comp = cvs
    		t.data = db
    		local isInsert = false
    		for j = 1, #showComps do
    			local tmp = showComps[j]
    			if t.data.menu_order < tmp.data.menu_order then
    				table.insert(showComps, j, t)
    				isInsert = true
    				break
    			end
    		end
    		if not isInsert then
    			table.insert(showComps, t)
    		end
		else
			cvs.Visible = false
		end
	end

	local originComp = self.originComp
	local yNum = originComp.UserTag --6
	local space = 0
	for i = 1, #showComps do
		local row = math.floor((i - 1) / yNum)
		local col = (i - 1) % yNum
		local comp = showComps[i].comp
		comp.X = self.defaultPos.x - (originComp.Width + space) * row
		comp.Y = self.defaultPos.y + (originComp.Height + space) * col
	end
	self.showComps = showComps
end

local function DoMoveAction(node, target, duration, cb)
	local moveAction = MoveAction()
	moveAction.TargetX = target.x
	moveAction.TargetY = target.y
	moveAction.Duration = duration
	-- moveAction.ActionEaseType = EaseType.easeOutBack
	node:AddAction(moveAction)
	moveAction.ActionFinishCallBack = cb
end

local function DoDelayAction(node, duration, cb)
	local delayAction = DelayAction()
	delayAction.Duration = duration
	node:AddAction(delayAction)
	delayAction.ActionFinishCallBack = cb
end

local function DoFadeAction(node, duration, cb)
	local alphaAction = FadeAction()
	alphaAction.TargetAlpha = 1
	alphaAction.Duration = duration
	node:AddAction(alphaAction)
	alphaAction.ActionFinishCallBack = cb
end

local function LoopMoveCb( node, row, dftX )
	local offX = (node.X - dftX) * 0.1
	if math.abs(offX) < 0.1 then
		node.X = dftX
	else
		local posTarget = Vector2(dftX - offX, node.Y)
		DoMoveAction(node, posTarget, 0.3, function( ... )
			LoopMoveCb(node, row, dftX)
		end)
	end
end

local function ResetIcon( showAnime )
	-- print('--------------PlayAnime-------------', showAnime)
	local originComp = self.originComp
	local yNum = originComp.UserTag --6
	local space = 0

	--展开
	for i = 1, #self.showComps do
		local row = (i - 1) % yNum --行
		local col = math.floor((i - 1) / yNum) --列
		local comp = self.showComps[i].comp
		local x = self.defaultPos.x - (originComp.Width + space) * col
		local y = self.defaultPos.y + (originComp.Height + space) * row
		-- print('------------', comp.EditName, x, y)
		comp:RemoveAllAction(false)
		if showAnime then
			comp.Alpha = 0
			local duration = i * 0.03
			DoDelayAction(comp, duration, function( ... )
				local posTarget = Vector2(x - (row + 1) * 8.0, y)
				comp.X = x + 60
				DoFadeAction(comp, 0.30)
				DoMoveAction(comp, posTarget, 0.15, function( ... )
					-- posTarget = Vector2(x, y)
					-- DoMoveAction(comp, posTarget, 0.038 * (row + 1))
					LoopMoveCb(comp, row, x)
				end)
			end)
		else
			comp.Position2D = Vector2(x, y)
		end
	end
end

local function OnFunctionOpen( eventname, params )
	if params.data.menu_type == 2 then
		ReSort()
	end
end

local function OnFunctionMenuShow(eventname, params)
	-- print('----------OnFunctionMenuShow------------')
	if self.switchBtn == nil then
		self.switchBtn = HudManager.Instance:GetHudUI("MainHud"):FindChildByEditName("tbt_an1", true)
	end
	
	if self.root.Visible ~= params.isShow then
		self.root.Visible = params.isShow
		if params.isShow then
			ResetIcon(params.showAnime)
		end
		HudManager.Instance.SkillBar.Visible = not params.isShow
	elseif params.reset then
		ResetIcon(params.showAnime)
	end
	if self.switchBtn.IsChecked ~= params.isShow then
		self.switchBtn.IsChecked = params.isShow
	end
end


local function OnInitUI( ... )
	local m_root = HudManager.Instance:AddHudUIFromXml("xml/hud/ui_hud_mainmenu.gui.xml", "FunctionHud")
	self.root = m_root
	self.root.Visible = false
	m_root.Enable = false
	HudManager.Instance:InitAnchorWithNode(m_root, bit.bor(HudManager.HUD_RIGHT, HudManager.HUD_TOP))
	self.originComp = self.root:FindChildByEditName("cvs_role", true)
	self.defaultPos = self.originComp.Position2D
	InitUI()
	ReSort()
end

--重登录回到标题画面时调用
local function fin()
	-- print ("FunctionMenu fin ")
	EventManager.Unsubscribe(Events.UI_HUD_LUAHUDINIT, OnInitUI)
	EventManager.Unsubscribe("Event.Hud.ShowFunctionMenu", OnFunctionMenuShow)
	EventManager.Unsubscribe("Event.FunctionOpen.FuncOpen", OnFunctionOpen)
	self.root = nil
	self.switchBtn = nil
end

--退出场景时调用，参数：是否短线重连触发的切场景
local function OnExitScene(reconnect)
	-- print (" FunctionMenu OnExitScene ", reconnect)
end

--进入场景时调用
local function OnEnterScene()
	-- print (" FunctionMenu OnEnterScene ")
end

--登录游戏后首次进入场景时调用
local function initial()
	-- print ("FunctionMenu initial ")
	EventManager.Subscribe(Events.UI_HUD_LUAHUDINIT, OnInitUI)
	EventManager.Subscribe("Event.Hud.ShowFunctionMenu", OnFunctionMenuShow)
	EventManager.Subscribe("Event.FunctionOpen.FuncOpen", OnFunctionOpen)
end


return { initial = initial, fin = fin, OnEnterScene = OnEnterScene, OnExitScene = OnExitScene }