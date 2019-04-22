local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local GetBackModel = require 'Model/GetBackModel'
local ItemModel = require 'Model/ItemModel' 


local function ShowCost(self,node,cost,timeLeft)
	-- local detail = ItemModel.GetDetailByTemplateID(2)
	local detail = ItemModel.GetDetailByTemplateID(cost.id[1])
    local itemIcon = 'static/item/' .. detail.static.atlas_id
	local huobi = UIUtil.FindChild(node,'ib_yuanbaoicon',true)
    UIUtil.SetImage(huobi,itemIcon)

	local ownerValue = ItemModel.CountItemByTemplateID(cost.id[1])
	local costValue = cost.num[1] * timeLeft
	local priceLabel = UIUtil.FindChild(node,'lb_price',true)
	priceLabel.Text = costValue
	if ownerValue >= costValue then
		priceLabel.FontColorRGB = Constants.Color.Normal2
	else
		priceLabel.FontColorRGB = Constants.Color.Red
	end
     
end

local function ShowRewardItem(self,node,groupID,timeLeft)
	local rewardItems = GetBackModel.GetReawrdGroupReward(groupID,self.ydayLv)
     
    for index = 1, 2 do
    	local itemIconCvs = UIUtil.FindChild(node, 'cvs_itemicon' .. index,true)
    	itemIconCvs.Visible = false
    end

	for index,templateID in pairs(rewardItems.id) do
		if templateID > 0 then
			local Count = rewardItems.num[index]
	    	local itemDetial = ItemModel.GetDetailByTemplateID(templateID)

			local itemIconCvs = UIUtil.FindChild(node, 'cvs_itemicon' .. index,true)
			itemIconCvs.Visible = true
		 	local itemIcon = UIUtil.FindChild(itemIconCvs, 'cvs_item')
 

    		local itShow = UIUtil.SetItemShowTo(itemIcon,itemDetial.static.atlas_id, itemDetial.static.quality,Count * timeLeft)
    		itShow.EnableTouch = true
    		itShow.TouchClick = function ( ... )
 				local detail = UIUtil.ShowNormalItemDetail({detail=itemDetial,itemShow=itShow,autoHeight=true})
    		end
		end
	end
end  

local function showItem(self,node,index)

	local data = self.rbackDatas[index] 
	 
	local ib_icon = UIUtil.FindChild(node,'ib_actiicon')
	UIUtil.SetImage(ib_icon, data.activity_icon)

	MenuBase.SetLabelText(node, 'lb_actiname', Util.GetText(data.activity_name), 0, 0)
	UIUtil.SetImage(ib_icon, data.activity_icon)

	local btnFree = UIUtil.FindChild(node, 'btn_freebuy',true)
	btnFree.TouchClick = function ( ... )
        -- print('btnFree:',data.function_id)
        GetBackModel.ClientFreeGetRewardBackRequest(data.function_id,function ( ... )
        	-- body
        	_M.ShowScrollPan(self)
        end)
    end

	local btnCost = UIUtil.FindChild(node, 'btn_yuanbaobuy',true)
	btnCost.TouchClick = function ( ... )
        print('btnCost:',data.function_id)
    	GetBackModel.ClientCostGetRewardBackRequest(data.function_id,function ( ... )
        	-- body
        	_M.ShowScrollPan(self)
        end)
    end

 	ShowRewardItem(self,node,data.group_id,data.timeLeft)

 	ShowCost(self,node,data.cost,data.timeLeft)

end

function _M.ShowScrollPan(self)
	local function eachupdatecb(node, index) 
		showItem(self,node,index)
	end

	GetBackModel.ClientGetBackInfoRequest(function (data,costID,totalCost,ydayLv)
		self.rbackDatas = data
		self.ydayLv = ydayLv

		UIUtil.ConfigVScrollPan(self.pan,self.tempnode,#self.rbackDatas,eachupdatecb)

		if totalCost > 0 then
			local detail = ItemModel.GetDetailByTemplateID(costID)
    		local itemIcon = 'static/item/' .. detail.static.atlas_id
    		UIUtil.SetImage(self.ui.comps.ib_oneybicon,itemIcon)
    		self.ui.comps.lb_onebuyprice.Visible = true
			self.ui.comps.lb_onebuyprice.Visible = true
			self.ui.comps.lb_onebuyprice.Text = totalCost

			local owner = ItemModel.CountItemByTemplateID(costID)
			if owner >= totalCost then
				self.ui.comps.lb_onebuyprice.FontColorRGB = Constants.Color.Normal2
			else
				self.ui.comps.lb_onebuyprice.FontColorRGB = Constants.Color.Red
			end

		end

		if #self.rbackDatas == 0 then
			self.ui.comps.cvs_noresource.Visible = true

			self.ui.comps.lb_onebuyprice.Visible = false
			self.ui.comps.lb_onebuyprice.Visible = false
			self.ui.comps.lb_onebuyprice.Text = 0


			self.onefreebuy.IsGray = true
			self.onefreebuy.Enable = false
			self.oneybbuy.IsGray = true
			self.oneybbuy.Enable = false

		else

			self.ui.comps.cvs_noresource.Visible = false

		end
	end) 
end

function _M.OnEnter( self, ...)

	_M.ShowScrollPan(self)
end

function _M.OnExit( self )
     
end

function _M.OnDestory( self )
     
end

function _M.OnInit( self )

 	self.pan = self.ui.comps.sp_activities
	self.tempnode = self.ui.comps.cvs_activitylists
	self.tempnode.Visible = false
	
    
	self.onefreebuy = self.ui.comps.btn_onefreebuy
    self.onefreebuy.TouchClick = function ( ... )
        print('one free')
		GetBackModel.ClientFreeGetRewardBackRequest('',function ( ... )
        	_M.ShowScrollPan(self)
        end)

    end

    self.oneybbuy = self.ui.comps.btn_oneybbuy
    self.oneybbuy.TouchClick = function ( ... )
         print('one key')
		GetBackModel.ClientCostGetRewardBackRequest('',function ( ... )
        	_M.ShowScrollPan(self)
        end)
    end
end

return _M