--! @addtogroup Guild
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
--! @brief 擂台赛结束
--! @param serverGroupId serverGroupId
--! @param changeruuid changeruuid
--! @param winForce winForce
function Api.SetArenaGameOver(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Arena.SetArenaGameOverEvent',...)
end
return Api
--! @}
