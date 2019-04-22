local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local PagodaModel = require 'Model/PagodaModel'
local ActivityModel = require 'Model/ActivityModel'
local TimeUtil = require 'Logic/TimeUtil'
local DisplayUtil = require 'Logic/DisplayUtil'
local ItemModel=require 'Model/ItemModel'

local CheckedIndex = nil
local mode = 1
local opened = false
local bugkey = nil
local uiself = nil

local function LoadEffect(self,pan,parent,filename,pos,scale)
	local param = 
				{
					DisableToUnload = true,
					Pos = pos,
					Parent = parent.UnityObject.transform,
					Clip = pan.Transform,
					LayerOrder = self.ui.menu.MenuOrder,
					Scale = scale,
					UILayer = true,
				}
	
	return Util.PlayEffect(filename,param)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

--右侧掉落预览
local function SetPreview(self,node,params)

	local itemData = ActivityModel.GetItemData(params)
	local itemdetail=ItemModel.GetDetailByTemplateID(params)
    
	UIUtil.SetItemShowTo(node,itemdetail,1)
	node.TouchClick = function( sender )
		UIUtil.ShowTips(self,sender,itemData.id)
	end
end


--右侧UI
local function SetRightUI(self,index)

	self.lb_name.Text = Util.GetText(self.maindata[mode][index].floor_name)
	self.lb_name2.Text = Util.GetText(self.maindata[mode][index].floor_name)..':'
	
	--图标显示
	-- local itemShow = ItemShow.Create(self.maindata[index].boss_icon, 0, 0)
	-- itemShow.Size2D = self.ib_boss.Size2D
	-- itemShow.EnableTouch = false
	-- self.ib_boss:AddChild(itemShow)
	UIUtil.SetImage(self.ib_boss,self.maindata[mode][index].boss_icon,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
	self.tb_explain.Text = Util.GetText(self.maindata[mode][index].map_desc)
	self.lb_neednum.Text = self.maindata[mode][index].power

	--自身修为显示
	local  nownum = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.FIGHTPOWER)
	self.lb_nownum.Text = nownum
	if self.maindata[mode][index].power > nownum then
		self.lb_nownum.FontColor = GameUtil.RGB2Color(Constants.Color.Red)
	else
		self.lb_nownum.FontColor = GameUtil.RGB2Color(Constants.Color.Green)
	end

	-- --关卡奖励信息
	-- self.ib_show
	-- self.sp_reward
	-- self.cvs_item

	self.cvs_item.Visible = false
	UIUtil.ConfigHScrollPan(self.sp_reward,self.cvs_item, #self.maindata[mode][index].preview.id, function(node, index1)
		SetPreview(self,node,self.maindata[mode][index].preview.id[index1])
	end)

	--关卡通关时间
	if self.pagodadata.alonePassTime[self.maindata[mode][index].infloor] then
		self.lb_own.Visible = true
		self.lb_own.Text = TimeUtil.SecToTimeformat(self.pagodadata.alonePassTime[self.maindata[mode][index].infloor])
	else
		self.lb_own.Visible = false
	end
	self.lb_othername.Text = self.pagodadata.serverPassData[self.maindata[mode][index].infloor].name and Util.GetText(self.pagodadata.serverPassData[self.maindata[mode][index].infloor].name) or Util.GetText(Constants.PagodaMain.Nofirst)

	if self.pagodadata.serverPassData[self.maindata[mode][index].infloor].sec then
		self.lb_other.Visible = true
		self.lb_other.Text = TimeUtil.SecToTimeformat(self.pagodadata.serverPassData[self.maindata[mode][index].infloor].sec)
	else
		self.lb_other.Visible = false
	end
end


--EF_UI_Partner_Activation(Clone)
local function InitData(self,node,index)
	local ib_steppass = node:FindChildByEditName('ib_steppass', true)
	local ib_head = node:FindChildByEditName('ib_head', true)
	local ib_stepreward = node:FindChildByEditName('ib_stepreward', true)
	-- local lb_stepreward = node:FindChildByEditName('lb_stepreward', true)
	local lb_stepname = node:FindChildByEditName('lb_stepname', true)
	local cvs_player = node:FindChildByEditName('cvs_player', true)
	local cvs_stepreward = node:FindChildByEditName('cvs_stepreward', true)
	lb_stepname.Text = Util.GetText(self.maindata[mode][index].floor_name)

	local cvs_pic = node:FindChildByEditName('cvs_pic', true)
	local cvs_stepinfo = node:FindChildByEditName('cvs_stepinfo', true)
	local cvs_steprewardparent = node:FindChildByEditName('cvs_steprewardparent', true)
	local ib_select = node:FindChildByEditName('ib_select', true)

	
	ib_select.Visible = CheckedIndex == index

	--设置位置(右侧)
	if index%2 == 0 then
		node.X = self.rightX
		cvs_pic.X = self.nodewidth - self.cvs[1]
		cvs_stepinfo.X = self.nodewidth - self.cvs[2] - cvs_stepinfo.Size2D.x
		cvs_steprewardparent.X = self.nodewidth - self.cvs[3] - cvs_steprewardparent.Size2D.x
		cvs_pic.Scale = Vector2(-1,1)
	else--(左侧)
		cvs_pic.X = self.cvs[1]
		cvs_stepinfo.X = self.cvs[2]
		cvs_steprewardparent.X = self.cvs[3]
		cvs_pic.Scale = Vector2(1,1)
	end

	--设置掉落详情
	local itemdetail=ItemModel.GetDetailByTemplateID(self.maindata[mode][index].first_reward)
   	local num =self.maindata[mode][index].first_reward_num
	UIUtil.SetItemShowTo(cvs_stepreward,itemdetail,num)

	if cvs_stepreward.UserTag == 0 and self.pagodadata.giftData[self.maindata[mode][index].infloor] == 1 then
		cvs_stepreward.UserTag = LoadEffect(
							self,
							self.sp_tower,
							cvs_stepreward,
							"/res/effect/ui/ef_ui_frame_01.assetbundles",
							Vector3(cvs_stepreward.Size2D[1]/2,-cvs_stepreward.Size2D[2]/2+1,0),
							Vector3(0.36,0.89,1))
		self.effc[cvs_stepreward.UserTag] = cvs_stepreward.UserTag
	elseif cvs_stepreward.UserTag ~= 0 and self.pagodadata.giftData[self.maindata[mode][index].infloor] ~= 1 then
		UnLoadEffect(cvs_stepreward.UserTag)
		self.effc[cvs_stepreward.UserTag] = nil
		cvs_stepreward.UserTag = 0
	end

	--显示奖励是否已被领取(0-未达到，1-待领取，2-已领取)
	if self.pagodadata.giftData[index] == 0 then
		ib_stepreward.Visible = false
 		
	elseif self.pagodadata.giftData[index] == 1 then
		ib_stepreward.Visible = false

	elseif self.pagodadata.giftData[index] == 2 then
		ib_stepreward.Visible = true
	end

	if self.pagodadata.curLayer == 0 then
		ib_steppass.Visible = true
	else
		if index < self.pagodadata.curLayer then
			ib_steppass.Visible = true
		else
			ib_steppass.Visible = false
		end
	end

	--设置玩家头像
	if index == self.pagodadata.curLayer then
		cvs_player.Visible = true
		ib_head.Visible = true
		local userdata = {DataMgr.Instance.UserData.Pro,DataMgr.Instance.UserData.Gender}
		UIUtil.SetImage(cvs_player, 'static/target/'..userdata[1]..'_'..userdata[2]..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
	else
		-- UIUtil.SetImage(cvs_player, self.maindata[index].boss_icon)
		cvs_player.Visible = false
		ib_head.Visible = false
	end

	--添加点击事件，显示奖励物品tips
	cvs_stepreward.TouchClick = function(sender)
		if self.pagodadata.giftData[self.maindata[mode][index].infloor] == 1 then
			-- TODO:领取首通奖励
			PagodaModel.GetSuccessReward(self.maindata[mode][index].floor,function()
				if sender.UserTag ~= 0 then
					UnLoadEffect(sender.UserTag)
					self.effc[sender.UserTag] = nil
					sender.UserTag = 0
				end
				self.pagodadata.giftData[self.maindata[mode][index].infloor] = 2
				ib_stepreward.Visible = true
			end)
		else
       		UIUtil.ShowTips(self,sender,self.maindata[mode][index].first_reward )
		end
	end

	--设置右侧UI显示
	cvs_stepinfo.TouchClick = function( sender )
		CheckedIndex = index
		self.sp_tower:RefreshShowCell()
		SetRightUI(self,index)
	end
	
end

local function SetBtn(self)
	if self.pagodadata.curLayer == 0 then
		self.btn_start.IsGray = true
		self.btn_start.Enable = false
	else
		self.btn_start.IsGray = false
		self.btn_start.Enable = true
	end
	self.lb_return.Text = self.pagodadata.resetTime..'/'..self.pagodadata.maxResetTime
	if self.pagodadata.resetTime == 0 then
		self.btn_return.IsGray = true
		self.btn_return.Enable = false
	else
		self.btn_return.IsGray = false
		self.btn_return.Enable = true
	end
end

local function StartSetUI(self)
	SetBtn(self)

	self.cvs_step.Visible = false

	if self.pagodadata.curLayer == 0 then
		self.pagodadata.curLayer = 10
	end
	CheckedIndex = self.pagodadata.curLayer
	
	UIUtil.ConfigVScrollPan(self.sp_tower,self.cvs_step, #self.maindata[mode], function(node, index)
		InitData(self,node,#self.maindata[mode]-index+1)		
	end)
	
	
	DisplayUtil.lookAt(self.sp_tower,#self.maindata[mode]-CheckedIndex)
	SetRightUI(self,CheckedIndex == 0 and 1 or CheckedIndex)
end 

local function InitDifficult(self,node,index)
	local lb_towername = node:FindChildByEditName('lb_towername', true)
	local lb_levelnum = node:FindChildByEditName('lb_levelnum', true)
	local lb_reqlevel = node:FindChildByEditName('lb_reqlevel', true)
	local btn_mop = node:FindChildByEditName('btn_mop', true)
	local ib_backg = node:FindChildByEditName('ib_backg', true)
	local ib_clear = node:FindChildByEditName('ib_clear', true)
	local lb_lock = node:FindChildByEditName('lb_lock', true)
	local lb_go = node:FindChildByEditName('lb_go', true)

	lb_lock.Visible = index == 1 and false or self.passeddata[index-1] == false
	ib_clear.Visible = self.passeddata[index]
	self.btn_start.Visible = false

	lb_towername.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
	lb_towername.Text = Util.GetText(self.difficultdata[index].dif_desc)

	if self.curlevel>= self.difficultdata[index].recom_level then
		lb_go.Visible = true
		lb_reqlevel.Visible = false
		lb_levelnum.Visible = false
	else
		lb_levelnum.Text = self.difficultdata[index].recom_level
		lb_go.Visible = false
		lb_reqlevel.Visible = true
		lb_levelnum.Visible = true
	end
	
	UIUtil.SetImage(ib_backg,self.difficultdata[index].dif_icon,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
	
	
	--显示主界面
	node.TouchClick = function(sender)
		if self.curlevel >= self.difficultdata[index].recom_level and lb_lock.Visible == false then
			PagodaModel.RequirePagodaData(self.difficultdata[index].dif_level,function(rsp)
				mode = self.difficultdata[index].dif_level
				self.cvs_specific.Visible = true
				self.cvs_diftower.Visible = false
				self.pagodadata = rsp
				StartSetUI(self)
				self.btn_start.Visible = true
				self.btn_sweep.Visible = true
				opened = true
				self.btn_count.Visible = false
				self.btn_return.Visible = false
				self.lb_return.Visible = false
			end)
		else
			if lb_lock.Visible == false then
				GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PagodaMain.LevelNotEnoughTips))
			else
				GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PagodaMain.LockTips))
			end
		end
	end
end

function _M.OnEnter(self , uiTag,storyid)
	uiself = self
	mode = 1
	self.curlevel = DataMgr.Instance.UserData.Level
	self.cvs_specific.Visible = false
	self.cvs_diftower.Visible = true
	self.btn_sweep.Visible = false
	self.difficultdata = PagodaModel.GetPagodaDifficult()
	PagodaModel.RequireDiffcultData(function (rsp)
		self.passeddata = rsp
		self.btn_count.Visible = true
		self.btn_return.Visible = true
		self.lb_return.Visible = true
		UIUtil.ConfigHScrollPan(self.sp_diflist,self.cvs_diflevel, #self.difficultdata, function(node, index)
			InitDifficult(self,node,index)
		end)
		PagodaModel.RequirePagodaData(mode,function(rsp)
			self.pagodadata = rsp
			SetBtn(self)
		end)
	end)
	

	self.effc = {}
	self.maindata = PagodaModel.GetPagodaData()
	
	--PagodaModel.RequirePagodaData(1,function(rsp)
	--	self.pagodadata = rsp
		--StartSetUI(self)
		if uiTag and type(uiTag) ~= "number" then
			GlobalHooks.UI.OpenUI(uiTag, 0,storyid)
		end
	--end)

	--进入镇妖塔
	self.btn_start.TouchClick = function( sender )
		local extdata = {}
		extdata['mode'] = mode
		FunctionUtil.OpenFunction('gotopagoda',true,{arg = extdata})
	end

	self.btn_story.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI('PagodaStory', 0)
	end

	self.btn_chest.TouchClick = function( sender )
		GlobalHooks.UI.OpenUI('PagodaChest', 0)
	end

	self.btn_return.TouchClick = function( sender )
		PagodaModel.SetEnterCount(function()
			PagodaModel.RequirePagodaData(mode,function(rsp)
				self.pagodadata = rsp
				if opened then
					StartSetUI(self)
				else
					SetBtn(self)
				end
				self.btn_start.IsGray = false
				self.btn_start.Enable = true
			end)
		end)
	end
	
	self.ib_back.TouchClick = function(sender)
		self.cvs_specific.Visible = false
		self.cvs_diftower.Visible = true
		self.btn_start.Visible = false
		self.btn_sweep.Visible = false
		self.btn_count.Visible = true
		opened = false
		self.btn_return.Visible = true
		self.lb_return.Visible = true
	end

	self.btn_sweep.TouchClick = function(sender)
		if GlobalHooks.IsFuncOpen('tower_mop_up',true) then
			PagodaModel.SweepTower(mode,function(rsp)
				self.pagodadata.curLayer = rsp.c2s_curLayer
				if self.pagodadata.curLayer == 0 then
					self.pagodadata.curLayer = 10
				end
				self.sp_tower:RefreshShowCell()
				DisplayUtil.lookAt(self.sp_tower,#self.maindata[mode]-self.pagodadata.curLayer)
			end)
		end
	end
	if not bugkey then
		local resettime = GameUtil.GetStringGameConfig("pagoda_buy_key")
		bugkey = resettime
	end
	
	self.btn_count.TouchClick  = function(sender)
		FunctionUtil.CanBuyTicketsRequest(bugkey)
	end
end

local function UpdateCount(eventname,params)
	if params.functionid == "tower_reset" and uiself then
		uiself.pagodadata.resetTime = uiself.pagodadata.resetTime + 1
		uiself.lb_return.Text = uiself.pagodadata.resetTime..'/'..uiself.pagodadata.maxResetTime
		uiself.btn_return.IsGray = false
		uiself.btn_return.Enable = true
	end
end

function _M.OnExit( self )
	EventManager.Unsubscribe("Event.TicketChange", UpdateCount)
	for i, v in pairs(self.effc) do
		if v then
			UnLoadEffect(v)
			v = nil
		end
	end
	self.effc = nil
	mode = nil
end

function _M.OnInit(self)
	self.cvs_specific = self.root:FindChildByEditName('cvs_specific', true)
	self.cvs_diftower = self.root:FindChildByEditName('cvs_diftower', true)


	self.sp_tower = UIUtil.FindChild(self.ui.comps.cvs_towinfo, 'sp_tower', true)
	self.cvs_step = UIUtil.FindChild(self.ui.comps.cvs_towinfo, 'cvs_step', true)
	self.ib_back = self.ui.comps.cvs_towinfo:FindChildByEditName('ib_back', true)
	self.ib_back.Enable = true
	self.ib_back.IsInteractive = true

	self.rightX = self.sp_tower.Size2D.x - self.cvs_step.Size2D.x
	self.cvs = {}
	self.nodewidth = self.cvs_step.Size2D.x

	self.cvs[1] = UIUtil.FindChild(self.cvs_step, 'cvs_pic', true).X + 10
	self.cvs[2] = UIUtil.FindChild(self.cvs_step, 'cvs_stepinfo', true).X + 10
	self.cvs[3] = UIUtil.FindChild(self.cvs_step, 'cvs_steprewardparent', true).X + 10

	--关卡信息
	self.lb_name = UIUtil.FindChild(self.ui.comps.cvs_floor, 'lb_name', true)
	self.ib_boss = UIUtil.FindChild(self.ui.comps.cvs_floor, 'ib_boss', true)
	self.lb_name2 = UIUtil.FindChild(self.ui.comps.cvs_floor, 'lb_name2', true)
	self.tb_explain = UIUtil.FindChild(self.ui.comps.cvs_floor, 'tb_explain', true)
	self.lb_neednum = UIUtil.FindChild(self.ui.comps.cvs_floor, 'lb_neednum', true)
	self.lb_nownum = UIUtil.FindChild(self.ui.comps.cvs_floor, 'lb_nownum', true)

	--关卡奖励信息
	self.ib_show = UIUtil.FindChild(self.ui.comps.cvs_reward, 'ib_show', true)
	self.sp_reward = UIUtil.FindChild(self.ui.comps.cvs_reward, 'sp_reward', true)
	self.cvs_item = UIUtil.FindChild(self.ui.comps.cvs_reward, 'cvs_item', true)

	--关卡通关时间
	self.lb_own = UIUtil.FindChild(self.ui.comps.cvs_time, 'lb_own', true)
	self.lb_othername = UIUtil.FindChild(self.ui.comps.cvs_time, 'lb_othername', true)
	self.lb_other = UIUtil.FindChild(self.ui.comps.cvs_time, 'lb_other', true)

	--其他信息
	self.btn_sweep = UIUtil.FindChild(self.ui.comps.cvs_other, 'btn_sweep', true)
	self.btn_story = UIUtil.FindChild(self.ui.comps.cvs_other, 'btn_story', true)
	self.btn_count = UIUtil.FindChild(self.ui.comps.cvs_other, 'btn_count', true)
	self.btn_chest = UIUtil.FindChild(self.ui.comps.cvs_other, 'btn_chest', true)
	self.btn_return = UIUtil.FindChild(self.ui.comps.cvs_other, 'btn_return', true)
	self.lb_return = UIUtil.FindChild(self.ui.comps.cvs_other, 'lb_return', true)
	self.btn_start = UIUtil.FindChild(self.ui.comps.cvs_other, 'btn_start', true)

	--难度选择
	self.cvs_diflevel = self.cvs_diftower:FindChildByEditName('cvs_diflevel', true)
	self.sp_diflist = self.cvs_diftower:FindChildByEditName('sp_diflist', true)
	self.cvs_diflevel.Visible = false

	EventManager.Subscribe("Event.TicketChange", UpdateCount)
end


return _M