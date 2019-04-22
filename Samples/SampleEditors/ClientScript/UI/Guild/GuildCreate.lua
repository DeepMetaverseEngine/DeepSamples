local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

function _M.OnEnter( self )

end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover

	--item
	local itemId = GlobalHooks.DB.GetGlobalConfig('guild_createitem')
	local itemdetail = ItemModel.GetDetailByTemplateID(itemId)
	local icon = itemdetail.static.atlas_id
	local quality = itemdetail.static.quality
	local num = ItemModel.CountItemByTemplateID(itemId)
	local cvs_item = self.comps.cvs_item
	local itshow = UIUtil.SetItemShowTo(cvs_item, icon, quality)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
        -- detail:SetPos(0, 350)
    end
	self.ui.comps.ib_itemnum.Text = tostring(GlobalHooks.DB.GetGlobalConfig('guild_createitemnum'))

	self.ui.comps.ti_import.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('guild_namelimit')
	self.ui.comps.ti_import1.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('guild_recruitlimit')

	self.ui.comps.btn_found.TouchClick = function( ... )
		if string.len(self.ui.comps.ti_import.Text) == 0 then
			GameAlertManager.Instance:ShowNotify(Util.GetText('guild_nonename'))
		else
			local name = self.ui.comps.ti_import.Input.Text
			local recuit = self.ui.comps.ti_import1.Input.Text
			GuildModel.ClientCreateGuildRequest(name, recuit, function( rsp )
				GameAlertManager.Instance:ShowNotify(Util.GetText('guild_creat', rsp.s2c_guildInfo.guildBase.name))
				self.ui.menu:Close()
				GlobalHooks.UI.CloseUIByTag('GuildList')
				GlobalHooks.UI.OpenUI("GuildMain", 0, 'GuildInfo', { data = rsp.s2c_guildInfo, position = rsp.s2c_position })
				Util.PlayEffect('/res/effect/ui/ef_ui_interface_advanced.assetbundles', { UILayer = true, LayerOrder = self.ui.menu.MenuOrder })
			end)
		end
	end
end

return _M