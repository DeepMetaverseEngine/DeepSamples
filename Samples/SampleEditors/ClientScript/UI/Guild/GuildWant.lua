-- GuildWantMain.lua

local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local GuildWantModel = require 'Model/GuildWantModel'
local ChatUtil = require 'UI/Chat/ChatUtil'
local ChatModel = require "Model/ChatModel"
local TeamModel = require 'Model/TeamModel'
local QuestUtil = require 'UI/Quest/QuestUtil'
local ActivityModel = require 'Model/ActivityModel'


local function Init3DSngleModel(self, parentCvs, pos2d, scale, menuOrder,fileName,index)
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	self.model[index] = info
	return info
end

local function Release3DModel(self,index)
	if self and self.model then
		if index then
			if self.model[index] then
				UI3DModelAdapter.ReleaseModel(self.model[index].Key)
			end
		else
			for i=1,3 do
				if self.model[i] then
					UI3DModelAdapter.ReleaseModel(self.model[i].Key)
				end
			end
			self.model = {}
		end
	end
end

local function SetDifficult(self,difficult,node)
	local ib_star = {}
	for i=1,5 do
		ib_star[i] = node:FindChildByEditName('ib_star'..i, true)
		if difficult >= i then
			ib_star[i].Visible = true
		else
			ib_star[i].Visible = false
		end
	end
	ib_star = nil
end

local function SetReward(self ,data ,node)
	local itemnode = {}
	for i=1,#data.showitem.key do
		itemnode[i] = node:FindChildByEditName('cvs_item'..i, true)
		if data.showitem.key[i] ~= 0 then
			itemnode[i].Visible = true
			UIUtil.SetItemShowTo(itemnode[i],data.showitem.key[i], data.showitem.val[i])
			itemnode[i].TouchClick = function(sender)
				UIUtil.ShowTips(self,sender,data.showitem.key[i] )
			end
		else
			itemnode[i].Visible = false
		end
	end
	itemnode = nil
end

local function RefreshData(self,data)
	for i,v in ipairs(data) do
		if not self.staticdata[i] or not self.staticdata[i].loaded then
			local detail = GuildWantModel.GetWantData(v.wantedid)
			detail.state = v.state
			detail.loaded = false
			self.staticdata[i] = detail
		end
	end
    _M.SetInfo(self)
	_M.SetTips(self)
end

local function LoadEffect(self,parent,filename,pos,scale)
	local param = 
				{
					Pos = pos,
					Parent = parent.UnityObject.transform,
					LayerOrder = self.ui.menu.MenuOrder,
					Scale = scale,
					UILayer = true,
				}
	
	return Util.PlayEffect(filename,param)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

local function SetBtn(self,parent,flag,index)

	local function TeamShout()
		local callinfo = Util.Format1234(Constants.GuildWant.CallConstants,self.staticdata[index].quality,Util.GetText(self.staticdata[index].name))
		ChatUtil.TeamShout(callinfo,ChatModel.ChannelState.CHANNEL_PLATFORM,nil,function ()
			ChatUtil.TeamShout(callinfo,ChatModel.ChannelState.CHANNEL_GUILD,nil, function()
				GuildWantModel.RefreshCallTime(index)
				SetBtn(self,parent,2,index)
			end)
		end)
	end


	local btn = {}
	btn[1] = parent:FindChildByEditName('btn_getquest', true)
	btn[2] = parent:FindChildByEditName('btn_dropquest', true)
	btn[3] = parent:FindChildByEditName('btn_getreward', true)
	btn[4] = parent:FindChildByEditName('btn_findteam', true)
	local lb_cd = parent:FindChildByEditName('lb_cd', true)


	for i=1,#btn do
		btn[i].Visible = i == flag
		if i == 3 then
			if self.effect[index] then
				UnLoadEffect(self.effect[index])
			end
			self.effect[index] = LoadEffect(
				self,
				btn[i],
				"/res/effect/ui/ef_ui_frame_01.assetbundles",
				Vector3(btn[3].Size2D[1]/2+1,-btn[3].Size2D[2]/2+2,0),
				Vector3(0.93,0.76,1))
		end
	end
	btn[4].Visible = flag == 2
	lb_cd.Visible = false

	btn[4].Enable = true
	btn[4].IsGray = false

	if GuildWantModel.Calltime[index] ~= 0 and flag == 2 then
		if self.timer[index] then
			LuaTimer.Delete(self.timer[index])
			self.timer[index] = nil
		end
		self.timer[index] = LuaTimer.Add(0,1000,function()
			if GuildWantModel.Calltime[index] > 0 then
				btn[4].Enable = false
				btn[4].IsGray = true
				lb_cd.Visible = true
				lb_cd.Text = '('..GuildWantModel.Calltime[index]..'s)'
				return true
			else
				btn[4].Enable = true
				btn[4].IsGray = false
				lb_cd.Visible = false
				lb_cd.Text = '('..GuildWantModel.Calltime[index]..'s)'
				-- LuaTimer.Delete(self.timer[index])
				return false
			end
		end)
	end

	btn[1].TouchClick = function(sender)
		if self.data.curReceivedTimes >= self.data.maxReceivedTimes then
			UIUtil.ShowOkAlert(Util.GetText(Constants.GuildWant.CountLimit))
		else
			GuildWantModel.AccpetGuildWanted(self.staticdata[index].wanted_id,function(rsp)
				self.lb_got.Text = Util.GetText('mail_list', rsp.s2c_ReceiveTime, self.data.maxReceivedTimes)
				self.data.curReceivedTimes = rsp.s2c_ReceiveTime
				self.staticdata[index].state = 2
				SetBtn(self,parent,2,index)
			end)
		end
	end
	btn[2].TouchClick = function(sender)
		QuestUtil.doQuestById(self.staticdata[index].questid)
	end
	btn[3].TouchClick = function(sender)
		ActivityModel.BagIsCanUse(3,function()
			if self.oneEffect then
				UnLoadEffect(self.oneEffect)
			end
			self.oneEffect = LoadEffect(self,self.cvs_quest[index],
				"/res/effect/ui/ef_ui_xianmen_postreward.assetbundles",
				Vector3(self.cvs_quest[index].Size2D[1]/2-4,-self.cvs_quest[index].Size2D[2]/2+16,-400),
				Vector3(1,1,1))
			GuildWantModel.SubmitGuildWanted(self.staticdata[index].wanted_id,function(rsp)
				self.staticdata[index].loaded = false
				RefreshData(self,rsp.s2c_data)
				UnLoadEffect(self.effect[index])
			end)
		end)
	end
	btn[4].TouchClick = function(sender)
		if DataMgr.Instance.TeamData:IsLeader() == false and DataMgr.Instance.TeamData.HasTeam then
			GameAlertManager.Instance:ShowNotify(Util.GetText('dungeon_enter_button'))
		else
			if DataMgr.Instance.TeamData.HasTeam == false then
				TeamModel.RequestCreateTeam(function()
					TeamShout()
				end)
			else
				if DataMgr.Instance.TeamData.IsInMatch then
					TeamModel.ReuestLeaveAutoMatch(function()
						UIUtil.ShowConfirmAlert(Util.GetText(Constants.GuildWant.CallTip),Util.GetText(Constants.GuildWant.CallTittle),function()
							TeamShout()
						end,nil)
					end)
				else
					TeamShout()
				end
			end
		end
	end
end

function _M.SetInfo(self)
	for i=1,#self.staticdata do
		if not self.staticdata[i].loaded then
			Release3DModel(self,i)
			self.lb_name[i].Text = Util.GetText(self.staticdata[i].name)
			local info = Init3DSngleModel(self, self.cvs_anchor[i], Vector2(0,0), self.staticdata[i].scale, self.ui.menu.MenuOrder,self.staticdata[i].monster.id,i)
			info.Callback = function(model)
				model.DC.localEulerAngles = Vector3(0,self.staticdata[i].rotate,0)
			end
			self.lb_playernum[i].Text = self.staticdata[i].teamnum
			SetDifficult(self,self.staticdata[i].quality,self.cvs_difficult[i])
			SetReward(self,self.staticdata[i],self.cvs_reward[i])
			SetBtn(self,self.cvs_quest[i],self.staticdata[i].state,i)
			self.staticdata[i].loaded = true
		end
	end
end

function _M.SetTips(self)
	self.lb_got.Text = Util.GetText('mail_list', self.data.curReceivedTimes, self.data.maxReceivedTimes)
	self.lb_join.Text = Util.GetText('mail_list', self.data.curPartakeTimes, self.data.maxPartakeTimes)

	if self.data.curRefreshTimes ~= 0 then
		self.lb_refresh.Visible = true
		self.lb_refresh.Text = Util.GetText('mail_list', self.data.curRefreshTimes, self.data.maxRefreshTimes)
		self.lb_refresh1.Visible = true
		self.cvs_caluerefre.Visible = false
	else
		self.lb_refresh.Visible = false
		self.lb_refresh1.Visible = false
		self.cvs_caluerefre.Visible = true
	end
	
	
	local refershtime = self.data.refreshTime
	local count = 2
	local delaytime = nil
	if self.time then
		LuaTimer.Delete(self.time)
		self.time = nil
	end
	self.time = LuaTimer.Add(0,1000,function()
		delaytime = -TimeUtil.TimeLeftSec(refershtime)
		if delaytime > 0 then
			count = 2
			if self.lb_cd1.Visible == false then
				self.lb_cd1.Visible = true
			end
			self.lb_cd1.Text = TimeUtil.SecToTimeformat( delaytime )
			return true
		else
			self.lb_cd1.Text ='(00:00:00)'
			GuildWantModel.RefreshTimeRequest(function(rsp)
				self.data.refreshTime = rsp.s2c_RefreshDateTime
				self.data.curRefreshTimes = rsp.s2c_RefreshTime
				self.lb_refresh1.Visible = true
				self.lb_refresh.Visible = true
				self.cvs_caluerefre.Visible = false
				refershtime = rsp.s2c_RefreshDateTime
			end)
			count = count - 1
			if count <= 0 then
				self.lb_cd1.Visible = false
				self.lb_refresh1.Visible = false
				self.lb_refresh.Visible = false
				self.cvs_caluerefre.Visible = true
				
				return false
			end
			return true
		end
	end)

end

function _M.ShowUI(self)
	GuildWantModel.GetGuildWantedInfo(function(rsp)
		self.data = rsp.s2c_data
		self.staticdata = {}
		for i,v in ipairs(self.data.QuestMap) do
			local detail = GuildWantModel.GetWantData(v.wantedid)
			detail.state = v.state
			if self.staticdata[i] then
				detail.loaded = self.staticdata[i].loaded or false
			else
				detail.loaded = false
			end
			self.staticdata[i] = detail
		end
		_M.SetInfo(self)
		_M.SetTips(self)
	end)
end

function _M.OnEnter( self )
	self.timer = {}
	self.effect = {}
	_M.ShowUI(self)
	self.btn_refresh.TouchClick = function(sender)
		for i, v in pairs(self.staticdata) do
			v.loaded = false
		end
		if self.data.curRefreshTimes == 0 then
			local alertKey = UIUtil.ShowCheckBoxConfirmAlert("GuildWantpayRefresh",
				string.gsub(Util.GetText(Constants.GuildWant.HelpTxt),"\\n","\n"),
				Util.GetText(Constants.GuildWant.CallTittle),
				function()
					GuildWantModel.RefreshGuildWanted(function(rsp)
						self.data.refreshTime = rsp.s2c_RefreshDateTime
						self.data.curRefreshTimes = rsp.s2c_RefreshTime
						if self.time then
							LuaTimer.Delete(self.time)
							self.time = nil
						end
						RefreshData(self,rsp.s2c_data)
					end)
				end)
			GameAlertManager.Instance.AlertDialog:SetDialogAnchor(alertKey, CommonUI.TextAnchor.C_C)
		else
			GuildWantModel.RefreshGuildWanted(function(rsp)
				self.data.refreshTime = rsp.s2c_RefreshDateTime
				self.data.curRefreshTimes = rsp.s2c_RefreshTime
				if self.time then
					LuaTimer.Delete(self.time)
					self.time = nil
				end
				RefreshData(self,rsp.s2c_data)
			end)
		end
	end
	self.btn_help.TouchClick = function(sender)
		self.ui.comps.cvs_tip.Visible = true
	end
	self.ui.comps.cvs_tip.TouchClick = function(sender)
		self.ui.comps.cvs_tip.Visible = false
	end
end

function _M.OnInit( self )
	self.cvs_info = self.ui.comps.cvs_info
	self.cvs_tips = self.ui.comps.cvs_tips
	self.btn_call = self.ui.comps.btn_call
	self.model = {}
	

	self.lb_got = self.cvs_tips:FindChildByEditName('lb_got', true)
	self.lb_join = self.cvs_tips:FindChildByEditName('lb_join', true)
	self.lb_refresh = self.cvs_tips:FindChildByEditName('lb_refresh', true)
	self.lb_refresh1 = self.cvs_tips:FindChildByEditName('lb_refresh1', true)
	self.cvs_caluerefre = self.cvs_tips:FindChildByEditName('cvs_caluerefre', true)
	self.lb_cd1 = self.cvs_tips:FindChildByEditName('lb_cd1', true)
	self.btn_help = self.cvs_tips:FindChildByEditName('btn_help', true)
	self.btn_refresh = self.cvs_tips:FindChildByEditName('btn_refresh', true)
	

	self.cvs_quest = {}
	self.lb_name = {}
	self.cvs_anchor = {}
	self.lb_playernum = {}
	self.cvs_difficult = {}
	self.cvs_reward = {}
	for i=1,3 do
		self.cvs_quest[i] = self.cvs_info:FindChildByEditName('cvs_quest'..i, true)
		self.lb_name[i] = self.cvs_quest[i]:FindChildByEditName('lb_name', true)
		self.cvs_anchor[i] = self.cvs_quest[i]:FindChildByEditName('cvs_anchor', true)
		self.lb_playernum[i] = self.cvs_quest[i]:FindChildByEditName('lb_playernum', true)
		self.cvs_difficult[i] = self.cvs_quest[i]:FindChildByEditName('cvs_difficult', true)
		self.cvs_reward[i] = self.cvs_quest[i]:FindChildByEditName('cvs_reward', true)
	end
	self.ui.menu:SetUILayer(self.ui.comps.cvs_tip,self.ui.menu.MenuOrder + UILayerMgr.MenuOrderSpace,-600)

end

function _M.OnExit(self)
	if self.oneEffect then
		UnLoadEffect(self.oneEffect)
	end
	Release3DModel(self)
	if self.effect then
		for i,v in pairs(self.effect) do
			UnLoadEffect(self.effect[i])
		end
		self.effect = nil
	end
	for i=3,1,-1 do
		if self.timer[i] then
			LuaTimer.Delete(self.timer[i])
			self.timer[i] = nil
		end		
	end
	self.timer = nil
	if self.time then
		LuaTimer.Delete(self.time)
		self.time = nil
	end
	self.staticdata = nil
	self.ui.comps.cvs_tip.Visible = false
end



return _M