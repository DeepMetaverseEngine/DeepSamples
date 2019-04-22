local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'


local function CheckIslock( self, data )
	if self.talentLv < self.list[self.talentSel].level then
		return true --未解锁
	end
	if data.front_id ~= 0 and (self.skills[data.front_id] == nil or self.skills[data.front_id] < data.front_lv) then
		return true --未解锁
	end
	return false --解锁
end

function _M.RefreshDetail( self )
	local data = self.db[self.skillSel]
	local lvdb = GlobalHooks.DB.Find('guild_talent_lv', { skill_id = data.skill_id } )
	local name = Util.GetText(data.skill_name)
	local skillLv = self.skills[data.skill_id] or 0
	self.ui.comps.lb_tips1.Text = string.format('%s(%d/%d)', name, skillLv, #lvdb - 1)

	-- 当前效果
    local attrs = ItemModel.GetXlsFixedAttribute(lvdb[skillLv + 1])
    if #attrs > 0 then
	    --取第一个属性显示
	    local attrName, value = ItemModel.GetAttributeString(attrs[1])
		self.ui.comps.lb_cur.Text = attrName .. '+' .. value
	else
		self.ui.comps.lb_cur.Text = Util.GetText('common_none')
	end
	-- 下级效果
	local isMax = not (skillLv < lvdb[#lvdb].skill_lv)
	if not isMax then
    	local attrs1 = ItemModel.GetXlsFixedAttribute(lvdb[skillLv + 1 + 1])
	    if #attrs1 > 0 then
		    --取第一个属性显示
		    local attrName, value = ItemModel.GetAttributeString(attrs1[1])
			self.ui.comps.lb_next.Text = attrName .. '+' .. value
		else
			self.ui.comps.lb_next.Text = Util.GetText('common_none')
		end
	else
		self.ui.comps.lb_next.Text = Util.GetText('common_none')
	end

	--升级条件
	local islock = CheckIslock(self, data)
	self.ui.comps.lb_unlock.Visible = islock
	self.ui.comps.cvs_costlist.Visible = not islock and not isMax
	self.ui.comps.btn_up.Visible = not islock and not isMax
	self.ui.comps.lb_max.Visible = isMax
	if islock then
		local preSkillId = data.front_id
		local dbCondition = unpack(GlobalHooks.DB.Find('guild_talent', { skill_id = preSkillId } ))
		if dbCondition ~= nil then
			local preSkillName = Util.GetText(dbCondition.skill_name)
			self.ui.comps.lb_unlock.Text = Util.GetText('guild_skillup_condition', preSkillName, data.front_lv)
		else
			self.ui.comps.lb_unlock.Text = Util.GetText(self.list[self.talentSel].des)
		end
	elseif isMax then

	else
		-- local cost = lvdb[skillLv + 1].cost
		local cvsRoot = self.ui.comps.cvs_costlist
		local space = 30
		local costs = ItemModel.ParseCostAndCostGroup(lvdb[skillLv + 1])
		local len = #costs > cvsRoot.NumChildren and #costs or cvsRoot.NumChildren
		for i = 1, len do
			local cvs_cost = cvsRoot:FindChildByName('cvs_cost'..i, true)
			if i <= #costs then
				if cvs_cost == nil then
					if i == 1 then
						cvs_cost = cvsRoot:FindChildByEditName('cvs_cost', true)
						cvs_cost.Name = 'cvs_cost1'
					else
						local prefab = cvsRoot:FindChildByName('cvs_cost1', true)
						cvs_cost = prefab:Clone()
						cvsRoot:AddChild(cvs_cost)
						cvs_cost.Name = 'cvs_cost'..i
						cvs_cost.Y = prefab.Y
					end
				end
				cvs_cost.Visible = true
				cvs_cost.X = (cvsRoot.Width - ((cvs_cost.Width + space) * #costs - space)) * 0.5 + (i - 1) * (cvs_cost.Width + space)

				local cvs_item = cvs_cost:FindChildByEditName('cvs_costitem', true)
				UIUtil.SetEnoughItemShowAndLabel(self, cvs_item, cvs_cost:FindChildByEditName('lb_costnum', true), costs[i])
			else
				if cvs_cost then
					cvs_cost.Visible = false
				end
			end
		end
	end
	self.ui.comps.btn_up.TouchClick = function( ... )
		GuildModel.ClientTalentSkillUpRequest(data.skill_id, function( rsp )
			--特效
			local node = self.ui.comps['cvs_skill'..self.talentSel]
			local efCvs = node:FindChildByEditName('fn_a'..self.skillSel, true)
			Util.PlayEffect('/res/effect/ui/ef_ui_xianmen_skill_upgrade.assetbundles', { 
				Parent = efCvs.Transform, Pos = { x = efCvs.Width * 0.5, y = -efCvs.Height * 0.5 }, 
				UILayer = true, DisableToUnload = true, LayerOrder = self.ui.menu.MenuOrder })

			--判断是否解锁新技能
			self.lastUnlockSkill = {}
			local db = GlobalHooks.DB.Find('guild_talent', { front_id = data.skill_id } )
			if db and #db > 0 then
				for i = 1, #db do
					if db[i].front_lv == rsp.s2c_skillLv then --刚好解锁
						self.lastUnlockSkill[db[i].skill_id] = true
					end
				end
			end

			self.skills[data.skill_id] = rsp.s2c_skillLv
			_M.RefreshTalentSkill(self, self.talentSel)
			self.pan:RefreshShowCell()
			_M.RefreshDetail(self)
			SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)
			GameAlertManager.Instance:ShowFloatingTips(Util.GetText('common_lvup'))
			self.lastUnlockSkill = {}
		end)
	end
end

function _M.RefreshTalentSkill( self, index )
	if self.talentSel ~= index then
		self.ui.comps['cvs_skill'..self.talentSel].Visible = false
		self.talentSel = index
	end
	self.ui.comps['cvs_skill'..self.talentSel].Visible = true
	self.ui.comps['cvs_skill'..self.talentSel].IsGray = self.talentLv < self.list[self.talentSel].level
	local node = self.ui.comps['cvs_skill'..self.talentSel]
	self.db = GlobalHooks.DB.Find('guild_talent', { talent_lv = self.talentSel } )
	for i=1, #self.db do
		local data = self.db[i]
		local fn = node:FindChildByEditName('fn_a'..i, true)
		local icon =fn:FindChildByEditName('ib_tupian', true)

		UIUtil.SetImage(icon,data.skill_icon,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
		MenuBase.SetVisibleUENode(fn, 'lb_up', GuildModel.CanTalentLvUp(data.talent_lv, data.skill_id))
		if self.skills[data.skill_id] then --已学习
			MenuBase.SetVisibleUENode(fn, 'ib_duan', true)
			MenuBase.SetLabelText(fn, 'ib_duan', self.skills[data.skill_id], 0, 0)
			MenuBase.SetVisibleUENode(fn, 'lb_lock', false)
		else
			MenuBase.SetVisibleUENode(fn, 'ib_duan', false)
			local islock = CheckIslock(self, data)
			MenuBase.SetVisibleUENode(fn, 'lb_lock', islock)
			MenuBase.SetVisibleUENode(node, 'cvs_x'..i, not islock)
			if self.lastUnlockSkill[data.skill_id] then
				--解锁特效
				Util.PlayEffect('/res/effect/ui/ef_ui_interface_skill_unlock.assetbundles', { 
					Parent = fn.Transform, Pos = { x = fn.Width * 0.5, y = -fn.Height * 0.5 }, 
					UILayer = true, DisableToUnload = true, LayerOrder = self.ui.menu.MenuOrder })
				end
		end

		local selSkillCvs = fn:FindChildByEditName('ib_select', true)
		if self.skillSel == i then
			self.selSkillCvs = selSkillCvs
		end
		selSkillCvs.Visible = self.skillSel == i
		
		local cvs = fn:FindChildByEditName('cvs_item', true)
		cvs.event_PointerUp = function( ... )
			self.skillSel = i
			SoundManager.Instance:PlaySoundByKey('button',false)
			_M.RefreshDetail(self)

			self.selSkillCvs.Visible = false
			self.selSkillCvs = selSkillCvs
			self.selSkillCvs.Visible = true
		end
	end
end

local function RefreshListCellData( self, node, index )
	local data = self.list[index]
	MenuBase.SetLabelText(node, 'lb_text1', Util.GetText(data.talent_name), 0, 0)
	MenuBase.SetVisibleUENode(node, 'ib_lock', self.talentLv < data.level)

	local red = node:FindChildByEditName('lb_skillred', true)
	local showReds = GuildModel.CanTalentGroupLvUp(index)
	GlobalHooks.UI.ShowRedPoint(red, showReds, 'guild_college')

	local tbt = node:FindChildByEditName('tbt_sub', true)
	tbt.IsChecked = self.talentSel == index
	tbt.TouchClick = function( ... )
		self.skillSel = 1
		_M.RefreshTalentSkill(self, index)
		self.pan:RefreshShowCell()
		_M.RefreshDetail(self)
	end
end

function _M.RefreshList( self )
	GuildModel.ClientGetTalentDataRequest(function( rsp )
		self.skills = rsp.s2c_talent.talentSkill
		self.talentLv = rsp.s2c_talent.talentLv
		self.list = GlobalHooks.DB.Find('guild_college', function(ele) return ele.level ~= 0 end )
		self.pan = self.ui.comps.sp_oar
		local cell = self.ui.comps.cvs_kuang1
		UIUtil.ConfigVScrollPan(self.pan, cell, #self.list, function(node, index)
			RefreshListCellData(self, node, index)
		end)
		_M.RefreshTalentSkill(self, self.talentSel)
		_M.RefreshDetail(self)
	end)
end

function _M.OnEnter( self )
	self.talentSel = 1
	self.skillSel = 1
	self.lastUnlockSkill = {}
	_M.RefreshList( self )
end

function _M.OnExit( self )
	self.ui.comps['cvs_skill'..self.talentSel].Visible = false
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.comps.cvs_kuang1.Visible = false
end

return _M