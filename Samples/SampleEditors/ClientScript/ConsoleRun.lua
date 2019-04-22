print('this is console run2')

local function fullpath(path)
    return '/ui_edit/lua/' .. path
end

local function destroyLua(path)
    package.loaded[fullpath(path)] = nil
end

local function destroyUI(path)
    destroyLua(path)
    MenuMgr.Instance:CloseAllMenu()
    MenuMgr.Instance:ClearAllCacheUI(0)
end

local function getClass(className)
    local class = _G
    string.gsub(className, '([^.]+)', function(s)
        if class then
            class = class[s]
        end
    end)
    return class
end
-- local c = getClass('List<<Task, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null>>')
-- print(c)

local env = {
    fullpath   = fullpath,
    destroyLua = destroyLua,
    destroyUI  = destroyUI,
    getClass   = getClass,
    __index    = _G,
}
setmetatable(env, env)

local function dotest(path)
    local func = loadfile(fullpath(path))
    setfenv(func, env)
    func()
end



-- dotest('UI/Task/test')
dotest('UI/Friend/test')



-- local s = {}
-- for k,v in pairs(package.loaded) do
--     table.insert(s, "loaded   ")
--     table.insert(s, tostring(k))
--     table.insert(s, '\t')
--     table.insert(s, tostring(v))
--     table.insert(s,'\n')
-- end
-- print(table.concat(s))
-- print(11+'xxx'+nil)


-- local ListTaskProgress = _G.["List<<CommonRPG.Data.TaskProgress, CommonRPG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>"]
-- print(ListTaskProgress)

local function printG(t)
    local s = {}
    local n = 0
    for k,v in pairs(t) do
        -- table.insert(s, "loaded   ")
        table.insert(s, tostring(k))
        table.insert(s, '\t')
        local t, k = pcall(tostring, v)
        table.insert(s, tostring(k))
        table.insert(s,'\n')
        n = n + 1
        if n >= 100 then
            print(table.concat(s))
            s = {}
            n = 0
        end
    end
    print(table.concat(s))
end
-- printG()

