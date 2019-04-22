---作者：任祥建
---时间：2018/9/11
---PlayRuleModel

local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'

local identitydata = {}
local refreshtime = nil
local eventId = nil

function _M.GetDefultData()
    local detail = GlobalHooks.DB.GetFullTable('imagearena_default')
    return detail
end

function _M.GetShowData(job)
    local detail = GlobalHooks.DB.Find('imagearena', {job = job})
    return detail
end

function _M.GetJobRes()
    local detail = GlobalHooks.DB.GetFullTable('imagearena_res')
    return detail
end

--请求门派数据
function _M.RequestPlayRule(index,cb)
    local msg = {c2s_index = index}
    Protocol.RequestHandler.TLClientGetMasterIdListInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--请求挑战
function _M.RequestBattle(masterid,index,cb)
    local msg = {c2s_masterid = masterid,c2s_index = index}
    Protocol.RequestHandler.TLClientMasterIdChallengeRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--修改亲信操作
function _M.RequestAddRelative(roleid,type,cb)
    local ui = GlobalHooks.UI.FindUI('PlayRuleMain')
    if ui then
        if type == 0 then
            ui.CancelRelative(function()
                local msg = {roleID = roleid,type = type}
                Protocol.RequestHandler.TLClientMasterIdAppointRequest(msg, function(rsp)
                    if cb then
                        cb()
                    end
                end)
            end)
        elseif type == 1 then
            ui.ChooseRelative(ui,function (uuid,node)
                local msg = {roleID = uuid,type = type}
                Protocol.RequestHandler.TLClientMasterIdAppointRequest(msg, function(rsp)
                    node.Visible = false
                    GameAlertManager.Instance:ShowNotify(Util.GetText("masterrace_friend_notice"))
                    if cb then
                        cb()
                    end
                end)
            end)
        end
    end
end

--更改头衔
function _M.RequestRename(roleid)
    local ui = GlobalHooks.UI.FindUI('PlayRuleMain')
    if ui then
        ui.ChangeName(ui,function(name,node)
            local msg = {roleID = roleid,rename = name}
            Protocol.RequestHandler.TLClientMasterIdChangeTitleRequest(msg, function(rsp)
                node.Visible = false
            end)
        end)
    end
end

local function Notify(msg)
    local ui = GlobalHooks.UI.FindUI('PlayRuleMain') 
    if ui then
        ui.StartSetUI(ui)
    end
end

local function RefreshIdentity()
    local alltime = {}
    local time = refreshtime:ToLocalTime()
    table.insert(alltime, {Hour = time.Hour, Minute = time.Minute+5, Second = time.Second})
    EventApi.Listen.TodayTime(alltime, function()
        Protocol.RequestHandler.TLClientMasterRaceResultRequest({},function(rsp)
            identitydata = rsp.raceDataMap
            refreshtime = rsp.refreshTime
            EventApi.Task.StopEvent(eventId)
            eventId = EventApi.Task.StartEvent(RefreshIdentity)
        end)
    end)
    EventApi.Task.Wait()
end

function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.TLClientMasterRaceQinXinChangeNotify(Notify)
    else
        Protocol.RequestHandler.TLClientMasterRaceResultRequest({},function(rsp)
            identitydata = rsp.raceDataMap
            refreshtime = rsp.refreshTime
            eventId = EventApi.Task.StartEvent(RefreshIdentity)
        end)
    end
end

--获取门职数据,缺少参数返回自己的数据
function _M.GetIdentityString(roleid,pro)
    local detail
    if not pro then
        pro = DataMgr.Instance.UserData.Pro
    end
    if not roleid then
        roleid = DataMgr.Instance.UserData.RoleID
    end
    for i1, v in pairs(identitydata) do
        for i2, v2 in pairs(v.raceDatalist) do
            if v2.playeruuid == roleid then
                detail = unpack(GlobalHooks.DB.Find('imagearena', {job = i1,imagearena_type = v2.masterid}))
                return Util.GetText(detail.name)
            end
        end
    end
    detail = unpack(GlobalHooks.DB.Find('imagearena', {job = pro,imagearena_type = 6}))
    return (Util.GetText(detail.name))
end

function _M.GetIdentity(roleid,pro)
    local detail
    if not pro then
        pro = DataMgr.Instance.UserData.Pro
    end
    if not roleid then
        roleid = DataMgr.Instance.UserData.RoleID
    end
    for i1, v in pairs(identitydata) do
        for i2, v2 in pairs(v.raceDatalist) do
            if v2.playeruuid == roleid then
                return GlobalHooks.DB.FindFirst('imagearena', {job = i1,imagearena_type = v2.masterid})
            end
        end
    end
    return GlobalHooks.DB.Find('imagearena', {job = pro,imagearena_type = 6})
end

function _M.fin()
    if eventId and EventApi then
        EventApi.Task.StopEvent(eventId)
        eventId = nil
    end
end



return _M