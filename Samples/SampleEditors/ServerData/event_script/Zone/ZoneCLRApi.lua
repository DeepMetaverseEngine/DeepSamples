--! @addtogroup Zone
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
local CPTower = {Task ={},Listen={}}
Api.CPTower = CPTower
local MasterRace = {Task ={},Listen={}}
Api.MasterRace = MasterRace
--! @brief 获取玩家所有任务 参数-玩家UUID
--! @param PlayerUUID 玩家UUID
--! @return QuestMap 玩家任务Map 任务id-状态
function Api.GetPlayerQuests(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerQuestsEvent',...)
end
--! @brief 新建私人位面
--! @param PlayerUUID 玩家UUID
function Api.CreatePlayerAoi(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.CreatePlayerAoiEvent',...)
end
--! @brief 玩家退出位面
--! @param PlayerUUID 玩家UUID
function Api.PlayerLeaveAoi(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerLeaveAoiEvent',...)
end
--! @brief 玩家私人位面添加单位
--! @param UnitTemplates 需要添加的单位列表
--! @param PlayerUUID 玩家UUID
function Api.PlayerAoiAddUnit(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerAoiAddUnitEvent',...)
end
--! @brief 设置玩家任务状态
--! @param PlayerUUID 
--! @param QuestID 
--! @param Key 
--! @param Value 
function Api.SetPlayerQuestState(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerQuestStateEvent',...)
end
--! @brief 设置道具的所有者，设置成功后其他玩家不可见
--! @param ItemID 道具的ObjectID
--! @param PlayerUUID 玩家的UUID
function Api.SetItemOwner(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetItemOwnerEvent',...)
end
--! @brief 创建一个跟随单位
--! @param PlayerUUID 玩家的UUID
--! @param TemplateID 单位模板ID
--! @param PlayerOnly 仅玩家可见
--! @return ObjectID ObjectID
function Api.AddFollowUnit(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.AddFollowUnitEvent',...)
end
--! @brief 设置玩家的阵营
--! @param PlayerUUID 玩家UUID
--! @param Force 需要设置的阵营
function Api.SetPlayerForce(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerForceEvent',...)
end
--! @brief 判断2个玩家是否是组队成员
--! @param PlayerUUID1 玩家UUID
--! @param PlayerUUID2 玩家UUID
--! @return IsTeamMember 是否是组队成员
function Api.IsSameTeam(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsSameTeamEvent',...)
end
--! @brief 获取队员,包括自己
--! @param PlayerUUID 玩家UUID
--! @return MembersUUID 队员UUID
function Api.GetTeamMembers(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetTeamMembersEvent',...)
end
--! @brief 是否是队长
--! @param PlayerUUID 玩家UUID
--! @return IsTeamLeader 是否是队长
function Api.IsTeamLeader(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsTeamLeaderEvent',...)
end
--! @brief 是否有队伍
--! @param PlayerUUID 玩家UUID
--! @return HasTeam 是否有队伍
function Api.HasTeam(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.HasTeamEvent',...)
end
--! @brief 添加场景环境变量
--! @param Key key
--! @param Value 值
--! @param SyncToClient 
function Api.AddEnvironment(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.AddEnvironmentEvent',...)
end
--! @brief 设置场景环境变量
--! @param Key key
--! @param Value 值
function Api.SetEnvironment(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetEnvironmentEvent',...)
end
--! @brief 获取场景的公会所有者
--! @return GuildUUID UUID
function Api.GetZoneGuildUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetZoneGuildUUIDEvent',...)
end
--! @brief 获取地图上的所有路点
--! @return Points Points
function Api.GetZonePointsName(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetZonePointsNameEvents',...)
end
--! @brief 场景内阵营玩家执行事件
--! @param Force Force
--! @param EventKey 事件key
--! @param EventArgs 事件key
--! @param EventParams 事件key
function Api.PlayersDoStart(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayersDoStartEventByKeyEvent',...)
end
--! @brief 获取玩家的公会ID
--! @param PlayerUUID 玩家UUID
--! @return GuildUUID 仙盟UUID
function Api.GetPlayerGuildUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerGuildUUIDEvent',...)
end
--! @brief 获取玩家的公会名称
--! @param PlayerUUID 玩家UUID
--! @return GuildName 公会名称
function Api.GetPlayerGuildName(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerGuildNameEvent',...)
end
--! @brief 场景添加Effect
--! @param Effect 单位特效参数
--! 	- Name 触发的特效名字
--! 	- BindBody 触发特效是否绑定在单位(坐标)
--! 	- BindBodyDirection 触发特效是否绑定在单位(方向)
--! 	- BindPartName 绑定挂载点位置
--! 	- ScaleToBodySize 如果不为0，则特效以该尺寸缩放
--! 	- SoundName 音效
--! 	- EarthQuakeMS 震屏时长(毫秒)
--! 	- EarthQuakeXYZ 震屏幅度
--! 	- Tag 自定义字段
--! 	- CameraAnimation 摄像机动画名字
--! 	- EffectTimeMS 特效持续时间，0表示只播放一次
--! 	- IsLoop 是否循环
--! 	- BlurStrength 模糊强度
--! 	- BlurBeginTime 开始模糊需要时间(毫秒)
--! 	- BlurWaitTime 模糊后持续时间(毫秒)
--! 	- BlurEndTime 模糊消失需要时间(毫秒)
--! 	- CameraDistance 镜头拉近距离，正值为拉近，负值为拉远
--! 	- CameraBeginTime 镜头拉到位置所需时间
--! 	- CameraWaitTime 镜头到位后持续时间(毫秒)
--! 	- CameraEndTime 镜头还原需要时间(毫秒)
--! 	- WarnType 预警显示类型
--! 	- WarnScaleX x轴缩放比例
--! 	- WarnScaleZ z轴缩放比例
--! 	- WarnSpeed 播放速度
--! 	- WarnDegree 角度(仅对扇形预警有效)
--! @param X X
--! @param Y Y
--! @param Direction Y
function Api.PlaySceneEffect(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlaySceneEffectEvent',...)
end
--! @brief 场景添加Effect
--! @param ObjectID ObjectID
--! @param Effect 单位特效参数
--! 	- Name 触发的特效名字
--! 	- BindBody 触发特效是否绑定在单位(坐标)
--! 	- BindBodyDirection 触发特效是否绑定在单位(方向)
--! 	- BindPartName 绑定挂载点位置
--! 	- ScaleToBodySize 如果不为0，则特效以该尺寸缩放
--! 	- SoundName 音效
--! 	- EarthQuakeMS 震屏时长(毫秒)
--! 	- EarthQuakeXYZ 震屏幅度
--! 	- Tag 自定义字段
--! 	- CameraAnimation 摄像机动画名字
--! 	- EffectTimeMS 特效持续时间，0表示只播放一次
--! 	- IsLoop 是否循环
--! 	- BlurStrength 模糊强度
--! 	- BlurBeginTime 开始模糊需要时间(毫秒)
--! 	- BlurWaitTime 模糊后持续时间(毫秒)
--! 	- BlurEndTime 模糊消失需要时间(毫秒)
--! 	- CameraDistance 镜头拉近距离，正值为拉近，负值为拉远
--! 	- CameraBeginTime 镜头拉到位置所需时间
--! 	- CameraWaitTime 镜头到位后持续时间(毫秒)
--! 	- CameraEndTime 镜头还原需要时间(毫秒)
--! 	- WarnType 预警显示类型
--! 	- WarnScaleX x轴缩放比例
--! 	- WarnScaleZ z轴缩放比例
--! 	- WarnSpeed 播放速度
--! 	- WarnDegree 角度(仅对扇形预警有效)
function Api.PlayUnitEffect(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayUnitEffectEvent',...)
end
--! @brief 通知公会服玩家破坏次数
--! @param PlayerUUID PlayerUUID
--! @param AttackType AttackType
function Api.NotifyGuildPlayerAttack(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.NotifyGuildPlayerAttackEvent',...)
end
--! @brief 获取击杀信息
--! @param PlayerUUID PlayerUUID
--! @return atkList 击杀信息列表
function Api.GetPlayerKillInfo(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerKillInfoEvent',...)
end
--! @brief 获取玩家的等级
--! @param PlayerUUID 玩家UUID
--! @return Level 玩家等级
function Api.GetPlayerLevel(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerLevelEvent',...)
end
--! @brief 获取玩家对怪物的伤害
--! @param ObjectID 单位ObjectID
--! @return AtkDataMap 伤害map信息
function Api.GetMonsterDamageInfo(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetMonsterDamageInfoEvent',...)
end
--! @brief 开启攻击记录
--! @param PlayerUUID PlayerUUID
function Api.RecordAttackData(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.RecordAttackDataEvent',...)
end
--! @brief 单位增加BUFF
--! @param ObjectID 单位实例ID
--! @param BuffID BUFF ID
function Api.UnitAddBuff(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitAddBuffEvent',...)
end
--! @brief 单位增加BUFF
--! @param PlayerUUID 玩家UUID
--! @param BuffID BUFF ID
function Api.PlayerAddBuff(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerAddBuffEvent',...)
end
--! @brief 获取玩家的ServerGroupID
--! @param PlayerUUID PlayerUUID
--! @return ServerGroupID ServerGroupID
function Api.GetPlayerServerGroup(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerServerGroupEvent',...)
end
--! @brief 玩家满血原地复活
--! @param PlayerUUID 玩家UUID
function Api.PlayerRecoveryOrRebirth(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerRecoveryOrRebirthEvent',...)
end
--! @brief 玩家满血传送到出生点
--! @param PlayerUUID 玩家UUID
function Api.PlayerRebirthOrgin(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerRebirthOrginEvent',...)
end
--! @brief 强制设置玩家为Pvp状态
--! @param PlayerUUID 玩家UUID
function Api.SetPlayerToPvpState(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerToPvpStateEvent',...)
end
--! @brief 强制设置玩家为Pvp状态
--! @param IsPvpState 是否在Pvp状态
--! @param PlayerUUID 玩家UUID
function Api.IsPlayerInPvpState(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsPlayerInPvpStateEvent',...)
end
--! @brief 获取场景内所有玩家
--! @param Force Force
--! @param Pro 职业
--! @param ReverseForce ReverseForce
--! @return Players 玩家的UUID列表
function Api.FindPlayers(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.FindPlayersEvent',...)
end
--! @brief 获得玩家所在地图lv
--! @param PlayerUUID 玩家UUID
--! @return mapLv MapLv
function Api.GetMapLv(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetMapLvEvent',...)
end
--! @brief 设置场景过期时间戳
--! @param hour 小时
--! @param minute 分钟
--! @param keepTime 保留多少分钟
function Api.SetSceneExpireTime(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetSceneExpireTimeEvent',...)
end
--! @brief 设置场景过期时间戳
--! @param ExpireTime 小时
function Api.SetSceneExpireDateTime(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetSceneExpireDateTimeEvent',...)
end
--! @brief 获取当前场景所在线
--! @return LineIndex 当前场景所在线
function Api.GetLineIndex(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetLineIndexEvent',...)
end
--! @brief 获取地图id对应的难度
--! @param mapid mapid
--! @return mode mode
function CPTower.GetCPTowerModeByMapId(...)
	return EventApi.DoSharpApi('CPTower.Sync','ThreeLives.Server.Events.Zone.GetCPTowerModeByMapIdEvent',...)
end
--! @brief 重置指定玩家的状态(回血，清怒气，清技能CD)
--! @param PlayerUUID PlayerUUID
function Api.PlayerResetStatus(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerResetStatusEvent',...)
end
--! @brief 获取当前场景的MapID
function Api.GetMapTemplateID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetMapTemplateIDEvent',...)
end
--! @brief 获取当前场景的UUID
function Api.GetZoneUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetZoneUUIDEvent',...)
end
--! @brief 设置单位最大血量
--! @param ObjectID 单位实例ID
--! @param MaxHP 最大生命值
--! @param AutoUpdate 是否自动按比例更新
function Api.SetUnitMaxHp(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetUnitMaxHpEvent',...)
end
--! @brief 设置单位当前血量
--! @param ObjectID 单位实例ID
--! @param HP 最大生命值
function Api.SetUnitHp(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetUnitHpEvent',...)
end
--! @brief 获取单位当前血量
--! @param ObjectID 单位实例ID
--! @return CurrentHP 最大生命值
function Api.GetUnitHp(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitHpEvent',...)
end
--! @brief 设置单位移动速度
--! @param ObjectID 单位实例ID
--! @param Speed 速度
function Api.SetUnitMoveSpeed(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetUnitMoveSpeedEvent',...)
end
--! @brief 设置单位最大血量
--! @param ObjectID 单位实例ID
--! @return MaxHP 最大生命值
function Api.GetUnitMaxHp(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitMaxHpEvent',...)
end
--! @brief 獲取单位移动速度
--! @param ObjectID 单位实例ID
--! @return Speed 速度
function Api.GetUnitMoveSpeed(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitMoveSpeedEvent',...)
end
--! @brief Get单位移动速度
--! @param PlayerUUID 玩家UUID
--! @return Speed 速度
function Api.GetPlayerMoveSpeed(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerMoveSpeedEvent',...)
end
--! @brief 设置单位移动速度
--! @param PlayerUUID 玩家UUID
--! @param Speed 速度
function Api.SetPlayerMoveSpeed(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerMoveSpeedEvent',...)
end
--! @brief 添加一个单位 返回值:单位的ObjectID
--! @param argMap
--! - Direction 单位方向
--! - EditorName EditorName
--! - Flag Flag
--! - Force 单位阵营
--! - MaxHP 最大生命值
--! - PlayerUUID PlayerUUID
--! - Speed 速度
--! - TemplateID 单位模板ID
--! - X X
--! - Y Y
--! @return ObjectID 单位实例ID
function Api.AddUnit(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.AddUnitEvent',...)
end
--! @brief 移除一个游戏对象
--! @param ObjectID ObjectID
function Api.RemoveObject(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.RemoveObjectEvent',...)
end
--! @brief 通过场景放置单位点名称查找生成单位
--! @param UnitFlag 单位Flag名称
--! @param Force 单位阵营
--! @return UnitObjectID 单位ID
function Api.GetUnitByUnitFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitByUnitFlagEvent',...)
end
--! @brief 设置单位沉默
--! @param ObjectID 单位实例ID
--! @param TimeMS 沉默时间MS
function Api.SetUnitSilentMS(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetUnitSilentMSEvent',...)
end
--! @brief 设置单位无敌
--! @param ObjectID 单位实例ID
--! @param TimeMS 无敌时间MS
function Api.SetUnitInvincibleMS(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetUnitInvincibleMSEvent',...)
end
--! @brief 判断单位是否无敌
--! @param ObjectID 单位实例ID
--! @return Invincible 是否无敌
function Api.IsUnitInvincible(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsUnitInvincibleEvent',...)
end
--! @brief 查找寻路坐标点
--! @param X 
--! @param Y 
--! @param TargetX 
--! @param TargetY 
--! @return Ways 坐标列表
function Api.FindPath(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.FindPathEvent',...)
end
--! @brief 单位前往某位置
--! @param ObjectID ObjectID
--! @param X 
--! @param Y 
--! @return MoveOk 是否到达目标坐标
function Api.UnitMoveTo(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitMoveToEvent',...)
end
--! @brief 单位前往某位置
--! @param ObjectID 单位实例ID
--! @param Flag 路点名称
function Api.UnitAttachToFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitAttachToFlagEvent',...)
end
--! @brief 单位停止移动
--! @param ObjectID ObjectID
function Api.UnitStopMove(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitStopMoveEvent',...)
end
--! @brief 获取一个游戏对象的坐标
--! @param ObjectID ObjectID
--! @return X 
--! @return Y 
function Api.GetObjectPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetObjectPositionEvent',...)
end
--! @brief 获取一个玩家的名称
--! @param PlayerUUID 玩家UUID
--! @return Name 玩家姓名
function Api.GetPlayerName(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerNameEvent',...)
end
--! @brief 获取玩家的坐标
--! @param PlayerUUID 玩家UUID
--! @return X 
--! @return Y 
function Api.GetPlayerPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerPositionEvent',...)
end
--! @brief 杀死一个单位
--! @param ObjectID 单位的ObjectID
function Api.KillUnit(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.KillUnitEvent',...)
end
--! @brief 杀死一个玩家
--! @param PlayerUUID 单位的ObjectID
function Api.KillPlayer(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.KillPlayerEvent',...)
end
--! @brief 添加一个道具 返回值:道具的ObjectID
--! @param argMap
--! - Direction 方向
--! - Force 阵营
--! - TemplateID 模板ID
--! - X 坐标X
--! - Y 坐标Y
--! @return ObjectID 道具的ObjectID
function Api.AddItem(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.AddItemEvent',...)
end
--! @brief 以圆心和半径随机坐标点
--! @param X 圆心X
--! @param Y 圆心Y
--! @param RadiusSize 半径
--! @param Count 数量
--! @return Postions 坐标列表
function Api.RandomPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.RandomPositionEvent',...)
end
--! @brief 传送某单位到某坐标, 参数 {ObjectID:X:Y}
--! @param ObjectID 单位的ObjectID
--! @param X 坐标X
--! @param Y 坐标Y
function Api.SetUnitPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetUnitPositionEvent',...)
end
--! @brief 传送某玩家到某坐标
--! @param PlayerUUID 单位的ObjectID
--! @param X 坐标X
--! @param Y 坐标Y
function Api.SetPlayerPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerPositionEvent',...)
end
--! @brief 传送某玩家到某坐标
--! @param PlayerUUID 玩家UUID
--! @param Direction 方向
function Api.SetPlayerDirection(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerDirectionEvent',...)
end
--! @brief 获取场景Flag坐标, 参数-flag名称 return {x,y}
--! @param FlagName 场景Flag名称
--! @return X Flag X坐标
--! @return Y Flag Y坐标
function Api.GetFlagPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetFlagPositionEvent',...)
end
--! @brief 获取场景Flag坐标, 参数-flag名称 return exists,x,y
--! @param FlagName 场景Flag名称
--! @return ExistFlag Flag X坐标
--! @return X Flag X坐标
--! @return Y Flag Y坐标
function Api.TryGetFlagPosition(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.TryGetFlagPositionEvent',...)
end
--! @brief 获取场景内玩家玩家数量
--! @param Force Force
--! @return PlayerCount Force
function Api.GetAllPlayersCount(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetAllPlayersCountEvent',...)
end
--! @brief 统计命令点附近人数
--! @param Flag Flag名称
--! @param R 范围
--! @param Force 阵营
--! @return Count 数量
function Api.GetPlayerCountNearFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerCountNearFlagEvent',...)
end
--! @brief 获取场景内所有玩家
--! @param Force Force
--! @param ReverseForce ReverseForce
--! @return PlayerList 玩家的UUID列表
function Api.GetAllPlayers(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetAllPlayersEvent',...)
end
--! @brief 获取场景内所有玩家
--! @param Force Force
--! @param ReverseForce ReverseForce
--! @return PlayerList 玩家的UUID列表
function Api.GetAllSessions(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetAllSessionsEvent',...)
end
--! @brief 查找场景内符合条件的单位
--! @param Force Force
--! @param TemplateID TemplateID
--! @param ReverseForce ReverseForce
--! @return Units 玩家的UUID列表
function Api.FindUnits(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.FindUnitsEvent',...)
end
--! @brief 获取玩家的ObjectID
--! @param PlayerUUID 玩家UUID
--! @return ObjectID ObjectID
function Api.GetPlayerObjectID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerObjectIDEvent',...)
end
--! @brief 获取玩家的UUID
--! @param ObjectID 玩家ObjectID
--! @return PlayerUUID 玩家UUID
function Api.GetPlayerUUID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerUUIDEvent',...)
end
--! @brief 获取玩家的账号名称
--! @param PlayerUUID 玩家UUID
--! @return Session Session名称
function Api.GetSession(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetSessionEvent',...)
end
--! @brief 获取玩家的阵营
--! @param PlayerUUID 玩家UUID
--! @return Force 玩家阵营
function Api.GetPlayerForce(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerForceEvent',...)
end
--! @brief 获取单位的阵营
--! @param ObjectID 单位实例ID
--! @return Force 单位阵营
function Api.GetUnitForce(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitForceEvent',...)
end
--! @brief 获取单位名称
--! @param ObjectID 单位实例ID
--! @return UnitName 单位名称
function Api.GetUnitName(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitNameEvent',...)
end
--! @brief 判断Object是否存在
--! @param ObjectID ObjectID
--! @return Exist 是否存在
function Api.IsExistObject(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsExistObjectEvent',...)
end
--! @brief 判断Player是否存在
--! @param PlayerUUID ObjectID
--! @return Exist 是否存在
function Api.IsPlayerExist(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsPlayerExistEvent',...)
end
--! @brief 打开Flag
--! @param EditorName Flag名称
function Api.OpenFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.OpenFlagEvent',...)
end
--! @brief Enable编辑器事件
--! @param ScriptName Flag名称
--! @param Enable enable
function Api.EnableEditorScript(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.EnableEditorScriptEvent',...)
end
--! @brief 关闭Flag
--! @param EditorName Flag名称
function Api.CloseFlag(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.CloseFlagEvent',...)
end
--! @brief 复活一个单位
--! @param ObjectID 单位 ObjectID
--! @param MaxHp MaxHp
--! @param MaxMp Mp
function Api.StartRebirth(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.StartRebirthEvent',...)
end
--! @brief 设置玩家坐标到出生点
--! @param PlayerUUID 玩家UUID
function Api.SetPlayerPositionToStart(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetPlayerPositionToStartEvent',...)
end
--! @brief 复活一个玩家
--! @param PlayerUUID 玩家UUID
--! @param MaxHp MaxHp
--! @param MaxMp Mp
function Api.PlayerStartRebirth(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerStartRebirthEvent',...)
end
--! @brief 获取玩家职业
--! @param PlayerUUID 玩家UUID
--! @return Pro Pro
function Api.GetPlayerPro(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerProEvent',...)
end
--! @brief 获取玩家性别
--! @param PlayerUUID 玩家UUID
--! @return Gender Pro
function Api.GetPlayerGender(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetPlayerGenderEvent',...)
end
--! @brief 添加某玩家血量百分比
--! @param PlayerUUID 玩家UUID
--! @param Pct 百分比[0-100]
function Api.AddPlayerHpPercent(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.AddPlayerHpPercentEvent',...)
end
--! @brief 添加某单位血量百分比
--! @param ObjectID 单位实例ID
--! @param Pct 百分比[0-100]
function Api.AddUnitHpPercent(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.AddUnitHpPercentEvent',...)
end
--! @brief 复活一个单位
--! @param ObjectID 单位实例ID
--! @param MaxHp MaxHp
--! @param MaxMp Mp
function Api.UnitStartRebirth(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitStartRebirthEvent',...)
end
--! @brief 玩家是否死亡
--! @param PlayerUUID 玩家UUID
--! @return IsDead 是否死亡
function Api.IsPlayerDead(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsPlayerDeadEvent',...)
end
--! @brief 玩家是否准备完毕
--! @param PlayerUUID 玩家UUID
--! @return IsReady 是否准备完毕
function Api.IsPlayerReady(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsPlayerReadyEvent',...)
end
--! @brief 发送GameOver事件
--! @param WinForce 胜利阵营
--! @param Reason 原因
--! @param Ext 自定义数据
--! 	- map 
function Api.SetGameOver(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.SetGameOverEvent',...)
end
--! @brief 判断某单位是否是玩家
--! @param ObjectID 单位ObjectID
--! @return IsPlayer 是否是玩家
function Api.IsPlayer(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsPlayerEvent',...)
end
--! @brief 判断某单位是否是道具
--! @param ObjectID 单位ObjectID
--! @return IsItem 是否是玩家
function Api.IsItem(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.IsItemEvent',...)
end
--! @brief 单位血量信息
--! @param ObjectID 单位ObjectID
--! @return ret 
--! - CurHP 单位当前血量
--! - HPPct 血量百分比
--! - MaxHP 单位最大血量
function Api.GetUnitHPData(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetUnitHPDataEvent',...)
end
--! @brief 获取环境指定的环境变量
--! @param Key 环境变量KEY
--! @return Value 环境变量VALUE
function Api.GetEnvironmentoIntVar(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetEnvironmentoIntVarEvent',...)
end
--! @brief 获取环境指定的环境变量
--! @param Key 环境变量KEY
--! @return Value 环境变量VALUE
function Api.GetEnvironmentoStringVar(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetEnvironmentoStringVarEvent',...)
end
--! @brief 判断玩家是否在圆内
--! @param X X
--! @param PlayerUUID 玩家UUID
--! @param Y Y
--! @param R R
--! @return Include 是否在圆内
function Api.PlayerInRound(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerInRoundEvent',...)
end
--! @brief 判断玩家是否在矩形内
--! @param X X
--! @param PlayerUUID 玩家UUID
--! @param Y Y
--! @param W W
--! @param H H
--! @return Include 是否在矩形内
function Api.PlayerInRect(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.PlayerInRectEvent',...)
end
--! @brief 判断玩家是否在圆内
--! @param X X
--! @param ObjectID 单位实例ID
--! @param Y Y
--! @param R R
--! @return Include 是否在圆内
function Api.UnitInRound(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitInRoundEvent',...)
end
--! @brief 判断玩家是否在矩形内
--! @param X X
--! @param ObjectID 单位实例ID
--! @param Y Y
--! @param W W
--! @param H H
--! @return Include 是否在矩形内
function Api.UnitInRect(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.UnitInRectEvent',...)
end
--! @brief 获取道具模板ID
--! @param ObjectID ObjectID
--! @return TemplateID 模板ID
function Api.GetItemTemplateID(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetItemTemplateIDEvent',...)
end
--! @brief 获取场景RoomKey
--! @return RoomKey RoomKey
function Api.GetZoneRoomKey(...)
	return EventApi.DoSharpApi('Sync','ThreeLives.Server.Events.Zone.GetZoneRoomKeyEvent',...)
end
--! @brief 玩家是否场景中血量最大（百分比）
--! @param PlayerUUID PlayerUUID
--! @return IsMax 是否最大血量
function MasterRace.IsMaxHpPct(...)
	return EventApi.DoSharpApi('MasterRace.Sync','ThreeLives.Server.Events.Zone.IsMaxHpPctEvent',...)
end
--! @brief 新建一个位面环境
--! @param PlayerUUID 玩家UUID
function Task.PlayerAoiWork(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.PlayerAoiWorkEvent',...)
end
--! @brief 跨场景传送玩家
--! @param argMap
--! - Flag Flag名称，和坐标二选一
--! - MapTemplateID 地图ID
--! - PlayerUUID PlayerUUID
--! - TimeoutMS TimeoutMS
--! - X X坐标
--! - Y Y坐标
--! - ZoneUUID ZoneUUID
function Task.TransportPlayer(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.TransportPlayerEvent',...)
end
--! @brief 阵营临时队伍
function Task.AutoForceTeam(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.AutoForceTeamEvent',...)
end
--! @brief 新建一个怪物刷新点 
--! @param argMap
--! - Region Region的编辑器名称
--! - UnitAbilityData 怪物刷新数据（同编辑器配置数据）
--! 	- UnitTemplates 怪物类型模板ID和等级组(新功能)
--! 	- UnitTemplatesID 怪物类型模板ID组(兼容老版本)
--! 	- UnitLevel 怪物等级
--! 	- StartTimeDelayMS 延迟启动时间(毫秒)
--! 	- IntervalMS 刷新间隔时间(毫秒)
--! 	- OnceCount 一次刷新数量
--! 	- TotalLimit 总刷新数量上限（0表示无上限）
--! 	- AliveLimit 存活数量上限（0表示无上限）
--! 	- WithoutAlive 每次刷新必须所有怪物死亡
--! 	- Force 怪物初始阵营
--! 	- UnitTag 怪物初始标记
--! 	- UnitName 怪物名字
--! 	- ResetOnWithoutAlive 怪物没有存活时，重置刷新点计时（间隔时间用StartTimeDelayMS控制）
--! 	- StartPointName 怪物初始路点
--! 	- StartPathHoldMinTimeMS 切换路点待机最小时间(毫秒)
--! 	- StartPathHoldMaxTimeMS 切换路点待机最大时间(毫秒)
--! 	- StartDirection 初始朝向(大于等于0有效)
--! 	- SpawnEffect 怪物产生时触发的特效
--! 	- TFormation 一次刷新多个怪物时初始阵型
--! 	- Name Name
function Task.UnitSpawnAbility(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.UnitSpawnAbilityEvent',...)
end
--! @brief 新建一个道具刷新点 
--! @param argMap
--! - ItemAbilityData 道具刷新数据（同编辑器配置数据）
--! 	- UnitTemplates 怪物类型模板ID和等级组(新功能)
--! 	- UnitTemplatesID 怪物类型模板ID组(兼容老版本)
--! 	- UnitLevel 怪物等级
--! 	- StartTimeDelayMS 延迟启动时间(毫秒)
--! 	- IntervalMS 刷新间隔时间(毫秒)
--! 	- OnceCount 一次刷新数量
--! 	- TotalLimit 总刷新数量上限（0表示无上限）
--! 	- AliveLimit 存活数量上限（0表示无上限）
--! 	- WithoutAlive 每次刷新必须所有怪物死亡
--! 	- Force 怪物初始阵营
--! 	- UnitTag 怪物初始标记
--! 	- UnitName 怪物名字
--! 	- ResetOnWithoutAlive 怪物没有存活时，重置刷新点计时（间隔时间用StartTimeDelayMS控制）
--! 	- StartPointName 怪物初始路点
--! 	- StartPathHoldMinTimeMS 切换路点待机最小时间(毫秒)
--! 	- StartPathHoldMaxTimeMS 切换路点待机最大时间(毫秒)
--! 	- StartDirection 初始朝向(大于等于0有效)
--! 	- SpawnEffect 怪物产生时触发的特效
--! 	- TFormation 一次刷新多个怪物时初始阵型
--! 	- Name Name
--! - Region Region的编辑器名称
function Task.ItemSpawnAbitity(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.ItemSpawnAbitityEvent',...)
end
--! @brief 新建一个坐标-范围的单位刷新点
--! @param argMap
--! - AOIBinder 绑定位面所有者,此值不为空表示刷新点在位面中
--! - AliveLimit 存活数量上限（0表示无上限
--! - DeadCallBack 单位死亡回调
--! - Force 怪物初始阵营
--! - IntervalMS 刷新间隔时间(毫秒)
--! - ObjectTemplates 怪物模版组
--! 	- 参数为一个Array []
--! 		- TemplateID 
--! 		- Direction 
--! 		- MirrorData 
--! - OnceCount 一次刷新数量
--! - RadiusSize 刷新半径
--! - ResetOnWithoutAlive 怪物没有存活时，重置刷新点计时（间隔时间用StartTimeDelayMS控制）
--! - StartTimeDelayMS 开始延迟毫秒
--! - TotalLimit 总刷新数量上限（0表示无上限）
--! - WithoutAlive 每次刷新必须所有怪物死亡
--! - X 
--! - Y 
function Task.UnitPositionSpawn(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.UnitPositionSpawnEvent',...)
end
--! @brief 新建一个坐标-范围的物品刷新点
--! @param argMap
--! - AOIBinder 绑定位面所有者,此值不为空表示刷新点在位面中
--! - AliveLimit 存活数量上限（0表示无上限
--! - Force 怪物初始阵营
--! - IntervalMS 刷新间隔时间(毫秒)
--! - ObjectTemplates 怪物模版组
--! 	- 参数为一个Array []
--! 		- TemplateID 
--! 		- Direction 
--! 		- MirrorData 
--! - OnceCount 一次刷新数量
--! - RadiusSize 刷新半径
--! - ResetOnWithoutAlive 怪物没有存活时，重置刷新点计时（间隔时间用StartTimeDelayMS控制）
--! - StartTimeDelayMS 开始延迟毫秒
--! - TotalLimit 总刷新数量上限（0表示无上限）
--! - WithoutAlive 每次刷新必须所有怪物死亡
--! - X 
--! - Y 
function Task.ItemPositionSpawn(...)
	return EventApi.DoSharpApi('Async','ThreeLives.Server.Events.Zone.ItemPositionSpawnEvent',...)
end
--! @brief 监听并定制单位进入场景前创建的Force
function Listen.InitPlayerForce(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.InitPlayerForceEvent',...)
end
--! @brief 监听玩家是否离开战斗状态
--! @param PlayerUUID 玩家UUID
function Listen.PlayerBattleStateChange(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerBattleStateChangeEvent',...)
end
--! @brief 监听玩家重连
--! @param PlayerUUID 玩家UUID
function Listen.SessionReconnect(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.SessionReconnectEvent',...)
end
--! @brief 是否可见目标
function Listen.IsVisibleAOI(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.IsVisibleAOIEvent',...)
end
--! @brief 对象进入玩家视野
--! @param PlayerUUID 玩家UUID
function Listen.ObjectEnterPlayerView(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.ObjectEnterPlayerViewEvent',...)
end
--! @brief 玩家进入玩家视野
--! @param PlayerUUID 玩家UUID
function Listen.PlayerEnterPlayerView(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerEnterPlayerViewEvent',...)
end
--! @brief 玩家准备完毕
--! @param PlayerUUID PlayerUUID
function Listen.PlayerReady(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerReadyEvent',...)
end
--! @brief 是否能采集某模板ID的道具
--! @param ObjectID 模板ID
function Listen.PlayerTryPickItem(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerTryPickItemEvent',...)
end
--! @brief 是否能采集某模板ID的道具
--! @param TemplateID 模板ID
function Listen.PlayerTryPickTemplateItem(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerTryPickTemplateItemEvent',...)
end
--! @brief 玩家进入某Region
--! @param EditorName Region名称
function Listen.PlayerEnterRegion(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerEnterRegionEvent',...)
end
--! @brief 玩家离开某Region
--! @param EditorName Region名称
function Listen.PlayerLeaveRegion(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerLeaveRegionEvent',...)
end
--! @brief Flag 被打开
--! @param EditorName Flag名称
function Listen.FlagOpend(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.FlagOpendEvent',...)
end
--! @brief Flag 被打开
--! @param EditorName Flag名称
function Listen.FlagClosed(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.FlagClosedEvent',...)
end
--! @brief 玩家进入某Area
--! @param EditorName Area名称
function Listen.PlayerEnterArea(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerEnterAreaEvent',...)
end
--! @brief 玩家离开某Area
--! @param EditorName Area名称
function Listen.PlayerLeaveArea(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerLeaveAreaEvent',...)
end
--! @brief 玩家进入场景
--! @param PlayerUUID PlayerUUID
function Listen.PlayerEnterZone(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerEnterZoneEvent',...)
end
--! @brief 玩家离开场景
--! @param PlayerUUID PlayerUUID
function Listen.PlayerLeaveZone(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerLeaveZoneEvent',...)
end
--! @brief 单位接受某任务
function Listen.PlayerQuestAccepted(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerQuestAcceptedEvent',...)
end
--! @brief 单位提交任务
function Listen.PlayerQuestCommitted(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerQuestCommittedEvent',...)
end
--! @brief 单位放弃任务
function Listen.PlayerQuestDropped(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerQuestDroppedEvent',...)
end
--! @brief 单位任务子状态更新
function Listen.PlayerQuestStateUpdated(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerQuestStateUpdatedEvent',...)
end
--! @brief 玩家死亡
--! @param PlayerUUID PlayerUUID
function Listen.PlayerDead(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerDeadEvent',...)
end
--! @brief 单位死亡
--! @param ObjectID ObjectID
function Listen.UnitDead(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitDeadEvent',...)
end
--! @brief 指定单位复活
--! @param ObjectID 单位 ObjectID
function Listen.UnitRebirth(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitRebirthEvent',...)
end
--! @brief 监听游戏对象被移除
--! @param ObjectID ObjectID
function Listen.ObjectRemove(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.ObjectRemoveEvent',...)
end
--! @brief 监听道具被采集
--! @param ItemID 道具ID
function Listen.PlayerPickedItem(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerPickedItemEvent',...)
end
--! @brief 监听某模版道具被采集 参数-模版ID
--! @param TemplateID 道具模板ID
function Listen.PlayerPickedTemplateItem(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerPickedTemplateItemEvent',...)
end
--! @brief 监听场景GameOver
function Listen.GameOver(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.GameOverEvent',...)
end
--! @brief 监听单位或指定单位受伤害
--! @param ObjectID 指定单位，为0表示所有单位
function Listen.UnitDamage(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitDamageEvent',...)
end
--! @brief 监听某模板单位受伤害
--! @param TemplateID 模板ID
function Listen.TemplateUnitDamage(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.TemplateUnitDamageEvent',...)
end
--! @brief 单位进入某矩形区域
--! @param X X
--! @param Y Y
--! @param W W
--! @param H H
--! @param ObjectID ObjectID
function Listen.UnitEnterRect(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitEnterRectEvent',...)
end
--! @brief 单位离开某矩形区域
--! @param X X
--! @param Y Y
--! @param W W
--! @param H H
--! @param ObjectID ObjectID
function Listen.UnitLeaveRect(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitLeaveRectEvent',...)
end
--! @brief 单位进入某圆形区域
--! @param X X
--! @param Y Y
--! @param R R
--! @param ObjectID ObjectID
function Listen.UnitEnterRound(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitEnterRoundEvent',...)
end
--! @brief 单位离开某圆形区域
--! @param X X
--! @param Y Y
--! @param R R
--! @param ObjectID ObjectID
function Listen.UnitLeaveRound(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.UnitLeaveRoundEvent',...)
end
--! @brief 监听单位进入指定坐标矩形范围
--! @param X X
--! @param Y Y
--! @param R R
--! @param PlayerUUID PlayerUUID
function Listen.PlayerEnterRound(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerEnterRoundEvent',...)
end
--! @brief 监听单位进入指定坐标矩形范围
--! @param X X
--! @param Y Y
--! @param R R
--! @param PlayerUUID PlayerUUID
function Listen.PlayerLeaveRound(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerLeaveRoundEvent',...)
end
--! @brief 监听单位进入指定坐标矩形范围
--! @param X X
--! @param Y Y
--! @param W W
--! @param H H
--! @param PlayerUUID PlayerUUID
function Listen.PlayerEnterRect(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerEnterRectEvent',...)
end
--! @brief 监听单位进入指定坐标矩形范围
--! @param X X
--! @param Y Y
--! @param W W
--! @param H H
--! @param PlayerUUID PlayerUUID
function Listen.PlayerLeaveRect(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.PlayerLeaveRectEvent',...)
end
--! @brief 环境变量是否发生更改
--! @param Key Key
function Listen.EnvironmentVarChanged(...)
	return EventApi.DoSharpApi('Listen','ThreeLives.Server.Events.Zone.EnvironmentVarChangedEvent',...)
end
return Api
--! @}
