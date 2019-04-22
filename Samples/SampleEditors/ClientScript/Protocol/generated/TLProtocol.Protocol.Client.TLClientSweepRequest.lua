
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientSweepRequest


local _M = {MessageID = 0x00040192,Name = 'TLProtocol.Protocol.Client.TLClientSweepRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040192] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientSweepRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutUTF(data.s2c_functionid)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.s2c_functionid = input:GetUTF()
end

