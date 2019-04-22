
local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'

_M.FuncsInfo = {
  --以key value形式保存
  --每一项为数据表里的一行数据
}
_M.OpenList = {
  --以数组形式保存，保证播放顺序
  --<string, int>
}

local function NotifyPlayedFunction(funcName)
  -- print("---------ClientFunctionPlayedNotify----------", funcName)

  local msg = { c2s_key = funcName }
  Protocol.NotifyHandler.ClientFunctionPlayedNotify(msg)
end

function _M.SetPlayedFunctionByName(funcName)
  -- print("-----------SetPlayedFunctionByName------------- ", funcName)
  local func = _M.FuncsInfo[funcName]
  if func ~= nil and func == 1 then
    NotifyPlayedFunction(funcName)
    _M.FuncsInfo[funcName] = 2

    --消红点
    -- EventManager.Fire('Event.FunctionOpen.WaitToPlay', {name = funcName, waitToPlay = false})
    local db = GlobalHooks.DB.FindFirst('module_open', { UI_flag = funcName })
    if db.open_type ~= 1 then
      local rootNode
      if db.menu_type == 1 then
        local ui = HudManager.Instance:GetHudUI("MainHud")
        rootNode = ui:FindChildByEditName('cvs_menuhud', true)
      elseif db.menu_type == 2 then
        rootNode = HudManager.Instance:GetHudUI("FunctionHud")
      elseif db.menu_type == 3 then
        local ui = HudManager.Instance:GetHudUI("MainHud")
        rootNode = ui:FindChildByEditName('cvs_chat', true)
      else
        rootNode = HudManager.Instance:GetHudUI("MainHud")
      end
      if rootNode ~= nil then
        local cvs = rootNode:FindChildByEditName(db.comp, true)
        local lb_red = cvs:FindChildByEditName('lb_tip', true)
        if lb_red then
          GlobalHooks.UI.ShowRedPoint(lb_red, 0, 'funcopen')
        end
      end
    end
  end
end

function GlobalHooks.IsFuncWaitToPlay(funcName)
  local ret = false
  local func = _M.FuncsInfo[funcName]
  if func ~= nil and func == 1 then --开放了，并且没打开过
    ret = true
  end
  -- print("------------IsFuncWaitToPlay ", funcName, ret)
  return ret
end

function GlobalHooks.IsFuncOpen(funcName, isShowTips)
  local func = _M.FuncsInfo[funcName]
  local isOpen = true
  if func ~= nil then
    isOpen = func ~= 0
    if not isOpen and isShowTips then
      local db = GlobalHooks.DB.FindFirst('module_open', { UI_flag = funcName })
      local tips = Util.GetText(db.open_desc)
      GameAlertManager.Instance:ShowNotify(tips)
    end
  end
  -- print("------------IsFuncOpen---------- ", funcName, isOpen, func)
  return isOpen
end

local function CheckIdleState()
  -- print("CheckIdleState")
  -- 场景未加载完毕
  if GameSceneMgr.Instance.BattleRun.Client == nil then
    return false
  end
  if not GameSceneMgr.Instance.BattleRun.Client.IsRunning then
    return false
  end

  -- 在非指定的场景里
  if not _M.canMapShowFuncOpen then
    return false
  end

  -- 有菜单未关闭
  if MenuMgr.Instance:GetTopMenu() ~= nil or MenuMgr.Instance:GetTopMsgBox() ~= nil then
    return false
  end

  -- 正在自动寻路
  if DataMgr.Instance.UserData:GetActor().IsAutoRun then
    return false
  end

  return true
end

local function InitOneFuncIcon(name, isOpen, funcData)
  local fType = funcData.open_type
  local mType = funcData.menu_type
  local fComp = funcData.comp
  print("InitOneFuncIcon ", name, fType, mType, isOpen)
  if fType ~= 4 then
    if mType == 1 then --主界面上方hud
      local ui = HudManager.Instance:GetHudUI("MainHud")
      local rootNode = ui:FindChildByEditName('cvs_menuhud', true)
      MenuBase.SetVisibleUENode(rootNode, fComp, isOpen)
    elseif mType == 3 then --主界面下方hud
      -- local ui = HudManager.Instance:GetHudUI("MainHud")
      -- local rootNode = ui:FindChildByEditName('cvs_chat', true)
      -- MenuBase.SetVisibleUENode(rootNode, fComp, isOpen)
    elseif mType == 2 then --二级hud
      local ui = HudManager.Instance:GetHudUI("FunctionHud")
      -- MenuBase.SetGrayUENode(ui, fComp, not isOpen) --置灰
      -- MenuBase.SetEnableUENode(ui, fComp, isOpen, isOpen) --不能点
      -- local node = ui:FindChildByEditName(fComp, true).Parent
      MenuBase.SetVisibleUENode(ui, fComp, isOpen)
    else --其他
      local ui = HudManager.Instance:GetHudUI("MainHud")
      MenuBase.SetVisibleUENode(ui, fComp, isOpen)
    end
  end
end

local function InitOpenList(funcs)
  -- print("InitOpenList0 ", #_M.OpenList)
  local function dealQueue()
    -- print("InitOpenList1")
    _M._timer = LuaTimer.Add(0, 33, function(id)
      -- print("InitOpenList3")
      if #_M.OpenList == 0 then
        -- print("InitOpenList4")
        LuaTimer.Delete(_M._timer)
        _M._timer = nil
        return
      end
      if CheckIdleState() then
        --功能列表标记为开启
        local funcDb = _M.OpenList[#_M.OpenList]
        local name = funcDb.UI_flag

        _M.FuncsInfo[name] = 1
        EventManager.Fire('Event.FunctionOpen.FuncOpen', { name = name, val = 1, data = funcDb })
        InitOneFuncIcon(name, false, funcDb)

        local luaObj = GlobalHooks.UI.CreateUI('FuncOpen', -1)
        luaObj.SetData(luaObj, funcDb, function()
          -- --刷新功能图标
          InitOneFuncIcon(name, true, funcDb)
          --移除待开放列表
          _M.OpenList[#_M.OpenList] = nil
        end)
        -- print("InitOpenList5")
        MenuMgr.Instance:AddMsgBox(luaObj.ui.menu)
      end
    end)
  end

  local queueEmpty = #_M.OpenList == 0
  local queueUpdate = false
  for name, val in pairs(funcs) do
    local db = GlobalHooks.DB.FindFirst('module_open', { UI_flag = name })
    if db ~= nil then
      -- print("InitOpenList ", name, db.open_type, val)
      if val == 1 and _M.FuncsInfo[name] == 0 then
        -- print("InitOpenList2 "..name)
        if db.open_type == 1 then --直接开启，无任何提示
          -- print("InitOpenList2.1 ", name)
          _M.FuncsInfo[name] = 1
          EventManager.Fire('Event.FunctionOpen.FuncOpen', { name = name, val = val, data = db })
          InitOneFuncIcon(name, true, db)
        elseif db.open_type == 2 then --直接开启，红点提示
          -- print('InitOpenList2.2', name)
          _M.FuncsInfo[name] = 1
          EventManager.Fire('Event.FunctionOpen.FuncOpen', { name = name, val = val, data = db })
          InitOneFuncIcon(name, true, db)
        else --弹框并加红点
          -- print("InitOpenList2.3 ", name)
          _M.OpenList[#_M.OpenList + 1] = db
          queueUpdate = true
        end
      end
    end
  end

  if queueEmpty and queueUpdate then
    dealQueue()
  end
end

local function InitFuncsData(funcs)
  -- print("InitFuncsIcon")
  for name, val in pairs(funcs) do
    local func = GlobalHooks.DB.FindFirst('module_open', { UI_flag = name })
    if func ~= nil and func.menu_type == 3 then
      InitOneFuncIcon(name, val > 0, func)
      -- if func.open_type ~= 1 then --红点提示
      --   local waitToPlay = val == 1
      --   -- print("InitFuncsIcon "..name.." waitToPlay "..tostring(waitToPlay))
      --   EventManager.Fire('Event.FunctionOpen.WaitToPlay', {name = name, waitToPlay = waitToPlay})
      -- end
    end
  end
end

local function OnFunctionOpenListPush(notify)
  print("---------OnFunctionOpenListPush------------")
  local funcs = notify.s2c_funList
  print_r(funcs)
  InitOpenList(funcs)
end

local function OnFuncOpenInit( ... )
  InitFuncsData(_M.FuncsInfo)
end

local function CheckMapType()
  local checkMaps = {
    PublicConst.SceneType._SingleDungeon, --2：单人副本
    PublicConst.SceneType._TeamDungeon,   --3：组队副本
    PublicConst.SceneType._ZhenYaoTa,   --4：镇妖塔
    PublicConst.SceneType._XianLinDao,    --5：仙灵岛
    PublicConst.SceneType._MiJing,      --8：秘境副本
    PublicConst.SceneType._ZhanChang10v10,  --10：10v10战场
    PublicConst.SceneType._zhanChang4v4,  --11：4v4竞技场
  }
  local sceneIdle = true
  local mapdb = GlobalHooks.DB.FindFirst('MapData',{ id = DataMgr.Instance.UserData.MapTemplateId })
  if mapdb then
    for i = 1, #checkMaps do
      if mapdb.type == checkMaps[i] then
        sceneIdle = false
        break
      end
    end
  end
  _M.canMapShowFuncOpen = sceneIdle
end

--退出场景时调用，参数：是否短线重连触发的切场景
function _M.OnExitScene(reconnect)

end

--进入场景时调用
function _M.OnEnterScene()
  CheckMapType()
end

function _M.fin()
  EventManager.Unsubscribe("Event.Scene.FirstInitFinish", OnFuncOpenInit)
  if _M._timer then
    LuaTimer.Delete(_M._timer)
    _M._timer = nil
  end
end

function _M.initial()
  -- print("FunctionOpen initial")
  _M.FuncsInfo = CSharpMap2Table(DataMgr.Instance.UserData.FuncOpen)
  EventManager.Subscribe("Event.Scene.FirstInitFinish", OnFuncOpenInit)
end

function _M.InitNetWork(initNotify)
  if initNotify then
      Protocol.PushHandler.ClientFunctionOpenNotify(OnFunctionOpenListPush)
  end
end

return _M
