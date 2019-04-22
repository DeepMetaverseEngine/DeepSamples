local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local UIUtil = require 'UI/UIUtil.lua'
local PetModel= require("Model/PetModel")
local ItemModel = require("Model/ItemModel")
local _M = {}
DisplayUtil.warpOOPSelf(_M)


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
		--ShowItemDetail(self, detail)
	end
	self.allItem[index] = itemShow
	canve:AddChild(itemShow)

end
local function addXiaohao( self,node,data,index)
	local detail = ItemModel.GetDetailByTemplateID(data.id)
	local tem = {}
	tem.icon = detail.static.atlas_id
	tem.quality = detail.static.quality
	tem.num = data.num
	addItem( self,node,tem,detail,index)
end 
local function setText(self,lv,currExp,expMax )
	self.ui.comps.lb_petlv.Text = "LV"..lv
	self.ui.comps.lb_expnum.Text = currExp.."/"..expMax
	local v = currExp/expMax
	if v > 1 then 
		v = 1
	elseif v <=0 then 
		v = 0
	end
	self.ui.comps.gg_exp.Value = v*100
end 
local function setPetInfo(self,data)
	-- body
	setText(self,data.level,data.exp,data.expMax )
	self.ui.comps.ib_icon.Layout =  HZUISystem.CreateLayoutFromFile(data.icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	
	addXiaohao( self,self.ui.comps.cvs_item1,data.developItems[1],1)
	addXiaohao( self,self.ui.comps.cvs_item2,data.developItems[2],2)
end

local function useItem( self ,petid,itemid,index)
	PetModel.TLClientPetDevelopRequest(petid,itemid,function ( data )

		self.allItem[index].Num = self.allItem[index].Num - 1
		setText(self,data.s2c_level,data.s2c_currExp,data.s2c_expMax )
	end)

end 

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, data)
	self.allData = data

	self.ui.comps.btn_close.TouchClick = function()
		print('bt_login TouchClick')
		self.ui:Close()
	end

	self.ui.comps.btn_use1.TouchClick = function()
		--print("TouchClick------------------------")
		useItem( self ,self.allData.id,self.allData.developItems[1].id,1)
	end
	self.ui.comps.btn_use1.event_LongPoniterDownStep = function()
		useItem( self ,self.allData.id,self.allData.developItems[1].id,1)
	end


	self.ui.comps.btn_use2.TouchClick = function()
		useItem( self ,self.allData.id,self.allData.developItems[2].id,2)
	end
	self.ui.comps.btn_use2.event_LongPoniterDownStep = function()
		useItem( self ,self.allData.id,self.allData.developItems[2].id,2)
	end
	setPetInfo(self,self.allData)
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
	self.allItem = {}
	self.lastBt = nil
	self.lastIndex = 1

end

return _M