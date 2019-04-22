
local _M = {}
_M.__index = _M
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'

local function OnUpdateCopper(self, val)
	self.comps.lb_num1.Text = val
end

local function OnUpdateDiamond(self, val)
	self.comps.lb_num3.Text = val
end

local function OnUpdateSilver(self, val)
	self.comps.lb_num2.Text = val
end

function _M.Notify(status, userdata, self)
	if userdata:ContainsKey(status, UserData.NotiFyStatus.COPPER) then
		OnUpdateCopper(self, userdata:GetAttribute(UserData.NotiFyStatus.COPPER))
	end
	if userdata:ContainsKey(status, UserData.NotiFyStatus.DIAMOND) then
		OnUpdateDiamond(self, userdata:GetAttribute(UserData.NotiFyStatus.DIAMOND))
	end
	if userdata:ContainsKey(status, UserData.NotiFyStatus.SILVER) then
		OnUpdateSilver(self, userdata:GetAttribute(UserData.NotiFyStatus.SILVER))
	end
end

function _M.OnEnter(self)
	self.attach_key = 'UI.Money.'..os.time()
	DataMgr.Instance.UserData:AttachLuaObserver(self.attach_key, self)
	DataMgr.Instance.UserData:Notify(UserData.NotiFyStatus._COPPER)
	DataMgr.Instance.UserData:Notify(UserData.NotiFyStatus._DIAMOND)
	DataMgr.Instance.UserData:Notify(UserData.NotiFyStatus._SILVER)
	self.btn_add1=self.ui.comps.btn_add1
	self.btn_add2=self.ui.comps.btn_add2
	self.btn_add3=self.ui.comps.btn_add3
	 
	self.btn_add1.TouchClick=function()
	local addcopper = {detail = ItemModel.GetDetailByTemplateID(1),cb = closecb}
		UIUtil.ShowGetItemWay(addcopper)
	end

	self.btn_add2.TouchClick=function()
	local addsilver = {detail = ItemModel.GetDetailByTemplateID(2),cb = closecb}
		UIUtil.ShowGetItemWay(addsilver)
	end

	self.btn_add3.TouchClick=function()
	local adddiamond = {detail = ItemModel.GetDetailByTemplateID(3),cb = closecb}
	MenuMgr.Instance:CloseAllMenu()
	GlobalHooks.UI.OpenUI('Recharge', 0,'RechargePay')
	end

end

function _M.OnExit(self)
	DataMgr.Instance.UserData:DetachLuaObserver(self.attach_key)
end

function _M.OnInit(self)
	self.ui:EnableTouchFrame(true)
	self.ui:EnableChildren(true)
	self.comps.lb_num2.Text = 0
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
end

return _M
