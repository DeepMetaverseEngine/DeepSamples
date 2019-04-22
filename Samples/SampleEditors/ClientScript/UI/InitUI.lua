local _M = {}
_M.__index = _M
GlobalHooks.UI = {}
require('UI/UITAG')
require('UI/OpenUI')

local Helper = require "Logic/Helper"

local path = {}
local modules = {}

--登录游戏后首次进入场景时调用
local function initial()
  print("UI Init")
  path = {
    "UI/OpenUI",
    "UI/Hud/LuaHudMain",
    "UI/Hud/FunctionMenu",
    "UI/Hud/MountHud",
    "UI/Quest/UINpcTalk",
    "UI/Quest/DialogueTalk",
    "Story/UIStoryTip",
    "UI/Common/InteractiveMenuUI",
    "UI/Hud/NuqiTipsMain",
    "UI/Hud/ShowBuffListMain",
    "UI/Power/PowerEffectMain",
    "UI/FunctionUtil",
    "UI/Hud/QuestHud",
    "UI/Hud/TLChatHud",
    'UI/Bag/AutoUseItem',
    'UI/System/SystemHud',
    'UI/Dungeon/DungeonTips',
  }

  modules = {}
  Helper.each_i(function (args)
    local tab = require(args.val)
    table.insert(modules, tab)
    if type(tab) == 'table' and tab.initial then
      if not tab.GlobalLuaInit then
        tab.GlobalLuaInit = true
        tab.initial()
      end
    end
  end, path)
end

--进入场景时调用
local function OnEnterScene()
  Helper.each_i(function (args)
    if type(args.val) == 'table' and args.val.OnEnterScene then
      args.val.OnEnterScene()
    end
  end,modules)
end

--退出场景时调用，参数：是否短线重连触发的切场景
local function OnExitScene(reconnect)
  Helper.each_i(function (args)
    if type(args.val) == 'table' and args.val.OnExitScene then
      args.val.OnExitScene(reconnect)
    end
  end,modules)
end

--重登录回到标题画面时调用
local function fin()
  Helper.each_i(function (args)
    if type(args.val) == 'table' and args.val.fin then
      args.val.fin()
      args.val.GlobalLuaInit = false
    end
  end,modules)

  for _,v in ipairs(path) do
    -- print('loaded == nil',v, package.loaded[v])
    package.loaded[v] = nil
  end
  modules = {}
end

-- local function InitNetWork()
--   Helper.each_i(function (args)
--     if type(args.val) == 'table' and args.val.InitNetWork then
--       args.val.InitNetWork()
--     end
--   end,modules)
-- end

_M.initial = initial
_M.OnEnterScene = OnEnterScene
_M.OnExitScene = OnExitScene
_M.fin = fin
_M.InitNetWork = InitNetWork

return _M

