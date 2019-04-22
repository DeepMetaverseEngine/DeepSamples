--! @addtogroup CommonServer
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
local CFiles = {Task ={},Listen={}}
Api.CFiles = CFiles
local MasterRace = {Task ={},Listen={}}
Api.MasterRace = MasterRace
--! @brief 按权重随机
--! @param Weights 权值数组
--! 	- 参数为一个Array []
--! @param Count 随机次数
--! @return Result 随机结果
function Api.RandomWeight(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Common.RandomWeightEvent',...)
end
--! @brief 获取文件列表
--! @param CurrentPath 路径
--! @return Infos 结果
function CFiles.ListAllFiles(...)
	return EventApi.DoSharpApi('CFiles.Sync','ThreeLives.Server.Events.Common.ListAllFilesEvent',...)
end
--! @brief 活动是否开放
--! @param functionId 活动Id
--! @return Result 是否开放
function Api.IsFuncOpenTime(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Common.IsFuncOpenTimeEvent',...)
end
--! @brief 执行全局lua指令
--! @param MgrType ManagerType
--! @param Chunk Command
function Api.StartChunk(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Common.StartChunkEvent',...)
end
--! @brief GetMonsterMirrorEvent
--! @param TemplateId TemplateId
--! @param SceneId SceneId
--! @param ScenePlayerLv ScenePlayerLv
--! @return RoleInfo MirrorInfo
function Api.GetMonsterMirror(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Common.GetMonsterMirrorEvent',...)
end
--! @brief MasterRaceLostEvent
--! @param CurMasterid curMasterid
--! @param CurMasterindex curMasterindex
--! @param Masterid masterid
--! @param Masterindex masterindex
--! @param RoleId roleid
function MasterRace.MasterRaceLost(...)
	return EventApi.DoSharpApi('MasterRace.Sync','ThreeLives.Server.Events.Common.MasterRaceLostEvent',...)
end
--! @brief GetPlayerMirrorSnapEvent
--! @param RoleId RoldId
--! @return RoleInfo MirrorInfo
function Api.GetPlayerMirrorSnap(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Common.GetPlayerMirrorSnapEvent',...)
end
--! @brief 是否在指定时期内
--! @param startday startday
--! @param endday endday
--! @return IsInDay IsInDay
function Api.IsInAppointedDay(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Common.IsInAppointedDayEvent',...)
end
--! @brief 读取玩家Session
--! @param PlayerUUID PlayerUUID
--! @return SessionName SessionName
function Task.GetOrLoadSession(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Common.GetOrLoadSessionEvent',...)
end
--! @brief 读取玩家Session
--! @param Players Players
--! 	- 参数为一个Array []
--! @return Sessions Sessions
function Task.GetOrLoadManySessions(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Common.GetOrLoadManySessionsEvent',...)
end
--! @brief 获取玩家的名称
--! @param PlayerUUID PlayerUUID
--! @return PlayerName SessionName
function Task.GetOrLoadPlayerName(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Common.GetOrLoadPlayerNameEvent',...)
end
--! @brief 获取玩家ServerGroupID
--! @param PlayerUUID PlayerUUID
--! @return ServerGroupID ServerGroupID
function Task.GetOrLoadServerGroup(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Common.GetOrLoadServerGroupEvent',...)
end
--! @brief 获取玩家ServerGroupID
--! @param PlayerUUID PlayerUUID
--! @return GuildName GuildName
function Task.GetOrLoadGuildName(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Common.GetOrLoadGuildNameEvent',...)
end
--! @brief 是否在活动开放
--! @param functionId 活动Id
function Task.FuncOpening(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Common.FuncOpeningEvent',...)
end
return Api
--! @}
