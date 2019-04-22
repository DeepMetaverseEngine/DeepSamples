local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local PagodaModel = require 'Model/PagodaModel'
local ActivityModel = require 'Model/ActivityModel'



local function ConfigHScrollPan(pan, tempnode, count, col, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
		local index = gy * col + gx + 1
		if index > count then
			node.Visible = false
		else
			node.Visible = true
			eachupdatecb(node, index)
		end
    end
    local s = tempnode.Size2D
    local row = count / col
    if count % col ~= 0 then
        row = row + 1
    end
    pan:Initialize(
        s.x+20,
        s.y+20,
        row,
        col,
        Vector2(70, 0),
        tempnode,
        UpdateListItem,
        function()
        end
    )
end


local function SetPreview(self,node,params)
	local itemData = ActivityModel.GetItemData(params)
	UIUtil.SetItemShowTo(node,itemData.atlas_id,itemData.quality,1)

	node.TouchClick = function( sender )
		UIUtil.ShowTips(self,sender,itemData.id)
	end
end

function _M.OnEnter( self, params,entertype)
	self.cvs_item.Visible = false

	self.sp_item:RefreshShowCell()
	if not params then
		params = {}
	end
	if entertype then
		self.lb_title.Text = Util.GetText(Constants.PagodaMain.RewardTittle2)
	else
		self.lb_title.Text = Util.GetText(Constants.PagodaMain.RewardTittle1)
	end

	for i, v in pairs(params) do
		if v == 0 or v ==nil then
			table.remove(params,i)
		end
	end
    
	ConfigHScrollPan(self.sp_item,self.cvs_item, #params,4, function(node, index)
		SetPreview(self,node,params[index])
	end)
	

	self.ui.menu.event_PointerUp = function(sender)
		if not self.canPress then
			self.ui:Close()
			return
		end
	end
end


function _M.OnInit(self)
	self.sp_item = UIUtil.FindChild(self.ui.comps.cvs_reward, 'sp_item', true)
	self.cvs_item = UIUtil.FindChild(self.ui.comps.cvs_reward, 'cvs_item', true)
	self.lb_title = UIUtil.FindChild(self.ui.comps.cvs_reward, 'lb_title', true)

	self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end


return _M