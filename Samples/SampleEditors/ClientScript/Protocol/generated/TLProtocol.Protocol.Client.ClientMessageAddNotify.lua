
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientMessageAddNotify


local _M = {MessageID = 0x00097003,Name = 'TLProtocol.Protocol.Client.ClientMessageAddNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00097003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientMessageAddNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutOBJ(data.s2c_data,'TLProtocol.Data.MessageSnap')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_data = input:GetOBJ('TLProtocol.Data.MessageSnap')
end


