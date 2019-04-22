local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local PagodaModel = require 'Model/PagodaModel'
local ActivityModel = require 'Model/ActivityModel'

local function InitData(self,node,data,index)
	
	local itemData = ActivityModel.GetItemData(data)
	local ib_itemlist = node:FindChildByEditName('ib_itemlist', true)
	local lb_itemlist = node:FindChildByEditName('lb_itemlist', true)
	local itemShow = ItemShow.Create(data, 0, 0)
	itemShow.Size2D = ib_itemlist.Size2D
	itemShow.EnableTouch = false
	ib_itemlist:AddChild(itemShow)
	lb_itemlist.Text = Util.GetText(itemData.name)
	-- lb_itemlist.FontColor = GameUtil.RGB2Color(Constants.QualityColor[itemData.quality])

end

function _M.SetInfo(self,data)
	UIUtil.ConfigVScrollPan(self.pan,self.tempnode, #data, function(node, index)
		InitData(self,node,data[index],index)
	end)
end

function _M.SetPos( self,pos)
	self.cvs_itemlist.X = pos[1]
	self.cvs_itemlist.Y = pos[2]
end

function _M.OnEnter( self )
	self.tempnode.Visible = false
end

function _M.OnExit( self )

end


function _M.OnInit(self)
	self.pan = UIUtil.FindChild(self.ui.comps.cvs_itemlist, 'sp_itemlist', true)
	self.tempnode = UIUtil.FindChild(self.ui.comps.cvs_itemlist, 'cvs_itemlist', true)
	self.cvs_itemlist = self.ui.comps.cvs_itemlist

	
	self.ui:EnableTouchFrameClose(true)
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.menu.ShowType = UIShowType.Cover
end


return _M