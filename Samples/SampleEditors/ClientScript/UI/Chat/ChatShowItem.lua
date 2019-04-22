local _M = {}
_M.__index = _M

local ChatUtil = require 'UI/Chat/ChatUtil'

local ItemModel = require 'Model/ItemModel'
local Util	= require "Logic/Util"

function _M:OnClickClose(displayNode)
	--关闭
	if self.callback then
		self.callback(nil)
	end
	
	self.ui.menu:Close()
end

function _M:OnItemSingleSelect(new, old)
	if new and self.callback then
		local data = self.listener:GetItemData(new)
		-- print_r('data is:',data)
		local detail = ItemModel.GetDetailByTemplateID(data.TemplateID)
		-- print_r('detail:',detail)
		detail.ID = data.ID
		detail.TemplateID = detail.static.id
		
		detail.Quality = detail.static.quality
		self.callback(detail)
		--self.ui.menu:Close()
	end
end

function _M:OnEnter(pos,pos2)
	-- self.ui.comps.cvs_itemshow.Position2D = pos or self.defaultPos 
	local position
    if pos then
        position = pos
    elseif pos2 then
        position = Vector2(pos2.x,pos2.y - self.Height)
    else
        position = self.defaultPos
    end

    self.ui.comps.cvs_itemshow.Position2D = position

	self.listener:Start(true,false)
	self.list:Init(self.listener, DataMgr.Instance.UserData.Bag.MaxSize)
end

function _M:OnExit()
	self.listener:Stop(false)
end

function _M:OnInit()
	self.defaultPos = self.ui.comps.cvs_itemshow.Position2D
   	self.Height = self.ui.comps.cvs_itemshow.Height

	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.menu.IsInteractive = false

	self.ui.menu.event_PointerClick = function() 
		self:OnClickClose() 
	end

	self.btn_close = self.ui.menu:GetComponent("btn_close")
	self.btn_close.TouchClick = function() 
		self:OnClickClose() 
	end

	local cvs_item = self.ui.comps.cvs_message
	self.list = ItemList(cvs_item.Size2D - Vector2(0, 2),Vector2(60, 60),5)
	self.listener = ItemListener(DataMgr.Instance.UserData.Bag,false,DataMgr.Instance.UserData.Bag.Size)
	self.list.Position2D = Vector2(0, 2)
	self.list.ShowBackground = true
	self.list.EnableSelect = true
	self.list.OnItemSingleSelect = function(new, old)
		self:OnItemSingleSelect(new, old)
	end

	self.listener.OnMatch = function()
		return true
	end
	cvs_item:AddChild(self.list)
end

return _M