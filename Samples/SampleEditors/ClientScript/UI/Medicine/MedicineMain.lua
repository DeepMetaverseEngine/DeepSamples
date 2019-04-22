local _M = {}
_M.__index = _M

local MedicineModel = require 'Model/MedicineModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local itemCountInBag = {}
local itemIndex = {}
local itemMapLength = 0

local function InitInfo( self)
   
end

 function _M.UpdateMedicineCount()
 	--print('MedicineMain.UpdateMedicineCount---------------------------------------')
--血瓶对应背包内的数量.
	 itemCountInBag = {}
	--排序
	 itemIndex = {}
	--获取所有种类的血拼及对应数量.
	itemMapLength = 0
	for i,v in pairs(MedicineModel.ItemsTable) do
		itemMapLength = itemMapLength + 1
		local itemCount =  DataMgr.Instance.UserData.Bag:Count(v.item_id)
		itemCountInBag[v.item_id] = itemCount
		itemIndex[itemMapLength] = v.item_id
	end
end

function _M.OnEnter( self )

	local d = DataMgr.Instance.UserData.GameOptionsData
	--自动吃药
	self.ui.comps.tbt_use1.IsChecked = d.AutoUseItem
	--自动选择
	self.ui.comps.tbt_use2.IsChecked = d.SmartSelect
	--吃药血量阀值
	self.ui.comps.gg_hp:SetGaugeMinMax(0, 100)
	self.ui.comps.gg_hp.Value = d.UseThreshold --1~99
	self.ui.comps.gg_hp:SetPullNode(self.ui.comps.ib_joystick)

	--物品ID
	--有设置物品且背包内有该道具.
	self:SetCurUseItem(d.itemID)

	self:UpdateMedicineCount()


    --将MAP里的物品转换成List
		local itemID = 0
		local itemCount = 0
 		local function UpdateElement(node, index)
 			       itemID = itemIndex[index]
 			       itemCount = itemCountInBag[itemID]
        	 local itemdetail = ItemModel.GetDetailByTemplateID(itemID)
         	 local icon = itemdetail.static.atlas_id
    		 local quality = itemdetail.static.quality
    		 local canUse = false
    		 		if itemdetail.static.level_limit <= DataMgr.Instance.UserData.Level then
    		 			canUse = true
    		 		end
			 local itshow = UIUtil.SetItemShowTo(node, icon, quality, itemCount)
    			   itshow.EnableTouch = true
    			   itshow.TouchClick = function() 
    			   local pos = self.menu:LocalToUIGlobal(itshow)
    			   	 if itshow.Num > 0 and canUse == true then    			   	 	
    			   	 self:SetCurUseItem(itemdetail.static.id)
    			   	 	local detail = UIUtil.ShowNormalItemDetail({anchor = 'r_t',x=pos.x ,y=111,detail = itemdetail, itemShow = itshow, autoHeight = true})
    			   	 elseif itshow.Num == 0 then
    			   	 	UIUtil.ShowGetItemWay({detail = itemdetail, itshow = itshow})
    			   	 elseif canUse == false then
    			   	 	local detail = UIUtil.ShowNormalItemDetail({anchor = 'r_t',x=pos.x ,y=111,detail = itemdetail, itemShow = itshow, autoHeight = true})
    			   	 end

    			   end

    			   if itemCount ~= 0 then
    			   	local img =  UIUtil.FindChildByUserTag(node,599999) 
    			   	if img ~= nil then
    			   		img.Visible = false
    			   	end
    			   elseif itemCount == 0 then
    			   		local img = UIUtil.FindChildByUserTag(node,599999) 
    			   		if img == nil then
    						local img  = HZImageBox.CreateImageBox()
            				img.Size2D = node.Size2D
            				img.UserTag = 599999
            				node:AddChild(img)
            				UIUtil.SetImage(img, 'static/item/add.png')
            			else 
            				img.Visible = true
            			end
    		 	   end
    		 	   --数量足但等级未到不能使用.
    		 	   if itemCount > 0 then
    		 	   		if canUse == true then
    		 	   			itshow.LevelLimit = false
    		 	   			itshow.Num = itemCount
    		 	   		else
    		 	   			itshow.LevelLimit = true
    		 	   		end
    		 	   else
    		 	   		 itshow.LevelLimit = false
    			   end
    	end

	--监听药品变更.
	self.listener = ItemListener(DataMgr.Instance.UserData.Bag, true, 0)
	self.listener.OnMatch = function(itdata)
		for i,v in pairs(MedicineModel.ItemsTable) do
			return itdata.TemplateID == v.item_id
		end
    end
    self.listener:Start(false, false)
    self.listener.OnItemUpdateAction = function()
    	print('MedicineMain.OnItemUpdateAction---------------------------------------')
    	 self:UpdateMedicineCount()
    	 local d = DataMgr.Instance.UserData.GameOptionsData
    	 self:SetCurUseItem(d.itemID)
		 UIUtil.ConfigHScrollPan(self.comps.sp_medicinelist, self.comps.cvs_medicine1, itemMapLength, UpdateElement)
	end

    		--print('Medicine itemMapLength = ',itemMapLength)
	 UIUtil.ConfigHScrollPan(self.comps.sp_medicinelist, self.comps.cvs_medicine1, itemMapLength, UpdateElement)

	--背包内没有该物品置灰，有显示原色，
	--点击物品直接替换药剂.

	--自动筛选逻辑TODO
	--自动筛选触发逻辑TODO
end


function _M.SetCurUseItem(self,itemID)
	print('MedicineMain.SetCurUseItem',itemID)
	self.itemID = itemID
	if self.itemID ~= 0  then
		local itemCount =  DataMgr.Instance.UserData.Bag:Count(self.itemID)
		
			local itemdetail = ItemModel.GetDetailByTemplateID(self.itemID)
    		local icon = itemdetail.static.atlas_id
    		local quality = itemdetail.static.quality
    		local itshow = UIUtil.SetItemShowTo(self.ui.comps.cvs_usingmedicine, icon, quality, itemCount)
    			  itshow.EnableTouch = true
 				   if itemCount == 0 then
    			   	itshow.IsGray = true
    			   else itshow.IsGray = false
    		 	   end
                  itshow.TouchClick = function()
                   local detail = UIUtil.ShowNormalItemDetail({x=self.ui.comps.cvs_usingmedicine.X - 42,y=111,detail = itemdetail, itemShow = itshow, autoHeight = true})
            end
	else
		self.ui.comps.cvs_usingmedicine:RemoveAllChildren(true)
	end
end


function _M.OnExit( self )
	print('Medicine OnExit')
	self.listener:Dispose()
end

function _M.OnDestory( self )
	print('Medicine OnDestory')
end

function _M.OnInit( self )
	print('Medicine OnInit')
	--设置类型，界面打开时其他界面不隐藏.
	self.ui.menu.ShowType = UIShowType.Cover
	--点击界面其他部分界面关闭.
  	self.ui.menu.event_PointerUp = function( ... )
 	self.ui:Close()
  	end
	self.itemID  = 0
	
	self.ui.comps.btn_more.TouchClick = function ( sender )
		local v = self.ui.comps.gg_hp.Value
		v = v + 1
		if v>100 then
			v = 100
		end
		self.ui.comps.gg_hp.Value = v
	end

	self.ui.comps.btn_less.TouchClick = function ( sender )
	local v = self.ui.comps.gg_hp.Value
		local v = self.ui.comps.gg_hp.Value
		v = v - 1
		if v<0 then
			v = 0
		end
		self.ui.comps.gg_hp.Value = v
	end									

	self.ui.comps.btn_ok.TouchClick = function ( sender )
		MedicineModel.SaveOptions(self.ui.comps.tbt_use1.IsChecked,
								  self.ui.comps.tbt_use2.IsChecked,
								  self.ui.comps.gg_hp.Value,
								  self.itemID,
								  function( msg )
 									self.ui:Close()
								  end)
	end
	self.ui.comps.btn_cancel.TouchClick = function ( sender )
		 self.ui:Close()
	end
	
	

	end

return _M
