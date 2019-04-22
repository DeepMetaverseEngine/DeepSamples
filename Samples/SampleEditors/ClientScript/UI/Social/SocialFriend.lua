local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local ItemModel = require 'Model/ItemModel'

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


------------------------- 亲密度列表 -----------------------------
local function ShowGiftRecord( self, isShow )
	local function RefreshRecordCellData( node, data )
		local presenter = string.IsNullOrEmpty(data.presenter) and Util.GetText('relationship_me') or data.presenter
		local receiver = string.IsNullOrEmpty(data.receiver) and Util.GetText('relationship_me') or data.receiver
		local giftDb = GlobalHooks.DB.FindFirst('relationItemData', { item_id = data.templateId })
		local itemName = Util.GetText(giftDb.item_name)
		local text = Util.GetText('relationship_gift', presenter, receiver, itemName, data.num)
		MenuBase.SetLabelText(node, 'lb_text1', text, 0, 0)
		--日期
		local localTime = data.time:ToLocalTime()
		local date = Util.Format1234('{0}/{1}/{2}', localTime.Year, localTime.Month, localTime.Day)
		MenuBase.SetLabelText(node, 'lb_recordtime', date, 0, 0)
	end

	self.ui.comps.cvs_record.Visible = isShow

	if isShow then
		local pan = self.ui.comps.sp_list
		local cell = self.ui.comps.cvs_recordlist
		UIUtil.ConfigVScrollPan(pan, cell, #self.recordList, function(node, index)
			RefreshRecordCellData(node, self.recordList[index])
		end)
	end
end

local function RequestRelationUp( self, roleId, itemId, num )
	SocialModel.RequestClientRelationUp(roleId, itemId, num, function(rsp)
		self.rltLastPos = self.giftPan.Scrollable.Container.Position2D
		func.RefreshRelationList(self)

    	local giftDb = GlobalHooks.DB.FindFirst('relationItemData', { item_id = itemId })
    	if giftDb.effect_type == 1 then
    		for i = 1, num do
        		SocialModel.WaitToPlayRelationEffect(itemId)
    		end
    	end
	end)
end

local function InitGiftCellData( self, node, data, roleId )
	local itemId = data.item_id
	local detail = ItemModel.GetDetailByTemplateID(itemId)
    local count = ItemModel.CountItemByTemplateID(itemId)
	local cost = {id = itemId, detail = detail, cur = count, need = 1 }
	local cvs_item = node:FindChildByEditName('cvs_gifticon1', true)
	local lb_havenum1 = node:FindChildByEditName('lb_havenum1', true)
	UIUtil.SetEnoughItemShowAndLabel(self, cvs_item, lb_havenum1, cost, { onlycur = true })

	local lb_rltNum = node:FindChildByEditName('lb_relatnum1', true)
	lb_rltNum.Text = '+'..data.item_exp
	MenuBase.SetLabelText(node, 'lb_itemname', Util.GetText(data.item_name), 0, 0)

	--赠送按钮
	local btn_give = node:FindChildByEditName('btn_give', true)
	btn_give.TouchClick = function( ... )
		count = ItemModel.CountItemByTemplateID(itemId)
		if count == 0 then
			FunctionUtil.Goto('social_buygift_'..data.id)
		elseif count == 1 then
			RequestRelationUp(self, roleId, itemId, 1)
		else
			GlobalHooks.UI.OpenUI('SocialGiftSelect', 0, detail, count, function( num )
				RequestRelationUp(self, roleId, itemId, num)
			end)
		end
	end
end

local function RefreshRelationInfo( self, data )
	local dbRelation = GlobalHooks.DB.FindFirst('relationship', { relat_lv = data.relationLv })

	--进度条
	if dbRelation.lv_num == 0 then --满级
		self.ui.comps.lb_lvmax.Visible = true
		self.ui.comps.cvs_nowandnext.Visible = false
	else
		self.ui.comps.lb_lvmax.Visible = false
		self.ui.comps.cvs_nowandnext.Visible = true
		local dbNext = GlobalHooks.DB.FindFirst('relationship', { relat_lv = data.relationLv + 1 })
		self.ui.comps.lb_now.Text = Util.GetText('LookPlayerInfo_Level', data.relationLv)..Util.GetText(dbRelation.name)
		self.ui.comps.lb_next.Text = Util.GetText('LookPlayerInfo_Level', data.relationLv + 1)..Util.GetText(dbNext.name)
		local gg_rate = self.ui.comps.gg_rate
		gg_rate:SetGaugeMinMax(0, dbRelation.lv_num)
		gg_rate.Value = data.relationExp > gg_rate.GaugeMaxValue and gg_rate.GaugeMaxValue or data.relationExp
		gg_rate.Text = gg_rate.Value..'/'..dbRelation.lv_num
	end

	--属性
    local attrs = ItemModel.GetXlsFixedAttribute(dbRelation)
	for i = 1, 4 do
		local cvs = self.ui.comps['cvs_attri'..i]
	    local attrName, value = ItemModel.GetAttributeString(attrs[i])
		if i <= #attrs then
			cvs.Visible = true
			local lb_name = cvs:FindChildByEditName('lb_add'..i, true)
			lb_name.Text = attrName
			local lb_attrinum = cvs:FindChildByEditName('lb_attrinum'..i, true)
			lb_attrinum.Text = '+'..(value / 100)..'%'
		else
			cvs.Visible = false
		end
	end

	--礼物列表
	local dbGift = GlobalHooks.DB.Find('relationItemData', {})
	local pan = self.ui.comps.sp_giftlist
	local cell = self.ui.comps.cvs_giftinfo
	UIUtil.ConfigHScrollPan(pan, cell, #dbGift, function(node, index)
		InitGiftCellData(self, node, dbGift[index], data.roleId)
	end)
end

local function RefreshRelationCellData( self, node, index )
	local data = self.relationList[index]
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_relatlv2', data.relationLv, 0, 0)
    if data.leaveTime == System.DateTime.MaxValue then
		MenuBase.SetLabelText(node, 'lb_giftonline', Util.GetText('common_online'), GameUtil.RGB2Color(0x6ed417))
	else
		MenuBase.SetLabelText(node, 'lb_giftonline', Util.GetText('common_offline'), GameUtil.RGB2Color(0x999898))
	end

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

	local tbt_choose = node:FindChildByEditName('tbt_choose', true)
	tbt_choose.IsChecked = self.rltRoleSel == index
	tbt_choose.TouchClick = function( ... )
		self.rltRoleSel = index
		self.giftPan:RefreshShowCell()
	end
	if self.rltRoleSel == index then
		RefreshRelationInfo(self, data)
	end
end

function func.RefreshRelationList( self )
	SocialModel.RequestClientGetRelationData(function(rsp)
		self.comps.cvs_gift.Visible = true
		self.relationList = rsp.s2c_data.friendList
		self.recordList = rsp.s2c_data.recordList
		self.ui.comps.lb_num1.Text = string.format('%d/%d', rsp.s2c_data.friendCount, rsp.s2c_data.friendMax)

		if table.len(self.recordList) > 0 then
			local recordList = {}
			for k, v in pairs(self.recordList) do
				table.insert(recordList, v)
			end
			table.sort(recordList, function( a, b )
				return a.time > b.time
			end)
			self.recordList = recordList
		end

		if #self.relationList > 0 then
			self.comps.cvs_progress.Visible = true
			self.comps.cvs_presentgift.Visible = true
			self.comps.lb_nofriend.Visible = false
			table.sort(self.relationList, function( a, b )
				if a.leaveTime == System.DateTime.MaxValue and b.leaveTime == System.DateTime.MaxValue then
					if a.relationLv == b.relationLv then
						return (a.relationExp - b.relationExp) > 0
					else
						return (a.relationLv - b.relationLv) > 0
					end
				else
					return (a.leaveTime - b.leaveTime).TotalSeconds > 0
				end
			end)

			--如果有默认选项，先查找
			local defaultSel = nil
			if self.sub == 4 and self.defPlayerId then
				for i = 1, #self.relationList do
					if self.defPlayerId == self.relationList[i].roleId then
						defaultSel = i
						break
					end
				end
				self.defPlayerId = nil
				if defaultSel == nil then
					GameAlertManager.Instance:ShowNotify('not friend')
				else
					self.rltRoleSel = defaultSel
				end
			end

			self.giftPan = self.ui.comps.sp_friendlist4
			local cell = self.ui.comps.cvs_player4
			UIUtil.ConfigVScrollPan(self.giftPan, cell, #self.relationList, function(node, index)
				RefreshRelationCellData(self, node, index)
			end)
			if defaultSel then
				UIUtil.MoveToScrollCell(self.giftPan, defaultSel)
			end
			if self.rltLastPos then
				self.giftPan.Scrollable:LookAt(-self.rltLastPos)
			end
		else
			self.comps.cvs_progress.Visible = false
			self.comps.cvs_presentgift.Visible = false
			self.comps.lb_nofriend.Visible = true
		end
	end)
end
------------------------- 亲密度列表 -----------------------------

------------------------- 推荐列表 -----------------------------
local function RefreshRecommendCellData( self, node, index )
	local data = self.playerList[index]
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	local guildName = string.IsNullOrEmpty(data.guildName) and Util.GetText('NoGuild') or data.guildName
	MenuBase.SetLabelText(node, 'lb_guild', guildName, 0, 0)
	MenuBase.SetVisibleUENode(node, 'lb_apply', data.applied)
	MenuBase.SetVisibleUENode(node, 'bt_apply', not data.applied)
	--MenuBase.SetLabelText(node, 'lb_practice', GameUtil.GetPracticeName(data.practiceLv, data.fightPower), 0, 0)
	local lb_practice = node:FindChildByEditName('lb_practice', true)
	if data.practiceLv > 0 then
 		lb_practice.Visible = true
		GameUtil.SetPracticeName(lb_practice,data.practiceLv,0)
	else
		lb_practice.Visible = false
	end


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

	local btn_apply = node:FindChildByEditName('bt_apply', true)
	btn_apply.TouchClick = function( ... )
		SocialModel.RequestClientApplyFriend(data.roleId, function()
			data.applied = true
			MenuBase.SetVisibleUENode(node, 'lb_apply', true)
			MenuBase.SetVisibleUENode(node, 'bt_apply', false)
		end)
	end
	node.TouchClick = function( ... )
	SoundManager.Instance:PlaySoundByKey('button',false)
		OpenFuncMenu(self, "friendapply", nil, nil, data.roleId, data.roleName)
	end
end

function func.RefreshRecommendList( self, key )
	SocialModel.RequestClientQueryFriend(key, function(rsp)
		self.ui.comps.bt_back.Visible = string.len(key) ~= 0
		self.ui.comps.bt_change.Visible = string.len(key) == 0
		self.comps.cvs_recommend.Visible = true
		self.playerList = rsp.s2c_data.playerList
		self.ui.comps.lb_num1.Text = string.format('%d/%d', rsp.s2c_data.friendCount, rsp.s2c_data.friendMax)
		local pan = self.ui.comps.sp_friendlist3
		local cell = self.ui.comps.cvs_player3
		UIUtil.ConfigVScrollPan(pan, cell, #self.playerList, function(node, index)
			RefreshRecommendCellData(self, node, index)
		end)
	end)
end
------------------------- 推荐列表 -----------------------------

------------------------- 申请列表 -----------------------------
local function RefreshApplyCellData( self, node, index )
	local data = self.applyList[index]
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	local guildName = string.IsNullOrEmpty(data.guildName) and Util.GetText('NoGuild') or data.guildName
	MenuBase.SetLabelText(node, 'lb_guild', guildName, 0, 0)

	local lb_practice = node:FindChildByEditName('lb_practice', true)
	if data.practiceLv > 0 then
 		lb_practice.Visible = true
		GameUtil.SetPracticeName(lb_practice,data.practiceLv,0)
	else
		lb_practice.Visible = false
	end

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

	local btn_yes = node:FindChildByEditName('bt_yes', true)
	btn_yes.TouchClick = function( ... )
		SocialModel.RequestClientReplyFriend(data.roleId, true, function( ... )
			GameAlertManager.Instance:ShowNotify(Util.GetText('friend_add_agree'))
			func.RefreshApplyList(self)
		end)
	end

	local bt_no = node:FindChildByEditName('bt_no', true)
	bt_no.TouchClick = function( ... )
		SocialModel.RequestClientReplyFriend(data.roleId, false, function( ... )
			GameAlertManager.Instance:ShowNotify(Util.GetText('friend_add_refuse'))
			func.RefreshApplyList(self)
		end)
	end

	node.TouchClick = function( ... )
	SoundManager.Instance:PlaySoundByKey('button',false)
		OpenFuncMenu(self, "friendapply", nil, nil, data.roleId, data.roleName)
	end
end

function func.RefreshApplyList( self )
	SocialModel.RequestClientApplyList(function(rsp)
		self.comps.cvs_apply.Visible = true
		self.applyList = rsp.s2c_data.applyList
		self.ui.comps.lb_num2.Text = string.format('%d/%d', rsp.s2c_data.applyCount, rsp.s2c_data.applyMax)
		local pan = self.ui.comps.sp_friendlist2
		local cell = self.ui.comps.cvs_player2
		UIUtil.ConfigVScrollPan(pan, cell, #self.applyList, function(node, index)
			RefreshApplyCellData(self, node, index)
		end)

		-- 清掉红点
	    GlobalHooks.UI.SetRedTips('social_apply', 0)
	end)

	self.ui.comps.bt_allyes.TouchClick = function( ... )
		SocialModel.RequestClientReplyFriend('', true, function( ... )
			GameAlertManager.Instance:ShowNotify(Util.GetText('friend_add_agree'))
			func.RefreshApplyList(self)
		end)
	end
	self.ui.comps.bt_allno.TouchClick = function( ... )
		GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, Util.GetText('friend_deleteapply'), '', '',  nil, function(p)
			SocialModel.RequestClientReplyFriend('', false, function( ... )
				GameAlertManager.Instance:ShowNotify(Util.GetText('friend_add_refuse'))
				func.RefreshApplyList(self)
			end)
		end, nil)
	end
end
------------------------- 申请列表 -----------------------------


------------------------- 好友列表 -----------------------------
local function RefreshFriendCellData( self, node, index )
	local data = self.friendList[index]
	MenuBase.SetLabelText(node, 'lb_lv', Util.GetText('common_level2', data.level), 0, 0)
	MenuBase.SetLabelText(node, 'lb_name', data.roleName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_fight', tostring(data.fightPower), 0, 0)
	local guildName = string.IsNullOrEmpty(data.guildName) and Util.GetText('NoGuild') or data.guildName
	MenuBase.SetLabelText(node, 'lb_guild', guildName, 0, 0)
	MenuBase.SetLabelText(node, 'lb_relatlv', tostring(data.relationLv), 0, 0)
	local dbrelation = GlobalHooks.DB.FindFirst('relationship', { relat_lv = data.relationLv })
	local gg_plan = node:FindChildByEditName('gg_plan', true)
	gg_plan:SetGaugeMinMax(0, dbrelation.lv_num == 0 and data.relationExp or dbrelation.lv_num)
	gg_plan.Value = data.relationExp > gg_plan.GaugeMaxValue and gg_plan.GaugeMaxValue or data.relationExp
	local processStr = dbrelation.lv_num == 0 and 'MAX' or data.relationExp..'/'..dbrelation.lv_num
	MenuBase.SetLabelText(node, 'lb_relatnum', processStr, 0, 0)
	MenuBase.SetLabelText(node, 'lb_relatname', Util.GetText(dbrelation.name), 0, 0)

    if data.leaveTime == System.DateTime.MaxValue then
		MenuBase.SetLabelText(node, 'lb_online', Util.GetText('common_online'), 0, 0)
		MenuBase.SetVisibleUENode(node, 'lb_offtime', false)
    else
		MenuBase.SetLabelText(node, 'lb_online', Util.GetText('common_offline'), 0, 0)
		MenuBase.SetVisibleUENode(node, 'lb_offtime', true)
    	local sec = TimeUtil.TimeLeftSec(data.leaveTime)
        local timeStr = TimeUtil.FormatToCN(sec)
		MenuBase.SetLabelText(node, 'lb_offtime', timeStr, 0, 0)
    end

	local lb_practice = node:FindChildByEditName('lb_practice', true)
	if data.practiceLv > 0 then
 		lb_practice.Visible = true
		GameUtil.SetPracticeName(lb_practice,data.practiceLv,0)
	else
		lb_practice.Visible = false
	end

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

	local btn_detail = node:FindChildByEditName('bt_details', true)
	btn_detail.TouchClick = function( ... )
    	GlobalHooks.UI.OpenUI('LookPlayerInfo', -1, data.roleId)
	end
	node.TouchClick = function( ... )
		SoundManager.Instance:PlaySoundByKey('button',false)
		OpenFuncMenu(self, "friend", nil, nil, data.roleId, data.roleName, function( ... )
			self.lastPos = self.pan.Scrollable.Container.Position2D
			func.RefreshFriendList(self)
		end)
	end
end

function func.RefreshFriendList( self )
	SocialModel.RequestClientGetFriendList(function(rsp)
		self.comps.cvs_friend.Visible = true
		self.friendList = rsp.s2c_data.friendList
		self.ui.comps.lb_num1.Text = string.format('%d/%d', rsp.s2c_data.friendCount, rsp.s2c_data.friendMax)

		table.sort(self.friendList, function( a, b )
			if a.leaveTime == System.DateTime.MaxValue and b.leaveTime == System.DateTime.MaxValue then
				if a.relationLv == b.relationLv then
					return (a.relationExp - b.relationExp) > 0
				else
					return (a.relationLv - b.relationLv) > 0
				end
			else
				return (a.leaveTime - b.leaveTime).TotalSeconds > 0
			end
		end)

		self.pan = self.ui.comps.sp_friendlist1
		local cell = self.ui.comps.cvs_player1
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.friendList, function(node, index)
			RefreshFriendCellData(self, node, index)
		end)
		if self.lastPos then
			self.pan.Scrollable:LookAt(-self.lastPos)
		end
		self.ui.comps.cvs_nothing.Visible = #self.friendList == 0
	end)
end
------------------------- 好友列表 -----------------------------

local function SwitchList( self, tag )
	self.comps.cvs_friend.Visible = false
	self.comps.cvs_apply.Visible = false
	self.comps.cvs_recommend.Visible = false
	self.comps.cvs_gift.Visible = false
	if tag == 0 then
		func.RefreshFriendList(self)
	elseif tag == 1 then
		func.RefreshApplyList(self)
	elseif tag == 2 then
		self.ui.comps.bt_back.Visible = false
		self.ui.comps.bt_change.Visible = false
		func.RefreshRecommendList(self, '')
	elseif tag == 3 then
		func.RefreshRelationList(self)
	end
end

function _M.OnEnable( self )
	-- print('----------OnEnable-----------')
end

function _M.OnDisable( self )
	-- print('----------OnDisable-----------')
end

function _M.OnEnter( self, sub, playerId )
	self.rltRoleSel = 1
	self.rltLastPos = nil
	self.sub = sub
	self.defPlayerId = playerId

    local tbts = {
        self.comps.tbt_friend,
        self.comps.tbt_applylist,
        self.comps.tbt_recommend,
        self.comps.tbt_gift
    }
    local default = tbts[sub or 1] or tbts[1]
    UIUtil.ConfigToggleButton(tbts, default, false,
        function(sender)
            if sender.IsChecked and self.ui.IsRunning then
                SwitchList(self, sender.UserTag)
            end
        end)
end

-- function _M.OnLoad(self, callBack, params)

-- end

function _M.OnExit( self )
	
end

function _M.OnDestory( self )
	
end

function _M.OnInit( self )
	self.ui.comps.cvs_player1.Visible = false
	self.ui.comps.cvs_player2.Visible = false
	self.ui.comps.cvs_player3.Visible = false
	self.ui.comps.cvs_player4.Visible = false
	self.ui.comps.cvs_giftinfo.Visible = false
	self.ui.comps.cvs_recordlist.Visible = false

	self.ui.comps.bt_search.TouchClick = function( ... )
		local key = self.ui.comps.ti_search.Input.Text
		if string.len(key) == 0 then
			GameAlertManager.Instance:ShowNotify(Util.GetText('friend_searchtips'))
		elseif key == DataMgr.Instance.UserData.DigitID or key == DataMgr.Instance.UserData.Name then
			GameAlertManager.Instance:ShowNotify(Util.GetText('friend_add_self'))
		else
			func.RefreshRecommendList(self, key)
		end
	end
	self.ui.comps.bt_back.TouchClick = function( ... )
		self.ui.comps.ti_search.Input.Text = ''
		func.RefreshRecommendList(self, '')
	end
	self.ui.comps.bt_change.TouchClick = function( ... )
		self.ui.comps.ti_search.Input.Text = ''
		func.RefreshRecommendList(self, '')
	end

	self.ui.comps.btn_help.TouchClick = function( ... )
		self.ui.comps.cvs_help.Visible = true
	end
    self.ui.comps.cvs_help.event_PointerUp = function( ... )
    	self.ui.comps.cvs_help.Visible = false
    end

	self.ui.comps.bt_record.TouchClick = function( ... )
		ShowGiftRecord(self, true)
	end

	self.ui.comps.btn_recordclose.TouchClick = function( ... )
		ShowGiftRecord(self, false)
	end

	self.ui.comps.btn_marry.TouchClick = function( ... )
		FunctionUtil.OpenFunction('marryapply')
        GlobalHooks.UI.CloseUIByTag('SocialMain')
	end
end

return _M