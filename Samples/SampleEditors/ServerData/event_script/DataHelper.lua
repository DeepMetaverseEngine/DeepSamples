local db
local merger_dbs = {}
local DataHelper = {}
local DataTag = {}
local RootPath
local LocalVersions = {}
function DataHelper.SetRootPath(root)
    RootPath = root
end

function DataHelper.ClearCache()
    db = {}
end

function DataHelper.SetDataTag(tag_map)
	DataTag = tag_map
end

function DataHelper.GetPath(tb_name)
    local tbpath = DataTag[tb_name] or tb_name
    assert(type(tbpath) == 'string')
    return tbpath
end

function DataHelper.GetVersion(tb_name)
    local tbpath = DataTag[tb_name] or tb_name
    assert(type(tbpath) == 'string')
    return  LocalVersions['/'..tbpath..'.lua']
end

local function CheckInitDB()
	if not db then
		db = {}
        local versions = require(RootPath..'_luaversion_')
        -- print_r('versions',versions)
        LocalVersions = versions
	end
end

function DataHelper.SetDataTable(tb_name,data,version)
    local tbpath = DataTag[tb_name] or tb_name
    assert(type(tbpath) == 'string')
    CheckInitDB()
    if not db[tbpath] then
        db[tbpath] = {__data__ = data}
    else
        db[tbpath].__data__ = data
    end
    LocalVersions['/'..tbpath..'.lua'] = version
end

local function GetDataTable(tb_name)
    CheckInitDB()
	local tbpath = DataTag[tb_name] or tb_name
    local tb = merger_dbs[tb_name] or db[tbpath]
    if not tb then
        local pathtype = type(tbpath)
        if pathtype == 'table' then
            tb = {__merger__ = {}}
            for _, v in ipairs(tbpath) do
                local ok, ret = pcall(require, RootPath .. v)
                if ok then
                    table.insert(tb.__merger__, v)
                    db[v] = {__data__ = ret}
                end
                print(tbpath, 'load', ok)
            end
            merger_dbs[tb_name] = tb
        elseif pathtype == 'string' then
            local ok, ret = pcall(require, RootPath .. tbpath)
            print(tbpath, 'load', ok)
            if ok then
                tb = {__data__ = ret}
                db[tbpath] = tb
            end
        end
    else
        -- print('hit cache --',tb_name)
    end
    return tb
end

local function TryFindMergeTb(tb, find_key)
    local all_ret = {}
    local key_ret
    local is_key = type(find_key) ~= 'table'
    for _, v in ipairs(tb.__data__) do
        local ret = DataHelper.Find(v, find_key)
        if is_key then
            if ret then
                key_ret = ret
                break
            end
        elseif #ret > 0 then
            for _, v in ipairs(ret) do
                table.insert(all_ret, v)
            end
        end
    end
    if is_key then
        return key_ret
    else
        return all_ret
    end
end

local function src_table_find(tb, find_key)
    if not find_key or not (tb._key_ or tb[1]) then
        return
    end

    local function get_key(index)
        if tb._key_ then
            return tb._key_[index]
        else
            return tb[1][index]
        end
    end

    local function gen_table(arr)
        local ret = {}
        --print_r(arr)
        local indexed
        for i, v in ipairs(arr) do
            local key = get_key(i)
            if key then
                ret[key] = v
            end
            indexed = i
        end
        if indexed < #arr then
            --参数数量不一致，检查
            local p = 1
            for k,v in pairs(arr) do
                if k ~= p then
                    local key = get_key(p)
                    error(string.format('%s:%s -- %s is nil', tag,find_key, key))
                end
                p = p + 1
            end
        end
        return ret
    end

    local function check(key, v, t)
        local c = t[key]
        if c then
            if type(c) == 'function' then
                if not c(v) then
                    return false
                end
            elseif c ~= v then
                return false
            end
        end
        return true
    end

    if type(find_key) == 'table' then
        local ret = {}
        for k, v in pairs(tb) do
            if (tb._key_ and k ~= '_key_') or (not tb._key_ and k > 1) then
                local check_ok = true
                for i, vv in ipairs(v) do
                    local key = get_key(i)
                    if not check(key, vv, find_key) then
                        check_ok = false
                        break
                    end
                end
                if check_ok then
                    table.insert(ret, gen_table(v))
                end
            end
        end
        return ret
    elseif type(find_key) == 'function' then
        local ret = {}
        for k, v in pairs(tb) do
            if (tb._key_ and k ~= '_key_') or (not tb._key_ and k > 1) then
                local check_t = gen_table(v)
                if find_key(check_t) then
                    table.insert(ret, check_t)
                end
            end
        end
        return ret
    elseif tb[find_key] then
        local tmp = tb[find_key]
        if tmp then
            return gen_table(tmp)
        end
    else
        return nil
    end
end

--【普通查找】find_key为table: {查找字段=值/函数(返回true表示满足条件),查找字段=...}
--,返回满足条件的集合,为空表{}则返回整表
--【遍历查找】find_key为function: 逐条校验，返回true表示满足条件,返回满足条件的集合
--【key查找】 find_key为整数或者字符串,返回满足条件的唯一值
function DataHelper.Find(tb_name, find_key)
    local tb = GetDataTable(tb_name)
    if not tb then return end
    -- print('find ', tb, tb and tb.__merger__, find_key,tb.__data__)
    if tb.__merger__ then
        local all_ret = {}
        local key_ret
        local is_key = type(find_key) ~= 'table'
        for _,v in ipairs(tb.__merger__) do
            local ret = src_table_find(db[v].__data__, find_key)
            if is_key then
                if ret then
                    key_ret = ret
                    break
                end
            elseif #ret > 0 then
                for _, vv in ipairs(ret) do
                    table.insert(all_ret, vv)
                end
            end
        end
        if is_key then
            return key_ret
        else
            return all_ret
        end
    else
        return src_table_find(tb.__data__, find_key)
    end
end

function DataHelper.FindFirst(tb_name, find_key)
    local ret = DataHelper.Find(tb_name, find_key)
    if type(find_key) == 'table' then
        return ret and ret[1]
    else
        return ret
    end
end

function DataHelper.GetFullTable(tb_name)
    return DataHelper.Find(tb_name, {})
end

return DataHelper
