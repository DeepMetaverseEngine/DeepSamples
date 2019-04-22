local _M = {}
_M.__index = _M

local TeamModel = require 'Model/TeamModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local function RefreshCellData(self, node, index)
	local msg = self.listData[index]
	node.Visible = false
	Util.GetRoleSnap(
		msg.FromRoleID,
		function(data)
			node.Visible = true
			MenuBase.SetLabelText(node, 'lb_lv', tostring(data.Level), 0, 0)
			MenuBase.SetLabelText(node, 'lb_name', data.Name, 0, 0)
			MenuBase.SetLabelText(node, 'lb_powernum', tostring(data.FightPower), 0, 0)
			local face_icon = node:FindChildByEditName('ib_target', true)

			if data.Options then
				local photoname = data.Options['Photo0']
				if not string.IsNullOrEmpty(photoname) then
					local SocielModel = require 'Model/SocialModel'
					SocielModel.SetHeadIcon(
						data.ID,
						photoname,
						function(UnitImg)
							if not face_icon.IsDispose then
								UIUtil.SetImage(face_icon, UnitImg, false, UILayoutStyle.IMAGE_STYLE_BACK_4)
							end
						end
					)
				else
					local path = GameUtil.GetHeadIcon(data.Pro, data.Gender)
					UIUtil.SetImage(face_icon, path)
				end
			end

			local pro_icon = node:FindChildByEditName('ib_job', true)
			UIUtil.SetImage(pro_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|pro_' .. data.Pro)
		end
	)

	local btn = node:FindChildByEditName('btn_an1', true)
	btn.TouchClick = function(...)
		local targetID
		if DataMgr.Instance.TeamData:IsLeader() and DataMgr.Instance.TeamData.IsInMatch then
			targetID = DataMgr.Instance.TeamData.Setting.TargetID
		end
		DataMgr.Instance.MsgData:RequestMessageResult(
			msg.Id,
			1,
			function(rsp)
				DataMgr.Instance.MsgData:RemoveMessage(msg.Id, msg.Type)
				MenuBase.SetVisibleUENode(node, 'btn_an1', false)
				MenuBase.SetVisibleUENode(node, 'btn_an2', false)
				MenuBase.SetVisibleUENode(node, 'lb_agree', true)
				self.ui:Close()
				if targetID then
					UnityHelper.WaitForSeconds(
						2,
						function()
							TeamModel.RequestAutoMatch(targetID)
						end
					)
				end
			end
		)
	end
	local btn = node:FindChildByEditName('btn_an2', true)
	btn.TouchClick = function(...)
		DataMgr.Instance.MsgData:RequestMessageResult(
			msg.Id,
			0,
			function(rsp)
				DataMgr.Instance.MsgData:RemoveMessage(msg.Id, msg.Type)
				MenuBase.SetVisibleUENode(node, 'btn_an1', false)
				MenuBase.SetVisibleUENode(node, 'btn_an2', false)
				MenuBase.SetVisibleUENode(node, 'lb_agree', false)
				self.ui:Close()
			end
		)
	end
end

local function CreateListData(self)
	self.listData = {}
	self.MessageType = DataMgr.Instance.TeamData:IsLeader() and AlertMessageType.TeamApply or AlertMessageType.TeamInvite
	local all = DataMgr.Instance.MsgData:GetAlertMessageByType(self.MessageType)
	if all then
		for i = 0, all.Count - 1 do
			local m = all:getItem(i)
			self.listData[i + 1] = m
		end
	end
end

local function RefreshList(self)
	CreateListData(self)
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

function _M.OnEnter(self)
	RefreshList(self)
end

function _M.OnExit(self)
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.comps.btn_close.TouchClick = function()
		self.ui:Close()
	end

	self.ui.comps.btn_clean.TouchClick = function(...)
		if self.listData and #self.listData > 0 then
			DataMgr.Instance.MsgData:RemoveList(self.MessageType)
			RefreshList(self)
		end
	end
end

return _M
