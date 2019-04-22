
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientTaiXuSweepRequest


local _M = {MessageID = 0x00040138,Name = 'TLProtocol.Protocol.Client.TLClientTaiXuSweepRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040138] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientTaiXuSweepRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutS32(data.c2s_groupid)
	output:PutS32(data.c2s_subid)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_groupid = input:GetS32()
	data.c2s_subid = input:GetS32()
end


