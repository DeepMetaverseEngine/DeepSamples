local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local function Release3DModel(self)
	if self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DModel(self, parentCvs, pos2d, scale, menuOrder, avatar, filter)
	local info = UI3DModelAdapter.AddAvatar(parentCvs, pos2d, scale, menuOrder, avatar, filter)
	self.model = info
	local trans = info.RootTrans
	UI3DModelAdapter.SetLoadCallback(info.Key,function(UIModelInfo)
		UIModelInfo.DynamicBoneEnable = true
	end)
	parentCvs.event_PointerMove = function(sender, data)
		local delta = -data.delta.x
		trans:Rotate(Vector3.up, delta * 1.2)
	end
end

local function OnAvatarChange( self, eventname, params )
	Release3DModel(self)
	local filter = bit.lshift(1,  GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))
	Init3DModel(self, self.ui.comps.cvs_model, self.ui.comps.cvs_anchor.Position2D, 160, self.ui.menu.MenuOrder, params.avatar, filter)
end

function _M.OnEnter( self )
	-- print('AttributeRole OnEnter')
	local filter = bit.lshift(1,  GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))
	Init3DModel(self, self.ui.comps.cvs_model, self.ui.comps.cvs_anchor.Position2D, 160, self.ui.menu.MenuOrder, DataMgr.Instance.UserData:GetAvatarList(), filter)
	--self.ui.comps.lb_lv.Text = Util.GetText("common_level2", DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0))
	self.ui.comps.lb_lv.Text = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)

	local pro = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.PRO, 0)
	local pro_icon = self.ui.comps.ib_factions
 	UIUtil.SetImage(pro_icon, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|pro_'.. pro)

	local exp = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.EXP, 0)
	local maxExp = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.NEEDEXP, 0)
	self.ui.comps.lb_name.Text = DataMgr.Instance.UserData.Name
	self.ui.comps.lb_exp.Text = exp..'/'..maxExp
	self.titleModel = UIUtil.SetTitle(self, self.ui.comps.lb_title, DataMgr.Instance.UserData.TitleID,DataMgr.Instance.UserData.TitleNameExt)
	
	
	
	local expGauge = self.ui.comps.gg_exp
	expGauge:SetGaugeMinMax(0, maxExp)
	expGauge.Value = exp
	-- local pkValue, pkColor = DataMgr.Instance.UserData:GetPKValue(0)
	-- self.ui.menu:SetLabelText('lb_killinfonum', tostring(pkValue), pkColor, 0)
	_M.eventFun = function( eventname, params )
		OnAvatarChange(self, eventname, params)
	end
	EventManager.Subscribe("EVENT_ACTOR_AVATAR_CHANGE", _M.eventFun)
end

function _M.OnExit( self )
	-- print('AttributeRole OnExit')
	EventManager.Unsubscribe("EVENT_ACTOR_AVATAR_CHANGE", _M.eventFun)
	Release3DModel(self)
	if self.titleModel then
		UI3DModelAdapter.ReleaseModel(self.titleModel.Key)
		self.titleModel = nil
	end
end

function _M.OnDestory( self )
	-- print('AttributeRole OnDestory')
end

function _M.OnInit( self )
	-- print('AttributeRole OnInit')
    self.ui:EnableTouchFrame(false)
    self.ui.comps.cvs_package.Enable = false
    self.ui.comps.cav_element.Enable = false
	-- self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)

	self.comps.btn_alter.TouchClick = function ( ... )
		-- body
		 GlobalHooks.UI.OpenUI('TitleFrame',0)
	end
end

return _M