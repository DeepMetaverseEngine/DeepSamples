local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local StrongModel=require 'Model/BeStrongModel'
local Util = require 'Logic/Util'


--设置变强功能
local function SetDetail(self,node,index,moduleFight)
	
	local cvs_icon=node:FindChildByEditName("cvs_icon", true)
	local lb_lvnum=node:FindChildByEditName("lb_lvnum", true)
	local lb_recomlvnum=node:FindChildByEditName("lb_recomlvnum", true)
	local lb_status1=node:FindChildByEditName('lb_status1',true)
	local gg_status1=node:FindChildByEditName('gg_status1',true)
	local btn_to=node:FindChildByEditName('btn_to',true)
	local lb_nowlv=node:FindChildByEditName('lb_nowlv',true)
	local lb_recomlv=node:FindChildByEditName('lb_recomlv',true)

	--设置icon
	UIUtil.SetImage(cvs_icon,self.strong[index].icon_res)

	--当前战力，通过function_id来查找
	for k,v in pairs(moduleFight) do
		if k == self.strong[index].function_id then
			lb_lvnum.Text=v
			lb_nowlv.Text=Util.GetText(self.strong[index].desc1)
			lb_recomlv.Text=Util.GetText(self.strong[index].desc2)
		end
	end

	--推荐战力，通过function_id来查找
	for k,v in pairs(self.strongLevel) do
		if k == self.strong[index].function_id then
			lb_recomlvnum.Text=v
		end
	end

	--进度条
	gg_status1.Text=lb_lvnum.Text..'/'..lb_recomlvnum.Text

	--比例值
	local proportion=tonumber(lb_lvnum.Text)/tonumber(lb_recomlvnum.Text)
	
	if proportion >=1 then 
		gg_status1.Value=100
	else
		gg_status1.Value=proportion*100
	end

	--根据比例设置图片
	UIUtil.SetImage(lb_status1,StrongModel.CalculateProportion(proportion))

	--通过function_id打开不同的功能
	btn_to.TouchClick=function()
		self.ui:Close()
		GlobalHooks.UI.OpenUI(self.strong[index].function_background,0,self.strong[index].function_tag)
	end
end


--设置进度条
local function SetFightPowerProgress(self)

	local nowPower =DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.FIGHTPOWER)
	local recomPower=self.strongLevel.nominate_fight
	self.lb_nownum.Text=nowPower
	self.lb_recomnum.Text=recomPower
	
	--比例值
	local proportion=nowPower/recomPower

	if proportion >=1 then
		self.gg_status.Value=100
	else
		self.gg_status.Value=proportion*100
	end

	self.gg_status.Text=nowPower..'/'..recomPower

	--根据比例设置图片
	UIUtil.SetImage(self.lb_status,StrongModel.CalculateProportion(proportion))
end


function _M.OnInit(self)

	--修为&进度条
	self.lb_nownum=self.ui.comps.lb_nownum
	self.lb_recomnum=self.ui.comps.lb_recomnum
	self.gg_status=self.ui.comps.gg_status
	self.lb_status=self.ui.comps.lb_status
	self.ib_status=self.ui.comps.ib_status
	self.lb_status.Visible=true

	self.sp_list=self.ui.comps.sp_list
	self.cvs_detail=self.ui.comps.cvs_detail
	self.cvs_detail.Visible=false

end


function _M.OnEnter(self)

	--获取到每个模块的战力值
	StrongModel.RequestModuleFight(function(resp)
        --新建键值对表，对应传过来的数据
        local moduleFight={}
		for i, v in ipairs(resp.scoremap) do
			local str = StrongModel.GetFunctionIdById(i)
			if str then
				moduleFight[str.function_id] = v
			end
		end
		
		--获取小于人物等级的宝典
		self.strong=StrongModel.GetAllStrongData()
	    --获取某个等级区间段内的功能目标值
		self.strongLevel=StrongModel.GetStrongLevel()
		--设置战力进度条
		SetFightPowerProgress(self)

        local function UpdateElement(node,index)
			SetDetail(self,node,index,moduleFight)
		end

		UIUtil.ConfigVScrollPan(self.sp_list, self.cvs_detail,#self.strong,UpdateElement)
    end)
end

return _M
