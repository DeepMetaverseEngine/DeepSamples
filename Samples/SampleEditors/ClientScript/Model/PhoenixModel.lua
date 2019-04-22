local _M = {}
_M.__index = _M


--获取所有的副本信息
local function GetAllPhoenixData()
	local allPhoenixData=GlobalHooks.DB.GetFullTable('PhoenixInfo')
    	table.sort(allPhoenixData,function(a,b) return a.order<b.order end)
	return allPhoenixData
end


--获取刷新时间
local function GetRefreshTimeById(refreshTime)
	
	local allBossTime=GlobalHooks.DB.GetFullTable('PhoenixTime')
	local timeTable={}
	for i=1,#allBossTime do
		if allBossTime[i].time_group == tonumber(refreshTime) then
			table.insert(timeTable,{starttime=allBossTime[i].start_time,endtime=allBossTime[i].end_time})
		end
	end
	return timeTable
end



_M.GetRefreshTimeById=GetRefreshTimeById
_M.GetAllPhoenixData=GetAllPhoenixData
return _M