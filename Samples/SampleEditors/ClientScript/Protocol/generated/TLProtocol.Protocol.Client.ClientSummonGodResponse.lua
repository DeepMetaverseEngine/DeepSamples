
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientSummonGodResponse


local _M = {MessageID = 0x00099502,Name = 'TLProtocol.Protocol.Client.ClientSummonGodResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00099502] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientSummonGodResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutS32(data.s2c_god_id)
	output:PutS32(data.s2c_god_lv)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_god_id = input:GetS32()
	data.s2c_god_lv = input:GetS32()
end


