local _M = {}
_M.__index = _M
 
local ItemModel = require 'Model/ItemModel'
local List = require "Logic/List"
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

function _M.PlayEffect(self, node)
    local t = {
            LayerOrder = self.menu.MenuOrder,
            UILayer = true,          --和 Layer = Constants.Layer.UI相同效果
            DisableToUnload = true,  --显示状态未Disable时自动Unload
            Parent = node.Transform,
            Pos = {x = 66, y = -25}
        }
 
    Util.PlayEffect('/res/effect/ui/ef_ui_equie_drop.assetbundles', t)
	
end

local function function_name( ... )
    -- body
end 

function _M.MoveFadeInAction(self)
	local node = self.cvs_itemall
	node.X = self.initX
    node.Y = HZUISystem.SCREEN_HEIGHT * 0.48
-- HZUISystem.SCREEN_WIDTH  + HZUISystem.Instance.StageOffsetY
    local moveIn = MoveAction()
    moveIn.Duration = 0.5
    moveIn.TargetX = self.initX
    moveIn.TargetY = self.initY
    node:AddAction(moveIn)
    local fadeIn = FadeAction()
    fadeIn.ActionEaseType = EaseType.linear
    fadeIn.Duration = 0.5
    fadeIn.TargetAlpha = 1
    node:AddAction(fadeIn)
    moveIn.ActionFinishCallBack = function(sender)

        self:PlayEffect(self.ui.comps.cvs_item1)

        if self.count and self.count == 2 then 
            self:PlayEffect(self.ui.comps.cvs_item2)
        end
    
        self:DoDelayAction()
    end
end
 
function _M.MoveFadeOutAction(self)
	local node = self.cvs_itemall
	local fadeOut = FadeAction()
    fadeOut.ActionEaseType = EaseType.linear
    fadeOut.Duration = 0.5
    fadeOut.TargetAlpha = 0
    node:AddAction(fadeOut)
 	
	local moveOut = MoveAction()
    moveOut.Duration = 0.5
    moveOut.TargetX = self.initX
    moveOut.TargetY = 200
    node:AddAction(moveOut)
    moveOut.ActionFinishCallBack = function(sender)
 		self.ui:Close()
    end
end

function _M.DoDelayAction(self)
	local node = self.cvs_itemall
	local delay = DelayAction()
	delay.Duration = 1
	node:AddAction(delay)
	delay.ActionFinishCallBack = function(sender)
 		self:MoveFadeOutAction()
    end
end

local function ShowIcon(self,node,templateID,Count,label)
    node.Visible = true
    local detial = ItemModel.GetDetailByTemplateID(templateID)
    local itShow = UIUtil.SetItemShowTo(node,detial.static.atlas_id,detial.static.quality,Count)
    label.Text = Util.GetText(detial.static.name)
end

function _M.OnEnter( self, ...)
    self.count = 0
 	-- self.treasureDatas = List:new()
	local params={...}
	local data1 = params[1]
	if not data1 then 
        return
	end

    self.ui.comps.cvs_itemall.Visible = true

    self.ui.comps.cvs_getitem1.Visible = true
    
    ShowIcon(self,self.ui.comps.cvs_item1,data1.templateID,data1.Count,self.ui.comps.lb_name1)


	local data2 = params[2]
	if data2 then
        self.count = 2
        self.ui.comps.cvs_getitem2.Visible = true
        ShowIcon(self,self.ui.comps.cvs_item2,data2.templateID,data2.Count,self.ui.comps.lb_name2)
	else
        self.ui.comps.cvs_getitem2.Visible = false
	end

    self:MoveFadeInAction()

end

function _M.OnExit( self )
	-- self.treasureDatas = List:new()
 
end

function _M.OnDestory( self )
	
 
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.HideBackMenu
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.IsInteractive = false

 	self.cvs_itemall = self.ui.comps.cvs_itemall
	self.initX = self.cvs_itemall.X
    self.initY = self.cvs_itemall.Y

end

return _M