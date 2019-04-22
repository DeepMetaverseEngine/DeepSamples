local _M = {}
_M.__index = _M
print('-------------load skillMainUI---------------------')
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local SkillModel = require("Model/SkillModel")
local ItemModule = require 'Model/ItemModel.lua' 


local function SkillUpgradeEffect(self,parent)
	local transSet = TransformSet()
	transSet.Pos = Vector2(parent.Width/2,-parent.Height/2)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_skill_upgrade.assetbundles'
	local id = RenderSystem.Instance:PlayEffect(assetname, transSet)
	return id
end

 
local function showCost(self,data)
	local items = ItemModule.ParseCostAndCostGroup(data)
	self.costLabel.Visible = false
	local csv_itemIcon = self.costCvs
	if items then
		for i,v in ipairs(items) do 
			local item = v.detail 
			self.costCvs.Visible = true 
			UIUtil.SetEnoughItemShowAndLabel(self, csv_itemIcon, self.costLabel, v)
  			self.costCvs.Visible = true
  			self.costLabel.Visible = true
  			break
		end 
	end
end


local function Release3DModel(self)
	if self and self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DSngleModel(self, parentCvs, pos2d, scale, menuOrder,fileName)
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	info.Callback = function (info2)
		-- body
		info2.RootTrans.localRotation = Quaternion.Euler(25, 130, -25)
	end
	self.model = info
end

local function ReloadModel(self,skillId)
	if self == nil then
		return
	end
	Release3DModel(self)

	local openData = SkillModel.GetSkillOpenData(skillId)
	local assetName = openData.skill_show
	-- local fixpos = Vector2(0,0)
	-- local filename = '/res/unit/mount_phoenix.assetbundles'
	local filename = assetName
	
	local fixpos = {x = 1,y =  -82}-- 偏移坐标
	local fixzoom = 93 -- 缩放比例

	local cvsModel = self.cvsModel
	-- local pos2d = self.ui.comps.cvs_anchor.Position2D
	local pos2d = Vector2(cvsModel.Width/2 + fixpos.x,cvsModel.Height + fixpos.y)
    Init3DSngleModel(self, cvsModel,pos2d , fixzoom, self.ui.menu.MenuOrder,filename)
end

local function setBtnStatus(self,status)
	-- body
	self.upBtn.IsGray = not status
	self.upBtn.Enable = status

	self.allUpBtn.IsGray =  not status
	self.allUpBtn.Enable = status
end  

local function showSkillDetial(self,skillLv,skillName)
	-- body
	-- 更新的时候不设置skillName
	if skillName ~= nil then
		self.nameLabel.Text = skillName
	end

	local maxLv = SkillModel.GetSkillLvMax() 
	self.maxLvLabel.Text = maxLv .. Constants.Text.skill_Level
	local data = SkillModel.GetSkillLvMaxData()
	self.condLabel.Visible = false
	self.ui.comps.lb_tips1.Visible = false
	if skillLv < maxLv then
		--self.condLabel.Text = Constants.Text.skill_Player .. data.player_lvmin .. Constants.Text.skill_Level
	else 
		local nextData = SkillModel.GetSkillLvMaxDataByLvMin(data.player_lvmax + 1)
		if nextData ~= nil then
			self.condLabel.Visible = true
			self.ui.comps.lb_tips1.Visible = true
			self.condLabel.Text = Constants.Text.skill_Player .. nextData.player_lvmin .. Constants.Text.skill_Level
		else
			--TODO显示满级或者其他
		end
	end


	self.LvLabel.Text = skillLv --skillLv .. Constants.Text.skill_Level


	local skillId = self.curSkillId
	
	if self.skillNetData[skillId] then
		setBtnStatus(self,true)
	else
		setBtnStatus(self,false)
	end

	local detial = SkillModel.GetSkillData(skillId,skillLv)
	if detial == nil then
		return
	end

	-- self.fightLabel.Text = 'z' .. detial.power
	self.fightLabel.Text = detial.power
	self.descLabel.XmlText = Util.GetText(detial.skill_desc)
 
	 
	showCost(self,detial)
end 


local function showLvUpEffect(self,parent)
	self.SkillEffectMap = self.SkillEffectMap or {}
	local transSet = TransformSet()
	transSet.Pos = Vector3(parent.Width/2,-parent.Height/2,-100)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local menuOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_skill_upgrade.assetbundles'
	local id = RenderSystem.Instance:PlayEffect(assetname, transSet)
	table.insert(self.SkillEffectMap,id)
end


local function showLvUp(self,skillMap)
	-- body
	for i,v in pairs(skillMap) do
		-- print(i,v)
		local skillId = i
		local skillLv = v
	 	
		local lastLv = self.skillNetData[skillId]

		local index = SkillModel.GetSkillPos(skillId) + 1
		if lastLv == nil or lastLv < skillLv then 
			
			local lvLabel = self.ui.comps['lb_lv'.. index] 

			lvLabel.Text = Util.GetText('common_level2',skillLv) 
			
			local skillCvs = self.ui.comps['cvs_skill'.. index] 
			showLvUpEffect(self,skillCvs)
		end

		self.skillNetData[skillId] = v
		-- skillLv .. Constants.Text.skill_Level 
		
		showLvUpEffect(self,self.ui.comps.cvs_skill)
 
		local redPoint = self.ui.comps['lb_up'.. index] 
  		local showRed = SkillModel.GetRedPoint(skillId)
		redPoint.Visible = showRed

		if skillId == self.curSkillId then
			self.lb_red_up.Visible = showRed
			showSkillDetial(self,skillLv) 
		end
	end
end

local function showSelected( self,skillId,skillName,skillLv,selectedImg,skillIcon,... )
	if self.curImg then
		self.curImg.Visible = false
	end

	self.curSkillId = skillId
	self.curImg = selectedImg
	self.curImg.Visible = true

	ReloadModel(self,self.curSkillId)

	UIUtil.SetImage(self.skilIcon,skillIcon)
	

	showSkillDetial(self,skillLv,skillName)

	local showRed = SkillModel.GetRedPoint(skillId)
	self.lb_red_up.Visible = showRed
end 

function _M.OnEnter( self, ...)
	print('SkillMain OnEnter ing ...')
 	
  	-- local lvMax = SkillModel.GetSkillLvMax()
 
  	-- self.skillListCvs = self.ui.comps.cvs_skilllist
  	self.SkillEffectMap = {}
  	
  	local skillDatas = SkillModel.GetUserSkills()
	
	SkillModel.ReqSkillList(function(resp)
 
		self.skillNetData =  resp.skillMap
 
		for i,v in pairs(skillDatas) do
			local index = v.icon_order + 1

			local skillCvs = self.ui.comps['cvs_skill' .. index]
			local icomImg = self.ui.comps['ib_icon'.. index]
			local selectedImg = self.ui.comps['ib_select'.. index] 
			local lvLabel = self.ui.comps['lb_lv'.. index] 

			local skillIcon = v.skill_icon
			UIUtil.SetImage(icomImg,skillIcon)

			local skillId = v.skill_id
			local skillName = Util.GetText(v.skill_name)
			
			local skillLv = self.skillNetData[v.skill_id]
			if skillLv == nill or skillLv == 0 then
				lvLabel.Text = v.open_lv .. Constants.Text.skill_Level .. Constants.Text.skill_Open
				skillCvs.IsGray = true
			else
				skillCvs.IsGray = false
				lvLabel.Text = Util.GetText('common_level2',skillLv) --skillLv .. Constants.Text.skill_Level
			end

			if i == 1 then
			 
				if skillLv == nil or skillLv == 0 then
					skillLv = 1
				end
				showSelected(self,skillId,skillName,skillLv,selectedImg,skillIcon)

			end

			local redPoint = self.ui.comps['lb_up'.. index] 
			local showRed = SkillModel.GetRedPoint(skillId)
			redPoint.Visible = showRed
			
			skillCvs.TouchClick = function (sender)
		 		print('skillId:',skillId,'self.curSkillId:',self.curSkillId)

				if skillId == self.curSkillId then
					return
				end
 				
 				local skillLv = self.skillNetData[skillId]
				if skillLv == nil or skillLv == 0 then
					skillLv = 1
				end

				showSelected(self,skillId,skillName,skillLv,selectedImg,skillIcon)

			end
		end
    end,true)

end

function _M.OnExit( self )
	-- testdetail:Close()
	print('SkillMain OnExit .. ')
	if self.curImg then
		self.curImg.Visible = false
	end

	Release3DModel(self)

	for k,v in pairs(self.SkillEffectMap) do
		if v ~= nil then
			RenderSystem.Instance:Unload(v)
		end
	end
	
	self.SkillEffectMap = {}
	 
end

function _M.OnDestory( self )
	
	print('SkillMain OnDestory .. ')

end

function _M.OnInit( self )
	print('SkillMain OnInit .. ')

	-- self.SkillInfoCvs = self.ui.comps.cvs_skillinfo
 	
 	self.lb_red_up =  self.ui.comps.lb_red_up

 	self.nameLabel = self.ui.comps.lb_name
 	self.LvLabel = self.ui.comps.lb_lv
 	self.fightLabel = self.ui.comps.lb_fight

 	self.skilIcon = UIUtil.FindChild(self.ui.comps.cvs_skill,'ib_icon')  


 	self.descLabel = self.ui.comps.tb_skilldes
 	self.condLabel = self.ui.comps.lb_condition  
 	self.maxLvLabel = self.ui.comps.lb_maxlv


 	self.costCvs = self.ui.comps.cvs_item1
	self.costItemCvs = self.ui.comps.ib_costicon
	self.costLabel = self.ui.comps.lb_costnum
	self.cvsModel = self.ui.comps.cvs_model

	local pro_icon = self.ui.comps.ib_job
 	UIUtil.SetImage(pro_icon, '$dynamic/TL_login/output/TL_login.xml|TL_login|prol_'.. SkillModel.GetPro())
   
	self.upBtn = self.ui.comps.btn_up
	self.upBtn.TouchClick = function (sender)
		print('self.upBtn.TouchClick requestUpgradeSkill:',self.curSkillId)	 
		SkillModel.ReqUpgradeSkill(self.curSkillId,0,function(resp)
			-- body
			showLvUp(self,SkillModel.skillMap)
			SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)

		end)
	end

	self.allUpBtn = self.ui.comps.btn_allup
	self.allUpBtn.TouchClick = function (sender)
		print('self.allUpBtn.TouchClick ')	

		SkillModel.ReqUpgradeSkill(0,1,function(resp)
			-- body
			showLvUp(self,SkillModel.skillMap)
			SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)

			GlobalHooks.UI.SetRedTips('skill', 0)
		end)
	end

end

return _M