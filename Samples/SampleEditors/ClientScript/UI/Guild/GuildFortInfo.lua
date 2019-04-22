local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

local function ShowFieldDetail( self, data )
    self.ui.comps.cvs_choosefield.Visible = true
	self.ui.comps.lb_leftnum.Text = '('..Util.GetText('guild_carriage_pcount', tostring(data.firstCount))..')'
	self.ui.comps.lb_rightnum.Text = '('..Util.GetText('guild_carriage_pcount', tostring(data.secondCount))..')'
end

local function RefreshListCellData( self, node, index )
	local data = self.guildList[index]
	MenuBase.SetLabelText(node, 'lb_rank', tostring(data.rank + 1), 0, 0)
	MenuBase.SetLabelText(node, 'lb_guild', data.name, 0, 0)
	MenuBase.SetLabelText(node, 'lb_power', tostring(data.fightPower), 0, 0)
end

local function InitRankList( self, selfRank, rankList )
	local pan = self.ui.comps.sp_ranklist
	local cell = self.ui.comps.cvs_guildinfo
	UIUtil.ConfigVScrollPan(pan, cell, #self.guildList, function(node, index)
		RefreshListCellData(self, node, index)
	end)
	if string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) or rankList[DataMgr.Instance.UserData.GuildId] == nil then
		self.ui.comps.lb_yourguild.Text = Util.GetText('guild_fort_noright')
	else
		self.ui.comps.lb_yourguild.Text = Util.GetText('guild_fort_rank', selfRank + 1)
	end
end

local function ShowFortDetail( self, db )
	self.ui.comps.cvs_pointinfo.Visible = true
	self.ui.comps.lb_choogsename.Text = Util.GetText(db.fort_name)
	self.ui.comps.tb_choosedesc.Text = Util.GetText(db.fort_des)
	UIUtil.SetImage(self.ui.comps.ib_chooseicon, '$dynamic/TL_guildfortfied/output/TL_guildfortfied.xml|TL_guildfortfied|point_'..db.id)

	local pan = self.ui.comps.sp_exchangelist
	local cell = self.ui.comps.cvs_reditem
	cell.Visible = false
	UIUtil.ConfigHScrollPan(pan, cell, #db.reward.id, function(node, index)
		local itemId = db.reward.id[index]
		if itemId > 0 then
			node.Visible = true
			local itshow = UIUtil.SetItemShowTo(node, itemId, db.reward.num[index])
		    itshow.EnableTouch = true
		    itshow.TouchClick = function()
		    	UIUtil.ShowNormalItemDetail({templateID = itemId, autoHeight = true, itemShow = itshow, anchor='c_b', x= 560,y=390})
		    end
		else
			node.Visible = false
		end
	end)
end

local function ShowConfirm( self, fortId )
	local dbfort = GlobalHooks.DB.FindFirst('guild_fort', { id = fortId })
	self.ui.comps.lb_confirmname.Text = Util.GetText(dbfort.fort_name)
	self.ui.comps.lb_costnum.Text = tostring(dbfort.fort_cost)
	self.ui.comps.cvs_confirm.Visible = true
	self.ui.comps.bt_yes.TouchClick = function( ... )
		GuildModel.ClientGuildFortSignUpRequest(dbfort.id, function( rsp )
			_M.RefreshFortInfo(self)
		end)
		self.ui.comps.cvs_confirm.Visible = false
	end
	self.ui.comps.bt_no.TouchClick = function( ... )
		self.ui.comps.cvs_confirm.Visible = false
	end
end

local function InitFortInfo( self, fortList, selfFort )
	local dbfort = GlobalHooks.DB.Find('guild_fort', {})
	for i = 1, #dbfort do
		local db = dbfort[i]
		local data = fortList[db.id]
		local cvs = self.ui.comps['cvs_point'..i]
		MenuBase.SetLabelText(cvs, 'lb_pointname'..i, Util.GetText(db.fort_name), 0, 0)
		local qulityImg = cvs:FindChildByEditName('lb_quality'..i, true)
		UIUtil.SetImage(qulityImg, '$dynamic/TL_guildfortfied/output/TL_guildfortfied.xml|TL_guildfortfied|lv_'..db.fort_type)
		local iconBtn = cvs:FindChildByEditName('btn_pointicon'..i, true)
		UIUtil.SetImage(iconBtn, '$dynamic/TL_guildfortfied/output/TL_guildfortfied.xml|TL_guildfortfied|point_'..db.id)
		iconBtn.TouchClick = function( ... )
			ShowFortDetail(self, db)
		end
		local cvs_point = cvs:FindChildByEditName('cvs_signguild', true)
		if not string.IsNullOrEmpty(data.holdGuildName) then --有占领仙盟就显示占领仙盟
			cvs_point.Visible = false
			self.ui.comps['lb_occupyguild'..i].Visible = true
			self.ui.comps['lb_occupyguild'..i].Text = data.holdGuildName
		else --没有占领仙盟
			if FunctionUtil.CheckNowIsOpen('guildfort_open', false) or FunctionUtil.CheckNowIsOpen('guildfort_in', false) then --在活动时间，就显示报名信息
				cvs_point.Visible = true
				self.ui.comps['lb_occupyguild'..i].Visible = false
				for j = 1, 2 do
					local lb_sign = cvs_point:FindChildByEditName('lb_signguild'..j, true)
					local lb_rank = cvs_point:FindChildByEditName('lb_rank'..j, true)
					local pData = data.fortPositionList[j]
					lb_rank.Visible = pData ~= nil and true or false
					if pData then
						lb_sign.Text = pData.guildName
						lb_rank.Text = tostring(pData.guildRank + 1)
					else
						lb_sign.Text = Util.GetText('common_none')
					end
				end
			else --不在活动时间，就显示未占领
				cvs_point.Visible = false
				self.ui.comps['lb_occupyguild'..i].Visible = true
				self.ui.comps['lb_occupyguild'..i].Text = Util.GetText('guild_fort_nohold')
			end
		end

		--报名
		local signBtn = self.ui.comps['btn_signup'..i]
		if FunctionUtil.CheckNowIsOpen('guildfort_open', false) and selfFort == 0 then
			local dbPosition = GlobalHooks.DB.FindFirst('guild_position', { position_id = DataMgr.Instance.GuildData.Position })
			signBtn.Visible = dbPosition.fort_sign == 1
			signBtn.TouchClick = function( ... )
				ShowConfirm(self, db.id)
			end
		else
			signBtn.Visible = false
		end
	end
end

function _M.RefreshFortInfo( self )
    GuildModel.ClientGuildFortInfoRequest(function( rsp )
    	-- rank list
		local guildList = {}
		for k, v in pairs(rsp.s2c_rankList) do
			table.insert(guildList, v)
		end
		table.sort(guildList, function( a, b )
			return a.rank < b.rank
		end)
		self.guildList = guildList
		InitRankList(self, rsp.s2c_selfRank, rsp.s2c_rankList)

		--fort
		InitFortInfo(self, rsp.s2c_fortList, rsp.s2c_selfFort)

		--down info
		if string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) or rsp.s2c_rankList[DataMgr.Instance.UserData.GuildId] == nil then
			self.ui.comps.lb_downranknum.Text = Util.GetText('guild_fort_noright')
		else
			self.ui.comps.lb_downranknum.Text = Util.GetText('guild_fort_rank', rsp.s2c_selfRank + 1)
		end
		local db = GlobalHooks.DB.FindFirst('guild_fort', { id = rsp.s2c_selfFort })
		if db then
			self.ui.comps.lb_signedpoint.Text = Util.GetText(db.fort_name)
			if FunctionUtil.CheckNowIsOpen('guildfort_in', false) then
				local fortData = rsp.s2c_fortList[rsp.s2c_selfFort]
				self.ui.comps.btn_go.Visible = string.IsNullOrEmpty(fortData.holdGuildId)
			else
				self.ui.comps.btn_go.Visible = false
			end
		else
			self.ui.comps.lb_signedpoint.Text = Util.GetText('guild_fort_nosign')
			self.ui.comps.btn_go.Visible = false
		end
    end)
end

function _M.OnEnter( self )
    self.ui.comps.cvs_choosefield.Visible = false
	self.ui.comps.cvs_pointinfo.Visible = false
	self.ui.comps.cvs_confirm.Visible = false
	self.ui.comps.btn_go.Visible = FunctionUtil.CheckNowIsOpen('guildfort_in', false)
	self.ui.comps.btn_guildrank.Visible = FunctionUtil.CheckNowIsOpen('guildfort_open', false)
	_M.RefreshFortInfo(self)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
    self.ui.comps.cvs_guildinfo.Visible = false

    --排行榜
    self.ui.comps.btn_guildrank.TouchClick = function( sender )
        self.ui.comps.cvs_guildrank.Visible = true
    end
    self.ui.comps.btn_rankclose.TouchClick = function( sender )
        self.ui.comps.cvs_guildrank.Visible = false
    end

    --规则
    self.ui.comps.btn_rule.TouchClick = function( sender )
        self.ui.comps.cvs_rule.Visible = true
    end
    self.ui.comps.cvs_rule.TouchClick = function( sender )
        self.ui.comps.cvs_rule.Visible = false
    end
    self.ui.comps.btn_ruleclose.TouchClick = function( sender )
        self.ui.comps.cvs_rule.Visible = false
    end

    --进入战场
    self.ui.comps.btn_go.TouchClick = function( sender )
        EventApi.Task.StartEvent(function()
	        local ok, ret = EventApi.Task.Wait(EventApi.Protocol.Task.Request('EnterGuildWallPlayerCount',{}))
	        ShowFieldDetail(self, ret)
	    end)
    end
    self.ui.comps.btn_closechoose.TouchClick = function( sender )
        self.ui.comps.cvs_choosefield.Visible = false
    end

    --进入主战场
    self.ui.comps.btn_gomainfield.TouchClick = function( sender )
        EventApi.Task.StartEvent(function()
	        EventApi.Task.Wait(EventApi.Protocol.Task.Request('EnterGuildWallZone',{mapid = 502000}))
	    end)
    end
    --进入副战场
    self.ui.comps.btn_goassifield.TouchClick = function( sender )
        EventApi.Task.StartEvent(function()
	        EventApi.Task.Wait(EventApi.Protocol.Task.Request('EnterGuildWallZone',{mapid = 502001}))
	    end)
    end

    self.ui.comps.cvs_pointinfo.event_PointerUp = function( ... )
    	self.ui.comps.cvs_pointinfo.Visible = false
    end
end

return _M