local _M = {}
_M.__index = _M


local ItemModel = require 'Model/ItemModel'


local showQuene = {}
local waitQuene = {}

local function AddItem(Quene,data)
	local t = {}
	t.index = data.index
	t.pos = data.pos
	t.score = data.score
	t.id = data.id
	table.insert(Quene,t)
end

local function AddUI(params)
	if GlobalHooks.UI.FindUI('AutoEquipsMain') == nil then
		local tempUI = GlobalHooks.UI.CreateUI('AutoEquipsMain', 0,params)
		local menu = nil
		if type(tempUI) == 'table' then
			menu = tempUI.ui.menu
		else
			menu = tempUI
		end
		MenuMgr.Instance:AddMenu(menu)
	end
end


local function ShowUI(params)
	if params == -1 then
		if #showQuene ~= 0 then
			AddUI(showQuene)
		end
	else
		if #waitQuene ~= 0 then
			AddUI(waitQuene)
		end
	end
end


local function GetEquipData(equipID)
	local detail = unpack(GlobalHooks.DB.Find('Equip', {id = equipID}))
	return detail
end
local function GetItemData(equipID)
	local item = unpack(GlobalHooks.DB.Find('Item', {id = equipID}))
	return item
end

local function checkShow(Data,equipInfo, bagItemData,params)
	local temptable = {}
	temptable.score = bagItemData.score
	temptable.id = bagItemData.static_equip.id
	temptable.pos = equipInfo.equip_pos
	temptable.index = params.index
	AddItem(Data, temptable)

	if params.reason ~= "monsterDrop" then
		ShowUI(-1)
	end
end

local function RecData(Data,params)
	local bagItemData = ItemModel.GetDetailByBagIndex(params.index)
	if bagItemData ~= nil then
		if bagItemData.score ~= 0 then 
			local equipInfo = GetEquipData(bagItemData.static_equip.id)
			local itemInfo = GetItemData(bagItemData.static_equip.id)
			local roleLevel = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)
			local rolePro = DataMgr.Instance.UserData.Pro
			local equiped = ItemModel.GetDetailByEquipBagIndex(equipInfo.equip_pos)
			if rolePro < tonumber(unpack(GlobalHooks.DB.Find('GameConfig', {id = 'equipbox_maxlv'})).paramvalue) then
				if equipInfo.profession == rolePro or equipInfo.profession == 0 then
					if roleLevel >= itemInfo.level_limit then
						if equiped == nil then
							checkShow(Data,equipInfo,bagItemData, params)
						else
							 if bagItemData.score > equiped.score  then
								checkShow(Data,equipInfo,bagItemData, params)
							 end
						end
					end
				end
			end
		end
	end
end

local function AddEquipFunction(eventname,params)
	if params.reason ~= "monsterDrop" then
		 RecData(showQuene,params)
	else
		 RecData(waitQuene,params)
	end
end

local function OnMenuEnter( tag)
	if tag =='BagMain' then
		if GlobalHooks.UI.FindUI('AutoEquipsMain') then
			GlobalHooks.UI.CloseUIByTag('AutoEquipsMain')
			showQuene = {}
			waitQuene = {}
		end
	end
end

local function 	OnMenuExit(tag)
	showQuene = {}
	waitQuene = {}
end

local function initial()
	EventManager.Subscribe("AddEquip",AddEquipFunction)
	EventManager.Subscribe("ShowUI",ShowUI)
	MenuMgr.Instance:AttachLuaObserver('BagMain','AutoEquipsMain',{OnMenuEnter = OnMenuEnter,OnMenuExit = OnMenuExit})
end

function OnExitScene()
	if GlobalHooks.UI.FindUI('AutoEquipsMain') then
		GlobalHooks.UI.CloseUIByTag('AutoEquipsMain')
	end
	showQuene = {}
	waitQuene = {}
end

local function fin(relogin)
    if relogin then
	    EventManager.Unsubscribe("AddEquip",AddEquipFunction)
	    EventManager.Unsubscribe("ShowUI",ShowUI)
	    MenuMgr.Instance:DetachLuaObserver('BagMain','AutoEquipsMain')

		showQuene = nil
		waitQuene = nil
	end
end


_M.GetEquipData = GetEquipData
_M.GetItemData = GetItemData
_M.fin = fin
_M.OnExitScene = OnExitScene
_M.initial = initial
return _M