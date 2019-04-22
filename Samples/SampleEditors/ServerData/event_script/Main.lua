--! @addtogroup Common
--! @{
----------------------------------- global---------------------------
local PRINT_STACK = false
local ERROR_STACK = true
local WRAN_STACK = true
local USE_CACHE = false
local SANDBOX_API = true
local CurrentManager
unpack = unpack or table.unpack
pack = pack or table.pack

local function string_split(str, sep, maxcount)
    if not str then
        return
    end
    local sep, fields = sep or ':', {}
    local pattern = string.format('([^%s]+)', sep)
    local other_fields = {}
    str:gsub(
        pattern,
        function(c)
            if maxcount and #fields >= maxcount then
                other_fields[#other_fields + 1] = c
            else
                fields[#fields + 1] = c
            end
        end
    )
    if #other_fields > 0 then
        fields[#fields + 1] = table.concat(other_fields, seq)
    end
    return fields
end

local function string_starts(String, Start)
    return string.sub(String, 1, string.len(Start)) == Start
end

local function string_ends(String, End)
    return End == '' or string.sub(String, -string.len(End)) == End
end

local function string_IsNullOrEmpty(str)
    return not str or str == ''
end

local function table_IsArray(t)
    if not t or type(t) ~= 'table' then
        return false
    end
    local i = 0
    for _ in pairs(t) do
        i = i + 1
        if t[i] == nil then
            return false
        end
    end
    return true
end

local function table_MaxIndex(t)
    local ret = 0
    for i, v in pairs(t) do
        ret = math.max(i, ret)
    end
    return ret
end

local function table_len(t)
    local ret = 0
    for i, v in pairs(t or {}) do
        ret = ret + 1
    end
    return ret
end

local function table_Keys(t)
    if not t then
        return
    end
    local ret = {}
    for k, v in pairs(t) do
        table.insert(ret, k)
    end
    return ret
end

local function table_Values(t)
    if not t then
        return
    end
    local ret = {}
    for k, v in pairs(t) do
        table.insert(ret, v)
    end
    return ret
end

local function table_ContainsKey(t, key)
    return t[key] ~= nil
end

local function table_ContainsValue(t, value)
    for k, v in pairs(t) do
        if v == value then
            return true
        end
    end
    return false
end

-- print('----------------GameEventManager-Main.Lua-----------------', _VERSION)
local function TableToString(root)
    if not root then
        return 'nil'
    end
    local cache = {[root] = '.'}
    local function _dump(t, space, name)
        local temp = {}
        for k, v in pairs(t) do
            local key = tostring(k)
            if cache[v] then
                table.insert(temp, '+' .. key .. ' {' .. cache[v] .. '}')
            elseif type(v) == 'table' then
                local new_key = name .. '.' .. key
                cache[v] = new_key
                table.insert(temp, '+' .. key .. _dump(v, space .. (next(t, k) and '|' or ' ') .. string.rep(' ', #key), new_key))
            else
                table.insert(temp, '+' .. key .. ' [' .. tostring(v) .. ']')
            end
        end
        return table.concat(temp, '\n' .. space)
    end

    return '\n' .. _dump(root, '', '')
end

local function get_print_string(deep, ...)
    local p = {...}
    local ret = ''
    for k, v in ipairs(p) do
        local t = type(v)
        if t == 'table' then
            ret = deep and ret .. TableToString(v) or ret .. tostring(v)
        else
            ret = ret .. tostring(v) .. '\t'
        end
    end
    return ret
end

local function dumpstack()
    if _VERSION == 'MoonSharp 2.0.0.0' then
        return ''
    end
    function vars(f)
        local dump = ''
        local func = debug.getinfo(f, 'f').func
        local i = 1
        local locals = {}
        -- get locals
        while true do
            local name, value = debug.getlocal(f, i)
            if not name then
                break
            end
            if string.sub(name, 1, 1) ~= '(' then
                dump = dump .. '    ' .. name .. '=' .. tostring(value) .. '\n'
            end
            i = i + 1
        end
        -- get varargs (these use negative indices)
        i = 1
        while true do
            local name, value = debug.getlocal(f, -i)
            -- `not name` should be enough, but LuaJIT 2.0.0 incorrectly reports `(*temporary)` names here
            if not name or name ~= '(*vararg)' then
                break
            end
            dump = dump .. '    ' .. name .. '=' .. tostring(value) .. '\n'
            i = i + 1
        end
        -- get upvalues
        i = 1
        while func do -- check for func as it may be nil for tail calls
            local name, value = debug.getupvalue(func, i)
            if not name then
                break
            end
            dump = dump .. '    ' .. name .. '=' .. tostring(value) .. '\n'
            i = i + 1
        end
        return dump
    end

    local dump = ''
    for i = 3, 100 do
        local source = debug.getinfo(i, 'S')
        if not source then
            break
        end
        dump = dump .. '- stack' .. tostring(i - 2) .. '\n'
        dump = dump .. vars(i + 1)
        if source.what == 'main' then
            break
        end
    end
    return dump
end

local inner_print = print
local function print(...)
    local ret = get_print_string(false, ...)
    if CurrentManager then
        CurrentManager:Log(ret)
    else
        inner_print(ret)
    end
    if PRINT_STACK then
        CurrentManager:Log(debug.traceback() .. dumpstack())
    end
end

local function pprint(...)
    local ret = get_print_string(true, ...)
    print(ret)
end

local inner_error = error
local function error(msg, eventid)
    msg = string.gsub(msg, '\r', '')
    if eventid then
        msg = '(' .. eventid .. ')' .. msg
    end
    if ERROR_STACK then
        msg = msg .. '\n' .. debug.traceback() .. dumpstack()
    end
    if CurrentManager then
        CurrentManager:LogError(msg)
        if eventid then
            CurrentManager:StopEvent(eventid, false, string.format('event(%d) exception', eventid))
        end
    else
        inner_error(ret)
    end
end

local function warn(msg)
    if WRAN_STACK then
        msg = msg .. '\n' .. debug.traceback()
    end
    if CurrentManager then
        CurrentManager:LogWarn(msg)
    else
        print(msg)
    end
end

local function traceback(msg)
    msg = msg or ''
    if CurrentManager then
        CurrentManager:LogStackTrace(msg .. '\n' .. debug.traceback())
    else
        print(msg .. '\n' .. debug.traceback())
    end
end

local function fold_t(func, z, t)
    for name, val in pairs(t) do
        z = func(z, {key = name, val = val})
    end
    return z
end

local function is_empty(t)
    local break_flag = false
    for name, val in pairs(t) do
        break_flag = true
        break
    end
    return not break_flag
end

local function merge_table(tTarget, tOrigin, isEmptyClean)
    assert(tTarget, 'merge_table target cant be nil')
    local func = function(z, item)
        if type(item.val) == 'table' then
            if not z[item.key] then
                z[item.key] = {}
            end
            if isEmptyClean and is_empty(item.val) then
                --空表代表清除数值
                z[item.key] = {}
            else
                merge_table(z[item.key], item.val, isEmptyClean)
            end
        else
            z[item.key] = item.val
        end
        return z
    end
    fold_t(func, tTarget, tOrigin)
    return tTarget
end

local function copy_table(org)
    local rtn = {}
    local func = function(z, i)
        if type(i.val) == 'table' then
            z[i.key] = copy_table(i.val)
        else
            z[i.key] = i.val
        end
        return z
    end
    fold_t(func, rtn, org)
    return rtn
end

---------------------------------------- Sanbox----------------------------------
local Sanbox = {cache_sanbox = {}}
function Sanbox.getSandbox()
    local math = math
    local os = os
    local coroutine = coroutine
    local string = string
    local table = table
    local io = io
    local env = {}
    env.assert = assert
    env.ipairs = ipairs
    env.next = next
    env.pairs = pairs
    env.pcall = pcall
    env.print = print
    env.select = select
    env.tonumber = tonumber
    env.tostring = tostring
    env.type = type
    env.unpack = unpack
    env._VERSION = _VERSION
    env.xpcall = xpcall

    env.coroutine = {}
    env.coroutine.create = coroutine.create
    env.coroutine.resume = coroutine.resume
    env.coroutine.running = coroutine.running
    env.coroutine.status = coroutine.status
    env.coroutine.wrap = coroutine.wrap
    env.coroutine.yield = coroutine.yield

    env.string = {}
    env.string.byte = string.byte
    env.string.char = string.char
    env.string.find = string.find
    env.string.format = string.format
    env.string.gmatch = string.gmatch
    env.string.gsub = string.gsub
    env.string.len = string.len
    env.string.lower = string.lower
    env.string.match = string.match
    env.string.rep = string.rep
    env.string.reverse = string.reverse
    env.string.sub = string.sub
    env.string.upper = string.upper
    env.string.split = string_split
    env.string.IsNullOrEmpty = string_IsNullOrEmpty
    env.string.starts = string_starts
    env.string.ends = string_ends

    env.table = {}
    env.table.insert = table.insert
    env.table.maxn = table.maxn
    env.table.remove = table.remove
    env.table.sort = table.sort
    env.table.concat = table.concat
    env.table.len = table_len
    env.table.keys = table_Keys
    env.table.values = table_Values
    env.table.MaxIndex = table_MaxIndex
    env.table.ContainsKey = table_ContainsKey
    env.table.ContainsValue = table_ContainsValue
    env.math = {}
    env.math.abs = math.abs
    env.math.acos = math.acos
    env.math.asin = math.asin
    env.math.atan = math.atan
    env.math.atan2 = math.atan2
    env.math.cos = math.cos
    env.math.cosh = math.cosh
    env.math.deg = math.deg
    env.math.exp = math.exp
    env.math.floor = function(d)
        return math.floor(d + 0.000001)
    end
    env.math.ceil = function(d)
        return math.ceil(d - 0.000001)
    end
    env.math.fmod = math.fmod
    env.math.frexp = math.frexp
    env.math.huge = math.huge
    env.math.ldexp = math.ldexp
    env.math.log = math.log
    env.math.log10 = math.log10
    env.math.max = math.max
    env.math.min = math.min
    env.math.modf = math.modf
    env.math.pi = math.pi
    env.math.pow = math.pow
    env.math.rad = math.rad
    env.math.random = math.random
    env.math.randomseed = math.randomseed
    env.math.sin = math.sin
    env.math.sinh = math.sinh
    env.math.sqrt = math.sqrt
    env.math.tan = math.tan
    env.math.tanh = math.tanh

    env.io = {}
    env.io.read = io.read
    env.io.popen = io.popen
    env.io.write = io.write
    env.io.flush = io.flush
    env.io.type = io.type
    env.io.open = io.open

    env.os = {}
    env.os.clock = os.clock
    env.os.difftime = os.difftime
    env.os.time = os.time
    env.os.date = os.date
    env.os.execute = os.execute

    env.debug = debug
    return env
end

local functionScript = [[
    function main(fn,...)
        return fn(...)
    end
]]

function Sanbox.loadStringTrunkAndGetEnv(strtrunk, envlist, fullenv)
    local tSandbox = Sanbox.getSandbox()
    local trunkfn, errmsg = load(strtrunk, 'chunk', 'bt', tSandbox)
    if not trunkfn then
        inner_error(errmsg)
    end
    local fUntrusted, sMessage = load(functionScript, 'chunk', 'bt', tSandbox)
    if not fUntrusted then
        inner_error(sMessage)
    end
    local function process()
        if type(fUntrusted) ~= 'function' then
            return false, fUntrusted
        end
        local _ENV = tSandbox
        if setfenv then
            setfenv(fUntrusted, tSandbox)
        end
        return pcall(fUntrusted)
    end

    local ok, res = process()
    if not ok then
        inner_error(res)
    end
    if fullenv then
        for k, v in pairs(_G) do
            tSandbox[k] = v
        end
    end
    for _, otherEnv in ipairs(envlist or {}) do
        for k, v in pairs(otherEnv) do
            tSandbox[k] = v
        end
    end
    tSandbox.main = trunkfn
    return tSandbox
end

function Sanbox.loadFunctionAndGetEnv(fn, envlist, fullenv)
    local tSandbox = Sanbox.getSandbox()
    local fUntrusted, sMessage = load(functionScript, 'chunk', 'bt', tSandbox)
    if not fUntrusted then
        inner_error(sMessage)
    end
    local function process()
        if type(fUntrusted) ~= 'function' then
            return false, fUntrusted
        end
        local _ENV = tSandbox
        if setfenv then
            setfenv(fUntrusted, tSandbox)
        end
        return pcall(fUntrusted)
    end

    local ok, res = process()
    if not ok then
        inner_error(res)
    end
    if fullenv then
        for k, v in pairs(_G) do
            tSandbox[k] = v
        end
    end
    for _, otherEnv in ipairs(envlist or {}) do
        for k, v in pairs(otherEnv) do
            tSandbox[k] = v
        end
    end
    tSandbox.main = fn
    return tSandbox
end

function Sanbox.loadAndGetEnv(sFileName, envlist, safecall, fullenv)
    local tSandbox = Sanbox.getSandbox()
    local fUntrusted, sMessage
    if loadfile_env then
        fUntrusted, sMessage = loadfile_env(sFileName .. '.lua', tSandbox)
    else
        fUntrusted, sMessage = loadfile(sFileName .. '.lua', 'bt', tSandbox)
    end
    if not fUntrusted then
        if safecall then
            print('loadAndGetEnv safe call error: ' .. sFileName)
            return
        end
        inner_error(sMessage)
    end

    local function process()
        if type(fUntrusted) ~= 'function' then
            return false, fUntrusted
        end
        local _ENV = tSandbox
        if setfenv then
            setfenv(fUntrusted, tSandbox)
        end
        return pcall(fUntrusted)
    end

    local ok, res = process()
    if not ok then
        if safecall then
            print('loadAndGetEnv safe call error: ' .. sFileName)
            return
        end
        inner_error(res)
    end
    if fullenv then
        for k, v in pairs(_G) do
            tSandbox[k] = v
        end
    end
    for _, otherEnv in ipairs(envlist or {}) do
        for k, v in pairs(otherEnv) do
            tSandbox[k] = v
        end
    end

    return tSandbox
end

function Sanbox.ClearCache()
    Sanbox.cache_sanbox = {}
end

function Sanbox.RemoveCache(scriptName)
    Sanbox.cache_sanbox = Sanbox.cache_sanbox or {}
    Sanbox.cache_sanbox[scriptName] = nil
end

------------------------------------------------------------------------------
----------------------------- BlackBoard---------------------------
local BlackBoard = {}
BlackBoard.__index = BlackBoard

function BlackBoard.Create()
    return setmetatable({NEXT_ID = 1, data = {}}, BlackBoard)
end

-- id有值时，按传入id作为key，id不存在时，使用内部id
function BlackBoard.Add(self, id, obj)
    if not obj then
        obj = id
        id = self.NEXT_ID
        self.NEXT_ID = self.NEXT_ID + 1
    else
        if type(id) == 'number' and not self[id] and self.NEXT_ID < id then
            self.NEXT_ID = id + 1
        end
    end
    if type(obj) ~= 'table' then
        error('type error')
    end
    self.data[id] = obj
    return id
end

function BlackBoard.Get(self, id)
    return self.data[id]
end

function BlackBoard.Remove(self, id)
    self.data[id] = nil
end

function BlackBoard.Find(self, find_iter)
    local function MatchInTable(a, b)
        for k, v in pairs(b or {}) do
            if type(v) == 'table' then
                if not MatchInTable(a[k], v) then
                    return false
                end
            elseif a[k] ~= v then
                return false
            end
        end
        return true
    end

    local str_type = type(find_iter)
    local ret = {}
    if str_type == 'table' then
        for k, v in pairs(self.data) do
            if MatchInTable(v, find_iter) then
                table.insert(ret, v)
            end
        end
    elseif str_type == 'function' then
        for k, v in pairs(self.data) do
            if find_iter(v) then
                table.insert(ret, v)
            end
        end
    end
end

function BlackBoard.GetAll(self)
    return self.data
end

local function DynamicToArgTable(...)
    local t = {len = select('#', ...), ...}
    return t
end

local function ArgTableToDynamic(arg, index)
    if not arg then
        return
    end
    if index and index > arg.len then
        return
    end
    return unpack(arg, index or 1, arg.len)
end

local function RepackArgTabel(arg, index)
    return DynamicToArgTable(ArgTableToDynamic(arg, index))
end

---------------------------------------- LuaEvent----------------------------------
local LuaEvent = {}
LuaEvent.__index = LuaEvent

function LuaEvent.Resume(self, ...)
    if coroutine.status(self.co) == 'suspended' then
        local ok, msg = coroutine.resume(self.co, ...)
        if not ok then
            error((self.ScriptDesc or 'unknown') .. ' ' .. (msg or ''), self.ID)
        end
    else
        error('Resume error ' .. coroutine.status(self.co), self.ID)
    end
end

function LuaEvent.Start(self)
    local ok, msg = coroutine.resume(self.co, self, ArgTableToDynamic(self._params))
    if not ok then
        error((self.ScriptDesc or 'unknown') .. ' ' .. msg, self.ID)
    end
end

function LuaEvent.Stop(self, success)
    if self.callbacks and self.callbacks.stop then
        self.callbacks.stop(self)
    end
end

function LuaEvent.BeforeStop(self)
    if self.script.clean then
        self.script.clean(self.IsSuccessed, self.ResultReason, ArgTableToDynamic(self._params))
    end

    if self._custom_clean then
        self._custom_clean(self.IsSuccessed, self.ResultReason, ArgTableToDynamic(self._params))
    end
    self.IsStoped = true
end

local function event_co_main(obj, ...)
    local sanbox = obj.script
    local callbacks = obj.callbacks
    sanbox.ID = obj.ID
    if callbacks.before then
        callbacks.before(obj)
    end
    local ret = DynamicToArgTable(xpcall(sanbox.main, obj._errorfn, ...))
    local ok, isSuccess = ret[1], ret[2]
    local reason
    if not ok then
        isSuccess = false
        reason = ret[2]
    elseif isSuccess == nil then
        isSuccess = true
    elseif not isSuccess then
        reason = ret[3]
    end

    if obj.IsStoped then
        return
    end
    -- pprint('ret -------',ret)
    local params
    if isSuccess and callbacks.after then
        params = DynamicToArgTable(ArgTableToDynamic(ret, 3))
    end
    if callbacks.after then
        callbacks.after(obj, isSuccess, reason, params)
    end
end

function LuaEvent.Create(sanbox, callbacks, cleanfn, ...)
    local obj = setmetatable({}, LuaEvent)
    obj.callbacks = callbacks
    obj._custom_clean = cleanfn
    obj._params = DynamicToArgTable(...)
    assert(sanbox.main)
    obj.ScriptDesc = sanbox.ScriptDesc
    obj._errorfn = function(err)
        error(err, obj.ID)
    end
    obj.co = coroutine.create(event_co_main)
    obj.script = sanbox
    return obj
end

------------------------------------------------------------------------------
_total_global = {
    _ids = {},
    blackBoard = BlackBoard.Create(),
    -- root events blackboard
    bbs = {}
}

local BaseApi = {Task = {}, Listen = {}}

local function GetConfig(name)
    local cfg = _total_global.config.Managers[name]
    if not cfg then
        cfg = {}
        _total_global.config.Managers[name] = cfg
    end
    return cfg
end

-----------------------------------------------------------------------------
local function AppendEventApi(sanbox, api)
    api = api or EventApi
    sanbox.Api = api
    sanbox.warn = api.warn
    sanbox.print = api.print
    sanbox.pprint = api.pprint
    sanbox.log = api.print
    sanbox.traceback = api.traceback
    sanbox.table.copy = api.copy_table
    sanbox.error = inner_error
end

local function IsLocolManager(managerName, uuid)
    local isLocal = not managerName or (managerName == CurrentManager.Name and (not uuid or uuid == CurrentManager.UUID))
    return isLocal
end

local function GetScriptPath(script_name, root)
    return CurrentManager.RootPath .. root .. script_name
end

local EventCallBack = {}
function EventCallBack.before(obj)
    _total_global._ids = _total_global._ids or {}
    _total_global._ids[obj.ID] = obj.ScriptDesc
end

function EventCallBack.after(obj, success, reason, output)
    if success == nil then
        success = true
    end
    -- print('after ------',success, reason, output)
    if not success then
        CurrentManager:StopEvent(obj.ID, success, reason)
    else
        CurrentManager:SetEventOutput(obj.ID, output)
        CurrentManager:StopEvent(obj.ID, true, 'main end')
    end
end

function EventCallBack.stop(obj, success)
    _total_global._ids[obj.ID] = nil
end

local function CreateLuaEvent(script_table, sanboxEnv, ...)
    local envlist = {sanboxEnv}
    local ok, ret_sanbox
    local safecall = script_table.IsTable and script_table.Desc.safecall or false
    if script_table.IsScript then
        local scriptName = script_table.IsTable and script_table.Desc.main or script_table.Desc
        if USE_CACHE and Sanbox.cache_sanbox[scriptName] then
            ok = true
            ret_sanbox = Sanbox.cache_sanbox[scriptName]
        else
            local path = GetScriptPath(scriptName, _total_global.config.ScriptRootPath)
            if script_table.Desc.env then
                table.insert(envlist, script_table.Desc.env)
            end
            ok, ret_sanbox = pcall(Sanbox.loadAndGetEnv, path, envlist, safecall, script_table.FullEnv)
            if ok and ret_sanbox then
                ret_sanbox.ScriptDesc = scriptName
                if USE_CACHE then
                    Sanbox.cache_sanbox[scriptName] = ret_sanbox
                end
            end
        end
    elseif script_table.IsFunction then
        local fn = script_table.IsTable and script_table.Desc.main or script_table.Desc
        ok, ret_sanbox = pcall(Sanbox.loadFunctionAndGetEnv, fn, envlist, script_table.FullEnv)
    elseif script_table.IsTrunk then
        ok, ret_sanbox = pcall(Sanbox.loadStringTrunkAndGetEnv, script_table.Desc, envlist, script_table.FullEnv)
    end

    if ok and ret_sanbox then
        ret_sanbox.ScriptDesc = script_table.IsTable and script_table.Desc.desc or ret_sanbox.ScriptDesc
        AppendEventApi(ret_sanbox)
        local cleanfn = script_table.IsTable and script_table.Desc.clean
        local e = LuaEvent.Create(ret_sanbox, EventCallBack, cleanfn, ...)
        return e
    else
        if not safecall then
            error(ret_sanbox)
        else
            --print(ret_sanbox)
        end
    end
end

local readonly_mt = {
    __index = t,
    __newindex = function(t, k, v)
        error('attempt to update a read-only table ')
    end
}

local function SplitParamsAndCallBack(...)
    local paramsLen = select('#', ...)
    if paramsLen == 0 then
        return DynamicToArgTable()
    end
    local cb = select(paramsLen, ...)
    if type(cb) == 'function' then
        local src_params = {...}
        if paramsLen > 1 then
            return DynamicToArgTable(unpack(src_params, 1, paramsLen - 1)), cb
        else
            return DynamicToArgTable(), cb
        end
    else
        return DynamicToArgTable(...)
    end
end

if not setfenv then
    function debug.setfenv(fn, env)
        local i = 1
        local debug = debug
        while true do
            local name = debug.getupvalue(fn, i)
            if name == '_ENV' then
                debug.upvaluejoin(
                    fn,
                    i,
                    (function()
                        return env
                    end),
                    1
                )
                break
            elseif not name then
                break
            end

            i = i + 1
        end
        return fn
    end
end

local function CreateSandboxApi(fn, env)
    if not SANDBOX_API or not env then
        return fn
    else
        local sfn = setfenv or debug.setfenv
        sfn(fn, env)
        return fn
    end
end

local sandbox_global
local function CreateSandboxMetatable(api)
    local ret = {}
    ret.__index = function(self, key)
        if not sandbox_global then
            sandbox_global = Sanbox.getSandbox()
            AppendEventApi(sandbox_global, api)
        end
        return sandbox_global[key] or _G[key]
    end
    return ret
end

local function CreateLocalApi(managerName, uuid)
    local function AppendApiTo(src, target, env)
        -- parent = parent or {}
        for k, v in pairs(src or {}) do
            local t = type(v)
            if t == 'table' then
                -- table.remove(parent)
                target[k] = target[k] or {}
                -- table.insert(parent, k)
                AppendApiTo(v, target[k], env)
            elseif t == 'function' then
                target[k] = CreateSandboxApi(v, env)
            else
                target[k] = v
            end
        end
    end

    local config = GetConfig(managerName)
    local ret = {}
    AppendApiTo(BaseApi, ret)
    local apiList = config.ApiList
    local initfns
    local endfns
    local func_env = setmetatable({}, CreateSandboxMetatable(ret))
    func_env.Api = ret
    for _, v in ipairs(apiList or {}) do
        local path = CurrentManager.RootPath .. v
        package.loaded[path] = nil
        -- print('require api :' .. v)
        local t = require(path)
        if t.OnStart then
            initfns = initfns or {}
            table.insert(initfns, t.OnStart)
        end
        if t.OnStop then
            endfns = endfns or {}
            table.insert(endfns, t.OnStop)
        end
        AppendApiTo(t, ret, func_env)
    end
    config.OnStops = endfns
    config.OnStarts = initfns
    -- pprint('api list', ret)
    ret.RootPath = CurrentManager.RootPath .. _total_global.config.ScriptRootPath
    ret.pprint = pprint
    ret.log = print
    ret.print = print
    ret.warn = warn
    ret.traceback = traceback
    ret.string_IsNullOrEmpty = string_IsNullOrEmpty
    ret.table_keys = table_Keys
    ret.string_split = string_split
    ret.string_ends = string_ends
    ret.TableToString = TableToString
    ret.copy_table = copy_table
    ret.merge_table = merge_table
    ret.table_MaxIndex = table_MaxIndex
    ret.ManagerName = managerName
    ret.UUID = uuid
    ret.Address = CurrentManager.Address
    -- if ret.Task then
    --     setmetatable(ret.Task, readonly_mt)
    -- end
    -- setmetatable(ret, readonly_mt)
    return ret
end

local _RemoteApi = {}
_RemoteApi.__index = function(t, k)
    local ret = rawget(t, k)
    if k == 'NoAutoWait' or string_starts(k, '_') then
        return ret
    end
    if not ret then
        ret = setmetatable({}, _RemoteApi)
        local lastName = t._fullname
        ret._name = k
        if not lastName then
            ret._fullname = k
        else
            ret._fullname = lastName .. '.' .. k
        end
        ret._parentName = t._name
        -- pprint('ret names ', ret)
        ret.info = t.info
        t[k] = ret
    end
    return ret
end

_RemoteApi.__call = function(t, ...)
    local parents = {}
    local nextInfo = {
        ManagerName = t.info.ManagerName,
        UUID = t.info.UUID,
        Rpc = t._fullname,
        Broadcast = t.info.Broadcast,
        Config = t.info.Config
    }
    local params
    local apiType = t._parentName
    if apiType == 'Listen' then
        local cb
        params, cb = SplitParamsAndCallBack(...)
        if type(cb) == 'function' then
            nextInfo.CallBack = cb
        end
    elseif apiType == 'Task' and (string_starts(t._name, 'StartEvent') or string_starts(t._name, 'StartChunk')) then
        nextInfo.IsStartEvent = true
    end
    if not params then
        params = DynamicToArgTable(...)
    end
    if string_starts(t._name, 'AddEventTo') then
        nextInfo.ParentEvent = params[1]
        nextInfo.Arg = DynamicToArgTable(ArgTableToDynamic(params, 2))
        nextInfo.Rpc = string.gsub(nextInfo.Rpc, 'AddEventTo', 'AddEvent')
    else
        nextInfo.Arg = params
    end

    if t.NoAutoWait then
        nextInfo.IsStartEvent = true
    end
    local eid = CurrentManager:CallSharpApi(nextInfo)
    assert(eid ~= 0)
    -- print('call', t._name, apiType)
    if not t.NoAutoWait and apiType ~= 'Task' and apiType ~= 'Listen' then
        local argTable = DynamicToArgTable(EventApi.Task.Wait(eid))
        assert(argTable[1], argTable[2])
        return ArgTableToDynamic(argTable, 2)
    else
        return eid
    end
end

local function CreateRemoteApi(managerName, uuid)
    local remoteApi = setmetatable({}, _RemoteApi)
    remoteApi.info = {ManagerName = managerName, UUID = uuid, Address = DeepCore.GameEvent.EventManager.GetAddress(managerName, uuid)}
    return remoteApi
end

local function CreateBroadcastApi(managerName, uuids_servergroup)
    local remoteApi = setmetatable({}, _RemoteApi)
    local config = {}
    local t = type(uuids_servergroup)
    if t == 'string' then
        config.Type = 'ServerGroup'
        config.Param = uuids_servergroup
    elseif t == 'table' then
        config.Param = uuids_servergroup
    end
    pprint('config --',uuids_servergroup,config)
    remoteApi.info = {ManagerName = managerName, Broadcast = true, Config = config}
    return remoteApi
end

local function CreateManagerApi(managerName, uuid)
    local isLocal = IsLocolManager(managerName, uuid)
    if isLocal then
        return EventApi or CreateLocalApi(managerName, uuid)
    else
        return CreateRemoteApi(managerName, uuid)
    end
end

------------------------------ base api----------------------------
local parent_once

local function SetEnvParent(pid)
    parent_once = pid
end

local function GetEnvParent()
    local pid = parent_once
    parent_once = nil
    return pid
end

local function CreateEventTable(scriptName, ...)
    local t = type(scriptName)
    local params = {}
    local ret
    if t == 'string' then
        params.IsScript = true
    elseif t == 'function' then
        params.IsScript = false
    elseif t == 'table' then
        params.IsTable = true
        params.IsScript = type(scriptName.main) == 'string'
    else
        error(string.format('argument error type:%s arg:%s', t, tostring(scriptName)))
    end
    params.Desc = scriptName
    params.IsFunction = not params.IsScript
    return CreateLuaEvent(params, _total_global.config.SanboxAppendEnv, ...)
end

local function CreateTrunkEventTable(trunk)
    local params = {}
    params.Desc = trunk
    params.IsTrunk = true
    return CreateLuaEvent(params, _total_global.config.SanboxAppendEnv)
end

function BaseApi.Memory()
    return collectgarbage('count')
end

function BaseApi.GC()
    collectgarbage('collect')
end

function BaseApi.Task.FromResult(ok, reason, ...)
    local id = EventApi.Task.CreateWaitAlways()
    if ok then
        EventApi.SetEventOutput(id, reason, ...)
        EventApi.Task.StopEvent(id)
    else
        EventApi.Task.StopEvent(id, false, reason)
    end
    return id
end

function BaseApi.Task.DelaySec(sec)
    return EventApi.DoSharpApi('Async', 'DeepCore.GameEvent.Events.DelaySecEvent', sec)
end

function BaseApi.Listen.Message(...)
    return EventApi.DoSharpApi('Listen', 'DeepCore.GameEvent.Events.NamedMessageEvent', ...)
end

function BaseApi.SendMessage(...)
    return EventApi.DoSharpApi('Listen', 'DeepCore.GameEvent.Events.SendNamedMessageEvent', ...)
end

function BaseApi.Listen.AddPeriodicSec(...)
    return EventApi.DoSharpApi('Listen', 'DeepCore.GameEvent.Events.PeriodicSecEvent', ...)
end

function BaseApi.StopParent()
    return EventApi.DoSharpApi('Sync', 'DeepCore.GameEvent.Events.StopParentEvent')
end

function BaseApi.InvokeListenCallBack(eid, fn, ...)
    if fn then
        fn(...)
    else
        EventApi.Task.StopEvent(eid)
    end
end

function BaseApi.Task.CreateWaitAlways()
    return EventApi.DoSharpApi('Async', 'DeepCore.GameEvent.Events.WaitAlwaysEvent')
end

function BaseApi.Task.CreateParallel()
    return EventApi.DoSharpApi('Async', 'DeepCore.GameEvent.Events.ParallelEvent')
end

function BaseApi.Task.CreateSelector(success_count)
    return EventApi.DoSharpApi('Async', 'DeepCore.GameEvent.Events.SelectorEvent', success_count)
end

function BaseApi.Task.CreateSequence()
    return EventApi.DoSharpApi('Async', 'DeepCore.GameEvent.Events.SequenceEvent')
end

function BaseApi.RandomPercent(...)
    return EventApi.DoSharpApi('Sync', 'DeepCore.GameEvent.Events.RandomPercentEvent', ...)
end

function BaseApi.RandomInteger(...)
    return EventApi.DoSharpApi('Sync', 'DeepCore.GameEvent.Events.RandomIntegerEvent', ...)
end

function BaseApi.RandomSingle()
    return EventApi.DoSharpApi('Sync', 'DeepCore.GameEvent.Events.RandomSingleEvent')
end

-- 周日为0
function BaseApi.DateTimeNow()
    return EventApi.DateTime()
end

function BaseApi.DateTime(time)
    local t
    if not time then
        t = os.date('*t')
    elseif type(time) == 'number' then
        t = os.date('*t',time)
    elseif type(time) == 'table' then
        t = time
    end
    return {
        Year = t.year,
        Month = t.month,
        Day = t.day,
        DayOfWeek = t.wday - 1,
        Hour = t.hour,
        Minute = t.min,
        Second = t.sec,
    }
end

function BaseApi.Listen.TodayTime(...)
    return EventApi.DoSharpApi('Listen', 'DeepCore.GameEvent.Events.TodayTimeEvent', ...)
end

function BaseApi.Listen.NewDay(...)
    return EventApi.DoSharpApi('Listen', 'DeepCore.GameEvent.Events.NewDayEvent', ...)
end

function BaseApi.Task.RunEvents(t, parent, etype)
    local ttype = type(t[1])
    local pfn
    local arg
    local stack_parents = {}
    local child_index = 1
    if ttype == 'table' then
        etype = 'Sequence'
    elseif ttype == 'string' then
        etype = t[1]
        child_index = 2
    elseif ttype == 'function' then
        --Sequence 至少延迟一帧
        if etype == 'Sequence' then
            EventApi.CallTo(parent, EventApi.Task.DelaySec, 0)
        end
        return EventApi.CallTo(parent, t[1], unpack(t, 2, table_MaxIndex(t)))
    else
        inner_error('not support ' .. ttype)
    end
    if etype == 'Sequence' then
        --默认为Sequence
        pfn = EventApi.Task.CreateSequence
    elseif etype == 'Selector' then
        pfn = EventApi.Task.CreateSelector
        if type(t[child_index + 1]) == 'number' then
            arg = t[child_index + 1]
            child_index = child_index + 1
        end
    elseif etype == 'Parallel' then
        pfn = EventApi.Task.CreateParallel
    else
        inner_error('not support ' .. etype)
    end
    local pid = parent and EventApi.CallTo(parent, pfn, arg) or pfn(arg)
    for i = child_index, #t do
        EventApi.Task.RunEvents(t[i], pid, etype)
    end
    return pid
end

function BaseApi.StopEvent(id, result, reason)
    if result == nil then
        result = true
    end
    reason = reason or 'StopEvent'
    local t = type(id)
    if t == 'string' then
        local eids = {EventApi.GetEventID(id)}
        for _, v in ipairs(eids) do
            return CurrentManager:StopEvent(v, result, reason)
        end
    elseif t == 'number' then
        return CurrentManager:StopEvent(id, result, reason)
    else
        warn('StopEvent but id == nil')
    end
end

function BaseApi.Task.StopEvent(id, result, reason)
    return EventApi.StopEvent(id, result, reason)
end

function BaseApi.ReStart()
    CurrentManager:ReStart()
end

function BaseApi.SetAllDirty()
    Sanbox.ClearCache()
end

local function CreateRpcInfo(parent, cate, rpc, ...)
    local info = {}
    info.Rpc = rpc
    info.ParentEvent = parent
    if string_ends(cate, 'Listen') then
        info.IsTriggerEvent = true
        local params, cb = SplitParamsAndCallBack(...)
        -- pprint('params ',rpc, params)
        -- pprint('cb ',type(cb))
        info.Arg = params
        if type(cb) == 'function' then
            info.CallBack = cb
        end
    else
        info.Arg = DynamicToArgTable(...)
    end
    return info
end

local function FixSharpResult(id, cate, rpc)
    if string_ends(cate, 'Sync') then
        local out = CurrentManager:GetEventOutput(id)
        if out.IsSuccess then
            if out.UnpackOutput then
                return ArgTableToDynamic(out.Output)
            else
                return out.Output
            end
        else
            inner_error(rpc .. ' not success ' .. (out.Output or ''))
        end
    else
        return id
    end
end

-- 所有和c#交互的api入口
function BaseApi.DoSharpApi(cate, rpc, ...)
    local parent = GetEnvParent()
    local info = CreateRpcInfo(parent, cate, rpc, ...)
    local id = CurrentManager:CallSharpApi(info)
    assert(id ~= 0)
    if parent then
        return id
    else
        return FixSharpResult(id, cate, rpc)
    end
end

function BaseApi.String2Function(apiName)
    local list = string_split(apiName, '.')
    local func = EventApi
    for _, v in ipairs(list) do
        if type(func) == 'table' then
            func = func[v]
        else
            break
        end
    end
    if type(func) == 'function' then
        return func, list[#list - 1], list[#list]
    end
end

--! @brief 监听一个事件的Trigger，触发时会添加一个新事件来执行此次触发
--! @param eid 事件ID
--! @param fn 监听方法
function BaseApi.Listen.ListenEvent(eid, fn)
    local id = EventApi.Task.CreateWaitAlways()
    CurrentManager:ListenEvent(
        eid,
        function(...)
            if fn then
                fn(...)
            else
                EventApi.SetEventOutput(id, ...)
                Api.Task.StopEvent(id)
            end
        end
    )
    return id
end

-- todo 是否保留？
function BaseApi.Listen.RemoveEventListen(eid)
    CurrentManager:RemoveEventListen(eid)
end

function BaseApi.TriggerEvent(id, ...)
    CurrentManager:TriggerLuaEvent(id, DynamicToArgTable(...))
end

function BaseApi.Task.Wait(id)
    local waitResult
    if not id then
        waitResult = CurrentManager:WaitAll()
    else
        waitResult = CurrentManager:Wait(id)
    end

    local success = false
    if waitResult then
        success = coroutine.yield()
    elseif not EventApi.IsEventStoped(id) then
        error('can not use wait')
    end

    if id then
        local out = CurrentManager:GetEventOutput(id)
        if not out then
            return success
        end
        if out.IsSuccess and out.UnpackOutput then
            return out.IsSuccess, ArgTableToDynamic(out.Output)
        else
            return out.IsSuccess, out.Output
        end
    else
        return success
    end
end

function BaseApi.Task.WaitAny(...)
    local params = {...}
    if type(params[1]) == 'table' then
        params = params[1]
    end
    local waitResult = CurrentManager:WaitAny(params)
    if not waitResult then
        error('cannot use Wait or Sleep')
        return
    end
    local success, eventID = coroutine.yield()
    if eventID ~= 0 then
        local out = CurrentManager:GetEventOutput(eventID)
        if out.IsSuccess and out.UnpackOutput then
            return success, eventID, ArgTableToDynamic(out.Output)
        else
            return success, eventID, out.Output
        end
    else
        return success
    end
end

function BaseApi.Task.WaitSelect(...)
    local params = {...}
    if type(params[1]) == 'table' then
        params = params[1]
    end
    local waitResult = CurrentManager:WaitSelect(params)
    if not waitResult then
        error('cannot use Wait or Sleep')
        return
    end
    local success, eventID = coroutine.yield()
    if eventID ~= 0 then
        local out = CurrentManager:GetEventOutput(eventID)
        if out.IsSuccess and out.UnpackOutput then
            return success, eventID, ArgTableToDynamic(out.Output)
        else
            return success, eventID, out.Output
        end
    else
        return success
    end
end

function BaseApi.Task.WaitParallel(...)
    local params = {...}
    if type(params[1]) == 'table' then
        params = params[1]
    end
    if #params == 0 then
        return
    end
    local waitResult = CurrentManager:WaitParallel(params)
    if not waitResult then
        error('cannot use Wait or Sleep')
        return
    end
    local success, eventID = coroutine.yield()
    local results = {}
    for _, v in ipairs(params) do
        local out = CurrentManager:GetEventOutput(v)
        table.insert(results, out or {IsSuccess = true})
    end
    return success, results
end

function BaseApi.UnpackOutput(out)
    if not out.IsSuccess then
        return
    end
    if out.UnpackOutput then
        return ArgTableToDynamic(out.Output)
    else
        return out.Output
    end
end

function BaseApi.IsEventStoped(id)
    return CurrentManager:IsEventStoped(id)
end

function BaseApi.IsEventSuccess(id)
    return CurrentManager:IsEventSuccess(id)
end

function BaseApi.Task.Sleep(sec)
    EventApi.Task.Wait(EventApi.Task.DelaySec(sec))
end

function BaseApi.Task.ContinueWith(id, fn)
    return CurrentManager:ContinueWith(id, fn)
end

function BaseApi.AddCacheData(...)
    return _total_global.blackBoard:Add(...)
end

function BaseApi.GetCacheData(...)
    return _total_global.blackBoard:Get(...)
end

function BaseApi.RemoveCacheData(...)
    return _total_global.blackBoard:Remove(...)
end

function BaseApi.FindCacheData(...)
    return _total_global.blackBoard:Find(...)
end

function BaseApi.GetAllCacheData()
    return _total_global.blackBoard:GetAll()
end

function BaseApi.Task.AddEventTo(eid, scriptName, ...)
    local ret = CreateEventTable(scriptName, ...)
    if ret then
        local eid = CurrentManager:AddLuaEventTo(eid, ret)
        assert(eid ~= 0)
        return eid
    end
end

function BaseApi.Task.AddChunkTo(eid, chunk)
    local ret = CreateTrunkEventTable(chunk)
    if ret then
        local eid = CurrentManager:AddLuaEventTo(eid, ret)
        assert(eid ~= 0)
        return eid
    end
end

function BaseApi.Task.AddEvent(scriptName, ...)
    local pid = GetEnvParent()
    if pid then
        return EventApi.Task.AddEventTo(pid, scriptName, ...)
    end
    local ret = CreateEventTable(scriptName, ...)
    if ret then
        local eid = CurrentManager:AddLuaEvent(ret)
        assert(eid ~= 0)
        return eid
    end
end

function BaseApi.Task.AddChunk(chunk)
    local pid = GetEnvParent()
    if pid then
        return EventApi.Task.AddChunkTo(pid, chunk)
    end
    local ret = CreateTrunkEventTable(chunk)
    if ret then
        local eid = CurrentManager:AddLuaEvent(ret)
        assert(eid ~= 0)
        return eid
    end
end

-- 此模式下，同步接口也返回ID
function BaseApi.CallTo(eid, func, ...)
    SetEnvParent(eid)
    local ret = func(...)
    SetEnvParent(nil)
    return ret
end

function BaseApi.Task.StartChunk(chunk)
    local ret = CreateTrunkEventTable(chunk)
    if ret then
        local eid = CurrentManager:StartLuaEvent(ret)
        assert(eid ~= 0)
        return eid
    end
end

function BaseApi.Task.StartEvent(scriptName, ...)
    local ret = CreateEventTable(scriptName, ...)
    if ret then
        local eid = CurrentManager:StartLuaEvent(ret)
        assert(eid ~= 0)
        return eid
    end
end

function BaseApi.SetEventOutput(id, ...)
    CurrentManager:SetEventOutput(id, DynamicToArgTable(...))
end

function BaseApi.GetEventOutput(eid)
    local out = CurrentManager:GetEventOutput(eid)
    if out.IsSuccess then
        if out.UnpackOutput then
            return ArgTableToDynamic(out.Output)
        else
            return out.Output
        end
    end
end

function BaseApi.GetEventID(scriptName)
    local ret = {}
    for id, v in pairs(_total_global._ids or {}) do
        if v == scriptName then
            table.insert(ret, id)
        end
    end
    if table_IsArray(ret) then
        return unpack(ret)
    else
        return ret
    end
end

function BaseApi.GetRunningEvents()
    return copy_table(_total_global._ids)
end

function BaseApi.CreateRemoteApi(managerName, uuid)
    return CreateManagerApi(managerName, uuid)
end

function BaseApi.CreateBroadcastApi(...)
    return CreateBroadcastApi(...)
end

function BaseApi.ReStart()
    CurrentManager:ReStart()
end

function BaseApi.GetCurrentConfig()
    local config = GetConfig(CurrentManager.Name)
    return copy_table(config)
end

function BaseApi.IsLocolManager(managerName, uuid)
    return IsLocolManager(managerName, uuid)
end

function BaseApi.Task.WaitAlways()
    local id = EventApi.Task.CreateWaitAlways()
    EventApi.Task.Wait(id)
end

function BaseApi.GetCurrentEventID()
    return CurrentManager:GetCurrentEventID()
end

function BaseApi.GetParentEventID(id)
    return CurrentManager:GetParentEventID(id)
end

function BaseApi.GetRootEventID(id)
    return CurrentManager:GetRootEventID(id)
end

function BaseApi.DynamicToArgTable(...)
    return DynamicToArgTable(...)
end

function BaseApi.ArgTableToDynamic(arg)
    return ArgTableToDynamic(arg)
end

function BaseApi.RepackArgTabel(arg, index)
    return RepackArgTabel(arg, index)
end

function BaseApi.GetEventSandbox(id)
    return CurrentManager:GetEventSandbox(id)
end

function BaseApi.GetXlsFullData(tb_name)
    return CurrentManager:GetDataFullTable(tb_name)
end

function BaseApi.GetXlsData(tb_name, key)
    return CurrentManager:GetDataTable(tb_name, key)
end

function BaseApi.GetXlsFileBytes(tb_name)
    return CurrentManager:GetDataFullBytes(tb_name)
end

local is_beforestop = false
-- todo 命名修改 ，修改成State作为字段存在于EventApi中
function BaseApi.IsEventManagerBeforeStop()
    return is_beforestop
end
------------------------------------------------------------------
------------------------- Use in csharp---------------------------
local function ServerEventLogic(func, functype, funcname, ...)
    if functype == 'Task' or functype == 'Listen' then
        -- print('ServerEventLogic ',functype, funcname)
        local eid = func(...)
        if not eid or eid == 0 then
            return false, 'create failed: ' .. funcname
        end
        if not string_starts(funcname, 'StartEvent') and not string_starts(funcname, 'StartChunk') then
            CurrentManager:ListenEvent(
                eid,
                function(...)
                    local fatherID = EventApi.GetParentEventID(eid)
                    if fatherID and fatherID ~= 0 then
                        EventApi.TriggerEvent(fatherID, ...)
                    end
                end
            )
        end
        return EventApi.Task.Wait(eid)
    else
        return true, func(...)
    end
end

function CreateServerEventTable(apiName, arg)
    local func, functype, funcname = EventApi.String2Function(apiName)
    if not func then
        error('call error ' .. apiName)
        return nil
    end
    local eventTable = CreateEventTable(ServerEventLogic, func, functype, funcname, ArgTableToDynamic(arg))
    if eventTable then
        return eventTable
    end
end

--{address:EventApi}
local all_inits = {}
function OnEventManagerStop()
    -- print('OnEventManagerStop ')
    local config = GetConfig(CurrentManager.Name)
    for _, v in ipairs(config.OnStops or {}) do
        v()
    end
    _total_global = nil
    EventApi = nil
    all_inits = nil
end

function OnEventManagerBeforeStop()
    is_beforestop = true
end

function OnRootEventStop(eid)
    --todo cache管理
end

function SetCurrentEventManager(mgr, restart)
    CurrentManager = mgr
    is_beforestop = false
    if not _total_global.path then
        _total_global.path = CurrentManager.RootPath
    end
    if not _total_global.config then
        _total_global.config = require(CurrentManager.RootPath .. CurrentManager.Config)
    end
    EventApi = all_inits[CurrentManager.Address]
    if not EventApi then
        local config = GetConfig(CurrentManager.Name)
        if config.GenNameSpaceApi and not restart then
            print('LUA VERSION : ' .. _VERSION)
            for _, v in ipairs(config.GenNameSpaceApi) do
                CurrentManager:GenNamespaceApi(v.NameSpace, CurrentManager.RootPath .. v.FileName, v.Group)
            end
        end
        EventApi = CreateManagerApi(CurrentManager.Name, CurrentManager.UUID)
        all_inits[CurrentManager.Address] = EventApi
        for _, v in ipairs(config.OnStarts or {}) do
            v()
        end
        if config.InitScript then
            EventApi.Task.StartEvent(config.InitScript)
        end
    end
    -- print('SetCurrentEventManager',CurrentManager:GetType().FullName)
end

---------------------------------------------------------------------------
-----------------------------------------------------------------

--全局变量1 --> EventApi : 所有的Api
--全局变量2 --> _total_global
--! @}
