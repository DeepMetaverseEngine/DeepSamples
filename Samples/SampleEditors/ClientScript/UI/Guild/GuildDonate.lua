local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local RechargeModel = require 'Model/RechargeModel'

local function InitShowData( self )
	local db = GlobalHooks.DB.Find('guild_donate', {})
    for i=1, #db do
        local node = self.ui.comps['cvs_k'..i]
        local data = db[i]
        if node then
			-- MenuBase.SetLabelText(node, 'lb_zi1', data.name, 0, 0)
			MenuBase.SetLabelText(node, 'lb_zi2', data.capital_num, 0, 0)
			MenuBase.SetLabelText(node, 'lb_zi3', data.contribution_num, 0, 0)

			local itemdetail = ItemModel.GetDetailByTemplateID(data.cost_id)
			local iconPath = 'static/item/'..itemdetail.static.atlas_id
			MenuBase.SetImageBox(node, 'ib_huobi3', iconPath, UILayoutStyle.IMAGE_STYLE_BACK_4, 8)
			MenuBase.SetLabelText(node, 'lb_num3', tostring(data.cost_num), GameUtil.RGB2Color(ItemModel.CountItemByTemplateID(data.cost_id) < data.cost_num and 0xff0000 or 0))

			local btn = node:FindChildByEditName('btn_an1', true)
			btn.TouchClick = function( ... )
				if self.donateCount >= self.maxCount then
                    GameAlertManager.Instance:ShowNotify(Util.GetText('guild_donatemax'))
				else
					GuildModel.ClientDailyDonateRequest(data.donate_type, function( rsp )
						GameAlertManager.Instance:ShowFloatingTips(Util.GetText('guild_fund_add', data.capital_num))
						--特效
						Util.PlayEffect('/res/effect/ui/ef_ui_xianmen_donation.assetbundles', { 
							Parent = node.Transform, Pos = { x = node.Width * 0.5, y = -node.Height * 0.5 }, 
							UILayer = true, DisableToUnload = true, LayerOrder = self.ui.menu.MenuOrder })

						self.donateCount = rsp.s2c_donateCount
						self.ui.comps.lb_count.Text = string.format('%d/%d', self.maxCount - self.donateCount, self.maxCount)
						if self.cb then
							local cbData = {}
							cbData.donateCount = rsp.s2c_donateCount
							cbData.contribution = rsp.s2c_contribution
							cbData.contributionMax = rsp.s2c_contributionMax
							self.cb(cbData)
						end
						InitShowData(self)
					end)
				end
			end
        end
    end
end

function _M.OnEnter( self, params )
	self.maxCount = RechargeModel.GetVipInfoValueByKey('guild_donate')
	self.donateCount = DataMgr.Instance.GuildData.DonateCount
	self.cb = params.cb
	self.ui.comps.lb_count.Text = string.format('%d/%d', self.maxCount - self.donateCount, self.maxCount)
	InitShowData(self)
end

function _M.OnExit( self )
    self.cb = nil
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
end

return _M