local _M = {}
_M.__index = _M
print('-------------load SceneMap ---------------------')

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local MapModel = require 'Model/MapModel'


local function closeAll(self)
	-- body
	self.ui.menu.ParentMenu:Close()
end

local function HideAllWayPoint(self)
	for k,v in pairs(self.wayPointCvsMap) do
		v.Visible = false
	end
end

local function AutoRunByXY(self,aimX,aimY)

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

local function AutoRunByNPC(self,npctempid)

	HideAllWayPoint(self)
	
	local actor = TLBattleScene.Instance.Actor
	if not actor then
		return
    end

	local action = MoveAndNpcTalk(npctempid)
	action.OpenFunction = true
	action.MapId = self.TemplateID
	EventManager.Fire("Event.State.MapTouch",{})
	TLBattleScene.Instance.Actor:AutoRunByAction(action)
end

local function RequestNearPlayers(cb)
	-- print('DataMgr.Instance.UserData.RoleID:',DataMgr.Instance.UserData.RoleID)
	local actor = TLBattleScene.Instance.Actor
	if not actor then
		return
    end
    actor:GetZonePlayersUUID(function(rsp)
		local list = rsp.b2c_list
    	local allRole = {}
		for playerId in Slua.iter(list) do
 		 	-- 过滤玩家自己
 		 	if playerId ~= DataMgr.Instance.UserData.RoleID then
 		 		table.insert(allRole, playerId)
 		 	end
 		end

    	DataMgr.Instance.UserData.RoleSnapReader:GetMany(allRole, function(snaps)
      	-- print('snaps ------------------')
      		if cb then
        		local ret = CSharpArray2Table(snaps)
        		cb(ret,list)
      		end
    	end)
    end)
 
end


-- 主角路点
local function GetWayPointCvs(self)
	local roadPoint = self.wayPointCvsMap[self.roadPointIndex]
	if roadPoint then
		return roadPoint
	end
	roadPoint = self.roadPointCvs:Clone()
	self.mapCvs:AddChild(roadPoint)
	roadPoint.UserTag =self.roadPointIndex
	self.wayPointCvsMap[self.roadPointIndex] = roadPoint
	return roadPoint
end

--路过隐藏路点
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

-- 每帧显示当前位置
local function ShowActorPos(self,oldV3)
 
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

	 	roadPoint.Position2D = Vector2(posX * 1.6+40,posY * 1.6+40)
		roadPoint.Visible = true
		
		wpMap[posX] = wpMap[posX] or {}
		wpMap[posX][posY] = roadPoint 		
	end
end

local function ClearWayPoint(self)
	for k,v in pairs(self.wayPointCvsMap) do
		v:RemoveFromParent(true)
	end
	self.wayPointCvsMap = {}
end

local function OnEventAutoRun(self,eventname, params)
	-- print_r('OnEventAutoRun:',params)
	local IsRun = params.IsRun
	if not IsRun and params.MoveType == AutoMoveType._SmallMapTouch then
		self.aimCvs.Visible = false
		closeAll(self)
	else
		-- self.aimCvs.Visible = false
		-- HideAllWayPoint(self)
		
		local target = params.target
		if target == nil then
			return
		end
		
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

---

-- 队伍
local function HideTeamCvs(self )
	for k,v in pairs(self.temCvsMap) do
		v.Visible = false
	end
end 

local function GetTeamCvs(self,teamInfo)
	local teamCvs = self.temCvsMap[teamInfo.ID]
	if teamCvs then
		return teamCvs
	end 

	teamCvs = self.teamCvs:Clone()
	local nameLabel = UIUtil.FindChild(teamCvs,'lb_name')
	nameLabel.Text = teamInfo.Name
	self.mapCvs:AddChild(teamCvs)
	self.temCvsMap[teamInfo.ID] = teamCvs
	return teamCvs
end  

local function ShowTeamPlayerPos(self)
	-- body
	HideTeamCvs(self)

	local hasTeam = DataMgr.Instance.TeamData.HasTeam
	local teamCount = DataMgr.Instance.TeamData.MemberCount
	if not hasTeam  then
		return
	end

	local teamList =  DataMgr.Instance.UserData:GetTeamInfoList()
	if teamList == nil then
		return
	end
 
 	for teamInfo in Slua.iter(teamList) do
 		local cvs = GetTeamCvs(self,teamInfo)
	 	cvs.Position2D = Vector2(teamInfo.X * 1.6+40,teamInfo.Y * 1.6+40)
	 	cvs.Visible = true
 	end
end

-----------------------------------------------------------------

local function GetTransferData(self)
	local transferDatas = self.transferDataMap[self.TemplateID]
	if  transferDatas then
		return transferDatas
	end
	local data = MapModel.GetTransferData(self.TemplateID)
	local transferList = GameUtil.GetTransferData(data)
	transferDatas = {}
	for transferData in Slua.iter(transferList) do
	 	-- print('transferData:',transferData.name)
	 	table.insert(transferDatas,transferData)
 	end
 	self.transferDataMap[self.TemplateID] = transferDatas
 	return transferDatas
end


local function GetNpcData(self)
	-- body
	local npcDatas = self.npcDataMap[self.TemplateID]
	if  npcDatas then
		return npcDatas
	end

	local data = MapModel.GetNpcData(self.TemplateID)
	-- local npcList = DataMgr.Instance.UserData:GetNpcData(data)
	local npcList = GameUtil.GetNpcData(data)

	npcDatas = {}
	for npcData in Slua.iter(npcList) do
	 	-- print('npcData:',npcData.templateId)
	 	npcData.name = MapModel.GetNpcName(npcData.templateId) or npcData.name
	 	table.insert(npcDatas,npcData)
 	end

 	local tarnserDatas = GetTransferData(self)
 	for k,transferData in pairs(tarnserDatas) do
 		table.insert(npcDatas,transferData)
 	end

 	self.npcDataMap[self.TemplateID] = npcDatas
 	return npcDatas
end


local function GetMonsterData(self)
	-- body
	local monsterDatas = self.monsterDataMap[self.TemplateID]
	if  monsterDatas then
		return monsterDatas
	end

	local data = MapModel.GetMonsterData(self.TemplateID)
	-- local monsterList = DataMgr.Instance.UserData:GetMonsterData(data)
	local monsterList = GameUtil.GetMonsterData(data)
	monsterDatas = {}
	for monsterData in Slua.iter(monsterList) do
	 	table.insert(monsterDatas,monsterData)
	 	monsterData.level = MapModel.GetMonsterLevel(self.TemplateID,monsterData.templateId)
 	end
 	self.monsterDataMap[self.TemplateID] = monsterDatas
 	return monsterDatas
end  


local function HideMonster(self)
	for k,v in pairs(self.monsterCvsMap) do
		v.Visible = false
	end
end

local function HideNpc(self)
	for k,v in pairs(self.npcCvsMap) do
		v.Visible = false
	end
end

local function HideTransfer(self)
	for k,v in pairs(self.transferCvsMap) do
		v.Visible = false
	end
end

local function GetTransferCvs(self)
	-- body
	local transferCvs = self.transferCvsMap[self.transferIndex]
	if transferCvs then
		return transferCvs
	end
	transferCvs = self.transferCvs:Clone()
	self.mapCvs:AddChild(transferCvs)
	self.transferCvsMap[self.transferIndex] = transferCvs
	return transferCvs
end

local function GetMonsterCvs(self)
	-- body
	local monsterCvs = self.monsterCvsMap[self.monsterIndex]
	if monsterCvs then
		return monsterCvs
	end
	monsterCvs = self.monsterCvs:Clone()
	self.mapCvs:AddChild(monsterCvs)
	self.monsterCvsMap[self.monsterIndex] = monsterCvs
	return monsterCvs
end  

local function GetNpcCvs(self,npcData)
	local npc = self.npcCvsMap[npcData.templateId]
	if npc then
		return npc
	end
	local npcCvs = self.npcCvs:Clone()
	self.mapCvs:AddChild(npcCvs)
	local npc = SmallMapNpc(npcData.templateId,npcCvs)
	npc:SetPosition(npcData.X * 1.6+40,npcData.Y * 1.6+40)
	self.npcCvsMap[npcData.templateId] = npc
	return npc
end  


local function AddMonster(self,monsterData)
	self.monsterIndex = self.monsterIndex + 1
	local monsterCvs = GetMonsterCvs(self)
	monsterCvs.Visible = true
	-- print('AddMonster: ',monsterData.name,monsterData.X,monsterData.Y)
	monsterCvs.Position2D = Vector2(monsterData.X * 1.6+40,monsterData.Y * 1.6+40)
	local monsterNameLabel = UIUtil.FindChild(monsterCvs,'lb_name')
	monsterNameLabel.Text = monsterData.name
end


local function AddMonsters(self,datas)
	self.monsterIndex = 0
	for i=1,#datas do
 		local monsterData = datas[i]
 		AddMonster(self,monsterData)
	end
end

local function AddNpc(self,npcData)
	if npcData.templateId > 0 then
		-- self.npcIndex = self.npcIndex + 1
		local npcCvs = GetNpcCvs(self,npcData)
		npcCvs.Visible = true
		-- npcCvs.Position2D = Vector2(npcData.X * 2,npcData.Y * 2)
	end
end

local function AddNpcs(self,datas)
	self.npcIndex = 0
	for i=1,#datas do
 		local npcData = datas[i]
 		-- print('npc.templateId: ',npcData.templateId)
 		-- print('npc.name: ',npcData.name)
 		AddNpc(self,npcData)
	end
end

local function AddTransfer(self,transferData)
	self.transferIndex = self.transferIndex + 1
	local cvs = GetTransferCvs(self)
	cvs.Enable = false
	cvs.Visible = true
	cvs.Position2D = Vector2(transferData.X * 1.6+40,transferData.Y * 1.6+40)
	local nameLabel = UIUtil.FindChild(cvs,'lb_name')
	nameLabel.Text = Util.GetText(transferData.name)
end

local function AddTransfers(self,datas)

	for i=1,#datas do
 		local transferData = datas[i]
 		-- print('npc.name: ',transferData.name)
 		AddTransfer(self,transferData)
	end
end

local function ShowNpcItem(self,node,index,itemData)

	MenuBase.SetLabelText(node, 'lb_name', Util.GetText(itemData.name), 0, 0)
	local icon = UIUtil.FindChild(node,'ib_icon')
	UIUtil.SetImage(icon,itemData.Icon)
	node.TouchClick = function ( sender)
		-- body
		local npctempid = itemData.templateId
		if npctempid > 0 then
			AutoRunByNPC(self,npctempid)
		else
			--TODO 传送点点击
			AutoRunByXY(self,itemData.X,itemData.Y)
		end
	end
end

local function ShowMonsterItem(self,node,index,itemData)

	MenuBase.SetLabelText(node, 'lb_name', itemData.name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2',itemData.level), 0, 0)

	node.TouchClick = function (sender)
		-- body
		HideAllWayPoint(self)
		AutoRunByXY(self,itemData.X,itemData.Y)
	end
end 

local function OpenFuncMenu( self, type, pos, anchor, uuid, name, cb )
    local args = {}
    args.playerId = uuid
    args.playerName = name
    args.menuKey = type
    args.pos = pos
    args.anchor = anchor
    args.cb = cb
    EventManager.Fire("Event.InteractiveMenu.Show", args)
end

local function ShowNearPlayer(self,node,index,itemData)
 
 	MenuBase.SetLabelText(node, 'lb_name', itemData.Name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2',itemData.Level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_job', Util.GetText('pro_'..itemData.Pro), 0, 0)

	local gen_icon = node:FindChildByEditName('ib_sex', true)
 	UIUtil.SetImage(gen_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|gen_'..itemData.Gender)

	node.TouchClick = function ( ... )
		-- body
		OpenFuncMenu(self, 'stranger', nil, nil, itemData.ID, itemData.Name)
	end
end

local function showScrollPan(self,type)
 	
 	local tempnode
 	local datas

	local function eachupdatecb(node, index)
		-- print('eachupdatecb index:',index)
		if type == 1 then
 		 	ShowNpcItem(self,node,index,datas[index])
 		elseif type == 2 then
			ShowMonsterItem(self,node,index,datas[index])
 		else 
 		 	ShowNearPlayer(self,node,index,datas[index])
 		end
	end

 	if type == 1 then
 		HideMonster(self)
 		tempnode = self.npcTemp
 		datas = GetNpcData(self)
 		AddNpcs(self,datas)

 	elseif type == 2 then
 		HideNpc(self)
 		tempnode = self.monsterTemp
 		datas = GetMonsterData(self)
 		AddMonsters(self,datas)
 	else 
 		--比较特殊
 		tempnode = self.playerTemp
 		RequestNearPlayers(function(roleDatas,idlist)
 			-- body
 			datas = roleDatas
 			UIUtil.ConfigVScrollPan(self.pan,tempnode,#datas,eachupdatecb)
 		end)
 		return
 	end


	UIUtil.ConfigVScrollPan(self.pan,tempnode,#datas,eachupdatecb)
end


local function ClearMonster(self)
	for k,v in pairs(self.monsterCvsMap) do
		v:RemoveFromParent(true)
	end
	self.monsterCvsMap = {}
end

local function ClearNpc(self)
	for k,v in pairs(self.npcCvsMap) do
		v:Clear()
	end
	self.npcCvsMap = {}
end

local function ClearTransfer(self)
	for k,v in pairs(self.transferCvsMap) do
		v:RemoveFromParent(true)
	end
	self.transferCvsMap = {}
end



local function ClearTeamCvs(self )
	for k,v in pairs(self.temCvsMap) do
		v:RemoveFromParent(true)
	end
	self.temCvsMap = {}
end 




function _M.OnEnter( self, ...)
 
	SoundManager.Instance:PlaySoundByKey('uiopen',false)
	
	-- local TemplateID  =  self.ui.menu.ExtParam[1]
	self.TemplateID = DataMgr.Instance.UserData.MapTemplateId
	self.MapName  =  DataMgr.Instance.UserData.MapName
 
	local mapData = GlobalHooks.DB.FindFirst('MapData',{ id = self.TemplateID })

	local mMapImgPath = 'dynamic/map/' .. mapData.small_map .. ".png"
	UIUtil.SetImage(self.mapCvs,mMapImgPath,true)

	self.mapCvs.Position2D = Vector2((self.minimapCvs.Size2D.x-self.mapCvs.Size2D.x)*0.5,(self.minimapCvs.Size2D.y-self.mapCvs.Size2D.y)*0.5)
	-- local mMapImgPath = 'dynamic/map/' .. TemplateID .. ".png"
	-- UIUtil.SetImage(self.mapCvs,mMapImgPath,true)
 
	local oldV3 = self.smallActor.Transform.localEulerAngles
	
	
	local datas = GetTransferData(self)
	self.transferIndex = 0 
 	AddTransfers(self,datas)
	 
 	-- GetNpcData(self)
 	-- GetMonsterData(self)

 	local function TogFunc(sender)
		local tag = sender.Tag 
		showScrollPan(self,tag)
	end

 	UIUtil.ConfigToggleButton(self.tbts , self.togNpc, false, TogFunc)
	
	-- print('TemplateID:',TemplateID)
 
 	ShowActorPos(self,oldV3)

 	local index = 0
	self.timerId = LuaTimer.Add(0,264,function(id)

		ShowActorPos(self,oldV3)
		index = index + 1
		if (index % 3 == 0) then
			ShowTeamPlayerPos(self)
		end
		return true
	end)

	if hasTeam and teamCount > 1 then
		self.teamTimerId = LuaTimer.Add(0,1000,function(id)
			-- print('DataMgr.Instance.TeamData.MemberCount:',DataMgr.Instance.TeamData.MemberCount)
			return ShowTeamPlayerPos(self)
		end)
	end

	_M.eventFun = function( eventname, params )
		OnEventAutoRun(self, eventname, params)
	end

	EventManager.Subscribe("Event.AutoRun.RoadPoint", _M.eventFun)
end

function _M.OnExit( self )
	 
	if self.timerId ~= nil then
		LuaTimer.Delete(self.timerId)
		self.timerId = nil
	end

	if self.teamTimerId ~= nil then
		LuaTimer.Delete(self.teamTimerId)
		self.teamTimerId = nil
	end

	if self.aimCvs then
		self.aimCvs.Visible = false
	end
	
	HideAllWayPoint(self)
	-- clear的话每次打开重画 HideAllWayPoint的话缓存
	-- ClearWayPoint(self)

	ClearNpc(self)
	ClearMonster(self)
	ClearTransfer(self)
	ClearTeamCvs(self)
 
	EventManager.Unsubscribe("Event.AutoRun.RoadPoint", _M.eventFun)
end

function _M.OnDestory( self )
	ClearWayPoint(self)
	ClearNpc(self)
	ClearMonster(self)
	ClearTransfer(self)
	ClearTeamCvs(self)
end



function _M.OnInit( self )
 
	self.temCvsMap = {}
	self.npcCvsMap = {}
	self.monsterCvsMap = {}
	self.transferCvsMap = {}
	self.wayPointCvsMap = {}

	self.npcDataMap = {}
	self.monsterDataMap = {}
	self.transferDataMap = {}

	local tbts = {}
	local togNpc = self.ui.comps.tbn_npc
	self.togNpc = togNpc
  	togNpc.Tag = 1
	local togMonster = self.ui.comps.tbn_monster
	togMonster.Tag = 2
	local togPlayer = self.ui.comps.tbn_player
	togPlayer.Tag = 3
	table.insert(tbts,togNpc)
	table.insert(tbts,togMonster)
	table.insert(tbts,togPlayer)
	self.tbts = tbts

	self.nameLabel = self.ui.comps.lb_mapname


	self.smallActor = self.ui.comps.cvs_actor
	self.monsterCvs = self.ui.comps.cvs_monster
	self.monsterCvs.Visible = false
	self.tpCvs = self.ui.comps.cvs_tp
	self.tpCvs.Visible = false
	self.teamCvs = self.ui.comps.cvs_teammate
	self.teamCvs.Visible = false
	self.npcCvs = self.ui.comps.cvs_npc
	self.npcCvs.Visible = false
	self.transferCvs = self.ui.comps.cvs_tp
	self.transferCvs.Visible = false

	self.roadPointCvs = self.ui.comps.lb_xunludian
	self.roadPointCvs.Visible = false


	self.pan = self.ui.comps.sp_list
	self.npcTemp = self.ui.comps.cvs_npcinfo
	self.npcTemp.Visible = false
	self.monsterTemp = self.ui.comps.cvs_monsterinfo
	self.monsterTemp.Visible = false
	self.playerTemp = self.ui.comps.cvs_playerinfo
	self.playerTemp.Visible = false

	self.aimCvs = self.ui.comps.cvs_xunlumubiao
	self.aimCvs.Visible = false
	self.mapCvs = self.ui.comps.cvs_map
	self.minimapCvs = self.ui.comps.cvs_minimap

	local function MapTouchClick(self,touchPoint)
		AutoRunByXY(self,(touchPoint.x-40)/1.6, (touchPoint.y-40)/1.6)
	end

	self.mapCvs.event_PointerUp =  function (sender,e)
		-- body
		-- HideAllWayPoint(self)
		self.aimCvs.Visible = false
		
		local touchPoint = self.mapCvs:ScreenToLocalPoint2D(e)
		-- print('touchPoint:',touchPoint.x * 0.5,touchPoint.y * 0.5)
		MapTouchClick(self,touchPoint)
	
	end
end

return _M