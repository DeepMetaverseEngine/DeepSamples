
local _M = {}
_M.__index = _M

local Helper = require "Logic/Helper"

local path = {}
local modules = {}

local function InitModules()
  if #modules == 0 then
    path = {
      'Model/DataCenter/DataHelper',
      'Model/RedCenter',
      'Model/FuncOpen',
      'Model/Attribute',
      'Model/ItemModel',
      'Model/MailModel',
      'Model/SocialModel',
      'Model/ReliveModel',
      'Model/TeamModel',
      'Model/QuestNpcDataModel',
      'Model/QuestModel',
      'Model/FallenPartnerModel',
      'Model/PracticeModel',
      'Model/MedicineModel',
      'Model/GuildModel',
      'Model/SceneModel',
      'Model/MountModel',
      'Model/ChatModel',
      'Model/ActivityModel',
      'Model/RankModel',
      'Model/AutoEquipsModel',
      'Model/PowerEffectModel',
      'Model/DungeonModel',
      'Model/PagodaModel',
      'Model/SyncEnvironmentVarEventModel',
      'Model/TargetModel',
      'Model/GuildWantModel',
      'Model/OnlinePush',
      'Model/GetBackModel',
      'Model/AuctionModel',
      'Model/TitleModel',
      'Model/RechargeModel',
      'Model/PlayRuleModel',
      'Model/BusinessModel',
      'Model/WingModel',
      'Model/SkillModel',
      'Model/ArtifactModel',
      'Model/SystemModel',
      'Model/TreasureModel',
      'Model/LevelSealModel',
      'Model/AchievementModel',
      'Model/SpringFestivalModel',
      'Model/MeridiansModel',
      'Model/PKModel',
    }

    modules = {}
    Helper.each_i(function (args)
      local tab = require(args.val)
      table.insert(modules, tab)
    end, path)

    require 'Constants'
    local soundKeys = GlobalHooks.DB.Find('SoundKey',{})
    -- print_r("initSoundkeys",soundKeys)
    for k,v in ipairs(soundKeys) do
      SoundManager.Instance:AddSoundKey(v.Key, v.Value)
    end

    --初始化协议多语言
    local langpath = 'Data/lang/'..LanguageManager.Instance.LangCode..'/response-code'
    local ok = pcall(require, langpath)
    if not ok then
      require 'Protocol/generated/response-code'
    end
  end
end

--登录游戏后首次进入场景时调用
local function initial()
  print("Model Init")
  InitModules()
  Helper.each_i(function (args)
  	if type(args.val) == 'table' and args.val.initial then
        if not args.val.GlobalLuaInit then
          args.val.GlobalLuaInit = true
          args.val.initial()
        end
  	end
  end,modules)
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
local function fin(relogin, reconnect)
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

local function InitNetWork(initNotify)
  InitModules()
  Helper.each_i(function (args)
    if type(args.val) == 'table' and args.val.InitNetWork then
      args.val.InitNetWork(initNotify)
    end
  end,modules)
end

local function BagInitOk()
  print(' bag ready --------------------')
  Helper.each_i(function (args)
    if type(args.val) == 'table' and args.val.OnBagReady then
      args.val.OnBagReady()
    end
  end,modules)
end

_M.initial = initial
_M.OnEnterScene = OnEnterScene
_M.OnExitScene = OnExitScene
_M.fin = fin
_M.InitNetWork = InitNetWork
_M.InitModules = InitModules
_M.BagInitOk = BagInitOk
return _M