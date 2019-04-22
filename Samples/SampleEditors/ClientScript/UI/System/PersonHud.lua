local _M = {}
_M.__index = _M
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local TimeUtil=require 'Logic/TimeUtil'
local SocialModel = require'Model/SocialModel'
local debugTimes = 0
local openTimes = 0

function _M.Notify(status,settingdata,opt)

    -- if settingdata:ContainsKey(status, SettingData.NotifySettingState.ALL) then
    -- 	local value = DataMgr.Instance.SettingData:GetAttribute(status)--SettingData.NotifySettingState.ALL)
    -- 	print_r(status,'------',value)
    -- end

end

local function InitTbt(tbtnode ,value)
	if value == '1' then
		tbtnode.IsChecked = true
	elseif value == '0' then
		tbtnode.IsChecked = false
	end
end

local function SetTbt(self ,tbtnode ,nodifykey ,key )
	tbtnode.TouchClick = function(sender)
		if sender.IsChecked then
			DataMgr.Instance.SettingData:ChangerData(nodifykey,1)
			self.changeredData[key] = '1'
		else
			DataMgr.Instance.SettingData:ChangerData(nodifykey,0)
			self.changeredData[key] = '0'
		end
	end
end

local function IntiConfig(self)
	InitTbt(self.tbt_friend ,self.settingdata.friend_request)
	InitTbt(self.tbt_team ,self.settingdata.enemy_request)
	InitTbt(self.tbt_mm1 ,self.settingdata.chat_friend)
	InitTbt(self.tbt_mm2 ,self.settingdata.chat_guild)
	InitTbt(self.tbt_mm3 ,self.settingdata.chat_stranger)
	InitTbt(self.tbt_fight1 ,self.settingdata.pk_friend)
	InitTbt(self.tbt_fight2 ,self.settingdata.pk_guild)
	InitTbt(self.tbt_fight3 ,self.settingdata.pk_stranger)
end

local function  SetUserInfo(self)
	Util.GetRoleSnap(DataMgr.Instance.UserData.RoleID,function (data)
		local photoname = data.Options['Photo0']
		if not string.IsNullOrEmpty(photoname) then
			SocialModel.SetHeadIcon(data.ID,photoname,function(UnitImg)
				if not self.root.IsDispose then
					UIUtil.SetImage(self.ib_icon,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
				end
			end)
		else
			UIUtil.SetImage(self.ib_icon, 'static/target/'..data.Pro..'_'..data.Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	end)
	
	
	if DataMgr.Instance.UserData.GuildName =='' then 
		MenuBase.SetLabelText(self.lb_group,Constants.System.NoGuild,0,0)
	else
		MenuBase.SetLabelText(self.lb_group,DataMgr.Instance.UserData.GuildName,0,0)	
	end
 	MenuBase.SetLabelText(self.lb_name,DataMgr.Instance.UserData.Name,0,0)
 	MenuBase.SetLabelText(self.lb_server,DataMgr.Instance.UserData.ServerName,0,0)
 	MenuBase.SetLabelText(self.lb_id,DataMgr.Instance.UserData.DigitID,0,0)
	
	
end

local function TbtClick(self)
	SetTbt(self ,self.tbt_friend,'addfriend','friend_request')
	SetTbt(self ,self.tbt_team,'addteam','enemy_request')
	SetTbt(self ,self.tbt_mm1,'msgfriend','chat_friend')
	SetTbt(self ,self.tbt_mm2,'msgguild','chat_guild')
	SetTbt(self ,self.tbt_mm3,'msgstranger','chat_stranger')
	SetTbt(self ,self.tbt_fight1,'pkfriend','pk_friend')
	SetTbt(self ,self.tbt_fight2,'pkguild','pk_guild')
	SetTbt(self ,self.tbt_fight3,'pkstranger','pk_stranger')
end


local function BtnClick(self)
	self.btn_user.TouchClick=function(sender)
		print('用户中心')
	end

	self.btn_report.TouchClick=function(sender)
		print('系统公告')
	end

	self.btn_share.TouchClick=function(sender)
		print('分享')
	end

	self.btn_ks.TouchClick=function(sender)
		UIUtil.ShowConfirmAlert(Constants.System.ResetPos,'',function() 
			Protocol.RequestHandler.ClientEscapeUnmoveableMapRequest({},function(rsp)
				GlobalHooks.UI.CloseUIByTag('SystemBackGround')
			end)
		end,nil)
	end

	self.InitRelogin.TouchClick=function(sender)
 	 	UIUtil.ShowConfirmAlert(Util.GetText('common_out'),' ',function() 
 	 		--点击立马设置省电模式关，避免该方法存取值出现问题 DataMgr.Instance.SettingData:SaveData()
 	 		DataMgr.Instance.SettingData.bIsSavePower=false 
 	 		self.ui:Close()
 	 		--释放计时器
    		if self.myTimer then
        		LuaTimer.Delete(self.myTimer)
    		end   
			GameSceneMgr.Instance:ExitGame('SystemSetting')
		end,function() end)
 	end
end

local function ChangerHashMap( map ,key ,value )
    for t in Slua.iter(map) do
    	if t.Key == key then
			t.Value = value
		end
    end
end

function _M.OnEnter(self, params)
	debugTimes = 0
	local temp = DataMgr.Instance.UserData.GameOptionsData.Options
	self.settingdata = CSharpMap2Table(temp)
	-- print_r(self.settingdata)
	-- print_r('#####################  OnEnter',self.settingdata['friend_request'],self.settingdata)
 	DataMgr.Instance.SettingData:AttachLuaObserver('PersonHud', self)
 	IntiConfig(self)
 	TbtClick(self)
 	BtnClick(self)
 	SetUserInfo(self)
end

function _M.OnExit( self )
	DataMgr.Instance.SettingData:SaveData()
	DataMgr.Instance.SettingData:DetachLuaObserver('PersonHud')

	local ishava = false
	local temp = {}
	for k1,v1 in pairs(self.changeredData) do
		for k2,v2 in pairs(self.settingdata) do
			if k1 == k2 and v1 ~= v2 then
				temp[k1] = v1
				ishava = true
			end
		end
	end
	if ishava then
		-- print_r('#####################  OnExit',temp)
		local msg = { c2s_options = temp }
		Protocol.RequestHandler.ClientSaveOtherOptionsRequest(msg, function(rsp)
			for k,v in pairs(temp) do
				DataMgr.Instance.UserData:LuaSaveOptionsData(k,v)
			end
			temp = nil
			ishava = false
	    end)
	end
end

function _M.OnInit(self)	

	self.changeredData = {}

	self.InitRelogin = self.ui.comps.btn_relogin
 	
 	self.btn_user = self.ui.comps.btn_user		--用户中心	
 	self.btn_report = self.ui.comps.btn_report	--系统公告
	self.btn_share = self.ui.comps.btn_share 	--分享
	self.btn_ks = self.ui.comps.btn_ks   		--脱离卡死
	
	self.tbt_friend = self.ui.comps.tbt_friend --好友（申请）
	self.tbt_team = self.ui.comps.tbt_team    --组队（申请）
	self.tbt_mm1 = self.ui.comps.tbt_mm1	  --好友 (私信)
	self.tbt_fight1 = self.ui.comps.tbt_fight1  --好友（切磋）
	self.tbt_mm2 = self.ui.comps.tbt_mm2     --仙盟 (私信)
	self.tbt_fight2 = self.ui.comps.tbt_fight2  --仙盟（切磋）
	self.tbt_mm3 = self.ui.comps.tbt_mm3    --陌生人（私信）
	self.tbt_fight3 = self.ui.comps.tbt_fight3  --陌生人（切磋）

	self.ib_icon = self.ui.comps.ib_icon
	self.lb_group = self.ui.comps.lb_group	--门派信息
	self.lb_name = self.ui.comps.lb_name	--玩家姓名
	self.lb_server = self.ui.comps.lb_server	--服务器信息
	self.lb_id = self.ui.comps.lb_id	--玩家id
	self.lb_name1 = self.ui.comps.lb_name1	--玩家姓名
	self.lb_name1.Enable = true
	self.lb_name1.IsInteractive = true
	self.lb_name1.event_PointerClick = function()
		debugTimes = debugTimes + 1
		local presstime = TLUnityDebug.DEBUG_MODE and 1 or 10
		if debugTimes == presstime then
			TLUnityDebug.SetDebug(not TLUnityDebug.DEBUG_MODE)
			debugTimes = 0
		end
	end
	

	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.menu.ShowType = UIShowType.Cover
end


return _M