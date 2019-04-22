---作者：任祥建
---时间：2019/2/23
---RenameGuild
local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'


function _M.OnEnter( self ,bagIndex)
    self.ti_new.Input.Text = ""
    self.ti_new.Input.characterLimit = 10
    self.lb_nowname.Text = DataMgr.Instance.UserData.GuildName



    self.bt_yes.TouchClick = function(sender)
        if string.IsNullOrEmpty(self.ti_new.Input.Text) then
            GameAlertManager.Instance:ShowNotify(Util.GetText("name_cant_empty"))
            return
        end
        local msg = {c2s_name = self.ti_new.Input.Text,c2s_change_type = 1,
                     c2s_item_index = bagIndex}
        Protocol.RequestHandler.ClientChangeRoleNameRequest(msg, function(rsp)
            GameAlertManager.Instance:ShowNotify(Util.GetText("rename_success"))
            self:Close()
        end)
    end

    self.bt_no.TouchClick = function(sender)
        self:Close()
    end

end

function _M.OnInit( self )

    self.lb_nowname = self.root:FindChildByEditName('lb_nowname', true)
    self.ti_new = self.root:FindChildByEditName('ti_new', true)
    self.bt_yes = self.root:FindChildByEditName('bt_yes', true)
    self.bt_no = self.root:FindChildByEditName('bt_no', true)


    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
end

function _M.OnExit( self )

end

return _M