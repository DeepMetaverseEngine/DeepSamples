--! @addtogroup Client
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
local Camera = {Task ={},Listen={}}
Api.Camera = Camera
--! @brief 设置主镜头参数
--! @param CameraArg 动态参数
--! @param PostionOffset 坐标偏移
--! @param AngleOffset 角度偏移
function Camera.SetArgument(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.SetArgumentEvent',...)
end
--! @brief 重置主镜头参数
function Camera.ResetArgument(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.ResetArgumentEvent',...)
end
--! @brief 重置主镜头参数
--! @param Pos 坐标
--! @param Angle 角度
--! @param Scale 缩放
function Camera.SetLocation(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.SetLocationEvent',...)
end
--! @brief 停止跟随主角
function Camera.StopFollowActor(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.StopFollowActorEvent',...)
end
--! @brief 重置主镜头参数
--! @param Pos 坐标
--! @param Angle 角度
--! @param Scale 缩放
function Camera.ResetLocation(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.ResetLocationEvent',...)
end
--! @brief 主镜头跟随某单位
--! @param EntityID ID
function Camera.FollowCGUnit(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.FollowCGUnitEvent',...)
end
--! @brief 主镜头跟随某玩家
--! @param PlayerUUID PlayerUUID
function Camera.FollowPlayer(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.FollowPlayerEvent',...)
end
--! @brief 主镜头跟随某UNIT
--! @param ObjectID ObjectID
function Camera.FollowTargetUnit(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.FollowTargetUnitEvent',...)
end
--! @brief 主镜头跟随某玩家
function Camera.FollowActor(...)
	return EventApi.DoSharpApi('Camera.Sync','EventScript.Client.Events.FollowActorEvent',...)
end
--! @brief 获取玩家的UUID
--! @return UUID 
function Api.GetActorUUID(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorUUIDEvent',...)
end
--! @brief 显示错误消息
--! @param Message 消息内容
function Api.ShowErrorMessage(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.ShowErrorMessageEvent',...)
end
--! @brief 显示消息
--! @param Message 消息内容
function Api.ShowMessage(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.ShowMessageEvent',...)
end
--! @brief 设置主角是否显示
--! @param Visible 是否显示
function Api.SetMainActorVisible(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.SetMainActorVisibleEvent',...)
end
--! @brief 设置玩家是否显示
--! @param roleId roleID
--! @param Visible 是否显示
function Api.SetPlayerVisible(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.SetPlayerVisibleEvent',...)
end
--! @brief 设置主角是否可控制
--! @param EnableCtrl 是否可控
function Api.SetActorEnableCtrl(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.SetActorEnableCtrlEvent',...)
end
--! @brief 设置玩家下坐骑
function Api.SetActorUnMount(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.SetActorUnMountEvent',...)
end
--! @brief 播放镜花水月特效
function Api.PlayRippleEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.PlayRippleEffectEvent',...)
end
--! @brief 开关模糊特效
--! @param argMap
--! - Enable 
--! - Strength 
function Api.SetEnableBlurEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.SetEnableBlurEffectEvent',...)
end
--! @brief 播放全屏Fadeout
--! @param Time 
function Api.PlayFadeOutEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.PlayFadeOutEffectEvent',...)
end
--! @brief 播放全屏Fadein
--! @param Time 
function Api.PlayFadeInEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.PlayFadeInEffectEvent',...)
end
--! @brief 对场景内单位添加气泡框
--! @param ObjectID 
--! @param Content 
--! @param KeepTime 
function Api.AddBubbleTalk(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.AddBubbleTalkEvent',...)
end
--! @brief 播放单位特效
--! @param ObjectID ObjectID
--! @param Effect LaunchEffect
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
--! @return EffectID 特效ID
function Api.PlayUnitEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.PlayUnitEffectEvent',...)
end
--! @brief 播放玩家特效
--! @param PlayerUUID PlayerUUID
--! @param Effect LaunchEffect
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
--! @return EffectID 特效ID
function Api.PlayPlayerEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.PlayPlayerEffectEvent',...)
end
--! @brief 停止一个特效
--! @param EffectID 特效ID
function Api.StopEffect(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.StopEffectEvent',...)
end
--! @brief 播放单位动作
--! @param ObjectID ObjectID
--! @param AnimationName AnimationName
function Api.PlayUnitAnimation(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.PlayUnitAnimationEvent',...)
end
--! @brief 获取主角ObjectID
--! @return ObjectID ObjectID
function Api.GetActorID(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorIDEvent',...)
end
--! @brief 获取主角ActorName
--! @return ActorName ActorName
function Api.GetActorName(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorNameEvent',...)
end
--! @brief 获取主角阵营
--! @return Force Force
function Api.GetActorForce(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorForceEvent',...)
end
--! @brief 主角加载完成
--! @return IsLoadFinish 是否加载完成
function Api.IsActorReady(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.IsActorReadyEvent',...)
end
--! @brief 主角是否已死亡
--! @return IsDead 是否死亡
function Api.IsActorDead(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.IsActorDeadEvent',...)
end
--! @brief 获取Actor当前坐标
--! @return X X
--! @return Y Y
function Api.GetActorPostion(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorPostionEvent',...)
end
--! @brief 停止主角自动寻路
function Api.StopActorAutoRun(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.StopActorAutoRunEvent',...)
end
--! @brief 主角是否在自动寻路状态
--! @return IsAutoRun IsAutoRun
function Api.IsActorAutoRun(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.IsActorAutoRunEvent',...)
end
--! @brief 获取主角公会ID
--! @return GuildUUID 公会ID
function Api.GetActorGuildUUID(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorGuildUUIDEvent',...)
end
--! @brief 获取指定阵营的玩家信息
--! @param Force Force
--! @return Players 玩家信息
function Api.GetPlayerInfoByForce(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetPlayerInfoByForceEvent',...)
end
--! @brief 查找寻路坐标点
--! @param X 
--! @param Y 
--! @param TargetX 
--! @param TargetY 
--! @return Ways 坐标列表
function Api.FindPath(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.FindPathEvent',...)
end
--! @brief 获取指定的玩家信息
--! @param PlayerUUID 玩家UUID
--! @return Player 玩家信息
function Api.GetPlayerInfo(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetPlayerInfoEvent',...)
end
--! @brief 获取玩家所在场景UUID
--! @return UUID UUID
function Api.GetZoneUUID(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetZoneUUIDEvent',...)
end
--! @brief 获取玩家所在场景UUID
--! @param ObjectID ObjectID
--! @return X X
--! @return Y Y
function Api.GetObjectPosition(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetObjectPositionEvent',...)
end
--! @brief 获取玩家所在场景UUID
--! @param ObjectID ObjectID
--! @return Pos Pos
function Api.GetObjectUnityPosition(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetObjectUnityPositionEvent',...)
end
--! @brief 2D坐标转换成3D坐标
--! @param X X
--! @param Y Y
--! @return Pos Pos
function Api.GetUnityPosistion(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetUnityPosistionEvent',...)
end
--! @brief 通过ObjectID获取模板ID
--! @param ObjectID ObjectID
--! @return TemplateID TemplateID
function Api.GetUnitTemplateID(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetUnitTemplateIDEvent',...)
end
--! @brief 获取Flag坐标
--! @param Flag Flag
--! @return X X
--! @return Y Y
function Api.GetFlagPosition(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetFlagPositionEvent',...)
end
--! @brief 获取当前地图模板ID
--! @return MapTemplateID 模板ID
function Api.GetMapTemplateID(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetMapTemplateIDEvent',...)
end
--! @brief 获取主角的Avartar map
--! @return AvatarMap AvatarMap
function Api.GetActorAvartarMap(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.GetActorAvartarMapEvent',...)
end
--! @brief 是否正处于跟随状态
--! @param IsFollow IsFollow
function Api.IsFollowState(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.IsFollowStateEvent',...)
end
--! @brief 跟随指定的单位
--! @param ObjectID 单位实例ID
--! @return FollowOK 是否执行成功
function Api.FollowUnit(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.FollowUnitEvent',...)
end
--! @brief 跟随选中的单位
--! @param ObjectID 单位实例ID
--! @return FollowOK 是否执行成功
function Api.FollowSelectUnit(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.FollowSelectUnitEvent',...)
end
--! @brief 主角是否在自动战斗状态
--! @return IsAutoGuard IsAutoGuard
function Api.IsActorAutoGuard(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.IsActorAutoGuardEvent',...)
end
--! @brief 二进制数据转字符串
--! @param Bytes 二进制数据
--! 	- 参数为一个Array []
--! @return Str Utf8字符串
function Api.Bytes2String(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.Bytes2StringEvent',...)
end
--! @brief 加载二进制数据
--! @param Bytes 二进制数据
--! 	- 参数为一个Array []
--! @return Obj LuaLoad result
function Api.LoadLuaBytes(...)
	return EventApi.DoSharpApi('Sync','EventScript.Client.Events.LoadLuaBytesEvent',...)
end
--! @brief 临时设置主镜头参数
--! @param CameraArg 动态参数
--! @param PostionOffset 坐标偏移
--! @param AngleOffset 角度偏移
function Camera.Task.SetArgumentOnce(...)
	return EventApi.DoSharpApi('Camera.Async','EventScript.Client.Events.SetArgumentOnceEvent',...)
end
--! @brief 镜头移动到
--! @param TargetPos Pos
--! @param Duration Duration
function Camera.Task.MoveTo(...)
	return EventApi.DoSharpApi('Camera.Async','EventScript.Client.Events.MoveToEvent',...)
end
--! @brief 镜头移动和设置
--! @param TargetPos Pos
--! @param Duration Duration
function Camera.Task.MoveToAndSet(...)
	return EventApi.DoSharpApi('Camera.Async','EventScript.Client.Events.MoveToAndSetEvent',...)
end
--! @brief 镜头旋转
--! @param TargetPos Pos
--! @param Duration Duration
function Camera.Task.RotateTo(...)
	return EventApi.DoSharpApi('Camera.Async','EventScript.Client.Events.RotateToEvent',...)
end
--! @brief 镜头旋转和设置
--! @param TargetPos Pos
--! @param Duration Duration
function Camera.Task.RotateToAndSet(...)
	return EventApi.DoSharpApi('Camera.Async','EventScript.Client.Events.RotateToAndSetEvent',...)
end
--! @brief 获取并执行Url Lua文件
--! @param Url Url
--! @return Obj LuaLoad result
function Task.LoadWWWLua(...)
	return EventApi.DoSharpApi('Async','EventScript.Client.Events.LoadWWWLuaEvent',...)
end
--! @brief 主角加载完成
function Listen.ActorReady(...)
	return EventApi.DoSharpApi('Listen','EventScript.Client.Events.ActorReadyEvent',...)
end
--! @brief 退出该场景
function Listen.ActorLeaveMap(...)
	return EventApi.DoSharpApi('Listen','EventScript.Client.Events.ActorLeaveMapEvent',...)
end
--! @brief 某单位成功进入视野
--! @param ObjectID ObjectID
function Listen.UnitInView(...)
	return EventApi.DoSharpApi('Listen','EventScript.Client.Events.UnitInViewEvent',...)
end
--! @brief 单位是否已出生
function Listen.ActorBirth(...)
	return EventApi.DoSharpApi('Listen','EventScript.Client.Events.ActorBirthEvent',...)
end
return Api
--! @}
