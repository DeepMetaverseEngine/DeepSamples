-- File: EventManager.lua
-- Author: Quon Lu
-- Created at: 五月 14, 2015

local _M = {}
_M.__index = _M

print("EventManager init")
local EventCallBacks = {}
local KeepEventCallBacks = {}
local GlobalCallBacks = {}
local EventRecvs = {}
local CSEventManager = CSEventManager

local function globle_callback(eventName, evtData, ...)
  local evs1 = EventCallBacks[eventName]
  local evs2 = KeepEventCallBacks[eventName]
  if not evs1 and not evs2 then return end
  --print("globle_callback "..eventName)
  local params = evtData
  if type(evtData) ~= "table" then
    --print("globle_callback "..eventName)
    -- 只能使用Dictionary
    params = {}
    --local iter = evtData:GetEnumerator()
    --while iter:MoveNext() do
     -- local data = iter.Current
    --  params[data.Key] = data.Value
    --end
	local i = 1
	for p in Slua.iter(evtData) do
	    --print("aaaaaaaaaa", p)
	    if i == 2 then
		    for data in Slua.iter(p) do
			--print("[bbbbbbbbbbbbbb]", data.Key, data.Value)
			params[data.Key] = data.Value
		    end
		    break
	    end
	    i = i + 1
	end
  end
  if evs1 then
    for name,val in pairs(evs1) do
      val(eventName, params, ...) 
    end
  end
  if evs2 then
    for name,val in pairs(evs2) do
      val(eventName, params, ...) 
    end
  end
end

local function Subscribe(sEventName, fCallBack, dontAutoRemove)
  -- print("[LUAEvent]Subscribe", sEventName)
  local hasEvents = EventCallBacks[sEventName] or KeepEventCallBacks[sEventName]
  if not hasEvents then
    CSEventManager.Subscribe(sEventName, globle_callback)
  end
  local callbacks = dontAutoRemove and KeepEventCallBacks or EventCallBacks

  if not callbacks[sEventName] then
    callbacks[sEventName] = {}
  --xpcall (function() CSEventManager.Subscribe(sEventName, globle_callback) end, function() print("[lua error]", debug.traceback()) end)
  end
  local evs = callbacks[sEventName]
  table.insert(evs, fCallBack)
end

local function HasSubscribed(sEventName, fCallBack)
  if EventCallBacks[sEventName] then
    for _,v in ipairs(EventCallBacks[sEventName]) do
      if v == fCallBack then
        return true
      end
    end
  end
  if KeepEventCallBacks[sEventName] then
    for _,v in ipairs(KeepEventCallBacks[sEventName]) do
      if v == fCallBack then
        return true
      end
    end
  end
  return false
end

local function Unsubscribe(sEventName, fCallBack, dontAutoRemove)
   -- print("[LUAEvent]Unsubscribe", sEventName)
  local callbacks = dontAutoRemove and KeepEventCallBacks or EventCallBacks
  local t = callbacks[sEventName]
  if t then
    for k, fun in pairs(t) do
      if fun == fCallBack then
        table.remove(t, k)
      end
    end
  end
end

local function UnsubscribeAll(isAll)
  print("EventManager.UnsubscribeAll")
  for name,val in pairs(EventCallBacks) do
    if isAll or not KeepEventCallBacks[name] then
      CSEventManager.Unsubscribe(name)
    end
  end

  EventCallBacks = {}
  GlobalCallBacks = {}
  if isAll then
    KeepEventCallBacks = {}
  end
end

local function Fire(...)
  CSEventManager.Fire(...)
  for _,v in ipairs(GlobalCallBacks) do
    v(...)
  end
end

local function SubscribeGlobalCallBack(fn)
  table.insert(GlobalCallBacks,fn)
end

local function UnsubscribeGlobalCallBack(fn)
  table.insert(GlobalCallBacks,fn)
end

_M.UnsubscribeAll = UnsubscribeAll
_M.Subscribe = Subscribe
_M.Unsubscribe = Unsubscribe
_M.Fire = Fire
_M.HasSubscribed = HasSubscribed
_M.SubscribeGlobalCallBack = SubscribeGlobalCallBack
_M.UnsubscribeGlobalCallBack = UnsubscribeGlobalCallBack

return _M
