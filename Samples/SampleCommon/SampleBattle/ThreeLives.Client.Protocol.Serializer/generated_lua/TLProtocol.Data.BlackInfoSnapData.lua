
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.BlackInfoSnapData


local _M = {MessageID = 0x00050504,Name = 'TLProtocol.Data.BlackInfoSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00050504] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.BlackInfoSnapData'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['TLProtocol.Data.SocialBaseSnapData'].Write(output, data)
	
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['TLProtocol.Data.SocialBaseSnapData'].Read(input, data)
	
end


