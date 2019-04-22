---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2018/12/14 10:22
---成就系统

local _M = {}
_M.__index = _M

local AchievementModel=require('Model/AchievementModel')
local Util=require('Logic/Util')
local UIUtil = require 'UI/UIUtil'
local ItemModel=require('Model/ItemModel')

local IsDispose = false


function _M.OnInit(self)

end

--子页签内的领取特效
local function PlayEffect(self,parent,pos,scale,filename,time,cb)
    local transSet = {}
    transSet.Pos = pos
    transSet.Scale = scale
    transSet.Parent = parent.Transform
    transSet.Layer = Constants.Layer.UI
    transSet.LayerOrder = 1000
    self.eff = self.eff or {}
    local num = #self.eff + 1
    self.eff[num] = Util.PlayEffect(filename,transSet,time,cb)
end

local function ReleaseEffect(self)
    if self.eff then
        for i, v in ipairs(self.eff) do
            RenderSystem.Instance:Unload(self.eff[i])
        end
        self.eff = {}
    end
end


--主页面上的领取特效
local function PlayTotalEffect(self,parent,pos,scale,filename)
    local transSet = {}
    transSet.Pos = pos
    transSet.Scale = scale
    transSet.Parent = parent.Transform
    transSet.Layer = Constants.Layer.UI
    transSet.LayerOrder = 1000
    self.total = self.total or {}
    local num =#self.total +1 
    self.total[num]= Util.PlayEffect(filename,transSet)
end

local function ReleaseTotalEffect(self)
    if self.total then
        for i, v in ipairs(self.total) do
            RenderSystem.Instance:Unload(self.total[i])
        end
        self.total = {}
    end
end


local function SelectByIndex(self, index)
    UIUtil.MoveToScrollCell(
            self.ui.comps.sp_itemlist,
            index,
            function(node)
            end
    )
end


--奖励等待领取和完成都返回true
local function CompletedOrNo(bool)
    if bool == AchievementModel.AchievementType.WaitForGetReward or bool ==AchievementModel.AchievementType.Finished then
        return true
    end
    return false
end


local function ShowRewardItem(node,itemid,num)
    --获取品质和数量
    local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(itemid))
    local itshow=UIUtil.SetItemShowTo(node,itemdetail,tonumber(num))
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        UIUtil.ShowNormalItemDetail({detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})
    end
end


--设置右边详细成就内容
local function InitCvs(self,data)

    ReleaseEffect(self)

    --删除复制出来的cvs
    if self.cvsicon then
        for i, v in ipairs(self.cvsicon) do
            v:RemoveFromParent(true)
        end
        self.cvsicon={}
    end

    local node = self.comps.cvs_info
    local ach=AchievementModel.GetAchDataByAchId(data.id) --成就id
    local isCompleted = CompletedOrNo(data.state) --是否完成(等待领取奖励)
    --描述&进度&隐藏成就(根据字符串长度显示*)
    local tb_requir=node:FindChildByEditName('tb_requir',true)
    if ach.Is_hidden ==1 and not isCompleted then
        local chang= math.modf(tonumber(#Util.GetText(ach.desc_show))/3)
        local str=''
        for i = 1, chang do
            str=str..'*'
        end
        tb_requir.UnityRichText=str
    else
        if data.progressList[1].TargetValue == 0 or data.progressList[1].TargetValue ==1 then
            tb_requir.UnityRichText=Util.GetText(ach.desc_show)
        else
            if data.progressList[1].CurValue >= data.progressList[1].TargetValue then --目标达成，绿色
                tb_requir.UnityRichText=Util.GetText(ach.desc_show)..' '..Util.GetText(Constants.Achievement.AchievementTargetText)
            else--未达到，红色
                tb_requir.UnityRichText=Util.GetText(ach.desc_show)..' '.."<color=#F01616FF>("..data.progressList[1].CurValue..'/'..data.progressList[1].TargetValue..')</color>'
            end
        end
    end
    
    --完成时间
    local lb_time1=node:FindChildByEditName('lb_time1',true)
    local time=GameUtil.FormatDateTime(data.finishTime:ToLocalTime(), "yyyy-MM-dd HH:mm")
        if  lb_time1.Visible == isCompleted and true or false then
            lb_time1.Text = time
        else
            lb_time1.Text = Util.GetText(Constants.Achievement.AchievementCompletionTips)
        end

    --奖励显示&领取
    local cvs_reward=node:FindChildByEditName('cvs_reward',true)
    local cvs_showreward2=node:FindChildByEditName('cvs_showreward2',true)
    cvs_showreward2.Visible=false
    cvs_reward.Visible= tonumber(ach.item_show[1]) ~= 0
    
    --根据奖励数量复制cvs
    for i = 1, #ach.item_show do
        if tonumber(ach.item_show[i]) ~= 0 then
            local cvs = cvs_showreward2:Clone()
            cvs_reward:AddChild(cvs)
            cvs.Position2D=Vector2((cvs_showreward2.Position2D.x+10)*i,cvs_showreward2.Position2D.y)
            table.insert(self.cvsicon,i,cvs)
            cvs.Visible=true
            ShowRewardItem(cvs,ach.item_show[i],ach.item_show_num[i])
        end
    end

    --领取奖励，以及刷新数据
    local btn_get2=node:FindChildByEditName('btn_get2',true)
    local lb_bought = node:FindChildByEditName('lb_bought',true)
    btn_get2.Visible = data.state == AchievementModel.AchievementType.WaitForGetReward
    lb_bought.Visible = data.state == AchievementModel.AchievementType.Finished and ach.is_drop~=0
    if btn_get2.Visible then
        PlayEffect(self,btn_get2,Vector3(btn_get2.Width/2,-btn_get2.Height/2,0),Vector3(1,1,1),'/res/effect/ui/ef_ui_frame_01.assetbundles',0,nil)
    end
    
    if TLUnityDebug.DEBUG_MODE then
        print_r('RedPointList',AchievementModel.RedPointList)
    end
    
    btn_get2.TouchClick=function()
        AchievementModel.RequestGetAchievementReward(ach.id,function (rsp)
            local drop={}
            for i, v in ipairs(rsp.s2c_data) do
                table.insert(drop,{id = v.templateid,num = v.num})
            end
            btn_get2.Visible=false
            lb_bought.Visible=true
            for i, v in ipairs(AchievementModel.RedPointList) do
                if v.id == data.id then
                    AchievementModel.RedPointList[i].state = AchievementModel.AchievementType.Finished
                    break
                end
            end
            
            if next(drop) then
                GlobalHooks.UI.OpenUI('GainItem',0,drop,3)
            end
            
            self.getRewardTime=LuaTimer.Add(1000,function()
                --红点(领取完先隐藏，在遍历判断是否显示)
                GlobalHooks.UI.ShowRedPoint(self.redpoint[data.type+1], 0, 'AchieveSub')
                for i, v in ipairs(AchievementModel.RedPointList) do
                    if v.id == data.id then
                        table.remove(AchievementModel.RedPointList,i)
                    end
                end
                if next(AchievementModel.RedPointList) then
                    for i, v in ipairs(AchievementModel.RedPointList) do
                        if v.state == AchievementModel.AchievementType.WaitForGetReward then
                            GlobalHooks.UI.ShowRedPoint(self.redpoint[v.type+1], 1, 'AchieveSub')
                        end
                    end
                end
                EventManager.Fire("Event.Achievement.UpdateAchievementUI",{type = {data.type},id={ach.id}})
            end)
        end)
    end
    local ib_finish=node:FindChildByEditName('ib_finish',true)
    ib_finish.Visible = isCompleted
end


local function SetEachAch(self,node,info,index)
    local ach=AchievementModel.GetAchDataByAchId(info.id)
    local ib_get=node:FindChildByEditName('ib_get',true)
    ib_get.Visible = info.state == AchievementModel.AchievementType.Finished or info.state == AchievementModel.AchievementType.WaitForGetReward

    local lb_name=node:FindChildByEditName('lb_name',true)
    if ach.Is_hidden == 1 and (info.state ==AchievementModel.AchievementType.NoVaild or info.state==AchievementModel.AchievementType.NotCompleted) then
        lb_name.Text=Util.GetText(Constants.Achievement.Hidden)
    else
        lb_name.Text=Util.GetText(ach.name)
    end
    
    local lb_num1=node:FindChildByEditName('lb_num1',true)
    lb_num1.Text=ach.achievement_num
    --六芒星图片
    local ib_level = node:FindChildByEditName('ib_level',true)
    if tonumber(ach.achievement_num) < 10 then
        ib_level.Position2D = Vector2(ib_level.Position2D.x,13)
        UIUtil.SetImage(ib_level,AchievementModel.Hexagram.Low,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    elseif tonumber(ach.achievement_num) >= 10 and tonumber(ach.achievement_num) <20 then
        ib_level.Position2D = Vector2(ib_level.Position2D.x,15)
        UIUtil.SetImage(ib_level,AchievementModel.Hexagram.Mid,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    elseif tonumber(ach.achievement_num) >= 20 then
        ib_level.Position2D = Vector2(ib_level.Position2D.x,15)
        UIUtil.SetImage(ib_level,AchievementModel.Hexagram.High,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    end
    
    --红点
    local lb_achred= node:FindChildByEditName('lb_achred',true)
    local isshow=info.state == AchievementModel.AchievementType.WaitForGetReward
    GlobalHooks.UI.ShowRedPoint(lb_achred,isshow and 1 or  0, 'achred')

    local ib_choose = node:FindChildByEditName('ib_choose',true)
    ib_choose.Visible = self.selectIndex == index
    node.TouchClick=function()
        self.selectIndex = index
        self.touchInfo=info
        InitCvs(self,self.touchInfo)
        self.comps.sp_itemlist:RefreshShowCell()
    end
end


--每次切换页签都会刷新成就列表(数据和类型)
local function InitList(self,data,type)
    for i, v in ipairs(data) do
        if v.state == AchievementModel.AchievementType.WaitForGetReward then
            self.selectIndex = i
            break
        end
    end
    
    if self.selectIndex > #data then
        self.selectIndex = 1
    end
    
    local node = self.comps.cvs_list
    local cvs_achie=node:FindChildByEditName('cvs_achie',true)
    cvs_achie.Visible=false
    local lb_title=node:FindChildByEditName('lb_title2',true)
    lb_title.Text = Util.GetText(self.AllAchType[type].name)
    local sp_itemlist=node:FindChildByEditName('sp_itemlist',true)
    local function eachupdatecb(node,index)
        SetEachAch(self,node,data[index],index)
    end
    UIUtil.ConfigGridVScrollPanWithOffset(sp_itemlist,cvs_achie,1,#data,0,0,eachupdatecb)
    
    local lb_num2=node:FindChildByEditName('lb_num2',true)
    for i, v in ipairs(self.achNum) do
        if v.type == type then
            lb_num2.Text=v.curVal..'/'..v.maxVal
        end
    end
end


local function InitCom(self,index)
    InitList(self,self.AllAchievementInfo[self.AllAchType[index].type],self.AllAchType[index].type)
    SelectByIndex(self,self.selectIndex)
    self.touchInfo = self.AllAchievementInfo[self.AllAchType[index].type][self.selectIndex]
    InitCvs(self,self.touchInfo)
end


--总览index为0，其他页签根据index的type来走
local function ShowIndexAchievement(self,index)
    self.togbtn[index+1].IsChecked=true
    self.comps.cvs_main.Visible=index == 0
    self.comps.cvs_detail.Visible=index ~= 0
    if index == 0 then
        return
    end
    
    --根据成就类型，取得对应的数据(如果有数据就不发协议了)
    if self.AllAchievementInfo[self.AllAchType[index].type] then
        InitCom(self,index)
    else
        AchievementModel.RequestGetAchievementList(self.AllAchType[index].type,function (rsp)

            if IsDispose then
                return
            end
            
            table.sort(rsp.s2c_data,function (a,b)
                return a.id < b.id
            end)
            
            local bigtype = self.AllAchType[index].type
            self.AllAchievementInfo[bigtype]={} 
           
            local tempsourcedata= {}
            for i, v in ipairs( rsp.s2c_data) do
                local ach=AchievementModel.GetAchDataByAchId(v.id)
                if ach.pre_id == 0 then
                    tempsourcedata[v.id] = v
                end
                local predata = tempsourcedata[ach.pre_id]
                if ach.pre_id ~= 0 and predata ~= nil and  predata.state == AchievementModel.AchievementType.Finished then
                    tempsourcedata[ach.pre_id] = nil
                    tempsourcedata[v.id] = v
                end
            end

            for i, v in pairs(tempsourcedata) do
                table.insert(self.AllAchievementInfo[bigtype],v)
            end
            local function paixu(a,b)
                local aRank = a.rank 
                local bRank = b.rank
                local aId = a.id
                local bId=b.id
                if aRank == bRank then
                    return  aId < bId
                else
                    return  aRank > bRank
                end
            end
            table.sort(self.AllAchievementInfo[bigtype],paixu)

            if TLUnityDebug.DEBUG_MODE then
                print_r('AchievementInfo',self.AllAchievementInfo[bigtype])
            end
            for i, v in ipairs(self.AllAchievementInfo[bigtype]) do
                if v.state == AchievementModel.AchievementType.WaitForGetReward then
                    self.selectIndex = i
                    break
                end
            end
            InitCom(self,index)
        end)
    end
end


--根据成就类型的数量，复制相对应的页签控件和红点
local function CopyTogBtnAndRedPoint(self,tog,redpoint,spacing)
    tog.Visible=false
    redpoint.Visible=false
    self.togbtn={}
    self.redpoint={}
    for i = 0 , #self.AllAchType do
        local clone = tog:Clone()
        local red=redpoint:Clone()
        self.comps.cvs_label:AddChild(clone)
        self.comps.cvs_label:AddChild(red)
        clone.Position2D=Vector2(tog.Position2D.x,(tog.Size2D.y+spacing)*i+36)
        red.Position2D=Vector2(redpoint.Position2D.x,clone.Position2D.y+10)
        if self.AllAchType[i] then
            clone.Text = Util.GetText(self.AllAchType[i].name)
            clone.UserTag = i
            red.UserTag = i
        else
            clone.Text = Util.GetText(Constants.Achievement.Total)
            clone.UserTag = 0
            red.UserTag = 0
        end
        table.insert(self.togbtn,clone)
        table.insert(self.redpoint,red)
    end
end


--初始化奖励
local function InitReward(self,node,cur,total)
    --领取完奖励会进来，先清一遍特效
    ReleaseTotalEffect(self)
    AchievementModel.RequestGetAchievementList(1,function (rsp)

        if IsDispose then
            return
        end
        
        table.sort(rsp.s2c_data,function (a,b)
            return a.id < b.id
        end)
        self.AllAchievementInfo[1] = rsp.s2c_data
        --判断取哪条数据显示在首页
        local index =1 
        local temp = {}
        for i, v in ipairs(self.AllAchievementInfo[1]) do
            if v.state ==AchievementModel.AchievementType.WaitForGetReward then
                temp = v
                index = i
                break
            end
        end
        if not next(temp) then --有值就不走了
            for i, v in ipairs(self.AllAchievementInfo[1]) do
                if v.progressList[1].TargetValue > cur then
                    temp = v
                    index = i
                    break
                end
                temp = self.AllAchievementInfo[1][#self.AllAchievementInfo[1]]
                index = 1
            end
        end
        
        --取模板数据
        local ach=AchievementModel.GetAchDataByAchId(temp.id)
        
        --奖励显示相关
        local ib_lockbox=node:FindChildByEditName('ib_lockbox',true)
        ib_lockbox.Visible=true
        ib_lockbox.Enable=true
        ib_lockbox.IsInteractive=true
        local ib_openbox=node:FindChildByEditName('ib_openbox',true)
        ib_openbox.Visible=false
        local cvs_touch=node:FindChildByEditName('cvs_touch',true)
        ib_lockbox.TouchClick=function()
            cvs_touch.Visible=true
            cvs_touch.TouchClick=function()
                cvs_touch.Visible=false
            end
        end
        local sp_itemlist=node:FindChildByEditName('sp_itemlist',true)
        local cvs_itemshow=node:FindChildByEditName('cvs_itemshow',true)
        cvs_itemshow.Visible=false
        local function eachupdatecb(node,index)
            if ach.item_show[index] ~= 0 then
                node.Visible=true
                ShowRewardItem(node,ach.item_show[index],ach.item_show_num[index])
            else
                node.Visible=false
            end
        end
        UIUtil.ConfigHScrollPanWithOffset(sp_itemlist,cvs_itemshow,#ach.item_show,15,eachupdatecb)
        
        --进度条
        local gg_plan=node:FindChildByEditName('gg_plan',true)
        local lb_pronum=node:FindChildByEditName('lb_pronum',true)
        lb_pronum.Text=cur..'/'..total
        local pro = cur/total >=1 and 1 or cur/total
        gg_plan.Value= pro*100 >=100 and 100 or pro*100
        local lb_aimnum=node:FindChildByEditName('lb_aimnum',true)
        lb_aimnum.Text= temp.progressList[1].TargetValue
        
        --领取奖励&可领取特效
        local cvs_anchor=node:FindChildByEditName('cvs_anchor',true)
        local btn_get=node:FindChildByEditName('btn_get',true)
        if temp.state == AchievementModel.AchievementType.WaitForGetReward then
            GlobalHooks.UI.ShowRedPoint(self.redpoint[1],1,'AchieveSub')
            PlayTotalEffect(self,btn_get,Vector3(btn_get.Width/2,-btn_get.Height/2,0),Vector3(1,1,1),'/res/effect/ui/ef_ui_frame_01.assetbundles')
            PlayTotalEffect(self,cvs_anchor,Vector3(0,-btn_get.Height/2,0),Vector3(1,1,1),'/res/effect/ui/ef_ui_chest.assetbundles')
        else
            GlobalHooks.UI.ShowRedPoint(self.redpoint[1],0, 'AchieveSub')
            ReleaseTotalEffect(self)
        end
        
        btn_get.TouchClick=function()
            if temp.state == AchievementModel.AchievementType.WaitForGetReward then
                AchievementModel.RequestGetAchievementReward(ach.id,function (rsp)
                    local drop={}
                    for i, v in ipairs(rsp.s2c_data) do
                        table.insert(drop,{id = v.templateid,num = v.num})
                    end
                    --1秒之后弹ui
                    self.getRewardTime=LuaTimer.Add(1000,function ()
                        if next(drop) then
                            GlobalHooks.UI.OpenUI('GainItem',0,drop,3)
                        end
                    end)
                    self.AllAchievementInfo[1][index].state=AchievementModel.AchievementType.Finished
                    --领取箱子动画和特效
                    ib_lockbox.Visible=false
                    ib_openbox.Visible=true
                    PlayTotalEffect(self,cvs_anchor,Vector3(0,-ib_lockbox.Height/2,0),Vector3(1,1,1),'/res/effect/ui/ef_ui_chest_open.assetbundles')
                        --消红点
                        GlobalHooks.UI.ShowRedPoint(self.redpoint[2], 0, 'AchieveSub')
                        for i, v in ipairs(AchievementModel.RedPointList) do
                            if v.id == ach.id then
                                table.remove(AchievementModel.RedPointList,i)
                            end
                        end
                        for i, v in ipairs(AchievementModel.RedPointList) do
                            if v.state == AchievementModel.AchievementType.WaitForGetReward then
                                GlobalHooks.UI.ShowRedPoint(self.redpoint[v.type+1], 1, 'AchieveSub')
                            end
                        end
                        --刷新界面
                        self.boxtime=LuaTimer.Add(3000,function()
                            EventManager.Fire("Event.Achievement.UpdateAchievementUI",{type = {temp.type},id={ach.id},skip=0})
                        end)
                end)
            else
                GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.Achievement.GetReward,temp.progressList[1].TargetValue))
            end
        end
    end)
end


--设置总览界面的成就
local function UpDateItem(self,node,index,source)
    if index > #self.AllAchType then
        node.Visible=false
        return
    end
    local btn_name=node:FindChildByEditName('btn_name',true)
    btn_name.Text=Util.GetText(self.AllAchType[index].name)    
    local ib_typeicon=node:FindChildByEditName('ib_typeicon',true)
    UIUtil.SetImage(ib_typeicon,self.AllAchType[index].icon_id,false, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    local lb_num=node:FindChildByEditName('lb_num',true)
    for i, v in ipairs(source) do
        if v.type == self.AllAchType[index].type then
            lb_num.Text= v.curVal..'/'..v.maxVal
        end
    end
    btn_name.TouchClick=function(sender)
        ShowIndexAchievement(self,index)
    end
end


--打开指定的成就id页面
local function ShowAppointAchievement(self,params)
    local data=AchievementModel.GetAchDataByAchId(params)
    if self.AllAchievementInfo[data.type] then
        for i, v in ipairs(self.AllAchievementInfo[data.type]) do
            if v.id == params then
                self.selectIndex = i
                ShowIndexAchievement(self,data.type)
                SelectByIndex(self,self.selectIndex)
                break
            end
        end
    else
        AchievementModel.RequestGetAchievementList(data.type,function (rsp)
            
            if IsDispose then
                return
            end
            
            table.sort(rsp.s2c_data,function (a,b)
                return a.id < b.id
            end)
            self.AllAchievementInfo[data.type] = rsp.s2c_data
            for i, v in ipairs(self.AllAchievementInfo[data.type]) do
                if v.id == params then
                    self.selectIndex = i
                    ShowIndexAchievement(self,data.type)
                    SelectByIndex(self,self.selectIndex)
                    break
                end
            end
        end)
    end
end


--完成成就刷新界面
local function OnAchievementCompleteNotify(self,params)
    for i, v in pairs(params.s2c_data) do
        if self.AllAchievementInfo[v.type] then
            for j, k in pairs(self.AllAchievementInfo[v.type]) do
                if k.id==v.id then
                    self.AllAchievementInfo[v.type][j] = v
                    break
                end
            end
        end
    end
    self.comps.sp_itemlist:RefreshShowCell()
    for i, v in pairs(params.s2c_data) do
        if self.touchInfo and v.id == self.touchInfo.id then
            self.touchInfo = v 
            InitCvs(self,v)
            break
        end
    end
end

--设置子页签的红点
local function ShowRedTips(self)
    for i, v in ipairs(AchievementModel.RedPointList) do
        if v.state == AchievementModel.AchievementType.WaitForGetReward then
            GlobalHooks.UI.ShowRedPoint(self.redpoint[v.type+1], 1, 'AchieveSub')
        end
    end
end

--领取完奖励刷新界面(最偷懒的办法，删除旧的，重新请求新的)
local function OnUpdateAchievementUINotify(self,params)
    self.AllAchievementInfo[params.type[1]] = nil
    if params.skip == 0 then
        ShowIndexAchievement(self,params.skip)
    else
        ShowIndexAchievement(self,params.type[1])
    end
    InitReward(self,self.comps.cvs_mainreward,self.curPoint,self.totalPoint)
end


function _M.OnEnter(self,params)

    IsDispose = false
    
    function _M.OnAchievementComplete(eventname,params)
        OnAchievementCompleteNotify(self,params)
    end
    EventManager.Subscribe("Event.Achievement.AchievementComplete", _M.OnAchievementComplete)
    function _M.OnUpdateAchievementUI(eventname,params)
        OnUpdateAchievementUINotify(self,params)
    end
    EventManager.Subscribe("Event.Achievement.UpdateAchievementUI", _M.OnUpdateAchievementUI)
    
    AchievementModel.RequestGetAchievementList(0,function (rsp)

        if IsDispose then
            return
        end
        
        self.comps.cvs_main.Visible = true
        self.comps.cvs_detail.Visible = false
        self.AllAchievementInfo={}
        
        --所有成就类型对应的完成数量，并设置tog侧边标签页
        self.achNum=rsp.s2c_catalogdata
        self.cvsicon={}
        self.AllAchType={}
        AchievementModel.InsertAndSort(self.AllAchType,self.achNum)
        CopyTogBtnAndRedPoint(self,self.comps.tbt_an,self.comps.lb_red,5)
        local function ToggleFunction(sender)
            sender.TouchClick=function()
                ShowIndexAchievement(self,sender.UserTag)
            end
        end
        UIUtil.ConfigToggleButton(self.togbtn,self.togbtn[1],false,ToggleFunction)
        
        ShowRedTips(self)
        
        self.curPoint=rsp.s2c_curFinishPoints
        self.totalPoint=rsp.s2c_totalFinishPoints
        InitReward(self,self.comps.cvs_mainreward,rsp.s2c_curFinishPoints,rsp.s2c_totalFinishPoints)
        
        --设置总览的成就分类
        self.comps.cvs_type.Visible=false
        local function eachupdatecb(node,index)
            UpDateItem(self,node,index,rsp.s2c_catalogdata)
        end
        UIUtil.ConfigGridVScrollPanWithOffset(self.comps.sp_list,self.comps.cvs_type,3,#self.AllAchType,15,15,eachupdatecb)
        self.ui.comps.sp_list.Scrollable.Scroll.vertical = #self.AllAchType > 6

        if params then --传成就id 直接跳转到某条成就
            ShowAppointAchievement(self,params)
        else
            --默认打开第一个
            self.selectIndex = 1
        end
    end)
end


function _M.OnExit(self)
    IsDispose = true
    
    GlobalHooks.UI.SetRedTips('achievement',0)
    for i, v in ipairs(AchievementModel.RedPointList) do
        if v.state == AchievementModel.AchievementType.WaitForGetReward then
            GlobalHooks.UI.SetRedTips('achievement',1)
            break
        end
    end
    
    ReleaseEffect(self)
    ReleaseTotalEffect(self)
    
    --删除复制出来的克隆体
    for i = 1, #self.togbtn do
        self.togbtn[i]:RemoveFromParent(true)
        self.redpoint[i]:RemoveFromParent(true)
    end
    self.togbtn=nil
    self.redpoint=nil
    if self.cvsicon then
        for i, v in ipairs(self.cvsicon) do
            v:RemoveFromParent(true)
        end
        self.cvsicon={}
    end
    
    if self.boxtime then
        LuaTimer.Delete(self.boxtime)
        self.boxtime= nil
    end
    if self.getRewardTime then
        LuaTimer.Delete(self.getRewardTime)
        self.getRewardTime = nil
    end
    
    EventManager.Unsubscribe("Event.Achievement.AchievementComplete", _M.OnAchievementComplete)
    EventManager.Unsubscribe("Event.Achievement.UpdateAchievementUI", _M.OnUpdateAchievementUI)

end


return _M