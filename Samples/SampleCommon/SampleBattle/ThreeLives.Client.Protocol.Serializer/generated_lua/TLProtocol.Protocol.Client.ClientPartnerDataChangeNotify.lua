
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPartnerDataChangeNotify


local _M = {MessageID = 0x00099007,Name = 'TLProtocol.Protocol.Client.ClientPartnerDataChangeNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00099007] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPartnerDataChangeNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutOBJ(data.s2c_data,'TLProtocol.Data.ClientPartnerData')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_data = input:GetOBJ('TLProtocol.Data.ClientPartnerData')
end


