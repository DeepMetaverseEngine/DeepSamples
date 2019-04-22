local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

local function RefreshWarehouse( self, wType, items )
	local rootCvs = wType == 0 and self.comps.cvs_item1 or self.comps.cvs_item2
	for i = 1, 8 do
		local cvs_item = rootCvs:FindChildByEditName('cvs_icon'..i, true)
		local data = items[i]
		if data then
			local Properties
			if data.Properties ~= nil then
				Properties = data.Properties.Properties
			end
		    local detail = ItemModel.GetDetailByTemplateID(data.SnapData.TemplateID, Properties)
		    detail.id = data.SnapData.ID
			local num = data.SnapData.Count
			local itshow = UIUtil.SetItemShowTo(cvs_item, detail, num)
		    itshow.EnableTouch = true
		    itshow.TouchClick = function()
		    	local detailMenu
			    local btns = {{Text=Constants.Text.detail_btn_get,cb = function()
					SocialModel.RequestClientCoupleWarehousePutOff(wType, i, function( ... )
						detailMenu:Close()
						_M.RequestWarehouseData(self)
					end)
				end}}
		       	detailMenu = UIUtil.ShowNormalItemDetail({detail = detail, autoHeight = true, buttons = btns })
		    end
		else
			cvs_item:RemoveAllChildren(true)
		end
	end
end

function _M.RequestWarehouseData( self )
	SocialModel.RequestClientGetCoupleWarehouseData(function(rsp)
		self.selfItems = rsp.selfItems
		self.mateItems = rsp.mateItems
		RefreshWarehouse(self, 0, self.selfItems)
		RefreshWarehouse(self, 1, self.mateItems)
	end)
end

function _M.OnEnter( self )
	SocialModel.SetCoupleWarehouseListener(function( ... )
		_M.RequestWarehouseData(self)
	end)
	_M.RequestWarehouseData(self)
end

function _M.OnExit( self )
	SocialModel.SetCoupleWarehouseListener(nil)
end

function _M.OnInit( self )
	self.ui:EnableTouchFrame(false)
	self.ui.comps.btn_help.TouchClick = function( ... )
		self.ui.comps.cvs_help.Visible = true
	end
    self.ui.comps.cvs_help.event_PointerUp = function( ... )
    	self.ui.comps.cvs_help.Visible = false
    end
end

return _M