
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TLClientCreateRoleExtData


local _M = {MessageID = 0x00062002,Name = 'TLProtocol.Data.TLClientCreateRoleExtData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00062002] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TLClientCreateRoleExtData'] = _M

function _M.Write(output,data)
		
	output:PutEnum8(data.RolePro)
	output:PutEnum8(data.gender)
end


function _M.Read(input,data)
		
	data.RolePro = input:GetEnum8()
	data.gender = input:GetEnum8()
end

