local _M = {}
_M.__index = _M
 
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil.lua'

local AvatarModel = require "Model/AvatarModel"
local ItemModel = require "Model/ItemModel"


local function showItem(self,node,index)
	 
	local attrData = self.alldata[index] 
 
	if attrData == nil then
		node.Visible = false
		return
	end

	local name = UIUtil.FindChild(node,'lb_detail')	
	name.Text = attrData.name

	local detail1 = UIUtil.FindChild(node,'lb_detailnum1')	
	detail1.Text = attrData.value

	local detail2 = UIUtil.FindChild(node,'lb_detailnum2')	
	detail2.Text = attrData.nextValue

 end

local function showScrollPan(self)
 
	local function eachupdatecb(node, index) 
		-- print('node:',index)
		showItem(self,node,index)
	end
 	
 	-- print('self.alldata: ',#self.alldata)

	UIUtil.ConfigGridVScrollPan(self.pan,self.tempnode,2,#self.alldata,eachupdatecb)
 
end

 
local function loadData(self)
	-- body

	self.beforelevelLabel.Text = self.Level
	self.afterlevelLabel.Text = self.Level + 1

	local AvatarLevelData = AvatarModel.GetAvatarLevel(self.Level)


	self.maxScore = AvatarLevelData.score_limit
	self.numLabel.Text = self.Score .. '/' .. self.maxScore
 
	local alldata = {}
 	local NextLevelData = AvatarModel.GetAvatarLevel(self.Level + 1)
 	if NextLevelData then
		
  		local all = ItemModel.GetXlsFixedAttribute(AvatarLevelData,true)
  		local nextAll = ItemModel.GetXlsFixedAttribute(NextLevelData,true)

  		for k,v in pairs(all) do
  			-- print_r(k,v)
  			local data = {}
  			data.name,data.value = ItemModel.GetAttributeString(v)
  			_,data.nextValue = ItemModel.GetAttributeString(nextAll[k])
  			table.insert(alldata,data)
  		end

 		self.alldata = alldata

 		showScrollPan(self)
 	end
    


end  

function _M.OnEnter( self, ...)
	-- print('WardrobeDetial OnEnter',self,...)
 	self.Score =  self.ui.menu.ExtParam[1]
	self.Level =  self.ui.menu.ExtParam[2]
	-- print('WardrobeDetial self.Score : ',self.Score )
	-- print('WardrobeDetial self.Level : ',self.Level)
	loadData(self)

end

function _M.OnExit( self )
	print('WardrobeDetial OnExit')
end

function _M.OnDestory( self )
	
	print('WardrobeDetial OnDestory')
end

function _M.OnInit( self )
	print('WardrobeDetial OnInit')

	 self.ui.menu.ShowType = UIShowType.Cover

	self.numLabel = self.ui.comps.lb_num

	self.beforelevelLabel = self.ui.comps.lb_beforelevel
	self.afterlevelLabel = self.ui.comps.lb_afterlevel

	self.pan = self.ui.comps.sp_detail
	self.tempnode = self.ui.comps.cvs_detail
	self.tempnode.Visible = false

	local levelUp = self.ui.comps.tbt_an1 
	levelUp.TouchClick = function (sender)
		
		AvatarModel.ReqWardrobeLevelUp(function(rsp)
			
			self.Level = rsp.s2c_Level

			loadData(self)
			SoundManager.Instance:PlaySoundByKey('jinjie',false)
			EventManager.Fire("Event.WardrobeUI.LevelUp", {s2c_Level = self.Level})   	
		end)
	end
end

return _M