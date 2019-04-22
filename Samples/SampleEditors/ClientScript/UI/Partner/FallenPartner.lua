local _M = {}
_M.__index = _M

local Util  = require "Logic/Util"
local PartnerUtil = require 'UI/Partner/FallenPartnerUtil'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local PartnerModel = require'Model/FallenPartnerModel'
local DisplayUtil = require "Logic/DisplayUtil"
local Helper = require'Logic/Helper'


-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
local curSelect = 1
local AttrType = {
	'attack',
	'maxhp',
	'defend',
	'mdef',
	'thunderdamage',
	'winddamage',
	'icedamage',
	'firedamage',
	'soildamage',
	'thunderresist',
	'windresist',
	'iceresist',
	'fireresist',
	'soilresist',
}

local ActiveState = {
	HasActived = 0,
	CanActive = 1,
	NoActive = 2
}

local showtime
--统一显示*********************


local function ReleaseAll3DEffect(self)
	if self.effect then
		for _, val in pairs(self.effect) do
			RenderSystem.Instance:Unload(val)
		end
		self.effect = {}
	end
end


--加载一般控件特效
local function Init3DModel(self, parentCvs, pos2d,scale, menuOrder, fileName)
	-- local transSet = TransformSet()
	-- transSet.Pos = Vector3(pos2d.x, -pos2d.y,-200)
	-- transSet.Parent = parentCvs.Transform
	-- transSet.Layer = Constants.Layer.UI
	-- transSet.LayerOrder = menuOrder
	-- self.effect = self.effect or {}
	-- self.effect[#self.effect + 1] = RenderSystem.Instance:PlayEffect(fileName, transSet)

	 -- 	self.LastInfo = self.LastInfo or {}
		-- if self.modelcache[modelid] then
		-- 	self.LastInfo.id = self.modelcache[modelid].ID
		-- 	self.modelcache[modelid].gameObject:SetActive(true)
		-- else
		-- 	local state = "n_idle"
		-- 	if ShowTabel[modelid] == nil and isNpc then
		-- 	  ShowTabel[modelid] = true
		-- 	  state = "n_talk"
		-- 	end
	 --  		self.LastInfo.id = QuestUtil.Init3DSngleModel(parentCvs, pos2d, scale, menuOrder,fileName,state)
	  		
		-- end

	pos2d.y = -pos2d.y
	local param = 
   	{
	   	Pos = pos2d,
	   	Parent = parentCvs.UnityObject.transform,
		LayerOrder = menuOrder,
		Scale = scale,
	  	UILayer = true,
    }
    self.effect = self.effect or {}
    local num = #self.effect + 1
	self.effect[num] = Util.PlayEffect(fileName,param)
	return self.effect[num]

end


--加载可激活特效（scroll比较特殊，专门用来加载主页面仙侣可激活特效）
local function InitEffect(self, parentCvs, pos2d,scale, menuOrder, fileName)
	pos2d.y = -pos2d.y
	local param =
	{
		Pos = pos2d,
		Parent = parentCvs.UnityObject.transform,
		LayerOrder = menuOrder,
		Scale = scale,
		UILayer = true,
		Clip=self.pan.Transform
	}
	self.effect = self.effect or {}
	local num = #self.effect + 1
	self.effect[num] = Util.PlayEffect(fileName,param)
	return self.effect[num]
end


local function ShowSkillItemNode(self,detail,parentnode,touchcb,itemnum)
	-- parentnode.Layout =  nil
	-- parentnode:RemoveAllChildren(true)
	-- parentnode.EnableChildren = true
	-- local itemShow = ItemShow.Create(detail.static.atlas_id,detail.static.quality, itemnum or 0)
	-- itemShow.Size2D = parentnode.Size2D
	-- itemShow.EnableTouch = true
	-- itemShow.TouchClick = function(sender)
	-- 	if touchcb then
	-- 		touchcb(sender)
	-- 	end
	-- end
	-- parentnode:AddChild(itemShow)
	-- itemShow.Position2D = UnityEngine.Vector2(0,0)
	-- return itemShow
end

--是否激活
local function isActive(self,Roleid)
   for i,v in ipairs(PartnerModel.PartnerList) do
      if v.ID == Roleid then
      	--print("isActive",Roleid,v.Lv)
        return true,v.Lv
      end
   end
   return false,0
end
--获得等级
local function GetLv(self,index)
    local PartnerData = self.PartnerList[index]
    local isActive,lv = isActive(self,PartnerData.god_id)
   	return lv
end
--激活状态
local function GetActiveState(self,index)
    local PartnerData = self.PartnerList[index]
    local isActive = isActive(self,PartnerData.god_id)
    if isActive then
    	return ActiveState.HasActived    -- 已激活
    else --获取背包里激活物品的数量
    	 local Num = PartnerUtil.GetItemCountByItemID(tonumber(PartnerData.god_item))--ItemModel.CountItemByTemplateID(tonumber(PartnerData.god_item)),
         if Num >= PartnerData.item_num then --如果数量大于激活数量，返回可激活
         	return ActiveState.CanActive --可激活
         else 
         	return ActiveState.NoActive--未激活
         end
    end
end

local function GetActiveStateById(self,list,id)
    for i,v in ipairs(list) do
    	if v.god_id == id then
    		local PartnerData = v
		    local isActive = isActive(self,PartnerData.god_id)
		    if isActive then
		    	return ActiveState.HasActived    -- 已激活
		    else
		    	 local Num = PartnerUtil.GetItemCountByItemID(tonumber(PartnerData.god_item))--ItemModel.CountItemByTemplateID(tonumber(PartnerData.god_item)),
		         if Num >= 1 then
		         	return ActiveState.CanActive --可激活
		         else 
		         	return ActiveState.NoActive--未激活
		         end
		    end
	   	end
    end

end


local function GetRank(self,lv)

	local advanceData = PartnerUtil.GetAdvanceData(self.curRoleData.god_id,lv or self.curRoleData.Lv)
	if advanceData ~= nil then
		return advanceData.client_rank
	else
		error('error rank with',self.curRoleData.god_id,self.curRoleData.Lv)
	end
end
--*********************

-- 仙侣属性 = 等级对应属性[取partner_level中的数据] * 资质系数[取partner_qualification中的数据]/10000 + 
-- 品质等级对应属性[取partner_quality中的数据] + 仙侣装备属性

-- 仙侣装备属性 = 装备基础属性[取partner_equip_advance中的数据] + 强化属性增长[取partner_equip_refine中的数据] * 强化等级


--技能显示界面
local function ShowCurSelect(self,curselect,isShow)
	--print("curselect",curselect,isShow)
	-- local node = self.ui.comps.cvs_tips
	-- node.Visible = isShow
	--self.selectCanvas.Visible = isShow
	-- if not isShow then
	-- 	return 
	-- end
	-- local skillID = self.curRoleData.skill.id[curselect]
	-- local skillname = UIUtil.FindChild(node,'lb_skillname',true)
	-- local skilllv = UIUtil.FindChild(node,'lb_lv',true)
	-- local skilldes = UIUtil.FindChild(node,'tb_des',true)
	-- local ib_icon = UIUtil.FindChild(node,'ib_icon',true)
	-- ib_icon.Visible = true
	-- --print("skillID",skillID,self.curRoleData.Lv)
	-- local RankLv = GetRank(self)
	-- local skilldatastatic = PartnerUtil.GetSkillData(skillID,RankLv)
	-- if skilldatastatic == nil then 
	-- 	error("skilldatastatic is nil",skillID,RankLv)
	-- 	return
	-- end
	-- local detail = {static = {atlas_id = self.curRoleData.skill.icon[curselect],quality = self.curRoleData.god_quality}}
	-- local itemShow = ShowSkillItemNode(self,detail,ib_icon)
	-- itemShow.EnableTouch =false

	-- skillname.Text = Util.GetText(skilldatastatic.skill_name)
	-- skillname.FontColor = GameUtil.RGB2Color(Constants.QualityColor[self.curRoleData.god_quality])
	-- skilllv.Text = RankLv --string.format(Constants.Text.partner_skilllv,skilldatastatic.skill_level)
	-- skilldes.Text = Util.GetText(skilldatastatic.skill_desc)
	-- local point = self.ui.comps.cvs_tips:GlobalToLocal(self.skill[curselect]:LocalToGlobal(),true)
	-- local cvs_skilltips = UIUtil.FindChild(node,'cvs_skilltips',true)
	-- cvs_skilltips.Position2D = point
	-- self.selectCanvas.Position2D = self.skill[curselect].Position2D
	-- cvs_skilltips.Y = point.y - cvs_skilltips.Height
	-- self.ui.menu:SetUILayer(node)
	-- node.event_PointerUp = function(displayNode, pos)
	-- 	ShowCurSelect(self,curselect,false)
	-- end
	
end
local function setSkillByPos(self,pos,RankLv)
	local skillId = self.curRoleData.skill.id[pos]
	local skillData = unpack(GlobalHooks.DB.Find('FallenPartnerSkillData',{skill_id = skillId}))
	local node = UIUtil.FindChild(self.ui.comps.cvs_partnershow,'cvs_skill'..pos,true)
	-- local ib_icon = UIUtil.FindChild(node,'ib_icon',true)
	-- ib_icon.Visible = true
	local SkillData = PartnerUtil.GetSkillData(skillId,RankLv)
	--print_r(SkillData)
	if SkillData ~= nil then
		local detail = {static = {atlas_id = self.curRoleData.skill.icon[pos],quality = self.curRoleData.god_quality}}
		--local itemShow = ShowSkillItemNode(self,detail,ib_icon)
		local itshow = UIUtil.SetItemShowTo(node, detail.static.atlas_id,0)
		itshow.IsCircleQualtiy = true
	    itshow.EnableTouch = true
	    itshow.UserTag = pos
	    -- itshow.X = node.Width/2 - itshow.Width/2
	    -- itshow.Y = node.Height/2 - itshow.Height/2
	    itshow.TouchClick = function(sender)
			local skilldatastatic = PartnerUtil.GetSkillData(skillId,RankLv)
			if skilldatastatic == nil then 
				error("skilldatastatic is nil",skillId,RankLv)
				return
			end
			--todo 仙侣技能先暂时写死1级 by老王 2018/12/27/14:48
			local RankLv = 1--GetRank(self)
			local itemdetail = UIUtil.ShowNormalSkillDetail(
				self.curRoleData.skill.icon[pos],
				self.curRoleData.god_quality,
				Util.GetText(skilldatastatic.skill_name),
				RankLv,
				skilldatastatic.skill_desc,itshow,self)
			local point = self.ui.root:GlobalToLocal(self.skill[pos]:LocalToGlobal(),true)
			itemdetail:SetPos(point.x,point.y - itemdetail.comps.cvs_itemtips.Height - 5)
	    end
	end

end

local function Release3DModel(self)

	if self.assetLoader then
		self.assetLoader:Unload()
		self.assetLoader = nil
	elseif self.loaderid then
		self.loaderid:Discard()
	end
	if self and self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DSngleModel(self, parentCvs, fixpos,scale, rotate,menuOrder,fileName,cb)
	self.model = UIUtil.InitFix3DSngleModel(parentCvs,fixpos, scale,rotate, menuOrder,fileName,true,cb)
	-- UI3DModelAdapter.SetLoadCallback(self.model.Key,function(UIModelInfo)
	-- 	local time = UIModelInfo.Anime:GetAnimTime("n_special")
	-- 	UIModelInfo.Anime:Play("n_special")
	-- 	if showtime ~= nil then
	-- 		LuaTimer.Delete(showtime)
	-- 	end
	-- 	showtime = LuaTimer.Add(time*1000,function()
	-- 		if UIModelInfo.Anime ~= nil then
	-- 			UIModelInfo.Anime:CrossFade("n_idle",0.15)
	-- 		end
	-- 	end)
	-- end)
end

local function ReloadModel(self)
		if self == nil then
			return
		end

		if self.lastAvatar ~= self.curRoleData.avatar_res then
			self.lastAvatar = self.curRoleData.avatar_res 
		else
			return
		end
		local filename = self.curRoleData.avatar_res
	
		if not string.empty(filename) then
			Release3DModel(self)
			local fixposdata = string.split(self.curRoleData.pos_xy,',')
			local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
		    local fixzoom = tonumber(self.curRoleData.zoom) -- 缩放比例
		    local rotate = 0
		    --local pos2d = self.ui.comps.cvs_anchor.Position2D
			Init3DSngleModel(self, self.ui.comps.cvs_model, fixpos,fixzoom, rotate,self.ui.menu.MenuOrder + 1,filename,function(info)
				local path = PartnerUtil.GetRankEffect(self.curRoleData.god_id,GetRank(self))
				if not string.IsNullOrEmpty(path) then
					local arr = string.split(path, '/')
				    local name = arr[#arr]
				    arr = string.split(name, '.')
				    if #arr > 1 then table.remove(arr) end
				    name = table.concat(arr, '.')
					local id = FuckAssetObject.GetOrLoad(path,name, function(assetLoader)
						self.assetLoader = assetLoader
						--self.assetLoader.DontMoveToCache = true
						local gameobj = assetLoader.gameObject
        				UILayerMgr.SetLocalLayerOrder(gameobj, self.ui.menu.MenuOrder, false, 5)
        				gameobj.transform:SetParent(info.RootTrans)
        				gameobj.transform.localPosition = Vector3(0, 0, 0) 
        				gameobj.transform.localScale = Vector3(1,1,1)
					end)
					self.loaderid = FuckAssetLoader.GetLoader(id)
		    	else
		    		print("model is nil",self.curRoleData.god_id,rank)
				end
			end)
		end
end

local function ShowRole(self,lv)

	ReloadModel(self)
	local advanceData = PartnerUtil.GetAdvanceData(self.curRoleData.god_id,lv or self.curRoleData.Lv)

	if advanceData ~= nil then

		if lv == nil then
			self.curStar = advanceData.client_star
		end
		local rank = Constants.Text['fallenpartner_rank'..advanceData.client_rank]
		--品阶
	    --local _name = self.curRoleData.god_name..'·'..rank

		-- self.ui.comps.lb_name.Text = _name
		-- self.ui.comps.lb_name.FontColor = GameUtil.RGB2Color(Constants.QualityColor[tonumber(self.curRoleData.god_quality)])

		PartnerUtil.SetNodeName(self.ui.comps.lb_name,Util.GetText(self.curRoleData.god_name),rank,self.curRoleData.god_quality)
		self.lb_fightMain=self.ui.comps.cvs_partnerskill:FindChildByEditName('lb_fight2',true)
		
		--新增仙侣技能战斗力
		--todo 仙侣技能先暂时写死1级 by老王 2018/12/27/14:48
		local RankLv = 1--GetRank(self,nextlv or self.curRoleData.Lv)
		local skillFight=0
		for i = 1, 3 do
			local  sk = PartnerUtil.GetSkillData(self.curRoleData.skill.id[i],RankLv)
			skillFight=skillFight+sk.skill_fight
		end
		self.lb_fightMain.Text=PartnerUtil.GetPartnerPower(advanceData)+skillFight
		--self.ui.comps.lb_fight.Text = PartnerUtil.GetPartnerPower(advanceData)--self.curRoleData.FightPower
		self.ui.comps.lb_quality.Text = rank
		--print("x",self.ui.comps.lb_name.TextSprite.PreferredSize.x)
		--self.ui.comps.lb_quality.X = self.ui.comps.lb_name.X + self.ui.comps.lb_name.Width/2 + self.ui.comps.lb_name.TextSprite.PreferredSize.x /2

		if lv == nil then
			self.ui.comps.gg_exp.Value = advanceData.client_lv/advanceData.client_lv_max
		end
		self.ui.comps.gg_exp.Text = advanceData.client_lv..'/'..advanceData.client_lv_max
		
		self.ui.comps.lb_rank.Text = rank
		for i = 1,5 do
			if i<= advanceData.client_star then
				self.ui.comps['cvs_'..i].Enable = true
				if lv ~= nil and self.curStar ~= nil and i > self.curStar then
					local _node = self.ui.comps['cvs_'..i]
					local width = _node.Width/2
					local height = _node.Height/2
					
					Init3DModel(self, _node, Vector3(width,height,0),Vector3.one,self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_interface_lightstar.assetbundles')
				end
			else
				self.ui.comps['cvs_'..i].Enable = false
			end
		end
		self.curStar = advanceData.client_star
		if lv ~= nil then
			local _node = self.ui.comps.cvs_model
			local width = _node.Width/2
			local height = _node.Height
			Init3DModel(self,_node, Vector3(width,height,0),Vector3.one,self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_interface_upgrade.assetbundles')
		end
	else
		print("GetRank error with id",self.curRoleData.god_id,lv or self.curRoleData.Lv)
	end
end


local function CheckUnActiveRedPoint(self)
	local Num
	for i, v in ipairs(PartnerModel.Unactivated) do
		local isshow = isActive(self,v.god_id)
		if not isshow then
			Num = PartnerUtil.GetItemCountByItemID(tonumber(PartnerModel.Unactivated[i].god_item))
			if Num >= PartnerModel.Unactivated[i].item_num then --如果数量大于激活数量，返回可激活
				GlobalHooks.UI.SetRedTips('partner',1)
				return 1
			end
		end
	end
	for i, v in ipairs(self.activePartner) do
		if self.activePartner[i].show ==1 then
			GlobalHooks.UI.SetRedTips('partner',1)
			return 1
		end
	end
	GlobalHooks.UI.SetRedTips('partner',0)
	return 0
end


local function SetCostItem(self,lv)
	
	local itemNodes = {}
	local itemIcons = {}
	local itemDatas = {}
	local costRefineCostData = {}
	local costnode = self.useTrainnode

	self.ui.comps.lb_max.Visible = false
	if self.curRoleData.ActiveState ~= ActiveState.HasActived then
		local partnerdata = self.PartnerList[self.listIndex]
		costRefineCostData.cost ={}
		costRefineCostData.cost.id = {partnerdata.god_item}
		costRefineCostData.cost.num = {partnerdata.item_num}
	else
		
		costRefineCostData = PartnerUtil.GetAdvanceData(tonumber(self.curRoleData.god_id),lv + 1)
		
		 if costRefineCostData == nil then
			self.ui.comps.lb_max.Visible = true
			costnode.Visible = false
		 	return false
		 end
		 costnode.Visible = true
	end
	
		--for i,v in ipairs(costRefineCostData.cost.id) do
			local v = costRefineCostData.cost.id[1]
			if v > 0 then
				
				local detail = ItemModel.GetDetailByTemplateID(v)
				local csv_itemIcon = UIUtil.FindChild(costnode,'ib_icon')
				local costItem = UIUtil.FindChild(costnode,'lb_costnum')
				costnode.Enable = false
				csv_itemIcon.EnableChildren = true
				csv_itemIcon.Visible = true
				csv_itemIcon.Layout =  nil
				costnode.Visible = true
				--csv_itemIcon:RemoveAllChildren(true)
				local count = PartnerUtil.GetItemCountByItemID(v) -- ItemModel.CountItemByTemplateID(v)
				-- if count < costRefineCostData.cost.num[1] then
				-- 	colortext = "<color=#fb1919>"..count.."</color>"
				-- else
				--     colortext = count
				-- end
				--local text = colortext..'/'..costRefineCostData.cost.num[1]
				--设置材料消耗，并支持不足跳转
				--cost 为ItemModel中ParseCostAndCostGroup返回的结构 {detail:cur:need}
				--tipsparams 为控制道具详情的参数 一般是{x:y:anchor}
				--self, cvs_itshow, lb_num, cost, tipsparams
				--local cost = {detail = detail,cur = count,id = costRefineCostData.cost.id[1],need = costRefineCostData.cost.num[1]}
				local costs = ItemModel.ParseCostAndCostGroup(costRefineCostData)
				local cost = costs[1]
				local point = self.ui.root:GlobalToLocal(csv_itemIcon:LocalToGlobal(),true)
				local tipsparams = {x = point.x,y = point.y - 5,anchor = 'r_b',cb = function()
					-- body
				end}
				UIUtil.SetEnoughItemShowAndLabel(self, csv_itemIcon, costItem,cost,tipsparams)
				--costItem.Text = text
				
				-- table.insert(itemNodes,costnode)
				-- table.insert(itemDatas,{v,NotEnough = count < costRefineCostData.cost.num[1]})
				-- table.insert(itemIcons,csv_itemIcon)
			end
		--end

	-- local width = itemNodes[1].Width
	-- local spacing = width + 60 - ((#itemNodes - 2)*30)

	-- for i = 1, #itemNodes do
	-- 	itemNodes[i].Position2D = Vector2(cvs_costlistCanvas.Width/2 - width/2 - math.floor(#itemNodes - 1)*spacing/2 + (i - 1)*spacing,cvs_costlistCanvas.Height/2 - itemNodes[i].Height/2)
	-- end


	 -- DisplayUtil.fillAwards(itemIcons, itemDatas,function(itemShowNode,itemShow)
	 -- 	local point = self.ui.root:GlobalToLocal(itemShowNode:LocalToGlobal(),true)
		-- itemShow:SetPos(point.x ,point.y  - 5,'r_b')
	 -- end,function()
	 -- 	self.ui.menu:Close()
	 -- end
	 --)
	return true
end
local function SetCurRoleAttr(self,nextlv)
	
	--local node = self.ui.comps.cvs_attribute
	local num = 1
	local advanceData = PartnerUtil.GetAdvanceData(self.curRoleData.god_id,nextlv or self.curRoleData.Lv)
	if advanceData ~= nil then
		for i,v in ipairs(AttrType) do

			local value = advanceData[v]
			
			if value > 0 then
				self.ui.comps['lb_shuxing'..num].Text = PartnerUtil.AttributeName(v)
				local ib_up = self.ui.comps['ib_up'..num]
				local lb_shuxingadd = self.ui.comps['lb_shuxing'..num..'add']
				local lb_shuxingnum = self.ui.comps['lb_shuxing'..num..'num']

				ib_up.Visible = false
				lb_shuxingadd.Visible = false
				if nextlv then
					local curvalue = tonumber(lb_shuxingnum.UserTag)
					if value > curvalue then
						lb_shuxingadd.Visible = true
						lb_shuxingadd.Text = (value - curvalue)
						lb_shuxingnum.UserTag = value
						UIUtil.AddNumberPlusPlusTimer(lb_shuxingnum, curvalue, value, 0.5)
						ib_up.Visible = true
						UIUtil.PlayCPJOnce(ib_up,1,function(sender)
							ib_up.Visible = false
						end)
					end
				else
					lb_shuxingnum.Text = value
					lb_shuxingnum.UserTag = value
				end
				num = num + 1
			end
		end
	end

	--激活按钮 和培养按钮
	self.ui.comps.cvs_use.Visible=false
	self.ui.comps.btn_use.Visible = false
	self.ui.comps.btn_alluse.Visible=false
	self.ui.comps.btn_alluse.Visible = false
	self.ui.comps.cvs_jihuo.Visible=false
	self.ui.comps.btn_jihuo.Visible = false
	self.ui.comps.tbt_use.Visible = true
	
	
    local hasCostItem = SetCostItem(self,nextlv or self.curRoleData.Lv) 
	if self.curRoleData.ActiveState == ActiveState.HasActived then
		if hasCostItem then
			self.ui.comps.cvs_use.Visible=true
			self.ui.comps.btn_use.Visible = true		
			self.ui.comps.btn_alluse.Visible=true
			self.ui.comps.btn_alluse.Visible = true 
		end
		self.ui.comps.tbt_use.IsChecked = self:GetFightState(self.curRoleData.god_id)
		if self.ui.comps.tbt_use.IsChecked then
			self.ui.comps.tbt_use.Enable = false
			self.ui.comps.tbt_use.Visible=false
			self.lb_chuzhan.Visible=true
		else
			self.ui.comps.tbt_use.Enable = true
			self.ui.comps.tbt_use.Visible=true
			self.lb_chuzhan.Visible=false
		end
	else	
		self.ui.comps.tbt_use.IsChecked = false
		self.ui.comps.tbt_use.Visible = false
		self.ui.comps.cvs_jihuo.Visible=true
		self.ui.comps.btn_jihuo.Visible = true
		self.lb_chuzhan.Visible=false
		-- if self.curRoleData.ActiveState == ActiveState.CanActive then
		-- 	self.ui.comps.btn_jihuo.IsGray = false
		-- else
		-- 	self.ui.comps.btn_jihuo.IsGray = true
		-- end

	end
	self.ui.comps.tbt_use.TouchClick = function( sender )
		self.ui.comps.tbt_use.Enable = false
		self.ui.comps.tbt_use.Visible=false
		self.lb_chuzhan.Visible=true
		if TLBattleScene.Instance.Actor:isNoBattleStatus()  then
		 	PartnerModel.PartnerFightRequest(self.curRoleData.god_id,function()
		 		self.ui.comps.tbt_use.IsChecked = true
		 		self.ui.comps.tbt_use.Visible=false
				self.lb_chuzhan.Visible=true
		 		self.pan:RefreshShowCell()
		 	end,function()
			 	 self.ui.comps.tbt_use.IsChecked = false
			 	 self.ui.comps.tbt_use.Enable = true
			 	 self.ui.comps.tbt_use.Visible=true
			 	 self.lb_chuzhan.Visible=false
		 	end)
		else
			 GameAlertManager.Instance:ShowNotify(Util.GetText('god_inbattle'))
		end
   		
    end
	if nextlv then
		if self.Timeid ~= nil then
			LuaTimer.Delete(self.Timeid)
		end
		self.Timeid = LuaTimer.Add(0,500,function()
			self.showValue = self.showValue - 1
			if self.showValue <= 0 then
				for i = 1,6 do
					self.ui.comps['lb_shuxing'..i..'add'].Visible = false
				end
				LuaTimer.Delete(self.Timeid)
			end
			return true
		end)
	end
	--todo 仙侣技能先暂时写死1级 by老王 2018/12/27/14:48
	local RankLv = 1--GetRank(self,nextlv or self.curRoleData.Lv)
	for i = 1,3 do
		setSkillByPos(self,i,RankLv)
	end
	ShowRole(self,nextlv)


	local function ExpAnim(self,_advanceData,currentNum,targetNum)

	--关闭ui时统一释放(特效id不太好取)
	Init3DModel(self,self.comps.gg_exp,Vector3(122,7.5,0),Vector3.one,self.ui.menu.MenuOrder,'/res/effect/ui/ef_ui_interface_progressBar.assetbundles')
	
	if self.timer then
    	LuaTimer.Delete(self.timer)
   	    self.comps.gg_exp.Value = self.next_ggvalue
	end

	local interval = 35
	local LifeMS = 1000

	local speed = (1/ LifeMS) * interval
	if currentNum ~= 1 then
		self.next_ggvalue=currentNum
	else
		self.next_ggvalue=0
	end
	
	self.timer=LuaTimer.Add(
		0,
		interval,	
		function()
			local next = self.comps.gg_exp.Value + speed
         	next = math.min(1, next)
            if next == 1 and targetNum then
                next = 0
                currentNum = targetNum
                targetNum = nil
                self.next_ggvalue = currentNum
            end
            
            self.comps.gg_exp.Value = next
            local ret = next < currentNum
            return ret
        end
		)
	end
	

	local  function UpGrade(lv)

	Init3DModel(self,self.useTrainnode,Vector3(self.useTrainnode.Size2D.x/2,self.useTrainnode.Size2D.y/2,0),Vector3.one,self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_interface_consume.assetbundles')

		self.OldRoleData = Helper.copy_table(self.curRoleData)
		self.showValue = 4
		SetCurRoleAttr(self,lv)
		self.curRoleData.Lv = lv
		local _oldadvanceData = PartnerUtil.GetAdvanceData(self.OldRoleData.god_id,self.OldRoleData.Lv)
		local _advanceData = PartnerUtil.GetAdvanceData(self.curRoleData.god_id,lv)

		if _advanceData ~= nil then
			if _oldadvanceData.client_lv_max ~= _advanceData.client_lv_max or
			  _oldadvanceData.client_lv > _advanceData.client_lv or
			   _oldadvanceData.client_rank ~= _advanceData.client_rank then
				ExpAnim(self,_advanceData,1,_advanceData.client_lv/_advanceData.client_lv_max)
			else
				ExpAnim(self,_advanceData,_advanceData.client_lv/_advanceData.client_lv_max)
			end
			
		end

		SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)
		local oldrank = GetRank(self,self.OldRoleData.Lv)
		local newrank = GetRank(self,lv)
   		if oldrank ~= newrank then

   			self.subui = GlobalHooks.UI.CreateUI('PartnerRankUp',0,{PartnerData = self.curRoleData,OldPartnerData = self.OldRoleData})

        	SoundManager.Instance:PlaySoundByKey('jinjie',false)
        	self.ui:AddSubUI(self.subui.ui)
   		end

		--培养完就检查一下红点
--[[		for i, v in ipairs(self.activePartner) do
			if self.activePartner[i].show ==1 then
				GlobalHooks.UI.SetRedTips('partner',1)
				break
			end
		end]]
			GlobalHooks.UI.SetRedTips('partner',CheckUnActiveRedPoint(self))
   	end

   	--培养
	local ShowRedPoint=0
	local allShow=0
	local lb_tipuse=self.ui.comps.cvs_use:FindChildByEditName('lb_tip',true)
	if self.curRoleData.ActiveState == ActiveState.HasActived then
		ShowRedPoint=PartnerUtil.CheckRedPoint(self.curRoleData.god_id,self.curRoleData.Lv)
		allShow=ShowRedPoint
		GlobalHooks.UI.ShowRedPoint(lb_tipuse, ShowRedPoint, 'partner_use')
	end
	self.ui.comps.btn_use.TouchClick = function (sender)
	    local _nextlv = nextlv or self.curRoleData.Lv
		PartnerModel.PartnerUpgradeRequest(self.curRoleData.god_id,_nextlv + 1,function(lv)
			self.pan:RefreshShowCell()
   		 	UpGrade(lv)
			--仙侣升级时，刷新model中的仙侣列表
			for i, v in ipairs(PartnerModel.CurHavePartnerList) do
				if PartnerModel.CurHavePartnerList[i].s2c_god_id == self.curRoleData.god_id then
					PartnerModel.CurHavePartnerList[i].s2c_god_lv =lv
				end
			end

			local upshow=PartnerUtil.CheckRedPoint(self.curRoleData.god_id,lv)
			GlobalHooks.UI.ShowRedPoint(lb_tipuse, upshow, 'partner_use')

			--不为空，则赋值
			if self.activePartner[self.listIndex] ~=nil then
				self.activePartner[self.listIndex].show=upshow
				self.activePartner[self.listIndex].lv=lv
			end
   		end)
	end

	--一键培养
	local lb_tipalluse=self.ui.comps.cvs_alluse:FindChildByEditName('lb_tip',true)
	GlobalHooks.UI.ShowRedPoint(lb_tipalluse, allShow, 'partner_alluse')
	self.ui.comps.btn_alluse.TouchClick = function (sender)
		self.OldRoleData = Helper.copy_table(self.curRoleData)
		PartnerModel.PartnerUpgradeRequest(self.curRoleData.god_id,-1,function(lv)
			self.curRoleData.Lv=lv
			--仙侣升级时，刷新model中的仙侣列表
			for i, v in ipairs(PartnerModel.CurHavePartnerList) do
				if PartnerModel.CurHavePartnerList[i].s2c_god_id == self.curRoleData.god_id then
					PartnerModel.CurHavePartnerList[i].s2c_god_lv =lv
				end
			end
			
			GlobalHooks.UI.ShowRedPoint(lb_tipalluse, 0, 'partner_alluse')
			--同上
			if self.activePartner[self.listIndex] ~=nil then
				self.activePartner[self.listIndex].show=0
				self.activePartner[self.listIndex].lv=lv
			end
			
			self.pan:RefreshShowCell()
			UpGrade(lv)
		end)
	end

end
--*************************************************

--左侧列表&激活按钮**********************
local function SelectItemCell(self, index)
	self.listIndex = index
	self.pan:RefreshShowCell()
	
	if self.Timeid ~= nil then
		LuaTimer.Delete(self.Timeid)
		self.Timeid = nil
	end
	self:SetCurRoleData()
end

function _M.GetFightState(self,id)
	for i,v in ipairs(PartnerModel.PartnerList) do
      if v.ID == id then
        return v.state ~= GodStatus._EIdle
      end
   end
   return false
end

function _M.SetCurRoleData(self)
	--ReleaseAll3DEffect(self)不释放特效，统一退出时在释放
	self.curRoleData = self.PartnerList[self.listIndex]
	self.curRoleData.Lv = GetLv(self,self.listIndex)
	self.curRoleData.ActiveState = GetActiveState(self,self.listIndex)
	SetCurRoleAttr(self)
	self.ui.comps.btn_show.TouchClick = function(sender)
		  self.subui = GlobalHooks.UI.CreateUI('PartnerPreview', 0,{PartnerData = self.curRoleData})
		  -- self.OldRoleData = Helper.copy_table(self.curRoleData)
		  -- self.subui = GlobalHooks.UI.CreateUI('PartnerRankUp',0,{PartnerData = self.curRoleData,OldPartnerData = self.OldRoleData})
          self.ui:AddSubUI(self.subui.ui)
	end

	--激活红点
	local lb_tipjihuo=self.ui.comps.cvs_jihuo:FindChildByEditName('lb_tip',true)
	if self.curRoleData.ActiveState == ActiveState.CanActive then
		GlobalHooks.UI.ShowRedPoint(lb_tipjihuo, 1, 'partner_jihuo')
	else
		GlobalHooks.UI.ShowRedPoint(lb_tipjihuo, 0, 'partner_jihuo')
	end
	self.ui.comps.btn_jihuo.TouchClick = function (sender)
		self.curRoleData.ActiveState = GetActiveState(self,self.listIndex)
		if self.curRoleData.ActiveState == ActiveState.CanActive then
			PartnerModel.PartnerActiveRequest(self.curRoleData.god_id,function()
				SelectItemCell(self,self.listIndex)
				--激活仙侣，插入一条新的数据
				table.insert(PartnerModel.CurHavePartnerList,{s2c_god_lv=1,s2c_god_id=self.curRoleData.god_id,s2c_god_status = 0})
				--把激活的仙侣从未激活列表移除
				for i, v in ipairs(PartnerModel.Unactivated) do
					if v.god_id == self.curRoleData.god_id then
						table.remove(PartnerModel.Unactivated,i)
					end
				end
				table.insert(self.activePartner,{id=self.curRoleData.god_id,lv=0,show=0})
				GlobalHooks.UI.ShowRedPoint(lb_tipjihuo, 0, 'partner_jihuo')
				local name =  Util.GetText(self.curRoleData.god_name)
				local message = Util.GetText('god_unlock',name)
				GlobalHooks.UI.OpenUI('AdvancedTips',0, message)
				CheckUnActiveRedPoint(self)
			end)
		else
			GameAlertManager.Instance:ShowNotify(Util.GetText('item_notenough'))
		end
	end
end


local function ShowList(self,node,index)
	local partnerdata = self.PartnerList[index]
	--local detail = ItemModel.GetDetailByTemplateID(partnerdata.god_item)
	local csv_itemIcon = UIUtil.FindChild(node,'ib_icon')
	local lb_jihuo = UIUtil.FindChild(node,'lb_jihuo')
	local lb_fight = UIUtil.FindChild(node,'lb_fight')
	local cvs_partnerinfo=UIUtil.FindChild(node,'cvs_partnerinfo')
	local ib_back=UIUtil.FindChild(node,'ib_back')
	local lb_tip=node:FindChildByEditName('lb_tip',true)
	local ib_onsell=node:FindChildByEditName('ib_onsell',true)

	ib_onsell.Visible = partnerdata.issale == 1

	if self.listIndex==index then 
		cvs_partnerinfo.Visible=true
	else
		cvs_partnerinfo.Visible=false
	end

	lb_fight.Visible = false
	lb_jihuo.Visible = false
	local activeState = GetActiveState(self,index)
	if activeState == ActiveState.CanActive then
		GlobalHooks.UI.ShowRedPoint(lb_tip, 0, 'partnerlist_use')
		lb_jihuo.Visible = true
	elseif activeState == ActiveState.HasActived then
		if self.activePartner[index] ~=nil then
			local  isshow=PartnerUtil.CheckRedPoint(self.activePartner[index].id,self.activePartner[index].lv)
			self.activePartner[index].show=isshow
			GlobalHooks.UI.ShowRedPoint(lb_tip, isshow, 'partnerlist_use')
		end
		if self:GetFightState(partnerdata.god_id) then
			lb_fight.Visible = true
		end
	elseif activeState == ActiveState.NoActive then
		GlobalHooks.UI.ShowRedPoint(lb_tip, 0, 'partnerlist_use')
	end
	csv_itemIcon.Layout =  nil
	csv_itemIcon:RemoveAllChildren(true)
	csv_itemIcon.EnableChildren = true
	local itemShow = ItemShow.Create(partnerdata.icon_id, partnerdata.god_quality, 0)
	itemShow.IsCircleQualtiy = true
	itemShow.Size2D = csv_itemIcon.Size2D
	--itemShow.EnableTouch = false
	--print("self.listIndex",self.listIndex)
	-- itemShow.TouchClick = function( sender )
	-- 	--print("itemShowTouchClick")
	-- 	self.listIndex=index
	-- 	SelectItemCell(self, index)
	-- end
	node.TouchClick = function( sender )
		self.listIndex=index
		SelectItemCell(self, index)
	end
	csv_itemIcon:AddChild(itemShow)

	--激活特效
	self.EffectList = self.EffectList or {}
	if node.UserTag ~= 0 then --如果node存在特效，则释放特效
		RenderSystem.Instance:Unload(node.UserTag)
		--self.EffectList[node.UserTag] = nil
		node.UserTag = 0
	end
	if activeState == ActiveState.CanActive then --如果可激活，把特效id存在对应node的usertag里
		self.EffectList[index]= InitEffect(self,node,Vector3(node.Size2D.x/2,40,0),Vector3(1,1.6,1),self.ui.menu.MenuOrder,'/res/effect/ui/ef_ui_frame_01.assetbundles')
		node.UserTag = self.EffectList[index]
	end

	--如果是已激活或者可激活状态，显示彩色
	if activeState == ActiveState.HasActived or activeState == ActiveState.CanActive then
		csv_itemIcon.IsGray = false
		cvs_partnerinfo.IsGray=false
		ib_back.IsGray=false
	else--如果不可激活，显示灰白
		csv_itemIcon.IsGray = true
		cvs_partnerinfo.IsGray=true
		ib_back.IsGray=true
	end
	itemShow.Position2D = UnityEngine.Vector2(0,0)
	--itemShow.IsSelected = index == self.listIndex

end

local function RefreshList(self)

	UIUtil.ConfigVScrollPanWithFixCoordinate(self.pan,self.tempnode,#self.PartnerList,Vector2(8,5),Vector2(0,5),function(node, index)
		ShowList(self,node,index)
	end)
end

--************************
local function GetList(self)
	local listdata = GlobalHooks.DB.Find('FallenPartnerListData',{})
	
	  	table.sort(listdata, function(partnera,partnerb)
	  		local activeStateA = GetActiveStateById(self,listdata,partnera.god_id)
	  		local activeStateB = GetActiveStateById(self,listdata,partnerb.god_id)
            if activeStateA ~=  activeStateB then
            	if activeStateA == ActiveState.HasActived then
            		return true
            	elseif activeStateA == ActiveState.CanActive then
            		if activeStateB == ActiveState.HasActived then
            			return false
            		else
            			return true
            		end
            	else
            		return false
            	end
            end

            local fightstateA = self:GetFightState(partnera.god_id)
            local fightstateB = self:GetFightState(partnerb.god_id)
            if fightstateA ~= fightstateB then
            	if fightstateA then
            		return true
            	else
            		return false
            	end
            end
            return partnera.god_id < partnerb.god_id
        end)
	return listdata
end


local function ShowAllFallenPartnerAttribute(self,node)

	Protocol.RequestHandler.ClientGetGodListRequest({},function(rsp)
    	self.pd=rsp
		
		--计算所有已激活仙侣技能战力
		local totskillfight=0
		for i = 1, #self.pd.s2c_list do
			local onePartnerSkillFight= PartnerUtil.GetSkillFightByPartnerId(self.pd.s2c_list[i].s2c_god_id,self.pd.s2c_list[i].s2c_god_lv);
			totskillfight=totskillfight+onePartnerSkillFight
		end

		self.cvs_tips.Visible=true
		self.ui.menu:SetUILayer(self.cvs_tips)
		self.cvs_tips.TouchClick=function()
			self.cvs_tips.Visible=false
		end

		self.d={}
		for i=1,#self.pd.s2c_list do
			self.d[i]=PartnerModel.GetFallenPartnetAttr(self.pd.s2c_list[i].s2c_god_id,self.pd.s2c_list[i].s2c_god_lv)
			self.d[i]=PartnerModel.GetXlsFixedAttribute(self.d[i])
		end

		self.result={}
		local lb_val={}
		for i=1,14 do
			self.result[i]=0
			for j =1,#self.d do
				self.result[i]=self.result[i]+self.d[j][i].Value
			end
			lb_val[i]=self.cvs_tips:FindChildByEditName('lb_val'..i,true)
 			lb_val[i].Text=self.result[i]
		end
		
		--属性加技能总战力
		self.lb_fight=node:FindChildByEditName('lb_fight3',true)
		self.lb_fight.Text=PartnerUtil.GetPartnerPowerbyValue(self.result,true)+totskillfight
	end)
end


--通过仙侣id，返回列表中的排序
local function OpenIndexPartner(self,partnerid)
	local allpartner= GetList(self)
	for i = 1, #allpartner do
		if allpartner[i].god_id==partnerid then
			return i
		end
	end
	UnityEngine.Debug.LogError("PartnerId = "..partnerid.." is not exist,Please check if the partnerId is correct")
	return 1
end


function _M.OnEnter(self,...)
	local params={...}
	if params[2] ~=nil then --如果有指定的仙侣编号，打开对应的仙侣
		self.listIndex=OpenIndexPartner(self,params[2])
	else --否则默认打开第一个仙侣
		self.listIndex =1
	end
	
	self.lastAvatar = nil
	
	--可激活仙侣集合
	self.activePartner={}
    PartnerModel.PartnerGetListRequest(function()
    	self.PartnerList = GetList(self)
		for i = 1, #self.PartnerList do
			local isactive,lv=isActive(self,self.PartnerList[i].god_id)
			if isactive then
				table.insert(self.activePartner,{id=self.PartnerList[i].god_id,lv=lv,show=0})
			end
		end
    	self:SetCurRoleData()
    	RefreshList(self)
    end)
end


function _M.OnExit( self )

	--ShowCurSelect(self,1,false)
	-- if showtime ~= nil then
	-- 	LuaTimer.Delete(showtime)
	-- 	showtime = nil
	-- end
	
	if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
	if self.Timeid then
		LuaTimer.Delete(self.Timeid)
		self.Timeid = nil
	end
	if self.subui and self.subui.ui~= nil then
	    self.subui.ui:Close() 
	    self.subui = nil
    end
     Release3DModel(self)
	 ReleaseAll3DEffect(self)
end

function _M.OnInit( self )
	
	self.pan =  self.ui.comps.sp_partnerlist
 	self.tempnode = self.ui.comps.cvs_quality
 	self.tempnode.Visible = false
    self.ui.comps.menu.Enable = false

    self.cvs_partnerinfo = self.ui.comps.cvs_partnerinfo
    self.cvs_partnerinfo.Visible=false

    self.skill = {}
	for i = 1,3 do
		table.insert(self.skill,self.ui.comps["cvs_skill"..i])
	end
	self.comps.gg_exp:SetGaugeMinMax(0, 1)
 	self.useTrainnode = self.ui.comps.cvs_item1
 	self.useTrainnode.Visible = false

 	self.lb_chuzhan=self.ui.comps.lb_chuzhan
 	self.lb_chuzhan.Visible=false

 	self.cvs_tips=self.ui.comps.cvs_tips
 	self.cvs_tips.Visible=false

 	self.btn_attr=self.ui.comps.btn_attr
 	self.btn_attr.TouchClick=function()
 		ShowAllFallenPartnerAttribute(self,self.cvs_tips)
 	end

 -- 	self.selectCanvas = UEImageBox()
	-- self.selectCanvas.Visible = false
	-- self.selectCanvas.Name = "SelectCanvas"
	-- self.selectCanvas.Size2D = self.ui.comps.cvs_skill1.Size2D
	
	-- self.selectCanvas.Enable = false
	-- self.selectCanvas.IsInteractive = false
	-- self.selectCanvas.EnableChildren = false
	-- local selectimg = '#dynamic/TL_tips/output/TL_tips.xml|TL_tips|35'
 --  	UIUtil.SetImage(self.selectCanvas,selectimg)
 --  	self.ui.comps.cvs_partnershow:AddChild(self.selectCanvas)
end


return _M
