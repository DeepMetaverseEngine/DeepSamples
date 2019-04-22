local _M = {}
_M.__index = _M

local Text = require 'Logic/Text'
local Helper = require 'Logic/Helper'

function _M.Format1234(str, ...)
    local args = {...}
    local str =
        string.gsub(
        str,
        '{(%d+)}',
        function(idx)
            return args[idx + 1]
        end
    )
    return str
end

function _M.FormatABCD(format, ...)
    if select('#', ...) == 0 then
        return format
    end
    local args = {...}
    -- A 65 a 97
    local str =
        string.gsub(
        format,
        '{([ABCDabcd])}',
        function(key)
            local code = string.byte(key) - 96
            if code < 0 then
                code = code + 32
            end
            return args[code]
        end
    )
    return str
end

function _M.ContainsTextKey(key)
    return LanguageManager.Instance:ContainsKey(key)
end

-- 多国语言
-- 格式化参数, 用{1}{2}..做为占位符
function _M.GetText(key, ...)
    local str = LanguageManager.Instance:GetString(key) or key
    if select('#', ...) > 0 then
        str = _M.Format1234(str, ...)
    end
    return str
end

function _M.GetLangNum(num)
    if num > #Constants.LangNum then
        errro('only support num <= ' .. num)
    end
    local t1 = math.floor(num / 10)
    local t2 = num % 10
    local ret = ''
    if t1 > 0 then
        ret = ret .. Constants.LangNum[t1]
        if Constants.LangNum[10] then
            ret = ret .. Constants.LangNum[10]
        end
    end

    if t2 > 0 then
        ret = ret .. Constants.LangNum[t2]
    elseif not Constants.LangNum[10] and Constants.LangNum[0] then
        ret = ret .. Constants.LangNum[0]
    end
    return ret
end

function _M.NumFormat(num, pos, sep)
    local num_str = tostring(num)
    local count = string.len(num_str)
    local ret = ''
    while count > pos do
        local str = string.sub(num_str, count - pos + 1, count)
        ret = sep .. str .. ret
        count = count - pos
    end
    local str = string.sub(num_str, 1, count)
    ret = str .. ret
    return ret
end

function _M.GetRounding(a)
    local r1, r2 = math.modf(a, 1)
    r2 = r2 >= 0.5 and 1 or 0
    return r1 + r2
end

function _M.str2Item(str)
    return loadstring('return ' .. str)()
end

function _M.str2Items(str, filterJob, filterGender)
    arr = string.split(str, ';', true)
    for i, v in ipairs(arr) do
        arr[i] = _M.str2Item(v)
    end
    return arr
end

local function FixSnap(snap)
    return snap
end

local function CLRRoleSnap2LuaRoleSnap(snap)
    if not snap then
        return
    end
    DataMgr.Instance.UserData:FixRoleSnap(snap)
    local ret = {}
    ret.ID = snap.ID
    ret.digitID = snap.digitID
    ret.Name = snap.Name
    ret.Gender = snap.Gender
    ret.Level = snap.Level
    ret.Pro = snap.Pro
    ret.FightPower = snap.FightPower
    ret.GuildId = snap.GuildId
    ret.GuildName = snap.GuildName
    ret.VipLevel = snap.vip_level
    ret.PracticeLv = snap.PracticeLv
    ret.OnlineState = snap.OnlineState
    ret.LastOfflineTime = snap.LastOfflineTime
    ret.ExpiredUtc = snap.ExpiredUtc
    ret.TitleID = snap.TitleID
    ret.CPTowerLayer = snap.CPTowerLayer
    ret.Avatar = {}
    for i = 0, snap.Avatar.Count - 1 do
        local m = snap.Avatar:getItem(i)
        table.insert(ret.Avatar, {PartTag = m.PartTag, FileName = m.FileName})
    end
    
    if snap.Options then
        ret.Options = CSharpMap2Table(snap.Options)
    end
    
    return FixSnap(ret)
end

function _M.GetRoleSnap(id, cb)
    DataMgr.Instance.UserData.RoleSnapReader:Get(
        id,
        function(snap)
            cb(CLRRoleSnap2LuaRoleSnap(snap))
        end
    )
end

function _M.GetCachedRoleSnap(id)
    local snap = DataMgr.Instance.UserData.RoleSnapReader:GetCache(id)
    return CLRRoleSnap2LuaRoleSnap(snap)
end

function _M.LoadRoleSnap(id)
    DataMgr.Instance.UserData.RoleSnapReader:Load(
        id,
        function(snap)
            cb(CLRRoleSnap2LuaRoleSnap(snap))
        end
    )
end

function _M.GetManyRoleSnap(ids, cb)
    DataMgr.Instance.UserData.RoleSnapReader:GetMany(
        ids,
        function(snaps)
            local ret = {}
            snaps = CSharpArray2Table(snaps)
            for _, m in ipairs(snaps) do
                table.insert(ret, CLRRoleSnap2LuaRoleSnap(m))
            end
            cb(ret)
        end
    )
end

function _M.SetRoleSnapDirty(idorIds)
    DataMgr.Instance.UserData.RoleSnapReader:SetDirty(idorIds)
end

function _M.GetShortText(text, num)
    local n = string.utf8len(text)
    if n <= num then
        return text
    else
        return string.utf8sub(text, 1, num) .. '...'
    end
end
-- local match = {
--     ['**'] = true,
-- }
local allwords
function GetBlackWords()
    if allwords == nil then
        allwords = {}
        local datas = GlobalHooks.DB.GetFullTable('blackword')
        for k, v in pairs(datas) do
            local text = v.blackword
            text = string.gsub(text, '%%', '%%%%')
            text = string.gsub(text, '%^', '%%^')
            text = string.gsub(text, '%$', '%%$')
            text = string.gsub(text, '%(', '%%(')
            text = string.gsub(text, '%)', '%%)')

            text = string.gsub(text, '%.', '%%.')
            text = string.gsub(text, '%[', '%%[')
            text = string.gsub(text, '%]', '%%]')
            text = string.gsub(text, '%*', '%%*')
            text = string.gsub(text, '%+', '%%+')
            text = string.gsub(text, '%-', '%%-')
            text = string.gsub(text, '%?', '%%?')
            table.insert(allwords, text)
        end
        table.sort(
            allwords,
            function(x, y)
                return string.len(x) > string.len(y)
            end
        )
    -- for i=1,100 do
    --     local msg = ''
    --     for j=0,i do
    --         msg = msg..'*'
    --     end
    --     match[msg] = true
    -- end
    end
    return allwords
end

function _M.FilterBlackWord(msg)
    local allwords = GetBlackWords()
    for k, blackword in ipairs(allwords) do
        msg = string.gsub(msg, blackword, '**')
        -- if match[msg] then
        --     print('11111111111111111111111111111111')
        --     break
        -- end
    end
    return msg
end

function _M.GetActorAvartarTable()
    return EventApi.GetActorAvartarMap()
end

local Layer_UI = 5
function _M.CreateTransformSet(t)
    local info = TransformSet()
    if t.Scale then
        if type(t.Scale) == 'number' then
            info.Scale = Vector3(t.Scale, t.Scale, t.Scale)
        else
            info.Scale = Vector3(t.Scale.x or 1, t.Scale.y or 1, t.Scale.z or 1)
        end
    end
    if t.Pos then
        info.Pos = Vector3(t.Pos.x or 0, t.Pos.y or 0, t.Pos.z or 0)
    end
    if t.Deg then
        info.Deg = Vector3(t.Deg.x or 0, t.Deg.y or 0, t.Deg.z or 0)
    end
    if t.Vectormove then
        info.Vectormove = Vector2(t.Vectormove.x, t.Vectormove.y)
    end

    if t.AnimatorState then
        info.AnimatorState = t.AnimatorState
    end

    if t.DisableToUnload then
        info.DisableToUnload = true
    end
    if t.Parent then
        info.Parent = t.Parent
    end
    if t.UILayer then
        info.Layer = Layer_UI
    else
        info.Layer = t.Layer or -1
    end
    if info.Layer > 0 and (not t.LayerOrder or t.LayerOrder == 0) then
        t.LayerOrder = 1500
    end
    if t.LayerOrder then
        info.LayerOrder = t.LayerOrder
    end
    if t.Visible ~= nil then
        info.Visible = t.Visible
    end
    if t.Clip then
        info.Clip = t.Clip
    end
    return info
end

function _M.LoadGameUnit(avatar_or_resname, transformset, cb, failcb)
    local info = _M.CreateTransformSet(transformset)
    local function load_success(aoe)
        if cb then
            cb(aoe)
        end
    end
    local function load_fail()
        if failcb then
            failcb()
        end
    end
    return RenderSystem.Instance:LoadGameUnit(avatar_or_resname, info, load_success, load_fail)
end

-- 特效播放
-- Util.PlayEffect(filename,tableset,[,duration][,cb][,loop_play])
-- 示例：
-- local t = {
--     LayerOrder = self.menu.MenuOrder,
--     UILayer = true,          --和 Layer = Constants.Layer.UI相同效果
--     DisableToUnload = true,  --显示状态未Disable时自动Unload
--     Parent = self.comps.btn_upgrade.Transform,
--     Pos = {x = 66, y = -25}
-- }
-- 普通播放，根据特效渲染自动Unload
-- Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t)
-- 播放完毕打印aaaa
-- Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t, function() print('aaaa') end)
-- 指定播放30秒，Unload
-- Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t, 30)
-- 一直播放
-- Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t, true)
-- 指定播放30秒，Unload，执行完毕后打印aaaa
-- Util.PlayEffect('/res/effect/ui/ef_ui_frame_01.assetbundles', t, 30, function() print('aaaa') end)
function _M.PlayEffect(filename, transformset, duration, cb)
    local info = _M.CreateTransformSet(transformset)
    local t = type(duration)
    if t == 'function' then
        cb = duration
        duration = 0
    elseif t == 'boolean' then
        cb = nil
        duration = 9999999
    end
    duration = duration or 0
    if cb then
        return RenderSystem.Instance:PlayEffect(filename, info, duration, cb)
    else
        return RenderSystem.Instance:PlayEffect(filename, info, duration)
    end
end

function _M.FixUIEffectRes(filename)
    return '/res/effect/ui/' .. filename .. '.assetbundles'
end

return _M
