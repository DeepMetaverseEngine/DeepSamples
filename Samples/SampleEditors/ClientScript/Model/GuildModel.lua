local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local ChatUtil = require 'UI/Chat/ChatUtil'
local ChatModel = require 'Model/ChatModel'
local ItemModel = require 'Model/ItemModel'
local RechargeModel = require 'Model/RechargeModel'
local SnapReader = require 'Model/SnapReader'


local CacheData = {
    guildList = {},
    guildListPart = 0,
    guildListFull = false,
    guildAttackList = {},
    guildAttackListPart = 0,
    guildAttackListFull = false,
    attackGuild = nil,
    attackedGuild = nil,
    talentLvUp = {},
}

_M.SortType = {
    default = 0,
    level = 1,
    fightPower = 2,
    memberCount = 3,
    pro = 4,
    position = 5,
    state = 6,
    contribution = 7,
    contributionMax = 8,
    attackCount = 9,
    attacked = 10,
}

_M.itemListener = {}

local eventId
local positionList ={}


--创建公会
function _M.ClientCreateGuildRequest(name, recuit, cb)
    -- print("----------ClientCreateGuildRequest----------")
    local msg = { c2s_guildName = name, c2s_recruitBulletin = recuit }
    Protocol.RequestHandler.ClientCreateGuildRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--公会信息
function _M.ClientGetGuildInfoRequest(cb)
    -- print("----------ClientGetGuildInfoRequest----------")
    local msg = {}
    Protocol.RequestHandler.ClientGetGuildInfoRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--公告板更新请求
function _M.ClientChangeNoticeRequest(notice, cb)
    -- print("----------ClientChangeNoticeRequest----------")
    local msg = { c2s_context = notice }
    Protocol.RequestHandler.ClientChangeNoticeRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--公会列表请求
function _M.ClientGetGuildListRequest(force, cb)
    -- print("----------ClientGetGuildListRequest----------")
    if force or not CacheData.guildListFull then
        if force then
            CacheData.guildList = {}
            CacheData.guildListPart = 0
            CacheData.guildListFull = false
        end
        local msg = { c2s_part = CacheData.guildListPart }
        Protocol.RequestHandler.ClientGetGuildListRequest(msg, function(rsp)
            -- print_r(rsp.s2c_guildList)
            table.appendList(CacheData.guildList, rsp.s2c_guildList)
            CacheData.guildListFull = rsp.s2c_isFull
            CacheData.guildListPart = rsp.s2c_isFull and CacheData.guildListPart or CacheData.guildListPart + 1
            if cb then
                cb(CacheData.guildList)
            end
        end, PackExtData(force, force))
    else
        if cb then
            cb(CacheData.guildList)
        end
    end
end

--申请加入公会
function _M.ClientApplyGuildRequest(guildId, cb)
    -- print("----------ClientApplyGuildRequest----------")
    local msg = { c2s_guildId = guildId }
    Protocol.RequestHandler.ClientApplyGuildRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取成员列表
function _M.ClientGuildMemListRequest(cb)
    -- print("----------ClientGuildMemListRequest----------")
    Protocol.RequestHandler.ClientGuildMemListRequest({}, function(rsp)
        -- print_r(rsp)
        positionList = {}
        local listData = rsp.s2c_members
        for i = 1, #listData do
            positionList[listData[i].roleId] = listData[i].position
        end
        if cb then
            cb(rsp)
        end
    end)
end

--获取审批列表
function _M.ClientGuildApplyListRequest(cb)
    -- print("----------ClientGuildApplyListRequest----------")
    Protocol.RequestHandler.ClientGuildApplyListRequest({}, function(rsp)
        print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--审批操作
function _M.ClientDealGuildApplyRequest(msgId, op, cb)
    -- print("----------ClientDealGuildApplyRequest----------")
    local msg = { c2s_msgId = msgId, c2s_operate = op }
    Protocol.RequestHandler.ClientDealGuildApplyRequest(msg, function(rsp)
        print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--申请条件
function _M.ClientApplyConditionRequest(condition, cb)
    -- print("----------ClientApplyConditionRequest----------")
    local msg = { c2s_condition = condition }
    Protocol.RequestHandler.ClientApplyConditionRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--招人公告更新请求
function _M.ClientChangeRecruitRequest(notice, cb)
    -- print("----------ClientChangeRecruitRequest----------")
    local msg = { c2s_context = notice }
    Protocol.RequestHandler.ClientChangeRecruitRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--每日捐献
function _M.ClientDailyDonateRequest(dType, cb)
    -- print("----------ClientDailyDonateRequest----------")
    local msg = { c2s_type = dType }
    Protocol.RequestHandler.ClientDailyDonateRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--修改职位
function _M.ClientChangePostionRequest(roleId, position, cb)
    -- print("----------ClientChangePostionRequest----------")
    local msg = {c2s_tarRoleId = roleId, c2s_tarPosition = position}
    Protocol.RequestHandler.ClientChangePostionRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--退出公会，踢人
function _M.ClientQuitGuildRequest(roleId, cb)
    -- print("----------ClientQuitGuildRequest----------")
    local msg = {c2s_roleId = roleId}
    Protocol.RequestHandler.ClientQuitGuildRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--转让会长
function _M.ClientTransferGuildRequest(roleId, cb)
    -- print("----------ClientTransferGuildRequest----------")
    local msg = {c2s_roleId = roleId}
    Protocol.RequestHandler.ClientTransferGuildRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--弹劾会长
function _M.ClientImpeachGuildRequest(cb)
    -- print("----------ClientImpeachGuildRequest----------")
    local msg = {}
    Protocol.RequestHandler.ClientImpeachGuildRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--建筑升级
function _M.ClientBuildLevelUpRequest(id, cb)
    -- print("----------ClientBuildLevelUpRequest----------")
    local msg = { c2s_buildId = id }
    Protocol.RequestHandler.ClientBuildLevelUpRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--请求天赋列表
function _M.ClientGetTalentDataRequest(cb)
    -- print("----------ClientGetTalentDataRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGetTalentDataRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--请求天赋升级
function _M.ClientTalentSkillUpRequest(skillId, cb)
    -- print("----------ClientTalentSkillUpRequest----------")
    local msg = { c2s_skillId = skillId }
    Protocol.RequestHandler.ClientTalentSkillUpRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--请求进入公会场景
function _M.ClientEnterGuildSceneRequest(cb)
    -- print("----------ClientEnterGuildSceneRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientEnterGuildSceneRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取礼物列表
function _M.ClientGiftInfoRequest(cb)
    -- print("----------ClientGiftInfoRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGiftInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--一键领取礼物
function _M.ClientOpenGiftRequest(cb)
    -- print("----------ClientOpenGiftRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientOpenGiftRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取神兽信息
function _M.ClientGetMonsterInfoRequest(cb)
    -- print("----------ClientGetMonsterInfoRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGetMonsterInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--神兽一键喂食
function _M.ClientMonsterLeveUpRequest(cb)
    -- print("----------ClientMonsterLeveUpRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientMonsterLeveUpRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--邀请公会
function _M.ClientInviteToGuildRequest(playerId, cb)
    -- print("----------ClientInviteToGuildRequest----------")
    local msg = { c2s_roleId = playerId }
    Protocol.RequestHandler.ClientInviteToGuildRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--公会目标列表
function _M.ClientGetGuildAttackListRequest(force, cb)
    -- print("----------ClientGetGuildAttackListRequest----------")
    if force or not CacheData.guildAttackListFull then
        if force then
            CacheData.guildAttackList = {}
            CacheData.guildAttackListPart = 0
            CacheData.guildAttackListFull = false
        end
        local msg = { c2s_part = CacheData.guildAttackListPart }
        Protocol.RequestHandler.ClientGetGuildAttackListRequest(msg, function(rsp)
            print_r('------------sss', rsp)
            local selfGuildId = DataMgr.Instance.UserData.GuildId
            local selfGuildIndex = 0
            --为每个公会添加被攻击次数数据
            for i = 1, #rsp.s2c_guildList do
                local info = rsp.s2c_guildList[i]
                if info.id == selfGuildId then
                    selfGuildIndex = i
                else
                    local attackInfo = rsp.s2c_attackGuilds[info.id]
                    if attackInfo ~= nil then
                        print_r('-----------------aaa', attackInfo)
                        info.attackCount = attackInfo.attackCount
                        info.attackTime = attackInfo.lastAttackTime
                    else
                        info.attackCount = 0
                    end
                end
            end
            --把自己公会剔除
            if selfGuildIndex > 0 then
                table.remove(rsp.s2c_guildList, selfGuildIndex)
            end

            table.appendList(CacheData.guildAttackList, rsp.s2c_guildList)
            CacheData.guildAttackListFull = rsp.s2c_isFull
            CacheData.guildAttackListPart = rsp.s2c_isFull and CacheData.guildAttackListPart or CacheData.guildAttackListPart + 1
            CacheData.attackGuild = rsp.s2c_attackGuild
            CacheData.attackedGuild = rsp.s2c_attackedGuild
            if cb then
                cb(CacheData.guildAttackList, CacheData.attackGuild, CacheData.attackedGuild)
            end
        end, PackExtData(force, force))
    else
        if cb then
            cb(CacheData.guildAttackList, CacheData.attackGuild, CacheData.attackedGuild)
        end
    end
end

function _M.ClientGuildCarriageStateRequest(cb,errcb)
    local msg = {}
    Protocol.RequestHandler.ClientGuildCarriageStateRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end,function() 
        if errcb then
            errcb()
        end
    end)
end


function _M.ClientGuildJoinCarriageRequest(cb)
    local msg = {}
    Protocol.RequestHandler.ClientGuildJoinCarriageRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--设置攻击公会
function _M.ClientSetGuildAttackRequest(guildId, cb)
    -- print("----------ClientSetGuildAttackRequest----------")
    local msg = { c2s_guildId = guildId }
    Protocol.RequestHandler.ClientSetGuildAttackRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--仙盟活动信息
function _M.ClientGuildActivityInfoRequest(cb)
    -- print("----------ClientGuildActivityInfoRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGuildActivityInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--仙盟据点信息
function _M.ClientGuildFortInfoRequest(cb)
    -- print("----------ClientGuildFortInfoRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGuildFortInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--仙盟据点报名
function _M.ClientGuildFortSignUpRequest(fortId, cb)
    -- print("----------ClientGuildFortSignUpRequest----------")
    local msg = { c2s_fortId = fortId }
    Protocol.RequestHandler.ClientGuildFortSignUpRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end


function _M.GetMemberPosition(id)
    if id and positionList[id] then
        return positionList[id]
    end
    return 0
end

function _M.CanTalentGroupLvUp(talentLv)
    local len = table.len(CacheData.talentLvUp[talentLv])
    return len
end

function _M.CanTalentLvUp(talentLv, skillId)
    return CacheData.talentLvUp[talentLv][skillId] and true or false
end

local function CheckTalentIslock( data )
    if DataMgr.Instance.GuildData:GetGuildBuildLv(GuildData.GuildBuild._College) < data.talent_lv then
        return true --未解锁
    end
    if data.front_id ~= 0 and (DataMgr.Instance.GuildData:GetGuildTalentLv(data.front_id) == 0 or DataMgr.Instance.GuildData:GetGuildTalentLv(data.front_id) < data.front_lv) then
        return true --未解锁
    end
    return false --解锁
end

local function CheckTalentLvUp(talentdb, lvdb)
    local enouth = false
    if not string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) and not CheckTalentIslock(talentdb) then
        local costs = ItemModel.ParseCostAndCostGroup(lvdb)
        enouth = ItemModel.IsCostAndCostGroupEnough(costs, true)
        GlobalHooks.UI.SetRedTips('guild_college', enouth and 1 or 0, talentdb.skill_id)
    else
        GlobalHooks.UI.SetRedTips('guild_college', 0, talentdb.skill_id)
    end
    CacheData.talentLvUp[talentdb.talent_lv] = CacheData.talentLvUp[talentdb.talent_lv] or {}
    CacheData.talentLvUp[talentdb.talent_lv][talentdb.skill_id] = enouth and 1 or nil
    -- print('ssssssssssssss', talentdb.skill_id, enouth)
end

local function CheckCanDonate()
    if not string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) then
        local donateLeft = RechargeModel.GetVipInfoValueByKey('guild_donate') - DataMgr.Instance.GuildData.DonateCount
        local canDonate = false
        if donateLeft > 0 then
            local donatedb = GlobalHooks.DB.GetFullTable('guild_donate')
            for k, v in ipairs(donatedb) do
                local count = ItemModel.CountItemByTemplateID(v.cost_id)
                if count >= v.cost_num then
                    canDonate = true
                    break
                end
            end
        end
        GlobalHooks.UI.SetRedTips('guild_donate', (canDonate and donateLeft > 0) and donateLeft or 0)
    else
        GlobalHooks.UI.SetRedTips('guild_donate', 0)
    end
end


local function OnClientGuildChangeNotify( notify )
    -- print("ClientGuildChangeNotify---------------------------------")
    -- print_r(notify)
    -- todo 关闭公会界面
    if notify.s2c_guildId == nil or notify.s2c_guildId == '' then --退出仙盟
        if GlobalHooks.UI.FindUI('GuildMain') then
            MenuMgr.Instance:CloseAllMenu()
        end
        if notify.s2c_isKick then
            GameAlertManager.Instance:ShowNotify(Util.GetText('guild_kick', notify.s2c_guildName))
        end
        notify.s2c_guildName = ''
    else
        _M.ClientGuildMemListRequest()
    end
    DataMgr.Instance.UserData.GuildId = notify.s2c_guildId
    DataMgr.Instance.UserData.GuildName = notify.s2c_guildName
end

local function ListenNextDay()
    local alltime = {}
    local resetTime = GlobalHooks.DB.GetGlobalConfig('reset_time')
    local b, dt = System.DateTime.TryParse(resetTime, System.DateTime.Now)
    table.insert(alltime, {Hour = dt.Hour, Minute = dt.Minute, Second = 5})
    EventApi.Listen.TodayTime(alltime, function()
        -- print('------------next day-------------')
        DataMgr.Instance.GuildData:ResetDonateCount()
        CheckCanDonate()
    end)
    EventApi.Task.Wait()
end

function _M.Notify(status, subject)
    if subject == DataMgr.Instance.GuildData then
        if subject:ContainsKey(status, GuildData.NotiFyStatus.Donate) then
            CheckCanDonate()
        elseif subject:ContainsKey(status, GuildData.NotiFyStatus.Talent) then
            local skills = CSharpMap2Table(DataMgr.Instance.GuildData.TalentList)
            for k, v in pairs(skills) do
                local skillId = k
                local skillLv = v
                if _M.itemListener['talent'..skillId].listener and _M.itemListener['talent'..skillId].level ~= skillLv then   --等级发生变化，重新监听新的升级道具
                    _M.itemListener['talent'..skillId].listener:Dispose()
                    local talentdb = GlobalHooks.DB.FindFirst('guild_talent', { skill_id = skillId } )
                    local lvdb = GlobalHooks.DB.FindFirst('guild_talent_lv', { skill_id = skillId, skill_lv = skillLv } )
                    _M.itemListener['talent'..skillId].listener = ItemModel.ListenCostXlsLine(lvdb, function()
                        CheckTalentLvUp(talentdb, lvdb)
                    end)
                    _M.itemListener['talent'..skillId].level = skillLv
                    CheckTalentLvUp(talentdb, lvdb)
                end
            end
        end
    end
end

local function LoadGuildSnap(keys, cb)
    local request = {c2s_rolesID = keys}
    Protocol.RequestHandler.GetGuildSnapRequest(
        request,
        function(rsp)
            cb(rsp.s2c_data)
        end
    )
end

function _M.fin()
  -- print('----------GuildModel fin------------')
    if _M.itemListener then
        for _, v in pairs(_M.itemListener) do
            if v.listener then
                v.listener:Dispose()
            end
        end
        _M.itemListener = {}
    end

    if eventId and EventApi then
        EventApi.Task.StopEvent(eventId)
        eventId = nil
    end

    DataMgr.Instance.GuildData:DetachLuaObserver('GuildModel')
end

function _M.OnBagReady()
    --监听捐献道具
    local donatedb = GlobalHooks.DB.GetFullTable('guild_donate')
    for k, v in ipairs(donatedb) do
        _M.itemListener['donate'..k] = {}
        _M.itemListener['donate'..k].listener = ItemModel.ListenByTemplateID(v.cost_id, function()
            CheckCanDonate()
        end)
    end
    CheckCanDonate()

    --监听技能道具
    local talentdb = GlobalHooks.DB.GetFullTable('guild_talent')
    for _, v in ipairs(talentdb) do
        local skillId = v.skill_id
        _M.itemListener['talent'..skillId] = {}
        local skillLv = DataMgr.Instance.GuildData:GetGuildTalentLv(skillId)
        -- if not CheckTalentIslock(v) then    --如果是可升级的，就添加监听
            local lvdb = GlobalHooks.DB.FindFirst('guild_talent_lv', { skill_id = v.skill_id, skill_lv = skillLv } )
            _M.itemListener['talent'..skillId].listener = ItemModel.ListenCostXlsLine(lvdb, function()
                CheckTalentLvUp(v, lvdb)
            end)
        -- end
        _M.itemListener['talent'..skillId].level = skillLv
        CheckTalentLvUp(v, lvdb)
    end
end

function _M.initial()
    if EventApi then
        eventId = EventApi.Task.StartEvent(ListenNextDay)
    end
    DataMgr.Instance.GuildData:AttachLuaObserver('GuildModel', _M)
end

function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ClientGuildChangeNotify(OnClientGuildChangeNotify)
    else
        _M.ClientGuildMemListRequest()
    end
end

_M.SnapReader = SnapReader.Create(LoadGuildSnap)

return _M
