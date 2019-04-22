local _M = {}
_M.__index = _M
 

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel' 
local MountModel = require 'Model/MountModel'


--- 初始的时候服务器过来是 rank:1  level:0  star:0 

local function ConfigPageScrollPan(self,pages, CreateCB,UpdateCB)
	local function CreateListItem(Scrollable,i)
		local node = self.tempnode:Clone()
		CreateCB(node,i)
		return node
	end
	local s = self.tempnode.Size2D
	self.pan:Initialize(pages, s, CreateListItem,UpdateCB)
end

local effectZ = -100
--点亮的特效
local function BigLevelActEffect(self,parent)
	if self.LvActId ~= nil then
		RenderSystem.Instance:Unload(self.LvActId)
	end

	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_dot_activation01.assetbundles'
	self.LvActId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end

--正在培养的特效
local function BigLevelNextEffect(self,parent)
	if self.nextLvId ~= nil then
		RenderSystem.Instance:Unload(self.nextLvId)
	end

	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.Clip =  self.pan.Transform
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_dot_01.assetbundles'
	self.nextLvId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end

--点亮的特效
local function SmallStarActEffect(self,parent)
	if self.StarActId ~= nil then
		RenderSystem.Instance:Unload(self.StarActId)
	end
	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_dot_activation02.assetbundles'
	self.StarActId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end

--正在培养的特效
local function SmallStarNextEffect(self,parent)
	if self.nextStarId ~= nil then
		RenderSystem.Instance:Unload(self.nextStarId)
	end

	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_dot_02.assetbundles'
	self.nextStarId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end

--星升满的特效
local function Star7Effect(self,parent)
	if self.Star7effectId ~= nil then
		RenderSystem.Instance:Unload(self.Star7effectId)
	end
	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_mount_refurbish.assetbundles'
	self.Star7effectId = RenderSystem.Instance:PlayEffect(assetname, transSet)
end


local function UnlockEffect(self,parent)
	if self.unlockEffectId ~= nil then
		RenderSystem.Instance:Unload(self.unlockEffectId)
	end
	local transSet = TransformSet()
	-- local parent = self.ui.comps.cvs_pulse
	transSet.Parent = parent.Transform
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,effectZ)
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_advanced.assetbundles'
	self.unlockEffectId = RenderSystem.Instance:PlayEffect(assetname, transSet)
	SoundManager.Instance:PlaySoundByKey('jinjie',false)
end

local function EffectUnload(self)
	-- body
	if self.LvActId ~= nil then
		RenderSystem.Instance:Unload(self.LvActId)
		self.LvActId = nil
	end
	-- if self.nextLvId ~= nil then
	-- 	RenderSystem.Instance:Unload(self.nextLvId)
	-- 	self.nextLvId = nil
	-- end
	if self.StarActId ~= nil then
		RenderSystem.Instance:Unload(self.StarActId)
		self.StarActId = nil
	end
	if self.nextStarId ~= nil then
		RenderSystem.Instance:Unload(self.nextStarId)
		self.nextStarId = nil
	end
	if self.Star7effectId ~= nil then
		RenderSystem.Instance:Unload(self.Star7effectId)
		self.Star7effectId = nil
	end
	if self.unlockEffectId ~= nil then
		RenderSystem.Instance:Unload(self.unlockEffectId)
		self.unlockEffectId = nil
	end

end


-- local function IsFullVein(self)
-- 	print('IsFullVein:',self.rank ,self.maxRank , self.level,self.maxLevel ,self.star,self.maxStar)
-- 	if self.rank == self.maxRank and self.level == self.maxLevel  and self.star == self.maxStar then
-- 		return true
-- 	end
-- end 


local function darwNextStarAttr(self,name,value)
	self.starAttrNameLabel.Text = name
	self.starAttrLabel.Text = '+'..value 
end 


local function calcNextStarAttr(attrData,lastAttrData)
	local lastAll = {}
	for k,v in ipairs(lastAttrData) do
		lastAll[v.Name] = v.Value
	end

	local nextData = {}
	for k,v in ipairs(attrData) do

		--print('aaaa:',k,v.Name,v.Value)
		local lastV = lastAll[v.Name]
		--print('bbbb:',lastV)
		if  lastV == nil then
		 	nextData.name = v.Name
		 	nextData.value = v.Value
		 	--print_r('calcNextStarAttr111111111:',nextData)
		 	break
		else
		 	local value = v.Value - lastV
		 	if value > 0 then
				nextData.name = v.Name
				nextData.value = value
				--print_r('calcNextStarAttr22222222:',nextData)
				break
		 	end
		end
	end
	
	return nextData
end
 

local function showNextStarAttr(self)
	-- body
 	-- if IsFullVein(self) then
 	-- 	darwNextStarAttr(self,"","")
 	-- 	--  满级满阶满星
		-- return
 	-- end

	-- local veinData = getNextMountVeinData(self)
	local veinData = MountModel.GetNextVeinData(self.veinId)
	if veinData == nil then
		darwNextStarAttr(self,"","")
		return
	end 
 
	local attrData = MountModel.GetXlsFixedAttribute(veinData)

	local lastVeinData = MountModel.GetVeinData(self.veinId)
 
 	local lastAttrData = {}
	if lastVeinData == nil or next(lastVeinData) == nil then 

	else
		lastAttrData = MountModel.GetXlsFixedAttribute(lastVeinData)
	end 
 
	local nextStar = calcNextStarAttr(attrData,lastAttrData)
	
	local attrData = GlobalHooks.DB.FindFirst('Attribute',{key = nextStar.name})
	local name = Util.GetText(attrData.name) or ""
	darwNextStarAttr(self,name,nextStar.value or  "")

end 

local function showAttribute(self)
	if self.veinId == 0 then
		return
	end

	local veinData = MountModel.GetVeinData(self.veinId)
	local all = MountModel.GetXlsFixedAttribute(veinData)

	for k,v in pairs(all) do
		
		local Name = v.Name
        local Value = v.Value
        -- print(k,v,Name,Value)
        local labelName = 'lb_' .. Name
        if self.ui.comps[labelName] then
        	self.ui.comps[labelName].Text = Value
        end
	end
end

 
local function setUseBtnStatus(self,status)
	-- print('setUseBtnStatus:',status)
	self.btnUse.IsGray = not status
	self.btn1KeyUse.IsGray = not status
	self.btnUse.Enable = status
	self.btn1KeyUse.Enable = status
end 

local function showCostItem(self,itemIcon,itemData)
	-- body
	local item = itemData.detail
	--local itemDetial = item 		--ItemModel.GetDetailByTemplateID(item.static.id)
	-- local itShow = UIUtil.SetItemShowTo(itemIcon,item.static.atlas_id, item.static.quality)
	-- itShow.EnableTouch = true
	-- itShow.TouchClick = function ( ... )
	-- 	-- body
	-- 	local detail = UIUtil.ShowNormalItemDetail({detail=itemDetial,itemShow=itShow,autoHeight=true})
	-- 	-- detail:SetPos(0,350)

	-- end

	local  costLabel = UIUtil.FindChild(itemIcon,'lb_num')
	-- if costLabel then
	-- 	costLabel.Visible = true
		
	-- 	-- 是否有这个必要
	-- 	-- if itemData.cur < itemData.need then
	-- 	-- 	setUseBtnStatus(self,false)
	-- 	-- end

	-- 	local value 
	-- 	if item.static.item_type == 0 then
	-- 		value = itemData.need
	-- 	else
	-- 		value = itemData.cur .. '/' .. itemData.need
	-- 	end
		
	-- 	local color = 0xffffff
	-- 	if itemData.cur < itemData.need then
	-- 		color = 0xff0000
	-- 	end

	-- 	costLabel.Text = value
    --  	costLabel.FontColorRGB = color
	-- end

	--升级消耗
	-- local detail = ItemModel.GetDetailByTemplateID(item.static.id)
	-- local count =itemData.cur
	-- local cost = {detail = detail,cur = count,need =itemData.need}
	--local point = self.ui.root:GlobalToLocal(itemIcon:LocalToGlobal(),true)
	local tipsparams = {x =itemIcon.Position2D.x+350, --point.x-300,
						y = itemIcon.Position2D.y+100,--point.y-300,
						anchoror = 'r_b'}
	UIUtil.SetEnoughItemShowAndLabel(self,itemIcon,costLabel,itemData,tipsparams)

end


-- TODO
local function ShowMaxVein(self)
	-- body
	-- print('ShowMaxVein111111111111111111')

	if self.nextLvId ~= nil then
		RenderSystem.Instance:Unload(self.nextLvId)
	end

	if self.nextStarId ~= nil then
		RenderSystem.Instance:Unload(self.nextStarId)
	end


	setUseBtnStatus(self,false)

	darwNextStarAttr(self,"","")

	self.costCvs.Visible = false

	self.ui.comps.lb_max.Visible = true
end 


local function showCost(self)
 
 	self.ui.comps.lb_max.Visible = false
 	-- if IsFullVein(self) then
 	-- 	ShowMaxVein(self)
 	-- 	return
 	-- end
 	-- if self.veinId == self.maxVeinId then
		-- ShowMaxVein(self)
 	-- 	return
 	-- end

 	-- print('showCost self.veinId1111111111111111111:',self.veinId)
	local moutVeinData =  MountModel.GetNextVeinData(self.veinId)
	if not moutVeinData then
		ShowMaxVein(self)
		 print('self.veinId moutVeinData is null :',self.veinId)
		return
	end

	local itemDatas = ItemModel.ParseCostAndCostGroup(moutVeinData)
	if itemDatas then
		local parentWidth = self.costCvs.Width
		local parentHeight = self.costCvs.Height
		local width = self.costItemCvs.Width
		local height = self.costItemCvs.Height
		local length = #itemDatas
  		local gap = width + 30 - ((length - 2)*30)

		for index,itemData in ipairs(itemDatas) do
			 
			local itemIcon = UIUtil.FindChildByUserTag(self.costCvs,index)
 			if itemIcon == nil then
 				itemIcon = self.costItemCvs:Clone()
 				itemIcon.Visible = true
 				itemIcon.UserTag = index
 				self.costCvs:AddChild(itemIcon)
 			end
  			showCostItem(self,itemIcon,itemData)
  			itemIcon.Position2D = Vector2(parentWidth/2 - width/2 - math.floor(length - 1)*gap/2 + (index - 1)*gap,parentHeight/2 - height/2)
		end
		self.costCvs.Visible = true
	else
		self.costCvs.Visible = false 
	end
end

 
local function drawPoints(self,node,rankMap)

	local pointCvs = UIUtil.FindChild(node,'cvs_pulsepointlayer')
	local point = UIUtil.FindChild(node,'cvs_pulsepoint')	
	point.Visible = false

	local levelPoints = {}
	for _, v in ipairs(rankMap) do
 		-- print_r(v)
 		local newPoint = point:Clone()
 		newPoint.Visible = true
 		newPoint.IsGray = true
 		newPoint.Position2D = UnityEngine.Vector2(v.pointx,v.pointy)
		pointCvs:AddChild(newPoint)

		local pLabel = UIUtil.FindChild(newPoint,'lb_pulsename')
		local name =  MountModel.GetMountVeinNameByLevel(v.vein_level)
		pLabel.Text =  Util.GetText(name)
 

		levelPoints[v.vein_level] = newPoint
 	end

 	return levelPoints
end


local  function drawGauge(gauge,p1,p2,pSize)
	local addSize = pSize  
	-- print_r('gauge.Size2D:',gauge.Size2D)
 	p1 = p1 + addSize  
 	p2 = p2 + addSize 

	local newGauge = gauge:Clone()


	local x = p2.x - p1.x 
	local y = p2.y - p1.y

	local length = math.sqrt(x*x + y*y)
	newGauge.Size2D = UnityEngine.Vector2(length,gauge.Size2D.y)

	local v = 0
	if y ~= 0 then
		v = math.deg(math.atan(x/y))	
	end

 	if v ~= 0 then
 		if y < 0 then
 			v = v + 90
 			p1.x = p1.x - gauge.Size2D.y / 2
 		else
 			v = v - 90
 			p1.x = p1.x + gauge.Size2D.y / 2
 		end
 		newGauge.Transform.rotation = Quaternion.Euler(0, 0, v);
 	else
 		p1.y = p1.y - gauge.Size2D.y / 2
 	end

 	newGauge.Visible = true
	newGauge:SetGaugeMinMax(0,100)
	newGauge.Position2D = p1
	newGauge.Value = 0

	return newGauge
end

local function drawGauges(self,node,rankMap)

	local gaugeCvs = UIUtil.FindChild(node,'cvs_ggpulselayer')	
	local gauge = UIUtil.FindChild(node,'gg_pulse1')
	gauge.Visible = false

	local levelGauges = {}

	local point = UIUtil.FindChild(node,'cvs_pulsepoint')
	local pointSize = point.Size2D / 2

	local endPos = #rankMap - 1
	-- print('drawGauges endPos:',endPos)
	for i = 1, endPos do
		-- print('drawGauges :',i)

		local lhs = rankMap[i]
		local rhs = rankMap[i+1]

		if lhs == nil or rhs == nil then
			break
		end

		local beginPoint = UnityEngine.Vector2(lhs.pointx,lhs.pointy)
		local endPoint = UnityEngine.Vector2(rhs.pointx,rhs.pointy)

		local newGauge = drawGauge(gauge,beginPoint,endPoint,pointSize)
		gaugeCvs:AddChild(newGauge)

		levelGauges[i] = newGauge
	end

	return levelGauges
end


local function initVeinDetial(self)
	-- body
 
	local starPoints = {}
	local starGauges = {}
 	
 	local starPointSelecteds = {}

	
	for i=1,7 do
		local point = self.ui.comps['cvs_throbpoint' .. i]
		starPoints[i] = point
		starPointSelecteds[i] = self.ui.comps['ib_select' .. i]
	end

	local gaugeCvs = self.ui.comps.cvs_ggthroblayer
 	local gauge = self.ui.comps.gg_throb1
 	gauge.Visible = false

 	local pp1 = self.ui.comps.cvs_throbpoint1
	local pSize  = pp1.Size2D / 2

	for i = 1,6 do 
		local p1 = starPoints[i].Position2D
		local p2 = starPoints[i+1].Position2D
		local newGauge = drawGauge(gauge,p1,p2,pSize)
		gaugeCvs:AddChild(newGauge)
 		starGauges[i] = newGauge

	end

	self.starPoints = starPoints
	self.starGauges = starGauges
	self.starPointSelecteds = starPointSelecteds

end 

local function createVeinRank(self,node,index) 

	local veinRank = index + 1
	local rankMap = self.moutVeinPointMap[veinRank] 
	-- print_r(rankMap)
	if rankMap == nil then
		node.Visible = false
		return
	end 

	local bgname = MountModel.GetMountVeinBgByRank(veinRank)
	local img = 'dynamic/TL_mountbg/' .. bgname ..'.png'
	UIUtil.SetImage(node,img)

	local points = drawPoints(self,node,rankMap)
	self.rankPoints[veinRank] = points
	self.maxLevel = #points

	local gauges = drawGauges(self,node,rankMap)
	self.rankGauges[veinRank] = gauges
end


 
local  function showLevelName(self,level)
	-- body
	local name = MountModel.GetMountVeinNameByLevel(level)
	self.starNameLabel.Text = Util.GetText(name)
end

local function InitStarPanel(self,veinRank)
	
	-- print('InitStarPanel.',self.rank,self.level,self.star)
	-- setUseBtnStatus(self,false)
	local starPoints = self.starPoints
	local starGauges = self.starGauges
 
 	if starPoints == nil then
 		print('UI Close ?')
 		return
 	end
 	
 	local star =  self.star

	for i = 1,#starPoints do
	 	starPoints[i].IsGray = (star < i)

	 	if i == star + 1 then
	 		self.starPointSelecteds[i].Visible = true
	 		SmallStarNextEffect(self,self.starPoints[i])
	 	else
			self.starPointSelecteds[i].Visible = false
	 	end

	 	if i <= #starGauges then
	 		local show = (star > i)
	 		if show then
	 			starGauges[i].Value = 100
	 		else
	 			starGauges[i].Value = 0
	 		end
	 	end
	end

	if self.level == self.maxLevel then
		showLevelName(self,self.level)
	else 
		showLevelName(self,self.level+1)
	end

end

--TODO
local function showVeinRank(self,veinRank)

	local levelPoints = self.rankPoints[veinRank]
	local levelGauges = self.rankGauges[veinRank]

	-- print('showVeinRank111111111111111111111111111111111:',self.rank,self.level,self.star)

	local level = 0
	if self.rank > veinRank then
		level = #levelPoints	 
	elseif self.rank == veinRank then
		level = self.level

		if level < self.maxLevel then
			local nextPoint = levelPoints[level+1]
 			BigLevelNextEffect(self,nextPoint)
		else
			if veinRank < self.maxRank then
				local nextPoint = self.rankPoints[veinRank + 1][1]
 				BigLevelNextEffect(self,nextPoint)
			end
		end
	end

	-- print('showVeinRank111111111111111111111:',level)
	for i = 1,#levelPoints do
	 	levelPoints[i].IsGray = (level < i)	 	 
	 	if i <= #levelGauges then
	 		-- levelGauges[i].Visible = (level > i)
	 		local show = (level > i)
	 		if show then
	 			levelGauges[i].Value = 100
	 		else
	 			levelGauges[i].Value = 0
	 		end
	 	end
	end

end

local function endGuage(self)
	if self.lastTimerId ~= nil then
		LuaTimer.Delete(self.lastTimerId)
		if self.lastNewGauge ~= nil then
			self.lastNewGauge.Visible = true
			self.lastNewGauge.Value = 100
		end
	end
end 

local function scheduleGuage(self,gauge,finishCb)
	local setp = 25
 	local addMax = false
 	gauge.Value = 0
 	gauge.Visible =  true
 	self.lastNewGauge = gauge
	self.lastTimerId = LuaTimer.Add(0,33,function(id)
		local value = gauge.Value + setp
		-- print("LuaTimer value :",value)
		if value > 100 then
			value = 100
			gauge.Value = value
			if finishCb ~= nil then
				finishCb()
			end
			return false
		end
		gauge.Value = value
		return true
	end)
end

local function showFight(self)

	-- print('ShowFight self.veinId:',self.veinId)
	if self.veinId == 0 then
		-- self.fightLabel.Text = 'z' .. 0
		self.fightLabel.Text = 0
		return
	end
	 
 	local fight = MountModel.GetFightByVeinId(self.veinId)
 	-- self.fightLabel.Text = 'z' .. fight
 	self.fightLabel.Text = fight
end  


local  function ShowFightAndStarAddAttr(self)
	-- body
	showFight(self)

	showNextStarAttr(self)
end 

local function showAfterAddStar(self)
	-- body
	setUseBtnStatus(self,true)

	showCost(self)

	ShowFightAndStarAddAttr(self)

	self.ui.comps.cvs_effect.Visible = false
end 


local function ShowUnlockMountAlter(self)
	local rank = self.rank
 	-- print('ShowUnlockMountAlter rank:',rank)
	 
	local mountData = GlobalHooks.DB.FindFirst('Mount',{unlock = rank})

	if mountData == nil then
		return
	end

	-- print('ShowUnlockMountAlter mountData.avatar_id:',mountData.avatar_id)
	-- print_r('self.userMoutMap:',self.userMoutMap)
	if self.userMoutMap[mountData.avatar_id] then
		-- print_r('self.userMoutMap:',self.userMoutMap)
		return
	end


	local name =  Util.GetText(mountData.name)
	local tips = Util.GetText('mount_unlock')

	local message = Util.Format1234(tips,name)
 -- 	local alertUI = UIUtil.ShowOkAlert(message)

 	-- UnlockEffect(self,alertUI)

 	self.ui.comps.cvs_notice.Visible = true
 	UnlockEffect(self,self.ui.comps.cvs_notice)

	local texBox = self.ui.comps.tb_common
	if texBox then
		texBox.XmlText = "<a>" .. message .. "</a>"
		texBox.TextComponent.Anchor = CommonUI.TextAnchor.C_C 
    end
 
 	
end



local function ResetStarPanel(self)
 	InitStarPanel(self)
end

local function addLevel(self,finishCb)
	self.star = 0
	self.level = self.level + 1
	
	-- print('self.level:',self.level)

	local rankPoints = self.rankPoints[self.currentRank]
	if not rankPoints then
		print(' self.currentRank rankPoints is null :',self.currentRank)
		if finishCb then 
			finishCb(self)
		end
		return
	end

	local point = rankPoints[self.level]
	if not point then
		print(' self.level point is null :',self.level)
		if finishCb then 
			finishCb(self)
		end
		return
	end

	BigLevelActEffect(self,point)

	if self.level == 1 then
		point.IsGray = false
		ResetStarPanel(self)

	 	local nextPoint = rankPoints[self.level+1]
 		BigLevelNextEffect(self,nextPoint)

		if finishCb then 
			finishCb(self)
		end
		return
	end

	local gauge = self.rankGauges[self.currentRank][self.level-1]
	scheduleGuage(self,gauge,function()
		-- body
		point.IsGray = false
	
		-- 满阶满级  星星为0
		local nextPoint = self.rankPoints[self.currentRank][self.level+1]
		-- if self.level == self.maxLevel then
		--新的满级判断
		if not nextPoint then
			if self.rank == self.maxRank then
				if self.nextLvId ~= nil then
					RenderSystem.Instance:Unload(self.nextLvId)
				end
				if self.nextStarId then
					RenderSystem.Instance:Unload(self.nextStarId)
					self.nextStarId = nil
				end
			else
				self.level = 0
				self.rank = self.rank + 1
				
				local nextPoint = self.rankPoints[self.currentRank][1]
 				BigLevelNextEffect(self,nextPoint)
				self.pan:ShowNextPage()
				ShowUnlockMountAlter(self)
			end	
		else
			-- local nextPoint = self.rankPoints[self.currentRank][self.level+1]
 			BigLevelNextEffect(self,nextPoint)

 			self.star = 0
		end

		ResetStarPanel(self)

		if finishCb then 
			finishCb(self)
		end
	end)
end

local function addStar(self,finishCb)
	self.star = self.star + 1

	-- print('self.star:',self.star)

	local point = self.starPoints[self.star]

	SmallStarActEffect(self,point)

	--从0到1
	if self.star == 1 then
		point.IsGray = false

		self.starPointSelecteds[self.star].Visible = false
		self.starPointSelecteds[self.star+1].Visible = true

		SmallStarNextEffect(self,self.starPoints[self.star+1])

		if finishCb then
			finishCb(self)
		end
		return
	end

	local gauge = self.starGauges[self.star-1]
	scheduleGuage(self,gauge,function()
		-- body
		point.IsGray = false
 
		local maxStar = #self.starPoints
		if self.star == maxStar then

			self.starPointSelecteds[1].Visible = true 
			SmallStarNextEffect(self,self.starPoints[1])

			self.starPointSelecteds[maxStar].Visible = false
			addLevel(self,finishCb)

			Star7Effect(self,point)

		else 
		 	
		 	local point = self.starPointSelecteds[self.star]
		 	if point then
		 		point.Visible = false
		 	end
		 	local nextPoint = self.starPointSelecteds[self.star+1]
			if nextPoint then
		 		nextPoint.Visible = true
		 		SmallStarNextEffect(self,self.starPoints[self.star+1])
		 	end
 
			if finishCb then
				finishCb(self)
			end
		end
	end)
end
 
local function showScrollPan(self)
  -- body
	local function eachCreateCb(node, index) 
	 	-- print('showScrollPan createCB: ',index)
	 	createVeinRank(self,node,index)
	end
 
 	local cvspulse1 = self.ui.comps.cvs_pulse1
	local veinRankLabel = UIUtil.FindChild(cvspulse1,'lb_name')

 	local moutVeinPointMap = self.moutVeinPointMap
	-- ConfigPageScrollPan(self,#moutVeinPointMap,eachupdatecb)
	local function eachUpdateCB(index)
		-- print('UpdateCB:',index)
		local veinRank = index + 1
		-- print('UpdateCB veinRank:',veinRank)
		self.currentRank = veinRank
		-- print('eachUpdateCB self.currentRank: ',self.currentRank)

		local rankName = MountModel.GetMountVeinNameByRank(veinRank)
		veinRankLabel.Text =  Util.GetText(rankName)
 		-- self.starNameLabel.Text = rankName

		if index == 0 then
			self.btnLeft.Visible = false
			self.btnRight.Visible = true
		elseif index == #moutVeinPointMap -1 then
			self.btnLeft.Visible = true
			self.btnRight.Visible = false
		else
			self.btnLeft.Visible = true
			self.btnRight.Visible = true
		end

		showVeinRank(self,veinRank)
	end 

	ConfigPageScrollPan(self,#moutVeinPointMap,eachCreateCb,eachUpdateCB)

end

-- local function updateNetData(self,netData )

-- 	-- print_r('updateNetData netData:',netData)
-- 	self.userMoutMap = netData.userMoutMap
-- 	self.veinId = netData.veinId
-- 	-- print('updateNetData self.veinId:',self.veinId)

-- 	if self.veinId > 0 then
-- 		local veinData = MountModel.GetVeinData(self.veinId)
-- 		-- print_r('updateNetData veinData1111111111111111111111111:',veinData)
	
-- 		self.star = veinData.vein_star
-- 		self.level = veinData.vein_level
-- 		self.rank = veinData.vein_rank

-- 		if self.star == self.maxStar and self.level == self.maxLevel and self.rank == self.maxRank then
-- 			-- print('updateNetData22222:',self.rank,self.level,self.star)
-- 			self.star = 0
-- 			return
-- 		end
	
-- 		if self.level == self.maxLevel then
-- 			if self.star == self.maxStar then
-- 				self.star = 0
-- 				self.level = 0
-- 				self.rank = self.rank + 1
-- 				return
-- 			end 
-- 		end

-- 		self.level = self.level - 1
-- 		if self.star == self.maxStar then
-- 			self.star = 0
--  			self.level = self.level + 1
--  		end

-- 	else
-- 		self.rank = 1
-- 		self.level = 0
-- 		self.star = 0 
-- 	end

-- 	-- print('updateNetData3333333333:',self.rank,self.level,self.star)
-- end 

local function updateNetData(self,netData)
	self.userMoutMap = netData.userMoutMap
	self.veinId = netData.veinId

	-- 0阶
	if self.veinId == 0 then
		self.rank = 1
		self.level = 0
		self.star = 0
		return
	end

	local veinData = MountModel.GetVeinData(self.veinId)
	if not veinData then
		print('服务器和客户端配置不一致，或者配置出现了降低，但是账号是老的')
		return
	end

	local nextVeinData = MountModel.GetNextVeinData(self.veinId)
	--最大阶
	if veinData and  not nextVeinData then
		self.star = 0
		self.level = veinData.vein_level
		self.rank = veinData.vein_rank
		return
	end
 

	if veinData.unlock_id > 0 then
		if nextVeinData then
			self.rank = nextVeinData.vein_rank
			self.star = 0
			self.level = 0
			return
		end
	end

	if veinData.vein_star  == self.maxStar then
		self.star = 0
		self.level = veinData.vein_level
		self.rank = veinData.vein_rank
		return
	end

	self.star = veinData.vein_star
	self.level = veinData.vein_level - 1
	self.rank = veinData.vein_rank
end

local function initData(self)
	-- body
	self.rank = 1
	self.level = 0
	self.star = 0 
	self.veinId = 0
	self.currentRank = 1
end


local function showPage(self)
	-- body
	-- print('showPage self.rank:',self.rank)
	
	self.currentRank = self.rank

	self.pan:ShowPage(self.rank - 1)
	

end

function _M.OnEnter( self, ...)
 	self.exit = false
	initData(self)

	-- print('self.maxRank,self.maxVeinId:',self.maxRank,self.maxVeinId)

    MountModel.RequestMountInfo(function (data)
    	-- body 
	 	updateNetData(self,data)
 		-- showVeinRank(self,1)
 		if self.rank == 1 then
			showVeinRank(self,1)
		end

 		showPage(self)
 		
		InitStarPanel(self) 

		showAfterAddStar(self) 

    end,true)
end

function _M.OnExit( self )
	endGuage(self)
	EffectUnload(self)
	self.exit = true
	-- print('MountVeins OnExit')
end

function _M.OnDestory( self )
	-- print('MountVeins OnDestory')
	EffectUnload(self)

end


local function InitCompmont(self )
	-- body
	local tipsCvs = self.ui.comps.cvs_tips
	local helpCvs = self.ui.comps.cvs_help
	local attrCvs = self.ui.comps.cvs_attribute

	self.ui.menu:SetUILayer(tipsCvs)

	local pulse1Cvs = self.ui.comps.cvs_pulse1

	local helpButton =  self.ui.comps.btn_help
	helpButton.TouchClick = function (sender)
		-- body
		tipsCvs.Visible = true
		helpCvs.Visible = true 
	end

	 
	local noticeBtn = self.ui.comps.btn_enter
	noticeBtn.TouchClick = function ( sender )
		self.ui.comps.cvs_notice.Visible = false
	end

	local noticeCloseBtn = self.ui.comps.btn_close3
	noticeCloseBtn.TouchClick = function ( sender )
		self.ui.comps.cvs_notice.Visible = false
	end

	self.ui.menu:SetUILayer(self.ui.comps.cvs_notice)
	self.attrBtn = self.ui.comps.btn_attr
	self.attrBtn.TouchClick = function ( sender)
		-- body
		tipsCvs.Visible = true
		attrCvs.Visible = true
		showAttribute(self)
	end

	local attrCvs = self.ui.comps.cvs_attribute
	local closeBtn = self.ui.comps.btn_close22
	closeBtn.TouchClick = function (sender)
		tipsCvs.Visible = false
		attrCvs.Visible = false
		helpCvs.Visible = false
	end

	tipsCvs.TouchClick = function ( sender )
		tipsCvs.Visible = false
		attrCvs.Visible = false
		helpCvs.Visible = false
	end

	local pulse2Cvs = self.ui.comps.cvs_pulse2
	self.starNameLabel = UIUtil.FindChild(pulse2Cvs,'lb_name')
	
 	initVeinDetial(self)
	self.pan = self.ui.comps.sp_mountsp
 	self.tempnode = self.ui.comps.cvs_mountbg
 	self.tempnode.Visible = false
 	

 	self.costCvs = self.ui.comps.cvs_cost
	self.costItemCvs = self.ui.comps.cvs_costitem 
	self.costItemCvs.Visible = false

	self.fightLabel = self.ui.comps.lb_fight


	self.starAttrNameLabel = self.ui.comps.lb_shuxing
	self.starAttrLabel = self.ui.comps.lb_shuxingnum

	self.btnLeft = self.ui.comps.btn_left
	self.btnLeft.TouchClick = function ( sender )
		self.pan:ShowPrevPage()
	end

	self.btnRight = self.ui.comps.btn_right
	self.btnRight.TouchClick = function ( sender )
		self.pan:ShowNextPage()
	end


 	
 	_M.AddStar = function (self,v1,v2)
 		-- body
 		-- print('v1,v2:',v1,v2)
 		if self.exit == true then
 			return
 		end
 		if v1 == v2 then
 			showAfterAddStar(self)
 			return 
 		else 
 			addStar(self,function ()
 				v1 = v1 + 1
 				_M.AddStar(self,v1,v2)
 				SoundManager.Instance:PlaySoundByKey('dianliang',false)
 			end)
 		end
 	end

	self.btnUse = self.ui.comps.btn_use
	self.btnUse.TouchClick = function ( sender )

		--测试
		--AndvaceEffect(self)

		setUseBtnStatus(self,false)
		self.ui.comps.cvs_effect.Visible = true
		-- print('RequestStarUp self.rank:',self.rank)
		-- self.pan:ShowPage(self.rank - 1)
		showPage(self)

		MountModel.RequestStarUp(0,
			function (rsp)
		 		_M.AddStar(self,self.veinId,rsp.veinId)
		 		self.veinId = rsp.veinId
		 		--SoundManager.Instance:PlaySoundByKey('dianliang',false)
		 		MountModel.CheckVeinUp()
		 	end,
		 	function ()
		 		setUseBtnStatus(self,true)
		 		self.ui.comps.cvs_effect.Visible = false
		 	end)
	end

	self.btn1KeyUse = self.ui.comps.btn_alluse
	self.btn1KeyUse.TouchClick = function ( sender )
		setUseBtnStatus(self,false)
		self.ui.comps.cvs_effect.Visible = true
		-- print('RequestStarUp self.rank:',self.rank)
		-- self.pan:ShowPage(self.rank - 1)
		showPage(self)

		MountModel.RequestStarUp(1,
			function (rsp)
		 		_M.AddStar(self,self.veinId,rsp.veinId)
		 		self.veinId = rsp.veinId 
				GlobalHooks.UI.SetRedTips('mount', 0)		
		 	end,
		 	function ()
		 		setUseBtnStatus(self,true)
		 		self.ui.comps.cvs_effect.Visible = false
		 	end)
	end

end

local function loadData(self)
	
	self.maxRank = 0
	self.maxLevel = 10
	self.maxStar = 7

	local all = GlobalHooks.DB.GetFullTable('MountVeinPoint')

	for _, v in ipairs(all) do
		local rankMap = self.moutVeinPointMap[v.vein_rank]
		if rankMap == nil then
			rankMap = {}
			self.moutVeinPointMap[v.vein_rank] = rankMap
		end
		rankMap[v.vein_level] = v
	end

	self.maxRank = #self.moutVeinPointMap

	-- print('self.maxRank,self.maxVeinId:',self.maxRank,self.maxVeinId)
end

function _M.OnInit( self )
	-- print('MountVeins OnInit Star')
 
	self.rankGauges = {}
	self.rankPoints = {}
	self.moutVeinPointMap = {}

	loadData(self)
 
	initData(self)
 
	InitCompmont(self)

	showScrollPan(self)

	-- self.maxVeinId = self.maxStar * self.maxLevel * self.maxRank  
	-- print('self.maxVeinId:',self.maxVeinId)	
	-- print('MountVeins OnInit End')
end

return _M