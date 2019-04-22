local _M = {}
_M.__index = _M
local Util = require("Logic/Util")
local UIUtil = require 'UI/UIUtil'
local QuestUtil = require 'UI/Quest/QuestUtil'
local QuestNpcDataModel = require 'Model/QuestNpcDataModel'
-- 名字框
local NameCanvas
-- 名字label
local NameLabel
-- 对话内容label
local ContentLabel
local NpcTemplateId
local NpcName
local CurQuest
local CallBack = nil
local ShowModelList = {}
local TalkStateFlag =
{
  Init = 1,
  None = 2,
  Begin = 3,
  End = 4
}
local TalkState = TalkStateFlag.Init
local TalkContext = nil
local lastModel = nil
local localParams = nil
local canSelect = true
local curLua = nil
local showtime = nil
local ShowTabel = {}
local cgPlay = false
local HasPress = false
local isSingleTalk = false

local function HasShowModel(modelid)
	--print(HasShowModel)
	--print_r(showModelList)
	if showModelList[modelid] == nil then
		--print("HasShowModel "..modelid)
		showModelList[modelid] = modelid
		return false
	else 
		return true
	end
end

-- 隐藏名字框
local function ShowNameCanvas()
	if NameLabel.Text ~= nil and string.empty(NameLabel.Text) then
		NameCanvas.Visible = false
	else
		NameCanvas.Visible = true
	end
end
--设置对话框上玩家名字
local function ShowSelf()
	--设置半身像
	--SetBustPic(PeekNpcData())
	--设置名称

	local userdata = DataMgr.Instance.UserData
	local name = userdata.Name
	NameLabel.Text = name	
	ShowNameCanvas()
end
--设置对话框NPC名字
local function ShowNpcName(NpcName)
	--设置名称
	-- local name = string.format("<f><name color='fffee663'>%s</name>",npc_data.Name)
	NameLabel.Text = NpcName	
	ShowNameCanvas()
end
local function GetSelfModelID()
	local pro = DataMgr.Instance.UserData.Pro
	local gender = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.GENDER)
	--print("GetSelfModelID",pro,gender)
	local model = "player_male_yz"
	if gender == GenderType._Man then
		if pro == RoleProType._YiZu then
			model = "player_male_yz"
		elseif pro == RoleProType._TianGong then
			model = "player_male_tg"
		elseif pro == RoleProType._KunLun then
			model = "player_male_kl"
		elseif pro == RoleProType._QinqQiu then
			model = "player_male_qq"
		end
	elseif gender == GenderType._Woman then
		if pro == RoleProType._YiZu then
			model = "player_female_yz"
		elseif pro == RoleProType._TianGong then
			model = "player_female_tg"
		elseif pro == RoleProType._KunLun then
			model = "player_female_kl"
		elseif pro == RoleProType._QinqQiu then
			model = "player_female_qq"
		end
	end
	return model
end
local function HasShowModel(modelid)
	if showModelList[modelid] == nil then
		--print("HasShowModel "..modelid)
		showModelList[modelid] = modelid
		return false
	else 
		return true
	end
end
local function GetModelData(modelname)
	local modeldata = unpack(GlobalHooks.DB.Find('NpcModelData',{npc_id = modelname}))
	if modeldata == nil then
		UnityEngine.Debug.LogError("can't found npcmodel with id "+modelname)
	end
	return modeldata
end




local function CloseLastModel(self)
		if self.LastInfo ~= nil then
			if self.modelcache[self.LastInfo.modelid] ~= nil then
				self.modelcache[self.LastInfo.modelid].gameObject:SetActive(false)
			elseif RenderSystem.Instance:IsLoadSuccess(self.LastInfo.id) then
				local aoe = RenderSystem.Instance:GetAssetGameObject(self.LastInfo.id)
				self.modelcache[self.LastInfo.modelid] = aoe
				aoe.gameObject:SetActive(false)
			else
				RenderSystem.Instance:Unload(self.LastInfo.id)
			end
			self.LastInfo = nil
	 	end

end

local function Release3DModel(self)
	CloseLastModel(self)
	if self.modelcache then
		for i,v in pairs(self.modelcache) do
			v:Unload()
		end
		self.modelcache = {}
	end
	
end

local function Init3DModel(self,modelid, parentCvs, pos2d, scale, menuOrder,fileName,isNpc)
	
	 --local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	 	print("modelid",modelid)
	 	
	 	self.LastInfo = self.LastInfo or {}
		if self.modelcache[modelid] then
			self.LastInfo.id = self.modelcache[modelid].ID
			self.modelcache[modelid].gameObject:SetActive(true)
		else
			local state = "n_idle"
			if ShowTabel[modelid] == nil and isNpc then
			  ShowTabel[modelid] = true
			  state = "n_talk"
			end
	  		self.LastInfo.id = QuestUtil.Init3DSngleModel(parentCvs, pos2d, scale, menuOrder,fileName,state)
	  		
		end
		self.LastInfo.modelid = modelid

end


--解析对话框内容for c#
local function ParseContent(str)
	local userName = DataMgr.Instance.UserData.Name
	--print("userName"..userName)
	if userName ~= nil and  not string.empty(userName) then
		local content = string.gsub(str or '','%%name',userName)
		return content
	end
	return str
end
--解析对话框内容
local function ParseDialogContent(str)
	-- print("ParseDialogContent"..str)
	local content = ParseContent(str)
	-- print("content"..content)
	local tmps = string.split(content,':')
	local npc_tempid = nil
	if #tmps == 2 then
		npc_tempid = tonumber(tmps[1])
		content = tmps[2]
	end
	return content,npc_tempid
end


--设置对话框内容
local function SetNpcDialogText(self,str)
	local content,_= ParseDialogContent(str or '')
	local defaultTextAttr = ContentLabel.TextComponent.RichTextLayer.DefaultTextAttribute
   	ContentLabel.AText = UIUtil.HandleTalkDecode(content,  GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr),defaultTextAttr.fontSize)

	--ContentLabel.XmlText = string.format("<f>%s</f>",content)
end

local function ReloadModel(self,modelid)
		if self == nil then
			return
		end
		--print("modelid",modelid)
		-- if self.modelcache[modelid] ~= nil then
		-- 	ShowModel(self,modelid)
		-- 	return
		-- end
		-- for i,v in ipairs(self.modelcache) do
		-- 	if v.modelid == modelid then
		-- 		ShowModel(self,v)
		-- 		return
		-- 	end
		-- end
		
		local hasShow = false
		 CloseLastModel(self)
		--Release3DModel(self)
		--local isPlayer = false

		if not string.IsNullOrEmpty(modelid) then
			local _modelid  = modelid
			local isPlayer = false
			if modelid == "<name>" then --玩家模版
				_modelid = GetSelfModelID()
				isPlayer = true
			end
			local modelData = GetModelData(_modelid)
			if modelData == nil then
				return
			end
			local fixposdata = string.split(modelData.pos_xy,',')
			local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
			local fixzoom = tonumber(modelData.zoom) -- 缩放比例
			local filename 
			hasShow = HasShowModel(_modelid)
			if isPlayer then
				filename = Util.GetActorAvartarTable()
				filename[Constants.AvatarPart.Ride_Avatar01] = nil
				filename[Constants.AvatarPart.Foot_Buff] = nil
			else
				filename = modelData.model_id
				if modelData.editor_unitid ~= 0 then
			    	filename = modelData.editor_unitid 
			    else
			    	filename = modelData.model_id
			    end
			end
			
			Init3DModel(self,_modelid,self.ui.comps.cvs_model, Vector3(self.ui.comps.cvs_model.Width/2+fixpos.x,(self.ui.comps.cvs_model.Height+fixpos.y),0), fixzoom, self.ui.menu.MenuOrder, filename,not isPlayer)				
			
		end

			if not hasShow then
			 	local  action = MoveAction()
	            action.TargetX = self.ShowModelOrginX
	            action.TargetY = self.ui.comps.cvs_model.Y
	            action.Duration = 0.2
    			action.ActionEaseType = EaseType.linear
	            action.ActionFinishCallBack = function(sender)
	            	self.canPress = true
    			end
				self.ui.comps.cvs_model.X = -self.ui.comps.cvs_model.Width
            	self.ui.comps.cvs_model:AddAction(action)
	           
	            self.canPress = false
	        else
	        	self.canPress = true
        	end
		--ShowNpcName(NpcModelName)
		
		
end

local function ShowChildNode(self,node,time,delay,isShow,cb)
		local action = FadeAction()
	    action.Duration = time
	   	action.TargetAlpha =  isShow and 1 or 0
	    action.ActionFinishCallBack = function()
       		if cb then
       			cb()
       		end
    	end
end

local ShowStep = 
{
	Open = 1,
	Close = 2
}
local NodeActionLsit = {
	
		{ -- open
			{'MoveAction',-1,1,0.4},-- from to time 
			{'DelayAction',0.1},--time
			{'FadeAction',0,1,0.5,0.3}-- from to time delay 底层逻辑有毒 delay最低不能为0
		 },
   		{--close
   			{'FadeAction',1,0,0.1,0.01},
			{'MoveAction',0,-1,0.5}
		},
}

local HideSort = {
	{1,3,2},
	{2,3,1},
	{3,1,2}
}


local function NodeShowAction (self,pressindex,nodenum,step,_type,cb)
	local actionlist = NodeActionLsit[_type]
	
	if step > #actionlist then
		if cb then
			cb()
		end
		return
	end  
	local curstep = actionlist[step]
	if curstep[1] == 'MoveAction' then
		local action = MoveAction()
     	action.Duration = curstep[4]
     	action.TargetX = self.talknode.X
     	action.ActionEaseType = EaseType.easeOutBack
    	action.TargetY = curstep[3] == -1 and -self.talknode.Height or curstep[3]
    	
    	action.ActionFinishCallBack = function()
    		NodeShowAction(self,pressindex,nodenum,step + 1,_type,cb)
    	end
    	self.talknode.Y = curstep[2] == -1 and -self.talknode.Height or curstep[2]
    	self.talknode:AddAction(action)
    	SoundManager.Instance:PlaySoundByKey('scrollpan',false)
	end
	if curstep[1] == 'DelayAction' then
		local action = DelayAction()
     	action.Duration = curstep[2]
    	action.ActionFinishCallBack = function()
    		NodeShowAction(self,pressindex,nodenum,step + 1,_type,cb)
    	end
    	self.talknode:AddAction(action)
	end
	if curstep[1] == 'FadeAction' then
		local showchildNodeNum = 0
		local num = 1
		for i,v in ipairs(HideSort[pressindex]) do
			 if self.childnode[v].Visible then
			 	 local delayaction = DelayAction()
		         delayaction.Duration = num == 1 and curstep[5]*0.5 or (num* curstep[5])
		         num = num + 1
		         delayaction.ActionFinishCallBack = function(sender)
		         	local action = FadeAction()
				 	action.Duration = curstep[4]
					action.TargetAlpha = curstep[3]
					action.ActionFinishCallBack = function()
						showchildNodeNum = showchildNodeNum + 1
						--print("showchildNodeNum",showchildNodeNum)
						if showchildNodeNum == nodenum then
							NodeShowAction(self,pressindex,nodenum,step + 1,_type,cb)
						end
					end
					sender:AddAction(action)
				end
	    		self.childnode[v]:AddAction(delayaction)
			 end
			
		end
	end

end

local function SetNpcChooseText(self,text)
	-- for i = 2,3 do
	-- 	self.ui.comps["cvs_choice"..i].Visible = false
	-- end
	local node = self.ui.comps.cvs_top
	node.Visible = false
	self.ui.comps.cvs_bottom.Visible = true
	for i,v in ipairs(self.childnode) do
		v.Visible = false
	end
	--print_r("SetNpcChooseText",TalkContext)
	if TalkContext == nil or TalkContext[1] == nil or (TalkContext[1] and TalkContext[1].choosenum == nil)then
		return 
	end
	--local choosenum = 0
	local desc ={}
	for i = 1,TalkContext[1].choosenum do
		local v = TalkContext[1].choose[i]
		local _desc = {}
		_desc.desc = Util.GetText(v)
		if not string.IsNullOrEmpty(_desc.desc) then
			_desc.index = i
			table.insert(desc,_desc)
		end
	end

	if #desc < 2 then

		return
	end

	node.Visible = true
	self.ui.comps.cvs_bottom.Visible = false
	local labelnode = UIUtil.FindChild(node,'tb_des',true)
	labelnode:SetCenterShow(true)
	local content,_ = ParseDialogContent(text or '')
	labelnode.XmlText = string.format("<f>%s</f>",content)
	--print("choosenum",choosenum)
	local talknode3 = UIUtil.FindChild(node,'cvs_option3',true)
	talknode3.Visible = false
	
	node.Visible = true
	self.ShowNode = false
	for i,v in ipairs(desc) do
		local talknode = UIUtil.FindChild(node,'cvs_option' ..i,true)
		talknode.Visible = true
		talknode.Alpha = 0
		talknode.UserTag = desc[i].index
		local talktextnode = UIUtil.FindChild(talknode,'thb_content',true)
		talktextnode.Text = desc[i].desc
		talktextnode:SetCenterShow(true)

		--print_r("TalkContext",TalkContext)
		talknode.TouchClick = function(sender)
			if not self.ShowNode or not canSelect then
				return 
			end
			canSelect = false
			NodeShowAction (self,sender.UserTag,#desc,1,ShowStep.Close,function()
				--print("Close")
				local index = sender.UserTag
				self.selectIndex = sender.UserTag
	    		TalkContext = QuestNpcDataModel.GetQuestContext(TalkContext[1].choose_desc[desc[i].index])
	    		self:ShowNpcTalkContext()
			end)
			SoundManager.Instance:PlaySoundByKey('talkbutton',false)
		end
	end
	canSelect = true
	NodeShowAction (self,1,#desc,1,ShowStep.Open,function ()
		self.ShowNode = true
		--print("self.ShowNode ",self.ShowNode )
		-- body
	end)

end
function _M.CheckOver(self)
	if TalkContext == nil or #TalkContext == 0 then
		self:CloseDialog()
	end
end

local function CheckDialogFinish(self)
		if not self.canPress or self.checkDiaglog or cgPlay then
			return
		end
		HasPress = true
		if self.time ~= nil then
			LuaTimer.Delete(self.time)
			self.time = nil
		end
		self.checkDiaglog = true
		TLBattleScene.Instance:StopVoice()
	 	self:CheckDialog()
		SoundManager.Instance:PlaySoundByKey('button',false)
		self.checkDiaglog = false
end

function _M.ShowNpcTalkContext(self)
		self.ui.comps.cvs_bottom.Visible = false
		self.ui.comps.cvs_top.Visible = false
		local text = ""--GetNpcDefaultTalk(self)
		local modelid = nil
		self.ShowNode = true
		--print_r("ShowNpcTalkContext",TalkContext)
		if TalkContext ~= nil and #TalkContext > 0 then

			if not string.IsNullOrEmpty(TalkContext[1].dubbing) then
				local fileName = "static/missionsound/"..TalkContext[1].dubbing
				TLBattleScene.Instance:PlayVoice(fileName,function()
    		 
     			end)
			end

			if not string.IsNullOrEmpty(TalkContext[1].cg_id) then
				cgPlay = true
				TLBattleScene.Instance:StopVoice()
				EventManager.Fire("EVENT_PLAYCG_START", {PlayCG = true});
				GlobalHooks.Drama.Start("quest/"..TalkContext[1].cg_id)
				table.remove(TalkContext,1)
				self:CheckOver()
				return
			end

			if  not string.IsNullOrEmpty(TalkContext[1].lua_id) then
				cgPlay = true
				curLua = "quest/"..TalkContext[1].lua_id
				print("filename",curLua)
				GlobalHooks.Drama.Start(curLua)
				self.ui.menu:SetFullBackground(null)
				table.remove(TalkContext,1)
				self:CheckOver()
				return
			end

			self.ui.comps.cvs_bottom.Visible = true
			self.ui.comps.cvs_top.Visible = true
			text = Util.GetText(TalkContext[1].dialogue_content)
			modelid = TalkContext[1].model_id
		end
		
		--print("text",text)
			SetNpcChooseText(self,text)
			SetNpcDialogText(self,text)
			ReloadModel(self,modelid)
		
		
		if TalkContext ~= nil and #TalkContext > 0 then
			local name = Util.GetText(TalkContext[1].speaker_name)
			if name == "<name>" then
				ShowNpcName(DataMgr.Instance.UserData.Name)
			else
				ShowNpcName(name)
			end
			--print("TalkContext = ",TalkContext.Countdown)
			--TalkContext[1].Countdown = 1
			if TalkContext[1].Countdown and TalkContext[1].Countdown > 0 then
				self.ui.comps.lb_cd.Visible = true
				self.countdowntime = TalkContext[1].Countdown
				if self.time ~= nil then
					LuaTimer.Delete(self.time)
					self.time = nil
				end
				self.time = LuaTimer.Add(0,1000,function()
					if self.countdowntime == 0 then
						CheckDialogFinish(self)
						return false
					end
					self.ui.comps.lb_cd.Text = Util.GetText(Constants.Text.countdowntime, self.countdowntime)
					self.countdowntime = self.countdowntime - 1
					return true
				end)
			else
				self.ui.comps.lb_cd.Visible = false
			end
			if not TalkContext[1].choose then
				table.remove(TalkContext,1)
			end
		end

		if TalkContext == nil or #TalkContext == 0 then
			TalkState = TalkStateFlag.End
			TalkContext = nil
		end
	
end


--按钮进度
local function TalkInProgress(self)

		if TalkState == TalkStateFlag.Begin then
			self:ShowNpcTalkContext()
		elseif TalkState == TalkStateFlag.End then
			self:CloseDialog()
		else
			--print("TalkInProgress state in "..TalkState)
		end
end


--检查是否有对话
function _M.CheckDialog(self)
	 TalkInProgress(self)
end
-- 关闭对话的判断
function _M.CloseDialog(self)
	--Release3DModel(self)
	self.ui:Close()
	
end



local function PLAYCG_START(eventname,params)
	-- body
	--print("DialogueTalkPLAYCG_START",params.PlayCG)

	if not params.PlayCG then
		local ui = GlobalHooks.UI.FindUI('DialogueTalk')

		if ui ~= nil then
			--print("DialogueTalk",TalkContext,isSingleTalk,HasPress)
			if TalkContext == nil or #TalkContext == 0 then
				if isSingleTalk and not HasPress then
					--ui:ShowNpcTalkContext()
					return
				else
					ui:CloseDialog()
				end
			else
				if cgPlay then
					cgPlay = false
					ui:ShowNpcTalkContext()
					return
				end
			end
		else
			EventManager.Fire("Event.Npc.DialogueTalk",{isTalk = false})
		end
	end
end

function _M.OnEnter( self, params)
	--MenuMgr.Instance:CloseAllMenu()
	--GlobalHooks.UI.CloseUIByTag('FuncOpen')
	local soundvol = GameUtil.GetIntGameConfig('BGM_Percentage_reduction')
	local changevol = SoundManager.Instance.DefaultBGMVolume * soundvol/100
	SoundManager.Instance:SetCurrentBGMVol(changevol)
	self.modelcache = {}
	--self.LastInfo = {}
	cgPlay = false
	for i,v in ipairs(self.childnode) do
		v.Alpha = 1
	end
	self.selectIndex = 0
    if params.cb then
    	self.cb = params.cb
    end
    SetNpcDialogText(self,"")
    ShowNpcName("")
	self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
	TalkContext = params.TalkContext
	--print_r("TalkContext1",TalkContext)
	--lastModel = nil
	showModelList = {}
	self.canPress = true
	HasPress = false
	TalkState = TalkStateFlag.Begin
	print("OnEnterShowNpcTalkContext")
	if #TalkContext == 1 then
		isSingleTalk = true
	else
		isSingleTalk = false
	end
	self:ShowNpcTalkContext()
	self.ui.menu:SetUILayer(self.ui.comps.cvs_receive)
	EventManager.Fire("Event.Npc.DialogueTalk",{isTalk = true})
	self.ui.menu:SetUILayer(self.ui.comps.cvs_top)
	self.ui.comps.cvs_btnbg.Visible = false
	--self.ui.EnableTouchFrame(self,true)
	EventManager.Subscribe(Events.PLAYCG_START,PLAYCG_START)
end

function _M.OnExit( self )
	SoundManager.Instance:SetCurrentBGMVol(SoundManager.Instance.DefaultBGMVolume)
	print('DialogueOnExit')
	if self.time ~= nil then
	   LuaTimer.Delete(self.time)
	   self.time = nil
	end
	-- if showtime ~= nil then
	-- 	LuaTimer.Delete(showtime)
	-- 	showtime = nil
	-- end
	if self.cb then
		self.cb(self.selectIndex)
		self.cb = nil
	end
	if self.talknode ~= nil then
		self.talknode:RemoveAllAction(false)
	end
	if self.childnode ~= nil then
		for i,v in ipairs(self.childnode) do
			v:RemoveAllAction(false)
		end
	end
	
	--  if  self.modelcachePlayer ~= nil then
	-- 	UI3DModelAdapter.ReleaseModel(self.modelcachePlayer.Key)
	-- 	self.modelcachePlayer = nil
	-- end

	curLua = nil
	SetNpcDialogText(self,"")
	Release3DModel(self)
	TalkState = TalkStateFlag.None
	EventManager.Fire("Event.Npc.DialogueTalk",{isTalk = false})
	ShowTabel = {}
	EventManager.Unsubscribe(Events.PLAYCG_START,PLAYCG_START)
end

function _M.OnDestory(self )
	--print('OnDestory')
end


function _M.OnInit( self )
	self.ui.comps.cvs_btn.Visible = false
	self.ui.menu.ShowType =  UIShowType.HideHudAndMenu
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.menu.event_PointerUp = function(sender)
		--print("self.ui.menu.event_PointerUp")
		CheckDialogFinish(self)
	end
	self.ui.comps.cvs_bottom.event_PointerUp = function(sender)
		--print("self.ui.comps.cvs_bottom.event_PointerUp ")
		CheckDialogFinish(self)
	end
	self.ui.comps.cvs_priming.event_PointerUp = function(sender)
		--print("self.ui.comps.cvs_priming.event_PointerUp")
		CheckDialogFinish(self)
	end
	NameLabel = self.ui.comps.lb_name
	ContentLabel = self.ui.comps.tbh_text
	NameCanvas = self.ui.comps.cvs_name
	self.ShowModelOrginX =self.ui.comps.cvs_model.X
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_bottom,bit.bor(HudManager.HUD_BOTTOM))
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_top, bit.bor(HudManager.HUD_TOP))
	
	local topnode = self.ui.comps.cvs_top
	self.talknode = UIUtil.FindChild(topnode,'cvs_talk',true)
	self.childnode = {}
	for i = 1,3 do
		local _child = UIUtil.FindChild(topnode,'cvs_option'..i,true)
		table.insert(self.childnode,_child)
	end

	self.PlayerModel = self.ui.comps.cvs_model:Clone()
	self.ui.comps.cvs_bottom:AddChild(self.PlayerModel)
	self.PlayerModel.Visible = false
	self.ui.comps.cvs_bottom.Visible = false
	self.ui.comps.cvs_top.Visible = false
	
end

local function OnCloseTalk(eventname,params)
	GlobalHooks.UI.CloseUIByTag('DialogueTalk')
end

local function DirectorEventFinish(eventname,params)
	print("DirectorEventFinish",params.fileName)
	if curLua ~= nil and curLua == params.fileName then
		curLua = nil
		local ui = GlobalHooks.UI.FindUI('DialogueTalk')
		if ui ~= nil then
			cgPlay = false
			ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
			ui:ShowNpcTalkContext()
		end
	end
end
local function initial()
	EventManager.Subscribe("NpcTalkClose", OnCloseTalk)
	
	EventManager.Subscribe("DirectorEventFinish",DirectorEventFinish)
end

local function fin()
	-- SoundManager.Instance:SetCurrentBGMVol(SoundManager.Instance.DefaultBGMVolume)
	EventManager.Unsubscribe("NpcTalkClose", OnCloseTalk)
	
	EventManager.Unsubscribe("DirectorEventFinish",DirectorEventFinish)
end

_M.fin = fin
_M.initial = initial

return _M


