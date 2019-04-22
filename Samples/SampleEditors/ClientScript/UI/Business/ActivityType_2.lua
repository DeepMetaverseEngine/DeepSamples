--- 每日礼包
local _M = {}
_M.__index = _M

local RechargeModel=require('Model/RechargeModel')
local UIUtil = require('UI/UIUtil')
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

local gainindex

function _M.OnInit(self)

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

    local productid=self.dailyinfo[index].id

    local lb_name=node:FindChildByEditName('lb_name',true)
    lb_name.Text=Util.GetText(self.dailyinfo[index].name)
    local cvs_item={}
    for i = 1, #self.dailyinfo[index].reward.item.id do
        cvs_item[i]=node:FindChildByEditName('cvs_item'..i,true)
        local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(self.dailyinfo[index].reward.item.id[i]))
        local quality = itemdetail.static.quality
        local icon=itemdetail.static.atlas_id
        local number=tonumber(self.dailyinfo[index].reward.item.num[i])
        local itshow=UIUtil.SetItemShowTo(cvs_item[i],icon,quality,number)
        itshow.EnableTouch = true
        itshow.TouchClick = function()
            local detail = UIUtil.ShowNormalItemDetail({x=node.X+250,y=node.Y+170,detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})
        end
    end

    local lb_tips=node:FindChildByEditName('lb_tips',true)
    lb_tips.Text=Util.GetText('business_activity_getprice',self.dailyinfo[index].show_cost)

    --限购次数 暂时不用了
    local lb_limit=node:FindChildByEditName('lb_limit',true)
    local first=InquiryBuyCount(self,productid)
    lb_limit.Text =Util.GetText('business_activity_daily_buy',self.dailyinfo[index].limit_num - first.s2c_buyCount,self.dailyinfo[index].limit_num)

    local btn_buy=node:FindChildByEditName('btn_buy',true)
    local lb_got=node:FindChildByEditName('lb_got',true)
    lb_got.Visible=first.s2c_buyCount >= self.dailyinfo[index].limit_num
    btn_buy.IsInteractive= not lb_got.Visible

    btn_buy.Text=Util.GetText('business_activity_showprice',self.dailyinfo[index].show_price)
    btn_buy.TouchClick=function(sender)
        local needsignature = OneGameSDK.Instance:GetPlatformData():GetInt(SDKAttName.NEED_SIGN)
        local queryOrder = OneGameSDK.Instance:GetPlatformData():GetInt(SDKAttName.QUERY_ORDER)
        RechargeModel.RequestRechargeBySellId(productid,OneGameSDK.Instance.PlatformID,needsignature,queryOrder,function (rsp)
            local ord=rsp.s2c_ext_data
            gainindex=index
            RechargeModel.SDKPayment(ord,queryOrder,OneGameSDK.Instance.PlatformID)
        end)
    end
end


--收到通知刷新数据和界面
local function UpdateUI(self,eventname,params)
    self.productinfo = params.buyinfo
    self.ui.comps.sp_libao:RefreshShowCell()
    GlobalHooks.UI.OpenUI('GainItem',0,self.dailyinfo[gainindex].reward.item,3)
end


function _M.OnEnter(self)

    function _M.OnUpdateUI(eventname, params)
        UpdateUI(self,eventname, params)
    end
    EventManager.Subscribe('Event.Recharge.DailyRechargeUIUpdate',_M.OnUpdateUI)

    --请求每日礼包充值信息
    RechargeModel.RequestDailyRechargeInfo(OneGameSDK.Instance.PlatformID,function(rsp)
        self.productinfo=rsp.s2c_list
        self.dailyinfo={}
        for i, v in ipairs(rsp.s2c_list) do
            local dailyinfo= RechargeModel.GetShowDailyRechargeInfo(v.s2c_productID)
            table.insert(self.dailyinfo,dailyinfo)
        end
        table.sort(self.dailyinfo,function (a,b)
            return a.order < b.order
        end)
        self.ui.comps.cvs_info.Visible=false
        local function UpdateElement(node,index)
            SetDetail(self,node,index)
        end
        UIUtil.ConfigHScrollPan(self.ui.comps.sp_libao, self.ui.comps.cvs_info,#self.dailyinfo,UpdateElement)
    end)
end


function _M.OnExit(self)
    EventManager.Unsubscribe('Event.Recharge.DailyRechargeUIUpdate',_M.OnUpdateUI)
end


return _M