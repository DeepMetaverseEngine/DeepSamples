local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local SocialModel = require 'Model/SocialModel'
local Util = require 'Logic/Util'

local function OnWarehourseEnter(self)
	self.RoleBagItem:SetOnInitDetailCB(function(detailMenu)
		local btns = {{Text=Constants.Text.detail_btn_put,cb = function()
			local bag = DataMgr.Instance.UserData.Bag
			bag:PutToWarehouse(detailMenu.index, detailMenu.count,function()
				detailMenu:Close()
			end)
		end}}
		detailMenu:SetButtons(btns)
		detailMenu:SetPos(610,94)
		-- detailMenu:EnableTouchFrameClose(true)
	end)
end

local function OnMarryWarehouseEnter(self)
	self.RoleBagItem:SetOnInitDetailCB(function(detailMenu)
		local btns = {{Text=Constants.Text.detail_btn_put,cb = function()
			-- local bag = DataMgr.Instance.UserData.Bag
			-- bag:PutToWarehouse(detailMenu.index, detailMenu.count,function()
			-- 	detailMenu:Close()
			-- end)
			-- print('道具存倉')
			SocialModel.RequestClientCoupleWarehousePutOn(detailMenu.index, detailMenu.count, function( ... )
				detailMenu:Close()
			end)

		end}}
		detailMenu:SetButtons(btns)
		detailMenu:SetPos(610,94)
		-- detailMenu:EnableTouchFrameClose(true)
	end)
end


function _M.OnEnter(self,subui)
	local RoleBagItem = GlobalHooks.UI.CreateUI('RoleBagItem')
	self:AddSubUI(RoleBagItem)
	self.RoleBagItem = RoleBagItem
	local function ToggleFunction(sender)
		if sender:Equals(self.comps.tbt_an1) then
			local RoleBagWarehourse =  GlobalHooks.UI.CreateUI('RoleBagWarehourse')
			self.RoleBagItem.comps.tbt_decompose.Visible = false
			self.RoleBagItem.comps.tbt_decompose.IsChecked = false
			self:AddSubUI(RoleBagWarehourse)
			self.RoleBagWarehourse = RoleBagWarehourse
			OnWarehourseEnter(self)
			if self.MarryWarehouse then
				self.MarryWarehouse:Close()
				self.MarryWarehouse = nil
			end
		elseif sender:Equals(self.comps.tbt_an2) then
			self.RoleBagItem.comps.tbt_decompose.Visible = true
			if self.RoleBagWarehourse then
				self.RoleBagWarehourse:Close()
				self.RoleBagWarehourse = nil
			end
			if self.MarryWarehouse then
				self.MarryWarehouse:Close()
				self.MarryWarehouse = nil
			end
			self.RoleBagItem:SetOnInitDetailCB(nil)
		elseif sender:Equals(self.comps.tbt_an3) then
			if self.RoleBagWarehourse then
				self.RoleBagWarehourse:Close()
				self.RoleBagWarehourse = nil
			end
			self.RoleBagItem.comps.tbt_decompose.Visible = false
			self.RoleBagItem.comps.tbt_decompose.IsChecked = false
			if string.IsNullOrEmpty(DataMgr.Instance.UserData.SpouseId) then
				sender.IsChecked = false
				GameAlertManager.Instance:ShowNotify(Util.GetText('需要豪华婚礼才能解锁夫妻仓库'))
			else
				local MarryWarehouse =  GlobalHooks.UI.CreateUI('MarryWarehouse')
				self.MarryWarehouse = MarryWarehouse
				self:AddSubUI(MarryWarehouse)
				OnMarryWarehouseEnter(self)
			end
		end
	end
	UIUtil.ConfigToggleButton({self.comps.tbt_an1,self.comps.tbt_an2,self.comps.tbt_an3},self.comps.tbt_an2,false,ToggleFunction)

	if subui == 'RoleBagDecompose' then
		self.RoleBagItem:OpenRoleBagDecompose()
	end
end

function _M.OnExit(self)
	self.RoleBagWarehourse = nil
end

function _M.OnInit(self)
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
end

return _M