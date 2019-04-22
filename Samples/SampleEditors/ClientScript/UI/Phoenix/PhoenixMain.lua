local _M = {}
_M.__index = _M


local PhoenixModel=require 'Model/PhoenixModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local TimeUtil=require 'Logic/TimeUtil'

--设置掉落
local function SetDropDetail(self,node,i,index)

	local itemdetail=ItemModel.GetDetailByTemplateID(self.phoenixData[index].reward.id[i])
	local quality = itemdetail.static.quality
	local icon=itemdetail.static.atlas_id
	local itshow=UIUtil.SetItemShowTo(node,icon,quality,1)
	itshow.EnableTouch = true
	itshow.TouchClick = function()
		local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})
	end

end


local function SetPhoenixInfo(self,node,index)
	
	--如果多复制出来了，则隐藏多余的控件
	if index > #self.phoenixData then 
		node.Visible=false
		return
	end

	--查找控件
	local ib_boss=node:FindChildByEditName('ib_boss',true)--boss头像
	--local lb_kill=node:FindChildByEditName('lb_kill',true)--已击杀图标
	local lb_place=node:FindChildByEditName('lb_place',true)--地点
	local lb_level=node:FindChildByEditName('lb_level',true)--等级
	local lb_time=node:FindChildByEditName('lb_time',true)--时间
	local lb_refresh=node:FindChildByEditName('lb_refresh',true)--未刷新
	local lb_freshed=node:FindChildByEditName('lb_freshed',true)--已刷新
	local btn_go=node:FindChildByEditName('btn_go',true)--前往讨伐
	local sp_item=node:FindChildByEditName('sp_item',true)--奖励scroll
	local cvs_item=node:FindChildByEditName('cvs_item',true)
	cvs_item.Visible=false

	--如果等级未到，置灰显示
	if self.phoenixData[index].boss_level > DataMgr.Instance.UserData.Level then 
		node.IsGray=true
		node.IsInteractive=false
		btn_go.IsInteractive=false
	end

	--设置boss信息
	UIUtil.SetImage(ib_boss,self.phoenixData[index].pic_res,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
	lb_place.Text=self.phoenixData[index].mapid
	lb_level.Text=self.phoenixData[index].boss_level..Util.GetText('skill_Level')

	--获取刷新时间
	local refresh=PhoenixModel.GetRefreshTimeById(self.phoenixData[index].refresh_time)
	
	for i=1,#refresh do

		--如果在刷新时间内，显示已刷新
		if GameSceneMgr.Instance.syncServerTime:IsBetweenTime(refresh[i].starttime,refresh[i].endtime) then
			lb_time.Text=refresh[i].starttime
			lb_freshed.Visible=true
			lb_refresh.Visible=false
			--lb_kill.Visible=false
			break

		--寻找下一个未到的刷新时间点
		elseif TimeUtil.TimeLeftSec(TimeUtil.CustomTodayTimeToUtc(tostring(refresh[i].starttime)..':00')) <=0 then
			lb_time.Text=refresh[i].starttime
			lb_freshed.Visible=false
			lb_refresh.Visible=true
			--lb_kill.Visible=true
			break
		else
			--若上述情况都不满足，则显示第一个刷新段
			lb_time.Text=refresh[1].starttime
			lb_freshed.Visible=false
			lb_refresh.Visible=true
			--lb_kill.Visible=true
		end
	end
	
	local bossIndex=index
	
	--前往按钮
	btn_go.TouchClick=function()
		FunctionUtil.seekAndNpcTalkByFunctionTag(self.phoenixData[index].function_tag)
	end
	
	--倒数遍历奖励数组，把空值移除
	for i = #self.phoenixData[index].reward.id,1,-1 do
		if self.phoenixData[index].reward.id[i]==0 or self.phoenixData[index].reward.id[i]==nil then
			table.remove(self.phoenixData[index].reward.id,i)
		end
	end
	
	--设置掉落
    local function eachupdatecb2(node,i,index)
    	SetDropDetail(self,node,i,bossIndex)
    end
    UIUtil.ConfigHScrollPan(sp_item,cvs_item,#self.phoenixData[index].reward.id,eachupdatecb2)

end


function _M.OnInit(self)

	--帮助按钮
	self.btn_help=self.ui.comps.btn_help
	self.cvs_help=self.ui.comps.cvs_help

	--scrollpan
	self.sp_boss=self.ui.comps.sp_boss

	self.cvs_boss=self.ui.comps.cvs_boss
	self.cvs_boss.Visible=false
end


function _M.OnEnter(self)

	self.phoenixData=PhoenixModel.GetAllPhoenixData()

	local function eachupdatecb(node,index)
		SetPhoenixInfo(self,node,index)
	end
	UIUtil.ConfigGridVScrollPanWithOffset(self.sp_boss,self.cvs_boss,2,#self.phoenixData,25,20,eachupdatecb)

	--帮助
	self.btn_help.TouchClick=function()
		self.cvs_help.Visible=true
		self.cvs_help.TouchClick=function()
			self.cvs_help.Visible=false
		end
	end

end


return _M