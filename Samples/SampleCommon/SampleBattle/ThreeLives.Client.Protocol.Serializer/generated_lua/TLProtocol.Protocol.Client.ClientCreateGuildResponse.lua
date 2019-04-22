
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientCreateGuildResponse


local _M = {MessageID = 0x00098002,Name = 'TLProtocol.Protocol.Client.ClientCreateGuildResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00098002] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientCreateGuildResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.GuildResponse'].Write(output, data)
	output:PutOBJ(data.s2c_guildInfo,'TLProtocol.Protocol.Data.GuildSnapData')
	output:PutS32(data.s2c_position)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.GuildResponse'].Read(input, data)
	data.s2c_guildInfo = input:GetOBJ('TLProtocol.Protocol.Data.GuildSnapData')
	data.s2c_position = input:GetS32()
end


