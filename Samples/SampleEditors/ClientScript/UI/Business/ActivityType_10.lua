local _M = {}
_M.__index = _M


function _M.OnEnter( self )
    
    self.btn_get.TouchClick = function(sender)
        local text = self.ti_code.Text
        if not string.IsNullOrEmpty(text) then
            local msg = {c2s_PlatformID = OneGameSDK.Instance.PlatformID,c2s_CDkey = text}
            Protocol.RequestHandler.ClientGetCDKeyRewardRequest(msg, function(rsp)
                GameAlertManager.Instance:ShowNotify(Constants.Business.GetSuccess)
            end)
        else
            GameAlertManager.Instance:ShowNotify(Constants.Business.EnterError)
        end
        
    end
    
end

function _M.OnInit( self )
    self.ti_code = self.root:FindChildByEditName('ti_code', true)
    self.btn_get = self.root:FindChildByEditName('btn_get', true)
end

function _M.OnExit( self )
    
end

return _M