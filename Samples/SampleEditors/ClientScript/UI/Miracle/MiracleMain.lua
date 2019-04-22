--神器

local _M = {}
_M.__index = _M
 
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local ArtifactModel = require 'Model/ArtifactModel'
local SkillModel = require 'Model/SkillModel'

local effectZ = -250

local mainPos = 5
local secondPos = 6

local function CanGetEffect(self,parent)
	local transSet = TransformSet()
	transSet.Pos = Vector2(parent.Width/2,-parent.Height/2)
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	-- transSet.Vectormove = Vector2(parent.Width,parent.Height)
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_frame_01.assetbundles'
	local id = RenderSystem.Instance:PlayEffect(assetname, transSet)
 	return id
end


local function ActiveEffect(self,parentCvs,pan,pos2d,scale,menuOrder,fileName)

	pos2d.y = -pos2d.y
	local param =
	{
		Pos = pos2d,
		Parent = parentCvs.UnityObject.transform,
		LayerOrder = menuOrder,
		Scale = scale,
		UILayer = true,
		Clip=pan.Transform
	}
	local num = #self.activeEffId + 1
	self.activeEffId[num] = Util.PlayEffect(fileName,param)
	return self.activeEffId[num]
end


local function LevelUpEffect(self,parent)
	local transSet = TransformSet()
	transSet.Pos = Vector2(parent.Width/2,-parent.Height/2)
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	-- transSet.Vectormove = Vector2(parent.Width,parent.Height)
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_consume.assetbundles'
	self.LevelUpEffID = RenderSystem.Instance:PlayEffect(assetname, transSet,1)
end


local function Release3DModel(self)
	if self and self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DSingleModel(self, parentCvs, pos2d, scale, rotate,fileName)

	local menuOrder = self.ui.menu.MenuOrder
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	self.model = info
	local trans = info.RootTrans
	info.Callback = function (info2)
	
		local trans2 = info2.RootTrans
		trans2:Rotate(Vector3.up,rotate)
		local localPosition = trans2.localPosition
		trans2.localPosition = Vector3(localPosition.x,localPosition.y,effectZ)
	end
	
	-- parentCvs.event_PointerMove = function(sender, data)
	-- 	local delta = -data.delta.x
	-- 	print('delta:',delta)
	-- 	trans:Rotate(Vector3.up, delta * 1.2)
	-- end
	
end

local function LvEffect(self,parent)
	-- body
	local parent = parent or self.ui.comps.cvs_effect1
	if self.LvEffId ~= nil then
		RenderSystem.Instance:Unload(self.LvEffId)
	end

	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ - 5)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_upgrade.assetbundles'
	self.LvEffId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end

local function AttrEffect(self,parent)
	-- body
	local parent = parent or self.ui.comps.cvs_effect2

	if self.AttrEffId ~= nil then
		RenderSystem.Instance:Unload(self.AttrEffId)
	end

	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ - 5)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_god_attribute.assetbundles'
	self.AttrEffId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end

local function InitGetEff(self,modelData,finishCB)

	if self == nil then
		return
	end
	-- Release3DModel(self)
	if self.expTimeId ~= nil then
		LuaTimer.Delete(self.expTimeId)
		self.expTimeId = nil
	end

	local parentCvs = self.ui.comps.cvs_model
 
	
	local fixposdata = string.split(modelData.pos_xy,',')
	local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
	local fixzoom = tonumber(modelData.zoom) -- 缩放比例
	local Pos3D = Vector3(fixpos.x,-fixpos.y,effectZ)

	local transSet = TransformSet()
	transSet.Scale = Vector3(fixzoom, fixzoom, fixzoom)
	transSet.Pos = Pos3D
	transSet.Parent = parentCvs.Transform

	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	if self.effect ~= nil then
		RenderSystem.Instance:Unload(self.effect)
	end

	local unlockingRes = modelData.unlocking_res
	self.effect = RenderSystem.Instance:PlayEffect(unlockingRes, transSet)
	self.newArtifactId = 0
	self.ui.comps.cvs_effect.Visible = true
	
	self.expTimeId = LuaTimer.Add(1500,1500,finishCB)
	 
end


local function ReloadModel(self,modelData,isOpen)
	if self == nil then
		return
	end
 
	local fixposdata = string.split(modelData.pos_xy,',')
	local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
	local fixzoom = tonumber(modelData.zoom) -- 缩放比例
	local pos2d = Vector2(fixpos.x,fixpos.y)
	local rotate = modelData.rotate

	-- print_r('modelData11111111111111111111:',modelData)
	-- print('111111111111111111111:',rotate)
	
	local cvsPos2d = self.ui.comps.cvs_model.Position2D
	-- local pos2d = self.ui.comps.cvs_anchor.Position2D
	
	local effectPos3d =  Vector3(fixpos.x,fixpos.y,-200)
	local modelRes
	-- 已解锁
	if isOpen then
		modelRes = modelData.avatar_res
	else
		-- 未解锁
		modelRes = modelData.seal_res
	end

	local parentCvs = self.ui.comps.cvs_model

	if self.newArtifactId == modelData.id then
		local index = 0
		InitGetEff(self, modelData,function (id)
			if index == 0 then
				Release3DModel(self)
				Init3DSingleModel(self, parentCvs, pos2d, fixzoom, rotate,modelData.avatar_res)
				index = index + 1
				return
			end

			index = index + 1
			RenderSystem.Instance:Unload(self.effect)
	 		
			self.ui.comps.cvs_effect.Visible = false

			local name =  Util.GetText(modelData.artifact_name)
			local message = Util.GetText('artifact_unlock',name)
			GlobalHooks.UI.OpenUI('AdvancedTips',0, message)

	 		return false
		end)
	else
		Release3DModel(self)
    	Init3DSingleModel(self, parentCvs, pos2d , fixzoom, rotate,modelRes)
    end
end


local  function showItemAttribute(self,artifactId,level)
	-- body
	local data = ArtifactModel.GetArtifactAttr(artifactId,level) 
	local attr = ArtifactModel.GetXlsFixedAttribute(data)

	for k,v in pairs(attr) do
		local shuxing = 'lb_shuxing' .. k
		local nameLabel = self.ui.comps[shuxing]
		local valueLabel = self.ui.comps[shuxing .. 'num']
		nameLabel.Text,valueLabel.Text = ItemModel.GetAttributeString(v)
 
	end

	local nextData = ArtifactModel.GetArtifactAttr(artifactId,level + 1)
	if nextData == nil then
		-- print('manji ........................################# ')
		for i=1,4 do
			self.ui.comps['lb_shuxing' .. i .. 'numnext'].Text = ""
		end
		return
	else
		local nextAttr = ArtifactModel.GetXlsFixedAttribute(nextData)
		for k,v in pairs(nextAttr) do
			_,self.ui.comps['lb_shuxing' .. k .. 'numnext'].Text  = ItemModel.GetAttributeString(v)
		end
	end
end

local  function updateItemAttribute(self,artifactId,level )
	-- body


	local data = ArtifactModel.GetArtifactAttr(artifactId,level)

	if data == nil then
		-- 满级
		return
	end

			--TODO cywu
	AttrEffect(self)


	showItemAttribute(self,artifactId,level)


end
 
local  function showAllAttribute(self,... )

	local all = ArtifactModel.GetAllArtifactAttribute(self.artifactMap)
	-- print_r('showAllAttribute:',all)

	for i=1,20 do
		self.ui.comps['lb_val'..i].Text = 0
	end

	local attrs = {}
	for k,v in pairs(all) do
		-- print_r(k,v)
		local name,value = ItemModel.GetAttributeString(v)
		-- print(name,value)
		-- self.ui.comps.lb_attribute[k].Text = name
		self.ui.comps['lb_val'..v.index].Text = value
		table.insert(attrs,v)
	end

 	-- local fight = getFight(all)
 	local  fight = ItemModel.CalcAttributesScore(attrs)
 	-- print('fight:',fight)
 	self.ui.comps.lb_fight.Text = fight
end


local function showSkill(self, itemData, ... )
	-- body

	UIUtil.SetImage(self.skillIcon,itemData.icon_id, false, UILayoutStyle.IMAGE_STYLE_BACK_4)
	
	local name = Util.GetText(itemData.artifact_name)
	self.nameLabel.Text = name
	self.skillNameLabel.Text = name

	local level = self.artifactMap[itemData.id] 
	if level == nil or level < 0 then
		level = 1
	end

	-- print('showSkill:',level)

	self.skillLvLabel.Text = level
	self.LvLabel.Text = level

	local attrData = ArtifactModel.GetArtifactAttr(itemData.id,level)
	if attrData == nil then
		return
	end

	self.skillDescLabel.XmlText =  Util.GetText(attrData.skill_desc) 

end


local function updateSkillInfo(self, artifactId, level)
	-- body
	local attrData = ArtifactModel.GetArtifactAttr(artifactId,level)
	if attrData == nil then
		return
	end

	LvEffect(self)

	self.skillLvLabel.Text = level
	self.LvLabel.Text = level
	self.skillDescLabel.XmlText =  Util.GetText(attrData.skill_desc)

	--TODO cywu
end

local function showGetCost(self,itemData)
	local items = ItemModel.ParseCostAndCostGroup(itemData)
	
	self.costLabel.Visible = false
	self.ui.comps.lb_max.Visible = false

	local csv_itemIcon = self.costCvs
	if items then
		for i,v in ipairs(items) do 
		-- 	print_r(v)
			local item = v.detail 
			self.costCvs.Visible = true 

			UIUtil.SetEnoughItemShowAndLabel(self, csv_itemIcon, self.costLabel, v)



  			self.costCvs.Visible = true
  			self.costLabel.Visible = true
  			self.btnBuy.Visible = true

  			self.ui.comps.lb_cost.Text = Constants.Text.artifact_buyCost

  			break
		end 
	end

	self.btnUse.Visible = false
end


local function showLevelCost(self,artifactId,level)

	print('showLevelCost:',level)

	local attrData = ArtifactModel.GetArtifactAttr(artifactId,level)

 	self.btnUp.Visible = false
	self.costCvs.Visible = false
	self.ui.comps.lb_max.Visible = false
	
	if attrData == nil then
		-- manji
		self.ui.comps.lb_max.Visible = true
		return
	end

	
	local items = ItemModel.ParseCostAndCostGroup(attrData)
	self.costLabel.Visible = false

	if items then
		for i,v in ipairs(items) do 
		-- 	print_r(v)
			local item = v.detail 
			self.costCvs.Visible = true 

  			UIUtil.SetEnoughItemShowAndLabel(self,  self.costCvs, self.costLabel, v)

			
  			self.costCvs.Visible = true
  			self.costLabel.Visible = true
  			self.btnUp.Visible = true

  			self.ui.comps.lb_cost.Text = Constants.Text.artifact_lvupCost
  			return
		end 
	end

	self.btnUp.Visible = false
	self.costCvs.Visible = false
	self.ui.comps.lb_max.Visible = true 
end


local function showDetialPanel(self,itemData)

	self.selectedItemData = itemData
	

	self.desc.Text = Util.GetText(itemData.artifact_desc)
	-- self.ui.comps.lb_name.Text = Util.GetText(itemData.artifact_name)

	showSkill(self,itemData)

	self.btnBuy.Visible = false
	self.btnUp.Visible = false

	self.costCvs.Visible = false
	self.costLabel.Visible = false

	local artifactId = itemData.id
	local level = self.artifactMap[artifactId]


	if level ~= nil and level > 0 then
		showLevelCost(self,itemData.id,level)

		local attrData = ArtifactModel.GetArtifactAttr(artifactId,level)
		local canLevelUp = ArtifactModel.CanGet(attrData,true)
		if canLevelUp then
			--TODO显示红点
			self.lb_red_up.Visible = true
		else
			--TODO隐藏红点
			self.lb_red_up.Visible = false
		end

		showItemAttribute(self,artifactId,level)

		self.btnUse.Visible = true

		ReloadModel(self,itemData,true)
	else
		showGetCost(self,itemData)
		showItemAttribute(self,artifactId,1)

		ReloadModel(self,itemData)

		local canActive = ArtifactModel.CanGet(itemData)
		-- 设置可激活特效

		--TODO隐藏红点
		self.lb_red_up.Visible = false
		if canActive then
			if self.canGetEffID == nil then
				self.canGetEffID = CanGetEffect(self,self.btnBuy)
			end
			--TODO显示红点

			self.lb_red_up.Visible = true


		elseif self.canGetEffID then
			RenderSystem.Instance:Unload(self.canGetEffID)
			self.canGetEffID = nil
		end

	end
end
 
local function itemTouchClick(self,index,node,togButton,itemData)
	if self.LevelUpEffID then
		RenderSystem.Instance:Unload(self.LevelUpEffID)
	end
	
    if self.defaultIndex == index then
        togButton.IsChecked = true
    elseif  self.selectedTogButton  then 
        self.selectedTogButton.IsChecked = false
        togButton.IsChecked = true
        self.selectedTogButton = togButton
        -- self.defaultNode = node
        showDetialPanel(self,itemData)
    end
	self.defaultIndex = index
end

local function showTogButton(self,node,index,itemData)
  -- body
	local togButton = UIUtil.FindChild(node,'tbt_icon')
	togButton.IsChecked = false
 
	if self.defaultIndex == index then
		togButton.IsChecked = true
		self.selectedTogButton = togButton
        -- self.defaultNode = node
		showDetialPanel(self,itemData)
	else
		togButton.IsChecked = false
	end

	togButton.TouchClick = function ( ... )
		itemTouchClick(self,index,node,togButton,itemData)
	end
end

local function showArtifactItem(self,node,index)

	local artifactData = self.artifacts[index] 
	local headIcon = UIUtil.FindChild(node,'ib_icon')
	UIUtil.SetImage(headIcon,artifactData.icon_id)
 	
 	local artifactId = artifactData.id

 	--UserTag是因为老蔡的新手引导加的
 	node.UserTag =  artifactId

 	local lb_active = UIUtil.FindChild(node,'lb_active')
 	lb_active.Visible = false

 	local icon12 = UIUtil.FindChild(node,'cvs_dress')
 	icon12.Visible = false
	if self.MainEquipId == artifactId then
		icon12.Visible = true
		local icon01 = UIUtil.FindChild(icon12,'lb_dress1')
		icon01.Visible = true
		local icon02 = UIUtil.FindChild(icon12,'lb_dress2')
		icon02.Visible = false
	elseif self.SecondEquipId == artifactId then
		icon12.Visible = true
		local icon01 = UIUtil.FindChild(icon12,'lb_dress1')
		icon01.Visible = false
		local icon02 = UIUtil.FindChild(icon12,'lb_dress2')
		icon02.Visible = true
	else
		icon12.Visible = false
	end
	
--[[	if not string.IsNullOrEmpty(node.UserData) then --如果node存在特效，则释放特效
		RenderSystem.Instance:Unload(tonumber(node.UserData))
		node.UserData = ''
	end]]
	local redPoint = UIUtil.FindChild(node,'lb_red_listup')
	

	local level = self.artifactMap[artifactId]
 	
	if level ~= nil and level > 0 then
		headIcon.IsGray = false
		node.IsGray = false

		local attrData = ArtifactModel.GetArtifactAttr(artifactId,level)
		local canLevelUp = false
		if attrData then
			canLevelUp = ArtifactModel.CanGet(attrData,true)
		end

		GlobalHooks.UI.ShowRedPoint(redPoint, canLevelUp and 1 or 0, 'MiracleMainList')
	else

		local canActive = ArtifactModel.CanGet(artifactData)
 
		if canActive then 
			headIcon.IsGray = false
			node.IsGray = false
			lb_active.Visible = canActive
			--设置可激活特效
			ActiveEffect(self,node,self.pan,Vector3(node.Size2D.x/2,40,0),Vector3(1,1.6,1),self.ui.menu.MenuOrder,'/res/effect/ui/ef_ui_frame_01.assetbundles')
		else
			headIcon.IsGray = true
			node.IsGray = true
		end

		GlobalHooks.UI.ShowRedPoint(redPoint, canActive and 1 or 0, 'MiracleMainList')
	end
	
	showTogButton(self,node,index,artifactData)
end


local function SelectByIndex(self, index)
    UIUtil.MoveToScrollCell(
        self.pan,
        index,
        function(node)
            
        end
    )
end

local function showScrollPan(self)
  -- body
 
	-- self.defaultIndex = 1;
	-- self.selectedTogButton = nil

	self.artifacts = ArtifactModel.GetArtifactData(self.artifactMap,self.MainEquipId,self.SecondEquipId)
	if self.selectedItemData then
		for i,v in ipairs(self.artifacts) do
			if self.selectedItemData.id == v.id then
				self.defaultIndex = i
				-- print('showScrollPan self.defaultIndex:',self.defaultIndex)
			end
		end
	else 
		self.defaultIndex = 1
	end

	-- print('showScrollPan artifacts.length: ',#self.artifacts)
	local function eachUpdateCb(node, index) 
		showArtifactItem(self,node,index)
	end
 
	UIUtil.ConfigVScrollPanWithOffset(self.pan,self.tempnode,#self.artifacts,4,eachUpdateCb)
 	SelectByIndex(self,self.defaultIndex)
 
end
 

local function showEquipItem(self,node,index)
 
	local artifactData = self.euipArtifacts[index] 
	if artifactData == nil then
		node.Visible = false
		return
	end

	local artifactId = artifactData.id
	local level = self.artifactMap[artifactId] 
	if level == nil or level < 0 then
		node.Visible = false
		return
	end
 
	local nameLabel = UIUtil.FindChild(node,'lb_name')
 	nameLabel.Text = Util.GetText(artifactData.artifact_name)
 	nameLabel.Enable = false

 	local nameEqup = UIUtil.FindChild(node,'lb_equip')
 	if self.MainEquipId == artifactId then
 		nameEqup.Text = Constants.Text.artifact_Main
 	elseif self.SecondEquipId == artifactId then
 		nameEqup.Text = Constants.Text.artifact_Second
 	else
 		nameEqup.Visible = false
 	end

 	local secImg = UIUtil.FindChild(node,"ib_select")
 	secImg.Visible = false
 	if self.selectedItemData ~= nil and self.selectedItemData.id == artifactId then
 		secImg.Visible = true 
		self.SelectEquipNode = node
 		self.SelectEquipId = artifactId
 		nameLabel.Enable = true
 	end


 	local  function EquipItemTouchClick( ... )
 		if self.SelectEquipNode then
 			local selectImg = UIUtil.FindChild(self.SelectEquipNode,"ib_select")
 			selectImg.Visible = false

 			local selectName = UIUtil.FindChild(self.SelectEquipNode,'lb_name')
 			selectName.Enable = false
 		end		
 		self.SelectEquipNode = node
 		
 		secImg.Visible = true
 		nameLabel.Enable = true

 		self.SelectEquipId = artifactId
 	end  

 	local icomCvs = UIUtil.FindChild(node,'cvs_icon')
 	UIUtil.SetImage(icomCvs,artifactData.icon_id,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
 	icomCvs.TouchClick = function (sender)
 		SoundManager.Instance:PlaySoundByKey('button',false)
 	 	EquipItemTouchClick(self,node,index)
 	end

 	node.TouchClick = function (sender)
		EquipItemTouchClick(self,node,index)
 	end

end

local function showEquipScrollPan(self, ... )
	-- body
	-- print('showScrollPan artifacts.length: ',#self.artifacts)
	local function eachUpdateCb(node, index) 
		-- print('eachUpdateCb eachUpdateCb:', index)
		showEquipItem(self,node,index)
	end

 	self.euipArtifacts = ArtifactModel.GetArtifactEquipData(self.artifactMap,self.MainEquipId,self.SecondEquipId) 	 

    local col = 5
    UIUtil.ConfigGridVScrollPan(self.euipPan,self.equipTempnode,col,#self.euipArtifacts,eachUpdateCb)
end

local function showMainEquip(self)
	-- body
	if self.MainEquipId > 0 then
		local artifactData = ArtifactModel.GetArtifact(self.MainEquipId)
		print('self.MainEquipId: ',self.MainEquipId)
		if artifactData then
			local iconId = artifactData.icon_id
			print('showMainEquip iconId: ',iconId)
			UIUtil.SetImage(self.mainEquipCvs,iconId,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	else
		self.mainEquipCvs.Layout = UILayout()
	end
end


local function showSecondEquip(self)

	local playerLevel = SkillModel.GetLevel()
	local needLevel = ArtifactModel.GetSecondEquipOpenLv()
	if playerLevel < needLevel then
		local lockLabel = self.ui.comps.lb_lock
		lockLabel.Text = needLevel .. Constants.Text.skill_Level
		lockLabel.Visible = true
	else
		local lockLabel = self.ui.comps.lb_lock
		lockLabel.Visible = false
	end

	if self.SecondEquipId > 0 then
		local artifactData = ArtifactModel.GetArtifact(self.SecondEquipId)
		
		if artifactData then
			local iconId = artifactData.icon_id
			UIUtil.SetImage(self.SecondEquipCvs,iconId,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	else
		self.SecondEquipCvs.Layout = UILayout()
	end
end

local function show12Equip(self, ... )
	-- body
	showMainEquip(self)

	showSecondEquip(self)
end

local function showEquipUI(self, ... )
	-- body
	show12Equip(self)

	showEquipScrollPan(self)
end


local function InitEquipCompmont(self)
	-- body

	self.EquipChange = false

	self.mainEquipCvs = self.ui.comps.cvs_zhu
	self.mainEquipCvs.TouchClick = function (sender)
		-- body
		SoundManager.Instance:PlaySoundByKey('button',false)

		if self.MainEquipId == self.SelectEquipId then

			local tips = Constants.Text.artifact_equip_again
			-- print('TouchClick tips:',tips)
			GameAlertManager.Instance:ShowNotify(tips)

			return
		end
 
		-- print("ReqUseArtiface self.SelectEquipId:",self.SelectEquipId)
		ArtifactModel.ReqUseArtifact(mainPos,self.SelectEquipId,function (resp)
			-- body
			if self.MainEquipId ~= resp.MainEquipId then
				self.MainEquipId = resp.MainEquipId
				self.EquipChange = true
				showMainEquip(self)
			end
		
			if self.SecondEquipId ~= resp.SecondEquipId then
				self.SecondEquipId = resp.SecondEquipId
				self.EquipChange = true
				showSecondEquip(self)
			end

			if self.EquipChange then
				showEquipScrollPan(self)
			end

		end)
	end

	self.SecondEquipCvs = self.ui.comps.cvs_fu
	self.SecondEquipCvs.TouchClick = function (sender)
		-- body
		--TO
		SoundManager.Instance:PlaySoundByKey('button',false)
		local playerLevel = SkillModel.GetLevel()
		local needLevel = ArtifactModel.GetSecondEquipOpenLv()
		if playerLevel < needLevel then
		    local message = Constants.Text.artifact_equipIndex_notOpen
			local tips =Util.Format1234(message,needLevel)
			GameAlertManager.Instance:ShowNotify(tips)
			return
		end

		if self.SecondEquipId == self.SelectEquipId then

			local tips = Constants.Text.artifact_equip_again
			print('self.SecondEquipCvs:',tips)
			GameAlertManager.Instance:ShowNotify(tips)

			return
		end

		ArtifactModel.ReqUseArtifact(secondPos,self.SelectEquipId,function (resp)
			-- body
			if self.MainEquipId ~= resp.MainEquipId then
				self.MainEquipId = resp.MainEquipId
				self.EquipChange = true
				showMainEquip(self)
			end
		
			if self.SecondEquipId ~= resp.SecondEquipId then
				self.SecondEquipId = resp.SecondEquipId
				self.EquipChange = true
				showSecondEquip(self)
			end

			if self.EquipChange then
				showEquipScrollPan(self)
			end

		end)
	end

	self.lockLabel = self.ui.comps.lb_lock

end
 
local function InitCompmont(self)

	self.newArtifactId = 0
	self.pan = self.ui.comps.sp_list
	self.tempnode = self.ui.comps.cvs_miracleicon
	self.tempnode.Visible = false


	self.lb_red_up = self.ui.comps.lb_red_up

	self.nameLabel = UIUtil.FindChild(self.ui.comps.cvs_show,'lb_name')
	self.LvLabel = UIUtil.FindChild(self.ui.comps.cvs_show,'lb_level')
	
	self.skillNameLabel = self.ui.comps.lb_skillname
	self.skillLvLabel = self.ui.comps.lb_lv
	self.skillDescLabel = self.ui.comps.tb_skilldes
	self.skillIcon = self.ui.comps.cvs_skillIcon

	self.costCvs = self.ui.comps.cvs_costicon
	self.costLabel = self.ui.comps.ib_costnum
 
	self.desc = self.ui.comps.tb_desc


	-- 激活
	self.btnBuy = self.ui.comps.btn_get
	self.btnBuy.TouchClick = function ( sender )
      	
		if self.selectedItemData == nil then
			return
		end

      	local artifactId = self.selectedItemData.id
      	ArtifactModel.ReqGetArtifact(artifactId,function(resp)
      		self.artifactMap[artifactId] = 1
      		self.newArtifactId = artifactId
      		showScrollPan(self)
      		SoundManager.Instance:PlaySoundByKey('shenqijiesuo',false)

      		if self.canGetEffID then
				RenderSystem.Instance:Unload(self.canGetEffID)
				self.canGetEffID = nil
			end
      	end)
    end

    -- 升级
	self.btnUp = self.ui.comps.btn_up
	self.btnUp.TouchClick = function ( sender )

		if self.selectedItemData == nil then
			return
		end

		local artifactId = self.selectedItemData.id

      	ArtifactModel.ReqArtifactLevelUp(artifactId,function(resp)
      		--local newLevel = self.artifactMap[artifactId] + 1
			--self.artifactMap[artifactId] = newLevel
			
			SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)
			showScrollPan(self)
			LevelUpEffect(self,self.ui.comps.cvs_costicon)

			--为了愚蠢的红点，之前的优化白做了
      		-- if resp.s2c_code == 200 then

      		-- 	local newLevel = self.artifactMap[artifactId] + 1
      		-- 	self.artifactMap[artifactId] = newLevel

      		-- 	updateSkillInfo(self,artifactId,newLevel)

      		-- 	updateItemAttribute(self,artifactId,newLevel)

      		-- 	showLevelCost(self,artifactId,newLevel)
      			
      		-- 	SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)

      		-- 	LevelUpEffect(self,self.ui.comps.cvs_costicon)

      		-- else

      		-- end
      	end)
    end

    self.EquipUIOpen = false
    local tipsCvs = self.ui.comps.cvs_tips
    
    local attrCvs = self.ui.comps.cvs_allattribute  -- UIUtil.FindChild(tipsCvs,'cvs_allattribute')
    local equipCvs = self.ui.comps.cvs_equip 
           -- UIUtil.FindChild(tipsCvs,'cvs_equip')
    local  function btnClose( self )
    	-- body
    	self.SelectEquipNode = nil
		self.SelectEquipId = 0
		self.EquipUIOpen = false

		tipsCvs.Visible = false
		attrCvs.Visible = false
		equipCvs.Visible = false

		if self.EquipChange  then
			self.EquipChange = false
			showScrollPan(self)
		end

		self.ui.comps.cvs_show.Visible = true

		-- if self.model then
		-- 	self.model.Enable = true
		-- end
    end

	self.ui.menu:SetUILayer(tipsCvs)
	tipsCvs.TouchClick = function (sender)
		-- body
		btnClose(self)
	end

	local btnClose22 = self.ui.comps.btn_close22
	btnClose22.TouchClick = function (sender)
		-- body
		btnClose(self)
	end


	self.btnAttribute = self.ui.comps.btn_attr
	self.btnAttribute.TouchClick = function ( sender )
		print('btnAttribute click')

		tipsCvs.Visible = true
		equipCvs.Visible = false
		attrCvs.Visible = true

		showAllAttribute(self)
    	
    	self.ui.comps.cvs_show.Visible = false
    end

    self.euipPan = self.ui.comps.sp_skilllist
    self.equipTempnode = self.ui.comps.cvs_skillinfo
    self.equipTempnode.Visible = false
    	-- 装备
	self.btnUse = self.ui.comps.tbt_use
	self.btnUse.TouchClick = function ( sender )
		print('btnUse click')

		local status = TLBattleScene.Instance.Actor:isNoBattleStatus()
		if status == false then
			local tips = Constants.Text.artifact_inbattle
			print('TouchClick tips:',tips)
			GameAlertManager.Instance:ShowNotify(tips)
			return
		end


		attrCvs.Visible = false

		tipsCvs.Visible = true
		equipCvs.Visible = true
		self.EquipUIOpen = true

		showEquipUI(self)

		self.ui.comps.cvs_show.Visible = false
		-- if self.model then
		-- 	self.model.Visible = false
		-- end
    end
end

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	self.canGetEffID = nil
	self.LevelUpEffID = nil
	self.activeEffId={}
	self.ui.comps.cvs_effect.Visible = false

	self.defaultIndex = 1
	self.selectedItemData = nil
	self.selectedTogButton = nil

	ArtifactModel.ReqArtifactList(function(resp)

		self.artifactMap = resp.artifactMap
		self.MainEquipId = resp.MainEquipId
		self.SecondEquipId = resp.SecondEquipId

		showScrollPan(self)

	end)

end

local function UnLoadEff(self)
	-- body
	if self.LvEffId ~= nil then
		RenderSystem.Instance:Unload(self.LvEffId)
	end

	if self.AttrEffId ~= nil then
		RenderSystem.Instance:Unload(self.AttrEffId)
	end

	if self.effect ~= nil then
		RenderSystem.Instance:Unload(self.effect)
		self.effect = nil
	end

	if self.canGetEffID then
		RenderSystem.Instance:Unload(self.canGetEffID)
		self.canGetEffID = nil
	end

	if self.LevelUpEffID then
		RenderSystem.Instance:Unload(self.LevelUpEffID)
	end
	
	for i = 1,#self.activeEffId do
		RenderSystem.Instance:Unload(self.activeEffId[i])
	end

end

function _M.OnExit( self )
	print('MiracleMain OnExit')
	if self.expTimeId ~= nil then
		LuaTimer.Delete(self.expTimeId)
		self.expTimeId = nil
	end
	
	

	UnLoadEff(self)
end

function _M.OnDestory( self )
	
	print('MiracleMain OnDestory')
	if self.expTimeId ~= nil then
		LuaTimer.Delete(self.expTimeId)
		self.expTimeId = nil
	end

	UnLoadEff(self)
end

function _M.OnInit( self )
	print('MiracleMain OnInit')

	InitCompmont(self)

	InitEquipCompmont(self)
end

return _M