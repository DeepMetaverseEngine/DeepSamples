local QuestState_Available = 1
local QuestState_Accepted = 2
local QuestState_Completed = 3
local QuestState_Failed = 4
local QuestState_Removed = 5

--任务监听,
--[任务ID] = '脚本Key'===>该任务接受时触发该脚本 (包括重新上线)
--[任务ID] = {[任务状态1] = '脚本Key', [任务状态] = '脚本Key2', restart = 是否重新上线执行(只支持已接受任务)} ==>该任务具体状态更新时触发该脚本
local quest_listenMap = {}

local function SetQuestResult(qData, questResult)
    if questResult then
        --完成任务
        Api.CompleteQuest(qData.id)
    else
        if qData.sub_type == 2100 or qData.sub_type == 2200 then
            --押镖任务直接放弃
            -- print('GiveUpQuest', qData.id)
            Api.GiveUpQuest(qData.id)
        else
            Api.Task.Sleep(2.5)
            -- print('ResetQuest', qData.id)
            Api.ResetQuest(qData.id)
        end
    end
end
local function TryStartOtherEvent(questID, state)
    local qmap = quest_listenMap[questID]
    if qmap then
        local keyScript
        local tMap = type(qmap)
        if tMap == 'table' then
            keyScript = qmap[state]
        elseif tMap == 'string' and state == QuestState_Accepted then
            keyScript = qmap
        else
            error('not support ' .. tMap)
        end
        if keyScript then
            Api.Task.StartEventByKey(keyScript, Api.GetArg({QuestID = questID, QuestState = state}))
        end
    end
end

local function StartQuestAcceptEvent(qData)
    local questID = qData.id
    local allids = {}
    for _, v in ipairs(qData.accept_event) do
        if not string.IsNullOrEmpty(v) then
            local nextArg = Api.GetArg({QuestID = qData.id, QuestState = QuestState_Accepted})
            local id = Api.Task.AddEventByKey(v, nextArg)
            table.insert(allids, id)
        end
    end
    local questResult
    if #allids > 1 then
        questResult = Api.Task.WaitParallel(allids)
    else
        questResult = Api.Task.Wait(allids[1])
    end
    QuestAcceptEvents[questID] = nil
    local cType = qData.condition.type[1]
    if cType == 'eFinishEvent' then
        SetQuestResult(qData, questResult)
    end
end

local function QuestAcceptEventClean(success, reason, qData)
    QuestAcceptEvents[qData.id] = nil
end

local function QuestStateChange(questID, state)
    local qData = Api.GetQuestData(questID)
    local cType = qData.condition.type[1]
    if state == QuestState_Accepted then
        if cType == 'eFinishEvent' then
            if not QuestAcceptEvents[questID] then
                QuestAcceptEvents[questID] = Api.Task.StartEvent({main = StartQuestAcceptEvent,clean = QuestAcceptEventClean}, qData)
            else
                warn('Already exists accepted events')
            end
        else
            Api.Task.StartEvent(StartQuestAcceptEvent, qData)
        end
    else
        if state == QuestState_Completed then
            for _, v in ipairs(qData.finish_event) do
                if not string.IsNullOrEmpty(v) then
                    local arg = Api.GetArg({QuestID = questID, QuestState = state})
                    local id = Api.Task.StartEventByKey(v, arg)
                end
            end
        end
        if cType == 'eFinishEvent' and QuestAcceptEvents[questID] then
            Api.Task.StopEvent(QuestAcceptEvents[questID], false, 'QuestState Change ' .. state)
            -- traceback()
        end
    end
    -- TryStartOtherEvent(questId, state)
end

local function SessionReconnect()
    Api.Task.WaitPlayerReady()
    local quests = Api.GetAcceptQuestList()
    for k, v in ipairs(quests or {}) do
        if v.state == QuestState_Accepted then
            local qData = Api.GetQuestData(v.id)
            local cType = qData.condition.type[1]
            if cType ~= 'eFinishEvent' then
                for __, vv in ipairs(qData.accept_event) do
                    if Api.IsClientScript(vv) then
                        local arg = Api.GetArg({QuestID = qData.id, QuestState = QuestState_Accepted})
                        Api.Task.StartEventByKey(vv, arg)
                    end
                end
            end
        end
    end
end

local function ReCheckAllQuest()
    local quests = Api.GetAcceptQuestList()
    -- 任务状态委托事件进行管理
    for k, v in ipairs(quests or {}) do
        if v.state == QuestState_Accepted then
            local qData = Api.GetQuestData(v.id)
            local cType = qData.condition.type[1]
            if cType == 'eFinishEvent' then
                SetQuestResult(qData, false)
            else
                -- TryStartOtherEvent(v.id, v.state)
                QuestStateChange(v.id, v.state)
            end
        end
    end
end

local function QuestListenerEvent()
    Api.Listen.QuestState(QuestStateChange)
    Api.Listen.SessionReconnect(
        function()
            Api.Task.AddEventTo(ID, SessionReconnect)
        end
    )
    ReCheckAllQuest()
    Api.Task.WaitAlways()
end

local function UseItemEvent()
    Api.Listen.UseItem(
        function(templateID, count)
            local data = Api.FindExcelData('item/item_consumption.xlsx/item_consumption', templateID)
            if not string.IsNullOrEmpty(data.item_event) then
                Api.Task.StartEventByKey(data.item_event, Api.GetNextArg(arg), templateID, count)
            end
        end
    )
    Api.Task.WaitAlways()
end

function main(...)
    QuestAcceptEvents = {}
    Api.Task.WaitPlayerReady()
    Api.Task.StartEvent('Player/client_protocol')
    Api.RestartScripts(Api.GetArg())
    Api.Task.AddEvent(QuestListenerEvent)
    -- local qData = Api.FindExcelData('quest/quest.xlsx/quest', 1046)
    Api.Task.AddEvent(UseItemEvent)
    Api.Listen.BeforeSaveData(
        function()
            Api.SaveRestartScripts()
        end
    )
    Api.Task.WaitAlways()
end

function clean(success,reason)
    if success then
        Api.SaveRestartScripts()
    end
end