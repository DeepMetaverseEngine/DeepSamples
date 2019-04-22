local _M = {}
_M.__index = _M


--充值返利活动
--只针对1服开放活动，CB1CB2期间充值过的玩家可领取奖励
--Editor By YuHua

local function ParserAccount(str)
    local src = string.split(str, ':')
    local ret = src[2]
    return ret
end

function _M.OnEnter( self )
   -- local accumulativecount = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.ACCUMULATIVECOUNT)

   	--print("Activity_20001 OnEnter ")
    self.btn_get.TouchClick = function(sender)
    	 --print("btnget onclick")
        --send request
        local req = {}
       	Protocol.RequestHandler.ClientGetRechargeRebateRewardReqeust(req, function(rsp)
       		--print_r("Receive ClientGetRechargeRebateRewardReqeust = ",rsp)
            if rsp ~= nil and rsp:IsSuccess() then
                    self.btn_get.Visible = false
                    self.lb_got.Visible = true
                    DataMgr.Instance.UserData:SetFreeData(self.recordFlag,"1")
            end
       	end)
    end

end

local function InitRecord(self)
    self.recordKey  = nil
    self.recordFlag = nil

    local serverID = DataMgr.Instance.UserData.ServerID
    local cfgData = unpack(GlobalHooks.DB.Find('return_config',{server_id = serverID}))
    --print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
    if cfgData ~= nil then
    --print("OnRecieve ClientGetRechargeRebateRewardResponse")
        self.recordFlag = cfgData.record_flag
        local req = {c2s_key = self.recordFlag}
        Protocol.RequestHandler.GetAccountExtTargetKeyValueRequest(req,function(rsp)
            if rsp ~= nil and rsp:IsSuccess() then
                self.recordKey = rsp.s2c_value
                --print("========================================= recordKey ",self.recordKey)
                if self.recordKey ~= nil then--账号其他角色已领取奖励
                    DataMgr.Instance.UserData:SetFreeData(cfgData.record_flag,"1")
                    self.btn_get.Visible = false
                    self.lb_got.Visible = true
                else
                    self.btn_get.Visible = true
                    self.lb_got.Visible = false
                end
            end
        end)
    end



end

function _M.OnInit( self )
 
 	--领取按钮
    self.btn_get=self.ui.comps.btn_get
    --已领取
    self.lb_got=self.ui.comps.lb_got
    self.btn_get.Visible = true
    self.lb_got.Visible = false

    InitRecord(self)

    --返利金额
    self.lb_returngold=self.ui.comps.lb_returngold
 	local accountID = DataMgr.Instance.UserData.AccountID
 	--print("src account = "..accountID)
 	local account = ParserAccount(accountID)
 	--print("parser account = "..account)
   	local rechargeData = GlobalHooks.DB.Find('return_content',{platform_account=account})
    if rechargeData ~= nil then
    	local detail = unpack(rechargeData)
        if detail ~= nil then
            local v = detail.price
            basegold = v / 100 * GameUtil.GetIntGameConfig("paymentParticle")
        --print("充值金额 = ",basegold)
            local serverID =  DataMgr.Instance.UserData.ServerID
         --Mock
         --serverID = "1"
            local cfgData = unpack(GlobalHooks.DB.Find('return_config',{server_id = serverID}))
            local god = 0
            if cfgData then
                god = basegold * cfgData.leverage
            end
        --print("return god = "..god)
            self.lb_returngold.Text = god
        else 
            self.lb_returngold.Text = 0
        end
    end
end

function _M.OnExit( self )
   
end

return _M