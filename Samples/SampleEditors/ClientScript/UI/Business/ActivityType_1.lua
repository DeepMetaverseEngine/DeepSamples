--- 周卡
local _M = {}
_M.__index = _M

local UIUtil = require('UI/UIUtil')
local Util = require('Logic/Util')
local RechargeModel=require('Model/RechargeModel')
local ItemModel = require('Model/ItemModel')
local ServerTime = require( 'Logic/ServerTime')
local BusinessModel = require 'Model/BusinessModel'

function _M.OnInit(self,params)
    self.activityType=params.activitytype
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end


--通过购买凭证查询每日奖励
local function InquiryRewardItemByKey(key)
    local timerechargeData=unpack(GlobalHooks.DB.Find('RechargeActivity',{flag = key}))
    return timerechargeData
end


--通过购买id，查询购买次数/剩余时间
local function InquiryBuyCount(self,payid)
    for i, v in ipairs(self.productinfo) do
        if v.s2c_productID==payid then
            return v
        end
    end
end


local function SetDetail(self,node,index)

    local productid=self.card[index].id

    local lb_name=node:FindChildByEditName('lb_name',true)
    local ib_reward=node:FindChildByEditName('ib_reward',true)
    local btn_get=node:FindChildByEditName('btn_get',true)
    local lb_un=node:FindChildByEditName('lb_un',true)
    local lb_use=node:FindChildByEditName('lb_use',true)
    local cvs_present=node:FindChildByEditName('cvs_present',true)
    local ib_tu=node:FindChildByEditName('ib_tu',true)
    local lb_num=node:FindChildByEditName('lb_num',true)
    --奖励说明
    local lb_tips1=node:FindChildByEditName('lb_tips1',true)
    local lb_tips3=node:FindChildByEditName('lb_tips3',true)
    lb_tips1.Text=Util.GetText('business_activity_day',self.card[index].last_time)
    local bounsitem= InquiryRewardItemByKey(self.card[index].flag)
    local item=ItemModel.GetDetailByTemplateID(bounsitem.reward.item.id[1])
    local name=Util.GetText(item.static.name)
    lb_tips3.Text=Util.GetText('business_activity_reward',bounsitem.reward.item.num[1],name)

    lb_name.Text=Util.GetText(self.card[index].name)
    UIUtil.SetImage(ib_reward,self.card[index].pic_res,false, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)

    --是否首次购买
    local first=InquiryBuyCount(self,productid)

    cvs_present.Visible=true
    local defaultcost=ItemModel.GetDetailByTemplateID(self.card[index].reward.item.id[1])
    MenuBase.SetImageBox(ib_tu,'static/item/'..defaultcost.static.atlas_id,UILayoutStyle.IMAGE_STYLE_BACK_4, 8)
    lb_num.Text=self.card[index].reward.item.num[1]

    local lb_got=node:FindChildByEditName('lb_got',true)
    
    --是否激活周卡
    local timeSpan =  first.s2c_leftTimeUTC:Subtract(ServerTime.getServerTime())
    --确定按钮是否置灰/激活
    lb_got.Visible=timeSpan.TotalSeconds > 0 and first.s2c_available == false
    btn_get.IsGray = timeSpan.TotalSeconds > 0 and first.s2c_available == false
    btn_get.IsInteractive = timeSpan.TotalSeconds <= 0 or first.s2c_available==true
 
    local lb_getred=node:FindChildByEditName('lb_getred',true)
    local show= (timeSpan.TotalSeconds >=0 and first.s2c_available == true) and 1 or 0
    GlobalHooks.UI.ShowRedPoint(lb_getred,show,'TimeCard')

    --未激活状态
    if timeSpan.TotalSeconds <= 0 then
        lb_un.Visible=true
        lb_use.Visible=false
        btn_get.Text = Util.GetText('business_activity_showprice',self.card[index].show_price)
        btn_get.TouchClick=function(sender)
            local needsignature = OneGameSDK.Instance:GetPlatformData():GetInt(SDKAttName.NEED_SIGN)
            local queryOrder = OneGameSDK.Instance:GetPlatformData():GetInt(SDKAttName.QUERY_ORDER)
            RechargeModel.RequestRechargeBySellId(productid,RechargeModel.GetRechargePlatformID(false),needsignature,queryOrder,function (rsp)
                local ord=rsp.s2c_ext_data
                RechargeModel.SDKPayment(ord,queryOrder,RechargeModel.GetRechargePlatformID(false))
                --红点
--[[                BusinessModel.cachedata[self.activityType][self.activityId][self.card[index].id] =1
                BusinessModel.cachedata[self.activityType][self.activityId].count= BusinessModel.cachedata[self.activityType][self.activityId].count + 1
                GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(self.activityType),BusinessModel.cachedata[self.activityType][self.activityId].count,self.activityId)]]
                self.parentui.pan:RefreshShowCell()
            end)
        end
    else --已激活
        lb_un.Visible=false
        lb_use.Visible=true
        --红点
        BusinessModel.cachedata[self.activityType][self.activityId][self.card[index].id] =1
        GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(self.activityType),BusinessModel.cachedata[self.activityType][self.activityId].count,self.activityId)
       
        lb_use.Text=Util.GetText('business_activity_lastday',math.floor(timeSpan.TotalDays) + 1)
        btn_get.Text=Util.GetText(Constants.Vip.Get)
        btn_get.TouchClick=function()
            RechargeModel.RequestGetTimeRechargeReward(productid,function (rsp)
                --领取成功，改变状态
                self.productinfo[index].s2c_available=false
                GlobalHooks.UI.ShowRedPoint(lb_getred,0,'TimeCard')
                lb_got.Visible=true
                btn_get.IsGray = true
                btn_get.IsInteractive = false
                GlobalHooks.UI.OpenUI('GainItem',0,bounsitem.reward.item,3)
                --红点
                BusinessModel.cachedata[self.activityType][self.activityId][self.card[index].id] = 0
                BusinessModel.cachedata[self.activityType][self.activityId].count = BusinessModel.cachedata[self.activityType][self.activityId].count - 1
                GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(self.activityType),BusinessModel.cachedata[self.activityType][self.activityId].count,self.activityId)
                self.parentui.pan:RefreshShowCell()
            end)
        end
    end
end


--收到通知刷新数据和界面
local function UpdateUI(self,eventname,params)
    self.productinfo = params.buyinfo
    self.ui.comps.sp_libao:RefreshShowCell()
end


function _M.OnEnter(self,params)
    self.activityId=params or 9

    function _M.OnUpdateUI(eventname, params)
        UpdateUI(self,eventname, params)
    end
    EventManager.Subscribe('Event.Recharge.TimeRechargeUIUpdate',_M.OnUpdateUI)

    --请求充值信息
    RechargeModel.RequestTimeRechargeInfo(OneGameSDK.Instance.PlatformID,function(rsp)
        self.productinfo=rsp.s2c_list
        self.card={}
        for i, v in ipairs(rsp.s2c_list) do
            local cardinfo= RechargeModel.GetShowCardById(v.s2c_productID)
            table.insert(self.card,cardinfo)
        end
        table.sort(self.card,function (a,b)
            return a.order < b.order
        end)
        self.ui.comps.cvs_info.Visible=false
        local function UpdateElement(node,index)
            SetDetail(self,node,index)
        end
        UIUtil.ConfigHScrollPan(self.ui.comps.sp_libao, self.ui.comps.cvs_info,#self.card,UpdateElement)
    end)
end


function _M.OnExit(self)
    EventManager.Unsubscribe('Event.Recharge.TimeRechargeUIUpdate',_M.OnUpdateUI)
end


return _M