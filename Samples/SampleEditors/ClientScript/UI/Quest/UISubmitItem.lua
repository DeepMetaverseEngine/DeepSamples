local _M = {}
_M.__index = _M
local QuestModel = require "Model/QuestModel.lua"
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

	for i=1,#self.btnList do
		if self.btnList[i].Visible then
			local SubmitItemData = {}
			local select_index = self.listener:GetSourceIndex(i - 1)
			if total > 0 then
				SubmitItemData.index = select_index
				local num = total - self.btnNum[i]
				if num >= 0 then
					SubmitItemData.count = self.btnNum[i]
				else 
					SubmitItemData.count = total
				end
				total = total - SubmitItemData.count
				table.insert(SubmitItemList,SubmitItemData)
			end
		end
	end
	-- print("SubmitItemList")
	-- print_r(SubmitItemList)
	if total == 0 then
		QuestModel.requestSumbitItem(SubmitItemList,self.questid,function()
		-- body
			Close(self)
		end)
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
	-- local data = string.split(static.condition_args,',')
	-- if #data == 0 then
	-- 	error("Questsubmitdetail is error with id ",Questid)
	-- 	return
	-- end
	-- print("data")
	-- print_r(data)
	local itemid = static.condition.arg1[1]
	local itemcount = static.condition.val[1]
	self.SumbiteTotal = itemcount - Quest.ProgressCur
	print("self.SumbiteTotal"..self.SumbiteTotal)
	--self.SumbiteTotal = tonumber(data[#data]) - Quest.ProgressCur
	self.itemPanel = ItemPanel(self.comps.cvs_item1.Size2D)
	self.itemPanel:AddLogicNode(0,self.comps.cvs_item1)
	self.itemPanel:AddLogicNode(1,self.comps.cvs_item2)
	self.itemPanel:AddLogicNode(2,self.comps.cvs_item3)
	self.itemPanel:AddLogicNode(3,self.comps.cvs_item4)
	self.itemPanel:AddLogicNode(4,self.comps.cvs_item5)
	self.btnList = 
	{
		self.comps.ib_opt1,
		self.comps.ib_opt2,
		self.comps.ib_opt3,
		self.comps.ib_opt4,
		self.comps.ib_opt5,
	}
	self.btnNum = {}
	self.itemPanel.EnableSelect = false
	self.listener = ItemListener(DataMgr.Instance.UserData.Bag, false, DataMgr.Instance.UserData.Bag.Size)
	self.listener.OnMatch = function(itdata)
	  	local templateid = itdata.TemplateID
	  		if itemid == templateid then
  					print("match on ",templateid)
  				return true
  			end
  			-- for j = #data,1,-1 do
  			-- 	if tonumber(data[j]) == templateid then
  			-- 		print("match on ",templateid)
  			-- 		return true
  			-- 	end
  			-- end
  		return false
	end
	self.listener.OnCompare = function(item1,item2)
		if item1.Count > item2.Count then
			return 1
		end
		return -1
	end
	self.itemPanel.OnItemInit = function (sender)
		-- print("OnItemInit",sender.IsEmpty)
		-- print("sender.Num", sender.Num)
		self.btnNum[sender.Index + 1] = sender.Num
		self:SelectItem(sender.Index,sender.IsEmpty,not sender.IsEmpty)
	end
	self.listener:Start(true, false)
	self.itemPanel:Init(self.listener)

	self.itemPanel.OnItemClick = function (sender)
		if  sender.IsEmpty then 
			return
		end
		self:SelectItem(sender.Index,sender.IsEmpty,not self.btnList[sender.Index + 1].Visible)
	end

	self.comps.tbh_text.Text = Util.GetText('quest_needitem')..self.SumbiteTotal

end
function _M.OnExit( self )
	print('OnExit')
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
	
end

return _M