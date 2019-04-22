local _M = {}
_M.__index = _M

local TimeUtil = require 'Logic/TimeUtil'
local Helper = require 'Logic/Helper'
local ServerTime = require 'Logic/ServerTime'

_M.cachedata = {}
_M.allactivity = {}
_M.OpenServerTime = nil
_M.FirstRed = {{},{[50001] = true}}


local function GetRedKey(activitytype)
	local redkey
	if activitytype == 1 then
		redkey = "activity_newopen"
	elseif activitytype == 2 then
		redkey = "business"
	elseif activitytype == 3 then
		redkey = "isshow_relationship"
	end
	return redkey
end

local function Notify(eventname,curVal)
	if _M.cachedata then
		for i, v in pairs(_M.cachedata) do
			for i2, v2 in pairs(v) do
				if v2.check_key == eventname then
					if eventname == 'PaySuccess' then
						_M.RequireData(i,i2)
					elseif string.find(eventname,"isshow_") ~= nil then
						GlobalHooks.UI.SetRedTips(GetRedKey(i),1,i2)
					else
						for i3, v3 in pairs(v2) do
							if type(i3) == "number" then
								v3.requireList[1].curVal = curVal
								if v3.state == 0 and curVal >= v3.requireList[1].minVal then
									if v3.requireList[1].maxVal == -1 or v3.requireList[1].maxVal >= v3.requireList[1].curVal then
										if v3.requireList[2] then
											if v3.requireList[2].curVal >= v3.requireList[2].minVal then
												if v3.requireList[2].maxVal == -1 or v3.requireList[2].maxVal >= v3.requireList[2].curVal then
													v3.state = 1
													v2.count = v2.count + 1
													GlobalHooks.UI.SetRedTips(GetRedKey(i),v2.count,i2)
												end
											end
										else
											v3.state = 1
											v2.count = v2.count + 1
											GlobalHooks.UI.SetRedTips(GetRedKey(i),v2.count,i2)
										end
									end
								end
							end
						end
					end
				end
			end
		end
	end
end

local function GetAllActivity(type, isShow)

	local detail = GlobalHooks.DB.Find('AllBusinessActivity', {activity_type = type})
	local tempdata = {}
	for i, v in pairs(detail) do
		tempdata[v.activity_id] = v
	end
	_M.allactivity[type] = tempdata

	local temp = {}
	if isShow then
		for i, v in pairs(_M.cachedata[type]) do
			table.insert(temp,_M.allactivity[type][i])
		end
	else
		return
	end
	return temp
end

local function GetCommonActivity(key)
	local detail = GlobalHooks.DB.GetFullTable(key)
	if detail then
		table.sort(detail,function(a,b )
			return a.id < b.id
		end)
	end
	return detail
end

local function RequireGet(activitytype,activityid,subid, cb,self)
	local request = {c2s_activity_id = activityid,
						c2s_sub_id = subid,
						}
	Protocol.RequestHandler.ClientGetBActivityRewardRequest(request, function(rsp)
		_M.cachedata[activitytype][activityid][subid].state = 2
		_M.cachedata[activitytype][activityid].count = _M.cachedata[activitytype][activityid].count - 1
		GlobalHooks.UI.SetRedTips(GetRedKey(activitytype),_M.cachedata[activitytype][activityid].count,activityid)
		if cb then
			cb(rsp)
		end
	end)
	if self and self.isopen == false then
		self.parentui.RefreshList(self.parentui)
	end
end

local function RequireData(activitytype,activityid, cb,force)
	if force == nil then
		force = true
	end
	local request = {c2s_activity_id = activityid,c2s_sub_ids = {}}
	Protocol.RequestHandler.ClientGetBActityInfosRequest(request, function(rsp)
		if _M.cachedata and _M.cachedata[activitytype] then
			if not _M.cachedata[activitytype][activityid] then
				_M.cachedata[activitytype][activityid] = {}
			end
			local count = 0
			local check_key = _M.cachedata[activitytype][activityid].check_key
			_M.cachedata[activitytype][activityid] = Helper.copy_table(rsp.activityMap)
			for i, v in pairs(_M.cachedata[activitytype][activityid]) do
				if v.state == 1 then
					count = count + 1
				end
			end
			_M.cachedata[activitytype][activityid].check_key = check_key
			GlobalHooks.UI.SetRedTips(GetRedKey(activitytype),count,activityid)
			_M.cachedata[activitytype][activityid].count = count
		end
		if cb then
			cb(rsp)
		end
	end,PackExtData(force, force))
end

local function GetServerOpenTime()
	if not _M.OpenServerTime then
		_M.OpenServerTime = DataMgr.Instance.UserData.Serverinfo.open_at.Date:AddHours(4)
	end
	return _M.OpenServerTime
end

local function RequireAllData(type,cb)
	local request = {c2s_activity_type = type}
	Protocol.RequestHandler.ClientGetAllBActityInfosRequest(request, function(rsp)
		if _M.cachedata[type] then
			for i, v in pairs(_M.cachedata[type]) do
				GlobalHooks.UI.SetRedTips(GetRedKey(type),0,i)
			end
		end
		_M.cachedata[type] = {}
		local tempdata = rsp.s2c_activityMap
		GetAllActivity(type,false)
		for i1, v1 in pairs(tempdata) do
			if _M.allactivity[type][i1] and i1 == _M.allactivity[type][i1].activity_id then
				local count = 0
				for i3, v3 in pairs(v1.data) do
					if v3.state == 1 then
						count = count + 1
					end
				end
				v1.data.check_key = _M.allactivity[type][i1].check_key
				v1.data.count = count
				GlobalHooks.UI.SetRedTips(GetRedKey(type),count,_M.allactivity[type][i1].activity_id)
			end
			_M.cachedata[type][i1] = v1.data
		end
		if cb then
			cb(_M.cachedata[type])
		end
	end)
end

function _M.FirstRequire(type,cb)
	if type == nil then
		type = 0
	end
	local nofinishcount = 0

	for i = type == 0 and 1 or type, type == 0 and 3 or type do
		nofinishcount = nofinishcount + 1
		RequireAllData(i,function()
			--请求周卡信息
			if i == 2 and _M.cachedata[2][9] then
				nofinishcount = nofinishcount + 1
				local RechargeModel = require 'Model/RechargeModel'
				RechargeModel.RequestTimeRechargeInfo(OneGameSDK.Instance.PlatformID,function(rsp)
					local count = 0
					for i, v in pairs(rsp.s2c_list) do
						if v.s2c_available == true then
							local time = TimeUtil.TimeLeftSec(v.s2c_leftTimeUTC)
							if time <= 0 then
								_M.cachedata[2][9][v.s2c_productID] = 1
							else
								_M.cachedata[2][9][v.s2c_productID] = 0
							end
						else
							_M.cachedata[2][9][v.s2c_productID] = 0
						end
						count = count + _M.cachedata[2][9][v.s2c_productID]
					end
					_M.cachedata[2][9].count = count
					GlobalHooks.UI.SetRedTips(GetRedKey(2),count,9)
					nofinishcount = nofinishcount - 1
					if nofinishcount == 0 and cb then
						cb()
					end
				end)
			end

			if i == 2 and _M.cachedata[2][50001] then
				nofinishcount = nofinishcount + 1
				local request = {c2s_platformID = OneGameSDK.Instance.PlatformID}
				Protocol.RequestHandler.ClientGetPlantingInfoRequest(request, function(rsp)
					local count = 0
					local today = ServerTime.getServerTime():ToLocalTime()
					local overtime = System.DateTime.Parse("2019-3-14 4:00:00")
					local isover = overtime < today
					for i, v in ipairs(rsp.s2c_list) do
						if v.s2c_state == 1 and not isover then
							_M.cachedata[2][50001][v.s2c_productID] = 1
						else
							_M.cachedata[2][50001][v.s2c_productID] = 0
						end
						if count == 0 and _M.FirstRed[2][50001] and not isover then
							count = 1
						end 
						count = count + _M.cachedata[2][50001][v.s2c_productID]
					end
					_M.cachedata[2][50001].count = count
					GlobalHooks.UI.SetRedTips(GetRedKey(2),count,50001)
					nofinishcount = nofinishcount - 1
					if nofinishcount == 0 and cb then
						cb()
					end
				end)
			end
			nofinishcount = nofinishcount - 1
			if nofinishcount == 0 and cb then
				cb()
			end
		end)
	end
end

local function UpdateData(params)
	local key = params.s2c_key
	local value = tonumber(params.s2c_value)
	Notify(key,value)
end

function _M.InitNetWork(initNotify)
	if initNotify then
		Protocol.PushHandler.ClientPushKeyValueNotify(UpdateData)
	end
end

function _M.GetActivityByActivityId(id)
	local detail = unpack(GlobalHooks.DB.Find('AllBusinessActivity', {activity_id = id}))
	return detail
end


_M.GetServerOpenTime = GetServerOpenTime
_M.RequireAllData = RequireAllData
_M.RequireData = RequireData
_M.RequireGet = RequireGet
_M.GetCommonActivity = GetCommonActivity
_M.GetAllActivity = GetAllActivity
_M.Notify = Notify
_M.GetRedKey = GetRedKey
return _M