
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- DeepMMO.Data.ZonePosition


local _M = {MessageID = 0x0002F001,Name = 'DeepMMO.Data.ZonePosition'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0002F001] = _M
Protocol.Serializer.StringDefined['DeepMMO.Data.ZonePosition'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.flagName)
	output:PutF32(data.x)
	output:PutF32(data.y)
end


function _M.Read(input,data)
		
	data.flagName = input:GetUTF()
	data.x = input:GetF32()
	data.y = input:GetF32()
end


