--! @addtogroup Player
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
local DemonTower = {Task ={},Listen={}}
Api.DemonTower = DemonTower
local CPDemonTower = {Task ={},Listen={}}
Api.CPDemonTower = CPDemonTower
local MasterRace = {Task ={},Listen={}}
Api.MasterRace = MasterRace
local GodIsland = {Task ={},Listen={}}
Api.GodIsland = GodIsland
--! @brief 获取玩家的UUID
--! @return UUID 
function Api.GetPlayerUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetPlayerUUIDEvent',...)
end
--! @brief 获取当前玩家的名称
--! @return PlayerName 名称
function Api.GetPlayerName(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetPlayerNameEvent',...)
end
--! @brief 获取当前玩家的公会名称
--! @return GuildName 名称
function Api.GetGuildName(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildNameEvent',...)
end
--! @brief 获取当前公会资金
--! @return Fund Fund
function Api.GetGuildFund(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildFundEvent',...)
end
--! @brief 获取当前公会人数
--! @return Fund Fund
function Api.GetGuildMembersCount(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildMembersCountEvent',...)
end
--! @brief 获取当前公会人数
--! @return Members Fund
function Api.GetGuildMembers(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildMembersEvent',...)
end
--! @brief 获取当前公会职位
--! @return Position Fund
function Api.GetGuildPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildPositionEvent',...)
end
--! @brief 离开当前场景
function Api.LeaveCurrentZone(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.LeaveCurrentZoneEvent',...)
end
--! @brief 获取当前公会UUID
--! @return GuildUUID 公会UUID
function Api.GetGuildUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildUUIDEvent',...)
end
--! @brief 获取玩家的等级
--! @return Level 等级
function Api.GetPlayerLevel(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetPlayerLevelEvent',...)
end
--! @brief 设置玩家任务状态为可接取
--! @param QuestID 任务ID
function Api.GiveUpQuest(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GiveUpQuestEvent',...)
end
--! @brief 重置玩家任务状态
--! @param QuestID 任务ID
function Api.ResetQuest(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.ResetQuestEvent',...)
end
--! @brief 设置玩家任务状态为完成可提交
--! @param QuestID 任务ID
function Api.CompleteQuest(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.CompleteQuestEvent',...)
end
--! @brief 设置玩家任务状态为已接取
--! @param QuestID 任务ID
function Api.AcceptQuest(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.AcceptQuestEvent',...)
end
--! @brief 任务状态
--! @param QuestID 任务ID
--! @return QuestState 任务状态
function Api.GetQuestState(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetQuestStateEvent',...)
end
--! @brief 获取玩家的地图模板ID
function Api.GetMapTemplateID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetMapTemplateIDEvent',...)
end
--! @brief 获取玩家的AccountID
--! @param PlayerUUID PlayerUUID
--! @return Session Session
function Api.GetSession(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetSessionEvent',...)
end
--! @brief 获取玩家的任务列表
--! @return Quests 获取玩家的任务列表
function Api.GetQuestList(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetQuestListEvent',...)
end
--! @brief 获取玩家的已接任务列表
--! @return Quests 获取玩家的任务列表
function Api.GetAcceptQuestList(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetAcceptQuestListEvent',...)
end
--! @brief 获取玩家所在的场景实例ID
--! @return ZoneUUID 场景实例ID
function Api.GetZoneUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetZoneUUIDEvent',...)
end
--! @brief 添加角色经验值
--! @param Exp 经验值
function Api.AddExp(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.AddExpEvent',...)
end
--! @brief 角色公共掉落
--! @param Reward TLRewardGroup
--! 	- reward_id 
--! 	- flag_reward 
--! 	- type 
--! 	- item 
--! 	- title_id 
--! 	- basetype 
--! 	- mail_id 
--! @param DropEffect 是否是掉落地上的表现
--! 	- X 
--! 	- Y 
--! 	- Show 
--! 	- MonsterType 
--! @param Reason 原因
--! @param CheckSettled 结算检查
function Api.CommonReward(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.CommonRewardEvent',...)
end
--! @brief 鎖定玩家传送
--! @param LockSec 地图ID
function Api.PlayerLockTransport(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.PlayerLockTransportEvent',...)
end
--! @brief SetStringFlag
--! @param Key Key
--! @param Value Value
--! @param DayClean 是否为日清
function Api.SetStringFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.SetStringFlagEvent',...)
end
--! @brief GetStringFlag
--! @param Key Key
--! @return Value Value
function Api.GetStringFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetStringFlagEvent',...)
end
--! @brief SetIntFlag
--! @param Key Key
--! @param Value Value
--! @param DayClean 是否为日清
function Api.SetIntFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.SetIntFlagEvent',...)
end
--! @brief AddIntFlag
--! @param Key Key
--! @param Value Value
--! @param DayClean 是否为日清
--! @param LimitValue 是否为日清
--! @return Result 添加后结果
function Api.AddIntFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.AddIntFlagEvent',...)
end
--! @brief GetIntFlag
--! @param Key Key
--! @param DayClean 是否为日清
--! @return Value Value
function Api.GetIntFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetIntFlagEvent',...)
end
--! @brief RecordPassTime
--! @param Layer Layer
--! @param Sec Time
function DemonTower.RecordPassTime(...)
	return EventApi.DoSharpApi('DemonTower.Sync','ThreeLives.Server.Events.Logic.RecordPassTimeEvent',...)
end
--! @brief CPRecordPassTime
--! @param Layer Layer
--! @param Sec Time
--! @param Players Players
--! 	- 参数为一个Array []
function CPDemonTower.CPRecordPassTime(...)
	return EventApi.DoSharpApi('CPDemonTower.Sync','ThreeLives.Server.Events.Logic.CPRecordPassTimeEvent',...)
end
--! @brief 离开副本
--! @param mapid mapid
function Api.LeaveDungeon(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.LeaveDungeonEvent',...)
end
--! @brief 玩家是否准备完毕
--! @return IsPlayerReady 是否准备完毕
function Api.IsPlayerReady(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.IsPlayerReadyEvent',...)
end
--! @brief CPDemonTowerPass
--! @param MapTemplateid mapid
--! @param passLayer passLayer
--! @param isWin isWin
function CPDemonTower.CPDemonTowerRewardItem(...)
	return EventApi.DoSharpApi('CPDemonTower.Sync','ThreeLives.Server.Events.Logic.CPDemonTowerRewardItemEvent',...)
end
--! @brief 战斗结算单独推送
--! @param MapTemplateid mapid
--! @param passtime passtime
--! @param isLeave isLeave
function DemonTower.SendRewardItem(...)
	return EventApi.DoSharpApi('DemonTower.Sync','ThreeLives.Server.Events.Logic.SendRewardItemEvent',...)
end
--! @brief 仙灵岛通关时间推送
--! @param checkpointId pointId
--! @param Sec Time
function GodIsland.SendIslandPassTime(...)
	return EventApi.DoSharpApi('GodIsland.Sync','ThreeLives.Server.Events.Logic.SendIslandPassTimeEvent',...)
end
--! @brief 获取玩家仙盟神兽.
--! @return ret 
--! - MonsterLv 仙盟神兽等级
--! - MonsterRank 仙盟神兽阶级
function Api.GetGuildMonsterData(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetGuildMonsterDataEvent',...)
end
--! @brief 检测玩家Require
--! @param RequireData 道具模板数据
--! 	- key 
--! 	- minval 
--! 	- maxval 
--! 	- text 
--! @return ErrorReason 结果
function Api.CheckRequire(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.CheckRequireEvent',...)
end
--! @brief 设置单位复活类型.
--! @param RebirthType 复活框类型
function Api.SetPlayerRebirthType(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.SetPlayerRebirthTypeEvent',...)
end
--! @brief 获取队员,包括自己
--! @return PlayersUUID 队员UUID
function Api.GetTeamMembers(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetTeamMembersEvent',...)
end
--! @brief 是否是队长
--! @return IsTeamLeader 是否是队长
function Api.IsTeamLeader(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.IsTeamLeaderEvent',...)
end
--! @brief 是否有队伍
--! @return HasTeam 是否有队伍
function Api.HasTeam(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.HasTeamEvent',...)
end
--! @brief 获取玩家的ServerGroupID
--! @param PlayerUUID PlayerUUID
--! @return ServerGroupID ServerGroupID
function Api.GetPlayerServerGroup(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GetPlayerServerGroupEvent',...)
end
--! @brief 玩家是否能进入场景
--! @param MapTemplateID MapTemplateID
--! @return CanEnter 是否可进入
--! @return ErrorReason 错误信息
function Api.CanEnterMap(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.CanEnterMapEvent',...)
end
--! @brief 执行GM指令
--! @param Command GM指令内容
function Api.GmCommand(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.GmCommandEvent',...)
end
--! @brief 记录BI日志
--! @param argMap
--! - ext ext
--! 	- 参数为一个Map 
--! - itemCount itemCount
--! - itemID itemID
--! - itemName itemName
--! - sourceType sourceType
function Api.BILog(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.BILogEvent',...)
end
--! @brief 战场结束结算
--! @param PvpType 战场类型(pvp4,pvp10)
--! @param Result 结果 0:平局 1:胜利 -1:失败
--! @param Exploit 功勋奖励
--! @return TodayTotalExploit 实际获得功勋
function Api.SetPvpResult(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.SetPvpResultEvent',...)
end
--! @brief GetMasterIndex
--! @return Masterid 身份
--! @return Masterindex 索引
function MasterRace.GetMasterIndex(...)
	return EventApi.DoSharpApi('MasterRace.Sync','ThreeLives.Server.Events.Logic.SetPvpResultEvent+GetMasterIndexEvent',...)
end
--! @brief GetCurMasterIndex
--! @return Masterid 身份
--! @return Masterindex 索引
function MasterRace.GetCurMasterIndex(...)
	return EventApi.DoSharpApi('MasterRace.Sync','ThreeLives.Server.Events.Logic.SetPvpResultEvent+GetCurMasterIndexEvent',...)
end
--! @brief 获取玩家职业
--! @return Pro 玩家职业
function Api.GetPlayerPro(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Logic.SetPvpResultEvent+GetPlayerProEvent',...)
end
--! @brief 添加角色道具
--! @param TemplateID 道具模板数据
--! @param Count 道具数量
--! @param Reason 原因
--! 	- sourceType 
--! 	- itemName 
--! 	- itemID 
--! 	- itemCount 
--! 	- ext 
--! @param Drop 是否是掉落地上的表现
--! 	- X 
--! 	- Y 
--! 	- Show 
--! 	- MonsterType 
function Task.AddItem(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Logic.AddItemEvent',...)
end
--! @brief 添加角色道具
--! @param Items 道具模板数据
--! 	- 参数为一个Array []
--! 		- TemplateID 
--! 		- Count 
--! 		- Compare 
--! @param Drop 是否是掉落地上的表现
--! 	- X 
--! 	- Y 
--! 	- Show 
--! 	- MonsterType 
--! @param MailID 邮件ID
--! @param Reason 原因
--! 	- sourceType 
--! 	- itemName 
--! 	- itemID 
--! 	- itemCount 
--! 	- ext 
function Task.AddMoreItem(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Logic.AddMoreItemEvent',...)
end
--! @brief 添加角色道具
--! @param ItemIDs 道具模板ID
--! 	- 参数为一个Array []
--! @param ItemNums 道具数量
--! 	- 参数为一个Array []
--! @param ItemGroupIDs 道具模板ID
--! 	- 参数为一个Array []
--! @param ItemGroupNums 道具数量
--! 	- 参数为一个Array []
--! @param Reason 原因
--! 	- sourceType 
--! 	- itemName 
--! 	- itemID 
--! 	- itemCount 
--! 	- ext 
function Task.CostItem(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Logic.CostItemEvent',...)
end
--! @brief 是否能进入婚礼场景
--! @param argMap
--! - Flag Flag名称，和坐标二选一
--! - MapName 地图名称
--! - MapTemplateID 地图ID
--! - X X坐标
--! - Y Y坐标
--! @return Entered 是否成功
function Task.EnterMarryZone(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Logic.EnterMarryZoneEvent',...)
end
--! @brief 是否能启动婚礼动画
--! @return Entered 是否成功
function Task.StartWeddingAnime(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Logic.StartWeddingAnimeEvent',...)
end
--! @brief 跨场景传送玩家
--! @param argMap
--! - Flag Flag名称，和坐标二选一
--! - Force Force
--! - MapName 地图名称
--! - MapTemplateID 地图ID
--! - Players 其他成员
--! 	- 参数为一个Array []
--! - RoomKey RoomKey
--! - X X坐标
--! - Y Y坐标
--! - ZoneUUID ZoneUUID
--! @return EnteredZoneUUID ZoneUUID
function Task.TransportPlayer(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Logic.TransportPlayerEvent',...)
end
--! @brief MasterRacePassEvent
--! @param Masterid masterid
--! @param Masterindex masterindex
--! @param RoleId roleid
--! @param isWin iswin
function MasterRace.Task.MasterRacePass(...)
	return EventApi.DoSharpApi('MasterRace.Async','ThreeLives.Server.Events.Logic.MasterRacePassEvent',...)
end
--! @brief 监听玩家进入某场景(使用MapTemplateID)
--! @param MapTemplateID 指定地图模板ID的场景
function Listen.EnterMap(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Logic.EnterMapEvent',...)
end
--! @brief 监听玩家任务状态
function Listen.QuestStateChange(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Logic.QuestStateChangeEvent',...)
end
--! @brief 监听玩家重连
function Listen.SessionReconnect(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Logic.SessionReconnectEvent',...)
end
--! @brief 监听玩家正在保存数据
function Listen.BeforeSaveData(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Logic.BeforeSaveDataEvent',...)
end
--! @brief 监听玩家使用道具
function Listen.UseItem(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Logic.UseItemEvent',...)
end
--! @brief 监听玩家准备完毕
function Listen.PlayerReady(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Logic.PlayerReadyEvent',...)
end
return Api
--! @}
