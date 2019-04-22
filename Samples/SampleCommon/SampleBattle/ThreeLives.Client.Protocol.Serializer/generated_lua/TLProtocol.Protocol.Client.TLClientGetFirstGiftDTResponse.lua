
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientGetFirstGiftDTResponse


local _M = {MessageID = 0x00040011,Name = 'TLProtocol.Protocol.Client.TLClientGetFirstGiftDTResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040011] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientGetFirstGiftDTResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutS32(data.s2c_giftid)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_giftid = input:GetS32()
end


