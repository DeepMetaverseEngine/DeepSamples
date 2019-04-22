
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientResetDTReponse


local _M = {MessageID = 0x0004000D,Name = 'TLProtocol.Protocol.Client.TLClientResetDTReponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0004000D] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientResetDTReponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutS32(data.s2c_resetTime)
	output:PutS32(data.s2c_curLayer)
	output:PutS32(data.s2c_maxLayer)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_resetTime = input:GetS32()
	data.s2c_curLayer = input:GetS32()
	data.s2c_maxLayer = input:GetS32()
end


