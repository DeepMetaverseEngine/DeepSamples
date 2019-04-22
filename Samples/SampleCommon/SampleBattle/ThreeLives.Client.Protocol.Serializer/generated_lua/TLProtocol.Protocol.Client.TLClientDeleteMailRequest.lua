
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientDeleteMailRequest


local _M = {MessageID = 0x00093003,Name = 'TLProtocol.Protocol.Client.TLClientDeleteMailRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00093003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientDeleteMailRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutBool(data.c2s_delete_all)
	output:PutList(data.c2s_remove_uuid_list, output.PutUTF,'string')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_delete_all = input:GetBool()
	data.c2s_remove_uuid_list = input:GetList(input.GetUTF,'string')
end

