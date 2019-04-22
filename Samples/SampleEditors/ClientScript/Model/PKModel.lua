---作者：任祥建
---时间：2019/2/21
---PKModel
local PKModel = {}
PKModel.__index = PKModel

local Util = require 'Logic/Util'

local timer = nil
local waittimer = nil
PKModel.waitcd = 0

PKModel.waitpkui = nil
PKModel.pkedui = nil

--搜索好友
function PKModel.SeachPlayer(name,cb)
    if string.IsNullOrEmpty(name) then
        GameAlertManager.Instance:ShowNotify(Util.GetText("name_cant_empty"))
        return
    end
    local msg = {c2s_filter = name}
    Protocol.RequestHandler.ClientGetPlayersByNameRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--发起挑战
function PKModel.IssueChallenge(uuid,PKType,Gold,text,cb)
    local msg = {c2s_beChallengerId = uuid,c2s_challengeType = PKType,c2s_challengeGold = Gold,c2s_message=text}
    Protocol.RequestHandler.ClientApplyChallengeRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--回应挑战
function PKModel.ResponseChallenge(opt, cbsuccess,cbfailed)
    local msg = {c2s_opt = opt}
    Protocol.RequestHandler.ClientReplyChallengeRequest(msg, function(rsp)
        if cbsuccess then
            cbsuccess(rsp)
        end
    end,function(rsp)
        if cbfailed and rsp.s2c_code ~= 506 then
            cbfailed(rsp)
        end
    end)
end

--获取当前有无比赛
function PKModel.GetChallengeState(cb)
    local msg = {}
    Protocol.RequestHandler.ClientGetArenaInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--观看战斗
function PKModel.WatchChallenge(cb)
    local msg = {}
    Protocol.RequestHandler.ClientWatchArenaRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--收到挑战
function PKModel.ReceivePK(params)
    local hudui = HudManager.Instance:GetHudUI("MainHud")
    local pkedui = nil
    if hudui and params.cd>0 then
        local hudnode = hudui:FindChildByEditName("cvs_hud_arena", true)
        local lb_cd = hudnode:FindChildByEditName("lb_hud_arenacountdown", true)
        hudnode.Visible = false
        pkedui = GlobalHooks.UI.OpenUI("PKed",0,params)
        pkedui.hudnode = hudnode
        hudnode.TouchClick = function(sender)
            hudnode.Visible = false
            pkedui = GlobalHooks.UI.OpenUI("PKed",0,params)
            pkedui.hudnode = hudnode
        end
        if timer then
            LuaTimer.Delete(timer)
            timer = nil
        end
        timer = LuaTimer.Add(0,1000,function()
            if params.cd > 0 then
                if pkedui and pkedui.Update then
                    pkedui.Update(params.cd)
                end
                lb_cd.Text = Util.GetText('business_activity_sec',params.cd)
                params.cd = params.cd - 1
                return true
            else
                if pkedui and pkedui.Update then
                    pkedui.Update(params.cd)
                end
                lb_cd.Text = Util.GetText('business_activity_sec',params.cd)
                if timer then
                    LuaTimer.Delete(timer)
                    timer = nil
                end
                hudnode.Visible = false
                PKModel.receive = nil
                return false
            end
        end)
    end
end

--function PKModel.StartWait(cd)
--    if cd then
--        PKModel.waitcd = cd
--    end
--
--    if waittimer then
--        LuaTimer.Delete(waittimer)
--        waittimer = nil
--    end
--    
--    PKModel.waitpkui = GlobalHooks.UI.FindUI("WaitPK")
--    waittimer = LuaTimer.Add(0,1000,function()
--        if PKModel.waitcd > 0 then
--            if not PKModel.waitpkui then
--                PKModel.waitpkui = GlobalHooks.UI.OpenUI("WaitPK",0)
--            end
--            if PKModel.waitpkui.Update then
--                PKModel.waitpkui.Update(PKModel.waitcd)
--            end
--        else
--            if PKModel.waitpkui then
--                if PKModel.waitpkui.Update then
--                    PKModel.waitpkui.Update(PKModel.waitcd)
--                end
--                PKModel.waitpkui:Close()
--            end
--            if waittimer then
--                LuaTimer.Delete(waittimer)
--                waittimer = nil
--            end
--            return false
--        end
--        PKModel.waitcd = PKModel.waitcd - 1
--        return true
--    end)
--end

local function ReceivePKResponse(opt)
    --local ui = GlobalHooks.UI.FindUI("WaitPK")
    if opt.s2c_opt == 0 then
        GameAlertManager.Instance:ShowNotify(Util.GetText("arena_refuse"))
    elseif opt.s2c_opt == 1 then
        GameAlertManager.Instance:ShowNotify(Util.GetText("arena_accept"))
    end
    --if ui then
    --    ui:Close()
    --end
end

local function ReceivePK(...)
    local temp = unpack({...})
    local params = {
        uuid = temp.s2c_challengerId,
        pktype = temp.s2c_challengeType,
        gold = temp.s2c_challengeGold,
        text = temp.s2c_challengeMessage,
        cd = temp.s2c_timeLeft
    }
    PKModel.receive = params
    PKModel.ReceivePK(params)
end

function PKModel.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ClientChallengeNotify(ReceivePKResponse)
        Protocol.PushHandler.ClientBeChallengeNotify(ReceivePK)
    end
end

function PKModel.OnEnterScene()
    --local sceneid = DataMgr.Instance.UserData.MapTemplateId
    --if sceneid == 0 then
    --    sceneid = GameGlobal.Instance.SceneID
    --end
    --if sceneid == 500210 and PKModel.waitcd > 0 then
    --    PKModel.waitpkui = GlobalHooks.UI.OpenUI("WaitPK",0)
    --end
end

function PKModel.initial()
    local tempkey = {"arena_silver","arena_gold","arena_watch"}
    for i, v in ipairs(tempkey) do
        FunctionUtil.UnRegister(v)
        FunctionUtil.Register(v,function(params)
            if params == tempkey[1] then
                PKModel.GetChallengeState(function (rsp)
                    if rsp.s2c_status ~= 1 then
                        GlobalHooks.UI.OpenUI("PKCostMain",0,0)
                    else
                        GameAlertManager.Instance:ShowNotify(Util.GetText("arena_busy"))
                    end
                end)
                return
            end
            if params == tempkey[2] then
                PKModel.GetChallengeState(function (rsp)
                    if rsp.s2c_status ~= 1 then
                        GlobalHooks.UI.OpenUI("PKCostMain",0,1)
                    else
                        GameAlertManager.Instance:ShowNotify(Util.GetText("arena_busy"))
                    end
                end)
                return
            end
            if params == tempkey[3] then
                PKModel.WatchChallenge()
                return
            end
        end)
    end
end

function PKModel.fin(relogin)
    if relogin then
        local tempkey = {"arena_silver","arena_gold","arena_watch"}
        for i, v in ipairs(tempkey) do
            FunctionUtil.UnRegister(v)
            FunctionUtil.Register(v,function(params)
                if params == tempkey[1] then
                    PKModel.GetChallengeState(function (rsp)
                        if rsp.s2c_status ~= 1 then
                            GlobalHooks.UI.OpenUI("PKCostMain",0,0)
                        else
                            GameAlertManager.Instance:ShowNotify(Util.GetText("arena_busy"))
                        end
                    end)
                    return
                end
                if params == tempkey[2] then
                    PKModel.GetChallengeState(function (rsp)
                        if rsp.s2c_status ~= 1 then
                            GlobalHooks.UI.OpenUI("PKCostMain",0,1)
                        else
                            GameAlertManager.Instance:ShowNotify(Util.GetText("arena_busy"))
                        end
                    end)
                    return
                end
                if params == tempkey[3] then
                    PKModel.WatchChallenge()
                    return
                end
            end)
        end
    end
end

return PKModel