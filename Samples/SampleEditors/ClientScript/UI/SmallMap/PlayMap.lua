local _M = {}
_M.__index = _M
print('-------------load PlayMap ---------------------')

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local MapModel = require 'Model/MapModel'

local function closeAll(self)
	-- body
	self.ui.menu:Close()
end

local function HideAllWayPoint(self)
	for k,v in pairs(self.wayPointCvsMap or {}) do
		v.Visible = false
	end
end

local function AutoRunByXY(self,aimX,aimY)

	self.aimCvs.Visible = false
	HideAllWayPoint(self)

	local actor = TLBattleScene.Instance.Actor
	if not actor then
		return
    end

	local action = MoveEndAction()
	action.AimX = aimX
	action.AimY = aimY
	action.MapId = self.TemplateID
	action.MoveType = AutoMoveType._SmallMapTouch
	EventManager.Fire("Event.State.MapTouch",{})
	actor:AutoRunByAction(action)
end

local function GetWayPointCvs(self)
	local roadPoint = self.wayPointCvsMap[self.roadPointIndex]
	if roadPoint then
		return roadPoint
	end
	roadPoint = self.xunludianCvs:Clone()
	roadPoint.UserTag =self.roadPointIndex
	self.mapCvs:AddChild(roadPoint)
	self.wayPointCvsMap[self.roadPointIndex] = roadPoint
	return roadPoint
end

local function PassWayPoint(self,X,Y )
	local wpMap = self.wpMap
	for i = -1,1 do
		local posX = X + i
		for j =  -1,1  do
			local posY = Y + j
			if wpMap and wpMap[posX] and wpMap[posX][posY] then
				local roadPoint = wpMap[posX][posY]
	 			roadPoint.Visible = false
	 			local tag = roadPoint.UserTag

	 			-- 修复下一点已消失前面一点还存在的bug
	 			if tag > 1 then
	 				local lastWp = self.wayPointCvsMap[tag - 1]	
	 				if lastWp then
	 					lastWp.Visible = false
	 				end
	 			end
				wpMap[posX][posY] = nil
			end
		end
	end
end

local function ClearWayPoint(self)
	for k,v in pairs(self.wayPointCvsMap) do
		v:RemoveFromParent(true)
	end
	self.wayPointCvsMap = {}
	self.wpMap = {}
end


local function ShowActorPos(self,oldV3)
	-- body
	-- local pos = DataMgr.Instance.UserData:GetActorPos() 
	local pos = GameUtil.GetActorPos() 
	local angle =  pos.z
	self.smallActor.Transform.localEulerAngles = Vector3(oldV3.x, oldV3.y, angle);
	
	local posX = math.floor(pos.x)
	local posY = math.floor(pos.y)
	local posstr = Util.Format1234('({0},{1})',posX,posY)

	self.nameLabel.Text = self.MapName .. '  ' .. posstr
	self.smallActor.Position2D = Vector2(posX * 1.6+40,posY * 1.6+40)

	PassWayPoint(self,posX,posY)
 
end

local function PreShowWayPoint(self,wpMap,wp)
	local posX = math.floor(wp.x)
	local posY = math.floor(wp.y)
	
	if wpMap[posX] == nil or wpMap[posX][posY] == nil then
		self.roadPointIndex = self.roadPointIndex + 1
		local roadPoint = GetWayPointCvs(self)
	 	roadPoint.Position2D = Vector2(posX * 1.6+40,posY  * 1.6+40)
	 	-- print('self.roadPointIndex:',self.roadPointIndex)
	 	-- roadPoint.Scale = Vector2(self.roadPointIndex,self.roadPointIndex)
		roadPoint.Visible = true
		
		wpMap[posX] = wpMap[posX] or {}
		wpMap[posX][posY] = roadPoint 		
	end
end



local function OnEventAutoRun(self,eventname, params)
	local IsRun = params.IsRun
	if not IsRun then
 		self.aimCvs.Visible = false
		closeAll(self)
	else
		local target = params.target
		if target == nil then
			return
		end

		local target = params.target
		self.aimCvs.Position2D = Vector2(target.x * 1.6+40,target.y * 1.6+40)
		self.aimCvs.Visible = true

		self.roadPointIndex = 0
		local wpMap = {}
		local wp = params.waypoint

		for point in Slua.iter(params.waypoint) do
 		 	PreShowWayPoint(self,wpMap,point)
 		end

		self.wpMap = wpMap
	end
end

local function GetUnitCvs(self,unitData)
	-- body
	local unit = self.unitCvsMap[unitData.ID]
	if  unit then
		return unit
	end

	if unitData.UnitType == UnitInfo.UnitType.TYPE_NPC then
		local npcCvs = self.npcCvs:Clone()
		unit = SmallMapNpc(unitData.templateId,npcCvs)
		if not string.IsNullOrEmpty(unitData.ICON) then
			unit:SetImage(unitData.ICON)
		end
		self.unitCvsMap[unitData.ID] = unit
		self.mapCvs:AddChild(npcCvs)
	else
		local unitCvs = self.unitCvs:Clone()
		unit = SmallMapUnit(unitCvs,unitData.ForceType,unitData.isTeamMate)
		if not string.IsNullOrEmpty(unitData.ICON) then
			unit:SetImage(unitData.ICON)
		end
		self.unitCvsMap[unitData.ID] = unit
		self.mapCvs:AddChild(unitCvs)
	end

	self.mapCvs:AddChild(unitCvs)
	self.unitCvsMap[unitData.ID] = unit
	return unit
end

local function RemoveAllUnit(self)
	-- body
	for k,v in pairs(self.unitCvsMap) do
		v:Clear()
	end
	self.unitCvsMap = {}
end 

 
local function HideAllUnit(self)
	for k,v in pairs(self.unitCvsMap) do
		v.Visible = false
	end
end 

local function AddUnit(self,unitData)
	local unitCvs = GetUnitCvs(self,unitData)
	unitCvs.Visible = true
	unitCvs:SetPosition(unitData.X * 1.6+40,unitData.Y * 1.6+40)

end

local function ShowMapUnit(self,mapId)
	-- body
	HideAllUnit(self)

	local dataList = GameUtil.GetPlayMapUnitByMapType(mapId) 
	 
	local unitDatas = {}
	for unitData in Slua.iter(dataList) do
		-- print_r('unitData:',unitData)
	 	AddUnit(self,unitData)
 	end
end

function _M.OnEnter(self, ...)

	self.unitCvsMap = {}

 	local force  =  self.ui.menu.ExtParam[1]

	self.aimCvs.Visible = false

	local TemplateID = DataMgr.Instance.UserData.MapTemplateId
 	self.TemplateID = TemplateID
 	self.MapName =  DataMgr.Instance.UserData.MapName


 
	local oldV3 = self.smallActor.Transform.localEulerAngles
	ShowActorPos(self,oldV3)

	-- GameUtil.GetPlayMapUnitList(UnitInfo.UnitType.TYPE_PLAYER,force)
	local mapData = GlobalHooks.DB.FindFirst('MapData',{ id = self.TemplateID })

	local mMapImgPath = 'dynamic/map/' .. mapData.small_map .. ".png"
	UIUtil.SetImage(self.mapCvs,mMapImgPath,true)

	self.mapCvs.Position2D = Vector2((self.minimapCvs.Size2D.x-self.mapCvs.Size2D.x)*0.5,(self.minimapCvs.Size2D.y-self.mapCvs.Size2D.y)*0.5)

	-- print_r('mapData:',mapData)
	-- local mapSetting = GlobalHooks.DB.FindFirst('MapSetting', { type = mapData.type })
	-- print_r('mapSetting:',mapSetting)

	ShowMapUnit(self,mapData.mapsetting_id)

	if self.timerId ~= nil then
		LuaTimer.Delete(self.timerId)
		self.timerId = nil
	end

	local index = 0
	self.timerId = LuaTimer.Add(0,264,function(id)
		ShowActorPos(self,oldV3)
		index = index + 1
		-- print('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa')
		if (index % 3 == 0) then
			-- print('bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb')
			ShowMapUnit(self,mapData.mapsetting_id)
		end
		return true
	end)

	_M.eventFun = function( eventname, params )
		OnEventAutoRun(self, eventname, params)
	end

	EventManager.Subscribe("Event.AutoRun.RoadPoint", _M.eventFun)

end


function _M.OnDestory( self )
	print('PlayMap OnDestory')
 
	
end

function _M.OnExit( self )
	if self.timerId ~= nil then
		LuaTimer.Delete(self.timerId)
		self.timerId = nil
	end


	ClearWayPoint(self)

	RemoveAllUnit(self)
	
	EventManager.Unsubscribe("Event.AutoRun.RoadPoint", _M.eventFun)
end


local function InitUnitComp(self)
	-- body
	self.npcCvs = self.ui.comps.cvs_npc
	self.unitCvs = self.ui.comps.cvs_unit 
	-- self.playerCvs = {}
 -- 	local palyer1 = self.ui.comps.cvs_player1
 -- 	palyer1.Visible = false
 -- 	self.playerCvs[1] = palyer1


	-- local palyer2 = self.ui.comps.cvs_player2
	-- palyer2.Visible = false
 -- 	self.playerCvs[2] = palyer2

 -- 	self.TeamMateCvs = self.ui.comps.cvs_teammate
 -- 	self.TeamMateCvs.Visible = false

 -- 	self.npcCvs = {}
	-- local npc0 = self.ui.comps.cvs_npc0
	-- npc0.Visible = false
	-- self.npcCvs[0] = npc0

	-- local npc1 = self.ui.comps.cvs_npc1
	-- npc1.Visible = false
	-- self.npcCvs[1] = npc1

	-- local npc2 = self.ui.comps.cvs_npc2
	-- npc2.Visible = false
	-- self.npcCvs[2] = npc2


	-- self.monsterCvs = {}
	-- local monster0 = self.ui.comps.cvs_monster0
	-- monster0.Visible = false
	-- self.monsterCvs[0] = monster0
 
	-- local monster1 = self.ui.comps.cvs_monster1
	-- monster1.Visible = false
 -- 	self.monsterCvs[1] = monster1

	-- local monster2 = self.ui.comps.cvs_monster2
	-- monster2.Visible = false
	-- self.monsterCvs[2] = monster2
end

function _M.OnInit( self )
 
	--目标点
	self.aimCvs = self.ui.comps.cvs_xunlumubiao
	self.aimCvs.Visible = false
	-- 路点
	self.xunludianCvs = self.ui.comps.lb_xunludian
	self.xunludianCvs.Visible = false

	self.wayPointCvsMap = {}
		
	InitUnitComp(self)

	-- local function MapTouchClick(self,touchPoint)
	-- 	local action = MoveEndAction()
	-- 	action.AimX = touchPoint.x * 0.5
	-- 	action.AimY = touchPoint.y * 0.5
	-- 	action.MapId = self.TemplateID
	-- 	action.MoveType = AutoMoveType._SmallMapTouch
	-- 	if TLBattleScene.Instance.Actor then
	-- 		EventManager.Fire("Event.State.MapTouch",{})
	-- 		TLBattleScene.Instance.Actor:AutoRunByAction(action)
	-- 	end
	-- end

	local function MapTouchClick(self,touchPoint)
		AutoRunByXY(self,(touchPoint.x-40)/1.6, (touchPoint.y-40)/1.6)
	end


	self.nameLabel = self.ui.comps.lb_mapname
	--主角
	self.smallActor = self.ui.comps.cvs_actor

	self.mapCvs = self.ui.comps.cvs_map

	self.minimapCvs = self.ui.comps.cvs_minimap

	self.mapCvs.event_PointerUp =  function (sender,e)

		
	 	local touchPoint = self.mapCvs:ScreenToLocalPoint2D(e)
	 	-- print('touchPoint:',touchPoint.x * 0.5,touchPoint.y * 0.5)
		MapTouchClick(self,touchPoint)
		
	end

end


return _M