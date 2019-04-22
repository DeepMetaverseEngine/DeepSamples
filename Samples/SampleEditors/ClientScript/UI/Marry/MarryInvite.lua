local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'


local function RefreshFriendCellData( self, node, index )
	local data = self.friendList[index]
	local isSelected = false
	for i = 1, #self.friends do
		if data.roleId == self.friends[i].Name then
			isSelected = true
			break
		end
	end

	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	local tbt = node:FindChildByEditName('tbt_choose', true)
	tbt.Enable = not isSelected
	tbt.IsChecked = isSelected or self.selList[data.roleId] ~= nil
	tbt.TouchClick = function( sender )
		if tbt.IsChecked then
			if #self.friends + table.len(self.selList) < GlobalHooks.DB.GetGlobalConfig('wedding_invitation_num') then
				self.selList[data.roleId] = sender.IsChecked and data.roleId or nil
				self.ui.comps.lb_choosed.Text = string.format('%d/%d', #self.friends + table.len(self.selList), GlobalHooks.DB.GetGlobalConfig('wedding_invitation_num'))
			else
				tbt.IsChecked = false
			end
		else
			self.selList[data.roleId] = sender.IsChecked and data.roleId or nil
			self.ui.comps.lb_choosed.Text = string.format('%d/%d', #self.friends + table.len(self.selList), GlobalHooks.DB.GetGlobalConfig('wedding_invitation_num'))
		end
	end
end

function _M.RefreshFriendList( self )
	SocialModel.RequestClientGetFriendList(function(rsp)
		self.friendList = rsp.s2c_data.friendList
		self.ui.comps.lb_choosed.Text = string.format('%d/%d', #self.friends + table.len(self.selList), GlobalHooks.DB.GetGlobalConfig('wedding_invitation_num'))

		table.sort(self.friendList, function( a, b )
			-- if a.leaveTime == System.DateTime.MaxValue and b.leaveTime == System.DateTime.MaxValue then
				if a.relationLv == b.relationLv then
					return (a.relationExp - b.relationExp) > 0
				else
					return (a.relationLv - b.relationLv) > 0
				end
			-- else
			-- 	return (a.leaveTime - b.leaveTime).TotalSeconds > 0
			-- end
		end)

		for i = 1, #self.friendList do
			if self.friendList[i].roleId == DataMgr.Instance.UserData.SpouseId then
				table.remove(self.friendList, i)
				break
			end
		end

		self.pan = self.ui.comps.sp_list
		local cell = self.ui.comps.cvs_firendlist
		cell.Visible = false
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.friendList, function(node, index)
			RefreshFriendCellData(self, node, index)
		end)
	end)
end

function _M.OnEnter( self , args )
	self.selList = {}
	self.slotIndex = args.slotIndex
	self.friends = args.Friends
	-- print_r('eeeeeeeeeeeeeeee', args)
	_M.RefreshFriendList( self )
	self.comps.lb_groomname.Text = args.Husband
	self.comps.lb_bridename.Text = args.Wife
	local date = System.DateTime.Parse(args.Date1)
	local dateStr = Util.GetText('marry_day', date.Year, date.Month, date.Day)
	local timeStr = Util.GetText('marry_time'..args.Times, date.Hour)
	self.comps.tb_info.Text = Util.GetText('wedding_text', dateStr..timeStr)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
	-- self.ui.menu.ShowType = UIShowType.Cover

	self.ui.comps.btn_send.TouchClick = function( ... )
		SocialModel.RequestClientSendInvitation(self.slotIndex, self.selList, function( ... )
		end)
		self.ui:Close()
	end
end

return _M