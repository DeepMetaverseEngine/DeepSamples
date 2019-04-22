
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientFunctionOpenNotify


local _M = {MessageID = 0x0009C10A,Name = 'TLProtocol.Protocol.Client.ClientFunctionOpenNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009C10A] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientFunctionOpenNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutMap(data.s2c_funList, output.PutUTF, output.PutU8,'string', 'byte')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_funList = input:GetMap(input.GetUTF, input.GetU8,'string', 'byte')
end


