
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientGetMagicBoxInfoResponse


local _M = {MessageID = 0x00040016,Name = 'TLProtocol.Protocol.Client.TLClientGetMagicBoxInfoResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040016] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientGetMagicBoxInfoResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutMap(data.s2c_keyMap, output.PutS32, output.PutS32,'int', 'int')
	output:PutBool(data.s2c_havekeys)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_keyMap = input:GetMap(input.GetS32, input.GetS32,'int', 'int')
	data.s2c_havekeys = input:GetBool()
end


