
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientGetCDKeyRewardRequest


local _M = {MessageID = 0x0009D012,Name = 'TLProtocol.Protocol.Client.ClientGetCDKeyRewardRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009D012] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientGetCDKeyRewardRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutS32(data.c2s_PlatformID)
	output:PutUTF(data.c2s_CDkey)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_PlatformID = input:GetS32()
	data.c2s_CDkey = input:GetUTF()
end

