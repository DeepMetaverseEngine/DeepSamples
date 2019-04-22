local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local CDExt = require 'Logic/CDExt'
local TimeUtil = require 'Logic/TimeUtil'

local function ClearCDExt( self )
	if self.cdExt then
	    for _, v in pairs(self.cdExt) do
	    	if v then
	    		v:Stop()
	    	end
	    end
	end
end

local function RefreshListCellData( self, node, index )
	local data = self.giftList[#self.giftList - index + 1]
	local itemdetail = ItemModel.GetDetailByTemplateID(data.itemId)
	local icon = itemdetail.static.atlas_id
	local quality = itemdetail.static.quality
	local num = 1
	local cvs_item = node:FindChildByEditName('cvs_item', true)
	local itshow = UIUtil.SetItemShowTo(cvs_item, icon, quality, num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
        -- detail:SetPos(0, 350)
    end

	MenuBase.SetLabelText(node, 'lb_zi1', Util.GetText(itemdetail.static.name), 0, 0)
	MenuBase.SetLabelText(node, 'lb_zi2', Util.GetText('guild_source_'..data.source, data.roleName), 0, 0)

	--cd
	local function cdFun( cd, label )
		MenuBase.SetLabelText(node, 'lb_zi4', TimeUtil.formatCD("%H:%M:%S", cd), 0, 0)
		if cd == 0 then
			--这里就不重刷新了，对操作中的玩家不友好
		end
	end
	local time = -TimeUtil.TimeLeftSec(data.expiredTime)
	if self.cdExt[index] == nil then
		self.cdExt[index] = CDExt.New(time, cdFun)
	else
		self.cdExt[index]:Stop()
		self.cdExt[index]:Reset(time, cdFun)
	end
	-- self.cdExt[index]:Start()
end

local function InitGiftInfo( self, giftInfo )
	self.comps.lb_lv.Text = 'Lv'..giftInfo.giftLv
	local giftdb = GlobalHooks.DB.FindFirst('guild_gift', { gift_lv = giftInfo.giftLv } )
	self.comps.gg_jindu:SetGaugeMinMax(0, giftdb.exp)
	self.comps.gg_jindu.Value = giftInfo.giftLvExp
	self.comps.gg_jindu.Text = string.format('%d/%d', giftInfo.giftLvExp, giftdb.exp)
	local giftTypedb = GlobalHooks.DB.FindFirst('guild_gift_type', { item_id = giftInfo.nextGiftId } )
	self.comps.lb_lwname.Text = Util.GetText(giftTypedb.name)
	UIUtil.SetImage(self.comps.ib_bjtu4, giftTypedb.icon)
	self.comps.gg_jindu2:SetGaugeMinMax(0, giftTypedb.need_key)
	self.comps.gg_jindu2.Value = giftInfo.giftOpenExp
	self.giftList = giftInfo.giftList
	self.comps.lb_num.Text = #self.giftList..'/'..GlobalHooks.DB.GetGlobalConfig('guild_giftmax')
	self.comps.lb_jundunum.Text = giftInfo.giftOpenExp..'/'..giftTypedb.need_key
	self.comps.cvs_kong.Visible = #self.giftList == 0
	local pan = self.ui.comps.sp_oar
	local cell = self.ui.comps.cvs_xinxi
	cell.Visible = false
	ClearCDExt(self)
	UIUtil.ConfigVScrollPan(pan, cell, #self.giftList, function(node, index)
		RefreshListCellData(self, node, index)
	end)
    GlobalHooks.UI.SetRedTips('guild_gift', #self.giftList)
end

function _M.OnEnter( self )
    self.cdExt = {}
    GuildModel.ClientGiftInfoRequest(function( rsp )
        InitGiftInfo(self, rsp.s2c_giftInfo)
    end)
end

function _M.OnExit( self )
	ClearCDExt(self)
    self.cdExt = nil
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
    self.ui.comps.btn_lingqu.TouchClick = function( sender )
	    GuildModel.ClientOpenGiftRequest(function( rsp )
	   --  	if rsp:IsSuccess() then
				-- GameAlertManager.Instance:ShowFloatingTips(Util.GetText('')) -- 已领取
	   --  	end
	        InitGiftInfo(self, rsp.s2c_giftInfo)
	    end)
    end

    self.ui.comps.btn_help.TouchClick = function( ... )
    	self.ui.comps.cvs_tips.Visible = true
    end

    self.ui.comps.cvs_tips.event_PointerUp = function( ... )
    	self.ui.comps.cvs_tips.Visible = false
    end
end

return _M