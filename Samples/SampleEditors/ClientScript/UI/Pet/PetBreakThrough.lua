local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local UIUtil = require 'UI/UIUtil.lua'
local PetModel= require("Model/PetModel")
local Util = require("Logic/Util")
local ItemModel = require("Model/ItemModel")
local detailMenu = nil
local _M = {}
DisplayUtil.warpOOPSelf(_M)
local function setPetInfo(self ,data)
	-- body
	self.ui.comps.lb_petname.Text = data.name
	self.ui.comps.ib_pinjie.Text = "+"..data.qualityLevel
	local pos = GlobalHooks.DB.Find('pet',{quality=data.quality})
	self.ui.comps.lb_pinzhi.XmlText =  "<recipe>"..pos[1].quality_tips.."</recipe>"
	self.ui.comps.lb_expnum.Text = data.exp.."/"..data.expMax
	self.ui.comps.lb_zhanli.Text =  Util.GetText("pet_fight")..data.fightPower
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
local function setPetInfoL(self,data )
	local pos = GlobalHooks.DB.Find('pet',{quality=data.quality})
	self.ui.comps.l_pinzhi.XmlText =  "<recipe><font size = \"18\">"..pos[1].quality_tips.."+"..data.qualityLevel.."</font></recipe>"

	self.ui.comps.l_jineng.Text = Util.GetText("pet_shangxian",data.skillNum)
	self.ui.comps.l_zhanli.Text = Util.GetText("pet_fight")..data.fightPower
	self.ui.comps.l_img.Layout =  HZUISystem.CreateLayoutFromFile(data.icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	

end
local function setPetInfoR(self ,data)
	local pos = GlobalHooks.DB.Find('pet',{quality=data.qualityUpgrade})
	self.ui.comps.r_pinzhi.XmlText =  "<recipe><font size = \"18\">"..pos[1].quality_tips.."+"..data.qualityLevelUpgrade.."</font></recipe>"

	self.ui.comps.r_jineng.Text =  Util.GetText("pet_shangxian",data.skillNumGrade)
	self.ui.comps.r_zhanli.Text = Util.GetText("pet_fight")..data.fightPowerUpgrade
	self.ui.comps.r_img.Layout =  HZUISystem.CreateLayoutFromFile(data.iconUpgrade,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	

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
local function addItem( self,node,slot,detail,index)
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
	self.allItem[index] = itemShow
	canve:AddChild(itemShow)

end
local function addXiaohao( self,node,data,index)
	local detail = ItemModel.GetDetailByTemplateID(data.id)
	local tem = {}
	--print_r(detail)
	tem.icon = detail.static.atlas_id
	tem.quality = detail.static.quality
	tem.num = data.num
	addItem( self,node,tem,detail,index)
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
local function getPetData( self,id )
	PetModel.TLClientPetUpgradeInfoRequest(id,function ( data )
		self.petInfo = data
		setPetInfo(self, data)
		setPetInfoL(self,data)
		setPetInfoR(self ,data)
		addXiaohao( self,self.ui.comps.xiaohao1,data.costItems[1],1)
		addXiaohao( self,self.ui.comps.xiaohao2,data.costItems[2],2)
	end)
end 
local function getListData( self )
	PetModel.TLClientGetPetListRequest(function ( data )

		self.allData = data
		getPetData( self,self.allData[1].id )
	end)
end 

local function setPetup( self,id )
	PetModel.TLClientPetUpgradeRequest(id,function ( data )
		getPetData( self,id )
	end)
end 




-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	print('OnEnter',self,...)
	self.ui.comps.btn_buzhen.TouchClick = function()
		setPetup( self,self.allData[self.lastIndex].id )
	end

	self.ui.comps.btn_expadd.TouchClick = function()
		GlobalHooks.UI.OpenUI("PetLvUp",0,self.petInfo)
	end
	self.ui.comps.ib_zuo.Enable = true
	self.ui.comps.ib_zuo.IsInteractive = true
	self.ui.comps.ib_zuo.TouchClick = function()
		self.lastIndex = self.lastIndex - 1
		self.ui.comps.ib_you.Visible = true
		if self.lastIndex<=1 then 
			self.lastIndex = 1
			self.ui.comps.ib_zuo.Visible = false
		end
		getPetData( self,self.allData[self.lastIndex].id )

	end
	self.ui.comps.ib_you.Enable = true
	self.ui.comps.ib_you.IsInteractive = true
	self.ui.comps.ib_you.TouchClick = function()
		self.lastIndex = self.lastIndex + 1
		self.ui.comps.ib_zuo.Visible = true
		if self.lastIndex>=#self.allData then 
			self.lastIndex = #self.allData
			self.ui.comps.ib_you.Visible = false
		end

		getPetData( self,self.allData[self.lastIndex].id )
	end
	self.ui.comps.ib_zuo.Visible = false
	self.ui.comps.ib_you.Visible = true
	getListData( self )
	addMask( self )
end

function _M.OnExit( self )
	print('OnExit')
end

function _M.OnDestory( self )
	print('OnDestory')
end

function _M.OnInit( self )
	print('OnInit')
	self.allData = {}
	self.allItem={}
	self.petInfo=nil
	self.lastBt = nil
	self.lastIndex = 1
	self.ui.menu.Enable = false
	self.lastModle = nil
end

return _M