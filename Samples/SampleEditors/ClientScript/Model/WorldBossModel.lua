local _M = {}
_M.__index = _M


--获取所有boss信息
local function GetAllBossData()
	local allBossData=GlobalHooks.DB.GetFullTable('WorldBossInfo')
    table.sort(allBossData,function(a,b) return a.id<b.id end)
	return allBossData
end


--获取单条信息
local function GetDesignatedBossData(index)
	local OneBossData=unpack(GlobalHooks.DB.Find('WorldBossInfo',{id=index}))
    return OneBossData
end


--通过时间编组获得单条boss的时间
local function GetBossTime(index)
	local indexBoss=unpack(GlobalHooks.DB.Find('WorldBossTime',{time_group=tonumber(index)}))
	return indexBoss
end


--通过时间编组获得该编组下所有的时间
local function GetBossAllTime(index)
	local allBossTime=GlobalHooks.DB.GetFullTable('WorldBossTime')
	local timeTable={}
	for i=1,#allBossTime do
		if allBossTime[i].time_group == tonumber(index) then
			table.insert(timeTable,{starttime=allBossTime[i].start_time,endtime=allBossTime[i].end_time})
		end
	end
	return timeTable
end


_M.GetBossAllTime=GetBossAllTime
_M.GetBossTime=GetBossTime
_M.GetAllBossData=GetAllBossData
_M.GetDesignatedBossData=GetDesignatedBossData

return _M