local _M = {}
_M.__index = _M

local TeamModel = require 'Model/TeamModel'
local SocielModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

local function ReleaseAll3DModel(self)
	for _,v in pairs(self.model or {}) do
		RenderSystem.Instance:Unload(v)
	end
	
	self.model = nil
end

local function Init3DModel(self, parentCvs, avatar, scale)
	local t = {
		Parent = parentCvs.Transform,
		Pos = {},
		Scale = scale,
		LayerOrder = self.menu.MenuOrder,
		UILayer = true,
		Deg = {y = -180}
	}

	self.model = self.model or {}
	self.model[#self.model + 1] = Util.LoadGameUnit(avatar, t)
end

local function OpenFuncMenu(self, pos, anchor, uuid, name, isLeader)
	local menuType
	if uuid == DataMgr.Instance.UserData.RoleID then
		menuType = 'team_self'
	else
		if isLeader then
			menuType = 'teamlead'
		else
			menuType = 'teammate'
		end
	end
	local args = {}
	args.playerId = uuid
	args.playerName = name
	args.menuKey = menuType
	args.pos = pos
	args.anchor = anchor
	EventManager.Fire('Event.InteractiveMenu.Show', args)
end

local function RefreshRedPoint(self)
	if DataMgr.Instance.TeamData.HasTeam then
		self.ui.comps.ib_dian.Visible = DataMgr.Instance.MsgData:GetMessageCount(AlertMessageType.TeamApply) > 0
	else
		self.ui.comps.ib_dian.Visible = DataMgr.Instance.MsgData:GetMessageCount(AlertMessageType.TeamInvite) > 0
	end
end

local function CreateTeamList(self)
	local members = DataMgr.Instance.TeamData.AllMembers
	self.listData = {}
	self.memberIds = {}
	if members then
		for i = 0, members.Count - 1 do
			local m = members:getItem(i)
			self.listData[i + 1] = {merber = m, snap = Util.GetCachedRoleSnap(m.RoleID)}
			table.insert(self.memberIds, m.RoleID)
		end
	end
	-- print_r('listData', self.listData)
end

local function RefreshRelationInfo( self, data )
	local maxRelationLv = 0
	local cav_team = self.ui.comps.cav_team
	for i = 1, cav_team.NumChildren do
		local showMember = i <= #self.listData
		local cell = self.ui.comps['cvs_role' .. i]
		if showMember then
			local m = self.listData[i].merber
			if data[m.RoleID] then
				MenuBase.SetVisibleUENode(cell, 'cvs_relat'..i, data[m.RoleID] > 0)
				MenuBase.SetLabelText(cell, 'lb_relatlv'..i, data[m.RoleID], 0, 0)
				maxRelationLv = data[m.RoleID] > maxRelationLv and data[m.RoleID] or maxRelationLv
			else
				MenuBase.SetVisibleUENode(cell, 'cvs_relat'..i, false)
			end
		else
			MenuBase.SetVisibleUENode(cell, 'cvs_relat'..i, false)
		end
	end

	--属性
	if maxRelationLv > 0 then
		self.ui.comps.cvs_attribute.Visible = true
		self.ui.comps.lb_noadd.Visible = false
		local dbRelation = GlobalHooks.DB.FindFirst('relationship', { relat_lv = maxRelationLv })
	    local attrs = ItemModel.GetXlsFixedAttribute(dbRelation)
		for i = 1, 4 do
			local cvs = self.ui.comps['cvs_attri'..i]
		    local attrName, value = ItemModel.GetAttributeString(attrs[i])
			if i <= #attrs then
				cvs.Visible = true
				local lb_name = cvs:FindChildByEditName('lb_add'..i, true)
				lb_name.Text = attrName
				local lb_num = cvs:FindChildByEditName('lb_num'..i, true)
				lb_num.Text = '+'..(value / 100)..'%'
			else
				cvs.Visible = false
			end
		end
	else
		self.ui.comps.cvs_attribute.Visible = false
		self.ui.comps.lb_noadd.Visible = true
	end
end

local function RefreshList(self)
	print('--------------Refresh Team List--------------')
	local hasTeam = DataMgr.Instance.TeamData.HasTeam
	local isLeader = DataMgr.Instance.TeamData:IsLeader()
	self.ui.comps.tbt_follow.IsChecked = DataMgr.Instance.TeamData.IsFollowLeader
	self.ui.comps.cvs_btnlist.Visible = not hasTeam
	self.ui.comps.cvs_btnlist1.Visible = hasTeam

	local targetID = DataMgr.Instance.TeamData.Setting.TargetID
	local target = GlobalHooks.DB.Find('team_target', targetID)
	self.comps.btn_taget.Text = Constants.Text.team_target_desc .. Util.GetText(target.target_name)

	CreateTeamList(self)
	RefreshRedPoint(self)
	ReleaseAll3DModel(self)
	local cav_team = self.ui.comps.cav_team

	for i = 1, cav_team.NumChildren do
		local showMember = i <= #self.listData
		local cell = self.ui.comps['cvs_role' .. i]
		MenuBase.SetVisibleUENode(cell, 'cvs_playerinfo', showMember)
		MenuBase.SetVisibleUENode(cell, 'cvs_addrole', hasTeam and not showMember and isLeader)
		if showMember then
			local data = self.listData[i].snap
			local m = self.listData[i].merber
			print('snap name', data.Name)
			local memberIsLeader = DataMgr.Instance.TeamData:IsLeader(data.ID)
			MenuBase.SetVisibleUENode(cell, 'ib_captain', memberIsLeader)

			local fightStr = string.format(Util.GetText('team_fight'), data.FightPower)
			MenuBase.SetLabelText(cell, 'lb_power', fightStr, 0, 0)
			MenuBase.SetLabelText(cell, 'lb_name', tostring(data.Name), 0, 0)
			MenuBase.SetLabelText(cell, 'lb_lv', tostring(data.Level), 0, 0)
			local pro_icon = cell:FindChildByEditName('ib_icon', true)
			UIUtil.SetImage(pro_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|pro_' .. data.Pro)
			local modelAnchor = cell:FindChildByEditName('cvs_anchor', true)

			local avatar_map = {}
			for _, v in ipairs(data.Avatar) do
				if v.PartTag ~= Constants.AvatarPart.Ride_Avatar01 then
					avatar_map[v.PartTag] = v.FileName
				end
			end
			Init3DModel(self, modelAnchor, avatar_map, 130)
			local cvs_playerInfo = cell:FindChildByEditName('cvs_playerinfo', true)
			local ib_follow = UIUtil.FindChild(cell, 'ib_follow')
			ib_follow.Visible = m.IsFollowLeader
			cvs_playerInfo.TouchClick = function(...)
				local lPos = self.ui.menu:LocalToUIGlobal(cell)
				local posx = i <= 2 and lPos.x + cell.Width or lPos.x
				local anchorx = i <= 2 and 0 or 1
				OpenFuncMenu(self, {x = posx}, {x = anchorx}, data.ID, data.Name, isLeader)
			end
		else
			local inviteBtn = cell:FindChildByEditName('btn_add', true)
			inviteBtn.TouchClick = function(sender)
				GlobalHooks.UI.OpenUI('TeamInvite', 0)
			end
		end
	end

	--请求亲密度列表
	SocielModel.RequestClientGetRelationDataMuti(self.memberIds, function( rsp )
		RefreshRelationInfo(self, rsp.data)
	end)
end

function _M.Notify(status, flag, self, opt)
	if flag == DataMgr.Instance.MsgData then
		RefreshRedPoint(self)
	else
		self.comps.btn_auto.Visible = not DataMgr.Instance.TeamData.IsInMatch
		print('Notify ------------------', status, flag)
		local members = DataMgr.Instance.TeamData.AllMembers
		local allRoles = {}
		if members then
			for i = 0, members.Count - 1 do
				local m = members:getItem(i)
				table.insert(allRoles, m.RoleID)
			end
		end
		Util.GetManyRoleSnap(
			allRoles,
			function()
				RefreshList(self)
			end
		)
	end
end

function _M.OnDestory(self)
end

function _M.OnExit(self)
	ReleaseAll3DModel(self)
	-- 取消注册组队数据监听
	DataMgr.Instance.TeamData:DetachLuaObserver('TeamInfo')
	DataMgr.Instance.MsgData:DetachLuaObserver('TeamInfo')
end

function _M.OnEnter(self)
	Util.GetRoleSnap(
		DataMgr.Instance.UserData.RoleID,
		function()
			RefreshList(self)
		end
	)
	self.ui.comps.btn_auto.Visible = not DataMgr.Instance.TeamData.IsInMatch
	-- 注册组队数据监听
	DataMgr.Instance.TeamData:AttachLuaObserver('TeamInfo', self)
	DataMgr.Instance.MsgData:AttachLuaObserver('TeamInfo', self)

	self.ui.menu:SetUILayer(self.ui.comps.cvs_relatattri, self.ui.menu.MenuOrder + UILayerMgr.MenuOrderSpace, -600)
end

function _M.OnInit(self)
	self.ui.comps.btn_quick.TouchClick = function()
		-- body
	end
	self.ui.comps.btn_create.TouchClick = function()
		TeamModel.RequestCreateTeam()
	end
	self.ui.comps.btn_leave.TouchClick = function()
		DataMgr.Instance.TeamData:RequestLeaveTeam(
			function()
			end
		)
	end
	self.ui.comps.tbt_follow.TouchClick = function()
		local checked = self.ui.comps.tbt_follow.IsChecked
		self.ui.comps.tbt_follow.IsChecked = not checked
		DataMgr.Instance.TeamData:RequestFollowLeader(
			checked,
			function()
			end
		)
	end
	self.ui.comps.btn_apply.TouchClick = function()
		if not DataMgr.Instance.TeamData.HasTeam or DataMgr.Instance.TeamData:IsLeader() then
			GlobalHooks.UI.OpenUI('TeamApply', 0)
		else
			GameAlertManager.Instance:ShowNotify(Util.GetText('team_no_authority'))
		end
	end
	self.ui.comps.btn_auto.TouchClick = function()
		print('auto touchclick',DataMgr.Instance.TeamData.Setting.TargetID)
		TeamModel.RequestAutoMatch(DataMgr.Instance.TeamData.Setting.TargetID)
	end

	self.comps.btn_hanhua.TouchClick = function()
		if not DataMgr.Instance.TeamData.HasTeam then
			GameAlertManager.Instance:ShowNotify(Constants.Text.team_warnNoTeam)
		elseif not DataMgr.Instance.TeamData:IsLeader() then
			GameAlertManager.Instance:ShowNotify(Constants.Text.team_warnNotLeader)
		else
			local targetID = DataMgr.Instance.TeamData.Setting.TargetID
			local target = GlobalHooks.DB.Find('team_target', targetID)
			local ChatUtil = require 'UI/Chat/ChatUtil'

			local showData = Util.GetText('team_yelling', Util.GetText(target.target_name), target.lv_min, target.lv_max)
			ChatUtil.TeamShout(showData)
		end
	end
	self.comps.btn_taget.TouchClick = function()
		GlobalHooks.UI.OpenUI('TeamTarget')
	end

	self.comps.btn_relatadd.TouchClick = function( )
		self.comps.cvs_relatattri.Visible = true
	end
    self.ui.comps.cvs_relatattri.event_PointerUp = function( ... )
    	self.ui.comps.cvs_relatattri.Visible = false
    end
end

_M.RefreshList = RefreshList
return _M
