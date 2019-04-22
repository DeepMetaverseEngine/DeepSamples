--! @addtogroup MasterRace
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
--! @brief 获得服务器师门赛uuid
--! @param Servicegroupid servicegroupid
--! @param Masterpro masterpro
--! @param Masterid masterid
--! @param Masterindex masterindex
--! @return RoleId uuid
function Api.GetMasterRaceRoleId(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.MasterRace.GetMasterRaceRoleIdEvent',...)
end
return Api
--! @}
