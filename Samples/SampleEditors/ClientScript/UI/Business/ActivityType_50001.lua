local _M = {}
_M.__index = _M
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local BusinessModel = require 'Model/BusinessModel'
local RechargeModel = require 'Model/RechargeModel'
local ServerTime = require 'Logic/ServerTime'



local myself = nil


local function CheckRedShow(self)
    if not BusinessModel.cachedata[self.activitytype] then
        BusinessModel.cachedata[self.activitytype] = {}
    end
    if not BusinessModel.cachedata[2][self.activityid] then
        BusinessModel.cachedata[2][self.activityid] = {}
    end

    local count = 0
    for i, v in ipairs(self.staticdata) do
        if v.state == 1 and not self.isover then
            BusinessModel.cachedata[2][50001][v.id] = 1
        else
            BusinessModel.cachedata[2][50001][v.id] = 0
        end
        count = count + BusinessModel.cachedata[2][50001][v.id]
    end
    BusinessModel.cachedata[2][50001].count = count
    GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(2),count,50001)
    self.parentui.pan:RefreshShowCell()
end

local function InitCompentent(self)
    self.text1 = self.cvs_info:FindChildByEditName('lb_give', true)
    self.text2 = self.cvs_info:FindChildByEditName('lb_canget', true)
    self.text3 = self.cvs_info:FindChildByEditName('lb_lastday', true)
    self.cvs_help.Visible = false
    self.cvs_info.Visible = false
    self.btn_help.TouchClick = function(sender)
        self.cvs_help.Visible = true
    end
    self.cvs_help.TouchClick = function(sender)
        sender.Visible = false
    end
end

local function SetNodeVisible(btns, state)
    for i, v in pairs(btns) do
        if state == i then
            v.Visible = true
        else
            v.Visible = false
        end
    end
end

local function InitList(self,node,index,data)
    local lb_name = node:FindChildByEditName('lb_name', true)
    local cvs_unbuy = node:FindChildByEditName('cvs_unbuy', true)
    local cvs_bought = node:FindChildByEditName('cvs_bought', true)
    local lb_canget = node:FindChildByEditName('lb_canget', true)
    local lb_give = node:FindChildByEditName('lb_give', true)
    local lb_lastday = node:FindChildByEditName('lb_lastday', true)
    local btns = {}
    btns[0] = node:FindChildByEditName('btn_buy', true)
    btns[1] = node:FindChildByEditName('btn_get', true)
    btns[2] = node:FindChildByEditName('lb_got', true)
    btns[3] = node:FindChildByEditName('lb_success', true)
    btns[4] = node:FindChildByEditName('lb_over', true)
    local picnode = {}
    for i = 1, 5 do
        picnode[i] = node:FindChildByEditName('cvs_state'..i, true)
    end

    lb_name.Text = Util.GetText(data.name)
    cvs_unbuy.Visible = data.state == 0
    cvs_bought.Visible = data.state ~= 0
    btns[0].Text = Util.GetText(data.show_price)
    lb_give.Text = Util.Format1234(self.text1.Text,data.show_give)
    lb_canget.Text = Util.Format1234(self.text2.Text,data.show_get)
    lb_lastday.Text = Util.Format1234(self.text3.Text,data.counts-data.times)
    local imagenode = picnode[1]:FindChildByEditName('ib_state1', true)
    UIUtil.SetImage(imagenode,data.pic_res,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)

    if self.isover and data.state == 0 then
        SetNodeVisible(btns,4)
    else
        SetNodeVisible(btns,data.state)
    end
    
    local showindex = 1
    if data.state == 3 then
        showindex = 5
    elseif data.state ~= 0 then
        showindex = 2 + data.times
    end
    SetNodeVisible(picnode,showindex)


    btns[0].TouchClick = function(sender)
        local queryOrder = OneGameSDK.Instance:GetPlatformData():GetInt(SDKAttName.QUERY_ORDER)
        RechargeModel.RequestRechargeBySellId(data.productid,OneGameSDK.Instance.PlatformID,needsignature,queryOrder,function (rsp)
            local ord=rsp.s2c_ext_data
            RechargeModel.SDKPayment(ord,queryOrder,OneGameSDK.Instance.PlatformID)
        end)
    end
    btns[1].TouchClick = function(sender)
        local request = {c2s_productID = data.productid}
        Protocol.RequestHandler.ClientGetPlantingRewardRequest(request, function(rsp)
            data.times = rsp.s2c_times
            data.state = rsp.s2c_state
            self.sp_libao:RefreshShowCell()
            CheckRedShow(self)
        end)
    end
end

local function PayResult(key,productinfo)
    for i, v in ipairs(productinfo.s2c_list) do
        myself.staticdata[i].productid = v.s2c_productID
        myself.staticdata[i].times = v.s2c_times
        myself.staticdata[i].state = v.s2c_state
        myself.sp_libao:RefreshShowCell()
        CheckRedShow(myself)
    end
end

function _M.OnEnter( self,activityid,subindex )
    local today = ServerTime.getServerTime():ToLocalTime()
    local overtime = System.DateTime.Parse("2019-3-14 4:00:00")
    self.isover = overtime < today
    self.activityid = activityid
    myself = self
    EventManager.Subscribe('Event.Recharge.PlantingBuy',PayResult)
    InitCompentent(self)

    local request = {c2s_platformID = OneGameSDK.Instance.PlatformID}
    Protocol.RequestHandler.ClientGetPlantingInfoRequest(request, function(rsp)
        for i, v in ipairs(rsp.s2c_list) do
            self.staticdata[i].productid = v.s2c_productID
            self.staticdata[i].times = v.s2c_times
            self.staticdata[i].state = v.s2c_state
        end
        CheckRedShow(self)
        UIUtil.ConfigHScrollPan(self.sp_libao,self.cvs_info,3,function(node,index)
            InitList(self,node,index,self.staticdata[index])
        end)
    end)
    
end

function _M.OnInit( self,params )
    self.activitytype = params.activitytype
    self.staticdata = params
    self.sp_libao = self.root:FindChildByEditName('sp_libao', true)
    self.cvs_info = self.root:FindChildByEditName('cvs_info', true)
    self.cvs_help = self.root:FindChildByEditName('cvs_help', true)
    self.btn_help = self.root:FindChildByEditName('btn_help', true)
    
    BusinessModel.FirstRed[2][50001] = false
end

function _M.OnExit( self )
    EventManager.Unsubscribe('Event.Recharge.PlantingBuy',PayResult)
    CheckRedShow(self)
end

return _M