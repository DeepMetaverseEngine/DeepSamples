-- File: F9.lua
-- Author: Ying Zuo
local function split(str,sep)
    local sep, fields = sep or ":", {}
    local pattern = string.format("([^%s]+)", sep)
    str:gsub(pattern, function(c) fields[#fields+1] = c end)
    return fields
end

function main()
	local path = '..\\..\\..\\GameData\\GameEditors\\ClientScript\\design'
	print("path"..path)
	
	local comand = 'dir /s /b '..path
	local s = io.popen(comand)
	print(comand)
	local file_lists = s:read('*all')
	s:close()
	file_lists = split(file_lists,'\n')
	local ret = {}
	print_r(file_lists)
	for _, v in ipairs(file_lists) do
		local find_index = string.find(v,'.lua',1,true)
		if find_index then
			local file_spr = split(v,'\\')
			local file_name = file_spr[#file_spr]
			file_name = split(file_name,'.')[1]
			local abc = tonumber(file_name)

			local file_path = file_spr[#file_spr-1]..'/'..file_spr[#file_spr]
			local check_chunk = split(file_name,'_')
			local cur_str
			if #check_chunk > 1 and check_chunk[1] == 'quest' and tonumber(check_chunk[2]) then
				local num = check_chunk[2]
				num = string.sub(num,1,1)		
				cur_str = string.format('%s = {%d,"%s",',file_name,num,file_path)
			else
				-- 默认单例
				if not abc then
					cur_str = string.format("%s = {3,'%s',",file_name,file_path)
				else
					cur_str = string.format("[%s] = {3,'%s',",file_name,file_path)
				end
			end
			cur_str = cur_str..'},\n'
			table.insert(ret,cur_str)
		end
	end
	local file_path = path..'\\file_list.lua'
	local f = io.open(file_path,'w')
	if f then
		f:write('--文件名称 类型\n')
		f:write('return {\n')
		f:write(unpack(ret))
		f:write('}')
		f:close()
	end
end
