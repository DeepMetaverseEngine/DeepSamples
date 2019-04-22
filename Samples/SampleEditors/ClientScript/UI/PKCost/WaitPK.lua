---作者：任祥建
---时间：2019/2/21
---WaitPK
local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'



function _M.OnEnter( self )

    --ui穿透
    self.ui:EnableTouchFrameClose(false)
    self.globalTouchKey = GameGlobal.Instance.FGCtrl:AddGlobalTouchUpHandler("UI.ItemDetail", function( obj, point ) end)

    if not self.text then
        self.text = self.lb_tip2.Text
    end

    self.Update = function(cd)
        self.lb_tip2.Text = Util.Format1234(self.text,cd)
        if cd<=0 then
            self:Close()
        end
    end
end

function _M.OnInit( self )
    self.lb_tip2 = self.root:FindChildByEditName('lb_tip2', true)

    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
end

function _M.OnExit( self )
    --if self.timer then
    --    LuaTimer.Delete(self.timer)
    --    self.timer = nil
    --end
end

return _M