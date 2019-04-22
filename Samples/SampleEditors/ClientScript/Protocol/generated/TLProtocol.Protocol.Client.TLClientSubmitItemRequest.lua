
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientSubmitItemRequest


local _M = {MessageID = 0x00073003,Name = 'TLProtocol.Protocol.Client.TLClientSubmitItemRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00073003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientSubmitItemRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutList(data.c2s_data, output.PutOBJ,'TLProtocol.Data.SubmitItemData')
	output:PutS32(data.c2s_questId)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_data = input:GetList(input.GetOBJ,'TLProtocol.Data.SubmitItemData')
	data.c2s_questId = input:GetS32()
end


