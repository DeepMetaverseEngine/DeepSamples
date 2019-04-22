
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPartnerForgetSkillRequest


local _M = {MessageID = 0x00099017,Name = 'TLProtocol.Protocol.Client.ClientPartnerForgetSkillRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00099017] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPartnerForgetSkillRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutS32(data.c2s_partnerID)
	output:PutS32(data.c2s_skillID)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_partnerID = input:GetS32()
	data.c2s_skillID = input:GetS32()
end


