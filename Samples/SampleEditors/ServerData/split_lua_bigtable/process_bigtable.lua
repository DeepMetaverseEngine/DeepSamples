function get_filelist(dirpath, deep, ret)
    local lfs = require 'lfs'
    ret = ret or {}
    dirpath = string.gsub(dirpath, '/$', '')
    for file in lfs.dir(dirpath) do
        if file ~= '.' and file ~= '..' then
            local f = dirpath .. '/' .. file
            local attr = lfs.attributes(f)
            if type(attr) == 'table' and attr.mode == 'directory' then
                if deep then
                    get_filelist(f .. '/', deep, ret)
                end
            else
                table.insert(ret, f)
            end
        end
    end
    return ret
end

function number2key(num)
    return '[' .. num .. ']'
end

function string2key(s)
    local begin = string.sub(s, 1, 1)
    if string.find(begin, '%a') then
        return s
    else
        return '[' .. s .. ']'
    end
end

function string2value(s)
    return '"' .. s .. '"'
end
function table.isarray(t)
    if not t or type(t) ~= 'table' then
        return false
    end
    local i = 0
    for k, v in pairs(t) do
        i = i + 1
        if not t[i] then
            return false
        end
    end
    return true
end

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

function pprint(...)
    local ret = get_print_string(true, ...)
    print(ret)
end

function table.tostring(t)
    local rets = {}
    local isarray = table.isarray(t)
    -- pprint(isarray, t)
    local itfn = isarray and ipairs or pairs
    for k, v in itfn(t) do
        local tkey = type(k)
        local tvalue = type(v)
        local valstr
        if tvalue == 'table' then
            valstr = table.tostring(v)
        elseif tvalue == 'string' then
            valstr = string2value(v)
        else
            valstr = tostring(v)
        end
        if isarray then
            table.insert(rets, string.format('%s', valstr))
        else
            local keystr
            if tkey == 'number' then
                keystr = number2key(k)
            elseif tkey == 'string' then
                keystr = string2key(k)
            elseif tkey == 'table' then
                keystr = table.tostring(k)
            else
                keystr = tostring(k)
            end
            table.insert(rets, string.format('%s = %s', keystr, valstr))
        end
    end
    return '{' .. table.concat(rets, ',') .. '}'
end

function string.ends(String, End)
    return End == '' or string.sub(String, -string.len(End)) == End
end

function string.starts(String, Start)
    return string.sub(String, 1, string.len(Start)) == Start
end

filelist = get_filelist(arg[1], true)

for _, v in ipairs(filelist) do
    if string.ends(v, '.lua') and not string.ends(v, 'response-code.lua') then
        print('convert file', v)
        -- local ret = loadfile(v)()
        -- local filelines = {}
        -- for k, v in pairs(ret) do
        --     table.insert(filelines, {key = k, value = v})
        -- end
        local filelines = {}
        local header_str = ''
        local chunk_index
        for line in io.lines(v) do
            if chunk_index then
                chunk_index = chunk_index + 1
                local first_char = string.sub(line, 1, 1)
                if first_char == '[' then
                    line = 'ret' .. line
                elseif first_char == '{' then
                    line = 'ret[' .. chunk_index .. '] = ' .. line
                else
                    line = nil
                end
                if line then
                    -- 去除逗号
                    line = string.gsub(line, ',$', '')
                    local tmp_env = {ret = {}}
                    if loadstring then
                        local fn = loadstring(line)
                        setfenv(fn, tmp_env)
                        fn()
                    else
                        load(line, nil, 't', tmp_env)()
                    end
                    local line_ret = {}
                    line_ret.key, line_ret.value = next(tmp_env.ret)
                    filelines[chunk_index] = line_ret
                end
            elseif not chunk_index and string.starts(line, 'return') then
                chunk_index = 0
            else
                header_str = header_str .. line .. '\n'
            end
        end
        if #filelines > 1000 then
            local file = io.open(v, 'wb')
            file:write(header_str)
            file:write('local ret = {}\n')
            local index = 0
            local fns = {}
            for i, v in ipairs(filelines) do
                if (i - 1) % 1001 == 0 then
                    if i ~= 1 then
                        file:write('end\n')
                    end
                    local fnstr = 'a_' .. math.floor(i / 1000) .. '()'
                    file:write(string.format('local function %s\n', fnstr))
                    table.insert(fns, fnstr)
                end
                if type(v.key) == 'string' then
                    v.key = '"' .. v.key .. '"'
                end
                file:write(string.format('\tret[%s] = %s\n', tostring(v.key), table.tostring(v.value)))
            end
            file:write('end\n')
            for _, v in ipairs(fns) do
                file:write(v .. '\n')
            end
            file:write('return ret')
            file:close()
        end
    end
end
