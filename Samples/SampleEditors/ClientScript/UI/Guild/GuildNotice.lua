local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

function _M.OnEnter( self, params )
    print('-----------------aaaaaaaa', params)
	if params then
    	self.ui.comps.ti_import1.Input.Text = params.recruitBulletin
        self.cb = params.cb
	end
end

function _M.OnExit( self )
    self.cb = nil
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
    self.ui.comps.ti_import1.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('guild_recruitlimit')
    self.ui.comps.ti_import1.event_endEdit = function( send, text )
        if self.lastRecruit ~= text then
            GuildModel.ClientChangeRecruitRequest(text, function( rsp )
                if rsp:IsSuccess() then
                    self.ui.comps.ti_import1.Input.Text = rsp.s2c_context
                    GameAlertManager.Instance:ShowNotify(Util.GetText('common_setover'))
                    if self.cb then
                        self.cb(rsp.s2c_context)
                    end
                else
                    self.ui.comps.ti_import1.Input.Text = self.lastRecruit
                end
            end)
        end
    end

    self.ui.comps.btn_ok.TouchClick = function( ... )
        self.ui.menu:Close()
    end
end

return _M