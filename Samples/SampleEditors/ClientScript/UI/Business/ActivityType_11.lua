local _M = {}
_M.__index = _M


function _M.OnEnter( self )
    local accumulativecount = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.ACCUMULATIVECOUNT)
    self.lb_nownum.Text = accumulativecount
    self.lb_returnnum.Text = accumulativecount * 6

   --临时跳转充值
    self.btn_pay.TouchClick = function(sender)
        GlobalHooks.UI.OpenUI('Recharge',0,'RechargePay')
    end

    --临时跳转衣柜
	self.btn_pay1.TouchClick = function(sender)
        GlobalHooks.UI.OpenUI('WardrobeMain',0)
    end

    --临时跳转商城
    self.btn_pay2.TouchClick = function(sender)
        GlobalHooks.UI.OpenUI('Recharge',0,'RechargeShop')
    end


end

function _M.OnInit( self )
    self.btn_pay=self.root:FindChildByEditName('btn_pay',true)
    self.btn_pay1=self.root:FindChildByEditName('btn_pay1',true)
    self.btn_pay2=self.root:FindChildByEditName('btn_pay2',true)
    self.lb_nownum=self.root:FindChildByEditName('lb_nownum',true)
    self.lb_returnnum=self.root:FindChildByEditName('lb_returnnum',true)
end

function _M.OnExit( self )
   
end

return _M