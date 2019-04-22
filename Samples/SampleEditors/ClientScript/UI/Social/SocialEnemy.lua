local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local func = {}

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

------------------------- 黑名单列表 -----------------------------
local function RefreshBlackCellData( self, node, index )
	local data = self.blackList[index]
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	local guildName = string.IsNullOrEmpty(data.guildName) and Util.GetText('NoGuild') or data.guildName
	MenuBase.SetLabelText(node, 'lb_guild',guildName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_practice', GameUtil.GetPracticeName(data.practiceLv, data.fightPower), 0, 0)
	
	local gen_icon = node:FindChildByEditName('ib_sex', true)
	UIUtil.SetImage(gen_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|gen_'..data.gender)
	local face_icon = node:FindChildByEditName('ib_icon', true)
	local curRoleId = data.roleId
	Util.GetRoleSnap(data.roleId, function( roleSnap )
		local photoname = roleSnap.Options['Photo0']
		if not string.IsNullOrEmpty(photoname) then
			SocialModel.SetHeadIcon(data.roleId, photoname, function(UnitImg)
				if not face_icon.IsDispose and curRoleId == data.roleId then
                    UIUtil.SetImage(face_icon,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
				end
			end)
		else
			UIUtil.SetImage(face_icon, 'static/target/'..data.pro..'_'..data.gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	end)

	local pro_icon = node:FindChildByEditName('ib_job', true)
	UIUtil.SetImage(pro_icon, GameUtil.GetProIcon(data.pro))

	local bt_delete = node:FindChildByEditName('bt_delete', true)
	bt_delete.TouchClick = function( ... )
		SocialModel.RequestClientRemoveBlackList(data.roleId, function( ... )
			-- show success
			func.RefreshBlackList(self)
		end)
	end
	node.TouchClick = function( ... )
		OpenFuncMenu(self, "blacklist", nil, nil, data.roleId, data.roleName, function( ... )
			func.RefreshBlackList(self)
		end)
	end
end

function func.RefreshBlackList( self )
	SocialModel.RequestClientGetBlackList(function(rsp)
		self.comps.cvs_blacklist.Visible = true
		self.blackList = rsp.s2c_data.blackList
		self.ui.comps.lb_num3.Text = string.format('%d/%d', rsp.s2c_data.blackCount, rsp.s2c_data.blackMax)
		local pan = self.ui.comps.sp_friendlist2
		local cell = self.ui.comps.cvs_player2
		UIUtil.ConfigVScrollPan(pan, cell, #self.blackList, function(node, index)
			RefreshBlackCellData(self, node, index)
		end)
	end)

	self.ui.comps.bt_alldelete.TouchClick = function( ... )
		SocialModel.RequestClientRemoveBlackList('', function( ... )
			-- show success
			func.RefreshBlackList(self)
		end)
	end
end
------------------------- 黑名单列表 -----------------------------


------------------------- 仇人列表 -----------------------------
local function RefreshEnemyCellData( self, node, index )
	local data = self.enemyList[index]
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	local guildName = string.IsNullOrEmpty(data.guildName) and Util.GetText('NoGuild') or data.guildName
	MenuBase.SetLabelText(node, 'lb_guild', guildName, 0, 0)
	MenuBase.SetVisibleUENode(node, 'lb_enemytype', data.deepHatred)
	MenuBase.SetLabelText(node, 'lb_practice', GameUtil.GetPracticeName(data.practiceLv, data.fightPower), 0, 0)

	local gen_icon = node:FindChildByEditName('ib_sex', true)
	UIUtil.SetImage(gen_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|gen_'..data.gender)
	local face_icon = node:FindChildByEditName('ib_icon', true)
	local curRoleId = data.roleId
	Util.GetRoleSnap(data.roleId, function( roleSnap )
		local photoname = roleSnap.Options['Photo0']
		if not string.IsNullOrEmpty(photoname) then
			SocialModel.SetHeadIcon(data.roleId, photoname, function(UnitImg)
				if not face_icon.IsDispose and curRoleId == data.roleId then
                    UIUtil.SetImage(face_icon,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
				end
			end)
		else
			UIUtil.SetImage(face_icon, 'static/target/'..data.pro..'_'..data.gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	end)

	local pro_icon = node:FindChildByEditName('ib_job', true)
	UIUtil.SetImage(pro_icon, GameUtil.GetProIcon(data.pro))

	local tbt_deep = node:FindChildByEditName('tbt_enemy', true)
	tbt_deep.IsChecked = data.deepHatred
	tbt_deep.TouchClick = function( ... )
		SocialModel.RequestClientSetDeepHatred(data.roleId, tbt_deep.IsChecked, function( ... )
			data.deepHatred = tbt_deep.IsChecked
			MenuBase.SetVisibleUENode(node, 'lb_enemytype', data.deepHatred)
			self.deepCount = data.deepHatred and self.deepCount + 1 or self.deepCount - 1
			self.ui.comps.lb_num2.Text = string.format('%d/%d', self.deepCount, self.deepMax)
			GameAlertManager.Instance:ShowNotify(Util.GetText(tbt_deep.IsChecked and 'friend_enemy_set' or 'friend_enemy_cancel'))
		end)
	end
	node.TouchClick = function( ... )
		OpenFuncMenu(self, "enemy", nil, nil, data.roleId, data.roleName, function( ... )
			func.RefreshEmenyList(self)
		end)
	end
end

function func.RefreshEmenyList( self )
	SocialModel.RequestClientGetEnemyList(function(rsp)
		self.comps.cvs_enemy.Visible = true
		self.enemyList = rsp.s2c_data.enemyList
		self.deepCount = rsp.s2c_data.deepCount
		self.deepMax = rsp.s2c_data.deepMax
		self.ui.comps.lb_num1.Text = string.format('%d/%d', rsp.s2c_data.enemyCount, rsp.s2c_data.enemyMax)
		self.ui.comps.lb_num2.Text = string.format('%d/%d', self.deepCount, self.deepMax)
		local pan = self.ui.comps.sp_friendlist1
		local cell = self.ui.comps.cvs_player1
		UIUtil.ConfigVScrollPan(pan, cell, #self.enemyList, function(node, index)
			RefreshEnemyCellData(self, node, index)
		end)
		self.ui.comps.cvs_nothing.Visible = #self.enemyList == 0
	end)
end
------------------------- 仇人列表 -----------------------------

local function SwitchList( self, tag )
	self.comps.cvs_enemy.Visible = false
	self.comps.cvs_blacklist.Visible = false
	if tag == 0 then
		func.RefreshEmenyList(self)
	elseif tag == 1 then
		func.RefreshBlackList(self)
	end
end

function _M.OnEnter( self )
    local tbts = {
        self.comps.tbt_enemylist,
        self.comps.tbt_blacklist
    }
    UIUtil.ConfigToggleButton(tbts, self.comps.tbt_enemylist, false,
        function(sender)
            if sender.IsChecked and self.ui.IsRunning then
                SwitchList(self, sender.UserTag)
            end
        end)
end

function _M.OnExit( self )
	
end

function _M.OnDestory( self )
	
end

function _M.OnInit( self )
	self.ui.comps.cvs_player1.Visible = false
	self.ui.comps.cvs_player2.Visible = false
end

return _M