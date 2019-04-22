---------------------------------
--! @file
--! @brief a Doxygen::Lua ApiGlobal.lua
---------------------------------
local ChatModel  = require "Model/ChatModel"
local _M = {}
local UIUtil = require "UI/UIUtil"
function _M.Call(parent,funcstr, ...)
	local list = string.split(funcstr,'.')
	local func = _G
	for k,v in ipairs(list) do
		func = func[v]
	end
	print('call function', func)
	if type(func) == 'function' then
		return func(...)
	end   
end

function _M._asyncShowChapter(self,chapterId)
	GlobalHooks.UI.OpenUI('ChapterMain',0,{chapterId = chapterId,CallBack = function()
		print("ShowChapterClose")
		self:Stop()
	end})
	self:Await()
end
function _M.PlayCG(self,fileName,canSkip,mapid)
	--print(fileName,isShowUi)
	local skip = canSkip
	if skip == nil then
		skip = true
	end
	TLBattleScene.Instance:PlayCG(fileName,skip,function()
		
	end,mapid or 0)
end

function _M._asyncWaitPlayCG(self,fileName,canSkip,mapid)
	--print(fileName,isShowUi)
	local skip = canSkip
	if skip == nil then
		skip = true
	end

	TLBattleScene.Instance:PlayCG(fileName,skip,function()
		 self:Stop()
	end,mapid or 0)
	self:Await()
end

function _M.CanSkipCG(self,var)
	--TLBattleScene.Instance:CanSkipCG(var)
end


function _M.PlaySound(self,fileName)
	--print(fileName,isShowUi)
	TLBattleScene.Instance:PlaySound(fileName,function()
		 
	end)
end

function _M.StopSound(self)
	-- print(fileName,isShowUi)
	-- TLBattleScene.Instance:StopSound(fileName,function()
		 
	-- end)
end

function _M.PlayVoice(self,fileName)
	TLBattleScene.Instance:PlayVoice(fileName,function()
		 
	end)
end

function _M.StopVoice(self)
	TLBattleScene.Instance:StopVoice()
end

function _M.PlayBGM(self,fileName)
	TLBattleScene.Instance:PlayBGM(fileName)
end
function _M.StopBGM(self)
	TLBattleScene.Instance:StopBGM()
end
function _M.PauseBGM(self)
	TLBattleScene.Instance:PauseBGM()
end
function _M.ResumeBGM(self)
	TLBattleScene.Instance:ResumeBGM()
end

function _M.ChangeBGM(self,filename)
	TLBattleScene.Instance:ChangeBGM(filename)
end
function _M.GetStep(self)
	--print("GetStep"..self.name)
	local step = DataMgr.Instance.UserData:GetFreeData(self.name)
	print("step",step)
	return step
end

function _M.SendStep(self,content)
	--print("GetStep"..self.name)
	DataMgr.Instance.UserData:SetFreeData(self.name,content)
end


function _M.StoryFinish(self)
	TLBattleScene.Instance:StoryFinish(self.name)
end

function _M.BlackScreen(self,isShow)
	--print("GetStep"..self.name)
	TLBattleScene.Instance:BlackScreen(isShow)
end

function _M._asyncWaitSecond(self,sec)
	LuaTimer.Add(sec*1000,function()
		self:Stop()
	end)
	self:Await()
end

function _M.ChatSend(self,text)
	ChatModel.ReqChatMessage(1,text)
end

function _M.PlayRippleEffect(self)
	FullScreenEffect.Instance:ShowRippleEffect()
end

function _M.PlayUI3DEffect(self, path, duration, size)
	TLBattleScene.Instance:PlayUI3DEffect(path, duration, size)
end

function _M._asyncListenPlayerInRegion(self,regionname)
	local region = TLBattleScene.Instance:GetEditorRegionFlag(regionname)
	if region ~= nil then
		 LuaTimer.Add(0,333,function()
			if not TLBattleScene.Instance or not TLBattleScene.Instance.Actor then
				self:Stop()
				return false
			end
			local x = TLBattleScene.Instance.Actor:GetX()
			local y = TLBattleScene.Instance.Actor:GetY()
			if TLBattleScene.Instance:IsInRegion(region.Data,x,y) then
				self:Stop()
				return false
			end
			return true
		end)
		self:Await()
	end
end
function _M.PlaySceneEffect(self,key,name,params,cb)
	local pos = params.pos and Vector2(params.pos[1],params.pos[2]) or Vector2.zero
	local duration = params.duration or 1
	local direct = params.direct or 0
	local scale = params.scale or 1
	local node = params.nodeparams or ""
	TLBattleScene.Instance:PlayEffect(key,node,pos,scale,direct, name,duration,function()
		if cb then
			cb()
		end
	end)
end

function _M.RemoveEffectByKey(self,key)
	TLBattleScene.Instance:RemoveEffectByKey(key)
end

function _M.ShowLowHPEffect(self)
	FullScreenEffect.Instance:ShowLowHPEffect()
end

function _M.HideLowHPEffect(self)
	FullScreenEffect.Instance:HideLowHPEffect()
end

function _M.ShowTelescope(self)
	FullScreenEffect.Instance:ShowTelescope()
end

function _M.HideSceneEffect(self)
	FullScreenEffect.Instance:HideSceneEffect()
end

function _M.ActorChangeDirection(self,dir,isCameraFollow,issmooth)
	local Smooth = issmooth or false
	local CameraFollow = isCameraFollow or false
	TLBattleScene.Instance:ChangeDirection(dir,CameraFollow,Smooth)
end

function _M.ShowLetterBox(self,isShow)
	EventManager.Fire("ShowLetterBox", {ShowBox = isShow})
end


function _M.RefreshNpc(self)
	 EventManager.Fire("RefreshNpc",{})
end

function _M.FireEvent(self,name,params)
	 EventManager.Fire(name,params)
end

function _M.OpenUI(self,uitagbyConfig)
	 FunctionUtil.OpenFunction(uitagbyConfig,true)
end


function _M.FindTreasure(self,isShow,x,y)
	EventManager.Fire("EVENT_UI_FindTreasure",{isShow = isShow,pos = {x = x,y = y}})
end

function _M.StartClientScript(self,filename,...)
	EventApi.Task.StartEvent(filename,...)
end

function _M.ShowWeather(self,params)
    EventManager.Fire('Event.Scene.ChangeWeather', params)
end
function _M.testshowitem(self,templateId)
	local params = {TemplateId = templateId,
					x = 100,
					y = 100}
	UIUtil.ShowGetItemWay(params)
end

function _M.SendChat(self, content)

	
	local input = {}
	input.channel_type = 2
	input.show_type = 0
	input.show_channel = {}
	-- input.show_channel = {_M.ChannelState.CHANNEL_PLATFORM}
	-- table.insert(input.show_channel,_M.ChannelState.CHANNEL_PLATFORM)
	input.from_name = DataMgr.Instance.UserData.Name
	input.from_uuid = DataMgr.Instance.UserData.RoleID
	input.content = content
	input.func_type = 0

	Protocol.RequestHandler.ClientChatRequest(input, function(ret)
		if cb ~= nil then
			cb(ret)
		end
	end)
end

return _M