print("this is Quest/test")
local QuestData = DataMgr.Instance.QuestData
-- destroyLua('xxx')
-- print(fullpath('abc'))

-- destroyLua("Logic/Util")
destroyLua("Logic/AccordionExt")
destroyLua('UI/Quest/QuestUtil')
destroyLua("Logic/DisplayUtil")
destroyLua("Logic/ScrollBarExt")

destroyUI('UI/Common/ItemUseUI')
destroyUI('UI/Quest/QuestMain')
-- GlobalHooks.UI.OpenUI("QuestUI")

destroyLua("UI/Hud/QuestHud")
HudManager.Instance.TeamQuest.Quest:Reset()

local xxx = Util.GetText("fabsadsa")
print("unkonw lang ", xxx, xxx == nil)
-- local hud = require("UI/Hud/LuaHudMain.lua")


-- GlobalHooks.UI.OpenUI("ItemUseUI", 0, 5006)

-- local n = 1
-- while true do
--     local name, value = debug.getupvalue(hud.initial, n)
--     if not name then break end
--     print(name, value)
--     n = n + 1
-- end
-- print(debug.getlocal(2, 1))
-- print(debug.getlocal(2, 2))
-- local ll = {}
-- for k,v in pairs(t) do
--     table.insert(ll, k)
-- end
-- print(table.concat(ll, '\n'))

local function changeTracing(QuestId)
    local Quest = QuestData:GetQuest(QuestId)
    Quest.tracing = not Quest.tracing
    QuestData:Notify('Event.Quest.ChangeTracing', Quest)
end

local function requestUpdateQuest(objType, objId, objNum)
    local msg = {c2s_objId = objId, c2s_objType = objType, c2s_objNum = objNum }
    Protocol.RequestHandler.ClientQuestTestRequest(msg, function(ret)
        
    end)
end

-- requestUpdateQuest(1, 20009, 1)

-- changeTracing(1002)

-- local QuestModel = require("Model/QuestModel")
-- QuestModel.requestAccept(self.Quest.id, self.Quest.mainType, function()

-- end)

local QuestDataSnap = CommonRPG.Data.QuestDataSnap
-- QuestProgress
-- QuestProgressData
local ListQuestDataSnap = getClass('List<<CommonRPG.Data.QuestDataSnap, CommonRPG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>')
local ListQuestProgress = getClass('List<<CommonRPG.Data.QuestProgress, CommonRPG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>')
local ListQuestProgress = getClass('List<<CommonRPG.Data.QuestProgress, CommonRPG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>')


local function decodeCshap(t)
    if type(t) ~= 'table' then return t end

    local cshap = t.__class()
    if t.__isList then
        for i,v in ipairs(t) do
            cshap:Add(decodeCshap(v))
        end
    else
        for k,v in pairs(t) do
            if k ~= '__class' then
                cshap[k] = decodeCshap(v)
            end
        end
    end
    return cshap
end

-- public const int GiveUp = -2;
-- public const int NotAccept = -1;
-- public const int NotCompleted = 0;
-- public const int CompletedNotSubmited = 1;
-- public const int Submited = 2;

-- 发送可接任务
-- local list = decodeCshap({
--     __class = ListQuestDataSnap, __isList = true,
--     {
--         __class = QuestDataSnap,
--         -- id = 1001, type = 1, state = QuestState.NotCompleted,
--         -- id = 1001, type = 1, state = QuestState.CompletedNotSubmited,
--         id = 1001, type = 1, state = QuestState.Submited,
--         progressList = {
--             __class = ListQuestProgress, __isList = true,
--             {
--                 __class = QuestProgress,
--                 objTargetNum = 3,
--                 progressData = {
--                     __class = QuestProgressData,
--                     objId = 1, objType = 1, objNum = 3,
--                 }
--             }
--         },
--     }
-- })
-- QuestData:OnQuestNotify(list)

-- local function printlocal()
--     local n = 1
--     while true do
--         local name, value = debug.getlocal(3, n)
--         if not name then break end
--         print('local', name, value)
--         n = n + 1
--     end
-- end

-- local function hook(...)
--     print('hook', ...)
--     -- debug.getupvalue()
--     printlocal()
--     print(env, env == _G)
--     -- for k,v in pairs(env) do
--     --     print(k,v)
--     -- end
-- end
-- debug.sethook(hook, 'r', 0)

-- hud.fin()

-- debug.sethook()


GlobalHooks.UI.OpenUI("ItemUseUI", 0, 5006)
GlobalHooks.UI.OpenUI("ItemUseUI", 0, 2101)
GlobalHooks.UI.OpenUI("ItemUseUI", 0, 2102)
GlobalHooks.UI.OpenUI("ItemUseUI", 0, 2103)
GlobalHooks.UI.OpenUI("ItemUseUI", 0, 2104)

-- destroyUI("UI/Common/InteractiveMenuUI")

-- EventManager.Fire("Event.InteractiveMenu.Show", {
--     plyaerId = 0,
--     menuKey = 'stranger',
--     })

