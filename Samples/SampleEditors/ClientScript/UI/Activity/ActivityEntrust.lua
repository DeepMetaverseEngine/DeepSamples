local _M = {}
_M.__index = _M


local UIUtil = require 'UI/UIUtil'
local ActivityModel = require 'Model/ActivityModel'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local QuestUtil = require 'UI/Quest/QuestUtil'
local DisplayUtil = require"Logic/DisplayUtil"


local function LoadEffect(self,parent,clip,filename,pos)
	local param =
	{
		Pos = pos,
		Clip = clip.Transform,
		DisableToUnload = true,
		Parent = parent.UnityObject.transform,
		LayerOrder = self.ui.menu.MenuOrder,
		Scale = Vector3(1,0.78,1),
		UILayer = true,
	}

	return Util.PlayEffect(filename,param,true)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

local function Update(self)
	for k, v in pairs(self.timerdata) do
		if self.data[v.index].cdtime > 0 then
			v.node.Text = TimeUtil.SecToTimeformat(self.data[v.index].cdtime)
		else
			if v.node.Parent.Visible == true then
				self.sp_info:RefreshShowCell()
			end
		end
	end
end

local function InitData(self,node,index)

	local function RefreshTask(self,node,index)
		ActivityModel.RefreshEntrustTime(index-1,function(rsp)
			self.data[index] = ActivityModel.GetEntrustDataByWantid(rsp.s2c_data.wantedid)
			self.data[index].state = rsp.s2c_data.state
			self.data[index].cdtime = rsp.s2c_data.cdtime
			InitData(self,node,index)
		end)
	end

	local ib_doing = node:FindChildByEditName('ib_doing', true)
	local ib_finish = node:FindChildByEditName('ib_finish', true)
	local lb_name = node:FindChildByEditName('lb_name', true)
	local lb_tex = node:FindChildByEditName('lb_tex', true)
	local sp_reward = node:FindChildByEditName('sp_reward', true)
	local cvs_item1 = node:FindChildByEditName('cvs_item1', true)
	local btn_getquest = node:FindChildByEditName('btn_getquest', true)
	local btn_getreward = node:FindChildByEditName('btn_getreward', true)
	local btn_go = node:FindChildByEditName('btn_go', true)
	local cvs_cd = node:FindChildByEditName('cvs_cd', true)
	local lb_time = node:FindChildByEditName('lb_time', true)
	--local lb_red_getquest = node:FindChildByEditName('lb_red_getquest', true)
	local lb_require = node:FindChildByEditName('lb_require', true)
	if lb_time.UserTag == 0 then
		lb_time.UserTag = index
	end


	if node.UserTag ~= 0 then
		UnLoadEffect(node.UserTag)
		node.UserTag = 0
	end
	
	if self.data[index].cdtime ~= 0 then
		cvs_cd.Visible = true
		lb_time.Text = TimeUtil.SecToTimeformat(self.data[index].cdtime)
		self.timerdata[lb_time.UserTag] = {index = index,node = lb_time}
	else
		cvs_cd.Visible = false
		lb_name.Text = Util.GetText(self.data[index].name)
		lb_require.Text = Util.GetText(self.data[index].quest_aim)
		lb_tex.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
		lb_tex.Text  = Util.GetText(self.data[index].desc_describe)
		
		ib_doing.Visible = self.data[index].state == 2
		ib_finish.Visible = self.data[index].state == 3

		btn_getquest.Visible = self.data[index].state == 1
		--lb_red_getquest.Visible = self.data[index].state == 1
		btn_getreward.Visible = self.data[index].state == 3
		btn_go.Visible = self.data[index].state == 2
		
		if self.data[index].state == 3 then
			if node.UserTag == 0 then
				node.UserTag = LoadEffect(
						self,
						btn_getreward,
						self.sp_info,
						"/res/effect/ui/ef_ui_frame_01.assetbundles",
						Vector3(btn_getreward.Size2D[1]/2,-btn_getreward.Size2D[2]/2+1,0),
						Vector3(1,1,1))
			end
		end


		UIUtil.ConfigHScrollPan(sp_reward,cvs_item1, #self.data[index].showitem.key, function(node, i)
			UIUtil.SetItemShowTo(node, self.data[index].showitem.key[i], self.data[index].showitem.val[i])
			node.TouchClick = function(sender)
				UIUtil.ShowTips( self,sender,self.data[index].showitem.key[i] )
			end
		end)
		cvs_item1.Visible = false
		btn_getquest.TouchClick = function(sender)
			ActivityModel.AccpetEntrustTask(self.data[index].wanted_id,function(rsp)
				self.data[index].state = 2
				ib_doing.Visible = true
				ib_finish.Visible = false
				btn_getquest.Visible = false
				btn_go.Visible = true
			end)
		end
		btn_getreward.TouchClick = function(sender)
			ActivityModel.SubmitEntrustTask(self.data[index].wanted_id,function()
				RefreshTask(self,node,index)
			end)
		end
		btn_go.TouchClick = function(sender)
			QuestUtil.doQuestById(self.data[index].questid)
		end
	end
end

function _M.OnEnter( self )
	self.timerdata = {}
	self.data = {}
	
	
	ActivityModel.GetEntrustData(function(rsp)
		if rsp then
			self.data = rsp
			UIUtil.ConfigHScrollPan(self.sp_info,self.cvs_info, #self.data, function(node, index)
				InitData(self,node,index)
			end)
			self.cvs_info.Visible = false

			if self.timer then
				LuaTimer.Delete(self.timer)
				self.timer = nil
			end
			self.timer = LuaTimer.Add(0,1000,function()
				Update(self)
				return true
			end)
		end
	end)

	self.btn_help.TouchClick = function(sender)
		self.cvs_tips.Visible = true
	end
	self.cvs_tips.TouchClick = function(sender)
		self.cvs_tips.Visible = false
	end
end

function _M.OnInit( self )

	self.cvs_entrust = self.ui.menu:FindChildByEditName('cvs_entrust', true)
	self.cvs_tips = self.ui.menu:FindChildByEditName('cvs_tips', true)

	self.sp_info = self.cvs_entrust:FindChildByEditName('sp_info', true)
	self.cvs_info = self.cvs_entrust:FindChildByEditName('cvs_info', true)

	self.cvs_tip = self.cvs_entrust:FindChildByEditName('cvs_tip', true)
	self.btn_help = self.cvs_tip:FindChildByEditName('btn_help', true)


	self.ui.menu.ShowType = UIShowType.HideHudAndMenu
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

function _M.OnExit( self )

	if self.timer then
		LuaTimer.Delete(self.timer)
		self.timer = nil
	end
	self.timerdata = nil
end

return _M
