--test
local Helper = require'Logic/Helper'

local BookData = {
	1 = {star = 3},
	2 = {star = 2}
}

local function GetStar(id)
	if BookData[id] == nil then
		return 0
	end
	return BookData[id].star
end

_M.GetStar = GetStar
return _M
