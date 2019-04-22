
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientOpenGiftResponse


local _M = {MessageID = 0x0009802C,Name = 'TLProtocol.Protocol.Client.ClientOpenGiftResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009802C] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientOpenGiftResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.GuildResponse'].Write(output, data)
	output:PutOBJ(data.s2c_giftInfo,'TLProtocol.Protocol.Data.GuildGiftSnapData')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.GuildResponse'].Read(input, data)
	data.s2c_giftInfo = input:GetOBJ('TLProtocol.Protocol.Data.GuildGiftSnapData')
end

