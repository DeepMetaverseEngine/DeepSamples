
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPartnerSkillRefineResponse


local _M = {MessageID = 0x00099014,Name = 'TLProtocol.Protocol.Client.ClientPartnerSkillRefineResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00099014] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPartnerSkillRefineResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutS32(data.s2c_lv)
	output:PutS32(data.s2c_skillID)
	output:PutS32(data.s2c_partnerID)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_lv = input:GetS32()
	data.s2c_skillID = input:GetS32()
	data.s2c_partnerID = input:GetS32()
end


