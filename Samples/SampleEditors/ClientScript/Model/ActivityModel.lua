local _M = {}
_M.__index = _M


local TimeUtil = require 'Logic/TimeUtil'
local FunctionUtil = require 'UI/FunctionUtil'
local Util = require 'Logic/Util'
local ServerTime = require 'Logic/ServerTime'


_M.entrustCanGetCount = nil
_M.doingtaskids = {}
_M.activitycount = 0
local timer = nil

local function IsOpenLevel(activity,roleLevel)
	if roleLevel == nil then
		roleLevel = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)
	end
	return activity.player_lv <= roleLevel
end

local function GetActivityUserData(cb,force)
	if force == nil then
		force = true
	end
	local msg = {}
	Protocol.RequestHandler.ClientGetActivityDataRequest(msg, function(rsp)
		local count = 0
		for i, v in pairs(rsp.s2c_data.RewardRecord) do
			if v == 0 then
				if i <= rsp.s2c_data.ActivityPoint then
					count = count + 1
				end
			end
		end
		_M.activitycount = count
		GlobalHooks.UI.SetRedTips("activity",count)
	    if cb then
	        cb(rsp)
	    end
    end,PackExtData(force, force))
end

local function SetActivityUserData(pointLv, cb)
	local msg = { c2s_pointLv = pointLv }
	Protocol.RequestHandler.ClientGetActivityRewardRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

local function GetOpenTime( functionid )
	local detail = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', {function_id = functionid}))
	return detail
end

local function GetSubData()
	local detail = GlobalHooks.DB.GetFullTable('Activity_type')
	return detail
end

local function CompareTime(timetable1,timetable2)
	if timetable1[1] ~= timetable2[1] then
		return timetable1[1]>timetable2[1]
	else
		return timetable1[2]>timetable2[2]
	end
end

local function OpenTime(functionid)
	local serverhour = ServerTime.getServerTime():ToLocalTime()
	local servertime = {serverhour.Hour,serverhour.Minute}
	local activitystate = 3
	local opentime = functionid
	if type(functionid) ~= 'table' then
		opentime = GetOpenTime(functionid)
	end
	if opentime.open_type == 1 and opentime then
		local startTime = TimeUtil.CustomTodayTimeToUtc('00:00:00')
		return TimeUtil.TimeLeftSec(startTime),true,1,activtystate
	end
	local startTime = nil
	local endTime = nil
	local delaytimestart = nil
	local delaytimeend = nil
	local shouldusetime = nil
	local shouldusepos = nil

	for i=1,#opentime.time.open do
		if not string.IsNullOrEmpty(opentime.time.open[i]) then
			startTime = TimeUtil.CustomTodayTimeToUtc(opentime.time.open[i]..':00')
			endTime = TimeUtil.CustomTodayTimeToUtc(opentime.time.close[i]..':00')

			local temp = {startTime:ToLocalTime().Hour,startTime:ToLocalTime().AddMinutes}
			if CompareTime(_M.configresethour ,servertime) and not CompareTime(_M.configresethour,temp) then
				startTime = startTime:AddDays(-1)
			end

			temp = {endTime:ToLocalTime().Hour,endTime:ToLocalTime().AddMinutes}
			if CompareTime(_M.configresethour,servertime) and not CompareTime(_M.configresethour,temp) then
				endTime = endTime:AddDays(-1)
			end

			delaytimestart = TimeUtil.TimeLeftSec(startTime)
			delaytimeend = TimeUtil.TimeLeftSec(endTime)

			if delaytimestart/delaytimeend < 0 then
				activitystate = 2
				return delaytimestart,true,i,activitystate
			else
				if not shouldusetime then
					shouldusetime = delaytimestart
					shouldusepos = i
				else
					if shouldusetime < 0 and delaytimestart < 0 then
						shouldusetime = shouldusetime > delaytimestart and shouldusetime or delaytimestart
					elseif shouldusetime > 0 then
						shouldusetime = delaytimestart < shouldusetime and delaytimestart or shouldusetime
					end
					if shouldusetime == delaytimestart then
						shouldusepos = i
					end
				end
			end
			
		end
	end
	if string.IsNullOrEmpty(opentime.time.open[shouldusepos+1]) and shouldusetime > 0 then
		activitystate = 4
	end
	return shouldusetime,false,shouldusepos,activitystate
end

local function IsDone(activity,rsp)
	for i=1,#rsp.ActivityList do
		if rsp.ActivityList[i].function_id == activity.function_id then
			activity.cur_val = rsp.ActivityList[i].cur_val
			activity.target_val = rsp.ActivityList[i].target_val
			activity.IsDone = activity.cur_val == activity.target_val and activity.target_val ~= 0
			return activity
		end
	end
end

local function GetActivityData(rsp,activitytype)
	local Activity = {}
	local datail = GlobalHooks.DB.GetFullTable('ActivityData')
	for i,v in ipairs(datail) do
		local _,temp = FunctionUtil.CheckNowIsOpen(v.function_id)
		if temp ~= -1 and IsOpenLevel(v,nil) then
			local temp = IsDone(v,rsp)
			if v.activity_type == activitytype then
				table.insert(Activity,temp)
			end
		end
	end
	table.sort(Activity,function(a , b )
		local aOpenTime,aIsOpen,_,aactivitystate = OpenTime(a.function_id)
		local bOpenTime,bIsOpen,_,bactivitystate = OpenTime(b.function_id)
		if a.IsDone == false and b.IsDone == false then
			if IsOpenLevel(a) == true and IsOpenLevel(b) == true then
				a.activitystate = aactivitystate
				b.activitystate = bactivitystate
				if aIsOpen and bIsOpen then
					return a.order < b.order
				elseif not aIsOpen and not bIsOpen then
					if aOpenTime < 0 and bOpenTime < 0 then
						return aOpenTime > bOpenTime
					else
						return aOpenTime < 0 and bOpenTime > 0 
					end
				else
					return aIsOpen and not bIsOpen
				end
			elseif IsOpenLevel(a) == true and IsOpenLevel(b) == false then
				return true
			else
				return false
			end
		elseif a.IsDone == false and b.IsDone == true then
			return true
		else
			return false
		end
	end)
	return Activity
end 

local function BagIsCanUse(rewardnum,canusecb)
	local num = DataMgr.Instance.UserData.Bag.EmptySlotCount
	if num>=rewardnum then
		if canusecb then
			canusecb()
		end
	else
		GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.Activity.BagNotEmpty))
	end
end

local function GetItemData(ItemID)
	local item = unpack(GlobalHooks.DB.Find('Item', {id = ItemID}))
	return item
end


local function GetEntrustDataByWantid( wantid )
	local detail = unpack(GlobalHooks.DB.Find('Consignation_reward', {wanted_id = wantid}))
	return detail
end

local function GetEntrustData(cb,force)
	if force == nil then
		force = true
	end
	local msg = {}
	Protocol.RequestHandler.TLClientGetConsignationInfoRequest(msg, function(rsp)
		local count = 0
		_M.doingtaskids = {}
		_M.entruestdata = {}
		for k,v in ipairs(rsp.s2c_data.QuestMap) do
			_M.entruestdata[v.index+1] = GetEntrustDataByWantid(v.wantedid)
			_M.entruestdata[v.index+1].state = v.state
			_M.entruestdata[v.index+1].cdtime = v.cdtime
			if v.state == 3 then
				count = count + 1
			end
			if v.state == 2 then
				table.insert(_M.doingtaskids,_M.entruestdata[v.index+1].submitquestid)
			end
		end
		rsp.s2c_data.QuestMap = _M.entruestdata
		_M.entrustCanGetCount = count
		GlobalHooks.UI.SetRedTips("activityentrust",count)
		if cb then
	        cb(rsp.s2c_data.QuestMap)
	    end
		
    end,PackExtData(force, force))
end

local function AccpetEntrustTask(wantid,cb)
	local msg = {c2s_wantedId = wantid}
	Protocol.RequestHandler.TLClientAccpetConsignationRequest(msg, function(rsp)
		local temp = GetEntrustDataByWantid(wantid)
		table.insert(_M.doingtaskids,temp.submitquestid)
	    if cb then
	        cb(rsp)
	    end
    end)
end

local function SubmitEntrustTask(wantid,cb)
	local msg = {c2s_wantedId = wantid}
	Protocol.RequestHandler.TLClientSubmitConsignationRequest(msg, function(rsp)
		_M.entrustCanGetCount = _M.entrustCanGetCount - 1
		GlobalHooks.UI.SetRedTips("activityentrust",_M.entrustCanGetCount)
		if cb then
	        cb(rsp)
	    end
    end)
end

local function RefreshEntrustTime(pos,cb)
	local msg = {index = pos}
	Protocol.RequestHandler.TLClientConsignationRefreshTimeRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

local function Update(self)
	if self.entruestdata then
		for i, v in pairs(self.entruestdata) do
			if v.state == 1 and v.cdtime < 1 then
				GlobalHooks.UI.SetRedTips("activityentrust",1,i)
			else
				if v.cdtime > 0 then
					v.cdtime = v.cdtime - 1
				end
				GlobalHooks.UI.SetRedTips("activityentrust",0,i)
			end
		end
	end
end

function _M.initial()
	if not _M.timer then
		_M.timer = LuaTimer.Add(0,1000,function()
			Update(_M)
			return true
		end)
	end
end

function _M.fin()
	if _M.timer then
		LuaTimer.Delete(_M.timer)
		_M.timer = nil
	end
	if _M.SevenTime then
		LuaTimer.Delete(_M.SevenTime)
		_M.SevenTime = nil
	end
end


_M.OpenDay = nil
_M.SevenOpeningRedPoint = {}
local function OnOpeningEventCompletedNotify(notify)
	if notify.ShowTips then
		if _M.OpenDay then
			table.insert(_M.SevenOpeningRedPoint,notify.s2c_data[1])
			if notify.s2c_data[1].type <= _M.OpenDay then
				GlobalHooks.UI.SetRedTips('openingevent',1)
			end
		else
			Protocol.RequestHandler.TLClientOpeningEventListRequest({c2s_type = 0}, function(rsp)
				_M.OpenDay = rsp.s2c_day
				table.insert(_M.SevenOpeningRedPoint,notify.s2c_data[1])
				if notify.s2c_data[1].type <= _M.OpenDay then
					GlobalHooks.UI.SetRedTips('openingevent',1)
				end
			end)
		end
	else
		_M.SevenOpeningRedPoint = notify.s2c_data
		for i, v in ipairs(notify.s2c_data) do
			if v.state == _M.OpeningEventType.WaitForGetReward then
				GlobalHooks.UI.SetRedTips('openingevent',1)
				break
			end
		end
	end
end

function _M.InitNetWork(initNotify)
	if initNotify then
		Protocol.PushHandler.TLClientOpeningEventCompletedNotify(OnOpeningEventCompletedNotify)--监听七日任务完成
	end
end

-----------------------------SevenDay---------------------------
--SevenDayInfo
--SevenDayScore

_M.OpeningEventType =
{
	NoVaild = 0,
	NotCompleted = 1,
	WaitForGetReward = 2,
	Finished = 3,
}

function _M.GetActByDay(day)
	local all = GlobalHooks.DB.GetFullTable('SevenDayInfo')
	local temp={}
	for i, v in ipairs(all) do
		if v.type == day then
			table.insert(temp,all[i])
		end
	end
	table.sort(temp,function (a,b)
		return a.id < b.id
	end)
	return temp
end

function _M.GetActById(id)
	local data = unpack(GlobalHooks.DB.Find('SevenDayInfo', { id = id }))
	return data
end

function _M.GetExchangeScore()
	local all = GlobalHooks.DB.GetFullTable('SevenDayScore')
	table.sort(all,function (a,b)
		return a.id < b.id 
	end)
	return all
end
--请求七日活动数据
function _M.RequestOpeningEventList(type,cb)
	local msg = { c2s_type = type}
	Protocol.RequestHandler.TLClientOpeningEventListRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end
--领取单个任务奖励
function _M.RequestGetOpeningEventReward(id,cb)
	local msg = { c2s_id = id}
	Protocol.RequestHandler.TLClientGetOpeningEventRewardRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end
--领取积分奖励
function _M.RequestOpeningEventExcharge(id,cb)
	local msg = { c2s_id = id }
	Protocol.RequestHandler.TLClientOpeningEventExchargeRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end

-------------------------------------------------------------------

--------------------------PowerRank-----------------------------
--FightPower
function _M.GetPowerRankInfo()
	local all = GlobalHooks.DB.GetFullTable('FightPower')
	table.sort(all,function (a,b)
		return a.id < b.id
	end)
	return all
end

-------------------------------------------------------------------

----------------------TurnTable---------------------------------
--TurnTable
--TurnTableType
function _M.GetTurnTableInfoByTime(type,time)
	local turn=unpack(GlobalHooks.DB.Find('TurnTable',function (add)
		return add.activity_id == type and add.time == time
	end))
	return turn
end 
function _M.GetTotalTime(type)
	local tottime = unpack(GlobalHooks.DB.Find('TurnTableType',{id == type}))
	return tottime.max_time
end
--请求奖励
function _M.RequestGetTurnTableReward(type,cb)
	local msg = { c2s_type = type }
	Protocol.RequestHandler.TLClientGetTurnTableRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end
--获取转盘数据
function _M.RequestGetTurnTableInfo(type,cb)
	local msg = { c2s_type = type }
	Protocol.RequestHandler.TLClientGetTurnTableInfoRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end

local function FirstInitFinish()
	local configresettime = TimeUtil.CustomTodayTimeToUtc(GameUtil.GetStringGameConfig("reset_time"))
	local refreshtime = ServerTime.getServerTime().Date:AddHours(configresettime.Hour):AddSeconds(10)
	local refreshcd = -TimeUtil.TimeLeftSec(refreshtime)
	if refreshcd < 0 then
		refreshtime = refreshtime:AddDays(1)
	end
	if timer then
		LuaTimer.Delete(timer)
		timer = nil
	end
	timer = LuaTimer.Add(0,60000,function()
		refreshcd = -TimeUtil.TimeLeftSec(refreshtime)
		if refreshcd < 0 then
			EventManager.Fire('Event.Activity.NewDay',{})
			refreshtime = refreshtime:AddDays(1)
		end
		return true
	end)
end

function _M.fin()
	EventManager.Unsubscribe("Event.Scene.FirstInitFinish", FirstInitFinish)
	if timer then
		LuaTimer.Delete(timer)
		timer = nil
	end
	FunctionUtil.UnRegister('sevenday')
end

function _M.initial()
	local temp = string.split(GameUtil.GetStringGameConfig("reset_time"),":")
	_M.configresethour = {tonumber(temp[1]),tonumber(temp[2])}
	EventManager.Subscribe("Event.Scene.FirstInitFinish", FirstInitFinish)
	
	FunctionUtil.Register('sevenday', function( ... )
		local time = DataMgr.Instance.UserData.Serverinfo.open_at:AddDays(GameUtil.GetIntGameConfig("openmaxday"))
				- ServerTime.getServerTime():ToLocalTime()
		if time.TotalSeconds > 0 then --还在活动时间内，跳转七日活动
			GlobalHooks.UI.OpenUI('SevenDay',0)
		else--活动结束，给出提示
			EventManager.Fire("Event.Hud.SetTopIcon", { comp = 'cvs_springfestival', showIcon = false})
			GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.SpringFestival.ActivityEnd))
		end
	end)
	
end


-------------------------------------------------------------------

_M.GetEntrustDataByWantid = GetEntrustDataByWantid
_M.GetEntrustData = GetEntrustData
_M.AccpetEntrustTask = AccpetEntrustTask
_M.SubmitEntrustTask = SubmitEntrustTask
_M.RefreshEntrustTime = RefreshEntrustTime
_M.BagIsCanUse = BagIsCanUse
_M.GetOpenTime = GetOpenTime
_M.OpenTime = OpenTime
_M.SetActivityUserData = SetActivityUserData
_M.GetActivityUserData = GetActivityUserData
_M.IsOpenLevel = IsOpenLevel
_M.GetItemData = GetItemData
_M.GetActivityData = GetActivityData
_M.GetSubData = GetSubData
return _M
