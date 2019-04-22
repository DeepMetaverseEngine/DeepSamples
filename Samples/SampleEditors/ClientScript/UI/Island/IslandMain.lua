local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local IslandModel = require 'Model/IslandModel'
local ItemModel = require 'Model/ItemModel'
local TimeUtil = require 'Logic/TimeUtil'
local RechargeModel=require('Model/RechargeModel')

local spacing=25
local spacing2 = 5
local effectZ = -100


local function ShowMain(self)
	self.pan1.Visible = true
	self.temp1.Visible = false
	self.neirongCvs.Visible = false
end 


local function ShowRewardNode(parentNode,templateId,num)
	local itemDetial = ItemModel.GetDetailByTemplateID(templateId)
	local itShow = UIUtil.SetItemShowTo(parentNode,itemDetial.static.atlas_id, itemDetial.static.quality,num)
	itShow.EnableTouch = true
	itShow.TouchClick = function ( ... )
		local detail = UIUtil.ShowNormalItemDetail({detail=itemDetial,itemShow=itShow,autoHeight=true})
				-- detail:SetPos(0,350)
	end
end


local function  showEffectirstPassReward(self,parent,checkPointId)
	if self.effMap[checkPointId] ~= nil then
		RenderSystem.Instance:Unload(self.effMap[checkPointId])
	end
	local transSet = TransformSet()
	transSet.Pos =  Vector3(parent.Width/2,-parent.Height/2,effectZ)
	-- transSet.Parent = self.ui.comps.cvs_pulse1.Transform
	transSet.Parent = parent.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = self.ui.menu.MenuOrder
	local assetname = '/res/effect/ui/ef_ui_interface_frame.assetbundles'
	local effId =   RenderSystem.Instance:PlayEffect(assetname, transSet)
	self.effMap[checkPointId] = effId
	return effId
end


local function RemoveEff(self,checkPointId)
	local effId = self.effMap[checkPointId]
	if effId then
		RenderSystem.Instance:Unload(effId)
		self.effMap[checkPointId] = nil
	end
end


local function ClearEff(self)
	for k,effId in pairs(self.effMap or {}) do
		if effId then
			RenderSystem.Instance:Unload(effId)
		end
	end
	self.effMap = {}
end


local function ShowFirstPassRewardNode(self,node,parentNode,templateId,num,checkPointId)
	local PointSnapData = self.PointSnapMap[checkPointId]
	local ibgot = UIUtil.FindChild(node,'ib_got')
	ibgot.Visible = false
	local ibget = UIUtil.FindChild(node,'ib_get')
	ibget.Visible = false

	local itemDetial = ItemModel.GetDetailByTemplateID(templateId)
	local itShow = UIUtil.SetItemShowTo(parentNode,itemDetial.static.atlas_id, itemDetial.static.quality,num)
	itShow.EnableTouch = true
	itShow.TouchClick = function ( ... )
		if PointSnapData.state == 1 then
			IslandModel.ReqGetPass1stReward(checkPointId,function (resp)
   				self.PointSnapMap[checkPointId].state = 2
   				--TODO 删除特效
   				RemoveEff(self,checkPointId)
   				ibgot.Visible = true
   				ibget.Visible = false
			end)
		else
			local detail = UIUtil.ShowNormalItemDetail({detail=itemDetial,itemShow=itShow,autoHeight=true})
			-- detail:SetPos(0,350)
		end
	end
	
	if PointSnapData.state == 1 then
		--显示奖励特效
		showEffectirstPassReward(self,parentNode,checkPointId)
		ibget.Visible = true
	elseif PointSnapData.state == 2 then
		ibgot.Visible = true
	end
end


local function ShowChapterData(self,node,chapterData,checkPointDatas)
 	MenuBase.SetLabelText(node, 'lb_landname', Util.GetText(chapterData.chapter_name), 0, 0)
 	--让字竖着显示
	self.lb_landname.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap

	local templateId1 = chapterData.item.id[1]
 	local rewardNode1 = UIUtil.FindChild(node,'cvs_reward1')
 	local templateId2 = chapterData.item.id[2]
 	local rewardNode2 = UIUtil.FindChild(node,'cvs_reward2')
	
 	ShowRewardNode(rewardNode1,templateId1)
	ShowRewardNode(rewardNode2,templateId2)

 	local chapterId = chapterData.chapter_id
	
 	local bgImg = UIUtil.FindChild(node,'ib_landpic')
 	UIUtil.SetImage(bgImg,chapterData.background)
 	local openId = chapterData.checkpoint_open_id
	local lockImg = UIUtil.FindChild(node,'ib_lock')
	local lockLabel = UIUtil.FindChild(node,'lb_lock')
 	-- local  passed = false
 	local isClose = false
 	if openId == 0 then
 		lockImg.Visible = false
 		lockLabel.Visible = false
 		isClose = false
 	else
 		local OpenSnapData = self.PointSnapMap[openId]
 		if OpenSnapData ~= nil and OpenSnapData.state ~= 0 then
 			lockImg.Visible = false
 			lockLabel.Visible = false
 			isClose = false 
 		else
 			lockImg.Visible = true
 			lockLabel.Visible = true
 			isClose = true 
 		end
 	end
	-- bgImg.Visible
	local passId = chapterData.checkpoint_pass_id
	local PassSnapData = self.PointSnapMap[passId]
 	local passImg = UIUtil.FindChild(node,'ib_passed')
 	
 	if PassSnapData ~= nil and PassSnapData.IsPassed then
 		passImg.Visible = true
 	else 
 		passImg.Visible = false
 	end
 	return isClose
end


local function ShowPointItem(self,node,pointData,index)
	MenuBase.SetLabelText(node, 'lb_stage', Util.GetText('god_island_checkpoint',pointData.checkpoint_id), 0, 0)
	MenuBase.SetLabelText(node, 'lb_stagename', Util.GetText(pointData.checkpoint_name), 0, 0)
	MenuBase.SetLabelText(node, 'lb_fightnum', Util.GetText(pointData.power), 0, 0)
  --   node.TouchClick = function(sender)
		-- ShowMain(self)
  --   end

  	local checkPointId  = pointData.checkpoint_id
  	local PointSnapData = self.PointSnapMap[checkPointId]
  	local playerLabel = UIUtil.FindChild(node,'lb_player')
  	local timeLabel = UIUtil.FindChild(node,'lb_time')

  	if not string.IsNullOrEmpty(PointSnapData.PassName1st) then
  		playerLabel.Visible = true
  		timeLabel.Visible = true
		playerLabel.Text = PointSnapData.PassName1st
		timeLabel.Text = TimeUtil.formatCD("%M:%S", PointSnapData.PassTime1st)
  	else	
  		playerLabel.Visible = false
  		timeLabel.Visible = false
  	end
	-- local timeLabel = UIUtil.FindChild(node,'lb_time')
	-- timeLabel.Text = PointSnapData.PassTime1st
	-- MenuBase.SetLabelText(node, 'lb_time', TimeUtil.formatCD("%M:%S", PointSnapData.PassTime1st), 0, 0)
 
  	local startBtn = UIUtil.FindChild(node,'btn_start')
	local btn_sweep=node:FindChildByEditName('btn_sweep',true)
  	local finishImg = UIUtil.FindChild(node,'ib_stageclear')
	
  	if checkPointId == 1 or  checkPointId <= (self.FinallyCheckPointId + 1) then
  		startBtn.Visible = true
  	else
  	 	startBtn.Visible = false
  	end
  	if checkPointId <= self.FinallyCheckPointId then
  		finishImg.Visible = true
  	else
  		finishImg.Visible = false
  	end

	local rewardNode = UIUtil.FindChild(node,'cvs_item')
	local templateId = pointData.first_item_id
	local num = pointData.first_item_num
	ShowFirstPassRewardNode(self,node,rewardNode,templateId,num,checkPointId)

  	startBtn.TouchClick = function ( sender )
		if checkPointId > self.FinallyCheckPointId + 1 then
  	 		GameAlertManager.Instance:ShowFloatingTips(Constants.Text.godisland_passbefore)
  	  		return 
  	 	end
		IslandModel.ReqEnterIsland(checkPointId,function (resp)
			self.ui:Close()
		end)
  	end
	--扫荡
	btn_sweep.Visible=startBtn.Visible
	btn_sweep.TouchClick=function(sender)
		local land=IslandModel.GetFunctionIdByCheckPoint(checkPointId)
		IslandModel.RequestSweepIsland(land.function_id, function(rsp)
			if rsp:IsSuccess() then
				self.TimeLeft=self.TimeLeft-1
				self.ui.comps.lb_count.Text = Util.GetText(Constants.Text.TimeOrCount,self.TimeLeft)
			end
		end)
	end
end


local function MoveNeirongCvs(self)
	self.neirongCvs.Position2D={self.neirongCvsPos.x - self.temp1.Size2D.x,self.neirongCvsPos.y}
    local moveToShow = MoveAction()
	moveToShow.Duration =0.2
	moveToShow.TargetX = self.neirongCvsPos.x
	moveToShow.TargetY = self.neirongCvsPos.y
	moveToShow.ActionEaseType = EaseType.linear
	self.neirongCvs:AddAction(moveToShow)
end


local function MoveTemp1Cvs(self,node)
	self.temp1.Position2D = {node.Position2D.x,self.temp1Pos.y}
    local moveToShow = MoveAction()
	moveToShow.Duration =0.2
	moveToShow.TargetX = self.temp1Pos.x
	moveToShow.TargetY = self.temp1Pos.y
	moveToShow.ActionEaseType = EaseType.linear
	self.temp1:AddAction(moveToShow)
end


local function ShowPointScrollPan(self,chapterData,checkPointDatas)
	self.pan1.Visible = false
	self.temp1.Visible = true
	ShowChapterData(self,self.temp1,chapterData,checkPointDatas)

	local function eachupdatecb(node, index) 
		local pointData = checkPointDatas[index] 
		ShowPointItem(self,node,pointData,index)
	end
	UIUtil.ConfigHScrollPanWithOffset(self.pan2,self.temp2,#checkPointDatas,spacing2,eachupdatecb)
end


local function ShowChapterItem(self,node,index)
	--让字竖着显示
	UIUtil.FindChild(node,'lb_landname').TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap

	local chapterData = self.islandDatas[index]
	local chapterId = chapterData.chapter_id
	local checkPointDatas = IslandModel.GetCheckPoint(chapterId)
 	local isClose = ShowChapterData(self,node,chapterData,checkPointDatas)

    node.TouchClick = function(sender)
      	if isClose then
      		GameAlertManager.Instance:ShowFloatingTips(Constants.Text.godisland_passlock)
      		return
      	end
      	ShowPointScrollPan(self,chapterData,checkPointDatas)
 		self.neirongCvs.Visible = true
		MoveNeirongCvs(self,node)
		MoveTemp1Cvs(self,node)
    end
end


local function ShowChapterScrollPan(self)
	local function eachupdatecb(node, index) 
		ShowChapterItem(self,node,index)
	end
 	self.pan1.Visible = true
 	self.temp1.Visible = false
 	self.neirongCvs.Visible = false
	UIUtil.ConfigHScrollPanWithOffset(self.pan1,self.temp1,#self.islandDatas,spacing,eachupdatecb)
	-- UIUtil.ConfigHScrollPan(self.pan1,self.temp1,#self.islandData,eachupdatecb)
end


--购买额外次数
local function BuyExtraCount(self)
	FunctionUtil.CanBuyTicketsRequest('god_island_tickets')
end
 

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	self.effMap = {}
	self.PointSnapMap = {}
	IslandModel.ReqIslandInfo(function (resp)
		self.PointSnapMap = resp.PointSnapMap or {}
		self.FinallyCheckPointId = resp.FinallyCheckPointId
		self.TimeLeft = resp.TimeLeft
		self.ui.comps.lb_count.Text = Util.GetText(Constants.Text.TimeOrCount,self.TimeLeft)
		
		self.ui.comps.btn_count.TouchClick=function(sender)
			BuyExtraCount(self)
		end
		ShowChapterScrollPan(self)
	end)
	
	function _M.OnTicketChange(eventname,params)
		if params.functionid == 'god_island_tickets' then
			GameAlertManager.Instance:ShowNotify(Constants.Text.shop_buysucc)
			self.TimeLeft=self.TimeLeft+1
			self.ui.comps.lb_count.Text=Util.GetText(Constants.Text.TimeOrCount,self.TimeLeft)
		end
	end
	EventManager.Subscribe("Event.TicketChange", _M.OnTicketChange)
end


function _M.OnExit( self )
	-- testdetail:Close()
	ClearEff(self)
	EventManager.Unsubscribe("Event.TicketChange",_M.OnTicketChange)
end


function _M.OnDestory( self )
	ClearEff(self)
end


function _M.OnInit( self )
	self.count = self.ui.comps.lb_count
	self.lb_landname=self.ui.comps.lb_landname
	self.leftBtn = self.ui.comps.btn_left
	self.rightBtn = self.ui.comps.btn_right
	self.temp1 = self.ui.comps.cvs_land
	self.temp1.Visible = false
	self.temp1.TouchClick = function (sender)
		ShowMain(self)
	end

	self.pan1 = self.ui.comps.sp_landlist
	self.temp2 = self.ui.comps.cvs_details
	self.temp2.Visible = false
	self.pan2 = self.ui.comps.sp_stagelist
	self.neirongCvs = self.ui.comps.cvs_neirong
	 
	self.neirongCvs.Visible = false
	self.neirongCvsPos = self.neirongCvs.Position2D
	self.temp1Pos  = self.temp1.Position2D
	self.islandDatas = IslandModel.GetGodIsland()
end


return _M