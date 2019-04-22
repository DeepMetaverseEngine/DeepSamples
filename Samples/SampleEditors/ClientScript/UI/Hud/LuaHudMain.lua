local _M = {}
_M.__index = _M

local TLChatHud = require "UI/Hud/TLChatHud"
local MountHud = require "UI/Hud/MountHud"
local UIUtil = require 'UI/UIUtil'
local SceneModel = require 'Model/SceneModel'
local Util = require 'Logic/Util'
local QuestUtil = require 'UI/Quest/QuestUtil'
local PagodaModel = require 'Model/PagodaModel'
local TargetModel=require 'Model/TargetModel'
local TimeUtil = require 'Logic/TimeUtil'
local ActivityModel = require 'Model/ActivityModel'
local ServerTime = require 'Logic/ServerTime'
local BusinessModel = require 'Model/BusinessModel'
local DisplayUtil = require 'Logic/DisplayUtil'
local MedicineModel = require 'Model/MedicineModel'
local SpringFestivalModel = require 'Model/SpringFestivalModel'

local self = {}
local showlist = {}
local targetEffectId=nil--目标特效id
local IsShowTarget=true
local storyfinishids = nil
local lastSceneid = nil
local isNetMode = true


--目标系统特效
local function InitEffect(assetname, parentCvs, scale, pos)

  	local transSet = {}												--加了2点偏移量
		transSet.Pos = Vector3(parentCvs.Size2D.x/2,-parentCvs.Size2D.y/2, 0)+pos
		transSet.Scale = scale
		transSet.Parent = parentCvs.Transform
		transSet.Layer = Constants.Layer.UI
		transSet.LayerOrder = 5
	local id=Util.PlayEffect(assetname,transSet)
	return id --防止没有及时取到对应的特效物体，返回id

end

--最后一次领取时，让目标面板渐隐，等特效放掉再隐藏
local function DoFadeAction(node,duration,cb)
	local alphaAction = FadeAction()
	alphaAction.TargetAlpha = 0
	alphaAction.Duration = duration
	node:AddAction(alphaAction)
	alphaAction.ActionFinishCallBack = cb
end


--每次升级时调用(多次)
local function OnLevelUp()

	--单机模式下直接返回
	if not isNetMode then
		return
	end

	local cvs_target=self.root:FindChildByEditName("cvs_target", true)
	local lb_level=cvs_target:FindChildByEditName("lb_level", true)
	local lb_get=cvs_target:FindChildByEditName("lb_get", true)
	local ib_item=cvs_target:FindChildByEditName("ib_item", true)
	local curTargetlv = DataMgr.Instance.UserData.TargetLv

	--如果lv<0,表示无目标可领取，界面隐藏，特效释放
	if curTargetlv < 0 then
		IsShowTarget=false
		if targetEffectId ~= nil then
			RenderSystem.Instance:Unload(targetEffectId)
			targetEffectId=nil
		end
		DoFadeAction(cvs_target,2,function()
			cvs_target.Visible=false
		end)
		cvs_target.Enable=false
		return
	else --需判断活动界面是否显示
		cvs_target.Visible=not self.cvs_title.Visible
	end

  	--如果达到目标，显示可领取特效
	if curTargetlv<=DataMgr.Instance.UserData.Level then 
		--加载可领取特效
		lb_level.FontColor = GameUtil.RGB2Color(Constants.Color.Green2)
		if targetEffectId ==nil then --如果特效为空，则加载，否则通过id设置特效显示/隐藏
			targetEffectId= InitEffect('/res/effect/ui/ef_ui_frame_01.assetbundles',cvs_target,Vector3(1.4,1.4,1),Vector3(0,2,0))
		else
			local eff=RenderSystem.Instance:GetAssetGameObject(targetEffectId)
			if eff then
				eff.gameObject:SetActive(true)
			end
		end
	else--未达到目标
		lb_level.FontColor = GameUtil.RGB2Color(Constants.Color.Red)
		if targetEffectId ~=nil then
			local eff=RenderSystem.Instance:GetAssetGameObject(targetEffectId)
				if eff then
					eff.gameObject:SetActive(false)
				else
					RenderSystem.Instance:Unload(targetEffectId)
					targetEffectId=nil
				end
		end
	end
	
	--通过lv，取得数据并赋值
	local target=TargetModel.GetTargetDataByLevel(curTargetlv)
	if target then
		lb_level.Text=target.target_level
		lb_get.Text=Util.GetText(target.name)
		local iconId = target.icon_res
		UIUtil.SetImage(ib_item,iconId,false, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)

		cvs_target.TouchClick=function (sender)
			GlobalHooks.UI.OpenUI('TargetDetail',0,target)
		end
	end
end


local function SetTargetLv(eventname,params)
	DataMgr.Instance.UserData.TargetLv = params.lv
	OnLevelUp()
end


--初始化加载时调用(一次)
local function InitTargetLv(cvs_target)
	--单机模式下直接返回
	if not isNetMode then
		return
	end

	--小于0，代表无目标可领取
	if DataMgr.Instance.UserData.TargetLv<0 then
		IsShowTarget=false
		if targetEffectId ~=nil then 
			RenderSystem.Instance:Unload(targetEffectId)
			targetEffectId=nil
		end
		cvs_target.Visible=false
		return
	else --需判断活动界面是否显示
		cvs_target.Visible=not self.cvs_title.Visible
	end

	local lb_level=cvs_target:FindChildByEditName("lb_level", true)
	local lb_get=cvs_target:FindChildByEditName("lb_get", true)
	local ib_item=cvs_target:FindChildByEditName("ib_item", true)

    --如果达到目标，显示可领取特效
	if DataMgr.Instance.UserData.TargetLv<=DataMgr.Instance.UserData.Level then 
		--加载可领取特效
		lb_level.FontColor = GameUtil.RGB2Color(Constants.Color.Green2)
			if targetEffectId ==nil then --如果为空，则加载，否则通过id设置特效显示/隐藏(登陆游戏，必走这里)
				targetEffectId= InitEffect('/res/effect/ui/ef_ui_frame_01.assetbundles',cvs_target,Vector3(1.4,1.4,1),Vector3(0,2,0))
			else
				local eff=RenderSystem.Instance:GetAssetGameObject(targetEffectId)
				if eff then
					eff.gameObject:SetActive(true)
				end
			end
	else
		lb_level.FontColor = GameUtil.RGB2Color(Constants.Color.Red)
		if targetEffectId ~=nil then
			local eff=RenderSystem.Instance:GetAssetGameObject(targetEffectId)
				if eff then
					eff.gameObject:SetActive(false)
				else
					RenderSystem.Instance:Unload(targetEffectId)
					targetEffectId=nil
				end
		end
	end

	--获得数据并赋值
	local target=TargetModel.GetTargetDataByLevel(DataMgr.Instance.UserData.TargetLv)	
	if target then
		lb_level.Text=target.target_level
		lb_get.Text=Util.GetText(target.name)
		local iconId = target.icon_res
		UIUtil.SetImage(ib_item,iconId,false, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)

		cvs_target.TouchClick=function (sender)
			GlobalHooks.UI.OpenUI('TargetDetail',0,target)
		end
	end
end


local function RefreshMsgIcon()
  local iconRoot = self.root:FindChildByEditName("cvs_hud_message", true)
  if iconRoot ~= nil then
	--扩展的时候修改这里, 添加控件即可
    local iconName = { "cvs_hud_team"}
    local msgData = DataMgr.Instance.MsgData
    local msgCount = {
      msgData:GetMessageCount(AlertMessageType.TeamApply) + msgData:GetMessageCount(AlertMessageType.TeamInvite),
    --   msgData:GetMessageCount(AlertMessageType.MailReceive),
    --   msgData:GetMessageCount(AlertMessageType.FriendInvite),
    --   msgData:GetMessageCount(AlertMessageType.GuildApply),
    }

    --计算整体偏移（根据聊天框位置决定）
    local targetX = self.MsgIconX == nil and 0 or self.MsgIconX
    if targetX ~= 0 then
      local parent = iconRoot.Parent
      local x = 0
      while parent ~= self.root do
        x = x + parent.X
        parent = parent.Parent
      end
      local offx = targetX - x
      iconRoot.X = offx
    else
      iconRoot.X = 0
    end

    --计算每个图标的相对位置
	local x = 0

    for i = 1, #iconName do
      local icon = iconRoot:FindChildByEditName(iconName[i], true)
      if icon ~= nil then
        if msgCount[i] > 0 then
          icon.Visible = true
          icon.X = x
          x = x + icon.Width
        else
          icon.Visible = false
        end
      end
    end
  end
end

local function InitMessageIcon()
	local iconRoot = self.root:FindChildByEditName("cvs_hud_message", true)
	if iconRoot ~= nil then

		-- 组队
		local team = iconRoot:FindChildByEditName("cvs_hud_team", true)
		if team then
			team.TouchClick = function()
				-- DataMgr.Instance.MsgData:ShowTeamAlert()
				GlobalHooks.UI.OpenUI('TeamApply', 0)
			end
		end

		-- 邮件
		local mail = iconRoot:FindChildByEditName("cvs_hud_mail", true)
		if mail then
			mail.TouchClick = function()
				-- 打开邮件界面
				GlobalHooks.UI.OpenUI('MailMain', 0)
				-- 删除邮件图标
				-- DataMgr.Instance.MsgData:RemoveList(AlertMessageType.MailReceive)
			end
		end

		-- 公会
		local guild = iconRoot:FindChildByEditName("cvs_hud_guild", true)
		if guild then
			guild.TouchClick = function()
				-- DataMgr.Instance.MsgData:ShowSimpleAlert(AlertMessageType.FriendInvite)
			end
		end

		RefreshMsgIcon()
	end
end


--设置经验条
local function SetEXPUI()
	local expGauge = self.expGauge
	if expGauge ~= nil then
		local curexp = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.EXP,0)
		local needexp = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.NEEDEXP,0)

		local percentage
		if needexp == 0 then
			percentage = 0
		else
			percentage = (curexp/needexp > 1) and 1 or (curexp/needexp)
		end

		expGauge.Value = percentage * 100

		self.curexp = curexp
		self.needexp = needexp
		self.curValue = percentage * 100

		-- self.lasetExp = curexp
		-- self.lastNeedExp = needexp
	end
end

local function UpdateEXPUI()
 
	local expGauge =  self.expGauge
	-- print('5555555555555555555555555')
	if expGauge ~= nil then

		local curexp = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.EXP,0)
		local needexp = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.NEEDEXP,0)

		if self.curexp == curexp and self.needexp == needexp then
			return
		end

		local percentage
		if needexp == 0 then
			percentage = 0
		else
			percentage = (curexp/needexp > 1) and 1 or (curexp/needexp)
		end

		local curValue = self.curValue
		local nextValue = percentage * 100

		if needexp > self.needexp then

			local updateValue = 100 - curValue
			if updateValue == 0 then
				return
			end

			local setp = (updateValue + nextValue) / 500 * 33

			local addMax = false
			if self.expTimeId ~= nil then
				LuaTimer.Delete(self.expTimeId)
				self.expTimeId = nil
			end
			self.expTimeId = LuaTimer.Add(0,33,function(id)
				-- print('2222222222222222222222222')
				if addMax and expGauge.Value >= nextValue then
					-- print('expGauge.Value 111111111111111111111111111111111111111111111111111111111111111111:',expGauge.Value,'curexp:',curexp,'needexp:',needexp,'nextValue:',nextValue)
					return false
				end

				if expGauge.Value >= 100 then
					expGauge.Value = 0
					addMax = true
				end

				local  expValue = expGauge.Value + setp
				if expValue > 100 then
					expValue = 100
				elseif addMax and expValue >= nextValue then
					expValue = nextValue
					expGauge.Value = expValue
					return false
				end

				expGauge.Value = expValue

				return true
			end)
		else

			local updateValue = nextValue - curValue

			if updateValue == 0 then
				return
			end

 			local setp = updateValue / 500 * 33

 			if self.expTimeId ~= nil then
				LuaTimer.Delete(self.expTimeId)
				self.expTimeId = nil
			end
			self.expTimeId = LuaTimer.Add(0,33,function(id)
				-- print('3333333333333')
				if expGauge.Value > nextValue then
					-- print('expGauge.Value 111111111111111111111111111111111111111111111111111111111111111111:',expGauge.Value,'curexp:',curexp,'needexp:',needexp,'nextValue:',nextValue)
					return false
				end

				local  expValue = expGauge.Value + setp
				if expValue >= 100 then
					expValue = 100
				elseif expValue >= nextValue then
					expValue = nextValue
					expGauge.Value = expValue
					return false
				end
				expGauge.Value = expValue

				return true
			end)
		end

		self.curexp = curexp
		self.needexp = needexp
 		self.curValue = nextValue

	end
end

local function ShowMoShiUI()

	local cur = self.cvs_moshi.Visible

	if cur == true then
	 self.cvs_moshi.Visible = not cur
		return
	end

	-- 重置状态
	self.moshi_team.IsChecked = false
	self.moshi_peace.IsChecked = false
	self.moshi_jus.IsChecked = false
	self.moshi_guild.IsChecked = false
	self.moshi_rev.IsChecked = false
	-- 当前模式高亮

    local mode = TLBattleScene.Instance.Actor:GetPKMode()

    if mode == PKInfo.PKMode.Peace then
    	self.moshi_peace.IsChecked = true
   	elseif mode == PKInfo.PKMode.Team then
		self.moshi_team.IsChecked = true
	elseif mode == PKInfo.PKMode.Justice then
		self.moshi_jus.IsChecked = true
	elseif mode == PKInfo.PKMode.Revenger then
		self.moshi_rev.IsChecked = true
	elseif mode == PKInfo.PKMode.Guild then
		self.moshi_guild.IsChecked = true
	end

	-- 界面显示
	self.cvs_moshi.Visible  = true

end

local function CloseMoShiUI()
	-- 界面显示
	self.cvs_moshi.Visible  = false
end

--发送切换PK模式请求.
local function SendPKModeChangeRequest(targetmode)
	--检查当前场景能否改变PK模式，如果不行提示.
	local id = DataMgr.Instance.UserData.MapTemplateId
	local mapdata = GlobalHooks.DB.Find('MapData', id)
	if mapdata ~= nil and mapdata.change_pk == 0 then
		GameAlertManager.Instance:ShowFloatingTips(Util.GetText('scene_not_change_pk'))
		CloseMoShiUI()
		return
	end


	local mode = TLBattleScene.Instance.Actor:GetPKMode()
		if mode == targetmode then
			CloseMoShiUI()
		else
			SceneModel.RequestChangePKMode(targetmode,
			function(rsp)
				CloseMoShiUI()
			end
		)
		end

end

local function CloseChangeSceneUI()
	self.cvs_changeScene.Visible  = false
end

local function OpenChangeSceneUI()
	local cur = self.cvs_changeScene.Visible 

	if cur == true then
	 self.cvs_changeScene.Visible = not cur
		return
	end

	SceneModel.RequestGetZoneInfoSnaps(
		function(rsp)
			local list = rsp.s2c_snaps
		    self.attrNode = self.attrNode or UIUtil.CreateCacheNodeGroup(self.tbt_moban, true)
		    self.attrNode:SetInitCB(
				 function(node, preNode)
					if preNode then
						node.Y = preNode.Y + preNode.Height + 10
					end
        		 end
		    )
		    
  			if list == nil then
  				CloseChangeSceneUI()
		    	return
		    end

		    local lineList = {}
		    local lineCount = 0
		    --筛选服务器列表.
		    for a,b in ipairs(list) do
		    	if lineCount < GameUtil.GetIntGameConfig("scene_linemax") and b.curPlayerCount < b.playerMaxCount then 
		    		table.insert(lineList,b)
		    		--print("line snap : ",b.lineIndex)
		    		lineCount = lineCount + 1
		    	end

		    end



		    self.attrNode:Reset(#lineList)
		    self.cvs_changeScene.Height = (self.tbt_moban.Height + 10) * #lineList + 10
		    local ret = self.attrNode:GetVisibleNodes()
		    for i, v in ipairs(ret) do
        		--拼中文.
        		local snap = lineList[i]
        		local line = Util.GetText('common_line',snap.lineIndex)
        		--判断颜色.
				v.Text = line

				if snap.curPlayerCount < snap.playerFullCount then
						v.FontColor = GameUtil.RGB2Color(0x39b72b)--绿
						v.FocuseFontColor = v.FontColor
				elseif snap.curPlayerCount < snap.playerMaxCount then
						v.FontColor = GameUtil.RGB2Color(0xff8400)--橙
						v.FocuseFontColor = v.FontColor
				else
						v.FontColor = GameUtil.RGB2Color(0xfb1919)--红
						v.FocuseFontColor = v.FontColor
				end

        		v.TouchClick = function (sender)
        			if lineList[i].uuid ~= rsp.s2c_curZoneUUID then --选择其他线时
        				--print("curZoneUUID: "..rsp.s2c_curZoneUUID)
        				--print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"..list[i].uuid)
        				SceneModel.RequestChangeZoneLine(lineList[i].uuid,
        					function (rsp)
								CloseChangeSceneUI()
        					end)
        			
        			else
        				CloseChangeSceneUI()
        			end
        		end
    		end
			--打印日志信息zoneinfoSnapList
			--for i,v in pairs(list) do
			--	print("line:"..v.lineIndex)
			--	print("players:"..v.curPlayerCount)
			--	print("playerMaxCount"..v.playerMaxCount)
			--	print("uuid:"..v.uuid)
			--end
		end
		)

	self.cvs_changeScene.Visible = true
end


local function GetActivityInfo()
	local detail = {}
	local opentime = GlobalHooks.DB.GetFullTable('Function_OpenTimeData')
	for _,v in pairs(opentime) do
		if v.open_type == 2 or v.open_type == 3 then
			table.insert(detail,v)
		end
	end
	return detail
end

local function GetActivityData(functionid)
	local detail = unpack(GlobalHooks.DB.Find('ActivityPushData', {function_id = functionid}))
	return detail
end

local function GetShowData(opentime)
	showlist = {}
	local function IsShowToday(functionid)
		local pushdata = GetActivityData(functionid)
		if pushdata then
			for i,v in ipairs(pushdata.time.open) do
				if v ~= '' then
					local endTime = TimeUtil.CustomTodayTimeToUtc(pushdata.time.close[i]..':00')
					local delaytimeEnd = TimeUtil.TimeLeftSec(endTime)
					if delaytimeEnd < 0 then
						local temp = {function_id = pushdata.function_id,
									  timepos = i,
									  pushtime = v,
									  endtime = pushdata.time.close[i],
									  noticestartid = pushdata.time.open_notice[i],
									  noticecloseid = pushdata.time.close_notice[i],
									  priority = pushdata.push_priority,
									  level = pushdata.player_lv
						}
						table.insert(showlist,temp)
					end 
				end
			end
		end
	end
	for _,v in pairs(opentime) do
		local _,temp = FunctionUtil.CheckNowIsOpen(v.function_id)
		if temp ~= -1 then
			IsShowToday(v.function_id)
		end
	end
	table.sort(showlist,function(a,b)
		return a.pushtime < b.pushtime
	end)
	for _,v in pairs(showlist) do
		v.pushtime = TimeUtil.CustomTodayTimeToUtc(v.pushtime..':00')
		v.endtime = TimeUtil.CustomTodayTimeToUtc(v.endtime..':00')
	end
end

local function IsOutdate(data)
	local delaytimeend = TimeUtil.TimeLeftSec(data.endtime)
	if delaytimeend < 0 then
		return false
	else
		return true
	end
end

local function SetPushCvs(node,showlist)
	local activityinfo = unpack(GlobalHooks.DB.Find('ActivityData', {function_id = showlist.function_id}))
	local opentime = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', {function_id = showlist.function_id}))
	local lb_name = node:FindChildByEditName('lb_name', true)
	local lb_time = node:FindChildByEditName('lb_time', true)
	local ib_icon = node:FindChildByEditName('ib_icon', true)
	lb_name.Text = Util.GetText(activityinfo.activity_name)
	local _,_,timepos = ActivityModel.OpenTime(opentime)
	lb_time.Text = opentime.time.open[timepos]
	UIUtil.SetImage(ib_icon, activityinfo.activity_icon)
	node.TouchClick = function(sender)
		if GlobalHooks.UI.FindUI('ActivityPushMain') == nil then
			GlobalHooks.UI.OpenUI('ActivityPushMain', 0,showlist)
		end
	end
end

local function IsInTime(data)
	local delaytimepush = TimeUtil.TimeLeftSec(data.pushtime)
	local delaytimeend = TimeUtil.TimeLeftSec(data.endtime)
	if delaytimepush/delaytimeend < 0 then
		return true
	end
	return false
end

local function Heighpriority()
	local temp = nil
	for k,v in pairs(showlist) do
		if IsInTime(v) then
			if v.noticestartid and v.noticestartid ~= 0 then
				SceneModel.BroadCastNotice(v.noticestartid)
				v.noticestartid = 0
			end
			if temp == nil then
				temp = v
			else
				if temp.priority > v.priority then
					temp = v
				end
			end
		end
	end
	return temp
end

local function UpdateTime(node)
	local functionid = nil
	local timepos = 1
	node.Visible = false
	self.timer1 = LuaTimer.Add(0,1000,function()
		if #showlist == 0 then
			LuaTimer.Delete(self.timer1)
			node.Visible = false
			
			--如果不显示推送，则判断是否显示目标
			if IsShowTarget and self.cvs_target.Visible==false then
				self.cvs_target.Visible=true
			end

			return false
		end
		if IsOutdate(showlist[1]) then
			if showlist[1].noticecloseid and showlist[1].noticecloseid ~= 0 then
				SceneModel.BroadCastNotice(showlist[1].noticecloseid)
			end
			table.remove(showlist,1)
			functionid = nil
		else
			local temp = Heighpriority()
			if temp then
				if temp.function_id ~= functionid or timepos ~= temp.timepos then
					if DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0) < temp.level then
						node.Visible = false

						--如果不显示推送，则判断是否显示目标
						if IsShowTarget and self.cvs_target.Visible==false then
							self.cvs_target.Visible=true
						end
						
					else
						node.Visible = true
						
						--如果推送显示，则隐藏目标
						if self.cvs_target.Visible==true then
							self.cvs_target.Visible=false
						end

						SetPushCvs(node,temp)
						functionid = temp.function_id
						timepos = temp.timepos
					end
				end
			else
				node.Visible = false

				--如果不显示推送，则判断是否显示目标
				if IsShowTarget and self.cvs_target.Visible==false then
					self.cvs_target.Visible=true
				end
			end
		end
		return true
	end)
end

local function AnalysisActivity(node)
	if self.timer1 then
		LuaTimer.Delete(self.timer1)
	end
	node.Visible = false
	local opentime = GetActivityInfo()
	GetShowData(opentime)
	UpdateTime(node)
end

local function OnChangeSceneFinish()
	local sceneid = DataMgr.Instance.UserData.MapTemplateId
	if sceneid == 0 then
		sceneid = GameGlobal.Instance.SceneID
	end
	lastSceneid = sceneid
	--显示当前线
	self.btn_changeScene.Text = Util.GetText('common_line',DataMgr.Instance.UserData.ZoneLineIndex)
	CloseChangeSceneUI()
	if #showlist == 0 then
		AnalysisActivity(self.cvs_title)
	end
end

local function AdaptiveExpGaugeX( self )
	local expGauge = self.root:FindChildByEditName("gg_exp", true)
	self.expGauge = expGauge
	-- local root = HZUISystem.Instance.RootRect
 --    local scale = root.width > HZUISystem.SCREEN_WIDTH and root.width / HZUISystem.SCREEN_WIDTH or root.height / HZUISystem.SCREEN_HEIGHT        
 --    local mMaskW = expGauge.Width * scale
 --    expGauge.Scale = Vector2(scale, 1)
    
end  


local function InitUI( ... )

	print("--------------InitUI---------------")
	local btn_bag = self.root:FindChildByEditName("btn_beibao", true)
	btn_bag.TouchClick = function( sender )
		-- MenuMgr.Instance:RemoveCacheUIByTag('Test',200)
		-- package.loaded['/ui_edit/lua/UI/Test/TestUI.lua'] = nil
		-- package.loaded['/ui_edit/lua/UI/UIUtil.lua'] = nil
		-- GlobalHooks.UI.OpenUI('Test', 0)
		GlobalHooks.UI.OpenUI('BagMain', 0)
	end
	-- end

	--拍卖行
	local btn_paimai = self.root:FindChildByEditName("btn_paimai", true)
	btn_paimai.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI('AuctionMain', 0, "AuctionList")
	end

	local btn_jiaohu = self.root:FindChildByEditName("btn_jiaohu", true)
	btn_jiaohu.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI('SocialMain', 0, "SocialFriend")
	end

	-- 邮件
	local mailModel = require 'Model/MailModel'
	if isNetMode then
		mailModel.CheckMailNum()
	end
	local btn_youjian = self.root:FindChildByEditName("btn_youjian", true)
	btn_youjian.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI('MailMain', 0)
	end

	local btn_yuyin = self.root:FindChildByEditName("btn_yuyin", true)
	btn_yuyin.TouchClick = function( sender )

	end

	--充值
	local btn_recharge=self.root:FindChildByEditName("btn_recharge",true)
	btn_recharge.TouchClick=function ( sender )
		GlobalHooks.UI.OpenUI('Recharge', 0, 'RechargeShop')
	end
	
	--等级封印
	local btn_fengyin =self.root:FindChildByEditName('btn_fengyin',true)
	btn_fengyin.TouchClick=function(sender)
		GlobalHooks.UI.OpenUI('LevelSeal', 0)
	end
	
	--七日活动
	local btn_sevenday = self.root:FindChildByEditName('btn_sevenday',true)
	btn_sevenday.TouchClick =function(sender)
		GlobalHooks.UI.OpenUI('SevenDay',0)
	end

	--情人节活动
	local btn_valentinesday = self.root:FindChildByEditName('btn_valentinesday',true)
	btn_valentinesday.TouchClick =function(sender)
		GlobalHooks.UI.OpenUI('CPActivityFrame',0)
	end
	
	--新春活动
	local btn_springfestival = self.root:FindChildByEditName('btn_springfestival',true)
	btn_springfestival.TouchClick =function(sender)
		--打开主页面判断活动是否开启或结束
		local isOpen = TimeUtil.inTime(System.DateTime.Parse(SpringFestivalModel.SpringFestivalOpenTime),
				System.DateTime.Parse(SpringFestivalModel.SpringFestivalCloseTime),
				ServerTime.getServerTime():ToLocalTime())
		if isOpen then
			GlobalHooks.UI.OpenUI('SpringFestivalMain',0)
		else
			EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_springfestival', showIcon = false})
			GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.SpringFestival.ActivityEnd))
		end
	end
	
	--首充奖励
	local btn_firstrecharge=self.root:FindChildByEditName('btn_firstrecharge',true)
	local cvs_firstrecharge=self.root:FindChildByEditName('cvs_firstrecharge',true)
	if isNetMode then
		if GlobalHooks.IsFuncOpen('Recharge_First') then
			Protocol.RequestHandler.ClientGetFirstRechargeInfoRequest({}, function(rsp)
				if rsp ~= nil then
					if rsp.s2c_close == true then
						EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_firstrecharge', showIcon = false})
					else
						EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_firstrecharge', showIcon = true})
					end
				end
			end)
			btn_firstrecharge.TouchClick=function(sender)
				GlobalHooks.UI.OpenUI('FirstRecharge', 0)
			end
		end
	end

	--变强
	local btn_powerbook=self.root:FindChildByEditName("btn_powerbook",true)
	btn_powerbook.TouchClick=function (sender)
		GlobalHooks.UI.OpenUI('BeStrongMain',0)
	end

    --副本
    local btn_fuben=self.root:FindChildByEditName("btn_fuben",true)
     btn_fuben.TouchClick=function (sender)
    	GlobalHooks.UI.OpenUI('DungeonMain', 0)
    end

	--活动
	local btn_huodong=self.root:FindChildByEditName("btn_huodong",true)
	btn_huodong.TouchClick=function ( sender )
		 GlobalHooks.UI.OpenUI('ActivityMain',0)
	end

	--福利
	local btn_fuli=self.root:FindChildByEditName("btn_fuli",true)
	btn_fuli.TouchClick=function ( sender )
		GlobalHooks.UI.OpenUI('BusinessFrame',0)
	end

	--开服庆典
	local btn_newopen=self.root:FindChildByEditName("btn_newopen",true)
	btn_newopen.TouchClick=function ( sender )
		GlobalHooks.UI.OpenUI('NewOpenFrame',0)
	end

	--商店
	-- local btn_store = self.root:FindChildByEditName("btn_store", true)
	-- btn_store.TouchClick = function(sender)
	-- 	GlobalHooks.UI.OpenUI('Shop', 0, 2)
	-- end

	local btn_getBack = self.root:FindChildByEditName("btn_getback", true)
	btn_getBack.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('GetBackFrame',0)
	end

	--活动推送
	local cvs_title = self.root:FindChildByEditName("cvs_title", true)
	self.cvs_title = cvs_title

	--目标系统
	local cvs_target=self.root:FindChildByEditName("cvs_target", true)
	InitTargetLv(cvs_target)
	self.cvs_target = cvs_target

	--世界BOSS
	local btn_store = self.root:FindChildByEditName("btn_boss", true)
	btn_store.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('WorldBossMain', 0)
	end

	--排行榜
	local btn_ranklist = self.root:FindChildByEditName("btn_ranklist", true)
	if btn_ranklist then
		btn_ranklist.TouchClick = function(sender)
			GlobalHooks.UI.OpenUI("RankMain",0)
		end
	end

	--委托任务
	local btn_weituo = self.root:FindChildByEditName("btn_weituo", true)
	btn_weituo.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('ActivityEntrust', 0)
	end

	--太虚幻境
	local btn_taixu = self.root:FindChildByEditName("btn_taixu", true)
	btn_taixu.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('TaiXuMain', 0)
	end

	--材料秘境
	local btn_hexin = self.root:FindChildByEditName("btn_hexin", true)
	btn_hexin.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('DailyDungeonMain', 0)
	end

	--战场
	local btn_zhanchang = self.root:FindChildByEditName("btn_zhanchang", true)
	btn_zhanchang.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('BattleGround', 0)
	end

	--师门
	local btn_shenfen = self.root:FindChildByEditName("btn_shenfen", true)
	btn_shenfen.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('PlayRuleMain', 0)
	end

	--镇妖塔
	local btn_pagoda = self.root:FindChildByEditName("btn_pagoda", true)
	btn_pagoda.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('PagodaMain', 0)
	end

	--社区
	self.cvs_shequ = self.root:FindChildByEditName("cvs_shequ", true)
	local btn_shequ = self.cvs_shequ:FindChildByEditName("btn_shequ", true)
	--btn_shequ.Visible = PublicConst.OSType == 5
	btn_shequ.Visible = true
	btn_shequ.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('CommunityMain', 0)
	end

	local cvs_menu2=self.root:FindChildByEditName("cvs_menu2",true)
 	HudManager.Instance:InitAnchorWithNode(cvs_menu2,bit.bor(HudManager.HUD_RIGHT,HudManager.HUD_TOP)) 
	--自动战斗
	--local btn_autoGuard = self.root:FindChildByEditName("tbt_zidong",true)
	--btn_autoGuard.IsChecked = TLBattleScene.Instance.Actor:SyncAutoGuardState()
	--btn_autoGuard.TouchClick = function ( sender )
	--	TLBattleScene.Instance.Actor:SetAutoGuard(btn_autoGuard.IsChecked)
	--end


	--聊天框
	local cvs_chat = self.root:FindChildByEditName("cvs_chat", true)
	HudManager.Instance:InitAnchorWithNode(cvs_chat, bit.bor(HudManager.HUD_BOTTOM))
	if GameUtil.IsIPhoneX() then
		local cvs_chat2 = self.root:FindChildByEditName("cvs_flexible", true)
		cvs_chat2.Y = cvs_chat2.Y - (HZUISystem.Instance.IPhoneXOffY - 10)
		local cvs_anniu = self.root:FindChildByEditName("cvs_anniu", true)
		cvs_anniu.Y = cvs_anniu.Y - (HZUISystem.Instance.IPhoneXOffY - 10)
	end

	self.chatHud = TLChatHud.Bind(self.root:FindChildByEditName("cvs_flexible", true))
	local btn_shezhi = self.root:FindChildByEditName('btn_shezhi', true)
	btn_shezhi.TouchClick = function(  )
		GlobalHooks.UI.OpenUI('ChatHudSetting', 0)
	end

	--坐骑按钮
	local btn_zuoqi = self.root:FindChildByEditName("btn_zuoqi", true)
	self.mountHud = MountHud.Bind(btn_zuoqi)
	HudManager.Instance:InitAnchorWithNode(btn_zuoqi, bit.bor(HudManager.HUD_LEFT, HudManager.HUD_BOTTOM))
	-- btn_zuoqi.TouchClick = function(sender)
	-- 	-- GlobalHooks.UI.OpenUI('StoreMain', 0,'Shop')

	--经验条
	local cvs_exp = self.root:FindChildByEditName("cvs_exp", true)

	HudManager.Instance:InitAnchorWithNode(cvs_exp, bit.bor(HudManager.HUD_BOTTOM))
	DisplayUtil.adaptiveFullSceenX(cvs_exp)
	AdaptiveExpGaugeX(self)

	SetEXPUI()
	
	--电池电量信息
	local cvs_xinxi = self.root:FindChildByEditName("cvs_xinxi", true)
	HudManager.Instance:InitAnchorWithNode(cvs_xinxi, bit.bor(HudManager.HUD_LEFT, HudManager.HUD_TOP))
	
	--电量5分钟刷新一次
	local battery=cvs_xinxi:FindChildByEditName("gg_dian", true)
	self.batteryTime=LuaTimer.Add(
		0,
		300000,
		function()
			battery.Value=PlatformMgr.GetBatteryLeftQuantity()
		return true
	end)

	local time=cvs_xinxi:FindChildByEditName("lb_shijian", true)
	local wifi=cvs_xinxi:FindChildByEditName("ib_wifi", true)
	local mobile=cvs_xinxi:FindChildByEditName("ib_4G", true)
	--时间与网络每分钟刷新一次
	self.time=LuaTimer.Add(
		0,
		60000,
		function()
			time.Text=GameUtil.FormatDateTime(ServerTime.getServerTime():ToLocalTime(),"HH:mm")
			local data=PlatformMgr.PluginGetNetworkStatus()
			if data =='2G' or data=='3G' or data=='4G' then 
				mobile.Visible=true
				wifi.Visible=false
			elseif data =='Wi-Fi' then 
				mobile.Visible=false
				wifi.Visible=true
			else --其他情况
				mobile.Visible=false
				wifi.Visible=true
			end
		return true
	end)	
	
	--功能菜单切换按钮
	self.cvs_target_title = self.root:FindChildByEditName("cvs_target_title", true)
	self.funcCvs = self.root:FindChildByEditName("cvs_shanzi", true)
	self.funcBtn = self.root:FindChildByEditName("tbt_an1", true)
	self.funcBtn.Selected = function(sender)
		EventManager.Fire("Event.Hud.ShowFunctionMenu", { isShow = sender.IsChecked, showAnime = true })
		self.cvs_target_title.Visible = not sender.IsChecked
	end

	--右上角那一堆图标通用适配
	self.cvs_topright=self.root:FindChildByEditName("cvs_topright", true)
	HudManager.Instance:InitAnchorWithNode(self.cvs_topright, bit.bor(HudManager.HUD_RIGHT, HudManager.HUD_TOP))
	--右下角适配
	self.cvs_bottomright=self.root:FindChildByEditName("cvs_bottomright", true)
	HudManager.Instance:InitAnchorWithNode(self.cvs_bottomright, bit.bor(HudManager.HUD_RIGHT, HudManager.HUD_BOTTOM))

	self.cvs_youjian=self.root:FindChildByEditName("cvs_youjian", true)
	self.cvs_fengyin =self.root:FindChildByEditName('cvs_fengyin',true)
	
	self.cvs_wardrobe=self.root:FindChildByEditName("cvs_wardrobe", true)
	self.btn_wardrobe = self.root:FindChildByEditName("btn_wardrobe", true)
	self.btn_wardrobe.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI("WardrobeMain",0)
	end

	-- 活动板块折叠框
	self.tbt_tophud = self.root:FindChildByEditName("tbt_an2", true)
	self.cvs_menuhud = self.root:FindChildByEditName('cvs_menuhud', true)
	self.tbt_tophud.TouchClick = function(sender)
		self.cvs_menuhud.Visible = sender.IsChecked
		if sender.IsChecked then
			self.PlayTopIconAnime(true)
		end
	end

	self.cvs_beibao = self.root:FindChildByEditName("cvs_beibao", true)
	
	-- PK模式切换
	-- 模式选择界面
	self.cvs_moshi = self.root:FindChildByEditName("cvs_moshi", true)
	self.cvs_moshi.Visible = false

	-- 和平模式按钮
	self.moshi_peace = self.root:FindChildByEditName("tbt_peace", true)
	self.moshi_peace.TouchClick = function(sender)
		SendPKModeChangeRequest(PKInfo.PKMode._Peace)
	end

	-- 组队模式
	self.moshi_team = self.root:FindChildByEditName("tbt_team", true)
	self.moshi_team.TouchClick = function(sender)
		SendPKModeChangeRequest(PKInfo.PKMode._Team)
	end

	 --善恶模式
	self.moshi_jus = self.root:FindChildByEditName("tbt_jus", true)
	self.moshi_jus.TouchClick = function(sender)
		SendPKModeChangeRequest(PKInfo.PKMode._Justice)
	end

	-- 公会模式
	self.moshi_guild = self.root:FindChildByEditName("tbt_guild", true)
	self.moshi_guild.TouchClick = function(sender)
		SendPKModeChangeRequest(PKInfo.PKMode._Guild)
	end

	--复仇模式
	self.moshi_rev = self.root:FindChildByEditName("tbt_rev", true)
	self.moshi_rev.TouchClick = function(sender)
		SendPKModeChangeRequest(PKInfo.PKMode._Revenger)
	end

	self.cvs_yuyinqie = self.root:FindChildByEditName("cvs_yuyinqie", true)
	self.cvs_yuyinqie.TouchClick = function(sender)
		--模式按钮点击，展开下拉菜单
		--GlobalHooks.UI.OpenUI('SmithyMain', 0, 'SmithyStrengthen')
	end

	self.btn_moshi = self.root:FindChildByEditName("btn_moshi", true)
		self.btn_moshi.TouchClick = function(sender)
		--模式按钮点击，展开下拉菜单
		ShowMoShiUI()
	end
	--自动战斗
	self.btn_autoGuard = self.root:FindChildByEditName("tbt_zidong",true)
	local cvs_zidong = self.root:FindChildByEditName("cvs_zidong", true)
	HudManager.Instance:InitAnchorWithNode(cvs_zidong, bit.bor(HudManager.HUD_BOTTOM))

	self.btn_autoGuard.TouchClick = function ( sender )
		--自动战斗是否开放.
		if not GlobalHooks.IsFuncOpen("AutoBattle",true) then
			self.btn_autoGuard.IsChecked = false
		end
		--地图是否允许自动战斗.
		local id = DataMgr.Instance.UserData.MapTemplateId
		local mapdata = GlobalHooks.DB.Find('MapData', id)
		if mapdata ~= nil and mapdata.auto_fight == 4 then
			self.btn_autoGuard.IsChecked = false
			GameAlertManager.Instance:ShowFloatingTips(Util.GetText('scene_not_auto_fight'))
			return
		end

		TLBattleScene.Instance.Actor:BtnSetAutoGuard(self.btn_autoGuard.IsChecked)
	end

	if GameGlobal.Instance.netMode then
		local temp =unpack(GlobalHooks.DB.Find('MedicinePool', {level = DataMgr.Instance.UserData.Level}))
		self.medicLimit = temp.limit or MedicineModel.MedicineLimitCount
	end

	--自动吃药设置
	local function Play3DEffect(parentCvs, menuOrder, fileName)
		local transSet = TransformSet()
		transSet.Pos = Vector3(0,0,200)
		transSet.Scale = Vector3(0.3, 0.3, 0.3)
		transSet.Parent = parentCvs.Transform
		transSet.Layer = Constants.Layer.UI
		transSet.LayerOrder = menuOrder
		self.model = RenderSystem.Instance:LoadGameObject(fileName,transSet,function (aoe)
			self.effobj=aoe.gameObject:GetComponent("Slider")
			self.effobj.value=(tonumber(DataMgr.Instance.UserData.MedicinePoolCurCount/self.medicLimit))
		end)
	end

	if GameGlobal.Instance.netMode then
		local btn_automedicine = self.root:FindChildByEditName("btn_morehp",true)
		local cvs_morehp=self.root:FindChildByEditName("cvs_morehp",true)
		local cvs_anchor=cvs_morehp:FindChildByEditName('cvs_anchor',true)
		HudManager.Instance:InitAnchorWithNode(cvs_morehp, bit.bor(HudManager.HUD_TOP,HudManager.HUD_LEFT))
		Play3DEffect(cvs_anchor,
				5,
				'/res/effect/ui/ef_ui_bloodpool_01.assetbundles')
		btn_automedicine.TouchClick = function(sender)
			--打开吃药界面.
			GlobalHooks.UI.OpenUI('MedicinePoolMain', 0)
		end
	end

     self.cvs_jiaohu = self.root:FindChildByEditName("cvs_jiaohu", true)

	------------------------------------------------------------------------------------------------------------
    --切线功能.
    self.cvs_changeScene = self.root:FindChildByEditName("cvs_xian",true)
	self.tbt_moban = self.root:FindChildByEditName("tbt_xian1",true)
	self.btn_changeScene = self.root:FindChildByEditName("btn_xian", true)
	self.btn_changeScene.TouchClick = function(sender)
		--切线按钮，展开下拉菜单

		--检查当前场景能否改变PK模式，如果不行提示.
	local id = DataMgr.Instance.UserData.MapTemplateId
	local mapdata = GlobalHooks.DB.Find('MapData', id)
	if mapdata ~= nil and mapdata.is_changeline == 0 then
		GameAlertManager.Instance:ShowFloatingTips(Util.GetText('scene_not_change_line'))
		return
	end
		OpenChangeSceneUI()
	end

	--加载活动推送特效
	if self.pusheff then
		RenderSystem.Instance:Unload(self.pusheff)
	end

	self.pusheff = InitEffect("/res/effect/ui/ef_ui_activity_prompt.assetbundles",
			self.cvs_title,Vector3.one,Vector3(6,0,0)
			)
	------------------------------------------------------------------------------------------------------------
	self.cvs_debufflist = self.root:FindChildByEditName("cvs_debufflist",true)
	self.debufficon = self.root:FindChildByEditName("gg_debufficon",true)
	self.cvs_debufflist.Visible = true
	self.debufficon.Visible = false
	self.debuflist = {}	--初始化消息图标
	InitMessageIcon()
	self.autoBattleImg = self.root:FindChildByEditName("ib_zidongzhandou",true)
	self.autoBattleImg.Layout = nil
	self.autoRunImg = self.root:FindChildByEditName("ib_zidongxunlu",true)
	self.autoRunImg.Layout = nil

	local cvs_carriage = self.root:FindChildByEditName("cvs_carriage",true)
	if cvs_carriage then
		HudManager.Instance:InitAnchorWithNode(cvs_carriage, bit.bor(HudManager.HUD_LEFT))
	end
end

--自动战斗按钮特效
local function PlayAutoFightEffect(self,parentCvs, menuOrder, fileName)
	local transSet = TransformSet()
	transSet.Pos = Vector3(parentCvs.Width/2,-parentCvs.Height/2,0)
	transSet.Parent = parentCvs.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = menuOrder
	RenderSystem.Instance:LoadGameObject(fileName,transSet,function (aoe)
		self.autofight=aoe.gameObject
		self.autofight:SetActive(false)
	end)
end

local function OnInitAutoGuardBtn(eventname,params)
	--自动战斗
	--self.btn_autoGuard = self.root:FindChildByEditName("tbt_zidong",true)
	if not self.btn_autoGuard then
		self.btn_autoGuard = self.root:FindChildByEditName("tbt_zidong",true)
	end
	self.btn_autoGuard.IsChecked = params.IsGuard

	if self.autofight ==nil then
		local cvs_zidong = self.root:FindChildByEditName("cvs_zidong",true)
		PlayAutoFightEffect(self,cvs_zidong,5,'/res/effect/ui/ef_ui_yigui_upgrade.assetbundles')
	end
	if self.autofight then
		self.autofight:SetActive(params.IsGuard)
	end
	--if not self.autoBattleImg then
	--end
	--自动战斗动画.
	if self.autoBattleImg.Layout == nil then
		self.autoBattleImg.Layout = HZUISystem.CreateLayoutFromCpj("static/effect/auto/output/auto.xml","zidongxunlu", 1)
		self.autoBattleImg.Layout.SpriteController:PlayAnimate(1,-1,nil)
	end
	self.autoBattleImg.Visible = params.IsGuard

end

local function OnAutoRunChange(eventname,params)
	--自动寻路
	if self.autoRunImg.Layout == nil then
		self.autoRunImg.Layout =  HZUISystem.CreateLayoutFromCpj("static/effect/auto/output/auto.xml","zidongxunlu", 0)
		self.autoRunImg.Layout.SpriteController:PlayAnimate(0,-1,nil)
	end
		self.autoRunImg.Visible = params.isRun
	
end

local function OnPlayTargetEffect(eventname,params)
	local cvs_target=self.root:FindChildByEditName("cvs_target", true)
	if cvs_target~=nil then --单独写一遍，避免污染targetEffectId
		local transSet = {}
			transSet.Pos = Vector3(cvs_target.Size2D.x/2,-cvs_target.Size2D.y/2, 0)
			transSet.Scale = Vector3(0.65,0.6,1)
			transSet.Parent = cvs_target.Transform
			transSet.Layer = Constants.Layer.UI
			transSet.LayerOrder = 1000
		Util.PlayEffect('/res/effect/ui/ef_ui_interface_god_attribute.assetbundles',transSet)
	end
end

local function OnPKModeChange(eventname,params)
	--print("--------------OnPKModeChange---------------")
	--local mode = params.Mode
	local mode = params.Mode

 	if mode == PKInfo.PKMode.Peace then
    	self.btn_moshi.Text = self.moshi_peace.TextSprite.Text
    	self.btn_moshi.FontColor = self.moshi_peace.FontColor
   	elseif mode == PKInfo.PKMode.Team then
		self.btn_moshi.Text = self.moshi_team.TextSprite.Text
    	self.btn_moshi.FontColor = self.moshi_team.FontColor
	elseif mode == PKInfo.PKMode.Justice then
		self.btn_moshi.Text = self.moshi_jus.TextSprite.Text
    	self.btn_moshi.FontColor = self.moshi_jus.FontColor
	elseif mode == PKInfo.PKMode.Revenger then
		self.btn_moshi.Text = self.moshi_rev.TextSprite.Text
    	self.btn_moshi.FontColor = self.moshi_rev.FontColor
	elseif mode == PKInfo.PKMode.Guild then
		self.btn_moshi.Text = self.moshi_guild.TextSprite.Text
    	self.btn_moshi.FontColor = self.moshi_guild.FontColor
	end

end

local function ReSortTopIcon( ... )
	-- print('-------------ReSortTopIcon--------------')
	local showComps = {}
	local rootNode = self.root:FindChildByEditName('cvs_menuhud', true)
	local compsCount = self.cvs_menuhud.NumChildren
	for i = 0, compsCount - 1 do
		local cvs = self.cvs_menuhud:GetChildAt(i)
		local name = cvs.EditName
    	local dbs = GlobalHooks.DB.Find('module_open', { menu_type = 1, comp = name })
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
		-- print('---------------- ', name)
		local isOpen = (db ~= nil) and GlobalHooks.IsFuncOpen(db.UI_flag) or false
		local canShow = self.TopIconState[name] == nil and true or self.TopIconState[name]
		if isOpen and canShow then
			cvs.Visible = true
			--红点
			local isShowRed = db.open_type ~= 1 and db.open_type ~= 4 and GlobalHooks.IsFuncWaitToPlay(db.UI_flag)
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

	local originComp = self.cvs_menuhud:FindChildByEditName("cvs_recharge", true)
	local xNum = originComp.UserTag --5
	local space = 0
	for i = 1, #showComps do
		local row = (i - 1) % xNum
		local col = math.floor((i - 1) / xNum)
		local comp = showComps[i].comp
		comp.X = originComp.X - (originComp.Width + space) * row
		comp.Y = originComp.Y + (originComp.Height + space) * col
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

function self.PlayTopIconAnime( showAnime )
	-- print('--------------PlayAnime-------------')
	local originComp = self.originComp
	local xNum = originComp.UserTag --6
	local space = 0

	for i = 1, #self.showComps do
		local row = (i - 1) % xNum
		local col = math.floor((i - 1) / xNum)
		local comp = self.showComps[i].comp
		local x = self.defaultPos.x - (originComp.Width + space) * row
		local y = self.defaultPos.y + (originComp.Height + space) * col
		if showAnime then
			local posTarget = Vector2(x, y)
			local duration = (row + 1) * 0.02
			comp.X = self.defaultPos.x + 50
			comp:RemoveAction("MoveAction", false)
			DoMoveAction(comp, posTarget, duration)
		else
			comp.Position2D = Vector2(x, y)
		end
	end
end

local function OnSetTopIcon( eventname, params )
	self.TopIconState[params.comp] = params.showIcon
	ReSortTopIcon()
end

local function OnShowTopHud( eventname, params )
	if self.tbt_tophud.IsChecked ~= params.isShow then
		self.tbt_tophud.IsChecked = params.isShow
	end
	if self.cvs_menuhud.Visible ~= params.isShow then
		self.cvs_menuhud.Visible = params.isShow
		if params.isShow then
			self.PlayTopIconAnime(params.showAnime)
		end
	end
end

local function FirstInitFinish()
	if not GameGlobal.Instance.netMode then return end

	if GlobalHooks.IsFuncOpen('ActivityMain') then
		ActivityModel.GetActivityUserData(nil,false)
	end
	if GlobalHooks.IsFuncOpen('BusinessFrame') or GlobalHooks.IsFuncOpen('NewOpenFrame') then
		BusinessModel.FirstRequire(nil,function()
			for i1, v1 in ipairs(BusinessModel.cachedata) do
				local ishave = false
				for i2, v2 in pairs(v1) do
					ishave = true
					break
				end
				if i1 == 1 and not ishave then
					EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_newopen', showIcon = false})
				elseif i1 == 2 and not ishave then
					EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_fuli', showIcon = false})
				elseif i1 == 3 and not ishave then
					EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_valentinesday', showIcon = false})
				end
			end
		end)
	end
	if GlobalHooks.IsFuncOpen('ActivityEntrust') then
		ActivityModel.GetEntrustData(nil,false)
	end
	if GlobalHooks.IsFuncOpen('PagodaMain') then
		PagodaModel.RequireStoryData(nil,false)
	end
	AnalysisActivity(self.cvs_title)

	--显示运营活动推送弹窗
	local sceneid = DataMgr.Instance.UserData.MapTemplateId
	if sceneid == 0 then
		sceneid = GameGlobal.Instance.SceneID
	end
	if sceneid ~= 100000 and not lastSceneid then
		GlobalHooks.UI.OpenUI('FirstOnlineShow', 0)
	end

	--隐藏七日活动入口
	self.SevenTime = nil
	local time = DataMgr.Instance.UserData.Serverinfo.open_at:AddDays(GameUtil.GetIntGameConfig("openmaxday"))
			- ServerTime.getServerTime():ToLocalTime()
	if time.TotalHours >= 0 and time.TotalHours <= 6 then
		self.SevenTime = LuaTimer.Add(math.floor(time.TotalSeconds)*1000,
			function()
				EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_sevenday', showIcon = false})
				if self.SevenTime then
					LuaTimer.Delete(self.SevenTime)
					self.SevenTime = nil
				end
			end)
	end
	if time.TotalSeconds <= 0 then
		EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_sevenday', showIcon = false})
	end
	
	--上线判断春节活动图标
	local isOpen = TimeUtil.inTime(System.DateTime.Parse(SpringFestivalModel.SpringFestivalOpenTime),
			System.DateTime.Parse(SpringFestivalModel.SpringFestivalCloseTime),
			ServerTime.getServerTime():ToLocalTime())
	if not isOpen then
		EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_springfestival', showIcon = false})
	end
end

local function UpdateNewOpenTime()
	local time = GlobalHooks.DB.GetGlobalConfig('openmaxday')*86400 - TimeUtil.TimeLeftSec(BusinessModel.GetServerOpenTime())
	if time < 0 then
		EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_newopen', showIcon = false})
	else
		self.activitytimer = LuaTimer.Add(time,function()
			EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_newopen', showIcon = false})
		end)
	end
end

local function OnFunctionOpen( eventname, params )
	if params.data.menu_type == 1 then
		ReSortTopIcon()
	end
	if params.name == "PagodaMain" then
		PagodaModel.RequireStoryData(nil,false)
	end
	if params.name == "ActivityMain" then
		ActivityModel.GetActivityUserData(nil,false)
	end
	if params.name == "Activity_Newopen" then
		BusinessModel.FirstRequire(2,function()
			for i1, v1 in pairs(BusinessModel.cachedata) do
				local ishave = false
				for i2, v2 in pairs(v1) do
					ishave = true
					break
				end
				if i1 == 1 and not ishave then
					EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_newopen', showIcon = false})
				end
			end
		end)
		UpdateNewOpenTime()
	end
	if params.name == "ActivityEntrust" then
		ActivityModel.GetEntrustData(nil,false)
	end
	if params.name == "BusinessFrame" then
		BusinessModel.FirstRequire()
	end
end

local function ActivityNewDayRefresh()
	FirstInitFinish()
end

local function OnAutoChangePKMode(eventname, params )
	print("--------------OnAutoChangePKMode---------------")
	SendPKModeChangeRequest(params.s2c_mode)
end

local function OnShowUI(eventname,params)
	print("--------------OnShowMainHud---------------")
	local m_root = HudManager.Instance:AddHudUIFromXml("xml/hud/ui_hud_other.gui.xml", "MainHud")
	self.root = m_root
	m_root.Enable = false
	self.originComp = self.root:FindChildByEditName("cvs_recharge", true)
	self.defaultPos = self.originComp.Position2D
	InitUI()
	ReSortTopIcon()
end


local function GetBufIndex(index)
	local pos = {2,1,3}
	return pos[index] - 1
end


local function OnBuffUpdate()
	if self.buficonlist ~= nil then
		local curTime = os.clock()
		for i,v in ipairs(self.buficonlist or {}) do
			if v.icon ~= nil then
				local value = v.icon.Value -  (curTime - v.curOSTime)*1000 --self.timegap
				v.curOSTime = curTime
				value = math.max(value,0)
				v.icon.Value = value
			end
		end
	end
end
local function OnShowAddBuff(eventname,params)
	if self.cvs_debufflist == nil then
		return
	end
	if self.timeid == nil then
		self.timegap = 33
		self.timeid = LuaTimer.Add(0, self.timegap, function()
			OnBuffUpdate()
			return true
		end)
	end

	

	local buffdata = {}
	buffdata.id = params.id
	buffdata.cd = params.cd
	buffdata.lifetime = params.lifetime
	buffdata.res = params.res
	if #self.debuflist == 3 then
		local cd = 0
		local index = 1
		local curvalue = 0
		for i,v in ipairs(self.debuflist) do
    		if v.cd > cd then
    			cd = v.cd
    			index = i
    		end
    	end

    	for i,v in ipairs(self.buficonlist or {}) do
			if v.index == self.debuflist[index].index then
				v.icon:RemoveFromParent(true)
				table.remove(self.buficonlist,i)
				break
			end
		end
    	table.remove(self.debuflist,index)
	end
	local sortid = {1,2,3}
	for i,v in ipairs(self.debuflist or {}) do
		for j,k in ipairs(sortid) do
			if v.index == k then
    			table.remove(sortid,j)
    			print("sortid",j)
    			break
			end
		end
	end
	buffdata.index = sortid[1]
	--print("buffdataindex",buffdata.id,buffdata.index)

	table.insert(self.debuflist,buffdata)
	--print_r("self.debuflist",self.debuflist)

	if self.buficonlist == nil then
		self.buficonlist = {}
	end
	local buficon = {}
	buficon.icon = self.debufficon:Clone()
	buficon.id = buffdata.id
	buficon.icon.Visible = true
	buficon.icon.Position2D = Vector2(0,0)
	buficon.index = buffdata.index
	self.cvs_debufflist:AddChild(buficon.icon)
	local path = "$dynamic/TL_debuff/output/TL_debuff.xml|TL_debuff|"..buffdata.res
	local imgup = path.."_u"
	local imgdown = path.."_d"
	--print("buffdata.res",buffdata.res)
	buficon.icon.Layout = HZUISystem.CreateLayout(imgdown, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 0)
	buficon.icon.StripLayout = HZUISystem.CreateLayout(imgup, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 0)
	buficon.icon.Position2D = Vector2(GetBufIndex(buffdata.index)*self.cvs_debufflist.Width/3,buficon.icon.Position2D.y)
	buficon.icon:SetGaugeMinMax(0, buffdata.lifetime)
  	buficon.icon.Value = buffdata.lifetime - buffdata.cd*buffdata.lifetime
  	buficon.curOSTime = os.clock()
  	table.insert(self.buficonlist,buficon)

end


local function OnShowChangeBuff(eventname,params)
	if self.cvs_debufflist == nil then
		return
	end
	for i,v in ipairs(self.buficonlist or {}) do
		if v.id == params.id then
			v.icon:SetGaugeMinMax(0, params.lifetime)
			v.icon.Value = params.lifetime - params.cd*params.lifetime
			v.curOSTime = os.clock()
		end
	end
end


local function OnShowRemoveBuff(eventname,params)
	if self.cvs_debufflist == nil then
		return
	end
	for i,v in ipairs(self.buficonlist or {}) do
		if v.id == params.id then
			v.icon:RemoveFromParent(true)
			table.remove(self.buficonlist,i)
			break
		end
	end

	for i,v in ipairs(self.debuflist or {}) do
		if v.id == params.id then
			table.remove(self.debuflist,i)
			break
		end
	end
end


--监听血池次数变化，修改特效的值
local function OnMedicinePoolChanged()
	if self.effobj then
		if self.medicLimit ~=nil then
			self.effobj.value=(tonumber(DataMgr.Instance.UserData.MedicinePoolCurCount/self.medicLimit))
		else
			local temp =unpack(GlobalHooks.DB.Find('MedicinePool', {level = DataMgr.Instance.UserData.Level}))
			self.medicLimit=temp.limit
			self.effobj.value=(tonumber(DataMgr.Instance.UserData.MedicinePoolCurCount/self.medicLimit))
		end
	else
		self.effobj=RenderSystem.Instance:GetAssetGameObject(self.model)
		self.effobj.value=(tonumber(DataMgr.Instance.UserData.MedicinePoolCurCount/self.medicLimit))
	end
end

--自动填充血池
local function OnMedicineCountToZero()
	if MedicineModel.Recharging then
		return
	end
	--次数大于0 或者未开启自动填充选项时，return
	if DataMgr.Instance.UserData.MedicinePoolCurCount > 0 or (not DataMgr.Instance.UserData.GameOptionsData.AutoRecharge) then
		return 
	end
	if self.pooldata ==nil then
		self.pooldata=MedicineModel.GetOneDataByPlayerLv(DataMgr.Instance.UserData.Level)
	end
	if DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.COPPER) >= (self.pooldata.cost_num*self.pooldata.limit) then
		MedicineModel.Recharging=true
		MedicineModel.DoRechargeMedicinePool(function (rsp)
			DataMgr.Instance.UserData.MedicinePoolCurCount=rsp.s2c_count
			MedicineModel.Recharging=false
		end)
	else
		DataMgr.Instance.UserData.GameOptionsData.AutoRecharge=false
		GameAlertManager.Instance:ShowNotify(Util.GetText('NoEnoughCopperToRechargeMedicinePool'))
	end
end

local function OnCopperChangeToRechargeMedicinePool()
	if MedicineModel.Recharging then
		return
	end
	if DataMgr.Instance.UserData.MedicinePoolCurCount > 0 then
		return
	end
	if not DataMgr.Instance.UserData.GameOptionsData.AutoRecharge then
		return
	end
	if self.pooldata ==nil then
		self.pooldata=MedicineModel.GetOneDataByPlayerLv(DataMgr.Instance.UserData.Level)
	end
	if DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.COPPER) >= (self.pooldata.cost_num*self.pooldata.limit) then
		MedicineModel.Recharging=true
		MedicineModel.DoRechargeMedicinePool(function (rsp)
			DataMgr.Instance.UserData.MedicinePoolCurCount=rsp.s2c_count
			MedicineModel.Recharging=false
		end)
	end
end

local function ChangeMedicineLimit()
	local temp =unpack(GlobalHooks.DB.Find('MedicinePool', {level = DataMgr.Instance.UserData.Level}))
	self.medicLimit = temp.limit or MedicineModel.MedicineLimitCount
end

function self.Notify(status,subject)
	if subject == DataMgr.Instance.UserData then
		if subject:ContainsKey(status, UserData.NotiFyStatus.LEVEL) then--如果推送过来的状态包含等级，则调用升级判断目标
			OnLevelUp()
			ChangeMedicineLimit()
			BusinessModel.Notify("pLevel",DataMgr.Instance.UserData.Level)
			self.pooldata=MedicineModel.GetOneDataByPlayerLv(DataMgr.Instance.UserData.Level)
		elseif subject:ContainsKey(status, UserData.NotiFyStatus.MEDICINEPOOLCURCOUNT) then--推送血池使用次数
			OnMedicineCountToZero()
			OnMedicinePoolChanged()
		elseif subject:ContainsKey(status, UserData.NotiFyStatus.ACCUMULATIVECOUNT) then
			BusinessModel.Notify("pAccumulativeCount",
					subject:TryGetIntAttribute(UserData.NotiFyStatus.ACCUMULATIVECOUNT,0))
		elseif subject:ContainsKey(status, UserData.NotiFyStatus.COPPER) then
			OnCopperChangeToRechargeMedicinePool()
		end

		if self.root ~= nil then
			-- if userdata:ContainsKey(status, UserData.NotiFyStatus.EXP) then
			-- 	UpdateEXPUI()
			-- end
			-- if userdata:ContainsKey(status, UserData.NotiFyStatus.EXP) then
			-- 	SetEXPUI()
			-- end

			UpdateEXPUI()
		end
	elseif subject == DataMgr.Instance.MsgData then
		RefreshMsgIcon()
	end


	if status == "Event.Quest.Complete" then
		if #ActivityModel.doingtaskids ~= 0 then
			for i, v in pairs(ActivityModel.doingtaskids) do
				if v == subject then
					GlobalHooks.UI.SetRedTips("activityentrust",1)
				end
			end
		end
	end
	if status == 'Event.Quest.Submited' then
		if storyfinishids then
			for _, v in pairs(storyfinishids) do
				if subject == v then
					PagodaModel.RequireStoryData()
					GlobalHooks.UI.SetRedTips("pagoda",PagodaModel.pagodaStory)
				end
			end
		end
	end
end

local function clearBuffIcon()
	if self.timeid ~= nil then
		LuaTimer.Delete(self.timeid)
		self.timeid = nil
	end
	if  self.buficonlist ~= nil then
		self.buficonlist = {}
	end
	if #self.debuflist > 0 then
		self.cvs_debufflist:RemoveAllChildren(true)
		self.debuflist = {}
	end

end

local function OnChangeScene(eventname,params)
	SceneModel.ReqChangeScene(params.mapId,params.mapUUid,params.flag)
end

local function fin()
	print (" ----------------------  lua Hud main fin -------------------- ")
	if self.timer1 then
		LuaTimer.Delete(self.timer1)
	end
	if self.activitytimer then
		LuaTimer.Delete(self.activitytimer)
	end
	if self.batteryTime then 
		LuaTimer.Delete(self.batteryTime)	
	end
	if self.time then 
		LuaTimer.Delete(self.time)		
	end
	if self.SevenTime then
		LuaTimer.Delete(self.SevenTime)
		self.SevenTime = nil
	end

	storyfinishids = nil
	
	print (" lua Hud main fin ")
	if self.expTimeId ~= nil then
		LuaTimer.Delete(self.expTimeId)
		self.expTimeId = nil
	end
	EventManager.Unsubscribe("Event.Activity.NewDay", ActivityNewDayRefresh)
	EventManager.Unsubscribe(Events.UI_HUD_LUAHUDINIT, OnShowUI)
	EventManager.Unsubscribe(Events.UI_HUD_SYNC_GUARD_STATE, OnInitAutoGuardBtn)
	EventManager.Unsubscribe("Event.AutoRun.Change", OnAutoRunChange)
	EventManager.Unsubscribe(Events.UI_HUD_SYNC_PK_MODE, OnPKModeChange)
	EventManager.Unsubscribe("Event.Buff.Add", OnShowAddBuff)
	EventManager.Unsubscribe("Event.Scene.FirstInitFinish", FirstInitFinish)
	EventManager.Unsubscribe("Event.Buff.Change", OnShowChangeBuff)
	EventManager.Unsubscribe("Event.Buff.Remmove", OnShowRemoveBuff)
	EventManager.Unsubscribe("Event.Scene.ChangeFinish",OnChangeSceneFinish)
	DataMgr.Instance.UserData:DetachLuaObserver('LuaHudMain')
	DataMgr.Instance.MsgData:DetachLuaObserver('LuaHudMain')
	DataMgr.Instance.QuestData:DetachLuaObserver('LuaHudMain')
	EventManager.Unsubscribe("Event.Map.ChangeScene", OnChangeScene)
	EventManager.Unsubscribe("Event.FunctionOpen.FuncOpen", OnFunctionOpen)
	EventManager.Unsubscribe("Event.AutoChangePKModeEventB2C", OnAutoChangePKMode)
	EventManager.Unsubscribe("Event.Hud.ShowTopHud", OnShowTopHud)
	EventManager.Unsubscribe("Event.Hud.SetTopIcon", OnSetTopIcon)
	EventManager.Unsubscribe("Event.TargetSystem.LevelUp", SetTargetLv)
	EventManager.Unsubscribe("Event.Target.PlayEffect", OnPlayTargetEffect)
	self.activiyfunctionid = nil
	targetEffectId=nil--清除特效
	storyfinishids = nil
	self.attrNode = nil
end

--退出场景时调用，参数：是否短线重连触发的切场景
local function OnExitScene(reconnect)
	print (" lua Hud main OnExitScene ", reconnect)
	clearBuffIcon()
	
	self.funcBtn.IsChecked = false
	EventManager.Fire("Event.Hud.ShowFunctionMenu", { isShow = false })

	if reconnect then
		if self.autoRunImg then
			self.autoRunImg.Visible = false
		end
	end
end

--进入场景时调用
local function OnEnterScene()
	print (" lua Hud main OnEnterScene ")

	local sceneid = DataMgr.Instance.UserData.MapTemplateId
	if sceneid == 0 then
		sceneid = GameGlobal.Instance.SceneID
	end
	
	local mapData = GlobalHooks.DB.FindFirst('MapData',{ id = sceneid })
	if mapData then
		local mapSetting = GlobalHooks.DB.FindFirst('MapSetting', { type = mapData.type })
		if mapSetting then
			local showHudIcon = mapSetting.hud_menu == 1
			local showSmallMap = mapSetting.radar_map == 1
			local showQuestmenu = mapSetting.quest_menu == 1
			self.tbt_tophud.Visible = showHudIcon
			self.cvs_menuhud.Visible = showHudIcon
			self.funcCvs.Visible = showHudIcon
			self.btn_wardrobe.Visible = showHudIcon
			self.cvs_target_title.Visible = showHudIcon
			self.cvs_wardrobe.Visible = showHudIcon
			self.cvs_youjian.Visible = showHudIcon
			self.cvs_fengyin.Visible = showHudIcon and self.cvs_fengyin.Visible
			self.cvs_shequ.Visible = showHudIcon
			self.cvs_beibao.Visible = showHudIcon or mapData.show_bag == 1
			self.cvs_jiaohu.Visible = showHudIcon
			HudManager.Instance.TeamQuest.Visible = showQuestmenu
			HudManager.Instance:GetHudUI("SmallMap").Visible = showSmallMap
		end
	end

	if lastSceneid then
		local mapData = GlobalHooks.DB.FindFirst('MapData',{ id = lastSceneid })
		if mapData then
			if not string.IsNullOrEmpty(mapData.exit_function_tag) then
				FunctionUtil.OpenFunction(mapData.exit_function_tag)
			end
		end
		if lastSceneid == 100000 then
			GlobalHooks.UI.OpenUI('FirstOnlineShow', 0)
		end
		lastSceneid = nil
	end
end

--师门身份赛被挑战下来后的处理
local function MasterraceBattleInfo(msg)
	if msg then
		local challengeed = self.root:FindChildByEditName("cvs_hud_identitybattle", true)
		challengeed.Visible = true
		challengeed.TouchClick = function(sender)
			GlobalHooks.UI.OpenUI('PlayRuleMain',0,'battlelist')
			sender.Visible = false
		end
	end
end

local function testGM()
  
	local message = GlobalHooks.DB.GetFullTable('_virtual/message')

    local dt = {}
    for i,v in ipairs(message or {}) do
        print_r(i,v)
        local data = {}
        if v.openState then
            data.id = v.id
            data.startDate = TimeUtil.CustomTodayTimeToUtc(v.startDate) 
            data.endDate = TimeUtil.CustomTodayTimeToUtc(v.endDate)  
            data.openState = v.openState
            data.content = v.content
            data.channels = v.channels
            data.broadCast = v.broadCast
            data.updateTime = v.updateTime
            data.custom1 = 0
            table.insert(dt,data)
        end
    end

    if #dt then
    	return
    end

    if self.GMTimer then
        LuaTimer.Delete(self.GMTimer)
    end

    self.GMTimer = LuaTimer.Add(0,1000,function()
        for i,v in ipairs(dt) do
             --print_r('11111111111111111111',i,v)
            if v.openState then
                --print_r('222222222222222',i,v)
                if TimeUtil.TimeLeftSec(v.startDate) > 0 and TimeUtil.TimeLeftSec(v.endDate) < 0 then
                    v.custom1 = v.custom1 + 1
                    if v.custom1 == v.updateTime then
                        v.custom1 = 0
                        -- print('sssssssssssssssssssssss')
                        SceneModel.ShowTipBroadCast(v.broadCast,v.content,v.channels)
                    end
                else
                    v.openState = 0
                end
            end
        end 
    end)
end

local function initial()
	print (" ----------------------  lua Hud main initial -------------------- ")

	EventManager.Subscribe(Events.UI_HUD_LUAHUDINIT, OnShowUI)
	EventManager.Subscribe(Events.UI_HUD_SYNC_GUARD_STATE, OnInitAutoGuardBtn)
	EventManager.Subscribe("Event.AutoRun.Change", OnAutoRunChange)
	EventManager.Subscribe(Events.UI_HUD_SYNC_PK_MODE, OnPKModeChange)
	DataMgr.Instance.UserData:AttachLuaObserver('LuaHudMain', self)
	DataMgr.Instance.MsgData:AttachLuaObserver('LuaHudMain', self)
	DataMgr.Instance.QuestData:AttachLuaObserver('LuaHudMain', self)
	EventManager.Subscribe("Event.Buff.Add", OnShowAddBuff)
	EventManager.Subscribe("Event.Scene.FirstInitFinish", FirstInitFinish)
	EventManager.Subscribe("Event.Buff.Change", OnShowChangeBuff)
	EventManager.Subscribe("Event.Buff.Remmove", OnShowRemoveBuff)
	EventManager.Subscribe("Event.Scene.ChangeFinish",OnChangeSceneFinish)
	EventManager.Subscribe("Event.Map.ChangeScene", OnChangeScene)
	EventManager.Subscribe("Event.FunctionOpen.FuncOpen", OnFunctionOpen)
	EventManager.Subscribe("Event.AutoChangePKModeEventB2C", OnAutoChangePKMode)
	EventManager.Subscribe("Event.Hud.ShowTopHud", OnShowTopHud)
	EventManager.Subscribe("Event.Hud.SetTopIcon", OnSetTopIcon)
	EventManager.Subscribe("Event.TargetSystem.LevelUp", SetTargetLv)
	EventManager.Subscribe("Event.Target.PlayEffect", OnPlayTargetEffect)
	EventManager.Subscribe("Event.Activity.NewDay", ActivityNewDayRefresh)
	self.TopIconState = {}

	isNetMode = GameGlobal.Instance.netMode
	
	if self.pusheff then
		RenderSystem.Instance:Unload(self.pusheff)
	end
	
	if not isNetMode then return end
	Protocol.PushHandler.TLClientMasterRaceIdChangeNotify(MasterraceBattleInfo)

	if not storyfinishids then
		storyfinishids = {}
		local data = PagodaModel.GetPagodaStoryData()
		for i, v in pairs(data) do
			table.insert(storyfinishids,v.finish_quest_id)
		end
	end

end

return { initial = initial, fin = fin, OnEnterScene = OnEnterScene, OnExitScene = OnExitScene }
