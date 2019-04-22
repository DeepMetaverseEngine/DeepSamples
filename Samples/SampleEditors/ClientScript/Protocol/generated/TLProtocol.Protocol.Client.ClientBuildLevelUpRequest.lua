
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientBuildLevelUpRequest


local _M = {MessageID = 0x0009801F,Name = 'TLProtocol.Protocol.Client.ClientBuildLevelUpRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009801F] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientBuildLevelUpRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutS32(data.c2s_buildId)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_buildId = input:GetS32()
end

