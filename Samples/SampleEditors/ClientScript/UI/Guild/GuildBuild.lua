local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local QuestUtil = require("UI/Quest/QuestUtil")
local ItemModel=require 'Model/ItemModel'

local function CheckCanLevelUp( self, id, showErrorTip )
	local builds = self.params.buildList
	local dbname = {'guild_lv','guild_stable','guild_monster','guild_college','guild_store'}
	local dblv = GlobalHooks.DB.Find(dbname[id], {})
	local dblvCur = unpack(GlobalHooks.DB.Find(dbname[id], {level = builds[id]}))
	if builds[id] >= dblv[#dblv].level then
		if showErrorTip then
	    	GameAlertManager.Instance:ShowNotify(Util.GetText('guild_build_lvmax'))
		end
		return false
	elseif id ~= 1 and builds[id] >= builds[1] then
		if showErrorTip then
	    	GameAlertManager.Instance:ShowNotify(Util.GetText('guild_lv_notenough'))
		end
		return false
	elseif self.params.fund < dblvCur.capital_lvup then
		if showErrorTip then
	    	GameAlertManager.Instance:ShowNotify(Util.GetText('guild_money_notenough'))
		end
		return false
	else
		return true
	end
end


local function RefreshDetail( self, id )
	local builds = self.params.buildList
	local db = unpack(GlobalHooks.DB.Find('guild', {id = id}))
	local dbname = {'guild_lv','guild_stable','guild_monster','guild_college','guild_store'}
	local dblv = GlobalHooks.DB.Find(dbname[id], {})
	local dblvCur = unpack(GlobalHooks.DB.Find(dbname[id], {level = builds[id]}))

	self.comps.lb_name1.Text = Util.GetText(db.build_name)
	self.comps.tb_des.Text = Util.GetText(db.des)

	local cvs_cur = self.comps.cvs_now
	MenuBase.SetLabelText(cvs_cur, 'lb_lv', Util.GetText('common_level2', builds[id]), 0, 0)
	MenuBase.SetLabelText(cvs_cur, 'lb_des1', string.gsub(Util.GetText(dblvCur.des), '\\n', '\n'), 0, 0)
	local cvs_next = self.comps.cvs_next
	local lvMax = builds[id] >= dblv[#dblv].level
	self.comps.cvs_item1.Visible = not lvMax
	self.comps.btn_up.Visible = not lvMax
	if lvMax then --已满级
		MenuBase.SetLabelText(cvs_next, 'lb_lv', '-', 0, 0)
		MenuBase.SetLabelText(cvs_next, 'lb_des1', '', 0, 0)
	else
		local dblvNext = unpack(GlobalHooks.DB.Find(dbname[id], {level = builds[id] + 1}))
		MenuBase.SetLabelText(cvs_next, 'lb_lv', Util.GetText('common_level2', builds[id] + 1), 0, 0)
		MenuBase.SetLabelText(cvs_next, 'lb_des1', string.gsub(Util.GetText(dblvNext.des), '\\n', '\n'), 0, 0)

		--升级消耗
		local detail = ItemModel.GetDetailByTemplateID(1000)
		local count =self.params.fund
		local cost = {id = 1000, detail = detail,cur = count,need =dblvCur.capital_lvup}
		-- local point = self.ui.root:GlobalToLocal(self.comps.cvs_item1:LocalToGlobal(),true)
		-- local tipsparams = {x = point.x-300,
		-- 					y = point.y-250,
		-- 					anchoror = 'r_b',
		-- 					cb = function()
		-- 						self.ui.menu:Close()
		-- 					end}
		local tipsparams = {ListenItem = false}
		UIUtil.SetEnoughItemShowAndLabel(self,self.comps.cvs_item1,self.comps.lb_costnum,cost,tipsparams)

	end
	--self.comps.lb_costnum.Text = string.format('%d/%d', self.params.fund, dblvCur.capital_lvup)

	self.comps.btn_up.TouchClick = function( ... )
		if CheckCanLevelUp(self, id, true) then
			GuildModel.ClientBuildLevelUpRequest(id, function( rsp )
				GameAlertManager.Instance:ShowFloatingTips(Util.GetText('common_lvup'))
				--特效
				local efCvs = self.ui.comps['cvs_build'..id]
				Util.PlayEffect('/res/effect/ui/ef_ui_xianmen_buildingupgrade.assetbundles', { 
					Parent = efCvs.Transform, Pos = { x = efCvs.Width * 0.5, y = -efCvs.Height * 0.5 }, 
					UILayer = true, DisableToUnload = true, LayerOrder = self.ui.menu.MenuOrder })
				
				self.params.buildList[id] = self.params.buildList[id] + 1
				self.params.fund = rsp.s2c_fund
				_M.InitBuilding(self, self.params.buildList)
				RefreshDetail(self, self.selected)
				if self.params.cb ~= nil then
					self.cbData = {}
					self.cbData.buildList = self.params.buildList
					self.cbData.fund = rsp.s2c_fund
				end
			end)
		end
	end
end

function _M.InitBuilding( self, builds )
	local db = GlobalHooks.DB.Find('guild', {})
	local tbts = {}
	for _, v in ipairs(db) do
		local cvs = self.ui.comps['cvs_build'..v.id]
		MenuBase.SetLabelText(cvs, 'lb_name', Util.GetText(v.build_name), 0, 0)
		MenuBase.SetLabelText(cvs, 'lb_lv', Util.GetText('common_level2', builds[v.id]), 0, 0)
		MenuBase.SetVisibleUENode(cvs, 'ib_up', CheckCanLevelUp(self, v.id, false))
		local icon = cvs:FindChildByEditName('ib_icon', true)
		UIUtil.SetImage(icon, v.icon)
		if cvs then
			local tbt = cvs:FindChildByEditName('tbt_build'..v.id, true)
			tbt.UserTag = v.id
			table.insert(tbts, tbt)
		end
		local go = cvs:FindChildByEditName('btn_go', true)
		go.TouchClick = function( ... )
			-- QuestUtil.tryOpenFunction(v.funtion_tag)
			FunctionUtil.OpenFunction(v.funtion_tag)
            MenuMgr.Instance:CloseAllMenu()
		end
	end
	UIUtil.ConfigToggleButton(tbts, tbts[self.selected], true, function( sender )
		self.selected = sender.UserTag
		RefreshDetail(self, sender.UserTag)
	end)
end

function _M.OnEnter( self, params )
	self.params = params
	self.selected = 1
	_M.InitBuilding(self, params.buildList)
end

function _M.OnExit( self )
    if self.params.cb then
		self.params.cb(self.cbData)
    	self.params.cb = nil
    end
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )

end

return _M