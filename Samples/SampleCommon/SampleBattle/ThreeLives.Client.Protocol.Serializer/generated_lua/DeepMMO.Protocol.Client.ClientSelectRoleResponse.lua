
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- DeepMMO.Protocol.Client.ClientSelectRoleResponse


local _M = {MessageID = 0x00033002,Name = 'DeepMMO.Protocol.Client.ClientSelectRoleResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00033002] = _M
Protocol.Serializer.StringDefined['DeepMMO.Protocol.Client.ClientSelectRoleResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	
end


