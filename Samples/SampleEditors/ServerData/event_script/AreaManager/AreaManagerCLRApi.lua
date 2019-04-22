--! @addtogroup AreaManager
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
--! @brief 获取所有场景UUID
--! @return Zones 场景信息列表
function Api.GetAllZones(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.AreaManager.GetAllZonesEvent',...)
end
--! @brief 获取Player Session
--! @param PlayerUUID PlayerUUID
--! @return SessionName Session
function Api.GetSession(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.AreaManager.GetSessionEvent',...)
end
--! @brief 获取玩家的ServerGroupID
--! @param PlayerUUID PlayerUUID
--! @return ServerGroupID ServerGroupID
function Api.GetPlayerServerGroup(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.AreaManager.GetPlayerServerGroupEvent',...)
end
--! @brief 获取指定RoomKey的ZoneUUID
--! @param RoomKey RoomKey
--! @return ZoneUUID ZoneUUID
function Api.GetZoneUUIDByRoomKey(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.AreaManager.GetZoneUUIDByRoomKeyEvent',...)
end
--! @brief 获取所有玩家UUID
--! @return Players 玩家的UUID列表
function Api.GetAllPlayers(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.AreaManager.GetAllPlayersEvent',...)
end
return Api
--! @}
