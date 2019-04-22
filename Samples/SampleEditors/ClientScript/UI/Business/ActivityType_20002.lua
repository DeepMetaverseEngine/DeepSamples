local _M = {}
_M.__index = _M

local BusinessModel = require 'Model/BusinessModel'
local UIUtil = require 'UI/UIUtil'


local function ParserAccount(str)
    local src = string.split(str, ':')
    local ret = src[2]
    return ret
end

local function InitRecord(self)
    self.recordKey  = nil
    self.recordFlag = nil

    local serverID = DataMgr.Instance.UserData.ServerID
    local cfgData = unpack(GlobalHooks.DB.Find('join_config',{server_id = serverID}))

    if cfgData ~= nil then
        self.recordFlag = cfgData.record_flag
        local req = {c2s_key = self.recordFlag}
        Protocol.RequestHandler.GetAccountExtTargetKeyValueRequest(req,function(rsp)
            if rsp ~= nil and rsp:IsSuccess() then
                self.recordKey = rsp.s2c_value
                if self.recordKey ~= nil then--账号其他角色已领取奖励
                    DataMgr.Instance.UserData:SetFreeData(self.recordFlag,"1")
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

function _M.OnEnter( self )
    self.staticdata = BusinessModel.GetCommonActivity(self.params.sheet_name)
    local accountID = DataMgr.Instance.UserData.AccountID
    local account = ParserAccount(accountID)
    local rechargeData = unpack(GlobalHooks.DB.Find('join_content_cb1',{platform_account=account}))
    local gettype = 0
    if rechargeData then
        gettype = 1
    else
        rechargeData = unpack(GlobalHooks.DB.Find('join_content_cb2',{platform_account=account}))
        if rechargeData then
            gettype = 2
        end
    end
    if gettype ~= 0 then
        UIUtil.SetItemShowTo(self.cvs_item1,self.staticdata[gettype].reward.item.id[1],self.staticdata[gettype].reward.item.num[1])
        UIUtil.SetItemShowTo(self.cvs_item2,self.staticdata[gettype].reward.item.id[2],self.staticdata[gettype].reward.item.num[2])
        self.btn_get.TouchClick = function(sender)
            local msg = {c2s_type = gettype}
            Protocol.RequestHandler.ClientGetCBRewardRequest(msg, function(rsp)
                sender.Visible = false
                self.lb_got.Visible = true
                DataMgr.Instance.UserData:SetFreeData(self.recordFlag,"1")
            end)
        end
        self.cvs_item1.TouchClick = function(sender) 
            UIUtil.ShowTips(self,sender,self.staticdata[gettype].reward.item.id[1])
        end
        self.cvs_item2.TouchClick = function(sender)
            UIUtil.ShowTips(self,sender,self.staticdata[gettype].reward.item.id[2])
        end
    end
end

function _M.OnInit( self,params )
    self.params = params
    self.cvs_item1=self.root:FindChildByEditName('cvs_item1',true)
    self.cvs_item2=self.root:FindChildByEditName('cvs_item2',true)
    self.btn_get=self.root:FindChildByEditName('btn_get',true)
    self.lb_got=self.root:FindChildByEditName('lb_got',true)
    
    InitRecord(self)
end

function _M.OnExit( self )
   
end

return _M