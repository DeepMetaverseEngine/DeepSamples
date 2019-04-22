--! @addtogroup Guild
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
--! @brief 获取公会建筑等级
--! @param GuildUUID GuildUUID
--! @param BuildType BuildType
--! @return Level BuildLevel
function Api.GetBuildLevel(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetBuildLevelEvent',...)
end
--! @brief 获取工会被破坏次数
--! @param GuildUUID GuildUUID
--! @param AttackType 破坏类型（1：放火 2：投毒）
--! @return Count DestroyCount
function Api.GetDestroyCount(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetDestroyCountEvent',...)
end
--! @brief 获取神兽等级和神兽ID
--! @param GuildUUID GuildUUID
--! @return TemplateID TemplateID
function Api.GetWallMonsterTemplateID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetWallMonsterTemplateIDEvent',...)
end
--! @brief 获取敌对押镖公会
--! @param GuildUUID 公会ID
--! @return EnemyGuildUUID 公会ID
--! @return EnemyGuildName 公会名称
function Api.GetCarriageEnemy(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetCarriageEnemyEvent',...)
end
--! @brief 获取敌对据点战公会
--! @param GuildUUID 公会ID
--! @return EnemyGuildUUID 公会ID
--! @return EnemyGuildName 公会名称
function Api.GetWallEnemy(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetWallEnemyEvent',...)
end
--! @brief 获取RoomKey
--! @param GuildUUID GuildUUID
--! @return RoomKey RoomKey
--! @return Force 阵营
function Api.GetCarriageZoneInfo(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetCarriageZoneInfoEvent',...)
end
--! @brief 获取RoomKey
--! @param GuildUUID GuildUUID
--! @param MapTemplateID MapTemplateID
--! @return RoomKey RoomKey
--! @return Force 阵营
--! @return FortID 据点ID
function Api.GetWallZoneInfo(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.GetWallZoneInfoEvent',...)
end
--! @brief 仙盟押镖奖励发放
--! @param GuildUUIDs 公会UUID
--! 	- 参数为一个Array []
--! @param WinUUID 是否是胜利方
function Api.CarriageReward(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.CarriageRewardEvent',...)
end
--! @brief 仙盟押镖奖励发放
--! @param WinUUID 胜利方UUID
--! @param SameScore 是否平局
--! @return ResultUUID 最终获胜方UUID
function Api.FortReward(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.FortRewardEvent',...)
end
--! @brief 仙盟押镖奖励发放
--! @param GuildUUID GuildUUID
--! @param IsEnter 是否是进入
function Api.NotifyGuildZoneInfoChange(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Guild.NotifyGuildZoneInfoChangeEvent',...)
end
return Api
--! @}
