local ItemModel = require 'Model/ItemModel'

local _M = {}
_M.__index = _M

_M.itemListener = {}

_M.skillMap = {}
_M.RedCache = {}

_M.lastSkillLvMax = 0

local function GetSex()
    -- return DataMgr.Instance.UserData.Gender + 1
     return DataMgr.Instance.UserData.Gender
end

local function GetPro()
    -- body
    return DataMgr.Instance.UserData.Pro
end

local function GetLevel()
    -- body
    return DataMgr.Instance.UserData.Level
end

local function GetUserSkills()
    -- body
    return GlobalHooks.DB.Find('SkillOpen',{sex = GetSex(),profession = GetPro()})
end

local function GetSkillPos(skillId)
    -- body
    local data = GlobalHooks.DB.FindFirst('SkillOpen',{sex = GetSex(),skill_id = skillId})
    return data.icon_order
end

local function GetSkillData(skillId,skillLv)
    -- body
    return GlobalHooks.DB.FindFirst('Skill',{skill_id = skillId,skill_lv = skillLv})
end

local function GetSkillOpenData(skillId)
    -- body
    return GlobalHooks.DB.FindFirst('SkillOpen',{sex = GetSex(),skill_id = skillId})
end

function _M.GetRedPoint(skillId)
    return _M.RedCache[skillId] or false
end

local function GetSkillLvMaxData()

    local level = GetLevel()
    local skillCondition = GlobalHooks.DB.GetFullTable('SkillCondition')
    for i,v in ipairs(skillCondition) do
        if level >= v.player_lvmin and level <= v.player_lvmax then
            return v
        end
    end
    return nil
end

-- 技能最大可以升级到多少级
local function GetSkillLvMax()
    -- body
    local data = GetSkillLvMaxData()
    if data ~= nil then
        return data.skill_lvmax
    end
    return 0
end


local function GetSkillLvMaxDataByLvMin(playerLvMin)
    -- body
    local level = GetLevel()
    -- print('player level:',level)
    local skillCondition = GlobalHooks.DB.FindFirst('SkillCondition',{player_lvmin = playerLvMin})
    return skillCondition
end


local function CheckSkillLvUp(skillId)

    local skillLv = _M.skillMap[skillId] or 1
    local skillLvMax = GetSkillLvMax()
     
    local playerLevel = GetLevel()
    if _M.itemListener['skill'..skillId] and playerLevel >= _M.itemListener['skill'..skillId].open_lv and skillLv < skillLvMax then
        local lvdb = GetSkillData(skillId, skillLv)
        local costs = ItemModel.ParseCostAndCostGroup(lvdb)
        local enouth = ItemModel.IsCostAndCostGroupEnough(costs)
        GlobalHooks.UI.SetRedTips('skill', enouth and 1 or 0, skillId)
        _M.RedCache[skillId] = enouth 
    else
        GlobalHooks.UI.SetRedTips('skill', 0, skillId)
        _M.RedCache[skillId] = false
    end
end


local function ReqSkillList(cb,force)
    local msg = {}
    Protocol.RequestHandler.TLClientGetSkillListRequest(msg, function(resp)
        for skillId,skillLv in pairs(resp.skillMap) do
            _M.skillMap[skillId] = skillLv
            if not _M.itemListener['skill'..skillId] then
                _M.itemListener['skill'..skillId] = {}
                local skillOpen = GlobalHooks.DB.FindFirst('SkillOpen',{sex = GetSex(),skill_id = skillId})
                _M.itemListener['skill'..skillId].open_lv = skillOpen.open_lv
                local lvdb = GetSkillData(skillId, skillLv)
                _M.itemListener['skill'..skillId].listener = ItemModel.ListenCostXlsLine(lvdb, function()
                    CheckSkillLvUp(skillId)
                end)

                CheckSkillLvUp(skillId)
            end
        end
 
        if cb then
            cb(resp)
        end
    -- end)
    end, PackExtData(force, force))
end

local function ReqUpgradeSkill(skillId,type, cb)
    local msg = { type = type, skillId = skillId }
    Protocol.RequestHandler.TLClientSkillUpLevelRequest(msg, function(resp)
        -- print_r(ret)
        _M.lastSkillLvMax = GetSkillLvMax()
        for skillId,skillLv in pairs(resp.skillMap) do
            _M.skillMap[skillId] = skillLv
        end

        local userSkill = GetUserSkills()
        for k,skillOpen in pairs(userSkill) do
            local skillId2 = skillOpen.skill_id 
            CheckSkillLvUp(skillId2)
        end

        if cb then
            cb(resp)
        end
    end)
end
 

function _M.Notify(status, subject)
    if subject == DataMgr.Instance.UserData then
        if subject:ContainsKey(status, UserData.NotiFyStatus.LEVEL) then
			
            if not GlobalHooks.IsFuncOpen('SkillFrame') or not GameGlobal.Instance.netMode then
                return
            end
  
            local userSkill = GetUserSkills()
            for _,skillOpen in pairs(userSkill) do
                local skillId = skillOpen.skill_id 
                CheckSkillLvUp(skillId)
            end
        end
    end
end

function _M.OnBagReady() 
   
    local skillLvMax = _M.lastSkillLvMax
    local userSkill = GetUserSkills()

    ReqSkillList(function (resp)
        for k,skillOpen in ipairs(userSkill) do

            local skillId = skillOpen.skill_id
            local skillLv = _M.skillMap[skillId] or 1

            _M.itemListener['skill'..skillId] = {}
            _M.itemListener['skill'..skillId].open_lv = skillOpen.open_lv

            local lvdb = GetSkillData(skillId, skillLv)
            _M.itemListener['skill'..skillId].listener = ItemModel.ListenCostXlsLine(lvdb, function()
                CheckSkillLvUp(skillId)
            end)

            CheckSkillLvUp(skillId)
        end
    end,false)
    
end

function _M.initial()
    DataMgr.Instance.UserData:AttachLuaObserver('SkillModel', _M)
    _M.skillMap = {}
    _M.RedCache = {}
    _M.lastSkillLvMax = GetSkillLvMax()
    
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
 
    DataMgr.Instance.UserData:DetachLuaObserver('SkillModel')
end
 
_M.GetSex = GetSex
_M.GetPro = GetPro
_M.GetLevel = GetLevel
_M.GetUserSkills = GetUserSkills
_M.GetSkillPos = GetSkillPos
_M.GetSkillData = GetSkillData
_M.GetSkillOpenData = GetSkillOpenData

_M.GetSkillLvMaxData = GetSkillLvMaxData
_M.GetSkillLvMax = GetSkillLvMax
_M.GetSkillLvMaxDataByLvMin = GetSkillLvMaxDataByLvMin

_M.ReqSkillList = ReqSkillList
_M.ReqUpgradeSkill = ReqUpgradeSkill
 

return _M
