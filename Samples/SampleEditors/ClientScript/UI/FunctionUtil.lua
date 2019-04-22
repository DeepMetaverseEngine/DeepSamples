FunctionUtil = {}
local QuestModel = require "Model/QuestModel"
local Util = require 'Logic/Util'
local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local QuestNpcDataModel = require 'Model/QuestNpcDataModel'
--local DungeonModel = require'Model/DungeonModel'
local ServerTime = require 'Logic/ServerTime'
local GuildModel = require 'Model/GuildModel'
local TeamModel = require 'Model/TeamModel'
local ChatModel = require 'Model/ChatModel'
local TimeUtil = require 'Logic/TimeUtil'

function FunctionUtil.HasGuild(showTips)
    if GlobalHooks.IsFuncOpen("GuildMain", showTips) then
        if not string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) then
            return true
        else
            if showTips then
                GameAlertManager.Instance:ShowNotify(Util.GetText('guild_noguild'))
            end
            return false
        end
    else
        return false
    end

end
--通过活动传送
function FunctionUtil.ActivityTransport(type)
    local request = {c2s_MapId = 0,c2s_Ext = {transporttype = type}}
    Protocol.RequestHandler.ClientChangeSceneRequest(request, function(rsp)
    end)

end

--通过mapId，请求进入副本
function FunctionUtil.EnterDungeon(function_id, params)
    --print("EnterDungeon",function_id)
    params = params or {}
    local request = { c2s_FuncId = function_id, c2s_MapId = 0, c2s_ext = params.arg }
    Protocol.RequestHandler.ClientEnterDungeonRequest(request, function(rsp)
        if params.cb then
            params.cb(true)
        end
    end, function(rsp)
        if params.cb then
            params.cb(false)
        end
    end)
end

function FunctionUtil.CheckInOtherGuild()

    local mapid = GameUtil.GetIntGameConfig("guild_guildmap")
    if DataMgr.Instance.UserData.MapTemplateId ~= mapid then
        return false
    end
    return DataMgr.Instance.UserData.ZoneGuildId ~= DataMgr.Instance.UserData.GuildId

end
--进入工会地图
function FunctionUtil.EnterGuildEvent(isself)
    if FunctionUtil.HasGuild(true) then
        MenuMgr.Instance:CloseAllMenu()
        if isself then

            GuildModel.ClientEnterGuildSceneRequest(function(rsp)

            end)
        else
            FunctionUtil.EnterDungeon('guildattack')
        end

        return true
    end
    return false
end

function FunctionUtil.EnterGuild(isself)
    if FunctionUtil.HasGuild(true) then
        local action = EnterGuildAction(isself)
        if TLBattleScene.Instance.Actor then
            TLBattleScene.Instance.Actor:AutoRunByAction(action)
            return true
        else
            return false
        end
    else
        return false
    end
end

--打开各种商店
function FunctionUtil.OpenGuildUI(uiarg)

    local uitag = uiarg[1]
    table.remove(uiarg, 1)
    if uitag == 'GuildInfo' then
        if GlobalHooks.IsFuncOpen("GuildMain", true) then
            if FunctionUtil.HasGuild(false) then
                GlobalHooks.UI.OpenUI("GuildMain", 0, 'GuildInfo')
            else
                GlobalHooks.UI.OpenUI("GuildList", 0)
            end
            return true
        else
            return false
        end
    else
        if FunctionUtil.HasGuild(true) then
            if FunctionUtil.CheckInOtherGuild() then
                GameAlertManager.Instance:ShowNotify(Constants.Text.notinselfguild)
                return false
            end
            if uitag == 'GuildShop' then
                local shopId = GlobalHooks.DB.GetGlobalConfig('guild_storeid')
                GlobalHooks.UI.OpenUI('Shop', 0, shopId)
            else
                GlobalHooks.UI.OpenUI(uitag, 0, unpack(uiarg))
            end
            return true
        else
            return false
        end
    end


end

function FunctionUtil.GoToShiMen(function_val, function_tag, params)
    local protag = function_val[DataMgr.Instance.UserData.Pro]
    --print("shimen",protag,DataMgr.Instance.UserData.Pro)
    if not string.IsNullOrEmpty(protag) then
        function_tag = protag
        local npctempid, curMapID, born_point, isRoad, roadtype = QuestNpcDataModel.FindRoadByFunctionTag(function_tag)
        if DataMgr.Instance.UserData.ZoneTemplateId == curMapID then
            GameAlertManager.Instance:ShowNotify(Constants.FunctionUtil.hasInScene)
            return false
        end
    end
    FunctionUtil.seekAndNpcTalkByFunctionTag(function_tag, params and params.Quest or nil)
end
function FunctionUtil.FindGuildStore(npctempid)
    if FunctionUtil.HasGuild(true) then
        local action = EnterGuildAndNpcTalk(npctempid)
        action.OpenFunction = true
        if TLBattleScene.Instance.Actor then
            TLBattleScene.Instance.Actor:AutoRunByAction(action)
            return true
        else
            return false
        end
    else
        return false
    end
end
--开启循环类型任务
function FunctionUtil.BeginLoopQuest(params)
    --print_r("BeginLoopQuest",params)
    if params == 2000 then
        if not FunctionUtil.HasGuild(true) then
            return
        end
    end

    QuestModel.requestBeginLoopQuest(params, function()
    end)
    return true
end

--组队
function FunctionUtil.TeamYelling(function_id)
    if function_id == 'beaststeam' then
        if DataMgr.Instance.TeamData.IsInMatch then
            UIUtil.ShowConfirmAlert(Util.GetText(Constants.GuildWant.CallTip), Util.GetText(Constants.GuildWant.CallTittle), function()
                TeamModel.ReuestLeaveAutoMatch()
                TeamModel.RequestCreateTeam(function()
                    local showData = Util.GetText('guild_monster_yelling')
                    local ChatUtil = require 'UI/Chat/ChatUtil'
                    ChatUtil.TeamShout(showData, ChatModel.ChannelState.CHANNEL_GUILD)
                end)
            end, nil)
        elseif DataMgr.Instance.TeamData:IsLeader() then
            local showData = Util.GetText('guild_monster_yelling')
            local ChatUtil = require 'UI/Chat/ChatUtil'
            ChatUtil.TeamShout(showData, ChatModel.ChannelState.CHANNEL_GUILD)
        elseif not DataMgr.Instance.TeamData.HasTeam then
            -- GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL,Util.GetText('dungeon_creat_team'),'','',nil, 
            --function ()
            TeamModel.RequestCreateTeam(function()
                local showData = Util.GetText('guild_monster_yelling')
                local ChatUtil = require 'UI/Chat/ChatUtil'
                ChatUtil.TeamShout(showData, ChatModel.ChannelState.CHANNEL_GUILD)
            end)
            --end
            --,nil)
            --end
        elseif not DataMgr.Instance.TeamData:IsLeader() then
            GameAlertManager.Instance:ShowNotify(Util.GetText('dungeon_enter_button'))
        end
    end
end

--方法名字
-- function FunctionUtil.GetFunctionName(tag)
-- 		print("GetFunctionName",tag)
-- 		local functiondata = unpack(GlobalHooks.DB.Find('NpcFunction',{function_tag = tag}))
-- 		if functiondata ~= nil then
-- 			--print("GetFunctionName",functiondata.function_name)
-- 			return Util.GetText(functiondata.function_name)
-- 		else
-- 			print("no found function_name with tag =",tag)
-- 		end
-- end

function FunctionUtil.GetFunctionName(function_id)
    local data = unpack(GlobalHooks.DB.Find('Function_Data', { function_id = function_id }))
    if data == nil then
        UnityEngine.Debug.LogError("Function_Data id =  " .. function_id .. " is not exist")
        return
    end
    return Util.GetText(data.function_name)
end

local function CheckSingleRequire(requiredata)
    local val = 0
    if string.sub(requiredata.key, 1, 1) == "p" then
        local functionname = string.sub(requiredata.key, 2)
        --print("functionname",functionname)
        val = DataMgr.Instance.UserData:TryGetIntAttribute(DataMgr.Instance.UserData:Key2Status(functionname), 0)

    else
        local arr = string.split(requiredata.key, '/')
        if #arr > 1 then
            if arr[1] == 'openFun' then
                if GlobalHooks.IsFuncOpen(arr[2], false) then
                    val = 0
                else
                    val = 1
                end
            end
        end
    end
    if requiredata.maxval == -1 then

        if val >= requiredata.minval then
            return true
        end
    else
        if val >= requiredata.minval and val <= requiredata.maxval then
            return true
        end
    end
    return false, Util.GetText(requiredata.text)
end

local function CheckRequire(requiredata)
    local info = {}
    for i, v in ipairs(requiredata.key) do
        local data = {}
        data.key = v
        data.minval = requiredata.minval[i]
        data.maxval = requiredata.maxval[i]
        data.text = requiredata.text[i]
        table.insert(info, data)
    end

    for i, v in ipairs(info) do
        local issucc, result = CheckSingleRequire(v)
        if not issucc then
            return issucc, result
        end
    end

    return true
end

local function doFunction(data, params)
    --print_r('doFunction',data,params)

    if not FunctionUtil.CheckNowIsOpen(data.function_id, true) then
        return false
    end
    local issucc, result = CheckRequire(data.require)
    if issucc then
        local functiontype = string.trim(data.function_type)
        local uiarg = {}
        for i, v in ipairs(data.type.val) do
            if not string.IsNullOrEmpty(v) then
                local val = tonumber(v)
                if val == nil then
                    val = v
                end
                table.insert(uiarg, val)
            end
        end
        if params.arg ~= nil then
            for i, v in ipairs(params.arg) do
                if not string.IsNullOrEmpty(v) and type(v) == 'number' then
                    local val = tonumber(v)
                    table.insert(uiarg, val)
                end
            end
        end
        --print_r("data.needcloseallmenu",data)
        if data.needcloseallmenu == 1 then
            MenuMgr.Instance:CloseAllMenu()
        end
        if functiontype == 'openui' then
            local uiname = uiarg[1]
            table.remove(uiarg, 1)
            --print_r('openui',uiarg)
            GlobalHooks.UI.OpenUI(uiname, 0, unpack(uiarg))
        elseif functiontype == 'openguildui' then
            return FunctionUtil.OpenGuildUI(uiarg)
        elseif functiontype == 'loopquest' then
            FunctionUtil.BeginLoopQuest(tonumber(uiarg[1]))
        elseif functiontype == 'enterdungeon' then
            params.arg = params.arg or {}
            for i, v in ipairs(data.type.val) do
                if not string.IsNullOrEmpty(v) then
                    params.arg['type' .. i] = v
                end
            end
            FunctionUtil.EnterDungeon(data.function_id, params)
            --DungeonModel.RequestEnterDungeon()
        elseif functiontype == 'enterguild' then
            FunctionUtil.EnterGuild(true)
        elseif functiontype == 'quicktransfer' then
            --if GameUtil.CanQuickTransfer(true) then
            MenuMgr.Instance:CloseAllMenu()
            local mapid = tonumber(uiarg[1])
            local _params = { mapId = mapid, mapUUid = 0 }
            EventManager.Fire("Event.Map.ChangeScene", _params)
            --end
        elseif functiontype == 'teamyelling' then
            FunctionUtil.TeamYelling(data.function_id)
        elseif functiontype == 'event' then
            for i, v in ipairs(uiarg) do
                EventApi.Task.StartEventByKey(v)
            end
        elseif functiontype == 'shimen' then
            FunctionUtil.GoToShiMen(data.function_val, data.function_tag, params)
        elseif functiontype == 'buytickets' then
            FunctionUtil.GoToBuyticketsRequest(data.function_id)
        elseif functiontype == 'activitytransport' then
            --print('fucntiontype',fucntiontype)
            MenuMgr.Instance:CloseAllMenu()
            --print_r('activitytransport',uiarg)
            local transporttype = tonumber(uiarg[1])
            FunctionUtil.ActivityTransport(transporttype)
        elseif functiontype == 'customcb' then
            MenuMgr.Instance:CloseAllMenu()
            FunctionUtil.GotoCustomCb(data.function_id,uiarg)
        end
        return true
    else
        GameAlertManager.Instance:ShowNotify(result)
        return false
    end

end
function FunctionUtil.CheckNeedOpen(function_id)
    if string.IsNullOrEmpty(function_id) then
        return false
    end
    local data = unpack(GlobalHooks.DB.Find('Function_Data', { function_id = function_id }))
    if data == nil then
        return false
    end
    return data.NeedOpen == 1
end
function FunctionUtil.OpenFunction(function_id, useimmediately, params)
    local params = params or {}
    print("function_id", function_id)
    local data = unpack(GlobalHooks.DB.Find('Function_Data', { function_id = function_id }))
    if data == nil then
        UnityEngine.Debug.LogError("function_id =  " .. function_id .. " is not exist")
        return
    end
    if data then
        local nofunctiontag = string.IsNullOrEmpty(data.function_tag)
        if useimmediately or nofunctiontag then
            return doFunction(data, params)
        elseif not nofunctiontag then

            local function_tag = data.function_tag
            if function_tag == 'shimen' then
                local protag = data.function_val[DataMgr.Instance.UserData.Pro]
                if not string.IsNullOrEmpty(protag) then
                    function_tag = protag
                end
            end
            return FunctionUtil.seekAndNpcTalkByFunctionTag(function_tag, params and params.Quest or nil)
        else
            return false
        end
    else

    end
    return false
end


----tag触发相关功能
--function FunctionUtil.OpenFunctionByTag(tag)
--	local fun = unpack(GlobalHooks.DB.Find('Function_Data',{function_id = tag}))
--		if fun then
--			GlobalHooks.UI.OpenUI(fun.type.val[1],0,fun.type.val[2],fun.type.val[3],fun.type.val[4])
--			return true
--		else
--			print("no function with tag =",tag)
--		end
--	return false
--end

local function DoEnterGuild(eventname, params)
    FunctionUtil.EnterGuildEvent(params.isSelf)
end

function FunctionUtil.Goto(function_go, params)
    params = params or {}
    local data = unpack(GlobalHooks.DB.Find('Function_GotoData', { function_go = function_go }))
    if data == nil then
        UnityEngine.Debug.LogError("FunctionUtil_Goto id =  " .. function_go .. " is not exist")
        return
    end
    local text = Util.GetText(data.des, unpack(params))

    GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, text,
            Util.GetText(data.okBtn),
            Util.GetText(data.cancelBtn),
            Util.GetText(data.Title),
            nil,
            function()
                FunctionUtil.OpenFunction(data.okBtnGoto)
            end, nil)
end

function FunctionUtil.initial()
    EventManager.Subscribe('Event.Guild.EnterGuild', DoEnterGuild)
end
function FunctionUtil.fin()
    EventManager.Unsubscribe('Event.Guild.EnterGuild', DoEnterGuild)
end

function FunctionUtil.CheckItemSourceRequire(function_id)

    local data = unpack(GlobalHooks.DB.Find('Function_Data', { function_id = function_id }))
    if data == nil then
        UnityEngine.Debug.LogError("function_id =  " .. function_id .. " is not exist")
        return false
    end
    local issucc, result = CheckRequire(data.require)
    if not issucc then
        return false, result
    else
        return true
    end

end

function FunctionUtil.GetSourceData(function_id)
    local data = unpack(GlobalHooks.DB.Find('Function_Data', { function_id = function_id }))
    if data == nil then
        UnityEngine.Debug.LogError("function_id =  " .. function_id .. " is not exist")
        return nil
    end
    return data
end

function FunctionUtil.seekAndNpcTalkByFunctionTag(functiontag, Quest)

    local npctempid, curMapID, born_point, isRoad, roadtype = QuestNpcDataModel.FindRoadByFunctionTag(functiontag)
    local action = nil
    if npctempid == 0 and isRoad ~= 1 then
        UnityEngine.Debug.LogError("function_id =  " .. functiontag .. " is not exist npc")
        return false
    end
    if roadtype == 'guild' then
        return FunctionUtil.FindGuildStore(npctempid)
    end
    if isRoad == 1 then
        --只寻路
        action = MoveEndAction()
    else
        action = MoveAndNpcTalk(npctempid)
        action.OpenFunction = true
        action.QuestId = Quest and Quest.id or 0
    end
    action.MapId = curMapID--roadData.born_scene
    action.RoadName = born_point--roadData.born_point
    if TLBattleScene.Instance.Actor then
        TLBattleScene.Instance.Actor:AutoRunByAction(action)
        return true
    end
    return false
end

--活动剩余时间  0是全天 -1 是未开放
function FunctionUtil.TimeLeftSec(function_id)
    local isSucc, result = FunctionUtil.CheckNowIsOpen(function_id, false)
    if isSucc then
        if result ~= nil and result == 0 then
            return 0
        else
            local activityData = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', { function_id = function_id }))
            if activityData == nil then
                return 0
            end
            for i, v in ipairs(activityData.time.close) do
                if not string.IsNullOrEmpty(v) then
                    local lefttime = FunctionUtil.ParseServerTime(v) - ServerTime.getServerTime()
                    if lefttime.TotalSeconds > 0 then
                        return math.floor(lefttime.TotalSeconds)
                    end
                end
            end
        end
    else
        return -1
    end
end
local week = {
    [1] = DayOfWeek.Monday,
    [2] = DayOfWeek.Tuesday,
    [3] = DayOfWeek.Wednesday,
    [4] = DayOfWeek.Thursday,
    [5] = DayOfWeek.Friday,
    [6] = DayOfWeek.Saturday,
    [7] = DayOfWeek.Sunday,
}

--是否在重置时间前
function FunctionUtil.IsBeforeResetTime()
    local datetime = ServerTime.getServerTime():ToLocalTime()
    local resettime = GameUtil.GetStringGameConfig("reset_time") -- 重置时间
    local resettimedateTime = FunctionUtil.ParseServerTime(resettime):ToLocalTime()  --重置时间格式化
    local lefttimedatetime = resettimedateTime - datetime  --今天是不是在重置时间前
    if datetime.Date == resettimedateTime.Date and lefttimedatetime.TotalSeconds > 0 then
        return true
    else
        return false
    end
end

-- function FunctionUtil.IsActivityOpenDay(functionid)
--     local activityData = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', { function_id = functionid }))

--     if activityData == nil then
--         --UnityEngine.Debug.LogError("function_id =  "..function_id.." is not exist")
--         return true
--     end

--     if activityData.open_type == 0 then
--         return false
--     end

--     if activityData.open_type == 1 then
--         return true
--     end

--     local isSucc = true
--     if activityData.open_type == 2 then
--         --周期
--         if not GameSceneMgr.Instance.syncServerTime:IsInToday(activityData.open_day) then
--             isSucc = false
--         end
--     elseif activityData.open_type == 3 then
--         --日期
--         if not GameSceneMgr.Instance.syncServerTime:IsBetweenTime(activityData.start_day, activityData.end_day) then
--             isSucc = false
--         end
--     end
--     return isSucc

-- end
--根据服务器时间生成datetime
function FunctionUtil.ParseServerTime(time)
    local parsetime = System.DateTime.Parse(time)
    local datetime = ServerTime.getServerTime():ToLocalTime().Date:AddHours(parsetime.Hour):AddMinutes(parsetime.Minute):ToUniversalTime()--System.DateTime.Parse(time)
    --local serverTime = datetime:ToUniversalTime():Add(ServerTime.getServerTime() - System.DateTime.UtcNow)
    return datetime
end
--包含重置时间计算的是否在当天
function FunctionUtil.IsInToday(function_id,onlyFuture)
    if onlyFuture == nil then
        onlyFuture = false
    end
    local activityData = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', { function_id = function_id }))
    if activityData == nil then
        return true
    end
    if activityData.open_type == 0 then
        return false
    end
    local open_day = activityData.open_day
    if string.IsNullOrEmpty(open_day) then
        return true
    end

    
    local datetime = ServerTime.getServerTime():ToLocalTime()
    local tmps = string.split(open_day, ',')
    local days = {}
    if #tmps > 0 then
        for i, v in ipairs(tmps) do
            local key = tonumber(v)
            if week[key] ~= nil then
                table.insert(days, week[key])
            end
        end
    else
        return true
    end
    -- print("function_id",function_id)
    -- print_r("days",days)
    -- print("datetime.DayOfWeek",datetime.DayOfWeek)
    if #days > 0 then
        for i, v in ipairs(days) do
            if datetime.DayOfWeek == v and ((not onlyFuture and not FunctionUtil.IsBeforeResetTime()) or onlyFuture) then
                return true
            else
                if not onlyFuture then
                    local yesterday = datetime:AddDays(-1).DayOfWeek -- 昨天的时间
                    if yesterday == v and FunctionUtil.IsBeforeResetTime() then
                        return true
                    end
                end
            end
        end
    end
    return false
end

--包含重置时间计算的是否在制定日期内
function FunctionUtil.IsBetweenTime(function_id)
    local activityData = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', { function_id = function_id }))
    if activityData == nil then
        return true
    end
    local startday = activityData.start_day
    local endday = activityData.end_day
    if string.IsNullOrEmpty(startday) or string.IsNullOrEmpty(endday) then
        return true
    end

    local datetime = ServerTime.getServerTime():ToLocalTime()
    local startDate = System.DateTime.Parse(startday)
    local stopDate = System.DateTime.Parse(endday)
    if (System.DateTime.Compare(datetime, startDate) < 0) then
        return false
    elseif (System.DateTime.Compare(datetime, stopDate) > 0) then

        if (datetime - stopDate).TotalDays <= 1 and FunctionUtil.IsBeforeResetTime() and (string.IsNullOrEmpty(activityData.time.open[1]) or activityData.time.open[1] == "0") then
            return true
        else
            return false
        end

    end
    return true
end
--获得最近的活动时间
function FunctionUtil.GetStartAndEndTimeTick(function_id)
    local activityData = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', { function_id = function_id }))
    if activityData == nil then
        return 0
    end
    local startticks = 0
    local endticks = 0

    for i, v in ipairs(activityData.time.close) do
        if not string.IsNullOrEmpty(v) then
            local closetime = FunctionUtil.ParseServerTime(v)
            if System.DateTime.Compare(closetime, ServerTime.getServerTime()) > 0 then
                --print("function_id",function_id,closetime,ServerTime.getServerTime())
                startticks = System.DateTime.Parse(activityData.time.open[i]).Ticks
                endticks = System.DateTime.Parse(v).Ticks
                return startticks, endticks
            end
        end
    end

    for i = #activityData.time.close, 1, -1 do
       if not string.IsNullOrEmpty(activityData.time.close[i]) then
           startticks = System.DateTime.Parse(activityData.time.open[i]).Ticks
           endticks = System.DateTime.Parse(activityData.time.close[i]).Ticks
           return startticks, endticks
       end
    end
    return -1

end


function FunctionUtil.NewIsBetweenTime(open, close)

        if string.IsNullOrEmpty(open) or string.IsNullOrEmpty(close) then
            return false
        end
        local datetime = ServerTime.getServerTime():ToLocalTime()
        local startDate = FunctionUtil.ParseServerTime(open):ToLocalTime()
        local stopDate = FunctionUtil.ParseServerTime(close):ToLocalTime()
        if datetime <startDate then
            return false
        elseif datetime > stopDate then
            return false
        end
        return true
end
function FunctionUtil.IsBetweenTimes(open, close)
    if open == nil or close == nil then
        return true
    end
    if #open == 0 or #close == 0 then
        return true
    end
    if #open ~= #close then
        return true
    end
    for i,v in ipairs(open) do
         if FunctionUtil.NewIsBetweenTime(v, close[i]) then
           return true
        end
    end
    return false
end

function FunctionUtil.IsInOpenSeverTime(lastdays,onlyFuture)
    local config = string.split(GameUtil.GetStringGameConfig("reset_time"),":")
    if lastdays == 0 then
        return true
    end
    local time = DataMgr.Instance.UserData.Serverinfo.open_at.Date:ToUniversalTime():AddDays(lastdays)
    if not onlyFuture then
        time = time:AddHours(tonumber(config[1])):AddMinutes(tonumber(config[2]))
    end
    local now = ServerTime.getServerTime()
    if now < time then
        return true
    end
    return false
end

--当前在活动时间返回true 全天活动返回开始时间0 ，否则返回开启时间 结束时间  （ticks）
--当前活动还未开启返回false 返回开启时间，结束时间 （ticks）
--活动不在当天开放的返回false 返回开启时间-1 
function FunctionUtil.CheckNowIsOpen(function_id, isShowTip,onlyFuture)
    local activityData = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', { function_id = function_id }))
    if activityData == nil then
        --UnityEngine.Debug.LogError("function_id =  "..function_id.." is not exist")
        return true, 0
    end

    if activityData.open_type == 0 then
        return false, -1
    end

    if activityData.open_type == 1 then
        return true, 0
    end

    local isSucc = true
    local startticks = 0
    local endticks = 0
    --if activityData.open_type == 3 then
        --日期

        if not FunctionUtil.IsBetweenTime(function_id) then
            isSucc = false
            startticks = -1
        end
        --print("FunctionUtil.IsBetweenTime(function_id)",function_id,isSucc)
    --end
    --if activityData.open_type == 2 then
        --周期
        if isSucc and not FunctionUtil.IsInToday(function_id,onlyFuture) then
            isSucc = false
            startticks = -1
        end
         --print("FunctionUtil.IsInToday(function_id)",function_id,isSucc)
    --end
    if isSucc and activityData.lastdays and not FunctionUtil.IsInOpenSeverTime(activityData.lastdays,onlyFuture) then
        isSucc = false
        startticks = -1
    end
    
   
    if isSucc then
        local open = {}
        local close = {}
        for i, v in ipairs(activityData.time.open) do
            if not string.IsNullOrEmpty(v) then
                table.insert(open, v)
            end
        end
        for i, v in ipairs(activityData.time.close) do
            if not string.IsNullOrEmpty(v) then
                table.insert(close, v)
            end
        end
        
        startticks, endticks = FunctionUtil.GetStartAndEndTimeTick(function_id)
        if #open ~= 0 and #close ~= 0 then
            local isResetTime = false
            if  onlyFuture and FunctionUtil.IsBeforeResetTime() then
                isSucc = false
                local resettime = FunctionUtil.GetResetTime()
                for i, v in ipairs(open) do
                    if TimeUtil.inTime(resettime.Date, FunctionUtil.GetResetTime(),FunctionUtil.ParseServerTime(v)) then
                        isSucc = true
                        isResetTime = true
                        break
                    end
                end
                if not isSucc then --重置时间前的处理
                    return false ,-1
                end
            end
            
            if not isResetTime and not FunctionUtil.IsBetweenTimes(open, close) then
                isSucc = false
            end
        end
        --print("FunctionUtil.IsInToday(function_id)",function_id,isSucc)
    end

    if not isSucc and isShowTip then
        GameAlertManager.Instance:ShowNotify(Constants.Text.notintime)
    end
    print("functionid",function_id)
    print("isSucc",isSucc)
    print_r("open",open)
    print_r("close",close)
    return isSucc, startticks, endticks
end

--是否在重置时间前
function FunctionUtil.GetResetTime()
    local resettime = GameUtil.GetStringGameConfig("reset_time") -- 重置时间
    local resettimedateTime = FunctionUtil.ParseServerTime(resettime):ToLocalTime()  --重置时间格式化
    return resettimedateTime
end
--打开相同ui下的不同页签(试用版)
function FunctionUtil.OpenFunctionByTagInSameUI(tag)
    local fun = unpack(GlobalHooks.DB.Find('Function_Data', { function_id = tag }))
    if fun then
        local ui = GlobalHooks.UI.FindUI(fun.type.val[1])
        if ui then
            local parent = GlobalHooks.UI.FindUI(ui.initParams.subui_1)
            if parent then
                for k, v in pairs(parent.initParams) do
                    if v == tostring(fun.type.val[2]) then
                        parent.root:FindChildByEditName(k, true).IsChecked = true
                    end
                end
            else
                return
            end
        else
            GlobalHooks.UI.OpenUI(fun.type.val[1], 0, fun.type.val[2], fun.type.val[3], fun.type.val[4])
        end
    else
        print("no function with tag =", tag)
        return
    end
end

function FunctionUtil.GotoInSameUI(function_go)
    local data = unpack(GlobalHooks.DB.Find('Function_GotoData', { function_go = function_go }))
    if data == nil then
        UnityEngine.Debug.LogError("FunctionUtil_Goto id =  " .. function_go .. " is not exist")
        return
    end
    GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL,
            Util.GetText(data.des),
            Util.GetText(data.okBtn),
            Util.GetText(data.cancelBtn),
            Util.GetText(data.Title),
            nil,
            function()
                FunctionUtil.OpenFunctionByTagInSameUI(data.okBtnGoto)
            end, nil)
end

--请求是否能门票
function FunctionUtil.CanBuyTicketsRequest(functionid,isShowTip)
    if isShowTip == nil then isShowTip = true end
    if GlobalHooks.IsFuncOpen(functionid, isShowTip) then
        local request = { c2s_functionid = functionid }
        Protocol.RequestHandler.ClientCanBuyTicketsRequest(request, function(rsp)
        end)
    end
end

--请求买门票
function FunctionUtil.GoToBuyticketsRequest(functionid)
    local request = { c2s_functionid = functionid }
    Protocol.RequestHandler.ClientBuyDailyTicketsRequest(request, function(rsp)
        EventManager.Fire("Event.TicketChange", { functionid = rsp.s2c_functionid, count = rsp.s2c_count })
    end)
end

function FunctionUtil.Register(key,cb)
    FunctionUtil.CustomCb = FunctionUtil.CustomCb or {}
    if FunctionUtil.CustomCb[key] == nil then
        FunctionUtil.CustomCb[key] = cb
    end
end

function FunctionUtil.UnRegister(key)
    FunctionUtil.CustomCb = FunctionUtil.CustomCb or {}
    if FunctionUtil.CustomCb[key] ~= nil then
        FunctionUtil.CustomCb[key] = nil
    end
end
function FunctionUtil.GotoCustomCb(key,params)
    if FunctionUtil.CustomCb ~= nil then
         if FunctionUtil.CustomCb[key] ~= nil then
            FunctionUtil.CustomCb[key](key,params)
         end
    end
end

return FunctionUtil