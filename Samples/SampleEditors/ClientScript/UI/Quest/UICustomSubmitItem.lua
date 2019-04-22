local _M = {}
_M.__index = _M
local QuestModel = require "Model/QuestModel.lua"
local ItemModel = require 'Model/ItemModel'
local Util = require("Logic/Util")
local function Close(self)
	if self.CloseCallBack ~= nil then 
		self.CloseCallBack()
	end
	self.ui:Close()
end
function _M.SetCloseCallBack(self,cb)
	if self.CloseCallBack == cb then return end
	self.CloseCallBack = cb
end

function _M:SelectItem(index,isEmpty,isSelect)
	-- local slots = self.listener.AllSlots
	if isEmpty then
		self.btnList[index + 1].Visible = false
		return
	end
	
	self.btnList[index + 1].Visible = isSelect
	
end
function _M:SubmiteItem()
	
	local SubmitItemList = {}
	local total = self.SumbiteTotal

	if self.SubmitData == nil then return end
	if total > 0 then
		local SubmitItemData = {}
		SubmitItemData.index = self.listener:GetSourceIndex(self.SubmitData.index)
		print("SubmitItemData.index",SubmitItemData.index)
		local num = total - self.SubmitData.count
		if num >= 0 then
			SubmitItemData.count = self.SubmitData.count
		else 
			SubmitItemData.count = total
		end
		total = total - SubmitItemData.count
		table.insert(SubmitItemList,SubmitItemData)
		self.SubmitData = nil
	end

	if total == 0 then
		if self.submittype == QuestCondition.SubmitCustomItem  then
			print("SubmitCustomItem")
			QuestModel.requestCustomSumbitItem(SubmitItemList,self.questid,function()
				Close(self)
			end)
		elseif self.submittype == QuestCondition.SubmitItem  then
			print("SubmitItem")
			QuestModel.requestSumbitItem(SubmitItemList,self.questid,function()
				Close(self)
			end)
		end
	else
		local str = Util.GetText('quest_notenoughitem')
		GameAlertManager.Instance:ShowNotify(str)
	end
	
end
function _M.OnEnter( self, params)
	--print(params)
	self.questid = params
	local Quest = DataMgr.Instance.QuestData:GetQuest(params)
	local static = Quest.Static


	self.submittype = static.condition.type[1] 
	local itemid = static.condition.arg1[1]
	local itemcount = static.condition.val[1]
	self.SumbiteTotal = itemcount - Quest.ProgressCur
	print("self.SumbiteTotal"..self.SumbiteTotal)
	

    if self.submittype == QuestCondition.SubmitCustomItem  then
		local data = GlobalHooks.DB.Find('Submit_item',itemid)
		if data == nil then 
			UnityEngine.Debug.LogError("doConditionSubmitCustomItem data error with customitemgroup "..itemid)
			return 
		end
	    
	    self.listener.OnMatch = function(itdata)
	        local detail = ItemModel.GetDetailByTemplateID(itdata.TemplateID)
			for i,v in ipairs(data.filter.type) do
				if not string.IsNullOrEmpty(v) then
					local min =	tonumber(data.filter.min[i])
					local max = tonumber(data.filter.max[i])
					local curValue = detail.static[v]
					if curValue == nil then
						error("error type with "..v.." itemgroup "..itemid)
					end
					print(i,v)
					
					if curValue >= min and curValue <= max and itdata.Count >= itemcount then
						return true
					else
						return false
					end
				end
			end
			return false
	    end
	elseif self.submittype == QuestCondition.SubmitItem then
		self.listener = ItemListener(DataMgr.Instance.UserData.Bag, false, DataMgr.Instance.UserData.Bag.Size)
	    self.listener.OnMatch = function(itdata)
	        local templateid = itdata.TemplateID
	  		if itemid == templateid then
  			   return true
  			end
  			return false
	    end

	end
    self.listener.OnCompare = function(item1,item2)
		if item1.Count > item2.Count then
			return 1
		end
		return -1
	end
	
    self.SubmitData = nil
    self.listener:Start(true, false)
    local count = self.currentMaxCount
    if self.listener.ItemCount > self.currentMaxCount then
    	 count = self.listener.ItemCount
    else
    	count = self.currentMaxCount
    end
	self.list:Init(self.listener, count)
    
    
   

	self.comps.tbh_text.Text = Util.GetText('quest_needitem')..self.SumbiteTotal

end
function _M.OnExit( self )
	print('OnExit')
	self.listener:Dispose()
end

function _M.OnDestory( self )
	print('OnDestory')

end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.comps.btn_close.event_PointerUp = function(sender)
		Close(self)
	end
	self.ui.menu.event_PointerUp = function(sender)
		Close(self)
	end

	self.ui.comps.btn_give.event_PointerUp = function(sender)
		self:SubmiteItem()
	end
	self.currentMaxCount = 4
	
    self.list = ItemList(self.comps.cvs_list.Size2D, Constants.Item.DefaultSize, self.currentMaxCount)
    self.list.Position2D = self.comps.cvs_list.Position2D
    self.comps.cvs_list.Parent:AddChild(self.list)
    self.listener = ItemListener(DataMgr.Instance.UserData.Bag, false, DataMgr.Instance.UserData.Bag.Size)
    self.list.ShowBackground = true
    self.list.EnableSelect = true
    self.list.OnItemSingleSelect = function(new, old)
       self.SubmitData = {}
       self.SubmitData.index = new.Index
       self.SubmitData.count = new.Num
    end
end

return _M