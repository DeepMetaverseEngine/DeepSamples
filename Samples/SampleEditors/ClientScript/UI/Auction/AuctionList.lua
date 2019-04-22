local _M = {}
_M.__index = _M

local AuctionModel = require 'Model/AuctionModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local function ClearSearch( self )
	self.ui.comps.ti_search.Input.Text = ''
	self.searchIds = {}
end

local function SetCondition( self, conditionId, value )
	local titleCvs
	if conditionId == 1 then
		titleCvs = self.ui.comps.btn_job
	elseif conditionId == 2 then
		titleCvs = self.ui.comps.btn_level
	elseif conditionId == 3 then
		titleCvs = self.ui.comps.btn_quality
	elseif conditionId == 4 then
		titleCvs = self.ui.comps.btn_type
	end
	local dbc = GlobalHooks.DB.FindFirst('trade_condition', { condition_id = conditionId, enumerate_min = value })
	titleCvs.Text = Util.GetText(dbc.enumerate_name)
	-- if conditionId == 3 then
	-- 	titleCvs.FontColor = GameUtil.RGB2Color(Constants.QualityColor[dbc.enumerate_min])
	-- end
	self.condition[conditionId] = value
end

local function ShowFilterOption( self, title, conditionId )
	self.ui.comps.cvs_select.Visible = true
	local pos = self.ui.menu:LocalToUIGlobal(title)
	self.ui.comps.cvs_type.X = pos.x
	local conditionId = title.UserTag
	local dbc = GlobalHooks.DB.Find('trade_condition', { condition_id = conditionId })
	local spaceX = 5
	local spaceY = 3
	local root = self.ui.comps.cvs_type
	local cell = root:FindChildByEditName('tbn_type', true)
	root.Width = title.Width
	root.Height = #dbc * (cell.Height + spaceY) + spaceY
	local len = root.NumChildren > #dbc and root.NumChildren or #dbc
	for i = 1, len do
		local node = root:FindChildByName('tbn_type'..i, false)
		if i <= #dbc then
			if not node then
				if i == 1 then
					node = cell
				else
					node = cell:Clone()
					root:AddChild(node)
				end
				node.Name = 'tbn_type'..i
			end
			node.Visible = true
			node.Text = Util.GetText(dbc[i].enumerate_name)
			-- if conditionId == 3 then
			-- 	node.FontColor = GameUtil.RGB2Color(Constants.QualityColor[dbc[i].enumerate_min])
			-- end
			node.X = spaceX
			node.Y = spaceY + ((i - 1) * (node.Height + spaceY))
			node.Width = title.Width - spaceX * 2
			node.TouchClick = function( ... )
				self.ui.comps.cvs_select.Visible = false
				SetCondition(self, conditionId, dbc[i].enumerate_min)
				ClearSearch(self)
    			_M.RefreshList(self, self.item_type, self.sec_type, self.condition[1], self.condition[2], self.condition[3], self.condition[4], self.searchIds)
			end
		else
			if node then
				node.Visible = false
			end
		end
	end
end

local function RefreshListCellData( self, node, index )
	local data = self.list[index]
	local templateID = data.item.TemplateID
    local detail = ItemModel.GetDetailByTemplateID(templateID)
    --名字
	MenuBase.SetLabelText(node, 'lb_name', Util.GetText(detail.static.name), GameUtil.RGB2Color(Constants.QualityColor[detail.static.quality]))
	--图标
	local cvs_item = node:FindChildByEditName('cvs_item', true)
	local num = data.item.Count
	local itshow = UIUtil.SetItemShowTo(cvs_item, detail, num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
    	if string.IsNullOrEmpty(data.item.ID) then	--非装备，直接取模板详情
        	UIUtil.ShowNormalItemDetail({detail = detail, autoHeight = true})
    	else --货架上的装备，向服务器请求装备详情
    		ItemModel.RequestDetailByID(data.item.ID, function(entityDetail)
        		UIUtil.ShowNormalItemDetail({detail = entityDetail, autoHeight = true})
    		end)
    	end
    end
    --单价
	MenuBase.SetLabelText(node, 'lb_money', tostring(data.price), 0, 0)
	--评分/总价
	MenuBase.SetVisibleUENode(node, 'ib_money1', string.IsNullOrEmpty(data.item.ID))
	MenuBase.SetLabelText(node, 'lb_money1', tostring(string.IsNullOrEmpty(data.item.ID) and (data.price * data.item.Count) or data.score), 0, 0)
	--购买按钮
	local buyBtn = node:FindChildByEditName('btn_buy', true)
	buyBtn.TouchClick = function( sender )
		-- AuctionModel.ClientAuctionBuyRequest(data.uuid, data.price, )
		GlobalHooks.UI.OpenUI('AuctionBuy', 0, { item = data, cb = function( ... )
    		_M.RefreshList(self, self.item_type, self.sec_type, self.condition[1], self.condition[2], self.condition[3], self.condition[4], self.searchIds)
		end })
	end
end

function _M.OnRefreshListResult( self, list )
	if list ~= nil and #list > 0 then
		self.ui.comps.lb_noitem.Visible = false
		self.list = list
		self.pan = self.ui.comps.sp_list
		self.pan.Visible = true
		local cell = self.ui.comps.cvs_listinfo
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.list, function(node, index)
			RefreshListCellData(self, node, index)
		end)
		if self.lastPos then
			self.pan.Scrollable:LookAt(-self.lastPos)
		end
        self.pan.Scrollable.event_Scrolled = function( panel, e )
        		-- print('sss', panel.Container.Height, panel.Container.Y, panel.ScrollRect2D.height)
        	if panel.Container.Height + panel.Container.Y <= panel.ScrollRect2D.height - 80 and self.EndDrag then
        		-- print('sss', panel.Container.Height, panel.Container.Y, panel.ScrollRect2D.height)
    			_M.RefreshList(self, self.item_type, self.sec_type, self.condition[1], self.condition[2], self.condition[3], self.condition[4], self.searchIds, true)
        	end
        	self.EndDrag = false
        end
        self.pan.Scrollable.event_OnEndDrag = function(panel, e)
        	-- print('eee', panel.Container.Height, panel.Container.Y, panel.ScrollRect2D.height)
        	self.EndDrag = true
        end
	else
		self.ui.comps.sp_list.Visible = false
		self.ui.comps.lb_noitem.Visible = true
	end
end

function _M.RefreshList( self, itemType, secType, pro, level, quality, star_level, tpltList, isNext )
	if isNext and self.isFull then
		return
	end
	self.ui.comps.lb_fight.Visible = itemType == 2 or itemType == 0
	self.ui.comps.lb_allprice.Visible = itemType ~= 2 and itemType ~= 0
	self.ui.comps.cvs_sift.Visible = itemType == 2
	self.ui.comps.tb_tips.Visible = itemType ~= 2
	local lastId = (isNext and self.list and #self.list > 0) and self.list[#self.list].uuid or nil
	local part = isNext and self.part + 1 or 0
	-- print('qqqqqqqqqqqqqq', isNext, self.part, part)
	pro = (secType == 7 or secType == 8) and 0 or pro
    AuctionModel.ClientGetAuctionItemListRequest(itemType, secType, pro, level, quality, star_level, tpltList, part, lastId, function( rsp )
    	self.part = part
    	self.isFull = rsp.s2c_isFull
    	local list = {}
    	if isNext then
    		if self.list ~= nil and #self.list > 0 then
    			list = self.list
    			for i = 1, #rsp.s2c_list do
    				table.insert(list, rsp.s2c_list[i])
    			end
    		end
			self.lastPos = self.pan.Scrollable.Container.Position2D
    	else
    		list = rsp.s2c_list
			self.lastPos = nil
    	end
    	_M.OnRefreshListResult(self, list)
    end)
end

local function DoSearchItem( self, name )
	-- print('-----DoSearchItem----', name, #self.searchdb)
	if not string.IsNullOrEmpty(name) then
		local ids = {}
		for i = 1, #self.searchdb do
			local itemName = Util.GetText(self.searchdb[i].name)
			if string.find(itemName, name) then
				table.insert(ids, self.searchdb[i].id)
			end
		end
		self.searchIds = ids
    	if #self.searchIds > 0 then
    		_M.RefreshList(self, self.item_type, self.sec_type, self.condition[1], self.condition[2], self.condition[3], self.condition[4], self.searchIds)
    	else
			_M.OnRefreshListResult(self, nil)
    	end
	else
		self.searchIds = {}
    	_M.RefreshList(self, self.item_type, self.sec_type, self.condition[1], self.condition[2], self.condition[3], self.condition[4], self.searchIds)
	end
end

local function OnSelect(self, data)
	-- print_r('ssssssssssssssss', data.item_type, data.sec_type)
	self.item_type = data.item_type
	self.sec_type = data.sec_type
	--刷新列表
    ClearSearch(self)
    _M.RefreshList(self, self.item_type, self.sec_type, self.condition[1], self.condition[2], self.condition[3], self.condition[4], self.searchIds)

    --设置模糊查找索引
    --1.根据筛选条件查询（开启这段要放到下拉列表选中时更新）
 --    local equipdb = GlobalHooks.DB.Find('Equip', { profession = self.condition[1], equip_pos = self.sec_type })
 --    local equiptb = {}
 --    for i = 1, #equipdb do
 --    	local id = equipdb[i].id
 --    	equiptb[id] = id
 --    end
	-- print_r('yyyyyyyyyyyyyyyyy', equiptb)
 --    local dbc = GlobalHooks.DB.FindFirst('trade_condition', { condition_id = 2, enumerate_min = self.condition[2] })
 --    print('ttttttttttttt', data.item_type, data.sec_type, self.condition[3], self.condition[4], dbc.enumerate_min, dbc.enumerate_max)
 --    self.searchdb = GlobalHooks.DB.Find('Item', { item_type = data.item_type, sec_type = data.sec_type, quality = self.condition[3], star_level = self.condition[4], 
 --    	id = function(i) return equiptb[i] end, level_limit = function(l) return l >= dbc.enumerate_min and l <= dbc.enumerate_max end })
	-- print_r('zzzzzzzzzzzzz', self.searchdb)

	--2.无视筛选条件查询（只根据物品分类搜索）
	if data.item_type == 0 then
    	self.searchdb = GlobalHooks.DB.Find('Item', {})
	else
    	self.searchdb = GlobalHooks.DB.Find('Item', { item_type = data.item_type, sec_type = data.sec_type })
	end
	-- print_r('zzzzzzzzzzzzz', #self.searchdb)
	self.select_data = data
end

function _M.OnEnter( self, param )
	self.part = 0
    --pro
    SetCondition(self, 1, DataMgr.Instance.UserData.Pro)
    --level
    local level = DataMgr.Instance.UserData.Level
    local dbc = GlobalHooks.DB.Find('trade_condition', { condition_id = 2 })
    local cLevel
    if level < dbc[1].enumerate_min then
    	cLevel = dbc[1].enumerate_min
    elseif level > dbc[#dbc].enumerate_max then
    	cLevel = dbc[#dbc].enumerate_max
    else
	    for i = 1, #dbc do
	    	if level >= dbc[i].enumerate_min and level <= dbc[i].enumerate_max then
    			cLevel = dbc[i].enumerate_min
	    		break
	    	end
	    end
    end
    SetCondition(self, 2, cLevel)
    --quality
    dbc = GlobalHooks.DB.Find('trade_condition', { condition_id = 3 })
    SetCondition(self, 3, dbc[1].enumerate_min)
    --starlevel
    dbc = GlobalHooks.DB.Find('trade_condition', { condition_id = 4 })
    SetCondition(self, 4, dbc[1].enumerate_min)

    if not self.first_element:IsEnable() then
    	self.first_element:SetEnableAndInvoke(true)
    else
		OnSelect(self, self.select_data)
    end
end

function _M.OnExit( self )

end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.part = 0
	self.condition = {}
	--init left list
    local db_type = GlobalHooks.DB.GetFullTable('trade_type')
    self.treeMenu = UIUtil.CreateTreeMenu()
    for i = 1, #db_type do
    	local dbt = db_type[i]
        local sub = self.treeMenu:AddChild(Util.GetText(dbt.type_name))
        sub:SetUserTag(dbt.type_id)
    	for j = 1, #dbt.contain.type do
    		local typeId = dbt.contain.type[j]
    		if typeId > 0 then
	    		local dbb = GlobalHooks.DB.FindFirst('trade_bank', { type_id = typeId })
	    		local element = sub:AddChild(Util.GetText(dbb.type_name), function()
		            OnSelect(self, dbb)
	    		end)
        		element:SetUserTag(typeId)
		        if not self.first_element then
		            self.first_element = element
		        end
    		end
    	end
    end
    self.treeMenu:Show(self.comps.tbt_sub, self.comps.tbt_sub1, nil, 8, 0, 4)
    
    --init top list
    self.ui.comps.btn_job.TouchClick = function( sender )
    	ShowFilterOption(self, sender, 1)
    end
    self.ui.comps.btn_level.TouchClick = function( sender )
    	ShowFilterOption(self, sender, 2)
    end
    self.ui.comps.btn_quality.TouchClick = function( sender )
    	ShowFilterOption(self, sender, 3)
    end
    self.ui.comps.btn_type.TouchClick = function( sender )
    	ShowFilterOption(self, sender, 4)
    end
    self.ui.comps.cvs_select.event_PointerUp = function( sender )
    	sender.Visible = false
    end

    --input
    self.ui.comps.ti_search.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('trade_search_num')
    self.ui.comps.btn_search.TouchClick = function( sender )
    	DoSearchItem(self, self.ui.comps.ti_search.Input.Text)
    end

    self.ui.comps.cvs_listinfo.Visible = false
end

return _M