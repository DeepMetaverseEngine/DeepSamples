
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.ExpireDay


local _M = {MessageID = 0x0006C603,Name = 'TLProtocol.Data.ExpireDay'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0006C603] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.ExpireDay'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.last_time)
	output:PutDateTime(data.endDate)
end


function _M.Read(input,data)
		
	data.last_time = input:GetS32()
	data.endDate = input:GetDateTime()
end


