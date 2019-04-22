local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local FateModel = require 'Model/FateModel'
local ItemModel = require 'Model/ItemModel'
 
local function UpdateFateSoul(self)
	self.ui.comps.lb_soulnum.Text = ItemModel.CountItemByTemplateID(9)
end 

local function GetFateLv(dynamicAttrs)
	local fateLv = 1
	for _, v in ipairs(dynamicAttrs or {}) do
		if v.Tag == 'FateTag' then
			fateLv = v.Value or 1
		end
	end
	return fateLv
end
 
local function GetFateLock(dynamicAttrs)
	for k, v in ipairs(dynamicAttrs or {}) do
		for _,sub in ipairs(v.SubAttributes or  {}) do
        	if sub.Tag == 'IsLockedTag' then
        		return true
        	end
    	end
	end
    return false
end 
 
local function UpdateFight(self)
	local bags = DataMgr.Instance.UserData.FateEquipBag.AllSlots
	local EquipFight = 0
	for i = 1, bags.Length do
		local detail = ItemModel.GetDetailByItemData(bags[i].Item)
		if detail then
			EquipFight = EquipFight + ItemModel.CalcAttributesScore(detail.dynamicAttrs,'FixedAttributeTag')
		end
	end
	self.ui.comps.lb_fightnum.Text = EquipFight
	self.EquipFight = EquipFight
	return EquipFight
end

local function ShowAttribute(self)
  
	local EquipBag = DataMgr.Instance.UserData.FateEquipBag.AllSlots
 	local  roleAttr = {}
	for i = 1, EquipBag.Length do
		local detail = ItemModel.GetDetailByItemData(EquipBag[i].Item)
		if detail then
			for i, v in ipairs(detail.dynamicAttrs or {}) do
				if v.Tag == 'FixedAttributeTag' then
					-- print_r('vvvvvvvvvvvvv:',v)
					local attr
					if roleAttr[v.Name] then
						roleAttr[v.Name] = roleAttr[v.Name] + v.Value or 0
					else
						roleAttr[v.Name] = v.Value or 0
					end
				end
			end
		end
	end

	local allattribute = FateModel.GetAllAttribute()
	for i,v in ipairs(allattribute) do
		local label = self.ui.comps['lb_' .. v]
		if label then
			label.Text = roleAttr[v] or 0
		end
	end

	self.ui.comps.lb_allfight.Text = self.EquipFight
	
end

local function GetItemDetailMenu(self)
    local lastMenu = self.detailMenu
    local detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
    self:AddSubUI(detailMenu)

    local function DetailMenuOnExit(menu)
        if menu == self.detailMenu then
            self.detailMenu = nil
            if self.bagList then
                self.bagList:CleanSelect()
            end
            if self.itemPanel then
            	self.itemPanel:CleanSelect()
            end
        end
    end

    detailMenu:SubscribOnExit(function() 
        DetailMenuOnExit(detailMenu)
    end)  
    self.detailMenu = detailMenu 

    if lastMenu then 
        lastMenu:Close()
    end
end

local function OnShowDetail(self, detail, count)
	GetItemDetailMenu(self)
	self.detailMenu:Reset({detail = detail, count = count, index = self.select_index, autoHeight = false,globalTouch = true})

	local lockStatus = GetFateLock(detail.dynamicAttrs)

	local btns = {{Text = Constants.Text.detail_btn_equip, cb = function()
		self.DoEquip = true
		DataMgr.Instance.UserData.FateBag:Equip(self.select_index, function( success )
			 UpdateFight(self)
		end)
	end},

	
	{Text = lockStatus and Constants.Text.Fate_UnLock or Constants.Text.Fate_Lock, cb = function()
 		FateModel.RequestFateLock(detail.id,not lockStatus,function ( ... )
 			-- body
 			self.detailMenu:Close()
 		end)
	end},
	-- {Text = '升级', cb = function()
 -- 		FateModel.RequestFateLevelUp(detail.id,function ( ... )
 -- 			-- body
 -- 			self.detailMenu:Reset({detail = detail, count = count, index = self.select_index, autoHeight = false,globalTouch = true})
 -- 		end)
	-- end},
	{Text = Constants.Text.Fate_Decompose, cb = function()
		-- body
		local allSelect = {}
		table.insert(allSelect,{index=self.select_index,count=1})
		FateModel.RequestFateDecompose(allSelect,function ( ... )
			-- body
			UpdateFateSoul(self)
		end)
	end}}

	self.detailMenu:SetButtons(btns)
	self.detailMenu:SetPos(260,94)
	-- self.detailMenu:EnableTouchFrameClose(true)
end

local function OnBagSizeChange(self)
	local bag = DataMgr.Instance.UserData.FateBag
	-- self.comps.lb_num.Text = string.format('%d/%d', bag.Size - bag.EmptySlotCount, bag.Size)
end

local function OnBagItemSingleSelect(self, new, old)
	if not new then
		if self.detailMenu then
			self.detailMenu:Close()
		end
		return
	end
	
	local itdata = self.bagListener:GetItemData(new)
	self.select_index = self.bagListener:GetSourceIndex(new.Index)
	-- print('select_index', self.select_index)
	local detail = ItemModel.GetDetailByItemData(itdata)
	OnShowDetail(self, detail, itdata.Count)
end

local function FatelvUp(dynamicAttrs)
	for _, v in ipairs(dynamicAttrs or {}) do
		if v.Tag == 'FateTag' then
			local fateLv = v.Value or 1
			v.Value = fateLv + 1
			return v.Value
		end
	end
	return 2
end  

local function OnEquipShowDetail(self, it)
	local itdata = self.equipListener:GetItemData(it)

	if not itdata then
		return
	end
	local count = itdata.Count or 1
	
	local detail = ItemModel.GetDetailByItemData(itdata)

	GetItemDetailMenu(self)
	self.detailMenu:Reset({detail = detail, count = count, index = self.equip_select_index, autoHeight = false,globalTouch = true})
	local btns = {{Text = Constants.Text.detail_btn_unEquip, cb = function()
	 	DataMgr.Instance.UserData.FateEquipBag:UnEquip(self.equip_select_index, function( success )
 			--TODO STH
			UpdateFight(self)
		end)
	end},
	-- 升级
	{Text = Constants.Text.Fate_LvUp, cb = function()
		FateModel.RequestFateLevelUp(detail.id,function()
			-- body
			UpdateFateSoul(self)
			UpdateFight(self) 

 			OnEquipShowDetail(self,it)
 			local newLv = GetFateLv(detail.dynamicAttrs) + 1
			local index = self.equip_select_index
			local LvLabel = self.ui.comps['lb_level' .. index + 1]
			if LvLabel then
				LvLabel.Visible = true
				LvLabel.Text =  Util.GetText('common_level2',newLv)
			end

			local t = {DisableToUnload = true, Parent = it.Parent.Transform, LayerOrder = self.menu.MenuOrder, Pos = {x = 33, y = -33}, UILayer = true}
        	Util.PlayEffect('/res/effect/ui/ef_ui_interface_consume.assetbundles', t)

		end)
	end}}
	self.detailMenu:SetButtons(btns)
	self.detailMenu:SetPos(596,94) 
end

local function OnEquipItemSingleSelect(self, new, old)
	if not new then
		if self.detailMenu then
			self.detailMenu:Close()
		end
		return
	end
	
	self.equip_select_index = self.equipListener:GetSourceIndex(new.Index) 
	OnEquipShowDetail(self, new)
end


local function AddPackUpTimer(self)
	self.timeid = LuaTimer.Add(1000, 1000, function()
		self._packup_cooldown = self._packup_cooldown - 1
		if self._packup_cooldown <= 0 then
			self._packup_cooldown = nil
			-- self.comps.btn_neaten.Text = '整理'
			self.timeid = nil
			return false
		else
			-- self.comps.btn_neaten.Text = string.format('00:%02d', self._packup_cooldown)
			return true
		end
	end)
end

function _M.OnEnter(self)
  	self.DoEquip = false
	self.EmptySlotIndexs = {}

	self.ui.comps.cvs_block.Visible = false
	
	UpdateFateSoul(self)

	UpdateFight(self)
	
 	local FateOpen = GlobalHooks.DB.GetFullTable('FateOpen')
 	for i,v in ipairs(FateOpen) do
 		local lockimg = self.ui.comps['ib_lock' .. v.id]
 		local unopenLabel = self.ui.comps['lb_unopen' .. v.id]
 		if lockimg then
			if v.open_vip_lv > 0 and DataMgr.Instance.UserData.VipLv < v.open_vip_lv then
				lockimg.Visible = true
				unopenLabel.Visible = true
				unopenLabel.Text = Util.GetText(v.oepn_cond)
			elseif DataMgr.Instance.UserData.Level < v.open_lv then
				lockimg.Visible = true
				unopenLabel.Visible = true
				unopenLabel.Text = Util.GetText(v.oepn_cond)
			else
				lockimg.Visible = false
				unopenLabel.Visible = false
				unopenLabel.Text = Util.GetText(v.oepn_cond)

			end 
 		end
 	end

	self.bagListener:Start(false, false)
	self.bagList:Init(self.bagListener, DataMgr.Instance.UserData.FateBag.MaxSize)
	OnBagSizeChange(self)

	self.equipListener:Start(false,false)
	self.itemPanel:Init(self.equipListener)
end

function _M.OnExit(self)
	self.equipListener:Stop(false)
	self.bagListener:Stop(false)
	self.ui.comps.cvs_block.Visible = false
end

function _M.Destory(self)
	self.equipListener:Dispose()
	self.bagListener:Dispose()

	if self.timeid then
		LuaTimer.Delete(self.timeid)
	end
end

-- local table = {0.11,0.22,0.33,0.44,0.55}
local function getSpeed(index)
	local value =  6 - index
 	if value <= 0 then
 		value = 1
 	elseif value > 5 then
 		value = 5
 	end
 	local result = value * 0.005
 	return result

end

function _M.OnInit(self)
	self.ui:EnableTouchFrame(false)
	self.DoEquip = false
	local LotteryData = GlobalHooks.DB.GetFullTable('FateType')
	for i,v in ipairs(LotteryData) do
		local numLabel = self.ui.comps['lb_num' .. i]
		numLabel.Text = v.lottery_cost_num
	end

	self.equip_select_index = 0
	self.equipListener = ItemListener(DataMgr.Instance.UserData.FateEquipBag,false,DataMgr.Instance.UserData.FateEquipBag.Size)

	self.itemPanel = ItemPanel(self.comps.cvs_hole1.Size2D)
	self.ui.root:AddChild(self.itemPanel)
	self.itemPanel:AddLogicNode(0,self.comps.cvs_hole1)
	self.itemPanel:AddLogicNode(1,self.comps.cvs_hole2)
	self.itemPanel:AddLogicNode(2,self.comps.cvs_hole3)
	self.itemPanel:AddLogicNode(3,self.comps.cvs_hole4)
	self.itemPanel:AddLogicNode(4,self.comps.cvs_hole5)
	self.itemPanel:AddLogicNode(5,self.comps.cvs_hole6)
	self.itemPanel:AddLogicNode(6,self.comps.cvs_hole7)
	self.itemPanel:AddLogicNode(7,self.comps.cvs_hole8)
	self.itemPanel:AddLogicNode(8,self.comps.cvs_hole9)
	self.itemPanel.EnableSelect = true
	self.itemPanel.EnableEmptySelect = true

	self.EquipFight = 0;
	self.itemPanel.OnItemInit = function(it)
		-- print_r('it index1111111111111111:',it.Index)
		it.IsCircleQualtiy = true
		it.IsBinded = false

		local index = it.Index + 1
		local  nameLabel = self.ui.comps['lb_name' .. index]
		local LvLabel = self.ui.comps['lb_level'.. index]
		local itdata = self.equipListener:GetItemData(it)
		if itdata and not string.IsNullOrEmpty(itdata.ID) then
			local detail = ItemModel.GetDetailByItemData(itdata)
			-- print_r('self.bagList detail:',detail)
			local fateLv = GetFateLv(detail.dynamicAttrs)
			local LvData = GlobalHooks.DB.FindFirst('FateLevel',{item_id = itdata.TemplateID,fate_lv = fateLv})
			if nameLabel  and LvData then
				nameLabel.Visible = true
				nameLabel.Text =  Util.GetText(LvData.show_name)
			end

 
			if LvLabel then
				LvLabel.Visible = true
				LvLabel.Text =  Util.GetText('common_level2',fateLv)
			end

			if self.DoEquip then
				local t = {DisableToUnload = true, Parent = it.Parent.Transform, LayerOrder = self.menu.MenuOrder, Pos = {x = 33, y = -33}, UILayer = true}
        		Util.PlayEffect('/res/effect/ui/ef_ui_interface_consume.assetbundles', t)
        	end
		else
			if nameLabel then
				nameLabel.Visible = false
			end
			if LvLabel then
				LvLabel.Visible = false
			end
		end


	end

	self.itemPanel.OnItemSingleSelect = function(new, old)
		-- print('new:',new)
		-- print('old:',old)

		OnEquipItemSingleSelect(self, new, old)
	end
 
	--背包
	local cvs_item = self.comps.cvs_bag
	cvs_item.Visible = false
	self.bagList = ItemList(cvs_item.Size2D, Vector2(64,64), 5)
	self.bagList.Position2D = cvs_item.Position2D
	self.bagList.ShowBackground = true
	self.bagList.EnableSelect = true
	self.bagList.OnItemSingleSelect = function(new, old)
		-- print('new:',new)
		-- print('old:',old)
		OnBagItemSingleSelect(self, new, old)
	end

	self.bagList.OnItemInit = function(it)
		-- it.Parent 

		it.IsCircleQualtiy = true
		local itdata = self.bagListener:GetItemData(it)

		local img = UIUtil.FindChildByUserTag(it.Parent,110)
		local lvLabel = UIUtil.FindChildByUserTag(it.Parent,120)
		local nameLabel = UIUtil.FindChildByUserTag(it.Parent,130)
		
		local backImg = UIUtil.FindChildByUserTag(it.Parent,100)
		if not backImg then
			backImg = HZImageBox.CreateImageBox()
			backImg.UserTag = 100
		    backImg.Size2D = Vector2(64,64)
		    backImg.Position2D = Vector2(10.6,10.6)
			UIUtil.SetImage(backImg,'#dynamic/TL_tips/output/TL_tips.xml|TL_tips|28',false, UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
		backImg.Visible = false
		it.Parent:AddChild(backImg)

		-- print_r('pos2222222:',pos)
		-- print_r('pos x pos y:',pos.x,pos.y)
		-- print_r('self.bagList detail:',detail)
		
		if itdata and not string.IsNullOrEmpty(itdata.ID) then
			
			local detail = ItemModel.GetDetailByItemData(itdata)

			if not img then
				img = HZImageBox.CreateImageBox()
				img.UserTag = 110
			    img.Position2D = Vector2(-6,32)
			    UIUtil.SetImage(img,'#dynamic/TL_fate/output/TL_fate.xml|TL_fate|18')
			end
 
			it.Parent:AddChild(img)
 
			local fateLv = GetFateLv(detail.dynamicAttrs)
			if not lvLabel then
				lvLabel = HZLabel.CreateLabel()
				lvLabel.UserTag = 120
				lvLabel.Position2D = Vector2(it.Width * 0.4+7,it.Height*0.8+22)
				lvLabel.FontColor = GameUtil.RGB2Color(Constants.Color.White)
				lvLabel:SetBorder(GameUtil.RGBA2Color(0x53462aff), Vector2(1, 1))
			end
			--放在这里add 可以修改index,防止移动到Icon后面被挡住
			it.Parent:AddChild(lvLabel)

			if not nameLabel then
				nameLabel = HZLabel.CreateLabel()
				nameLabel.UserTag = 130
				nameLabel.Position2D = Vector2(it.Width * 0.35+3,it.Height*0.8+1)
				nameLabel.FontColor = GameUtil.RGB2Color(Constants.Color.White)
				nameLabel:SetBorder(GameUtil.RGBA2Color(0x53462aff), Vector2(1, 1))
			end
			--放在这里add 可以修改index,防止移动到Icon后面被挡住
			it.Parent:AddChild(nameLabel)
			
			
			lvLabel.Text = Util.GetText('common_level2',fateLv)


			local LvData = GlobalHooks.DB.FindFirst('FateLevel',{item_id = itdata.TemplateID})
			nameLabel.Text = Util.GetText(LvData.show_name)
			--nameLabel.Position2D = Vector2(it.Width * 0.5 - nameLabel.Width * 0,5,it.Height * 0.8)
			nameLabel.Visible = true
 
			if not self.EmptySlotIndexs[it.Index] then
				img.Visible = true
				lvLabel.Visible = true
				nameLabel.Visible = true
			else
				local Width = self.EmptySlotIndexs[it.Index]
				-- print('Width111111111111111111111111111111111:',Width)
				self.EmptySlotIndexs[it.Index]= nil
				
				img.Visible = false
				lvLabel.Visible = false
				nameLabel.Visible = false

				--测试关闭
				it.Visible = false
				backImg.Visible = true
				



				local initT = {
					LayerOrder = self.menu.MenuOrder,
					UILayer = true,
					DisableToUnload = true,
					Parent = self.ui.comps.cvs_showmodel.Transform,
					 
				}

				local id = Util.PlayEffect('/res/effect/ui/ef_ui_skillunlock.assetbundles', initT,0,function()
					-- body
					local initPos = UIUtil.ToLocalPos(self.ui.comps.cvs_showmodel, self.ui.comps.cvs_background)
				local pos = UIUtil.ToLocalPos(it, self.ui.comps.cvs_background)
				-- local p2 = UIUtil.ToLocalPos(self.ui.comps.cvs_stone2, self.ui.comps.cvs_background)
				local pnew
				local pnew2
				if (Width>3)then
					pnew = Width*120
					pnew2 =Width
				else
					pnew = Width*(-60)
					pnew2 =Width*(-1)
				end

				local p2 = Vector2(pos.x - pnew2 *50-500,pos.y+ pnew-200) 
				--local p2 = Vector2(HZUISystem.SCREEN_WIDTH*0.4 + Width *0,pos.y+ pnew-200) 
				 

					local displayNode = DisplayNode('effect')
					displayNode.Position2D = Vector2(initPos.x, initPos.y)
					self.ui.comps.cvs_background:AddChild(displayNode)

					local t = {
						LayerOrder = self.menu.MenuOrder,
						UILayer = true,
						DisableToUnload = true,
						Parent = displayNode.Transform,
						 
					}
	 
					
					pos = Vector2(pos.x+37,pos.y+37)
					-- local id = Util.PlayEffect('/res/effect/ui/ef_ui_xiuxing_click_02.assetbundles', t)
					local info = Util.CreateTransformSet(t)
					RenderSystem.Instance:LoadGameObject('/res/effect/ui/ef_ui_xiuxing_click_02.assetbundles', info,function (aoe)
						 local t = 0
						 local speed  =  getSpeed(Width)*0.5
						 LuaTimer.Add(0,11,function(id)
							t = t + 0.03 + speed
						 	displayNode.Position2D = Vector2.Lerp(Vector2.Lerp(initPos,p2,t),Vector2.Lerp(p2,pos,t),t)
						 	if (t >= 1) then
						 		LuaTimer.Delete(id)
						 		if not it.IsDispose and it.Parent then
									-- UIUtil.SetImage(it.Parent,nil)
									if backImg then
										backImg.Visible = false
									end
									
									if lvLabel then
										lvLabel.Visible = true
									end

									if nameLabel then
										nameLabel.Visible = true
									end

									if img then
										img.Visible =  true
									end
								end
								
								if it then
									it.Visible = true
								end
								displayNode:RemoveFromParent(true)
								self.ui.comps.cvs_block.Visible = false
						 		return false
						 	end
							return true
						end)
			 
					end)

				end)
				-- local initPos = UIUtil.ToLocalPos(self.ui.comps.cvs_high, self.ui.comps.cvs_background)
				
			end

		else

			if not it.IsDispose then 
			-- print('2222222222222222222222')
				if lvLabel then
					lvLabel.Visible = false
				end

				if nameLabel then
					nameLabel.Visible = false
				end

				if img then
					img.Visible =  false
				end
			end
		end
	end

	cvs_item.Parent:AddChild(self.bagList)

	local FateBag = DataMgr.Instance.UserData.FateBag
	self.bagListener = ItemListener(FateBag, false, FateBag.Size)

	self.bagListener.OnFilledSizeChange = function(size)
		-- print('OnFilledSizeChange 1111111111111111111111111111111111111111111111111111111111111:',size)
		OnBagSizeChange(self)
		-- local node = self.list:GetDisplayNodeAt(size)
		-- print_r('node:',node)
	end

 	self.EmptySlotIndexs = {}
 	self.emptyIndex = 100
	local function initEmptyIndex(self)
		local EmptySlotIndexs = DataMgr.Instance.UserData.FateBag.EmptySlotIndexs
 		self.EmptySlotIndexs = {}
 		self.emptyIndex = 100
 		local j = 0
		for index in Slua.iter(EmptySlotIndexs) do
			if index < self.emptyIndex then
				self.emptyIndex = index
 			end
 			j = j + 1
 			self.EmptySlotIndexs[index] = j

 		end
	end

	local function SelectByIndex(pan, index)
    	UIUtil.MoveToScrollCell(pan,index,function(node)
            
        end)
	end

	--3个按钮
	self.ui.comps.btn_low.TouchClick = function (sender)
		if self.lotteryTimerId then
			GameAlertManager.Instance:ShowNotify(Constants.Text.Fate_CD)
			return
		end
		self.lotteryTimerId = LuaTimer.Add(1500,function(id)
			LuaTimer.Delete(self.lotteryTimerId)
			self.lotteryTimerId = nil
			self.ui.comps.cvs_block.Visible = false
		 	return true
		end)

		initEmptyIndex(self)

	
		SelectByIndex(self.bagList.mPan,self.emptyIndex)

		self.ui.comps.cvs_block.Visible = true
		FateModel.RequestFateLottery(1,function (resp,result)
			 if not result then
			 	self.ui.comps.cvs_block.Visible = false
			 end
		end)
	end

	self.ui.comps.btn_high.TouchClick = function (sender)
		if self.lotteryTimerId then
			GameAlertManager.Instance:ShowNotify(Constants.Text.Fate_CD)
			return
		end
		self.lotteryTimerId = LuaTimer.Add(1500,function(id)
			LuaTimer.Delete(self.lotteryTimerId)
			self.lotteryTimerId = nil
			self.ui.comps.cvs_block.Visible = false
		 	return true
		end)

		initEmptyIndex(self)
		SelectByIndex(self.bagList.mPan,self.emptyIndex)
		self.ui.comps.cvs_block.Visible = true
		FateModel.RequestFateLottery(2,function (resp,result)
			-- self.bagListener:Start(false, false)
			if not result then
				self.ui.comps.cvs_block.Visible = false
			end
		end)
	end

	self.ui.comps.btn_highm.TouchClick = function (sender)
		if self.lotteryTimerId then
			GameAlertManager.Instance:ShowNotify(Constants.Text.Fate_CD)
			return
		end
		self.lotteryTimerId = LuaTimer.Add(1500,function(id)
			LuaTimer.Delete(self.lotteryTimerId)
			self.lotteryTimerId = nil
			self.ui.comps.cvs_block.Visible = false
		 	return true
		end)

		initEmptyIndex(self)
		SelectByIndex(self.bagList.mPan,self.emptyIndex)
		self.ui.comps.cvs_block.Visible = true
		FateModel.RequestFateLottery(3,function (resp,result)
			if not result then
				self.ui.comps.cvs_block.Visible = false
			end
		end)
	end


	self.comps.btn_cleanup.TouchClick = function()
		-- if not self._packup_cooldown then
			FateBag:PackUpItems(function()

			end)
			self._packup_cooldown = Constants.WarehoursePackUpCoolDownSec
			-- self.comps.btn_neaten.Text = string.format('00:%2d', Constants.WarehoursePackUpCoolDownSec)
			-- AddPackUpTimer(self)
		-- end
	end

	self.ui.comps.cvs_decompose.TouchClick = function ( ... )
		self.ui.comps.cvs_decompose.Visible = false
	end



	local function GetFateBack(self,quality)
		local totalComposeNum = 0
		local Fatebags = DataMgr.Instance.UserData.FateBag.AllSlots
		for i = 1, Fatebags.Length do
			local bagIndex = Fatebags[i]
			local item = Fatebags[i].Item
			local detail = ItemModel.GetDetailByItemData(item)
			if detail and quality[detail.static.quality]then 
				local detail = ItemModel.GetDetailByItemData(item)
				local locked = GetFateLock(detail.dynamicAttrs)
				if not locked then
					local fateLv = GetFateLv(detail.dynamicAttrs)
					local LvData = GlobalHooks.DB.FindFirst('FateLevel',{item_id = item.TemplateID,fate_lv = fateLv})
					-- print('LvData.decompose.num[1]:',LvData.decompose.num[1])
					totalComposeNum = totalComposeNum + LvData.decompose.num[1] or 0
				end
			end
		end
		return totalComposeNum
	end

	local function OnKeyBreak(self,quality)
		local Fatebags = DataMgr.Instance.UserData.FateBag.AllSlots
		local allSelect = {}
		for i = 1, Fatebags.Length do
			local bagIndex = Fatebags[i]
			local item = Fatebags[i].Item 
			local detail = ItemModel.GetDetailByItemData(item)
			if detail and quality[detail.static.quality] then 
				local lockStatus = GetFateLock(detail.dynamicAttrs) 
				if not lockStatus then
					table.insert(allSelect,{index=bagIndex.Index,count=1})
				end
			end
		end 

		if not next(allSelect) then
			print('没有选中任何物品')
			return
		end

		FateModel.RequestFateDecompose(allSelect,function ( ... )
			-- body
			UpdateFateSoul(self)
		end)
	end 

	local function ShowOneKeyBreak(self)
		self.quality = {}
		local remember = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. '_remember')
		if remember == 1 then
			self.ui.comps.tbt_requa.IsChecked = true
			local q1 = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. '_quality1')
			-- print('q1:',q1)
			if q1 == 1 then
		 		self.ui.comps.tbt_select1.IsChecked = true
		 		self.quality[1] = true
		 	else 
		 		self.ui.comps.tbt_select1.IsChecked = false
		 		self.quality[1] = false
		 	end

		 	local q2 = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. '_quality2')
			if q2 == 1 then
		 		self.ui.comps.tbt_select2.IsChecked = true
		 		self.quality[2] = true
		 	else
		 		self.ui.comps.tbt_select2.IsChecked = false
		 		self.quality[2] = false
		 	end

			local q3 = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. '_quality3')
			if q3 == 1 then
		 		self.ui.comps.tbt_select3.IsChecked = true
		 		self.quality[3] = true
		 	else
		 		self.ui.comps.tbt_select3.IsChecked = false
		 		self.quality[3] = false
		 	end

			local q4 = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. '_quality4')
			if q4 == 1 then
		 		self.ui.comps.tbt_select4.IsChecked = true
		 		self.quality[4] = true
		 	else
		 		self.ui.comps.tbt_select4.IsChecked = false
		 		self.quality[4] = false
		 	end

		 	self.ui.comps.lb_cangetnum.Text = GetFateBack(self,self.quality)
		else

			self.quality[1] = false
			self.quality[2] = false
			self.quality[3] = false
			self.quality[4] = false

			self.ui.comps.tbt_requa.IsChecked = false
			self.ui.comps.tbt_select1.IsChecked = false
			self.ui.comps.tbt_select2.IsChecked = false
			self.ui.comps.tbt_select3.IsChecked = false
			self.ui.comps.tbt_select4.IsChecked = false
			self.ui.comps.lb_cangetnum.Text = 0
		end
	end

	--一键分解
	self.ui.comps.btn_break.TouchClick = function()
		self.ui.comps.cvs_decompose.Visible = true
		ShowOneKeyBreak(self)
	end



	self.ui.comps.btn_cancel.TouchClick = function()
		self.ui.comps.cvs_decompose.Visible = false
	end

	self.ui.comps.btn_ok.TouchClick = function()
		OnKeyBreak(self,self.quality or {})
		self.ui.comps.cvs_decompose.Visible = false
	end

	self.ui.comps.tbt_select1.TouchClick = function (sender)
		-- body
		-- print_r('sender1:',sender.IsChecked)
		UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. '_quality1', sender.IsChecked and 1 or 0)
		self.quality[1] = sender.IsChecked
		self.ui.comps.lb_cangetnum.Text = GetFateBack(self,self.quality)
	end

	self.ui.comps.tbt_select2.TouchClick = function (sender)
		-- body
		-- print_r('sender2:',sender.IsChecked)
		UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. '_quality2', sender.IsChecked and 1 or 0)
		self.quality[2] = sender.IsChecked
		self.ui.comps.lb_cangetnum.Text = GetFateBack(self,self.quality)
	end

	self.ui.comps.tbt_select3.TouchClick = function (sender)
		-- body
		-- print_r('sender3:',sender.IsChecked)
		UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. '_quality3', sender.IsChecked and 1 or 0)
		self.quality[3] = sender.IsChecked
		self.ui.comps.lb_cangetnum.Text = GetFateBack(self,self.quality)

	end

	self.ui.comps.tbt_select4.TouchClick = function (sender)
		-- body
		-- print_r('sender4:',sender.IsChecked)
		UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. '_quality4', sender.IsChecked and 1 or 0)
		self.quality[4] = sender.IsChecked
		self.ui.comps.lb_cangetnum.Text = GetFateBack(self,self.quality)
	end

	self.ui.comps.tbt_requa.TouchClick = function (sender)
		-- body
		-- print_r('tbt_requa:',sender.IsChecked)
		UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. '_remember', sender.IsChecked and 1 or 0)
	end


	self.ui.comps.cvs_tips.TouchClick = function()
		self.ui.comps.cvs_tips.Visible = false
	end

	self.ui.comps.cvs_help.TouchClick = function()
		self.ui.comps.cvs_tips.Visible = false
	end
	
	self.ui.comps.btn_help.TouchClick = function()
		self.ui.comps.cvs_allattribute.Visible = false
		self.ui.comps.cvs_tips.Visible = true
		self.ui.comps.cvs_help.Visible = true
	end


	self.ui.comps.btn_attr.TouchClick = function()
		self.ui.comps.cvs_help.Visible = false
		self.ui.comps.cvs_tips.Visible = true
		self.ui.comps.cvs_allattribute.Visible = true
		--TODO ShowAttr
		ShowAttribute(self)
	end


end

return _M
