--! @addtogroup Client
--! @{
local Api = {Task={},Listen={}}
local Task = Api.Task
local Listen = Api.Listen
local CG = {Task ={},Listen={}}
Api.CG = CG
--! @brief 新建一个单位
--! @return EntityID ID
function CG.CreateUnit(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.CreateUnitEvent',...)
end
--! @brief 加载单位模型和Avatar
--! @param EntityID EntityID
--! @param Direction Direction
function CG.UnitSetDirection(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.UnitSetDirectionEvent',...)
end
--! @brief 单位设置坐标
--! @param EntityID X
--! @param X X
--! @param Y Y
function CG.UnitSetPostion(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.UnitSetPostionEvent',...)
end
--! @brief 单位播放特效
--! @param EntityID EntityID
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
--! @return EffectID EffectID
function CG.UnitPlayEffect(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.UnitPlayEffectEvent',...)
end
--! @brief 单位停止播放特效
--! @param EntityID EntityID
--! @return EffectID EffectID
function CG.UnitStopEffect(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.UnitStopEffectEvent',...)
end
--! @brief 单位播放动作
--! @param EntityID EntityID
--! @param AnimName 动画名称
function CG.UnitPlayAnimation(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.UnitPlayAnimationEvent',...)
end
--! @brief 单位添加冒泡文字
--! @param EntityID 
--! @param Content 
--! @param KeepTime 
function CG.AddBubbleTalk(...)
	return EventApi.DoSharpApi('CG.Sync','EventScript.Client.Events.CG.AddBubbleTalkEvent',...)
end
--! @brief 进入一个游戏世界
function CG.Task.EnterGameWorld(...)
	return EventApi.DoSharpApi('CG.Async','EventScript.Client.Events.CG.EnterGameWorldEvent',...)
end
--! @brief 加载单位模型和Avatar
--! @param EntityID X
--! @param ModelName 模型路径
--! @param Avatars 模型Avartar
--! 	- 参数为一个Map 
function CG.Task.LoadUnit(...)
	return EventApi.DoSharpApi('CG.Async','EventScript.Client.Events.CG.LoadUnitEvent',...)
end
--! @brief 新建并加载一个单位
--! @param ModelName 模型路径
--! @param Avatars 模型Avartar
--! 	- 参数为一个Map 
--! @param X X
--! @param Y Y
--! @param Direction Direction
--! @return EntityID ID
function CG.Task.CreateAndLoadUnit(...)
	return EventApi.DoSharpApi('CG.Async','EventScript.Client.Events.CG.CreateAndLoadUnitEvent',...)
end
--! @brief 单位前往某位置
--! @param EntityID EntityID
--! @param Duration 
--! @param X 
--! @param Y 
--! @param AnimName 
function CG.Task.UnitMoveTo(...)
	return EventApi.DoSharpApi('CG.Async','EventScript.Client.Events.CG.UnitMoveToEvent',...)
end
return Api
--! @}
