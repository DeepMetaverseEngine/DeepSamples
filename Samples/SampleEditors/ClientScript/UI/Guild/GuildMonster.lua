local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local TimeUtil = require 'Logic/TimeUtil'

local function Release3DModel(self)
	if self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DModel(self, parentCvs, pos2d, scale, rotate, menuOrder, filename)
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder, filename)
	local trans = info.RootTrans
	self.model = info
	info.Callback = function( arg )
		trans:Rotate(Vector3.up, rotate)
	end
	parentCvs.event_PointerMove = function(sender, data)
		local delta = -data.delta.x
		trans:Rotate(Vector3.up, delta * 1.2)
	end
end

local function ShowAttribute( self, data, oldData )
	self.comps.cvs_attribute.Visible = true
	local monsterRank = self.selectIndex
	local monsterLevel
	local lvdb = GlobalHooks.DB.Find('guild_monster_culture', { monster_rank = monsterRank } )
	local lvdbOld = oldData ~= nil and GlobalHooks.DB.Find('guild_monster_culture', { monster_rank = oldData.monsterRank } ) or nil
	if self.selectIndex < data.monsterRank then
		monsterLevel = #lvdb
	elseif self.selectIndex == data.monsterRank then
		monsterLevel = data.monsterLevel
	else
		monsterLevel = 1
	end

    local attrs = ItemModel.GetXlsFixedAttribute(lvdb[monsterLevel])
    local attrsOld = oldData ~= nil and ItemModel.GetXlsFixedAttribute(lvdbOld[oldData.monsterLevel]) or nil
	local space = 20
	local cvsRoot = self.comps.cvs_tips1
    local prefab = cvsRoot:FindChildByEditName('cvs_info1', true)
	for i = 1, #attrs do
		local cvs = cvsRoot:FindChildByName('cvs_info'..i, true)
		if cvs == nil then
			if i == 1 then
				cvs = prefab
				cvs.Name = 'cvs_info1'
			elseif i <=5 then
				cvs = prefab:Clone()
				cvsRoot:AddChild(cvs)
				cvs.Name = 'cvs_info'..i
				cvs.X = prefab.X
				cvs.Y = prefab.Y + (prefab.Height + space) * (i - 1)
			else
			cvs = prefab:Clone()
					cvsRoot:AddChild(cvs)
					cvs.Name = 'cvs_info'..i
					cvs.X = prefab.X + 250
					cvs.Y = prefab.Y + (prefab.Height + space) * (i  - 6)

			end
		end

		local attrName, valueStr, value = ItemModel.GetAttributeString(attrs[i])
		MenuBase.SetLabelText(cvs, 'ib_attribute', attrName, 0, 0)
		local valueComp = cvs:FindChildByEditName('lb_atbvalue', true)
		local effectNode = cvs:FindChildByEditName('ib_atbup', true)
		local addComp = cvs:FindChildByEditName('lb_atbadd', true)

		local addValue = 0
		if oldData then
			local attrNameOld, valueStrOld, valueOld = ItemModel.GetAttributeString(attrsOld[i])
			addValue = value - valueOld
		end
		if addValue > 0 then --显示升级效果
			--箭头
			effectNode.Visible = true
			UIUtil.PlayCPJOnce(effectNode, 1, function(sender)
				effectNode.Visible = false
			end)
			--增加数字
			addComp.Visible = true
			addComp.Text = addValue
			--滚动数字
			UIUtil.AddNumberPlusPlusTimer(valueComp, value - addValue, value, 0.5, nil, function()
				addComp.Visible = false
			end)
		else
			valueComp.Text = valueStr
			effectNode.Visible = false
			addComp.Visible = false
		end
	end
end

local function InitMonsterInfo( self, data )
	local monsterRank = self.selectIndex
	local rankdbs = GlobalHooks.DB.GetFullTable('guild_monster_plus')
	local rankdb = rankdbs[monsterRank]
	local lvdb = GlobalHooks.DB.Find('guild_monster_culture', { monster_rank = monsterRank } )
	local monsterLevel = self.selectIndex < data.monsterRank and #lvdb or data.monsterLevel
	-- local monsterExp = self.selectIndex == data.monsterRank and data.monsterLevel or 

	self.ui.comps.lb_name.Text = Util.GetText(rankdb.monster_name)
	self.ui.comps.lb_lv.Text = Util.GetText('common_level4', monsterRank, monsterLevel)

	if self.selectIndex <= data.monsterRank then
		self.comps.cvs_bottom.Visible = true
		self.comps.lb_need.Visible = false
		--gauge
		if monsterLevel < #lvdb then --未满级
			self.comps.cvs_cost.Visible = true
			self.comps.lb_max.Visible = false
			self.comps.gg_exp:SetGaugeMinMax(0, lvdb[monsterLevel].cost_num)
			self.comps.gg_exp.Value = data.monsterExp
			self.ui.comps.lb_now.Text = Util.GetText('common_level4', monsterRank, monsterLevel)
			self.ui.comps.lb_next.Text = Util.GetText('common_level4', monsterRank, monsterLevel + 1)
			self.comps.gg_exp.Text = string.format('%d/%d', data.monsterExp, lvdb[monsterLevel].cost_num)
		else --满级
			self.comps.cvs_cost.Visible = false
			self.comps.lb_max.Visible = true
			self.comps.gg_exp:SetGaugeMinMax(0, 1)
			self.comps.gg_exp.Value = 1
			self.ui.comps.lb_now.Text = Util.GetText('common_level4', monsterRank, monsterLevel - 1)
			self.ui.comps.lb_next.Text = Util.GetText('common_level4', monsterRank, monsterLevel)
			self.comps.gg_exp.Text = 'MAX'
		end

		--item
		local itemId = GlobalHooks.DB.GetGlobalConfig('guild_itemid')
		local itemdetail = ItemModel.GetDetailByTemplateID(itemId)
		local icon = itemdetail.static.atlas_id
		local quality = itemdetail.static.quality
		local num = ItemModel.CountItemByTemplateID(itemId)
		local cvs_item = self.comps.cvs_item1
		local itshow = UIUtil.SetItemShowTo(cvs_item, icon, quality, num)
	    itshow.EnableTouch = true
	    itshow.TouchClick = function()
	        local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
	        -- detail:SetPos(0, 350)
	    end
		-- self.ui.comps.lb_costnum.Text = string.format('%d/%d', num, lvdb[monsterLevel].cost_num - data.monsterExp)
		local cost = { id = itemId, detail = itemdetail, cur = num, need = 1}
		UIUtil.SetEnoughItemShowAndLabel(self, self.comps.cvs_item1, self.comps.lb_costnum, cost, {onlycur = true })
	else
		self.comps.cvs_bottom.Visible = false
		self.comps.lb_need.Visible = true
		local monsterdb = GlobalHooks.DB.FindFirst('guild_monster', { monster_rankmax = monsterRank } )
		self.ui.comps.lb_need.Text = Util.GetText('guild_monster_needlv', monsterdb.level)
	end

	--被动属性
    local attrs = ItemModel.GetXlsFixedAttribute(rankdb)
    for i = 1, 3 do
    	local nameLab = self.ui.comps['lb_shuxing'..i]
    	local valueLab = self.ui.comps['lb_shuxingnum'..i]
    	if i <= #attrs then
		    local attrName, value = ItemModel.GetAttributeString(attrs[i])
			nameLab.Visible = true
			valueLab.Visible = true
			nameLab.Text = attrName
			valueLab.Text = '+'..value
    	else
			nameLab.Visible = false
			valueLab.Visible = false
    	end
    end

    --model
	local modelAnchor = self.ui.comps.cvs_anchor
    local posXY = string.split(rankdb.pos_xy, ',')
    local pos = Vector2(modelAnchor.X + tonumber(posXY[1]), modelAnchor.Y + tonumber(posXY[2]))
    Release3DModel(self)
	Init3DModel(self, self.comps.cvs_model, pos, tonumber(rankdb.zoom), rankdb.rotate, self.ui.menu.MenuOrder, rankdb.monster_model)
	self.ui.comps.btn_left.Visible = self.selectIndex > 1
	self.ui.comps.btn_right.Visible = self.selectIndex < #rankdbs

	--log
    local log = ''
    -- print_r(data.logList)
    for i = 1, #data.logList do
        local timeStr
        local sec = TimeUtil.TimeLeftSec(data.logList[i].time)
        if sec < 60 then
            timeStr = Util.GetText('common_now')
        else
            timeStr = Util.GetText('common_ago', TimeUtil.FormatToCN(sec))
        end
        local content = data.logList[i].content
        for k=1,#data.logList[i].args do
            local v= data.logList[i].args[k]
            if string.find(v, 'tlg_') == 1 then
                data.logList[i].args[k] = Util.GetText(string.sub(v, 5))
            end
        end
        content = Util.GetText(content, unpack(data.logList[i].args))
        log = log..timeStr..'\n'..content..'\n\n'
    end

    local logComp = self.ui.comps.tb_log
    logComp.Text = log
    logComp.Height = logComp.TextComponent.PreferredSize.y
end

function _M.OnEnter( self, params )
	-- print('----------OnEnter', params)
end

function _M.OnLoad(self, callBack, params)
	-- print('----------OnLoad', params)
    GuildModel.ClientGetMonsterInfoRequest(function( rsp )
    	if rsp:IsSuccess() then --返回正常，显示界面
	    	self.monsterInfo = rsp.s2c_monsterInfo
	    	self.selectIndex = self.monsterInfo.monsterRank
	        InitMonsterInfo(self, self.monsterInfo)
			callBack:Invoke(true)
    	else --功能未开放，关闭UI
			callBack:Invoke(true)
    		self.ui.menu:Close()
    	end
    end)
end

function _M.OnExit( self )
    Release3DModel(self)
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu:SetUILayer(self.ui.comps.cvs_attribute)
	self.ui.menu:SetUILayer(self.ui.comps.cvs_help)
    self.ui.comps.btn_use.TouchClick = function( sender )
	    GuildModel.ClientMonsterLeveUpRequest(function( rsp )
	    	SoundManager.Instance:PlaySoundByKey('gongnengshengji',false)
			Util.PlayEffect('/res/effect/ui/ef_ui_interface_upgrade.assetbundles', { 
				Parent = self.comps.cvs_anchor.Transform, UILayer = true, DisableToUnload = true, LayerOrder = self.ui.menu.MenuOrder })

	    	local oldData = self.monsterInfo
    		self.monsterInfo = rsp.s2c_monsterInfo
	    	if oldData.monsterLevel < rsp.s2c_monsterInfo.monsterLevel or oldData.monsterRank < rsp.s2c_monsterInfo.monsterRank then
				ShowAttribute(self, self.monsterInfo, oldData)
				if oldData.monsterRank < rsp.s2c_monsterInfo.monsterRank then
					self.selectIndex = self.selectIndex + 1
				end
				--effect
				SoundManager.Instance:PlaySoundByKey('jinjie',false)
				Util.PlayEffect('/res/effect/ui/ef_ui_interface_advanced.assetbundles', { UILayer = true, LayerOrder = self.ui.menu.MenuOrder })
	    	end
    		self.monsterInfo = rsp.s2c_monsterInfo
        	InitMonsterInfo(self, self.monsterInfo)
	    end)
    end
	self.ui.comps.btn_left.TouchClick = function( ... )
		self.selectIndex = self.selectIndex - 1
        InitMonsterInfo(self, self.monsterInfo)
	end
	self.ui.comps.btn_right.TouchClick = function( ... )
		self.selectIndex = self.selectIndex + 1
        InitMonsterInfo(self, self.monsterInfo)
	end
	self.ui.comps.btn_help.TouchClick = function( ... )
		self.ui.comps.cvs_help.Visible = true
	end
	self.ui.comps.btn_shuxing.TouchClick = function( ... )
		ShowAttribute(self, self.monsterInfo)
	end
	self.ui.comps.cvs_help.TouchClick = function( ... )
		self.ui.comps.cvs_help.Visible = false
	end
	self.ui.comps.cvs_attribute.TouchClick = function( ... )
		self.ui.comps.cvs_attribute.Visible = false
	end
end

return _M