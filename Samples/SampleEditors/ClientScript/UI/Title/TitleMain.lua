local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local TitleModel = require 'Model/TitleModel'
local ItemModel = require 'Model/ItemModel'
local ServerTime = require('Logic/ServerTime')

local takeOn = Constants.Title.takeOn --'装备'
local takeOff = Constants.Title.takeOff --'卸下'
local switch = Constants.Title.switch --'切换'
local timeOver = Constants.Title.timeOver --'已过有效期'
local timeForever = Constants.Title.timeForever --'永久'
local timeHour = Constants.Title.timeHour --'小时'
local already = Constants.Title.already -- '已获得该称号'
local noEquipText = Constants.Title.noEquipText -- "尚未装备称号"


_M.btnType = {
	takeOn = 1,
	takeOff = 2,
	switch = 3,
}

local ownerColor = 0x87755f

local titleScale = 1

local LockImg = 'LockImg'
local function AddLockmageByElement(self,element)
	if not element or not element.button then
		return
	end

	local img = UIUtil.FindChildByUserTag(element.button,120) 
	if img then
		return
	end
 	
 	img = HZImageBox.CreateImageBox()
 	img.UserTag = 120
	img.Name = LockImg
	UIUtil.SetImage(img, Constants.InternalImg.title_lock,true)
	img.Position2D = Vector2(18,13)
	element.button:AddChild(img)
end

local function AddLockImageByTitle(self,titleId)
	-- local element = self.treeItems[titleId]
	-- if element then
	-- 	AddLockmageByElement(self,element)
	-- end
end

local EupipImg = 'EupipImg'
local function AddEquipImageByElement(self,element)
	if not element or not element.button then
		return
	end

	local img = UIUtil.FindChildByUserTag(element.button,110) 
	if img then
		return
	end

	img = HZImageBox.CreateImageBox()
 	img.UserTag = 110
	img.Name = EupipImg
	UIUtil.SetImage(img, Constants.InternalImg.title_equip,true)
	img.Position2D = Vector2(element.button.Width-img.Width-9,1)
	element.button:AddChild(img)
end

local function AddEquipImageByTitle(self,titleId)
	local element = self.treeItems[titleId]
	if element then
		AddEquipImageByElement(self,element)
	end
end

local function RemoveEquipImageByElement(self,element)
	if element and element.button then
		element.button:RemoveChildByName(EupipImg,true)
	end
end

local function RemoveEquipImageByTitle(self,titleId)
	local element = self.treeItems[titleId]
	if element then
		RemoveEquipImageByElement(self,element)
	end
end 

local function ReleaseAll3DModel(self)
	if self.playerModel then
		UI3DModelAdapter.ReleaseModel(self.playerModel.Key)
		self.playerModel = nil
	end
	if self.check_model then
		UI3DModelAdapter.ReleaseModel(self.check_model.Key)
		self.check_model = nil
	end
 	if self.equip_model then
		UI3DModelAdapter.ReleaseModel(self.equip_model.Key)
		self.equip_model = nil
	end

	for k, v in pairs(self.TitleModel or  {}) do
		if v then
			UI3DModelAdapter.ReleaseModel(v.Key)
		end
	end
	self.TitleModel = {}
end

local function EquipTitleRed(self,Visible)
	-- body
end

local function AttrTitleRed(self,Visible)
	-- body
end


local function Init3DModel(self, parentCvs, pos2d, scale, menuOrder, avatar, filter)
	local info = UI3DModelAdapter.AddAvatar(parentCvs, pos2d, scale, menuOrder, avatar, filter)
	self.playerModel = info
	local trans = info.RootTrans
	UI3DModelAdapter.SetLoadCallback(info.Key,function(UIModelInfo)
		UIModelInfo.DynamicBoneEnable = true
	end)
	-- parentCvs.event_PointerMove = function(sender, data)
	-- 	local delta = -data.delta.x
	-- 	trans:Rotate(Vector3.up, delta * 1.2)
	-- end
end
 
local function HideSwitchuiUI(self)
	self.comps.cvs_switchui.Visible = false
	self.comps.cvs_mainmodel.Visible = true
	for k, v in pairs(self.TitleModel or  {}) do
		if v then
			UI3DModelAdapter.ReleaseModel(v.Key)
		end
	end
	self.TitleModel = {}
	self.comps.lb_equiptitletext.Visible = true
end

local function ShowNoEquipTitle(self)
	-- self.comps.cvs_checktitle.Visible = false
 	self.comps.lb_checktime.Visible = false
 	self.comps.lb_checktimenum.Visible = false
 	self.comps.lb_get.Visible = false
 	self.comps.tb_battledesc.Visible = false
 	self.comps.lb_noequip.Visible = true
end

local function ShowNoEquipAttr(self)
	self.comps.cvs_equipInfo.Visible = false	
	self.comps.lb_noequipattr.Visible = true
end

local function ShowEquipButton(self)
	---按钮都改为切换状态
	self.comps.tbt_equip1.Visible = true
	self.comps.tbt_equip1.Text = switch
	self.comps.tbt_equip1.UserTag = _M.btnType.switch

	self.comps.tbt_equip2.Visible = true
	self.comps.tbt_equip2.Text = switch
	self.comps.tbt_equip2.UserTag = _M.btnType.switch
end


local function ShowTitle(self, titlenode, titleId,detail)
	if titleId == -1 then
		titlenode.Visible = true
		titlenode.Layout = null
		titlenode.Text = Constants.Text.NoEquipTitle --"尚未装备称号"
	elseif titleId > 0 then
		titlenode.Visible = true
		titlenode.Text = ""
		titlenode.Layout = null
        detail =  detail or unpack(GlobalHooks.DB.Find('title', {title_id = titleId}))
        if detail.title_type == 2 then
            local title = UI3DModelAdapter.AddSingleModel(titlenode, Vector2(0,0), 1, self.ui.menu.MenuOrder,detail.effect_res)
            title.Callback = function (info)
                local trans2 = info.RootTrans
                trans2:Rotate(Vector3.up,180)
                trans2.localPosition = Vector3(titlenode.Size2D[1]/2,-titlenode.Size2D[2], -500)
            end
            return title
        elseif detail.title_type == 1 then
            titlenode.Text = Util.GetText(detail.word_res)
        elseif detail.title_type == 3 then
        	titlenode.Text = Util.GetText(detail.word_res,DataMgr.Instance.UserData.TitleNameExt)
        	titlenode.Visible = true 
        	if not string.IsNullOrEmpty(detail.pic_res) then
        		print('zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz:',detail.pic_res)
		  		titlenode.Layout = HZUISystem.CreateLayout(detail.pic_res, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 8);
			end
            return title
        end
    else
        titlenode.Visible = false
    end
    return nil
end

--右边选中称号
local function ShowCheckTitle(self,data)
	if self.check_model then
		UI3DModelAdapter.ReleaseModel(self.check_model.Key)
		self.check_model = nil
	end
	
	self.comps.lb_noequip.Visible = false
	self.comps.lb_checktitletext.Visible = false

	if data == nil then
 		ShowNoEquipTitle(self)
		return
	end

	-- self.comps.cvs_checktitle.Visible = true
    

	-- if data.title_type == 1 then
	-- 	self.comps.lb_checktitletext.Visible = true
	-- 	self.comps.lb_checktitletext.Text = Util.GetText(data.word_res)
	-- elseif data.title_type == 2 then 
	-- 	local fileName = data.effect_res
	-- 	local parentCvs = self.ui.comps.cvs_checktitle
	-- 	local pos2d = self.ui.comps.cvs_checkanchor.Position2D
	-- 	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, titleScale, self.ui.menu.MenuOrder,fileName)
	-- 	info.Callback = function (info2)
	-- 		local trans2 = info2.RootTrans
	-- 		trans2:Rotate(Vector3.up,180)
	-- 	end
	-- 	self.check_model = info
	-- end 

	self.check_model = ShowTitle(self,self.comps.lb_checktitletext,data.title_id,data)
end

local function ShowEquipCheckPanel(self,data)
	-- self.comps.cvs_check.Visible = true
	
	local lb_checktime = self.comps.lb_checktime
	local lb_checktimenum = self.comps.lb_checktimenum

	local lb_get = self.comps.lb_get
	local titleGetLabel = self.comps.tb_battledesc

	lb_checktime.Visible = true
	lb_checktimenum.Visible = true

	lb_get.Visible = true
	titleGetLabel.Visible = true
	titleGetLabel.XmlText =  '<f>'.. already ..'</f>'

	local dateTime = self.titleMap[data.title_id]
	
	-- if dateTime:Equals(System.DateTime.MaxValue) then
	if dateTime.Year == System.DateTime.MaxValue.Year then
		lb_checktimenum.Text = timeForever
	else
		local timeSpan =  dateTime:Subtract(ServerTime.getServerTime())
		if timeSpan.TotalSeconds > 0 then
			local hours = math.floor(timeSpan.TotalHours) + 1
			lb_checktimenum.Text = hours .. timeHour
		else
		lb_checktimenum.Text = timeOver
		end
	end
end 

--玩家头上的称号
local function ShowEquipTitle(self,data)
	if self.equip_model then
		UI3DModelAdapter.ReleaseModel(self.equip_model.Key)
		self.equip_model = nil
	end


	self.comps.lb_equiptitletext.Visible = false
	
	if data == nil then
		self.equip_model = ShowTitle(self,self.comps.lb_equiptitletext,-1)
		return
	end


	self.ui.comps.lb_equiptip.Visible = false 

	self.equip_model = ShowTitle(self,self.comps.lb_equiptitletext,data.title_id,data)

	-- if data.title_type == 1 then
	-- 	self.comps.lb_equiptitletext.Visible = true
	-- 	self.comps.lb_equiptitletext.Text = Util.GetText(data.word_res)
	-- elseif data.title_type == 2 then
	-- 	local fileName = data.effect_res
	-- 	local parentCvs = self.ui.comps.cvs_equiptitle
	-- 	local pos2d = self.ui.comps.cvs_equiptitleanchor.Position2D
	-- 	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, titleScale, self.ui.menu.MenuOrder,fileName)
	-- 	info.Callback = function (info2)
	-- 		local trans2 = info2.RootTrans
	-- 		trans2:Rotate(Vector3.up,180)
	-- 	end
	-- 	self.equip_model = info
	-- end
end

 
local function ShowCheckAttr(self,data)
	-- body
	if data == nil then
		ShowNoEquipAttr(self)
		return
	end
		
	self.comps.lb_noequipattr.Visible = false
	self.comps.cvs_equipInfo.Visible = true

	for i = 1, 2 do
		local attributeName = data.loadattribute.key[i]
		local num = data.loadattribute.num[i]
		local nameText = self.ui.comps['lb_equipAttr' ..i]
		local valueText = self.ui.comps['lb_equipNum' ..i]
		if attributeName and num then
			nameText.Visible = true
			valueText.Visible = true
			local attrData = GlobalHooks.DB.FindFirst('Attribute',{key = attributeName})
			if attrData then
				nameText.Text = Util.GetText(attrData.name)
				valueText.Text = num
			else
				nameText.Visible = false
				valueText.Visible = false
			end
		else
			nameText.Visible = false
			valueText.Visible = false
		end
	end
end

--切换称号
local function ShowSwitchTitleUI(self)

	self.comps.cvs_switchui.Visible = true
	self.comps.cvs_switchtitle.Visible = true
	self.comps.cvs_switchattri.Visible = false

	self.comps.cvs_mainmodel.Visible = false

	local pan = self.comps.sp_titlelist

	local tempnode = self.comps.cvs_titleinfo
	tempnode.Visible = false

	local datas = {}
	for titleId,dateTime in pairs(self.titleMap or  {}) do
		if self.equipTitle ~= titleId then
			local data = {}
			data.title_id = titleId
			data.dateTime = dateTime
			table.insert(datas,data)
		end
	end
	
	local function eachupdatecb(node, index)
		local data = datas[index]

		local titleData =   GlobalHooks.DB.FindFirst('title',{title_id = data.title_id})
	

		local titleText = UIUtil.FindChild(node,'lb_titletext',true)
		if self.TitleModel[titleText] then
			UI3DModelAdapter.ReleaseModel(self.TitleModel[titleText].Key)
			self.TitleModel[titleText] = nil
		end
		-- local titleCvs = UIUtil.FindChild(node,'cvs_title',true)
		-- local anchorCvs = UIUtil.FindChild(node,'cvs_anchor',true)

		-- if titleData.title_type == 1 then
		-- 	titleCvs.Visible = false
		-- 	titleText.Visible = true
		-- 	titleText.Text = Util.GetText(titleData.word_res)
		-- elseif titleData.title_type == 2 then
		-- 	titleText.Visible = false
		-- 	titleCvs.Visible = true
		-- 	local fileName = titleData.effect_res
		-- 	local pos2d = anchorCvs.Position2D
		-- 	local info = UI3DModelAdapter.AddSingleModel(titleCvs, pos2d, titleScale, self.ui.menu.MenuOrder,fileName)
		-- 	info.Callback = function (info2)
		-- 		local trans2 = info2.RootTrans
		-- 		trans2:Rotate(Vector3.up,180)
		-- 	end
		-- 	self.TitleModel[data.title_id] = info
		-- end
		
		self.TitleModel[titleText] = ShowTitle(self,titleText,titleData.title_id,titleData)

		local timeLabel = UIUtil.FindChild(node,'lb_timenum')
		local dateTime = self.titleMap[data.title_id]
		-- if dateTime:Equals(System.DateTime.MaxValue)  then
		if dateTime.Year == System.DateTime.MaxValue.Year then
			timeLabel.Text = timeForever
		else
			local timeSpan =  dateTime:Subtract(ServerTime.getServerTime())
			if timeSpan.TotalSeconds > 0 then
				local hours = math.floor(timeSpan.TotalHours) + 1
				timeLabel.Text = hours .. timeHour
			else
				timeLabel.Text = timeOver
			end
		end

		node.TouchClick = function() 
			print('ShowSwitchTitleUI node.TouchClick: ',data.title_id) 
	 		TitleModel.ClientEquiTitleRequest(data.title_id,function (resp)

	 			RemoveEquipImageByTitle(self,self.equipTitle)
	 			AddEquipImageByTitle(self,data.title_id)
	 			
	 			DataMgr.Instance.UserData:SetTitleID(data.title_id)

				self.equipTitle = data.title_id
				 
				ShowCheckTitle(self,titleData)	

				ShowEquipCheckPanel(self,titleData)

				ShowEquipTitle(self,titleData)
				
				HideSwitchuiUI(self)	

				self.ui.comps.lb_equiptip.Visible = false 
			end)
		end
	end

	UIUtil.ConfigVScrollPan(pan,tempnode,#datas,eachupdatecb)

	self.comps.lb_noswitch.Visible = #datas == 0
end

--切换称号属性
local function ShowSwitchAttrUI(self)
	self.comps.cvs_switchui.Visible = true
	self.comps.cvs_switchtitle.Visible = false
	self.comps.cvs_switchattri.Visible = true

	self.comps.cvs_mainmodel.Visible = false

	local pan = self.comps.sp_attrlist
	local tempnode = self.comps.cvs_titleattr
	tempnode.Visible = false
	local datas = {}
	for titleId,dateTime in pairs(self.titleMap or  {}) do
		if self.equipAttr ~= titleId then
			local data = {}
			data.title_id = titleId
			data.dateTime = dateTime
			table.insert(datas,data)
		end
	end

	local function eachupdatecb(node, index)
		local data = datas[index]
 
		local titleData =   GlobalHooks.DB.FindFirst('title',{title_id = data.title_id})

		local timeLabel = UIUtil.FindChild(node,'lb_timenum')
		local dateTime = self.titleMap[data.title_id]
		-- if dateTime:Equals(System.DateTime.MaxValue)  then
		if dateTime.Year == System.DateTime.MaxValue.Year then
			timeLabel.Text = timeForever
		else
			local timeSpan =  dateTime:Subtract(ServerTime.getServerTime())
			if timeSpan.TotalSeconds > 0 then
				local hours = math.floor(timeSpan.TotalHours) + 1
				timeLabel.Text = hours .. timeHour
			else
				timeLabel.Text = timeOver
			end
		end

		local ret = 0
		for i = 1, 2 do
			local attributeName = titleData.loadattribute.key[i]
			local num = titleData.loadattribute.num[i]
			local nameText = UIUtil.FindChild(node,'lb_attribute' .. i,true)
			local valueText =  UIUtil.FindChild(node,'lb_num' .. i,true)
			if attributeName and num then
				nameText.Visible = true
				valueText.Visible = true
				local attrData = GlobalHooks.DB.FindFirst('Attribute',{key = attributeName})
		 
        		if attrData   then
        			nameText.Text = Util.GetText(attrData.name)
					valueText.Text = num
            		ret = ret + num * (attrData.fight / 10000)
        		end

			else
				nameText.Visible = false
				valueText.Visible = false
			end
 
		end

		local powerLabel = UIUtil.FindChild(node,'lb_powernum',true)
		powerLabel.Text = math.floor(ret)

		node.TouchClick = function()
			print('ShowSwitchAttrUI node.TouchClick: ',data.title_id) 
			TitleModel.ClientEquiTitleAttrRequest(data.title_id,function (resp)
				self.equipAttr = data.title_id
				ShowCheckAttr(self,titleData)
				HideSwitchuiUI(self)	 
				self.ui.comps.lb_switchtip.Visible = false
			end)
		end
	end

	UIUtil.ConfigVScrollPan(pan,tempnode,#datas,eachupdatecb)
	self.comps.lb_noattri.Visible =  #datas == 0
end


--显示选中面板
local function ShowCheckPanel(self,data)

	ShowCheckTitle(self,data)
	
	-- self.comps.cvs_check.Visible = true
	
	local lb_checktime = self.comps.lb_checktime
	local lb_checktimenum = self.comps.lb_checktimenum

	local lb_get = self.comps.lb_get
	local titleGetLabel = self.comps.tb_battledesc

	lb_checktime.Visible = true
	lb_checktimenum.Visible = true

	if self.titleMap == nil or self.titleMap[data.title_id] == nil then
		 
		
		lb_get.Visible = true
		titleGetLabel.Visible = true
		titleGetLabel.XmlText =  '<f>'..Util.GetText(data.text_desc)..'</f>'


		if data.last_time > 0 then
 			lb_checktimenum.Text = data.last_time .. timeHour
		else
			lb_checktimenum.Text = timeForever
		end 
		
		self.comps.tbt_equip1.Visible = false
		self.comps.tbt_equip2.Visible = false
	else
 


		lb_get.Visible = true
		titleGetLabel.Visible = true
		titleGetLabel.XmlText =  '<f>'.. already ..'</f>'

		self.comps.tbt_equip1.Visible = true
		self.comps.tbt_equip2.Visible = true
		
		-- self.comps.cvs_checktitle.Visible = true

		if data.title_id == self.equipTitle then
			self.comps.tbt_equip1.Text = takeOff
			self.comps.tbt_equip1.UserTag = _M.btnType.takeOff
		else
			self.comps.tbt_equip1.Text = takeOn
			self.comps.tbt_equip1.UserTag = _M.btnType.takeOn
		end

		if data.title_id == self.equipAttr then
			self.comps.tbt_equip2.Text = takeOff
			self.comps.tbt_equip2.UserTag = _M.btnType.takeOff
		else
			self.comps.tbt_equip2.Text = takeOn
			self.comps.tbt_equip2.UserTag = _M.btnType.takeOn
		end
			 
		local dateTime = self.titleMap[data.title_id]
		-- if dateTime:Equals(System.DateTime.MaxValue)  then
		if dateTime.Year == System.DateTime.MaxValue.Year then
			lb_checktimenum.Text = timeForever
		else
			local timeSpan =  dateTime:Subtract(ServerTime.getServerTime())
			if timeSpan.TotalSeconds > 0 then
				local hours = math.floor(timeSpan.TotalHours) + 1
				lb_checktimenum.Text = hours .. timeHour
			else
				lb_checktimenum.Text = timeOver
			end
		end
	end

	--装备属性框
	ShowCheckAttr(self,data)

		--收集属性框
	self.comps.cvs_collectattribute.Visible = true
	--按钮隐藏
	self.comps.btn_alluse.Visible = false


	self.comps.cvs_collectInfo.Visible = false
	local hascollectattr = false
	for i = 1, 2 do
		local attributeName = data.collectattribute.key[i]
		local num = data.collectattribute.num[i]
		local nameText = self.ui.comps['lb_collectAttr' ..i]
		local valueText = self.ui.comps['lb_collectNum' ..i]
		if not string.IsNullOrEmpty(attributeName)  and num > 0 then
			nameText.Visible = true
			valueText.Visible = true
			local attrData = GlobalHooks.DB.FindFirst('Attribute',{key = attributeName})
			nameText.Text = Util.GetText(attrData.name)
			valueText.Text = num
			self.comps.cvs_collectInfo.Visible = true
			hascollectattr = true
		else
			nameText.Visible = false
			valueText.Visible = false
			
		end
	end

	self.comps.lb_nocollectattr.Visible = not hascollectattr
end  


--显示已装备称号界面
local function ShowEquipPanel(self)
	self.comps.tbt_current.Enable = false
	self.target = nil
	
	self.tree_menu:SetChildrenEnable(false)
   
	if self.check_model then
		UI3DModelAdapter.ReleaseModel(self.check_model.Key)
		self.check_model = nil
	end
 
	ShowEquipButton(self)
	
	self.comps.cvs_collectattribute.Visible = false
	self.comps.btn_alluse.Visible = true

	--显示时间 有限期
	local lb_checktime = self.comps.lb_checktime
	lb_checktime.Visible = false
	local lb_checktimenum = self.comps.lb_checktimenum
	lb_checktimenum.Visible = false

	local lb_get = self.comps.lb_get
	lb_get.Visible = false
	local titleGetLabel = self.comps.tb_battledesc
	titleGetLabel.Visible = false
	
	if self.equipTitle and  self.equipTitle > 0 then
		self.comps.cvs_check.Visible = true
		local titleData = GlobalHooks.DB.FindFirst('title',{title_id = self.equipTitle})
		ShowCheckTitle(self,titleData)

		ShowEquipTitle(self,titleData)

		lb_checktime.Visible = true
		lb_checktimenum.Visible = true
		local dateTime = self.titleMap[self.equipTitle]
		-- if dateTime:Equals(System.DateTime.MaxValue)  then
		if dateTime.Year == System.DateTime.MaxValue.Year then
			lb_checktimenum.Text = timeForever
		else
			local timeSpan =  dateTime:Subtract(ServerTime.getServerTime())
			if timeSpan.TotalSeconds > 0 then
				local hours = math.floor(timeSpan.TotalHours) + 1
				lb_checktimenum.Text = hours .. timeHour
			else
				lb_checktimenum.Text = timeOver
			end
		end
		 
		lb_get.Visible = true
		titleGetLabel.Visible = true
		titleGetLabel.XmlText =  '<f>'.. already ..'</f>'

	else
		ShowCheckTitle(self,nil)

		ShowEquipTitle(self,nil)

		if self.titleMap and next(self.titleMap) then
			self.ui.comps.lb_equiptip.Visible = true
		else
			self.ui.comps.lb_equiptip.Visible = false
		end
	end


	if self.equipAttr and  self.equipAttr > 0 then
		self.comps.cvs_check.Visible = true
		 
		local titleData = GlobalHooks.DB.FindFirst('title',{title_id = self.equipAttr})
		for i = 1, 2 do
			local attributeName = titleData.loadattribute.key[i]
			local num = titleData.loadattribute.num[i]
			local nameText = self.ui.comps['lb_equipAttr' ..i]
			local valueText = self.ui.comps['lb_equipNum' ..i]
			if attributeName and num then
				nameText.Visible = true
				valueText.Visible = true
				local attrData = GlobalHooks.DB.FindFirst('Attribute',{key = attributeName})
				if attrData then
					nameText.Text = Util.GetText(attrData.name)
				else
					nameText.Text = ""
				end
				valueText.Text = num
			else
				nameText.Visible = false
				valueText.Visible = false
			end
		end
	else
		ShowCheckAttr(self,nil)
		if self.titleMap and next(self.titleMap) then 
			self.ui.comps.lb_switchtip.Visible = true
		else
			self.ui.comps.lb_switchtip.Visible = false
		end
	end
end


local function OnSelectTarget(self, target)

	self.comps.tbt_current.Enable = true
	self.comps.tbt_current.IsChecked = false
	
    self.target = target

	ShowCheckPanel(self,self.target)
	
 	ShowEquipTitle(self,self.target)
 	-- print_r('target:',target)
end

local function SelectTitle(self,titleId)
	local element = self.tree_menu:FindChildByUserTag(titleId)
	if element then
		element:SetEnableAndInvoke(true)
	end
end

 
local function ShowAllCollectattribute(self)
	self.comps.cvs_allcollectattribute.Visible = true
	
	local attrs = {}
	for titleId,dateTime in pairs(self.titleMap or {}) do
		-- if dateTime:Equals(System.DateTime.MaxValue)  then
		if dateTime.Year == System.DateTime.MaxValue.Year then
			local titleData = GlobalHooks.DB.FindFirst('title',{title_id = titleId})
			for index,v in pairs(titleData.collectattribute.key or {}) do
				attrs[v] = attrs[v] or 0
				local value = titleData.collectattribute.num[index]
				attrs[v] = attrs[v] + value
			end
		end
	end
	
	for attrName,value in pairs(attrs) do
		-- print('attrs:',attrName,v)
		local  attrLabel = self.comps['lb_'.. attrName]
		if attrLabel then
			attrLabel.Text = value
		else
			print('策划填错了，UI里面没有对应的控件 lb_' .. attrName)	
			print_r('attr ui error:',attrName,value)	
		end
	end
end



function _M.OnEnter(self, titleId)

	-- self.ui.comps.lb_equiptip.Visible = false  
	-- self.ui.comps.lb_switchtip.Visible = false

	self.comps.tbt_current.Enable = true
	self.comps.tbt_current.IsChecked = false
	self.comps.cvs_switchui.Visible = false
	-- self.comps.cvs_check.Visible = false
	self.comps.lb_palyername.Text = DataMgr.Instance.UserData.Name

	local filter = bit.lshift(1,  GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))
	Init3DModel(self, self.comps.cvs_mainmodel, self.comps.cvs_mainanchor.Position2D, 150, self.ui.menu.MenuOrder, DataMgr.Instance.UserData:GetAvatarList(), filter)


	local titleMenu = GlobalHooks.DB.GetFullTable('title_menu')

	self.treeItems  = {}
	self.subTreeOpen = {}
	TitleModel.ClientGetTitleInfoRequest(function(resp)
		-- body
		self.equipTitle = resp.s2c_equipTitle
		self.equipAttr = resp.s2c_equipAttr
		self.titleMap = resp.s2c_titleMap or {}


		local tree = UIUtil.CreateTreeMenu()
		local map_tree = {}

		for _, v in ipairs(titleMenu) do
			local menuID = v.id
			if menuID  ~= 0 and not string.IsNullOrEmpty(v.menu_name) then
				has_element = true
				map_tree[menuID] = tree:AddChild(Util.GetText(v.menu_name),function (IsClose,treeSub)
					-- print('111111111111111111111111111111111:',isOpen,item.GetUserTag(item))

					-- self.ui.comps.lb_equiptip.Visible = false  
					-- self.ui.comps.lb_switchtip.Visible = false

					local treeTag = treeSub:GetUserTag()
					--if IsClose or self.subTreeOpen[treeTag] then
					--	return
					--end	
					self.subTreeOpen[treeTag] = true
					
					local subTitles = GlobalHooks.DB.Find('title',{menu_id = treeTag,is_show=1})
					for _, title in ipairs(subTitles or {}) do
						local element = self.treeItems[title.title_id]
					 	if not self.titleMap[title.title_id] then
					 		if element and element.button then
					 			local img = UIUtil.GetLayout('#dynamic/TL_title/output/TL_title.xml|TL_title|13')
					 			element.button.FontColor = GameUtil.RGB2Color(ownerColor)
					 			element.button:SetLayout(img,img)
					 		end
					 		AddLockImageByTitle(self,title.title_id)
					 	else
							if self.equipTitle == title.title_id then
								AddEquipImageByTitle(self,title.title_id)
							end
					 		
					 		if element and element.button then
					 			local img = UIUtil.GetLayout('#dynamic/TL_title/output/TL_title.xml|TL_title|14')
					 			element.button.FontColor = GameUtil.RGB2Color(ownerColor)
					 			element.button:SetLayout(img,img)
					 		end
					 	end
						
					end 
				end)

				local sub = map_tree[menuID]
				sub:SetUserTag(menuID)
				local titles = GlobalHooks.DB.Find('title',{menu_id = menuID})
				for _, title in ipairs(titles) do
					local titleName = title.title_name
					if title.title_type == 3 then
						if self.titleMap[title.title_id] then
							local element = sub:AddChild(Util.GetText(titleName,DataMgr.Instance.UserData.TitleNameExt),function()
								self.ui.comps.lb_equiptip.Visible = false  
								self.ui.comps.lb_switchtip.Visible = false

								OnSelectTarget(self, title)
							end)
							element:SetUserTag(title.title_id)
							self.treeItems[title.title_id] = element
						end
					elseif not string.IsNullOrEmpty(titleName) then
						local element = sub:AddChild(Util.GetText(titleName),function()

							self.ui.comps.lb_equiptip.Visible = false  
							self.ui.comps.lb_switchtip.Visible = false

							OnSelectTarget(self, title)

						end)
						element:SetUserTag(title.title_id)
						self.treeItems[title.title_id] = element
						
					end
				end
			end
		end

		tree:Show(self.comps.tbt_sub, self.comps.tbt_sub1, 0, 0, 0, 5)
		self.tree_menu = tree

		if titleId then
			SelectTitle(self,titleId)
			
			local element = self.treeItems[titleId]
			if element and element.button then
				self.comps.sp_list.Scrollable:LookAt(element.button.Position2D)
			end
		else 
			self.comps.tbt_current.IsChecked = true
			ShowEquipPanel(self)
		end
		
		UnityHelper.WaitForEndOfFrame(function()
			for titleId,element in pairs(self.treeItems) do
				if not self.titleMap[titleId] then
					AddLockImageByTitle(self,titleId)
					if element and element.button then
						local img = UIUtil.GetLayout('#dynamic/TL_title/output/TL_title.xml|TL_title|13')
					 	element.button.FontColor = GameUtil.RGB2Color(ownerColor)
					 	element.button:SetLayout(img,img)
					end
				else
					if self.equipTitle == titleId then
						AddEquipImageByTitle(self,titleId)
					end
					local element = self.treeItems[titleId]
					if element and element.button then
						local img = UIUtil.GetLayout('#dynamic/TL_title/output/TL_title.xml|TL_title|14')
						element.button.FontColor = GameUtil.RGB2Color(ownerColor)
						element.button:SetLayout(img,img)
					end
				end
			end
		end)
	end)

end
 
function _M.OnExit(self)
	self.tree_menu:Close()

	ReleaseAll3DModel(self)
end

function _M.OnDestory(self)


end


function _M.OnInit(self)

	self.TitleModel = {}

	self.comps.tbt_current.TouchClick = function ( ... )
		-- body
		ShowEquipPanel(self)
		
	end
 
	--装备称号外观
	self.comps.tbt_equip1.TouchClick = function()
		if self.target then
			local UserTag = self.comps.tbt_equip1.UserTag
			if UserTag == _M.btnType.takeOn then
				local  titleId = self.target.title_id
				TitleModel.ClientEquiTitleRequest(titleId,function (resp)

					RemoveEquipImageByTitle(self,self.equipTitle)
					AddEquipImageByTitle(self,titleId)

					DataMgr.Instance.UserData:SetTitleID(titleId)

					self.equipTitle = titleId
					self.comps.tbt_equip1.Text = takeOff
					self.comps.tbt_equip1.UserTag = _M.btnType.takeOff
 
				end)
				
			elseif UserTag == _M.btnType.takeOff then
				TitleModel.ClientEquiTitleRequest(0,function (resp)

					RemoveEquipImageByTitle(self,self.equipTitle)
					DataMgr.Instance.UserData:SetTitleID(0)
					self.equipTitle = 0
					self.comps.tbt_equip1.Text = takeOn
					self.comps.tbt_equip1.UserTag = _M.btnType.takeOn

				end)
			end
			self.comps.lb_equiptitletext.Visible = true
		else 
			--切换UI
			ShowSwitchTitleUI(self)
			self.comps.lb_equiptitletext.Visible = false
		end
	end
 
	--装备称号属性
	self.comps.tbt_equip2.TouchClick = function()

		if self.target then
			local UserTag = self.comps.tbt_equip2.UserTag
			if UserTag == _M.btnType.takeOn then
				local  titleId = self.target.title_id
				TitleModel.ClientEquiTitleAttrRequest(titleId,function (resp)
					self.equipAttr = titleId
					self.comps.tbt_equip2.Text = takeOff
					self.comps.tbt_equip2.UserTag = _M.btnType.takeOff
					
 
				end)
			elseif UserTag == _M.btnType.takeOff then
				--卸下
				TitleModel.ClientEquiTitleAttrRequest(0,function (resp)
					self.equipAttr = 0
					self.comps.tbt_equip2.Text = takeOn
					self.comps.tbt_equip2.UserTag = _M.btnType.takeOn
	 
				end)
			end
			self.comps.lb_equiptitletext.Visible = true
		else
			ShowSwitchAttrUI(self)
			self.comps.lb_equiptitletext.Visible = false
		end
	end

	self.comps.cvs_switchui.TouchClick = function ( ... )
		HideSwitchuiUI(self)
		self.comps.lb_equiptitletext.Visible = true
	end

	self.comps.btn_close2.TouchClick = function()
		HideSwitchuiUI(self)
		self.comps.lb_equiptitletext.Visible = true
	end

	--卸下当前称号
	self.comps.btn_currentTitle.TouchClick = function()
		TitleModel.ClientEquiTitleRequest(0,function (resp)

			RemoveEquipImageByTitle(self,self.equipTitle)
		    DataMgr.Instance.UserData:SetTitleID(0)

			self.equipTitle = 0
		 
			ShowCheckTitle(self,nil) 

			ShowEquipTitle(self,nil)

			HideSwitchuiUI(self)
					--模型上面的外观显示？	

			self.ui.comps.lb_equiptip.Visible = true  
		
		end)
	end

	--卸下当前属性
	self.comps.btn_currentAttr.TouchClick = function()
		TitleModel.ClientEquiTitleAttrRequest(0,function (resp)
			
			self.equipAttr = 0  

			ShowCheckAttr(self,nil)

			HideSwitchuiUI(self)	 

			self.ui.comps.lb_switchtip.Visible = true
				
		end)
	end
	--显示属性界面
	self.comps.btn_alluse.TouchClick = function()
		ShowAllCollectattribute(self)
	end
	--关闭属性界面
	self.comps.cvs_allcollectattribute.TouchClick = function ( ... )
		self.comps.cvs_allcollectattribute.Visible = false
	end

	self.ui.menu:SetUILayer(self.comps.cvs_switchtitle)
	self.ui.menu:SetUILayer(self.comps.cvs_switchattri)
	self.ui.menu:SetUILayer(self.comps.cvs_allcollectattribute)

 
end
 

return _M