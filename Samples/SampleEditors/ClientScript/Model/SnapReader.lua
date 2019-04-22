local _M = {}
_M.__index = _M

function _M.Get(self, key, cb)
    if not self._caches[key] then
        self:Load(
            key,
            function()
                cb(self._caches[key])
            end
        )
    else
        cb(self._caches[key])
    end
end

function _M.GetCaches(self, keys)
    local vals = {}
    for i, v in ipairs(keys) do
        table.insert(vals, self._caches[v] or false)
    end
    return vals
end

function _M.GetMany(self, keys, cb)
    local loadkeys = {}
    for i, v in ipairs(keys) do
        if not self._caches[v] then
            table.insert(loadkeys, v)
        end
    end
    if #loadkeys > 0 then
        self:LoadMany(
            loadkeys,
            function()
                cb(self:GetCaches(keys))
            end
        )
    else
        cb(self:GetCaches(keys))
    end
end

function _M.SetDirty(self, key)
    self._caches[key] = nil
end

function _M.Load(self, key, cb)
    self:LoadMany(
        {key},
        function(vals)
            cb(vals[1])
        end
    )
end

function _M.LoadMany(self, keys, cb)
    if not keys or #keys == 0 then
        cb({})
    else
        self._loadcb(
            keys,
            function(vals)
                for i, v in ipairs(keys) do
                    self._caches[v] = vals[i]
                end
                cb(vals)
            end
        )
    end
end

function _M.ContainsCache(self, key)
    if self._caches[key] then
        return true
    else
        return false
    end
end

function _M.Create(loadcb)
    local ret = setmetatable({}, _M)
    ret._loadcb = loadcb
    ret._caches = {}
    return ret
end

return _M
