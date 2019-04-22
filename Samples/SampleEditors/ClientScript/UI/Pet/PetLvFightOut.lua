local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local UIUtil = require 'UI/UIUtil.lua'
local PetModel= require("Model/PetModel")

local _M = {}
DisplayUtil.warpOOPSelf(_M)
local function getAllData( self )
	PetModel.TLClientPetPatternRequest(function ( data )
		for i=1,10 do
			self.allNode[i].Visible = false
			if i <= #data.followPet then 
				self.allNode[i].Visible = true
				self.setPetInfo(self,self.allNode[i],data.followPet[i])
			end
		end
		if data.mainPet~=nil then 
			self.setPetInfo(self,self.ui.comps.fn_zhu,data.mainPet)
		end
	end)
end 
local function setPetMain( self,id )
	PetModel.TLClientPetMainRequest(id,function ( data )
		getAllData( self )
	end)
end 
function _M.setPetInfo(self,node,data)
	-- body
	local ib_lock =  node:FindChildByEditName('ib_lock', true)	
	ib_lock.Visible = false
	local lb_wenzi =  node:FindChildByEditName('lb_wenzi', true)
	lb_wenzi.Text = data.fightPower
	local btn_add =  node:FindChildByEditName('btn_add', true)
	btn_add.Visible =false
	local ib_shunxu =  node:FindChildByEditName('ib_shunxu', true)
	ib_shunxu.Visible = false
	local ib_icon =  node:FindChildByEditName('ib_icon', true)
	local ib_xuanze =  node:FindChildByEditName('ib_xuanze', true)
	ib_xuanze.Visible = false
	ib_icon.Layout =  HZUISystem.CreateLayoutFromFile(data.icon,UILayoutStyle.IMAGE_STYLE_BACK_4,0)	
	ib_icon.Enable = true
	ib_icon.IsInteractive = true
	ib_icon.event_PointerClick = function( sender,touch )
		setPetMain( self,data.id )
	end
	if data.icon==nil or data.icon=="" then 
		ib_xuanze.Visible = true
	end
end






-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	print('OnEnter',self,...)
	self.ui.comps.btn_close.TouchClick = function()
		print('bt_login TouchClick')
		self.ui:Close()
	end
	getAllData( self )
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
	self.allNode = {self.ui.comps.fn_a1,self.ui.comps.fn_a2,self.ui.comps.fn_a3,self.ui.comps.fn_a4,self.ui.comps.fn_a5,
					self.ui.comps.fn_a6,self.ui.comps.fn_a7,self.ui.comps.fn_a8,self.ui.comps.fn_a9,self.ui.comps.fn_a10,}
	self.lastBt = nil
	self.lastIndex = 1

end

return _M