local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local UIUtil = require 'UI/UIUtil.lua'
local PetModel= require("Model/PetModel")
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


	if self.lastModle~=nil then
		UI3DModelAdapter.ReleaseModel(self.lastModle.Key)
	end
	self.lastModle = UI3DModelAdapter.AddSingleModel(self.ui.comps.cvs_moxin, Vector2(128.1,194.1), 100, self.ui.menu.MenuOrder,data.model )
end
local function setSkillui(self,data)
	self.ui.comps.l_mingzi.Text = data.name
	self.ui.comps.r_mingzi.Text = data.nameUpgrade
	self.ui.comps.l_skill.XmlText = "<recipe><font size = \"16\">"..data.tips.."</font></recipe>"
	self.ui.comps.r_skill.XmlText = "<recipe><font size = \"16\">"..data.tipsUpgrade.."</font></recipe>"
	self.ui.comps.jineng_img.Layout =  HZUISystem.CreateLayoutFromFile(data.icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	

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
	node.Visible = true
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
local function touchXuan(self,ib_xuanze, index )
	-- body
	if self.lastBt ~=nil then
		self.lastBt.Visible = false
	end
	ib_xuanze.Visible = true
	self.lastBt =ib_xuanze
	self.skillIndex = index
	setSkillui(self,self.SkillInfo[index])
	self.ui.comps.xiaohao1.Visible = false
	self.ui.comps.xiaohao2.Visible = false
	self.ui.comps.xiaohao3.Visible = false
	if #self.SkillInfo[index].costItems > 1 then 
		addXiaohao( self,self.ui.comps.xiaohao1,self.SkillInfo[index].costItems[1],1)
		addXiaohao( self,self.ui.comps.xiaohao2,self.SkillInfo[index].costItems[2],2)
	else
		addXiaohao( self,self.ui.comps.xiaohao3,self.SkillInfo[index].costItems[1],1)
	end

end 
local function setSkill(self,index,node ,data)
	local ib_lock =  node:FindChildByEditName('ib_lock', true)	
	ib_lock.Visible = data.bOpen == 0
	local lb_wenzi =  node:FindChildByEditName('lb_wenzi', true)
	lb_wenzi.Text = data.name
	local btn_add =  node:FindChildByEditName('btn_add', true)
	btn_add.Visible =false
	local ib_shunxu =  node:FindChildByEditName('ib_shunxu', true)
	ib_shunxu.Visible =false
	local ib_icon =  node:FindChildByEditName('ib_icon', true)
	local ib_xuanze =  node:FindChildByEditName('ib_xuanze', true)
	ib_xuanze.Visible = false
	if self.skillIndex == index then 
		touchXuan(self,ib_xuanze, index )
	end
	ib_icon.Layout =  HZUISystem.CreateLayoutFromFile(data.icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	
	ib_icon.Enable = true
	ib_icon.IsInteractive = true
	ib_icon.event_PointerClick = function( sender,touch )
		
		touchXuan(self,ib_xuanze, index )
	end
end 
local function getPetData( self,id )
	PetModel.TLClientPetSkillInfoRequest(id,function ( data )
		self.petInfo = data
		self.SkillInfo = data.skillList
		setPetInfo(self, data)
		for i=1,5 do
			setSkill(self,i,self.skillNode[i] ,self.SkillInfo[i])
		end
	end)
end 
local function getListData( self )
	PetModel.TLClientGetPetListRequest(function ( data )

		self.allData = data
		getPetData( self,self.allData[1].id )
	end)
end 

local function setPetup( self,id,skillid )
	PetModel.TLClientPetSkillUpgradeRequest(id,skillid,function ( data )
		getPetData( self,id )
	end)
end 
-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	print('OnEnter',self,...)
	self.ui.comps.btn_buzhen.TouchClick = function()
		if self.SkillInfo[self.skillIndex].bOpen == 1 then 
			setPetup( self,self.allData[self.lastIndex].id ,self.SkillInfo[self.skillIndex].id)
		end
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
	self.SkillInfo = {}
	self.skillNode = {self.ui.comps.fn_skill1,self.ui.comps.fn_skill2,self.ui.comps.fn_skill3,self.ui.comps.fn_skill4,self.ui.comps.fn_skill5}
	self.lastBt = nil
	self.lastIndex = 1
	self.skillIndex = 1
	self.ui.menu.Enable = false
	self.lastModle = nil
end

return _M