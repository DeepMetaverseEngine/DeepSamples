---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2018/9/11 9:38
---
local _M = {}
_M.__index = _M

local RechargeModel=require('Model/RechargeModel')
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

-----------------------------------ScrollPan------------------------------------------
local function ConfigPageScrollPan(self,pages, CreateCB,UpdateCB)
    local function CreateListItem(Scrollable,i)
        local node = self.ui.comps.tb_tex:Clone()
        node.Scrollable=true
        CreateCB(node,i)
        return node
    end
    local s = self.ui.comps.tb_tex.Size2D
    self.ui.comps.sp_tex:Initialize(pages, s, CreateListItem,UpdateCB)
end

local function ShowScrollPan(self)
    
    local function eachCreateCb(node, index)
        index=index+1
        node.UnityRichText=string.gsub(Util.GetText(self.allvipinfo[index].vip_tips), '\\n', '\n')
    end
    
    local function eachUpdateCB(index)
        index=index+1

        if index ==1 then --控制起始位置
            self.ui.comps.btn_left.Visible = false
            self.ui.comps.btn_right.Visible = true
        elseif index == #self.allvipinfo then
            self.ui.comps.btn_left.Visible = true
            self.ui.comps.btn_right.Visible = false
        else
            self.ui.comps.btn_left.Visible = true
            self.ui.comps.btn_right.Visible = true
        end
        --限制vip查看等级
        local vipLv= self.par or DataMgr.Instance.UserData.VipLv or 0
        local limit=RechargeModel.GetVipLevelInfo(vipLv)
        if index >=limit.check_limit+1 then
            index=limit.check_limit+1
            self.ui.comps.btn_right.Visible=false
        end
        
    end
    ConfigPageScrollPan(self,#self.allvipinfo,eachCreateCb,eachUpdateCB)
end
-----------------------------------ScrollPan------------------------------------------


--释放模型
local function Release3DModel(self)
    if self.model then
        UI3DModelAdapter.ReleaseModel(self.model.Key)
        self.model = nil
    end
end


--加载模型
local function Init3DEffect(self)
    --拆分坐标&偏移坐标
    local fixposdata=string.split(self.vipinfo.pos_xy,',')
    local fixpos = {x = tonumber(fixposdata[1]),y = tonumber(fixposdata[2])}

    local info = UI3DModelAdapter.AddSingleModel(
            self.ui.comps.cvs_anchor,
            Vector2(fixpos.x,fixpos.y),
            tonumber(self.vipinfo.zoom),
            self.ui.menu.MenuOrder,
            self.vipinfo.npc_model)
    self.model = info
    --初始旋转角度
    info.Callback = function (info2)
        local trans2 = info2.RootTrans
        trans2:Rotate(Vector3.up,self.vipinfo.rotate)
    end
end


--检查左右箭头红点
local function CheckArrowRedPoint(self)
    for i = 0,#self.reward do
        if self.reward[i] == true and self.viplevel > i then
            GlobalHooks.UI.ShowRedPoint(self.comps.lb_redleft,1,'leftarrow')
            break
        else
            GlobalHooks.UI.ShowRedPoint(self.comps.lb_redleft,0,'leftarrow')
        end
    end
    for i = 0, #self.reward do
        if self.reward[i] == true and self.viplevel < i then
            GlobalHooks.UI.ShowRedPoint(self.comps.lb_redright,1,'rightarrow')
            break
        else
            GlobalHooks.UI.ShowRedPoint(self.comps.lb_redright,0,'rightarrow')
        end
    end
end

--显示所有特权
local function ShowAllPrivilege(self)
    self.ui.comps.cvs_touch.Visible=true
    local tb_texall=self.ui.comps.cvs_touch:FindChildByEditName('tb_texall',true)
    tb_texall.Scrollable = true
    tb_texall.Scroll.Container.Position2D =Vector2(0,0)
    tb_texall.UnityRichText=string.gsub(Util.GetText(self.vipinfo.vip_alltips), '\\n', '\n')
   self.ui.comps.cvs_touch.TouchClick=function(sender)
        self.ui.comps.cvs_touch.Visible=false
    end
end


--初始化标题
local function  InitTitle(self,node)
    
    --查看全部特权
    local btn_show=node:FindChildByEditName('btn_show',true)
    btn_show.TouchClick=function()
        ShowAllPrivilege(self)
    end
    
    --前往充值
    local btn_pay=node:FindChildByEditName('btn_pay',true)
    btn_pay.TouchClick=function()
        GlobalHooks.UI.FindUI('RechargeFrame').ui.root:FindChildByEditName('tbt_an2',true).IsChecked=true
    end
    
    --Vip经验条
    local gg_vip=node:FindChildByEditName('gg_vip',true)
    gg_vip.Text=DataMgr.Instance.UserData.VipCurExp..'/'..self.vipgift.vip_exp
    if self.vipgift.vip_exp ==0 then
        gg_vip.Value=0
    else
        gg_vip.Value=((DataMgr.Instance.UserData.VipCurExp/self.vipgift.vip_exp)*100 <100 and {(DataMgr.Instance.UserData.VipCurExp/self.vipgift.vip_exp)*100} or {100})[1]
    end
    if DataMgr.Instance.UserData.VipLv > self.viplevel then
        gg_vip.Value=100
    end
    
    --Vip阶数
    local cvs_vip12=node:FindChildByEditName('cvs_vip12',true)
    local cvs_vip13=node:FindChildByEditName('cvs_vip13',true)
    local  lb_vipnum=cvs_vip12:FindChildByEditName('lb_vipnum',true)
    lb_vipnum.Text=Util.GetText(self.vipinfo.vip_name)
    lb_vipnum.FontColorRGB = Constants.Color.White
    cvs_vip12.Visible=self.viplevel ~= 13
    cvs_vip13.Visible=self.viplevel == 13
    
    --Vip图标
    local ib_vip=node:FindChildByEditName('ib_vip',true)
    UIUtil.SetImage(ib_vip,self.vipinfo.vip_pic)
end


--设置vip页面礼物详情
local function SetGiftBouns(self,node,index)
    --微调奖励物品位置
    node.Transform.localPosition=Vector2(node.Transform.localPosition.x+5,-4.5)
    local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(self.vipgift.show.item[index]))
    local quality = itemdetail.static.quality
    local icon=itemdetail.static.atlas_id
    local num =self.vipgift.show.itemnum[index]
    local itshow=UIUtil.SetItemShowTo(node,icon,quality,num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        SoundManager.Instance:PlaySoundByKey('button',false)
            UIUtil.ShowNormalItemDetail({x=node.X+359,y=node.Y+280,detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})
    end
end


--初始化礼物
local function InitGift(self,node)
    
    local lb_buy=node:FindChildByEditName('lb_buy',true)
    local isShowRed =self.reward[self.viplevel] == true and 1 or 0
    GlobalHooks.UI.ShowRedPoint(lb_buy, isShowRed, 'getbouns')
    
    local cvs_item=node:FindChildByEditName('cvs_item',true)
    cvs_item.Visible=false
    local sp_libao=node:FindChildByEditName('sp_libao',true)
    
    local function eachupdatecb(node,index)
        SetGiftBouns(self,node,index)
    end
    UIUtil.ConfigHScrollPanWithOffset(sp_libao,cvs_item,#self.vipgift.show.item,10,eachupdatecb)
    
    --购买奖励物品
    local cvs_rget=node:FindChildByEditName('cvs_rget',true)
    local btn_buy=node:FindChildByEditName('btn_buy',true)
    local ib_money = node:FindChildByEditName('ib_money',true)
    local cost=ItemModel.GetDetailByTemplateID(self.vipgift.cost.id[1])
    UIUtil.SetImage(ib_money,'static/item/'..cost.static.atlas_id,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
    local lb_price=node:FindChildByEditName('lb_price',true)
    lb_price.Text =self.vipgift.cost.num[1]
    
    btn_buy.Text = self.viplevel ==0 and Util.GetText('vip_reward_get') or Util.GetText('vip_reward_buy')
    ib_money.Visible = self.viplevel ~= 0
    lb_price.Visible =self.viplevel ~= 0
    
    local lb_bought=node:FindChildByEditName('lb_bought',true)
    cvs_rget.Visible = self.reward[self.viplevel] == true or self.reward[self.viplevel] == nil 
    lb_bought.Visible=self.reward[self.viplevel]==false
    btn_buy.TouchClick=function(sender)
        if self.viplevel > DataMgr.Instance.UserData.VipLv then --至尊等级不够
            local notenoughlv=Util.GetText('vip_not_enough_level')
                GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, notenoughlv,'','',nil,
                        function ()
                            GlobalHooks.UI.FindUI('RechargeFrame').ui.root:FindChildByEditName('tbt_an2',true).IsChecked=true
                        end
                ,nil)
        else
            --判断钱够不够
            if ItemModel.CountItemByTemplateID(self.vipgift.cost.id[1]) < self.vipgift.cost.num[1] then
                local tag =''
                if self.vipgift.cost.id[1] == Constants.VirtualItems.Diamond then
                    tag = 'not_enough_gold'
                elseif self.vipgift.cost.id[1] == Constants.VirtualItems.Silver then
                    tag = 'not_enough_silver'
                elseif self.vipgift.cost.id[1] == Constants.VirtualItems.Copper then
                    tag ='not_enough_copper'
                end
                FunctionUtil.GotoInSameUI(tag)
            else
                RechargeModel.RequestGetVipReward(self.viplevel,function (rsp)
                    if rsp.s2c_code ==200 then --领取成功，将领取状态改变
                        self.reward[self.viplevel]=false
                        cvs_rget.Visible=false
                        lb_bought.Visible=true
                        
                        --红点
                        GlobalHooks.UI.ShowRedPoint(lb_buy,0,'getbouns')
--[[                        for i = 0, #self.reward do
                            if self.reward[i] == true then
                                GlobalHooks.UI.SetRedTips('vipgift_reward',1)
                                break
                            end
                            GlobalHooks.UI.SetRedTips('vipgift_reward',0)
                        end]]
                    end
                    
                    if rsp.s2c_msg ~=nil then
                        GameAlertManager.Instance:ShowNotify(Util.GetText(rsp.s2c_msg))
                    end
                end)
            end
        end
    end
end


--初始化谈话
local function InitTalk(self,node)
    
    local cvs_talk =node:FindChildByEditName('cvs_talk',true)
    local tb_talk=cvs_talk:FindChildByEditName('tb_talk',true)
    cvs_talk.Alpha=1

    local r=math.random(#self.vipinfo.talk.info)
    local time=self.vipinfo.talk.time[r]+self.vipinfo.talk_cd
    tb_talk.UnityRichText=Util.GetText(self.vipinfo.talk.info[r])

    self.time=LuaTimer.Add(
            0,
            1000,
            function()
                time=time-1000
                if time <=0 then --倒计时结束更换对话内容
                    if not self.vipinfo then
                        self.vipinfo=RechargeModel.GetVipLevelInfo(self.viplevel)
                    end
                    r=math.random(#self.vipinfo.talk.info)
                    tb_talk.UnityRichText=Util.GetText(self.vipinfo.talk.info[r])
                    time=self.vipinfo.talk.time[r]+self.vipinfo.talk_cd
                    cvs_talk.Alpha=1
                end
                if time < self.vipinfo.talk_cd then --小于间隔时间隐藏对话框
                    tb_talk.UnityRichText=''
                    cvs_talk.Alpha=0
                end
                return true
            end)
end


--翻页刷新vip信息
local function UpDateInfo(self)
    self.vipinfo=RechargeModel.GetVipLevelInfo(self.viplevel)
    self.vipgift=RechargeModel.GetVipLevelSet(self.viplevel)
    self.ui.comps.lb_vlevel.Text=self.viplevel
    
    --CheckArrowRedPoint(self)
    
    InitTitle(self,self.ui.comps.cvs_title)
    InitGift(self,self.ui.comps.cvs_info)
    Release3DModel(self)
    Init3DEffect(self)
    if self.time then
        LuaTimer.Delete(self.time)
        self.time = nil
    end
    InitTalk(self,self.ui.comps.cvs_info)
end


--初始化相关vip特权
local function InitInfo(self,node)
    
    local tb_tex=node:FindChildByEditName('tb_tex',true)
    tb_tex.Visible=false
    ShowScrollPan(self)

    InitGift(self,self.ui.comps.cvs_info)
    
    --通过vip等级显示对应的描述(限制self.viplevel的取值范围)
    self.ui.comps.sp_tex:ShowPage(self.viplevel)
    self.ui.comps.btn_left.TouchClick = function ( sender )
        self.ui.comps.btn_left.Visible = false
        self.viplevel= (self.viplevel <=0 and {0} or {self.viplevel-1})[1]
        self.ui.comps.sp_tex:ShowPrevPage()
        UpDateInfo(self)
    end
    self.ui.comps.btn_right.TouchClick = function ( sender )
        self.ui.comps.btn_right.Visible = false
        self.viplevel= (self.viplevel >=#self.allvipinfo-1 and {#self.allvipinfo-1} or {self.viplevel+1})[1]
        self.ui.comps.sp_tex:ShowNextPage()
        UpDateInfo(self)
    end
    
    local lb_vlevel=node:FindChildByEditName('lb_vlevel',true)
    lb_vlevel.Text=self.viplevel

    --CheckArrowRedPoint(self)
end


--初始化组件
local function InitComponent(self)
    InitTitle(self,self.ui.comps.cvs_title)
    InitInfo(self,self.ui.comps.cvs_info)
    Init3DEffect(self)
    InitTalk(self,self.ui.comps.cvs_info)
    --锁水平，放垂直
    self.ui.comps.sp_tex.Scrollable.Scroll.vertical=true
    self.ui.comps.sp_tex.Scrollable.Scroll.horizontal=false
end


function _M.OnInit(self)

end


function _M.OnEnter(self,params)
    GlobalHooks.UI.SetRedTips('vipgift_reward',0)
    self.allvipinfo=RechargeModel.GetAllVipInfo()
    if params then
        if params > 13 then
            params = 13
        end
        local limit=RechargeModel.GetVipLevelInfo(DataMgr.Instance.UserData.VipLv)
        if limit.check_limit >= params then
            self.par=params
            self.viplevel=params
        else
            self.viplevel = limit.check_limit
        end
    else
        self.viplevel= DataMgr.Instance.UserData.VipLv
    end
    self.vipinfo=RechargeModel.GetVipLevelInfo(self.viplevel)
    self.vipgift=RechargeModel.GetVipLevelSet(self.viplevel)
    RechargeModel.RequestVipInfo(function (rsp)
        self.reward=rsp.s2c_vip_reward_record
        InitComponent(self)
    end)
end


function _M.OnExit(self)
    Release3DModel(self)
    if self.time then
        LuaTimer.Delete(self.time)
        self.time = nil
    end
end


return _M