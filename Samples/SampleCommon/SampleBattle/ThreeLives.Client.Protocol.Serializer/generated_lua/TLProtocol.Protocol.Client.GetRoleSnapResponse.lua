
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.GetRoleSnapResponse


local _M = {MessageID = 0x00037102,Name = 'TLProtocol.Protocol.Client.GetRoleSnapResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00037102] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.GetRoleSnapResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutArray(data.s2c_data, output.PutOBJ,'TLProtocol.Data.TLClientRoleSnap')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_data = input:GetArray(input.GetOBJ,'TLProtocol.Data.TLClientRoleSnap')
end


