--衣柜系统

local _M = {}
_M.__index = _M
-- print('-------------load WardrobeMain---------------------')
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local AvatarModel = require 'Model/AvatarModel'
local ItemModule = require 'Model/ItemModel'
local ServerTime = require 'Logic/ServerTime'
local ItemModel = require 'Model/ItemModel'
local PlayerScale = 175

local function CanGetEffect(self,parent)
	local t = {
		LayerOrder = self.menu.MenuOrder,
		UILayer = true,
		--DisableToUnload = true,
		Parent = parent.Transform,
		Scale = Vector3(0.6,1.5,1),
		Pos = {x = parent.Width/2, y = -parent.Height/2 + 2, z= -600},
		 
	}
	return Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t,true)
end

local function UnlockEffect(self,parent)
	local transSet = TransformSet()
	transSet.Pos = Vector2(parent.Width/2,-parent.Height/2)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_yigui_unlock.assetbundles'
	local id = RenderSystem.Instance:PlayEffect(assetname, transSet)
	return id
end

local function UpgradeEffect(self,parent)
	local transSet = TransformSet()
	transSet.Pos = Vector2(parent.Width/2,-parent.Height/2)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_yigui_upgrade.assetbundles'
	local id = RenderSystem.Instance:PlayEffect(assetname, transSet)
	return id
end

local function GetRequireList(self,avatarType,sheetId)
	local requireList
	local requireData = self.requireDataMap[avatarType]
	if requireData ~= nil and requireData[sheetId] ~= nil and requireData[sheetId].requireList ~= nil then
		requireList = requireData[sheetId].requireList 
	end 
	return requireList
end  

local function GetCostData(groupData)
	local allCost = {}
 	local items = ItemModule.ParseCostAndCostGroup(groupData)	 
	if items then
		for i,v in ipairs(items) do
			local item = v.detail
			local data = {}
			if v.cur >= v.need then
				data.result = true
			else 
				data.result = false
			end
			
			data.desc = Util.GetText(item.static.name)  
			data.value = v.cur .. '/' .. v.need
			table.insert(allCost,data)
		end 
	end
	return allCost
end
 
local function GetRequireCostData(requireList,groupData)
	local canGet = true
	local allData = {}
	for k,v in pairs(requireList or {}) do
		 
		local data = {}
		data.result = v.result
		data.desc = v.reason
		data.value = v.curVal .. '/' .. v.minVal 			
		table.insert(allData,data)

		if not v.result then
			canGet = false
		end 
	end

	local  allCost = GetCostData(groupData)
	for k,costData in pairs(allCost) do 
		 
		table.insert(allData,costData)
		if not costData.result then
			canGet = false
		end 
	end
 
	return allData,canGet
end 
local function IsAvatarShowGray(self,avatarId)
	local dayInfo = self.dayMap[avatarId]
	local isGray = false
	if dayInfo == nil then
		isGray = true
	else
		--TODO 显示剩余天数 
		-- if dayInfo:Equals(System.DateTime.MaxValue) or dayInfo:Subtract(ServerTime.getServerTime()).TotalSeconds > 0 then
		if dayInfo.Year == System.DateTime.MaxValue.Year or dayInfo:Subtract(ServerTime.getServerTime()).TotalSeconds > 0 then 
			isGray = false
		else 
			isGray = true
		end
	end
	return isGray
end

local function IsCanGetItem(self,avatarShowData)
	local avatarId = avatarShowData.id
	local groupDatas = AvatarModel.GetAvatarGroupDatasByAvatarId(avatarId)
	for _,groupData in ipairs(groupDatas) do
		-- avatarShowData.avatar_type 等价与selected
		local requireList = GetRequireList(self,avatarShowData.avatar_type,sheetId)
		local _, canGet = GetRequireCostData(requireList,groupData)
		 
		if canGet then
			return true
		end
	end
	return false
end

local function Release3DBGModel(self)
	if self and self.bgModel then
		UI3DModelAdapter.ReleaseModel(self.bgModel.Key)
		self.bgModel = nil
	end
end

local function Init3DBGModel(self, parentCvs, pos2d, scale, rotate,menuOrder,fileName)
    local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	info.Callback = function (info2)
		info2.RootTrans.localPosition = Vector3(0,0,500)
	end
	self.bgModel = info

end
 
local function ReleaseAvatarModel(self)
	if self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

--人物
local function Init3DModel(self, parentCvs, pos2d, scale, menuOrder, avatar, filter,modleAction)
	local info = UI3DModelAdapter.AddAvatar(parentCvs, pos2d, scale, menuOrder, avatar, filter)
	self.model = info
	local trans = info.RootTrans
	info.Callback = function (info2)
		if self.playerRotation  then
			trans.rotation = self.playerRotation 
			
		end
		if not string.IsNullOrEmpty(modleAction) then
			info.Anime:Play(modleAction)
		end 
		trans.localScale = Vector3.one * scale
		-- print_r('Vector3.one * scale:',Vector3.one * scale)
		-- info.DynamicBoneEnable = false
	end
 
	parentCvs.event_PointerMove = function(sender, data)
 		local delta = -data.delta.x
		trans:Rotate(Vector3.up, delta * 1.2)
		self.playerRotation = trans.rotation
	end
end

local function InitDefaultModel(self)
	-- body
	ReleaseAvatarModel(self)
	local pos2d = self.ui.comps.cvs_anchor.Position2D
	local avatar = DataMgr.Instance.UserData:GetAvatarList()
   	local filter = bit.lshift(1,  GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))  	
	Init3DModel(self, self.cvsModel, pos2d, PlayerScale, self.ui.menu.MenuOrder,avatar , filter)
end

--坐骑
local function Init3DSngleModel(self, parentCvs, pos2d, scale, rotate,menuOrder,fileName,modleAction)
	parentCvs.Visible = false
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	self.model = info
	local trans = info.RootTrans
	info.Callback = function (info2)
		-- body
		parentCvs.Visible = true
		local trans2 = info2.RootTrans
		
		if self.mountRotation then
			trans2.rotation = self.mountRotation 
		else
			trans2:Rotate(Vector3.up,rotate)
		end
		if not string.IsNullOrEmpty(modleAction) then
			info.Anime:Play(modleAction)
		end 
	end
	
	parentCvs.event_PointerMove = function(sender, data)
		local delta = -data.delta.x
		trans:Rotate(Vector3.up, delta * 1.2)
		self.mountRotation = trans.rotation
	end
end

local function ShowPlayer(self,modleAction)
	-- body
	local euipMap = {}
	for k,euqipData in pairs(self.tryEquipMap or {}) do
		-- print_r('vvvvvvvvvvvvvvvvv:',euqipData)
		if euqipData ~= nil then
			for _,model in pairs(euqipData.models or {}) do
				-- print_r('sssssssssssssssssssssssssssss:',model)
				euipMap[model.partTag] = model.assetname
			end
		end
	end

	local pos2d = self.ui.comps.cvs_anchor.Position2D
	local avatar = DataMgr.Instance.UserData:GetNewAvatar(euipMap)
	local filter = bit.lshift(1,  GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))  	
	Init3DModel(self, self.cvsModel, pos2d, 200, self.ui.menu.MenuOrder,avatar , filter,modleAction)
end

local function ReloadModel(self,avatarShowData)
	if self == nil then
		return
	end

	ReleaseAvatarModel(self)

	-- if avatarShowData ~= nil and (avatarShowData.avatar_type == 0 or avatarShowData.avatar_type == 6)then
	if avatarShowData ~= nil and avatarShowData.avatar_type == 0 then
		
  		-- 坐骑
  		local pos2d = self.ui.comps.cvs_anchor.Position2D
		-- local filename = '/res/unit/' .. modelData.avatar_res .. '.assetbundles'
		local filename
		for k,v in pairs(avatarShowData.showmodle.key) do
			local model =  avatarShowData.showmodle.value[k]
			if not string.IsNullOrEmpty(model) then
        		filename = model 
        	end
   	 	end 
 		-- print('ReloadModel avatar filename:',filename)

   	 	if filename ~= nil and string.len(filename) > 0 then
			local fixzoom = tonumber(avatarShowData.zoom) -- 缩放比例
			local rotate = avatarShowData.rotate

			local realFileName = filename
			if avatarShowData.avatar_type == 0 then
				realFileName = '/res/unit/' .. filename .. '.assetbundles'
			end

    		Init3DSngleModel(self,self.cvsModel,pos2d, fixzoom,rotate, self.ui.menu.MenuOrder,realFileName,avatarShowData.modle_action)
    	end
	else
		if avatarShowData then 
			ShowPlayer(self,avatarShowData.modle_action)
		else
			ShowPlayer(self)
		end
	end
end
 
local function ShowRequireDescScrollPan(self,allData)
	-- print_r('ShowRequireDescScrollPan:',allData)
	self.ui.comps.cvs_need.Visible = false
    local function eachUpdateCb1(node, index)
		-- print('ShowRequireDescScrollPan:',index)
       local data = allData[index]
       local color = 0xff0000
       if data.result then
       		color = 0xffffff
       end

       local needLabel = UIUtil.FindChild(node,'lb_needtex')
       needLabel.Text = data.desc
       -- needLabel.FontColorRGB = color
       local neednumLabel = UIUtil.FindChild(node,'lb_neednum')
       neednumLabel.Text = data.value
       neednumLabel.FontColorRGB = color
    end
    UIUtil.ConfigVScrollPan(self.ui.comps.sp_need,self.ui.comps.cvs_need,#allData,eachUpdateCb1)
end


local function ShowItemSheetDetial(self,sheetID)
	self.selectSheetId = sheetID
	local groupData = AvatarModel.GetAvatarGroupDataBySheetId(sheetID)
 	if groupData.score > 0 then
		self.scoreLabel.Text = "+"..groupData.score
	else
		self.scoreLabel.Text = ""
	end

	local requireList = GetRequireList(self,self.selectAvatarType,sheetID)
	local allData = GetRequireCostData(requireList,groupData)

 	ShowRequireDescScrollPan(self,allData)
end

-- 7 天 14 天 永久的按钮
local function SheetTogButton(self,avatarId)

	for i=1,3 do
		self.desctbts[i].Visible = false
	end

	-- 3天 7天 永久
	local function SheetTogFunc(sender)
		 local sheetId = sender.UserTag
		 
		 ShowItemSheetDetial(self,sheetId)
	end

	self.ui.comps.ib_alltime.Visible = false

	local newsubtbs = {}
	local groupDatas = AvatarModel.GetAvatarGroupDatasByAvatarId(avatarId)
	if #groupDatas == 1 then
		self.ui.comps.ib_alltime.Visible = true
		local sheet_id = groupDatas[1].sheet_id
		ShowItemSheetDetial(self,sheet_id)
	else
		for k,v in ipairs(groupDatas) do
			local togButton = self.desctbts[k] 
			if togButton then
				togButton.Visible = true
				togButton.Text = Util.GetText(v.tab_desc)
				togButton.UserTag = v.sheet_id
				table.insert(newsubtbs,togButton)
				
			end
		end

		UIUtil.ConfigToggleButton(newsubtbs, self.defaultdesctb, false, SheetTogFunc)
	end
end


local function ShowGray(self,node,isGray)
	local huiImg = UIUtil.FindChild(node,'ib_hui')
	local lockImg = UIUtil.FindChild(node,'ib_suo')
	huiImg.Visible = isGray
	lockImg.Visible = isGray
end

local function ShowLocked(self,node)
	-- body
	self.cvsyongjiu.Visible = false
	self.cvslinshi.Visible = true
	
	self.buyBtn.Visible = true
	self.takeOn.Visible = false
	self.buy2Btn.Visible = false
	self.leftTimeLabel.Visible = false
end 

local function showLinshi(self,day)
	-- body
	self.cvsyongjiu.Visible = false
	self.cvslinshi.Visible = true

	self.buyBtn.Visible = false
	self.takeOn.Visible = true
	self.buy2Btn.Visible = true

	self.leftTimeLabel.Visible = true
	self.leftTimeLabel.Text = Util.GetText(Constants.Text.wardobe_dayLeftText,day) --'剩余:' .. day .. '天'
end  

local function showYongjiu(self)
	
	self.cvsyongjiu.Visible = true
	self.cvslinshi.Visible = false
end


local function ShowLockStatus(self,node,avatarId)
	local dayInfo = self.dayMap[avatarId]
	-- -1是永久 还有7天  14天
	if dayInfo == nil then
		ShowLocked(self,node)
	else

		-- 隐藏初始时装的装备和卸下按钮
		local isInitAvatar = AvatarModel.GetAvatarInit(avatarId) or false

		self.takeOn.Visible = not isInitAvatar
		self.yongjiuBtn.Visible = not isInitAvatar
		
		-- if dayInfo:CompareTo(System.DateTime.MaxValue) then
		if dayInfo.Year == System.DateTime.MaxValue.Year then
			showYongjiu(self)

			local avatar_type = self.selectAvatarType
			if self.equipMap[avatar_type] == avatarId then
				self.takeOn.Text = Constants.Text.wardobe_takeOffText --'卸下'     
				self.yongjiuBtn.Text = Constants.Text.wardobe_takeOffText --'卸下'
			else
				self.takeOn.Text = Constants.Text.wardobe_takeOnText --'穿戴'
				self.yongjiuBtn.Text = Constants.Text.wardobe_takeOnText --'穿戴'
			end
			
		else
			local timeSpan =  dayInfo:Subtract(ServerTime.getServerTime())
            if timeSpan.TotalSeconds > 0 then
            	local day = math.floor(timeSpan.TotalDays) + 1
				showLinshi(self,day)

				local avatar_type = self.selectAvatarType
				if self.equipMap[avatar_type] == avatarId then
					self.takeOn.Text = Constants.Text.wardobe_takeOffText --'卸下'     
					self.yongjiuBtn.Text = Constants.Text.wardobe_takeOffText --'卸下'
				else
					self.takeOn.Text = Constants.Text.wardobe_takeOnText --'穿戴'
					self.yongjiuBtn.Text = Constants.Text.wardobe_takeOnText --'穿戴'
				end
				
			else
				ShowLocked(self,node)

				self.takeOn.Visible = false
				self.yongjiuBtn.Visible = false
				
			end
		end
 
	end
end

local function SetAvatarShowItemIconBg(self,node,avatarShowData)
	local avatarId = avatarShowData.id

	local isGray = IsAvatarShowGray(self,avatarId)
    ShowGray(self,node,isGray)

	self.CanGetEffMap = self.CanGetEffMap or {}
 
  	--未拥有 可获得
	if isGray and IsCanGetItem(self,avatarShowData) then
		if not self.CanGetEffMap[avatarId] then
 			self.CanGetEffMap[avatarId] = CanGetEffect(self,node)
 		end
	end

end 

local function SetSelectAvatarShow(self,index,node,selectedImg,avatarShowData)
	local avatarId = avatarShowData.id
	self.selectAvatarId = avatarId
	self.selectAvatarNode = node
	self.selectedImg = selectedImg

	self.selectedImg.Visible = true

	self.ui.comps.cvs_frame2.Visible = true
	self.ui.comps.cvs_linshi.Visible = true
	self.ui.comps.cvs_yongjiu.Visible = true

	self.nameLabel.Text = Util.GetText(avatarShowData.name)
	 
    self.descLabel.Text = Util.GetText(avatarShowData.introduce_1)
    

    ShowLockStatus(self,node,avatarId)

	SheetTogButton(self,avatarId)

	ReloadModel(self,avatarShowData)
end



local function TryTakeOnAvatarShow(self,index,avatarShowData)
	local equipData = {}
	equipData.index = index
	equipData.id = avatarShowData.id
	local models = AvatarModel.GetAvatarModelData(avatarShowData)
	equipData.models = models 
	self.tryEquipMap[avatarShowData.avatar_type] = equipData
end

local function SetAvatarShowToItemShow(self,node,index,avatarShowData,selectedImg)
  -- body

	SetAvatarShowItemIconBg(self,node,avatarShowData)

	local cvsItem = UIUtil.FindChild(node,'cvs_item1') 
	local selectedImg = UIUtil.FindChild(node,'ib_xuan')
	selectedImg.Visible = false

  	local itemDetial =  ItemModel.GetDetailByTemplateID(avatarShowData.item_id)
    
 
    local itShow = UIUtil.SetItemShowTo(cvsItem,itemDetial.static.atlas_id, itemDetial.static.quality)
    node:SetChildIndex(itShow,0)
    itShow.EnableTouch = true
    itShow.TouchClick = function ( ... )
        if self.defaultIndex == index then
			return
		end

		if self.selectedImg then
			self.selectedImg.Visible = false
		end

		self.defaultIndex = index
		TryTakeOnAvatarShow(self,index,avatarShowData)

		SetSelectAvatarShow(self,index,node,selectedImg,avatarShowData)

    end
end

local function ShowAvatarShowItemData(self,node,index)
	-- body
	local avatarShowData = self.AvatarShowDataTable[index]
	if avatarShowData == nil then
		node.Visible = false
		return
	end
	
	SetAvatarShowToItemShow(self,node,index,avatarShowData)

	local chuandaiLabel = UIUtil.FindChild(node,'lb_chuandaizhong')
	local jiayuLabel = UIUtil.FindChild(node,'lb_jiayuzhong')
	chuandaiLabel.Visible = false
	jiayuLabel.Visible = false
 
	if self.equipMap[avatarShowData.avatar_type] == avatarShowData.id then
		if avatarShowData.avatar_type == 0 then
			chuandaiLabel.Visible = true
		else
			jiayuLabel.Visible = true
		end
	end
end

local function SelectByIndex(self, index)
    UIUtil.MoveToScrollCell(self.itemPan,index,function(node)
        if self.selectedImg then
			self.selectedImg.Visible = false
		end

       	local avatarShowData = self.AvatarShowDataTable[index]
        if not avatarShowData then
        	return
        end

		local selectedImg = UIUtil.FindChild(node,'ib_xuan')
		selectedImg.Visible = false
		self.defaultIndex = index
		SetSelectAvatarShow(self,index,node,selectedImg,avatarShowData)

		if self.newLockAvatarId and self.newLockAvatarId == avatarShowData.id then
			self.UnlockEffMap = self.UnlockEffMap or {}
			self.UnlockEffMap[avatarShowData.id] = UnlockEffect(self,node)
			self.newLockAvatarId = nil
		end
    end)
end

local function ClearEff(self)

	for _,effId in pairs(self.CanGetEffMap or {}) do
		if effId then
			RenderSystem.Instance:Unload(effId)
		end
	end
	self.CanGetEffMap = {}
	
	for _,effId in pairs(self.UnlockEffMap or {}) do
		if effId then
			RenderSystem.Instance:Unload(effId)
		end
	end
	self.UnlockEffMap = {}
	
end  

 

local function ShowAvatarItemScrollPan(self,avatarType)

	ClearEff(self)
 
    local function eachAvatarUpdateCb(node, index) 
        ShowAvatarShowItemData(self,node,index)
    end
  
    local col = 4
    if #self.AvatarShowDataTable < 4 then
    	col = #self.AvatarShowDataTable
    end

    -- print_r('self.AvatarShowDataTable:',self.AvatarShowDataTable)

    local spacingX = 0
    local spacingY = 0
    UIUtil.ConfigGridVScrollPanWithOffset(self.itemPan,self.itemTempnode,col,#self.AvatarShowDataTable,spacingX,spacingY,eachAvatarUpdateCb)
    if self.defaultIndex > 0 then
    	SelectByIndex(self,self.defaultIndex)
    end
end

local function SubTypeTouchClick(self,avatarType)
	if self.selectedImg then
		self.selectedImg.Visible = false
	end
 
	AvatarModel.ReqGetWardrobeInfo(avatarType,function (resp)
		-- body
		self.requireDataMap[avatarType] = resp.s2c_requireMap
	 		
	 	self.defaultIndex = 1
	 	self.ui.comps.cvs_frame2.Visible = false
		self.ui.comps.cvs_linshi.Visible = false
		self.ui.comps.cvs_yongjiu.Visible = false

	    self.AvatarShowDataTable = AvatarModel.GetAvatarShowDataTable(avatarType)
	  
	    local equipData = self.tryEquipMap[avatarType]
		if  equipData then
			self.defaultIndex = equipData.index 
		elseif self.equipMap[avatarType] then
			local avatarId = self.equipMap[avatarType] 
			for index,avatarShowData in pairs(self.AvatarShowDataTable) do
				-- print_r(index,v)
				if avatarShowData.id == avatarId then 
					TryTakeOnAvatarShow(self,index,avatarShowData)
					self.defaultIndex = index
				end
			end
		else
			local avatarShowData = self.AvatarShowDataTable[1]
			if avatarShowData then
				self.defaultIndex = 1 
				TryTakeOnAvatarShow(self,self.defaultIndex,avatarShowData)
			else 
				self.defaultIndex = -1
			end
		end

		ShowAvatarItemScrollPan(self,avatarType)
	end)
end


local function ShowMaxScore(self)
	self.beforelevelLabel.Text = self.Level
	self.maxScore = AvatarModel.GetAvatarLevelScore(self.Level)

	if self.Score >= self.maxScore then
		--TODO  小红点
		GlobalHooks.UI.SetRedTips("wardrobe", 1)
		if not self.UpgradeEffectId then
			self.UpgradeEffectId = UpgradeEffect(self,self.ui.comps.btn_lift)
		end
	else
		GlobalHooks.UI.SetRedTips("wardrobe", 0)
		if self.UpgradeEffectId then
			RenderSystem.Instance:Unload(self.UpgradeEffectId)
			self.UpgradeEffectId = nil
		end	
	end

	self.totalScoreLabel.Text = self.Score .. '/' .. self.maxScore
end  

local function UpdateScore(self,newScore)
	self.Score = newScore
	if self.Score >= self.maxScore then
		--TODO  小红点
		GlobalHooks.UI.SetRedTips("wardrobe", 1)
		if not self.UpgradeEffectId then
			self.UpgradeEffectId = UpgradeEffect(self,self.ui.comps.btn_lift)
		end
	else
		GlobalHooks.UI.SetRedTips("wardrobe", 0)
		if self.UpgradeEffectId then
			RenderSystem.Instance:Unload(self.UpgradeEffectId)
			self.UpgradeEffectId = nil
		end	
	end

	self.totalScoreLabel.Text = self.Score .. '/' .. self.maxScore
end 

local function OnWardrobeLevelUp(self,eventname, params)
  	self.Level= params.s2c_Level 
	SoundManager.Instance:PlaySoundByKey('jinjie',false)
  	ShowMaxScore(self)
end


local function ShowFrame(self, withAnime)
	-- body
	-- print('ShowFrame self.MoveNode.X :',self.MoveNode.X)
	MenuBase.SetVisibleUENode(self.MoveNode, true);
	if withAnime then
		local ma = MoveAction()
    	ma.Duration = 0.2
    	ma.TargetX = self.MoveNodeStartX
    	-- print('ma.TargetX:',ma.TargetX)
    	ma.TargetY =  self.MoveNode.Y
    	self.MoveNode:AddAction(ma);
    else
    	self.MoveNode.X = self.MoveNodeStartX
    end
end

local function HideFrame(self, withAnime )
	-- print('HideFrame self.MoveNode.X :',self.MoveNode.X)

	if withAnime then
    	local ma = MoveAction()
    	ma.Duration = 0.2
    	ma.TargetX = HZUISystem.SCREEN_WIDTH  + HZUISystem.Instance.StageOffsetX-- self.MoveNode.X + self.MoveNode.Width
    	-- print('ma.TargetX:',ma.TargetX)

    	ma.TargetY =  self.MoveNode.Y
    	ma.ActionFinishCallBack = function (sender)
    		-- MenuBase.SetVisibleUENode(self.MoveNode,false)
    	end
    	self.MoveNode:AddAction(ma);
    else
    	self.MoveNode.X =  self.MoveNodeEndX
    	-- MenuBase.SetVisibleUENode(self.MoveNode,false)
    end
end  

local function OnMenuOrderReset( self, eventname, params )
	if self.assetLoader then
		UILayerMgr.SetLayer(self.assetLoader.gameObject, 17)
	end
end

function _M.OnEnable( self )
	-- print('----------OnEnable-----------')
	if self.assetLoader then
		self.assetLoader.gameObject:SetActive(true)
	end
end

function _M.OnDisable( self )
	-- print('----------OnDisable-----------')
	if self.assetLoader then
		self.assetLoader.gameObject:SetActive(false)
	end
end

function _M.OnEnter( self, mainIndex,...)
 
 
    self.playerRotation = nil
	self.mountRotation = nil


	self.defaultmain = self.maintbts[mainIndex] or self.ui.comps.tbt_an_m1
	self.defaultsub = self.ui.comps.tbt_an1

	-- local pos2d = Vector2(0,0)
 --    local filename = '/res/effect/ui/ef_ui_yigui.assetbundles'				--self.ui.comps.cvs_bg.Position2D
 --    Init3DBGModel(self, self.ui.comps.cvs_bg, pos2d , 1,0, self.ui.menu.MenuOrder,filename)
	local path = '/res/effect/ui/ef_ui_yigui.assetbundles'
	local id = FuckAssetObject.GetOrLoad(path, 'ef_ui_yigui', function(assetLoader)
		self.assetLoader = assetLoader
		self.assetLoader.DontMoveToCache = true
		self.assetLoader.gameObject.transform:SetParent(self.ui.comps.cvs_bg.UnityObject.transform)
		local X = self.assetLoader.gameObject.transform.localPosition.x
		local Y = self.assetLoader.gameObject.transform.localPosition.y
		-- local Z = self.ui.menu.UnityObject.transform.localPosition.z
		self.assetLoader.gameObject.transform.localPosition = Vector3(X,Y,0) 
	end)
	self.loader = FuckAssetLoader.GetLoader(id)

    self.requireDataMap = {}
    self.equipMap = {}
    self.tryEquipMap = {}
    self.Score = 0
    self.Level = 1
    self.dayMap = {}

	local function SubTypeFunc(sender)
		local tag = sender.UserTag
		self.subtag = tag
		self.selectedImg = nil
 
		local avatarType = tonumber(sender.UserData)
		self.selectAvatarType = avatarType
		SubTypeTouchClick(self,avatarType)
	end

 	local function MainTypeFunc(sender)
		local tag = sender.UserTag
		self.maintag = tag
		-- print('self.maintag:',self.maintag)
		for k,v in pairs(self.subtbts) do
			v.Visible = false
		end

		local newsubtbs = {}
		local subMenuData = AvatarModel.GetSubMenu(tag)
		for k,v in pairs(subMenuData) do
			local togButton = self.subtbts[k] 
			if togButton then
				togButton.Visible = true
				togButton.Text = Util.GetText(v.secsheet_name)
				table.insert(newsubtbs,togButton)
				togButton.UserData = v.avatar_type
			end
		end

		-- UIUtil.ConfigToggleButton(self.subtbts, self.defaultsub, false, SubTogFunc)
		UIUtil.ConfigToggleButton(newsubtbs, self.defaultsub, false, SubTypeFunc)
	end

	AvatarModel.ReqGetWardrobeData(function (resp)
 		self.Score = resp.s2c_Score
 		self.Level = resp.s2c_Level
  
		ShowMaxScore(self)

		self.dayMap	= resp.s2c_dayMap or {}
		self.equipMap = resp.s2c_equipMap or {}
 
		UIUtil.ConfigToggleButton(self.maintbts, self.defaultmain, false, MainTypeFunc)

	end)

	InitDefaultModel(self)

	_M.eventFun = function( eventname, params )
		OnWardrobeLevelUp(self, eventname, params)
	end

	_M.eventFun2 = function( eventname, params )
		OnMenuOrderReset(self, eventname, params)
	end

	EventManager.Subscribe("Event.WardrobeUI.LevelUp", _M.eventFun)
	EventManager.Subscribe("Event.UI.ResetOrder", _M.eventFun2)
end

function _M.OnExit( self )
	
	ClearEff(self)

	if self.UpgradeEffectId then
		RenderSystem.Instance:Unload(self.UpgradeEffectId)
		self.UpgradeEffectId = nil
	end

	print(' WardrobeMain OnExit ... ')
    self.playerRotation = nil
	self.mountRotation = nil

	if self.assetLoader then 
		self.assetLoader:Unload()
		self.assetLoader = nil
		self.scene = nil
		self.data = nil
	elseif self.loader then
		self.loader:Discard()
	end
	self.loader = nil

   
    ReleaseAvatarModel(self)

    EventManager.Unsubscribe("Event.WardrobeUI.LevelUp", _M.eventFun)
	EventManager.Unsubscribe("Event.UI.ResetOrder", _M.eventFun2)

    Release3DBGModel(self)
end

function _M.OnDestory( self )
	print(' WardrobeMain  OnDestory ... ')
end

local function InitTogbuttons(self )
	-- body

	local maintbts = {}
 	for i=1,5 do
 		local togButton = self.ui.comps['tbt_an_m' .. i]
 		togButton.UserTag = i
 		table.insert(maintbts,togButton)

 		local menuData = GlobalHooks.DB.FindFirst('AvatarMenu',{sheet_id = i})
 		if menuData then 
 			togButton.Text = Util.GetText(menuData.sheet_name)
 		end
 	end

 	self.maintbts = maintbts

  
 	local subtbts = {}
 	for i=1,3 do
 		 local togButton = self.ui.comps['tbt_an' .. i]
 		 togButton.UserTag = i
 		 -- table.insert(subtbts,togButton)
 		 subtbts[i] = togButton
 	end
 	self.subtbts = subtbts

 	local desctbts = {}
 	desctbts[1] = self.ui.comps.tbt_7tian
 	self.defaultdesctb = desctbts[1]
 	desctbts[2] = self.ui.comps.tbt_30tian
 	desctbts[3] = self.ui.comps.tbt_alltime
 	self.desctbts = desctbts
end

local  function BuyAvatar(self)

	local node = self.selectAvatarNode
	local avatarId = self.selectAvatarId
	self.newLockAvatarId = nil

	AvatarModel.ReqestBuyAvatar(self.selectSheetId,function(resp)
		for k,v in pairs(resp.s2c_dayMap or {}) do
			-- print('BuyAvatar:',k,v)
			self.dayMap[k] = v
		end

		UpdateScore(self,resp.s2c_Score)

		self.newLockAvatarId = avatarId

		ShowAvatarItemScrollPan(self,self.selectAvatarType)

		self.getTipCvs.Visible = true

	end,function ()
		local groupData = AvatarModel.GetAvatarGroupDataBySheetId(self.selectSheetId)
		if groupData and groupData.item_goto and groupData.item_goto > 0 then
			local templateId = groupData.item_goto
			local pos2d = self.ui.comps.cvs_bg.Position2D
			local params = {TemplateId = templateId,
          			x = pos2d.x,
          			y = pos2d.y}
  			UIUtil.ShowGetItemWay(params)
		end
	end)
end

local  function tryEquip(self)
	-- body
	local node = self.selectAvatarNode
	local avatarId = self.selectAvatarId
	local avatarType = self.selectAvatarType
	if self.equipMap[avatarType] == avatarId then
		
		--卸下
		local takeOff = 1
 		AvatarModel.ReqTakeOnAvatar(takeOff,avatarId,function (resp)
 			-- body
 			self.equipMap[avatarType] = nil
 			-- self.tryEquipMap[avatar_type] = nil
 			self.takeOn.Text = Constants.Text.wardobe_takeOnText		--'穿戴'
			self.yongjiuBtn.Text = Constants.Text.wardobe_takeOnText	--'穿戴'

			if node then
				local chuandaiLabel = UIUtil.FindChild(node,'lb_chuandaizhong')
				local jiayuLabel = UIUtil.FindChild(node,'lb_jiayuzhong')
				chuandaiLabel.Visible = false
				jiayuLabel.Visible = false
			end
 
 		end)
    else
    	--穿上
     	local takeOff = 0
 		AvatarModel.ReqTakeOnAvatar(takeOff,avatarId,function (resp)
 			-- body
 			self.equipMap[avatarType] = avatarId
 		-- 	self.takeOn.Text = Constants.Text.wardobe_takeOffText			-- '卸下'
			-- self.yongjiuBtn.Text = Constants.Text.wardobe_takeOffText		-- '卸下'

			ShowAvatarItemScrollPan(self,avatarType)

 		end)
    end
end


function _M.OnInit( self )
	-- print(' WardrobeMain OnInit')
 
 	PlayerScale = GlobalHooks.DB.GetGlobalConfig('avatar_zoom') or 175

    self.ui.menu.ShowType = UIShowType.HideBackAll
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
    -- self.ui:EnableTouchFrame(false)
    
    self.ui.comps.cvs_frame2.Visible = false
	self.ui.comps.cvs_linshi.Visible = false
	self.ui.comps.cvs_yongjiu.Visible = false

    self.cvsModel = self.ui.comps.cvs_model
     
    self.beforelevelLabel = self.ui.comps.lb_num
    self.totalScoreLabel = self.ui.comps.lb_wenzi2

    self.scoreLabel = self.ui.comps.lb_total2
    self.nameLabel = self.ui.comps.lb_name
    self.descLabel = self.ui.comps.tb_tips

    self.getTipCvs = self.ui.comps.cvs_attention
    self.ui.menu:SetUILayer(self.getTipCvs,self.ui.menu.MenuOrder+1,-800)
    self.getTipCvs.TouchClick = function ( sender )
    	self.getTipCvs.Visible = false
    end
    --这是个取消按钮
    self.ui.comps.bt_get.TouchClick = function (sender)
    	-- body
    	self.getTipCvs.Visible = false
    end
    self.ui.comps.btn_close2.TouchClick = function ( sender)
    	-- body
    	self.getTipCvs.Visible = false
    end

    self.ui.comps.bt_wear.TouchClick = function (sender)
    	-- body
    	self.getTipCvs.Visible = false
    	--todo 穿戴
    	local avatarId = self.selectAvatarId
		local takeOff = 0
		local avatarType = self.selectAvatarType
 		AvatarModel.ReqTakeOnAvatar(takeOff,avatarId,function (resp)
 			self.equipMap[avatarType] = avatarId
			ShowAvatarItemScrollPan(self,avatarType)
 		end)
    end

    self.liftBtn = self.ui.comps.btn_lift
    self.liftBtn.TouchClick = function ( sender )
    	-- body
    	GlobalHooks.UI.OpenUI("WardrobeDetial",0,self.Score,self.Level)
    end

	self.cvsyongjiu = self.ui.comps.cvs_yongjiu
	self.cvsyongjiu.Visible = false

	self.yongjiuBtn = self.ui.comps.btn_yongjiu
	-- 穿戴卸下
    self.yongjiuBtn.TouchClick = function (sender)
     	tryEquip(self)
    end

    self.cvslinshi = self.ui.comps.cvs_linshi
    self.cvslinshi.Visible = false
    -- 穿戴卸下
    self.takeOn = self.ui.comps.btn_linshi
    self.takeOn.TouchClick = function (sender)
		tryEquip(self)
    end

    self.leftTimeLabel = self.ui.comps.lb_lefttime
    --激活
    self.buyBtn = self.ui.comps.btn_unlock
    self.buyBtn.TouchClick = function (sender)
    	-- body
    	BuyAvatar(self)
    end

    --续费
    self.buy2Btn = self.ui.comps.btn_xufei
    self.buy2Btn.TouchClick = function (sender  )
    	-- body
    	BuyAvatar(self)
    end
 
 
	self.itemTempnode = self.ui.comps.cvs_item
	self.itemTempnode.Visible = false
	self.itemPan = self.ui.comps.sp_itemlist

	InitTogbuttons(self)


	self.MoveNode = self.ui.comps.cvs_frame3
	self.MoveNodeStartX = self.MoveNode.X
	local hideBtn = self.ui.comps.tbt_retract
	hideBtn.TouchClick = function (sender)
		-- print('hideBtn.IsChecked:',hideBtn.IsChecked)
    	if hideBtn.IsChecked then
    		HideFrame(self,true)
    	else 
    		ShowFrame(self,true)
    	end
    end

  
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.btn_close, bit.bor(HudManager.HUD_RIGHT,HudManager.HUD_TOP))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_frame1, bit.bor(HudManager.HUD_LEFT,HudManager.HUD_TOP))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_frame2, bit.bor(HudManager.HUD_LEFT))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_frame3, bit.bor(HudManager.HUD_RIGHT))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_btnlist, bit.bor(HudManager.HUD_LEFT,HudManager.HUD_BOTTOM))



end

return _M