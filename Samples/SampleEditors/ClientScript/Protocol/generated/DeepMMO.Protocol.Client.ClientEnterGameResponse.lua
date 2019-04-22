
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- DeepMMO.Protocol.Client.ClientEnterGameResponse


local _M = {MessageID = 0x0003300E,Name = 'DeepMMO.Protocol.Client.ClientEnterGameResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0003300E] = _M
Protocol.Serializer.StringDefined['DeepMMO.Protocol.Client.ClientEnterGameResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutDateTime(data.s2c_suspendTime)
	if data:IsSuccess()==true then
	output:PutOBJ(data.s2c_role,'DeepMMO.Data.ClientRoleData')
	end
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_suspendTime = input:GetDateTime()
	if data:IsSuccess()==true then
	data.s2c_role = input:GetOBJ('DeepMMO.Data.ClientRoleData')
	end
end

