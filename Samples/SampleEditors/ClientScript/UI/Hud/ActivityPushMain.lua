local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local ActivityModel = require 'Model/ActivityModel'



local function GetActivityData(functionid)
	 local detail = unpack(GlobalHooks.DB.Find('ActivityPushData', {function_id = functionid}))
	 return detail
end

local function ShowTime(self,opentime,timepos)
	local startTime = TimeUtil.CustomTodayTimeToUtc(opentime.time.open[timepos]..':00')
	local delaytimestart = nil
	self.lb_lesstime1.Visible = false
	self.lb_lesstime.Visible = false
	self.btn_go.Visible = false
	self.timer2 = LuaTimer.Add(0,1000,function()
		delaytimestart = -TimeUtil.TimeLeftSec(startTime)
		if delaytimestart <= 0 then
			self.lb_lesstime.Text = Util.Format1234(Constants.Activity.Delaytimestart,0,0)
			if FunctionUtil.CheckNowIsOpen(opentime.function_id,false) then
				self.btn_go.Visible = true
				self.lb_lesstime.Visible = false
				self.lb_lesstime1.Visible = false

				if self.timer2 then
					LuaTimer.Delete(self.timer2)
				end
				return false
			else
				return true
			end
		else
			self.lb_lesstime1.Visible = true
			self.lb_lesstime.Visible = true
			self.lb_lesstime.Text = Util.Format1234(Constants.Activity.Delaytimestart,math.floor(delaytimestart/60),delaytimestart%60)
			delaytimestart = -TimeUtil.TimeLeftSec(startTime)
			return true
		end
	end)
end

local function ShowUI(self, opentime, activityinfo, timepos)
	self.lb_name.Text = Util.GetText(activityinfo.activity_name)
	self.lb_opentime.Text = Util.Format1234(Constants.Activity.ActivityPushTime,opentime.time.open[timepos],opentime.time.close[timepos])
	self.lb_form.Text = Util.GetText(activityinfo.activity_type)
	self.tb_explain.Text = Util.GetText(activityinfo.desc)

	local rewardnum = 0
	for k,v in pairs(activityinfo.awardshow.id) do
		if v ~= '' then
			rewardnum = rewardnum + 1
		end
	end
	UIUtil.ConfigHScrollPanWithOffset(self.sp_reward, self.cvs_reward, rewardnum, 10, function(node,index)
		local itemid = tonumber(activityinfo.awardshow.id[index])
		UIUtil.SetItemShowTo(node,itemid,0,0)
		node.TouchClick = function(sender)
			UIUtil.ShowTips(self,sender,itemid)
		end
	end)
	UIUtil.SetImage(self.ib_icon,activityinfo.activity_icon)
end

function _M.OnEnter( self,showlist )

	--ui穿透
    self.ui:EnableTouchFrameClose(false)
    self.globalTouchKey = GameGlobal.Instance.FGCtrl:AddGlobalTouchUpHandler("UI.ItemDetail", function() end)
	
	if self.timer2 then
		LuaTimer.Delete(self.timer2)
		self.timer2 = nil
	end

	local activityinfo = GetActivityData(showlist.function_id)
	local opentime = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', {function_id = showlist.function_id}))
	local _,_,timepos = ActivityModel.OpenTime(opentime)
	ShowUI(self,opentime,activityinfo,timepos)
	ShowTime(self,opentime,timepos)
	self.btn_go.TouchClick = function(sender)
		FunctionUtil.OpenFunction(showlist.function_id)
		self.ui:Close()
	end
	self.btn_close.TouchClick = function(sender)
		self.ui:Close()
	end
end

function _M.OnExit( self )
    if self.timer2 then
		LuaTimer.Delete(self.timer2)
		self.timer2 = nil
	end
	
	if self.globalTouchKey then
        GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(self.globalTouchKey)
        self.globalTouchKey = nil
    end
end

function _M.OnInit( self )
 	self.cvs_tips = self.root:FindChildByEditName('cvs_tips', true)

 	self.lb_name = self.cvs_tips:FindChildByEditName('lb_name', true)
	self.btn_go = self.cvs_tips:FindChildByEditName('btn_go', true)
	self.btn_close = self.cvs_tips:FindChildByEditName('btn_close', true)
	self.lb_opentime = self.cvs_tips:FindChildByEditName('lb_opentime', true)
	self.lb_form = self.cvs_tips:FindChildByEditName('lb_form', true)
	self.tb_explain = self.cvs_tips:FindChildByEditName('tb_explain', true)
	self.lb_lesstime = self.cvs_tips:FindChildByEditName('lb_lesstime', true)
	self.lb_lesstime1 = self.cvs_tips:FindChildByEditName('lb_lesstime1', true)
	self.ib_icon = self.cvs_tips:FindChildByEditName('ib_icon', true)
	self.sp_reward = self.cvs_tips:FindChildByEditName('sp_reward', true)
	self.cvs_reward = self.cvs_tips:FindChildByEditName('cvs_reward', true)

	self.cvs_reward.Visible = false

	self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	HudManager.Instance:InitAnchorWithNode(self.cvs_title, bit.bor(HudManager.HUD_CENTER,HudManager.HUD_CENTER))
end

return _M