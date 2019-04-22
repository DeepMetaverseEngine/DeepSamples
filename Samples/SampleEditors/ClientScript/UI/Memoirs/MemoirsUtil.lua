--test
local Helper = require'Logic/Helper'
local MemoirsModel = require 'Model/MemoirsModel'
local _M = {}
_M.__index = _M
local function GetStar(id)
	local bookData = MemoirsModel.BookData[id]
	if bookData == nil then
		return 0
	end
	local BookLib = GlobalHooks.DB.Find('MemoirsData',id)
	local star = 0
	for i,v in ipairs(BookLib.quest_id) do
		for j,k in ipairs(bookData) do
			if k == v then
				star = star + BookLib.quest_star[i]
			end
		end
	end
	return star
end

local function GetContent(id)
	local bookdata = GlobalHooks.DB.Find('ThreeLivesBookData',id)
	if bookdata ~= nil then
		return bookdata
	end
	error("id is not exist",id)
	return nil
end

local function ChapterIsUnlocked(chapterid)
	local bookData = MemoirsModel.BookData[chapterid]
	if bookData == nil then
		return false
	else
		return true
	end
end

local function GetTaskHasDone(chapterid,questid)
	local bookData = MemoirsModel.BookData[chapterid]
	if bookData == nil then
		return false
	end
	for i,v in ipairs(bookData) do
		if v == questid then
			return true
		end
	end
end

local function GetUnlockNum()
	local num = 0
	for k,v in pairs(MemoirsModel.BookData) do
		num = num + 1
	end
	return num
end


_M.GetStar = GetStar
_M.GetContent = GetContent
_M.GetTaskHasDone = GetTaskHasDone
_M.ChapterIsUnlocked = ChapterIsUnlocked
_M.GetUnlockNum = GetUnlockNum
return _M
