local _M = {}
_M.__index = _M


local DailyDungeonModel=require 'Model/DailyDungeonModel'
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local Dungeon= require 'Model/DungeonModel'
local ItemModel = require 'Model/ItemModel'
local RechargeModel=require('Model/RechargeModel')

local D=DailyDungeonModel.GetAllDailyDungeonData()--返回读取的副本数据
local spacing=0--UI之间的间隔
local fbdetailsPos={}
local count={}--每个副本进入的次数

local selectIndex--选中的索引
local container
local sp_fblist
local cvs_fbdetails
local cvs_fbmain
local clone={}--保存复制出来的ui


--创建页签的副本，覆盖在原来的UI上，作为Main的子物体
local function CreateCopyUI(self,sender,index)

    --复制一份克隆体
	cloneCard = sender:Clone()

    --设置克隆体的位置，与点击的UI重合
	cloneCard.Position2D=self.ui.menu:LocalToUIGlobal(sender)-Vector2(spacing*2,38)
 
    --将克隆体添加为Main的子物体，设置动画，为其赋值
    cvs_fbmain:AddChild(cloneCard)

    --设置移动动画位置与时间
    local move_clone=MoveAction()
          move_clone.Duration=0.3
          move_clone.TargetX=70--30
          move_clone.TargetY=100--39
 		  move_clone.ActionEaseType = EaseType.linear
 	cloneCard:AddAction(move_clone)
    
    --查找控件
    local cloneCard_fbname=cloneCard:FindChildByEditName("lb_fbname", true)
    local cloneCard_level=cloneCard:FindChildByEditName("lb_levelnum", true)
    local cloneCard_countnum=cloneCard:FindChildByEditName('lb_countnum',true)
    local cloneCard_go=cloneCard:FindChildByEditName('lb_go', true)
    local cloneCard_fbpic=cloneCard:FindChildByEditName('ib_fbpic', true)

    --设置原图
    UIUtil.SetImage(cloneCard_fbpic,D[index].pic_res)

    --竖着显示
    cloneCard_fbname.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    
    --控件赋值
    cloneCard_level.Text=D[index].level_key..Util.GetText('skill_Level')
    cloneCard_fbname.Text=Util.GetText(D[index].dungen_name)
	cloneCard_countnum.Text='  '..count[D[index].order]--..'/'..D[index].count
    cloneCard_go.Text=Constants.Dungeon.Back

    --瞬间消失
    cloneCard.TouchClick=function(sender)
        SoundManager.Instance:PlaySoundByKey('button', false)
     	cvs_fbdetails.Visible=false
        --显示Main所有UI
        sp_fblist.Visible=true
        --删除创建的克隆体
	    cloneCard:RemoveFromParent(true)
        cloneCard=nil
    end
end


--设置细节UI的位移动画
local function FbDetailAnim()

	cvs_fbdetails.Position2D={30,fbdetailsPos.y}   

    local moveToShow_fbdetails = MoveAction()
		  moveToShow_fbdetails.Duration =0.2
		  moveToShow_fbdetails.TargetX = fbdetailsPos.x
		  moveToShow_fbdetails.TargetY = fbdetailsPos.y
	      moveToShow_fbdetails.ActionEaseType = EaseType.linear
	cvs_fbdetails:AddAction(moveToShow_fbdetails)
end


--设置掉落预览
local function UpDateItem(selectIndex,node,index)
    --获取品质和数量
    local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(D[selectIndex].awardshow.id[index]))
    local quality = itemdetail.static.quality
    local icon=itemdetail.static.atlas_id
    local itshow=UIUtil.SetItemShowTo(node,icon,quality,1)
    itshow.EnableTouch = true
        itshow.TouchClick = function()      
            UIUtil.ShowNormalItemDetail({detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})         
        end
end


--购买额外的次数
local function BuyExtraCount(self,index)
    
    --查询购买消耗物品
    local cost=DailyDungeonModel.InquiryCost(D[index].function_id)
    --购买额外次数多语言
    local buyextracount =Util.GetText('vip_buy_extra_count',cost.cost.num[1])
    --vip等级不足多语言
    local notenoughlv=Util.GetText('vip_not_enough_level')
    --元宝不足多语言
    local notenoughgold=Util.GetText('vip_not_enough_gold')
    --查询可购买次数
    local time =DailyDungeonModel.InquiryCanBuyCount(D[index].function_id)

    --vip等级不足，引导充值界面
    if time <1 then
        GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, notenoughlv,'','',nil,
                function ()
                    GlobalHooks.UI.OpenUI('Recharge',0, 'RechargePay')
                end
        ,nil)
        return
    end

    --元宝不足，引导充值界面
    if DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.DIAMOND) < cost.cost.num[1] then
        GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, notenoughgold,'','',nil,
                function ()
                    GlobalHooks.UI.OpenUI('Recharge',0, 'RechargePay')
                end
        ,nil)
        return
    end
    
    --满足条件，发送协议购买次数
    GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, buyextracount,'','',nil,
        function ()
            DailyDungeonModel.RequestBuyEnterCount(D[index].function_id,function (rsp)
                if rsp.s2c_code==200 then --购买成功，漂字提示，同时增加界面显示
                    GameAlertManager.Instance:ShowNotify(Constants.Text.shop_buysucc)
                    count[D[index].order]=count[D[index].order]+1
                    if cloneCard ~= nil then
                        cloneCard:FindChildByEditName('lb_countnum',true).Text='  '..count[D[index].order]
                    end
                    if self.cvs_fb ~= nil then
                        self.cvs_fb:FindChildByEditName('lb_countnum',true).Text='  '..count[D[index].order]
                    end
                    if clone[index] ~= nil then
                        clone[index]:FindChildByEditName('lb_countnum',true).Text='  '..count[D[index].order]
                    end
                end
            end)
        end
    ,nil)
end


--返回表内最大值&索引
local function MaxInTable(t)
    local index = 1
    local max = t[index]
    for i,val in ipairs(t) do
        if val > max then
            index = i
            max = val
        end
    end
    return max, index
end


--针对MainUI所有页签添加点击事件(传入ui控件及索引)
local function OpenDungeonDetails(self,sender,index)
  
    selectIndex=index

    --显示副本细节UI，并创建页签的克隆体
    cvs_fbdetails.Visible=true

    --副本描述
    self.lb_intorduce.Text=Util.GetText(D[index].dungeon_desc)
    
    --计算vip额外购买次数
    local extra ={}
    for i = 0, 13 do 
        local time=RechargeModel.GetVipInfoValueByKey(D[index].buy_record,i)
        table.insert(extra,time)
    end
    local max,vip=MaxInTable(extra)
    self.lb_viptips.Text=Util.GetText('show_VipTimes',vip-1,max)
    
    --赋值一个页签，并作出动画
    CreateCopyUI(self,sender,index)
    FbDetailAnim()
    
    sp_fblist.Visible=false

    --设置详情页的掉落预览
    sp_drops=self.ui.comps.sp_drops
    cvs_dpitem=self.ui.comps.cvs_dpitem
    cvs_dpitem.Visible=false

    local function UpdateItem(node,index)     	
		UpDateItem(selectIndex,node,index)
    end

    --根据掉落数量，控制显示掉落列表
    for i= #D[index].awardshow.id,1,-1 do
		if D[index].awardshow.id[i]==nil or D[index].awardshow.id[i]=='' then
	 		table.remove(D[index].awardshow.id,i)
	 	end
	end
    UIUtil.ConfigHScrollPan(sp_drops,cvs_dpitem,#D[index].awardshow.id,UpdateItem)

    --开始按钮
    btn_start=self.ui.comps.btn_start
    btn_start.TouchClick=function()
    	if not DataMgr.Instance.TeamData.HasTeam then --如果单人情况下
            if count[selectIndex] >0 then --如果该副本可进入，则传送
                Dungeon.RequestEnterDungeon(D[selectIndex].function_id,function(resp)
          			self.ui:Close()
				end)
            else --否则提示次数已用完
                GameAlertManager.Instance:ShowNotify(Constants.GuildWant.CountLimit)
            end
		else--否则提示单人进入
			GameAlertManager.Instance:ShowNotify(Constants.Dungeon.MustSolo)
		end
	end
    
    --购买按钮
    self.ui.comps.btn_count.TouchClick=function()
        BuyExtraCount(self,index)
    end
end


--回调函数(元素，索引)
local function UpDateDungeon(self,node,index)
    
    clone[index]=node
    --查找控件
    local lb_fbname=node:FindChildByEditName("lb_fbname", true)
    local lb_level=node:FindChildByEditName("lb_levelnum", true)
    local lb_countnum=node:FindChildByEditName('lb_countnum',true)
    local lb_go=node:FindChildByEditName('lb_go',true)
    local ib_fbpic=node:FindChildByEditName('ib_fbpic',true)
    local ib_lock=node:FindChildByEditName('ib_lock',true)
    local lb_lock=node:FindChildByEditName('lb_lock',true)
        
    --竖着显示
    lb_fbname.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    
    --控件赋值
    lb_fbname.Text=Util.GetText(D[index].dungen_name)
    lb_level.Text=D[index].level_key..Util.GetText('skill_Level')
    lb_lock.Text=Util.GetText('skill_openLv', D[index].level_key)
  	lb_countnum.Text='  '..count[D[index].order]--..'/'..D[index].count
	lb_go.Text=Constants.Dungeon.Go

	--设置原图
    UIUtil.SetImage(ib_fbpic,D[index].pic_res)

    --判断人物等级是否达到副本要求等级，若未达到，则不开放副本
    if D[index].level_key > DataMgr.Instance.UserData.Level then  
        ib_lock.Visible = true
        lb_lock.Visible = true
        node.IsInteractive=false
    else 
        ib_lock.Visible = false
        lb_lock.Visible = false
        node.IsInteractive=true
    end

	--添加监听事件
    node.TouchClick=function(sender)
        SoundManager.Instance:PlaySoundByKey('button',false)
        selectIndex = index
        OpenDungeonDetails(self,sender,index)
        sp_fblist:RefreshShowCell()
    end

end


function _M.OnInit(self)
	
	self.cvs_fb=self.ui.comps.cvs_fb
	self.cvs_fb.Visible = false

	sp_fblist=self.ui.comps.sp_fblist
  	container=sp_fblist.ContainerPanel

	cvs_fbdetails=self.ui.comps.cvs_fbdetails
	cvs_fbdetails.Visible=false

	self.lb_intorduce=self.ui.comps.lb_intorduce
	cvs_fbmain=self.ui.comps.cvs_fbmain
    self.lb_viptips=self.ui.comps.lb_viptips

 	fbdetailsPos=cvs_fbdetails.Position2D
    
    --设置副本描述自动换行
    self.lb_intorduce.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap

end


--打开指定索引的ui页面
local function OpenDesignatedUI(self,node,index)

    --隐藏scroll面板
    sp_fblist.Visible=false
    
    --显示单独一个控件
    node.Visible=true
    
    --查找控件
    local lb_fbname=node:FindChildByEditName("lb_fbname", true)
    local lb_level=node:FindChildByEditName("lb_levelnum", true)
    local lb_countnum=node:FindChildByEditName('lb_countnum',true)
    local lb_go=node:FindChildByEditName('lb_go',true)
    local ib_fbpic=node:FindChildByEditName('ib_fbpic',true)
    local lb_lock=node:FindChildByEditName('lb_lock',true)
    
    --计算vip额外购买次数
    local extra ={}
    for i = 0, 13 do
        local time=RechargeModel.GetVipInfoValueByKey(D[index].buy_record,i)
        table.insert(extra,time)
    end
    local max,vip=MaxInTable(extra)
    self.lb_viptips.Text=Util.GetText('show_VipTimes',vip-1,max)
    
    --竖着显示
    lb_fbname.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    
    --控件赋值
    lb_fbname.Text=Util.GetText(D[index].dungen_name)
    lb_level.Text=D[index].level_key..Util.GetText('skill_Level')
    lb_lock.Text=Util.GetText('skill_openLv', D[index].level_key)
    lb_countnum.Text='  '..count[D[index].order]--..'/'..D[index].count
    lb_go.Text=Constants.Dungeon.Back
    
    --设置原图
    UIUtil.SetImage(ib_fbpic,D[index].pic_res)

    selectIndex=index

    --显示副本细节UI
    cvs_fbdetails.Visible=true

    --副本描述
    self.lb_intorduce.Text=Util.GetText(D[index].dungeon_desc)

    --设置详情页的掉落预览
    sp_drops=self.ui.comps.sp_drops
    cvs_dpitem=self.ui.comps.cvs_dpitem
    cvs_dpitem.Visible=false

    local function UpdateItem(node,index)       
        UpDateItem(selectIndex,node,index)
    end

    --根据掉落数量，控制显示掉落列表
    for i= #D[index].awardshow.id,1,-1 do
        if D[index].awardshow.id[i]==nil or D[index].awardshow.id[i]=='' then
            table.remove(D[index].awardshow.id,i)
        end
    end
    UIUtil.ConfigHScrollPan(sp_drops,cvs_dpitem,#D[index].awardshow.id,UpdateItem)

    --开始按钮
    btn_start=self.ui.comps.btn_start
    btn_start.TouchClick=function()
        if not DataMgr.Instance.TeamData.HasTeam then --如果单人情况下
            if count[selectIndex] >0 then --如果副本次数不为0
                Dungeon.RequestEnterDungeon(D[selectIndex].function_id,function(resp)
                    self.ui:Close()
                end)
            else --否则提示次数已用完
                GameAlertManager.Instance:ShowNotify(Constants.GuildWant.CountLimit)
            end
        else--否则提示单人进入
            GameAlertManager.Instance:ShowNotify(Constants.Dungeon.MustSolo)
        end
    end
    
    --购买按钮
    self.ui.comps.btn_count.TouchClick=function()
        BuyExtraCount(self,index)
    end
    
    --点击返回主页面，恢复原样
    node.TouchClick=function()
        sp_fblist.Visible=true
        node.Visible=false
        cvs_fbdetails.Visible=false
    end

end


function _M.OnEnter(self,params)

	--获取进入副本的次数,并重新排序
	DailyDungeonModel.RequestDailyDungeonEnterCount(function(rsp)
		for k,v in pairs(rsp.s2c_info) do
            count[DailyDungeonModel.MatchCountByKey(k)]=v
		end

    --如果有参数，就打开指定的页签
    if params ~=nil then 
       OpenDesignatedUI(self,self.cvs_fb,params)
    end

	--回调函数
    local function UpdateDungeon(node,index)
		UpDateDungeon(self,node,index)
	end
	--滑动控件
    UIUtil.ConfigHScrollPanWithOffset(sp_fblist,self.cvs_fb,#D,spacing,UpdateDungeon)
	end)
    
end


function _M.OnExit(self)

	--ui复原
	cvs_fbdetails.Visible=false
	sp_fblist.Visible=true

    --避免因打开指定页签而出现显示问题
    self.cvs_fb.Visible = false

    --若直接点击关闭按钮，则移除复制的克隆体
	if cloneCard ~=nil then 
	   cloneCard:RemoveFromParent(true)
	   cloneCard=nil
    end 

    --关闭ui的时候，清空次数表，避免重复累加
    count={}
end


return _M