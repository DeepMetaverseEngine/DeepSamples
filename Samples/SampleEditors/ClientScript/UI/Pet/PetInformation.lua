local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local UIUtil = require 'UI/UIUtil.lua'
local PetModel= require("Model/PetModel")
local ItemModel = require("Model/ItemModel")
local Util = require("Logic/Util")
local _M = {}
DisplayUtil.warpOOPSelf(_M)
local detailMenu = nil
local function getListData( self )
	PetModel.TLClientGetPetListRequest(function ( data )

		self.allData = data
		if self.allData~=nil then 
			print("self.allData-------------------",#self.allData)
			self.ui.comps.sp_oar:ResetRowsAndColumns(1,#self.allData)
		end
	end)
end 


local function setPetInfo(self ,data)
	-- body
	self.ui.comps.lb_petname.Text = data.name
	self.ui.comps.ib_pinjie.Text = "+"..data.qualityLevel
	local pos = GlobalHooks.DB.Find('pet',{quality=data.quality})
	self.ui.comps.lb_pinzhi.XmlText =  "<recipe>"..pos[1].quality_tips.."</recipe>"
	self.ui.comps.lb_expnum.Text = data.exp.."/"..data.expMax
	self.ui.comps.lb_zhanli.Text = Util.GetText("pet_fight")..data.fightPower
	local v = data.exp/data.expMax
	if v > 1 then 
		v = 1
	elseif v <=0 then 
		v = 0
	end
	self.ui.comps.gg_exp.Value = v*100
	self.ui.comps.lb_petlv.Text = "LV"..data.level

	if self.lastModle~=nil then
		UI3DModelAdapter.ReleaseModel(self.lastModle.Key)
	end
	self.lastModle = UI3DModelAdapter.AddSingleModel(self.ui.comps.cvs_moxin, Vector2(128.1,194.1), 100, self.ui.menu.MenuOrder,data.model )
end
local function setPetAllInfo(self,data )
	-- body
	self.ui.comps.lb_num1.Text = data.attributes[1].value
	self.ui.comps.lb_num2.Text = data.attributes[2].value
	self.ui.comps.lb_num3.Text = data.attributes[3].value
	self.ui.comps.lb_num4.Text = data.attributes[4].value
	self.ui.comps.lb_num5.Text = data.attributes[5].value
	self.ui.comps.lb_num6.Text = data.attributes[6].value
	self.ui.comps.lb_num7.Text = data.attributes[7].value
	self.ui.comps.lb_num8.Text = data.attributes[8].value
	self.ui.comps.lb_num9.Text = data.attributes[9].value
	self.ui.comps.lb_num10.Text = data.attributes[10].value
	local pos = GlobalHooks.DB.Find('pet',{type=data.type})
	self.ui.comps.lb_xi.XmlText = "<recipe>"..pos[1].type_tips.."</recipe>"
	--local text = "<recipe><font size = \"18\" color=\"ffff6646\">55555555555555555555</font></recipe>"
	self.ui.comps.tbh_wenzi.XmlText = data.tips
end
local function ShowItemDetail(self, detail)

	detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
	self:AddSubUI(detailMenu)
	detailMenu:Reset({detail=detail,index=1,compare=false,IsEquiped=false})
	-- detailMenu:EnableTouchFrame(false)
	detailMenu:SetButtons({})
	self.ui.comps.pin.Enable = true
	self.ui.comps.pin.IsInteractive = true
end
local function addItem( self,node,slot,detail)
	-- body
	--print("slot.Item---------------------",slot.Item)
	--printG(slot.Item)
	local canve = node--self.ui.comps.cvs_costitem
	canve.EnableChildren = true
	canve:RemoveAllChildren(true)
	local itemShow = ItemShow.Create(slot.icon,slot.quality,slot.num)
	itemShow.Size2D = canve.Size2D
	itemShow.EnableTouch = true
	itemShow.IsEquiped = false
	itemShow.TouchClick = function( sender )
		ShowItemDetail(self, detail)
	end
	canve:AddChild(itemShow)

end
local function addMask( self )
	self.ui.comps.pin.Transform.anchoredPosition3D = Vector3(-400,400,0) 
	self.ui.comps.pin.Size2D = Vector2(10000,10000)
	self.ui.comps.pin.Layout = UILayout.CreateUILayoutColor(Color.clear,Color.clear)
	self.ui.comps.pin.Enable = false
	self.ui.comps.pin.IsInteractive = false
	self.ui.comps.pin.event_PointerClick = function()
		if detailMenu~=nil then 
			detailMenu.ui:Close() 
			detailMenu = nil
		end

		self.ui.comps.pin.Enable = false
		self.ui.comps.pin.IsInteractive = false
	end
end
local function setPetgetInfo(self,data )
	-- body
	local text =  Util.GetText("pet_suipian",data.name)--"<recipe><font size = \"18\" color=\"ffff6646\">碎片--</font></recipe>"
	print("Text-------------------",text)
	self.ui.comps.tbh_zi1.XmlText = text
	local text2 = Util.GetText("pet_suipianshu",data.pieceNum)--"<recipe><font size = \"18\" color=\"ffff6646\">目前拥有碎片数量"..data.pieceNum.."</font></recipe>"
	self.ui.comps.tbh_zi2.XmlText = text2
	local text3 = Util.GetText("pet_huode")--"<recipe><font size = \"18\" color=\"ffff6646\">通过十连抽可以获得</font></recipe>"
	self.ui.comps.tbh_zi3.XmlText = text3
	local detail = ItemModel.GetDetailByTemplateID(data.item_piece)
	local tem = {}
	tem.icon = detail.static.atlas_id
	tem.quality = detail.static.quality
	tem.num = 0
	addItem( self,self.ui.comps.suicon,tem,detail)
	--self.ui.comps.suicon.Layout =  HZUISystem.CreateLayoutFromFile(data.icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	

end
local function getPetData( self,id )
	PetModel.TLClientPetDetailRequest(id,function ( data )
		self.petInfo = data
		setPetInfo(self, data)
		setPetAllInfo(self,data )
		setPetgetInfo(self,data )
	end)
end 
local function touchXuan(self,ib_xuanze, index )
	-- body
	if self.lastBt ~=nil then
		self.lastBt.Visible = false
	end
	ib_xuanze.Visible = true
	self.lastBt =ib_xuanze
	self.lastIndex = index
	--
	getPetData(self,self.allData[index].id)
end 
local function RefreshCellData( self,node,index )
	--print("index---------------------",index)
	node.Visible = true
	local ib_lock =  node:FindChildByEditName('ib_lock', true)	
	ib_lock.Visible = false
	local lb_wenzi =  node:FindChildByEditName('lb_wenzi', true)
	lb_wenzi.Visible = false
	local btn_add =  node:FindChildByEditName('btn_add', true)
	btn_add.Visible =false
	local ib_shunxu =  node:FindChildByEditName('ib_shunxu', true)
	ib_shunxu.Text = index
	local ib_icon =  node:FindChildByEditName('ib_icon', true)
	local ib_xuanze =  node:FindChildByEditName('ib_xuanze', true)
	ib_xuanze.Visible = false
	if self.lastIndex == index then 
		touchXuan(self,ib_xuanze, index )
	end
	ib_icon.Layout =  HZUISystem.CreateLayoutFromFile(self.allData[index].icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	
	ib_icon.Enable = true
	ib_icon.IsInteractive = true
	ib_icon.event_PointerClick = function( sender,touch )
		
		touchXuan(self,ib_xuanze, index )
	end
	
end


-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	print('OnEnter',self,...)
	self.ui.comps.btn_gaiming.TouchClick = function()

	end
	self.ui.comps.btn_waiguan.TouchClick = function()

	end
	self.ui.comps.btn_del.TouchClick = function()

	end
	self.ui.comps.btn_buzhen.TouchClick = function()
		GlobalHooks.UI.OpenUI("PetLvFightOut")
	end
	self.ui.comps.btn_expadd.TouchClick = function()
		self.petInfo.icon = self.allData[self.lastIndex].icon
		print("self.petInfo.icon-----------",self.petInfo.icon)
		GlobalHooks.UI.OpenUI("PetLvUp",0,self.petInfo)
	end
	self.ui.comps.bt_zuo.TouchClick = function()
		self.lastIndex = self.lastIndex - 1
		if self.lastIndex<=1 then 
			self.lastIndex = 1
		end
		self.ui.comps.sp_oar:ResetRowsAndColumns(1,#self.allData)
		DisplayUtil.lookAt(self.ui.comps.sp_oar,self.lastIndex)
	end
	self.ui.comps.bt_you.TouchClick = function()
		self.lastIndex = self.lastIndex + 1
		if self.lastIndex>=#self.allData then 
			self.lastIndex = #self.allData
		end

		self.ui.comps.sp_oar:ResetRowsAndColumns(1,#self.allData)
		DisplayUtil.lookAt(self.ui.comps.sp_oar,self.lastIndex)
	end
	self.ui.comps.tbt_hot.event_PointerClick = function()
		self.ui.comps.tbt_hot.IsChecked  = true
		self.ui.comps.tbt_rarity.IsChecked  = false
		self.ui.comps.cvs_xinxik.Visible = true
		self.ui.comps.cvs_get.Visible = false
	end
	self.ui.comps.tbt_rarity.event_PointerClick = function()
		self.ui.comps.tbt_hot.IsChecked  = false
		self.ui.comps.tbt_rarity.IsChecked  = true
		self.ui.comps.cvs_xinxik.Visible = false
		self.ui.comps.cvs_get.Visible = true
	end
	self.ui.comps.tbt_hot.IsChecked  = true
	self.ui.comps.tbt_rarity.IsChecked  = false
	addMask( self )
	getListData( self )
end

function _M.OnExit( self )
	print('OnExit')
	if self.lastModle~=nil then
		UI3DModelAdapter.ReleaseModel(self.lastModle.Key)
	end
end

function _M.OnDestory( self )
	print('OnDestory')
end

function _M.OnInit( self )
	print('OnInit')
	self.allData = {}
	self.petInfo = nil
	self.lastBt = nil
	self.lastIndex = 1
	self.ui.menu.Enable = false
	self.lastModle = nil
	--self.ui.menu.ShowType = UIShowType.HideBackMenu
    local pan = self.ui.comps.sp_oar
	local cell = self.ui.comps.fn_pet
	cell.Visible = false
	pan:Initialize(cell.Size2D.x+20, 50, 0, 0, cell, function(gx, gy, node )
		RefreshCellData(self, node, gx+1)
	end,function() end)
end

return _M