local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local AutoEquipsModel = require 'Model/AutoEquipsModel'

local IsDown = false
local uiself = nil
local data = nil

local function EndOne(self,index)
	if self.myTimer then
		LuaTimer.Delete(self.myTimer)
	end
	if data and data[1] then
		if index then
			if index == data[1].index then
				table.remove(data,1)
			end
		else
			table.remove(data,1)
		end
	else
		data = {}
	end
	self.ui.comps.Visible = false
end

local function getEquipedData(pos)
	return ItemModel.GetDetailByEquipBagIndex(pos)
end

local function ClickEquip(data, cb)
	IsDown = true
	local temp = {}
    if data and data[1] and ItemModel.GetDetailByBagIndex(data[1].index) then
    	temp = ItemModel.GetDetailByBagIndex(data[1].index)
    else
    	temp.score = 0
    end
	if data and data[1] and temp.score == data[1].score then
		DataMgr.Instance.UserData.Bag:Equip(data[1].index, function( success )
			if cb then
				cb()
			else
				table.remove(data,1)
				if #data > 0 then
					ClickEquip(data)
				end
			end
		end)
	else
		if cb then
			cb()
		end
	end	
end

--遍历装备队列并设置UI控件
local function TraverseSetUI(self)
	IsDown = false
	if #data ~= 0 then
		SoundManager.Instance:PlaySoundByKey('zhuangbei',false)
		local equipItem = ItemModel.GetDetailByTemplateID(data[1].id)
		local equipedScore = getEquipedData(data[1].pos) == nil and 0 or getEquipedData(data[1].pos).score
		
		
		if equipedScore == nil or data[1].score > equipedScore then
			UIUtil.SetItemShowTo(self.ui.comps.cvs_item,equipItem, 1)
			self.ui.comps.lb_name.Text = Util.GetText(equipItem.static.name)
			self.ui.comps.lb_name.FontColor = GameUtil.RGB2Color(Constants.QualityColor[equipItem.static.quality])
			
			self.ui.comps.Visible = true
			if self.myTimer then
				LuaTimer.Delete(self.myTimer)
			end
			local time = self.time
			self.myTimer = LuaTimer.Add(
		        0,
		        1000,
		        function()
		            if time <= 0 then
		                self.comps.btn_use.Text = Constants.Text.detail_btn_equip
						ClickEquip(data,function()
							EndOne(self)
							TraverseSetUI(self)	
						end)
		                return false
		            else
		            	if tonumber(unpack(GlobalHooks.DB.Find('GameConfig', {id = 'equipbox_auto_maxlv'})).paramvalue) >= DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0) then
		            		self.comps.btn_use.Text = Constants.Text.detail_btn_equip..string.format('(%01d秒)', time)
							time = time - 1
		            		return true
		            	else
		            		self.comps.btn_use.Text = Constants.Text.detail_btn_equip
		            		return false
		            	end
		            end
		        end)
		else
			EndOne(self)
			TraverseSetUI(self)
		end
	else
		self.ui:Close()
    	return
    end
end

local function ShowItemDetail2(self, detail, x, y)
	local detail_retain = {}
	if not self.detailMenu then
		detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
		self.detailMenu = detailMenu
		self:AddSubUI(detailMenu)
	end
	detailMenu:Reset({detail=detail,autoHeight=true,index=self.select_index,compare=not isEquiped,IsEquiped=isEquiped})
	detailMenu:EnableTouchFrameClose(true)
	detailMenu:SetPos(x-detailMenu.src_size[1], y-detailMenu.src_size[2]/4)
	local function OnDetailExit()
        detailMenu:UnSubscribOnExit(OnDetailExit)
        self.detailMenu = nil
        if detail.itemShow then
            local retain = detail_retain[detail.itemShow]
            retain = retain - 1
            if retain <= 0 then
                detail.itemShow.IsSelected = false
                detail_retain[detail.itemShow] = nil
            else
                detail_retain[detail.itemShow] = retain
            end
        end
    end

    detailMenu:SubscribOnExit(OnDetailExit)
end

local function ShowItemDetail(self, index, x, y)
	local detail = ItemModel.GetDetailByBagIndex(index)
	ShowItemDetail2(self, detail, x, y)
end

local function RemoveEquip(funname,equipInfo)
	if IsDown == false then
		if #data > 1 then
			for i=2,#data do
				if equipInfo == data[i].index then
					table.remove(data,i)
				end
			end
		end
		if equipInfo.index == data[1].index and data then
			table.remove(data,1)
			if #data > 0 then
				EndOne(uiself,equipInfo.index)
				TraverseSetUI(uiself)
			else
				uiself.ui:Close()
			end
		end
	end
end


 
function _M.OnEnter( self, equipsdata )
	self.time = tonumber(unpack(GlobalHooks.DB.Find('GameConfig', {id = 'equipbox_auto_time'})).paramvalue)
	uiself = self
	EventManager.Subscribe("RemoveEquip",RemoveEquip)
	data = equipsdata
	self.ui.comps.Visible = false

	--ui穿透
    self.ui:EnableTouchFrameClose(false)
    self.globalTouchKey = GameGlobal.Instance.FGCtrl:AddGlobalTouchUpHandler("UI.ItemDetail", function( obj, point )
    if self.ui.IsRunning then
        -- OnGlobalTouchUp(self, obj, point)
    end
    end)

    self.ui.comps.btn_use.TouchClick = function( sender )
		ClickEquip(data,function()
			EndOne(self)
			TraverseSetUI(self)
		end)
	end

	self.ui.comps.btn_close.TouchClick = function( sender )
		EndOne(self)
		TraverseSetUI(self)
	end

	self.ui.comps.cvs_item.TouchClick = function( sender )
	    if self.detailMenu == nil then
			ShowItemDetail(self,data[1].index,self.ui.comps.cvs_quickuse.X,self.ui.comps.cvs_quickuse.Y)
		end
	end

	TraverseSetUI(self)

end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)

    --HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_quickuse, bit.bor(HudManager.HUD_BOTTOM))
end


function _M.OnExit( self )
	EventManager.Unsubscribe("RemoveEquip",RemoveEquip)
	if self.globalTouchKey then
		GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(self.globalTouchKey)
		self.globalTouchKey = nil
	end
	if self.myTimer then
		LuaTimer.Delete(self.myTimer)
	end
	if #data > 0 then
		local autolimit = tonumber(unpack(GlobalHooks.DB.Find('GameConfig', {id = 'equipbox_auto_maxlv'})).paramvalue)
		local rolelevel = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)
		if autolimit >= rolelevel then
			ClickEquip(data)
		end
	end
	data = {}
	uiself = nil
	IsDown = false
end

local function fin(relogin)
    if relogin then
    	EventManager.Unsubscribe("RemoveEquip",RemoveEquip)
		if self.globalTouchKey then
		    GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(self.globalTouchKey)
		    self.globalTouchKey = nil
		end
		if self.myTimer then
			LuaTimer.Delete(self.myTimer)
		end
		data = {}
		uiself = nil
		IsDown = false
    end
end

_M.fin = fin
return _M