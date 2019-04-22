local _M = {}
_M.__index = _M
local SocialModel = require 'Model/SocialModel'
local TeamModel = require 'Model/TeamModel'
local ChatModel = require 'Model/ChatModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local function RefreshCellData(self, node, index)
	local data = self.listData[index]
	MenuBase.SetLabelText(node, 'lb_lv', tostring(data.Level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.Name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_powernum', tostring(data.FightPower), 0, 0)
	local face_icon = node:FindChildByEditName('ib_target', true)
	local path = GameUtil.GetHeadIcon(data.Pro, data.Gender)
	UIUtil.SetImage(face_icon, path)
	MenuBase.SetVisibleUENode(node, 'btn_an1', self.inviteList[data.ID] ~= true)
	MenuBase.SetVisibleUENode(node, 'lb_invited', self.inviteList[data.ID] == true)
	local isInTeam = DataMgr.Instance.TeamData:IsTeamMember(data.ID)
	MenuBase.SetVisibleUENode(node, 'cvs_state', not isInTeam)
	MenuBase.SetVisibleUENode(node, 'ib_team', isInTeam)
	local btn = node:FindChildByEditName('btn_an1', true)
	btn.TouchClick = function(...)
	 	local resut = ChatModel.AddChatRole(data.ID,data.Name)
		if resut then
			EventManager.Fire('Event.Chat.AddPrivateChat',{roleId = data.ID, roleName = data.Name,byself = true})
		end
		self.ui:Close()
	end
end

 
local function RefreshList(self, listData)
	self.listData = listData
	local pan = self.ui.comps.sp_oar
	local cell = self.ui.comps.cvs_role
	cell.Visible = false
	UIUtil.ConfigVScrollPan(
		pan,
		cell,
		#self.listData,
		function(node, index)
			RefreshCellData(self, node, index)
		end
	)
end


-- local function OnNearButtonClick(self)
-- 	TLBattleScene.Instance.Actor:GetZonePlayersUUID(function(rsp)
-- 		local roleList = CSharpList2Table(rsp.b2c_list)
-- 		for i,v in ipairs(roleList) do
-- 			if v == DataMgr.Instance.UserData.RoleID then
-- 				table.remove(roleList, i)
-- 				break
-- 			end
-- 		end
-- 		Util.GetManyRoleSnap(roleList,function(roleDatas)
-- 			RefreshList(self, roleDatas)
-- 		end)
--     end)
-- end

local function OnFriendButtonClick(self)
	-- print('OnFriendButtonClick..')
	-- SocialModel.RequestClientGetFriendList(function (rsp)
	-- 	local friendList = rsp.s2c_data.friendList
	-- 	RefreshList(self,friendList)

	-- end)
	TeamModel.RequestFriendRoleList(
		function(rolelist)
			if not rolelist then
				RefreshList(self, {})
				return
			end
			for i, v in ipairs(rolelist) do
				if v == DataMgr.Instance.UserData.RoleID then
					table.remove(rolelist, i)
					break
				end
			end
			Util.GetManyRoleSnap(rolelist,function(roleDatas)
				RefreshList(self, roleDatas)
			end)
		end
	)
end

local function OnGuildButtonClick(self)
	-- print('OnGuildButtonClick..')
	-- GuildModel.ClientGuildMemListRequest(function (rsp)
	-- 	local guildList = rsp.s2c_members
	-- 	RefreshList(self,guildList)
	-- end)

	TeamModel.RequestGuildRoleList(
		function(rolelist)
			if not rolelist then
				RefreshList(self, {})
				return
			end
			for i, v in ipairs(rolelist) do
				if v == DataMgr.Instance.UserData.RoleID then
					table.remove(rolelist, i)
					break
				end
			end
			Util.GetManyRoleSnap(
				rolelist,
				function(roleDatas)
					RefreshList(self, roleDatas)
				end
			)
		end
	)

end

function _M.OnEnter( self )
	self.MessageType = DataMgr.Instance.TeamData:IsLeader() and AlertMessageType.TeamApply or AlertMessageType.TeamInvite
	self.inviteList = {}
	DataMgr.Instance.MsgData:RequestSendedMessage(self.MessageType,function(list) 
		if list then
			list = CSharpList2Table(list)
			for _,msg in ipairs(list) do
				self.inviteList[msg.TargetRoleID] = true
			end
		end
	end)

	UIUtil.ConfigToggleButton({self.ui.comps.tbt_an2, self.ui.comps.tbt_an3}, self.ui.comps.tbt_an2, false, function( sender )
		if sender == self.ui.comps.tbt_an2 then
			OnFriendButtonClick(self)
		elseif sender == self.ui.comps.tbt_an3 then
			OnGuildButtonClick(self)
		end
	end)
end

function _M.OnExit( self )
	
end

function _M.OnDestory( self )
	
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
    self.ui.comps.btn_close.TouchClick = function()
        self.ui:Close()
    end


end

return _M