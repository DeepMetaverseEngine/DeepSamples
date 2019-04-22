
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TLClientIncomingMailNotify


local _M = {MessageID = 0x00082004,Name = 'TLProtocol.Data.TLClientIncomingMailNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00082004] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TLClientIncomingMailNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutS32(data.s2c_new_mail_count)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_new_mail_count = input:GetS32()
end


