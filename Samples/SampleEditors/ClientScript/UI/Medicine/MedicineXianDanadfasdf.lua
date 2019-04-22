local _M = {}
_M.__index = _M

local MedicineModel = require 'Model/MedicineModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
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


	--物品ID
	--有设置物品且背包内有该道具.
	self:SetCurUseItem(d.itemID)

	self:UpdateMedicineCount()


    --将MAP里的物品转换成List
		local itemID = 0
		local itemCount = 0
		local itemCvs
		local itemLbCount
		local itemLbName
		local itemLbDesc

		local select_index 
		local select_node
 		local function UpdateElement(node, index)
 				   --item图标Canvas
 				   itemCvs = node:FindChildByEditName('cvs_item', true)
 			       itemID = itemIndex[index]
 			       itemCount = itemCountInBag[itemID]
 			       itemLbCount = node:FindChildByEditName('lb_num', true)
 			       itemLbName = node:FindChildByEditName('lb_name', true)
 			       itemLbDesc =node:FindChildByEditName('lb_action', true)


        	 local itemdetail = ItemModel.GetDetailByTemplateID(itemID)
        	 itemLbName.Text =  Util.GetText(itemdetail.static.name)
        	 itemLbDesc.Text = Util.GetText(itemdetail.static.using_desc)
         	 local icon = itemdetail.static.atlas_id
    		 local quality = itemdetail.static.quality
    		 local canUse = false
    		 			if itemdetail.static.level_limit <= DataMgr.Instance.UserData.Level then
    		 				canUse = true
    		 			end
			 local itshow = UIUtil.SetItemShowTo(itemCvs, icon, quality, 1)
    			   itshow.EnableTouch = true
    			   itshow.IsCircleQualtiy = true
    			   itshow.TouchClick = function() 
    			  	 	canUse = false
    		 			if itemdetail.static.level_limit <= DataMgr.Instance.UserData.Level then
    		 				canUse = true
    		 			end
    			   		local pos = self.menu:LocalToUIGlobal(itshow)
    			   		if canUse == true then    	
    			   	 		 UIUtil.ShowGetItemWay({detail = itemdetail, itshow = itshow,x = pos.x - 333 ,y = 150})
    			   	 	else 
    			   	 		local detail = UIUtil.ShowNormalItemDetail({anchor = 'r_t',x=pos.x-10,y=150,detail = itemdetail, autoHeight = true})
    			   	 	end
    			   	end

    			   if itemCount ~= 0 then
    			   	local img =  UIUtil.FindChildByUserTag(itemCvs,599999) 
    			   	if img ~= nil then
    			   		img.Visible = false
    			   	end
    			   elseif itemCount == 0 then
    			   		local img = UIUtil.FindChildByUserTag(itemCvs,599999) 
    			   		if img == nil then
    						local img  = HZImageBox.CreateImageBox()
            				img.Size2D = itemCvs.Size2D
            				img.UserTag = 599999
            				itemCvs:AddChild(img)
            				UIUtil.SetImage(img, 'static/item/add.png')
            			else 
            				img.Visible = true
            			end
    		 	   end
    		 	   --数量足但等级未到不能使用.
    		 	   if itemCount > 0 then
    		 	   		itemLbCount.Visible = true
    		 	   		itemLbCount.Text = itemCount
    		 	   		if canUse == true then
    		 	   			itshow.LevelLimit = false
    		 	   			itshow.Num = 1

    		 	   		else
    		 	   			itshow.LevelLimit = true
    		 	   		end
    		 	   else
    		 	   		itemLbCount.Visible = false
    		 	   		itshow.LevelLimit = false
    			   end
    			   	local selectImg = node:FindChildByEditName('ib_select', true)
					selectImg.Visible = select_index == index
    			    node.TouchClick = function() 
    			    	if select_node == node then
    			    		return
    			    	end   			   		
    			   		if select_node then
    			   			local lastselectImg = select_node:FindChildByEditName('ib_select', true)
    			   			lastselectImg.Visible = false
    			   		end
    			   		select_node = node
    			   		select_index = index
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
		 UIUtil.ConfigVScrollPan(self.comps.sp_list, self.comps.cvs_list, itemMapLength, UpdateElement)
	end

    		--print('Medicine itemMapLength = ',itemMapLength)
	 UIUtil.ConfigVScrollPan(self.comps.sp_list, self.comps.cvs_list, itemMapLength, UpdateElement)

	--背包内没有该物品置灰，有显示原色，
	--点击物品直接替换药剂.

	--自动筛选逻辑TODO
	--自动筛选触发逻辑TODO
end

function _M.SetCurUseItem(self,itemID)
	print('MedicineMain.SetCurUseItem',itemID)
	--self.itemID = itemID
	--if self.itemID ~= 0  then
	--	local itemCount =  DataMgr.Instance.UserData.Bag:Count(self.itemID)
		
	--		local itemdetail = ItemModel.GetDetailByTemplateID(self.itemID)
    --		local icon = itemdetail.static.atlas_id
    --		local quality = itemdetail.static.quality
    	
    	--	local itshow = UIUtil.SetItemShowTo(self.ui.comps.cvs_usingmedicine, icon, quality, itemCount)
    	--		  itshow.EnableTouch = true
 		--		   if itemCount == 0 then
    	--		   	itshow.IsGray = true
    	--		   else itshow.IsGray = false
    	--	 	   end
        --        itshow.TouchClick = function()
                 --  local detail = UIUtil.ShowNormalItemDetail({x=self.ui.comps.cvs_usingmedicine.X - 42,y=111,detail = itemdetail, itemShow = itshow, autoHeight = true})
        --    end
	--else
		--self.ui.comps.cvs_usingmedicine:RemoveAllChildren(true)
	--end
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
	
			
	end

return _M