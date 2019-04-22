
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPutOffShelvesRequest


local _M = {MessageID = 0x000A0003,Name = 'TLProtocol.Protocol.Client.ClientPutOffShelvesRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x000A0003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPutOffShelvesRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutUTF(data.c2s_auctionId)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_auctionId = input:GetUTF()
end


