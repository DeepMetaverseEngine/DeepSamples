
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- DeepMMO.Data.PropertyStruct


local _M = {MessageID = 0x00022004,Name = 'DeepMMO.Data.PropertyStruct'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00022004] = _M
Protocol.Serializer.StringDefined['DeepMMO.Data.PropertyStruct'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.key)
	output:PutUTF(data.value)
	output:PutS32(data.type)
end


function _M.Read(input,data)
		
	data.key = input:GetUTF()
	data.value = input:GetUTF()
	data.type = input:GetS32()
end


