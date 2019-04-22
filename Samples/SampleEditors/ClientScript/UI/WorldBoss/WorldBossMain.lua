local _M = {}
_M.__index = _M


local UIUtil = require 'UI/UIUtil'
local WorldBossModel=require 'Model/WorldBossModel'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'
local TimeUtil=require 'Logic/TimeUtil'


local selectIndex
local container


--释放模型
local function Release3DModel(self)
	if self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end


--加载模型
local function Set3DModel(self)

	local fixposdata=string.split(self.bossData[selectIndex].pos_xy,',')--拆分坐标
	local fixpos = {x = tonumber(fixposdata[1]),y = tonumber(fixposdata[2])}-- 偏移坐标

	local info = UI3DModelAdapter.AddSingleModel(
			self.cvs_models,
			Vector2(fixpos.x,fixpos.y),
			tonumber(self.bossData[selectIndex].zoom),
			self.ui.menu.MenuOrder,
			self.bossData[selectIndex].modle)

	self.model = info

	--初始旋转角度
	info.Callback = function (info2)
		local trans2 = info2.RootTrans
		trans2:Rotate(Vector3.up,self.bossData[selectIndex].rotate)
	end

	--拖动旋转
	local trans = info.RootTrans
	self.cvs_model.event_PointerMove = function(sender, data)
	local delta = -data.delta.x
		trans:Rotate(Vector3.up, delta * 1.2)
	end
	
end


--设置掉落
local function SetDropDetail(self,node,index)
	
    local itemdetail=ItemModel.GetDetailByTemplateID(self.bossData[selectIndex].reward.id[index])
--[[    local quality = itemdetail.static.quality
    local icon=itemdetail.static.atlas_id]]
    local itshow=UIUtil.SetItemShowTo(node,itemdetail,1)
      itshow.EnableTouch = true
          itshow.TouchClick = function()      
        local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})         
    end	

end


--设置boss信息页
local function SetBossInfo(self,node,index)

	--boss面板以及选中框
	local ib_pic=node:FindChildByEditName('ib_pic', true)
	local ib_select=node:FindChildByEditName('ib_select',true)

	--判断当前点击，是否显示选中框
	if selectIndex~=index then 
		ib_select.Visible=false
	else
		ib_select.Visible=true
	end

	local lb_level=node:FindChildByEditName('lb_level',true)
	local lb_time=node:FindChildByEditName('lb_time',true)
	local lb_refresh=node:FindChildByEditName('lb_refresh',true)--未刷新
	local lb_freshed=node:FindChildByEditName('lb_freshed',true)--已刷新

	--通过索引获取该时间组内所有的开始结束时间
   	self.bosstime=WorldBossModel.GetBossAllTime(self.bossData[index].refresh_time)

   	for i=1,#self.bosstime do

   		--如果在刷新时间内，显示已刷新
   		if GameSceneMgr.Instance.syncServerTime:IsBetweenTime(self.bosstime[i].starttime,self.bosstime[i].endtime) then
   			lb_time.Text=self.bosstime[i].starttime
   			lb_freshed.Visible=true
   			lb_refresh.Visible=false
   			break
   	
   		--寻找下一个未到的刷新时间点
   		elseif TimeUtil.TimeLeftSec(TimeUtil.CustomTodayTimeToUtc(tostring(self.bosstime[i].starttime)..':00')) <=0 then
   			lb_time.Text=self.bosstime[i].starttime
   			lb_freshed.Visible=false
   			lb_refresh.Visible=true
   			break
   		else

   		--若上述情况都不满足，则显示第一个刷新段
   			lb_time.Text=self.bosstime[1].starttime
   			lb_freshed.Visible=false
   			lb_refresh.Visible=true
   		end
   	end

    --等级，刷新时间
	lb_level.Text=self.bossData[index].boss_level..Util.GetText('skill_Level')
	UIUtil.SetImage(ib_pic,self.bossData[index].pic_res,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
	
	--点击切换boss，释放模型，刷新列表，加载新模型
	node.TouchClick=function()
		SoundManager.Instance:PlaySoundByKey('button',false)
		selectIndex=index
		Release3DModel(self)
		self.sp_boss:RefreshShowCell()
		self.sp_item:RefreshShowCell()
		Set3DModel(self)
	end
end


function _M.OnInit(self)

	--boss面板
	self.cvs_boss=self.ui.comps.cvs_boss
	self.cvs_boss.Visible=false
	self.sp_boss=self.ui.comps.sp_boss
	container=self.sp_boss.ContainerPanel

	--帮助
	self.btn_help=self.ui.comps.btn_help

	--前往
	self.btn_go=self.ui.comps.btn_go

	--上下箭头
	self.btn_up=self.ui.comps.btn_up
	self.btn_down=self.ui.comps.btn_down

	--帮助
	self.cvs_help=self.ui.comps.cvs_help

	--掉落列表
	self.cvs_item=self.ui.comps.cvs_item
	self.cvs_item.Visible=false
	self.sp_item=self.ui.comps.sp_item

	--模型点
	self.cvs_models=self.ui.comps.cvs_models

	--模型面
	self.cvs_model=self.ui.comps.cvs_model

end


--boss页面的移动动画
local function ContainerMoveAnim(self,dir)
	
    local ConMove=MoveAction()
   		ConMove.Duration =0.2
		ConMove.TargetY = container.Position2D.y+(self.cvs_boss.Size2D.y)*(-dir)
	    ConMove.ActionEaseType = EaseType.linear
	    container:AddAction(ConMove)
end


--点击按钮移动UI页签
local function MovePage(self,dirnum)
  
  -- -1往上移动
	if dirnum==-1 then
		if container.Position2D.y <=1 and container.Position2D.y>=0 then 
		  	return false
		end
	    ContainerMoveAnim(self,dirnum)
	 
  	elseif dirnum==1 then  -- 1往下移动
    	if container.Position2D.y>-(self.cvs_boss.Size2D.y*(#self.bossData-3)) and container.Position2D.y<-(self.cvs_boss.Size2D.y*(#self.bossData-4)) then 
       	 	return false
    	end
		ContainerMoveAnim(self,dirnum)
  	end
end


function _M.OnEnter(self)

	self.bossData=WorldBossModel.GetAllBossData()

	--设置默认选中框
	selectIndex=1

	--加载默认模型
	Set3DModel(self)

	--若世界boss数量大于5，则显示上下箭头
	if #self.bossData >5 then 
		self.btn_up.Visible=true
		self.btn_down.Visible=true
		self.btn_up.TouchClick=function()
			MovePage(self,-1)
		end
		self.btn_down.TouchClick=function()
			MovePage(self,1)
		end
	else
		self.btn_up.Visible=false
		self.btn_down.Visible=false
	end

	--点击显示帮助，随意点击屏幕隐藏帮助
	self.btn_help.TouchClick=function()
		self.cvs_help.Visible=true
		--Release3DModel(self)
		--Set3DModel(self)
		--设置说明ui层，遮住模型
		self.ui.menu:SetUILayer(self.cvs_help,1001,-700)
		self.cvs_help.event_PointerUp=function()
			self.cvs_help.Visible=false
		end
	end

	self.btn_go.TouchClick=function()
		FunctionUtil.seekAndNpcTalkByFunctionTag(self.bossData[selectIndex].funtion_tag)
	end

    --设置boss
	local function eachupdatecb(node,index)
        SetBossInfo(self,node,index)
    end
    UIUtil.ConfigVScrollPan(self.sp_boss,self.cvs_boss,#self.bossData,eachupdatecb)

    --设置掉落
    local function eachupdatecb2(node,index)
    	SetDropDetail(self,node,index)
    end
    UIUtil.ConfigHScrollPan(self.sp_item,self.cvs_item,#self.bossData[selectIndex].reward.id,eachupdatecb2)

end


function _M.OnExit(self)

	--退出时释放模型
	Release3DModel(self)

end


return _M