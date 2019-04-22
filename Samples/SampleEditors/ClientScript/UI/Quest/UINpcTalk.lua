local _M = {}
_M.__index = _M
local QuestModel = require "Model/QuestModel"
local QuestUtil = require "UI/Quest/QuestUtil"
local QuestNpcDataModel = require'Model/QuestNpcDataModel'
local Util = require("Logic/Util")
local DisplayUtil = require("Logic/DisplayUtil")
local Helper = require 'Logic/Helper'
local UIUtil = require 'UI/UIUtil.lua'-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
-- 名字框
local NameCanvas
-- 名字label
local NameLabel
-- 对话内容label
local ContentLabel
local NpcTemplateId
local ButtonTemplate
local TbnList = {}
local NpcName
local NpcStatic = {}
local CurrentBtn = nil
local AutoQuestId = 0
local canPress = true
local OrgBackGroundHeight = 0
local OrgBackGroundY = 0
local Guide_FtnId = {
	['yz_shimen'] = 'guide_questshimen',
	['tg_shimen'] = 'guide_questshimen',
	['kl_shimen'] = 'guide_questshimen',
	['qq_shimen'] = 'guide_questshimen',
} 


--local showModelList = {}
-- local QuestFlag = 
-- {
-- 	[1] = '#static/hud/output/hud.xml|hud|79',
-- 	[2] = '#static/hud/output/hud.xml|hud|108',
-- 	[3] = '#static/hud/output/hud.xml|hud|114',
-- 	[4] = '#static/hud/output/hud.xml|hud|115',
-- }


local TalkStateFlag =
{
  Init = 1,
  None = 2,
  Begin = 3,
  End = 4
}

local TalkState = TalkStateFlag.Init
--任务按钮
local BtnsList
local LastQuestId = 0
--local CurrentQuestData = nil
local focusEffs = {}
local TalkContext = nil
local localParams = nil
local PlayCGStart = false
local showtime = nil
local function ParseQuestIsAutoDone(Questid)

 		-- //接取、完成全手动 寻路自动 ：0
   --      //接取、完成全自动：1
   --      //接取、完成全手动，寻路接取自动 完成后手动寻路：2
   --      //接取不自动、其余自动：3
	if Questid ~= nil and Questid ~= 0 then
		local Quest = DataMgr.Instance.QuestData:GetQuest(Questid)
		if Quest ~= nil  then
			if Quest.Static.automatic == QuestAutoMaticType.AllAuto then
				if Quest.state == QuestState.NotAccept then
					return true
				elseif Quest.state == QuestState.CompletedNotSubmited then
					return true
				end
			elseif Quest.Static.automatic == QuestAutoMaticType.AutoAccept then

				if Quest.state == QuestState.NotAccept then
				return true
			    end
			elseif Quest.Static.automatic == QuestAutoMaticType.NoAutoAccetpButAutoDone then
				if Quest.state == QuestState.CompletedNotSubmited then
				return true
			    end
			end
		end
	end
	return false
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
local function GetModelData(modelname)
	local modeldata = unpack(GlobalHooks.DB.Find('NpcModelData',{npc_id = modelname}))
	return modeldata
end
local function Release3DModel(self)

	-- if self.model then
	-- 	for i,v in pairs(self.modelcache) do
	-- 		RenderSystem.Instance:Unload(v.id)
	-- 	end
	-- 	self.model = {}
	-- end
	if self.modelid then
		RenderSystem.Instance:Unload(self.modelid)
		self.modelid = nil
	end
end

local function Init3DSngleModel(self, parentCvs, pos2d, scale, menuOrder,fileName)
		
		local state = "n_talk"
	  	self.modelid = QuestUtil.Init3DSngleModel(parentCvs, pos2d, scale, menuOrder,fileName,state)
	  	
	  -- self.model = self.model or {}
	  -- local id = QuestUtil.Init3DSngleModel(parentCvs, pos2d, scale, menuOrder,fileName,function(aoe)
	  		
	  -- 		if self.model[id] == nil then
	  -- 			return
	  -- 		end
	  -- 		self.model[id].aoe = aoe
	  -- 		self.LastInfo.aoe = aoe
	  -- 		if self.model[id] or not self.model[id].show then
		 -- 		if showtime ~= nil then
			-- 		LuaTimer.Delete(showtime)
			-- 	end
			-- 	aoe.gameObject:SetActive(false)
		 -- 		return
		 -- 	end
			-- 	local time = aoe:GetAnimTime("n_talk")
			-- 	aoe:Play("n_talk")
			-- 	if showtime ~= nil then
			-- 		LuaTimer.Delete(showtime)
			-- 	end
			-- 	showtime = LuaTimer.Add(time*1000,function()
			-- 		if self.model and self.model[id].show then
			-- 			aoe:CrossFade("n_idle",0.15)
			-- 		end
			-- 	end)
	  -- 	end)
	  -- self.model = self.model or {}
	  -- self.model[id] = {}
	  -- self.model[id].show = true
	  -- self.model[id].id = id
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

--关闭界面释放按钮控件

local function HideBtn()
	if BtnsList ~= nil then
		for i,v in pairs(BtnsList) do
			v.Node.Visible = false
		end
	end
	
end

--设置对话框内容
local function SetNpcDialogText(self,str)
	local content,npc_tempid = ParseDialogContent(str or '')
	local defaultTextAttr = ContentLabel.TextComponent.RichTextLayer.DefaultTextAttribute
   	ContentLabel.AText = UIUtil.HandleTalkDecode(content,  GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr),defaultTextAttr.fontSize)
	--ContentLabel.XmlText = string.format("<f>%s</f>",content)
end

local function ReloadModel(self,modelid)
		if self == nil then
			return
		end
	    Release3DModel(self)
		if modelid ~= nil then
			--print("ReloadModel"..modelid)
			if not string.IsNullOrEmpty(modelid) then
				local modelData = GetModelData(modelid)
				--hasShow = HasShowModel(modelid)
				local fixposdata = string.split(modelData.pos_xy,',')
				local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
			    local fixzoom = tonumber(modelData.zoom) -- 缩放比例
			    local filename
			    if modelData.editor_unitid ~= 0 then
			    	filename = modelData.editor_unitid 
			    else
			    	filename = modelData.model_id
			    end
			    --local NpcModelName = modelData.npc_name
				--print_r(modelData)
				if modelData then 
					Init3DSngleModel(self, self.ui.comps.cvs_model, Vector3(self.ui.comps.cvs_model.Width/2+fixpos.x,self.ui.comps.cvs_model.Height+fixpos.y,0), fixzoom, self.ui.menu.MenuOrder,filename)
				end
			end
		end
end


--npc默认对话
local function GetNpcDefaultTalk(self)
	print("GetNpcDefaultTalk",GetNpcDefaultTalk)
	if NpcTemplateId ~= 0 then
		local npcshowmodel = GlobalHooks.DB.Find('npc', NpcTemplateId)
		ReloadModel(self,npcshowmodel.npc_model)
	end
	if NpcStatic ~= nil then
		local defauletext = Util.GetText(NpcStatic.default_dialogue)
		ShowNpcName(Util.GetText(NpcStatic.npc_name))
		return defauletext
    else
    	--error()
    	ShowNpcName("")
    	return "NpcTemplateId "..NpcTemplateId.." is not exist in npc.lua"
	end

end
local function RemoveAllBtn()
	if BtnsList ~= nil then
	    for k,v in ipairs(BtnsList) do
	    	if k ~= 1 and v.Node ~= nil then 
		    	v.Node:RemoveFromParent(true)
	        end
	    end
	    BtnsList = {}	
  	end
end
function _M.EnterTalk(self)
	--EventManager.Fire("Event.Npc.NpcTalk",{isTalk = true})
end

local function CloseUI(self)
	if self.ui ~= nil then
		self.ui:Close()
	end
	
	--self:AutoQuest()
end

local function IsSubmitQuest(Questid)
	local Quest = DataMgr.Instance.QuestData:GetQuest(Questid)
		if Quest ~= nil  then
		 	local static = Quest.Static
			if static.condition.type[1] == QuestCondition.SubmitItem
			 or static.condition.type[1] == QuestCondition.SubmitCustomItem  then
				return true
			end
		end
	return false
end
--提交任务判断是否道具提交
local function SubmitQuest(self,Questid,Questtype)
	print("Questid",Questid)
			if IsSubmitQuest(Questid) then
				local Quest = DataMgr.Instance.QuestData:GetQuest(Questid)
				if Quest ~= nil  then
				 	local static = Quest.Static
				 	local UISubmitItem = nil
					
					UISubmitItem = GlobalHooks.UI.CreateUI('UICustomSubmitItem',0,Questid)
					
					self:AddSubUI(UISubmitItem)
					self.UISubmitItem = UISubmitItem
					self.UISubmitItem:SetCloseCallBack(function ()
						-- body
						--print("closeSubItem")
						TalkState = TalkStateFlag.None
						self.initQuestBtn(self)
					end)
					return true
				else
					return false
				end
			else
				return false
			end
end

--按钮进度
local function TalkInProgress(self,btnData,issecond)

		--print("TalkInProgress state in "..TalkState)
		if TalkState == TalkStateFlag.None  then
			self:CloseDialog()
		elseif TalkState == TalkStateFlag.Begin then
			 --ShowNpcTalkContext(self)
		elseif TalkState == TalkStateFlag.End then
			--SetNextStep(self,btnData.QuestID,btnData.QuestState,btnData.QuestType,btnData.QuestAutomatic)
			if btnData == nil then 
				self:CloseDialog()
				return
			end
			QuestModel.ParseNextStep(
				btnData.QuestID,
				btnData.selectIndex,
				function(isSucc)
					if not SubmitQuest(self,btnData.QuestID, btnData.QuestType) then
						if issecond then
							CloseUI(self)
						else
							self:CloseDialog()
						end
						
					end
				end
			)
			--self.ui:Close()
		else
			
		end
end


--按键事件
local function TouchBtn(self,btn,issecond)
			local _state = btn.QuestState
			local fixX = -10
		  	self.talkdata = btn--dataArray
		  	TalkState = TalkStateFlag.Begin

		  	if btn.FunctionID  then
		  		FunctionUtil.OpenFunction(btn.FunctionID,true)
		  		TalkState = TalkStateFlag.None
		  	else
		  		TalkContext = QuestModel.ParseQuestTalkContext(btn.QuestID,NpcTemplateId)
				if TalkContext and #TalkContext >= 1 then
					TalkState = TalkStateFlag.Begin
					self.ui.comps.cvs_receive.Visible = true
					local params = 
					{
						TalkContext = TalkContext,
						cb = function (selectIndex)
								if self.talkdata then
									self.talkdata.selectIndex = selectIndex
								end
							    TalkState = TalkStateFlag.End
								self:CheckDialog()
							end
					}
		 			QuestNpcDataModel.OpenQuestTalk(params)
					return
				else
					TalkState = TalkStateFlag.End
				end
		  	end
		    
			self:CheckDialog(issecond)
		
		
end

function showEffect(self,effectName, cell)
    --print("showEffect1",effectName)
    
    if cell == nil then
        return
    end
    local transSet = TransformSet()
    transSet.Pos = Vector3(0,0,0)
    transSet.Parent = cell.Transform
    transSet.Scale = Vector3(1, 1, 1)
	transSet.Vectormove = Vector2(cell.Width,cells.Height)
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = 2000
    self.effect = self.effect or {}
    self.effect[#self.effect + 1] = RenderSystem.Instance:PlayEffect(effectName,transSet)
     
end
local function ShowFrame(self,node)
    -- local params = {
    --     scale = Vector3(1, 1, 1),
    --     stores = focusEffs,
    --     --offsetPos = Vector2(-5,0),
    --     cb = function(go)
    --        local vector3move = go:GetComponentInChildren("Vector3Move")
    --        vector3move:setRect(node.Width,node.Height)
    --        go.transform.localPosition = Vector3(0, 0, 0) 
    --     end
    -- }
    -- DisplayUtil.loadEffect("/res/effect/ui/ef_ui_frame.assetbundles", node, params)
    showEffect(self,"/res/effect/ui/ef_ui_frame.assetbundles", node)
end
-- 初始化按钮
local function initQuestBtn(self,issecond)
	SetNpcDialogText(self,"")
	ShowNpcName("")
	ButtonTemplate.Visible = false
	RemoveAllBtn()
	DisplayUtil.clearEffects(focusEffs)
	
	CurrentBtn,BtnsList = QuestModel.ParseInitQuest(NpcTemplateId)

	if CurrentBtn ~= nil then
		TouchBtn(self,CurrentBtn,issecond)
		CurrentBtn = nil
		return false
	end
	self.ui.comps.cvs_receive.Visible = true
	SetNpcDialogText(self,GetNpcDefaultTalk(self))
	--local NpcQuestDataList = DataMgr.Instance.QuestMangerData:GetNpcQuestData(NpcTemplateId)
	--if NpcQuestDataList ~=nil and NpcQuestDataList.Count>0 then

	if #BtnsList > 0 then
        self.ui.comps.cvs_btnbg.Visible = true
		ButtonTemplate.Visible = true
        local btnHeight = 0
		for j,v in ipairs(BtnsList) do
			local btn = v
			if j == 1 then
				btn.Node = 	ButtonTemplate
			else 
				btn.Node = 	ButtonTemplate:Clone()
			end
			btn.lb_questname = btn.Node:FindChildByEditName("lb_missionname", true)
			btn.tbn_an1 = btn.Node:FindChildByEditName("tbn_an1", true)
			local text = ""
			local IsGray = false
			if btn.FunctionID then --循环任务
				local scriptname =  Guide_FtnId[btn.FunctionID]
				if scriptname ~= nil then
					scriptname = 'client.Guide/'..scriptname
					self.eventid = EventApi.Task.StartEventByKey(scriptname)
				end
				text = btn.FunctionName
				text = "<color=#5a3912>"..text.."</color>"
		
			else --普通任务
				local Quest = btn.Quest
				local static = Quest.Static
				text = Util.GetText(static.quest_name)
				
				if Quest.state == QuestState.NotAccept then
					text = "<color=#5a3912>"..text.."</color>"
    			elseif Quest.state == QuestState.NotCompleted then 
    				text = "<color=#6c6c6c>"..text.."</color>"
					IsGray = true
    			elseif Quest.state == QuestState.CompletedNotSubmited then
    				text = "<color=#5a3912>"..text.."</color>"
    			end
				if Quest.state == QuestState.CompletedNotSubmited or Quest.state == QuestState.NotAccept then
					ShowFrame(btn.tbn_an1)
				end
			end
			btn.lb_questname.Text = text
			btn.tbn_an1.Tag = j
			btn.tbn_an1.IsGray = IsGray
			btn.Node.Y = btn.Node.Y - (j - 1)*(btn.Node.Height + 5)
            
            btnHeight = (j - 1)*(btn.Node.Height + 5)
			btn.tbn_an1.TouchClick = function(sender)
				TouchBtn(self,btn)
			end
			if j ~= 1 then 
				ButtonTemplate.Parent:AddChild(btn.Node)
			end
		end
        self.ui.comps.cvs_btnbg.Height = OrgBackGroundHeight + btnHeight
        self.ui.comps.cvs_btnbg.Y = OrgBackGroundY - btnHeight
	else
        self.ui.comps.cvs_btnbg.Visible = false
		ButtonTemplate.Visible = false
	end
	return true
end
--检查是否有对话
function _M.CheckDialog(self,issecond)

	 if TalkState == TalkStateFlag.Init then
		SetNpcDialogText(self,"")
		ShowNpcName("")
		TalkState = TalkStateFlag.None
		ButtonTemplate.Visible = false
		QuestModel.QuestIsTalkFinish(NpcTemplateId,function()
	 		initQuestBtn(self)
	 	end)
	 	return
	 end

	 TalkInProgress(self,self.talkdata,issecond)

	 
end
-- 关闭对话的判断
function _M.CloseDialog(self)

	--print("CloseDialog.."..TalkState)
	QuestModel.QuestIsTalkFinish(NpcTemplateId,function()
			if TalkState == TalkStateFlag.End then
				TalkState = TalkStateFlag.None
				local isReturn = false
				if initQuestBtn(self,true) then
					if #BtnsList >= 1 then
						for i,v in ipairs(BtnsList) do
							local Queststate = v.QuestState
							if Queststate == QuestState.NotAccept or Queststate == QuestState.CompletedNotSubmited then
								isReturn = true
								break
							end
						end
						if isReturn then
							return
						end
					end
				else
					return
				end
			end
			CloseUI(self)
	end)
	
end

function _M.OnEnter(self,params)

	--MenuMgr.Instance:CloseAllMenu()
	print("UINPCTalk............................")
	LastQuestId = 0
	AutoQuestId = params.QuestId
	NpcTemplateId = params.TemplateId
	self.CallBack = params.CallBack
	NpcStatic = GlobalHooks.DB.Find('npc', NpcTemplateId)
	NpcName = params.NpcUnitName
	TalkState = TalkStateFlag.Init
	self.ui.comps.cvs_receive.Visible =false
	self:CheckDialog()
	self:EnterTalk()
	self.ui.menu:SetUILayer(self.ui.comps.cvs_receive)
	self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
	EventManager.Fire("Event.Npc.NpcTalk",{isTalk = true})
end
function _M.OnExit( self )
	print('OnExitUITalk')
	-- if showtime ~= nil then
	-- 	LuaTimer.Delete(showtime)
	-- 	showtime = nil
	-- end
	DisplayUtil.clearEffects(focusEffs)
	Release3DModel(self)
	RemoveAllBtn()
	if self.UISubmitItem~= nil then
		self.UISubmitItem:SetCloseCallBack(nil)
	end
	if self.CallBack ~= nil then
	 	self.CallBack()
	 	self.CallBack = nil
	end
	EventManager.Fire("Event.Npc.NpcTalk",{isTalk = false})
	--EventManager.Fire("CloseNpcCamera",{unit = NpcUnit})
	CurrentBtn = nil
	--CurrentQuestData = nil
	AutoQuestId = 0
	if self.eventid ~= nil then
		EventApi.Task.StopEvent(self.eventid)
	end
end

function _M.OnDestory(self)
	print('OnDestory')
end
local function PlaySound()
	SoundManager.Instance:PlaySoundByKey('button',false)
end
function _M.OnInit( self )

	self.ui.menu.ShowType =  UIShowType.HideHudAndMenu
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.comps.cvs_top.Visible = false
	self.ui.comps.lb_cd.Visible = false
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_bottom, bit.bor(HudManager.HUD_BOTTOM))

	self.ui.menu.event_PointerUp = function(sender)
		self:CheckDialog()
		PlaySound()
	end
	self.ui.comps.cvs_bottom.event_PointerUp = function(sender)
		self:CheckDialog()
		PlaySound()
	end
	self.ui.comps.cvs_priming.event_PointerUp = function(sender)
		self:CheckDialog()
		PlaySound()
	end
	self.ShowModelOrginX =self.ui.comps.cvs_model.X
    OrgBackGroundY = self.ui.comps.cvs_btnbg.Y
    OrgBackGroundHeight = self.ui.comps.cvs_btnbg.Height
	ButtonTemplate = self.ui.comps.cvs_btn
	NameLabel = self.ui.comps.lb_name
	ContentLabel = self.ui.comps.tbh_text
	NameCanvas = self.ui.comps.cvs_name
	PlayCGStart = false
end

--解析是否是自动完成任务

--任务交互
-- function _M.ParseNextStep(self,Questid,Queststate,Questtype,Questautomatictype,SelectIndex,cb)

-- 	local _state = Queststate
-- 	local _Questid = Questid
-- 	local _Questtype = Questtype

-- 	if _state == QuestState.NotAccept then
-- 		if canPress then
-- 			canPress = false
-- 			QuestModel.requestAccept(_Questid, _Questtype,function()
-- 				AcceptQuest(_Questid)
-- 				canPress = true
-- 				if cb then
-- 					cb()
-- 				end
-- 			end)
-- 		end
-- 	elseif _state == QuestState.NotCompleted then
-- 			if cb then
-- 				cb()
-- 			end
-- 	elseif _state == QuestState.CompletedNotSubmited then
-- 		if canPress then
-- 			canPress = false
-- 			QuestModel.requestSubmiit(_Questid, _Questtype,SelectIndex,function()
-- 				CompleteQuest(_Questid)
-- 				canPress = true
-- 				if cb then
-- 					cb()
-- 				end
-- 			end)
-- 		end
-- 	end
-- end
-- function _M.ParseQuestTalkContext(self,QuestID)
-- 	--print("ParseQuestTalkContextQuestID",QuestID)
-- 	local params = {
-- 		id = QuestID,
-- 		model_id = NpcStatic.npc_model,
-- 		speaker_name = NpcStatic.npc_name,
-- 		NpcTemplateId  = NpcTemplateId}
-- 	return QuestNpcDataModel.ParseQuestTalkContext(params)

-- end
-- function _M.ParseInitQuest(self)

-- 	local NpcQuestDataList = DataMgr.Instance.QuestMangerData:GetNpcQuestData(NpcTemplateId)

-- 	local i = 1
-- 	local curbtn = nil
-- 	BtnsList = {}
-- 	if NpcQuestDataList ~=nil and NpcQuestDataList.Count>0 then
-- 			--print("ParseInitQuest",NpcQuestDataList.Count)
-- 		for Quest in Slua.iter(NpcQuestDataList) do
-- 	        local btn = {}
-- 			local static = Quest.Static
-- 			if Quest.mainType ~= QuestType.TypeTip then
-- 				btn.Quest = Quest
-- 				btn.QuestID =tonumber(Quest.id)
-- 				btn.QuestState = tonumber(Quest.state)
-- 				btn.QuestType = tonumber(Quest.mainType)
-- 				btn.QuestAutomatic = tonumber(Quest.Static.automatic)
-- 				print("AutoQuestId",AutoQuestId)
-- 				print_r("AutoQuestIdbtn",btn)
-- 				if AutoQuestId == Quest.id then
-- 					if Quest.state == QuestState.NotAccept or Quest.state == QuestState.CompletedNotSubmited then
-- 						curbtn = btn
-- 						print("curbtn",curbtn.QuestID)
-- 					end
-- 				end
-- 				BtnsList = BtnsList or {}
-- 				table.insert(BtnsList,btn)
-- 			end
			
-- 			--print_r("BtnsList",BtnsList)
-- 		end
-- 	end
-- 		local  functionbtn = {}
-- 		for i,v in ipairs(NpcStatic.function_tag) do
-- 			if not string.IsNullOrEmpty(v) then
-- 				local btn = {}
-- 				btn.FunctionID = v
-- 				btn.FunctionName = FunctionUtil.GetFunctionName(v)
-- 				btn.QuestAutomatic = 0
-- 				table.insert(functionbtn,btn)
-- 			else 
-- 				break
-- 			end
		 	
-- 		end 
-- 		if #functionbtn > 0 then
-- 			table.sort(functionbtn,function(a,b)
-- 				return a.FunctionID > b.FunctionID
-- 			end)
-- 			for i,v in ipairs(functionbtn) do
-- 	       		table.insert(BtnsList,v)
-- 			end
-- 		end
	
-- 	if curbtn == nil and BtnsList ~= nil and #BtnsList >0 then 
-- 		for i,v in ipairs(BtnsList) do
-- 			local _btn = v
-- 			local autoDone = ParseQuestIsAutoDone(_btn.QuestID)
-- 			if autoDone then
-- 				curbtn = _btn
-- 				break
-- 			end
-- 		end
-- 	end
-- 	--print("AutoQuestIdcurbtn",curbtn)
-- 	return curbtn
-- end

-- function _M.ParseQuestCanSumbit(self,btn)
-- 	local state = btn.QuestState
-- 	if state == QuestState.NotAccept or state == QuestState.CompletedNotSubmited then
-- 		return true
-- 	elseif _state == QuestState.NotCompleted then
-- 		return IsSubmitQuest(btn.QuestID)
-- 	end
-- 	return false
-- end
--初始化判断是否要打开npc对话
-- function _M.ParseInitTalk(self,params)
-- 	--print("ParseInitTalk-",params)
-- 	QuestIsTalkFinish(function()
-- 		local _CurrentBtn = self:ParseInitQuest()
-- 		local _TalkContext = _CurrentBtn and self:ParseQuestTalkContext(_CurrentBtn.QuestID) or nil
		
-- 		if _TalkContext ~= nil and _CurrentBtn then
-- 			--print_r(_TalkContext)
-- 			local _params = 
-- 			{
-- 				TalkContext = _TalkContext,
-- 				cb = function (selectIndex)
-- 					print("selectIndex",selectIndex)
-- 					print("_CurrentBtn.QuestID",_CurrentBtn.QuestID)
-- 					self.FirstTalk = false
-- 					self:ParseNextStep(_CurrentBtn.QuestID,_CurrentBtn.QuestState,_CurrentBtn.QuestType,_CurrentBtn.QuestAutomatic,selectIndex,function()	
-- 						self:ParseInitTalk(params)
-- 					end)
-- 				end
-- 			}
-- 			QuestNpcDataModel.OpenQuestTalk(_params)
-- 		else
-- 		 	if _CurrentBtn ~= nil then
-- 				self:ParseNextStep(_CurrentBtn.QuestID,_CurrentBtn.QuestState,_CurrentBtn.QuestType,_CurrentBtn.QuestAutomatic,0,function()
-- 					self.FirstTalk = false
-- 					self:ParseInitTalk(params)
-- 				end)
-- 			else
-- 				--print("BtnsListCount = ",#BtnsList)
-- 				--print("self.FirstTalk = ",self.FirstTalk)
-- 				if #BtnsList == 0 and not self.FirstTalk then
-- 					self:AutoQuest()
-- 					self:OnExit()
-- 				else
-- 					if not self.FirstTalk then
-- 						if #BtnsList > 0 then --检查是否有可提交任务
-- 							local subquest = false
-- 							for i,v in ipairs(BtnsList) do
-- 								local _btn = v
-- 								local cansub = self:ParseQuestCanSumbit(_btn)
-- 								if cansub then
-- 									subquest = true
-- 									break
-- 								end
-- 							end
-- 							if not subquest then
-- 								self:AutoQuest()
-- 								self:OnExit()
-- 								return
-- 							end
-- 						end
-- 					end
-- 					local ui = GlobalHooks.UI.FindUI('UINpcTalk')
-- 					if ui ~= nil then
-- 					 	return
-- 					end
-- 					--print("ParseInitTalk---------------")
-- 				 	GlobalHooks.UI.OpenUI('UINpcTalk',1,params)
-- 				end
				
-- 			end
-- 		end
-- 	end)
	
-- end
-- --初始化判断是否要打开npc对话
-- function  _M.QuestDataDoAction(self,params)
-- 	--print("QuestDataDoAction".. params.QuestId)
-- 	if PlayCGStart then
-- 		localParams = params
-- 		return
-- 	end
-- 	NpcUnit = params.NpcUnit
-- 	AutoQuestId = params.QuestId
-- 	NpcTemplateId = params.TemplateId
-- 	NpcStatic = GlobalHooks.DB.Find('npc', NpcTemplateId)
-- 	--print("NpcTemplateId"..NpcTemplateId)
-- 	self:EnterTalk()
-- 	self.FirstTalk = true
-- 	self:ParseInitTalk(params)
-- end

--打开聊天界面
-- local function OnShowNpcTalk(eventname,params)
-- 	_M:QuestDataDoAction(params)----
-- end
--外部调用关闭npctalk界面
local function OnCloseTalk(eventname,params)
	--print("OnCloseTalk")
	GlobalHooks.UI.CloseUIByTag('UINpcTalk')
end
-- local function PLAYCG_START(eventname,params)
-- 	-- body
-- 	--print("UINpcTalk",params.PlayCG)
-- 	PlayCGStart = params.PlayCG
-- 	if not PlayCGStart then
-- 		if localParams ~= nil then
-- 			_M:QuestDataDoAction(localParams)
-- 			localParams = nil
-- 		end
-- 	end
-- end


local function initial()
	PlayCGStart = false
	--EventManager.Subscribe(Events.UI_NPCTALK,OnShowNpcTalk)
	EventManager.Subscribe("NpcTalkClose", OnCloseTalk)
	--EventManager.Subscribe(Events.PLAYCG_START,PLAYCG_START)
end

 local function fin()
-- 	PlayCGStart = false
-- 	--EventManager.Unsubscribe(Events.UI_NPCTALK,OnShowNpcTalk)
	EventManager.Unsubscribe("NpcTalkClose", OnCloseTalk)
-- 	--EventManager.Unsubscribe(Events.PLAYCG_START,PLAYCG_START)
	
 end

_M.fin = fin
_M.initial = initial
GlobalHooks.NpcTalkParse= GlobalHooks.NpcTalkParse or {}
GlobalHooks.NpcTalkParse.ParseContent = ParseContent
return _M