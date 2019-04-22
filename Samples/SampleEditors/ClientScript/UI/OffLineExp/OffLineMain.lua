local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local RechargeModel=require('Model/RechargeModel')

function _M.OnInit(self)

	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	--名字
	--self.lb_welcome=self.ui.comps.lb_back
	--离线时间
	self.lb_time=self.ui.comps.lb_time
	--离线经验
	self.lb_exp=self.ui.comps.lb_exp
	self.btn_ok=self.ui.comps.btn_ok
end


function _M.OnEnter(self,params)

	--黑屏
	self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
	--名字
	--self.lb_welcome.Text=string.format(Constants.System.Welcome,DataMgr.Instance.UserData.Name)
	--经验
	self.lb_exp.Text=params.exp..Constants.System.Point
	--时间
	self.lb_time.Text=string.format(Constants.System.OffLineTime,params.time.Days,params.time.Hours,params.time.Minutes)
	local vipadd=RechargeModel.GetVipInfoValueByKey('offline_exp_addition')
	self.viplevel=DataMgr.Instance.UserData.VipLv

	if  vipadd <= 0 then
		self.ui.comps.lb_novip.Visible=true
		self.ui.comps.cvs_vip.Visible=false
	else
		self.ui.comps.lb_novip.Visible=false
		self.ui.comps.cvs_vip.Visible=true
		self.ui.comps.lb_tips3.FontColor = GameUtil.RGB2Color(Constants.Color.White)
		self.ui.comps.lb_tips3.Text= self.viplevel ==13 and 't' or 'z'..self.viplevel
		self.ui.comps.lb_vipexp.Text=params.vipexp..Constants.System.Point
	end

	self.btn_ok.TouchClick=function()
		self.ui:Close()
	end

end


return _M