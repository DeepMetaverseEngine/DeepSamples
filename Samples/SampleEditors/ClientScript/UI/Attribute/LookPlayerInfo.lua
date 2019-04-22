-- 查看玩家信息
-- 作者：任祥建
-- 日期：2018.7.16

local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local MountModel = require 'Model/MountModel'
local ArtifactModel = require 'Model/ArtifactModel'
local PartnerModel = require'Model/FallenPartnerModel'
local PartnerUtil = require 'UI/Partner/FallenPartnerUtil'
local Helper = require 'Logic/Helper'
local PlayRuleModel = require 'Model/PlayRuleModel'


local function GetSuitDataByID(ID)
	return unpack(GlobalHooks.DB.Find('AvatarShow',{id = ID})).item_id
end

local  function GetMiraclePower(map)
	local all = ArtifactModel.GetAllArtifactAttribute(map)
	local attrs = {}
	for k,v in pairs(all) do
		local name,value = ItemModel.GetAttributeString(v)
		table.insert(attrs,v)
	end
 	local  fight = ItemModel.CalcAttributesScore(attrs)
 	return fight
end

local function FillAttributes(lv)
	local detail = GlobalHooks.DB.FindFirst('wings_prop', lv)
    local info = GlobalHooks.DB.FindFirst('wings', {wings_class = detail.wings_class})
	attr = ItemModel.GetXlsFixedAttribute(detail, true)
    local fight = ItemModel.CalcAttributesScore(attr)
    return fight,info.wing_name,info.checkzoom,info.avatar_res
end

local function Release3DModel(self,modelindex)
	if self.model then
		if self.model[modelindex] then
			UI3DModelAdapter.ReleaseModel(self.model[modelindex].Key)
			self.model[modelindex] = nil
		end
	end
end

local function Init3DSngleModel(self, parentCvs, pos2d, scale, rotate, menuOrder, fileName,modelindex)
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	self.model[modelindex] = info
	local trans = info.RootTrans
	info.Callback = function (info2)
		local trans2 = info2.RootTrans
		trans2:Rotate(rotate)
	end
	
	-- parentCvs.event_PointerMove = function(sender, data)
	-- 	local delta = -data.delta.x
	-- 	trans:Rotate(Vector3.up, delta * 1.2)
	-- end
end

local function InitModelByAvatarRes(self,anchor,avatar_res,modelindex,scale,posoffy,rotate)
	Release3DModel(self,modelindex)
	local filename = '/res/unit/' .. avatar_res .. '.assetbundles'
	local fixposdata = Vector2(0,0)
	if posoffy then
		fixposdata = Vector2(0,posoffy)
	end
	local fixzoom = 50 -- 缩放比例
	if scale then
		fixzoom = tonumber(scale)
	end
    Init3DSngleModel(self, anchor, fixposdata, fixzoom,rotate, self.ui.menu.MenuOrder,filename,modelindex)
end

local function Init3DModel(self, parentCvs, pos2d, scale, menuOrder, avatar, filte,modelindex)
	local info = UI3DModelAdapter.AddAvatar(parentCvs, pos2d, scale, menuOrder, avatar, filte)
	self.model[modelindex] = info
	local trans = info.RootTrans
	UI3DModelAdapter.SetLoadCallback(info.Key,function(UIModelInfo)
		UIModelInfo.DynamicBoneEnable = true
	end)
	parentCvs.event_PointerMove = function(sender, data)
		local delta = -data.delta.x
		trans:Rotate(Vector3.up, delta * 1.2)
	end
end

local function CloseItemDetail( self, key )
	if self.detailMenu then
		if key then
			if self.detailMenu[key] ~= nil then
				if key == 1 then
					self.detailMenu[1].itemShow.IsSelected = false
				end
				self.detailMenu[key]:Close()
				self.detailMenu[key] = nil
			end
		else
			if self.detailMenu[1] ~= nil then
				self.detailMenu[1].itemShow.IsSelected = false
				self.detailMenu[1]:Close()
				self.detailMenu[1] = nil
			end
			if self.detailMenu[2] ~= nil then
				self.detailMenu[2]:Close()
				self.detailMenu[2] = nil
			end
		end
	end
end

--同时显示装备对比的信息
local function ShowItemDetail(self, detail,itemShow, x, y)
	local detail = ItemModel.GetDetailByTemplateID(detail.static_equip.id, detail.dynamicAttrs, detail.refine, detail.gem)
	local detail2 = nil
	if not detail.static_equip then
		CloseItemDetail(self)
		detail2 = nil
	else
		detail2 = ItemModel.GetDetailByEquipBagIndex(detail.static_equip.equip_pos)
		if not detail2 then
			CloseItemDetail(self,2)
		end
	end

	if not self.detailMenu[1] then
		self.detailMenu[1] = GlobalHooks.UI.CreateUI('ItemDetail')
		self:AddSubUI(self.detailMenu[1])
	else
		self.detailMenu[1].itemShow.IsSelected = false
	end
	self.detailMenu[1]:Reset({detail=detail,autoHeight=true,IsEquiped=false})
	self.detailMenu[1]:EnableTouchFrame(false)
	self.detailMenu[1]:EnableTouchFrameClose(false)
	self.detailMenu[1]:SetButtons({})
	self.detailMenu[1]:SetPos(x, y)
	self.detailMenu[1].itemShow = itemShow
    self.detailMenu[1].itemShow.IsSelected = true

	if detail2 then
		if not self.detailMenu[2] then
			self.detailMenu[2] = GlobalHooks.UI.CreateUI('ItemDetail')
			self:AddSubUI(self.detailMenu[2])
		end
	    self.detailMenu[2]:Reset({detail=detail2,autoHeight=true,IsEquiped=true})
		self.detailMenu[2]:EnableTouchFrame(false)
		self.detailMenu[2]:EnableTouchFrameClose(false)
		self.detailMenu[2]:SetButtons({})
		self.detailMenu[2]:SetPos(x+340, y)
	end

end

--设置装备的信息
local function SetEquipInfo(self)
	local temp = self.ui.root:GlobalToLocal(self.cvs_anchor.Parent:LocalToGlobal(),true)
	for k,v in pairs(self.cvs_equip) do
		if self.equips[k] then
			local itemShow = UIUtil.SetItemShowTo(v,self.equips[k].static_equip.id,0)
			v.TouchClick = function(sender)
				ShowItemDetail(self, Helper.copy_table(self.equips[k]), itemShow, temp[1], temp[2])
			end
		else
			v.TouchClick = function(sender)
				CloseItemDetail(self)
			end
		end
	end
end

--设置模型
local function SetModelInfo(self)
	self.lb_name.Text = self.rolesanpdata.Name
	self.lb_vipnum.Text = self.rolesanpdata.VipLevel
	self.lb_vipnum.Visible = true
	self.lb_lvnum.Text = self.rolesanpdata.Level
	self.lb_fightnum.Text = self.rolesanpdata.FightPower
	local TitleNameExt = self.rolesanpdata.Options["TitleNameExt"] or ""
	self.titlemodel = UIUtil.SetTitle(self, self.lb_title, self.rolesanpdata.TitleID,TitleNameExt)
	UIUtil.SetImage(self.ib_pro, '$dynamic/TL_login/output/TL_login.xml|TL_login|prol_'.. self.rolesanpdata.Pro)
	local filter =  bit.lshift(1,  GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))
	local avatarInfo = self.rolesanpdata.Avatar
	Init3DModel(self,self.cvs_anchor.Parent, Vector2(self.cvs_anchor.X,self.cvs_anchor.Y), 170, self.ui.menu.MenuOrder, avatarInfo, filter,1)
end

local function SetAttributeInfo(self)
	self.easyinfo[1].Text = self.rolesanpdata.digitID
	self.easyinfo[2].Text = self.allinfo.PKValue
	self.easyinfo[3].Text = self.rolesanpdata.PracticeLv == 0 and Util.GetText('NoAnything') or GameUtil.GetPracticeName(self.rolesanpdata.PracticeLv, 0)
	self.easyinfo[4].Text = string.IsNullOrEmpty(self.rolesanpdata.GuildName) and Util.GetText('NoGuild') or self.rolesanpdata.GuildName
	self.easyinfo[5].Text = PlayRuleModel.GetIdentityString(self.rolesanpdata.ID,self.rolesanpdata.Pro)
end

local function InitAttribute1(self,node,index)
	local lb_info = node:FindChildByEditName('lb_info', true)
	local lb_infonum = node:FindChildByEditName('lb_infonum', true)
	local ib_iback = node:FindChildByEditName('ib_iback', true)

	
	ib_iback.Visible = index % 2 == 1
	lb_info.Text = Util.GetText(self.allinfo.PropSnap[index].key)
	lb_infonum.Text = self.allinfo.PropSnap[index].val
	
end

local function Attribute1(self,index)
	local sp_attribute = self.cvs_other[index]:FindChildByEditName('sp_attribute', true)
	local cvs_info = self.cvs_other[index]:FindChildByEditName('cvs_info', true)
	UIUtil.ConfigVScrollPan(sp_attribute,cvs_info,#self.allinfo.PropSnap,function(node, index)
		InitAttribute1(self,node,index)
	end)
	cvs_info.Visible = false
end

local function ShowAttribute(self,data,types)
	local sp_attribute = self.cvs_other[types]:FindChildByEditName('sp_attribute', true)
	local cvs_attinfo = self.cvs_other[types]:FindChildByEditName('cvs_attinfo', true)
	local lb_fightnum = self.cvs_other[types]:FindChildByEditName('lb_fightnum', true)
	local lb_name = self.cvs_other[types]:FindChildByEditName('lb_name', true)
	local lb_level = self.cvs_other[types]:FindChildByEditName('lb_level', true)

	local name = nil
	local attribute = nil

	if types == 2 then
		name = GlobalHooks.DB.FindFirst('FallenPartnerListData',{god_id = data.id}).god_name
		attribute = PartnerModel.GetFallenPartnetAttr(data.id,data.lv)
	elseif types == 3 then
		name = ArtifactModel.GetArtifact(data.id).artifact_name
 		attribute = ArtifactModel.GetArtifactAttr(data.id,data.lv)
	end

	lb_name.Text = Util.GetText(name)
	if types == 2 then
		local advanceData = PartnerUtil.GetAdvanceData(data.id,data.lv)
		if advanceData then
			lb_level.Text = Constants.Text['fallenpartner_rank'..advanceData.client_rank]
		end
	else
		lb_level.Text = Util.GetText(Constants.LookPlayerInfo.Level,data.lv)
	end
	
	local temp = {}
	for k,v in pairs(attribute) do
		local tempkey = unpack(GlobalHooks.DB.Find('Attribute',{key = k}))
		if tempkey and v ~= 0 then
			if tempkey.client_showtype == 1 then
				v = tostring(v/100)..'%'
			end
			table.insert(temp,{key = tempkey.name,val = v,id = tempkey.id})
		end
	end
	table.sort(temp,function(a,b)
		return a.id<b.id
	end)
	UIUtil.ConfigVScrollPan(sp_attribute,cvs_attinfo,#temp,function(node, index)
		local ib_iback = node:FindChildByEditName('ib_iback', true)
		local lb_key = node:FindChildByEditName('lb_info', true)
		local lb_val = node:FindChildByEditName('lb_infonum', true)
		ib_iback.Visible = index%2 == 1
		lb_key.Text = Util.GetText(temp[index].key)
		lb_val.Text = temp[index].val
	end)
	cvs_attinfo.Visible = false
end

local function InitArtifact(self,node,index,types,data)
	local ib_icon = node:FindChildByEditName('ib_icon', true)
	local artInfo = ArtifactModel.GetArtifact(data.id)
	UIUtil.SetImage(ib_icon,artInfo.icon_id)
	node.TouchClick = function(sender)
		ShowAttribute(self,data,types)
	end
end

local function InitGod(self,node,index,types,data)
	local ib_icon = node:FindChildByEditName('ib_icon', true)
	local godinfo = GlobalHooks.DB.FindFirst('FallenPartnerListData',{god_id = data.id})
	UIUtil.SetImage(ib_icon,'static/item/'..godinfo.icon_id)
	node.TouchClick = function(sender)
		ShowAttribute(self,data,types)
	end
end

local function Attribute2(self,types)
	local lb_nothing = self.cvs_other[types]:FindChildByEditName('lb_nothing', true)
	local sp_list = self.cvs_other[types]:FindChildByEditName('sp_list', true)
	local cvs_list = self.cvs_other[types]:FindChildByEditName('cvs_list', true)
	local lb_fightnum = self.cvs_other[types]:FindChildByEditName('lb_fightnum', true)
	lb_nothing.Visible = true
	local data = {}
	data[2] = {}
	data[3] = {}

	for k,v in pairs(self.allinfo.GoldMap) do
		table.insert(data[2],{id = k,lv = v})
	end
	for k,v in pairs(self.allinfo.ArtifactMap) do
		table.insert(data[3],{id = k,lv = v})
	end


	--显示仙侣
	if types == 2 and #data[2] ~= 0 then
		local fight = 0
		for i=1,#data[types] do
			local attribute = PartnerModel.GetFallenPartnetAttr(data[types][i].id,data[types][i].lv)
			fight = fight + PartnerUtil.GetPartnerPowerbyValue(attribute)
		end
		lb_fightnum.Text = fight
		lb_nothing.Visible = false
		ShowAttribute(self,data[types][1],types)
		UIUtil.ConfigHScrollPan(sp_list,cvs_list,#data[types],function(node, index)
			InitGod(self,node,index,types,data[types][index])
		end)
	--显示神器
	elseif types == 3 and #data[3] ~= 0 then
		fight = GetMiraclePower(self.allinfo.ArtifactMap)
		lb_fightnum.Text = fight
		lb_nothing.Visible = false
		ShowAttribute(self,data[types][1],types)
		UIUtil.ConfigHScrollPan(sp_list,cvs_list,#data[types],function(node, index)
			InitArtifact(self,node,index,types,data[types][index])
		end)
	end
	cvs_list.Visible = false
end

local function Attribute3(self,index)
	local lb_nothing = self.cvs_other[index]:FindChildByEditName('lb_nothing', true)
	local lb_fightnum = self.cvs_other[index]:FindChildByEditName('lb_fightnum', true)
	local cvs_anchor = self.cvs_other[index]:FindChildByEditName('cvs_anchor', true)
	local lb_name = self.cvs_other[index]:FindChildByEditName('lb_name', true)
	lb_nothing.Visible = true


	local wingfilename = nil
	for _,v in pairs(self.rolesanpdata.Avatar) do
		if v.PartTag == 11 then
			wingfilename = v.FileName
		end
	end

	--展示金轮
	if index == 4 and wingfilename then
		lb_nothing.Visible = false
		local fight,name,scale,avatar_res = FillAttributes(self.allinfo.WingLv)
		if avatar_res == wingfilename and wingfilename ~= avatar_res and self.equips[16] then
			scale = unpack(GlobalHooks.DB.Find('AvatarGroup',{id = self.equips[16].id})).checkzoom
		end
		lb_fightnum.Text = fight
		lb_name.Text = Util.GetText(name)
		InitModelByAvatarRes(self,cvs_anchor,wingfilename,index,scale,-80,Vector3(0,180,0))

	--展示坐骑
	elseif index == 5 and self.allinfo.MountId ~= 0 and self.allinfo.MountId then
		lb_nothing.Visible = false
		if self.allinfo.VeinId == 0 then
			lb_fightnum.Text = 0
			local veinAttribute = MountModel.GetVeinData(1)
			lb_name.Text = Util.GetText(MountModel.GetMountVeinNameByRank(veinAttribute.vein_rank))
		else
			local veinAttribute = MountModel.GetVeinData(self.allinfo.VeinId)
			lb_name.Text = Util.GetText(MountModel.GetMountVeinNameByRank(veinAttribute.vein_rank))
			lb_fightnum.Text = MountModel.GetFightByVeinId(self.allinfo.VeinId)
		end
		local model = nil
		if self.equips[11] then
			model = unpack(GlobalHooks.DB.Find('AvatarGroup',{id = self.equips[11].id}))
		end
		if model == nil then
			model = GlobalHooks.DB.FindFirst('Mount',{avatar_id = self.allinfo.MountId})
			InitModelByAvatarRes(self,cvs_anchor,model.avatar_res,index,model.checkzoom,0,Vector3(0,395,0))
		else
			InitModelByAvatarRes(self,cvs_anchor,model.showmodle.value[1],index,model.checkzoom,0,Vector3(0,395,0))
		end
	end
end

local function ChangeAttributeShow(self,index)
	for i=1,#self.cvs_other do
		self.cvs_other[i].Visible = i == index
	end
	if index == 1 then
		Attribute1(self,index)
	elseif index == 2 then
		Attribute2(self,index)
	elseif index == 3 then
		Attribute2(self,index)
	elseif index == 4 then
		Attribute3(self,index)
	elseif index == 5 then
		Attribute3(self,index)
	end
end

--设置属性选择按钮的方法
local function SetSelectBtn(self)
	local cvs_class = self.cvs_attribute:FindChildByEditName('cvs_class', true)
	
	local lb_title2 = cvs_class:FindChildByEditName('lb_title2', true)
	local btn_left = cvs_class:FindChildByEditName('btn_left', true)
	local btn_right = cvs_class:FindChildByEditName('btn_right', true)
	local index = 1
	lb_title2.Text = Util.GetText(Constants.LookPlayerInfo[index])
	ChangeAttributeShow(self,index)
	btn_left.TouchClick = function(sender)
		index = index == 1 and 5 or index - 1
		lb_title2.Text = Util.GetText(Constants.LookPlayerInfo[index])
		ChangeAttributeShow(self,index)
	end
	btn_right.TouchClick = function(sender)
		index = index == 5 and 1 or index + 1
		lb_title2.Text = Util.GetText(Constants.LookPlayerInfo[index])
		ChangeAttributeShow(self,index)
	end
end

function _M.OnEnter( self ,roleid)
	self.model = {}
	self.detailMenu = {}

	Util.GetRoleSnap(roleid, function(data)
		ItemModel.SnapExtReader:LoadMany({roleid},function(rsp)
			self.allinfo = rsp[1]
			self.equips = {}
			self.rolesanpdata = data
			local temp = {}
			for k,v in pairs(self.allinfo.PropSnap) do
				local tempkey = unpack(GlobalHooks.DB.Find('Attribute',{key = string.lower(k)}))
				if tempkey then
					if tempkey.client_showtype == 1 then
						v = tostring(v/100)..'%'
					end
					if tempkey.showtoother == 1 then
						table.insert(temp,{key = tempkey.name,val = v,id = tempkey.id})
					end
				end
			end
			table.sort( temp, function(a,b)
				return a.id<b.id
			end)
			self.allinfo.PropSnap = temp

			for i,v1 in pairs(self.allinfo.EquipMap) do
				local temp = {}
				temp = ItemModel.GetDetailByTemplateID(v1.item.SnapData.TemplateID)
				temp.dynamicAttrs = {}
				for k,v2 in pairs(self.allinfo.GemMap) do
					if v2.EquipPos == i then
						temp.gem = v2
					end
				end
				for k,v2 in pairs(self.allinfo.RefineMap) do
					if v2.EquipPos == i then
						temp.refine = v2
					end
				end
				for k2,v2 in pairs(v1.item.Properties.Properties) do
					table.insert(temp.dynamicAttrs,v2)
				end
				temp.id = v1.item.SnapData.ID
				temp.bind = false
				self.equips[temp.static_equip.equip_pos] = temp
			end
			if self.allinfo.SuitEquipMap then
				for k,v in pairs(self.allinfo.SuitEquipMap) do
					self.equips[tonumber(k)+11] = {static_equip = {id = GetSuitDataByID(v)},id = v}
				end
			end
			self.allinfo.SuitEquipMap = nil
			self.allinfo.RefineMap = nil
			self.allinfo.EquipMap = nil
			self.allinfo.GemMap = nil
			 --print_r('^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^  ',data)
			 --print_r('^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^  ',self.allinfo)
			SetModelInfo(self)
			SetEquipInfo(self)
			SetAttributeInfo(self)
			SetSelectBtn(self)
		end)

		--交互按钮
		self.comps.tbt_more.TouchClick = function(sender)
			sender.IsChecked = false
			SoundManager.Instance:PlaySoundByKey('button',false)
			local args = {}
			args.playerId = roleid
			if args.playerId ~= DataMgr.Instance.UserData.RoleID then
				args.playerName = data.Name
				args.menuKey = 'stranger_more'
				EventManager.Fire("Event.InteractiveMenu.Show", args)
			end
		end
	end)

	self.btn_dress.TouchClick = function(sender)
		CloseItemDetail(self)
		self.cvs_equips.Visible = false
		self.cvs_equip_wardrobe.Visible = true
	end
	self.btn_equip.TouchClick = function(sender)
		CloseItemDetail(self)
		self.cvs_equips.Visible = true
		self.cvs_equip_wardrobe.Visible = false
	end
	self.ui.menu.event_PointerClick = function()
		CloseItemDetail(self)
	end
	self.cvs_model.event_PointerClick = function()
		CloseItemDetail(self)
	end
	self.cvs_attribute.event_PointerClick = function()
		CloseItemDetail(self)
	end
end

function _M.OnExit( self )
	for k,v in pairs(self.model) do
		Release3DModel(self,k)
	end
	if self.titlemodel then
		UI3DModelAdapter.ReleaseModel(self.titlemodel.Key)
		self.titlemodel = nil
	end
	CloseItemDetail(self)
	self.model = nil
	for i, v in pairs(self.cvs_equip) do
		if v.Layout then
			v.Layout = nil
		end
	end
end

function _M.OnInit(self)
	self.cvs_package = self.ui.comps.cvs_package
	self.cvs_equips = self.ui.comps.cvs_equip_panel
	self.cvs_equip_wardrobe = self.ui.comps.cvs_equip_wardrobe
	self.cvs_attribute = self.ui.comps.cvs_attribute

	self.cvs_model = self.cvs_package:FindChildByEditName('cvs_model', true)

	self.ib_pro = self.cvs_package:FindChildByEditName('ib_factions', true)
	self.lb_name = self.cvs_package:FindChildByEditName('lb_name', true)
	self.lb_vipnum = self.cvs_package:FindChildByEditName('lb_vipnum', true)
	self.lb_lvnum = self.cvs_package:FindChildByEditName('lb_lvnum', true)
	self.lb_fightnum = self.cvs_package:FindChildByEditName('lb_fightnum', true)
	self.lb_title = self.cvs_package:FindChildByEditName('lb_title', true)
	self.cvs_anchor = self.cvs_package:FindChildByEditName('cvs_anchor', true)

	self.cvs_equip = {}
	self.cvs_equip[1] = self.cvs_equips:FindChildByEditName('cvs_equip', true)
	self.cvs_equip[2] = self.cvs_equips:FindChildByEditName('cvs_helmet', true)
	self.cvs_equip[3] = self.cvs_equips:FindChildByEditName('cvs_clothes', true)
	self.cvs_equip[4] = self.cvs_equips:FindChildByEditName('cvs_pants', true)
	self.cvs_equip[5] = self.cvs_equips:FindChildByEditName('cvs_belt', true)
	self.cvs_equip[6] = self.cvs_equips:FindChildByEditName('cvs_shoe', true)
	self.cvs_equip[7] = self.cvs_equips:FindChildByEditName('cvs_necklace', true)
	self.cvs_equip[8] = self.cvs_equips:FindChildByEditName('cvs_ring', true)
	self.cvs_equip[9] = self.cvs_equips:FindChildByEditName('cvs_medal', true)
	self.cvs_equip[10] = self.cvs_equips:FindChildByEditName('cvs_adorn', true)
	self.btn_dress = self.cvs_equips:FindChildByEditName('btn_dress', true)

	self.cvs_equip[11] = self.cvs_equip_wardrobe:FindChildByEditName('cvs_mount', true)
	self.cvs_equip[12] = self.cvs_equip_wardrobe:FindChildByEditName('cvs_dress', true)
	self.cvs_equip[13] = self.cvs_equip_wardrobe:FindChildByEditName('cvs_hair', true)
	self.cvs_equip[15] = self.cvs_equip_wardrobe:FindChildByEditName('cvs_wepon', true)
	self.cvs_equip[16] = self.cvs_equip_wardrobe:FindChildByEditName('cvs_wing', true)
	self.cvs_equip[17] = self.cvs_equip_wardrobe:FindChildByEditName('cvs_effect', true)
	self.btn_equip = self.cvs_equip_wardrobe:FindChildByEditName('btn_equip', true)

	self.cvs_other = {}
	self.cvs_text1 = self.cvs_attribute:FindChildByEditName('cvs_text1', true)
	self.cvs_other[1] = self.cvs_attribute:FindChildByEditName('cvs_player', true)
	self.cvs_other[2] = self.cvs_attribute:FindChildByEditName('cvs_partner', true)
	self.cvs_other[3] = self.cvs_attribute:FindChildByEditName('cvs_miracle', true)
	self.cvs_other[4] = self.cvs_attribute:FindChildByEditName('cvs_wing', true)
	self.cvs_other[5] = self.cvs_attribute:FindChildByEditName('cvs_mount', true)

	self.easyinfo = {}
	self.easyinfo[1] = self.cvs_text1:FindChildByEditName('lb_roleid', true)
	self.easyinfo[2] = self.cvs_text1:FindChildByEditName('lb_killinfonum', true)
	self.easyinfo[3] = self.cvs_text1:FindChildByEditName('lb_shengelv', true)
	self.easyinfo[4] = self.cvs_text1:FindChildByEditName('lb_guildname', true)
	self.easyinfo[5] = self.cvs_text1:FindChildByEditName('lb_joblv', true)

	HudManager.Instance:InitAnchorWithNode(self.ui.root, bit.bor(HudManager.HUD_CENTER))
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.menu.ShowType = UIShowType.HideHudAndMenu
end

return _M
