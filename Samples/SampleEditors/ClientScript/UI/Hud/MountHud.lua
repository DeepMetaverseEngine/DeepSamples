
local _M = {}
local Util      = require "Logic/Util"
local UIUtil = require 'UI/UIUtil.lua'

local MountModel

local self = {}
self.updateTime = 0

function _M.Bind(btn)
	setmetatable(self, {__index=_M})
	self:Init(btn)
	return self
end

local function OnRideStatusChange(eventname, params)
	-- print("OnRideStatusChange:",params.rideStatus)
	self.rideStatus = params.rideStatus
	if params.isRideFailed == true then
		local tips 
		if not string.IsNullOrEmpty(params.ReasonKey) then
			tips = Util.GetText(params.ReasonKey)
		else
			tips = Util.GetText('mount_inbattle')
		end
		GameAlertManager.Instance:ShowNotify(tips)
	end
end


local function OnRideMount(eventname, params)
	-- print('OnRideMount eventname:',eventname,'   ___params.ride : ',params.ride)
	-- 狂点的时候切场景了 主角有可能为空
	if TLBattleScene.Instance == nil or TLBattleScene.Instance.Actor == nil then
		return
	end

	local isOpen = GlobalHooks.IsFuncOpen('MountFrame',true)
	if not isOpen then
		return
	end

	--当前场景能否骑乘.
	local id = DataMgr.Instance.UserData.MapTemplateId
	local mapdata = GlobalHooks.DB.Find('MapData', id)
	if mapdata ~= nil and mapdata.ride_mount == 0 then
		if params.RideByUser then
			GameAlertManager.Instance:ShowFloatingTips(Util.GetText('scene_not_ride_mount'))
		end
		return
	end


	local isDead = TLBattleScene.Instance.Actor:Dead()
	if isDead then
			--TODO需要提示
			-- GameAlertManager.Instance:ShowNotify('死亡状态不能骑乘坐骑')
		return
	end

	local status = TLBattleScene.Instance.Actor:isNoBattleStatus()
	if status == false and params.RideByUser then
		local tips = Util.GetText('mount_inbattle')
		GameAlertManager.Instance:ShowNotify(tips)
		return
	end

 	

	if self.updateTime > 0 then
		-- print('isClick == true')
		if params.RideByUser then
			local tips = Util.GetText('mount_notice')
			local message = Util.Format1234(tips,self.updateTime)
			GameAlertManager.Instance:ShowNotify(message)
		end
		return
	end
 
 	--Request的第一个参数其实已经没有实际意义了
 	local ride = params.ride
	MountModel.RequestRidingMount(false,function()
		self.updateTime = self.mountInterval
		if self.expTimeId ~= nil then
			LuaTimer.Delete(self.expTimeId)
			self.expTimeId = nil
		end
		self.expTimeId = LuaTimer.Add(0,1000,function(id)
			self.updateTime = self.updateTime - 1
			if self.updateTime <= 0 then
				self.updateTime = 0
				return false
			end
			return true
		end)

	end)
end


function _M:Init(root)

	MountModel = require "Model/MountModel"

	self.mountInterval = GlobalHooks.DB.GetGlobalConfig('mount_interval')
	self.updateTime = 0

	print('mountInterval:',self.mountInterval)

	self.btn_zuoqi = root
	self.rideStatus = false
	

	self.btn_zuoqi.TouchClick = function(sender)
		
		local ride = true
		local params = {}
		params.ride = ride
		params.RideByUser = true
		OnRideMount('ClickButton',params)
 
	end
end

local function OnUnlockMount(eventname, params)
	local mountId = params.mountId
	local mountData = GlobalHooks.DB.FindFirst('Mount',{avatar_id = mountId})
	if mountData == nil then
		return
	end
	local name =  Util.GetText(mountData.name)
	local tips = Util.GetText('mount_unlock')

	local message = Util.Format1234(tips,name)
 	UIUtil.ShowOkAlert(message)
end

local function fin()
	-- print ("FunctionMenu fin ")
	EventManager.Unsubscribe("Event.UI.RideStatusChange", OnRideStatusChange)
	-- EventManager.Unsubscribe("Event.UI.UnlockMount", OnUnlockMount)
	EventManager.Unsubscribe("Event.Mount.RideMount", OnRideMount)
end

local function initial()
	-- print ("FunctionMenu initial ")
	EventManager.Subscribe("Event.UI.RideStatusChange", OnRideStatusChange)
 	EventManager.Subscribe("Event.Mount.RideMount", OnRideMount)
	-- EventManager.Subscribe("Event.UI.UnlockMount", OnUnlockMount)
end

_M.initial = initial
_M.fin = fin

return _M