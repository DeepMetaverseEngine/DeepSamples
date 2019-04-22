
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TalentData


local _M = {MessageID = 0x0006B603,Name = 'TLProtocol.Data.TalentData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0006B603] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TalentData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.level)
	output:PutS32(data.type)
	output:PutS32(data.openLevel)
end


function _M.Read(input,data)
		
	data.level = input:GetS32()
	data.type = input:GetS32()
	data.openLevel = input:GetS32()
end


