
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Data.GuildTalentSnapData


local _M = {MessageID = 0x00087009,Name = 'TLProtocol.Protocol.Data.GuildTalentSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00087009] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Data.GuildTalentSnapData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.talentLv)
	output:PutMap(data.talentSkill, output.PutS32, output.PutS32,'int', 'int')
end


function _M.Read(input,data)
		
	data.talentLv = input:GetS32()
	data.talentSkill = input:GetMap(input.GetS32, input.GetS32,'int', 'int')
end

