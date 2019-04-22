local _M = {}
_M.__index = _M

local AtbModel = require 'Model/Attribute'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'
local PlayRuleModel = require 'Model/PlayRuleModel'

local bindTable = {
--战斗属性
	{ comp = 'lb_MaxHPNum', arrow = 'ib_MaxHP'},
	{ comp = 'lb_AttackNum', arrow = 'ib_Attack'},
	{ comp = 'lb_DefendNum', arrow = 'ib_Defend'},
	{ comp = 'lb_MDefNum', arrow = 'ib_MDef'},
	{ comp = 'lb_ThroughNum', arrow = 'ib_Through'},
	{ comp = 'lb_BlockNum', arrow = 'ib_Block'},
	{ comp = 'lb_HitNum', arrow = 'ib_Hit'},
	{ comp = 'lb_DodgeNum', arrow = 'ib_Dodge'},
	{ comp = 'lb_CritNum', arrow = 'ib_Crit'},
	{ comp = 'lb_CriDamagePerNum', arrow = 'ib_CriDamagePer'},
--基础属性（更多）
	{ comp = 'lb_MaxHPNum2', arrow = ''},
	{ comp = 'lb_AttackNum2', arrow = ''},
	{ comp = 'lb_DefendNum2', arrow = ''},
	{ comp = 'lb_MDefNum2', arrow = ''},
	{ comp = 'lb_ThroughNum2', arrow = ''},
	{ comp = 'lb_BlockNum2', arrow = ''},
	{ comp = 'lb_HitNum2', arrow = ''},
	{ comp = 'lb_DodgeNum2', arrow = ''},
	{ comp = 'lb_CritNum2', arrow = ''},
	{ comp = 'lb_ResCritNum2', arrow = ''},
	{ comp = 'lb_CriDamagePerNum2', arrow = ''},
	{ comp = 'lb_RedcridamageperNum2', arrow = ''},
	{ comp = 'lb_DefreductionNum2', arrow = ''},
	{ comp = 'lb_MdefreductionNum2', arrow = ''},
	{ comp = 'lb_AutorecoverhpNum2', arrow = ''},
	{ comp = 'lb_TargettomonsterNum2', arrow = ''},
	{ comp = 'lb_TargettoplayerNum2', arrow = ''},
--元素属性（更多）
	{ comp = 'lb_ThunderdamageNum2', arrow = ''},
	{ comp = 'lb_WinddamageNum2', arrow = ''},
	{ comp = 'lb_IcedamageNum2', arrow = ''},
	{ comp = 'lb_FiredamageNum2', arrow = ''},
	{ comp = 'lb_SoildamageNum2', arrow = ''},
	{ comp = 'lb_ThunderresistNum2', arrow = ''},
	{ comp = 'lb_WindresistNum2', arrow = ''},
	{ comp = 'lb_IceresistNum2', arrow = ''},
	{ comp = 'lb_FireresistNum2', arrow = ''},
	{ comp = 'lb_SoilresistNum2', arrow = ''},
}

local function CloseItemDetail( self, key )
	if self.detailMenu then
		if key then
			if self.detailMenu[key] ~= nil then
				self.detailMenu[key]:Close()
				self.detailMenu[key] = nil
			end
		else
			if self.detailMenu['equiped'] ~= nil then
				self.detailMenu['equiped']:Close()
			end
			if self.detailMenu['unequiped'] ~= nil then
				self.detailMenu['unequiped']:Close()
			end
			self.detailMenu = nil
		end
	end
end

local function ShowItemDetail2(self, detail, isEquiped, x, y)
	-- print('ShowItemDetail ', isEquiped)
	self.detailMenu = self.detailMenu or {}
	local detailMenuKey = isEquiped and 'equiped' or 'unequiped'
	local detailMenu = self.detailMenu[detailMenuKey]
	if not detailMenu then
		detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
		self:AddSubUI(detailMenu)
		self.detailMenu[detailMenuKey] = detailMenu
	end
	detailMenu:Reset({detail=detail,autoHeight=true,index=self.select_index,compare=not isEquiped,IsEquiped=isEquiped})
	detailMenu:EnableTouchFrame(false)
	detailMenu:SetButtons({})
	detailMenu:SetPos(x, y)
end

local function ShowItemDetail(self, index, item)
	local detail = ItemModel.GetDetailByItemData(item)
	ShowItemDetail2(self, detail, index <= self.equipNum, 456, 96)
	if self.equipNum > 0 and index > self.equipNum then --该部位已装备，并且选中的是未装备项
		local detail1 = ItemModel.GetDetailByItemData(self.listData[1].Item)
		ShowItemDetail2(self, detail1, true, 790, 96)
	else
		CloseItemDetail(self, index <= self.equipNum and 'unequiped' or 'equiped')
	end
end

local function SelectItemCell(self, index, slot)
	self.listIndex = index
	self.ui.comps.sp_oar:RefreshShowCell()
	ShowItemDetail(self, index, slot.Item)
end

local function RefreshCellData(self, node, index)
	-- print('-----RefreshCellData----', index, #self.listData)
	local slot = self.listData[index]
	if not slot.Item then
		return
	end

	local detail = ItemModel.GetDetailByTemplateID(slot.Item.TemplateID)
	local cvs_icon = node:FindChildByEditName('cvs_item', true)
	cvs_icon:RemoveAllChildren(true)
	local itemShow = ItemShow.Create(slot.Item)
	itemShow.Size2D = cvs_icon.Size2D
	itemShow.EnableTouch = true
	itemShow.IsEquiped = index <= self.equipNum
	if DataMgr.Instance.UserData.Level < detail.static.level_limit then
		itemShow.LevelLimit = true
	end
	itemShow.TouchClick = function( sender )
		SelectItemCell(self, index, slot)
	end
	cvs_icon:AddChild(itemShow)
	itemShow.Position2D = UnityEngine.Vector2(0,0)

	MenuBase.SetLabelText(node, "lb_itemname", Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))
	MenuBase.SetVisibleUENode(node, "ib_discern", false)
	MenuBase.SetVisibleUENode(node, "btn_equip", index <= self.equipNum)
	MenuBase.SetVisibleUENode(node, "btn_unequip", index > self.equipNum)

	local equipBtn = UIUtil.FindChild(node, 'btn_equip', true)
	equipBtn.Visible = index > self.equipNum
	local unequipBtn = UIUtil.FindChild(node, 'btn_unequip', true)
	unequipBtn.Visible = index <= self.equipNum
	equipBtn.TouchClick = function( sender )
		DataMgr.Instance.UserData.Bag:Equip(slot.Index, function( success )
			_M.CheckEquipUp(self, self.select_index)
			UnityHelper.WaitForEndOfFrame(function( ... )
				CloseItemDetail(self)
			end)
		end)
	end
	unequipBtn.TouchClick = function( sender )
		DataMgr.Instance.UserData.EquipBag:UnEquip(slot.Index, function( success )
			_M.CheckEquipUp(self, self.select_index)
			CloseItemDetail(self)
		end)
	end

	local tbt = UIUtil.FindChild(node, 'tbt_bjtu', true)
	tbt.IsChecked = self.listIndex == index
	tbt.TouchClick = function( sender, e )
		SelectItemCell(self, index, slot)
	end

	local detail = ItemModel.GetDetailByItemData(slot.Item)

	local curScore = detail.score
	local label = node:FindChildByEditName('lb_text3', true)
	if label then
		label.Text = curScore
		if index == 1 then
			self.score = self.equipNum > 0 and curScore or 0
		end
		local arrowUp = node:FindChildByEditName('ib_up', true)
		arrowUp.Visible = curScore > self.score
		arrowUp.X = label.X + label.PreferredSize.x + 4
		local arrowDown = node:FindChildByEditName('ib_down', true)
		arrowDown.Visible = curScore < self.score
		arrowDown.X = label.X + label.PreferredSize.x + 4
	end
end

function _M.CheckEquipUp( self, slotIndex )
	local function CheckNeedCompare(detail)
		if not detail then return false end
		local pro = DataMgr.Instance.UserData.Pro
		return detail.static.item_type == 2 and detail.static_equip.equip_pos == slotIndex and (pro == detail.static_equip.profession or detail.static_equip.profession == 0)
	end	
	
	local eqItdata = self.listener:GetItemData(slotIndex)
	local eqScore = 0
	if eqItdata then
		local eqDetail = ItemModel.GetDetailByItemData(eqItdata)
		eqScore = eqDetail.score
	end

	local itemShow = self.listener:GetShowAt(slotIndex)
	itemShow.IsArrowUp = false
	local bags = DataMgr.Instance.UserData.Bag.AllSlots
	for i = 1, bags.Length do
		local detail = ItemModel.GetDetailByItemData(bags[i].Item)
		if CheckNeedCompare(detail) and eqScore < detail.score and DataMgr.Instance.UserData.Level >= detail.static.level_limit then
			itemShow.IsArrowUp = true
			break
		end
	end
end

local function InitEquipArrow( self )
	local ret = {}
	local equips = self.listener.AllSlots
	for i = 1, equips.Length do
		local index = equips[i].Index
		_M.CheckEquipUp(self, index)
	end
	-- self.bagListener.OnMatch = self.OnMatch
	-- self.bagListener:Start(true,true)
end

local function RefreshEquipList(self, slotIndex)
	print('---------RefreshEquipList----------', slotIndex)
	local equips = self.listener.AllSlots
	local bags = self.bagListener.AllSlots
	local listData = {}
	self.equipNum = 0
	local selectEquip = equips[slotIndex + 1]
	if not selectEquip.IsNull then
		listData[1] = equips[slotIndex + 1]
	end
	self.equipNum = #listData
	for i=1, bags.Length do
		local slot = bags[i]
		if not slot.IsNull then
			listData[#listData + 1] = slot
			slot.Index = self.bagListener:GetSourceIndex(slot.Index)
		end
	end
	self.listData = listData
	-- print_r(listData)
	local pan = self.ui.comps.sp_oar
	local cell = self.ui.comps.cvs_itemlist
	cell.Visible = false
	UIUtil.ConfigVScrollPan(pan, cell, #self.listData, function(node, index)
		RefreshCellData(self, node, index)
	end)
	pan.IsInteractive = false
	pan.Scrollable.IsInteractive = false
	pan.ContainerPanel.IsInteractive = false
end

function _M.ShowMore( self, isShow )
	if self.ui.comps.tbt_more.IsChecked ~= isShow then
		self.ui.comps.tbt_more.IsChecked = isShow
	end
	if isShow then
		if self.ui.comps.cvs_equiplist.Visible then
			_M.CloseEquipList(self)
		end
		self.parent.ShowRoleMenu(false)
	else
		if not self.ui.comps.cvs_equiplist.Visible then
			self.parent.ShowRoleMenu(true)
		end
	end
	self.ui.comps.cvs_more.Visible = isShow
end

function _M.CloseEquipList(self)
	self.itemPanel:CleanSelect()
	self.select_index = 0
	_M.ShowEquipList(self, 0)
	CloseItemDetail(self)
end

function _M.ShowEquipList(self, slotIndex)
	-- print('---------ShowEquipList----------', slotIndex)
	local showEquip = slotIndex ~= 0
	self.ui.comps.cvs_equiplist.Visible = showEquip
	self.parent.ShowRoleMenu(not showEquip)
	if showEquip then
		_M.ShowMore(self, false)
	end
	self.listIndex = 0
	self.OnMatch = function ( itdata )
		local detail = ItemModel.GetDetailByTemplateID(itdata.TemplateID)
		-- TODO 改成枚举
		local pro = DataMgr.Instance.UserData.Pro
		return detail.static.item_type == 2 and detail.static_equip.equip_pos == slotIndex and (pro == detail.static_equip.profession or detail.static_equip.profession == 0)
	end
	self.bagListener.OnMatch = self.OnMatch
	self.bagListener:Start(true,true)
	if showEquip then
		RefreshEquipList(self, slotIndex)
	end
end

function _M.ShowRole(self)
	_M.CloseEquipList(self)
	_M.ShowMore(self, false)
end

local function RefreshPropData(self, showAnime)
	-- print('---------RefreshPropData----------', #bindTable)
	for i=1, #bindTable do
		local t = bindTable[i]
		local prop = self.ui.comps[t.comp].UserTag
		local b, vType, v = DataMgr.Instance.UserData:TryGetRoleProp(prop, 0, 0)
		local attr = unpack(GlobalHooks.DB.Find('Attribute',{id=prop}))	
		local vText
		-- print('----------- ', t.comp, vType, attr.client_showtype)
		if vType == TLBattle.RoleValueType._Percent or attr.client_showtype == 1 then --万分比
			vText = (v / 100)..'%'
		elseif vType == TLBattle.RoleValueType._Value then --值类型
			vText = tostring(v)
		else --无此类型
			vText = '0'
		end

		if showAnime and t.arrow ~= '' then
			local vStr = self.ui.comps[t.comp].Text
			local oldValue = vType == TLBattle.RoleValueType._Value and tonumber(vStr) or tonumber(string.sub(vStr, 1, -2))
			local newValue = (vType == TLBattle.RoleValueType._Percent or attr.client_showtype == 1) and v / 100 or v
			local result = newValue - oldValue
			local effectNode = self.ui.comps[t.arrow]
			if result == 0 then
				effectNode.Visible = false
			else
				-- 播放特效
				effectNode.Visible = true
				-- effectNode:SetAnchor(Vector2.New(0.5,0.5))
				-- effectNode.Scale = Vector2.New(1, 1)
				UIUtil.PlayCPJOnce(effectNode,result > 0 and 1 or 0,function(sender)
					effectNode.Visible = false
				end)
				-- 数字
				local formatStr
				if vType == TLBattle.RoleValueType._Percent or attr.client_showtype == 1 then --万分比
					formatStr = '{0}%'
				elseif vType == TLBattle.RoleValueType._Value then --值类型
					formatStr = nil
				else --无此类型
					
				end
				UIUtil.AddNumberPlusPlusTimer(self.ui.comps[t.comp], oldValue, newValue, 0.5, formatStr)
			end
		else
			self.ui.comps[t.comp].Text = vText
		end
	end
end

local function OnItemSelectChange(self, new, old)
	self.select_index = new and self.listener:GetSourceIndex(new.Index) or 0
	_M.ShowEquipList(self, self.select_index)
	if new and not new.IsEmpty then
		SelectItemCell(self, 1, self.listData[1])
	else
		CloseItemDetail(self)
	end
end

function _M.OnEnter( self )
	-- print('AttributeEquip OnEnter')
	self.parent = self.ui.menu.ExtParam[1]
	self.ui.comps.cvs_equiplist.Visible = false
	self.ui.comps.cvs_equiplist.EnableChildren = true
	self.ui.comps.lb_roleid.Text = DataMgr.Instance.UserData.DigitID
	self.ui.comps.lb_killinfonum.Text = DataMgr.Instance.UserData.PKValue

	local practicelv = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.PRACTICELV, 0)

	if practicelv ==0 then
		self.ui.comps.lb_shengelv.Text = Util.GetText('common_none')
	else
		self.ui.comps.lb_shengelv.Text = GameUtil.GetPracticeName(practicelv, 0)
	end
	

	if string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildName) then
		self.ui.comps.lb_guildname.Text = Util.GetText('NoGuild')
	else
		self.ui.comps.lb_guildname.Text = DataMgr.Instance.UserData.GuildName	
	end

    self.ui.comps.lb_joblv.Text = PlayRuleModel.GetIdentityString()

	RefreshPropData(self, false)

	AtbModel.AddPropChangeListener('AttributeEquip', function(data)
		RefreshPropData(self, true)
	end)

	self.listener:Start(false,false)
	self.itemPanel:Init(self.listener)

	_M.ShowMore(self, false)

	local showEquip = self.parent.ShowEquip
	if showEquip ~= nil then
		self.select_index = showEquip
		self.itemPanel:Select(showEquip)
		_M.ShowEquipList(self, self.select_index)
		if self.parent.EquipSelIndex then
			local itemData = ItemModel.GetBagItemDataByIndex(self.parent.EquipSelIndex)
			local listIndex
			for i = 1, #self.listData do
				local item = self.listData[i].Item
				if item.ID == itemData.ID then
					listIndex = i
					break
				end
			end
			SelectItemCell(self, listIndex, self.listData[listIndex])
		end
	else
		_M.ShowEquipList(self, 0)
	end

	InitEquipArrow(self)

	self.ui.comps.btn_close.TouchClick = function()
		_M.CloseEquipList(self)
	end

	self.ui.comps.cvs_close.TouchClick = function()
		_M.ShowRole(self)
	end

	self.ui.comps.cvs_equiplist.TouchClick = function()
		CloseItemDetail(self)
	end

	self.ui.menu.ParentMenu.ParentMenu.event_PointerClick = function()
		_M.ShowRole(self)
	end

	self.ui.comps.btn_dress.TouchClick = function (sender)
		-- body
		GlobalHooks.UI.OpenUI('WardrobeMain', 0)
	end
end

function _M.OnExit( self )
	-- print('AttributeEquip OnExit')
	AtbModel.RemovePropChangeListener('AttributeEquip')
	self.listener:Stop(false)
	self.bagListener:Stop(false)
	CloseItemDetail(self)
end

function _M.OnDestory( self )
	-- print('AttributeEquip OnDestory')
	self.listener:Dispose()
	self.bagListener:Dispose()
end

function _M.OnInit( self )
	-- print('AttributeEquip OnInit')
	self.ui:EnableTouchFrame(false)
	-- self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	
	self.select_index = 0
	self.listener = ItemListener(DataMgr.Instance.UserData.EquipBag,false,DataMgr.Instance.UserData.EquipBag.Size)
	self.itemPanel = ItemPanel(self.comps.cvs_equip.Size2D)
	self.ui.root:AddChild(self.itemPanel)
	self.itemPanel:AddLogicNode(1,self.comps.cvs_equip)
	self.itemPanel:AddLogicNode(2,self.comps.cvs_helmet)
	self.itemPanel:AddLogicNode(3,self.comps.cvs_clothes)
	self.itemPanel:AddLogicNode(4,self.comps.cvs_pants)
	self.itemPanel:AddLogicNode(5,self.comps.cvs_belt)
	self.itemPanel:AddLogicNode(6,self.comps.cvs_shoe)
	self.itemPanel:AddLogicNode(7,self.comps.cvs_necklace)
	self.itemPanel:AddLogicNode(8,self.comps.cvs_ring)
	self.itemPanel.EnableSelect = true
	self.itemPanel.EnableEmptySelect = true
	self.itemPanel.OnItemSingleSelect = function(new,old)
		if not new and not old then return end
		OnItemSelectChange(self,new,old)
	end
	self.itemPanel.OnItemInit = function(it)
		it.IsBinded = false
	end
	self.bagListener=ItemListener(DataMgr.Instance.UserData.Bag,false,0)

	self.bagListener.OnCompare = function ( it1,it2 )
		-- TODO  排序  小于0 it1 小于 it2；大于0 x 大于y 
		local detail1 = ItemModel.GetDetailByItemData(it1)
		local detail2 = ItemModel.GetDetailByItemData(it2)
		if detail1.score == detail2.score then
			if detail2.static.quality == detail1.static.quality then
				return (detail1.static.level_limit - detail2.static.level_limit)
			else
				return (detail2.static.quality-detail1.static.quality)
			end
		else
			return (detail2.score-detail1.score)
		end
	end
	self.bagListener.OnItemUpdateAction = function ( act )
		if act.Type ~= ItemUpdateAction.ActionType.Init then
			self.needRefresh = true
			self.ui.comps.cvs_equiplist.EnableChildren = false
			UnityHelper.WaitForSeconds(0.1, function()
				if self.needRefresh then
					self.needRefresh = false
					self.ui.comps.cvs_equiplist.EnableChildren = true
					RefreshEquipList(self, self.select_index)
				end
			end)
		end
	end

	self.listener.OnItemUpdateAction = function ( act )
		if act.Type ~= ItemUpdateAction.ActionType.Init then
			self.needRefresh = true
			self.ui.comps.cvs_equiplist.EnableChildren = false
			UnityHelper.WaitForSeconds(0.1, function()
				if self.needRefresh then
					self.needRefresh = false
					self.ui.comps.cvs_equiplist.EnableChildren = true
					RefreshEquipList(self, self.select_index)
				end
			end)
		end
	end

	self.ui.comps.tbt_more.Selected = function( sender )
		_M.ShowMore(self, sender.IsChecked)
	end

	self.ui.comps.btn_moreclose.TouchClick = function( sender )
		self.ui.comps.tbt_more.IsChecked = false
	end
end

return _M