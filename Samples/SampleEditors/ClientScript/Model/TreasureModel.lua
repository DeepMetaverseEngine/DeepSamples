local _M = {}
_M.__index = _M

local ItemModel = require 'Model/ItemModel'
local List = require "Logic/List"

_M.treasureDatas = nil
_M.isUpdate = false

 
local function OnItemCountUpdate(eventname,params)
	local Count = params.Count
	if Count <= 0 then 
		return
	end

	if params.Reason == "SwapItem" then
		return
	end

	local templateID = params.TemplateID
	local detial = GlobalHooks.DB.Find('Item', templateID)
	if not detial then
		return
	end

	-- 处理稀有
	if not detial.is_treasure or detial.is_treasure ~= 1 then
		return
	end
	 
	local itemData = {}
	itemData.templateID = templateID
	itemData.Count = Count

	List.pushright(_M.treasureDatas,itemData)

end

function _M.Update()
	if not _M.isUpdate  or not _M.treasureDatas then 
		return
	end

	local data1 = List.popleft(_M.treasureDatas)
	if not data1 then
		return
	end

	local data2 =  List.popleft(_M.treasureDatas)
 
    local TreasureUI = GlobalHooks.UI.FindUI('GetTreasureUI')
	if  not TreasureUI then
		GlobalHooks.UI.OpenMsgBox('GetTreasureUI', 0,data1,data2)
	else
		List.pushright(_M.treasureDatas,data1)
	end
end

local function initial()

	_M.treasureDatas = List:new()

	_M.isUpdate = true
 	_M.timer = LuaTimer.Add(0,1000,function(id)
		_M.Update() 
		return true
    end)

	EventManager.Subscribe("Event.Item.CountUpdate",OnItemCountUpdate)
end

local function OnExitScene()
	 

end

local function fin(relogin)

	EventManager.Unsubscribe("Event.Item.CountUpdate",OnItemCountUpdate)

	_M.isUpdate = false
 	_M.treasureData = nil

    if _M.timer  ~= nil then
        LuaTimer.Delete(_M.timer )
        _M.timer = nil
    end
end


_M.fin = fin
_M.OnExitScene = OnExitScene
_M.initial = initial
return _M