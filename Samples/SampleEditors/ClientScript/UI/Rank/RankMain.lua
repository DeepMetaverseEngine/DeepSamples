local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local RankModel = require 'Model/RankModel'
local GuildModel = require 'Model/GuildModel'




local leftCheckedIndex = 1
local topCheckedIndex = 1
local centerCheckIndex
local userinfo = DataMgr.Instance.UserData


--填充排行榜玩家列表信息
local function InitRankListData(self,node,index,datatype)
	local lb_r1 =  node:FindChildByEditName('lb_r1', true)
	local lb_r2 =  node:FindChildByEditName('lb_r2', true)
	local lb_r3 =  node:FindChildByEditName('lb_r3', true)
	local lb_r4 =  node:FindChildByEditName('lb_r4', true)
	local lb_r5 =  node:FindChildByEditName('lb_r5', true)
	local ib_select =  node:FindChildByEditName('ib_select', true)


	ib_select.Visible = centerCheckIndex == index
	lb_r1.Text = index
	if datatype == 0 then
		lb_r2.Text = self.roledata[index].Name
		lb_r3.Text = Constants.ProName[self.roledata[index].Pro]
		lb_r4.Visible = false
		if self.data.val[4] == 'practice' then
			lb_r4.Text = self.roledata[index].PracticeLv == 0 and '' or GameUtil.GetPracticeName(self.roledata[index].PracticeLv, 0)
			lb_r4.Visible = true
		elseif self.data.val[4] == 'backlevel' then
			lb_r4.Text = self.roledata[index].GuildName
		elseif self.data.val[4] == 'godlevel' then
			lb_r4.Text = self.roledata[index].GuildName
		elseif self.data.val[4] == 'petlevel' then
			lb_r4.Text = self.roledata[index].GuildName
		elseif self.data.val[4] == 'mountlevel' then
			lb_r4.Text = self.roledata[index].GuildName
		else
			lb_r4.Text = self.roledata[index].GuildName
			lb_r4.Visible = true
		end
	elseif datatype == 1 then
		lb_r2.Text = self.roledata[index].name
		lb_r3.Text = self.roledata[index].memberNum
		lb_r4.Text = self.roledata[index].level
	end
	lb_r5.Text = self.listdetail[index].value

	local ib_flag1 =  node:FindChildByEditName('ib_r1', true)
	local ib_flag2 =  node:FindChildByEditName('ib_r2', true)
	local ib_flag3 =  node:FindChildByEditName('ib_r3', true)
	ib_flag1.Visible = false
	ib_flag2.Visible = false
	ib_flag3.Visible = false
	if index == 1 then
		ib_flag1.Visible = true
	end
	if index == 2 then
		ib_flag2.Visible = true
	end
	if index == 3 then
		ib_flag3.Visible = true
	end


	--好友交互弹窗
	node.TouchClick = function(sender)
	SoundManager.Instance:PlaySoundByKey('button',false)
		ib_select.Visible = true
		centerCheckIndex = index
		self.center_pan:RefreshShowCell()
		local args = {}
		if datatype == 0 then
			args.playerId = self.listdetail[index].id
			if args.playerId ~= DataMgr.Instance.UserData.RoleID then
				args.playerName = self.roledata[index].Name
				args.menuKey = 'stranger'
				EventManager.Fire("Event.InteractiveMenu.Show", args)
			end
		--elseif  datatype == 1 then
		--	args.playerId = self.listdetail[index].id
		--	if args.playerId == userinfo.GuildId then
		--		args.playerName = self.roledata[index].Name
		--		args.menuKey = 'guild_self'
		--		EventManager.Fire("Event.InteractiveMenu.Show", args)
		--	end
		end
	end
end

--设置自己的排行信息
local function SetOtherInfo(self,datatype)
	self.data = RankModel.GetGroup(self.grouplist[leftCheckedIndex].id,self.grouplist[leftCheckedIndex].childList[topCheckedIndex].sub_id)
	for i=1,#self.lb_s do
		self.lb_s[i].Text = Util.GetText(self.data.key[i])
	end

	-- self.lb_player.Text = Util.GetText(self.data.key[1])
	

	local myinfo = datatype == 0 and userinfo.RoleID or userinfo.GuildId
	self.lb_prank.Text = Constants.RankList.RankName
	self.lb_pnum.Text = 0
	if self.listdetail ~= nil then
		local ishavarank = false
		for i=1,#self.listdetail do
			if self.listdetail[i].id == myinfo then
				self.lb_prank.Text = i
				self.lb_pnum.Text = self.listdetail[i].value
				ishavarank = true
			end
		end
	end

	self.lb_pinfo.Visible = false
	self.lb_pnum.Visible = false
	
	--self.lb_pinfo.Text = Util.GetText(self.data.key[5])
	--if datatype == 0 then
	--	if string.sub(self.data.val[5],1,6) == 'plevel' then
	--		self.lb_pnum.Text = userinfo.FightPower
	--	elseif self.data.val[5] == 'dresspoint' then
	--		self.lb_pnum.Text = userinfo:GetAttribute(UserData.NotiFyStatus.ACCUMULATIVECOUNT)
	--	elseif self.data.val[5] == 'money' then
	--		self.lb_pnum.Text = userinfo:GetAttribute(UserData.NotiFyStatus.SILVER)
	--	elseif self.data.val[5] == 'coin' then
	--		self.lb_pnum.Text = userinfo:GetAttribute(UserData.NotiFyStatus.DIAMOND)
	--	elseif self.data.val[5] == 'finishfloor' then
	--		--TODO 镇妖塔完成层数  self.lb_pnum.Text = userinfo:GetAttribute(UserData.NotiFyStatus.DIAMOND)
	--	--else
	--	--	self.lb_pnum.Text = self.roledata[index].GuildName
	--	end
	--	
	--	self.lb_pnum.Text = myinfo.Name
	--	
	--else
	--	GuildModel.SnapReader:GetMany({userinfo.GuildId},function(snaps)
	--		self.lb_pnum.Text = snaps.fightPower
	--	end)
	--end
end


local function RequestUserData(self,datatype,cb)
	
	local actor = TLBattleScene.Instance.Actor
	if not actor then
		return
    end

	local allRole = {}
	for i=1,#self.listdetail do
		table.insert(allRole,self.listdetail[i].id)
	end
	if datatype == 0 then
		DataMgr.Instance.UserData.RoleSnapReader:GetMany(allRole, function(snaps)
			if cb then
				local ret = CSharpArray2Table(snaps)
				cb(ret)
			end
		end)
	elseif datatype == 1 then
		GuildModel.SnapReader:GetMany(allRole,function(snaps)
			if cb then
				cb(snaps)
			end
		end)
	end
end

--使用列表所有uuid获取玩家其他信息
local function GetAllUserData( self,datatype )
	RequestUserData(self,datatype,function(roleDatas)
		self.roledata = {}
		for k,v in ipairs(roleDatas) do
			if v == false then
				table.remove(self.listdetail,k)
			else
				table.insert(self.roledata, v)
			end
		end
		SetOtherInfo(self,datatype)
		UIUtil.ConfigVScrollPan(self.center_pan,self.center_list, #self.listdetail, function(node, index1)
			InitRankListData(self,node,index1,datatype)
		end)
	end)
end


--初始化子页标签
local function InitTopData(self,node,index)
	local tbt_stype =  node:FindChildByEditName('tbt_stype', true)
	tbt_stype.Text = Util.GetText(self.grouplist[leftCheckedIndex].childList[index].name)
	tbt_stype.IsChecked = topCheckedIndex == index
	if tbt_stype.IsChecked then
		RankModel.GetRankListDetail(self.grouplist[leftCheckedIndex].id,self.grouplist[leftCheckedIndex].childList[index].sub_id,function(rsp)
			self.listdetail = rsp.s2c_list
			if self.listdetail ~= nil then
				GetAllUserData(self,self.grouplist[leftCheckedIndex].childList[topCheckedIndex].source_type)
			else
				UIUtil.ConfigVScrollPan(self.center_pan,self.center_list, 0, function(node, index1) end)
			end
		end)
	end
	tbt_stype.TouchClick = function(sender)
		if topCheckedIndex ~= index then
			topCheckedIndex = index
			self.top_pan:RefreshShowCell()
		end
		sender.IsChecked = true
	end
end

--初始化主页标签
local function InitLeftData(self,node,index)
	local tbt_type = node:FindChildByEditName('tbt_type', true)

	tbt_type.Text = Util.GetText(self.grouplist[index].name)

	tbt_type.IsChecked = leftCheckedIndex == index
	if tbt_type.IsChecked then
		UIUtil.ConfigHScrollPan(self.top_pan,self.top_list, #self.grouplist[leftCheckedIndex].childList, function(node, index1)
			InitTopData(self,node,index1)
		end)
	end
	tbt_type.TouchClick = function(sender)
		sender.IsChecked = true
		if leftCheckedIndex ~= index then
			leftCheckedIndex = index
			topCheckedIndex = 1
			self.left_pan:RefreshShowCell()
		end
	end
end


function _M.OnEnter( self ,params)
	

	RankModel.FormRanklist(function(rsp)
		self.grouplist = rsp.s2c_data
		UIUtil.ConfigVScrollPan(self.left_pan,self.left_list, #self.grouplist, function(node, index)
				InitLeftData(self,node,index)
			end)
	end)


	self.top_list.Visible = false  
	self.left_list.Visible = false 
	self.center_list.Visible = false

	self.ui.comps.btn_close.TouchClick = function(sender )
		self.ui:Close()
	end

end


function _M.OnExit( self )

end


function _M.OnInit(self)
	self.center_pan = self.ui.comps.sp_ranklist
	self.center_list = self.ui.comps.cvs_ranklist

	self.left_pan = self.ui.comps.sp_type
	self.left_list = self.ui.comps.cvs_info

	self.top_pan = self.ui.comps.sp_stype
	self.top_list = self.ui.comps.cvs_info1

	self.lb_s = {}
	self.lb_s[1] = self.ui.comps.lb_s1
	self.lb_s[2] = self.ui.comps.lb_s2
	self.lb_s[3] = self.ui.comps.lb_s3
	self.lb_s[4] = self.ui.comps.lb_s4
	self.lb_s[5] = self.ui.comps.lb_s5

	self.lb_player = self.ui.comps.lb_player
	self.lb_prank = self.ui.comps.lb_prank

	self.lb_pinfo = self.ui.comps.lb_pinfo
	self.lb_pnum = self.ui.comps.lb_pnum



	HudManager.Instance:InitAnchorWithNode(self.ui.root, bit.bor(HudManager.HUD_CENTER))

	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.menu.ShowType = UIShowType.HideHudAndMenu
end

return _M