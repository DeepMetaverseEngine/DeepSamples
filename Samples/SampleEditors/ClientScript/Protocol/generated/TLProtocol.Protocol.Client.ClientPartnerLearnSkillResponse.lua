
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPartnerLearnSkillResponse


local _M = {MessageID = 0x00099016,Name = 'TLProtocol.Protocol.Client.ClientPartnerLearnSkillResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00099016] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPartnerLearnSkillResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutS32(data.s2c_partnerID)
	output:PutOBJ(data.s2c_skillData,'TLProtocol.Data.ClientPartnerSkillData')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_partnerID = input:GetS32()
	data.s2c_skillData = input:GetOBJ('TLProtocol.Data.ClientPartnerSkillData')
end


