--! @cond DO_NOT_DOCUMENT
local PlayerApi = {
    Task = {},
    Listen = {}
}

local QuestState_Available = 1
local QuestState_Accepted = 2
local QuestState_Completed = 3
local QuestState_Failed = 4
local QuestState_Removed = 5

local function ListenQuestState(state, questID, fn)
    local eid
    if type(questID) == 'function' then
        fn = questID
        questID = nil
    end
    local function CheckState(qid, s)
        if (questID and qid ~= questID) or (state and s ~= state) then
            return
        end
        if fn then
            fn(qid, s)
        else
            EventApi.Task.StopEvent(eid)
        end
    end
    eid = EventApi.Listen.QuestStateChange(CheckState)
    return eid
end
--! @endcond

--! @brief 监听某任务已接受
function PlayerApi.Listen.QuestAccept(questID, fn)
    return ListenQuestState(QuestState_Accepted, questID, fn)
end
--! @brief 监听某任务已完成
function PlayerApi.Listen.QuestComplete(questID, fn)
    return ListenQuestState(QuestState_Completed, questID, fn)
end
--! @brief 监听某任务已移除
function PlayerApi.Listen.QuestRemoved(questID, fn)
    return ListenQuestState(QuestState_Removed, questID, fn)
end
--! @brief 监听某任务已失败
function PlayerApi.Listen.QuestFailed(questID, fn)
    return ListenQuestState(QuestState_Failed, questID, fn)
end
--! @brief 监听某任务可接取
function PlayerApi.Listen.QuestAvailable(questID, fn)
    return ListenQuestState(QuestState_Available, questID, fn)
end

--! @brief 监听某任务任务状态已改变可接取
function PlayerApi.Listen.QuestState(questID, state, fn)
    if type(state) == 'function' then
        fn = state
        state = nil
    end
    return ListenQuestState(state, questID, fn)
end

local restartScript = {}

function PlayerApi.SaveRestartScripts()
    local f = ''
    for key, v in pairs(restartScript) do
        if not EventApi.string_IsNullOrEmpty(f) then
            f = f .. '|'
        end
        local num = v.len
        local p = v
        local stringArg = ''
        for i = 1, num do
            local t = type(p[i])
            if not EventApi.string_IsNullOrEmpty(stringArg) then
                stringArg = stringArg .. '#'
            end
            stringArg = stringArg .. t .. ',' .. tostring(p[i])
        end
        f = f .. key .. ':' .. stringArg
    end
    EventApi.SetStringFlag('Player.EventScript', f)
end

--! @brief 记录下次上线需要执行的脚本 scriptKey1:nunber,3#bool,true|scriptKey2:nunber,3#bool,true
function PlayerApi.SetRestartScript(key, ...)
    restartScript[key] = EventApi.DynamicToArgTable(...)
    EventApi.SaveRestartScripts()
end

function PlayerApi.RemoveRestartScript(key)
    restartScript[key] = nil
    EventApi.SaveRestartScripts()
end

function PlayerApi.RestartScripts(scriptArg)
    local f = EventApi.GetStringFlag('Player.EventScript')
    if EventApi.string_IsNullOrEmpty(f) then
        return
    end
    local all = EventApi.string_split(f, '|')
    -- EventApi.pprint('restartscript',all)
    for _, v in ipairs(all) do
        local keypairs = EventApi.string_split(v, ':')
        local key = keypairs[1]
        local argStrs = EventApi.string_split(keypairs[2], '#')
        local p = {}
        for i, vv in ipairs(argStrs or {}) do
            local argpairs = EventApi.string_split(vv, ',')
            local t = argpairs[1]
            local arg
            if t == 'number' then
                arg = tonumber(argpairs[2])
            elseif t == 'boolean' then
                arg = argpairs[2] == 'true' and true or false
            elseif t == 'string' then
                arg = argpairs[2]
            end
            p[i] = arg
        end
        EventApi.Task.StartEventByKey(key, EventApi.GetNextArg(scriptArg, {IsRestart = true}), unpack(p, 1, argStrs and #argStrs or 0))
    end
    EventApi.SetStringFlag('Player.EventScript', '')
end

function PlayerApi.Task.TransportPlayerByExcel(ele)
    local p = {MapTemplateID = ele.mapid}
    p.Flag, p.X, p.Y = EventApi.ParseExcelPosition(ele)
    return EventApi.Task.TransportPlayer(p)
end

function PlayerApi.Task.WaitEnterMap(mapid)
    if EventApi.GetMapTemplateID() ~= mapid then
        EventApi.Task.Wait(EventApi.Listen.EnterMap(mapid))
    end
end

local PvpEnterFlagKey = {
    pvp4 = 'EnterPvp4Count',
    pvp10 = 'EnterPvp10Count',
    pvp_duoqi = 'Enterpvp_duoqiCount',
    pvp_qinglong = 'Enterpvp_qinglongCount',
    pvp_longshen = 'Enterpvp_longshenCount'
}
local PvpRewardFlagKey = {
    pvp4 = 'pvp4_rewardOk',
    pvp10 = 'Pvp10_rewardOk',
    pvp_duoqi = 'pvp_duoqi_rewardOk',
    pvp_qinglong = 'pvp_qinglong_rewardOk',
    pvp_longshen = 'pvp_longshen_rewardOk'
}
function PlayerApi.CheckPvpRewardRedTips()
    local allreward = EventApi.FindExcelData('pvp/pvp.xlsx/pvp_reward', {})
    local ret = {
        TodayExploit = EventApi.GetIntFlag('TodayExploit', true),
        reward = {},
        Count = {}
    }
    local ret = false
    for _, v in ipairs(allreward) do
        local today_count = EventApi.GetIntFlag(PvpEnterFlagKey[v.function_id], true)
        local get_key = PvpRewardFlagKey[v.function_id] .. v.partake_num
        local get_state = EventApi.GetIntFlag(get_key, true)
        if get_state == 0 then
            if today_count >= v.partake_num then
                ret = true
                break
            end
        end
    end
    local ClientApi = EventApi.GetClientApi(EventApi.UUID)
    ClientApi.SetRedTips.NoAutoWait = true
    ClientApi.SetRedTips('battleground', ret and 1 or 0)
end

return PlayerApi
