-- circular buffer factory for lua


local function rotate_indice(i, n)
    return ((i - 1) % n) + 1
end

local circular_buffer = {}

circular_buffer.filled = function(self)
    return #(self.history) == self.max_length
end

circular_buffer.push = function(self, value)
    if self:filled() then
        local value_to_be_removed = self.history[self.oldest]
        self.history[self.oldest] = value
        self.oldest = self.oldest == self.max_length and 1 or self.oldest + 1
    else
        self.history[#(self.history) + 1] = value
    end
end

circular_buffer.metatable = {}

-- positive values index from newest to oldest (starting with 1)
-- negative values index from oldest to newest (starting with -1)
circular_buffer.metatable.__index = function(self, i)
    if i == "length" then return getmetatable(self).__len(self) end
    local history_length = #(self.history)
    if i == 0 or math.abs(i) > history_length then
        return nil
    elseif i >= 1 then
        local i_rotated = rotate_indice(self.oldest - i, history_length)
        return self.history[i_rotated]
    elseif i <= -1 then
        local i_rotated = rotate_indice(i + 1 + self.oldest, history_length)
        return self.history[i_rotated]
    end
end

circular_buffer.metatable.__len = function(self)
    return #(self.history)
end

circular_buffer.new = function(self, max_length)
    if type(max_length) ~= 'number' or max_length <= 1 then
        error("Buffer length must be a positive integer")
    end

    local instance = {
        history = {},
        oldest = 1,
        max_length = max_length,
        push = circular_buffer.push,
        filled = circular_buffer.filled,
    }
    setmetatable(instance, circular_buffer.metatable)
    return instance
end

return circular_buffer


-- for i=#r < need and #r or need,1,-1 do print(r[i]) end
